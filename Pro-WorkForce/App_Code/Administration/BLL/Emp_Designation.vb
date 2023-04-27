Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin

    Public Class Emp_Designation

#Region "Class Variables"


        Private _DesignationId As Integer
        Private _DesignationCode As String
        Private _DesignationName As String
        Private _DesignationArabicName As String
        Private _AnnualLeaveBalance As Double
        Private _FK_OvertimeRuleId As Integer
        Private _IsTAException As Boolean
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _FK_ManagerId As Integer
        Private objDALEmp_Designation As DALEmp_Designation

#End Region

#Region "Public Properties"

       

        Public Property DesignationId() As Integer
            Set(ByVal value As Integer)
                _DesignationId = value
            End Set
            Get
                Return (_DesignationId)
            End Get
        End Property


        Public Property DesignationCode() As String
            Set(ByVal value As String)
                _DesignationCode = value
            End Set
            Get
                Return (_DesignationCode)
            End Get
        End Property


        Public Property DesignationName() As String
            Set(ByVal value As String)
                _DesignationName = value
            End Set
            Get
                Return (_DesignationName)
            End Get
        End Property


        Public Property DesignationArabicName() As String
            Set(ByVal value As String)
                _DesignationArabicName = value
            End Set
            Get
                Return (_DesignationArabicName)
            End Get
        End Property


        Public Property AnnualLeaveBalance() As Double
            Set(ByVal value As Double)
                _AnnualLeaveBalance = value
            End Set
            Get
                Return (_AnnualLeaveBalance)
            End Get
        End Property


        Public Property FK_OvertimeRuleId() As Integer
            Set(ByVal value As Integer)
                _FK_OvertimeRuleId = value
            End Set
            Get
                Return (_FK_OvertimeRuleId)
            End Get
        End Property


        Public Property IsTAException() As Boolean
            Set(ByVal value As Boolean)
                _IsTAException = value
            End Set
            Get
                Return (_IsTAException)
            End Get
        End Property


        Public Property CREATED_BY() As String
            Set(ByVal value As String)
                _CREATED_BY = value
            End Set
            Get
                Return (_CREATED_BY)
            End Get
        End Property


        Public Property CREATED_DATE() As DateTime
            Set(ByVal value As DateTime)
                _CREATED_DATE = value
            End Set
            Get
                Return (_CREATED_DATE)
            End Get
        End Property


        Public Property LAST_UPDATE_BY() As String
            Set(ByVal value As String)
                _LAST_UPDATE_BY = value
            End Set
            Get
                Return (_LAST_UPDATE_BY)
            End Get
        End Property


        Public Property LAST_UPDATE_DATE() As DateTime
            Set(ByVal value As DateTime)
                _LAST_UPDATE_DATE = value
            End Set
            Get
                Return (_LAST_UPDATE_DATE)
            End Get
        End Property

        Public Property FK_ManagerId() As Integer
            Set(ByVal value As Integer)
                _FK_ManagerId = value
            End Set
            Get
                Return (_FK_ManagerId)
            End Get
        End Property
#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_Designation = New DALEmp_Designation()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer
            Dim rslt As Integer = objDALEmp_Designation.Add(_DesignationId, _DesignationCode, _DesignationName, _DesignationArabicName, _AnnualLeaveBalance, _FK_OvertimeRuleId, _IsTAException, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Add", _DesignationId, "Emp_Designation", "Define Designation")
            Return rslt
        End Function

        Public Function Update() As Integer
            Dim rslt As Integer = objDALEmp_Designation.Update(_DesignationId, _DesignationCode, _DesignationName, _DesignationArabicName, _AnnualLeaveBalance, _FK_OvertimeRuleId, _IsTAException, _CREATED_BY, _CREATED_DATE, _LAST_UPDATE_BY, _LAST_UPDATE_DATE)
            App_EventsLog.Insert_ToEventLog("Edit", _DesignationId, "Emp_Designation", "Define Designation")
            Return rslt

        End Function

        Public Function Delete() As Integer
            Dim rslt As Integer = objDALEmp_Designation.Delete(_DesignationId)
            App_EventsLog.Insert_ToEventLog("Delete", _DesignationId, "Emp_Designation", "Define Designation")
            Return rslt

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_Designation.GetAll()

        End Function

        Public Function GetAllDesignationByManger() As DataTable

            Return objDALEmp_Designation.GetAllDesignationByManger(_FK_ManagerId)

        End Function

        Public Function GetByPK() As Emp_Designation

            Dim dr As DataRow
            dr = objDALEmp_Designation.GetByPK(_DesignationId)

            If dr Is Nothing Then Return Nothing

            If Not IsDBNull(dr("DesignationId")) Then
                _DesignationId = dr("DesignationId")
            End If
            If Not IsDBNull(dr("DesignationCode")) Then
                _DesignationCode = dr("DesignationCode")
            End If
            If Not IsDBNull(dr("DesignationName")) Then
                _DesignationName = dr("DesignationName")
            End If
            If Not IsDBNull(dr("DesignationArabicName")) Then
                _DesignationArabicName = dr("DesignationArabicName")
            End If
            If Not IsDBNull(dr("AnnualLeaveBalance")) Then
                _AnnualLeaveBalance = dr("AnnualLeaveBalance")
            End If
            If Not IsDBNull(dr("FK_OvertimeRuleId")) Then
                _FK_OvertimeRuleId = dr("FK_OvertimeRuleId")
            End If
            If Not IsDBNull(dr("IsTAException")) Then
                _IsTAException = dr("IsTAException")
            End If
            If Not IsDBNull(dr("CREATED_BY")) Then
                _CREATED_BY = dr("CREATED_BY")
            End If
            If Not IsDBNull(dr("CREATED_DATE")) Then
                _CREATED_DATE = dr("CREATED_DATE")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_BY")) Then
                _LAST_UPDATE_BY = dr("LAST_UPDATE_BY")
            End If
            If Not IsDBNull(dr("LAST_UPDATE_DATE")) Then
                _LAST_UPDATE_DATE = dr("LAST_UPDATE_DATE")
            End If
            Return Me
        End Function

#End Region

    End Class
End Namespace