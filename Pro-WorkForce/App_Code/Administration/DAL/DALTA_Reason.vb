Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES


Namespace TA.Admin

    Public Class DALTA_Reason
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private TA_Reason_Select As String = "TA_Reason_select"
        Private TA_Reason_Select_All As String = "TA_Reason_select_All"
        Private TA_Reason_Insert As String = "TA_Reason_Insert"
        Private TA_Reason_Update As String = "TA_Reason_Update"
        Private TA_Reason_Delete As String = "TA_Reason_Delete"
        Private TA_Reason_Select_IsScheduleTiming As String = "TA_Reason_Select_IsScheduleTiming"
        Private TA_Reason_Select_NotIsScheduleTiming As String = "TA_Reason_Select_NotIsScheduleTiming"
        Private TA_Reason_Select_Remote As String = "TA_Reason_Select_Remote"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal ReasonCode As Integer, ByVal ReasonName As String, ByVal ReasonArabicName As String, ByVal Type As Char, ByVal IsInsideWork As Boolean, ByVal IsScheduleTiming As Boolean, ByVal IsFirstIn As Boolean, ByVal IsLastOut As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                If Type <> "I" Then
                    errNo = objDac.AddUpdateDeleteSPTrans(TA_Reason_Insert, New SqlParameter("@ReasonCode", ReasonCode),
                    New SqlParameter("@ReasonName", ReasonName),
                    New SqlParameter("@ReasonArabicName", ReasonArabicName),
                    New SqlParameter("@Type", Type),
                    New SqlParameter("@IsConsiderInside", IsInsideWork),
                    New SqlParameter("@IsScheduleTiming", IsScheduleTiming),
                    New SqlParameter("@IsFirstIn", IsFirstIn),
                    New SqlParameter("@IsLastOut", IsLastOut))
                Else
                    errNo = objDac.AddUpdateDeleteSPTrans(TA_Reason_Insert, New SqlParameter("@ReasonCode", ReasonCode),
                    New SqlParameter("@ReasonName", ReasonName),
                    New SqlParameter("@ReasonArabicName", ReasonArabicName),
                    New SqlParameter("@Type", Type),
                    New SqlParameter("@IsScheduleTiming", IsScheduleTiming),
                    New SqlParameter("@IsFirstIn", IsFirstIn),
                    New SqlParameter("@IsLastOut", IsLastOut))
                End If

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal ReasonCode As Integer, ByVal ReasonName As String, ByVal ReasonArabicName As String, ByVal Type As Char, ByVal IsInsideWork As Boolean, ByVal IsScheduleTiming As Boolean, ByVal IsFirstIn As Boolean, ByVal IsLastOut As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                If Type <> "I" Then
                    errNo = objDac.AddUpdateDeleteSPTrans(TA_Reason_Update, New SqlParameter("@ReasonCode", ReasonCode),
                    New SqlParameter("@ReasonName", ReasonName),
                    New SqlParameter("@ReasonArabicName", ReasonArabicName),
                    New SqlParameter("@Type", Type),
                    New SqlParameter("@IsConsiderInside", IsInsideWork),
                    New SqlParameter("@IsScheduleTiming", IsScheduleTiming),
                    New SqlParameter("@IsFirstIn", IsFirstIn),
                    New SqlParameter("@IsLastOut", IsLastOut))
                Else
                    errNo = objDac.AddUpdateDeleteSPTrans(TA_Reason_Update, New SqlParameter("@ReasonCode", ReasonCode),
                    New SqlParameter("@ReasonName", ReasonName),
                    New SqlParameter("@ReasonArabicName", ReasonArabicName),
                    New SqlParameter("@Type", Type),
                    New SqlParameter("@IsScheduleTiming", IsScheduleTiming),
                    New SqlParameter("@IsFirstIn", IsFirstIn),
                    New SqlParameter("@IsLastOut", IsLastOut))
                End If

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal ReasonCode As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TA_Reason_Delete, New SqlParameter("@ReasonCode", ReasonCode))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal ReasonCode As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(TA_Reason_Select, New SqlParameter("@ReasonCode", ReasonCode)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(TA_Reason_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetIsScheduleTiming() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(TA_Reason_Select_IsScheduleTiming, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetNotIsScheduleTiming() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(TA_Reason_Select_NotIsScheduleTiming, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_Remote() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(TA_Reason_Select_Remote, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region

#Region "Extended Methods"



        Public Function IS_Exist(ByVal ReasonCode As Integer) As Boolean





            Dim result As Integer = -1

            objDac = DAC.getDAC()

            Try

                Dim sqlParamReasonCode As New SqlParameter("@ReasonCode", ReasonCode)

                sqlParamReasonCode.Direction = ParameterDirection.InputOutput

                result = objDac.GetSingleValue(Of String)("TA_Reason_IS_Exist", New SqlParameter("@ReasonCode", _
                                                    ReasonCode))


            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try

            result = IIf(result = 0, False, True)

            Return result


        End Function



#End Region





    End Class
End Namespace