Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Employees

    Public Class DALUserPrivileg_Entities
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private UserPrivileg_Entities_Select As String = "UserPrivileg_Entities_select"
        Private UserPrivileg_Entities_Select_All As String = "UserPrivileg_Entities_select_All"
        Private UserPrivileg_Entities_Insert As String = "UserPrivileg_Entities_Insert"
        Private UserPrivileg_Entities_Update As String = "UserPrivileg_Entities_Update"
        Private UserPrivileg_Entities_Delete As String = "UserPrivileg_Entities_Delete"
        Private UserPrivileg_GetManagerEntity As String = "UserPrivileg_GetManagerEntity"
        Private Get_User_Privilege_Entity_by_EmpId As String = "Get_User_Privilege_Entity_by_EmpId"
#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef Id As Integer, ByVal FK_EmployeeId As Long, ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal IsCoordinator As Boolean, ByVal CoordinatorType As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@Id", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, Id)
                errNo = objDac.AddUpdateDeleteSPTrans(UserPrivileg_Entities_Insert, sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId), _
               New SqlParameter("@FK_EntityId", FK_EntityId), _
               New SqlParameter("@IsCoordinator", IsCoordinator), _
               New SqlParameter("@CoordinatorType", CoordinatorType))
                If Not IsDBNull(sqlOut.Value) Then
                    Id = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal Id As Integer, ByVal FK_EmployeeId As Long, ByVal FK_CompanyId As Integer, ByVal FK_EntityId As Integer, ByVal IsCoordinator As Boolean, ByVal CoordinatorType As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(UserPrivileg_Entities_Update, New SqlParameter("@Id", Id), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId), _
               New SqlParameter("@FK_EntityId", FK_EntityId), _
               New SqlParameter("@IsCoordinator", IsCoordinator), _
               New SqlParameter("@CoordinatorType", CoordinatorType))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal Id As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(UserPrivileg_Entities_Delete, New SqlParameter("@Id", Id))
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
                objRow = objDac.GetDataTable(UserPrivileg_Entities_Select, New SqlParameter("@Id", Id)).Rows(0)
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
                objColl = objDac.GetDataTable(UserPrivileg_Entities_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetManagerEntity() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(UserPrivileg_GetManagerEntity, Nothing)
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
                objRow = objDac.GetDataTable(Get_User_Privilege_Entity_by_EmpId, New SqlParameter("@FK_EmployeeId", EmployeeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function
#End Region


    End Class
End Namespace