Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Definitions

    Public Class DALSkillCategory
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private SkillCategory_Select As String = "SkillCategory_select"
        Private SkillCategory_Select_All As String = "SkillCategory_select_All"
        Private SkillCategory_Insert As String = "SkillCategory_Insert"
        Private SkillCategory_Update As String = "SkillCategory_Update"
        Private SkillCategory_Delete As String = "SkillCategory_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef CategoryId As Integer, ByVal CategoryName As String, ByVal CategoryArabicName As String, ByVal DisplayName As String, ByVal DisplayArabicName As String, ByVal CREATED_BY As String, ByVal HasDate As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@CategoryId", SqlDbType.Int, 8, ParameterDirection.InputOutput, False, 0, 0, "", DataRowVersion.Default, 0)
                errNo = objDac.AddUpdateDeleteSPTrans(SkillCategory_Insert, sqlOut, New SqlParameter("@CategoryName", CategoryName), _
               New SqlParameter("@CategoryArabicName", CategoryArabicName), _
               New SqlParameter("@DisplayName", DisplayName), _
               New SqlParameter("@DisplayArabicName", DisplayArabicName), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@HasDate", HasDate))
                If errNo = 0 Then CategoryId = sqlOut.Value Else CategoryId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try

            Return errNo

        End Function

        Public Function Update(ByVal CategoryId As Long, ByVal CategoryName As String, ByVal CategoryArabicName As String, ByVal DisplayName As String, ByVal DisplayArabicName As String, ByVal LAST_UPDATE_BY As String, ByVal HasDate As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(SkillCategory_Update, New SqlParameter("@CategoryId", CategoryId), _
               New SqlParameter("@CategoryName", CategoryName), _
               New SqlParameter("@CategoryArabicName", CategoryArabicName), _
               New SqlParameter("@DisplayName", DisplayName), _
               New SqlParameter("@DisplayArabicName", DisplayArabicName), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@HasDate", HasDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal CategoryId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(SkillCategory_Delete, New SqlParameter("@CategoryId", CategoryId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal CategoryId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(SkillCategory_Select, New SqlParameter("@CategoryId", CategoryId)).Rows(0)
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
                objColl = objDac.GetDataTable(SkillCategory_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace