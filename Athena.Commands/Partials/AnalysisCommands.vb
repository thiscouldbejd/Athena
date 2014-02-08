Imports Athena.Analysis
Imports Athena.Core
Imports Leviathan.Commands
Imports Leviathan.Commands.StringCommands
Imports Leviathan.Configuration
Imports Leviathan.Visualisation
Imports IT = Leviathan.Visualisation.InformationType
Imports VL = Leviathan.Commands.VerbosityLevel
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Text
Imports C = System.Data.CommandType

Partial Public Class AnalysisCommands

	#Region " Private Variables "

	#End Region

	#Region " Private Properties "

		Private ReadOnly Property Enforce_Restrictions As Boolean
			Get
				Return Not Restrict_Days Is Nothing AndAlso Restrict_Days.Length > 0
			End Get
		End Property

		Private ReadOnly Property Enforce_Ignoring As Boolean
			Get
				Return Not Ignore_Options Is Nothing AndAlso Ignore_Options.Length > 0
			End Get
		End Property

	#End Region

	#Region " Private Methods "

		Private Function Is_Option_Restricted( _
			ByVal value As [Option] _
		) As Boolean

			For i As Integer = 0 To Ignore_Options.Length - 1

				If value.Name.ToLower().IndexOf(Ignore_Options(i).ToLower()) >= 0 Then Return True

			Next

			Return False

		End Function

		Private Function Is_Action_Restricted( _
			ByVal value As Action _
		) As Boolean

			Dim check_Time As Boolean = False

			Select Case value.Created.DayOfWeek
				Case DayOfWeek.Monday
					If Array.IndexOf(Restrict_Days, "Mon") > =0 OrElse Array.IndexOf(Restrict_Days, "MON") >= 0 OrElse Array.IndexOf(Restrict_Days, "mon") >= 0 Then check_Time = True
				Case DayOfWeek.Tuesday
					If Array.IndexOf(Restrict_Days, "Tue") > =0 OrElse Array.IndexOf(Restrict_Days, "TUE") >= 0 OrElse Array.IndexOf(Restrict_Days, "tue") >= 0 Then check_Time = True
				Case DayOfWeek.Wednesday
					If Array.IndexOf(Restrict_Days, "Wed") > =0 OrElse Array.IndexOf(Restrict_Days, "WED") >= 0 OrElse Array.IndexOf(Restrict_Days, "wed") >= 0 Then check_Time = True
				Case DayOfWeek.Thursday
					If Array.IndexOf(Restrict_Days, "Thu") > =0 OrElse Array.IndexOf(Restrict_Days, "THU") >= 0 OrElse Array.IndexOf(Restrict_Days, "thu") >= 0 Then check_Time = True
				Case DayOfWeek.Friday
					If Array.IndexOf(Restrict_Days, "Fri") > =0 OrElse Array.IndexOf(Restrict_Days, "FRI") >= 0 OrElse Array.IndexOf(Restrict_Days, "fri") >= 0 Then check_Time = True
				Case DayOfWeek.Saturday
					If Array.IndexOf(Restrict_Days, "Sat") > =0 OrElse Array.IndexOf(Restrict_Days, "SAT") >= 0 OrElse Array.IndexOf(Restrict_Days, "sat") >= 0 Then check_Time = True
				Case DayOfWeek.Sunday
					If Array.IndexOf(Restrict_Days, "Sun") > =0 OrElse Array.IndexOf(Restrict_Days, "SUN") >= 0 OrElse Array.IndexOf(Restrict_Days, "sun") >= 0 Then check_Time = True
			End Select

			If check_Time Then

				If value.Created.TimeOfDay >= Time_Start.TimeOfDay AndAlso value.Created.TimeOfDay <= Time_End.TimeOfDay Then
					Return True
				Else
					Return False
				End If

			Else
				
				Return False

			End If

		End Function

		Private Function Get_Actions( _
			ByVal for_Instance As Instance _
		) As List(Of Action)

			Dim actions As List(Of Action) = Action.All.Filter( _
				Data.Connection(Athena_Source), Action.All.Filters.INSTANCE, for_Instance.Id).ToList()

			If Enforce_Restrictions Then

				Dim last As Integer = actions.Count - 1

				For i As Integer = 0 To last

					If i > last Then Exit For

					If Is_Action_Restricted(actions(i)) Then

						actions.RemoveAt(i)
						i -= 1
						last -= 1

					End If
				
				Next

			End If

			actions.Sort()

			Return actions

		End Function

		Private Sub Analyse_Actions( _
			ByVal actions As List(Of Action), _
			ByRef results As SortedDictionary(Of Int64, SortedDictionary(Of Int64, List(Of Timespan))) _
		)

			If Host.Available(VL.Debug) Then

				Dim str_Events As String = Nothing

				For i As Integer = 0 To actions.Count - 1

				
					If Not String.IsNullOrEmpty(str_Events) Then str_Events = str_Events & " "
					str_Events = str_Events & actions(i).Event

				Next

				Host.Debug(str_Events)

			End If

			Dim processed_Parent_Events As New List(of Int64)
			
			For i As Integer = 0 To actions.Count - 1

				If Not processed_Parent_Events.Contains(actions(i).Event) Then

					processed_Parent_Events.Add(actions(i).Event)

					Dim processed_Child_Events As New List(of Int64)

					For j As Integer = (i + 1) To actions.Count - 1

						If actions(i).Event <> actions(j).Event AndAlso Not processed_Child_Events.Contains(actions(j).Event) _
							AndAlso actions(j).Created > actions(i).Created AndAlso Not processed_Parent_Events.Contains(actions(j).Event) Then

							processed_Child_Events.Add(actions(j).Event)

							If Not results.ContainsKey(actions(i).Event) Then _
								results.Add(actions(i).Event, New SortedDictionary(Of Int64, List(Of Timespan)))

							If Not results(actions(i).Event).ContainsKey(actions(j).Event) Then _
								results(actions(i).Event).Add(actions(j).Event, New List(Of Timespan))

							results(actions(i).Event)(actions(j).Event).Add(actions(j).Created.Subtract(actions(i).Created))

						End If

					Next

				End If

			Next

		End Sub

		Private Function Analyse_Exercise( _
			ByVal value As Exercise, _
			Optional ByVal completion_Weighting As Integer = -1 _
		) As IFixedWidthWriteable()

			Dim all_Results As New List(Of Result)
			Dim results_Rows As New List(Of Row)
			Dim summary_Rows As New List(Of Row)
			Dim differencing_Rows As New List(Of Row)
			Dim all_Events As New Dictionary(Of Int64, [Event])
			Dim final_Events As New Dictionary(Of Int64, Int32)
			Dim final_Event_Count As Int32 = 0
			Dim final_Event As Int64 = -1
			Dim edge_List As New List(of String)
			Dim value_List As New List(Of List(Of Double))
			Dim it_List As New List(Of List(Of Leviathan.Visualisation.InformationType))
			Dim potential_Weighting As Integer = 0
			Dim actual_Weighting As Integer = 0
			Dim display_Round As Integer = 2
			Dim analysis_Span As SpanType = SpanType.Hours

			For Each single_Option As [Option] In [Option].All.Filter( _
				Data.Connection(Athena_Source), [Option].All.Filters.EXERCISE, value.Id).ToList()

				If Not Enforce_Ignoring OrElse Not Is_Option_Restricted(single_Option) Then

					Dim new_Result As New Result()
					new_Result.Name = single_Option.Name

					Dim option_Results As New SortedDictionary(Of Int64, SortedDictionary(Of Int64, List(Of Timespan)))

					Dim event_Count As New SortedDictionary(Of Int64, Integer)
					Dim event_Earliest As New SortedDictionary(Of Int64, DateTime)
					Dim event_Latest As New SortedDictionary(Of Int64, DateTime)
					Dim event_Totals As New SortedDictionary(Of Int64, Double)

					Dim all_Instances As List(Of Instance) = Instance.All.Filter( _
						Data.Connection(Athena_Source), Instance.All.Filters.OPTION, single_Option.Id).ToList()

					For Each single_Instance As Instance In all_Instances

						Dim actions As List(Of Action) = Get_Actions(single_Instance)

						Dim processed_Events As New List(of Int64)

						For Each act As Action In actions

							If Not all_Events.ContainsKey(act.Event) Then

								Dim single_Event As [Event] = [Event].All.One(Data.Connection(Athena_Source), act.Event)
								all_Events.Add(single_Event.Id, single_Event)
								event_Count.Add(single_Event.Id, 0)

							End If

							If Not event_Count.ContainsKey(act.Event) Then event_Count.Add(act.Event, 1) Else event_Count(act.Event) += 1

							If Not processed_Events.Contains(act.Event) Then

								If Not event_Earliest.ContainsKey(act.Event) Then
									event_Earliest.Add(act.Event, act.Created)
									event_Latest.Add(act.Event, act.Created)
								Else
									If event_Earliest(act.Event) > act.Created Then event_Earliest(act.Event) = act.Created
									If event_Latest(act.Event) < act.Created Then event_Latest(act.Event) = act.Created
								End If

								processed_Events.Add(act.Event)

							End If

							If Not event_Totals.ContainsKey(act.Event) Then
								event_Totals.Add(act.Event, act.Created.Ticks)
							Else
								event_Totals(act.Event) += act.Created.Ticks
							End If 
							
						Next

						If processed_Events.Count > 0 Then

							If Not final_Events.ContainsKey(processed_Events(processed_Events.Count - 1)) Then _
								final_Events.Add(processed_Events(processed_Events.Count - 1), 0)
							final_Events(processed_Events(processed_Events.Count - 1)) += 1

							Analyse_Actions(actions, option_Results)

						End If

					Next

					For Each key As Int64 In final_Events.Keys

						If final_Events(key) >= final_Event_Count Then
							final_Event_Count = final_Events(key)
							final_Event = key
						End If

					Next

					For i As Integer = 0 To event_Count.Keys.Count - 1

						Dim new_Node As New Node()
						new_Node.Name = all_Events(event_Count.Keys(i)).Name
						If final_Event = event_Count.Keys(i) Then new_Node.Name = new_Node.Name & "*"
						new_Node.Count = event_Count(event_Count.Keys(i))
						new_Node.Completion = Math.Min(new_Node.Count / all_Instances.Count, 1)
						new_Node.Earliest = event_Earliest(event_Count.Keys(i))
						new_Node.Latest = event_Latest(event_Count.Keys(i))
						new_Node.Average = New System.DateTime(Convert.ToInt64(event_Totals(event_Count.Keys(i)) / new_Node.Count))

						' Work Out Best Timespan to Use!

						If final_Event = event_Count.Keys(i) AndAlso i > 0 Then
							Dim analysis_TS As Timespan = new_Node.Average - new_Result.Nodes(0).Average
							If (analysis_TS.TotalDays > 5 Or analysis_TS.TotalDays < -5) Then
								analysis_Span = SpanType.Days
							ElseIf (analysis_TS.TotalHours > 5 Or analysis_TS.TotalHours < -5) Then
								analysis_Span = SpanType.Hours
							ElseIf (analysis_TS.TotalMinutes > 5 Or analysis_TS.TotalMinutes < -5) Then
								analysis_Span = SpanType.Minutes
							ElseIf (analysis_TS.TotalSeconds > 5 Or analysis_TS.TotalSeconds < -5) Then
								analysis_Span = SpanType.Seconds
							ElseIf (analysis_TS.TotalMilliseconds > 5 Or analysis_TS.TotalMilliseconds < -5) Then
								analysis_Span = SpanType.Milliseconds
							Else
								analysis_Span = SpanType.Ticks
							End If
						End If

						new_Result.Nodes.Add(new_Node)

					Next

					For Each from_Event As Int64 In option_Results.Keys

						For Each to_Event As Int64 In option_Results(from_Event).Keys

							Dim new_Edge As New Edge()
							new_Edge.Name = all_Events(from_Event).Name & " > " & all_Events(to_Event).Name
							If to_Event = final_Event Then new_Edge.Name = new_Edge.Name + "*"

							Dim min_ts As Timespan = Nothing
							Dim max_ts As Timespan = Nothing
							Dim total_ts As Double = 0

							For Each ts As Timespan In option_Results(from_Event)(to_Event)

								If min_ts = Nothing OrElse ts < min_ts Then min_ts = ts
								If max_ts = Nothing OrElse ts > max_ts Then max_ts = ts
								total_ts += ts.Ticks

							Next

							Dim average_ts As New Timespan(Convert.ToInt64(total_ts/option_Results(from_Event)(to_Event).Count))

							new_Edge.Count = option_Results(from_Event)(to_Event).Count

							Select Case analysis_Span
								Case SpanType.Days
									new_Edge.Average = average_ts.TotalDays
									new_Edge.Min = min_ts.TotalDays
									new_Edge.Max = max_ts.TotalDays
								Case SpanType.Hours
									new_Edge.Average = average_ts.TotalHours
									new_Edge.Min = min_ts.TotalHours
									new_Edge.Max = max_ts.TotalHours
								Case SpanType.Minutes
									new_Edge.Average = average_ts.TotalMinutes
									new_Edge.Min = min_ts.TotalMinutes
									new_Edge.Max = max_ts.TotalMinutes
								Case SpanType.Seconds
									new_Edge.Average = average_ts.TotalSeconds
									new_Edge.Min = min_ts.TotalSeconds
									new_Edge.Max = max_ts.TotalSeconds
								Case SpanType.Milliseconds
									new_Edge.Average = average_ts.TotalMilliseconds
									new_Edge.Min = min_ts.TotalMilliseconds
									new_Edge.Max = max_ts.TotalMilliseconds
								Case SpanType.Ticks
									new_Edge.Average = average_ts.Ticks
									new_Edge.Min = min_ts.Ticks
									new_Edge.Max = max_ts.Ticks
							End Select

							new_Result.Edges.Add(new_Edge)

						Next

					Next

					all_Results.Add(new_Result)

				End If

			Next ' Option'

			If Host.Available(VL.Verbose) Then Host.Log("Assuming Event: " & all_Events(final_Event).Name & " is the completion event.")

			' -- Compute Averages/Standard Deviation --
			Dim node_Totals As New Dictionary(Of String, List(Of Double))
			Dim node_Averages As New Dictionary(Of String, Double)
			Dim edges_Totals As New Dictionary(Of String, List(Of Double))
			Dim edge_Averages As New Dictionary(Of String, Double)

			For Each single_Result As Result in all_Results

				For Each single_Node As Node In single_Result.Nodes

					If Not node_Totals.ContainsKey(single_Node.Name) Then _
						node_Totals.Add(single_Node.Name, New List(Of Double))
					node_Totals(single_Node.Name).Add(single_Node.Completion)

				Next

				For Each single_Edge As Edge In single_Result.Edges

					If Not edges_Totals.ContainsKey(single_Edge.Name) Then _
						edges_Totals.Add(single_Edge.Name, New List(Of Double))
					edges_Totals(single_Edge.Name).Add(single_Edge.Average)

				Next

			Next

			For Each node_Name As String In node_Totals.Keys

				Dim node_Total As Double = 0
				For i As Integer = 0 To node_Totals(node_Name).Count - 1
					node_Total += node_Totals(node_Name)(i)
				Next
				node_Averages.Add(node_Name, (node_Total / node_Totals(node_Name).Count))

			Next

			For Each edge_Name As String In edges_Totals.Keys

				Dim edge_Total As Double = 0
				For i As Integer = 0 To edges_Totals(edge_Name).Count - 1
					edge_Total += edges_Totals(edge_Name)(i)
				Next
				edge_Averages.Add(edge_Name, (edge_Total / edges_Totals(edge_Name).Count))

			Next

			Dim node_Devations As New Dictionary(Of String, List(Of Double))
			Dim node_SDs As New Dictionary(Of String, Double)
			Dim edges_Devations As New Dictionary(Of String, List(Of Double))
			Dim edge_SDs As New Dictionary(Of String, Double)

			For Each single_Result As Result in all_Results

				For Each single_Node As Node In single_Result.Nodes

					If Not node_Devations.ContainsKey(single_Node.Name) Then _
						node_Devations.Add(single_Node.Name, New List(Of Double))
					node_Devations(single_Node.Name).Add((single_Node.Completion - node_Averages(single_Node.Name)) ^ 2)

				Next

				For Each single_Edge As Edge In single_Result.Edges

					If Not edges_Devations.ContainsKey(single_Edge.Name) Then _
						edges_Devations.Add(single_Edge.Name, New List(Of Double))
					If Host.Available(VL.Debug) Then Host.Debug("Average for Edge: " & single_Edge.Name & " [" & single_Edge.Average & "]") ' -- DEBUG --
					If Host.Available(VL.Debug) Then Host.Debug("Averages for Edge: " & single_Edge.Name & " [" & edge_Averages(single_Edge.Name) & "]") ' -- DEBUG --
					Dim d As Double = ((single_Edge.Average - edge_Averages(single_Edge.Name)) ^ 2)
					If Host.Available(VL.Debug) Then Host.Debug("Adding Devation for Edge: " & single_Edge.Name & " [" & d & "]") ' -- DEBUG --
					edges_Devations(single_Edge.Name).Add(d)

				Next

			Next

			If Host.Available(VL.Debug) Then Host.Debug("Node Devations: " & node_Devations.Count) ' -- DEBUG --
			For Each node_Name As String In node_Devations.Keys

				If Host.Available(VL.Debug) Then Host.Debug("Node Devations [" & node_Name & "]: " & node_Devations(node_Name).Count) ' -- DEBUG --
				Dim node_Total As Double = 0
				For i As Integer = 0 To node_Devations(node_Name).Count - 1
					node_Total += node_Devations(node_Name)(i)
				Next
				node_SDs.Add(node_Name, Math.Sqrt(node_Total / node_Devations(node_Name).Count))
				If Host.Available(VL.Debug) Then Host.Debug("Node Standard Devation [" & node_Name & "]: " & node_SDs(node_Name)) ' -- DEBUG --

			Next

			If Host.Available(VL.Debug) Then Host.Debug("Edge Devations: " & edges_Devations.Count) ' -- DEBUG --
			For Each edge_Name As String In edges_Devations.Keys

				If Host.Available(VL.Debug) Then Host.Debug("Edge Devations [" & edge_Name & "]: " & edges_Devations(edge_Name).Count) ' -- DEBUG --

				Dim edge_Total As Double = 0
				For i As Integer = 0 To edges_Devations(edge_Name).Count - 1
					edge_Total += edges_Devations(edge_Name)(i)
				Next
				edge_SDs.Add(edge_Name, Math.Sqrt(edge_Total / edges_Devations(edge_Name).Count))
				If Host.Available(VL.Debug) Then Host.Debug("Edge Standard Devation [" & edge_Name & "]: " & edge_SDs(edge_Name)) ' -- DEBUG --

			Next
			' --------------------- '

			Dim edge_SD_Average As Double = 0
			Dim edge_SD_SD As Double = 0

			For Each sd As Double in edge_SDs.Values
				edge_SD_Average += sd
			Next
			edge_SD_Average = edge_SD_Average / edge_SDs.Count

			For Each sd As Double in edge_SDs.Values
				edge_SD_SD += (sd - edge_SD_Average) ^ 2
			Next
			edge_SD_SD = Math.Sqrt(edge_SD_SD)


			Dim aggregate_Results As New Dictionary(Of String, List(Of Double))
			Dim aggregate_Completion As New Dictionary(Of String, Double)
			Dim aggregate_Overall As New Dictionary(Of String, Double)

			For Each single_Result As Result in all_Results

				value_List.Add(New List(Of Double))
				it_List.Add(New List(Of Leviathan.Visualisation.InformationType))

				For i As Integer = 0 To single_Result.Nodes.Count - 1

					Dim new_Row As New Row()
					If i = 0 Then new_Row.Add(single_Result.Display_Name) Else new_Row.Add()

					new_Row.Add(single_Result.Nodes(i).Name & " [" & Math.Round(single_Result.Nodes(i).Completion * 100, display_Round) & "%]").Add(single_Result.Nodes(i).Count).Add(single_Result.Nodes(i).Average) _
						.Add(single_Result.Nodes(i).Earliest).Add(single_Result.Nodes(i).Latest).Add()
					results_Rows.Add(new_Row)

				Next


				' -- Aggregate Record -- '
				For j As Integer = 0 To single_Result.Names.Length - 1
					If Not aggregate_Results.ContainsKey(single_Result.Names(j)) Then _
						aggregate_Results.Add(single_Result.Names(j), New List(Of Double))
				Next
				' -- Aggregate Record -- '


				Dim running_Count As Double = 0

				For i As Integer = 0 To single_Result.Edges.Count - 1

					' It's a proper edge!
					If single_Result.Edges(i).Count >= (single_Result.Average_Edge_Count / 3) Then

						Dim new_Row As New Row()

						new_Row.Add().Add(single_Result.Edges(i).Name).Add(single_Result.Edges(i).Count)

						Dim disp_Value As Double = 0
						Dim displacement As Integer = 0

						If edge_SDs(single_Result.Edges(i).Name) > 0 Then _
							disp_Value = (edge_Averages(single_Result.Edges(i).Name) - single_Result.Edges(i).Average) / edge_SDs(single_Result.Edges(i).Name)
						If disp_Value < 0 Then displacement = Math.Floor(disp_Value) Else displacement = Math.Ceiling(disp_Value)

						Dim it_Type As Leviathan.Visualisation.InformationType
						If displacement < -1 Then
							it_Type = IT.Error
						ElseIf displacement < 0 Then
							it_Type = IT.Performance
						ElseIf displacement > 1 Then
							it_Type = IT.Success
						ElseIf displacement > 0 Then
							it_Type = IT.Question
						Else
							it_Type = IT.General
						End If
						
						Dim sd_Type As Leviathan.Visualisation.InformationType = IT.General
						If edge_SDs(single_Result.Edges(i).Name) > 0 Then
							Dim sd_disp_Value As Double = (edge_SDs(single_Result.Edges(i).Name) - edge_SD_Average) / edge_SD_SD
							Dim sd_displacement As Integer = 0
							If sd_disp_Value < 0 Then sd_displacement = Math.Floor(sd_disp_Value * 4) Else sd_displacement = Math.Ceiling(sd_disp_Value * 4)
							If sd_displacement > 1 Then
								sd_Type = IT.Error
							ElseIf sd_displacement > 0 Then
								sd_Type = IT.Performance
							ElseIf sd_displacement < -1 Then
								sd_Type = IT.Success
							ElseIf sd_displacement < 0 Then
								sd_Type = IT.Question
							Else
								sd_Type = IT.General
							End If
						End If

						new_Row.Add(Math.Round(single_Result.Edges(i).Average, display_Round), it_Type) _
							.Add(Math.Round(single_Result.Edges(i).Min, display_Round)) _
							.Add(Math.Round(single_Result.Edges(i).Max, display_Round)) _
							.Add(Math.Round(edge_SDs(single_Result.Edges(i).Name), display_Round), sd_Type)
						results_Rows.Add(new_Row)

						If single_Result.Edges(i).Name.EndsWith("*") Then

							If Not edge_List.Contains(single_Result.Edges(i).Name) Then
								edge_List.Add(single_Result.Edges(i).Name)
								potential_Weighting += 1
							End If

							Dim edge_Index As Integer = edge_List.IndexOf(single_Result.Edges(i).Name)

							If value_List(value_List.Count - 1).Count < edge_Index + 1 Then
								For j As Integer = value_List(value_List.Count - 1).Count To edge_Index
									value_List(value_List.Count - 1).Add(0)
									it_List(it_List.Count - 1).Add(IT.General)
								Next
							End If

							' -- Aggregate Record -- '
							For j As Integer = 0 To single_Result.Names.Length - 1
								If Not aggregate_Overall.ContainsKey(single_Result.Names(j)) Then _
									aggregate_Overall.Add(single_Result.Names(j), 0)
								If aggregate_Results(single_Result.Names(j)).Count < edge_Index + 1 Then
								For k As Integer = aggregate_Results(single_Result.Names(j)).Count To edge_Index
									aggregate_Results(single_Result.Names(j)).Add(0)
								Next
							End If
							Next
							' -- Aggregate Record -- '

							' value_List(value_List.Count - 1).Item(edge_Index) = single_Result.Edges(i).Average ' -- REMOVED TO GIVE RELATIVE --
							Dim val As Double = edge_Averages(single_Result.Edges(i).Name) - single_Result.Edges(i).Average
							running_Count = running_Count + val

							' -- Aggregate Record -- '
							For j As Integer = 0 To single_Result.Names.Length - 1
								' aggregate_Results(single_Result.Names(j))(edge_Index) = aggregate_Results(single_Result.Names(j))(edge_Index) + val
								aggregate_Results(single_Result.Names(j))(edge_Index) = aggregate_Results(single_Result.Names(j))(edge_Index) + disp_Value
								aggregate_Overall(single_Result.Names(j)) = aggregate_Overall(single_Result.Names(j)) + disp_Value
							Next
							' -- Aggregate Record -- '

							value_List(value_List.Count - 1).Item(edge_Index) = val
							it_List(it_List.Count - 1).Item(edge_Index) = it_Type

						End If

					End If

				Next

				' -- Set Weighting -- '
				Dim l_Weighting As Integer = actual_Weighting
				If completion_Weighting = -1 Then actual_Weighting = potential_Weighting Else actual_Weighting = completion_Weighting
				If Host.Available(VL.Verbose) AndAlso actual_Weighting <> l_Weighting Then Host.Log("Changing Weighting Value from " & l_Weighting & " to " & actual_Weighting)

				Dim summary_Row As New Row()
				
				summary_Row.Add(single_Result.Display_Name)

				For i As Integer = 0 To single_Result.Nodes.Count - 1
					If single_Result.Nodes(i).Name.EndsWith("*") Then

						Dim disp_Value As Double = 0
						Dim displacement As Integer = 0
						If node_SDs(single_Result.Nodes(i).Name) > 0 Then _
							disp_Value = (single_Result.Nodes(i).Completion - node_Averages(single_Result.Nodes(i).Name)) / node_SDs(single_Result.Nodes(i).Name)
						If disp_Value < 0 Then displacement = Math.Floor(disp_Value) Else displacement = Math.Ceiling(disp_Value)

						Dim it_Type As Leviathan.Visualisation.InformationType
						If displacement < -1 Then
							it_Type = IT.Error
						ElseIf displacement < 0 Then
							it_Type = IT.Performance
						ElseIf displacement > 1 Then
							it_Type = IT.Success
						ElseIf displacement > 0 Then
							it_Type = IT.Question
						Else
							it_Type = IT.General
						End If

						'summary_Row.Add((single_Result.Nodes(i).Completion * 100) & "%", it_Type) ' -- REMOVED TO GIVE RELATIVE --
						Dim val As Double = (single_Result.Nodes(i).Completion - node_Averages(single_Result.Nodes(i).Name)) * 100
						running_Count = running_Count + (val * actual_Weighting) ' -- x to Weight in favour of completion! '
						summary_Row.Add(Math.Round(val, display_Round), it_Type)

						' -- Aggregate Record -- '
						For j As Integer = 0 To single_Result.Names.Length - 1
							If Not aggregate_Completion.ContainsKey(single_Result.Names(j)) Then _
								aggregate_Completion.Add(single_Result.Names(j), 0)
							If Not aggregate_Overall.ContainsKey(single_Result.Names(j)) Then _
								aggregate_Overall.Add(single_Result.Names(j), 0)

							'aggregate_Completion(single_Result.Names(j)) = aggregate_Completion(single_Result.Names(j)) + val
							'aggregate_Overall(single_Result.Names(j)) = aggregate_Overall(single_Result.Names(j)) + running_Count
							aggregate_Completion(single_Result.Names(j)) = aggregate_Completion(single_Result.Names(j)) + disp_Value
							aggregate_Overall(single_Result.Names(j)) = aggregate_Overall(single_Result.Names(j)) + (disp_Value * actual_Weighting) ' -- x to Weight in favour of completion! '
						Next
						' -- Aggregate Record -- '

						Exit For
					End If
				Next

				If summary_Row.Count = 1 Then summary_Row.Add()

				If running_Count > 0 Then
					summary_Row.Insert(1, Math.Round(running_Count, display_Round), IT.Success)
				Else
					summary_Row.Insert(1, Math.Round(running_Count, display_Round), IT.Error)
				End If

				For i As Integer = 0 To edge_List.Count - 1
					If value_List(value_List.Count - 1).Count < i + 1 Then
						summary_Row.Add()
					Else
						summary_Row.Add(Math.Round(value_List(value_List.Count - 1).Item(i), display_Round), _
							it_List(it_List.Count - 1).Item(i))
					End If
				Next
				summary_Rows.Add(summary_Row)

			Next ' Result '

			summary_Rows.Add(New Row()) ' Spacing Row '
			summary_Rows.Add(New Row().Add("-- Aggregates --", IT.Information)) ' Spacing Row '

			' -- Aggregate Records -- '
			Dim aggregate_Sort = aggregate_Overall.OrderByDescending(Function(p) p.Value).ToList()

			For i As Integer = 0 To aggregate_Sort.Count - 1

				Dim aggregate_Name  = aggregate_Sort(i).Key

				Dim aggregate_Row As New Row()

				aggregate_Row.Add(aggregate_Name)
				aggregate_Row.Add(Math.Round(aggregate_Overall(aggregate_Name), display_Round), _
					Get_Display_Type(aggregate_Overall(aggregate_Name)))
				aggregate_Row.Add(Math.Round(aggregate_Completion(aggregate_Name), display_Round), _
					Get_Display_Type(aggregate_Completion(aggregate_Name)))

				For j As Integer = 0 To edge_List.Count - 1
					If j > aggregate_Results(aggregate_Name).Count - 1 Then
						aggregate_Row.Add()
					Else
						aggregate_Row.Add(Math.Round(aggregate_Results(aggregate_Name)(j), display_Round), _
							Get_Display_Type(aggregate_Results(aggregate_Name)(j)))
					End If
				Next

				summary_Rows.Add(aggregate_Row)

			Next

			' -- Aggregate Records -- '

			edge_List.Insert(0, "Completion")
			edge_List.Insert(0, "Delta")
			edge_List.Insert(0, "Option Name")

			For i As Integer = 0 TO summary_Rows.Count - 1
				If summary_Rows(i).Count < edge_List.Count Then
					For j As Integer = summary_Rows(i).Count To edge_List.Count - 1
						summary_Rows(i).Add()
					Next 
				End If
			Next

			If Host.Available(VL.Verbose) Then

				Dim units As String = Nothing
				Select Case analysis_Span
					Case SpanType.Days
						units = "/d"
					Case SpanType.Hours
						units = "/hr"
					Case SpanType.Minutes
						units = "/mi"
					Case SpanType.Seconds
						units = "/s"
					Case SpanType.Milliseconds
						units = "/ms"
					Case SpanType.Ticks
						units = "/tk"
				End Select
				Return New IFixedWidthWriteable() { _
					Cube.Create(IT.Information, String.Format("{0} [{1}] // Summary", value.Name, value.Code), _
					FormatterProperty.Create(edge_List.ToArray())).Add(New Slice(summary_Rows)), _
					Cube.Create(IT.Information, String.Format("{0} [{1}] // Raw Results", value.Name, value.Code), _
					"Option Name", "Event", "Count", "Avg" + units, "Min" + units, "Max" + units, "SD" + units).Add(New Slice(results_Rows)) _
				}

			Else

				Return New IFixedWidthWriteable() { _
					Cube.Create(IT.Information, String.Format("{0} [{1}] // Summary", value.Name, value.Code), _
					FormatterProperty.Create(edge_List.ToArray())).Add(New Slice(summary_Rows)) _
				}

			End If

		End Function

		Private Function Get_Display_Type( _
			ByVal value As Double _
		) As Leviathan.Visualisation.InformationType

			If value > 2 Then
				Return IT.Success
			ElseIf value > 0 Then
				Return IT.Question
			ElseIf value < -2 Then
				Return IT.Error
			ElseIf value < 0 Then
				Return IT.Performance
			Else
				Return IT.General
			End If

		End Function

	#End Region

	#Region " Public Command Methods "

		<Command( _
			Name:="all", _
			Description:="Analyse All Exercises" _
		)> _
		Public Function Analyse_All(
		) As IFixedWidthWriteable()

			Return Analyse_All(-1)

		End Function

		<Command( _
			Name:="all", _
			Description:="Analyse All Exercises" _
		)> _
		Public Function Analyse_All(
			<Configurable( _
				Description:="Weight Completion by ..." _
			)> _
			ByVal completion_Weighting As Integer _
		) As IFixedWidthWriteable()

			Dim return_List As New List(Of IFixedWidthWriteable)

			For Each single_Exercise As Exercise In Exercise.All.Get(Data.Connection(Athena_Source)).ToList()

				return_List.AddRange(Analyse_Exercise(single_Exercise, completion_Weighting))

			Next

			Return return_List.ToArray()

		End Function

		<Command( _
			Name:="exercise", _
			Description:="Analyse Specific Exerise" _
		)> _
		Public Function Analyse_Exercise( _
			<Configurable( _
				Description:="Exercise Name or Code" _
			)> _
			ByVal name As String _
		) As IFixedWidthWriteable()

			Return Analyse_Exercise(name, -1)

		End Function

		<Command( _
			Name:="exercise", _
			Description:="Analyse Specific Exerise" _
		)> _
		Public Function Analyse_Exercise( _
			<Configurable( _
				Description:="Exercise Name or Code" _
			)> _
			ByVal name As String, _
			<Configurable( _
				Description:="Weight Completion by ..." _
			)> _
			ByVal completion_Weighting As Integer _
		) As IFixedWidthWriteable()

			Dim existing_Exercise As Exercise = Exercise.All.Filter( _
				Data.Connection(Athena_Source), Exercise.All.Filters.NAME, name).FirstOrDefault()

			If existing_Exercise Is Nothing Then existing_Exercise = Exercise.All.Filter( _
				Data.Connection(Athena_Source), Exercise.All.Filters.CODE, name).FirstOrDefault()

			If Not existing_Exercise Is Nothing Then
				Return Analyse_Exercise(existing_Exercise, completion_Weighting)
			Else
				Return Nothing
			End If

		End Function

	#End Region

End Class