Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup

Namespace TA.Employees

    Public Class DALTAExceptions
        Inherits MGRBase

#Region "Class Variables"

        Private Emp_TAException_Insert As String = "Emp_TAException_Insert"
        Private Emp_TAExceptions_Update As String = "Emp_TAExceptions_Update"
        Private Emp_TAExceptions_Delete As String = "Emp_TAExceptions_Delete"
        Private Emp_TAExceptions_Select_All As String = "Emp_TAExceptions_Select_All"
        Private Emp_TAExceptions_Select As String = "Emp_TAExceptions_Select"
        Private Emp_TAException_Select_AllInnerWithEmployee As String = "Emp_TAException_Select_AllInnerWithEmployee"

#End Region

#Region "Constructor"
        Public Sub New()

        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Reason As String, ByVal CREATED_BY As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_TAException_Insert, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", IIf(ToDate = Nothing, DBNull.Value, ToDate)), _
               New SqlParameter("@Reason", Reason), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal Reason As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_TAExceptions_Update, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FromDate", FromDate), _
               New SqlParameter("@ToDate", IIf(ToDate = Nothing, DBNull.Value, ToDate)), _
               New SqlParameter("@Reason", Reason), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_TAExceptions_Delete, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                                      New SqlParameter("@FromDate", FromDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_TAExceptions_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetByPK(ByVal FK_EmployeeId As Integer, ByVal FromDate As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_TAExceptions_Select, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                             New SqlParameter("@FromDate", FromDate)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAllInnerEmployee() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_TAException_Select_AllInnerWithEmployee, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

#End Region

    End Class

End Namespace
