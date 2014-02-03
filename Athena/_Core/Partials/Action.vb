Namespace Core

	Public Partial Class Action

		#Region " Public Shared Methods "

			Public Shared Function Create_Action( _
				ByVal _db As System.Data.IDbConnection, _
				ByVal _instance As Instance, _
				ByVal _event As [Event], _
				ByVal _agent As Agent _
			) As Action

				' -- Create New Action --
				Dim new_Action As New Action()
				new_Action.Instance = _instance.Id
				new_Action.Event = _event.Id
				new_Action.Agent = _agent.Id
				If Not Action.All.Set(_db, new_Action) Then _
					Return Nothing
				' ----------------------------------------------

				Return new_Action

			End Function

		#End Region

	End Class
	
End Namespace