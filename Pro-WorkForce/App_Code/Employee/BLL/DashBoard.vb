Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports TA.Lookup

Namespace TA.DashBoard


    Public Class DashBoard
        Inherits MGRBase

#Region "Class Variables"

        Private objDALDash_Board As DALDash_Board
        Private _FK_CompanyId As Integer
        Private _FK_EntityId As Integer
        Private _FK_EmployeeId As Integer
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _Lang As String
        Private _ID As Integer

        Private _DrillDownType As String

#End Region

#Region "Properties"

        Public Property FK_CompanyId() As Integer
            Set(ByVal value As Integer)
                _FK_CompanyId = value
            End Set
            Get
                Return (_FK_CompanyId)
            End Get
        End Property

        Public Property FK_EntityId() As Integer
            Set(ByVal value As Integer)
                _FK_EntityId = value
            End Set
            Get
                Return (_FK_EntityId)
            End Get
        End Property

        Public Property FK_EmployeeId() As Integer
            Set(ByVal value As Integer)
                _FK_EmployeeId = value
            End Set
            Get
                Return (_FK_EmployeeId)
            End Get
        End Property

        Public Property FromDate() As DateTime
            Set(ByVal value As DateTime)
                _FromDate = value
            End Set
            Get
                Return (_FromDate)
            End Get
        End Property

        Public Property ToDate() As DateTime
            Set(ByVal value As DateTime)
                _ToDate = value
            End Set
            Get
                Return (_ToDate)
            End Get
        End Property

        Public Property Lang() As String
            Set(ByVal value As String)
                _Lang = value
            End Set
            Get
                Return (_Lang)
            End Get
        End Property

        Public Property DrillDownType() As String
            Set(ByVal value As String)
                _DrillDownType = value
            End Set
            Get
                Return (_DrillDownType)
            End Get
        End Property

        Public Property ID() As Integer
            Set(ByVal value As Integer)
                _ID = value
            End Set
            Get
                Return (_ID)
            End Get
        End Property

#End Region

#Region "Constructor"

        Public Sub New()

            objDALDash_Board = New DALDash_Board

        End Sub

#End Region


        Public Function GetAttendDash() As DataTable

            Return objDALDash_Board.GetAttendDash(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate, _Lang)

        End Function

        Public Function GetAbsentDash() As DataTable

            Return objDALDash_Board.GetAbsentDash(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate, _Lang)

        End Function

        Public Function GetDelayDash() As DataTable

            Return objDALDash_Board.GetDelayDash(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate, _Lang)

        End Function

        Public Function GetEarlyoutDash() As DataTable

            Return objDALDash_Board.GetEarlyoutDash(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate, _Lang)

        End Function

        Public Function GetLeaveDash() As DataTable

            Return objDALDash_Board.GetLeaveDash(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate, _Lang)

        End Function

        Public Function GetSummaryDash() As DataTable

            Return objDALDash_Board.GetSummaryDash(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate, _Lang)

        End Function

        Public Function GetStatusCountDash() As DataTable

            Return objDALDash_Board.GetStatusCountDash(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate)

        End Function

        Public Function GetRequestStatusCountDash() As DataTable

            Return objDALDash_Board.GetRequestStatusCountDash(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate)

        End Function

        Public Function GetEmployeeWorkingHoursDash() As DataTable

            Return objDALDash_Board.GetEmployeeWorkingHoursDash(_FK_CompanyId, _FK_EntityId, FK_EmployeeId, _FromDate, _ToDate)

        End Function

        Public Function GetTransaction_StatsDash() As DataTable

            Return objDALDash_Board.GetTransaction_StatsDash(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate)

        End Function

        Public Function GetDailyTransactionsDash() As DataTable

            Return objDALDash_Board.GetDailyTransactionsDash(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate, _Lang)

        End Function

        Public Function GetEmployeeSummaryDash() As DataTable

            Return objDALDash_Board.GetEmployeeSummaryDash(_FK_EmployeeId, _FromDate, _ToDate, _Lang)

        End Function
        Public Function RetreivDrillDownData() As DataTable

            Return objDALDash_Board.GetRetreivDrillDownData(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate, _Lang, _DrillDownType)

        End Function
        Public Function GetFeedbackDash() As DataTable

            Return objDALDash_Board.GetFeedbackDash(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate, _Lang)

        End Function

        Public Function GetUserGroups() As DataTable

            Return objDALDash_Board.GetUserGroups(_Lang)

        End Function

        Public Function GetInnerEmployee_AttendDash() As DataTable

            Return objDALDash_Board.GetInnerEmployee_AttendDash(_FK_CompanyId, _Lang)

        End Function

        Public Function GetInnerPage_AttendPercent_DashB() As DataTable

            Return objDALDash_Board.GetInnerPage_AttendPercent_DashB(_ID, _FromDate, _Lang)

        End Function

        Public Function GetSummaryCountDash() As DataTable

            Return objDALDash_Board.GetSummaryCountDash(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate)

        End Function

        Public Function GetLeaveRequestCountDash() As DataTable

            Return objDALDash_Board.GetLeaveRequestCountDash(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate)

        End Function

        Public Function GetPermissionRequestCountDash() As DataTable

            Return objDALDash_Board.GetPermissionRequestCountDash(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate)

        End Function

        Public Function GetSummaryViolationCountDash() As DataTable

            Return objDALDash_Board.GetSummaryViolationCountDash(_FK_CompanyId, _FK_EntityId, _FromDate, _ToDate)

        End Function

    End Class
End Namespace
