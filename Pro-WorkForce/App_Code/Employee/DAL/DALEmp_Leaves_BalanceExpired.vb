Imports Microsoft.VisualBasic
Imports ST.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports ST.UTILITIES
Imports TA.Lookup
Imports SmartV.UTILITIES
Imports SmartV.DB


Namespace TA.Employees

    Public Class DALEmp_Leaves_BalanceExpired
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_Leaves_BalanceExpired_Select As String = "Emp_Leaves_BalanceExpired_select"
        Private Emp_Leaves_BalanceExpired_Select_All As String = "Emp_Leaves_BalanceExpired_select_All"
        Private Emp_Leaves_BalanceExpired_Select_Expired As String = "Emp_Leaves_BalanceExpired_Select_Expired"
        Private Emp_Leaves_BalanceExpired_Insert As String = "Emp_Leaves_BalanceExpired_Insert"
        Private Emp_Leaves_BalanceExpired_Update As String = "Emp_Leaves_BalanceExpired_Update"
        Private Emp_Leaves_BalanceExpired_Delete As String = "Emp_Leaves_BalanceExpired_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal BalanceExpiredId As Long, ByVal FK_EmployeeId As Long, ByVal FK_LeaveId As Integer, ByVal ExpireDate As DateTime, ByVal ExpireBalance As Double, ByVal Remarks As String, ByVal Action As Integer, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Leaves_BalanceExpired_Insert, New SqlParameter("@BalanceExpiredId", BalanceExpiredId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_LeaveId", FK_LeaveId), _
               New SqlParameter("@ExpireDate", ExpireDate), _
               New SqlParameter("@ExpireBalance", ExpireBalance), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@Action", Action), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal BalanceExpiredId As Long, ByVal FK_EmployeeId As Long, ByVal FK_LeaveId As Integer, ByVal ExpireDate As DateTime, ByVal ExpireBalance As Double, ByVal Remarks As String, ByVal Action As Integer, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Leaves_BalanceExpired_Update, New SqlParameter("@BalanceExpiredId", BalanceExpiredId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_LeaveId", FK_LeaveId), _
               New SqlParameter("@ExpireDate", ExpireDate), _
               New SqlParameter("@ExpireBalance", ExpireBalance), _
               New SqlParameter("@Remarks", Remarks), _
               New SqlParameter("@Action", Action), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal BalanceExpiredId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Leaves_BalanceExpired_Delete, New SqlParameter("@BalanceExpiredId", BalanceExpiredId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal BalanceExpiredId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_Leaves_BalanceExpired_Select, New SqlParameter("@BalanceExpiredId", BalanceExpiredId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_Leaves_BalanceExpired_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetLeaveExpiredBalance(ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal EmployeeId As Long) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Leaves_BalanceExpired_Select_Expired, New SqlParameter("@CompanyId", CompanyId), _
                                              New SqlParameter("@EntityId", EntityId), _
                                              New SqlParameter("@EmployeeId", EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace