Imports SmartV.UTILITIES
Imports System.Data
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports Telerik.Web.UI
Imports TA.Lookup
Imports System.Web.UI
Imports TA.Employees
Imports TA.Security
Imports TA_SchoolScheduling

Partial Class SchoolScheduling_TeacherCourses
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objCourseTeachers As CourseTeachers
    Private objTeacherClasses As TeacherClasses
    Private objEmployee As Employee
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Protected dir As String
    Private Lang As CtlCommon.Lang
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
#End Region

#Region "Page Events"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)
    End Sub

    Protected Sub btnFilter_Click(sender As Object, e As System.EventArgs) Handles btnFilter.Click
        BindData()

    End Sub

    Protected Sub lnkEditList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEditList.Click
        ' FillGridView()
        'MvEmployee.ActiveViewIndex = 2
        EditTeacher1.FillcblCourseList()

        Response.Redirect("TeacherCourses.aspx")
    End Sub

    'Protected Sub grdVwEmployees_DataBound(sender As Object, e As System.EventArgs) Handles grdVwEmployees.DataBound
    '    Dim value As String = ""
    '    For Each item As GridDataItem In grdVwEmployees.MasterTableView.Items
    '        Dim cell As TableCell = item("EmployeeId")
    '        If Not cell.Text = value Then
    '            value = cell.Text
    '            cell.Style.Item(HtmlTextWriterStyle.BackgroundColor) = value
    '            cell.Text = ""
    '        End If
    '    Next
    'End Sub

    

    Protected Sub grdVwEmployees_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdVwEmployees.SelectedIndexChanged
        'If IsRetrieve Then
        '    CtlCommon.ShowMessage(Me.Page, "Please press Get By Filter to refresh page")
        '    'SetSortingValue()
        'Else
        Dim intEmployeeId As Integer = Convert.ToInt32(DirectCast(grdVwEmployees.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeId"))
        EditTeacher1.EmployeeId = intEmployeeId

        MvEmployee.ActiveViewIndex = 1
        'End If
    End Sub

    Protected Sub grdVwEmployees_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdVwEmployees.ItemCommand
        Try
            If e.CommandName = "FilterRadGrid" Then
                RadFilter1.FireApplyCommand()
            ElseIf e.CommandName = "EditTeacher" Then

                EmployeeId = Convert.ToInt32(DirectCast(e.Item, GridDataItem).GetDataKeyValue("EmployeeId"))
                EditTeacher1.EmployeeId = EmployeeId
                EditTeacher1.FillControlsForEditing()
                MvEmployee.ActiveViewIndex = 1
            End If
            BindData()
        Catch ex As Exception

        End Try
    End Sub

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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If SessionVariables.CultureInfo = "ar-JO" Then
            Lang = CtlCommon.Lang.AR

        Else
            Lang = CtlCommon.Lang.EN
        End If


    End Sub

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
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
        dir = GetPageDirection()
    End Sub

#End Region

#Region "Methods"
    Public Shared Function GetPageDirection() As String
        If SessionVariables.CultureInfo = "ar-JO" Then
            Return "rtl"
        Else
            Return "ltr"
        End If
    End Function

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", grdVwEmployees.Skin))
    End Function

    Private Sub BindData()
        Dim dtEmployees As New DataTable
        'Dim a As Integer
        'a = getTotalWeeklyCourses(EmployeeFilter.EmployeeId)

        If (EmployeeFilter.EmployeeId <> 0) Then
            objEmployee = New Employee()
            objEmployee.EmployeeId = EmployeeFilter.EmployeeId
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

End Class
