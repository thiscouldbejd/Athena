Namespace Analysis

	Public Partial Class Result

		#Region " Public Constants "

			Public Const NAME_DELINEATOR As String = "//"

		#End Region

		#Region " Public Properties "

			Public ReadOnly Property Names As String()
				Get
					If String.IsNullOrEmpty(Name) Then
						Return New String() {}
					Else
						Return Name.Split(New String() {NAME_DELINEATOR}, System.StringSplitOptions.RemoveEmptyEntries)
					End If
				End Get
			End Property
			
			Public ReadOnly Property Display_Name As System.String
				Get
					Return Name.Replace(NAME_DELINEATOR, " ")
				End Get
			End Property

			Public ReadOnly Property Average_Edge_Count As System.Double
				Get
					If Edges Is Nothing OrElse Edges.Count = 0 Then
						Return 0
					Else
						Dim ret_Val As Double
						For i As Integer = 0 To Edges.Count - 1
							ret_Val += Edges(i).Count
						Next
						Return ret_Val / Edges.Count
					End If
				End Get
			End Property

		#End Region

	End Class
	
End Namespace