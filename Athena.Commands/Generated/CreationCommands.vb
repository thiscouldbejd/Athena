''' <summary></summary>
''' <autogenerated>Generated from a T4 template. Modifications will be lost, if applicable use a partial class instead.</autogenerated>
''' <generator-date>08/02/2014 17:30:12</generator-date>
''' <generator-functions>1</generator-functions>
''' <generator-source>Athena\Athena.Commands\Generated\CreationCommands.tt</generator-source>
''' <generator-template>Text-Templates\Classes\VB_Object.tt</generator-template>
''' <generator-version>1</generator-version>
<System.CodeDom.Compiler.GeneratedCode("Athena\Athena.Commands\Generated\CreationCommands.tt", "1")> _
<Leviathan.Commands.Command(ResourceContainingType:=GetType(CreationCommands), ResourceName:="CommandDetails", Name:="athena-create", Description:="Athena Creation Commands", Hidden:=False)> _
Partial Public Class CreationCommands
	Inherits System.Object

	#Region " Public Constructors "

		''' <summary>Parametered Constructor (1 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution _
		)

			MyBase.New()

			Host = _Host

			m_MailServerPort = 25
			m_Data = New DataCommands(Host)
		End Sub

		''' <summary>Parametered Constructor (2 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution, _
			ByVal _Athena_Source As System.String _
		)

			MyBase.New()

			Host = _Host
			Athena_Source = _Athena_Source

			m_MailServerPort = 25
			m_Data = New DataCommands(Host)
		End Sub

		''' <summary>Parametered Constructor (3 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution, _
			ByVal _Athena_Source As System.String, _
			ByVal _Athena_Url As System.String _
		)

			MyBase.New()

			Host = _Host
			Athena_Source = _Athena_Source
			Athena_Url = _Athena_Url

			m_MailServerPort = 25
			m_Data = New DataCommands(Host)
		End Sub

		''' <summary>Parametered Constructor (4 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution, _
			ByVal _Athena_Source As System.String, _
			ByVal _Athena_Url As System.String, _
			ByVal _MailLog As System.IO.DirectoryInfo _
		)

			MyBase.New()

			Host = _Host
			Athena_Source = _Athena_Source
			Athena_Url = _Athena_Url
			MailLog = _MailLog

			m_MailServerPort = 25
			m_Data = New DataCommands(Host)
		End Sub

		''' <summary>Parametered Constructor (5 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution, _
			ByVal _Athena_Source As System.String, _
			ByVal _Athena_Url As System.String, _
			ByVal _MailLog As System.IO.DirectoryInfo, _
			ByVal _MailSuppress As System.Boolean _
		)

			MyBase.New()

			Host = _Host
			Athena_Source = _Athena_Source
			Athena_Url = _Athena_Url
			MailLog = _MailLog
			MailSuppress = _MailSuppress

			m_MailServerPort = 25
			m_Data = New DataCommands(Host)
		End Sub

		''' <summary>Parametered Constructor (6 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution, _
			ByVal _Athena_Source As System.String, _
			ByVal _Athena_Url As System.String, _
			ByVal _MailLog As System.IO.DirectoryInfo, _
			ByVal _MailSuppress As System.Boolean, _
			ByVal _MailServer As System.String _
		)

			MyBase.New()

			Host = _Host
			Athena_Source = _Athena_Source
			Athena_Url = _Athena_Url
			MailLog = _MailLog
			MailSuppress = _MailSuppress
			MailServer = _MailServer

			m_MailServerPort = 25
			m_Data = New DataCommands(Host)
		End Sub

		''' <summary>Parametered Constructor (7 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution, _
			ByVal _Athena_Source As System.String, _
			ByVal _Athena_Url As System.String, _
			ByVal _MailLog As System.IO.DirectoryInfo, _
			ByVal _MailSuppress As System.Boolean, _
			ByVal _MailServer As System.String, _
			ByVal _MailServerPort As System.Int32 _
		)

			MyBase.New()

			Host = _Host
			Athena_Source = _Athena_Source
			Athena_Url = _Athena_Url
			MailLog = _MailLog
			MailSuppress = _MailSuppress
			MailServer = _MailServer
			MailServerPort = _MailServerPort

			m_Data = New DataCommands(Host)
		End Sub

		''' <summary>Parametered Constructor (8 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution, _
			ByVal _Athena_Source As System.String, _
			ByVal _Athena_Url As System.String, _
			ByVal _MailLog As System.IO.DirectoryInfo, _
			ByVal _MailSuppress As System.Boolean, _
			ByVal _MailServer As System.String, _
			ByVal _MailServerPort As System.Int32, _
			ByVal _MailServerSSL As System.Boolean _
		)

			MyBase.New()

			Host = _Host
			Athena_Source = _Athena_Source
			Athena_Url = _Athena_Url
			MailLog = _MailLog
			MailSuppress = _MailSuppress
			MailServer = _MailServer
			MailServerPort = _MailServerPort
			MailServerSSL = _MailServerSSL

			m_Data = New DataCommands(Host)
		End Sub

		''' <summary>Parametered Constructor (9 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution, _
			ByVal _Athena_Source As System.String, _
			ByVal _Athena_Url As System.String, _
			ByVal _MailLog As System.IO.DirectoryInfo, _
			ByVal _MailSuppress As System.Boolean, _
			ByVal _MailServer As System.String, _
			ByVal _MailServerPort As System.Int32, _
			ByVal _MailServerSSL As System.Boolean, _
			ByVal _MailServerDomain As System.String _
		)

			MyBase.New()

			Host = _Host
			Athena_Source = _Athena_Source
			Athena_Url = _Athena_Url
			MailLog = _MailLog
			MailSuppress = _MailSuppress
			MailServer = _MailServer
			MailServerPort = _MailServerPort
			MailServerSSL = _MailServerSSL
			MailServerDomain = _MailServerDomain

			m_Data = New DataCommands(Host)
		End Sub

		''' <summary>Parametered Constructor (10 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution, _
			ByVal _Athena_Source As System.String, _
			ByVal _Athena_Url As System.String, _
			ByVal _MailLog As System.IO.DirectoryInfo, _
			ByVal _MailSuppress As System.Boolean, _
			ByVal _MailServer As System.String, _
			ByVal _MailServerPort As System.Int32, _
			ByVal _MailServerSSL As System.Boolean, _
			ByVal _MailServerDomain As System.String, _
			ByVal _MailServerUsername As System.String _
		)

			MyBase.New()

			Host = _Host
			Athena_Source = _Athena_Source
			Athena_Url = _Athena_Url
			MailLog = _MailLog
			MailSuppress = _MailSuppress
			MailServer = _MailServer
			MailServerPort = _MailServerPort
			MailServerSSL = _MailServerSSL
			MailServerDomain = _MailServerDomain
			MailServerUsername = _MailServerUsername

			m_Data = New DataCommands(Host)
		End Sub

		''' <summary>Parametered Constructor (11 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution, _
			ByVal _Athena_Source As System.String, _
			ByVal _Athena_Url As System.String, _
			ByVal _MailLog As System.IO.DirectoryInfo, _
			ByVal _MailSuppress As System.Boolean, _
			ByVal _MailServer As System.String, _
			ByVal _MailServerPort As System.Int32, _
			ByVal _MailServerSSL As System.Boolean, _
			ByVal _MailServerDomain As System.String, _
			ByVal _MailServerUsername As System.String, _
			ByVal _MailServerPassword As System.String _
		)

			MyBase.New()

			Host = _Host
			Athena_Source = _Athena_Source
			Athena_Url = _Athena_Url
			MailLog = _MailLog
			MailSuppress = _MailSuppress
			MailServer = _MailServer
			MailServerPort = _MailServerPort
			MailServerSSL = _MailServerSSL
			MailServerDomain = _MailServerDomain
			MailServerUsername = _MailServerUsername
			MailServerPassword = _MailServerPassword

			m_Data = New DataCommands(Host)
		End Sub

		''' <summary>Parametered Constructor (12 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution, _
			ByVal _Athena_Source As System.String, _
			ByVal _Athena_Url As System.String, _
			ByVal _MailLog As System.IO.DirectoryInfo, _
			ByVal _MailSuppress As System.Boolean, _
			ByVal _MailServer As System.String, _
			ByVal _MailServerPort As System.Int32, _
			ByVal _MailServerSSL As System.Boolean, _
			ByVal _MailServerDomain As System.String, _
			ByVal _MailServerUsername As System.String, _
			ByVal _MailServerPassword As System.String, _
			ByVal _MailFromAddress As System.String _
		)

			MyBase.New()

			Host = _Host
			Athena_Source = _Athena_Source
			Athena_Url = _Athena_Url
			MailLog = _MailLog
			MailSuppress = _MailSuppress
			MailServer = _MailServer
			MailServerPort = _MailServerPort
			MailServerSSL = _MailServerSSL
			MailServerDomain = _MailServerDomain
			MailServerUsername = _MailServerUsername
			MailServerPassword = _MailServerPassword
			MailFromAddress = _MailFromAddress

			m_Data = New DataCommands(Host)
		End Sub

		''' <summary>Parametered Constructor (13 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution, _
			ByVal _Athena_Source As System.String, _
			ByVal _Athena_Url As System.String, _
			ByVal _MailLog As System.IO.DirectoryInfo, _
			ByVal _MailSuppress As System.Boolean, _
			ByVal _MailServer As System.String, _
			ByVal _MailServerPort As System.Int32, _
			ByVal _MailServerSSL As System.Boolean, _
			ByVal _MailServerDomain As System.String, _
			ByVal _MailServerUsername As System.String, _
			ByVal _MailServerPassword As System.String, _
			ByVal _MailFromAddress As System.String, _
			ByVal _MailFromDisplay As System.String _
		)

			MyBase.New()

			Host = _Host
			Athena_Source = _Athena_Source
			Athena_Url = _Athena_Url
			MailLog = _MailLog
			MailSuppress = _MailSuppress
			MailServer = _MailServer
			MailServerPort = _MailServerPort
			MailServerSSL = _MailServerSSL
			MailServerDomain = _MailServerDomain
			MailServerUsername = _MailServerUsername
			MailServerPassword = _MailServerPassword
			MailFromAddress = _MailFromAddress
			MailFromDisplay = _MailFromDisplay

			m_Data = New DataCommands(Host)
		End Sub

		''' <summary>Parametered Constructor (14 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution, _
			ByVal _Athena_Source As System.String, _
			ByVal _Athena_Url As System.String, _
			ByVal _MailLog As System.IO.DirectoryInfo, _
			ByVal _MailSuppress As System.Boolean, _
			ByVal _MailServer As System.String, _
			ByVal _MailServerPort As System.Int32, _
			ByVal _MailServerSSL As System.Boolean, _
			ByVal _MailServerDomain As System.String, _
			ByVal _MailServerUsername As System.String, _
			ByVal _MailServerPassword As System.String, _
			ByVal _MailFromAddress As System.String, _
			ByVal _MailFromDisplay As System.String, _
			ByVal _MailReplyToAddress As System.String _
		)

			MyBase.New()

			Host = _Host
			Athena_Source = _Athena_Source
			Athena_Url = _Athena_Url
			MailLog = _MailLog
			MailSuppress = _MailSuppress
			MailServer = _MailServer
			MailServerPort = _MailServerPort
			MailServerSSL = _MailServerSSL
			MailServerDomain = _MailServerDomain
			MailServerUsername = _MailServerUsername
			MailServerPassword = _MailServerPassword
			MailFromAddress = _MailFromAddress
			MailFromDisplay = _MailFromDisplay
			MailReplyToAddress = _MailReplyToAddress

			m_Data = New DataCommands(Host)
		End Sub

		''' <summary>Parametered Constructor (15 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution, _
			ByVal _Athena_Source As System.String, _
			ByVal _Athena_Url As System.String, _
			ByVal _MailLog As System.IO.DirectoryInfo, _
			ByVal _MailSuppress As System.Boolean, _
			ByVal _MailServer As System.String, _
			ByVal _MailServerPort As System.Int32, _
			ByVal _MailServerSSL As System.Boolean, _
			ByVal _MailServerDomain As System.String, _
			ByVal _MailServerUsername As System.String, _
			ByVal _MailServerPassword As System.String, _
			ByVal _MailFromAddress As System.String, _
			ByVal _MailFromDisplay As System.String, _
			ByVal _MailReplyToAddress As System.String, _
			ByVal _MailReplyToDisplay As System.String _
		)

			MyBase.New()

			Host = _Host
			Athena_Source = _Athena_Source
			Athena_Url = _Athena_Url
			MailLog = _MailLog
			MailSuppress = _MailSuppress
			MailServer = _MailServer
			MailServerPort = _MailServerPort
			MailServerSSL = _MailServerSSL
			MailServerDomain = _MailServerDomain
			MailServerUsername = _MailServerUsername
			MailServerPassword = _MailServerPassword
			MailFromAddress = _MailFromAddress
			MailFromDisplay = _MailFromDisplay
			MailReplyToAddress = _MailReplyToAddress
			MailReplyToDisplay = _MailReplyToDisplay

			m_Data = New DataCommands(Host)
		End Sub

		''' <summary>Parametered Constructor (16 Parameters)</summary>
		Public Sub New( _
			ByVal _Host As Leviathan.Commands.ICommandsExecution, _
			ByVal _Athena_Source As System.String, _
			ByVal _Athena_Url As System.String, _
			ByVal _MailLog As System.IO.DirectoryInfo, _
			ByVal _MailSuppress As System.Boolean, _
			ByVal _MailServer As System.String, _
			ByVal _MailServerPort As System.Int32, _
			ByVal _MailServerSSL As System.Boolean, _
			ByVal _MailServerDomain As System.String, _
			ByVal _MailServerUsername As System.String, _
			ByVal _MailServerPassword As System.String, _
			ByVal _MailFromAddress As System.String, _
			ByVal _MailFromDisplay As System.String, _
			ByVal _MailReplyToAddress As System.String, _
			ByVal _MailReplyToDisplay As System.String, _
			ByVal _Data As Leviathan.Commands.DataCommands _
		)

			MyBase.New()

			Host = _Host
			Athena_Source = _Athena_Source
			Athena_Url = _Athena_Url
			MailLog = _MailLog
			MailSuppress = _MailSuppress
			MailServer = _MailServer
			MailServerPort = _MailServerPort
			MailServerSSL = _MailServerSSL
			MailServerDomain = _MailServerDomain
			MailServerUsername = _MailServerUsername
			MailServerPassword = _MailServerPassword
			MailFromAddress = _MailFromAddress
			MailFromDisplay = _MailFromDisplay
			MailReplyToAddress = _MailReplyToAddress
			MailReplyToDisplay = _MailReplyToDisplay
			Data = _Data

		End Sub

	#End Region

	#Region " Public Constants "

		''' <summary>Public Shared Reference to the Name of the Property: Host</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_HOST As String = "Host"

		''' <summary>Public Shared Reference to the Name of the Property: Athena_Source</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_ATHENA_SOURCE As String = "Athena_Source"

		''' <summary>Public Shared Reference to the Name of the Property: Athena_Url</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_ATHENA_URL As String = "Athena_Url"

		''' <summary>Public Shared Reference to the Name of the Property: MailLog</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_MAILLOG As String = "MailLog"

		''' <summary>Public Shared Reference to the Name of the Property: MailSuppress</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_MAILSUPPRESS As String = "MailSuppress"

		''' <summary>Public Shared Reference to the Name of the Property: MailServer</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_MAILSERVER As String = "MailServer"

		''' <summary>Public Shared Reference to the Name of the Property: MailServerPort</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_MAILSERVERPORT As String = "MailServerPort"

		''' <summary>Public Shared Reference to the Name of the Property: MailServerSSL</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_MAILSERVERSSL As String = "MailServerSSL"

		''' <summary>Public Shared Reference to the Name of the Property: MailServerDomain</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_MAILSERVERDOMAIN As String = "MailServerDomain"

		''' <summary>Public Shared Reference to the Name of the Property: MailServerUsername</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_MAILSERVERUSERNAME As String = "MailServerUsername"

		''' <summary>Public Shared Reference to the Name of the Property: MailServerPassword</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_MAILSERVERPASSWORD As String = "MailServerPassword"

		''' <summary>Public Shared Reference to the Name of the Property: MailFromAddress</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_MAILFROMADDRESS As String = "MailFromAddress"

		''' <summary>Public Shared Reference to the Name of the Property: MailFromDisplay</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_MAILFROMDISPLAY As String = "MailFromDisplay"

		''' <summary>Public Shared Reference to the Name of the Property: MailReplyToAddress</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_MAILREPLYTOADDRESS As String = "MailReplyToAddress"

		''' <summary>Public Shared Reference to the Name of the Property: MailReplyToDisplay</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_MAILREPLYTODISPLAY As String = "MailReplyToDisplay"

		''' <summary>Public Shared Reference to the Name of the Property: Data</summary>
		''' <remarks></remarks>
		Public Const PROPERTY_DATA As String = "Data"

	#End Region

	#Region " Private Variables "

		''' <summary>Private Data Storage Variable for Property: Host</summary>
		''' <remarks></remarks>
		Private m_Host As Leviathan.Commands.ICommandsExecution

		''' <summary>Private Data Storage Variable for Property: Athena_Source</summary>
		''' <remarks></remarks>
		Private m_Athena_Source As System.String

		''' <summary>Private Data Storage Variable for Property: Athena_Url</summary>
		''' <remarks></remarks>
		Private m_Athena_Url As System.String

		''' <summary>Private Data Storage Variable for Property: MailLog</summary>
		''' <remarks></remarks>
		Private m_MailLog As System.IO.DirectoryInfo

		''' <summary>Private Data Storage Variable for Property: MailSuppress</summary>
		''' <remarks></remarks>
		Private m_MailSuppress As System.Boolean

		''' <summary>Private Data Storage Variable for Property: MailServer</summary>
		''' <remarks></remarks>
		Private m_MailServer As System.String

		''' <summary>Private Data Storage Variable for Property: MailServerPort</summary>
		''' <remarks></remarks>
		Private m_MailServerPort As System.Int32

		''' <summary>Private Data Storage Variable for Property: MailServerSSL</summary>
		''' <remarks></remarks>
		Private m_MailServerSSL As System.Boolean

		''' <summary>Private Data Storage Variable for Property: MailServerDomain</summary>
		''' <remarks></remarks>
		Private m_MailServerDomain As System.String

		''' <summary>Private Data Storage Variable for Property: MailServerUsername</summary>
		''' <remarks></remarks>
		Private m_MailServerUsername As System.String

		''' <summary>Private Data Storage Variable for Property: MailServerPassword</summary>
		''' <remarks></remarks>
		Private m_MailServerPassword As System.String

		''' <summary>Private Data Storage Variable for Property: MailFromAddress</summary>
		''' <remarks></remarks>
		Private m_MailFromAddress As System.String

		''' <summary>Private Data Storage Variable for Property: MailFromDisplay</summary>
		''' <remarks></remarks>
		Private m_MailFromDisplay As System.String

		''' <summary>Private Data Storage Variable for Property: MailReplyToAddress</summary>
		''' <remarks></remarks>
		Private m_MailReplyToAddress As System.String

		''' <summary>Private Data Storage Variable for Property: MailReplyToDisplay</summary>
		''' <remarks></remarks>
		Private m_MailReplyToDisplay As System.String

		''' <summary>Private Data Storage Variable for Property: Data</summary>
		''' <remarks></remarks>
		Private m_Data As Leviathan.Commands.DataCommands

	#End Region

	#Region " Public Properties "

		''' <summary>Provides Access to the Property: Host</summary>
		''' <remarks></remarks>
		Public Property Host() As Leviathan.Commands.ICommandsExecution
			Get
				Return m_Host
			End Get
			Set(value As Leviathan.Commands.ICommandsExecution)
				m_Host = value
			End Set
		End Property

		''' <summary>Provides Access to the Property: Athena_Source</summary>
		''' <remarks></remarks>
		<Leviathan.Configuration.Configurable("athena-source", ResourceContainingType:=GetType(CreationCommands), ResourceName:="CommandDetails", Description:="Athena Data Source", ArgsDescription:="<data source", Prefix:="/")> _
		Public Property Athena_Source() As System.String
			Get
				Return m_Athena_Source
			End Get
			Set(value As System.String)
				m_Athena_Source = value
			End Set
		End Property

		''' <summary>Provides Access to the Property: Athena_Url</summary>
		''' <remarks></remarks>
		<Leviathan.Configuration.Configurable("athena-url", ResourceContainingType:=GetType(CreationCommands), ResourceName:="CommandDetails", Description:="Athena Url", Prefix:="/")> _
		Public Property Athena_Url() As System.String
			Get
				Return m_Athena_Url
			End Get
			Set(value As System.String)
				m_Athena_Url = value
			End Set
		End Property

		''' <summary>Provides Access to the Property: MailLog</summary>
		''' <remarks></remarks>
		<Leviathan.Configuration.Configurable("mail-log", ResourceContainingType:=GetType(CreationCommands), ResourceName:="CommandDetails", Description:="Mail Logging Directory", Prefix:="/")> _
		Public Property MailLog() As System.IO.DirectoryInfo
			Get
				Return m_MailLog
			End Get
			Set(value As System.IO.DirectoryInfo)
				m_MailLog = value
			End Set
		End Property

		''' <summary>Provides Access to the Property: MailSuppress</summary>
		''' <remarks></remarks>
		<Leviathan.Configuration.Configurable("mail-suppress", ResourceContainingType:=GetType(CreationCommands), ResourceName:="CommandDetails", Description:="Suppress Email Sends", Prefix:="/")> _
		Public Property MailSuppress() As System.Boolean
			Get
				Return m_MailSuppress
			End Get
			Set(value As System.Boolean)
				m_MailSuppress = value
			End Set
		End Property

		''' <summary>Provides Access to the Property: MailServer</summary>
		''' <remarks></remarks>
		<Leviathan.Configuration.Configurable("mail-server", ResourceContainingType:=GetType(CreationCommands), ResourceName:="CommandDetails", Description:="SMTP Server", Prefix:="/")> _
		Public Property MailServer() As System.String
			Get
				Return m_MailServer
			End Get
			Set(value As System.String)
				m_MailServer = value
			End Set
		End Property

		''' <summary>Provides Access to the Property: MailServerPort</summary>
		''' <remarks></remarks>
		<Leviathan.Configuration.Configurable("mail-server-port", ResourceContainingType:=GetType(CreationCommands), ResourceName:="CommandDetails", Description:="SMTP Port", Prefix:="/")> _
		Public Property MailServerPort() As System.Int32
			Get
				Return m_MailServerPort
			End Get
			Set(value As System.Int32)
				m_MailServerPort = value
			End Set
		End Property

		''' <summary>Provides Access to the Property: MailServerSSL</summary>
		''' <remarks></remarks>
		<Leviathan.Configuration.Configurable("mail-server-ssl", ResourceContainingType:=GetType(CreationCommands), ResourceName:="CommandDetails", Description:="SMTP SSL", Prefix:="/")> _
		Public Property MailServerSSL() As System.Boolean
			Get
				Return m_MailServerSSL
			End Get
			Set(value As System.Boolean)
				m_MailServerSSL = value
			End Set
		End Property

		''' <summary>Provides Access to the Property: MailServerDomain</summary>
		''' <remarks></remarks>
		<Leviathan.Configuration.Configurable("mail-server-auth-domain", ResourceContainingType:=GetType(CreationCommands), ResourceName:="CommandDetails", Description:="Authentication Domain", Prefix:="/")> _
		Public Property MailServerDomain() As System.String
			Get
				Return m_MailServerDomain
			End Get
			Set(value As System.String)
				m_MailServerDomain = value
			End Set
		End Property

		''' <summary>Provides Access to the Property: MailServerUsername</summary>
		''' <remarks></remarks>
		<Leviathan.Configuration.Configurable("mail-server-auth-username", ResourceContainingType:=GetType(CreationCommands), ResourceName:="CommandDetails", Description:="Authentication Username", Prefix:="/")> _
		Public Property MailServerUsername() As System.String
			Get
				Return m_MailServerUsername
			End Get
			Set(value As System.String)
				m_MailServerUsername = value
			End Set
		End Property

		''' <summary>Provides Access to the Property: MailServerPassword</summary>
		''' <remarks></remarks>
		<Leviathan.Configuration.Configurable("mail-server-auth-password", ResourceContainingType:=GetType(CreationCommands), ResourceName:="CommandDetails", Description:="Authentication Password", Prefix:="/")> _
		Public Property MailServerPassword() As System.String
			Get
				Return m_MailServerPassword
			End Get
			Set(value As System.String)
				m_MailServerPassword = value
			End Set
		End Property

		''' <summary>Provides Access to the Property: MailFromAddress</summary>
		''' <remarks></remarks>
		<Leviathan.Configuration.Configurable("mail-from", ResourceContainingType:=GetType(CreationCommands), ResourceName:="CommandDetails", Description:="From", Prefix:="/")> _
		Public Property MailFromAddress() As System.String
			Get
				Return m_MailFromAddress
			End Get
			Set(value As System.String)
				m_MailFromAddress = value
			End Set
		End Property

		''' <summary>Provides Access to the Property: MailFromDisplay</summary>
		''' <remarks></remarks>
		<Leviathan.Configuration.Configurable("mail-from-display", ResourceContainingType:=GetType(CreationCommands), ResourceName:="CommandDetails", Description:="From Display", Prefix:="/")> _
		Public Property MailFromDisplay() As System.String
			Get
				Return m_MailFromDisplay
			End Get
			Set(value As System.String)
				m_MailFromDisplay = value
			End Set
		End Property

		''' <summary>Provides Access to the Property: MailReplyToAddress</summary>
		''' <remarks></remarks>
		<Leviathan.Configuration.Configurable("mail-replyto", ResourceContainingType:=GetType(CreationCommands), ResourceName:="CommandDetails", Description:="Reply To", Prefix:="/")> _
		Public Property MailReplyToAddress() As System.String
			Get
				Return m_MailReplyToAddress
			End Get
			Set(value As System.String)
				m_MailReplyToAddress = value
			End Set
		End Property

		''' <summary>Provides Access to the Property: MailReplyToDisplay</summary>
		''' <remarks></remarks>
		<Leviathan.Configuration.Configurable("mail-replyto-display", ResourceContainingType:=GetType(CreationCommands), ResourceName:="CommandDetails", Description:="Reply To Display", Prefix:="/")> _
		Public Property MailReplyToDisplay() As System.String
			Get
				Return m_MailReplyToDisplay
			End Get
			Set(value As System.String)
				m_MailReplyToDisplay = value
			End Set
		End Property

		''' <summary>Provides Access to the Property: Data</summary>
		''' <remarks></remarks>
		Public Property Data() As Leviathan.Commands.DataCommands
			Get
				Return m_Data
			End Get
			Set(value As Leviathan.Commands.DataCommands)
				m_Data = value
			End Set
		End Property

	#End Region

End Class