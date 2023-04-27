Imports Microsoft.VisualBasic

Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Imports SmartV.DB
Imports TA.LookUp
Imports SmartV.UTILITIES


Namespace TA.Employees

	Public Class DALEmp_Shifts
		Inherits MGRBase



#Region "Class Variables"
		Private strConn As String

        Private Emp_Shifts_select_All As String = "Emp_Shifts_select_All"
        Private Emp_Shifts_select_ByDate As String = "Emp_Shifts_select_ByDate"
		Private Emp_Shifts_Insert As String = "Emp_Shifts_Insert"
		Private Emp_Shifts_Update As String = "Emp_Shifts_Update"
		Private Emp_Shifts_Delete As String = "Emp_Shifts_Delete"
		Private Emp_Shifts_SelectByDate As String = "Emp_Shifts_SelectByDate"
        Private Emp_Shifts_InsertUpdateDeleteSchedule As String = "Emp_Shifts_InsertUpdateDeleteSchedule"
        Private Emp_ShiftsRequest_InsertUpdateDeleteSchedule As String = "Emp_ShiftsRequest_InsertUpdateDeleteSchedule"
        Private Emp_Shift_GetValidtionMsg As String = "Emp_Shift_GetValidtionMsg"
        Private Emp_Shift_Request_GetValidtionMsg As String = "Emp_Shift_Request_GetValidtionMsg"
        Private Emp_ShiftsRequest_SelectByDate As String = "Emp_ShiftsRequest_SelectByDate"
        Private Emp_Shifts_InsertUpdateDeleteScheduleApprove As String = "Emp_Shifts_InsertUpdateDeleteScheduleApprove"

#End Region
#Region "Constructor"
		Public Sub New()



		End Sub

#End Region

#Region "Methods"

		Public Function Add(ByVal FK_EmployeeId As Integer, ByVal FK_ShiftId As Integer, ByVal WorkDate As DateTime) As Integer

			objDac = DAC.getDAC()
			Try
				errNo = objDac.AddUpdateDeleteSPTrans(Emp_Shifts_Insert, _
				  New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
				   New SqlParameter("@FK_ShiftId", FK_ShiftId), _
				   New SqlParameter("@WorkDate", WorkDate))
			Catch ex As Exception
				errNo = -11
				CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
			End Try
			Return errNo

		End Function

		Public Function Delete(ByVal FK_EmployeeId As Integer, ByVal FK_ShiftId As Integer, ByVal WorkDate As DateTime) As Integer

			objDac = DAC.getDAC()
			Try
				errNo = objDac.AddUpdateDeleteSPTrans(Emp_Shifts_Delete, _
				  New SqlParameter("@FK_EmployeeId", FK_EmployeeId), _
				   New SqlParameter("@FK_ShiftId", FK_ShiftId), _
				   New SqlParameter("@WorkDate", WorkDate))
			Catch ex As Exception
				errNo = -11
				CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
			End Try
			Return errNo

		End Function

		Public Function GetAll() As DataTable

			objDac = DAC.getDAC()
			Dim objColl As DataTable = Nothing
			Try
				objColl = objDac.GetDataTable(Emp_Shifts_select_All, Nothing)
			Catch ex As Exception
				errNo = -11
				CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
			End Try
			Return objColl


        End Function

        Public Function GetShiftsByDate(ByVal FK_EmployeeId As Integer, ByVal WorkDate As DateTime) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_Shifts_select_ByDate, _
      New SqlParameter("@EmployeeId", FK_EmployeeId), _
       New SqlParameter("@WorkDate", WorkDate))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function


		Public Function GetByDate(ByVal Year As Integer, ByVal Month As Integer) As DataTable

			objDac = DAC.getDAC()
			Dim objColl As DataTable = Nothing
			Try
				objColl = objDac.GetDataTable(Emp_Shifts_SelectByDate, New SqlParameter("@Year", Year), New SqlParameter("@Month", Month))
			Catch ex As Exception
				errNo = -11
				CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
			End Try
			Return objColl


		End Function

        Public Function GetRequestByDate(ByVal Year As Integer, ByVal Month As Integer) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Try
                objColl = objDac.GetDataTable(Emp_ShiftsRequest_SelectByDate, New SqlParameter("@Year", Year), New SqlParameter("@Month", Month))
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

		Public Function InsertUpdateDeleteSchedule_passTable(ByVal ScheduleTable As DataTable, ByRef rowCount As Integer) As Integer
			objDac = DAC.getDAC()
			Dim paramScheduleTable As New SqlParameter()
			paramScheduleTable.SqlDbType = SqlDbType.Structured
			paramScheduleTable.ParameterName = "ScheduleTable"
			paramScheduleTable.Value = ScheduleTable

			Dim paramAffectedRows As New SqlParameter()
			paramAffectedRows.SqlDbType = SqlDbType.Int
			paramAffectedRows.Direction = ParameterDirection.Output
			paramAffectedRows.ParameterName = "AffectedRows"
			paramAffectedRows.Value = rowCount


			objDac = DAC.getDAC()
			Try
				errNo = objDac.AddUpdateDeleteSPTrans(Emp_Shifts_InsertUpdateDeleteSchedule, paramScheduleTable, paramAffectedRows)
				rowCount = paramAffectedRows.Value
			Catch ex As Exception
				errNo = -11
				CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
			End Try
			Return errNo

		End Function

        Public Function InsertUpdateDeleteSchedule(ByVal ScheduleXml As Xml, ByRef rowCount As Integer) As Integer
            objDac = DAC.getDAC()
            Dim paramScheduleTable As New SqlParameter()
            paramScheduleTable.SqlDbType = SqlDbType.Xml
            paramScheduleTable.ParameterName = "SchedulesXml"
            paramScheduleTable.Value = ScheduleXml.Document.InnerXml

            Dim paramAffectedRows As New SqlParameter()
            paramAffectedRows.SqlDbType = SqlDbType.Int
            paramAffectedRows.Direction = ParameterDirection.Output
            paramAffectedRows.ParameterName = "AffectedRows"
            paramAffectedRows.Value = rowCount


            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Shifts_InsertUpdateDeleteSchedule, paramScheduleTable, paramAffectedRows)
                rowCount = paramAffectedRows.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function InsertUpdateDeleteScheduleApprove(ByVal ScheduleXml As Xml, ByRef rowCount As Integer) As Integer
            objDac = DAC.getDAC()
            Dim paramScheduleTable As New SqlParameter()
            paramScheduleTable.SqlDbType = SqlDbType.Xml
            paramScheduleTable.ParameterName = "SchedulesXml"
            paramScheduleTable.Value = ScheduleXml.Document.InnerXml

            Dim paramAffectedRows As New SqlParameter()
            paramAffectedRows.SqlDbType = SqlDbType.Int
            paramAffectedRows.Direction = ParameterDirection.Output
            paramAffectedRows.ParameterName = "AffectedRows"
            paramAffectedRows.Value = rowCount


            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_Shifts_InsertUpdateDeleteScheduleApprove, paramScheduleTable, paramAffectedRows)
                rowCount = paramAffectedRows.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function InsertUpdateDeleteScheduleRequest(ByVal ScheduleXml As Xml, ByRef rowCount As Integer) As Integer
            objDac = DAC.getDAC()
            Dim paramScheduleTable As New SqlParameter()
            paramScheduleTable.SqlDbType = SqlDbType.Xml
            paramScheduleTable.ParameterName = "SchedulesXml"
            paramScheduleTable.Value = ScheduleXml.Document.InnerXml

            Dim paramAffectedRows As New SqlParameter()
            paramAffectedRows.SqlDbType = SqlDbType.Int
            paramAffectedRows.Direction = ParameterDirection.Output
            paramAffectedRows.ParameterName = "AffectedRows"
            paramAffectedRows.Value = rowCount


            objDac = DAC.getDAC()
            Try
                errNo = objDac.AddUpdateDeleteSPTrans(Emp_ShiftsRequest_InsertUpdateDeleteSchedule, paramScheduleTable, paramAffectedRows)
                rowCount = paramAffectedRows.Value
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return errNo

        End Function

        Public Function GetValidtionMsg(ByVal ValidationMinimumShift As Xml) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Dim paramScheduleTable As New SqlParameter()
            paramScheduleTable.SqlDbType = SqlDbType.Xml
            paramScheduleTable.ParameterName = "ValidationMinimumShift"
            paramScheduleTable.Value = ValidationMinimumShift.Document.InnerXml
            Try
                objColl = objDac.GetDataTable(Emp_Shift_GetValidtionMsg, paramScheduleTable)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function

        Public Function GetRequestValidtionMsg(ByVal ValidationMinimumShift As Xml) As DataTable

            objDac = DAC.getDAC()
            Dim objColl As DataTable = Nothing
            Dim paramScheduleTable As New SqlParameter()
            paramScheduleTable.SqlDbType = SqlDbType.Xml
            paramScheduleTable.ParameterName = "ValidationMinimumShift"
            paramScheduleTable.Value = ValidationMinimumShift.Document.InnerXml
            Try
                objColl = objDac.GetDataTable(Emp_Shift_Request_GetValidtionMsg, paramScheduleTable)
            Catch ex As Exception
                errNo = -11
                CtlCommon.CreateErrorLog(logPath, ex.Message, MethodBase.GetCurrentMethod.Name)
            End Try
            Return objColl


        End Function
#End Region


	End Class
End Namespace