Namespace Analysis

	''' <summary></summary>
	''' <autogenerated>Generated from a T4 template. Modifications will be lost, if applicable use a partial class instead.</autogenerated>
	''' <generator-date>03/02/2014 13:05:49</generator-date>
	''' <generator-functions>1</generator-functions>
	''' <generator-source>Athena\Athena\_Analysis\Generated\Edge.tt</generator-source>
	''' <generator-template>Text-Templates\Classes\VB_Object.tt</generator-template>
	''' <generator-version>1</generator-version>
	<System.CodeDom.Compiler.GeneratedCode("Athena\Athena\_Analysis\Generated\Edge.tt", "1")> _
	<System.Serializable()> _
	Partial Public Class Edge
		Inherits System.Object
		Implements System.IComparable
		Implements System.ComponentModel.INotifyPropertyChanged

		#Region " Public Constructors "

			''' <summary>Default Constructor</summary>
			Public Sub New()

				MyBase.New()

			End Sub

			''' <summary>Parametered Constructor (1 Parameters)</summary>
			Public Sub New( _
				ByVal _Name As System.String _
			)

				MyBase.New()

				Name = _Name

			End Sub

			''' <summary>Parametered Constructor (2 Parameters)</summary>
			Public Sub New( _
				ByVal _Name As System.String, _
				ByVal _Earliest As System.DateTime _
			)

				MyBase.New()

				Name = _Name
				Earliest = _Earliest

			End Sub

			''' <summary>Parametered Constructor (3 Parameters)</summary>
			Public Sub New( _
				ByVal _Name As System.String, _
				ByVal _Earliest As System.DateTime, _
				ByVal _Count As System.Int32 _
			)

				MyBase.New()

				Name = _Name
				Earliest = _Earliest
				Count = _Count

			End Sub

			''' <summary>Parametered Constructor (4 Parameters)</summary>
			Public Sub New( _
				ByVal _Name As System.String, _
				ByVal _Earliest As System.DateTime, _
				ByVal _Count As System.Int32, _
				ByVal _Average As System.Double _
			)

				MyBase.New()

				Name = _Name
				Earliest = _Earliest
				Count = _Count
				Average = _Average

			End Sub

			''' <summary>Parametered Constructor (5 Parameters)</summary>
			Public Sub New( _
				ByVal _Name As System.String, _
				ByVal _Earliest As System.DateTime, _
				ByVal _Count As System.Int32, _
				ByVal _Average As System.Double, _
				ByVal _Min As System.Double _
			)

				MyBase.New()

				Name = _Name
				Earliest = _Earliest
				Count = _Count
				Average = _Average
				Min = _Min

			End Sub

			''' <summary>Parametered Constructor (6 Parameters)</summary>
			Public Sub New( _
				ByVal _Name As System.String, _
				ByVal _Earliest As System.DateTime, _
				ByVal _Count As System.Int32, _
				ByVal _Average As System.Double, _
				ByVal _Min As System.Double, _
				ByVal _Max As System.Double _
			)

				MyBase.New()

				Name = _Name
				Earliest = _Earliest
				Count = _Count
				Average = _Average
				Min = _Min
				Max = _Max

			End Sub

		#End Region

		#Region " Class Plumbing/Interface Code "

			#Region " IComparable Implementation "

				#Region " Public Methods "

					''' <summary>Comparison Method</summary>
					Public Overridable Function IComparable_CompareTo( _
						ByVal value As System.Object _
					) As System.Int32 Implements System.IComparable.CompareTo

						Dim typed_Value As Edge = TryCast(value, Edge)

						If typed_Value Is Nothing Then

							Throw New ArgumentException(String.Format("Value is not of comparable type: {0}", value.GetType.Name), "Value")

						Else

							Dim return_Value As Integer = 0

							return_Value = Earliest.CompareTo(typed_Value.Earliest)
							If return_Value <> 0 Then Return return_Value

							Return return_Value

						End If

					End Function

				#End Region

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

			''' <summary>Public Shared Reference to the Name of the Property: Name</summary>
			''' <remarks></remarks>
			Public Const PROPERTY_NAME As String = "Name"

			''' <summary>Public Shared Reference to the Name of the Property: Earliest</summary>
			''' <remarks></remarks>
			Public Const PROPERTY_EARLIEST As String = "Earliest"

			''' <summary>Public Shared Reference to the Name of the Property: Count</summary>
			''' <remarks></remarks>
			Public Const PROPERTY_COUNT As String = "Count"

			''' <summary>Public Shared Reference to the Name of the Property: Average</summary>
			''' <remarks></remarks>
			Public Const PROPERTY_AVERAGE As String = "Average"

			''' <summary>Public Shared Reference to the Name of the Property: Min</summary>
			''' <remarks></remarks>
			Public Const PROPERTY_MIN As String = "Min"

			''' <summary>Public Shared Reference to the Name of the Property: Max</summary>
			''' <remarks></remarks>
			Public Const PROPERTY_MAX As String = "Max"

		#End Region

		#Region " Private Variables "

			''' <summary>Private Data Storage Variable for Property: Name</summary>
			''' <remarks></remarks>
			Private m_Name As System.String

			''' <summary>Private Data Storage Variable for Property: Earliest</summary>
			''' <remarks></remarks>
			Private m_Earliest As System.DateTime

			''' <summary>Private Data Storage Variable for Property: Count</summary>
			''' <remarks></remarks>
			Private m_Count As System.Int32

			''' <summary>Private Data Storage Variable for Property: Average</summary>
			''' <remarks></remarks>
			Private m_Average As System.Double

			''' <summary>Private Data Storage Variable for Property: Min</summary>
			''' <remarks></remarks>
			Private m_Min As System.Double

			''' <summary>Private Data Storage Variable for Property: Max</summary>
			''' <remarks></remarks>
			Private m_Max As System.Double

		#End Region

		#Region " Public Properties "

			''' <summary>Provides Access to the Property: Name</summary>
			''' <remarks></remarks>
			<System.Xml.Serialization.XmlElement(ElementName:="Name")> _
			Public Property Name() As System.String
				Get
					Return m_Name
				End Get
				Set(value As System.String)
					m_Name = value
				End Set
			End Property

			''' <summary>Provides Access to the Property: Earliest</summary>
			''' <remarks></remarks>
			<System.Xml.Serialization.XmlElement(ElementName:="Earliest")> _
			Public Property Earliest() As System.DateTime
				Get
					Return m_Earliest
				End Get
				Set(value As System.DateTime)
					m_Earliest = value
				End Set
			End Property

			''' <summary>Provides Access to the Property: Count</summary>
			''' <remarks></remarks>
			<System.Xml.Serialization.XmlElement(ElementName:="Count")> _
			Public Property Count() As System.Int32
				Get
					Return m_Count
				End Get
				Set(value As System.Int32)
					m_Count = value
				End Set
			End Property

			''' <summary>Provides Access to the Property: Average</summary>
			''' <remarks></remarks>
			<System.Xml.Serialization.XmlElement(ElementName:="Average")> _
			Public Property Average() As System.Double
				Get
					Return m_Average
				End Get
				Set(value As System.Double)
					m_Average = value
				End Set
			End Property

			''' <summary>Provides Access to the Property: Min</summary>
			''' <remarks></remarks>
			<System.Xml.Serialization.XmlElement(ElementName:="Min")> _
			Public Property Min() As System.Double
				Get
					Return m_Min
				End Get
				Set(value As System.Double)
					m_Min = value
				End Set
			End Property

			''' <summary>Provides Access to the Property: Max</summary>
			''' <remarks></remarks>
			<System.Xml.Serialization.XmlElement(ElementName:="Max")> _
			Public Property Max() As System.Double
				Get
					Return m_Max
				End Get
				Set(value As System.Double)
					m_Max = value
				End Set
			End Property

		#End Region

	End Class

End Namespace