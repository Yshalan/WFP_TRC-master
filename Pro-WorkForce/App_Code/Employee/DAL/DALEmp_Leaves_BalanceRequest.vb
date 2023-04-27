Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Employees

    Public Class DALEmp_Leaves_BalanceRequest
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_Leaves_BalanceRequest_Select As String = "Emp_Leaves_BalanceRequest_select"
        Private Emp_Leaves_BalanceRequest_Select_All As String = "Emp_Leaves_BalanceRequest_select_All"
        Private Emp_Leaves_BalanceRequest_Insert As String = "Emp_Leaves_BalanceRequest_Insert"
        Private Emp_Leaves_BalanceRequest_Update As String = "Emp_Leaves_BalanceRequest_Update"
        Private Emp_Leaves_BalanceRequest_Delete As String = "Emp_Leaves_BalanceRequest_Delete"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_LeaveTypeId As Integer, ByVal EffictiveDate As DateTime, ByVal CREATED_BY As String, ByVal RequestStatus As Integer, ByVal LeaveTypeNewBalance As Decimal, ByVal LeaveTypeOldBalance As Decimal) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Leaves_BalanceRequest_Insert, New SqlParameter("@FK_LeaveTypeId", FK_LeaveTypeId), _
               New SqlParameter("@EffictiveDate", EffictiveDate), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@RequestStatus", RequestStatus), _
               New SqlParameter("@LeaveTypeNewBalance", LeaveTypeNewBalance), _
               New SqlParameter("@LeaveTypeOldBalance", LeaveTypeOldBalance))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal RequestId As Integer, ByVal FK_LeaveTypeId As Integer, ByVal EffictiveDate As DateTime, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal RequestStatus As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Leaves_BalanceRequest_Update, New SqlParameter("@RequestId", RequestId), _
               New SqlParameter("@FK_LeaveTypeId", FK_LeaveTypeId), _
               New SqlParameter("@EffictiveDate", EffictiveDate), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@RequestStatus", RequestStatus))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal RequestId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Leaves_BalanceRequest_Delete, New SqlParameter("@RequestId", RequestId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal RequestId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_Leaves_BalanceRequest_Select, New SqlParameter("@RequestId", RequestId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_Leaves_BalanceRequest_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace