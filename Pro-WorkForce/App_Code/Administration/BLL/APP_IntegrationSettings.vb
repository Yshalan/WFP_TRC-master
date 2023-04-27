Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Events

Namespace TA.Admin

    Public Class APP_IntegrationSettings

#Region "Class Variables"


        Private _CompanyName As String
        Private _HasEmployee As Boolean
        Private _HasEmployeeLeave As Boolean
        Private _HasLeaveAudit As Boolean
        Private _HasEmployeeSupervisor As Boolean
        Private _HasGrade As Boolean
        Private _HasDesignation As Boolean
        Private _HasNationality As Boolean
        Private _HasWorkLocation As Boolean
        Private _HasOrganization As Boolean
        Private _HasLeaveTypes As Boolean
        Private _HasEmployeeDelegate As Boolean
        Private _HasHoliday As Boolean
        Private _HasStudyLeave As Boolean
        Private _HasApproveErpViolation As Boolean
        Private _ReprocessFirstSchedule As Integer
        Private _ReprocessSecondSchedule As Integer
        Private _ReprocessThirdSchedule As Integer
        Private _EmailErrorReceiver As String
        Private _EmailPortNumber As Integer
        Private _EmailEnableSsl As Boolean
        Private _ServiceURL As String
        Private _ServiceUserName As String
        Private _ServicePassword As String
        Private _EntityName As String
        Private _IntegrationType As String
        Private _IsPendingLeave As Boolean
        Private _IsApproveLeave As Boolean
        Private _IsTrainingLeave As Boolean
        Private _IsExtraInfoLeave As Boolean
        Private _IsDutyLeave As Boolean
        Private _RunTimesByMinutes As Integer
        Private objDALAPP_IntegrationSettings As DALAPP_IntegrationSettings

#End Region

#Region "Public Properties"


        Public Property CompanyName() As String
            Set(ByVal value As String)
                _CompanyName = value
            End Set
            Get
                Return (_CompanyName)
            End Get
        End Property


        Public Property HasEmployee() As Boolean
            Set(ByVal value As Boolean)
                _HasEmployee = value
            End Set
            Get
                Return (_HasEmployee)
            End Get
        End Property


        Public Property HasEmployeeLeave() As Boolean
            Set(ByVal value As Boolean)
                _HasEmployeeLeave = value
            End Set
            Get
                Return (_HasEmployeeLeave)
            End Get
        End Property


        Public Property HasLeaveAudit() As Boolean
            Set(ByVal value As Boolean)
                _HasLeaveAudit = value
            End Set
            Get
                Return (_HasLeaveAudit)
            End Get
        End Property


        Public Property HasEmployeeSupervisor() As Boolean
            Set(ByVal value As Boolean)
                _HasEmployeeSupervisor = value
            End Set
            Get
                Return (_HasEmployeeSupervisor)
            End Get
        End Property


        Public Property HasGrade() As Boolean
            Set(ByVal value As Boolean)
                _HasGrade = value
            End Set
            Get
                Return (_HasGrade)
            End Get
        End Property


        Public Property HasDesignation() As Boolean
            Set(ByVal value As Boolean)
                _HasDesignation = value
            End Set
            Get
                Return (_HasDesignation)
            End Get
        End Property


        Public Property HasNationality() As Boolean
            Set(ByVal value As Boolean)
                _HasNationality = value
            End Set
            Get
                Return (_HasNationality)
            End Get
        End Property


        Public Property HasWorkLocation() As Boolean
            Set(ByVal value As Boolean)
                _HasWorkLocation = value
            End Set
            Get
                Return (_HasWorkLocation)
            End Get
        End Property


        Public Property HasOrganization() As Boolean
            Set(ByVal value As Boolean)
                _HasOrganization = value
            End Set
            Get
                Return (_HasOrganization)
            End Get
        End Property


        Public Property HasLeaveTypes() As Boolean
            Set(ByVal value As Boolean)
                _HasLeaveTypes = value
            End Set
            Get
                Return (_HasLeaveTypes)
            End Get
        End Property


        Public Property HasEmployeeDelegate() As Boolean
            Set(ByVal value As Boolean)
                _HasEmployeeDelegate = value
            End Set
            Get
                Return (_HasEmployeeDelegate)
            End Get
        End Property


        Public Property HasHoliday() As Boolean
            Set(ByVal value As Boolean)
                _HasHoliday = value
            End Set
            Get
                Return (_HasHoliday)
            End Get
        End Property


        Public Property HasStudyLeave() As Boolean
            Set(ByVal value As Boolean)
                _HasStudyLeave = value
            End Set
            Get
                Return (_HasStudyLeave)
            End Get
        End Property


        Public Property HasApproveErpViolation() As Boolean
            Set(ByVal value As Boolean)
                _HasApproveErpViolation = value
            End Set
            Get
                Return (_HasApproveErpViolation)
            End Get
        End Property


        Public Property ReprocessFirstSchedule() As Integer
            Set(ByVal value As Integer)
                _ReprocessFirstSchedule = value
            End Set
            Get
                Return (_ReprocessFirstSchedule)
            End Get
        End Property


        Public Property ReprocessSecondSchedule() As Integer
            Set(ByVal value As Integer)
                _ReprocessSecondSchedule = value
            End Set
            Get
                Return (_ReprocessSecondSchedule)
            End Get
        End Property


        Public Property ReprocessThirdSchedule() As Integer
            Set(ByVal value As Integer)
                _ReprocessThirdSchedule = value
            End Set
            Get
                Return (_ReprocessThirdSchedule)
            End Get
        End Property


        Public Property EmailErrorReceiver() As String
            Set(ByVal value As String)
                _EmailErrorReceiver = value
            End Set
            Get
                Return (_EmailErrorReceiver)
            End Get
        End Property


        Public Property EmailPortNumber() As Integer
            Set(ByVal value As Integer)
                _EmailPortNumber = value
            End Set
            Get
                Return (_EmailPortNumber)
            End Get
        End Property


        Public Property EmailEnableSsl() As Boolean
            Set(ByVal value As Boolean)
                _EmailEnableSsl = value
            End Set
            Get
                Return (_EmailEnableSsl)
            End Get
        End Property


        Public Property ServiceURL() As String
            Set(ByVal value As String)
                _ServiceURL = value
            End Set
            Get
                Return (_ServiceURL)
            End Get
        End Property


        Public Property ServiceUserName() As String
            Set(ByVal value As String)
                _ServiceUserName = value
            End Set
            Get
                Return (_ServiceUserName)
            End Get
        End Property


        Public Property ServicePassword() As String
            Set(ByVal value As String)
                _ServicePassword = value
            End Set
            Get
                Return (_ServicePassword)
            End Get
        End Property


        Public Property EntityName() As String
            Set(ByVal value As String)
                _EntityName = value
            End Set
            Get
                Return (_EntityName)
            End Get
        End Property


        Public Property IntegrationType() As String
            Set(ByVal value As String)
                _IntegrationType = value
            End Set
            Get
                Return (_IntegrationType)
            End Get
        End Property


        Public Property IsPendingLeave() As Boolean
            Set(ByVal value As Boolean)
                _IsPendingLeave = value
            End Set
            Get
                Return (_IsPendingLeave)
            End Get
        End Property


        Public Property IsApproveLeave() As Boolean
            Set(ByVal value As Boolean)
                _IsApproveLeave = value
            End Set
            Get
                Return (_IsApproveLeave)
            End Get
        End Property


        Public Property IsTrainingLeave() As Boolean
            Set(ByVal value As Boolean)
                _IsTrainingLeave = value
            End Set
            Get
                Return (_IsTrainingLeave)
            End Get
        End Property


        Public Property IsExtraInfoLeave() As Boolean
            Set(ByVal value As Boolean)
                _IsExtraInfoLeave = value
            End Set
            Get
                Return (_IsExtraInfoLeave)
            End Get
        End Property


        Public Property IsDutyLeave() As Boolean
            Set(ByVal value As Boolean)
                _IsDutyLeave = value
            End Set
            Get
                Return (_IsDutyLeave)
            End Get
        End Property


        Public Property RunTimesByMinutes() As Integer
            Set(ByVal value As Integer)
                _RunTimesByMinutes = value
            End Set
            Get
                Return (_RunTimesByMinutes)
            End Get
        End Property

#End Region


        Private _EntityCode As String
        Public Property EntityCode() As String
            Get
                Return _EntityCode
            End Get
            Set(ByVal value As String)
                _EntityCode = value
            End Set
        End Property

        Private _RunTimesByHours As Integer
        Public Property RunTimesByHours() As Integer
            Get
                Return _RunTimesByHours
            End Get
            Set(ByVal value As Integer)
                _RunTimesByHours = value
            End Set
        End Property

        Private _Runat As Integer
        Public Property Runat() As Integer
            Get
                Return _Runat
            End Get
            Set(ByVal value As Integer)
                _Runat = value
            End Set
        End Property

        Private _Runat2 As Integer
        Public Property Runat2() As Integer
            Get
                Return _Runat2
            End Get
            Set(ByVal value As Integer)
                _Runat2 = value
            End Set
        End Property

        Private _RecordPerPage As Integer
        Public Property RecordPerPage() As Integer
            Get
                Return _RecordPerPage
            End Get
            Set(ByVal value As Integer)
                _RecordPerPage = value
            End Set
        End Property

        Private _IsProduction As Boolean
        Public Property IsProduction() As Boolean
            Get
                Return _IsProduction
            End Get
            Set(ByVal value As Boolean)
                _IsProduction = value
            End Set
        End Property


#Region "Constructor"

        Public Sub New()

            objDALAPP_IntegrationSettings = New DALAPP_IntegrationSettings()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer
            Dim rslt As Integer = objDALAPP_IntegrationSettings.Add(_CompanyName, _HasEmployee, _HasEmployeeLeave, _HasLeaveAudit, _HasEmployeeSupervisor, _HasGrade, _HasDesignation, _HasNationality, _HasWorkLocation, _HasOrganization, _HasLeaveTypes, _HasEmployeeDelegate, _HasHoliday, _HasStudyLeave, _HasApproveErpViolation, _ReprocessFirstSchedule, _ReprocessSecondSchedule, _ReprocessThirdSchedule, _EmailErrorReceiver, _EmailPortNumber, _EmailEnableSsl, _ServiceURL, _ServiceUserName, _ServicePassword, _EntityName, _IntegrationType, _IsPendingLeave, _IsApproveLeave, _IsTrainingLeave, _IsExtraInfoLeave, _IsDutyLeave, _RunTimesByMinutes, _RunTimesByHours, _Runat, _Runat2, _EntityCode, _RecordPerPage, _IsProduction)
            App_EventsLog.Insert_ToEventLog("Add", _CompanyName, "APP_IntegrationSettings", "Integration Settings")
            Return rslt
        End Function

        Public Function GetAll() As APP_IntegrationSettings
            Dim dt As DataTable = objDALAPP_IntegrationSettings.GetAll()

            If dt IsNot Nothing And dt.Rows.Count > 0 Then

                Dim dr As DataRow
                dr = dt.Rows(0)
                If Not IsDBNull(dr("HasEmployee")) Then
                    _HasEmployee = dr("HasEmployee")
                End If
                If Not IsDBNull(dr("HasEmployeeLeave")) Then
                    _HasEmployeeLeave = dr("HasEmployeeLeave")
                End If
                If Not IsDBNull(dr("HasLeaveAudit")) Then
                    _HasLeaveAudit = dr("HasLeaveAudit")
                End If
                If Not IsDBNull(dr("HasEmployeeSupervisor")) Then
                    _HasEmployeeSupervisor = dr("HasEmployeeSupervisor")
                End If
                If Not IsDBNull(dr("HasGrade")) Then
                    _HasGrade = dr("HasGrade")
                End If
                If Not IsDBNull(dr("HasDesignation")) Then
                    _HasDesignation = dr("HasDesignation")
                End If
                If Not IsDBNull(dr("HasNationality")) Then
                    _HasNationality = dr("HasNationality")
                End If
                If Not IsDBNull(dr("HasWorkLocation")) Then
                    _HasWorkLocation = dr("HasWorkLocation")
                End If
                If Not IsDBNull(dr("HasOrganization")) Then
                    _HasOrganization = dr("HasOrganization")
                End If
                If Not IsDBNull(dr("HasLeaveTypes")) Then
                    _HasLeaveTypes = dr("HasLeaveTypes")
                End If
                If Not IsDBNull(dr("HasEmployeeDelegate")) Then
                    _HasEmployeeDelegate = dr("HasEmployeeDelegate")
                End If
                If Not IsDBNull(dr("HasHoliday")) Then
                    _HasHoliday = dr("HasHoliday")
                End If
                If Not IsDBNull(dr("HasStudyLeave")) Then
                    _HasStudyLeave = dr("HasStudyLeave")
                End If
                If Not IsDBNull(dr("HasApproveErpViolation")) Then
                    _HasApproveErpViolation = dr("HasApproveErpViolation")
                End If
                If Not IsDBNull(dr("ReprocessFirstSchedule")) Then
                    _ReprocessFirstSchedule = dr("ReprocessFirstSchedule")
                End If
                If Not IsDBNull(dr("ReprocessSecondSchedule")) Then
                    _ReprocessSecondSchedule = dr("ReprocessSecondSchedule")
                End If
                If Not IsDBNull(dr("ReprocessThirdSchedule")) Then
                    _ReprocessThirdSchedule = dr("ReprocessThirdSchedule")
                End If
                If Not IsDBNull(dr("EmailErrorReceiver")) Then
                    _EmailErrorReceiver = dr("EmailErrorReceiver")
                End If
                If Not IsDBNull(dr("EmailPortNumber")) Then
                    _EmailPortNumber = dr("EmailPortNumber")
                End If
                If Not IsDBNull(dr("EmailEnableSsl")) Then
                    _EmailEnableSsl = dr("EmailEnableSsl")
                End If
                If Not IsDBNull(dr("ServiceURL")) Then
                    _ServiceURL = dr("ServiceURL")
                End If
                If Not IsDBNull(dr("ServiceUserName")) Then
                    _ServiceUserName = dr("ServiceUserName")
                End If
                If Not IsDBNull(dr("ServicePassword")) Then
                    _ServicePassword = dr("ServicePassword")
                End If
                If Not IsDBNull(dr("EntityName")) Then
                    _EntityName = dr("EntityName")
                End If
                If Not IsDBNull(dr("IntegrationType")) Then
                    _IntegrationType = dr("IntegrationType")
                End If
                If Not IsDBNull(dr("IsPendingLeave")) Then
                    _IsPendingLeave = dr("IsPendingLeave")
                End If
                If Not IsDBNull(dr("IsApproveLeave")) Then
                    _IsApproveLeave = dr("IsApproveLeave")
                End If
                If Not IsDBNull(dr("IsTrainingLeave")) Then
                    _IsTrainingLeave = dr("IsTrainingLeave")
                End If
                If Not IsDBNull(dr("IsExtraInfoLeave")) Then
                    _IsExtraInfoLeave = dr("IsExtraInfoLeave")
                End If
                If Not IsDBNull(dr("IsDutyLeave")) Then
                    _IsDutyLeave = dr("IsDutyLeave")
                End If
                If Not IsDBNull(dr("RunTimesByMinutes")) Then
                    _RunTimesByMinutes = dr("RunTimesByMinutes")
                End If
                If Not IsDBNull(dr("Runat")) Then
                    _Runat = dr("Runat")
                End If
                If Not IsDBNull(dr("Runat2")) Then
                    _Runat2 = dr("Runat2")
                End If

                If Not IsDBNull(dr("EntityCode")) Then
                    _EntityCode = dr("EntityCode")
                End If

                If Not IsDBNull(dr("RecordPerPage")) Then
                    _RecordPerPage = dr("RecordPerPage")
                End If

                If Not IsDBNull(dr("IsProduction")) Then
                    _IsProduction = dr("IsProduction")
                End If

                Return Me

            End If

            Return Nothing
        End Function

#End Region

    End Class
End Namespace