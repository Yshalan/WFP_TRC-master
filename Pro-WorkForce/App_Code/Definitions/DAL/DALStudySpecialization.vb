Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES

Namespace TA.Definitions

    Public Class DALStudySpecialization
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private StudySpecialization_Select As String = "StudySpecialization_select"
        Private StudySpecialization_Select_All As String = "StudySpecialization_select_All"
        Private StudySpecialization_Insert As String = "StudySpecialization_Insert"
        Private StudySpecialization_Update As String = "StudySpecialization_Update"
        Private StudySpecialization_Delete As String = "StudySpecialization_Delete"
        Private StudySpecialization_Select_All_Inner As String = "StudySpecialization_Select_All_Inner"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef SpecializationId As Integer, ByVal FK_MajorId As Integer, ByVal SpecializationName As String, ByVal SpecializationArabicName As String, ByVal CREATED_BY As String) As Integer
            Dim sqlOut = New SqlParameter("@SpecializationId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, SpecializationId)

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(StudySpecialization_Insert, sqlOut, New SqlParameter("@FK_MajorId", FK_MajorId),
               New SqlParameter("@SpecializationName", SpecializationName),
               New SqlParameter("@SpecializationArabicName", SpecializationArabicName),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If errNo = 0 Then SpecializationId = sqlOut.Value Else SpecializationId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal SpecializationId As Integer, ByVal FK_MajorId As Integer, ByVal SpecializationName As String, ByVal SpecializationArabicName As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(StudySpecialization_Update, New SqlParameter("@SpecializationId", SpecializationId),
               New SqlParameter("@FK_MajorId", FK_MajorId),
               New SqlParameter("@SpecializationName", SpecializationName),
               New SqlParameter("@SpecializationArabicName", SpecializationArabicName),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal SpecializationId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(StudySpecialization_Delete, New SqlParameter("@SpecializationId", SpecializationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal SpecializationId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(StudySpecialization_Select, New SqlParameter("@SpecializationId", SpecializationId)).Rows(0)
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
                objColl = objDac.GetDataTable(StudySpecialization_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_Inner(ByVal FK_MajorId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(StudySpecialization_Select_All_Inner, New SqlParameter("@FK_MajorId", FK_MajorId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace