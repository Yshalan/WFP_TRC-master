Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA_SchoolScheduling

    Public Class DALTeacherGrades
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private TeacherGrades_Select As String = "TeacherGrades_select"
        Private TeacherGrades_Select_All As String = "TeacherGrades_select_All"
        Private TeacherGrades_Insert As String = "TeacherGrades_Insert"
        Private TeacherGrades_Update As String = "TeacherGrades_Update"
        Private TeacherGrades_Delete As String = "TeacherGrades_Delete"
        Private GetTeacherGrades_Select_AllbyEmpId As String = "TeacherGrades_Select_AllbyEmpId"
        Private TeacherGrades_GetAllClasses_ByEmployeeId As String = "TeacherGrades_GetAllClasses_ByEmployeeId"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EmployeeId As Long, ByVal FK_ClassGradeId As Integer, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_DATE As DateTime, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TeacherGrades_Insert, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_ClassGradeId", FK_ClassGradeId), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_EmployeeId As Long, ByVal FK_ClassGradeId As Integer, ByVal LAST_UPDATE_DATE As DateTime, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TeacherGrades_Update, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_ClassGradeId", FK_ClassGradeId), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_EmployeeId As Long, ByVal FK_ClassGradeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(TeacherGrades_Delete, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_ClassGradeId", FK_ClassGradeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_EmployeeId As Long, ByVal FK_ClassGradeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(TeacherGrades_Select, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_ClassGradeId", FK_ClassGradeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll_ByEmployeeId(ByVal FK_EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(TeacherGrades_GetAllClasses_ByEmployeeId, New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
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
                objColl = objDac.GetDataTable(TeacherGrades_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetTeacherGrades_Select_byEmpId(ByVal FK_EmployeeId As Long) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(GetTeacherGrades_Select_AllbyEmpId, New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
    
#End Region


    End Class
End Namespace