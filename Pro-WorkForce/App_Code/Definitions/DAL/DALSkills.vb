Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Definitions

    Public Class DALSkills
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private Skills_Select As String = "Skills_select"
        Private Skills_Select_All As String = "Skills_select_All"
        Private Skills_Insert As String = "Skills_Insert"
        Private Skills_Update As String = "Skills_Update"
        Private Skills_Delete As String = "Skills_Delete"
        Private Skills_Select_All_By_FK_CategoryId As String = "Skills_Select_All_By_FK_CategoryId"
        Private Skills_Select_By_NameEn As String = "Skills_Select_By_NameEn"
        Private Skills_Select_By_NameAr As String = "Skills_Select_By_NameAr"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef SkillId As Integer, ByVal SkillName As String, ByVal SkillArabicName As String, ByVal FK_CategoryId As Long, ByVal Desc_En As String, ByVal Desc_Ar As String, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@SkillId", SqlDbType.Int, 8, ParameterDirection.InputOutput, False, 0, 0, "", DataRowVersion.Default, 0)
                errNo = objDac.AddUpdateDeleteSPTrans(Skills_Insert, sqlOut, New SqlParameter("@SkillName", SkillName), _
               New SqlParameter("@SkillArabicName", SkillArabicName), _
               New SqlParameter("@FK_CategoryId", FK_CategoryId), _
               New SqlParameter("@Desc_En", Desc_En), _
               New SqlParameter("@Desc_Ar", Desc_Ar), _
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If errNo = 0 Then SkillId = sqlOut.Value Else SkillId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try

            Return errNo

        End Function

        Public Function Update(ByVal SkillId As Long, ByVal SkillName As String, ByVal SkillArabicName As String, ByVal FK_CategoryId As Long, ByVal Desc_En As String, ByVal Desc_Ar As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Skills_Update, New SqlParameter("@SkillId", SkillId), _
               New SqlParameter("@SkillName", SkillName), _
               New SqlParameter("@SkillArabicName", SkillArabicName), _
               New SqlParameter("@FK_CategoryId", FK_CategoryId), _
               New SqlParameter("@Desc_En", Desc_En), _
               New SqlParameter("@Desc_Ar", Desc_Ar), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal SkillId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Skills_Delete, New SqlParameter("@SkillId", SkillId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal SkillId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Skills_Select, New SqlParameter("@SkillId", SkillId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetByNameEn(ByVal SkillName As String) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Skills_Select_By_NameEn, New SqlParameter("@SkillName", SkillName)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetByNameAr(ByVal SkillArabicName As String) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Skills_Select_By_NameAr, New SqlParameter("@SkillArabicName", SkillArabicName)).Rows(0)
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
                objColl = objDac.GetDataTable(Skills_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_ByFK_CategoryId(ByVal FK_CategoryId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Skills_Select_All_By_FK_CategoryId, New SqlParameter("@FK_CategoryId", FK_CategoryId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace