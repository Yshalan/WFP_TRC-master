Imports Microsoft.VisualBasic
Imports ST.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports ST.UTILITIES
Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES

Namespace TA.DailyTasks

    Public Class DALEmp_Shifts_ModificationDate
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_Shifts_ModificationDate_Select As String = "Emp_Shifts_ModificationDate_select"
        Private Emp_Shifts_ModificationDate_Select_All As String = "Emp_Shifts_ModificationDate_select_All"
        Private Emp_Shifts_ModificationDate_Insert As String = "Emp_Shifts_ModificationDate_Insert"
        Private Emp_Shifts_ModificationDate_Update As String = "Emp_Shifts_ModificationDate_Update"
        Private Emp_Shifts_ModificationDate_Delete As String = "Emp_Shifts_ModificationDate_Delete"
        Private Emp_Shifts_ModificationDate_DateExists As String = "Emp_Shifts_ModificationDate_DateExists"
        Private Emp_Shifts_ModificationDate_Select_ActiveDates As String = "Emp_Shifts_ModificationDate_Select_ActiveDates"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef ModificationId As Long, ByVal DateOption As Integer, ByVal FromDay As Integer?, ByVal ToDay As Integer?, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@ModificationId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, ModificationId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Shifts_ModificationDate_Insert, sqlOut, New SqlParameter("@DateOption", DateOption),
               New SqlParameter("@FromDay", FromDay),
               New SqlParameter("@ToDay", ToDay),
               New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, Nothing, FromDate)),
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, Nothing, ToDate)),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If errNo = 0 Then ModificationId = sqlOut.Value Else ModificationId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal ModificationId As Long, ByVal DateOption As Integer, ByVal FromDay As Integer?, ByVal ToDay As Integer?, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Shifts_ModificationDate_Update, New SqlParameter("@ModificationId", ModificationId),
               New SqlParameter("@DateOption", DateOption),
               New SqlParameter("@FromDay", FromDay),
               New SqlParameter("@ToDay", ToDay),
               New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, Nothing, FromDate)),
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, Nothing, ToDate)),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal ModificationId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Shifts_ModificationDate_Delete, New SqlParameter("@ModificationId", ModificationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal ModificationId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_Shifts_ModificationDate_Select, New SqlParameter("@ModificationId", ModificationId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_Shifts_ModificationDate_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_DateExists() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Shifts_ModificationDate_DateExists, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetActiveDates() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Shifts_ModificationDate_Select_ActiveDates, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace