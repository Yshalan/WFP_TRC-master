Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.ScheduleGroups

    Public Class DALScheduleGroups
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private ScheduleGroups_Select As String = "ScheduleGroups_select"
        Private ScheduleGroups_Select_All As String = "ScheduleGroups_select_All"
        Private ScheduleGroups_Insert As String = "ScheduleGroups_Insert"
        Private ScheduleGroups_Update As String = "ScheduleGroups_Update"
        Private ScheduleGroups_Delete As String = "ScheduleGroups_Delete"
        Private ScheduleGroups_Select_ForFill As String = "ScheduleGroups_Select_ForFill"
        Private ScheduleGroupsGetByScheduleDate As String = "ScheduleGroupsGetByScheduleDate"
        Private ScheduleGroups_Select_AllGroupIDs As String = "ScheduleGroups_Select_AllGroupIDs"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal GroupCode As String, ByVal GroupNameEn As String, ByVal GroupNameAr As String, ByVal FK_EntityId As Integer, ByVal IsActive As Boolean, ByVal CREATED_BY As String, ByVal WorkDayNo As Integer, ByVal RestDayNo As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ScheduleGroups_Insert, New SqlParameter("@GroupCode", GroupCode), _
               New SqlParameter("@GroupNameEn", GroupNameEn), _
               New SqlParameter("@GroupNameAr", GroupNameAr), _
               New SqlParameter("@FK_EntityId", FK_EntityId), _
               New SqlParameter("@IsActive", IsActive), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@WorkDayNo", WorkDayNo), _
               New SqlParameter("@RestDayNo", RestDayNo))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal GroupId As Integer, ByVal GroupCode As String, ByVal GroupNameEn As String, ByVal GroupNameAr As String, ByVal FK_EntityId As Integer, ByVal IsActive As Boolean, ByVal LAST_UPDATE_BY As String, ByVal WorkDayNo As Integer, ByVal RestDayNo As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ScheduleGroups_Update, New SqlParameter("@GroupId", GroupId), _
               New SqlParameter("@GroupCode", GroupCode), _
               New SqlParameter("@GroupNameEn", GroupNameEn), _
               New SqlParameter("@GroupNameAr", GroupNameAr), _
               New SqlParameter("@FK_EntityId", FK_EntityId), _
               New SqlParameter("@IsActive", IsActive), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@WorkDayNo", WorkDayNo), _
               New SqlParameter("@RestDayNo", RestDayNo))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal GroupId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ScheduleGroups_Delete, New SqlParameter("@GroupId", GroupId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal GroupId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(ScheduleGroups_Select, New SqlParameter("@GroupId", GroupId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAllGroupIDs() As String

            objDac = DAC.getDAC()
            Dim objColl As String
            Try
                objColl = objDac.GetSingleValue(Of String)(ScheduleGroups_Select_AllGroupIDs)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(ScheduleGroups_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAllForFill(ByVal Lang As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(ScheduleGroups_Select_ForFill, New SqlParameter("@Lang", Lang))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetByScheduleDate(ByVal ScheduleDate As DateTime, ByVal UserId As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(ScheduleGroupsGetByScheduleDate, _
                 New SqlParameter("@ScheduleDate", ScheduleDate), New SqlParameter("@userId", UserId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
#End Region


    End Class
End Namespace