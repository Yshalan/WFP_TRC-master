Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.Lookup
Imports SmartV.UTILITIES


namespace TA_AnnouncementsTemplates

Public Class DALAnnouncementTemplates
Inherits MGRBase



#Region "Class Variables"
private strConn As String
Private AnnouncementTemplates_Select As String = "AnnouncementTemplates_select" 
Private AnnouncementTemplates_Select_All As String = "AnnouncementTemplates_select_All" 
Private AnnouncementTemplates_Insert As String = "AnnouncementTemplates_Insert" 
private AnnouncementTemplates_Update As String = "AnnouncementTemplates_Update" 
private AnnouncementTemplates_Delete As String = "AnnouncementTemplates_Delete" 
#End Region
#Region "Constructor"
Public Sub New()



End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef TemplateId As Integer, ByVal announcementType As Integer, ByVal FK_HolidayId As Integer, ByVal FK_leaveType As Integer, ByVal AtLeaveStart As Boolean, ByVal TitleEn As String, ByVal TitleAr As String, ByVal TextEn As String, ByVal TextAr As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@TemplateId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, TemplateId)
                errNo = objDac.AddUpdateDeleteSPTrans(AnnouncementTemplates_Insert, sqlOut,
               New SqlParameter("@announcementType", announcementType),
               New SqlParameter("@FK_HolidayId", FK_HolidayId),
               New SqlParameter("@FK_leaveType", FK_leaveType),
               New SqlParameter("@AtLeaveStart", AtLeaveStart),
               New SqlParameter("@TitleEn", TitleEn),
               New SqlParameter("@TitleAr", TitleAr),
               New SqlParameter("@TextEn", TextEn),
               New SqlParameter("@TextAr", TextAr))
                If errNo = 0 Then
                    TemplateId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal TemplateId As Integer ,ByVal announcementType As Integer ,ByVal FK_HolidayId As Integer ,ByVal FK_leaveType As Integer ,ByVal AtLeaveStart As Boolean ,ByVal TitleEn As String ,ByVal TitleAr As String ,ByVal TextEn As String ,ByVal TextAr As String ) As Integer

objDac = DAC.getDAC()
Try
 errNo = objDac.AddUpdateDeleteSPTrans(AnnouncementTemplates_Update,New SqlParameter("@TemplateId",TemplateId) , _
New SqlParameter("@announcementType",announcementType) , _
New SqlParameter("@FK_HolidayId",FK_HolidayId) , _
New SqlParameter("@FK_leaveType",FK_leaveType) , _
New SqlParameter("@AtLeaveStart",AtLeaveStart) , _
New SqlParameter("@TitleEn",TitleEn) , _
New SqlParameter("@TitleAr",TitleAr) , _
New SqlParameter("@TextEn",TextEn) , _
New SqlParameter("@TextAr",TextAr))
Catch ex As Exception
errNo = -11
CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
End Try
 Return errNo

End Function

Public Function Delete(ByVal TemplateId As Integer ) As Integer

objDac = DAC.getDAC()
Try
 errNo = objDac.AddUpdateDeleteSPTrans(AnnouncementTemplates_Delete,New SqlParameter("@TemplateId",TemplateId))
Catch ex As Exception
errNo = -11
CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
End Try
 Return errNo

End Function

Public Function GetByPK(ByVal TemplateId As Integer ) As DataRow

objDac = DAC.getDAC()
Dim objRow As DataRow
Try
 objRow = objDac.GetDataTable(AnnouncementTemplates_Select,New SqlParameter("@TemplateId",TemplateId)).Rows(0)
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
 objColl = objDac.GetDataTable(AnnouncementTemplates_Select_All, Nothing)
Catch ex As Exception
errNo = -11
CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
End Try
 Return objColl


End Function

#End Region 


 End Class
End Namespace