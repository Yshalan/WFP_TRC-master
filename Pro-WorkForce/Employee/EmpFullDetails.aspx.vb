Imports TA.Employees
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports System.Data

Partial Class Employee_EmpFullDetails
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmployee As New Employee
    Private Lang As CtlCommon.Lang
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objVersion As SmartV.Version.version
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))

#End Region

#Region "Properties"
    Private Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
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
    Public Property SortExepression() As String
        Get
            Return ViewState("SortExepression")
        End Get
        Set(ByVal value As String)
            ViewState("SortExepression") = value
        End Set
    End Property
#End Region

#Region "Page Events"

    Protected Sub Employee_EmpFullDetails_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
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

    Protected Sub Employee_EmpFullDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                dgrdEmpDetails.Columns(1).Visible = False
                dgrdEmpDetails.Columns(3).Visible = False
                dgrdEmpDetails.Columns(5).Visible = False
                dgrdEmpDetails.Columns(7).Visible = False
                dgrdEmpDetails.Columns(9).Visible = False
                dgrdEmpDetails.Columns(11).Visible = False
                dgrdEmpDetails.Columns(15).Visible = False
                dgrdEmpDetails.Columns(17).Visible = False
                dgrdEmpDetails.Columns(19).Visible = False
            Else
                Lang = CtlCommon.Lang.EN
                dgrdEmpDetails.Columns(2).Visible = False
                dgrdEmpDetails.Columns(4).Visible = False
                dgrdEmpDetails.Columns(6).Visible = False
                dgrdEmpDetails.Columns(8).Visible = False
                dgrdEmpDetails.Columns(10).Visible = False
                dgrdEmpDetails.Columns(12).Visible = False
                dgrdEmpDetails.Columns(16).Visible = False
                dgrdEmpDetails.Columns(18).Visible = False
                dgrdEmpDetails.Columns(20).Visible = False
            End If
            PageHeader1.HeaderText = ResourceManager.GetString("EmpDetails", CultureInfo)
            'fillgrid()
        End If
    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow.Click
        EmployeeId = objEmp_Filter.EmployeeId
        If EmployeeId = 0 Then
            CtlCommon.ShowMessage(Me.Page, IIf(Lang = CtlCommon.Lang.AR, "الرجاء اختيار الموظف", " Please Select Employee"), "info")
            Return
        End If
        fillgrid()
    End Sub

    Protected Sub dgrdEmpDetails_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs) Handles dgrdEmpDetails.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmpDetails.Skin))
    End Function

    Protected Sub dgrdEmpDetails_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmpDetails.NeedDataSource
        Try
            objEmployee = New Employee
            objEmployee.FK_CompanyId = objEmp_Filter.CompanyId
            objEmployee.FK_EntityId = objEmp_Filter.EntityId
            objEmployee.EmployeeId = EmployeeId
            'dgrdEmpDetails.DataSource = objEmployee.GetEmpDetails()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dgrdEmpDetails_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdEmpDetails.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item) Then
            Dim Itm As GridDataItem = e.Item
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub fillgrid()
        objEmployee = New Employee
        objEmployee.FK_CompanyId = objEmp_Filter.CompanyId
        objEmployee.FK_EntityId = objEmp_Filter.EntityId
        objEmployee.EmployeeId = EmployeeId
        'dgrdEmpDetails.DataSource = objEmployee.GetEmpDetails
        'DataBind()

        '''''''''''''''''''''''''''''
        Dim dt As DataTable
        Dim found As Boolean = False
        Dim delete As Boolean = False
        'Dim value As Integer
        dt = objEmployee.GetEmpDetails()
        dtCurrent = dt
        'Dim EmployeeNo As String
        Dim ArrEmployee As ArrayList = New ArrayList
        'For i As Integer = 0 To dt.Rows.Count - 1
        '    ArrEmployee = New ArrayList
        '    found = False
        '    delete = False
        '    EmployeeNo = (dt.Rows(i).Item(0, DataRowVersion.Original).ToString()).ToString
        '    For j As Integer = 0 To dt.Rows.Count - 1
        '        If ((dt.Rows(j).Item(0, DataRowVersion.Original).ToString()).ToString = EmployeeNo) Then
        '            If Lang = CtlCommon.Lang.EN Then
        '                ArrEmployee.Add(dt.Rows(j).Item(13, DataRowVersion.Original).ToString())
        '            Else
        '                ArrEmployee.Add(dt.Rows(j).Item(14, DataRowVersion.Original).ToString())
        '            End If

        '        End If
        '    Next

        '    Dim stringBuilder As StringBuilder = New StringBuilder

        '    For arr As Integer = 0 To ArrEmployee.Count - 1
        '        If Not String.IsNullOrEmpty(ArrEmployee(arr).ToString()) Then
        '            stringBuilder.AppendFormat("{0}, ", ArrEmployee(arr).ToString(), Environment.NewLine)
        '        End If
        '    Next

        '    For current As Integer = 0 To dtCurrent.Rows.Count - 1
        '        If (dtCurrent.Rows(current).Item(0, DataRowVersion.Original) = EmployeeNo And found = False) Then
        '            found = True
        '            If Lang = CtlCommon.Lang.EN Then
        '                dtCurrent.Rows(current).Item(13) = stringBuilder.ToString()
        '            Else
        '                dtCurrent.Rows(current).Item(14) = stringBuilder.ToString()
        '            End If
        '        End If

        '        If (found) Then
        '            If (dt.Rows(current).Item(0, DataRowVersion.Original) = EmployeeNo) Then
        '                If (delete = True) Then
        '                    dt.Rows(current).Delete()
        '                    value = i
        '                Else
        '                    delete = True
        '                End If
        '            End If
        '        End If
        '    Next

        'Next

        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdEmpDetails.DataSource = dt
        dgrdEmpDetails.DataBind()
    End Sub

#End Region

End Class
