Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Namespace TA.OverTime

    Public Class Emp_OverTime_Master

#Region "Class Variables"


        Private _OT_MasterID As Long
        Private _FK_EmployeeID As Long
        Private _OverTimeMonth As Integer
        Private _OverTimeYear As Integer
        Private _Planned_OT_Normal As Integer
        Private _Planned_OT_Rest As Integer
        Private _Worked_OT_Normal As Integer
        Private _Worked_OT_Rest As Integer
        Private _Approved_OT_Normal As Integer
        Private _Approved_OT_Rest As Integer
        Private _Note As String
        Private _FK_NextApprover_EmpID As Long
        Private _FK_OTStatusID As Integer
        Private _Final_ActionDate As DateTime
        Private _JustificationRequested As Boolean
        Private _Justificationtext As String
        Private _CREATED_BY As String
        Private _CREATED_DATE As DateTime
        Private _LAST_UPDATE_BY As String
        Private _LAST_UPDATE_DATE As DateTime
        Private _LoggedUserEmployeeID As Long

        Private objDALEmp_OverTime_Master As DALEmp_OverTime_Master
        Private _FK_OTDecisionID As Integer
#End Region

#Region "Public Properties"
        Public Property FK_OTDecisionID() As Integer
            Set(ByVal value As Integer)
                _FK_OTDecisionID = value
            End Set
            Get
                Return (_FK_OTDecisionID)
            End Get
        End Property
        Public Property LoggedUserEmployeeID() As Long
            Set(ByVal value As Long)
                _LoggedUserEmployeeID = value
            End Set
            Get
                Return (_LoggedUserEmployeeID)
            End Get
        End Property
        Public Property OT_MasterID() As Long
            Set(ByVal value As Long)
                _OT_MasterID = value
            End Set
            Get
                Return (_OT_MasterID)
            End Get
        End Property


        Public Property FK_EmployeeID() As Long
            Set(ByVal value As Long)
                _FK_EmployeeID = value
            End Set
            Get
                Return (_FK_EmployeeID)
            End Get
        End Property


        Public Property OverTimeMonth() As Integer
            Set(ByVal value As Integer)
                _OverTimeMonth = value
            End Set
            Get
                Return (_OverTimeMonth)
            End Get
        End Property


        Public Property OverTimeYear() As Integer
            Set(ByVal value As Integer)
                _OverTimeYear = value
            End Set
            Get
                Return (_OverTimeYear)
            End Get
        End Property


        Public Property Planned_OT_Normal() As Integer
            Set(ByVal value As Integer)
                _Planned_OT_Normal = value
            End Set
            Get
                Return (_Planned_OT_Normal)
            End Get
        End Property


        Public Property Planned_OT_Rest() As Integer
            Set(ByVal value As Integer)
                _Planned_OT_Rest = value
            End Set
            Get
                Return (_Planned_OT_Rest)
            End Get
        End Property


        Public Property Worked_OT_Normal() As Integer
            Set(ByVal value As Integer)
                _Worked_OT_Normal = value
            End Set
            Get
                Return (_Worked_OT_Normal)
            End Get
        End Property


        Public Property Worked_OT_Rest() As Integer
            Set(ByVal value As Integer)
                _Worked_OT_Rest = value
            End Set
            Get
                Return (_Worked_OT_Rest)
            End Get
        End Property


        Public Property Approved_OT_Normal() As Integer
            Set(ByVal value As Integer)
                _Approved_OT_Normal = value
            End Set
            Get
                Return (_Approved_OT_Normal)
            End Get
        End Property


        Public Property Approved_OT_Rest() As Integer
            Set(ByVal value As Integer)
                _Approved_OT_Rest = value
            End Set
            Get
                Return (_Approved_OT_Rest)
            End Get
        End Property


        Public Property Note() As String
            Set(ByVal value As String)
                _Note = value
            End Set
            Get
                Return (_Note)
            End Get
        End Property


        Public Property FK_NextApprover_EmpID() As Long
            Set(ByVal value As Long)
                _FK_NextApprover_EmpID = value
            End Set
            Get
                Return (_FK_NextApprover_EmpID)
            End Get
        End Property



        Public Property FK_OTStatusID() As Integer
            Set(ByVal value As Integer)
                _FK_OTStatusID = value
            End Set
            Get
                Return (_FK_OTStatusID)
            End Get
        End Property


        Public Property Final_ActionDate() As DateTime
            Set(ByVal value As DateTime)
                _Final_ActionDate = value
            End Set
            Get
                Return (_Final_ActionDate)
            End Get
        End Property


        Public Property JustificationRequested() As Boolean
            Set(ByVal value As Boolean)
                _JustificationRequested = value
            End Set
            Get
                Return (_JustificationRequested)
            End Get
        End Property


        Public Property Justificationtext() As String
            Set(ByVal value As String)
                _Justificationtext = value
            End Set
            Get
                Return (_Justificationtext)
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

#End Region


#Region "Constructor"

        Public Sub New()

            objDALEmp_OverTime_Master = New DALEmp_OverTime_Master()

        End Sub

#End Region

#Region "Methods"

        Public Function Add() As Integer

            Return objDALEmp_OverTime_Master.Add(_FK_EmployeeID, _OverTimeMonth, _OverTimeYear, _Planned_OT_Normal, _Planned_OT_Rest, _Worked_OT_Normal, _Worked_OT_Rest, _Approved_OT_Normal, _Approved_OT_Rest, _Note, _FK_OTDecisionID, _JustificationRequested, _Justificationtext, _CREATED_BY, _LoggedUserEmployeeID)
        End Function

        Public Function Update() As Integer

            Return objDALEmp_OverTime_Master.Update(_OT_MasterID, _Planned_OT_Normal, _Planned_OT_Rest, _Worked_OT_Normal, _Worked_OT_Rest, _Approved_OT_Normal, _Approved_OT_Rest, _Note, _FK_OTDecisionID, _JustificationRequested, _Justificationtext, _LAST_UPDATE_BY, _LoggedUserEmployeeID)

        End Function
        Public Function AddPlannedOT() As Integer

            Return objDALEmp_OverTime_Master.AddPlannedOT(_FK_EmployeeID, _OverTimeMonth, _OverTimeYear, _Planned_OT_Normal, _Planned_OT_Rest, _CREATED_BY)
        End Function


        Public Function Delete() As Integer

            Return objDALEmp_OverTime_Master.Delete(_OT_MasterID)

        End Function

        Public Function GetAll() As DataTable

            Return objDALEmp_OverTime_Master.GetAll(_CREATED_BY)

        End Function

        Public Function GetOTSummary() As DataTable

            Return objDALEmp_OverTime_Master.GetOTSummary(_FK_EmployeeID, _OverTimeMonth, _OverTimeYear)

        End Function
        Public Function GetOTSummaryForManager() As DataTable

            Return objDALEmp_OverTime_Master.GetOTSummaryForManager(_FK_EmployeeID)

        End Function
        Public Function GetOTMyApplications() As DataTable

            Return objDALEmp_OverTime_Master.GetOTMyApplications(_FK_EmployeeID)

        End Function
        Public Function GetOTSummaryForHREmployee() As DataTable

            Return objDALEmp_OverTime_Master.GetOTSummaryForHREmployee(_OverTimeMonth, _OverTimeYear)

        End Function
        Public Function GetByPK() As Emp_OverTime_Master

            Dim dr As DataRow
            dr = objDALEmp_OverTime_Master.GetByPK(_OT_MasterID)

            If Not IsDBNull(dr("OT_MasterID")) Then
                _OT_MasterID = dr("OT_MasterID")
            End If
            If Not IsDBNull(dr("FK_EmployeeID")) Then
                _FK_EmployeeID = dr("FK_EmployeeID")
            End If
            If Not IsDBNull(dr("OverTimeMonth")) Then
                _OverTimeMonth = dr("OverTimeMonth")
            End If
            If Not IsDBNull(dr("OverTimeYear")) Then
                _OverTimeYear = dr("OverTimeYear")
            End If
            If Not IsDBNull(dr("Planned_OT_Normal")) Then
                _Planned_OT_Normal = dr("Planned_OT_Normal")
            End If
            If Not IsDBNull(dr("Planned_OT_Rest")) Then
                _Planned_OT_Rest = dr("Planned_OT_Rest")
            End If
            If Not IsDBNull(dr("Worked_OT_Normal")) Then
                _Worked_OT_Normal = dr("Worked_OT_Normal")
            End If
            If Not IsDBNull(dr("Worked_OT_Rest")) Then
                _Worked_OT_Rest = dr("Worked_OT_Rest")
            End If
            If Not IsDBNull(dr("Approved_OT_Normal")) Then
                _Approved_OT_Normal = dr("Approved_OT_Normal")
            End If
            If Not IsDBNull(dr("Approved_OT_Rest")) Then
                _Approved_OT_Rest = dr("Approved_OT_Rest")
            End If
            If Not IsDBNull(dr("Note")) Then
                _Note = dr("Note")
            End If
            If Not IsDBNull(dr("FK_NextApprover_EmpID")) Then
                _FK_NextApprover_EmpID = dr("FK_NextApprover_EmpID")
            End If
           
            If Not IsDBNull(dr("FK_OTStatusID")) Then
                _FK_OTStatusID = dr("FK_OTStatusID")
            End If
            If Not IsDBNull(dr("Final_ActionDate")) Then
                _Final_ActionDate = dr("Final_ActionDate")
            End If
            If Not IsDBNull(dr("JustificationRequested")) Then
                _JustificationRequested = dr("JustificationRequested")
            End If
            If Not IsDBNull(dr("Justificationtext")) Then
                _Justificationtext = dr("Justificationtext")
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
        Public Function GetOTDetailedReport() As DataTable

            Return objDALEmp_OverTime_Master.GetOTDetailedReport(_FK_EmployeeID, _OverTimeMonth, _OverTimeYear)

        End Function
#End Region

    End Class
End Namespace