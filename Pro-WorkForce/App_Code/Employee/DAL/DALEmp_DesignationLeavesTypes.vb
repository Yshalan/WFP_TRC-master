Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES


Namespace TA.Lookup

    Public Class DALEmp_DesignationLeavesTypes
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_DesignationLeavesTypes_Select As String = "Emp_DesignationLeavesTypes_select"
        Private Emp_DesignationLeavesTypes_Select_All As String = "Emp_DesignationLeavesTypes_select_All"
        Private Emp_DesignationLeavesTypes_Insert As String = "Emp_DesignationLeavesTypes_Insert"
        Private Emp_DesignationLeavesTypes_Update As String = "Emp_DesignationLeavesTypes_Update"
        Private Emp_DesignationLeavesTypes_Delete As String = "Emp_DesignationLeavesTypes_Delete"
        Private Emp_DesignationLeavesTypes_DeleteByDesignationId As String = "Emp_DesignationLeavesTypes_DeleteByDesignationId"
        Private Emp_DesignationLeavesTypes_SelectByDesignationId As String = "Emp_DesignationLeavesTypes_SelectByDesignationId"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_LeaveId As Integer, ByVal FK_DesignationId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_DesignationLeavesTypes_Insert, New SqlParameter("@FK_LeaveId", FK_LeaveId), _
               New SqlParameter("@FK_DesignationId", FK_DesignationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_LeaveId As Integer, ByVal FK_DesignationId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_DesignationLeavesTypes_Update, New SqlParameter("@FK_LeaveId", FK_LeaveId), _
               New SqlParameter("@FK_DesignationId", FK_DesignationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_LeaveId As Integer, ByVal FK_DesignationId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_DesignationLeavesTypes_Delete, New SqlParameter("@FK_LeaveId", FK_LeaveId), _
               New SqlParameter("@FK_DesignationId", FK_DesignationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_LeaveId As Integer, ByVal FK_DesignationId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_DesignationLeavesTypes_Select, New SqlParameter("@FK_LeaveId", FK_LeaveId), _
               New SqlParameter("@FK_DesignationId", FK_DesignationId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_DesignationLeavesTypes_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function DeleteByFkDesignation(ByVal FK_DesignationId As Integer) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_DesignationLeavesTypes_DeleteByDesignationId, New SqlParameter("@FK_DesignationId", FK_DesignationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        Public Function SelectByFk(ByVal FK_DesignationId As Integer) As DataTable
            objDac = DAC.getDAC
            Dim objCol As DataTable
            Try
                objCol = objDac.GetDataTable(Emp_DesignationLeavesTypes_SelectByDesignationId, New SqlParameter("@FK_DesignationId", FK_DesignationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objCol
        End Function
#End Region


    End Class
End Namespace