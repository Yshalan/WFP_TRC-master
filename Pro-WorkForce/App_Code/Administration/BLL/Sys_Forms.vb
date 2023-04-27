Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.Forms

    Public Class Sys_Forms

#Region "Class Variables"


        Private _FormID As Integer
        Private _FormName As String
        Private _Desc_En As String
        Private _Desc_Ar As String
        Private _ModuleID As Integer
        Private _FormPath As String
        Private _FormOrder As Integer
        Private _ParentID As Integer
        Private _Visible As Integer
        Private _FormOnlinePath As String
        Private _FormOnlineOrder As Integer
        Private _ShowToEmp As Boolean
        Private _ShowToClient As Boolean
        Private _ShowToPersonal As Boolean
        Private _OnlineFormPath As String
        Private _DescOnline_En As String
        Private _DescOnline_Ar As String
        Private _Packages As String
        Private _AddBtnName As String
        Private _EditBtnName As String
        Private _DeleteBtnName As String
        Private _PrintBtnName As String
        Private objDALSys_Forms As DALSys_Forms

#End Region

#Region "Public Properties"


        Public Property FormID() As Integer
            Set(ByVal value As Integer)
                _FormID = value
            End Set
            Get
                Return (_FormID)
            End Get
        End Property


        Public Property FormName() As String
            Set(ByVal value As String)
                _FormName = value
            End Set
            Get
                Return (_FormName)
            End Get
        End Property


        Public Property Desc_En() As String
            Set(ByVal value As String)
                _Desc_En = value
            End Set
            Get
                Return (_Desc_En)
            End Get
        End Property


        Public Property Desc_Ar() As String
            Set(ByVal value As String)
                _Desc_Ar = value
            End Set
            Get
                Return (_Desc_Ar)
            End Get
        End Property


        Public Property ModuleID() As Integer
            Set(ByVal value As Integer)
                _ModuleID = value
            End Set
            Get
                Return (_ModuleID)
            End Get
        End Property


        Public Property FormPath() As String
            Set(ByVal value As String)
                _FormPath = value
            End Set
            Get
                Return (_FormPath)
            End Get
        End Property


        Public Property FormOrder() As Integer
            Set(ByVal value As Integer)
                _FormOrder = value
            End Set
            Get
                Return (_FormOrder)
            End Get
        End Property


        Public Property ParentID() As Integer
            Set(ByVal value As Integer)
                _ParentID = value
            End Set
            Get
                Return (_ParentID)
            End Get
        End Property


        Public Property Visible() As Integer
            Set(ByVal value As Integer)
                _Visible = value
            End Set
            Get
                Return (_Visible)
            End Get
        End Property


        Public Property FormOnlinePath() As String
            Set(ByVal value As String)
                _FormOnlinePath = value
            End Set
            Get
                Return (_FormOnlinePath)
            End Get
        End Property


        Public Property FormOnlineOrder() As Integer
            Set(ByVal value As Integer)
                _FormOnlineOrder = value
            End Set
            Get
                Return (_FormOnlineOrder)
            End Get
        End Property


        Public Property ShowToEmp() As Boolean
            Set(ByVal value As Boolean)
                _ShowToEmp = value
            End Set
            Get
                Return (_ShowToEmp)
            End Get
        End Property


        Public Property ShowToClient() As Boolean
            Set(ByVal value As Boolean)
                _ShowToClient = value
            End Set
            Get
                Return (_ShowToClient)
            End Get
        End Property


        Public Property ShowToPersonal() As Boolean
            Set(ByVal value As Boolean)
                _ShowToPersonal = value
            End Set
            Get
                Return (_ShowToPersonal)
            End Get
        End Property


        Public Property OnlineFormPath() As String
            Set(ByVal value As String)
                _OnlineFormPath = value
            End Set
            Get
                Return (_OnlineFormPath)
            End Get
        End Property


        Public Property DescOnline_En() As String
            Set(ByVal value As String)
                _DescOnline_En = value
            End Set
            Get
                Return (_DescOnline_En)
            End Get
        End Property


        Public Property DescOnline_Ar() As String
            Set(ByVal value As String)
                _DescOnline_Ar = value
            End Set
            Get
                Return (_DescOnline_Ar)
            End Get
        End Property


        Public Property Packages() As String
            Set(ByVal value As String)
                _Packages = value
            End Set
            Get
                Return (_Packages)
            End Get
        End Property


        Public Property AddBtnName() As String
            Set(ByVal value As String)
                _AddBtnName = value
            End Set
            Get
                Return (_AddBtnName)
            End Get
        End Property


        Public Property EditBtnName() As String
            Set(ByVal value As String)
                _EditBtnName = value
            End Set
            Get
                Return (_EditBtnName)
            End Get
        End Property


        Public Property DeleteBtnName() As String
            Set(ByVal value As String)
                _DeleteBtnName = value
            End Set
            Get
                Return (_DeleteBtnName)
            End Get
        End Property


        Public Property PrintBtnName() As String
            Set(ByVal value As String)
                _PrintBtnName = value
            End Set
            Get
                Return (_PrintBtnName)
            End Get
        End Property

#End Region


#Region "Constructor"

        Public Sub New()

            objDALSys_Forms = New DALSys_Forms()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALSys_Forms.Add(_FormID, _FormName, _Desc_En, _Desc_Ar, _ModuleID, _FormPath, _FormOrder, _ParentID, _Visible, _FormOnlinePath, _FormOnlineOrder, _ShowToEmp, _ShowToClient, _ShowToPersonal, _OnlineFormPath, _DescOnline_En, _DescOnline_Ar, _Packages, _AddBtnName, _EditBtnName, _DeleteBtnName, _PrintBtnName)
        End Function

        Public Function Update() As Integer

            Return objDALSys_Forms.Update(_FormID, _FormName, _Desc_En, _Desc_Ar, _ModuleID, _FormPath, _FormOrder, _ParentID, _Visible, _FormOnlinePath, _FormOnlineOrder, _ShowToEmp, _ShowToClient, _ShowToPersonal, _OnlineFormPath, _DescOnline_En, _DescOnline_Ar, _Packages, _AddBtnName, _EditBtnName, _DeleteBtnName, _PrintBtnName)

        End Function



        Public Function Delete() As Integer

            Return objDALSys_Forms.Delete(_FormID)

        End Function

        Public Function GetAll() As DataTable

            Return objDALSys_Forms.GetAll()

        End Function

        Public Function GetAll_ForEventLog() As DataTable

            Return objDALSys_Forms.GetAll_ForEventLog()

        End Function
        Public Function GetAll_CDCTables() As DataTable

            Return objDALSys_Forms.GetAll_CDCTables()

        End Function

        Public Function GetByPK() As Sys_Forms

            Dim dr As DataRow
            dr = objDALSys_Forms.GetByPK(_FormID)

            If Not IsDBNull(dr("FormID")) Then
                _FormID = dr("FormID")
            End If
            If Not IsDBNull(dr("FormName")) Then
                _FormName = dr("FormName")
            End If
            If Not IsDBNull(dr("Desc_En")) Then
                _Desc_En = dr("Desc_En")
            End If
            If Not IsDBNull(dr("Desc_Ar")) Then
                _Desc_Ar = dr("Desc_Ar")
            End If
            If Not IsDBNull(dr("ModuleID")) Then
                _ModuleID = dr("ModuleID")
            End If
            If Not IsDBNull(dr("FormPath")) Then
                _FormPath = dr("FormPath")
            End If
            If Not IsDBNull(dr("FormOrder")) Then
                _FormOrder = dr("FormOrder")
            End If
            If Not IsDBNull(dr("ParentID")) Then
                _ParentID = dr("ParentID")
            End If
            If Not IsDBNull(dr("Visible")) Then
                _Visible = dr("Visible")
            End If
            If Not IsDBNull(dr("FormOnlinePath")) Then
                _FormOnlinePath = dr("FormOnlinePath")
            End If
            If Not IsDBNull(dr("FormOnlineOrder")) Then
                _FormOnlineOrder = dr("FormOnlineOrder")
            End If
            If Not IsDBNull(dr("ShowToEmp")) Then
                _ShowToEmp = dr("ShowToEmp")
            End If
            If Not IsDBNull(dr("ShowToClient")) Then
                _ShowToClient = dr("ShowToClient")
            End If
            If Not IsDBNull(dr("ShowToPersonal")) Then
                _ShowToPersonal = dr("ShowToPersonal")
            End If
            If Not IsDBNull(dr("OnlineFormPath")) Then
                _OnlineFormPath = dr("OnlineFormPath")
            End If
            If Not IsDBNull(dr("DescOnline_En")) Then
                _DescOnline_En = dr("DescOnline_En")
            End If
            If Not IsDBNull(dr("DescOnline_Ar")) Then
                _DescOnline_Ar = dr("DescOnline_Ar")
            End If
            If Not IsDBNull(dr("Packages")) Then
                _Packages = dr("Packages")
            End If
            If Not IsDBNull(dr("AddBtnName")) Then
                _AddBtnName = dr("AddBtnName")
            End If
            If Not IsDBNull(dr("EditBtnName")) Then
                _EditBtnName = dr("EditBtnName")
            End If
            If Not IsDBNull(dr("DeleteBtnName")) Then
                _DeleteBtnName = dr("DeleteBtnName")
            End If
            If Not IsDBNull(dr("PrintBtnName")) Then
                _PrintBtnName = dr("PrintBtnName")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace