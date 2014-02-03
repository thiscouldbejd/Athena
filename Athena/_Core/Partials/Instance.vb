Namespace Core

	Public Partial Class Instance

		#Region " Public Shared Methods "

			Public Shared Function Create_Instance( _
				ByVal _db As System.Data.IDbConnection, _
				ByVal _option As [Option] _
			) As Instance

				' -- Create New Instance --
				Dim new_Instance As New Instance()
				new_Instance.Option = _option.Id
				new_Instance.Code = Exercise.Generate_Code(_db, ObjectType.Instance)
				If Not Instance.All.Set(_db, new_Instance) Then _
					Return Nothing
				'' -------------------------

				Return new_Instance

			End Function

		#End Region

	End Class
	
End Namespace