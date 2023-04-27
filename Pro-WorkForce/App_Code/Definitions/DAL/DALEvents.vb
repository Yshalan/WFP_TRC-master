Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Forms

    Public Class DALEvents
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Events_Select As String = "Events_select"
        Private Events_Select_All As String = "Events_select_All"
        Private Events_Insert As String = "Events_Insert"
        Private Events_Update As String = "Events_Update"
        Private Events_Delete As String = "Events_Delete"
        Private Events_SelectAll_Details As String = "Events_SelectAll_Details"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef EventId As Integer, ByVal EventName As String, ByVal EventDescription As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal ResposiblePerson As Long) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@EventId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, EventId)
                errNo = objDac.AddUpdateDeleteSPTrans(Events_Insert, sqlOut, New SqlParameter("@EventName", EventName),
               New SqlParameter("@EventDescription", EventDescription),
               New SqlParameter("@StartDate", StartDate),
               New SqlParameter("@EndDate", EndDate),
               New SqlParameter("@ResposiblePerson", ResposiblePerson))
                If errNo = 0 Then EventId = sqlOut.Value Else EventId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal EventId As Integer, ByVal EventName As String, ByVal EventDescription As String, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal ResposiblePerson As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Events_Update, New SqlParameter("@EventId", EventId), _
               New SqlParameter("@EventName", EventName), _
               New SqlParameter("@EventDescription", EventDescription), _
               New SqlParameter("@StartDate", StartDate), _
               New SqlParameter("@EndDate", EndDate), _
               New SqlParameter("@ResposiblePerson", ResposiblePerson))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal EventId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Events_Delete, New SqlParameter("@EventId", EventId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal EventId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Events_Select, New SqlParameter("@EventId", EventId)).Rows(0)
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
                objColl = objDac.GetDataTable(Events_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAll_Details() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Events_SelectAll_Details, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace