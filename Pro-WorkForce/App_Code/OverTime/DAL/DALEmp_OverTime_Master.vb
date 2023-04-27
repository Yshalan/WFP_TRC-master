Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.OverTime

    Public Class DALEmp_OverTime_Master
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_OverTime_Master_Select As String = "Emp_OverTime_Master_select"
        Private Emp_OverTime_Master_Select_All As String = "Emp_OverTime_Master_select_All"
        Private Emp_OverTime_Master_Insert As String = "Emp_OverTime_Master_Insert"
        Private Emp_OverTime_Master_Update As String = "Emp_OverTime_Master_Update"
        Private Emp_OverTime_Master_Delete As String = "Emp_OverTime_Master_Delete"
        Private Emp_Overtime_GetList As String = "Emp_Overtime_GetList"
        Private Emp_Overtime_GetList_ForIntManager As String = "Emp_Overtime_GetList_ForIntManager"
        Private Emp_Overtime_GetList_ForHR As String = "Emp_Overtime_GetList_ForHR"
        Private Emp_Overtime_GetApplications As String = "Emp_Overtime_GetApplications"
        Private Emp_OverTime_Master_InsertPlannedOT As String = "Emp_OverTime_Master_InsertPlannedOT"
        Private Rpt_Emp_OverTime_Details As String = "Rpt_Emp_OverTime_Details"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EmployeeID As Long, ByVal OverTimeMonth As Integer, ByVal OverTimeYear As Integer, ByVal Planned_OT_Normal As Integer, ByVal Planned_OT_Rest As Integer, ByVal Worked_OT_Normal As Integer, ByVal Worked_OT_Rest As Integer, ByVal Approved_OT_Normal As Integer, ByVal Approved_OT_Rest As Integer, ByVal Note As String, ByVal FK_OTDecisionID As Integer, ByVal JustificationRequested As Boolean, ByVal Justificationtext As String, ByVal CREATED_BY As String, ByVal LoggedUserEmployeeID As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_OverTime_Master_Insert, _
               New SqlParameter("@FK_EmployeeID", FK_EmployeeID), _
               New SqlParameter("@OverTimeMonth", OverTimeMonth), _
               New SqlParameter("@OverTimeYear", OverTimeYear), _
               New SqlParameter("@Planned_OT_Normal", Planned_OT_Normal), _
               New SqlParameter("@Planned_OT_Rest", Planned_OT_Rest), _
               New SqlParameter("@Worked_OT_Normal", Worked_OT_Normal), _
               New SqlParameter("@Worked_OT_Rest", Worked_OT_Rest), _
               New SqlParameter("@Approved_OT_Normal", Approved_OT_Normal), _
               New SqlParameter("@Approved_OT_Rest", Approved_OT_Rest), _
               New SqlParameter("@Note", Note), _
               New SqlParameter("@FK_OTDecisionID", FK_OTDecisionID), _
               New SqlParameter("@JustificationRequested", JustificationRequested), _
               New SqlParameter("@Justificationtext", Justificationtext), _
               New SqlParameter("@CREATED_BY", CREATED_BY), New SqlParameter("@LoggedUserEmployeeID", LoggedUserEmployeeID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function AddPlannedOT(ByVal FK_EmployeeID As Long, ByVal OverTimeMonth As Integer, ByVal OverTimeYear As Integer, ByVal Planned_OT_Normal As Integer, ByVal Planned_OT_Rest As Integer, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_OverTime_Master_InsertPlannedOT, _
               New SqlParameter("@FK_EmployeeID", FK_EmployeeID), _
               New SqlParameter("@OverTimeMonth", OverTimeMonth), _
               New SqlParameter("@OverTimeYear", OverTimeYear), _
               New SqlParameter("@Planned_OT_Normal", Planned_OT_Normal), _
               New SqlParameter("@Planned_OT_Rest", Planned_OT_Rest), _
               New SqlParameter("@CREATED_BY", CREATED_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function Update(ByVal OT_MasterID As Long, ByVal Planned_OT_Normal As Integer, ByVal Planned_OT_Rest As Integer, ByVal Worked_OT_Normal As Integer, ByVal Worked_OT_Rest As Integer, ByVal Approved_OT_Normal As Integer, ByVal Approved_OT_Rest As Integer, ByVal Note As String, ByVal FK_OTDecisionID As Integer, ByVal JustificationRequested As Boolean, ByVal Justificationtext As String, ByVal LAST_UPDATE_BY As String, ByVal LoggedUserEmployeeID As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_OverTime_Master_Update, New SqlParameter("@OT_MasterID", OT_MasterID), _
               New SqlParameter("@Planned_OT_Normal", Planned_OT_Normal), _
               New SqlParameter("@Planned_OT_Rest", Planned_OT_Rest), _
               New SqlParameter("@Worked_OT_Normal", Worked_OT_Normal), _
               New SqlParameter("@Worked_OT_Rest", Worked_OT_Rest), _
               New SqlParameter("@Approved_OT_Normal", Approved_OT_Normal), _
               New SqlParameter("@Approved_OT_Rest", Approved_OT_Rest), _
               New SqlParameter("@Note", Note), _
               New SqlParameter("@FK_OTDecisionID", FK_OTDecisionID), _
               New SqlParameter("@JustificationRequested", JustificationRequested), _
               New SqlParameter("@Justificationtext", Justificationtext), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), New SqlParameter("@LoggedUserEmployeeID", LoggedUserEmployeeID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal OT_MasterID As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_OverTime_Master_Delete, New SqlParameter("@OT_MasterID", OT_MasterID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal OT_MasterID As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_OverTime_Master_Select, New SqlParameter("@OT_MasterID", OT_MasterID)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll(ByVal LoggedUserID As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_OverTime_Master_Select_All, New SqlParameter("@LoggedUserID", LoggedUserID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetOTSummary(ByVal FK_EmployeeID As Long, ByVal OverTimeMonth As Integer, ByVal OverTimeYear As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Overtime_GetList, New SqlParameter("@LoggedEmployeeID", FK_EmployeeID), _
               New SqlParameter("@Month", OverTimeMonth), _
               New SqlParameter("@Year", OverTimeYear))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetOTSummaryForManager(ByVal FK_EmployeeID As Long) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Overtime_GetList_ForIntManager, New SqlParameter("@LoggedEmployeeID", FK_EmployeeID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetOTMyApplications(ByVal FK_EmployeeID As Long) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Overtime_GetApplications, New SqlParameter("@LoggedEmployeeID", FK_EmployeeID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetOTSummaryForHREmployee(ByVal OverTimeMonth As Integer, ByVal OverTimeYear As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Overtime_GetList_ForHR, New SqlParameter("@Month", OverTimeMonth), _
               New SqlParameter("@Year", OverTimeYear))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetOTDetailedReport(ByVal EmployeeID As Integer, ByVal OverTimeMonth As Integer, ByVal OverTimeYear As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Rpt_Emp_OverTime_Details, New SqlParameter("@FK_EmployeeID", EmployeeID), New SqlParameter("@Month", OverTimeMonth), New SqlParameter("@Year", OverTimeYear))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace