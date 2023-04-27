Imports Microsoft.VisualBasic
Imports SmartV.DB
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports TA.Lookup


Namespace TA.Employees

    Public Class DALEmp_WorkSchedule
        Inherits MGRBase

#Region "Class Variables"

        Private strConn As String
        Private Emp_WorkSchedule_Select As String = "Emp_WorkSchedule_select"
        Private Emp_WorkSchedule_Select_All As String = "Emp_WorkSchedule_select_All"
        Private Emp_WorkSchedule_Insert As String = "Emp_WorkSchedule_Insert"
        Private Emp_WorkSchedule_Update As String = "Emp_WorkSchedule_Update"
        Private Emp_WorkSchedule_Delete As String = "Emp_WorkSchedule_Delete"
        Private EmployeeGetByScheduleDate As String = "EmployeeGetByScheduleDate"
        Private Emp_GetActiveSchedule As String = "Emp_GetActiveSchedule"
        Dim Emp_WorkSchedule_Select_For_Employee_ByDateRange As String = "Emp_WorkSchedule_Select_For_Employee_ByDateRange"
        Private Emp_WorkSchedule_SelectAll_ByCompany As String = "Emp_WorkSchedule_SelectAll_ByCompany"
        Private Emp_WorkSchedule_Insert_Assign As String = "Emp_WorkSchedule_Insert_Assign"
        Private WorkSchedule_Get_Emp_Details As String = "WorkSchedule_Get_Emp_Details"
        Private Emp_WorkSchedule_SelectBy_Schedule_EmpId As String = "Emp_WorkSchedule_SelectBy_Schedule_EmpId"
        Private CALC_EFFECTIVE_SCHEDULE_WithFlix As String = "CALC_EFFECTIVE_SCHEDULE_WithFlix"
        Private Emp_WorkSchedule_Select_For_Employee As String = "Emp_WorkSchedule_Select_For_Employee"
        Private Get_ExpectedOutTime As String = "Get_ExpectedOutTime"
        Private GetScheduleGroupInfo As String = "GetScheduleGroupInfo"
        Private GetAllScheduleDeatils As String = "GetAllScheduleDeatils"
        Private WorkSchedule_Get_Emp_Details_Advanced As String = "WorkSchedule_Get_Emp_Details_Advanced"
        Private EmployeeGetByScheduleDatebyManager As String = "EmployeeGetByScheduleDatebyManager"
        Private WorkSchedule_Get_Emp_Details_Advanced_Mgr As String = "WorkSchedule_Get_Emp_Details_Advanced_Mgr"
        Private Check_IsOffDay As String = "Check_IsOffDay"
        Private Check_IsHoliday As String = "Check_IsHoliday"

