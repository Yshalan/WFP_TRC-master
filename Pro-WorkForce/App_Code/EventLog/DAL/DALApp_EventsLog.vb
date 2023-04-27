Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports SmartV.DB
Imports TA.Lookup


Namespace TA.Events

    Public Class DALApp_EventsLog
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private App_EventsLog_Select As String = "App_EventsLog_select"
        Private App_EventsLog_Select_All As String = "App_EventsLog_select_All"
        Private App_EventsLog_Insert As String = "App_EventsLog_Insert"
        Private App_EventsLog_Update As String = "App_EventsLog_Update"
        Private App_EventsLog_Delete As String = "App_EventsLog_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_UserId As Integer, ByVal ControlName As String, ByVal ActoinType As String, ByVal RecordId As String, ByVal RecordName As String, ByVal RecordDescription As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(App_EventsLog_Insert, _
               New SqlParameter("@FK_UserId", FK_UserId), _
               New SqlParameter("@ControlName", ControlName), _
               New SqlParameter("@ActoinType", ActoinType), _
               New SqlParameter("@RecordId", RecordId), _
               New SqlParameter("@RecordName", RecordName), _
               New SqlParameter("@RecordDescription", RecordDescription))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal EventId As Long, ByVal EventDateTime As DateTime, ByVal FK_UserId As Integer, ByVal ControlName As String, ByVal ActoinType As String, ByVal RecordId As String, ByVal RecordName As String, ByVal RecordDescription As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(App_EventsLog_Update, New SqlParameter("@EventId", EventId), _
               New SqlParameter("@EventDateTime", EventDateTime), _
               New SqlParameter("@FK_UserId", FK_UserId), _
               New SqlParameter("@ControlName", ControlName), _
               New SqlParameter("@ActoinType", ActoinType), _
               New SqlParameter("@RecordId", RecordId), _
               New SqlParameter("@RecordName", RecordName), _
               New SqlParameter("@RecordDescription", RecordDescription))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal EventId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(App_EventsLog_Delete, New SqlParameter("@EventId", EventId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal EventId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(App_EventsLog_Select, New SqlParameter("@EventId", EventId)).Rows(0)
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
                objColl = objDac.GetDataTable(App_EventsLog_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace