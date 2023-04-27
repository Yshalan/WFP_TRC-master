Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.LookUp


Namespace TA.Employees

    Public Class DALEmployeesNumberLog
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private EmployeesNumberLog_Select As String = "EmployeesNumberLog_select"
        Private EmployeesNumberLog_Select_All As String = "EmployeesNumberLog_select_All"
        Private EmployeesNumberLog_Insert As String = "EmployeesNumberLog_Insert"
        Private EmployeesNumberLog_Update As String = "EmployeesNumberLog_Update"
        Private EmployeesNumberLog_Delete As String = "EmployeesNumberLog_Delete"
        Private EmployeesNumberLog_Select_All_Inner As String = "EmployeesNumberLog_Select_All_Inner"

#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef EmpNumberlogId As Long, ByVal FK_EmployeeId As Long, ByVal FK_CompanyId As Integer, ByVal OldEmpNo As String, ByVal NewEmpNo As String, ByVal Reason As String, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@EmpNumberlogId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, EmpNumberlogId)
                errNo = objDac.AddUpdateDeleteSPTrans(EmployeesNumberLog_Insert, sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@FK_CompanyId", FK_CompanyId),
               New SqlParameter("@OldEmpNo", OldEmpNo),
               New SqlParameter("@NewEmpNo", NewEmpNo),
               New SqlParameter("@Reason", Reason),
               New SqlParameter("@CREATED_BY", CREATED_BY))
                If errNo = 0 Then EmpNumberlogId = sqlOut.Value Else EmpNumberlogId = 0
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal EmpNumberlogId As Long, ByVal FK_EmployeeId As Long, ByVal OldEmpNo As String, ByVal NewEmpNo As String, ByVal Reason As String, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(EmployeesNumberLog_Update, New SqlParameter("@EmpNumberlogId", EmpNumberlogId),
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@OldEmpNo", OldEmpNo),
               New SqlParameter("@NewEmpNo", NewEmpNo),
               New SqlParameter("@Reason", Reason),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@CREATED_DATE", CREATED_DATE),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY),
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal EmpNumberlogId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(EmployeesNumberLog_Delete, New SqlParameter("@EmpNumberlogId", EmpNumberlogId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal EmpNumberlogId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(EmployeesNumberLog_Select, New SqlParameter("@EmpNumberlogId", EmpNumberlogId)).Rows(0)
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
                objColl = objDac.GetDataTable(EmployeesNumberLog_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_Inner(ByVal FromDate As DateTime?, ByVal ToDate As DateTime?) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(EmployeesNumberLog_Select_All_Inner, New SqlParameter("FromDate", FromDate),
                                                                                   New SqlParameter("@ToDate", ToDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region


    End Class
End Namespace