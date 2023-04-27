Imports SmartV.UTILITIES
Imports System.Data
Imports SmartV.Security
Imports SmartV.DB
Imports SmartV.Security.UI
Imports System.Threading
Imports TA.Security
Imports TA.Security.UI
Imports Telerik.Web.UI
Imports SmartV.UTILITIES.ProjectCommon
Imports System.Web.UI.WebControls
Partial Class DefineGroup
    Inherits System.Web.UI.Page

#Region "Page Variables"

    Private _sysGroup As SYSGroups
    Private _sysForms As SYSForms
    Private _sysPrivilege As SYSPrivilege
    Private _sysModules As SYSModules
    Private chkList As CheckBoxList
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Public lang As Integer
    Dim tabcontainer As AjaxControlToolkit.TabContainer
    Private moduleTabs As List(Of SmartV.Security.UI.AISTabPanel) = New List(Of SmartV.Security.UI.AISTabPanel)
    Protected dir, textalign As String

    'Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.String", System.Reflection.Assembly.Load("App_GlobalResources"))
    ' Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    'Private rmVr As New System.Resources.ResourceManager("Resources.Messages", System.Reflection.Assembly.Load("App_GlobalResources"))

    'Public GroupExist, FailedToDeleteGroup, GroupDeleted, FailedToDeletePrivileges, NoDataAvailable, AreYouSureyouwanttoDelete, PleaseSelectfromList, SavedSuccessfully, UpdatedSuccessfully, OperationFailed As String

#End Region

#Region "Properties"

    Public Property Group_Id() As Integer
        Get
            Return ViewState("ID") 'hdnid.Value
        End Get
        Set(ByVal value As Integer)
            'hdnid.Value = value
            ViewState("ID") = value
        End Set
    End Property

    Private Property SortDir() As String
        Get
            Return hdnsortDir.Value
        End Get
        Set(ByVal value As String)
            hdnsortDir.Value = value
        End Set
    End Property

    Private Property SortExepression() As String
        Get
            Return hdnsortExp.Value
        End Get
        Set(ByVal value As String)
            hdnsortExp.Value = value
        End Set
    End Property

    Private Property data() As DataTable
        Get
            Return ViewState("dt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dt") = value
        End Set
    End Property

    Private Property OldPrivilages() As DataTable
        Get
            Return ViewState("OldPrivilages")
        End Get
        Set(ByVal value As DataTable)
            ViewState("OldPrivilages") = value
        End Set
    End Property

    Private Property NewPrivilages() As DataTable
        Get
            Return ViewState("NewPrivilages")
        End Get
        Set(ByVal value As DataTable)
            ViewState("NewPrivilages") = value
        End Set
    End Property

    Private Property SortExep() As String
        Get
            Return ViewState("sortExp")
        End Get
        Set(ByVal value As String)
            ViewState("sortExp") = value
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

#End Region

#Region "Page Events"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If (SessionVariables.CultureInfo Is Nothing) Then
            SessionVariables.CultureInfo = "en-US"
            dir = "ltr"
            textalign = "left"
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
                dir = "ltr"
                textalign = "left"
            Else
                SessionVariables.CultureInfo = "ar-JO"
                dir = "rtl"
                textalign = "right"
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '****By Mohammad Shanaa 17/05/2011 For Pages Authentications
        'Dim FormID As String = "22"
        'If SessionVariables.Sys_LoginUser Is Nothing Then
        '    Response.Redirect("~/default/Logout.aspx")
        'End If
        'If Not SmartV.Security.SYSForms.IsAccessible(FormID) Then
        '    Response.Redirect("~/default/Logout.aspx")
        'End If
        'If SessionVariables.LoginUser.IsFirstLogin = 0 Then
        '    Response.Redirect("../Security/ChangePassword.aspx?Change=True", False)
        'End If
        '***********************
        loadCaptions()
        CtlTab.Controls.Add(tabcontainer)
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

        ''CType(Master.FindControl("PageText"), Label).Text = "6.1"
        'If (SessionVariables.CultureInfo = "en-US") Then
        '    lang = CtlCommon.Lang.EN
        'Else
        '    lang = CtlCommon.Lang.AR
        'End If
        If Not Page.IsPostBack Then

            LoadGrid()
            PageHeader1.HeaderText = ResourceManager.GetString("DefineUserGroups", CultureInfo)
            Page.Title = "Work Force-Pro : : " + ResourceManager.GetString("DefineUserGroups", CultureInfo)
            'pnlPermissions.Visible = False

        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" & gvGroups.ClientID & "');")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("AddBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("EditBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("PrintBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
        loadTabs()

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click, btnCreate.Click
        Dim dac As DAC = dac.getDAC
        _sysGroup = New SYSGroups
        _sysPrivilege = New SYSPrivilege
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errNo As Integer
        With _sysGroup
            .EnglishName = txtGroupEnName.Text
            .ArabicName = txtGroupArName.Text
            '.fdon = True
            .DefaultPage = rblDefaultPage.SelectedValue

            If Group_Id = 0 Then
                errNo = .Add()
            Else
                _sysGroup.GroupId = Group_Id
                errNo = .update()
            End If
        End With

        If errNo = -11 Then

            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("Groupalreadyexists", CultureInfo), "info")
            Exit Sub
        End If

        If errNo <> 0 Then


            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("OperationFailed", CultureInfo), "error")
            Exit Sub
        End If

        With _sysPrivilege
            .GroupId = _sysGroup.GroupId
            If (chkListPermessions.Items(0).Selected) Then
                .SavePerv = 1
            Else
                .SavePerv = 0
            End If

            If (chkListPermessions.Items(1).Selected) Then
                .DeletePerv = 1
            Else
                .DeletePerv = 0
            End If

            If (chkListPermessions.Items(2).Selected) Then
                .ApprovePerv = 1
            Else
                .ApprovePerv = 0
            End If

            If (chkListPermessions.Items(3).Selected) Then
                .PrintPerv = 1
            Else
                .PrintPerv = 0
            End If

            errNo = .Delete
            For Each myTab As AISTabPanel In moduleTabs
                For Each chkBox As AISListItem In myTab.items
                    If chkBox.myItem.Selected Then
                        .FormId = chkBox.formID
                        If Group_Id = 0 Then
                            errNo = .Add()
                        Else
                            _sysPrivilege.GroupId = Group_Id
                            errNo = .Update()
                        End If
                        If errNo <> 0 Then

                            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("OperationFailed", CultureInfo), "error")
                            Exit Sub
                        End If
                    End If
                Next
            Next
        End With

        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
        LoadGrid()
        ClearAll()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim dac As DAC = dac.getDAC
        _sysGroup = New SYSGroups
        _sysPrivilege = New SYSPrivilege
        Dim errNo As Integer
        Dim errnum As Integer
        For Each Item As GridDataItem In gvGroups.Items
            If CType(Item.FindControl("chkGroup"), CheckBox).Checked Then

                _sysPrivilege.GroupId = Item("GroupID").Text
                errnum += _sysPrivilege.Delete()

                If errnum <> 0 Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("FailedPreviligies", CultureInfo) & CType(Item.FindControl("lblGroupID"), Label).Text, "info")
                    Exit Sub
                End If

                _sysGroup.GroupId = Item("GroupID").Text
                errNo += _sysGroup.delete
            End If
        Next

        If errNo = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully"), "success")
            LoadGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete"), "error")

        End If
        ClearAll()
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        gvGroups.SelectedIndexes.Clear()
        ClearAll()
    End Sub

    Protected Sub gvGroups_PageIndexChanging(ByVal sender As Object, ByVal e As GridPageChangedEventArgs) Handles gvGroups.PageIndexChanged

        gvGroups.CurrentPageIndex = e.NewPageIndex
        Dim dv As New DataView(data)
        dv.Sort = SortExepression
        gvGroups.DataSource = dv
        gvGroups.DataBind()
        CtlCommon.ClearCtlContent(New WebControl() {txtGroupArName}, CtlCommon.OpMode.ClearContent)
        CtlCommon.ClearCtlContent(New WebControl() {txtGroupEnName}, CtlCommon.OpMode.ClearContent)

    End Sub

    Protected Sub gvGroups_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles gvGroups.NeedDataSource

        _sysGroup = New SYSGroups
        data = _sysGroup.GetAll()
        gvGroups.DataSource = data
        Dim dv As New DataView(data)
        dv.Sort = SortExepression
        gvGroups.DataSource = dv

    End Sub

    Protected Sub gvGroups_SelectedIndexChanges(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvGroups.SelectedIndexChanged
        ClearAll()
        'LoadGrid()

        Group_Id = CInt(DirectCast(gvGroups.SelectedItems(0), GridDataItem).GetDataKeyValue("GroupID").ToString())
       
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim tabHeader As AISTabHeader = New AISTabHeader
        _sysGroup = New SYSGroups
        Dim chkd As Boolean = True
        Dim dt As DataTable
        _sysGroup.GroupId = Group_Id
        _sysGroup.GetGroup()
        txtGroupEnName.Text = _sysGroup.EnglishName
        txtGroupArName.Text = _sysGroup.ArabicName
        rblDefaultPage.SelectedValue = _sysGroup.DefaultPage
        'chkActive.Checked = _secRole.Active
        dt = _sysGroup.GetPrivileges()
        OldPrivilages = dt
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dRow As DataRow In dt.Rows
                Dim formID As Integer = dRow.Item(1)
                Dim moduleID As Integer = dRow.Item(2)
                If Not checkForm(moduleID, formID) Then
                    chkd = False
                End If
            Next

            chkListPermessions.Items(0).Selected = Convert.ToBoolean(dt.Rows(0).Item(3))
            chkListPermessions.Items(1).Selected = Convert.ToBoolean(dt.Rows(0).Item(5))
            chkListPermessions.Items(2).Selected = Convert.ToBoolean(dt.Rows(0).Item(4))
            chkListPermessions.Items(3).Selected = Convert.ToBoolean(dt.Rows(0).Item(6))
        End If

        For Each myTab As AISTabPanel In moduleTabs
            For Each chkBox As AISListItem In myTab.items
                If chkBox.myItem.Selected Then
                    chkd = True
                Else
                    chkd = False
                    Exit For
                End If
            Next
        Next

    End Sub

    'Protected Sub gvRoles_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvGroups.DataBound
    '    Arrow()
    'End Sub

#End Region

#Region "Page Methods"

    Private Sub loadCaptions()
        Dim CultureInfo As New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Me.Page.Form.Attributes.Add("dir", "dir")
        Dim cssFile = New HtmlLink()
        'cssFile.Href = "../css/" & ResourceManager.GetString("CssFileName", CultureInfo)
        cssFile.Attributes("rel") = "stylesheet"
        cssFile.Attributes("type") = "text/css"
        cssFile.Attributes("media") = "all"
        Me.Page.Header.Controls.Add(cssFile)
        MyBase.InitializeCulture()
    End Sub

    Protected Overrides Sub InitializeCulture()
        Dim _culture As String = SessionVariables.CultureInfo
        If (String.IsNullOrEmpty(_culture)) Then
            _culture = "en-US"
            Me.UICulture = _culture
            Me.Culture = _culture
        End If
        If (_culture <> "Auto") Then
            Dim ci As New System.Globalization.CultureInfo(_culture)
            System.Threading.Thread.CurrentThread.CurrentCulture = ci
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci
        End If
        MyBase.InitializeCulture()
    End Sub

    Private Function checkForm(ByVal moduleID As Integer, ByVal formID As Integer) As Boolean
        Dim ltabHeader As AISTabHeader = New AISTabHeader
        For Each myTab As AISTabPanel In moduleTabs
            If (myTab.moduleID = moduleID) Then
                For Each chkBox As AISListItem In myTab.items
                    If (chkBox.formID = formID) Then
                        chkBox.myItem.Selected = True
                    End If
                Next
            End If
        Next
    End Function

    Private Sub LoadGrid()

        _sysGroup = New SYSGroups
        'data = _secRole.Get_fdon_All

        data = _sysGroup.GetAll()
        gvGroups.DataSource = data
        gvGroups.DataBind()

    End Sub

    Private Sub loadTabs()
        tabcontainer = New AjaxControlToolkit.TabContainer
        tabcontainer.CssClass = "SvAjaxTabStyle"
        Dim countZones As Integer = 0

        Dim StrForms As String
        Dim objSecurity As New STSupremeKeyValidation.STSupremeKeyValidation.SmartKeyValidation
        Dim BuildMenu As New SmartV.Security.MENU.SYSMenuCreator
        StrForms = (SessionVariables.LicenseDetails.FormIds)



        _sysModules = New SYSModules
        _sysPrivilege = New SYSPrivilege

        Dim dt As DataTable
        Dim dtPaivilege As DataTable = Nothing
        dt = BuildMenu.GetModuleByFormId(StrForms)
        'dt = _sysModules.load()
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1

                Dim tab As AISTabPanel = New AISTabPanel
                tab.Visible = True
                tab.moduleID = dt.Rows(i).Item(0)
                'If tab.moduleID <> 3 Then

                chkList = New CheckBoxList
                If SessionVariables.CultureInfo = "en-US" Then
                    dtPaivilege = _sysPrivilege.GetFormsObj(dt.Rows(i).Item(0), CtlCommon.Lang.EN)
                Else
                    dtPaivilege = _sysPrivilege.GetFormsObj(dt.Rows(i).Item(0), CtlCommon.Lang.AR)
                End If
                If dtPaivilege IsNot Nothing AndAlso dtPaivilege.Rows.Count > 0 Then
                    For j As Integer = 0 To dtPaivilege.Rows.Count - 1
                        Dim list As New AISListItem
                        If countZones > 0 Then
                            'list.myItem.Text = dtPaivilege.Rows(j).Item(1).ToString & " <a href='#' onclick='return javascript:windowOO('" & dtPaivilege.Rows(j).Item(0) & "') target='_blank'  > ..Zones.. </a>"
                            If SessionVariables.CultureInfo = "en-US" Then
                                list.myItem.Text = dtPaivilege.Rows(j).Item(1).ToString & " <a OnClick='javascript:newwindow(" & dtPaivilege.Rows(j).Item(0) & ")' style='text-decoration:underline;cursor:hand;' target='_blank'  > ..Zones.. </a>"
                            Else
                                list.myItem.Text = dtPaivilege.Rows(j).Item(1).ToString & " <a OnClick='javascript:newwindow(" & dtPaivilege.Rows(j).Item(0) & ")' style='text-decoration:underline;cursor:hand;' target='_blank'  > ..أقسام الصفحة.. </a>"
                            End If

                            'list.myItem.Text = dtPaivilege.Rows(j).Item(1).ToString & " <input id='1' type='button' onclick='javascript:window.open('Zones.aspx','popup','width=370,height=230,left=175,top=175,resizable=no');' value='..Zones..' /> "
                        Else
                            If StrForms.Contains(dtPaivilege.Rows(j).Item(0)) = True Then
                                list.myItem.Text = dtPaivilege.Rows(j).Item(1).ToString
                            End If
                        End If

                        If StrForms.Contains(dtPaivilege.Rows(j).Item(0)) = True Then
                            list.myItem.Value = dtPaivilege.Rows(j).Item(0)
                            list.formID = dtPaivilege.Rows(j).Item(0)
                            If dt.Rows(i).Item(0).ToString() = "4" Or dt.Rows(i).Item(0).ToString() = "8" Or dt.Rows(i).Item(0).ToString() = "9" Then
                                If dtPaivilege.Rows(j).Item(2).ToString = "0" Then
                                    list.myItem.Text = "<b><font size=3>" & list.myItem.Text & "</font></b>"
                                Else
                                    list.myItem.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & list.myItem.Text
                                End If
                            End If

                            tab.items.Add(list)
                            chkList.Items.Add(list.myItem)
                            list = Nothing
                        End If


                    Next

                    countZones = 0
                    chkList.RepeatDirection = RepeatDirection.Vertical
                    chkList.RepeatColumns = 2
                    chkList.RepeatLayout = RepeatLayout.Table
                    chkList.AutoPostBack = False
                    chkList.Visible = True

                    ' AddHandler chkList.SelectedIndexChanged, AddressOf chkList_SelectedIndexChanged
                    chkList.ID = "cbl" & dt.Rows(i).Item(1).ToString & i


                    tab.Controls.Add(chkList)
                    'End If
                    'tab.HeaderTemplate = New AISTabHeader(i, dt.Rows(i).Item(2).ToString)
                    tab.HeaderText = dt.Rows(i).Item(1).ToString
                    tabcontainer.Tabs.Add(tab)
                    moduleTabs.Add(tab)
                    Dim objTabHeader As New AISTabHeader
                End If
            Next
        End If
        tabcontainer.Width = Unit.Percentage(100)
    End Sub

    Protected Overrides Sub OnInit(ByVal e As EventArgs)
        loadTabs()
        MyBase.OnInit(e)
        Me.Page.RegisterRequiresControlState(Me)

    End Sub

    Private Sub ClearAll()
        txtGroupArName.Text = String.Empty
        txtGroupEnName.Text = String.Empty
        rblDefaultPage.SelectedValue = 1
        'chkAdd.Checked = False
        'chkUpdate.Checked = False
        'chkDelete.Checked = False
        'chkPrint.Checked = False
        ClearTabs()
        SortDir = Nothing
        'LoadGrid()
        'SortDir = "ASC"
        Group_Id = 0
        chkListPermessions.ClearSelection()
        'pnlPermissions.Visible = False
        'btnCreate.Visible = True
    End Sub

    Private Sub ClearTabs()
        Dim tabHeader As AISTabHeader = New AISTabHeader
        Dim myTab As AISTabPanel = Nothing
        For Each myTab In moduleTabs
            For Each chkBox As AISListItem In myTab.items
                chkBox.myItem.Selected = False
            Next
        Next
        DirectCast(DirectCast(myTab.Parent, System.Web.UI.Control), AjaxControlToolkit.TabContainer).ActiveTab = DirectCast(DirectCast(myTab.Parent, System.Web.UI.Control), AjaxControlToolkit.TabContainer).Tabs(0)
    End Sub

    'Private Sub Arrow()
    '    Dim img As System.Web.UI.WebControls.Image = New System.Web.UI.WebControls.Image()
    '    If Not SortDir = Nothing AndAlso Not SortDir = String.Empty Then
    '        If SortDir = "ASC" Then
    '            img.ImageUrl = "~/images/ar-down.png"
    '        Else
    '            img.ImageUrl = "~/images/ar-up.png"
    '        End If
    '        Select Case SortExep
    '            Case "EnglishName"
    '                gvGroups.HeaderRow.Cells(1).Controls.Add(New LiteralControl(" "))
    '                gvGroups.HeaderRow.Cells(1).Controls.Add(img)
    '        End Select
    '    End If
    'End Sub

    'Private Sub SaveToHistory()
    '    Try
    '        _sysGroup = New SYSGroups
    '        _sysPrivilege = New SYSPrivilege 'SaveHistoryRecord
    '        _sysGroup.GroupId = Group_Id
    '        NewPrivilages = _sysGroup.GetPrivileges()
    '        Dim dtForms As New DataTable()
    '        dtForms = _sysPrivilege.GetAllForms()
    '        Dim OldFormID As String = ""
    '        Dim NewFormId As String = ""

    '        Dim isThere As Boolean = False
    '        For Each Nrow As DataRow In NewPrivilages.Rows
    '            isThere = False
    '            For i As Integer = 0 To OldPrivilages.Rows.Count - 1
    '                If OldPrivilages.Rows(i)("FormID").ToString() = Nrow("FormID").ToString() Then
    '                    isThere = True
    '                End If
    '                NewFormId = Nrow("FormID").ToString()
    '            Next
    '            If isThere = False Then
    '                Dim Expre As String = "formId = " & NewFormId
    '                Dim foundRows As DataRow() = dtForms.Select(Expre)
    '                _sysPrivilege.PrivDescAr = foundRows(0)("Module_Desc_Ar").ToString() & " - " & foundRows(0)("Desc_Ar").ToString()
    '                _sysPrivilege.PrivDescEn = foundRows(0)("Module_Desc_En").ToString() & " - " & foundRows(0)("Desc_En").ToString()
    '                _sysPrivilege.PrivType = "إضافة"
    '                _sysPrivilege.Affected = txtGroupArName.Text
    '                _sysPrivilege.PrevValue = "لا يوجد"
    '                _sysPrivilege.NewValue = "يوجد"
    '                _sysPrivilege.SaveHistoryRecord()
    '            End If
    '        Next



    '        isThere = False
    '        For Each Nrow As DataRow In OldPrivilages.Rows
    '            isThere = False
    '            For i As Integer = 0 To NewPrivilages.Rows.Count - 1
    '                If NewPrivilages.Rows(i)("FormID").ToString() = Nrow("FormID").ToString() Then
    '                    isThere = True
    '                End If
    '                OldFormID = Nrow("FormID").ToString()
    '            Next
    '            If isThere = False Then
    '                Dim Expre As String = "formId = " & OldFormID
    '                Dim foundRows As DataRow() = dtForms.Select(Expre)
    '                _sysPrivilege.PrivDescAr = foundRows(0)("Module_Desc_Ar").ToString() & " - " & foundRows(0)("Desc_Ar").ToString()
    '                _sysPrivilege.PrivDescEn = foundRows(0)("Module_Desc_En").ToString() & " - " & foundRows(0)("Desc_En").ToString()
    '                _sysPrivilege.PrivType = "حذف"
    '                _sysPrivilege.Affected = txtGroupArName.Text
    '                _sysPrivilege.PrevValue = "يوجد"
    '                _sysPrivilege.NewValue = "لا يوجد"
    '                _sysPrivilege.SaveHistoryRecord()
    '            End If
    '        Next

    '    Catch ex As Exception

    '    End Try
    'End Sub

#End Region

    '#Region "Grid Filter"

    '    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    '    End Sub

    '    Protected Function GetFilterIcon() As String
    '        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", gvGroups.Skin))
    '    End Function

    '    Protected Sub gvGroups_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles gvGroups.ItemCommand
    '        If e.CommandName = "FilterRadGrid" Then
    '            RadFilter1.FireApplyCommand()
    '        End If
    '    End Sub

    '#End Region

End Class
