Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.DailyTasks

    Public Class DALHR_TAExceptionRequest
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private HR_TAExceptionRequest_Select As String = "HR_TAExceptionRequest_select"
        Private HR_TAExceptionRequest_Select_All As String = "HR_TAExceptionRequest_select_All"
        Private HR_TAExceptionRequest_Insert As String = "HR_TAExceptionRequest_Insert"
        Private HR_TAExceptionRequest_Update As String = "HR_TAExceptionRequest_Update"
        Private HR_TAExceptionRequest_Delete As String = "HR_TAExceptionRequest_Delete"
        Private HR_TAExceptionRequest_Select_AllInnerWithEmployee As String = "HR_TAExceptionRequest_Select_AllInnerWithEmployee"
        Private HR_TAExceptionRequest_Select_AllInnerByStatus As String = "HR_TAExceptionRequest_Select_AllInnerByStatus"
        Private Update_TAExceptionRequestStatus As String = "Update_TAExceptionRequestStatus"
        Private HR_TAExceptionRequest_Select_AllInnerRejected As String = "HR_TAExceptionRequest_Select_AllInnerRejected"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Reason As String, ByVal CREATED_BY As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(HR_TAExceptionRequest_Insert, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", IIf(ToDate = Nothing, DBNull.Value, ToDate)), _
               New SqlParameter("@Reason", Reason), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Reason As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(HR_TAExceptionRequest_Update, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", IIf(ToDate = Nothing, DBNull.Value, ToDate)), _
               New SqlParameter("@Reason", Reason), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function Update_TAException_RequestStatus(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal IsRejected As Boolean, ByVal RejectionReason As String, ByVal LAST_UPDATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Update_TAExceptionRequestStatus, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@IsRejected", IsRejected), _
               New SqlParameter("@RejectionReason", RejectionReason), _
               New SqlParameter("@LAST_UPDATED_BY", LAST_UPDATED_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function Delete(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(HR_TAExceptionRequest_Delete, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                                      New SqlParameter("@FromDate", FromDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(HR_TAExceptionRequest_Select, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                             New SqlParameter("@FromDate", FromDate)).Rows(0)
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
                objColl = objDac.GetDataTable(HR_TAExceptionRequest_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllInnerEmployee() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(HR_TAExceptionRequest_Select_AllInnerWithEmployee, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function HR_TAExceptionRequest_Select_AllInner_ByStatus(ByVal UserId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(HR_TAExceptionRequest_Select_AllInnerByStatus, New SqlParameter("@UserId", UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function HR_TAExceptionRequest_Select_AllInner_Rejected() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(HR_TAExceptionRequest_Select_AllInnerRejected, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

#End Region


    End Class
End Namespace