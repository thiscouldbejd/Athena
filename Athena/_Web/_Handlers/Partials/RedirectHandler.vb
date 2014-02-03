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

	Public Class RedirectHandler

		#Region " IHttpHandler Implementation "

			Public Sub ProcessRequest( _
				ByVal context As System.Web.HttpContext _
			) Implements System.Web.IHttpHandler.ProcessRequest

				Dim redirect_To As String = context.Request.QueryString(QUERY_REDIRECT)

				Dim instance_Code As String = context.Request.QueryString(QUERY_INSTANCE)
				
				Dim event_Code As String = context.Request.QueryString(QUERY_EVENT)

				If Not String.IsNullOrEmpty(instance_Code) AndAlso Not String.IsNullOrEmpty(event_Code) Then

					Try					

						If DB.State = ConnectionState.Closed Then DB.Open()

						' -- Get the Instance to Which this refers --
						Dim existing_Instance As Instance = Instance.All.Filter(DB, Instance.All.Filters.CODE, instance_Code.ToLower()).FirstOrDefault()
						If existing_Instance Is Nothing Then
						End If
						' -------------------------------------------

						' -- Get the Event to Which this refers --
						Dim existing_Event As [Event] = [Event].All.Filter(DB, [Event].All.Filters.CODE, event_Code.ToLower()).FirstOrDefault()
						If existing_Event Is Nothing Then
						End If
						' ----------------------------------------

						' -- Get the Agent to Which this refers --
						Dim existing_Agent As Agent = Agent.All.Filter(DB, Agent.All.Filters.DETAILS, context.Request.UserAgent).FirstOrDefault()
						If existing_Agent Is Nothing Then
							existing_Agent = New Agent()
							existing_Agent.Details = context.Request.UserAgent
							Agent.All.Set(DB, existing_Agent)
						End If
						' ----------------------------------------

						' -- Create & Persist a New Action --
						Dim new_Action As New Action()
						new_Action.Instance = existing_Instance.Id
						new_Action.Event = existing_Event.Id
						new_Action.Agent = existing_Agent.Id
						Action.All.Set(DB, new_Action)
						' -----------------------------------

					Catch ex As Exception
					
					End Try

				End If

				context.Response.ClearContent()

				If Not String.IsNullOrEmpty(redirect_To) Then

					context.Response.Redirect(UrlDecode(redirect_To), false)
					context.ApplicationInstance.CompleteRequest()

				Else

					context.Response.Flush()

				End If

			End Sub

		#End Region

	End Class

End Namespace