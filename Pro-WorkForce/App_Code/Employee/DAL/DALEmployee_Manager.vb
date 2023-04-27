Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Employees

    Public Class DALEmployee_Manager
        Inherits MGRBase

#Region "Class Variables"

        Private strConn As String
        Private Employee_Manager_Select As String = "Employee_Manager_select"
        Private Employee_Manager_Select_All As String = "Employee_Manager_select_All"
        Private Employee_Manager_Insert_Assign As String = "Employee_Manager_Insert_Assign"
        Private Emp_GetActiveManagerByEmpId As String = "Emp_GetActiveManagerByEmpId"
        Private Emp_GetActiveManagerbyCompanyorEntity As String = "Emp_GetActiveManagerbyCompanyorEntity"
        Private Employee_Manager_Insert As String = "Employee_Manager_Insert"
        Private Employee_Manager_Update As String = "Employee_Manager_Update"
        Private Employee_Manager_Delete As String = "Employee_Manager_Delete"
        Private Employee_Manager_SelectByManagerID As String = "Employee_Manager_SelectByManagerID"
        Private Get_Manager_Info_ByManagerId As String = "Get_Manager_Info_ByManagerId"
        Private Get_Manager_Notifications As String = "GetManagerNotifications"
        Private Employee_Manager_Assign As String = "Employee_Manager_Assign"
        Private Employee_Manager_IsManager As String = "Employee_Manager_IsManager"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal EmpManagerId As Long, ByVal FK_EmployeeId As Integer, ByVal FK_ManagerId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_Manager_Insert, New SqlParameter("@EmpManagerId", EmpManagerId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_ManagerId", FK_ManagerId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", ToDate), _
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

        Public Function Update(ByVal EmpManagerId As Long, ByVal FK_EmployeeId As Integer, ByVal FK_ManagerId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_Manager_Update, New SqlParameter("@EmpManagerId", EmpManagerId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_ManagerId", FK_ManagerId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", ToDate), _
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

        Public Function Delete(ByVal EmpManagerId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_Manager_Delete, New SqlParameter("@EmpManagerId", EmpManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal EmpManagerId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Employee_Manager_Select, New SqlParameter("@EmpManagerId", EmpManagerId)).Rows(0)
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
                objColl = objDac.GetDataTable(Employee_Manager_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function AssignManager(ByVal FK_EmployeeId As Long, ByVal FK_ManagerId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsTemporary As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_Manager_Insert_Assign, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                  New SqlParameter("@FK_ManagerId", FK_ManagerId), _
                  New SqlParameter("@FromDate", FromDate), _
                  New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
                  New SqlParameter("@IsTemporary", IsTemporary))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Assign_EmployeesManager(ByVal FK_EmployeeId As Long, ByVal FK_ManagerId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsTemporary As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_Manager_Assign, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                  New SqlParameter("@FK_ManagerId", FK_ManagerId), _
                  New SqlParameter("@FromDate", FromDate), _
                  New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
                  New SqlParameter("@IsTemporary", IsTemporary))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetActiveManagerByEmpId(ByVal EmployeeId As Integer, ByVal AssignDate As DateTime) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_GetActiveManagerByEmpId, New SqlParameter("@EmployeeId", EmployeeId), New SqlParameter("@AssignDate", AssignDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetActiveManagerByCompanyandEntity(ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal Assigndate As DateTime) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_GetActiveManagerbyCompanyorEntity, New SqlParameter("@CompanyId", CompanyId), _
                                              New SqlParameter("@EntityId", EntityId), New SqlParameter("@AssignDate", Assigndate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeManagerByManagerID(ByVal FK_ManagerId As Integer) As DataRow
            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Employee_Manager_SelectByManagerID, New SqlParameter("@FK_ManagerID", FK_ManagerId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow
        End Function

        Public Function GetManagerInfoByManagerId(ByVal FK_ManagerId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Get_Manager_Info_ByManagerId, New SqlParameter("@FK_ManagerId", FK_ManagerId), _
                                              New SqlParameter("@FromDate", FromDate), _
                                              New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetManagerNotifications(ByVal EmpIDs As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Get_Manager_Notifications, New SqlParameter("@EmpIDs", EmpIDs))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function IsManager_Employee(ByVal FK_EmployeeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Employee_Manager_IsManager, New SqlParameter("@FK_EmployeeId", FK_EmployeeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow
        End Function
        Public Function IsManager(ByVal FK_EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Employee_Manager_IsManager, New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function
#End Region

    End Class
End Namespace