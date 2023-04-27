Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Definitions

    Public Class DALWorkSchedule
        Inherits MGRBase

#Region "Class Variables"
        Private strConn As String
        Private WorkSchedule_Select As String = "WorkSchedule_select"
        Private WorkScheduleFlexible_Select_All As String = "WorkScheduleFlexible_Select_All"
        Private WorkScheduleNormal_Select_All As String = "WorkScheduleNormal_Select_All"
        Private WorkSchedule_Insert As String = "WorkSchedule_Insert"
        Private WorkSchedule_Update As String = "WorkSchedule_Update"
        Private WorkSchedule_Delete As String = "WorkSchedule_Delete"
        Private WorkSchedule_Select_ByScheduleType As String = "WorkSchedule_Select_ByScheduleType"
        Private WorkSchedule_Select_ByMultiTypes As String = "WorkSchedule_Select_ByMultiTypes"
        Private WorkSchedule_Select_All_ForDDL As String = "WorkSchedule_Select_All_ForDDL"
        Private Emp_GetActiveSchedulebyEmpId As String = "Emp_GetActiveSchedulebyEmpId"
        Private Emp_GetActiveSchedulebyCompanyorEntity As String = "Emp_GetActiveSchedulebyCompanyorEntity"
        Private WorkSchedule_Normal_Flexible_Select_All As String = "WorkSchedule_Normal_Flexible_Select_All"
        Private CheckDefault_Schedule As String = "CheckDefault_Schedule"
        Private WorkSchedule_Update_IsDefault As String = "WorkSchedule_Update_IsDefault"
        Private WorkSchedule_Select_Default As String = "WorkSchedule_Select_Default"
        Private Emp_GetActiveSchedulebyCompanyorEntityWithNames As String = "Emp_GetActiveSchedulebyCompanyorEntityWithNames"
        Private WorkSchedule_Select_ByParentID As String = "WorkSchedule_Select_ByParentID"
        Private GET_EmpScheduleTime_Normal As String = "GET_EmpScheduleTime_Normal"
        Private GET_EmpScheduleTime_Flexible As String = "GET_EmpScheduleTime_Flexible"
        Private Emp_WorkSchedule_EmployeeSchedule As String = "Emp_WorkSchedule_EmployeeSchedule"
        Private WorkSchedule_Select_ByStudyNursing_Schedule As String = "WorkSchedule_Select_ByStudyNursing_Schedule"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal ScheduleName As String, ByVal ScheduleArabicName As String, ByVal ScheduleType As Integer, ByVal GraceIn As Integer, ByVal GraceOut As Integer, ByVal IsDefault As Boolean, ByVal CREATED_BY As String, ByRef ScheduleID As Integer, ByVal IsRamadanSch As Boolean, ByVal ParentSchId As Integer, ByVal MinimumAllowTime As Integer, ByVal GraceInGender As String, ByVal GraceOutGender As String, ByVal ConsiderShiftScheduleAtEnd As Boolean?, ByVal IsActive As Boolean?) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@ScheduleID", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, ScheduleID)
                errNo = objDac.AddUpdateDeleteSPTrans(WorkSchedule_Insert, New SqlParameter("@ScheduleName", ScheduleName), _
                  New SqlParameter("@ScheduleArabicName", ScheduleArabicName), _
                  New SqlParameter("@ScheduleType", ScheduleType), _
                  New SqlParameter("@GraceIn", GraceIn), _
                  New SqlParameter("@GraceOut", GraceOut), _
                  New SqlParameter("@IsDefault", IsDefault), _
                  New SqlParameter("@IsRamadanSch", IsRamadanSch), _
                  New SqlParameter("@ParentSchId", ParentSchId), _
                  New SqlParameter("@CREATED_BY", CREATED_BY), _
                  New SqlParameter("@MinimumAllowTime", MinimumAllowTime), _
                  New SqlParameter("@GraceInGender", GraceInGender), _
                  New SqlParameter("@GraceOutGender", GraceOutGender), _
                  New SqlParameter("@ConsiderShiftScheduleAtEnd", ConsiderShiftScheduleAtEnd), _
                  New SqlParameter("@IsActive", IsActive), sqlOut)
                ScheduleID = sqlOut.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal ScheduleId As Integer, ByVal ScheduleName As String, ByVal ScheduleArabicName As String, ByVal ScheduleType As Integer, ByVal GraceIn As Integer, ByVal GraceOut As Integer, ByVal LAST_UPDATE_BY As String, ByVal IsDefault As Boolean, ByVal IsRamadanSch As Boolean, ByVal ParentSchId As Integer, ByVal MinimumAllowTime As Integer, ByVal GraceInGender As String, ByVal GraceOutGender As String, ByVal ConsiderShiftScheduleAtEnd As Boolean?, ByVal IsActive As Boolean?) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(WorkSchedule_Update, New SqlParameter("@ScheduleId", ScheduleId), _
                    New SqlParameter("@ScheduleName", ScheduleName), _
                    New SqlParameter("@ScheduleArabicName", ScheduleArabicName), _
                    New SqlParameter("@ScheduleType", ScheduleType), _
                    New SqlParameter("@GraceIn", GraceIn), _
                    New SqlParameter("@GraceOut", GraceOut), _
                    New SqlParameter("@IsRamadanSch", IsRamadanSch), _
                    New SqlParameter("@ParentSchId", ParentSchId), _
                    New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY), _
                    New SqlParameter("@IsDefault", IsDefault), _
                    New SqlParameter("@MinimumAllowTime", MinimumAllowTime), _
                    New SqlParameter("@GraceInGender", GraceInGender), _
                    New SqlParameter("@GraceOutGender", GraceOutGender), _
                    New SqlParameter("@ConsiderShiftScheduleAtEnd", ConsiderShiftScheduleAtEnd), _
                    New SqlParameter("@IsActive", IsActive))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal ScheduleId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(WorkSchedule_Delete, New SqlParameter("@ScheduleId", ScheduleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function
        'WorkSchedule_Select_All_ForDDL

        Public Function GetAllForDDL() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WorkSchedule_Select_All_ForDDL, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByPK(ByVal ScheduleId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(WorkSchedule_Select, New SqlParameter("@ScheduleId", ScheduleId)).Rows(0)
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
                objColl = objDac.GetDataTable(WorkScheduleNormal_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllByType(ByVal ScheduleType As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WorkSchedule_Select_ByScheduleType, New SqlParameter("@ScheduleType", ScheduleType))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByType(ByVal ScheduleType As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WorkSchedule_Select_ByMultiTypes, New SqlParameter("@ScheduleType", ScheduleType))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeActiveSchedule(ByVal CompanyId As Integer, ByVal EntityId As Integer, ByVal ScheduleDate As DateTime, ByVal FilterType As String) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                If FilterType = "C" Then
                    objColl = objDac.GetDataTable(Emp_WorkSchedule_EmployeeSchedule, New SqlParameter("@FK_CompanyId", CompanyId), _
                                                  New SqlParameter("@FK_EntityId", EntityId), New SqlParameter("@Scheduledate", ScheduleDate))
                ElseIf FilterType = "W" Then
                    objColl = objDac.GetDataTable(Emp_WorkSchedule_EmployeeSchedule, New SqlParameter("@FK_CompanyId", CompanyId), _
                                               New SqlParameter("@FK_WorkLocationId", EntityId), New SqlParameter("@Scheduledate", ScheduleDate))
                ElseIf FilterType = "L" Then
                    objColl = objDac.GetDataTable(Emp_WorkSchedule_EmployeeSchedule, New SqlParameter("@FK_CompanyId", CompanyId), _
                                               New SqlParameter("@FK_LogicalGroupId", EntityId), New SqlParameter("@Scheduledate", ScheduleDate))
                Else
                    objColl = objDac.GetDataTable(Emp_WorkSchedule_EmployeeSchedule, New SqlParameter("@FK_CompanyId", CompanyId), _
                                               New SqlParameter("@FK_EntityId", EntityId), New SqlParameter("@Scheduledate", ScheduleDate))
                End If

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetEmployeeActiveScheduleByEmpId(ByVal EmployeeId As Integer, ByVal ScheduleDate As DateTime) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_WorkSchedule_EmployeeSchedule, New SqlParameter("@FK_EmployeeId", EmployeeId), _
                    New SqlParameter("@ScheduleDate", ScheduleDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetActive_SchedulebyEmpId_row(ByVal EmployeeId As Integer, ByVal ScheduleDate As DateTime) As DataRow
            objDac = DAC.getDAC()
            Dim objrow As DataRow
            Try
                objrow = objDac.GetDataTable(Emp_WorkSchedule_EmployeeSchedule, New SqlParameter("@FK_EmployeeId", EmployeeId), _
                    New SqlParameter("@ScheduleDate", ScheduleDate)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objrow

        End Function

        Public Function GetAllFlexible() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WorkScheduleFlexible_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetNormal_Flexible() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WorkSchedule_Normal_Flexible_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function CheckDefaultSchedule(ByVal ScheduleId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(CheckDefault_Schedule, New SqlParameter("@ScheduleId", ScheduleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function UpdateIsdefault(ByVal ScheduleId As Integer, ByVal IsDefault As Boolean) As Integer
            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(WorkSchedule_Update_IsDefault, New SqlParameter("@ScheduleId", ScheduleId), _
            New SqlParameter("@IsDefault", IsDefault))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo
        End Function

        Public Function GetByDefault() As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(WorkSchedule_Select_Default, Nothing).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetByParentId(ByVal ParentSchId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(WorkSchedule_Select_ByParentID, New SqlParameter("@ParentSchId", ParentSchId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function


        Public Function GetScheduleTime_Normal(ByVal ScheduleId As Integer, ByVal DayId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objrow As DataRow
            Try
                objrow = objDac.GetDataTable(GET_EmpScheduleTime_Normal, New SqlParameter("@ScheduleId", ScheduleId), _
                                                New SqlParameter("@DayId", DayId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objrow

        End Function

        Public Function GetScheduleTime_Flexible(ByVal ScheduleId As Integer, ByVal DayId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objrow As DataRow
            Try
                objrow = objDac.GetDataTable(GET_EmpScheduleTime_Flexible, New SqlParameter("@ScheduleId", ScheduleId), _
                                                New SqlParameter("@DayId", DayId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objrow

        End Function

        Public Function Get_ByStudyNursing_Schedule(ByVal StudyNursing_Schedule As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WorkSchedule_Select_ByStudyNursing_Schedule, New SqlParameter("@StudyNursing_Schedule", StudyNursing_Schedule))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

#End Region
    End Class
End Namespace