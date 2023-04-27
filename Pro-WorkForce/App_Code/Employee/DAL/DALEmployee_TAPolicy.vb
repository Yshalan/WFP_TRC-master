Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports SmartV.UTILITIES

Imports SmartV.DB
Imports TA.Lookup

Namespace TA.Employees

    Public Class DALEmployee_TAPolicy
        Inherits MGRBase

#Region "Class Variables"

        Private strConn As String
        Private Employee_TAPolicy_Select As String = "Employee_TAPolicy_select"
        Private Employee_TAPolicy_Select_All As String = "Employee_TAPolicy_select_All"
        Private Employee_TAPolicy_Insert As String = "Employee_TAPolicy_Insert"
        Private Employee_TAPolicy_Update As String = "Employee_TAPolicy_Update"
        Private Employee_TAPolicy_Delete As String = "Employee_TAPolicy_Delete"
        Private Emp_TAPolicy_Insert_Assign As String = "Emp_TAPolicy_Insert_Assign"

#End Region

#Region "Constructor"

        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EmployeeId As Long, ByVal FK_TAPolicyId As Integer, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_TAPolicy_Insert, _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId), _
               New SqlParameter("@StartDate", StartDate), _
               New SqlParameter("@EndDate", IIf(EndDate = DateTime.MinValue, DBNull.Value, EndDate)), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function AssignTAPolicy(ByVal FK_EmployeeId As Long, ByVal FK_TAPolicyId As Integer, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_TAPolicy_Insert_Assign, _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId), _
               New SqlParameter("@StartDate", StartDate), _
               New SqlParameter("@EndDate", IIf(EndDate = DateTime.MinValue, DBNull.Value, EndDate)), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_EmployeeId As Long, ByVal FK_TAPolicyId As Integer, ByVal StartDate As DateTime, ByVal EndDate As DateTime, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_TAPolicy_Update, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId), _
               New SqlParameter("@StartDate", StartDate), _
               New SqlParameter("@EndDate", IIf(EndDate = DateTime.MinValue, DBNull.Value, EndDate)), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_EmployeeId As Long, ByVal FK_TAPolicyId As Integer, ByVal StartDate As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Employee_TAPolicy_Delete, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId), _
               New SqlParameter("@StartDate", StartDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_EmployeeId As Long, ByVal FK_TAPolicyId As Integer, ByVal StartDate As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Employee_TAPolicy_Select, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_TAPolicyId", FK_TAPolicyId), _
               New SqlParameter("@StartDate", StartDate)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetAll() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Employee_TAPolicy_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region

#Region "Extended Class Variables"

        Private EmployeeTAPolicySelectAllInnerJoin As String = "Employee_TAPolicy_Select_AllInnerJoin"

        Private EmployeeTAPolicyGetActiveTaPolicyId As String = "Employee_TAPolicy_GetActiveTAPolicyId"

        Private EmployeeTAPolicyGetByEmployeeId As String = "Employee_TAPolicy_GetByEmployeeId"

        Private EmployeeTAPolicyGetEmployeeLastStartedPolicy As String = "Employee_TAPolicy_GetEmployeeLastStartedPolicy"

#End Region

#Region "Extended Methods"

        Public Function GetAllInnerJoin() As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(EmployeeTAPolicySelectAllInnerJoin, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetActivePolicyId(ByVal FK_EmployeeId As Integer) As DataRow
            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(EmployeeTAPolicyGetActiveTaPolicyId, _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetByEmployeeId(ByVal EmployeeId As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(EmployeeTAPolicyGetByEmployeeId, _
                                              New SqlParameter("@EmployeeId", EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeLastStartedTAPolicy(ByVal FK_EmployeeId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(EmployeeTAPolicyGetEmployeeLastStartedPolicy, _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

#End Region

    End Class
End Namespace