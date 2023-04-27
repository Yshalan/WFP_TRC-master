Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Card_Request

    Public Class DALCard_Request
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Card_Request_Select As String = "Card_Request_select"
        Private Card_Request_Select_All As String = "Card_Request_select_All"
        Private Card_Request_Insert As String = "Card_Request_Insert"
        Private Card_Request_Update As String = "Card_Request_Update"
        Private Card_Request_Delete As String = "Card_Request_Delete"
        Private CardRequests_Select_AllInner As String = "CardRequests_Select_AllInner"
        Private Card_Request_UpdateStatus As String = "Card_Request_UpdateStatus"
        Private Card_Request_Select_Status As String = "Card_Request_Select_Status"
        Private Card_Request_GetByFK As String = "Card_Request_GetByFK"
        Private Emp_CardRequest_Select_ByDM As String = "Emp_CardRequest_Select_ByDM"
        Private Card_Request_Status_Update As String = "Card_Request_Status_Update"
        Private Card_Request_PrintStatus_Update As String = "Card_Request_PrintStatus_Update"
        Private Emp_CardRequest_Select_ByEmployee As String = "Emp_CardRequest_Select_ByEmployee"


#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef CardRequestId As Long, ByVal FK_EmployeeId As Integer, ByVal ReasonId As Integer, ByVal OtherReason As String, ByVal Status As Integer, ByVal CardType As Integer, ByVal Remarks As String, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@CardRequestId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, CardRequestId)
                errNo = objDac.AddUpdateDeleteSPTrans(Card_Request_Insert, sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@ReasonId", ReasonId),
               New SqlParameter("@OtherReason", IIf(OtherReason = Nothing, DBNull.Value, OtherReason)),
               New SqlParameter("@Status", Status),
               New SqlParameter("@CardType", CardType),
               New SqlParameter("@Remarks", Remarks),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If errNo = 0 Then CardRequestId = sqlOut.Value Else CardRequestId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal CardRequestId As Long, ByVal FK_EmployeeId As Integer, ByVal ReasonId As Integer, ByVal OtherReason As String, ByVal Status As Integer, ByVal CardType As Integer, ByVal Remarks As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Card_Request_Update, New SqlParameter("@CardRequestId", CardRequestId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@ReasonId", ReasonId), _
               New SqlParameter("@OtherReason", IIf(OtherReason = Nothing, DBNull.Value, OtherReason)), _
               New SqlParameter("@Status", Status), _
               New SqlParameter("@CardType", CardType), _
               New SqlParameter("@Remarks", Remarks),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function UpdateStatus(ByVal CardRequestId As Long, ByVal Status As Integer, ByVal CardType As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Card_Request_Status_Update, New SqlParameter("@CardRequestId", CardRequestId), _
               New SqlParameter("@Status", Status), _
               New SqlParameter("@CardType", CardType))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function UpdatePrintStatus(ByVal Fk_EmployeeId As Long, ByVal Status As Integer, ByVal CardType As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Card_Request_PrintStatus_Update, New SqlParameter("@Fk_EmployeeId", Fk_EmployeeId), _
               New SqlParameter("@Status", Status), _
               New SqlParameter("@CardType", CardType))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function Delete(ByVal CardRequestId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Card_Request_Delete, New SqlParameter("@CardRequestId", CardRequestId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal CardRequestId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Card_Request_Select, New SqlParameter("@CardRequestId", CardRequestId)).Rows(0)
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
                objColl = objDac.GetDataTable(Card_Request_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_Status(ByVal PrintStatus As Boolean) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Card_Request_Select_Status, New SqlParameter("@PrintStatus", PrintStatus))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_Inner(ByVal Status As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(CardRequests_Select_AllInner, New SqlParameter("@Status", Status))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAll_CardRequest(ByVal ManagerID As Integer, ByVal Status As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_CardRequest_Select_ByDM,
                                              New SqlParameter("@FK_ManagerId", ManagerID),
                                              New SqlParameter("@FK_StatusId", Status))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAll_CardRequest_ByEmployee(ByVal EmployeeID As Integer, ByVal Status As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_CardRequest_Select_ByEmployee,
                                              New SqlParameter("@FK_EmployeeID", EmployeeID),
                                              New SqlParameter("@FK_StatusId", Status))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function UpdateRequestStatus(ByVal CardRequestId As Long, ByVal Status As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Card_Request_UpdateStatus, New SqlParameter("@CardRequestId", CardRequestId), _
               New SqlParameter("@Status", Status))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByFK(ByVal FK_EmployeeId As Integer, ByVal Status As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Card_Request_GetByFK, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                             New SqlParameter("@Status", Status)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

#End Region


    End Class
End Namespace