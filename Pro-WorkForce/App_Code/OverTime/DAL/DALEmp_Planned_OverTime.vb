Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.OverTime

    Public Class DALEmp_Planned_OverTime
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private Emp_Planned_OverTime_Select As String = "Emp_Planned_OverTime_select"
        Private Emp_Planned_OverTime_Select_All As String = "Emp_Planned_OverTime_select_All"
        Private Emp_Planned_OverTime_Insert As String = "Emp_Planned_OverTime_Insert"
        Private Emp_Planned_OverTime_Update As String = "Emp_Planned_OverTime_Update"
        Private Emp_Planned_OverTime_Delete As String = "Emp_Planned_OverTime_Delete"
#End Region
#Region "Constructor"

        Public Sub New()



        End Sub

#End Region

#Region "Methods"


        Public Function Add(ByVal FK_EmployeeID As Long, ByVal OverTimeMonth As Integer, ByVal OverTimeYear As Integer, ByVal PlannedOT_Normal_Num As Integer, ByVal PlannedOT_Rest_Num As Integer, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Planned_OverTime_Insert, New SqlParameter("@FK_EmployeeID", FK_EmployeeID), _
               New SqlParameter("@OverTimeMonth", OverTimeMonth), _
               New SqlParameter("@OverTimeYear", OverTimeYear), _
               New SqlParameter("@PlannedOT_Normal_Num", PlannedOT_Normal_Num), _
               New SqlParameter("@PlannedOT_Rest_Num", PlannedOT_Rest_Num), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal PlannedOT_ID As Long, ByVal FK_EmployeeID As Long, ByVal OverTimeMonth As Integer, ByVal OverTimeYear As Integer, ByVal PlannedOT_Normal_Num As Integer, ByVal PlannedOT_Rest_Num As Integer, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Planned_OverTime_Update, New SqlParameter("@PlannedOT_ID", PlannedOT_ID), _
               New SqlParameter("@FK_EmployeeID", FK_EmployeeID), _
               New SqlParameter("@OverTimeMonth", OverTimeMonth), _
               New SqlParameter("@OverTimeYear", OverTimeYear), _
               New SqlParameter("@PlannedOT_Normal_Num", PlannedOT_Normal_Num), _
               New SqlParameter("@PlannedOT_Rest_Num", PlannedOT_Rest_Num), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal PlannedOT_ID As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Planned_OverTime_Delete, New SqlParameter("@PlannedOT_ID", PlannedOT_ID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal PlannedOT_ID As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_Planned_OverTime_Select, New SqlParameter("@PlannedOT_ID", PlannedOT_ID)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Planned_OverTime_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace