Imports Microsoft.VisualBasic
Imports SmartV.UTILITIES
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient

Namespace SmartV.Security.MENU
    Public Class SECForm
        Inherits AISMenu

#Region "Variables & Properties"
        Private pFormID As Integer
        Private pParentID As Integer
        Private pArabicName As String
        Private pEnglishName As String
        Private pFormURL As String
        Private pSequance As Integer
        Private pImageURL As String
        Private pShow As Boolean
        Private pDescription As String
        Private pModuleID As Integer
        Private pParentURL As String

        Public Property formID() As Integer
            Get
                Return pFormID
            End Get
            Set(ByVal value As Integer)
                pFormID = value
            End Set
        End Property

        Public Property parentID() As Integer
            Get
                Return pParentID
            End Get
            Set(ByVal value As Integer)
                pParentID = value
            End Set
        End Property

        Public Property arabicName() As String
            Get
                Return pArabicName
            End Get
            Set(ByVal value As String)
                pArabicName = value
            End Set
        End Property


        Public Property englishName() As String
            Get
                Return pEnglishName
            End Get
            Set(ByVal value As String)
                pEnglishName = value
            End Set
        End Property

        Public Property FormURL() As String
            Get
                Return pFormURL
            End Get
            Set(ByVal value As String)
                pFormURL = value
            End Set
        End Property


        Public Property sequance() As Integer
            Get
                Return pSequance
            End Get
            Set(ByVal value As Integer)
                pSequance = value
            End Set
        End Property


        Public Property imageURL() As String
            Get
                Return pImageURL
            End Get
            Set(ByVal value As String)
                pImageURL = value
            End Set
        End Property



        Public Property show() As Boolean
            Get
                Return pShow
            End Get
            Set(ByVal value As Boolean)
                pShow = value
            End Set
        End Property

        Public Property description() As String
            Get
                Return pDescription
            End Get
            Set(ByVal value As String)
                pDescription = value
            End Set
        End Property


        Public Property moduleID() As Integer
            Get
                Return pModuleID
            End Get
            Set(ByVal value As Integer)
                pModuleID = value
            End Set
        End Property

        Public Property parentURL() As String
            Get
                Return pParentURL
            End Get
            Set(ByVal value As String)
                pParentURL = value
            End Set
        End Property


#End Region

        Public Function getForm() As Boolean
            Dim dac As DAC = dac.getDAC
            Dim sqlparam1 As New SqlParameter("@PageName", SqlDbType.NVarChar, 4000, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, FormURL)
            Dim dt As DataTable = dac.GetDataTable("PAY_GETForm", sqlparam1)
            If dt.Rows.Count = 0 Then
                Return False
            End If
            Me.formID = dt.Rows(0).Item(0)
            Me.parentID = dt.Rows(0).Item(1)
            Me.arabicName = dt.Rows(0).Item(2)
            Me.englishName = dt.Rows(0).Item(3)
            Dim parentURL As String = dt.Rows(0).Item(4)
            Return True
        End Function

        Private Sub helperFunction(ByVal strArr As Stack(Of String), ByVal currForm As SECForm)

            If parentID = 0 Then
                Return
            End If
            Dim parentForm As New SECForm
            parentForm.FormURL = currForm.parentURL
            parentForm.getForm()
            helperFunction(strArr, parentForm)
        End Sub

        Public Function getWhereAreYouGo() As String
            Dim dac As DAC = dac.getDAC
            Dim sqlparam1 As New SqlParameter("@FormID", SqlDbType.Int, 4, ParameterDirection.Input, False, 0, 0, "", DataRowVersion.Default, formID)
            Dim dt As DataTable = dac.GetDataTable("PAY_GETForm", sqlparam1)
            If Not dt.Rows.Count = 0 Then
                Return "You Will Move To Page - " & dt.Rows(0).Item("EnglishName")
            End If
            Return ""
        End Function

    

    End Class
End Namespace

