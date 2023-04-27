Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Employees

    Public Class DALUserPrivileg_Companies
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private UserPrivileg_Companies_Select As String = "UserPrivileg_Companies_select"
        Private UserPrivileg_Companies_Select_All As String = "UserPrivileg_Companies_select_All"
        Private UserPrivileg_Companies_Insert As String = "UserPrivileg_Companies_Insert"
        Private UserPrivileg_Companies_Update As String = "UserPrivileg_Companies_Update"
        Private UserPrivileg_Companies_Delete As String = "UserPrivileg_Companies_Delete"
        Private UserPrivileg_GetManagerCompanies As String = "UserPrivileg_GetManagerCompanies"
        Private Get_Company_User_Privilege_by_Emp_Id As String = "Get_Company_User_Privilege_by_Emp_Id"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef Id As Integer, ByVal FK_EmployeeId As Long, ByVal FK_CompanyId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@Id", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, Id)
                errNo = objDac.AddUpdateDeleteSPTrans(UserPrivileg_Companies_Insert, sqlout, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId))
                Id = sqlout.value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal Id As Integer, ByVal FK_EmployeeId As Long, ByVal FK_CompanyId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(UserPrivileg_Companies_Update, New SqlParameter("@Id", Id), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal Id As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(UserPrivileg_Companies_Delete, New SqlParameter("@Id", Id))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal Id As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(UserPrivileg_Companies_Select, New SqlParameter("@Id", Id)).Rows(0)
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
                objColl = objDac.GetDataTable(UserPrivileg_Companies_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetManagerCompanies() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(UserPrivileg_GetManagerCompanies, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByEmpId(ByVal EmployeeId As Integer) As DataRow
            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Get_Company_User_Privilege_by_Emp_Id, New SqlParameter("@FK_EmployeeId", EmployeeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow
        End Function

#End Region

    End Class
End Namespace