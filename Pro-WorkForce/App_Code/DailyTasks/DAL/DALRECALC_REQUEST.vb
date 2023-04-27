Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.DailyTasks

    Public Class DALRECALC_REQUEST
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private RECALC_REQUEST_Select As String = "RECALC_REQUEST_select"
        Private RECALC_REQUEST_Select_All As String = "RECALC_REQUEST_select_All"
        Private RECALC_REQUEST_Insert As String = "RECALC_REQUEST_Insert"
        Private RECALC_REQUEST_Update As String = "RECALC_REQUEST_Update"
        Private RECALC_REQUEST_Delete As String = "RECALC_REQUEST_Delete"
        Private RECALC_REQUEST_Select_requst As String = "RECALC_REQUEST_Select_requst"

        Private RECALCULATE As String = "RECALCULATE"
        Private RecalculateTransactions As String = "RecalculateTransactions"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal EMP_NO As String, ByVal VALID_FROM_NUM As String, ByVal IsCalculated As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(RECALC_REQUEST_Insert, New SqlParameter("@EMP_NO", EMP_NO), _
                    New SqlParameter("@IsCalculated", IsCalculated), _
                    New SqlParameter("@VALID_FROM_NUM", VALID_FROM_NUM))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function RECALCULATE_REQ(ByVal EMP_NO As String, ByVal VALID_FROM_NUM As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(RECALCULATE, New SqlParameter("@EmployeeId", EMP_NO), _
                    New SqlParameter("@MDATE_INT", VALID_FROM_NUM))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal requestID As String, ByVal EMP_NO As String, ByVal VALID_FROM_NUM As String, ByVal IsCalculated As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(RECALC_REQUEST_Update, New SqlParameter("@requestID", requestID), _
               New SqlParameter("@EMP_NO", EMP_NO), _
               New SqlParameter("@IsCalculated", IsCalculated), _
               New SqlParameter("@VALID_FROM_NUM", VALID_FROM_NUM))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal EMP_NO As String, ByVal VALID_FROM_NUM As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(RECALC_REQUEST_Delete, New SqlParameter("@EMP_NO", EMP_NO), _
                      New SqlParameter("@VALID_FROM_NUM", VALID_FROM_NUM))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal EMP_NO As String, ByVal VALID_FROM_NUM As String) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(RECALC_REQUEST_Select_requst, New SqlParameter("@EMP_NO", EMP_NO), _
                      New SqlParameter("@VALID_FROM_NUM", VALID_FROM_NUM)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(RECALC_REQUEST_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function RecalculateTransactions_REQ(ByVal UserID As Integer, ByVal FromDate As Integer, ByVal ToDate As Integer, ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal EmployeeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                If EntityId = -1 Then
                    errNo = objDac.AddUpdateDeleteSPTrans(RecalculateTransactions, New SqlParameter("@UserId", UserID), _
                                                          New SqlParameter("@FromDate", FromDate), New SqlParameter("@ToDate", ToDate), New SqlParameter("@CompanyId", CompanyId), _
                                                          New SqlParameter("@EmployeeId", EmployeeId))
                Else
                    errNo = objDac.AddUpdateDeleteSPTrans(RecalculateTransactions, New SqlParameter("@UserId", UserID), _
                                                        New SqlParameter("@FromDate", FromDate), New SqlParameter("@ToDate", ToDate), _
                                                        New SqlParameter("@CompanyId", CompanyId), New SqlParameter("@EntityId", EntityId), _
                                                        New SqlParameter("@EmployeeId", EmployeeId))
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

#End Region

    End Class
End Namespace