#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByVal FK_EmployeeId As Long, ByVal FK_ScheduleId As Integer, ByVal ScheduleType As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsTemporary As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_WorkSchedule_Insert, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                  New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
                  New SqlParameter("@ScheduleType", ScheduleType), _
                  New SqlParameter("@FromDate", FromDate), _
                  New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
                  New SqlParameter("@IsTemporary", IsTemporary))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function AssignSchedule(ByVal FK_EmployeeId As Long, ByVal FK_ScheduleId As Integer, ByVal ScheduleType As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsTemporary As Boolean, ByVal CREATED_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_WorkSchedule_Insert_Assign, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                  New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
                  New SqlParameter("@ScheduleType", ScheduleType), _
                  New SqlParameter("@FromDate", FromDate), _
                  New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
                  New SqlParameter("@IsTemporary", IsTemporary), _
                  New SqlParameter("@CREATED_BY", CREATED_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal EmpWorkScheduleId As Long, ByVal FK_EmployeeId As Long, ByVal FK_ScheduleId As Integer, ByVal ScheduleType As Integer, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal IsTemporary As Boolean, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_WorkSchedule_Update, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                            New SqlParameter("@EmpWorkScheduleId", EmpWorkScheduleId), _
                  New SqlParameter("@FK_ScheduleId", FK_ScheduleId), _
                  New SqlParameter("@ScheduleType", ScheduleType), _
                  New SqlParameter("@FromDate", FromDate), _
                  New SqlParameter("@ToDate", IIf(ToDate = DateTime.MinValue, DBNull.Value, ToDate)), _
                  New SqlParameter("@IsTemporary", IsTemporary), _
                  New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Delete(ByVal EmpWorkScheduleId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_WorkSchedule_Delete, New SqlParameter("@EmpWorkScheduleId", EmpWorkScheduleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal EmpWorkScheduleId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_WorkSchedule_Select, New SqlParameter("@EmpWorkScheduleId", EmpWorkScheduleId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_WorkSchedule_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetByScheduleDate(ByVal ScheduleDate As DateTime, ByVal ScheduleId As Integer) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(EmployeeGetByScheduleDate, _
                 New SqlParameter("@ScheduleDate", ScheduleDate), _
                 New SqlParameter("@ScheduleId", ScheduleId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetByScheduleDateByManager(ByVal ScheduleDate As DateTime, ByVal ScheduleId As Integer, ByVal ManagerId As Integer, Optional ByVal FilterOption As Integer = 0) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(EmployeeGetByScheduleDatebyManager, _
                 New SqlParameter("@ScheduleDate", ScheduleDate), _
                 New SqlParameter("@ManagerId", ManagerId), _
                 New SqlParameter("@ScheduleId", ScheduleId), _
                 New SqlParameter("@FilterOption", FilterOption))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function
        Public Function GetAllByEmployeeandDateRange(ByVal EmployeeId As Long, ByVal StartDate As DateTime, ByVal EndDate As DateTime) As DataTable
            Dim objColl As DataTable
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(Emp_WorkSchedule_Select_For_Employee_ByDateRange, New SqlParameter("@StartDate", StartDate), _
                     New SqlParameter("@EndDate", EndDate), New SqlParameter("@FK_EmployeeId", EmployeeId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        'Emp_WorkSchedule_SelectAll_ByCompany
        Public Function GetAllByEmployeeCompany(ByVal intCompanyId As Integer) As DataTable
            Dim objColl As DataTable
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable(Emp_WorkSchedule_SelectAll_ByCompany, New SqlParameter("@CompanyId", intCompanyId))

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl
        End Function

        Public Function GetActiveSchedule(ByVal EmployeeNo As String) As DataTable
            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_GetActiveSchedule, New SqlParameter("@EmployeeNo", EmployeeNo))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_Emp_Schedule_Details(ByVal Fk_EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WorkSchedule_Get_Emp_Details, New SqlParameter("@FK_EmployeeId", Fk_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetBy_EmpId_ScheduleId(ByVal EmpWorkScheduleId As Long, ByVal FK_EmployeeId As Integer) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_WorkSchedule_Select, New SqlParameter("@EmpWorkScheduleId", EmpWorkScheduleId), _
                                             New SqlParameter("@EmployeeId", FK_EmployeeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function GetEmpScheduleWithTime(ByVal FK_EmployeeId As Integer, ByVal ScheduleDate As DateTime) As String

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Dim returnParameter As String = String.Empty
            Try
                Dim sqlParamA As New SqlParameter()
                sqlParamA.ParameterName = "@A"
                sqlParamA.Value = 0
                sqlParamA.Direction = ParameterDirection.Output

                Dim sqlParamB As New SqlParameter()
                sqlParamB.ParameterName = "@B"
                sqlParamB.Value = 0
                sqlParamB.Direction = ParameterDirection.Output

                Dim sqlParamC As New SqlParameter()
                sqlParamC.ParameterName = "@C"
                sqlParamC.Value = 0
                sqlParamC.Direction = ParameterDirection.Output

                Dim sqlParamD As New SqlParameter()
                sqlParamD.ParameterName = "@D"
                sqlParamD.Value = 0
                sqlParamD.Direction = ParameterDirection.Output

                Dim sqlParamSCH_START_TIME As New SqlParameter()
                sqlParamSCH_START_TIME.ParameterName = "@SCH_START_TIME"
                sqlParamSCH_START_TIME.Value = 0
                sqlParamSCH_START_TIME.Direction = ParameterDirection.Output

                Dim sqlParamSCH_END_TIME As New SqlParameter()
                sqlParamSCH_END_TIME.ParameterName = "@SCH_END_TIME"
                sqlParamSCH_END_TIME.SqlDbType = SqlDbType.Int
                sqlParamSCH_END_TIME.Value = 0
                sqlParamSCH_END_TIME.Direction = ParameterDirection.Output

                Dim sqlParamTOT_WORK_HRS As New SqlParameter()
                sqlParamTOT_WORK_HRS.ParameterName = "@TOT_WORK_HRS"
                sqlParamTOT_WORK_HRS.Value = 0
                sqlParamTOT_WORK_HRS.Direction = ParameterDirection.Output

                Dim sqlParamIsOffDay As New SqlParameter()
                sqlParamIsOffDay.ParameterName = "@IsOffDay"
                sqlParamIsOffDay.Value = False
                sqlParamIsOffDay.Direction = ParameterDirection.Output

                Dim sqlParamScheduleType As New SqlParameter()
                sqlParamScheduleType.ParameterName = "@ScheduleType"
                sqlParamScheduleType.Value = 0
                sqlParamScheduleType.Direction = ParameterDirection.Output

                Dim sqlParamFlixTime1 As New SqlParameter()
                sqlParamFlixTime1.ParameterName = "@FlixTime1"
                sqlParamFlixTime1.Value = 0
                sqlParamFlixTime1.Direction = ParameterDirection.Output

                Dim sqlParamFlixTime2 As New SqlParameter()
                sqlParamFlixTime2.ParameterName = "@FlixTime2"
                sqlParamFlixTime2.Value = 0
                sqlParamFlixTime2.Direction = ParameterDirection.Output

                Dim sqlParamScheduleID As New SqlParameter()
                sqlParamScheduleID.ParameterName = "@ScheduleID"
                sqlParamScheduleID.Value = 0
                sqlParamScheduleID.Direction = ParameterDirection.Output

                Dim errReturn As Integer = objDac.AddUpdateDeleteSPTrans(CALC_EFFECTIVE_SCHEDULE_WithFlix, New SqlParameter("@EMPID", IIf(FK_EmployeeId = Nothing, DBNull.Value, FK_EmployeeId)), _
                                                New SqlParameter("@MoveDATE", IIf(ScheduleDate = Nothing, DBNull.Value, ScheduleDate)), sqlParamA, sqlParamB, sqlParamC, sqlParamD, _
                                                sqlParamSCH_START_TIME, sqlParamSCH_END_TIME, sqlParamTOT_WORK_HRS, sqlParamIsOffDay, sqlParamScheduleType, sqlParamFlixTime1, _
                                                sqlParamFlixTime2, sqlParamScheduleID)

                Dim flixDuration1 As Integer
                Dim flixDuration2 As Integer
                Dim flixStartTime1 As Integer
                Dim flixStartTime2 As Integer
                Dim flixToTime1 As Integer
                Dim flixToTime2 As Integer
                Dim totalWorkHours As Integer

                Dim startTime As Integer
                Dim endTime As Integer
                Dim ScheduleType As Integer

                If Not IsDBNull(sqlParamFlixTime1.Value) Then
                    flixDuration1 = sqlParamFlixTime1.Value
                End If

                If Not IsDBNull(sqlParamFlixTime2.Value) Then
                    flixDuration2 = sqlParamFlixTime2.Value
                End If

                If Not IsDBNull(sqlParamSCH_START_TIME.Value) Then
                    startTime = sqlParamSCH_START_TIME.Value
                End If

                If Not IsDBNull(sqlParamSCH_END_TIME.Value) Then
                    endTime = sqlParamSCH_END_TIME.Value
                End If

                If Not IsDBNull(sqlParamScheduleType.Value) Then
                    ScheduleType = sqlParamScheduleType.Value
                End If

                If ScheduleType = 2 Then
                    If Not IsDBNull(sqlParamA.Value) Then
                        flixStartTime1 = sqlParamA.Value
                    End If

                    If Not IsDBNull(sqlParamB.Value) Then
                        flixToTime1 = sqlParamB.Value
                    End If

                    If Not IsDBNull(sqlParamC.Value) Then
                        flixStartTime2 = sqlParamC.Value
                    End If

                    If Not IsDBNull(sqlParamD.Value) Then
                        flixToTime2 = sqlParamD.Value
                    End If

                    If Not IsDBNull(sqlParamTOT_WORK_HRS.Value) Then
                        totalWorkHours = sqlParamTOT_WORK_HRS.Value
                    End If
                End If

                returnParameter = "," + flixDuration1.ToString() + "," + flixDuration2.ToString() + "," + startTime.ToString() + "," + endTime.ToString() + "," + ScheduleType.ToString() + "," + flixStartTime1.ToString() + _
                                  "," + flixToTime1.ToString() + "," + flixStartTime2.ToString() + "," + flixToTime2.ToString() + "," + totalWorkHours.ToString()

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return returnParameter

        End Function

        Public Function GetByEmpId(ByVal FK_EmployeeId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_WorkSchedule_Select_For_Employee, New SqlParameter("@FK_EmployeeId", FK_EmployeeId)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function Get_EmpExpectedOutTime(ByVal EmployeeId As Integer, ByVal M_Date As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objrow As DataRow
            Try
                objrow = objDac.GetDataTable(Get_ExpectedOutTime, New SqlParameter("@EmployeeId", EmployeeId), _
                                                New SqlParameter("@M_Date", M_Date)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objrow

        End Function
        Public Function GetScheduleGroup_Info(ByVal FK_EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(GetScheduleGroupInfo, New SqlParameter("@FK_EmployeeId", FK_EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAll_ScheduleDeatils(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(GetAllScheduleDeatils, New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
                                               New SqlParameter("@MoveDate", MoveDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function Get_Emp_Schedule_Details_Advanced() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WorkSchedule_Get_Emp_Details_Advanced, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
        Public Function Get_Emp_Schedule_Details_Advanced_Mgr(ByVal FK_ManagerId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(WorkSchedule_Get_Emp_Details_Advanced_Mgr, New SqlParameter("@ManagerId", FK_ManagerId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_IsOffDay(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Check_IsOffDay, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                             New SqlParameter("@M_Date", MoveDate)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

        Public Function Get_IsHoliday(ByVal FK_EmployeeId As Integer, ByVal MoveDate As DateTime) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Check_IsHoliday, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                             New SqlParameter("@M_Date", MoveDate)).Rows(0)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objRow

        End Function

#End Region

    End Class
End Namespace