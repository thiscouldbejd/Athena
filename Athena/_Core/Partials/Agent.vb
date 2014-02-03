Imports System.Linq

Namespace Core

	Public Partial Class Agent
	
		#Region " Public Shared Methods "

			Public Shared Function Get_Agent( _
				ByVal _db As System.Data.IDbConnection, _
				ByVal _details As String _
			) As Agent

				' -- Check whether Agent Already Exists --
				Dim existing_Agent As Agent = Agent.All.Filter(_db, Agent.All.Filters.DETAILS, _
					New Object() {_details}).FirstOrDefault()

				' -- If Not, Then Create a New One --
				If existing_Agent Is Nothing Then

					' -- Create New Agent --
					existing_Agent = New Agent()
					existing_Agent.Details = _details
					If Not Agent.All.Set(_db, existing_Agent) Then _
						Return Nothing
					' ----------------------

				End If

				Return existing_Agent

			End Function

		#End Region

	End Class
	
End Namespace