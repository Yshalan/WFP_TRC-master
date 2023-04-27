Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Definitions

    Public Class DALWeekDays
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private WeekDays_Select As String = "WeekDays_select"
        Private WeekDays_Select_All As String = "WeekDays_select_All"
        Private WeekDays_Insert As String = "WeekDays_Insert"
        Private WeekDays_Update As String = "WeekDays_Update"
        Private WeekDays_Delete As String = "WeekDays_Delete"
        Private WeekDays_Flexible_Select_All As String = "WeekDays_Flexible_Select_All"
        Private WeekDays_Normal_Select_All As String = "WeekDays_Normal_Select_All"
        Private WeekDays_Select_ForDDL As String = "WeekDays_Select_ForDDL"
        Private WeekDays_Update_Order As String = "WeekDays_Update_Order"
        Private WeekDays_Select_ByDayOrder As String = "WeekDays_Select_ByDayOrder"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal DayId As Integer, ByVal DayName As String, ByVal DayArabicName As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(WeekDays_Insert, New SqlParameter("@DayId", DayId), _
               New SqlParameter("@DayName", DayName), _
               New SqlParameter("@DayArabicName", DayArabicName))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal DayId As Integer, ByVal DayName As String, ByVal DayArabicName As String, ByVal DayOrder As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(WeekDays_Update, New SqlParameter("@DayId", DayId), _
               New SqlParameter("@DayName", DayName), _
               New SqlParameter("@DayArabicName", DayArabicName), _
               New SqlParameter("@DayOrder", DayOrder))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal DayId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(WeekDays_Delete, New SqlParameter("@DayId", DayId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal DayId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(WeekDays_Select, New SqlParameter("@DayId", DayId)).Rows(0)
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
                objColl = objDac.GetDataTable(WeekDays_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Flexible_GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WeekDays_Flexible_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Normal_GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WeekDays_Normal_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetForDDL() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WeekDays_Select_ForDDL, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function UpdateDayOrder(ByVal DayId As Integer, ByVal DayOrder As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(WeekDays_Update_Order, New SqlParameter("@DayId", DayId), _
               New SqlParameter("@DayOrder", DayOrder))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByDayOrder(ByVal DayOrder As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(WeekDays_Select_ByDayOrder, New SqlParameter("@DayOrder", DayOrder)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

#End Region

    End Class
End Namespace