Imports System.Linq

Namespace Core

	Public Partial Class [Option]

		#Region " Public Shared Methods "

			Public Shared Function Get_Option( _
				ByVal _db As System.Data.IDbConnection, _
				ByVal _exercise As Exercise, _
				ByVal _name As String _
			) As [Option]

				' -- Check whether Option Already Exists --
				Dim existing_Option As [Option] = [Option].All.Filter(_db, [Option].All.Filters.EXERCISE Or [Option].All.Filters.NAME, _
					New Object() {_exercise.Id, _name}).FirstOrDefault()

				' -- If Not, Then Create a New One --
				If existing_Option Is Nothing Then

					' -- Create New Option --
					existing_Option = New [Option]()
					existing_Option.Exercise = _exercise.Id
					existing_Option.Code = Athena.Core.Exercise.Generate_Code(_db, ObjectType.Option)
					existing_Option.Name = _name
					If Not [Option].All.Set(_db, existing_Option) Then _
						Return Nothing
					' -------------------------

				End If
				' -------------------------------------------

				Return existing_Option

			End Function

		#End Region

	End Class
	
End Namespace