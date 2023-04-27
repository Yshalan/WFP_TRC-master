Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Employees

    Public Class DALCard_Template
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Card_Template_Select As String = "Card_Template_select"
        Private Card_Template_Select_All As String = "Card_Template_select_All"
        Private Card_Template_Insert As String = "Card_Template_Insert"
        Private Card_Template_Update As String = "Card_Template_Update"
        Private Card_Template_Delete As String = "Card_Template_Delete"
        Private Card_Template_TemplatePath_Insert As String = "Card_Template_TemplatePath_Insert"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef TemplateId As Integer, ByVal TemplateName As String, ByVal TemplateArabicName As String, ByVal TemplateFilePath As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@TemplateId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, TemplateId)
                errNo = objDac.AddUpdateDeleteSPTrans(Card_Template_Insert, sqlOut, New SqlParameter("@TemplateName", TemplateName), _
               New SqlParameter("@TemplateArabicName", TemplateArabicName), _
               New SqlParameter("@TemplateFilePath", TemplateFilePath))
                If errNo = 0 Then TemplateId = sqlOut.Value Else TemplateId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Card_TemplatePath_Insert(ByVal TemplateId As Integer, ByVal TemplateFilePath As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Card_Template_TemplatePath_Insert, New SqlParameter("@TemplateId", TemplateId), _
               New SqlParameter("@TemplateFilePath", TemplateFilePath))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal TemplateId As Integer, ByVal TemplateName As String, ByVal TemplateArabicName As String, ByVal TemplateFilePath As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Card_Template_Update, New SqlParameter("@TemplateId", TemplateId), _
               New SqlParameter("@TemplateName", TemplateName), _
               New SqlParameter("@TemplateArabicName", TemplateArabicName), _
               New SqlParameter("@TemplateFilePath", TemplateFilePath))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal TemplateId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Card_Template_Delete, New SqlParameter("@TemplateId", TemplateId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal TemplateId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Card_Template_Select, New SqlParameter("@TemplateId", TemplateId)).Rows(0)
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
                objColl = objDac.GetDataTable(Card_Template_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace