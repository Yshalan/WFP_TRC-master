Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA_SchoolScheduling

    Public Class DALClassGradeCourses
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private ClassGradeCourses_Select As String = "ClassGradeCourses_select"
        Private ClassGradeCourses_Select_All As String = "ClassGradeCourses_select_All"
        Private ClassGradeCourses_Insert As String = "ClassGradeCourses_Insert"
        Private ClassGradeCourses_Update As String = "ClassGradeCourses_Update"
        Private ClassGradeCourses_Delete As String = "ClassGradeCourses_Delete"
        Private ClassGradeCourses_GetByClassGradeId As String = "ClassGradeCourses_GetByClassGradeId"
        Private ClassGradeCourses_GetWeeklyNo As String = "ClassGradeCourses_GetWeeklyNo"
        Private Course_Select_All_ByClassGradeId As String = "Course_Select_All_ByClassGradeId"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"
     
        Public Function Add(ByVal FK_ClassGradeId As Integer, ByVal FK_CourseId As Integer, ByVal WeeklyCourcesNumber As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ClassGradeCourses_Insert, New SqlParameter("@FK_ClassGradeId", FK_ClassGradeId), _
               New SqlParameter("@FK_CourseId", FK_CourseId), _
               New SqlParameter("@WeeklyCourcesNumber", WeeklyCourcesNumber))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_ClassGradeId As Integer, ByVal FK_CourseId As Integer, ByVal WeeklyCourcesNumber As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ClassGradeCourses_Update, New SqlParameter("@FK_ClassGradeId", FK_ClassGradeId), _
               New SqlParameter("@FK_CourseId", FK_CourseId), _
               New SqlParameter("@WeeklyCourcesNumber", WeeklyCourcesNumber))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_ClassGradeId As Integer, ByVal FK_CourseId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(ClassGradeCourses_Delete, New SqlParameter("@FK_ClassGradeId", FK_ClassGradeId), _
               New SqlParameter("@FK_CourseId", FK_CourseId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_ClassGradeId As Integer, ByVal FK_CourseId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(ClassGradeCourses_Select, New SqlParameter("@FK_ClassGradeId", FK_ClassGradeId), _
               New SqlParameter("@FK_CourseId", FK_CourseId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAllbyFK_ClassGradeIdFK_CourseId(ByVal FK_ClassGradeId As Integer, ByVal FK_CourseId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(ClassGradeCourses_Select, New SqlParameter("@FK_ClassGradeId", FK_ClassGradeId), _
               New SqlParameter("@FK_CourseId", FK_CourseId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(ClassGradeCourses_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllbyFK_ClassGradeId(ByVal FK_ClassGradeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(ClassGradeCourses_GetByClassGradeId, New SqlParameter("@FK_ClassGradeId", FK_ClassGradeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetWeeklyNo(ByVal FK_CourseId As Integer, ByVal FK_ClassGradeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(ClassGradeCourses_GetWeeklyNo, New SqlParameter("@FK_CourseId", FK_CourseId), _
                                              New SqlParameter("@FK_ClassGradeId", FK_ClassGradeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAllCourse_ByClassGradeId(ByVal FK_ClassGradeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Course_Select_All_ByClassGradeId, New SqlParameter("@FK_ClassGradeId", FK_ClassGradeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace