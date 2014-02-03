Imports Athena.Core
Imports Microsoft.VisualBasic
Imports System.Configuration.ConfigurationManager
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Web
Imports System.Web.HttpUtility

Namespace Web.Handlers

	Public Partial Class ContentHandler
		Implements System.Web.IHttpHandler

		#Region " IHttpHandler Implementation "

			Public Sub ProcessRequest( _
				ByVal context As System.Web.HttpContext _
			) Implements System.Web.IHttpHandler.ProcessRequest


				Dim html_Source As String = context.Request.QueryString(QUERY_SOURCE)

				If String.IsNullOrEmpty(html_Source) Then html_Source = context.Request.FilePath
				If html_Source.IndexOf("/") >= 0 Then _
					html_Source = html_Source.SubString(html_Source.LastIndexOf("/") + 1)
				If html_Source.IndexOf(".") >= 0 Then _
					html_Source = html_Source.SubString(0, html_Source.IndexOf("."))
				Dim page_Name As String = html_Source
				html_Source = html_Source + ".html"

				Dim incoming_Html As String = Hermes.Email.Message.LoadTextFromFile(context.Server.MapPath(html_Source))

				Dim exercise_Name As String = context.Request.QueryString(QUERY_EXERCISE)
				Dim option_Name As String = context.Request.QueryString(QUERY_OPTION)
				If String.IsNullOrEmpty(option_Name) Then option_Name = "All"

				If Not String.IsNullOrEmpty(exercise_Name) Then

					Try					

						If DB.State = ConnectionState.Closed Then DB.Open()

						' -- Get the Exercise --
						Dim existing_Exercise As Exercise = Exercise.Get_Exercise(DB, exercise_Name)

						' -- Get the Agent --
						Dim existing_Agent As Agent = Agent.Get_Agent(DB, context.Request.UserAgent)

						If Not existing_Exercise Is Nothing AndAlso _
							Not existing_Agent Is Nothing Then

							' -- Get the Option --
							Dim existing_Option As [Option] = [Option].Get_Option(DB, existing_Exercise, option_Name)

							' -- Create a New Instance --
							Dim new_Instance As Instance = Instance.Create_Instance(DB, existing_Option)

							' -- Transform the HTML --
							Dim new_Server As New HtmlParser(False)

							incoming_Html = new_Server.Transform_Html(DB, existing_Exercise, new_Instance, Nothing, Nothing, _
								incoming_Html, True, Url)

						End If

					Catch ex As Exception
					Finally
						If DB.State = ConnectionState.Open Then DB.Close()
					End Try

				End If

				context.Response.ClearContent()

				If Not String.IsNullOrEmpty(incoming_Html) Then _
					context.Response.Write(incoming_Html)

				context.Response.Flush()

			End Sub

		#End Region

	End Class

End Namespace