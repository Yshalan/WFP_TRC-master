Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.LookUp


Namespace TA_Announcements

    Public Class DALAnnouncements
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Announcements_Select As String = "Announcements_select"
        Private Announcements_Select_All As String = "Announcements_select_All"
        Private Announcements_Insert As String = "Announcements_Insert"
        Private Announcements_Update As String = "Announcements_Update"
        Private Announcements_Delete As String = "Announcements_Delete"
        Private Announcements_Select_Top5 As String = "Announcements_Select_Top5"
        Private Announcements_Select_Top5forSelfServices As String = "Announcements_Select_Top5forSelfServices"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef ID As Integer, ByVal IsSpecificDate As Boolean, ByVal AnnouncementDate As DateTime, ByVal IsYearlyFixed As Boolean, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Title_En As String, ByVal Title_Ar As String, ByVal Fk_WorklocationId As Integer, ByVal Fk_CompanyId As Integer, ByVal Fk_EmployeeId As Long, ByVal Fk_EntityId As Integer, ByVal Fk_LogicalGroupId As Integer, ByVal Content_En As String, ByVal Content_Ar As String, ByVal Created_By As String, ByVal LanguageSelection As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@ID", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, ID)
                errNo = objDac.AddUpdateDeleteSPTrans(Announcements_Insert, sqlOut,
                            New SqlParameter("@IsSpecificDate", IsSpecificDate),
                            New SqlParameter("@AnnouncementDate", IIf(AnnouncementDate = DateTime.MinValue, DBNull.Value, AnnouncementDate)),
                            New SqlParameter("@IsYearlyFixed", IsYearlyFixed),
                            New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, Nothing, FromDate)),
                            New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, Nothing, ToDate)),
                            New SqlParameter("@Title_En", Title_En),
                            New SqlParameter("@Title_Ar", Title_Ar),
                            New SqlParameter("@Content_En", Content_En),
                            New SqlParameter("@Content_Ar", Content_Ar),
                            New SqlParameter("@Created_By", Created_By),
                            New SqlParameter("@FK_CompanyId", Fk_CompanyId),
                            New SqlParameter("@FK_EntityId", Fk_EntityId),
                            New SqlParameter("@FK_workLocationId", Fk_WorklocationId),
                            New SqlParameter("@FK_LogicalGroupId", Fk_LogicalGroupId),
                            New SqlParameter("@FK_EmployeeId", Fk_EmployeeId),
                            New SqlParameter("@LanguageSelection", LanguageSelection))
                If errNo = 0 Then ID = sqlOut.Value Else ID = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal ID As Integer, ByVal IsSpecificDate As Boolean, ByVal AnnouncementDate As DateTime, ByVal IsYearlyFixed As Boolean, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Title_En As String, ByVal Title_Ar As String, ByVal Fk_WorklocationId As Integer, ByVal Fk_CompanyId As Integer, ByVal Fk_EmployeeId As Long, ByVal Fk_EntityId As Integer, ByVal Fk_LogicalGroupId As Integer, ByVal Content_En As String, ByVal Content_Ar As String, ByVal Altered_By As String, ByVal LanguageSelection As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Announcements_Update, New SqlParameter("@ID", ID),
                            New SqlParameter("@IsSpecificDate", IsSpecificDate),
                            New SqlParameter("@AnnouncementDate", IIf(AnnouncementDate = DateTime.MinValue, DBNull.Value, AnnouncementDate)),
                            New SqlParameter("@IsYearlyFixed", IsYearlyFixed),
                            New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, Nothing, FromDate)),
                            New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, Nothing, ToDate)),
                            New SqlParameter("@Title_En", Title_En),
                            New SqlParameter("@Title_Ar", Title_Ar),
                            New SqlParameter("@Content_En", Content_En),
                            New SqlParameter("@Content_Ar", Content_Ar),
                            New SqlParameter("@Altered_By", Altered_By),
                            New SqlParameter("@FK_CompanyId", Fk_CompanyId),
                            New SqlParameter("@FK_EntityId", Fk_EntityId),
                            New SqlParameter("@FK_workLocationId", Fk_WorklocationId),
                            New SqlParameter("@FK_LogicalGroupId", Fk_LogicalGroupId),
                            New SqlParameter("@FK_EmployeeId", Fk_EmployeeId),
                            New SqlParameter("@LanguageSelection", LanguageSelection))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal ID As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Announcements_Delete, New SqlParameter("@ID", ID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal ID As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Announcements_Select, New SqlParameter("@ID", ID)).Rows(0)
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
                objColl = objDac.GetDataTable(Announcements_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetTopFive(ByVal userid As String) As DataTable

            objDac = DAC.getDAC()
            Dim objTop As DataTable
            Try
                objTop = objDac.GetDataTable(Announcements_Select_Top5, New SqlParameter("@userid", userid))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objTop


        End Function
        Public Function GetTop5AnnouncementsSelfServices(ByVal userid As String, ByVal language As String) As DataTable

            objDac = DAC.getDAC()
            Dim objTop As DataTable
            Try
                objTop = objDac.GetDataTable(Announcements_Select_Top5forSelfServices, New SqlParameter("@userid", userid), New SqlParameter("@language", language))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objTop


        End Function

#End Region


    End Class
End Namespace