Imports SmartV.UTILITIES
Imports TA.Admin
Imports TA.Lookup
Imports System.Data
Imports Telerik.Web.UI
Imports TA.Employees
Partial Class Admin_EmployeeSearch
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Protected dir, textalign As String
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objEmployee As New Employee

#End Region

#Region "Properties"
    Public Property dtCurrent() As DataTable
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
        Get
            Return (ViewState("dtCurrent"))
        End Get
    End Property
    Public Property EmployeeId() As Integer
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
        Get
            Return (ViewState("EmployeeId"))
        End Get
    End Property
    Public Property CompanyId() As Integer
        Set(ByVal value As Integer)
            ViewState("CompanyId") = value
        End Set
        Get
            Return (ViewState("CompanyId"))
        End Get
    End Property
    Public Property EmployeeNo() As String
        Set(ByVal value As String)
            ViewState("EmployeeNo") = value
        End Set
        Get
            Return (ViewState("EmployeeNo"))
        End Get
    End Property
#End Region

#Region "Page Events"

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            Page.UICulture = SessionVariables.CultureInfo
        End If
    End Sub

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
                Lang = CtlCommon.Lang.EN
            Else
                SessionVariables.CultureInfo = "ar-JO"
                dir = "rtl"
                textalign = "right"
                Lang = CtlCommon.Lang.AR
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")

            ElseIf SessionVariables.CultureInfo = "ar-JO" Then

                Lang = CtlCommon.Lang.AR
                RadCmbBxType.Items(0).Text = "--الرجاء الاختيار--"
                RadCmbBxType.Items(1).Text = "اسم الموظف باللغة الانجليزية"
                RadCmbBxType.Items(2).Text = "اسم الموظف باللغة العربية"
                RadCmbBxType.Items(3).Text = "الجنسية باللغة الانجليزية"
                RadCmbBxType.Items(4).Text = "الجنسية باللغة العربية"
                RadCmbBxType.Items(5).Text = "الديانة باللغة الانجليزية"
                RadCmbBxType.Items(6).Text = "الديانة باللغة العربية"
                RadCmbBxType.Items(7).Text = "رقم بطاقة الموظف"

                RadCmbBxOperator.Items(0).Text = "--الرجاء الاختيار--"
                RadCmbBxOperator.Items(1).Text = "يبدأ ب"
                RadCmbBxOperator.Items(2).Text = "يحتوي"
                RadCmbBxOperator.Items(3).Text = "يساوي"
                RadCmbBxOperator.Items(4).Text = "ينتهي ب"
            Else
                Lang = CtlCommon.Lang.EN
                RadCmbBxType.Items(0).Text = "--Please Select--"
                RadCmbBxType.Items(1).Text = "Employee English Name"
                RadCmbBxType.Items(2).Text = "Employee Arabic Name"
                RadCmbBxType.Items(3).Text = "Employee English Nationality"
                RadCmbBxType.Items(4).Text = "Employee Arabic Nationality"
                RadCmbBxType.Items(5).Text = "Employee English Religon"
                RadCmbBxType.Items(6).Text = "Employee Arabic Religon"
                RadCmbBxType.Items(7).Text = "Employee Card No"

                RadCmbBxOperator.Items(0).Text = "--Please Select--"
                RadCmbBxOperator.Items(1).Text = "Starts With"
                RadCmbBxOperator.Items(2).Text = "Contains"
                RadCmbBxOperator.Items(3).Text = "Equals"
                RadCmbBxOperator.Items(4).Text = "Ends With"
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("SearchMethod", CultureInfo)
            Me.Title = ResourceManager.GetString("SearchMethod", CultureInfo)

            If Not Request.QueryString("SourceControlId") Is Nothing Then
                Session("SourceControl_Session") = Request.QueryString("SourceControlId")
            End If
        End If
    End Sub

    Protected Sub btnSearchEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchEmp.Click
        FillGrid()
    End Sub

    Protected Sub dgrdSearchResult_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdSearchResult.ItemCommand
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

            Dim link As LinkButton = DirectCast(item.FindControl("lnkEmpNo"), LinkButton)
            EmployeeNo = DirectCast(item.FindControl("lnkEmpNo"), LinkButton).Text
            EmployeeId = item.GetDataKeyValue("EmployeeId").ToString()
            CompanyId = item.GetDataKeyValue("CompanyId").ToString()
            Dim SearchValues As New ArrayList
            SearchValues.Add(EmployeeId)
            SearchValues.Add(CompanyId)
            SearchValues.Add(EmployeeNo)
            Session("EmpSearchResult") = SearchValues

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Search", "CloseAndRefresh();", True)

            'If Not link Is Nothing And EmployeeId > 0 Then
            '    link.Attributes.Add("onclick", "closeFormOK();")

            'End If
        End If
    End Sub

    'Protected Sub dgrdSearchResult_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdSearchResult.NeedDataSource
    '    dgrdSearchResult.DataSource = dtCurrent
    'End Sub
    'Protected Sub dgrdSearchResult_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdSearchResult.ItemCommand
    '    If e.CommandName = "Emp_No" Then
    '        Dim item As GridDataItem = (CType(CType(e.CommandSource, LinkButton).NamingContainer, GridDataItem))
    '        Dim link As LinkButton = DirectCast(item.FindControl("lnkEmpNo"), LinkButton)

    '        EmployeeId = CInt(item("EmployeeId").Text.Trim)
    '        CompanyId = CInt(item("CompanyId").Text.Trim)

    '        EmployeeId = item("EmployeeId").Text
    '        CompanyId = item("CompanyId").Text

    '        link.Attributes.Add("onclick", String.Format("odata[0] = '{0}'; odata[1] = '{1}'; closeFormOK();", EmployeeId, CompanyId))
    '    End If
    'End Sub



    'Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    'End Sub

    'Protected Sub dgrdSearchResult_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdSearchResult.ItemCommand
    '    If e.CommandName = "FilterRadGrid" Then
    '        RadFilter1.FireApplyCommand()
    '    End If
    'End Sub
    'Protected Function GetFilterIcon() As String
    '    Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdSearchResult.Skin))
    'End Function

#End Region

#Region "Methods"

    Private Sub FillGrid()
        ' Dim SearchType As Integer = 1
        Dim SearchType As Integer = RadCmbBxType.SelectedValue
        Dim RowCount As Integer
        Select Case SearchType
            Case 1
                dtCurrent = SearchbyEngName()
            Case 2
                dtCurrent = SearchbyArName()
            Case 3
                dtCurrent = SearchbyEngNationality()
            Case 4
                dtCurrent = SearchbyArNationality()
            Case 5
                dtCurrent = SearchbyEngReligon()
            Case 6
                dtCurrent = SearchbyArReligon()
            Case 7
                dtCurrent = SearchbyCardNo()
        End Select
        If dtCurrent IsNot Nothing Then
            RowCount = dtCurrent.Rows.Count
        End If
        dgrdSearchResult.DataSource = dtCurrent
        dgrdSearchResult.DataBind()


    End Sub

    Private Function SearchbyEngName() As DataTable
        objEmployee = New Employee
        With objEmployee
            .Type = 1
            .Oper = RadCmbBxOperator.SelectedValue
            .Value = txtValue.Text
            Return .GetBySearchCriteria()
        End With
    End Function

    Private Function SearchbyArName() As DataTable
        objEmployee = New Employee
        With objEmployee
            .Type = 2
            .Oper = RadCmbBxOperator.SelectedValue
            .Value = txtValue.Text
            Return .GetBySearchCriteria()
        End With
    End Function

    Private Function SearchbyEngNationality() As DataTable
        objEmployee = New Employee
        With objEmployee
            .Type = 3
            .Oper = RadCmbBxOperator.SelectedValue
            .Value = txtValue.Text
            Return .GetBySearchCriteria()
        End With
    End Function

    Private Function SearchbyArNationality() As DataTable
        objEmployee = New Employee
        With objEmployee
            .Type = 4
            .Oper = RadCmbBxOperator.SelectedValue
            .Value = txtValue.Text
            Return .GetBySearchCriteria()
        End With
    End Function

    Private Function SearchbyEngReligon() As DataTable
        objEmployee = New Employee
        With objEmployee
            .Type = 5
            .Oper = RadCmbBxOperator.SelectedValue
            .Value = txtValue.Text
            Return .GetBySearchCriteria()
        End With
    End Function

    Private Function SearchbyArReligon() As DataTable
        objEmployee = New Employee
        With objEmployee
            .Type = 6
            .Oper = RadCmbBxOperator.SelectedValue
            .Value = txtValue.Text
            Return .GetBySearchCriteria()
        End With
    End Function
    Private Function SearchbyCardNo() As DataTable
        objEmployee = New Employee
        With objEmployee
            .Type = 7
            .Oper = RadCmbBxOperator.SelectedValue
            .Value = txtValue.Text
            Return .GetBySearchCriteria()
        End With
    End Function
#End Region

End Class
