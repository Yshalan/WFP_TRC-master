Imports SmartV.UTILITIES
Imports System.Data
Imports TA.Lookup
Imports TA.Employees
Imports TA.DailyTasks

Partial Class Admin_RecalculateAttendance
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objEmp As Employee
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

#End Region

#Region "Page Events"

    Protected Sub btnStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Dim StrLevel As String = ""
        Dim StrLevelCode As String = ""
        Dim TempFromDate As Date
        Dim TempFromStr As String
        Dim TempToDate As Date
        Dim TempToStr As String
        Dim err As Integer
        Dim dt As DataTable


        objRECALC_REQUEST = New RECALC_REQUEST


        Dim IDss As String()

        Try
            objEmp = New Employee

            If Not dpFromDate.SelectedDate Is Nothing Then
                TempFromDate = dpFromDate.SelectedDate
                TempFromStr = DateToString(TempFromDate)
            End If

            If Not dpToDate.SelectedDate Is Nothing Then
                TempToDate = dpToDate.SelectedDate
                TempToStr = DateToString(TempToDate)
            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

            If (DateDiff(DateInterval.Day, TempFromDate, TempToDate, Microsoft.VisualBasic.FirstDayOfWeek.System, FirstWeekOfYear.System) > 30) Then
                CtlCommon.ShowMessage(Me, ResourceManager.GetString("MaxDeff", CultureInfo), "info")
                Return
            End If
            If dpToDate.SelectedDate < dpFromDate.SelectedDate Then
                CtlCommon.ShowMessage(Me, ResourceManager.GetString("DateRange", CultureInfo), "info")
                Return
            End If


            If UserCtrlOrgHeirarchy.EntityId <> 0 Then
                objEmp.FK_CompanyId = UserCtrlOrgHeirarchy.CompanyId
                objEmp.FK_EntityId = UserCtrlOrgHeirarchy.EntityId

                dt = objEmp.GetEmpByCompEnt()
            End If

            If UserCtrlOrgHeirarchy.EmployeeId <> 0 Then

                objRECALC_REQUEST.EMP_NO = UserCtrlOrgHeirarchy.EmployeeId
                If dpFromDate.SelectedDate <= Date.Now Then
                    If dpToDate.SelectedDate > Date.Now Then
                        TempToDate = Date.Now
                    End If

                    Dim NoOfDays As Long = DateDiff(DateInterval.Day, TempFromDate, TempToDate, Microsoft.VisualBasic.FirstDayOfWeek.Sunday, FirstWeekOfYear.System)

                    For i As Long = 0 To NoOfDays
                        objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(DateToString(TempFromDate.AddDays(Convert.ToDouble(i))))
                        err = objRECALC_REQUEST.RECALCULATE()
                    Next
                End If
            Else
                If UserCtrlOrgHeirarchy.EntityId = 0 Then
                    CtlCommon.ShowMessage(Me, ResourceManager.GetString("PlaeseSelectLevel", CultureInfo), "info")
                    Return
                End If

                For Each dr As DataRow In dt.Rows
                    objRECALC_REQUEST.EMP_NO = dr("Emp_no")
                    If dpFromDate.SelectedDate <= Date.Now Then
                        If dpToDate.SelectedDate > Date.Now Then
                            TempToDate = Date.Now
                        End If

                        Dim NoOfDays As Long = DateDiff(DateInterval.Day, TempFromDate, TempToDate, Microsoft.VisualBasic.FirstDayOfWeek.Sunday, FirstWeekOfYear.System)

                        For i As Long = 0 To NoOfDays
                            objRECALC_REQUEST.VALID_FROM_NUM = Integer.Parse(DateToString(TempFromDate.AddDays(Convert.ToDouble(i))))
                            err += objRECALC_REQUEST.RECALCULATE()
                        Next
                    End If
                Next

            End If


            If err = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("RecalSuccess", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("RecalFailed", CultureInfo), "error")
            End If
        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "Methods"

    Private Function DateToString(ByVal TempDate As Date) As String
        Dim tempDay As String
        Dim tempMonth As String

        If TempDate.Month.ToString.Length = 1 Then
            tempMonth = "0" + TempDate.Month.ToString
        Else
            tempMonth = TempDate.Month.ToString
        End If
        If TempDate.Day.ToString.Length = 1 Then
            tempDay = "0" + TempDate.Day.ToString
        Else
            tempDay = TempDate.Day.ToString
        End If
        Return TempDate.Year.ToString() + tempMonth + tempDay
    End Function

#End Region

End Class
