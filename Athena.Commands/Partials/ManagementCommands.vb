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

Partial Public Class ManagementCommands

	#Region " Public Command Methods "

		<Command( _
			Name:="exercises", _
			Description:="List Exercises (and Options)" _
		)> _
		Public Function List_Exercises() As IFixedWidthWriteable

			Dim summary_Rows As New List(Of Row)

			Dim all_Exercises As List(Of Exercise) = Exercise.All.Get(Data.Connection(Athena_Source)).ToList()

			For Each single_Exercise As Exercise In all_Exercises

				Dim all_Options As List(Of [Option]) = [Option].All.Filter( _
					Data.Connection(Athena_Source), [Option].All.Filters.EXERCISE, single_Exercise.Id).ToList()

				summary_Rows.Add(New Row().Add(single_Exercise.Name).Add(single_Exercise.Code).Add(all_Options.Count))

			Next

			Return Cube.Create(IT.Information, "Exercises // Summary", "Name", "Code", "Options").Add(New Slice(summary_Rows))

		End Function

		<Command( _
			Name:="delete-exercise", _
			Description:="Delete Specific Exerise" _
		)> _
		Public Function Delete_Exercise( _
			<Configurable( _
				Description:="Exercise Name or Code" _
			)> _
			ByVal name As String _
		) As System.Boolean

			Dim existing_Exercise As Exercise = Exercise.All.Filter( _
				Data.Connection(Athena_Source), Exercise.All.Filters.NAME, name).FirstOrDefault()

			If existing_Exercise Is Nothing Then existing_Exercise = Exercise.All.Filter( _
				Data.Connection(Athena_Source), Exercise.All.Filters.CODE, name).FirstOrDefault()

			If Not existing_Exercise Is Nothing Then
				
				If Host.Available(VL.Interactive) AndAlso Host.BooleanResponse(String.Format("Are you should you want to delete data for {0}", existing_Exercise.Name)) Then

					Dim all_Events As List(Of [Event]) = [Event].All.Filter( _
						Data.Connection(Athena_Source), [Event].All.Filters.EXERCISE, existing_Exercise.Id).ToList()

					Dim all_Options As List(Of [Option]) = [Option].All.Filter( _
						Data.Connection(Athena_Source), [Option].All.Filters.EXERCISE, existing_Exercise.Id).ToList()

					For Each single_Option As [Option] In all_Options

						Dim all_Instances As List(Of [Instance]) = [Instance].All.Filter( _
							Data.Connection(Athena_Source), [Instance].All.Filters.OPTION, single_Option.Id).ToList()

						For Each single_Instance As Instance In all_Instances

							Dim all_Actions As List(Of [Action]) = [Action].All.Filter( _
								Data.Connection(Athena_Source), [Action].All.Filters.INSTANCE, single_Instance.Id).ToList()

							For Each single_Action As Action In all_Actions

								If Not [Action].All.Delete(Data.Connection(Athena_Source), single_Action) Then
									Host.Warn(String.Format("Failed To Delete Action (Id: {0})", single_Action.Id))
									Return False
								End If

							Next

							If Not [Instance].All.Delete(Data.Connection(Athena_Source), single_Instance) Then
								Host.Warn(String.Format("Failed To Delete Instance (Id: {0})", single_Instance.Id))
								Return False
							End If

						Next

						If Not [Option].All.Delete(Data.Connection(Athena_Source), single_Option) Then
							Host.Warn(String.Format("Failed To Delete Option (Id: {0})", single_Option.Id))
							Return False
						End If

					Next

					For Each single_Event As [Event] In all_Events

						If Not [Event].All.Delete(Data.Connection(Athena_Source), single_Event) Then
							Host.Warn(String.Format("Failed To Delete Event (Id: {0})", single_Event.Id))
							Return False
						End If

					Next

					If Not [Exercise].All.Delete(Data.Connection(Athena_Source), existing_Exercise) Then
						Host.Warn(String.Format("Failed To Delete Exercise (Id: {0})", existing_Exercise.Id))
						Return False
					End If

					Return True

				End If

			End If

			Return False

		End Function

	#End Region

End Class