Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports SmartV.DB
Imports TA.Lookup


Namespace TA.Admin

    Public Class DALApp_EmailConfigurations
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private App_EmailConfigurations_Select As String = "App_EmailConfigurations_select"
        Private App_EmailConfigurations_Select_All As String = "App_EmailConfigurations_select_All"
        Private App_EmailConfigurations_Insert As String = "App_EmailConfigurations_Insert"
        Private App_EmailConfigurations_Update As String = "App_EmailConfigurations_Update"
        Private App_EmailConfigurations_Delete As String = "App_EmailConfigurations_Delete"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal SMTP_Server As String, ByVal EmailFrom As String, ByVal EnableEmailService As Boolean, ByVal EnableSMSService As Boolean, ByVal SMTPUserName As String, ByVal SMTPPassword As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(App_EmailConfigurations_Insert, New SqlParameter("@SMTP_Server", SMTP_Server), _
               New SqlParameter("@EmailFrom", EmailFrom), _
               New SqlParameter("@EnableEmailService", EnableEmailService), _
               New SqlParameter("@SMTPUserName", SMTPUserName), _
               New SqlParameter("@SMTPPassword", SMTPPassword), _
               New SqlParameter("@EnableSMSService", EnableSMSService))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal SMTP_Server As String, ByVal EmailFrom As String, ByVal EnableEmailService As Boolean, ByVal EnableSMSService As Boolean, ByVal SMTPUserName As String, ByVal SMTPPassword As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(App_EmailConfigurations_Update, New SqlParameter("@SMTP_Server", SMTP_Server), _
               New SqlParameter("@EmailFrom", EmailFrom), _
               New SqlParameter("@EnableEmailService", EnableEmailService), _
               New SqlParameter("@SMTPUserName", SMTPUserName), _
               New SqlParameter("@SMTPPassword", SMTPPassword), _
               New SqlParameter("@EnableSMSService", EnableSMSService))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal SMTP_Server As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(App_EmailConfigurations_Delete, New SqlParameter("@SMTP_Server", SMTP_Server))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK() As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(App_EmailConfigurations_Select, New SqlParameter("@SMTP_Server", DBNull.Value)).Rows(0)
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
                objColl = objDac.GetDataTable(App_EmailConfigurations_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region

    End Class
End Namespace