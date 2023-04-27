Imports Microsoft.VisualBasic
Imports ST.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports ST.UTILITIES
Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES


Namespace TA.Employees

    Public Class DALEmp_Leaves_BalanceHistory
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_Leaves_BalanceHistory_Select As String = "Emp_Leaves_BalanceHistory_select"
        Private Emp_Leaves_BalanceHistory_Select_All As String = "Emp_Leaves_BalanceHistory_select_All"
        Private Emp_Leaves_BalanceHistory_Insert As String = "Emp_Leaves_BalanceHistory_Insert"
        Private Emp_Leaves_BalanceHistory_Update As String = "Emp_Leaves_BalanceHistory_Update"
        Private Emp_Leaves_BalanceHistory_Delete As String = "Emp_Leaves_BalanceHistory_Delete"
        Private Emp_Leaves_BalanceHistory_GetBalance As String = "Emp_Leaves_BalanceHistory_GetBalance"
        Private Emp_Leaves_BalanceHistory_GetLastForLeave As String = "Emp_Leaves_BalanceHistory_GetLastForLeave"
        Private Emp_Leaves_BalanceHistory_Insert_ByPerm As String = "Emp_Leaves_BalanceHistory_Insert_ByPerm"
        Private Emp_Leaves_BalanceHistory_GetLastBalance As String = "Emp_Leaves_BalanceHistory_GetLastBalance"
        Private Emp_Leaves_BalanceHistory_Select_History As String = "Emp_Leaves_BalanceHistory_Select_History"
        Private Emp_Leaves_BalanceHistory_Select_All_LastBalance As String = "Emp_Leaves_BalanceHistory_Select_All_LastBalance"
        Private Emp_Leaves_BalanceHistory_SelectLeave_TimeLine As String = "Emp_Leaves_BalanceHistory_SelectLeave_TimeLine"


#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef BalanceId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_LeaveId As Integer, ByVal BalanceDate As DateTime, ByVal Balance As Double, ByVal TotalBalance As Double, ByVal Remarks As String, ByVal FK_EmpLeaveId As Integer, ByVal CREATED_DATE As DateTime, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@BalanceId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, BalanceId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Leaves_BalanceHistory_Insert, sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_LeaveId", FK_LeaveId), _
               New SqlParameter("@BalanceDate", BalanceDate), _
               New SqlParameter("@Balance", Balance), _
               New SqlParameter("@TotalBalance", TotalBalance), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@FK_EmpLeaveId", FK_EmpLeaveId), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@CREATED_BY", CREATED_BY))
                BalanceId = sqlOut.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function AddAddBalance(ByVal FK_EmployeeId As Long, ByVal FK_LeaveId As Integer, ByVal BalanceDate As DateTime, ByVal Balance As Double, ByVal TotalBalance As Double, ByVal Remarks As String, ByVal FK_EmpPermId As Integer, ByVal CREATED_DATE As DateTime, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Leaves_BalanceHistory_Insert_ByPerm, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_LeaveId", FK_LeaveId), _
               New SqlParameter("@BalanceDate", BalanceDate), _
               New SqlParameter("@Balance", Balance), _
               New SqlParameter("@TotalBalance", TotalBalance), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@FK_EmpPermId", FK_EmpPermId), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@CREATED_BY", CREATED_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal BalanceId As Long, ByVal FK_EmployeeId As Long, ByVal FK_LeaveId As Integer, ByVal BalanceDate As DateTime, ByVal Balance As Double, ByVal TotalBalance As Double, ByVal Remarks As String, ByVal FK_EmpLeaveId As Integer, ByVal CREATED_DATE As DateTime, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Leaves_BalanceHistory_Update, New SqlParameter("@BalanceId", BalanceId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_LeaveId", FK_LeaveId), _
               New SqlParameter("@BalanceDate", BalanceDate), _
               New SqlParameter("@Balance", Balance), _
               New SqlParameter("@TotalBalance", TotalBalance), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@FK_EmpLeaveId", FK_EmpLeaveId), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@CREATED_BY", CREATED_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal BalanceId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Leaves_BalanceHistory_Delete, New SqlParameter("@BalanceId", BalanceId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal BalanceId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_Leaves_BalanceHistory_Select, New SqlParameter("@BalanceId", BalanceId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetLastForLeave(ByVal EmployeeId As Long, ByVal EmpLeaveId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_Leaves_BalanceHistory_GetLastForLeave, New SqlParameter("@EmployeeId", EmployeeId), New SqlParameter("@FK_EmpLeaveId", EmpLeaveId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function
        Public Function GetLastBalance(ByVal EmployeeId As Long, ByVal LeaveId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_Leaves_BalanceHistory_GetLastBalance, New SqlParameter("@EmployeeId", EmployeeId), New SqlParameter("@FK_LeaveId", LeaveId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_Leaves_BalanceHistory_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function


        Public Function GetLeaveBalanceHistory(ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal EmployeeId As Long) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Leaves_BalanceHistory_Select_History, New SqlParameter("@CompanyId", CompanyId), _
                                              New SqlParameter("@EntityId", EntityId), _
                                              New SqlParameter("@EmployeeId", EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetEmpLeaveBalance(ByVal EmployeeId As Integer, ByVal LeaveTypeId As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Leaves_BalanceHistory_GetBalance, New SqlParameter("@EmployeeId", EmployeeId), New SqlParameter("@LeaveTypeId", LeaveTypeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetEmpAllLeave_LastBalance(ByVal EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Leaves_BalanceHistory_Select_All_LastBalance, New SqlParameter("@EmployeeId", EmployeeId),
                                              New SqlParameter("@FromDate", FromDate),
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetEmpLeave_TimeLine(ByVal EmployeeId As Integer, ByVal FK_LeaveId As Integer, ByVal BalanceYear As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Leaves_BalanceHistory_SelectLeave_TimeLine, New SqlParameter("@FK_EmployeeId", EmployeeId),
                                              New SqlParameter("@FK_LeaveId", FK_LeaveId),
                                              New SqlParameter("@BalanceYear", BalanceYear))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

#End Region

    End Class
End Namespace