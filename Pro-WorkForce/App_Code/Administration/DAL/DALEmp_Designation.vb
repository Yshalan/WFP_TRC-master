Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports SmartV.UTILITIES
Imports SmartV.DB
Imports TA.Lookup


Namespace TA.Admin

    Public Class DALEmp_Designation
        Inherits MGRBase



#Region "Class Variables"
        Private strConn As String
        Private Emp_Designation_Select As String = "Emp_Designation_select"
        Private Emp_Designation_Select_All As String = "Emp_Designation_select_All"
        Private Emp_Designation_Insert As String = "Emp_Designation_Insert"
        Private Emp_Designation_Update As String = "Emp_Designation_Update"
        Private Emp_Designation_Delete As String = "Emp_Designation_Delete"
        Private Emp_Designation_Select_AllByManager As String = "Emp_Designation_Select_AllByManager"
#End Region
#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef DesignationId As Integer, ByVal DesignationCode As String, ByVal DesignationName As String, ByVal DesignationArabicName As String, ByVal AnnualLeaveBalance As Double, ByVal FK_OvertimeRuleId As Integer, ByVal IsTAException As Boolean, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                If (FK_OvertimeRuleId <> 0) Then
                    Dim sqlOut = New SqlParameter("@DesignationId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, DesignationId)
                    errNo = objDac.AddUpdateDeleteSPTrans(Emp_Designation_Insert, sqlOut, New SqlParameter("@DesignationCode", DesignationCode), _
                   New SqlParameter("@DesignationName", DesignationName), _
                   New SqlParameter("@DesignationArabicName", DesignationArabicName), _
                   New SqlParameter("@AnnualLeaveBalance", AnnualLeaveBalance), _
                   New SqlParameter("@FK_OvertimeRuleId", FK_OvertimeRuleId), _
                   New SqlParameter("@IsTAException", IsTAException), _
                   New SqlParameter("@CREATED_BY", CREATED_BY), _
                   New SqlParameter("@CREATED_DATE", CREATED_DATE), _
                   New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
                   New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
                    If Not IsDBNull(sqlOut.Value) Then
                        DesignationId = sqlOut.Value
                    End If
                Else
                    Dim sqlOut = New SqlParameter("@DesignationId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, DesignationId)
                    errNo = objDac.AddUpdateDeleteSPTrans(Emp_Designation_Insert, sqlOut, New SqlParameter("@DesignationCode", DesignationCode), _
                   New SqlParameter("@DesignationName", DesignationName), _
                   New SqlParameter("@DesignationArabicName", DesignationArabicName), _
                   New SqlParameter("@AnnualLeaveBalance", AnnualLeaveBalance), _
                   New SqlParameter("@IsTAException", IsTAException), _
                   New SqlParameter("@CREATED_BY", CREATED_BY), _
                   New SqlParameter("@CREATED_DATE", CREATED_DATE), _
                   New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
                   New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
                    If Not IsDBNull(sqlOut.Value) Then
                        DesignationId = sqlOut.Value
                    End If
                End If

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal DesignationId As Integer, ByVal DesignationCode As String, ByVal DesignationName As String, ByVal DesignationArabicName As String, ByVal AnnualLeaveBalance As Double, ByVal FK_OvertimeRuleId As Integer, ByVal IsTAException As Boolean, ByVal CREATED_BY As String, ByVal CREATED_DATE As DateTime, ByVal LAST_UPDATE_BY As String, ByVal LAST_UPDATE_DATE As DateTime) As Integer

            objDac = DAC.getDAC()
            Try
                If FK_OvertimeRuleId <> 0 Then
                    errNo = objDac.AddUpdateDeleteSPTrans(Emp_Designation_Update, New SqlParameter("@DesignationId", DesignationId), _
               New SqlParameter("@DesignationCode", DesignationCode), _
               New SqlParameter("@DesignationName", DesignationName), _
               New SqlParameter("@DesignationArabicName", DesignationArabicName), _
               New SqlParameter("@AnnualLeaveBalance", AnnualLeaveBalance), _
               New SqlParameter("@FK_OvertimeRuleId", FK_OvertimeRuleId), _
               New SqlParameter("@IsTAException", IsTAException), _
               New SqlParameter("@CREATED_BY", CREATED_BY), _
               New SqlParameter("@CREATED_DATE", CREATED_DATE), _
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
               New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
                Else
                    errNo = objDac.AddUpdateDeleteSPTrans(Emp_Designation_Update, New SqlParameter("@DesignationId", DesignationId), _
              New SqlParameter("@DesignationCode", DesignationCode), _
              New SqlParameter("@DesignationName", DesignationName), _
              New SqlParameter("@DesignationArabicName", DesignationArabicName), _
              New SqlParameter("@AnnualLeaveBalance", AnnualLeaveBalance), _
              New SqlParameter("@IsTAException", IsTAException), _
              New SqlParameter("@CREATED_BY", CREATED_BY), _
              New SqlParameter("@CREATED_DATE", CREATED_DATE), _
              New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
              New SqlParameter("@LAST_UPDATE_DATE", LAST_UPDATE_DATE))
                End If

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal DesignationId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Designation_Delete, New SqlParameter("@DesignationId", DesignationId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal DesignationId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow = Nothing
            Try
                objRow = objDac.GetDataTable(Emp_Designation_Select, New SqlParameter("@DesignationId", DesignationId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_Designation_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllDesignationByManger(ByVal Fk_ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Designation_Select_AllByManager, New SqlParameter("@Fk_ManagerId", Fk_ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


    End Class
End Namespace