Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports TA.Lookup
Imports SmartV.DB
Imports SmartV.UTILITIES

Namespace TA.Employees

    Public Class DALEmp_DeputyManager
        Inherits MGRBase

#Region "Class Variables"

        Private strConn As String
        Private Emp_DeputyManager_Insert As String = "Emp_DeputyManager_Insert"
        Private Emp_DeputyManager_Update As String = "Emp_DeputyManager_Update"
        Private Emp_DeputyManager_Delete As String = "Emp_DeputyManager_Delete"
        Private Emp_DeputyManager_Select As String = "Emp_DeputyManager_Select"
        Private Emp_DeputyManager_Select_ALL As String = "Emp_DeputyManager_Select_ALL"
        Private Emp_DeputyManager_Select_ByManagerId As String = "Emp_DeputyManager_Select_ByManagerId"
        Private Emp_Get_Employee_ByEmpNo As String = "Emp_Get_Employee_ByEmpNo"
        Private Emp_Deputy_Manager_Select_ByFKDeputymanager As String = "Emp_Deputy_Manager_Select_ByFKDeputymanager"

#End Region

#Region "Constructor"
        Public Sub New()

        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef DeuputyManagerId As Integer, ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FK_ManagerId As Long, ByVal FK_DeputyManagerId As Long, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As Integer

            objDac = DAC.getDAC()

            Try
                Dim sqlOut = New SqlParameter("@DeuputyManagerId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, DeuputyManagerId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_DeputyManager_Insert, sqlOut, _
                New SqlParameter("@FK_CompanyId", FK_CompanyId), _
                New SqlParameter("@FK_EntityId", FK_EntityId), _
                New SqlParameter("@FK_ManagerId", FK_ManagerId), _
                New SqlParameter("@FK_DeputyManagerId", FK_DeputyManagerId), _
                New SqlParameter("@FromDate", FromDate), _
                New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)))
                DeuputyManagerId = sqlOut.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal Id As Integer, ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal FK_ManagerId As Long, ByVal FK_DeputyManagerId As Long, ByVal FromDate As DateTime, ByVal ToDate As DateTime) As Integer
            objDac = DAC.getDAC()

            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_DeputyManager_Update, _
                New SqlParameter("@ID", Id), _
                New SqlParameter("@FK_CompanyId", FK_CompanyId), _
                New SqlParameter("@FK_EntityId", FK_EntityId), _
                New SqlParameter("@FK_ManagerId", FK_ManagerId), _
                New SqlParameter("@FK_DeputyManagerId", FK_DeputyManagerId), _
                New SqlParameter("@FromDate", FromDate), _
                New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal Id As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_DeputyManager_Delete, New SqlParameter("@ID", Id))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal Id As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Emp_DeputyManager_Select, New SqlParameter("@ID", Id)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_DeputyManager_Select_ALL, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetByManagerId(ByVal ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_DeputyManager_Select_ByManagerId, New SqlParameter("@ManagerId", ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetByDeputyManager(ByVal FK_DeputyManager As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Emp_Deputy_Manager_Select_ByFKDeputymanager, New SqlParameter("@FK_DeputyManager", FK_DeputyManager)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function


#End Region

    End Class

End Namespace
