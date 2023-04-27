Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.ScheduleGroups

    Public Class DALScheduleGroup_Shifts
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private ScheduleGroup_Shifts_Select As String = "ScheduleGroup_Shifts_select"
        Private ScheduleGroup_Shifts_Select_All As String = "ScheduleGroup_Shifts_select_All"
        Private ScheduleGroup_Shifts_Insert As String = "ScheduleGroup_Shifts_Insert"
        Private ScheduleGroup_Shifts_Update As String = "ScheduleGroup_Shifts_Update"
        Private ScheduleGroup_Shifts_Delete As String = "ScheduleGroup_Shifts_Delete"
        Private ScheduleGroups_Shifts_SelectByDate As String = "ScheduleGroups_Shifts_SelectByDate"
        Private ScheduleGroup_Shifts_InsertUpdateDeleteSchedule As String = "ScheduleGroup_Shifts_InsertUpdateDeleteSchedule"
        Private ScheduleGroup_Shifts_WorkDays As String = "ScheduleGroup_Shifts_WorkDays"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_GroupId As Integer, ByVal FK_ShiftId As Integer, ByVal WorkDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ScheduleGroup_Shifts_Insert, New SqlParameter("@FK_GroupId", FK_GroupId), _
               New SqlParameter("@FK_ShiftId", FK_ShiftId), _
               New SqlParameter("@WorkDate", WorkDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal GroupShiftId As Integer, ByVal FK_GroupId As Integer, ByVal FK_ShiftId As Integer, ByVal WorkDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ScheduleGroup_Shifts_Update, New SqlParameter("@GroupShiftId", GroupShiftId), _
               New SqlParameter("@FK_GroupId", FK_GroupId), _
               New SqlParameter("@FK_ShiftId", FK_ShiftId), _
               New SqlParameter("@WorkDate", WorkDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal GroupShiftId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ScheduleGroup_Shifts_Delete, New SqlParameter("@GroupShiftId", GroupShiftId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal GroupShiftId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(ScheduleGroup_Shifts_Select, New SqlParameter("@GroupShiftId", GroupShiftId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function Get_WorkDay(ByVal WorkDate As DateTime, ByVal FK_EmployeeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(ScheduleGroup_Shifts_WorkDays, New SqlParameter("@WorkDate", WorkDate), _
                                             New SqlParameter("@FK_EmployeeId", FK_EmployeeId)).Rows(0)
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
                objColl = objDac.GetDataTable(ScheduleGroup_Shifts_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetByDate(ByVal Year As Integer, ByVal Month As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(ScheduleGroups_Shifts_SelectByDate, New SqlParameter("@Year", Year), New SqlParameter("@Month", Month))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function InsertUpdateDeleteSchedule(ByVal ScheduleXml As Xml, ByRef rowCount As Integer, ByVal UserId As String) As Integer
            objDac = DAC.getDAC()
            Dim paramScheduleTable As New SqlParameter()
            paramScheduleTable.SqlDbType = SqlDbType.Xml
            paramScheduleTable.ParameterName = "SchedulesXml"
            paramScheduleTable.Value = ScheduleXml.Document.InnerXml

            Dim paramAffectedRows As New SqlParameter()
            paramAffectedRows.SqlDbType = SqlDbType.Int
            paramAffectedRows.Direction = ParameterDirection.Output
            paramAffectedRows.ParameterName = "AffectedRows"
            paramAffectedRows.Value = rowCount



            Dim paramUserId As New SqlParameter()
            paramUserId.SqlDbType = SqlDbType.VarChar
            paramUserId.ParameterName = "Userid"
            paramUserId.Value = UserId

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ScheduleGroup_Shifts_InsertUpdateDeleteSchedule, paramScheduleTable, paramAffectedRows, paramUserId)
                rowCount = paramAffectedRows.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
#End Region


    End Class
End Namespace