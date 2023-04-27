Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA.SelfServices

    Public Class DALEmployeeViolations
        Inherits MGRBase

#Region "Class Variables"

        Private Get_Emp_Violations As String = "Get_Emp_Violations"
        Private Get_EmpSummary As String = "Get_EmpSummary"
        Private Get_EmpInTime As String = "Get_EmpInTime"
        Private Get_EmpMoveStatus_ByEmpId As String = "Get_EmpMoveStatus_ByEmpId"
        Private Get_HR_Emp_Violations As String = "Get_HR_Emp_Violations"
        Private Employee_GetCalendar As String = "Employee_GetCalendar"
        Private Employee_GetManagerCalendar As String = "Employee_GetManagerCalendar"
        Private Employee_GetManager_CalendarDetails As String = "Employee_GetManager_CalendarDetails"

#End Region

#Region "Constructor"
        Public Sub New()

        End Sub

#End Region

#Region "Methods"
       
        Public Function GetEmployeeViolations(ByVal FK_EmployeeId As String, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Get_Emp_Violations, New SqlParameter("@EmployeeId", IIf(FK_EmployeeId = Nothing, DBNull.Value, FK_EmployeeId)), New SqlParameter("@FromDate", FromDate), _
                                                New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetEmpSummary(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objrow As DataRow
            Try
                objrow = objDac.GetDataTable(Get_EmpSummary, New SqlParameter("@EmployeeId", FK_EmployeeId), _
                                                New SqlParameter("@FromDate", FromDate), _
                                                New SqlParameter("@ToDate", ToDate)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objrow

        End Function
        Public Function GetEmpInTime(ByVal FK_EmployeeId As Integer, ByVal M_Date As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objrow As DataRow
            Try
                objrow = objDac.GetDataTable(Get_EmpInTime, New SqlParameter("@EmployeeId", FK_EmployeeId), _
                                                New SqlParameter("@MoveDate", M_Date)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objrow

        End Function
        Public Function Get_EmpMoveStatus(ByVal FK_EmployeeId As Integer, ByVal M_Date As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objrow As DataRow
            Try
                objrow = objDac.GetDataTable(Get_EmpMoveStatus_ByEmpId, New SqlParameter("@EmployeeId", FK_EmployeeId), _
                                                New SqlParameter("@M_DATE", M_Date)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objrow

        End Function
        Public Function GetHR_EmployeeViolations(ByVal FK_EmployeeId As String, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Get_HR_Emp_Violations, New SqlParameter("@EmployeeId", IIf(FK_EmployeeId = Nothing, DBNull.Value, FK_EmployeeId)), New SqlParameter("@FromDate", FromDate), _
                                                New SqlParameter("@ToDate", ToDate), _
                                                New SqlParameter("@CompanyId", CompanyId), _
                                                New SqlParameter("@EntityId", EntityId), _
                                                New SqlParameter("@WorkLocationId", WorkLocationId), _
                                                New SqlParameter("@LogicalGroupId", LogicalGroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeCalendar(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Employee_GetCalendar, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                                New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetManagerCalendar(ByVal ManagerId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Employee_GetManagerCalendar, New SqlParameter("@ManagerId", ManagerId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                                New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetManagerCalendarDetails(ByVal ApptType As Integer, ByVal ManagerId As Integer, ByVal FromDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Employee_GetManager_CalendarDetails, New SqlParameter("@ApptType", ApptType), _
                                              New SqlParameter("@FK_ManagerId", ManagerId), _
                                                New SqlParameter("@Date", FromDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

#End Region

    End Class

End Namespace
