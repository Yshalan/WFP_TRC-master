Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA_SchoolScheduling

    Public Class DALTeacherClasses
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private TeacherClasses_Select As String = "TeacherClasses_select"
        Private TeacherClasses_Select_All As String = "TeacherClasses_select_All"
        Private TeacherClasses_Insert As String = "TeacherClasses_Insert"
        Private TeacherClasses_Update As String = "TeacherClasses_Update"
        Private TeacherClasses_Delete As String = "TeacherClasses_Delete"
        Private TeacherClasses_GetByClassId As String = "TeacherClasses_GetByClassId"
        Private TeacherClasses_GetByEmployeeId As String = "TeacherClasses_GetByEmployeeId"
        Private TeacherClasses_GetCountByClassId As String = "TeacherClasses_GetCountByClassId"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_ClassId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_CourseId As Integer, ByVal weeklyCount As Integer, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TeacherClasses_Insert, New SqlParameter("@FK_ClassId", FK_ClassId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_CourseId", FK_CourseId), _
               New SqlParameter("@weeklyCount", weeklyCount), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_ClassId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_CourseId As Integer, ByVal weeklyCount As Integer, ByVal LAST_UPDATE_DATE As DateTime, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TeacherClasses_Update, New SqlParameter("@FK_ClassId", FK_ClassId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_CourseId", FK_CourseId), _
               New SqlParameter("@weeklyCount", weeklyCount), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_ClassId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_CourseId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TeacherClasses_Delete, New SqlParameter("@FK_ClassId", FK_ClassId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_CourseId", FK_CourseId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_ClassId As Integer, ByVal FK_EmployeeId As Long, ByVal FK_CourseId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(TeacherClasses_Select, New SqlParameter("@FK_ClassId", FK_ClassId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_CourseId", FK_CourseId)).Rows(0)
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
                objColl = objDac.GetDataTable(TeacherClasses_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetAllByClassId(ByVal FK_ClassId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(TeacherClasses_GetByClassId, New SqlParameter("@FK_ClassId", FK_ClassId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetClassWeeklyCount(ByVal FK_ClassId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(TeacherClasses_GetCountByClassId, New SqlParameter("@FK_ClassId", FK_ClassId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetAllByEmployee(ByVal FK_EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(TeacherClasses_GetByEmployeeId, New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

#End Region


    End Class
End Namespace