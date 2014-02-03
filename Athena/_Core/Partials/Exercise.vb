Imports Hermes.Cryptography.Cipher
Imports System.Linq

Namespace Core

	Public Partial Class Exercise

		#Region " Friend Shared Methods "

			Protected Friend Shared Function Generate_Code( _
				ByVal _db As System.Data.IDbConnection, _
				ByVal _for As ObjectType _
			) As String

				' -- Create a new Code, ensuring the new Code is Unique --
				Dim proposed_Code As String = Nothing
				Dim exists_Proposed_Code As Boolean = True
				While exists_Proposed_Code
					proposed_Code = Create_Password(10, 0).ToLower()
					Select Case _for
						Case ObjectType.Exercise
							exists_Proposed_Code = Not Exercise.All.Filter(_db, Exercise.All.Filters.CODE, proposed_Code).FirstOrDefault() Is Nothing
						Case ObjectType.[Event]
							exists_Proposed_Code = Not [Event].All.Filter(_db, [Event].All.Filters.CODE, proposed_Code).FirstOrDefault() Is Nothing
						Case ObjectType.Option
							exists_Proposed_Code = Not [Option].All.Filter(_db, [Option].All.Filters.CODE, proposed_Code).FirstOrDefault() Is Nothing
						Case ObjectType.Instance
							exists_Proposed_Code = Not Instance.All.Filter(_db, Instance.All.Filters.CODE, proposed_Code).FirstOrDefault() Is Nothing
					End Select
				End While
				' --------------------------------------------------------

				Return proposed_Code
				
			End Function

		#End Region

		#Region " Public Shared Methods "

			Public Shared Function Get_Exercise( _
				ByVal _db As System.Data.IDbConnection, _
				ByVal _name As String _
			) As Exercise

				' -- Check whether Exercise Already Exists --
				Dim existing_Exercise As Exercise = Exercise.All.Filter(_db, Exercise.All.Filters.NAME, _
					New Object() {_name}).FirstOrDefault()

				' -- If Not, Then Create a New One --
				If existing_Exercise Is Nothing Then

					' -- Create New Exercise --
					existing_Exercise = New Exercise()
					existing_Exercise.Code = Exercise.Generate_Code(_db, ObjectType.Exercise)
					existing_Exercise.Name = _name
					If Not Exercise.All.Set(_db, existing_Exercise) Then _
						Return Nothing
					' -------------------------

				End If
				' -------------------------------------------

				return existing_Exercise

			End Function

		#End Region

	End Class
	
End Namespace