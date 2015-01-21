Imports Athena.Core
Imports Hermes.Cryptography.Cipher
Imports Leviathan.Configuration
Imports Leviathan.Configuration.DateTimeConvertor
Imports Leviathan.Visualisation
Imports System.Linq
Imports System.Net
Imports System.IO
Imports System.Xml
Imports System.Web
Imports System.Web.Script.Serialization
Imports System.Web.HttpUtility
Imports IT = Leviathan.Visualisation.InformationType
Imports VL = Leviathan.Commands.VerbosityLevel

Partial Public Class CreationCommands

	#Region " Private Constants "

		Private Const HTML_ATTRIBUTE_EVENT As String = "athena-event"

		Private Const HTML_ATTRIBUTE_INSTANCE As String = "athena-instance"

		Private Const HTML_ATTRIBUTE_COMPLETE As String = "athena-complete"

	#End Region

	#Region " Friend Constants "

		Friend Const XML_ELEMENT_EXERCISE As String = "Exercise"

		Friend Const XML_ELEMENT_BATCH As String = "Batch"

		Friend Const XML_ELEMENT_OPTION As String = "Option"

		Friend Const XML_ELEMENT_TITLE As String = "Title"

		Friend Const XML_ELEMENT_ATTACHMENT As String = "Attachment"

		Friend Const XML_ELEMENT_INSTANCE As String = "Instance"

		Friend Const XML_ELEMENT_EMAIL As String = "Email"

		Friend Const XML_ELEMENT_TO As String = "To"

		Friend Const XML_ELEMENT_DATA As String = "Data"

		Friend Const XML_ATTRIBUTE_NAME As String = "name"

		Friend Const XML_ATTRIBUTE_EXECUTED As String = "executed"

		Friend Const XML_ATTRIBUTE_TIMESTAMP As String = "timestamp"

		Friend Const XML_ATTRIBUTE_TEMPLATE As String = "template"

	#End Region

	#Region " Private Functions "

		Private Function Create_Exercise( _
			ByVal name As String _
		) As IFixedWidthWriteable

			' -- Check whether Exercise Already Exists --
			Dim existing_Exercise As Exercise = Exercise.All.Filter(Data.Connection(Athena_Source), Exercise.All.Filters.NAME, name).FirstOrDefault()
			If Not existing_Exercise Is Nothing Then
				Host.Warn("Exercise with Proposed Name already Exists in the Data Store")
				Return Nothing
			End If
			' -------------------------------------------

			' -- Create a Code --
			Dim proposed_Code As String = Nothing
			Dim exists_Code As Boolean = True
			While exists_Code
				proposed_Code = Create_Password(10, 0).ToLower()
				exists_Code = Not Exercise.All.Filter(Data.Connection(Athena_Source), Exercise.All.Filters.CODE, proposed_Code).FirstOrDefault() Is Nothing
			End While
			' -------------------
					
			' -- Create New Exercise --
			Dim value As New Exercise()
			value.Code = proposed_Code
			value.Name = name
			If Exercise.All.Set(Data.Connection(Athena_Source), value) Then

				Dim results_Rows As New List(Of Row)

				results_Rows.Add(New Row().Add(value.Id).Add(value.Code).Add(value.Name).Add(value.Created))

				Return Cube.Create(IT.Information, "Exercise Details", "Id", "Code", "Name", "Created").Add(New Slice(results_Rows))

			Else
				Host.Warn("Could not Set Exercise to Data Store")
				Return Nothing
			End If
			' ------------------------

		End Function

		Private Function Get_Event( _
			ByVal _exercise As Exercise, _
			ByVal _name As String _
		) As [Event]

			' -- Check whether Event Already Exists --
			Dim existing_Event As [Event] = [Event].All.Filter(Data.Connection(Athena_Source), [Event].All.Filters.EXERCISE Or [Event].All.Filters.NAME, _
				New Object() {_exercise.Id, _name}).FirstOrDefault()

			If existing_Event Is Nothing Then

				' -- Create a Code --
				Dim event_Proposed_Code As String = Nothing
				Dim exists_Proposed_Code As Boolean = True
				While exists_Proposed_Code
					event_Proposed_Code = Create_Password(10, 0).ToLower()
					exists_Proposed_Code = Not [Event].All.Filter(Data.Connection(Athena_Source), [Event].All.Filters.CODE, event_Proposed_Code).FirstOrDefault() Is Nothing
				End While
				' -------------------
					
				' -- Create New Event --
				existing_Event = New [Event]()
				existing_Event.Exercise = _exercise.Id
				existing_Event.Code = event_Proposed_Code
				existing_Event.Name = _name
				If Not [Event].All.Set(Data.Connection(Athena_Source), existing_Event) Then
					Host.Warn("Could not Set Event to Data Store, Perhaps there is a Data Connection Issue")
					Return Nothing
				End If
				' -------------------------

			End If
			' -------------------------------------------

			return existing_Event

		End Function

	#End Region

	#Region " Public Command Methods "

		<Command( _
			Name:="execute-batch", _
			Description:="Execute First Available Exercise Batch from an XML File" _
		)> _
		Public Function Execute_Batch( _
			<Configurable( _
				Description:="Source XML Document" _
			)> _
			ByVal source As String _
		) As IFixedWidthWriteable

			Return Execute_Batch(source, True, "Completed")

		End Function

		<Command( _
			Name:="execute-batch", _
			Description:="Execute First Available Exercise Batch from an XML File" _
		)> _
		Public Function Execute_Batch( _
			<Configurable( _
				Description:="Source XML Document" _
			)> _
			ByVal source As String, _
			<Configurable( _
				Description:="Enable Tracking / Analysis" _
			)> _
			ByVal enable_Tracking As Boolean, _
			<Configurable( _
				Description:="Completed Event Name" _
			)> _
			ByVal complete_Event As String _
		) As IFixedWidthWriteable

			Dim source_Doc As New XmlDocument()
			If Not System.io.File.Exists(source) Then
				Host.Warn(String.Format("Cannot find Source File ({0})", source))
				Return Nothing
			End If
			source_Doc.Load(source)

			Dim exercise_Name As String = source_Doc.SelectSingleNode("//" & _
				XML_ELEMENT_EXERCISE).Attributes.ItemOf(XML_ATTRIBUTE_NAME).Value

			' -- Check whether Exercise Already Exists --
			Dim existing_Exercise As Exercise = Exercise.All.Filter(Data.Connection(Athena_Source), Exercise.All.Filters.NAME, exercise_Name).FirstOrDefault()

			' -- Get the Agent to Which this refers --
			Dim server_Agent As Agent = Agent.All.Filter(Data.Connection(Athena_Source), Agent.All.Filters.DETAILS, "Server-Side").FirstOrDefault()
			If server_Agent Is Nothing Then
				server_Agent = New Agent()
				server_Agent.Details = "Server-Side"
				Agent.All.Set(Data.Connection(Athena_Source), server_Agent)
			End If
			' ----------------------------------------

			' -- Create Results Row for Return --
			Dim results_Rows As New List(Of Row)
			Dim option_Count As New Dictionary(Of String, Integer)
			' -----------------------------------

			If existing_Exercise Is Nothing Then

				' -- Create a Code --
				Dim proposed_Code As String = Nothing
				Dim exists_Code As Boolean = True
				While exists_Code
					proposed_Code = Create_Password(10, 0).ToLower()
					exists_Code = Not Exercise.All.Filter(Data.Connection(Athena_Source), Exercise.All.Filters.CODE, proposed_Code).FirstOrDefault() Is Nothing
				End While
				' -------------------
					
				' -- Create New Exercise --
				existing_Exercise = New Exercise()
				existing_Exercise.Code = proposed_Code
				existing_Exercise.Name = exercise_Name
				If Not Exercise.All.Set(Data.Connection(Athena_Source), existing_Exercise) Then
					Host.Warn("Could not Set Exercise to Data Store, Perhaps there is a Data Connection Issue")
					Return Nothing
				End If
				' -------------------------

			End If
			' -------------------------------------------

			Dim first_Batch As XmlNode = source_Doc.SelectSingleNode("descendant::" & _
				XML_ELEMENT_BATCH & "[@" & XML_ATTRIBUTE_EXECUTED & "='false']")

			If first_Batch Is Nothing Then

				Host.Warn("No Available Batches to Execute (e.g. Document is not correctly structured or all batches have been executed)")
				Return Nothing

			Else

				' -- Set the From/Reply-To --
				Dim _from As System.Net.Mail.MailAddress = Hermes.Email.Manager.CreateAddress(MailFromAddress, MailFromDisplay)
				Dim _replyTo As System.Net.Mail.MailAddress = Hermes.Email.Manager.CreateAddress(MailReplyToAddress, MailReplyToDisplay)

				' -- Set Up the Hermes Manager and Send Email --
				Dim _manager = new Hermes.Email.Manager()
				_manager.Suppress_Send = MailSuppress
				_manager.SMTP_MaxBatch = 20
				_manager.Client_Application = "Athena Commands"
				If Not MailLog Is Nothing Then _manager.Logging_Directory = MailLog
				_manager.SMTP_Server = Hermes.Email.Manager.CreateServer(MailServer)
				_manager.SMTP_Port = MailServerPort
				_manager.Use_SSL = MailServerSSL
				_manager.Verify_SSL = MailServerValidateCertificate
				If String.IsNullOrEmpty(MailServerUsername) Then
					_manager.SMTP_Credential = Hermes.Email.Manager.CreateIntegratedCredential()
				Else
					_manager.SMTP_Credential = Hermes.Email.Manager.CreateCredential(MailServerUsername, MailServerPassword, MailServerDomain)
				End If

				For Each single_Option As XmlNode In first_Batch.SelectNodes(XML_ELEMENT_OPTION)

					Dim option_Name As String = single_Option.Attributes.ItemOf(XML_ATTRIBUTE_NAME).Value
					option_Count.Add(option_Name, 0)

					' -- Create/Retrieve the DB Option --

					' -- Check whether Option Already Exists --
					Dim existing_Option As [Option] = [Option].All.Filter(Data.Connection(Athena_Source), [Option].All.Filters.EXERCISE Or [Option].All.Filters.NAME, _
						New Object() {existing_Exercise.Id, option_Name}).FirstOrDefault()

					If existing_Option Is Nothing Then

						' -- Create a Code --
						Dim proposed_Code As String = Nothing
						Dim exists_Code As Boolean = True
						While exists_Code
							proposed_Code = Create_Password(10, 0).ToLower()
							exists_Code = Not [Option].All.Filter(Data.Connection(Athena_Source), [Option].All.Filters.CODE, proposed_Code).FirstOrDefault() Is Nothing
						End While
						' -------------------
					
						' -- Create New Option --
						existing_Option = New [Option]()
						existing_Option.Exercise = existing_Exercise.Id
						existing_Option.Code = proposed_Code
						existing_Option.Name = option_Name
						If Not [Option].All.Set(Data.Connection(Athena_Source), existing_Option) Then
							Host.Warn("Could not Set Option to Data Store, Perhaps there is a Data Connection Issue")
							Return Nothing
						End If
						' -------------------------

					End If
					' -------------------------------------------

					' -----------------------------------

					Dim option_Template As String = single_Option.Attributes.ItemOf(XML_ATTRIBUTE_TEMPLATE).Value

					If Not System.IO.File.Exists(option_Template) Then

						Host.Warn(String.Format("Could Not Find/Access Template File ({0})", option_Template))
						Return Nothing

					Else

						Dim option_Title As String = single_Option.SelectSingleNode(XML_ELEMENT_TITLE).InnerText
						Dim option_Attachments As New List(Of String)

						Dim attachment_Nodes As XmlNodeList = single_Option.SelectNodes(XML_ELEMENT_ATTACHMENT)

						If Not attachment_Nodes Is Nothing AndAlso attachment_Nodes.Count > 0 Then

							For Each attachment_Node As XmlNode In attachment_Nodes

								Dim option_Attachment As String = attachment_Node.InnerText
								If Not String.IsNullOrEmpty(option_Attachment) AndAlso Not System.IO.File.Exists(option_Attachment) Then
									Host.Warn(String.Format("Could Not Find/Access Attachment File ({0})", option_Attachment))
									Return Nothing
								Else
									option_Attachments.Add(option_Attachment)
								End If

							Next

						End If

						Dim all_Instances As XmlNodeList = single_Option.SelectNodes(XML_ELEMENT_INSTANCE)

						Dim prog_Index As Integer = 1
						Dim prog_Total As Integer = all_Instances.Count

						For Each single_Instance As XmlNode in all_Instances

							' -- Create a DB Instance --
							' -- Create a Code --
							Dim proposed_Code As String = Nothing
							Dim exists_Code As Boolean = True
							While exists_Code
								proposed_Code = Create_Password(10, 0).ToLower()
								exists_Code = Not Instance.All.Filter(Data.Connection(Athena_Source), Instance.All.Filters.CODE, proposed_Code).FirstOrDefault() Is Nothing
							End While
							' -------------------
					
							' -- Create New Instance --
							Dim new_Instance As New Instance()
							new_Instance.Option = existing_Option.Id
							new_Instance.Code = proposed_Code
							If Not Instance.All.Set(Data.Connection(Athena_Source), new_Instance) Then
								Host.Warn("Could not Set Instance to Data Store, Perhaps there is a Data Connection Issue")
								Return Nothing
							End If
							' -------------------------
							' --------------------------

							' -- Set Up the Hermes Message --
							Dim _message As New Hermes.Email.Message(Hermes.Email.BodyType.HTML)
							_message.Subject = option_Title

							Dim _message_Body As String = Hermes.Email.Message.LoadTextFromFile(option_Template)
							Dim recipient_List_Type As Hermes.Email.SendingType = Hermes.Email.SendingType.Blind_Carbon_Copy
							If _message_Body.IndexOf("{HERMES-ADDRESS}") >= 0 Then
								recipient_List_Type = Hermes.Email.SendingType.Single_Recipient
							Else
								If Host.Available(VL.Verbose) Then Host.Log("Could Not Find Hermes Address Tag")
							End If

							Dim data_Node As XmlNode = single_Instance.SelectSingleNode(XML_ELEMENT_DATA)
							If Not data_Node Is Nothing Then

								For Each value_Node As XmlNode In data_Node.ChildNodes
									_message_Body = _message_Body.Replace("{" & value_Node.Name & "}", value_Node.InnerText)
								Next

							End If

							_message_Body = _message_Body.Replace("{" & HTML_ATTRIBUTE_INSTANCE & "}", new_Instance.Code)
							_message_Body = _message_Body.Replace("{" & HTML_ATTRIBUTE_COMPLETE & "}", Get_Event(existing_Exercise, complete_Event).Code)

							If enable_Tracking Then

								Dim current_Position As Int32 = 0
								Dim attr_Location As Int32 = _message_Body.ToLower().IndexOf(HTML_ATTRIBUTE_EVENT & "=", current_Position)

								While attr_Location >= 0

									' -- Get the Relevant HTML Tag --
									Dim tag_Start As Int32 = _message_Body.LastIndexOf("<", attr_Location)
									Dim tag_End As Int32 =  _message_Body.IndexOf(">", attr_Location) + 1 ' TODO: Is this robust enough?
									Dim html_Tag As String = _message_Body.Substring(tag_Start, tag_End - tag_Start)
									If Host.Available(VL.Debug) Then Host.Debug("Found Athena Attributed Tag: " & html_Tag)
									' -------------------------------

									' -- Get the Athena Attribute Value --
									Dim attr_Length As Int32 = HTML_ATTRIBUTE_EVENT.length + 1
									Dim attr_Value_Location As Integer = (attr_Location - tag_Start) + HTML_ATTRIBUTE_EVENT.length + 1
									Dim attr_Value_Deliminator As String = " "

									Do Until Not attr_Value_Deliminator = " "
										attr_Value_Deliminator = html_Tag.Substring(attr_Value_Location, 1)
										attr_Value_Location += 1
										attr_Length += 1
									Loop

									Dim attr_End As Int32 = html_Tag.IndexOf(attr_Value_Deliminator, attr_Value_Location)
									Dim attr_Value As String = html_Tag.Substring(attr_Value_Location, attr_End - attr_Value_Location)
									attr_Length += attr_Value.Length
									attr_Length += 1
									If Host.Available(VL.Debug) Then Host.Debug("Found Athena Attribute Value: " & attr_Value)
									Dim attr_Start As Int32 = attr_Location - tag_Start
									html_Tag = html_Tag.Remove(attr_Start, attr_Length)
									' ------------------------------------

									' -- Get the Event for this Attribute --
									Dim existing_Event As [Event] = Get_Event(existing_Exercise, attr_Value)
									' --------------------------------------

									' -- Get the URL for this Attribute --
									Dim url_Delimiter As String = " "
									Dim url_Start As Integer = 0

									If html_Tag.ToLower().IndexOf("src=") > 0 Then
										url_Start = html_Tag.ToLower().IndexOf("src=") + "src=".Length
									ElseIf html_Tag.ToLower().IndexOf("href=") > 0 Then
										url_Start = html_Tag.ToLower().IndexOf("href=") + "href=".Length
									End If

									Dim url_Deliminator As String = " "

									Do Until Not url_Deliminator = " "
										url_Deliminator = html_Tag.Substring(url_Start, 1)
										url_Start += 1
									Loop
									Dim url_End As Int32 = html_Tag.IndexOf(url_Deliminator, url_Start)
									Dim url_Value As String = html_Tag.Substring(url_Start, url_End - url_Start)
									If Host.Available(VL.Debug) Then Host.Debug("Found Athena URL Value to track: " & url_Value)
									Dim url_NewValue As String = String.Format(Athena_Url.Replace("&amp;", "&"), new_Instance.Code, existing_Event.Code, UrlEncode(url_Value))
									If Host.Available(VL.Debug) Then Host.Debug("Converting To: " & url_NewValue)
									html_Tag = html_Tag.Replace(url_Value, url_NewValue)
									' ------------------------------------

									' -- Clean Up HTML --
									_message_Body = _message_Body.Remove(tag_Start, tag_End - tag_Start) ' Removes entire HTML Tag
									_message_Body = _message_Body.Insert(tag_Start, html_Tag)
									' -------------------

									current_Position = tag_Start + html_Tag.Length
									attr_Location = _message_Body.ToLower().IndexOf(HTML_ATTRIBUTE_EVENT & "=", current_Position)

								End While

							End If

							_message.Append(_message_Body)

							' -- Add Attachment if required --
							If Not option_Attachments Is Nothing AndAlso option_Attachments.Count > 0 Then
								For Each option_Attachment As String In option_Attachments
									_message.AppendAttachment(option_Attachment, _
										option_Attachment.Substring(option_Attachment.LastIndexOf("\") + 1))
								Next
							End If

							Dim email_Node As XmlNode = single_Instance.SelectSingleNode(XML_ELEMENT_EMAIL)
							If email_Node Is Nothing Then
							
								' TODO: Handle Error '
							
							Else

								' -- Set Up the Hermes Recipient List --
								' Dim _recipients As Hermes.Email.Distribution(recipient_List_Type)
								Dim _recipients As New Hermes.Email.Distribution(Hermes.Email.SendingType.Single_Recipient)

								For Each email_Address As XmlNode in email_Node.SelectNodes(XML_ELEMENT_TO)

									Dim address As String = email_Address.InnerText
									If Not String.IsNullOrEmpty(address) Then _recipients.Add(address.ToLower())

								Next

								_manager.SendMessage(_from, _recipients, _message, Nothing, _
									String.Format("Athena Batch Mailing ({0})", exercise_Name), _replyTo)

								' -- Create & Persist a New Email Sent Action --
								Dim new_Action As New Action()
								new_Action.Instance = new_Instance.Id
								new_Action.Event = Get_Event(existing_Exercise, "Email Sent").Id
								new_Action.Agent = server_Agent.Id
								Action.All.Set(Data.Connection(Athena_Source), new_Action)
								' ----------------------------------------------

								option_Count(option_Name) += 1

							End If

							Host.Progress((prog_Index / prog_Total), "Sending Athena Instances")

							If all_Instances.Count > 100 Then System.Threading.Thread.Sleep(2000)

							prog_Index += 1

						Next

					End If

				Next

				If first_Batch.Attributes(XML_ATTRIBUTE_EXECUTED) Is Nothing Then

					Dim executed As XmlNode = source_Doc.CreateNode(XmlNodeType.Attribute, XML_ATTRIBUTE_EXECUTED, Nothing)
					executed.Value = "true"
					first_Batch.Attributes.SetNamedItem(executed)

				Else

					first_Batch.Attributes(XML_ATTRIBUTE_EXECUTED).Value = "true"

				End If

				If first_Batch.Attributes(XML_ATTRIBUTE_TIMESTAMP) Is Nothing Then

					Dim executed As XmlNode = source_Doc.CreateNode(XmlNodeType.Attribute, XML_ATTRIBUTE_TIMESTAMP, Nothing)
					executed.Value = System.Xml.XmlConvert.ToString(DateTime.Now(), System.Xml.XmlDateTimeSerializationMode.Local)
					first_Batch.Attributes.SetNamedItem(executed)

				Else

					first_Batch.Attributes(XML_ATTRIBUTE_TIMESTAMP).Value = _
						System.Xml.XmlConvert.ToString(DateTime.Now(), System.Xml.XmlDateTimeSerializationMode.Local)

				End If

				source_Doc.Save(source)

			End If
			
			For Each single_Option As String In option_Count.Keys

				results_Rows.Add(New Row().Add(single_Option).Add(option_Count(single_Option)))

			Next

			Return Cube.Create(IT.Information, "Batch Details" , "Option", "Emails Sent").Add(New Slice(results_Rows))

		End Function

	#End Region

End Class