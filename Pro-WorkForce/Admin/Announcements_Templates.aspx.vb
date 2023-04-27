Imports System.Data
Imports TA.Employees
Imports SmartV.UTILITIES
Imports System.Web.UI.WebControls
Imports Telerik.Web.UI
Imports TA.Admin
Imports TA.Security
Imports TA_AnnouncementsTemplates
Imports TA.Definitions

Partial Class Admin_AnnouncementsTemplates
    Inherits System.Web.UI.Page



#Region "Class Variables"

    Private objAnnouncements As AnnouncementTemplates
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Private objProjectCommon As New ProjectCommon
    Private objLeavesTypes As LeavesTypes
    Private objHolidayTypes As Holiday
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
            FillTemplateType()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                dgrdVwAnnouncements.Columns(6).Visible = True
                dgrdVwAnnouncements.Columns(7).Visible = True
                dgrdVwAnnouncements.Columns(9).Visible = True
                dgrdVwAnnouncements.Columns(11).Visible = True
                dgrdVwAnnouncements.Columns(4).Visible = False
                dgrdVwAnnouncements.Columns(5).Visible = False
                dgrdVwAnnouncements.Columns(8).Visible = False
                dgrdVwAnnouncements.Columns(10).Visible = False

            Else
                Lang = CtlCommon.Lang.EN
                dgrdVwAnnouncements.Columns(6).Visible = False
                dgrdVwAnnouncements.Columns(7).Visible = False
                dgrdVwAnnouncements.Columns(9).Visible = False
                dgrdVwAnnouncements.Columns(11).Visible = False
                dgrdVwAnnouncements.Columns(4).Visible = True
                dgrdVwAnnouncements.Columns(5).Visible = True
                dgrdVwAnnouncements.Columns(8).Visible = True
                dgrdVwAnnouncements.Columns(10).Visible = True

            End If
            UserCtrlAnnouncements.HeaderText = IIf(Lang = CtlCommon.Lang.AR, "نماذج الإعلانات", "Announcements Templates")
            lblLeaveType.Text = IIf(Lang = CtlCommon.Lang.AR, "نوع الإجازة", "Leave Type")
            lblHolidayType.Text = IIf(Lang = CtlCommon.Lang.AR, "نوع العطلة الرسمية", "Holiday Type")
            rblIsStart.Items(0).Text = IIf(Lang = CtlCommon.Lang.AR, " عند بداية الإجازة", "At leave start")
            rblIsStart.Items(1).Text = IIf(Lang = CtlCommon.Lang.AR, "عند العودة من الإجازة", "When comeback from leave")
            btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdVwAnnouncements.ClientID + "');")

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
    Protected Sub RadComboTemplateType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboTemplateType.SelectedIndexChanged
        If RadComboTemplateType.SelectedValue = 6 Then
            trLeaveType.Visible = True
            trrblIsStart.Visible = True
            trHolidayType.Visible = False
            FillLeaveType()
        ElseIf RadComboTemplateType.SelectedValue = 7 Then
            trHolidayType.Visible = True
            trLeaveType.Visible = False
            trrblIsStart.Visible = False
            FillHolidayType()
        Else
            trLeaveType.Visible = False
            trHolidayType.Visible = False
            trrblIsStart.Visible = False

        End If
    End Sub
    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillGridView()
        ClearAll()
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objAnnouncements = New AnnouncementTemplates()
        Dim errorNum As Integer = -1
        ' Set data into object for Add / Update

        With objAnnouncements
            If (RadComboTemplateType.SelectedValue = 6) Then
                .announcementType = RadComboTemplateType.SelectedValue
                .FK_leaveType = RadComboLeaveType.SelectedValue
                .AtLeaveStart = rblIsStart.SelectedValue
                .TitleEn = txtEnglishTitle.Text()
                .TitleAr = txtArabicTitle.Text()
                .TextEn = txtEnglishContent.Text()
                .TextAr = txtArabicContent.Text()
            ElseIf (RadComboTemplateType.SelectedValue = 7) Then
                .announcementType = RadComboTemplateType.SelectedValue
                .FK_HolidayId = RadComboHolidayType.SelectedValue
                .TitleEn = txtEnglishTitle.Text()
                .TitleAr = txtArabicTitle.Text()
                .TextEn = txtEnglishContent.Text()
                .TextAr = txtArabicContent.Text()
            Else
                .announcementType = RadComboTemplateType.SelectedValue
                .TitleEn = txtEnglishTitle.Text()
                .TitleAr = txtArabicTitle.Text()
                .TextEn = txtEnglishContent.Text()
                .TextAr = txtArabicContent.Text()
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
            objAnnouncements.TemplateId = ID

            If (RadComboTemplateType.SelectedValue = 6) Then
                objAnnouncements.announcementType = RadComboTemplateType.SelectedValue
                objAnnouncements.FK_leaveType = RadComboLeaveType.SelectedValue
                objAnnouncements.AtLeaveStart = rblIsStart.SelectedValue
                objAnnouncements.TitleEn = txtEnglishTitle.Text()
                objAnnouncements.TitleAr = txtArabicTitle.Text()
                objAnnouncements.TextEn = txtEnglishContent.Text()
                objAnnouncements.TextAr = txtArabicContent.Text()
            ElseIf (RadComboTemplateType.SelectedValue = 7) Then
                objAnnouncements.announcementType = RadComboTemplateType.SelectedValue
                objAnnouncements.FK_HolidayId = RadComboHolidayType.SelectedValue
                objAnnouncements.TitleEn = txtEnglishTitle.Text()
                objAnnouncements.TitleAr = txtArabicTitle.Text()
                objAnnouncements.TextEn = txtEnglishContent.Text()
                objAnnouncements.TextAr = txtArabicContent.Text()
                objAnnouncements.AtLeaveStart = Nothing
            Else
                objAnnouncements.announcementType = RadComboTemplateType.SelectedValue
                objAnnouncements.TitleEn = txtEnglishTitle.Text()
                objAnnouncements.TitleAr = txtArabicTitle.Text()
                objAnnouncements.TextEn = txtEnglishContent.Text()
                objAnnouncements.TextAr = txtArabicContent.Text()
            End If
            errorNum = objAnnouncements.Update()
            If errorNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
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
                Dim intDeleteId As Integer = row.GetDataKeyValue("TemplateID").ToString().Trim
                ' Delete current checked item
                objAnnouncements = New AnnouncementTemplates()
                objAnnouncements.TemplateId = intDeleteId
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
            objAnnouncements = New AnnouncementTemplates()
            objAnnouncements.TemplateId = ID
            dgrdVwAnnouncements.DataSource = objAnnouncements.GetAll()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgrdVwAnnouncements_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdVwAnnouncements.SelectedIndexChanged
        If dgrdVwAnnouncements.SelectedItems.Count = 1 Then
            objAnnouncements = New AnnouncementTemplates()
            ID = CType(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("TemplateID").ToString()
            If Not (CType(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("LeaveType").ToString() = "") Then
                objAnnouncements.LeaveType = CType(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("LeaveType").ToString()
            End If
            If Not (CType(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("LeaveTypeAr").ToString() = "") Then
                objAnnouncements.LeaveTypeAr = CType(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("LeaveTypeAr").ToString()
            End If
            If Not (CType(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("HolidayType").ToString() = "") Then
                objAnnouncements.HolidayType = CType(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("HolidayType").ToString()
            End If
            If Not (CType(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("HolidayTypeAr").ToString() = "") Then
                objAnnouncements.HolidayTypeAr = CType(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("HolidayTypeAr").ToString()
            End If
            If Not (CType(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("AtLeaveStart").ToString() = "") Then
                objAnnouncements.AtLeaveStart = CType(dgrdVwAnnouncements.SelectedItems(0), GridDataItem).GetDataKeyValue("AtLeaveStart").ToString()
            End If

            FillControls()



        End If
    End Sub
#End Region

#Region "Methods"

    Private Sub FillControls()
        objAnnouncements = New AnnouncementTemplates()
        objAnnouncements.TemplateId = ID
        objAnnouncements.GetByPK()
        With objAnnouncements
            If (.announcementType = 6) Then
                trLeaveType.Visible = True
                trrblIsStart.Visible = True
                trHolidayType.Visible = False
                RadComboTemplateType.SelectedValue = 6
                FillLeaveType()
                RadComboLeaveType.SelectedItem.Text = .LeaveType
                RadComboLeaveType.SelectedValue = .FK_leaveType
                If (.AtLeaveStart = False) Then
                    rblIsStart.SelectedValue = 0
                Else
                    rblIsStart.SelectedValue = 1
                End If


            ElseIf (.announcementType = 7) Then
                RadComboTemplateType.SelectedValue = 7
                trLeaveType.Visible = False
                trrblIsStart.Visible = False
                trHolidayType.Visible = True
                FillHolidayType()
                RadComboHolidayType.SelectedItem.Text = .HolidayType
                RadComboHolidayType.SelectedValue = .FK_HolidayId


            Else
                RadComboTemplateType.SelectedValue = .announcementType
                trLeaveType.Visible = False
                trrblIsStart.Visible = False
                trHolidayType.Visible = False
            End If

            txtEnglishTitle.Text() = .TitleEn
            txtArabicTitle.Text() = .TitleAr

            txtEnglishContent.Text() = .TextEn
            txtArabicContent.Text() = .TextAr


        End With
    End Sub

    Private Sub FillGridView()
        Try

            objAnnouncements = New AnnouncementTemplates()
            objAnnouncements.TemplateId = ID
            dgrdVwAnnouncements.DataSource = objAnnouncements.GetAll()
            dgrdVwAnnouncements.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ClearAll()
        'Clear Controls
        txtEnglishTitle.Text() = String.Empty
        txtArabicTitle.Text() = String.Empty

        txtEnglishContent.Text() = String.Empty
        txtArabicContent.Text() = String.Empty

        ' Reset to prepare to the next add operation'
        ID = 0
    End Sub

    Private Sub FillTemplateType()
        Dim item1 As New RadComboBoxItem
        Dim item2 As New RadComboBoxItem
        Dim item3 As New RadComboBoxItem
        Dim item4 As New RadComboBoxItem
        Dim item5 As New RadComboBoxItem
        Dim item6 As New RadComboBoxItem
        Dim item7 As New RadComboBoxItem
        Dim item8 As New RadComboBoxItem


        item1.Value = -1
        item1.Text = IIf(Lang = CtlCommon.Lang.AR, "--الرجاء الاختيار--", "--Please Select--")
        RadComboTemplateType.Items.Add(item1)


        item2.Value = 1
        item2.Text = IIf(Lang = CtlCommon.Lang.AR, "تاريخ الميلاد", "Birthday")
        RadComboTemplateType.Items.Add(item2)

        item3.Value = 2
        item3.Text = IIf(Lang = CtlCommon.Lang.AR, "ذكرى سنوية", "Anniversary")
        RadComboTemplateType.Items.Add(item3)

        item4.Value = 3
        item4.Text = IIf(Lang = CtlCommon.Lang.AR, "موظف جديد", "New Joinee")
        RadComboTemplateType.Items.Add(item4)

        item5.Value = 4
        item5.Text = IIf(Lang = CtlCommon.Lang.AR, "السنة الجديدة", "New Year")
        RadComboTemplateType.Items.Add(item5)

        item6.Value = 5
        item6.Text = IIf(Lang = CtlCommon.Lang.AR, "رمضان", "Ramdan")
        RadComboTemplateType.Items.Add(item6)

        item7.Value = 6
        item7.Text = IIf(Lang = CtlCommon.Lang.AR, "إجازات", "Leaves")
        RadComboTemplateType.Items.Add(item7)

        item8.Value = 7
        item8.Text = IIf(Lang = CtlCommon.Lang.AR, "العطل الرسمية", "Holidays")
        RadComboTemplateType.Items.Add(item8)


    End Sub
    Private Sub FillLeaveType()
        objProjectCommon = New ProjectCommon()
        Dim dt As DataTable = Nothing
        dt = Nothing
        objLeavesTypes = New LeavesTypes()
        dt = objLeavesTypes.GetAllForDDL
        ProjectCommon.FillRadComboBox(RadComboLeaveType, dt, "LeaveName", _
                                     "LeaveArabicName", "LeaveId")



    End Sub
    Private Sub FillHolidayType()
        objProjectCommon = New ProjectCommon()
        Dim dt As DataTable = Nothing
        dt = Nothing
        objHolidayTypes = New Holiday()
        dt = objHolidayTypes.GetAll()
        ProjectCommon.FillRadComboBox(RadComboHolidayType, dt, "HolidayName", _
                                     "HolidayArabicName", "HolidayId")
    End Sub
#End Region



End Class
