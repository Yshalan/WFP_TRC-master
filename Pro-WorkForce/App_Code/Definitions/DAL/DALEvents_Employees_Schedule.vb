Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports TA.Lookup
Imports SmartV.UTILITIES
Imports SmartV.DB


Namespace TA.Definitions

    Public Class DALEvents_Employees_Schedule
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private Events_Employees_Schedule_Select As String = "Events_Employees_Schedule_select"
        Private Events_Employees_Schedule_Select_All As String = "Events_Employees_Schedule_select_All"
        Private Events_Employees_Schedule_Insert As String = "Events_Employees_Schedule_Insert"
        Private Events_Employees_Schedule_Update As String = "Events_Employees_Schedule_Update"
        Private Events_Employees_Schedule_Delete As String = "Events_Employees_Schedule_Delete"
        Private Events_Employees_Schedule_Select_ByEventID As String = "Events_Employees_Schedule_Select_ByEventID"
        Private Events_Employees_Schedule_Delete_ByEventID As String = "Events_Employees_Schedule_Delete_ByEventID"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EventId As Integer, ByVal ScheduleDate As DateTime, ByVal FK_EmployeeId As Long, ByVal Shift As Char) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Events_Employees_Schedule_Insert, New SqlParameter("@FK_EventId", FK_EventId), _
               New SqlParameter("@ScheduleDate", ScheduleDate), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@Shift", Shift))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal EventScheduleId As Long, ByVal FK_EventId As Integer, ByVal ScheduleDate As DateTime, ByVal FK_EmployeeId As Long, ByVal Shift As Char) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Events_Employees_Schedule_Update, New SqlParameter("@EventScheduleId", EventScheduleId), _
               New SqlParameter("@FK_EventId", FK_EventId), _
               New SqlParameter("@ScheduleDate", ScheduleDate), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@Shift", Shift))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal EventScheduleId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Events_Employees_Schedule_Delete, New SqlParameter("@EventScheduleId", EventScheduleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal EventScheduleId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Events_Employees_Schedule_Select, New SqlParameter("@EventScheduleId", EventScheduleId)).Rows(0)
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
                objColl = objDac.GetDataTable(Events_Employees_Schedule_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllByEventID(ByVal EventID As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Events_Employees_Schedule_Select_ByEventID, New SqlParameter("@EventID", EventID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function DeleteByEventID(ByVal EventID As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Events_Employees_Schedule_Delete_ByEventID, New SqlParameter("@EventsID", EventID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

#End Region

    End Class
End Namespace