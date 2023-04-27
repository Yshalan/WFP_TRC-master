Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.LookUp


Namespace TA_ApproveViolationToERP

    Public Class DALApproveViolationToERP
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private ApproveViolationToERP_Select As String = "ApproveViolationToERP_select"
        Private ApproveViolationToERP_Select_All As String = "ApproveViolationToERP_select_All"
        Private ApproveViolationToERP_Insert As String = "ApproveViolationToERP_Insert"
        Private ApproveViolationToERP_Update As String = "ApproveViolationToERP_Update"
        Private ApproveViolationToERP_Delete As String = "ApproveViolationToERP_Delete"
        Private ApproveViolationToERP_GetAll As String = "ApproveViolationToERP_GetAll"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EmployeeId As Integer, ByVal ViolationType As String, ByVal ViolationDate As DateTime, ByVal ViolationDateNum As Integer, ByVal IsApproved As Boolean, ByVal APPROVED_BY As String, ByVal APPROVED_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ApproveViolationToERP_Insert, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@ViolationType", ViolationType),
               New SqlParameter("@ViolationDate", ViolationDate),
               New SqlParameter("@ViolationDateNum", ViolationDateNum),
               New SqlParameter("@IsApproved", IsApproved),
               New SqlParameter("@APPROVED_BY", APPROVED_BY),
               New SqlParameter("@APPROVED_DATE", APPROVED_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal ID As Integer, ByVal FK_EmployeeId As Integer, ByVal ViolationType As String, ByVal ViolationDate As DateTime, ByVal ViolationDateNum As Integer, ByVal IsApproved As Boolean, ByVal APPROVED_BY As String, ByVal APPROVED_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ApproveViolationToERP_Update, New SqlParameter("@ID", ID),
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@ViolationType", ViolationType),
               New SqlParameter("@ViolationDate", ViolationDate),
               New SqlParameter("@ViolationDateNum", ViolationDateNum),
               New SqlParameter("@IsApproved", IsApproved),
               New SqlParameter("@APPROVED_BY", APPROVED_BY),
               New SqlParameter("@APPROVED_DATE", APPROVED_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal ID As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ApproveViolationToERP_Delete, New SqlParameter("@ID", ID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal ID As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(ApproveViolationToERP_Select, New SqlParameter("@ID", ID)).Rows(0)
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
                objColl = objDac.GetDataTable(ApproveViolationToERP_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function


        Public Function GetAll_ByFilter(ByVal EmployeeId As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal Year As Integer, ByVal Month As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(ApproveViolationToERP_GetAll, New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                New SqlParameter("@Year", Year),
                New SqlParameter("@Month", Month))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetAll_ByFilter_Header(ByVal EmployeeId As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal Year As Integer, ByVal Month As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable("ApproveViolationToERP_GetAll_Header", New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@Year", Year),
                New SqlParameter("@Month", Month),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetAll_ByFilter_DOF(ByVal EmployeeId As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal Year As Integer, ByVal Month As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable("ApproveViolationToERP_GetAll_DOF", New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                New SqlParameter("@Year", Year),
                New SqlParameter("@Month", Month))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetAll_ByFilter_Header_DOF(ByVal EmployeeId As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal Year As Integer, ByVal Month As Integer, ByVal DirectStaffOnly As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable("ApproveViolationToERP_GetAll_Header_DOF", New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@Year", Year),
                New SqlParameter("@Month", Month),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
#End Region



        Public Function GetAll_ByFilter_ManaftPayroll(ByVal EmployeeId As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal Year As Integer, ByVal Month As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable("ApproveViolationToERP_GetAll_Manafth", New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@Year", Year),
                New SqlParameter("@Month", Month))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function


        Public Function GetAll_ByFilter_Header_ManaftPayroll(ByVal EmployeeId As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal WorkLocationId As Integer, ByVal LogicalGroupId As Integer, ByVal Year As Integer, ByVal Month As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable("ApproveViolationToERP_GetAll_Header_Manafth", New SqlParameter("@EmployeeId", EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@Year", Year),
                New SqlParameter("@Month", Month))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function PayrollManafth_Approve(Delay As Boolean, EarlyOut As Boolean, Absent As Boolean, MissingIn As Boolean, MissingOut As Boolean, ByVal OutDuration As Boolean, RecordId As String) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans("PayrollManafth_Approve", New SqlParameter("@RecordId", Convert.ToInt32(RecordId)),
                                                            New SqlParameter("@ConsiderDelay", Delay),
                                                            New SqlParameter("@ConsiderEarlyOut", EarlyOut),
                                                            New SqlParameter("@ConsiderAbsent", Absent),
                                                            New SqlParameter("@ConsiderMissingIn", MissingIn),
                                                            New SqlParameter("@ConsiderMissingOut", MissingOut),
                                                            New SqlParameter("@ConsiderOutDuration", OutDuration))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Function PayrollManafth_FinalApprove(p1 As String, Delay As Boolean, EarlyOut As Boolean, Absent As Boolean, MissingIn As Boolean, MissingOut As Boolean, OutDuration As Boolean) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans("PayrollManafth_PostFinalApproval", New SqlParameter("@DeductionId", Convert.ToInt32(p1)) _
                                                          , New SqlParameter("@ConsiderDelay", Delay) _
                                                        , New SqlParameter("@ConsiderEarlyOut", EarlyOut),
                                                        New SqlParameter("@ConsiderAbsent", Absent) _
                                                        , New SqlParameter("@ConsiderMissingIn", MissingIn) _
                                                         , New SqlParameter("@ConsiderMissingOut", MissingOut) _
                                                         , New SqlParameter("@ConsiderOutDuration", OutDuration))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Function PayrollManafth_GetFinalApproval(Year As Integer, Month As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable("PayrollManafth_GetFinalApproval",
                New SqlParameter("@Year", Year),
                New SqlParameter("@Month", Month))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Function ManafthPayrollDeduction_Remove(recordId As String) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans("ManafthPayrollDeduction_Remove", New SqlParameter("@RecordId", Convert.ToInt32(recordId)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Function Approve(ID As Integer, IsApproved As Boolean, APPROVED_BY As String) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans("DOFPayrollDeduction_Approve",
                                                New SqlParameter("@ID", Convert.ToInt32(ID)),
                                                New SqlParameter("@APPROVED_BY", APPROVED_BY)
                                                      )
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Function GetEmployeeLeaveBalance_DOF() As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable("DOF_EmployeeLeaveBalance_GET")
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Function ProcessViolation_DOF(FK_EmployeeId As Integer, CompanyId As Integer, EntityId As Integer, WorkLocationId As Integer, LogicalGroupId As Integer, Year As Integer, Month As Integer, DirectStaffOnly As Boolean) As Integer
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                errNo = objDac.AddUpdateDeleteSPTrans("ApproveViolationToERP_ProcessAll_DOF", New SqlParameter("@EmployeeId", FK_EmployeeId),
                New SqlParameter("@CompanyId", CompanyId),
                New SqlParameter("@EntityId", EntityId),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@LogicalGroupId", LogicalGroupId),
                New SqlParameter("@DirectStaffOnly", DirectStaffOnly),
                New SqlParameter("@Year", Year),
                New SqlParameter("@Month", Month))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

    End Class
End Namespace