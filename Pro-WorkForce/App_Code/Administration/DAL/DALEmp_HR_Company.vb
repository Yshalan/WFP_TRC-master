Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Admin

    Public Class DALEmp_HR_Company
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private Emp_HR_Company_Select As String = "Emp_HR_Company_select"
        Private Emp_HR_Company_Select_All As String = "Emp_HR_Company_select_All"
        Private Emp_HR_Company_Insert As String = "Emp_HR_Company_Insert"
        Private Emp_HR_Company_Update As String = "Emp_HR_Company_Update"
        Private Emp_HR_Company_Delete As String = "Emp_HR_Company_Delete"
        Private EMP_HR_Company_SelectBy_FK_HREmployeeId As String = "EMP_HR_Company_SelectBy_FK_HREmployeeId"
        Private EMP_HR_Company_DeleteBy_FK_HREmployeeId As String = "EMP_HR_Company_DeleteBy_FK_HREmployeeId"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_HREmployeeId As Long, ByVal FK_CompanyId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_HR_Company_Insert, New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_HREmployeeId As Long, ByVal FK_CompanyId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_HR_Company_Update, New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_HREmployeeId As Long, ByVal FK_CompanyId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_HR_Company_Delete, New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function DeleteBy_FK_HREmployeeId(ByVal FK_HREmployeeId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(EMP_HR_Company_DeleteBy_FK_HREmployeeId, New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_HREmployeeId As Long, ByVal FK_CompanyId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_HR_Company_Select, New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_HR_Company_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetBy_FK_HREmployeeId(ByVal FK_HREmployeeId As Long) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(EMP_HR_Company_SelectBy_FK_HREmployeeId, _
                                              New SqlParameter("@FK_HREmployeeId", FK_HREmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
#End Region


    End Class
End Namespace