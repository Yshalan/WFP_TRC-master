Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.DailyTasks

    Public Class DALMonthly_Deduction
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private Monthly_Deduction_Select As String = "Monthly_Deduction_select"
        Private Monthly_Deduction_Select_All As String = "Monthly_Deduction_select_All"
        Private Monthly_Deduction_Insert As String = "Monthly_Deduction_Insert"
        Private Monthly_Deduction_Update As String = "Monthly_Deduction_Update"
        Private Monthly_Deduction_Delete As String = "Monthly_Deduction_Delete"
        Private Monthly_Deduction_Select_All_Inner As String = "Monthly_Deduction_Select_All_Inner"
        Private Monthly_Deduction_UpdateApproval As String = "Monthly_Deduction_UpdateApproval"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal Year As String, ByVal Month As String, ByVal FK_EmployeeId As Integer, ByVal Delay As DateTime, ByVal EarlyOut As DateTime, ByVal Delay_Count As Integer, ByVal EarlyOut_Count As Integer, ByVal Absent_Count As Integer, ByVal MissingIN_Count As Integer, ByVal MissingOut_Count As Integer, ByVal Uncomplete50WHRS As Integer, ByVal IsApproved As Boolean, ByVal Apporved_By As String, ByVal Approval_Date As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Monthly_Deduction_Insert, New SqlParameter("@Year", Year), _
               New SqlParameter("@Month", Month), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@Delay", Delay), _
               New SqlParameter("@EarlyOut", EarlyOut), _
               New SqlParameter("@Delay_Count", Delay_Count), _
               New SqlParameter("@EarlyOut_Count", EarlyOut_Count), _
               New SqlParameter("@Absent_Count", Absent_Count), _
               New SqlParameter("@MissingIN_Count", MissingIN_Count), _
               New SqlParameter("@MissingOut_Count", MissingOut_Count), _
               New SqlParameter("@Uncomplete50WHRS", Uncomplete50WHRS), _
               New SqlParameter("@IsApproved", IsApproved), _
               New SqlParameter("@Apporved_By", Apporved_By), _
               New SqlParameter("@Approval_Date", Approval_Date))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal Year As String, ByVal Month As String, ByVal FK_EmployeeId As Integer, ByVal Delay As DateTime, ByVal EarlyOut As DateTime, ByVal Delay_Count As Integer, ByVal EarlyOut_Count As Integer, ByVal Absent_Count As Integer, ByVal MissingIN_Count As Integer, ByVal MissingOut_Count As Integer, ByVal Uncomplete50WHRS As Integer, ByVal IsApproved As Boolean, ByVal Apporved_By As String, ByVal Approval_Date As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Monthly_Deduction_Update, New SqlParameter("@Year", Year), _
               New SqlParameter("@Month", Month), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@Delay", Delay), _
               New SqlParameter("@EarlyOut", EarlyOut), _
               New SqlParameter("@Delay_Count", Delay_Count), _
               New SqlParameter("@EarlyOut_Count", EarlyOut_Count), _
               New SqlParameter("@Absent_Count", Absent_Count), _
               New SqlParameter("@MissingIN_Count", MissingIN_Count), _
               New SqlParameter("@MissingOut_Count", MissingOut_Count), _
               New SqlParameter("@Uncomplete50WHRS", Uncomplete50WHRS), _
               New SqlParameter("@IsApproved", IsApproved), _
               New SqlParameter("@Apporved_By", Apporved_By), _
               New SqlParameter("@Approval_Date", Approval_Date))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update_Approval(ByVal Year As String, ByVal Month As String, ByVal FK_EmployeeId As Integer, ByVal Apporved_By As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Monthly_Deduction_UpdateApproval, New SqlParameter("@Year", Year), _
               New SqlParameter("@Month", Month), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@Apporved_By", Apporved_By))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal Year As String, ByVal Month As String, ByVal FK_EmployeeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Monthly_Deduction_Delete, New SqlParameter("@Year", Year), _
               New SqlParameter("@Month", Month), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal Year As String, ByVal Month As String, ByVal FK_EmployeeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Monthly_Deduction_Select, New SqlParameter("@Year", Year), _
               New SqlParameter("@Month", Month), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId)).Rows(0)
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
                objColl = objDac.GetDataTable(Monthly_Deduction_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_Inner(ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal Year As String, ByVal Month As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Monthly_Deduction_Select_All_Inner, New SqlParameter("@Year", Year), _
                                              New SqlParameter("@Month", Month), _
                                        New SqlParameter("@CompanyId", FK_CompanyId), _
                                        New SqlParameter("@EntityId", FK_EntityId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace