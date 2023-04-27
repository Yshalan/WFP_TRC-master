Imports System.Data
Imports TA.Definitions
Imports System.Linq
Imports TA.Employees
Imports SmartV.UTILITIES
Imports TA.Admin
Imports Telerik.Web.UI.DatePickerPopupDirection

Partial Class Employee_Emp_Skills
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objAPP_Settings As APP_Settings
    Private objEmployee As Employee
    Private objSkillCategory As SkillCategory
    Private objSkills As Skills
    Private objEmp_Skills As Emp_Skills
    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

#End Region

#Region "Public Properties"

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Public Property CategoryId() As Integer
        Get
            Return ViewState("CategoryId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CategoryId") = value
        End Set
    End Property

    Public Property IsRetrieve() As Boolean
        Get
            Return ViewState("IsRetrieve")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsRetrieve") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")

            ElseIf SessionVariables.CultureInfo = "ar-JO" Then

                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            FillSkillCategory()
            PageHeader1.HeaderText = ResourceManager.GetString("EmployeeSkills", CultureInfo)
            'EmployeeId = 20338
            SetRadDatePickerPeoperties()
            FormatSelection()
        End If
    End Sub

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

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        objEmp_Skills = New Emp_Skills
        objSkillCategory = New SkillCategory
        objSkills = New Skills
        Dim err As Integer = -3 '---Skill Not Found
        With objSkills
            If Lang = CtlCommon.Lang.AR Then
                .SkillArabicName = txtSearch.Text
                .GetByNameAr()
            Else
                .SkillName = txtSearch.Text
                .GetByNameEn()
            End If
        End With

        If Not objSkills.SkillId = 0 Then
            With objSkillCategory
                .CategoryId = objSkills.FK_CategoryId
                CategoryId = .CategoryId
                .GetByPK()
            End With

            With objEmp_Skills
                .FK_EmployeeId = EmployeeId
                .FK_SkillId = objSkills.SkillId
                If objSkillCategory.HasDate = True Then
                    .FromDate = dtpFromDate.DbSelectedDate
                    .ToDate = dtpToDate.DbSelectedDate
                End If
                err = .Add()
            End With
        End If
        If err = 0 Then
            'FillEmployeeSkills()
            FormatSelectionAfterAdd()
        ElseIf err = -2 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SkillExists"), "info")
        ElseIf err = -3 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("IncorrectSkill"), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave"), "error")
        End If
    End Sub

    Protected Sub repSkillCategory_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repSkillCategory.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lnkSkillCategoryEn As LinkButton
            Dim lnkSkillCategoryAr As LinkButton
            lnkSkillCategoryEn = CType(e.Item.FindControl("lnkSkillCategoryEn"), LinkButton)
            lnkSkillCategoryAr = CType(e.Item.FindControl("lnkSkillCategoryAr"), LinkButton)

            If Lang = CtlCommon.Lang.AR Then
                CType(e.Item.FindControl("lnkSkillCategoryAr"), LinkButton).Visible = True
            Else
                CType(e.Item.FindControl("lnkSkillCategoryEn"), LinkButton).Visible = True
            End If

            If Not Request.Cookies("style") Is Nothing Then
                Dim strstyleValue As String = Request.Cookies("style").Value
                Dim intstyleValue As Integer = Convert.ToInt32(strstyleValue.Replace("styles", ""))

                Select Case intstyleValue
                    Case 1 'Dof
                        CType(e.Item.FindControl("imgSkillCategory"), ImageButton).ImageUrl = "~/images/certificates_Gray.png"
                    Case 2 'Green
                        CType(e.Item.FindControl("imgSkillCategory"), ImageButton).ImageUrl = "~/images/certificates_Green.png"
                    Case 3 'Red
                        CType(e.Item.FindControl("imgSkillCategory"), ImageButton).ImageUrl = "~/images/certificates_Red.png"
                    Case 4 'Blue
                        CType(e.Item.FindControl("imgSkillCategory"), ImageButton).ImageUrl = "~/images/certificates_Blue.png"
                    Case 5 'Violet
                        CType(e.Item.FindControl("imgSkillCategory"), ImageButton).ImageUrl = "~/images/certificates_Violet.png"
                    Case 6 'Gold
                        CType(e.Item.FindControl("imgSkillCategory"), ImageButton).ImageUrl = "~/images/certificates_Gold.png"
                    Case 7 'White
                        CType(e.Item.FindControl("imgSkillCategory"), ImageButton).ImageUrl = "~/images/certificates_White.png"
                End Select

            Else
                objAPP_Settings = New APP_Settings
                objAPP_Settings.GetByPK()
                If objAPP_Settings.DefaultTheme = 1 Then
                    CType(e.Item.FindControl("imgSkillCategory"), ImageButton).ImageUrl = "~/images/certificates_Gray.png"
                ElseIf objAPP_Settings.DefaultTheme = 2 Then
                    CType(e.Item.FindControl("imgSkillCategory"), ImageButton).ImageUrl = "~/images/certificates_Green.png"
                ElseIf objAPP_Settings.DefaultTheme = 3 Then
                    CType(e.Item.FindControl("imgSkillCategory"), ImageButton).ImageUrl = "~/images/certificates_Red.png"
                ElseIf objAPP_Settings.DefaultTheme = 4 Then
                    CType(e.Item.FindControl("imgSkillCategory"), ImageButton).ImageUrl = "~/images/certificates_Blue.png"
                ElseIf objAPP_Settings.DefaultTheme = 5 Then
                    CType(e.Item.FindControl("imgSkillCategory"), ImageButton).ImageUrl = "~/images/certificates_Violet.png"
                ElseIf objAPP_Settings.DefaultTheme = 6 Then
                    CType(e.Item.FindControl("imgSkillCategory"), ImageButton).ImageUrl = "~/images/certificates_Gold.png"
                ElseIf objAPP_Settings.DefaultTheme = 7 Then
                    CType(e.Item.FindControl("imgSkillCategory"), ImageButton).ImageUrl = "~/images/certificates_White.png"
                End If
            End If

        End If
    End Sub

    'Protected Sub grdVwEmployees_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdVwEmployees.NeedDataSource
    '    Dim dtEmployees As New DataTable
    '    objAPP_Settings = New APP_Settings
    '    objEmployee = New Employee()
    '    objAPP_Settings.GetByPK()

    '    If (EmployeeFilter.EmployeeId <> 0 And objAPP_Settings.ShowEmployeeList = True) Then
    '        objEmployee.EmployeeId = EmployeeFilter.EmployeeId
    '        dtEmployees = objEmployee.GetByEmpId
    '    ElseIf (EmployeeFilter.EmployeeIdtxt <> 0 And objAPP_Settings.ShowEmployeeList = False) Then

    '        objEmployee.EmployeeId = EmployeeFilter.EmployeeIdtxt
    '        dtEmployees = objEmployee.GetByEmpId

    '    ElseIf EmployeeFilter.EntityId <> 0 Then
    '        objEmployee = New Employee()
    '        objEmployee.FK_CompanyId = EmployeeFilter.CompanyId
    '        objEmployee.FK_EntityId = EmployeeFilter.EntityId
    '        objEmployee.FilterType = EmployeeFilter.FilterType
    '        If EmployeeFilter.ShowOnlyManagers Then
    '            dtEmployees = objEmployee.GetManagersByCompany
    '        Else
    '            dtEmployees = objEmployee.GetEmployee_ByStatus()
    '        End If


    '    ElseIf EmployeeFilter.CompanyId <> 0 Then
    '        objEmployee = New Employee()
    '        objEmployee.FK_CompanyId = EmployeeFilter.CompanyId
    '        objEmployee.FK_EntityId = -1
    '        objEmployee.FilterType = EmployeeFilter.FilterType
    '        dtEmployees = objEmployee.GetEmpByCompany
    '        If EmployeeFilter.ShowOnlyManagers Then
    '            dtEmployees = objEmployee.GetManagersByCompany
    '        Else
    '            dtEmployees = objEmployee.GetEmployee_ByStatus()
    '        End If
    '    End If
    '    grdVwEmployees.DataSource = dtEmployees
    'End Sub

    Protected Sub imglnkEmpSkills_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        For Each item As RepeaterItem In repSkillCategory.Items
            Dim dvimgControl = DirectCast(item.FindControl("dvSkillCategory"), HtmlGenericControl)

            dvimgControl.Attributes.Add("class", "none")
        Next

        CategoryId = Convert.ToInt32(DirectCast((DirectCast(sender, ImageButton).Parent).FindControl("imgskillcategory"), ImageButton).CommandArgument)
        FillEmployeeSkills()
        'If repSkills.Items.Count > 0 Then
        Dim dvControl = (DirectCast((DirectCast(sender, ImageButton).Parent).FindControl("dvSkillCategory"), HtmlGenericControl))
        dvControl.Attributes.Add("class", "repetericonarea")
        'End If


    End Sub

    Protected Sub lnkEmpSkills_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        For Each item As RepeaterItem In repSkillCategory.Items
            Dim dvimgControl = DirectCast(item.FindControl("dvSkillCategory"), HtmlGenericControl)

            dvimgControl.Attributes.Add("class", "none")
        Next

        If Lang = CtlCommon.Lang.AR Then
            CategoryId = Convert.ToInt32(DirectCast((DirectCast(sender, LinkButton).Parent).FindControl("lnkSkillCategoryAr"), LinkButton).CommandArgument)
        Else
            CategoryId = Convert.ToInt32(DirectCast((DirectCast(sender, LinkButton).Parent).FindControl("lnkSkillCategoryEn"), LinkButton).CommandArgument)
        End If
        FillEmployeeSkills()
        'If repSkills.Items.Count > 0 Then
        Dim dvControl = (DirectCast((DirectCast(sender, LinkButton).Parent).FindControl("dvSkillCategory"), HtmlGenericControl))
        dvControl.Attributes.Add("class", "repetericonarea")
        'End If


    End Sub

    Protected Sub repSkills_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repSkills.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblSkillNameEn As Label
            Dim lblSkillNameAr As Label
            lblSkillNameEn = CType(e.Item.FindControl("lblSkillNameEn"), Label)
            lblSkillNameAr = CType(e.Item.FindControl("lblSkillNameAr"), Label)

            If Lang = CtlCommon.Lang.AR Then
                CType(e.Item.FindControl("lblSkillNameAr"), Label).Visible = True
            Else
                CType(e.Item.FindControl("lblSkillNameEn"), Label).Visible = True
            End If

            Dim FormatFromDate As String
            Dim FormatToDate As String

            If Not CType(e.Item.FindControl("lblSkillFromDateVal"), Label).Text = "" Then
                FormatFromDate = Convert.ToDateTime(CType(e.Item.FindControl("lblSkillFromDateVal"), Label).Text).ToString("dd/MM/yyyy")
                CType(e.Item.FindControl("lblSkillFromDate"), Label).Visible = True
                CType(e.Item.FindControl("lblSkillFromDateVal"), Label).Visible = True
                CType(e.Item.FindControl("lblSkillFromDateVal"), Label).Text = FormatFromDate.ToString
            End If

            If Not CType(e.Item.FindControl("lblSkillToDateVal"), Label).Text = "" Then

                FormatToDate = Convert.ToDateTime(CType(e.Item.FindControl("lblSkillToDateVal"), Label).Text).ToString("dd/MM/yyyy")
                CType(e.Item.FindControl("lblSkillToDate"), Label).Visible = True
                CType(e.Item.FindControl("lblSkillToDateVal"), Label).Visible = True
                CType(e.Item.FindControl("lblSkillToDateVal"), Label).Text = FormatToDate.ToString
            End If

        End If
    End Sub

    Protected Sub imgDeleteEmpSkills_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim SkillId As Integer = Convert.ToInt32(DirectCast((DirectCast(sender, ImageButton).Parent).FindControl("imgRemoveSkill"), ImageButton).CommandArgument)
        objEmp_Skills = New Emp_Skills
        Dim err As Integer = -1
        With objEmp_Skills
            .FK_SkillId = SkillId
            .FK_EmployeeId = EmployeeId
            err = .Delete()
        End With
        If err = 0 Then
            FillEmployeeSkills()
        End If
    End Sub

    Protected Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        BindData()
        IsRetrieve = False
    End Sub

    'Protected Sub grdVwEmployees_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdVwEmployees.ItemCommand
    '    Try
    '        If e.CommandName = "FilterRadGrid" Then
    '            RadFilter1.FireApplyCommand()
    '        ElseIf e.CommandName = "EditEmp" Then
    '            If IsRetrieve Then
    '                CtlCommon.ShowMessage(Me.Page, "Please press Get By Filter to refresh page", "info")
    '                SetSortingValue()
    '            Else
    '                EmployeeId = Convert.ToInt32(DirectCast(e.Item, GridDataItem).GetDataKeyValue("EmployeeId").ToString())
    '                mvEmp_Skills.ActiveViewIndex = 1
    '            End If
    '        End If
    '        BindData()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
    '    Response.Redirect("../Employee/Emp_Skills.aspx")
    'End Sub

#End Region

#Region "Methods"

    Private Sub FillSkillCategory()
        objSkillCategory = New SkillCategory
        With objSkillCategory
            repSkillCategory.DataSource = .GetAll
            repSkillCategory.DataBind()
        End With
    End Sub

    <System.Web.Script.Services.ScriptMethod(), _
    System.Web.Services.WebMethod()> Public Shared Function SearchSkills(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim Lang As New CtlCommon.Lang
        Dim ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))

        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR
        Else
            Lang = CtlCommon.Lang.EN
        End If

        Dim objSkills As New Skills
        Dim dt As DataTable
        Dim skill As List(Of String) = New List(Of String)
        dt = objSkills.GetAll
        If Lang = CtlCommon.Lang.AR Then
            For Each row In dt.Rows
                skill.Add(row("SkillArabicName"))
            Next
        Else
            For Each row In dt.Rows
                skill.Add(row("SkillName"))
            Next
        End If
        Dim newList = skill.Where(Function(item) item.ToString.ToUpper.Contains(prefixText.ToString.ToUpper)).ToList
        'Dim newList = skill.Where(Function(item) item.ToString.ToUpper.StartsWith(prefixText.ToString.ToUpper)).ToList
        Return newList
    End Function

    Public Sub SetSortingValue()
        'Dim dtEmployees As New DataTable
        'grdVwEmployees.DataSource = dtEmployees
        'grdVwEmployees.DataBind()
        EmployeeId = EmployeeFilter.EmployeeId
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)
    End Sub

    'Protected Function GetFilterIcon() As String
    '    Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", grdVwEmployees.Skin))
    'End Function

    Private Sub BindData()
        Dim dtEmployees As New DataTable
        objAPP_Settings = New APP_Settings
        objEmployee = New Employee()
        objAPP_Settings.GetByPK()

        If (EmployeeFilter.EmployeeId <> 0 And objAPP_Settings.ShowEmployeeList = True) Then
            objEmployee.EmployeeId = EmployeeFilter.EmployeeId
            dtEmployees = objEmployee.GetByEmpId
        ElseIf (EmployeeFilter.EmployeeIdtxt <> 0 And objAPP_Settings.ShowEmployeeList = False) Then

            objEmployee.EmployeeId = EmployeeFilter.EmployeeIdtxt
            dtEmployees = objEmployee.GetByEmpId

        ElseIf EmployeeFilter.EntityId <> 0 Then
            objEmployee = New Employee()
            objEmployee.FK_CompanyId = EmployeeFilter.CompanyId
            objEmployee.FK_EntityId = EmployeeFilter.EntityId
            objEmployee.FilterType = EmployeeFilter.FilterType
            If EmployeeFilter.ShowOnlyManagers Then
                dtEmployees = objEmployee.GetManagersByCompany
            Else
                dtEmployees = objEmployee.GetEmployee_ByStatus()
            End If


        ElseIf EmployeeFilter.CompanyId <> 0 Then
            objEmployee = New Employee()
            objEmployee.FK_CompanyId = EmployeeFilter.CompanyId
            objEmployee.FK_EntityId = -1
            objEmployee.FilterType = EmployeeFilter.FilterType
            dtEmployees = objEmployee.GetEmpByCompany
            If EmployeeFilter.ShowOnlyManagers Then
                dtEmployees = objEmployee.GetManagersByCompany
            Else
                dtEmployees = objEmployee.GetEmployee_ByStatus()
            End If
        End If
        'grdVwEmployees.DataSource = dtEmployees
        'grdVwEmployees.DataBind()
    End Sub

    Private Sub FillEmployeeSkills()
        objSkillCategory = New SkillCategory
        With objSkillCategory
            .CategoryId = CategoryId
            .GetByPK()
            If .HasDate = True Then
                dvDate.Visible = True
            Else
                dvDate.Visible = False
            End If
        End With

        objEmp_Skills = New Emp_Skills
        Dim dt As DataTable
        With objEmp_Skills
            .FK_CategoryId = CategoryId
            .FK_EmployeeId = EmployeeId
            dt = .GetAll_By_EmployeeId_CategoryId()
            repSkills.DataSource = dt
            repSkills.DataBind()
        End With
        'If Not dt.Rows.Count > 0 Then
        '    Empskills.Visible = False
        'Else
        '    Empskills.Visible = True
        'End If

    End Sub

    Private Sub SetRadDatePickerPeoperties()

        dtpFromDate.SelectedDate = Today.Date
        dtpFromDate.PopupDirection = TopRight
        dtpFromDate.ShowPopupOnFocus = True
        dtpFromDate.MinDate = New Date(2006, 1, 1)
        dtpFromDate.MaxDate = New Date(2040, 1, 1)

        dtpToDate.SelectedDate = Today.Date
        dtpToDate.PopupDirection = TopRight
        dtpToDate.ShowPopupOnFocus = True
        dtpToDate.MinDate = New Date(2006, 1, 1)
        dtpToDate.MaxDate = New Date(2040, 1, 1)
    End Sub

    Public Sub hdnValue_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim skillname As String = (CType(sender, HiddenField)).Value

        objSkills = New Skills
        Dim err As Integer = -3 '---Skill Not Found
        With objSkills
            If Lang = CtlCommon.Lang.AR Then
                .SkillArabicName = skillname
                .GetByNameAr()
            Else
                .SkillName = skillname
                .GetByNameEn()
            End If
        End With
        objSkillCategory = New SkillCategory
        With objSkillCategory
            CategoryId = objSkills.FK_CategoryId
            .CategoryId = objSkills.FK_CategoryId
            .GetByPK()
            If .HasDate = True Then
                dvDate.Visible = True
            Else
                dvDate.Visible = False
            End If
        End With

    End Sub

    Private Sub FormatSelection()
        Dim dvimgControl = DirectCast(repSkillCategory.Items(0).FindControl("dvSkillCategory"), HtmlGenericControl)
        dvimgControl.Attributes.Add("class", "repetericonarea")
    End Sub

    Private Sub FormatSelectionAfterAdd()
        For Each item As RepeaterItem In repSkillCategory.Items
            Dim dvimgControl = DirectCast(item.FindControl("dvSkillCategory"), HtmlGenericControl)

            dvimgControl.Attributes.Add("class", "none")
        Next

        FillEmployeeSkills()
        If repSkills.Items.Count > 0 Then
            For Each item In repSkillCategory.Items

                If Convert.ToInt32(item.FindControl("lblCategoryid").text) = CategoryId Then
                    Dim itemIndex As Integer = DirectCast(item, System.Web.UI.WebControls.RepeaterItem).ItemIndex
                    Dim dvimgControl = DirectCast(repSkillCategory.Items(itemIndex).FindControl("dvSkillCategory"), HtmlGenericControl)
                    dvimgControl.Attributes.Add("class", "repetericonarea")
                End If

            Next
        End If
    End Sub
#End Region

End Class
