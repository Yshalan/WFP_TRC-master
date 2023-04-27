Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES


Namespace TA.Employees

    Public Class DALEmployee
        Inherits MGRBase

#Region "Class Variables"

        Private strConn As String
        Private Employee_ChangeEntity As String = "Employee_ChangeEntity"
        Private Employee_Select As String = "Employee_select"
        Private Get_Filtered_Employee As String = "Get_Filtered_Employee"
        Private Get_Filtered_Former_Employee As String = "Get_Filtered_Former_Employee" 'ID: M01 || Date: 20-04-2023 || By: Yahia shalan || Description: Defining stored procedure name for the former employees.'
        Private Employee_Select_All As String = "Employee_select_All"
        Private Employee_Insert As String = "Employee_Insert"
        Private Employee_Update As String = "Employee_Update"
        Private Employee_Delete As String = "Employee_Delete"
        Private Employee_Select_ByCompany As String = "Employee_Select_ByCompany"
        Private Emp_GetAllSchedule As String = "Emp_GetAllSchedule"
        Private Employee_SelectScedule_ByCompany As String = "Employee_SelectScedule_ByCompany"
        Private Employee_SelectAllActiveSchedule As String = "Employee_SelectAllActiveSchedule"
        Private EMPLOYEE_Select_ByCompEnt As String = "EMPLOYEE_Select_ByCompEnt"
        Private Employee_Select_All_ForDDL As String = "Employee_Select_All_ForDDL"
        Private Rpt_Attend_DashB As String = "Rpt_Attend_DashB"
        Private Get_Filtered_Employee_Print_Status As String = "Get_Filtered_Employee_Print_Status"
        Private Emp_ImagePath_Insert As String = "Emp_ImagePath_Insert"
        Private Employee_LogicalGroup_Update As String = "Employee_LogicalGroup_Update"
        Private Employee_Select_ByFK_LogicalGroup As String = "Employee_Select_ByFK_LogicalGroup"
        Private Get_Filtered_Managers As String = "Get_Filtered_Managers"
        Private Get_EmployeeFullDetails As String = "Get_EmployeeFullDetails"
        Private Emp_Get_Employee_ByEmpNo As String = "Emp_Get_Employee_ByEmpNo"
        Private Employee_Select_All_Count As String = "Employee_Select_All_Count"
        Private Emp_Search_Method As String = "Emp_Search_Method"
        Private CheckExist_Employee As String = "CheckExist_Employee"
        Private Employee_Select_ByFK_WorkLocation As String = "Employee_Select_ByFK_WorkLocation"
        Private Get_Filtered_Employee_ByStatus As String = "Get_Filtered_Employee_ByStatus"
        Private Employee_UpdateEmpNo As String = "Employee_UpdateEmpNo"
        Private Employee_Get_ByEntityId As String = "Employee_Get_ByEntityId"
        Private GET_MaxEmpNo As String = "GET_MaxEmpNo"
        Private Get_Filtered_EmployeeByManager As String = "Get_Filtered_EmployeeByManager"
        Private Get_Filtered_EmployeeByManagerForEntity As String="Get_Filtered_EmployeeByManagerForEntity"
        Private Get_Filtered_EmployeeByManager_Advanced As String = "Get_Filtered_EmployeeByManager_Advanced"
        Private Get_Filtered_EmployeeByManager_Advanced_ByFilter As String = "Get_Filtered_EmployeeByManager_Advanced_ByFilter"
        Private Employee_Get_RecordCount As String = "Employee_Get_RecordCount"
        Private Employee_SelectBy_EmpEmail As String = "Employee_SelectBy_EmpEmail"
        Private Employee_Select_All_Status_ByEmpNo As String = "Employee_Select_All_Status_ByEmpNo"
        Private Employee_CheckCardNo As String = "Employee_CheckCardNo"
        Private Employee_InnerPage_Summary As String = "Employee_InnerPage_Summary"
        Private Employee_GetActive_EmployeeCount As String = "Employee_GetActive_EmployeeCount"
        Private Employee_SelectEmployee_EntityCount As String = "Employee_SelectEmployee_EntityCount"
        Private Employee_SelectByEmpNo As String = "Employee_SelectByEmpNo"
        Private Get_Employee_Print_Card As String = "Get_Employee_Print_Card"


#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Get_EmployeeRecordCount(ByVal EmployeeId As Integer, ByVal FK_EntityId As Integer, ByVal Fk_CompanyId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Employee_Get_RecordCount, New SqlParameter("@EmpId", EmployeeId), _
                                             New SqlParameter("@EntityId", FK_EntityId), _
                                             New SqlParameter("@CompanyId", Fk_CompanyId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function ChangeEntity(EmployeeId As Long, FK_EntityId As Integer, Last_Update_By As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_ChangeEntity, New SqlParameter("@EmployeeId", EmployeeId), _
               New SqlParameter("@FK_EntityId", FK_EntityId), New SqlParameter("@LAST_UPDATE_BY", Last_Update_By))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function Add(ByRef EmployeeId As Integer, ByVal EmployeeNo As String, ByVal EmployeeCardNo As String, ByVal FK_Status As Integer, ByVal EmployeeName As String, ByVal EmployeeArabicName As String, ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal EntityHierarchy As String, ByVal Gender As String, ByVal DOB As DateTime, ByVal Email As String, ByVal FK_Nationality As Integer, ByVal FK_Religion As Integer, ByVal FK_MaritalStatus As Integer, ByVal FK_WorkLocation As Integer, ByVal FK_Grade As Integer, ByVal FK_Designation As Integer, ByVal FK_LogicalGroup As Integer, ByVal AnnualLeaveBalance As Double, ByVal JoinDate As DateTime, ByVal IsTerminated As Boolean, ByVal TerminateDate As DateTime, ByVal Remarks As String, ByVal EmpImagePath As String, ByVal NationalId As String, ByVal MobileNo As String, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime, ByVal PayRollNumber As String, ByVal FK_EmployeeTypeId As Integer, ByVal ContractEndDate As DateTime, ByVal ExternalPartyName As String, ByVal GTVerfiedBy As String, ByVal Pin As String) As Integer


            Dim sqlParamEmpId As New SqlParameter()
            sqlParamEmpId.Value = EmployeeId
            sqlParamEmpId.ParameterName = "@EmployeeId"
            sqlParamEmpId.Direction = ParameterDirection.Output

            objDac = DAC.getDAC()
            Try


                errNo = objDac.AddUpdateDeleteSPTrans(Employee_Insert, sqlParamEmpId, New SqlParameter("@EmployeeNo", EmployeeNo), _
                                                      New SqlParameter("@EmployeeCardNo", EmployeeCardNo), _
               New SqlParameter("@FK_Status", FK_Status), _
               New SqlParameter("@EmployeeName", EmployeeName), _
               New SqlParameter("@EmployeeArabicName", EmployeeArabicName), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId), _
               New SqlParameter("@FK_EntityId", FK_EntityId), _
               New SqlParameter("@EntityHierarchy", EntityHierarchy), _
               New SqlParameter("@Gender", Gender), _
               New SqlParameter("@DOB", IIf(DOB = DateTime.MinValue, DBNull.Value, DOB)), _
               New SqlParameter("@Email", Email), _
               New SqlParameter("@FK_Nationality", IIf(FK_Nationality = -1, DBNull.Value, FK_Nationality)), _
               New SqlParameter("@FK_Religion", IIf(FK_Religion = -1, DBNull.Value, FK_Religion)), _
               New SqlParameter("@FK_MaritalStatus", IIf(FK_MaritalStatus = -1, DBNull.Value, FK_MaritalStatus)), _
               New SqlParameter("@FK_WorkLocation", IIf(FK_WorkLocation = -1, DBNull.Value, FK_WorkLocation)), _
               New SqlParameter("@FK_Grade", IIf(FK_Grade = -1, DBNull.Value, FK_Grade)), _
               New SqlParameter("@FK_Designation", IIf(FK_Designation = -1, DBNull.Value, FK_Designation)), _
               New SqlParameter("@FK_LogicalGroup", IIf(FK_LogicalGroup = -1, DBNull.Value, FK_LogicalGroup)), _
               New SqlParameter("@AnnualLeaveBalance", AnnualLeaveBalance), _
               New SqlParameter("@JoinDate", IIf(JoinDate = DateTime.MinValue, DBNull.Value, JoinDate)), _
               New SqlParameter("@IsTerminated", IsTerminated), _
               New SqlParameter("@TerminateDate", IIf(TerminateDate = DateTime.MinValue, DBNull.Value, TerminateDate)), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@EmpImagePath", EmpImagePath), _
               New SqlParameter("@NationalId", NationalId), _
               New SqlParameter("@MobileNo", MobileNo), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE), _
               New SqlParameter("@PayRollNumber", PayRollNumber), _
               New SqlParameter("@FK_EmployeeTypeId", FK_EmployeeTypeId), _
               New SqlParameter("@ContractEndDate", IIf(ContractEndDate = DateTime.MinValue, DBNull.Value, ContractEndDate)), _
               New SqlParameter("@ExternalPartyName", ExternalPartyName), _
               New SqlParameter("@GT_VerfiedBy", IIf(GTVerfiedBy = Nothing, DBNull.Value, GTVerfiedBy)), _
               New SqlParameter("@PIN", IIf(Pin = Nothing, DBNull.Value, Pin)))
                If Not IsDBNull(sqlParamEmpId.Value) Then
                    EmployeeId = sqlParamEmpId.Value
                End If


            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal EmployeeId As Long, ByVal EmployeeNo As String, ByVal EmployeeCardNo As String, ByVal FK_Status As Integer, ByVal EmployeeName As String, ByVal EmployeeArabicName As String, ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal EntityHierarchy As String, ByVal Gender As String, ByVal DOB As DateTime, ByVal Email As String, ByVal FK_Nationality As Integer, ByVal FK_Religion As Integer, ByVal FK_MaritalStatus As Integer, ByVal FK_WorkLocation As Integer, ByVal FK_Grade As Integer, ByVal FK_Designation As Integer, ByVal FK_LogicalGroup As Integer, ByVal AnnualLeaveBalance As Double, ByVal JoinDate As DateTime, ByVal IsTerminated As Boolean, ByVal TerminateDate As DateTime, ByVal Remarks As String, ByVal EmpImagePath As String, ByVal NationalId As String, ByVal MobileNo As String, ByVal LAST_UPDATE_BY As String, ByVal GTVerfiedBy As String, ByVal Pin As String, ByVal PayRollNumber As String, ByVal FK_EmployeeTypeId As Integer, ByVal ContractEndDate As DateTime, ByVal ExternalPartyName As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_Update, New SqlParameter("@EmployeeId", EmployeeId), _
               New SqlParameter("@EmployeeNo", EmployeeNo), _
               New SqlParameter("@EmployeeCardNo", EmployeeCardNo), _
               New SqlParameter("@FK_Status", FK_Status), _
               New SqlParameter("@EmployeeName", EmployeeName), _
               New SqlParameter("@EmployeeArabicName", EmployeeArabicName), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId), _
               New SqlParameter("@FK_EntityId", FK_EntityId), _
               New SqlParameter("@EntityHierarchy", EntityHierarchy), _
               New SqlParameter("@Gender", Gender), _
               New SqlParameter("@DOB", IIf(DOB = DateTime.MinValue, DBNull.Value, DOB)), _
               New SqlParameter("@Email", Email), _
               New SqlParameter("@FK_Nationality", IIf(FK_Nationality = -1, DBNull.Value, FK_Nationality)), _
               New SqlParameter("@FK_Religion", IIf(FK_Religion = -1, DBNull.Value, FK_Religion)), _
               New SqlParameter("@FK_MaritalStatus", IIf(FK_MaritalStatus = -1, DBNull.Value, FK_MaritalStatus)), _
               New SqlParameter("@FK_WorkLocation", IIf(FK_WorkLocation = -1, DBNull.Value, FK_WorkLocation)), _
               New SqlParameter("@FK_Grade", IIf(FK_Grade = -1, DBNull.Value, FK_Grade)), _
               New SqlParameter("@FK_Designation", IIf(FK_Designation = -1, DBNull.Value, FK_Designation)), _
               New SqlParameter("@FK_LogicalGroup", IIf(FK_LogicalGroup = -1, DBNull.Value, FK_LogicalGroup)), _
               New SqlParameter("@AnnualLeaveBalance", AnnualLeaveBalance), _
               New SqlParameter("@JoinDate", JoinDate), _
               New SqlParameter("@IsTerminated", IsTerminated), _
               New SqlParameter("@TerminateDate", IIf(TerminateDate = DateTime.MinValue, DBNull.Value, TerminateDate)), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@EmpImagePath", EmpImagePath), _
               New SqlParameter("@NationalId", NationalId), _
               New SqlParameter("@MobileNo", MobileNo), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@GT_VerfiedBy", IIf(GTVerfiedBy = Nothing, DBNull.Value, GTVerfiedBy)), _
               New SqlParameter("@PIN", IIf(Pin = Nothing, DBNull.Value, Pin)), _
               New SqlParameter("@PayRollNumber", PayRollNumber), _
               New SqlParameter("@FK_EmployeeTypeId", FK_EmployeeTypeId), _
               New SqlParameter("@ContractEndDate", IIf(ContractEndDate = DateTime.MinValue, DBNull.Value, ContractEndDate)), _
               New SqlParameter("@ExternalPartyName", ExternalPartyName))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update_EmpNo(ByVal EmployeeId As Long, ByVal EmployeeNo As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_UpdateEmpNo, New SqlParameter("@EmployeeId", EmployeeId), _
               New SqlParameter("@EmployeeNo", EmployeeNo))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal EmployeeId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_Delete, New SqlParameter("@EmployeeId", EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal EmployeeId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Employee_Select, New SqlParameter("@EmployeeId", EmployeeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetRowByEmpNo(ByVal EmployeeNo As String) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Employee_SelectByEmpNo, New SqlParameter("@EmployeeNo", EmployeeNo)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetMaxEmpNo(ByVal InitialIndex As Integer, ByVal FK_EmployeeTypeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(GET_MaxEmpNo, New SqlParameter("@InitialIndex", InitialIndex), _
                                            New SqlParameter("@FK_EmployeeTypeId", FK_EmployeeTypeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function Get_EmployeeIdByEmail(ByVal Email As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_SelectBy_EmpEmail, New SqlParameter("@Email", Email))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetActive_EmployeeCount(ByVal FK_CompanyId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_GetActive_EmployeeCount, New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_Inner_Summary(ByVal UserId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_InnerPage_Summary, New SqlParameter("@UserId", UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetCountEmployeesAndUsers() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_Select_All_Count, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GettEmployee_EntityCount(ByVal FK_CompanyId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_SelectEmployee_EntityCount, New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetEmpByCompEnt(ByVal CompanyId As Integer, ByVal EntityId As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(EMPLOYEE_Select_ByCompEnt, New SqlParameter("@CompanyId", CompanyId), _
               New SqlParameter("@EntityId", EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetEmpByCompany(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FK_LogicalGroup As Integer, ByVal FK_WorkLocations As Integer, ByVal FilterType As String, ByVal PageNo As Integer, ByVal PageSize As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                If Not FK_EntityId = -1 Then
                    objColl = objDac.GetDataTable(Get_Filtered_Employee, New SqlParameter("@CompanyId", FK_CompanyId), _
                                                  New SqlParameter("@EntityId", FK_EntityId), _
                                                  New SqlParameter("@FilterType", FilterType), _
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID), _
                                                  New SqlParameter("@PageNo", PageNo), _
                                                  New SqlParameter("@PageSize", PageSize))
                ElseIf Not FK_LogicalGroup = -1 Then
                    objColl = objDac.GetDataTable(Get_Filtered_Employee, New SqlParameter("@CompanyId", FK_CompanyId), _
                                                  New SqlParameter("@WorkGroupID", FK_LogicalGroup), _
                                                  New SqlParameter("@FilterType", FilterType), _
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID), _
                                                  New SqlParameter("@PageNo", PageNo), _
                                                  New SqlParameter("@PageSize", PageSize))
                ElseIf Not FK_WorkLocations = -1 Then
                    objColl = objDac.GetDataTable(Get_Filtered_Employee, New SqlParameter("@CompanyId", FK_CompanyId), _
                                                  New SqlParameter("@WorkLocationsID", FK_WorkLocations), _
                                                  New SqlParameter("@FilterType", FilterType), _
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID), _
                                                  New SqlParameter("@PageNo", PageNo), _
                                                  New SqlParameter("@PageSize", PageSize))
                Else
                    objColl = objDac.GetDataTable(Get_Filtered_Employee, New SqlParameter("@CompanyId", FK_CompanyId), _
                                                  New SqlParameter("@FilterType", FilterType), _
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID), _
                                                  New SqlParameter("@PageNo", PageNo), _
                                                  New SqlParameter("@PageSize", PageSize))
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetEmpByStatus(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FK_LogicalGroup As Integer, ByVal FK_WorkLocations As Integer, ByVal FilterType As String, ByVal Status As Integer, ByVal PageNo As Integer, ByVal PageSize As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                If Not FK_EntityId = -1 Then
                    objColl = objDac.GetDataTable(Get_Filtered_Employee_ByStatus, New SqlParameter("@CompanyId", FK_CompanyId), _
                                                  New SqlParameter("@EntityId", FK_EntityId), _
                                                  New SqlParameter("@FilterType", FilterType), _
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID), _
                                                  New SqlParameter("@Status", Status), _
                                                  New SqlParameter("@PageNo", PageNo), _
                                                  New SqlParameter("@PageSize", PageSize))
                ElseIf Not FK_LogicalGroup = -1 Then
                    objColl = objDac.GetDataTable(Get_Filtered_Employee_ByStatus, New SqlParameter("@CompanyId", FK_CompanyId), _
                                                  New SqlParameter("@WorkGroupID", FK_LogicalGroup), _
                                                  New SqlParameter("@FilterType", FilterType), _
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID), _
                                                  New SqlParameter("@Status", Status), _
                                                  New SqlParameter("@PageNo", PageNo), _
                                                  New SqlParameter("@PageSize", PageSize))
                ElseIf Not FK_WorkLocations = -1 Then
                    objColl = objDac.GetDataTable(Get_Filtered_Employee_ByStatus, New SqlParameter("@CompanyId", FK_CompanyId), _
                                                  New SqlParameter("@WorkLocationsID", FK_WorkLocations), _
                                                  New SqlParameter("@FilterType", FilterType), _
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID), _
                                                  New SqlParameter("@Status", Status), _
                                                  New SqlParameter("@PageNo", PageNo), _
                                                  New SqlParameter("@PageSize", PageSize))
                Else
                    objColl = objDac.GetDataTable(Get_Filtered_Employee_ByStatus, New SqlParameter("@CompanyId", FK_CompanyId), _
                                                  New SqlParameter("@FilterType", FilterType), _
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID), _
                                                  New SqlParameter("@Status", Status), _
                                                  New SqlParameter("@PageNo", PageNo), _
                                                  New SqlParameter("@PageSize", PageSize))
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetManagersByCompany(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FilterType As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                If (FK_EntityId <> -1) Then
                    objColl = objDac.GetDataTable(Get_Filtered_Managers, New SqlParameter("@CompanyId", FK_CompanyId), New SqlParameter("@EntityId", FK_EntityId), New SqlParameter("@FilterType", FilterType), New SqlParameter("@UserId", SessionVariables.LoginUser.ID))
                Else
                    objColl = objDac.GetDataTable(Get_Filtered_Managers, New SqlParameter("@CompanyId", FK_CompanyId), New SqlParameter("@FilterType", FilterType), New SqlParameter("@UserId", SessionVariables.LoginUser.ID))
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetEmpByCompany_ByPrintQueue(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal PrintStatus As Boolean, ByVal PageNo As Integer, ByVal PageSize As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                If (FK_EntityId <> -1) Then
                    objColl = objDac.GetDataTable(Get_Filtered_Employee_Print_Status, New SqlParameter("@CompanyId", FK_CompanyId), _
                                                  New SqlParameter("@EntityId", FK_EntityId), _
                                                  New SqlParameter("@PrintStatus", PrintStatus), _
                                                  New SqlParameter("@PageNo", PageNo), _
                                                  New SqlParameter("@PageSize", PageSize), _
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID))
                Else
                    objColl = objDac.GetDataTable(Get_Filtered_Employee_Print_Status, _
                                                  New SqlParameter("@CompanyId", FK_CompanyId), _
                                                  New SqlParameter("@PrintStatus", PrintStatus), _
                                                  New SqlParameter("@PageNo", PageNo), _
                                                  New SqlParameter("@PageSize", PageSize), _
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID))
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetEmpByCompanyDesig_ByPrintQueue(ByVal FK_CompanyId As Integer, ByVal _FK_Designation As Integer, ByVal PrintStatus As Boolean, ByVal _FK_CardType As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Get_Employee_Print_Card, New SqlParameter("@CompanyId", FK_CompanyId), _
                                                  New SqlParameter("@DesignationID", _FK_Designation), _
                                                  New SqlParameter("@FK_CardType", _FK_CardType), _
                                                  New SqlParameter("@PrintStatus", PrintStatus))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllEmpSchedule(ByVal EmployeeNo As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_GetAllSchedule, New SqlParameter("@EmployeeNo", EmployeeNo))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllScheduleByCompany(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_SelectScedule_ByCompany, New SqlParameter("@FK_CompanyId", FK_CompanyId), New SqlParameter("@FK_EntityId", IIf(FK_EntityId = -1, DBNull.Value, FK_EntityId)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function AssignEmployeeLogicalGroup(ByVal EmployeeID As Integer, ByVal FK_LogicalGroup As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_LogicalGroup_Update, New SqlParameter("@EmployeeId", EmployeeID), _
               New SqlParameter("@LogicalGroup", IIf(FK_LogicalGroup = Nothing, DBNull.Value, FK_LogicalGroup)))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByLogicalGroup(ByVal FK_LogicalGroup As Integer, ByVal FK_Entity As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_Select_ByFK_LogicalGroup, _
                                              New SqlParameter("@FK_LogicalGroup", FK_LogicalGroup), _
                                              New SqlParameter("@FK_Entity", IIf(FK_Entity = -1, DBNull.Value, FK_Entity)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmpByEmpNo(ByVal EmployeeNo As String, ByVal FK_CompanyId As Integer, ByVal UserId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Get_Employee_ByEmpNo, New SqlParameter("@EmployeeNo", EmployeeNo),
                                              New SqlParameter("@FK_CompanyId", FK_CompanyId),
New SqlParameter("@UserId", UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetByWorkLocation(ByVal FK_WorkLocationId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_Select_ByFK_WorkLocation, _
                                              New SqlParameter("@FK_WorkLocation", FK_WorkLocationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetByEntityId(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_Get_ByEntityId, _
                                              New SqlParameter("@CompanyId", FK_CompanyId), _
                                              New SqlParameter("@EntityId", FK_EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetAll_ByEmplyeeNo(ByVal EmployeeNo As String, ByVal FK_CompanyId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_Select_All_Status_ByEmpNo, New SqlParameter("@EmployeeNo", EmployeeNo), _
                                              New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function CheckCardNo(ByVal EmployeeCardNo As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_CheckCardNo, New SqlParameter("@EmployeeCardNo", EmployeeCardNo))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#Region "Get Employees By Company or Entity"
        Public Function GetEmployeesByCompanyOrEntity(ByVal CompanyId As Integer, ByVal EntityId As String, ByVal FilterType As String, ByVal PageNo As Integer, ByVal PageSize As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                If (EntityId <> -1) Then
                    objColl = objDac.GetDataTable(Get_Filtered_Employee, New SqlParameter("@CompanyId", CompanyId),
                                                  New SqlParameter("@EntityId", EntityId), New SqlParameter("@FilterType", FilterType),
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID),
                                                  New SqlParameter("@PageNo", PageNo),
                                                  New SqlParameter("@pagesize", PageSize))
                Else
                    objColl = objDac.GetDataTable(Get_Filtered_Employee, New SqlParameter("@CompanyId", CompanyId),
                                                  New SqlParameter("@FilterType", FilterType),
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID),
                                                  New SqlParameter("@PageNo", PageNo),
                                                  New SqlParameter("@pagesize", PageSize))
                End If

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetFormerEmployeesByCompanyOrEntity(ByVal CompanyId As Integer, ByVal EntityId As String, ByVal FilterType As String, ByVal PageNo As Integer, ByVal PageSize As Integer) As DataTable 'ID: M02 || Date: 20-04-2023 || By: Yahia shalan || Description: Get the former employees.'

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                If (EntityId <> -1) Then
                    objColl = objDac.GetDataTable(Get_Filtered_Former_Employee, New SqlParameter("@CompanyId", CompanyId),
                                                  New SqlParameter("@EntityId", EntityId), New SqlParameter("@FilterType", FilterType),
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID),
                                                  New SqlParameter("@PageNo", PageNo),
                                                  New SqlParameter("@pagesize", PageSize))
                Else
                    objColl = objDac.GetDataTable(Get_Filtered_Former_Employee, New SqlParameter("@CompanyId", CompanyId),
                                                  New SqlParameter("@FilterType", FilterType),
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID),
                                                  New SqlParameter("@PageNo", PageNo),
                                                  New SqlParameter("@pagesize", PageSize))
                End If

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeesByCompanyOrEntity_ByStatus(ByVal CompanyId As Integer, ByVal EntityId As String, ByVal FilterType As String, ByVal Status As Integer, ByVal PageNo As Integer, ByVal PageSize As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                If (EntityId <> -1) Then
                    objColl = objDac.GetDataTable(Get_Filtered_Employee_ByStatus, New SqlParameter("@CompanyId", CompanyId), _
                                                  New SqlParameter("@EntityId", EntityId), New SqlParameter("@FilterType", FilterType), _
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID), _
                                                  New SqlParameter("@Status", Status), _
                                                  New SqlParameter("@PageNo", PageNo), _
                                                  New SqlParameter("@pagesize", PageSize))
                Else
                    objColl = objDac.GetDataTable(Get_Filtered_Employee_ByStatus, New SqlParameter("@CompanyId", CompanyId), _
                                                  New SqlParameter("@FilterType", FilterType), _
                                                  New SqlParameter("@UserId", SessionVariables.LoginUser.ID), _
                                                  New SqlParameter("@Status", Status), _
                                                  New SqlParameter("@PageNo", PageNo), _
                                                  New SqlParameter("@pagesize", PageSize))
                End If

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeByManagerId(ByVal FK_CompanyId As Integer, ByVal FK_ManagerId As Integer, ByVal EntityId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Get_Filtered_EmployeeByManager, New SqlParameter("@FK_CompanyId", FK_CompanyId), _
                                              New SqlParameter("@FK_ManagerId", FK_ManagerId), _
                                              New SqlParameter("@FK_EntityId", EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetEmployeeByManagerIdForEntity(ByVal FK_CompanyId As Integer, ByVal FK_ManagerId As Integer, ByVal FK_EntityId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Get_Filtered_EmployeeByManagerForEntity, New SqlParameter("@FK_CompanyId", FK_CompanyId), _
                                              New SqlParameter("@FK_ManagerId", FK_ManagerId), _
                                               New SqlParameter("@FK_EntityId", FK_EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetEmployeeByManagerIdAdvanced(ByVal FK_CompanyId As Integer, ByVal FK_ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Get_Filtered_EmployeeByManager_Advanced, New SqlParameter("@FK_CompanyId", FK_CompanyId), _
                                              New SqlParameter("@FK_ManagerId", FK_ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetEmployeeByManagerIdAdvancedByFilter(ByVal FK_CompanyId As Integer, ByVal FK_ManagerId As Integer, ByVal FK_EntityId As Integer, ByVal FK_Designation As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Get_Filtered_EmployeeByManager_Advanced_ByFilter, New SqlParameter("@FK_CompanyId", FK_CompanyId), _
                                              New SqlParameter("@FK_ManagerId", FK_ManagerId), _
                                               New SqlParameter("@FK_EntityId", FK_EntityId), _
                                                New SqlParameter("@FK_DesginationId", FK_Designation))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
#End Region

#Region "SearchMethod"
        Public Function GetBySearchCriteria(ByVal Type As Integer, ByVal Value As String, ByVal Oper As String) As DataTable

            objDac = DAC.getDAC()
            Dim objDT As DataTable
            Try
                objDT = objDac.GetDataTable(Emp_Search_Method, New SqlParameter("@Type", Type), _
                                            New SqlParameter("@Value", Value), _
                                            New SqlParameter("@Operator", Oper))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
                objDT = Nothing
            End Try
            Return objDT
        End Function

#End Region
#End Region

#Region "Extended Class variables"

        Private EmployeeGetAllInnerJoin As String = "Employee_GetAllInnerJoin"
        Private EmployeeFindExisting As String = "Employee_Find_Existing"

#End Region

#Region "Extended Methods"
        Public Function GetAllInnerJoin() As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(EmployeeGetAllInnerJoin, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function FindExisting(ByVal EmployeeId As Integer) As Boolean
            objDac = DAC.getDAC()
            Try
                Dim rslt As Integer = objDac.GetSingleValue(Of Integer)( _
                EmployeeFindExisting, _
               New SqlParameter("@EmployeeId", EmployeeId))
                If rslt = 1 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetAllforDDL() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_Select_All_ForDDL, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function EmpImagePath_Insert(ByVal EmployeeId As Long, ByVal EmpImagePath As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_ImagePath_Insert, New SqlParameter("@EmployeeId", EmployeeId), _
                                                      New SqlParameter("@EmpImagePath", EmpImagePath))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function GetEmpDetails(ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal EmployeeId As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Get_EmployeeFullDetails, New SqlParameter("@EmployeeId", EmployeeId), _
                                              New SqlParameter("@EntityId", EntityId), _
                                              New SqlParameter("@CompanyId", CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetExist_Employee(ByVal EmployeeId As Integer) As Integer
            objDac = DAC.getDAC()
            Dim objColl As Integer
            Try
                objColl = objDac.AddUpdateDeleteSP(CheckExist_Employee, New SqlParameter("@EmployeeId", EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region



    End Class
End Namespace