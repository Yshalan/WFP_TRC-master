Imports SmartV.UTILITIES
Imports System.Data
Imports System.Reflection
Imports System.Data.SqlClient
Imports SmartV.DB

Namespace TA.Security
    <Serializable()> _
    Public Class SYSModules
#Region "DECLARATION"

        Private _moduleID As Integer
        Private _englishName As String
        Private _arabicName As String
        Dim lang As Integer
        Dim _seq As Integer
        Private _Packages As String
        Private _div As String
        Private _icon As String

#End Region

#Region "PROPERTIES"

        Public Property ModuleID() As Integer
            Get
                Return _moduleID
            End Get
            Set(ByVal value As Integer)
                _moduleID = value
            End Set
        End Property

        Public Property EnglishName() As String
            Get
                Return _englishName
            End Get
            Set(ByVal value As String)
                _englishName = value
            End Set
        End Property

        Public Property ArabicName() As String
            Get
                Return _arabicName
            End Get
            Set(ByVal value As String)
                _arabicName = value
            End Set
        End Property

        Public Property Seq() As Integer
            Get
                Return _seq
            End Get
            Set(ByVal value As Integer)
                _seq = value
            End Set
        End Property

        Public Property Packages() As String
            Get
                Return _Packages
            End Get
            Set(ByVal value As String)
                _Packages = value
            End Set
        End Property

        Public Property div() As String
            Get
                Return _div
            End Get
            Set(ByVal value As String)
                _div = value
            End Set
        End Property

        Public Property icon() As String
            Get
                Return _icon
            End Get
            Set(ByVal value As String)
                _icon = value
            End Set
        End Property

#End Region

#Region "CONSTRUCTORS"

        Public Sub New()
            _moduleID = 0
        End Sub

        Public Sub New(ByVal i_moduleID As Integer)
            _moduleID = i_moduleID
        End Sub

#End Region

#Region "METHODS"

        Public Function load() As DataTable
            Dim dac As DAC = dac.getDAC
            Dim dt As DataTable

            If (SessionVariables.CultureInfo = "en-US") Then
                lang = 0
            Else
                lang = 1
            End If
            Dim sqlparam As New SqlParameter("@Lang", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, lang)
            dt = dac.GetDataTable("Sys_GetModules", sqlparam)

            Return dt
        End Function

        Public Function GetByPK() As SYSModules
            Dim dac As DAC = dac.getDAC
            Dim dr As DataRow
            dr = dac.GetDataTable("Sys_Modules_GetByPK", New SqlParameter("@ModuleID", _moduleID))(0)

            If Not dr Is Nothing Then

                If Not IsDBNull(dr("ModuleID")) Then
                    _moduleID = dr("ModuleID")
                End If

                If Not IsDBNull(dr("Desc_Ar")) Then
                    _arabicName = dr("Desc_Ar")
                End If

                If Not IsDBNull(dr("Desc_En")) Then
                    _englishName = dr("Desc_En")
                End If

                If Not IsDBNull(dr("Seq")) Then
                    _seq = dr("Seq")
                End If

                If Not IsDBNull(dr("Packages")) Then
                    _Packages = dr("Packages")
                End If

                If Not IsDBNull(dr("div")) Then
                    _div = dr("div")
                End If

                If Not IsDBNull(dr("icon")) Then
                    _icon = dr("icon")
                End If

                Return Me
            Else
                Return Nothing
            End If

        End Function

        Public Function GetAll() As DataTable
            Dim dac As DAC = dac.getDAC
            Dim dt As DataTable

            If (SessionVariables.CultureInfo = "en-US") Then
                lang = 0
            Else
                lang = 1
            End If
            Dim sqlparam As New SqlParameter("@Lang", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, lang)
            dt = dac.GetDataTable("Sys_Modules_Select_All", sqlparam)

            Return dt
        End Function
        Public Function GetRequestsCount(ByVal FK_EmployeeId As Integer) As DataTable
            Dim dac As DAC = dac.getDAC
            Dim dt As DataTable

            Dim sqlparam As New SqlParameter("@FK_EmployeeId", FK_EmployeeId)
            dt = dac.GetDataTable("GetRequestsCount", sqlparam)

            Return dt
        End Function

#End Region

    End Class

End Namespace