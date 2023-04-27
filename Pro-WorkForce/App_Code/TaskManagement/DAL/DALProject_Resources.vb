Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.TaskManagement

    Public Class DALProject_Resources
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Project_Resources_Select As String = "Project_Resources_select"
        Private Project_Resources_Select_All As String = "Project_Resources_select_All"
        Private Project_Resources_Insert As String = "Project_Resources_Insert"
        Private Project_Resources_Update As String = "Project_Resources_Update"
        Private Project_Resources_Delete As String = "Project_Resources_Delete"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_ProjectId As Long, ByVal FK_EmployeeId As Long, ByVal FK_DesignationId As Integer, ByVal IsPM As Boolean, ByVal RoleId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Project_Resources_Insert, New SqlParameter("@FK_ProjectId", FK_ProjectId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_DesignationId", FK_DesignationId), _
               New SqlParameter("@IsPM", IsPM), _
               New SqlParameter("@RoleId", RoleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_ProjectId As Long, ByVal FK_EmployeeId As Long, ByVal FK_DesignationId As Integer, ByVal IsPM As Boolean, ByVal RoleId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Project_Resources_Update, New SqlParameter("@FK_ProjectId", FK_ProjectId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_DesignationId", FK_DesignationId), _
               New SqlParameter("@IsPM", IsPM), _
               New SqlParameter("@RoleId", RoleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_ProjectId As Long, ByVal FK_EmployeeId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Project_Resources_Delete, New SqlParameter("@FK_ProjectId", FK_ProjectId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_ProjectId As Long, ByVal FK_EmployeeId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Project_Resources_Select, New SqlParameter("@FK_ProjectId", FK_ProjectId), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId)).Rows(0)
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
                objColl = objDac.GetDataTable(Project_Resources_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace