Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.ScheduleGroups

    Public Class DALScheduleGroups_Managers
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private ScheduleGroups_Managers_Select As String = "ScheduleGroups_Managers_select"
        Private ScheduleGroups_Managers_Select_All As String = "ScheduleGroups_Managers_select_All"
        Private ScheduleGroups_Managers_Insert As String = "ScheduleGroups_Managers_Insert"
        Private ScheduleGroups_Managers_Update As String = "ScheduleGroups_Managers_Update"
        Private ScheduleGroups_Managers_Delete As String = "ScheduleGroups_Managers_Delete"
        Private ScheduleGroups_Managers_GetByGroupId As String = "ScheduleGroups_Managers_GetByGroupId"
        Private ScheduleGroups_Managers_GetGroup_ActiveManager As String = "ScheduleGroups_Managers_GetGroup_ActiveManager"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_GroupId As Integer, ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal IsTemp As Boolean, ByVal ToDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ScheduleGroups_Managers_Insert, New SqlParameter("@FK_GroupId", FK_GroupId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@IsTemp", IsTemp), _
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal GroupManagerId As Integer, ByVal FK_GroupId As Integer, ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal IsTemp As Boolean, ByVal ToDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ScheduleGroups_Managers_Update, New SqlParameter("@GroupManagerId", GroupManagerId), _
               New SqlParameter("@FK_GroupId", FK_GroupId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@IsTemp", IsTemp), _
               New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal GroupManagerId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ScheduleGroups_Managers_Delete, New SqlParameter("@GroupManagerId", GroupManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal GroupManagerId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(ScheduleGroups_Managers_Select, New SqlParameter("@GroupManagerId", GroupManagerId)).Rows(0)
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
                objColl = objDac.GetDataTable(ScheduleGroups_Managers_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByGroupId(ByVal FK_GroupId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(ScheduleGroups_Managers_GetByGroupId, New SqlParameter("@FK_GroupId", FK_GroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetGroup_ActiveManager(ByVal FK_GroupId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(ScheduleGroups_Managers_GetGroup_ActiveManager, New SqlParameter("@FK_GroupId", FK_GroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace