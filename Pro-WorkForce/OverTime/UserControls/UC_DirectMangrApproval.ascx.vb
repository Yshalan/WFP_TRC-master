Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports System.Reflection
Imports System.Data
Imports TA.OverTime

Partial Class OverTime_UserControls_UC_DirectMangrApproval
    Inherits System.Web.UI.UserControl
    Public Property SortDir() As String
        Get
            Return ViewState("SortDir")
        End Get
        Set(ByVal value As String)
            ViewState("SortDir") = value
        End Set
    End Property
    Public Property SortExep() As String
        Get
            Return ViewState("SortExep")
        End Get
        Set(ByVal value As String)
            ViewState("SortExep") = value
        End Set
    End Property
    Public Property SortExepression() As String
        Get
            Return ViewState("SortExepression")
        End Get
        Set(ByVal value As String)
            ViewState("SortExepression") = value
        End Set
    End Property
    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property
    Public Property dtOTMgrCurrent() As DataTable
        Get
            Return ViewState("dtOTMgrCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtOTMgrCurrent") = value
        End Set
    End Property
    Public Property dtOTMyCurrent() As DataTable
        Get
            Return ViewState("dtOTMyCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtOTMyCurrent") = value
        End Set
    End Property
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            FillYear()
            FillMonth()
            FillGrid()
            FillOTMangerGrid()
            'FillMyAppsGrid()
        End If
    End Sub
    Private Sub FillMonth()
        Try

            Dim LastMonthDate As DateTime = DateTime.Now.AddMonths(-1)

            Dim itm1 As New Telerik.Web.UI.RadComboBoxItem

            itm1.Value = 1
            itm1.Text = Resources.Labels.Jan

            cmbMonth.Items.Add(itm1)

            Dim itm2 As New Telerik.Web.UI.RadComboBoxItem
            itm2.Value = 2
            itm2.Text = Resources.Labels.Feb

            cmbMonth.Items.Add(itm2)

            Dim itm3 As New Telerik.Web.UI.RadComboBoxItem
            itm3.Value = 3
            itm3.Text = Resources.Labels.Mar

            cmbMonth.Items.Add(itm3)

            Dim itm4 As New Telerik.Web.UI.RadComboBoxItem
            itm4.Value = 4
            itm4.Text = Resources.Labels.Apr

            cmbMonth.Items.Add(itm4)

            Dim itm5 As New Telerik.Web.UI.RadComboBoxItem
            itm5.Value = 5
            itm5.Text = Resources.Labels.May

            cmbMonth.Items.Add(itm5)

            Dim itm6 As New Telerik.Web.UI.RadComboBoxItem
            itm6.Value = 6
            itm6.Text = Resources.Labels.Jun

            cmbMonth.Items.Add(itm6)

            Dim itm7 As New Telerik.Web.UI.RadComboBoxItem
            itm7.Value = 7
            itm7.Text = Resources.Labels.Jul

            cmbMonth.Items.Add(itm7)

            Dim itm8 As New Telerik.Web.UI.RadComboBoxItem
            itm8.Value = 8
            itm8.Text = Resources.Labels.Aug

            cmbMonth.Items.Add(itm8)

            Dim itm9 As New Telerik.Web.UI.RadComboBoxItem
            itm9.Value = 9
            itm9.Text = Resources.Labels.Sep

            cmbMonth.Items.Add(itm9)

            Dim itm10 As New Telerik.Web.UI.RadComboBoxItem
            itm10.Value = 10
            itm10.Text = Resources.Labels.Oct

            cmbMonth.Items.Add(itm10)

            Dim itm11 As New Telerik.Web.UI.RadComboBoxItem
            itm11.Value = 11
            itm11.Text = Resources.Labels.Nov

            cmbMonth.Items.Add(itm11)

            Dim itm12 As New Telerik.Web.UI.RadComboBoxItem
            itm12.Value = 12
            itm12.Text = Resources.Labels.Dec

            cmbMonth.Items.Add(itm12)

            cmbMonth.SelectedValue = LastMonthDate.Month
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub FillYear()
        Try

      
        For i As Integer = DateTime.Now.Year - 5 To DateTime.Now.Year

            Dim lst As New RadComboBoxItem
            If SessionVariables.CultureInfo = "en-US" Then
                lst = New RadComboBoxItem(i.ToString(), i.ToString())
                cmbYear.Items.Add(lst)
            Else
                lst = New RadComboBoxItem(i.ToString(), i.ToString())
                cmbYear.Items.Add(lst)
            End If

            ' cmbYear.Items.Add(i.ToString())
        Next

        Dim LastMonthDate As DateTime = DateTime.Now.AddMonths(-1)

        cmbYear.Items.FindItemByValue(LastMonthDate.Year.ToString()).Selected = True

        'cmbYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = True  'set current year as 
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub FillGrid()
        Try
            Dim obj As New Emp_OverTime_Master

            With obj
                .FK_EmployeeID = SessionVariables.LoginUser.FK_EmployeeId
                .OverTimeMonth = cmbMonth.SelectedValue
                .OverTimeYear = cmbYear.SelectedValue

            End With
            dtCurrent = obj.GetOTSummary()
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            gdvOverTimeSummary.DataSource = dv
            gdvOverTimeSummary.DataBind()

            If Not dtCurrent Is Nothing Then
                If dtCurrent.Rows.Count > 0 Then
                    If SessionVariables.CultureInfo = "en-US" Then
                        LbRowCount.Text = "Search Result:" & dtCurrent.Rows.Count
                    Else
                        LbRowCount.Text = "نتيجة البحث " & dtCurrent.Rows.Count
                    End If
                Else
                    If SessionVariables.CultureInfo = "en-US" Then
                        LbRowCount.Text = "Search Result:" & 0
                    Else
                        LbRowCount.Text = "نتيجة البحث " & 0
                    End If
                End If
            Else
                If SessionVariables.CultureInfo = "en-US" Then
                    LbRowCount.Text = "Search Result:" & 0
                Else
                    LbRowCount.Text = "نتيجة البحث " & 0
                End If

            End If
            obj = Nothing

        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub FillOTMangerGrid()
        Try
            Dim obj As New Emp_OverTime_Master

            With obj
                .FK_EmployeeID = SessionVariables.LoginUser.FK_EmployeeId
            End With
            dtOTMgrCurrent = obj.GetOTSummaryForManager()
            Dim dv As New DataView(dtOTMgrCurrent)
            dv.Sort = SortExepression
            gdvManagerOTApps.DataSource = dv
            gdvManagerOTApps.DataBind()

            If Not dtOTMgrCurrent Is Nothing Then
                If dtOTMgrCurrent.Rows.Count > 0 Then
                    If SessionVariables.CultureInfo = "en-US" Then
                        LbRowCountOTMgr.Text = "Search Result:" & dtOTMgrCurrent.Rows.Count
                    Else
                        LbRowCountOTMgr.Text = "نتيجة البحث " & dtOTMgrCurrent.Rows.Count
                    End If
                Else
                    If SessionVariables.CultureInfo = "en-US" Then
                        LbRowCountOTMgr.Text = "Search Result:" & 0
                    Else
                        LbRowCountOTMgr.Text = "نتيجة البحث " & 0
                    End If
                End If
            Else
                If SessionVariables.CultureInfo = "en-US" Then
                    LbRowCountOTMgr.Text = "Search Result:" & 0
                Else
                    LbRowCountOTMgr.Text = "نتيجة البحث " & 0
                End If

            End If
            obj = Nothing

        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Private Sub FillMyAppsGrid()
        Try
            Dim obj As New Emp_OverTime_Master

            With obj
                .FK_EmployeeID = SessionVariables.LoginUser.FK_EmployeeId
            End With
            dtOTMyCurrent = obj.GetOTMyApplications()
            Dim dv As New DataView(dtOTMyCurrent)
            dv.Sort = SortExepression
            gdvMyApplications.DataSource = dv
            gdvMyApplications.DataBind()

            If Not dtOTMyCurrent Is Nothing Then
                If dtOTMyCurrent.Rows.Count > 0 Then
                    If SessionVariables.CultureInfo = "en-US" Then
                        LbRowCountOTMyApps.Text = "Search Result:" & dtOTMyCurrent.Rows.Count
                    Else
                        LbRowCountOTMyApps.Text = "نتيجة البحث " & dtOTMyCurrent.Rows.Count
                    End If
                Else
                    If SessionVariables.CultureInfo = "en-US" Then
                        LbRowCountOTMyApps.Text = "Search Result:" & 0
                    Else
                        LbRowCountOTMyApps.Text = "نتيجة البحث " & 0
                    End If
                End If
            Else
                If SessionVariables.CultureInfo = "en-US" Then
                    LbRowCountOTMyApps.Text = "Search Result:" & 0
                Else
                    LbRowCountOTMyApps.Text = "نتيجة البحث " & 0
                End If

            End If
            obj = Nothing

        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Protected Sub gdvOverTimeSummary_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvOverTimeSummary.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim cmbDecision As RadComboBox = DirectCast(e.Row.FindControl("cmbDecision"), RadComboBox)
                Dim txtApprovedOTNormal_HH As TextBox = DirectCast(e.Row.FindControl("txtApprovedOTNormal_HH"), TextBox)
                Dim txtApprovedOTNormal_MM As TextBox = DirectCast(e.Row.FindControl("txtApprovedOTNormal_MM"), TextBox)
                Dim txtApprovedOTRest_HH As TextBox = DirectCast(e.Row.FindControl("txtApprovedOTRest_HH"), TextBox)
                Dim txtApprovedOTRest_MM As TextBox = DirectCast(e.Row.FindControl("txtApprovedOTRest_MM"), TextBox)


                If txtApprovedOTNormal_HH.Text.Trim() <> "" Then
                    Dim OTNormal() As String = txtApprovedOTNormal_HH.Text.Trim().Split(":")

                    txtApprovedOTNormal_HH.Text = OTNormal(0).ToString()
                    txtApprovedOTNormal_MM.Text = OTNormal(1).ToString()
                Else
                    txtApprovedOTNormal_HH.Text = "00"
                    txtApprovedOTNormal_MM.Text = "00"
                End If

                If txtApprovedOTRest_HH.Text.Trim() <> "" Then
                    Dim OTNormal() As String = txtApprovedOTRest_HH.Text.Trim().Split(":")

                    txtApprovedOTRest_HH.Text = OTNormal(0).ToString()
                    txtApprovedOTRest_MM.Text = OTNormal(1).ToString()

                Else
                    txtApprovedOTRest_HH.Text = "00"
                    txtApprovedOTRest_MM.Text = "00"
                End If


                Dim obj As New OverTime_Decision

                If SessionVariables.CultureInfo = "en-US" Then
                    CtlCommon.FillTelerikDropDownList(cmbDecision, obj.GetAllWithFilter("2,3"), CtlCommon.Lang.EN)
                Else
                    CtlCommon.FillTelerikDropDownList(cmbDecision, obj.GetAllWithFilter("2,3"), CtlCommon.Lang.AR)
                End If

            End If
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Protected Sub gdvMyApplications_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gdvMyApplications.Sorting
        Try
            If SortDir = "ASC" Then
                SortDir = "DESC"
            Else
                SortDir = "ASC"
            End If
            SortExepression = e.SortExpression & Space(1) & SortDir
            Dim dv As New DataView(dtOTMyCurrent)

            dv.Sort = SortExepression
            SortExep = e.SortExpression
            gdvMyApplications.DataSource = dv
            gdvMyApplications.DataBind()
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Protected Sub gdvMyApplications_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gdvMyApplications.PageIndexChanging
        Try
            gdvMyApplications.SelectedIndex = -1
            gdvMyApplications.PageIndex = e.NewPageIndex
            Dim dv As New DataView(dtOTMyCurrent)
            dv.Sort = SortExepression

            gdvMyApplications.DataSource = dv
            gdvMyApplications.DataBind()
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Protected Sub gdvOverTimeSummary_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gdvOverTimeSummary.Sorting
        Try
            If SortDir = "ASC" Then
                SortDir = "DESC"
            Else
                SortDir = "ASC"
            End If
            SortExepression = e.SortExpression & Space(1) & SortDir
            Dim dv As New DataView(dtCurrent)

            dv.Sort = SortExepression
            SortExep = e.SortExpression
            gdvOverTimeSummary.DataSource = dv
            gdvOverTimeSummary.DataBind()
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
   
    Protected Sub gdvOverTimeSummary_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gdvOverTimeSummary.PageIndexChanging
        Try
            gdvOverTimeSummary.SelectedIndex = -1
            gdvOverTimeSummary.PageIndex = e.NewPageIndex
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression

            gdvOverTimeSummary.DataSource = dv
            gdvOverTimeSummary.DataBind()
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Protected Sub gdvManagerOTApps_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gdvManagerOTApps.Sorting
        Try
            If SortDir = "ASC" Then
                SortDir = "DESC"
            Else
                SortDir = "ASC"
            End If
            SortExepression = e.SortExpression & Space(1) & SortDir
            Dim dv As New DataView(dtOTMgrCurrent)

            dv.Sort = SortExepression
            SortExep = e.SortExpression
            gdvManagerOTApps.DataSource = dv
            gdvManagerOTApps.DataBind()
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Protected Sub gdvManagerOTApps_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gdvManagerOTApps.PageIndexChanging
        Try
            gdvManagerOTApps.SelectedIndex = -1
            gdvManagerOTApps.PageIndex = e.NewPageIndex
            Dim dv As New DataView(dtOTMgrCurrent)
            dv.Sort = SortExepression

            gdvManagerOTApps.DataSource = dv
            gdvManagerOTApps.DataBind()
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    

    Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            FillGrid()
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Protected Sub btnSaveOT_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim gvr As GridViewRow = CType(sender, Button).Parent.Parent

            Dim obj As New Emp_OverTime_Master
            Dim err As Integer

            Dim lblEmployeeId As Label = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("lblEmployeeId"), Label)
            Dim lblMonth As Label = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("lblMonth"), Label)
            Dim lblYear As Label = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("lblYear"), Label)
            Dim cmbDecision As RadComboBox = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("cmbDecision"), RadComboBox)
            Dim txtNotes As TextBox = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("txtNotes"), TextBox)

            Dim lblWorkedOTNormalNum As Label = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("lblWorkedOTNormalNum"), Label)
            Dim lblWorkedOTRestNum As Label = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("lblWorkedOTRestNum"), Label)

            Dim lblPlannedOTNormalNum As Label = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("lblPlannedOTNormalNum"), Label)
            Dim lblPlannedOTRestNum As Label = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("lblPlannedOTRestNum"), Label)

            Dim txtApprovedOTNormal_HH As TextBox = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("txtApprovedOTNormal_HH"), TextBox)
            Dim txtApprovedOTNormal_MM As TextBox = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("txtApprovedOTNormal_MM"), TextBox)
            Dim txtApprovedOTRest_HH As TextBox = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("txtApprovedOTRest_HH"), TextBox)
            Dim txtApprovedOTRest_MM As TextBox = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("txtApprovedOTRest_MM"), TextBox)
          
            If cmbDecision.SelectedValue = -1 Then 'Please select 
                CtlCommon.ShowMessage(Me.Page, "Please Select Your Decision", "info")
                Exit Sub

            End If

            If cmbDecision.SelectedValue = 1 Then 'Pending
                CtlCommon.ShowMessage(Me.Page, "Sorry the request is already pending,Please choose another decision to proceed", "info")
                Exit Sub

            End If


            If cmbDecision.SelectedValue = 3 Or cmbDecision.SelectedValue = 4 Then
                If txtNotes.Text.Trim() = "" Then
                    CtlCommon.ShowMessage(Me.Page, "Please Enter your Reason/Notes", "info")
                    Exit Sub
                End If
            End If

           

            If Not IsNumeric(txtApprovedOTNormal_HH.Text.Trim()) Or Not IsNumeric(txtApprovedOTNormal_MM.Text.Trim()) Or Not IsNumeric(txtApprovedOTRest_HH.Text.Trim()) Or Not IsNumeric(txtApprovedOTRest_MM.Text.Trim()) Then

                CtlCommon.ShowMessage(Me.Page, "Approved OT for Normal and Rest Days should be numeric(digits)", "info")
                Exit Sub
            End If




            With obj

                .FK_EmployeeID = lblEmployeeId.Text.Trim()
                .OverTimeMonth = lblMonth.Text.Trim()
                .OverTimeYear = lblYear.Text.Trim()
                .Planned_OT_Normal = 0
                .Planned_OT_Rest = 0

                If IsNumeric(lblWorkedOTNormalNum.Text.Trim()) Then
                    .Worked_OT_Normal = (lblWorkedOTNormalNum.Text.Trim() * 60)
                Else
                    .Worked_OT_Normal = 0
                End If
                If IsNumeric(lblWorkedOTRestNum.Text.Trim()) Then
                    .Worked_OT_Rest = (lblWorkedOTRestNum.Text.Trim() * 60)
                Else
                    .Worked_OT_Rest = 0
                End If


                If IsNumeric(txtApprovedOTRest_HH.Text.Trim()) And IsNumeric(txtApprovedOTRest_MM.Text.Trim()) Then
                    .Approved_OT_Rest = (txtApprovedOTRest_HH.Text.Trim() * 60) + txtApprovedOTRest_MM.Text.Trim()
                Else
                    .Approved_OT_Rest = 0
                End If


                If IsNumeric(txtApprovedOTNormal_HH.Text.Trim()) And IsNumeric(txtApprovedOTNormal_MM.Text.Trim()) Then
                    .Approved_OT_Normal = (txtApprovedOTNormal_HH.Text.Trim() * 60) + txtApprovedOTNormal_MM.Text.Trim()
                Else
                    .Approved_OT_Normal = 0
                End If



                .JustificationRequested = False
                .Justificationtext = ""

                .CREATED_BY = SessionVariables.LoginUser.ID
                .LoggedUserEmployeeID = SessionVariables.LoginUser.FK_EmployeeId
                .FK_OTDecisionID = cmbDecision.SelectedValue
                .Note = txtNotes.Text.Trim()



                If (.Approved_OT_Normal > lblPlannedOTNormalNum.Text.Trim() And .Approved_OT_Normal > 0) Then

                    If txtNotes.Text.Trim() = "" Then
                        CtlCommon.ShowMessage(Me.Page, "Please Enter your Reason/Notes as the Approved Normal OT is greater than the Planned Normal OT ", "info")
                        Exit Sub
                    End If
                End If

                If (.Approved_OT_Rest > lblPlannedOTRestNum.Text.Trim() And .Approved_OT_Rest > 0) Then

                    If txtNotes.Text.Trim() = "" Then
                        CtlCommon.ShowMessage(Me.Page, "Please Enter your Reason/Notes as the Approved Rest OT is greater than the Planned Rest OT ", "info")
                        Exit Sub
                    End If
                End If


                err = .Add()


                If err = 0 Then
                    FillGrid()
                    ' FillMyAppsGrid()
                    CtlCommon.ShowMessage(Me.Page, "Saved Successfully", "success")
                    Exit Sub
                Else
                    CtlCommon.ShowMessage(Me.Page, "Error while Saving", "error")
                    Exit Sub
                End If

            End With


            obj = Nothing


        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Protected Sub gdvManagerOTApps_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gdvManagerOTApps.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim cmbDecision As RadComboBox = DirectCast(e.Row.FindControl("cmbDecision"), RadComboBox)
                Dim txtApprovedOTNormal_HH As TextBox = DirectCast(e.Row.FindControl("txtApprovedOTNormal_HH"), TextBox)
                Dim txtApprovedOTNormal_MM As TextBox = DirectCast(e.Row.FindControl("txtApprovedOTNormal_MM"), TextBox)
                Dim txtApprovedOTRest_HH As TextBox = DirectCast(e.Row.FindControl("txtApprovedOTRest_HH"), TextBox)
                Dim txtApprovedOTRest_MM As TextBox = DirectCast(e.Row.FindControl("txtApprovedOTRest_MM"), TextBox)
                Dim lblOT_MasterID As Label = DirectCast(e.Row.FindControl("lblOT_MasterID"), Label)

                If txtApprovedOTNormal_HH.Text.Trim() <> "" Then
                    Dim OTNormal() As String = txtApprovedOTNormal_HH.Text.Trim().Split(":")

                    txtApprovedOTNormal_HH.Text = OTNormal(0).ToString()
                    txtApprovedOTNormal_MM.Text = OTNormal(1).ToString()
                Else
                    txtApprovedOTNormal_HH.Text = "00"
                    txtApprovedOTNormal_MM.Text = "00"
                End If

                If txtApprovedOTRest_HH.Text.Trim() <> "" Then
                    Dim OTNormal() As String = txtApprovedOTRest_HH.Text.Trim().Split(":")

                    txtApprovedOTRest_HH.Text = OTNormal(0).ToString()
                    txtApprovedOTRest_MM.Text = OTNormal(1).ToString()

                Else
                    txtApprovedOTRest_HH.Text = "00"
                    txtApprovedOTRest_MM.Text = "00"
                End If


                Dim obj As New OverTime_Decision

                If SessionVariables.CultureInfo = "en-US" Then
                    CtlCommon.FillTelerikDropDownList(cmbDecision, obj.GetAllWithFilter("2,3,4"), CtlCommon.Lang.EN)
                Else
                    CtlCommon.FillTelerikDropDownList(cmbDecision, obj.GetAllWithFilter("2,3,4"), CtlCommon.Lang.AR)
                End If

            End If
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Protected Sub btnSaveMgrOT_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim gvr As GridViewRow = CType(sender, Button).Parent.Parent

            Dim obj As New Emp_OverTime_Master
            Dim err As Integer

            Dim lblOT_MasterID As Label = DirectCast(gdvManagerOTApps.Rows(gvr.RowIndex).FindControl("lblOT_MasterID"), Label)
            Dim cmbDecision As RadComboBox = DirectCast(gdvManagerOTApps.Rows(gvr.RowIndex).FindControl("cmbDecision"), RadComboBox)
            Dim txtNotes As TextBox = DirectCast(gdvManagerOTApps.Rows(gvr.RowIndex).FindControl("txtNotes"), TextBox)
            Dim txtJustificationText As TextBox = DirectCast(gdvManagerOTApps.Rows(gvr.RowIndex).FindControl("txtJustificationText"), TextBox)
            Dim lblWorkedOTNormalNum As Label = DirectCast(gdvManagerOTApps.Rows(gvr.RowIndex).FindControl("lblWorkedOTNormalNum"), Label)
            Dim lblWorkedOTRestNum As Label = DirectCast(gdvManagerOTApps.Rows(gvr.RowIndex).FindControl("lblWorkedOTRestNum"), Label)
            Dim txtApprovedOTNormal_HH As TextBox = DirectCast(gdvManagerOTApps.Rows(gvr.RowIndex).FindControl("txtApprovedOTNormal_HH"), TextBox)
            Dim txtApprovedOTNormal_MM As TextBox = DirectCast(gdvManagerOTApps.Rows(gvr.RowIndex).FindControl("txtApprovedOTNormal_MM"), TextBox)
            Dim txtApprovedOTRest_HH As TextBox = DirectCast(gdvManagerOTApps.Rows(gvr.RowIndex).FindControl("txtApprovedOTRest_HH"), TextBox)
            Dim txtApprovedOTRest_MM As TextBox = DirectCast(gdvManagerOTApps.Rows(gvr.RowIndex).FindControl("txtApprovedOTRest_MM"), TextBox)

            If cmbDecision.SelectedValue = -1 Then 'Please select 
                CtlCommon.ShowMessage(Me.Page, "Please Select Your Decision", "info")
                Exit Sub

            End If

            If cmbDecision.SelectedValue = 1 Then 'Pending
                CtlCommon.ShowMessage(Me.Page, "Sorry the request is already pending,Please choose another decision to proceed", "info")
                Exit Sub

            End If


            If cmbDecision.SelectedValue = 3 Then
                If txtNotes.Text.Trim() = "" Then
                    CtlCommon.ShowMessage(Me.Page, "Please Enter your Reason/Notes", "info")
                    Exit Sub
                End If
            End If

            If cmbDecision.SelectedValue = 4 Then
                If txtJustificationText.Text.Trim() = "" Then
                    CtlCommon.ShowMessage(Me.Page, "Please Enter your Justification Text as the decision is Request for justification", "info")
                    Exit Sub
                End If
            End If

            If Not IsNumeric(txtApprovedOTNormal_HH.Text.Trim()) Or Not IsNumeric(txtApprovedOTNormal_MM.Text.Trim()) Or Not IsNumeric(txtApprovedOTRest_HH.Text.Trim()) Or Not IsNumeric(txtApprovedOTRest_MM.Text.Trim()) Then

                CtlCommon.ShowMessage(Me.Page, "Approved OT for Normal and Rest Days should be numeric(digits)", "info")
                Exit Sub
            End If


            With obj
                .OT_MasterID = lblOT_MasterID.Text.Trim()

                .GetByPK()

                If IsNumeric(txtApprovedOTNormal_HH.Text.Trim()) And IsNumeric(txtApprovedOTNormal_MM.Text.Trim()) Then
                    .Approved_OT_Normal = (txtApprovedOTNormal_HH.Text.Trim() * 60) + txtApprovedOTNormal_MM.Text.Trim()
                Else
                    .Approved_OT_Normal = 0
                End If


                If IsNumeric(txtApprovedOTRest_HH.Text.Trim()) And IsNumeric(txtApprovedOTRest_MM.Text.Trim()) Then
                    .Approved_OT_Rest = (txtApprovedOTRest_HH.Text.Trim() * 60) + txtApprovedOTRest_MM.Text.Trim()
                Else
                    .Approved_OT_Rest = 0
                End If

                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                .LoggedUserEmployeeID = SessionVariables.LoginUser.FK_EmployeeId
                .FK_OTDecisionID = cmbDecision.SelectedValue
                .Note = txtNotes.Text.Trim()

                If cmbDecision.SelectedValue = 4 Then 'Request for Justification
                    .Justificationtext = txtJustificationText.Text.Trim()
                    .JustificationRequested = True
                Else
                    .Justificationtext = ""
                    .JustificationRequested = False
                End If


                If (.Approved_OT_Normal > .Planned_OT_Normal) And .Approved_OT_Normal > 0 Then

                    If txtNotes.Text.Trim() = "" Then
                        CtlCommon.ShowMessage(Me.Page, "Please Enter your Reason/Notes as the Approved Normal OT is greater than the Planned Normal OT ", "info")
                        Exit Sub
                    End If
                End If

                If (.Approved_OT_Rest > .Planned_OT_Rest) And .Approved_OT_Rest > 0 Then

                    If txtNotes.Text.Trim() = "" Then
                        CtlCommon.ShowMessage(Me.Page, "Please Enter your Reason/Notes as the Approved Rest OT is greater than the Planned Rest OT ", "info")
                        Exit Sub
                    End If
                End If

                err = .Update()


                If err = 0 Then
                    FillOTMangerGrid()
                    ' FillMyAppsGrid()
                    CtlCommon.ShowMessage(Me.Page, "Saved Successfully", "success")
                    Exit Sub
                Else
                    CtlCommon.ShowMessage(Me.Page, "Error while Saving", "error")
                    Exit Sub
                End If

            End With


            obj = Nothing


        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Protected Sub cmbDecision_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim gvr As GridViewRow = CType(sender, RadComboBox).Parent.Parent

            Dim txtJustificationText As TextBox = DirectCast(gdvManagerOTApps.Rows(gvr.RowIndex).FindControl("txtJustificationText"), TextBox)
            Dim cmbDecision As RadComboBox = DirectCast(gdvManagerOTApps.Rows(gvr.RowIndex).FindControl("cmbDecision"), RadComboBox)

            If cmbDecision.SelectedValue = 4 Then 'Request for Justification
                DirectCast(gdvManagerOTApps.Columns.Cast(Of DataControlField)().Where(Function(fld) fld.HeaderText = "Justification Text").SingleOrDefault(), DataControlField).Visible = True
            Else
                txtJustificationText.Text = ""
                DirectCast(gdvManagerOTApps.Columns.Cast(Of DataControlField)().Where(Function(fld) fld.HeaderText = "Justification Text").SingleOrDefault(), DataControlField).Visible = False
            End If

        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Protected Sub btnPrintPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim gvr As GridViewRow = CType(sender, Button).Parent.Parent

            Dim lblEmployeeId As Label = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("lblEmployeeId"), Label)
            Dim lblMonth As Label = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("lblMonth"), Label)
            Dim lblYear As Label = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("lblYear"), Label)

            CallPrint(lblEmployeeId.Text.Trim(), lblMonth.Text.Trim(), lblYear.Text.Trim(), "PDF")
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub
    Protected Sub btnPrintExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim gvr As GridViewRow = CType(sender, Button).Parent.Parent

            Dim lblEmployeeId As Label = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("lblEmployeeId"), Label)
            Dim lblMonth As Label = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("lblMonth"), Label)
            Dim lblYear As Label = DirectCast(gdvOverTimeSummary.Rows(gvr.RowIndex).FindControl("lblYear"), Label)

            CallPrint(lblEmployeeId.Text.Trim(), lblMonth.Text.Trim(), lblYear.Text.Trim(), "Excel")
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try
    End Sub

    Private Sub CallPrint(ByVal EmployeeID As Long, ByVal OverTimeMonth As Integer, ByVal OvertimeYear As Integer, ByVal ReportFormat As String)
        Try
            Dim str As String
            str = ""
            Dim strLast As String = "','window','height=' + (screen.height/2) + ', width=' + (screen.width/1.15) + ', toolbar=no, menubar=no,resizable=yes,location=no,scrollbars=yes,directories=no, status=no ' + mypos);"
            str = "var iX = (screen.width/4)-275; var iY = (screen.height/4)-150; var mypos = 'left='+iX+',top='+iY; var win = null; win = window.open('../Reports/OTReportViewer.aspx?EmployeeID=" & EmployeeID & "&OverTimeMonth=" & OverTimeMonth & "&ReportType=" & 1 & "&OvertimeYear=" & OvertimeYear & "&ReportFormat=" & ReportFormat
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, Guid.NewGuid.ToString, str + strLast, True)
        Catch ex As Exception
            CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message.ToString(), MethodBase.GetCurrentMethod.Name)
        End Try

    End Sub
End Class
