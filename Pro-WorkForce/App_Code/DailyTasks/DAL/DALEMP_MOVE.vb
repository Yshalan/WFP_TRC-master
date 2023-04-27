Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports SmartV.UTILITIES
Imports SmartV.DB
Imports TA.Lookup


Namespace TA.DailyTasks

    Public Class DALEmp_Move
        Inherits MGRBase



#Region "Class Variables"

        Private strConn As String
        Private Emp_Move_Select As String = "Emp_Move_select"
        Private Emp_Move_Select_All As String = "Emp_Move_select_All"
        Private Emp_Move_Insert As String = "Emp_Move_Insert"
        Private Emp_Move_Update As String = "Emp_Move_Update"
        Private Emp_Move_Delete As String = "Emp_Move_Delete"
        Private EMP_MOVE_Select_Filter As String = "EMP_MOVE_Select_Filter"
        Private Emp_Move_Select_All_RealTime As String = "Emp_Move_Select_All_RealTime"
        Private GetDailyTransEmployees As String = "GetDailyTransEmployees"
        Private Emp_Move_Select_ByEmp_DateDiff As String = "Emp_Move_Select_ByEmp_DateDiff"
        Private Emp_Move_UpdateTransaction_Type As String = "Emp_Move_UpdateTransaction_Type"
        Private Emp_Move_Insert_Reader_Simulation As String = "Emp_Move_Insert_Reader_Simulation"
        Private EMP_MOVE_Select_Filter_ByUser As String = "EMP_MOVE_Select_Filter_ByUser"
        Private Emp_Move_SelectAttend_Absent As String = "Emp_Move_SelectAttend_Absent"


#End Region

#Region "Constructor"
        Public Sub New()



        End Sub

#End Region

#Region "Methods"

        Public Function Add(ByRef MoveId As Integer, ByVal FK_EmployeeId As Long, ByVal Type As String, ByVal MoveDate As DateTime, ByVal MoveTime As DateTime, ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal Reader As String, ByVal Status As String, ByVal IsManual As Boolean, ByVal CREATED_BY As String, ByVal IsFromInvalid As Boolean, ByVal AttachedFile As String, ByVal IsRemoteWork As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@MoveId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, MoveId)
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Move_Insert, sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@Type", Type),
               New SqlParameter("@MoveDate", MoveDate),
               New SqlParameter("@MoveTime", MoveTime),
               New SqlParameter("@FK_ReasonId", FK_ReasonId),
               New SqlParameter("@REMARKS", Remarks),
               New SqlParameter("@Reader", Reader),
               New SqlParameter("@Status", Status),
               New SqlParameter("@IsManual", IsManual),
               New SqlParameter("@CREATED_BY", CREATED_BY),
               New SqlParameter("@IsFromInvalid", IsFromInvalid),
               New SqlParameter("@AttachedFile", AttachedFile),
               New SqlParameter("@IsRemoteWork", IsRemoteWork))
                If Not IsDBNull(sqlOut.Value) Then
                    MoveId = sqlOut.Value
                End If

            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Update(ByVal MoveId As Integer, ByVal FK_EmployeeId As Long, ByVal Type As String, ByVal MoveDate As DateTime, ByVal MoveTime As DateTime, ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal Reader As String, ByVal Status As String, ByVal IsManual As Boolean, ByVal LAST_UPDATE_BY As String, ByVal AttachedFile As String, ByVal IsRemoteWork As Boolean) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Move_Update, New SqlParameter("@MoveId", MoveId),
               New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
               New SqlParameter("@Type", Type),
               New SqlParameter("@MoveDate", MoveDate),
               New SqlParameter("@MoveTime", MoveTime),
               New SqlParameter("@FK_ReasonId", FK_ReasonId),
               New SqlParameter("@REMARKS", Remarks),
               New SqlParameter("@Reader", Reader),
               New SqlParameter("@Status", Status),
               New SqlParameter("@IsManual", IsManual),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY),
               New SqlParameter("@AttachedFile", AttachedFile),
               New SqlParameter("@IsRemoteWork", IsRemoteWork))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetFilter(ByVal EMP_NO As Integer, ByVal M_DATE As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(EMP_MOVE_Select_Filter, New SqlParameter("@EmployeeId", EMP_NO),
               New SqlParameter("@MoveDate", M_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function GetFilter_ByUser(ByVal Id As Integer, ByVal M_DATE As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(EMP_MOVE_Select_Filter_ByUser, New SqlParameter("@Id", Id),
               New SqlParameter("@MoveDate", M_DATE))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.ReflectedType.FullName & "." & MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl

        End Function

        Public Function Delete(ByVal MoveId As Long) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Move_Delete, New SqlParameter("@MoveId", MoveId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetByPK(ByVal MoveId As Long) As DataRow

            objDac = DAC.getDAC()
            Dim objRow As DataRow
            Try
                objRow = objDac.GetDataTable(Emp_Move_Select, New SqlParameter("@MoveId", MoveId)).Rows(0)
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
                objColl = objDac.GetDataTable(Emp_Move_Select_All, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_Attend_Absent(ByVal FK_EmployeeId As Integer, ByVal M_DATE_NUM As Integer, ByVal FK_EntityId As Integer, ByVal FK_CompanyId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Move_SelectAttend_Absent, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                                                                            New SqlParameter("@M_DATE_NUM", M_DATE_NUM),
                                                                            New SqlParameter("@FK_EntityId", FK_EntityId),
                                                                            New SqlParameter("@FK_CompanyId", FK_CompanyId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetAllForRealTime() As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Move_Select_All_RealTime, Nothing)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetDailyTrans(ByVal EmployeeId As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(GetDailyTransEmployees, New SqlParameter("@EmployeeId", EmployeeId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Get_ByEmp_DateDiff(ByVal FK_EmployeeId As Integer, ByVal ManualEntryAllowedBefore As String) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable
            Try
                objColl = objDac.GetDataTable(Emp_Move_Select_ByEmp_DateDiff, New SqlParameter("@EmployeeId", FK_EmployeeId),
                                              New SqlParameter("@ManualEntryAllowedBefore", ManualEntryAllowedBefore))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function Update_TransactionType(ByVal MoveId As Integer, ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal LAST_UPDATE_BY As String) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Move_UpdateTransaction_Type, New SqlParameter("@MoveId", MoveId),
               New SqlParameter("@FK_ReasonId", FK_ReasonId),
               New SqlParameter("@REMARKS", Remarks),
               New SqlParameter("@LAST_UPDATE_BY", LAST_UPDATE_BY))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function Add_TransactionBySimulation(ByVal FK_EmployeeId As Long, ByVal FK_ReasonId As Integer) As Integer

            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Move_Insert_Reader_Simulation, New SqlParameter("@EMPID", FK_EmployeeId),
               New SqlParameter("@V_REASON", FK_ReasonId))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function AddMoveFromMobile(ByRef MoveId As Integer, FK_EmployeeId As Long, ByVal Type As String, ByVal MoveDate As DateTime, ByVal MoveTime As DateTime, ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal Reader As String, ByVal M_DATE_NUM As String, ByVal M_TIME_NUM As String, ByVal Status As String, ByVal IsManual As Boolean, ByVal CREATED_BY As String, ByVal IsFromMobile As String, ByVal GPSCoordinates As String, ByVal WorkLocationId As Integer, ByVal DeviceId As String) As Integer
            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@MoveId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, MoveId)
                errNo = objDac.AddUpdateDeleteSPTrans("Emp_Move_Insert_From_Mobile", sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                New SqlParameter("@Type", Type),
                New SqlParameter("@MoveDate", MoveDate),
                New SqlParameter("@MoveTime", MoveTime),
                New SqlParameter("@FK_ReasonId", FK_ReasonId),
                New SqlParameter("@Remarks", Remarks),
                New SqlParameter("@Reader", Reader),
                New SqlParameter("@M_DATE_NUM", M_DATE_NUM),
                New SqlParameter("@M_TIME_NUM", M_TIME_NUM),
                New SqlParameter("@Status", Status),
                New SqlParameter("@IsManual", IsManual),
                New SqlParameter("@IsFromMobile", IsFromMobile),
                New SqlParameter("@MobileCoordinates", GPSCoordinates),
                New SqlParameter("@CREATED_BY", CREATED_BY),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@DeviceId", DeviceId))
                If errNo = 0 Then
                    MoveId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function AddMoveFromMobile_Mobile_Beacon(ByRef MoveId As Integer, FK_EmployeeId As Long, ByVal Type As String, ByVal MoveDate As DateTime, ByVal MoveTime As DateTime, ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal Reader As String, ByVal M_DATE_NUM As String, ByVal M_TIME_NUM As String, ByVal Status As String, ByVal IsManual As Boolean, ByVal CREATED_BY As String, ByVal IsFromMobile As String, ByVal BeaconId As Integer, ByVal WorkLocationId As Integer, ByVal DeviceId As String) As Integer
            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@MoveId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, MoveId)
                errNo = objDac.AddUpdateDeleteSPTrans("Emp_Move_Insert_From_Mobile_Beacon", sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                New SqlParameter("@Type", Type),
                New SqlParameter("@MoveDate", MoveDate),
                New SqlParameter("@MoveTime", MoveTime),
                New SqlParameter("@FK_ReasonId", FK_ReasonId),
                New SqlParameter("@Remarks", Remarks),
                New SqlParameter("@Reader", Reader),
                New SqlParameter("@M_DATE_NUM", M_DATE_NUM),
                New SqlParameter("@M_TIME_NUM", M_TIME_NUM),
                New SqlParameter("@Status", Status),
                New SqlParameter("@IsManual", IsManual),
                New SqlParameter("@IsFromMobile", IsFromMobile),
                New SqlParameter("@BeaconId", BeaconId),
                New SqlParameter("@CREATED_BY", CREATED_BY),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@DeviceId", DeviceId))
                If errNo = 0 Then
                    MoveId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function AddMoveFromMobileTemporary(ByRef MoveId As Integer, FK_EmployeeId As Long, ByVal Type As String, ByVal MoveDate As DateTime, ByVal MoveTime As DateTime, ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal Reader As String, ByVal M_DATE_NUM As String, ByVal M_TIME_NUM As String, ByVal Status As String, ByVal IsManual As Boolean, ByVal CREATED_BY As String, ByVal IsFromMobile As String, ByVal GPSCoordinates As String, ByVal WorkLocationId As Integer, ByVal DeviceId As String) As Integer
            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@MoveId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, MoveId)
                errNo = objDac.AddUpdateDeleteSPTrans("Emp_Move_Insert_From_MobileTemporary", sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                New SqlParameter("@Type", Type),
                New SqlParameter("@MoveDate", MoveDate),
                New SqlParameter("@MoveTime", MoveTime),
                New SqlParameter("@FK_ReasonId", FK_ReasonId),
                New SqlParameter("@Remarks", Remarks),
                New SqlParameter("@Reader", Reader),
                New SqlParameter("@M_DATE_NUM", M_DATE_NUM),
                New SqlParameter("@M_TIME_NUM", M_TIME_NUM),
                New SqlParameter("@Status", Status),
                New SqlParameter("@IsManual", IsManual),
                New SqlParameter("@IsFromMobile", IsFromMobile),
                New SqlParameter("@MobileCoordinates", GPSCoordinates),
                New SqlParameter("@CREATED_BY", CREATED_BY),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@DeviceId", DeviceId))
                If errNo = 0 Then
                    MoveId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function AddMoveFromMobile_Mobile_BeaconTemporary(ByRef MoveId As Integer, FK_EmployeeId As Long, ByVal Type As String, ByVal MoveDate As DateTime, ByVal MoveTime As DateTime, ByVal FK_ReasonId As Integer, ByVal Remarks As String, ByVal Reader As String, ByVal M_DATE_NUM As String, ByVal M_TIME_NUM As String, ByVal Status As String, ByVal IsManual As Boolean, ByVal CREATED_BY As String, ByVal IsFromMobile As String, ByVal BeaconId As Integer, ByVal WorkLocationId As Integer, ByVal DeviceId As String) As Integer
            objDac = DAC.getDAC()
            Try
                Dim sqlOut = New SqlParameter("@MoveId", SqlDbType.Int, 8, ParameterDirection.Output, False, 0, 0, "", DataRowVersion.Default, MoveId)
                errNo = objDac.AddUpdateDeleteSPTrans("Emp_Move_Insert_From_Mobile_BeaconTemporary", sqlOut, New SqlParameter("@FK_EmployeeId", FK_EmployeeId),
                New SqlParameter("@Type", Type),
                New SqlParameter("@MoveDate", MoveDate),
                New SqlParameter("@MoveTime", MoveTime),
                New SqlParameter("@FK_ReasonId", FK_ReasonId),
                New SqlParameter("@Remarks", Remarks),
                New SqlParameter("@Reader", Reader),
                New SqlParameter("@M_DATE_NUM", M_DATE_NUM),
                New SqlParameter("@M_TIME_NUM", M_TIME_NUM),
                New SqlParameter("@Status", Status),
                New SqlParameter("@IsManual", IsManual),
                New SqlParameter("@IsFromMobile", IsFromMobile),
                New SqlParameter("@BeaconId", BeaconId),
                New SqlParameter("@CREATED_BY", CREATED_BY),
                New SqlParameter("@WorkLocationId", WorkLocationId),
                New SqlParameter("@DeviceId", DeviceId))
                If errNo = 0 Then
                    MoveId = sqlOut.Value
                End If
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function IsTimeInValid(EmployeeId As Integer, Reason As Integer, TimeIn As DateTime, Lang As Int32) As DataTable
            Dim objColl As DataTable
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable("Emp_Move_IsTimeInValid_Mobile", New SqlParameter("@Lang", Lang),
                                              New SqlParameter("@EmployeeId", EmployeeId),
                                              New SqlParameter("@Reason", Reason),
                                              New SqlParameter("@TimeIn", TimeIn))

            Catch ex As Exception
                errNo = -11
            End Try
            Return objColl
        End Function

        Public Function Check_IsFirstIn(FK_EmployeeId As Long) As DataTable
            Dim objColl As DataTable
            objDac = DAC.getDAC()
            Try
                objColl = objDac.GetDataTable("Emp_Move_Check_IsFirstIn", New SqlParameter("@FK_EmployeeId", FK_EmployeeId))

            Catch ex As Exception
                errNo = -11

            End Try
            Return objColl
        End Function

#End Region


    End Class
End Namespace