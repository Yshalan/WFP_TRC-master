Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES


Namespace TA.Lookup

    Public Class DALEmp_GradeLeavesTypes
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private Emp_GradeLeavesTypes_Select As String = "Emp_GradeLeavesTypes_select"
        Private Emp_GradeLeavesTypes_Select_All As String = "Emp_GradeLeavesTypes_select_All"
        Private Emp_GradeLeavesTypes_Insert As String = "Emp_GradeLeavesTypes_Insert"
        Private Emp_GradeLeavesTypes_Update As String = "Emp_GradeLeavesTypes_Update"
        Private Emp_GradeLeavesTypes_Delete As String = "Emp_GradeLeavesTypes_Delete"
        Private Emp_GradeLeavesTypes_SelectByFK_GradeId As String = "Emp_GradeLeavesTypes_SelectByFK_GradeId"
        Private Emp_GradeLeavesTypes_DeleteByFK_GradeId As String = "Emp_GradeLeavesTypes_DeleteByFK_GradeId"
        Private Emp_GradeLeavesTypes_SelectByFK_LeaveId As String = "Emp_GradeLeavesTypes_SelectByFK_LeaveId"
        Private Emp_GradeLeavesTypes_DeleteByFK_LeaveId As String = "Emp_GradeLeavesTypes_DeleteByFK_LeaveId"
        Private Emp_GradeLeavesTypes_SelectInnerByFK_LeaveId As String = "Emp_GradeLeavesTypes_SelectInnerByFK_LeaveId"
        Private Emp_GradeLeavesTypes_bulk_insert As String = "Emp_GradeLeavesTypes_bulk_insert"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_LeaveId As Integer, ByVal FK_GradeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try

                errNo = objDac.AddUpdateDeleteSPTrans(Emp_GradeLeavesTypes_Insert, New SqlParameter("@FK_LeaveId", FK_LeaveId), _
               New SqlParameter("@FK_GradeId", FK_GradeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal FK_LeaveId As Integer, ByVal FK_GradeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_GradeLeavesTypes_Update, New SqlParameter("@FK_LeaveId", FK_LeaveId), _
               New SqlParameter("@FK_GradeId", FK_GradeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal FK_LeaveId As Integer, ByVal FK_GradeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_GradeLeavesTypes_Delete, New SqlParameter("@FK_LeaveId", FK_LeaveId), _
               New SqlParameter("@FK_GradeId", FK_GradeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal FK_LeaveId As Integer, ByVal FK_GradeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_GradeLeavesTypes_Select, New SqlParameter("@FK_LeaveId", FK_LeaveId), _
               New SqlParameter("@FK_GradeId", FK_GradeId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_GradeLeavesTypes_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllByFk(ByVal FK_GradeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_GradeLeavesTypes_SelectByFK_GradeId, New SqlParameter("@FK_GradeId", FK_GradeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllByFK_LeaveId(ByVal FK_LeaveId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_GradeLeavesTypes_SelectByFK_LeaveId, New SqlParameter("@FK_LeaveId", FK_LeaveId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllInnerByFK_LeaveId(ByVal FK_LeaveId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_GradeLeavesTypes_SelectInnerByFK_LeaveId, New SqlParameter("@FK_LeaveId", FK_LeaveId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Add_Bulk(ByVal xml As String, ByVal LeaveId As Integer) As Integer
            '@CompanyId
            objDac = DAC.getDAC()
            Try

                errNo = objDac.AddUpdateDeleteSPTrans(Emp_GradeLeavesTypes_bulk_insert, New SqlParameter("@xml", xml), _
                                                      New SqlParameter("@FK_LeaveId", LeaveId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function DeleteByFk(ByVal FK_GradeId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_GradeLeavesTypes_DeleteByFK_GradeId, New SqlParameter("@FK_GradeId", FK_GradeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function DeleteFK_LeaveId(ByVal FK_LeaveId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_GradeLeavesTypes_DeleteByFK_LeaveId, New SqlParameter("@FK_LeaveId", FK_LeaveId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
#End Region


    End Class
End Namespace