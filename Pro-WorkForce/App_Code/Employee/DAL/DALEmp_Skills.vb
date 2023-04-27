Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA.Employees
    Public Class DALEmp_Skills
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private Emp_Skills_Select As String = "Emp_Skills_select"
        Private Emp_Skills_Select_All As String = "Emp_Skills_select_All"
        Private Emp_Skills_Insert As String = "Emp_Skills_Insert"
        Private Emp_Skills_Update As String = "Emp_Skills_Update"
        Private Emp_Skills_Delete As String = "Emp_Skills_Delete"
        Private Emp_Skills_SelectBy_EmployeeId_CategoryId As String = "Emp_Skills_SelectBy_EmployeeId_CategoryId"
        Private Emp_Skills_SelectBy_FK_EmployeeId_Appraisal As String = "Emp_Skills_SelectBy_FK_EmployeeId_Appraisal"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EmployeeId As Long, ByVal FK_SkillId As Long, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Skills_Insert, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_SkillId", FK_SkillId), _
               New SqlParameter("@FromDate", IIf(FromDate = DateTime.MinValue, Nothing, FromDate)), _
               New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, Nothing, ToDate)))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_EmployeeId As Long, ByVal FK_SkillId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Skills_Update, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_SkillId", FK_SkillId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_EmployeeId As Long, ByVal FK_SkillId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Skills_Delete, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_SkillId", FK_SkillId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_EmployeeId As Long, ByVal FK_SkillId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_Skills_Select, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_SkillId", FK_SkillId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_Skills_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_By_EmployeeId_CategoryId(ByVal CategoryId As Integer, ByVal FK_EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Skills_SelectBy_EmployeeId_CategoryId, New SqlParameter("@CategoryId", CategoryId), _
                                              New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_By_FK_EmployeeId_Appraisal(ByVal FK_EmployeeId As Integer, ByVal Year As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Skills_SelectBy_FK_EmployeeId_Appraisal, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                              New SqlParameter("@Year", Year))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region

    End Class
End Namespace