Imports System.Linq

Namespace Core

	Public Partial Class [Event]

		#Region " Public Shared Methods "

			Public Shared Function Get_Event( _
				ByVal _db As System.Data.IDbConnection, _
				ByVal _exercise As Exercise, _
				ByVal _name As String _
			) As [Event]

				' -- Check whether Event Already Exists --
				Dim existing_Event As [Event] = [Event].All.Filter(_db, [Event].All.Filters.EXERCISE Or [Event].All.Filters.NAME, _
					New Object() {_exercise.Id, _name}).FirstOrDefault()

				' -- If Not, Then Create a New One --
				If existing_Event Is Nothing Then

					' -- Create New Event --
					existing_Event = New [Event]()
					existing_Event.Exercise = _exercise.Id
					existing_Event.Code = Athena.Core.Exercise.Generate_Code(_db, ObjectType.[Event])
					existing_Event.Name = _name
					If Not [Event].All.Set(_db, existing_Event) Then _
						Return Nothing
					' -------------------------

				End If
				' -------------------------------------------

				return existing_Event

			End Function

		#End Region

	End Class
	
End Namespace