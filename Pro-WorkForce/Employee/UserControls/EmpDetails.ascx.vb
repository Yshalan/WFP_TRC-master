Imports SmartV.UTILITIES
Imports System.Data
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports Telerik.Web.UI
Imports TA.LookUp
Imports TA.OrgCompany
Imports System.Web.UI
Imports TA.Employees
Imports TA.Security
Imports TA.Admin

Partial Class EmpDetails_WebUserControl
    Inherits System.Web.UI.UserControl

#Region "Class Variables"
    ' Instances will be used to fill combo boxes
    Private objEmployee As Employee
    ' Shared variables of main Gridview

    ' shared variables of Ta policy grid
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim objApp_Settings As APP_Settings
#End Region

#Region "Properties"

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Public Property CompanyID() As Integer
        Get
            Return ViewState("CompanyID")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyID") = value
        End Set
    End Property

    Public Property FileFullPath() As String
        Get
            Return ViewState("FileFullPath")
        End Get
        Set(ByVal value As String)
            ViewState("FileFullPath") = value
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("EmployeeStatus") = rblEmpStatus.Text 'ID: M01 || Date: 20-04-2023 || By: Yahia shalan || Description: Get the employee status if it's (1 or 2 >> Active or Inactive) based on the radio button option on the screen.'

        If Not Page.IsPostBack Then
            EmployeeFilter.IsEmployeeRequired = True
            'FillGridView()
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("Employees", CultureInfo)
        End If

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not Me.FindControl(row("AddBtnName")) Is Nothing Then
                        Me.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not EditEmp1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        EditEmp1.FindControl(row("DeleteBtnName")).Visible = False
                        EditEmp1.AllowDelete = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not EditEmp1.FindControl(row("EditBtnName")) Is Nothing Then
                        EditEmp1.FindControl(row("EditBtnName")).Visible = False
                        EditEmp1.AllowEdit = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not Me.FindControl(row("PrintBtnName")) Is Nothing Then
                        Me.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        BindData()
        IsRetrieve = False
    End Sub

    Protected Sub lnkAddList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAddList.Click
        NewEmp1.EnableValidation(False)
        grdVwEmployees.SelectedIndexes.Clear()
        'FillGridView()
        MvEmployee.ActiveViewIndex = 2
    End Sub

    Protected Sub lnkEditList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEditList.Click
        ' FillGridView()
        MvEmployee.ActiveViewIndex = 2
    End Sub

    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnadd.Click
        'NewEmp1.loadwizard()

        MvEmployee.ActiveViewIndex = 0
        NewEmp1.ClearAll()
        NewEmp1.CompanyID = CompanyID

    End Sub

    Protected Sub rblEmpStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnadd.Click
        'NewEmp1.loadwizard()

        MvEmployee.ActiveViewIndex = 0
        NewEmp1.ClearAll()
        NewEmp1.CompanyID = CompanyID

    End Sub

    'new 
    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)
    End Sub
    'new 
    Protected Sub grdVwEmployees_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdVwEmployees.ItemCommand
        Try
            If e.CommandName = "FilterRadGrid" Then
                RadFilter1.FireApplyCommand()
            ElseIf e.CommandName = "EditEmp" Then
                If IsRetrieve Then
                    CtlCommon.ShowMessage(Me.Page, "Please press Get By Filter to refresh page", "info")
                    SetSortingValue()
                Else
                    EmployeeId = Convert.ToInt32(DirectCast(e.Item, GridDataItem).GetDataKeyValue("EmployeeId").ToString())
                    EditEmp1.EmployeeId = EmployeeId
                    EditEmp1.FillControlsForEditing()
                    EditEmp1.ManageControls(True)
                    EditEmp1.Sch_Status = False
                    MvEmployee.ActiveViewIndex = 1
                End If
            End If
            BindData()
        Catch ex As Exception

        End Try
    End Sub

    'new 
    Protected Sub grdVwEmployees_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdVwEmployees.NeedDataSource
        'objEmployee = New Employee()
        'grdVwEmployees.DataSource = objEmployee.GetAll()
        Dim dtEmployees
        If (EmployeeFilter.EmployeeId <> 0) Then
            objEmployee = New Employee()
            objEmployee.EmployeeId = EmployeeFilter.EmployeeId
            dtEmployees = objEmployee.GetByEmpId

        ElseIf EmployeeFilter.EntityId <> 0 Then
            objEmployee = New Employee()
            objEmployee.FK_CompanyId = EmployeeFilter.CompanyId
            objEmployee.FK_EntityId = EmployeeFilter.EntityId
            dtEmployees = objEmployee.GetEmpByCompEnt

        ElseIf EmployeeFilter.CompanyId <> 0 Then
            objEmployee = New Employee()
            objEmployee.FK_CompanyId = EmployeeFilter.CompanyId
            objEmployee.FK_EntityId = -1
            dtEmployees = objEmployee.GetEmpByCompany
            'Else
            '    objEmployee = New Employee()
            '    grdVwEmployees.DataSource = objEmployee.GetAll()
            '    Return
        End If

        'grdVwEmployees.DataSource = dtEmployees
    End Sub
    'new 
    Protected Sub grdVwEmployees_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVwEmployees.SelectedIndexChanged
        If IsRetrieve Then
            CtlCommon.ShowMessage(Me.Page, "Please press Get By Filter to refresh page", "info")
            SetSortingValue()
        Else
            Dim intEmployeeId As Integer = Convert.ToInt32(DirectCast(grdVwEmployees.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeId").ToString())
            EditEmp1.EmployeeId = intEmployeeId
            EditEmp1.FillControlsForEditing()
            EditEmp1.ManageControls(False)
            EditEmp1.Sch_Status = True
            MvEmployee.ActiveViewIndex = 1
        End If
    End Sub

    Protected Sub grdVwEmployees_PageChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles grdVwEmployees.PageIndexChanged
        grdVwEmployees.CurrentPageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub NewEmployee_Submit(ByVal sender As Object, ByVal e As EventArgs) Handles NewEmp1.Submit
        'FillGridView()
        BindData()
        Me.MvEmployee.ActiveViewIndex = 2

    End Sub

    Protected Sub grdVwEmployees_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles grdVwEmployees.SortCommand

    End Sub
#End Region

#Region "Methods"

#Region "GridView related methods"

    'Private Sub FillGridView()
    '    Try
    '        objEmployee = New Employee()
    '        grdVwEmployees.DataSource = objEmployee.GetAll()
    '        grdVwEmployees.DataBind()
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Public Sub SetSortingValue()

        Dim dtEmployees As New DataTable
        grdVwEmployees.DataSource = dtEmployees
        grdVwEmployees.DataBind()
        IsRetrieve = True

    End Sub

#End Region

    Private Function IS_Exist() As Integer
        ' The View , and Edit modes require to have a valid Leave Id 
        objEmployee = New Employee()
        objEmployee.EmployeeId = EmployeeId
        Dim _EXIT As Integer = 0
        If EmployeeId <= 0 Then
            _EXIT = -1
        ElseIf objEmployee.FindExisting() = False Then
            _EXIT = -1
        End If
        Return _EXIT
    End Function

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", grdVwEmployees.Skin))
    End Function

    Private Sub BindData()
        Dim dtEmployees As New DataTable
        objApp_Settings = New APP_Settings
        objEmployee = New Employee()
        objApp_Settings.GetByPK()

        If (EmployeeFilter.EmployeeId <> 0 And objApp_Settings.ShowEmployeeList = True) Then



            objEmployee.EmployeeId = EmployeeFilter.EmployeeId
            dtEmployees = objEmployee.GetByEmpId


        ElseIf (EmployeeFilter.EmployeeIdtxt <> 0 And objApp_Settings.ShowEmployeeList = False) Then

            objEmployee.EmployeeId = EmployeeFilter.EmployeeIdtxt
            dtEmployees = objEmployee.GetByEmpId


        ElseIf EmployeeFilter.EntityId <> 0 Then
            objEmployee = New Employee()
            objEmployee.FK_CompanyId = EmployeeFilter.CompanyId
            objEmployee.FK_EntityId = EmployeeFilter.EntityId
            objEmployee.FilterType = EmployeeFilter.FilterType
            objEmployee.Status = rblEmpStatus.SelectedValue
            'dtEmployees = objEmployee.GetEmpByCompEnt
            If EmployeeFilter.ShowOnlyManagers Then
                dtEmployees = objEmployee.GetManagersByCompany
            Else
                dtEmployees = objEmployee.GetEmployee_ByStatus()
                ' dtEmployees = objEmployee.GetEmployeeIDz()
            End If


        ElseIf EmployeeFilter.CompanyId <> 0 Then
            objEmployee = New Employee()
            objEmployee.FK_CompanyId = EmployeeFilter.CompanyId
            objEmployee.FK_EntityId = -1
            objEmployee.FilterType = EmployeeFilter.FilterType
            objEmployee.Status = rblEmpStatus.SelectedValue
            dtEmployees = objEmployee.GetEmpByCompany
            'dtEmployees = objEmployee.GetEmpByCompEnt
            If EmployeeFilter.ShowOnlyManagers Then
                dtEmployees = objEmployee.GetManagersByCompany
            Else
                dtEmployees = objEmployee.GetEmployee_ByStatus()
                'dtEmployees = objEmployee.GetEmployeeIDz()
            End If
        End If


        grdVwEmployees.DataSource = dtEmployees
        grdVwEmployees.DataBind()
    End Sub

#End Region

    'Public Sub LoadData()
    '    FillGridView()
    'End Sub

    'Public Sub UploadImg(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim objEmployee = New Employee
    '    If FileUpload1.HasFile Then
    '        Dim fileName As String = EmployeeId
    '        Dim filepath As String = ConfigurationManager.AppSettings("EmpImages").ToString
    '        Dim validFileTypes As String() = {"bmp", "gif", "png", "jpg", "jpeg"}
    '        Dim ext As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName)
    '        Dim isValidFile As Boolean = False
    '        For i As Integer = 0 To validFileTypes.Length - 1

    '            If ext = "." & validFileTypes(i) Then

    '                isValidFile = True

    '                Exit For

    '            End If

    '        Next
    '        If isValidFile Then
    '            If EmployeeId <> 0 Then
    '                FileFullPath = filepath + fileName + ext
    '                EditEmp1.FileFullPath = FileFullPath
    '                FileUpload1.PostedFile.SaveAs(FileFullPath)
    '                CtlCommon.ShowMessage(Me.Page, "Uploaded Successfully")
    '                'Dim fileExt As String = FileUpload2.FileName.Substring(FileUpload2.FileName.IndexOf("."),
    '                'If Not System.IO.Directory.Exists(Server.MapPath("~/EmployeeImages") + fileName) Then
    '                'System.IO.Directory.CreateDirectory(Server.MapPath("~/EmployeeImages") + fileName)
    '                'End If
    '                EditEmp1.UpdateEmployee()
    '            End If
    '        Else

    '            CtlCommon.ShowMessage(Me.Page, "File Extension is not valid. Valid Extensions are " & String.Join(", ", validFileTypes))
    '            '    CtlCommon.ShowMessage(Me.Page, "Uploaded Successfully")
    '        End If
    '    End If
    'End Sub


    Protected Sub rblEmpStatus_SelectedIndexChanged(sender As Object, e As EventArgs)
        EmployeeFilter.ClearValues()
    End Sub
End Class
