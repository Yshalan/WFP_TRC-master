Imports Microsoft.VisualBasic
Imports ST.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports ST.UTILITIES
Imports TA.LookUp
Imports SmartV.DB
Imports SmartV.UTILITIES

Namespace TA.Definitions

    Public Class DALStudyMajors
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private StudyMajors_Select As String = "StudyMajors_select"
        Private StudyMajors_Select_All As String = "StudyMajors_select_All"
        Private StudyMajors_Insert As String = "StudyMajors_Insert"
        Private StudyMajors_Update As String = "StudyMajors_Update"
        Private StudyMajors_Delete As String = "StudyMajors_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef MajorId As Integer, ByVal MajorName As String, ByVal MajorArabicName As String, ByVal CREATED_BY As String) As Integer

            Dim sqlOut = New SqlParameter("@MajorId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, MajorId)

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(StudyMajors_Insert, sqlOut, New SqlParameter("@MajorName", MajorName),
               New SqlParameter("@MajorArabicName", MajorArabicName),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If errNo = 0 Then MajorId = sqlOut.Value Else MajorId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal MajorId As Integer, ByVal MajorName As String, ByVal MajorArabicName As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(StudyMajors_Update, New SqlParameter("@MajorId", MajorId),
               New SqlParameter("@MajorName", MajorName),
               New SqlParameter("@MajorArabicName", MajorArabicName),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal MajorId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(StudyMajors_Delete, New SqlParameter("@MajorId", MajorId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal MajorId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(StudyMajors_Select, New SqlParameter("@MajorId", MajorId)).Rows(0)
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
                objColl = objDac.GetDataTable(StudyMajors_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace