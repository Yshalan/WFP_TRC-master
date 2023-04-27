Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Employees

    Public Class DALUserPrivileg_WorkLocations
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private UserPrivileg_WorkLocations_Select As String = "UserPrivileg_WorkLocations_select"
        Private UserPrivileg_WorkLocations_Select_All As String = "UserPrivileg_WorkLocations_select_All"
        Private UserPrivileg_WorkLocations_Insert As String = "UserPrivileg_WorkLocations_Insert"
        Private UserPrivileg_WorkLocations_Update As String = "UserPrivileg_WorkLocations_Update"
        Private UserPrivileg_WorkLocations_Delete As String = "UserPrivileg_WorkLocations_Delete"
        Private UserPrivileg_GetManagerWorkLocation As String = "UserPrivileg_GetManagerWorkLocation"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef Id As Integer, ByVal FK_EmployeeId As Long, ByVal FK_CompanyId As Integer, ByVal FK_WorkLocationId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@Id", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, Id)
                errNo = objDac.AddUpdateDeleteSPTrans(UserPrivileg_WorkLocations_Insert, sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId), _
               New SqlParameter("@FK_WorkLocationId", FK_WorkLocationId))
                Id = sqlOut.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal Id As Integer, ByVal FK_EmployeeId As Long, ByVal FK_CompanyId As Integer, ByVal FK_WorkLocationId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(UserPrivileg_WorkLocations_Update, New SqlParameter("@Id", Id), _
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
               New SqlParameter("@FK_CompanyId", FK_CompanyId), _
               New SqlParameter("@FK_WorkLocationId", FK_WorkLocationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal Id As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(UserPrivileg_WorkLocations_Delete, New SqlParameter("@Id", Id))
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
                objRow = objDac.GetDataTable(UserPrivileg_WorkLocations_Select, New SqlParameter("@Id", Id)).Rows(0)
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
                objColl = objDac.GetDataTable(UserPrivileg_WorkLocations_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function GetManagerWorkLocation() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(UserPrivileg_GetManagerWorkLocation, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace