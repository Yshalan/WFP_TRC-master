Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports SmartV.DB
Imports TA.Lookup


Namespace TA.Admin

    Public Class DALEmailManagerReport
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private EmailManagerReport_Select As String = "EmailManagerReport_select"
        Private EmailManagerReport_Select_All As String = "EmailManagerReport_select_All"
        Private EmailManagerReport_Insert As String = "EmailManagerReport_Insert"
        Private EmailManagerReport_Update As String = "EmailManagerReport_Update"
        Private EmailManagerReport_Delete As String = "EmailManagerReport_Delete"
        Private Get_Email_Manager_Report_ByManagerId As String = "Get_Email_Manager_Report_ByManagerId"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_ManagerId As Integer, ByVal ReportType As Integer, ByVal SendDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(EmailManagerReport_Insert, New SqlParameter("@FK_ManagerId", FK_ManagerId), _
               New SqlParameter("@ReportType", ReportType), _
               New SqlParameter("@SendDate", SendDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal ID As Long, ByVal FK_ManagerId As Integer, ByVal ReportType As Integer, ByVal SendDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(EmailManagerReport_Update, New SqlParameter("@ID", ID), _
               New SqlParameter("@FK_ManagerId", FK_ManagerId), _
               New SqlParameter("@ReportType", ReportType), _
               New SqlParameter("@SendDate", SendDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal ID As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(EmailManagerReport_Delete, New SqlParameter("@ID", ID))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal ID As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(EmailManagerReport_Select, New SqlParameter("@ID", ID)).Rows(0)
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
                objColl = objDac.GetDataTable(EmailManagerReport_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetEmailManagerReportByManagerId(ByVal FK_ManagerId As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Get_Email_Manager_Report_ByManagerId, New SqlParameter("@FK_ManagerId", FK_ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

#End Region


    End Class
End Namespace