Namespace Core

	''' <summary></summary>
	''' <autogenerated>Generated from a T4 template. Modifications will be lost, if applicable use a partial class instead.</autogenerated>
	''' <generator-date>03/02/2014 13:07:05</generator-date>
	''' <generator-functions>1</generator-functions>
	''' <generator-source>Athena\Athena\_Core\Generated\Action.tt</generator-source>
	''' <generator-template>Text-Templates\Classes\VB_Object.tt</generator-template>
	''' <generator-version>1</generator-version>
	<System.CodeDom.Compiler.GeneratedCode("Athena\Athena\_Core\Generated\Action.tt", "1")> _
	<System.Serializable()> _
	Partial Public Class Action
		Inherits System.Object
		Implements System.IComparable
		Implements System.ComponentModel.INotifyPropertyChanged

		#Region " Public Constructors "

			''' <summary>Default Constructor</summary>
			Public Sub New()

				MyBase.New()

				m_Created = DateTime.Now()
			End Sub

			''' <summary>Parametered Constructor (1 Parameters)</summary>
			Public Sub New( _
				ByVal _Id As System.Int64 _
			)

				MyBase.New()

				m_Id = _Id

				m_Created = DateTime.Now()
			End Sub

			''' <summary>Parametered Constructor (2 Parameters)</summary>
			Public Sub New( _
				ByVal _Id As System.Int64, _
				ByVal _Instance As System.Int64 _
			)

				MyBase.New()

				m_Id = _Id
				Instance = _Instance

				m_Created = DateTime.Now()
			End Sub

			''' <summary>Parametered Constructor (3 Parameters)</summary>
			Public Sub New( _
				ByVal _Id As System.Int64, _
				ByVal _Instance As System.Int64, _
				ByVal _Event As System.Int64 _
			)

				MyBase.New()

				m_Id = _Id
				Instance = _Instance
				[Event] = _Event

				m_Created = DateTime.Now()
			End Sub

			''' <summary>Parametered Constructor (4 Parameters)</summary>
			Public Sub New( _
				ByVal _Id As System.Int64, _
				ByVal _Instance As System.Int64, _
				ByVal _Event As System.Int64, _
				ByVal _Agent As System.Int32 _
			)

				MyBase.New()

				m_Id = _Id
				Instance = _Instance
				[Event] = _Event
				Agent = _Agent

				m_Created = DateTime.Now()
			End Sub

			''' <summary>Parametered Constructor (5 Parameters)</summary>
			Public Sub New( _
				ByVal _Id As System.Int64, _
				ByVal _Instance As System.Int64, _
				ByVal _Event As System.Int64, _
				ByVal _Agent As System.Int32, _
				ByVal _Created As System.DateTime _
			)

				MyBase.New()

				m_Id = _Id
				Instance = _Instance
				[Event] = _Event
				Agent = _Agent
				m_Created = _Created

			End Sub

		#End Region

		#Region " Class Plumbing/Interface Code "

			#Region " IComparable Implementation "

				#Region " Public Methods "

					''' <summary>Comparison Method</summary>
					Public Overridable Function IComparable_CompareTo( _
						ByVal value As System.Object _
					) As System.Int32 Implements System.IComparable.CompareTo

						Dim typed_Value As Action = TryCast(value, Action)

						If typed_Value Is Nothing Then

							Throw New ArgumentException(String.Format("Value is not of comparable type: {0}", value.GetType.Name), "Value")

						Else

							Dim return_Value As Integer = 0

							return_Value = Created.CompareTo(typed_Value.Created)
							If return_Value <> 0 Then Return return_Value

							Return return_Value

						End If

					End Function

				#End Region

			#End Region

			#Region " DbRetrievable Implementation "

				#Region " Protected Methods "

					''' <summary>This method should be overridden to provide custom validation logic.</summary>
					Protected Overridable Function DbRetrievable_ValidateForSet() As Boolean

						Return True

					End Function

				#End Region

				Public Class All
					Implements System.Collections.Generic.IEnumerable(Of Action)
					Implements System.Collections.Generic.IEnumerator(Of Action)
					Implements System.ComponentModel.ITypedList

					#Region " Public Enums "

						<Flags()> _
						Public Enum Filters As System.Int64

							ID = 1
							INSTANCE = 2
							[EVENT] = 4
							AGENT = 8
							CREATED = 16

						End Enum

					#End Region

					#Region " Private Variables "

						''' <summary></summary>
						''' <remarks></remarks>
						Private IDX_ACTION_ID As System.Int32 = -1

						''' <summary></summary>
						''' <remarks></remarks>
						Private IDX_ACTION_INSTANCE As System.Int32 = -1

						''' <summary></summary>
						''' <remarks></remarks>
						Private IDX_ACTION_EVENT As System.Int32 = -1

						''' <summary></summary>
						''' <remarks></remarks>
						Private IDX_ACTION_AGENT As System.Int32 = -1

						''' <summary></summary>
						''' <remarks></remarks>
						Private IDX_ACTION_CREATED As System.Int32 = -1

					#End Region

					#Region " Protected Variables "

						''' <summary></summary>
						''' <remarks></remarks>
						Protected Cmd As System.Data.IDbCommand

						''' <summary></summary>
						''' <remarks></remarks>
						Protected Rdr As System.Data.IDataReader

						''' <summary></summary>
						''' <remarks></remarks>
						Protected Cur As Action

						''' <summary></summary>
						''' <remarks></remarks>
						Protected Ini As System.Boolean

						''' <summary></summary>
						''' <remarks></remarks>
						Protected Idx As System.Int32

						''' <summary></summary>
						''' <remarks></remarks>
						Protected Own As System.Int32 = System.Threading.Thread.CurrentThread.ManagedThreadId

					#End Region

					#Region " Protected Methods "

						Protected Function Hydrate( _
							ByVal reader As System.Data.IDataReader _
						) As Action

							Return Hydrate(New Action(), reader)

						End Function

						Protected Function Hydrate( _
							ByRef value As Action, _
							ByVal reader As System.Data.IDataReader _
						) As Action

							If Not reader.IsDBNull(IDX_ACTION_ID) Then _
								value.m_Id = reader.GetInt64(IDX_ACTION_ID)
							If Not reader.IsDBNull(IDX_ACTION_INSTANCE) Then _
								value.Instance = reader.GetInt64(IDX_ACTION_INSTANCE)
							If Not reader.IsDBNull(IDX_ACTION_EVENT) Then _
								value.Event = reader.GetInt64(IDX_ACTION_EVENT)
							If Not reader.IsDBNull(IDX_ACTION_AGENT) Then _
								value.Agent = reader.GetInt32(IDX_ACTION_AGENT)
							If Not reader.IsDBNull(IDX_ACTION_CREATED) Then _
								value.m_Created = reader.GetDateTime(IDX_ACTION_CREATED)
							Return value

						End Function

					#End Region

					#Region " Public Constructors "

						Public Sub New( _
							ByVal _cmd As System.Data.IDbCommand _
						)

							MyBase.New()

							Cmd = _cmd

						End Sub

					#End Region

					#Region " IEnumerator Implementation "

						Private ReadOnly Property GetCurrent_UnTyped() As Object Implements IEnumerator.Current
							Get
								Return Cur
							End Get
						End Property

						Private ReadOnly Property GetCurrent_Typed() As Action Implements IEnumerator(Of Action).Current
							Get
								Return Cur
							End Get
						End Property

						Private Function MoveNext() As Boolean Implements IEnumerator(Of Action).MoveNext

							If Not Ini Then Reset()
							If Rdr.Read() Then
								Cur = Hydrate(Rdr)
								Idx += 1
								Return True
							Else
								Cur = Nothing
								Return False
							End If

						End Function

						Private Sub Reset() Implements IEnumerator(Of Action).Reset

							If Not Rdr Is Nothing Then Rdr.Close()
							If Cmd.Connection.State = System.Data.ConnectionState.Closed Then Cmd.Connection.Open()
							Rdr = Cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
							Idx = -1

							IDX_ACTION_ID = rdr.GetOrdinal("Action_Id")
							If IDX_ACTION_ID < 0 Then Throw New Exception("Mapping For Field Not Correct: Action_Id")

							IDX_ACTION_INSTANCE = rdr.GetOrdinal("Action_Instance")
							If IDX_ACTION_INSTANCE < 0 Then Throw New Exception("Mapping For Field Not Correct: Action_Instance")

							IDX_ACTION_EVENT = rdr.GetOrdinal("Action_Event")
							If IDX_ACTION_EVENT < 0 Then Throw New Exception("Mapping For Field Not Correct: Action_Event")

							IDX_ACTION_AGENT = rdr.GetOrdinal("Action_Agent")
							If IDX_ACTION_AGENT < 0 Then Throw New Exception("Mapping For Field Not Correct: Action_Agent")

							IDX_ACTION_CREATED = rdr.GetOrdinal("Action_Created")
							If IDX_ACTION_CREATED < 0 Then Throw New Exception("Mapping For Field Not Correct: Action_Created")

							Ini = True

						End Sub

					#End Region

					#Region " IEnumerable Implementation "

						Private Function GetEnumerator_UnTyped As IEnumerator Implements IEnumerable.GetEnumerator

							Return GetEnumerator()

						End Function

						Private Function GetEnumerator As IEnumerator(Of Action) Implements IEnumerable(Of Action).GetEnumerator

							If System.Threading.Thread.CurrentThread.ManagedThreadId = Own AndAlso Idx = -1 Then
								Return Me
							Else
								Return New All(Cmd)
							End If

						End Function

					#End Region

					#Region " IDisposable Implementation "

						Private Sub Dispose() Implements IEnumerator(Of Action).Dispose

							If Not Rdr Is Nothing Then Rdr.Close()

						End Sub

					#End Region

					#Region " ITypedList Implementation "

						Public Function GetItemProperties( _
							ByVal listAccessors() As System.ComponentModel.PropertyDescriptor _
						) As System.ComponentModel.PropertyDescriptorCollection _
						Implements System.ComponentModel.ITypedList.GetItemProperties

							Return System.ComponentModel.TypeDescriptor.GetProperties(GetType(Action))

						End Function

						Public Function GetListName( _
							ByVal listAccessors() As System.ComponentModel.PropertyDescriptor _
						) As String _
						Implements System.ComponentModel.ITypedList.GetListName

							Return "Enumerable Collection of Action"

						End Function

					#End Region

					#Region " Public Shared Select Methods "

						Protected Friend Shared Function [Select]( _
							ByVal connection As System.Data.IDbConnection, _
							ByVal query As String _
						) As IEnumerable(Of Action)

							Return [Select](connection, query, _
								CType(Nothing, System.Collections.Generic.KeyValuePair(Of System.String, System.Object)()))

						End Function

						Protected Friend Shared Function [Select]( _
							ByVal connection As System.Data.IDbConnection, _
							ByVal query As String, _
							ByVal ParamArray parameters As System.Collections.Generic.KeyValuePair(Of System.String, System.Object)() _
						) As IEnumerable(Of Action)

							Dim command As System.Data.IDbCommand = connection.CreateCommand()
							If query.IndexOf(" ") > 0 Then command.CommandType = System.Data.CommandType.Text Else command.CommandType = System.Data.CommandType.StoredProcedure
							command.CommandText = query

								If Not parameters Is Nothing Then

									For i As Integer = 0 To parameters.Length - 1

										Dim param As System.Data.IDbDataParameter = command.CreateParameter()
										param.ParameterName = parameters(i).Key
										param.Value = parameters(i).Value
										command.Parameters.Add(param)

									Next

								End If

								Return New All(command)

							End Function

							Protected Friend Shared Function [Select]( _
								ByVal command As System.Data.IDbCommand, _
								ByVal ParamArray parameters As System.Data.IDbDataParameter() _
							) As IEnumerable(Of Action)

								If Not parameters Is Nothing Then

									For i As Integer = 0 To parameters.Length - 1

										If parameters(i).Value Is Nothing AndAlso (parameters(i).Direction = System.Data.ParameterDirection.InputOutput _
											OrElse parameters(i).Direction = System.Data.ParameterDirection.Input) Then parameters(i).Value = DBNull.Value

											command.Parameters.Add(parameters(i))

										Next

									End If

									Return New All(command)

								End Function

							#End Region

							#Region " Public Shared Enumeration Methods "

								Public Shared Function [Get]( _
									ByVal connection As System.Data.IDbConnection _
								) As IEnumerable(Of Action)

									Return [Get](connection, "tbl_Core_Actions")

								End Function

								Public Shared Function [Get]( _
									ByVal connection As System.Data.IDbConnection, _
									ByVal viewName As String _
								) As IEnumerable(Of Action)

									Return [Select](connection, String.Format("SELECT * FROM {0}", viewName))

								End Function

								Public Shared Function Filter( _
									ByVal connection As System.Data.IDbConnection, _
									ByVal field As Filters, _
									ByVal value As Object _
								) As IEnumerable(Of Action)

									Return Filter(connection, field, New Object() {value})

								End Function

								Public Shared Function Filter( _
									ByVal connection As System.Data.IDbConnection, _
									ByVal fields As Filters, _
									ByVal values As Object(), _
									Optional ByVal condition_AND As Boolean = True _
								) As IEnumerable(Of Action)

									Dim value_Index As Integer = 0
									Dim parameter_Names As New System.Text.StringBuilder()
									Dim parameter_Values As New System.Collections.Generic.List(Of System.Collections.Generic.KeyValuePair(Of System.String, System.Object))
									Dim condition_Operator As String = " AND "
									If Not condition_AND Then condition_Operator = " OR "

									If (fields And 1) = 1 Then
										If parameter_Names.Length > 0 Then parameter_Names.Append(condition_Operator)
										parameter_Names.Append("(Action_Id = ")
										If connection.GetType Is GetType(System.Data.OleDb.OleDbConnection) Then
											parameter_Names.Append("?)")
										Else
											parameter_Names.Append("@Id)")
										End If
										parameter_Values.Add(New System.Collections.Generic.KeyValuePair(Of System.String, System.Object)("Id", values(value_Index)))
										value_Index += 1
									End If

									If (fields And 2) = 2 Then
										If parameter_Names.Length > 0 Then parameter_Names.Append(condition_Operator)
										parameter_Names.Append("(Action_Instance = ")
										If connection.GetType Is GetType(System.Data.OleDb.OleDbConnection) Then
											parameter_Names.Append("?)")
										Else
											parameter_Names.Append("@Instance)")
										End If
										parameter_Values.Add(New System.Collections.Generic.KeyValuePair(Of System.String, System.Object)("Instance", values(value_Index)))
										value_Index += 1
									End If

									If (fields And 4) = 4 Then
										If parameter_Names.Length > 0 Then parameter_Names.Append(condition_Operator)
										parameter_Names.Append("(Action_Event = ")
										If connection.GetType Is GetType(System.Data.OleDb.OleDbConnection) Then
											parameter_Names.Append("?)")
										Else
											parameter_Names.Append("@Event)")
										End If
										parameter_Values.Add(New System.Collections.Generic.KeyValuePair(Of System.String, System.Object)("Event", values(value_Index)))
										value_Index += 1
									End If

									If (fields And 8) = 8 Then
										If parameter_Names.Length > 0 Then parameter_Names.Append(condition_Operator)
										parameter_Names.Append("(Action_Agent = ")
										If connection.GetType Is GetType(System.Data.OleDb.OleDbConnection) Then
											parameter_Names.Append("?)")
										Else
											parameter_Names.Append("@Agent)")
										End If
										parameter_Values.Add(New System.Collections.Generic.KeyValuePair(Of System.String, System.Object)("Agent", values(value_Index)))
										value_Index += 1
									End If

									If (fields And 16) = 16 Then
										If parameter_Names.Length > 0 Then parameter_Names.Append(condition_Operator)
										parameter_Names.Append("(Action_Created = ")
										If connection.GetType Is GetType(System.Data.OleDb.OleDbConnection) Then
											parameter_Names.Append("?)")
										Else
											parameter_Names.Append("@Created)")
										End If
										parameter_Values.Add(New System.Collections.Generic.KeyValuePair(Of System.String, System.Object)("Created", values(value_Index)))
										value_Index += 1
									End If

									Return [Select](connection, String.Format("SELECT * FROM {0} WHERE (" & parameter_Names.ToString() & ")", "tbl_Core_Actions"), _
										parameter_Values.ToArray())

								End Function

							#End Region

							#Region " Public Shared Manipulation Methods "

								Public Shared Function [Set]( _
									ByVal connection As System.Data.IDbConnection, _
									ByRef value As Action _
								) As Boolean

									Return [Set](connection, value, "tbl_Core_Actions")

								End Function

								Public Shared Function [Set]( _
									ByVal connection As System.Data.IDbConnection, _
									ByRef value As Action, _
									ByVal tableName As String _
								) As Boolean

									If value Is Nothing Then Throw New ArgumentNullException("value")
									If connection Is Nothing Then Throw New ArgumentNullException("connection")
									If String.IsNullOrEmpty(tableName) Then Throw New ArgumentNullException("tableName")
									If connection.State = System.Data.ConnectionState.Closed Then connection.Open()

									Dim return_Value As Boolean = False

									If value.DbRetrievable_ValidateForSet Then

										Dim insert_New As Boolean = (value.Id <= 0)

										If connection.GetType Is GetType(System.Data.OleDb.OleDbConnection) Then

											Dim OLEDB_Proc As String

											If insert_New Then
												OLEDB_Proc = String.Format("INSERT INTO {0} (Action_Instance, Action_Event, Action_Agent) VALUES (?, ?, ?)", tableName)
											Else
												OLEDB_Proc = String.Format("UPDATE {0} SET Action_Instance = ?, Action_Event = ?, Action_Agent = ? WHERE (Action_Id = ?)", tableName)
											End If

											Dim OLEDB_Comm As New System.Data.OleDb.OleDbCommand(OLEDB_Proc, CType(connection, System.Data.OleDb.OleDbConnection))
											OLEDB_Comm.CommandType = System.Data.CommandType.Text

											OLEDB_Comm.Parameters.Add(New System.Data.OleDb.OleDbParameter("Instance", value.Instance))

											OLEDB_Comm.Parameters.Add(New System.Data.OleDb.OleDbParameter("Event", value.Event))

											OLEDB_Comm.Parameters.Add(New System.Data.OleDb.OleDbParameter("Agent", value.Agent))

											If Not insert_New Then OLEDB_Comm.Parameters.Add(New System.Data.OleDb.OleDbParameter("Id", value.Id))
											return_Value = (OLEDB_Comm.ExecuteNonQuery() = 1)

											If insert_New Then

												Dim OLEDB_Identity As New System.Data.OleDb.OleDbCommand("SELECT @@IDENTITY", CType(connection, System.Data.OleDb.OleDbConnection))
												OLEDB_Identity.CommandType = System.Data.CommandType.Text
												value.m_Id = CType(OLEDB_Identity.ExecuteScalar(), System.Int64)

											End If

										ElseIf connection.GetType Is GetType(System.Data.SqlClient.SqlConnection) Then

											Dim SQL_Proc As String
											If insert_New Then
												SQL_Proc = String.Format("INSERT INTO {0} (Action_Instance, Action_Event, Action_Agent) VALUES (@Instance, @Event, @Agent); SELECT @Id = SCOPE_IDENTITY();", tableName)
											Else
												SQL_Proc = String.Format("UPDATE {0} SET Action_Instance = @Instance, Action_Event = @Event, Action_Agent = @Agent WHERE (Action_Id = @Id)", tableName)
											End If

											Dim SQL_Comm As New System.Data.SqlClient.SqlCommand(SQL_Proc, CType(connection, System.Data.SqlClient.SqlConnection))
											SQL_Comm.CommandType = System.Data.CommandType.Text

											Dim param_1 As New System.Data.SqlClient.SqlParameter("@Id", value.Id)
											If insert_New Then param_1.Direction = System.Data.ParameterDirection.Output
											SQL_Comm.Parameters.Add(param_1)

											Dim param_2 As System.Data.SqlClient.SqlParameter
											param_2 = New System.Data.SqlClient.SqlParameter("@Instance", value.Instance)
											SQL_Comm.Parameters.Add(param_2)

											Dim param_3 As System.Data.SqlClient.SqlParameter
											param_3 = New System.Data.SqlClient.SqlParameter("@Event", value.Event)
											SQL_Comm.Parameters.Add(param_3)

											Dim param_4 As System.Data.SqlClient.SqlParameter
											param_4 = New System.Data.SqlClient.SqlParameter("@Agent", value.Agent)
											SQL_Comm.Parameters.Add(param_4)

											return_Value = (SQL_Comm.ExecuteNonQuery() = 1)

											If insert_New Then value.m_Id = CType(param_1.Value, System.Int64)

										Else

											Throw New NotImplementedException(String.Format("DB Connection of Type: {0} Not Yet Supported", connection.GetType.Name))

										End If

									End If

									If connection.State = System.Data.ConnectionState.Open Then connection.Close()

									Return return_Value

								End Function

								Public Shared Function [Create]( _
									ByVal connection As System.Data.IDbConnection, _
									ByVal _instance as System.Int64, _
									ByVal _event as System.Int64, _
									ByVal _agent as System.Int32 _
								) As Boolean

									Return [Create](connection, _instance, _event, _agent, "tbl_Core_Actions")

								End Function

								Public Shared Function [Create]( _
									ByVal connection As System.Data.IDbConnection, _
									ByVal _instance as System.Int64, _
									ByVal _event as System.Int64, _
									ByVal _agent as System.Int32, _
									ByVal tableName As String _
								) As Boolean

									If connection Is Nothing Then Throw New ArgumentNullException("connection")
									If String.IsNullOrEmpty(tableName) Then Throw New ArgumentNullException("tableName")
									If connection.State = System.Data.ConnectionState.Closed Then connection.Open()

									Dim return_Value As Boolean = False

									Dim value As New Action()
									value.Instance = _instance
									value.Event = _event
									value.Agent = _agent

									If value.DbRetrievable_ValidateForSet Then

										If connection.GetType Is GetType(System.Data.OleDb.OleDbConnection) Then

											Dim OLEDB_Comm As New System.Data.OleDb.OleDbCommand(String.Format("INSERT INTO {0} (Action_Instance, Action_Event, Action_Agent) VALUES (?, ?, ?)", tableName), CType(connection, System.Data.OleDb.OleDbConnection))
											OLEDB_Comm.CommandType = System.Data.CommandType.Text

											OLEDB_Comm.Parameters.Add(New System.Data.OleDb.OleDbParameter("Instance", value.Instance))

											OLEDB_Comm.Parameters.Add(New System.Data.OleDb.OleDbParameter("Event", value.Event))

											OLEDB_Comm.Parameters.Add(New System.Data.OleDb.OleDbParameter("Agent", value.Agent))

											return_Value = (OLEDB_Comm.ExecuteNonQuery() = 1)

										ElseIf connection.GetType Is GetType(System.Data.SqlClient.SqlConnection) Then

											Dim SQL_Comm As New System.Data.SqlClient.SqlCommand(String.Format("INSERT INTO {0} (Action_Instance, Action_Event, Action_Agent) VALUES (@Instance, @Event, @Agent); SELECT @Id = SCOPE_IDENTITY();", tableName), CType(connection, System.Data.SqlClient.SqlConnection))
											SQL_Comm.CommandType = System.Data.CommandType.Text

											Dim param_1 As System.Data.SqlClient.SqlParameter
											param_1 = New System.Data.SqlClient.SqlParameter("@Instance", value.Instance)
											SQL_Comm.Parameters.Add(param_1)

											Dim param_2 As System.Data.SqlClient.SqlParameter
											param_2 = New System.Data.SqlClient.SqlParameter("@Event", value.Event)
											SQL_Comm.Parameters.Add(param_2)

											Dim param_3 As System.Data.SqlClient.SqlParameter
											param_3 = New System.Data.SqlClient.SqlParameter("@Agent", value.Agent)
											SQL_Comm.Parameters.Add(param_3)

											return_Value = (SQL_Comm.ExecuteNonQuery() = 1)

										Else

											Throw New NotImplementedException(String.Format("DB Connection of Type: {0} Not Yet Supported", connection.GetType.Name))

										End If

									End If

									If connection.State = System.Data.ConnectionState.Open Then connection.Close()

									Return return_Value

								End Function

							#End Region

							#Region " Public Shared Identity Methods "

								Public Shared Function [One]( _
									ByVal connection As System.Data.IDbConnection, _
									ByRef value As Action _
								) As Action

									Return [One](connection, value, "tbl_Core_Actions")

								End Function

								Public Shared Function [One]( _
									ByVal connection As System.Data.IDbConnection, _
									ByVal id As System.Int64 _
								) As Action

									Return [One](connection, id, "tbl_Core_Actions")

								End Function

								Public Shared Function [One]( _
									ByVal connection As System.Data.IDbConnection, _
									ByRef value As Action, _
									ByVal viewName As String _
								) As Action

									Return [One](connection, value.Id, viewName)

								End Function

								Public Shared Function [One]( _
									ByVal connection As System.Data.IDbConnection, _
									ByVal id As System.Int64, _
									ByVal viewName As String _
								) As Action

									If connection.GetType Is GetType(System.Data.OleDb.OleDbConnection) Then

										Return System.Linq.Enumerable.FirstOrDefault([Select](connection, _
											String.Format("SELECT * FROM {0} WHERE (Action_Id = ?)", viewName), _
											New System.Collections.Generic.KeyValuePair(Of System.String, System.Object)("Id", id)))

									Else

										Return System.Linq.Enumerable.FirstOrDefault([Select](connection, _
											String.Format("SELECT * FROM {0} WHERE (Action_Id = @Id)", viewName), _
											New System.Collections.Generic.KeyValuePair(Of System.String, System.Object)("Id", id)))

									End If

								End Function

								Public Shared Function [Delete]( _
									ByVal connection As System.Data.IDbConnection, _
									ByRef value As Action _
								) As Boolean

									Return [Delete](connection, value, "tbl_Core_Actions")

								End Function

								Public Shared Function [Delete]( _
									ByVal connection As System.Data.IDbConnection, _
									ByVal id As System.Int64 _
								) As Boolean

									Return [Delete](connection, id, "tbl_Core_Actions")

								End Function

								Public Shared Function [Delete]( _
									ByVal connection As System.Data.IDbConnection, _
									ByRef value As Action, _
									ByVal tableName As String _
								) As Boolean

									Return [Delete](connection, value.Id, tableName)

								End Function

								Public Shared Function [Delete]( _
									ByVal connection As System.Data.IDbConnection, _
									ByVal id As System.Int64, _
									ByVal tableName As String _
								) As Boolean

									If (id <= 0) Then Throw New ArgumentNullException("value")
									If connection Is Nothing Then Throw New ArgumentNullException("connection")
									If String.IsNullOrEmpty(tableName) Then Throw New ArgumentNullException("tableName")
									If connection.State = System.Data.ConnectionState.Closed Then connection.Open()

									Dim return_Value As Boolean = False

									If connection.GetType Is GetType(System.Data.OleDb.OleDbConnection) Then

										Dim OLEDB_Proc As String = String.Format("DELETE FROM {0} WHERE (Action_Id = ?)", tableName)

										Dim OLEDB_Comm As New System.Data.OleDb.OleDbCommand(OLEDB_Proc, CType(connection, System.Data.OleDb.OleDbConnection))
										OLEDB_Comm.CommandType = System.Data.CommandType.Text

										OLEDB_Comm.Parameters.Add(New System.Data.OleDb.OleDbParameter("Id", id))

										return_Value = (OLEDB_Comm.ExecuteNonQuery() = 1)

									ElseIf connection.GetType Is GetType(System.Data.SqlClient.SqlConnection) Then

										Dim SQL_Proc As String = String.Format("DELETE FROM {0} WHERE (Action_Id = @Id)", tableName)

										Dim SQL_Comm As New System.Data.SqlClient.SqlCommand(SQL_Proc, CType(connection, System.Data.SqlClient.SqlConnection))
										SQL_Comm.CommandType = System.Data.CommandType.Text

										SQL_Comm.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Id", id))

										return_Value = (SQL_Comm.ExecuteNonQuery() = 1)

									Else

										Throw New NotImplementedException(String.Format("DB Connection of Type: {0} Not Yet Supported", connection.GetType.Name))

									End If

									If connection.State = System.Data.ConnectionState.Open Then connection.Close()

									Return return_Value

								End Function

							#End Region

						End Class

					#End Region

			#Region " INotifyPropertyChanged Implementation "

				#Region " Public Events "

					''' <summary></summary>
					''' <remarks></remarks>
					Public Event INotifyPropertyChanged_PropertyChanged( _
						ByVal sender As System.Object, _
						ByVal e As System.ComponentModel.PropertyChangedEventArgs _
					) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

				#End Region

				#Region " Protected Methods "

					''' <summary></summary>
					''' <remarks></remarks>
					Protected Sub INotifyPropertyChanged_RaiseChanged( _
						ByVal propertyName As System.String _
					)
						RaiseEvent INotifyPropertyChanged_PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(propertyName))
					End Sub

				#End Region

			#End Region

		#End Region

		#Region " Public Constants "

			''' <summary>Public Shared Reference to the Name of the Property: Id</summary>
			''' <remarks></remarks>
			Public Const PROPERTY_ID As String = "Id"

			''' <summary>Public Shared Reference to the Name of the Property: Instance</summary>
			''' <remarks></remarks>
			Public Const PROPERTY_INSTANCE As String = "Instance"

			''' <summary>Public Shared Reference to the Name of the Property: Event</summary>
			''' <remarks></remarks>
			Public Const PROPERTY_EVENT As String = "Event"

			''' <summary>Public Shared Reference to the Name of the Property: Agent</summary>
			''' <remarks></remarks>
			Public Const PROPERTY_AGENT As String = "Agent"

			''' <summary>Public Shared Reference to the Name of the Property: Created</summary>
			''' <remarks></remarks>
			Public Const PROPERTY_CREATED As String = "Created"

		#End Region

		#Region " Private Variables "

			''' <summary>Private Data Storage Variable for Property: Id</summary>
			''' <remarks></remarks>
			Private m_Id As System.Int64

			''' <summary>Private Data Storage Variable for Property: Instance</summary>
			''' <remarks></remarks>
			Private m_Instance As System.Int64

			''' <summary>Private Data Storage Variable for Property: Event</summary>
			''' <remarks></remarks>
			Private m_Event As System.Int64

			''' <summary>Private Data Storage Variable for Property: Agent</summary>
			''' <remarks></remarks>
			Private m_Agent As System.Int32

			''' <summary>Private Data Storage Variable for Property: Created</summary>
			''' <remarks></remarks>
			Private m_Created As System.DateTime

		#End Region

		#Region " Public Properties "

			''' <summary>Provides Access to the Property: Id</summary>
			''' <remarks></remarks>
			<System.Xml.Serialization.XmlElement(ElementName:="Id")> _
			Public ReadOnly Property Id() As System.Int64
				Get
					Return m_Id
				End Get
			End Property

			''' <summary>Provides Access to the Property: Instance</summary>
			''' <remarks></remarks>
			<System.Xml.Serialization.XmlElement(ElementName:="Instance")> _
			Public Property Instance() As System.Int64
				Get
					Return m_Instance
				End Get
				Set(value As System.Int64)
					m_Instance = value
				End Set
			End Property

			''' <summary>Provides Access to the Property: Event</summary>
			''' <remarks></remarks>
			<System.Xml.Serialization.XmlElement(ElementName:="Event")> _
			Public Property [Event]() As System.Int64
				Get
					Return m_Event
				End Get
				Set(value As System.Int64)
					m_Event = value
				End Set
			End Property

			''' <summary>Provides Access to the Property: Agent</summary>
			''' <remarks></remarks>
			<System.Xml.Serialization.XmlElement(ElementName:="Agent")> _
			Public Property Agent() As System.Int32
				Get
					Return m_Agent
				End Get
				Set(value As System.Int32)
					m_Agent = value
				End Set
			End Property

			''' <summary>Provides Access to the Property: Created</summary>
			''' <remarks></remarks>
			<System.Xml.Serialization.XmlElement(ElementName:="Created")> _
			Public ReadOnly Property Created() As System.DateTime
				Get
					Return m_Created
				End Get
			End Property

		#End Region

	End Class

End Namespace