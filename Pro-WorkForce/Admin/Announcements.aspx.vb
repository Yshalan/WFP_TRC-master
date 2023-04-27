Imports System.Data
Imports TA.Employees
Imports SmartV.UTILITIES
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI
Imports TA.Admin
Imports TA.Security
Imports TA_Announcements

Partial Class Admin_Announcements
    Inherits System.Web.UI.Page




#Region "Class Variables"

    Private objAnnouncements As Announcements
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region
#Region "Properties"

    Private Property ID() As Integer
        Get
            Return ViewState("ID")
        End Get
        Set(ByVal value As Integer)
            ViewState("ID") = value
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
    Public Property AnnouncementEmployeeId() As Integer
        Get
            Return ViewState("AnnouncementEmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("AnnouncementEmployeeId") = value
        End Set
    End Property
    Public Property AnnouncementCompanyId() As Integer
        Get
            Return ViewState("AnnouncementCompanyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("AnnouncementCompanyId") = value
        End Set
    End Property
    Public Property AnnouncementEntityId() As Integer
        Get
            Return ViewState("AnnouncementEntityId")
        End Get
        Set(ByVal value As Integer)
            ViewState("AnnouncementEntityId") = value
        End Set
    End Property

#End Region

#Region "PageEvents"

    Protected Sub dgrdVwAnnouncements_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs) Handles dgrdVwAnnouncements.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdVwAnnouncements.Skin))
    End Function

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                dgrdVwAnnouncements.Columns(3).Visible = False
                dgrdVwAnnouncements.Columns(5).Visible = False
                dgrdVwAnnouncements.Columns(7).Visible = False
                dgrdVwAnnouncements.Columns(9).Visible = False
                dgrdVwAnnouncements.Columns(11).Visible = False
                dgrdVwAnnouncements.Columns(13).Visible = False
                dgrdVwAnnouncements.Columns(15).Visible = False
                dgrdVwAnnouncements.Columns(4).Visible = True
                dgrdVwAnnouncements.Columns(6).Visible = True
                dgrdVwAnnouncements.Columns(8).Visible = True
                dgrdVwAnnouncements.Columns(10).Visible = True
                dgrdVwAnnouncements.Columns(12).Visible = True
                dgrdVwAnnouncements.Columns(14).Visible = True
                dgrdVwAnnouncements.Columns(16).Visible = True

                dteDate.Culture = New System.Globalization.CultureInfo("ar-EG", False)
                dtpFromDate.Culture = New System.Globalization.CultureInfo("ar-EG", False)
                dtpToDate.Culture = New System.Globalization.CultureInfo("ar-EG", False)

            Else
                Lang = CtlCommon.Lang.EN
                dgrdVwAnnouncements.Columns(3).Visible = True
                dgrdVwAnnouncements.Columns(5).Visible = True
                dgrdVwAnnouncements.Columns(7).Visible = True
                dgrdVwAnnouncements.Columns(9).Visible = True
                dgrdVwAnnouncements.Columns(11).Visible = True
                dgrdVwAnnouncements.Columns(13).Visible = True
                dgrdVwAnnouncements.Columns(15).Visible = True
                dgrdVwAnnouncements.Columns(4).Visible = False
                dgrdVwAnnouncements.Columns(6).Visible = False
                dgrdVwAnnouncements.Columns(8).Visible = False
                dgrdVwAnnouncements.Columns(10).Visible = False
                dgrdVwAnnouncements.Columns(12).Visible = False
                dgrdVwAnnouncements.Columns(14).Visible = False
                dgrdVwAnnouncements.Columns(16).Visible = False

                dteDate.Culture = New System.Globalization.CultureInfo("en-US", False)
                dtpFromDate.Culture = New System.Globalization.CultureInfo("en-US", False)
                dtpToDate.Culture = New System.Globalization.CultureInfo("en-US", False)
            End If
            FillGridView()
            UserCtrlAnnouncements.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "إعلانات", "Announcements")
            rblLanguage.Items(0).Text = IIf(Lang = CtlCommon.Lang.AR, "لغة المستخدم", "User Language")
            rblLanguage.Items(1).Text = IIf(Lang = CtlCommon.Lang.AR, "الإنجليزية", "English")
            rblLanguage.Items(2).Text = IIf(Lang = CtlCommon.Lang.AR, "العربية", "Arabic")
            btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdVwAnnouncements.ClientID + "');")


            dteDate.DbSelectedDate = Date.Today
            dtpFromDate.DbSelectedDate = Date.Today
            dtpToDate.DbSelectedDate = Date.Today

            Dim formPath As String = Request.Url.AbsoluteUri
            Dim strArr() As String = formPath.Split("/")
            Dim objSysForms As New SYSForms
            dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
            For Each row As DataRow In dtCurrent.Rows
                If Not row("AllowAdd") = 1 Then
                    If Not IsDBNull(row("AddBtnName")) Then
                        If Not pnlAnnouncements.FindControl(row("AddBtnName")) Is Nothing Then
                            pnlAnnouncements.FindControl(row("AddBtnName")).Visible = False
                        End If
                    End If
                End If

                If Not row("AllowDelete") = 1 Then
                    If Not IsDBNull(row("DeleteBtnName")) Then
                        If Not pnlAnnouncements.FindControl(row("DeleteBtnName")) Is Nothing Then
                            pnlAnnouncements.FindControl(row("DeleteBtnName")).Visible = False
                        End If
                    End If
                End If

                If Not row("AllowSave") = 1 Then
                    If Not IsDBNull(row("EditBtnName")) Then
                        If Not pnlAnnouncements.FindControl(row("EditBtnName")) Is Nothing Then
                            pnlAnnouncements.FindControl(row("EditBtnName")).Visible = False
                        End If
                    End If
                End If

                If Not row("AllowPrint") = 1 Then
                    If Not IsDBNull(row("PrintBtnName")) Then
                        If Not pnlAnnouncements.FindControl(row("PrintBtnName")) Is Nothing Then
                            pnlAnnouncements.FindControl(row("PrintBtnName")).Visible = False
                        End If
                    End If
                End If
            Next

        End If

    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillGridView()
        ClearAll()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objAnnouncements = New Announcements()
        Dim errorNum As Integer = -1
        ' Set data into object for Add / Update

        With objAnnouncements
            If rblIsSpecificDate.SelectedValue = 1 Then
                .IsSpecificDate = True
                .AnnouncementDate = dteDate.SelectedDate
            Else
                .IsSpecificDate = False
                .FromDate = dtpFromDate.DbSelectedDate
                .ToDate = dtpToDate.DbSelectedDate
            End If

            .IsYearlyFixed = chkIsYearlyFixed.Checked
            .Title_En = txtEnglishTitle.Text()
            .Title_Ar = txtArabicTitle.Text()
            .Content_En = txtEnglishContent.Text()
            .Content_Ar = txtArabicContent.Text()
            .Created_By = SessionVariables.LoginUser.ID
            .Created_Date = DateTime.Now()
            .Fk_EmployeeId = EmployeeFilterUC.EmployeeId
            .Fk_CompanyId = EmployeeFilterUC.CompanyId

            If (EmployeeFilterUC.FilterType = "C") Then
                .Fk_EntityId = EmployeeFilterUC.EntityId
            ElseIf (EmployeeFilterUC.FilterType = "W") Then
                .Fk_WorklocationId = EmployeeFilterUC.EntityId
            Else
                .Fk_LogicalGroupId = EmployeeFilterUC.EntityId
            End If

        End With

        If ID = 0 Then
            ' Do add operation 
            errorNum = objAnnouncements.Add()

            If errorNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If

        Else
            ' Do update operations
            objAnnouncements.ID = ID
            If rblIsSpecificDate.SelectedValue = 1 Then
                objAnnouncements.IsSpecificDate = True
                objAnnouncements.AnnouncementDate = dteDate.SelectedDate
            Else
                objAnnouncements.IsSpecificDate = False
                objAnnouncements.FromDate = dtpFromDate.DbSelectedDate
                objAnnouncements.ToDate = dtpToDate.DbSelectedDate
            End If

            objAnnouncements.IsYearlyFixed = chkIsYearlyFixed.Checked
            objAnnouncements.Title_En = txtEnglishTitle.Text()
            objAnnouncements.Title_Ar = txtArabicTitle.Text()
            objAnnouncements.Content_En = txtEnglishContent.Text()
            objAnnouncements.Content_Ar = txtArabicContent.Text()
            objAnnouncements.Altered_By = SessionVariables.LoginUser.ID
            objAnnouncements.Altered_Date = DateTime.Now()
            objAnnouncements.Fk_EmployeeId = EmployeeFilterUC.hEmployeeId
            objAnnouncements.Fk_CompanyId = EmployeeFilterUC.CompanyId

            If (EmployeeFilterUC.FilterType = "C") Then
                objAnnouncements.Fk_EntityId = EmployeeFilterUC.EntityId
            ElseIf (EmployeeFilterUC.FilterType = "W") Then
                objAnnouncements.Fk_WorklocationId = EmployeeFilterUC.EntityId
            Else
                objAnnouncements.Fk_LogicalGroupId = EmployeeFilterUC.EntityId
            End If

            errorNum = objAnnouncements.Update()

            If errorNum = 0 Then

                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")

            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If
        End If


        If errorNum = 0 Then

            FillGridView()
            ClearAll()

        Else

        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdVwAnnouncements.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intDeleteId As Integer = row.GetDataKeyValue("ID").ToString().Trim
                ' Delete current checked item
                objAnnouncements = New Announcements()
                objAnnouncements.ID = intDeleteId
                errNum = objAnnouncements.Delete()
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If

        FillGridView()
        ClearAll()

    End Sub

    Protected Sub dgrdVwAnnouncements_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdVwAnnouncements.NeedDataSource
        Try
            objAnnouncements = New Announcements()
            objAnnouncements.ID = ID
            dgrdVwAnnouncements.DataSource = objAnnouncements.GetAll()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgrdVwAnnouncements_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdVwAnnouncements.SelectedIndexChanged
        If dgrdVwAnnouncements.SelectedItems.Count = 1 Then
            ID = CType(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("ID").ToString()

            If ((DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_EmployeeId").ToString()) <> "" And (DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_CompanyId").ToString()) <> "" And (DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_EntityId").ToString()) <> "") Then
                AnnouncementEmployeeId = Convert.ToInt32(DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_EmployeeId"))
                AnnouncementCompanyId = Convert.ToInt32(DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_CompanyId"))
                AnnouncementEntityId = Convert.ToInt32(DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_EntityId"))

            ElseIf ((DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_EmployeeId").ToString()) = "" And (DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_CompanyId").ToString()) <> "" And (DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_EntityId").ToString()) = "") Then

                AnnouncementCompanyId = Convert.ToInt32(DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_CompanyId"))
                AnnouncementEmployeeId = 0
                AnnouncementEntityId = 0
            ElseIf ((DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_EmployeeId").ToString()) = "" And (DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_CompanyId").ToString()) <> "" And (DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_EntityId").ToString()) <> "") Then
                AnnouncementEmployeeId = 0
                AnnouncementCompanyId = Convert.ToInt32(DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_CompanyId"))
                AnnouncementEntityId = Convert.ToInt32(DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_EntityId"))
            ElseIf ((DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_EmployeeId").ToString()) <> "" And (DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_CompanyId").ToString()) <> "" And (DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_EntityId").ToString()) = "") Then
                AnnouncementEmployeeId = Convert.ToInt32(DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_EmployeeId"))
                AnnouncementCompanyId = Convert.ToInt32(DirectCast(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("Fk_CompanyId"))
                AnnouncementEntityId = 0
            Else
                AnnouncementEmployeeId = 0
                AnnouncementCompanyId = 0
                AnnouncementEntityId = 0

            End If

            FillControls()



        End If
    End Sub

    Private Sub rblIsSpecificDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblIsSpecificDate.SelectedIndexChanged
        If rblIsSpecificDate.SelectedValue = 1 Then
            dvSpecificDate.Visible = True
            dvSpecificPeriod.Visible = False

            rfvFromDate.Enabled = False
            rfvToDate.Enabled = False
            cvDate.Enabled = False
        Else
            dvSpecificDate.Visible = False
            dvSpecificPeriod.Visible = True

            rfvFromDate.Enabled = True
            rfvToDate.Enabled = True
            cvDate.Enabled = True


        End If
    End Sub

    Private Sub chkIsYearlyFixed_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsYearlyFixed.CheckedChanged
        If chkIsYearlyFixed.Checked = True Then

            dteDate.Calendar.TitleFormat = "MMMM"
            dteDate.DateInput.DateFormat = "dd-MMMM"
            dteDate.DateInput.DisplayDateFormat = "dd-MMMM"

            dtpFromDate.Calendar.TitleFormat = "MMMM"
            dtpFromDate.DateInput.DateFormat = "dd-MMMM"
            dtpFromDate.DateInput.DisplayDateFormat = "dd-MMMM"

            dtpToDate.Calendar.TitleFormat = "MMMM"
            dtpToDate.DateInput.DateFormat = "dd-MMMM"
            dtpToDate.DateInput.DisplayDateFormat = "dd-MMMM"


        Else

            dteDate.Calendar.TitleFormat = "MMMM yyyy"
            dteDate.DateInput.DateFormat = "dd/MM/yyyy"
            dteDate.DateInput.DisplayDateFormat = "dd/MM/yyyy"

            dtpFromDate.Calendar.TitleFormat = "MMMM yyyy"
            dtpFromDate.DateInput.DateFormat = "dd/MM/yyyy"
            dtpFromDate.DateInput.DisplayDateFormat = "dd/MM/yyyy"

            dtpToDate.Calendar.TitleFormat = "MMMM yyyy"
            dtpToDate.DateInput.DateFormat = "dd/MM/yyyy"
            dtpToDate.DateInput.DisplayDateFormat = "dd/MM/yyyy"

        End If
    End Sub

    Private Sub dgrdVwAnnouncements_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdVwAnnouncements.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            If Not item("IsSpecificDate").Text = "&nbsp;" Then

                If item("IsSpecificDate").Text = "True" Then
                    If Not item("IsYearlyFixed").Text = "&nbsp;" Then
                        If item("IsYearlyFixed").Text = "True" Then
                            item("AnnouncementDate").Text = Convert.ToDateTime(item("AnnouncementDate").Text).ToString("MMM dd")
                        Else
                            item("AnnouncementDate").Text = Convert.ToDateTime(item("AnnouncementDate").Text).ToString("dd/MM/yyyy")
                        End If
                    End If

                Else
                    If Not item("IsYearlyFixed").Text = "&nbsp;" Then
                        If item("IsYearlyFixed").Text = "True" Then
                            item("AnnouncementDate").Text = Convert.ToDateTime(item("FromDate").Text).ToString("MMM dd") + " - " + Convert.ToDateTime(item("ToDate").Text).ToString("MMM dd")
                        Else
                            item("AnnouncementDate").Text = Convert.ToDateTime(item("FromDate").Text).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(item("ToDate").Text).ToString("dd/MM/yyyy")
                        End If
                    End If

                End If

            End If

            If Not item("IsYearlyFixed").Text = "&nbsp;" Then
                If item("IsYearlyFixed").Text = "1" Then

                Else

                End If
            End If

        End If

    End Sub

#End Region

#Region "Methods"

    Private Sub FillControls()
        objAnnouncements = New Announcements()
        objAnnouncements.ID = ID
        objAnnouncements.GetByPK()
        With objAnnouncements

            If .IsSpecificDate = True Then
                rblIsSpecificDate.SelectedValue = 1
                dteDate.DbSelectedDate = Convert.ToDateTime(.AnnouncementDate)

                dvSpecificDate.Visible = True
                dvSpecificPeriod.Visible = False

                rfvFromDate.Enabled = False
                rfvToDate.Enabled = False
                cvDate.Enabled = False

            Else
                rblIsSpecificDate.SelectedValue = 2
                dtpFromDate.DbSelectedDate = .FromDate
                dtpToDate.DbSelectedDate = .ToDate

                dvSpecificDate.Visible = False
                dvSpecificPeriod.Visible = True

                rfvFromDate.Enabled = True
                rfvToDate.Enabled = True
                cvDate.Enabled = True

            End If
            chkIsYearlyFixed.Checked = .IsYearlyFixed

            If .IsYearlyFixed = True Then

                dteDate.Calendar.TitleFormat = "MMMM"
                dteDate.DateInput.DateFormat = "dd-MMMM"
                dteDate.DateInput.DisplayDateFormat = "dd-MMMM"

                dtpFromDate.Calendar.TitleFormat = "MMMM"
                dtpFromDate.DateInput.DateFormat = "dd-MMMM"
                dtpFromDate.DateInput.DisplayDateFormat = "dd-MMMM"

                dtpToDate.Calendar.TitleFormat = "MMMM"
                dtpToDate.DateInput.DateFormat = "dd-MMMM"
                dtpToDate.DateInput.DisplayDateFormat = "dd-MMMM"


            Else

                dteDate.Calendar.TitleFormat = "MMMM yyyy"
                dteDate.DateInput.DateFormat = "dd/MM/yyyy"
                dteDate.DateInput.DisplayDateFormat = "dd/MM/yyyy"

                dtpFromDate.Calendar.TitleFormat = "MMMM yyyy"
                dtpFromDate.DateInput.DateFormat = "dd/MM/yyyy"
                dtpFromDate.DateInput.DisplayDateFormat = "dd/MM/yyyy"

                dtpToDate.Calendar.TitleFormat = "MMMM yyyy"
                dtpToDate.DateInput.DateFormat = "dd/MM/yyyy"
                dtpToDate.DateInput.DisplayDateFormat = "dd/MM/yyyy"

            End If

            txtEnglishTitle.Text() = .Title_En
            txtArabicTitle.Text() = .Title_Ar

            txtEnglishContent.Text() = .Content_En
            txtArabicContent.Text() = .Content_Ar

            If (AnnouncementEmployeeId <> 0 And AnnouncementCompanyId <> 0 And AnnouncementEntityId <> 0) Then

                EmployeeFilterUC.EmployeeId = AnnouncementEmployeeId
                EmployeeFilterUC.CompanyId = AnnouncementCompanyId
                EmployeeFilterUC.EntityId = AnnouncementEntityId
                EmployeeFilterUC.IsEntityClick = "True"
                EmployeeFilterUC.GetEmployeeInfo(AnnouncementEmployeeId)

            ElseIf (AnnouncementEmployeeId = 0 And AnnouncementCompanyId <> 0 And AnnouncementEntityId = 0) Then

                EmployeeFilterUC.ClearValuesAnnouncements()
                EmployeeFilterUC.GetSelectedCompany(AnnouncementCompanyId)
                EmployeeFilterUC.CompanyId = AnnouncementCompanyId

            ElseIf (AnnouncementEmployeeId = 0 And AnnouncementCompanyId <> 0 And AnnouncementEntityId <> 0) Then
                EmployeeFilterUC.ClearValuesAnnouncements()
                EmployeeFilterUC.GetSelectedEntity(AnnouncementEntityId)
                EmployeeFilterUC.GetSelectedCompany(AnnouncementCompanyId)
                EmployeeFilterUC.CompanyId = AnnouncementCompanyId
                EmployeeFilterUC.EntityId = AnnouncementEntityId
            ElseIf (AnnouncementEmployeeId <> 0 And AnnouncementCompanyId <> 0 And AnnouncementEntityId = 0) Then
                EmployeeFilterUC.ClearValuesAnnouncements()
                EmployeeFilterUC.GetSelectedCompany(AnnouncementCompanyId)
                EmployeeFilterUC.GetEmployeeNameWithoutEntity(AnnouncementEmployeeId)
                EmployeeFilterUC.EmployeeId = AnnouncementEmployeeId
                EmployeeFilterUC.CompanyId = AnnouncementCompanyId

            Else
                EmployeeFilterUC.ClearValuesAnnouncements()
            End If


        End With
    End Sub

    Private Sub FillGridView()
        Try

            objAnnouncements = New Announcements()
            objAnnouncements.ID = ID
            dgrdVwAnnouncements.DataSource = objAnnouncements.GetAll()
            dgrdVwAnnouncements.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClearAll()
        'Clear Controls
        txtEnglishTitle.Text() = String.Empty
        txtArabicTitle.Text() = String.Empty
        dteDate.Clear()
        txtEnglishContent.Text() = String.Empty
        txtArabicContent.Text() = String.Empty
        EmployeeFilterUC.ClearValues()
        ' Reset to prepare to the next add operation'
        rblIsSpecificDate.SelectedValue = 1
        dvSpecificDate.Visible = True
        dvSpecificPeriod.Visible = False

        rfvFromDate.Enabled = False
        rfvToDate.Enabled = False
        cvDate.Enabled = False

        chkIsYearlyFixed.Checked = False

        dteDate.Calendar.TitleFormat = "MMMM yyyy"
        dteDate.DateInput.DateFormat = "dd/MM/yyyy"
        dteDate.DateInput.DisplayDateFormat = "dd/MM/yyyy"

        dtpFromDate.Calendar.TitleFormat = "MMMM yyyy"
        dtpFromDate.DateInput.DateFormat = "dd/MM/yyyy"
        dtpFromDate.DateInput.DisplayDateFormat = "dd/MM/yyyy"

        dtpToDate.Calendar.TitleFormat = "MMMM yyyy"
        dtpToDate.DateInput.DateFormat = "dd/MM/yyyy"
        dtpToDate.DateInput.DisplayDateFormat = "dd/MM/yyyy"

        dteDate.DbSelectedDate = Date.Today
        dtpFromDate.DbSelectedDate = Date.Today
        dtpToDate.DbSelectedDate = Date.Today

        ID = 0
    End Sub


#End Region



End Class
