Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.DailyTasks

    Public Class DALINVALID_ATTEMPTS
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private INVALID_ATTEMPTS_Select As String = "INVALID_ATTEMPTS_select"
        Private INVALID_ATTEMPTS_Select_All As String = "INVALID_ATTEMPTS_select_All"
        Private INVALID_ATTEMPTS_Insert As String = "INVALID_ATTEMPTS_Insert"
        Private INVALID_ATTEMPTS_Update As String = "INVALID_ATTEMPTS_Update"
        Private INVALID_ATTEMPTS_Delete As String = "INVALID_ATTEMPTS_Delete"
        Private INVALID_ATTEMPTS_Select_All_Invalid As String = "INVALID_ATTEMPTS_Select_All_Invalid"
        Private INVALID_ATTEMPTS_UpdateStatus As String = "INVALID_ATTEMPTS_UpdateStatus"
        Private INVALID_ATTEMPTS_Select_ReasonName As String = "INVALID_ATTEMPTS_Select_ReasonName"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal Id As Integer, ByVal FK_EmployeeId As Integer, ByVal card_no As String, ByVal M_Date As DateTime, ByVal M_Time As DateTime, ByVal Reason As String, ByVal Reader As String, ByVal EMP_IMAGE As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(INVALID_ATTEMPTS_Insert, New SqlParameter("@Id", Id), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@card_no", card_no), _
               New SqlParameter("@M_Date", M_Date), _
               New SqlParameter("@M_Time", M_Time), _
               New SqlParameter("@Reason", Reason), _
               New SqlParameter("@Reader", Reader), _
               New SqlParameter("@EMP_IMAGE", EMP_IMAGE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal Id As Integer, ByVal FK_EmployeeId As Integer, ByVal card_no As String, ByVal M_Date As DateTime, ByVal M_Time As DateTime, ByVal Reason As String, ByVal Reader As String, ByVal EMP_IMAGE As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(INVALID_ATTEMPTS_Update, New SqlParameter("@Id", Id), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@card_no", card_no), _
               New SqlParameter("@M_Date", M_Date), _
               New SqlParameter("@M_Time", M_Time), _
               New SqlParameter("@Reason", Reason), _
               New SqlParameter("@Reader", Reader), _
               New SqlParameter("@EMP_IMAGE", EMP_IMAGE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal Id As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(INVALID_ATTEMPTS_Delete, New SqlParameter("@Id", Id))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal Id As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(INVALID_ATTEMPTS_Select, New SqlParameter("@Id", Id)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function Get_ReasonName_ByPK(ByVal Id As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(INVALID_ATTEMPTS_Select_ReasonName, New SqlParameter("@Id", Id)).Rows(0)
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
                objColl = objDac.GetDataTable(INVALID_ATTEMPTS_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_Invalid() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(INVALID_ATTEMPTS_Select_All_Invalid, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function UpdateStatus(ByVal Id As Integer, ByVal TransactionStatus As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(INVALID_ATTEMPTS_UpdateStatus, New SqlParameter("@TransactionId", Id), _
                                                      New SqlParameter("@TransactionStatus", TransactionStatus))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

#End Region


    End Class
End Namespace