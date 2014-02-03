Imports Athena.Core
Imports System.Linq
Imports System.Net
Imports System.IO
Imports System.Xml
Imports System.Web
Imports System.Web.HttpUtility

Namespace Web

	Partial Public Class HtmlParser

		#Region " Private Constants "

			Private Const HTML_ATTRIBUTE_EVENT As String = "athena-event"

			Private Const HTML_ATTRIBUTE_INSTANCE As String = "athena-instance"

			Private Const HTML_ATTRIBUTE_COMPLETE As String = "athena-complete"

		#End Region

		#Region " Public Events "

			Public Event Parsed_Athena_Tracked_Url()

		#End Region

		#Region " Public Methods "

			Public Function Transform_Html( _
				ByVal db As System.Data.IDbConnection, _
				ByVal _exercise As Exercise, _
				ByVal _instance As Instance, _
				ByVal _completion As [Event], _
				ByVal _data As Dictionary(Of String, String), _
				ByVal _html As String, _
				Optional ByVal enable_Tracking As Boolean = True, _
				Optional ByVal tracking_Url As String = Nothing _
			) As String

				If Not _data Is Nothing AndAlso _data.Count > 0 Then
					For Each key As String In _data.Keys
						_html = _html.Replace("{" & key & "}", _data(key))
					Next
				End If

				If Not _instance Is Nothing Then _
					_html = _html.Replace("{" & HTML_ATTRIBUTE_INSTANCE & "}", _instance.Code)
				If Not _completion Is Nothing Then _
					_html = _html.Replace("{" & HTML_ATTRIBUTE_COMPLETE & "}", _completion.Code)

				If enable_Tracking Then

					Dim current_Position As Int32 = 0
					Dim attr_Location As Int32 = _html.ToLower().IndexOf(HTML_ATTRIBUTE_EVENT & "=", current_Position)

					While attr_Location >= 0

						' -- Get the Relevant HTML Tag --
						Dim tag_Start As Int32 = _html.LastIndexOf("<", attr_Location)
						Dim tag_End As Int32 =  _html.IndexOf(">", attr_Location) + 1 ' TODO: Is this robust enough?
						Dim html_Tag As String = _html.Substring(tag_Start, tag_End - tag_Start)
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
						Dim attr_Start As Int32 = attr_Location - tag_Start
						html_Tag = html_Tag.Remove(attr_Start, attr_Length)
						' ------------------------------------

						' -- Get the Event for this Attribute --
						Dim existing_Event As [Event] = [Event].Get_Event(db, _exercise, attr_Value)
						' --------------------------------------

						' -- Get the URL for this Attribute --
						Dim url_Delimiter As String = " "
						Dim url_Start As Integer = 0

						If html_Tag.ToLower().IndexOf("src=") > 0 Then
							url_Start = html_Tag.ToLower().IndexOf("src=") + "src=".Length
						ElseIf html_Tag.ToLower().IndexOf("href=") > 0 Then
							url_Start = html_Tag.ToLower().IndexOf("href=") + "href=".Length
						ElseIf html_Tag.ToLower().IndexOf("onclick=") > 0 AndAlso html_Tag.ToLower().IndexOf("$.get(") > 0 Then
							url_Start = html_Tag.ToLower().IndexOf("$.get(") + "$.get(".Length
						End If

						Dim url_Deliminator As String = " "

						Do Until Not url_Deliminator = " "
							url_Deliminator = html_Tag.Substring(url_Start, 1)
							url_Start += 1
						Loop
						
						Dim url_End As Int32 = html_Tag.IndexOf(url_Deliminator, url_Start)
						Dim url_Value As String = html_Tag.Substring(url_Start, url_End - url_Start)
						Dim url_NewValue As String = String.Format(tracking_Url.Replace("&amp;", "&"), _instance.Code, existing_Event.Code, UrlEncode(url_Value))
						html_Tag = html_Tag.Replace(url_Value, url_NewValue)
						' ------------------------------------

						' -- Clean Up HTML --
						_html = _html.Remove(tag_Start, tag_End - tag_Start) ' Removes entire HTML Tag
						_html = _html.Insert(tag_Start, html_Tag)
						' -------------------

						current_Position = tag_Start + html_Tag.Length
						attr_Location = _html.ToLower().IndexOf(HTML_ATTRIBUTE_EVENT & "=", current_Position)

					End While

				End If

				Return _html

			End Function

		#End Region

	End Class

End Namespace