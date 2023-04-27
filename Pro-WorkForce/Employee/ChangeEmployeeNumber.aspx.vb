Imports Telerik.Web.UI
Imports TA.Employees
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Security
Imports TA.Admin
Imports TA.Definitions

Partial Class Employee_ChangeEmployeeNumber
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmployeesNumberLog As EmployeesNumberLog
    Private objEmployee As Employee
    Private objAPP_Settings As APP_Settings
    Private objEmployee_Type As Employee_Type
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Public Property dtCurrent() As DataTable
        Get
            Return ViewState("dtCurrent")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtCurrent") = value
        End Set
    End Property

    Public Property dtGrid() As DataTable
        Get
            Return ViewState("dtGrid")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtGrid") = value
        End Set
    End Property

    Public Property EmpNumberlogId() As Integer
        Get
            Return ViewState("EmpNumberlogId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmpNumberlogId") = value
        End Set
    End Property

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Public Property IsInternal() As Boolean
        Get
            Return ViewState("IsInternal")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsInternal") = value
        End Set
    End Property

#End Region

#Region "Page Events"

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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                dtpFromDate.Culture = New System.Globalization.CultureInfo("ar-EG", False)
                dtpToDate.Culture = New System.Globalization.CultureInfo("ar-EG", False)
            Else
                Lang = CtlCommon.Lang.EN
                dtpFromDate.Culture = New System.Globalization.CultureInfo("en-US", False)
                dtpToDate.Culture = New System.Globalization.CultureInfo("en-US", False)
            End If
            EmployeeFilterUC.ValidationGroup = btnSave.ValidationGroup
            EmployeeFilterUC.CompanyRequiredValidationGroup = btnSave.ValidationGroup
            EmployeeFilterUC.EmployeeRequiredValidationGroup = btnSave.ValidationGroup
            EmployeeFilterUC.IsEmployeeRequired = True
            EmployeeFilterUC.CompanyRequiredValidationGroup = True
            EmployeeFilterUC.IsLevelRequired = True
            FillGrid()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("ChangeEmpNo", CultureInfo)

            dtpFromDate.DbSelectedDate = Date.Today
            dtpToDate.DbSelectedDate = Date.Today
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdChangeNo.ClientID + "');")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not update1.FindControl(row("AddBtnName")) Is Nothing Then
                        update1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not update1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        update1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not update1.FindControl(row("EditBtnName")) Is Nothing Then
                        update1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not update1.FindControl(row("PrintBtnName")) Is Nothing Then
                        update1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdChangeNo.Skin))
    End Function

    Protected Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdChangeNo.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                EmpNumberlogId = Convert.ToInt32(row.GetDataKeyValue("EmpNumberlogId").ToString())
                objEmployeesNumberLog = New EmployeesNumberLog
                objEmployeesNumberLog.EmpNumberlogId = EmpNumberlogId
                errNum = objEmployeesNumberLog.Delete()
                With strBuilder

                End With
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If
        ClearAll()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        objAPP_Settings = New APP_Settings
        objEmployee = New Employee
        objEmployee_Type = New Employee_Type
        objAPP_Settings.GetByPK()
        EmployeeId = EmployeeFilterUC.EmployeeId
        objEmployee.EmployeeId = EmployeeId
        objEmployee.GetByPK()

        objEmployee_Type.EmployeeTypeId = objEmployee.FK_EmployeeTypeId
        objEmployee_Type.GetInternal()

        IsInternal = objEmployee_Type.IsInternaltype

        If objAPP_Settings.AllowChangeEmpNo = 2 Then
            If IsInternal = True Then
                SaveEmpNo()
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("CannotChangeExternalNo", CultureInfo), "info")
            End If

        ElseIf objAPP_Settings.AllowChangeEmpNo = 3 Then
            If IsInternal = False Then
                SaveEmpNo()
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("CannotChangeInternalNo", CultureInfo), "info")
            End If
        Else
            SaveEmpNo()
        End If

    End Sub

    Protected Sub dgrdChangeNo_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdChangeNo.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item
            If Lang = CtlCommon.Lang.AR Then
                item("EmployeeName").Text = DirectCast(item.FindControl("hdnEmployeeNameAr"), HiddenField).Value
            End If
        End If
    End Sub

    Protected Sub dgrdChangeNo_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdChangeNo.NeedDataSource
        objEmployeesNumberLog = New EmployeesNumberLog
        'With objEmployeesNumberLog
        '    dtGrid = .GetAll_Inner
        'End With
        dgrdChangeNo.DataSource = dtGrid
    End Sub

#End Region

#Region "Methods"
    Private Sub FillGrid()
        objEmployeesNumberLog = New EmployeesNumberLog
        With objEmployeesNumberLog
            dtGrid = .GetAll_Inner
        End With
        dgrdChangeNo.DataSource = dtGrid
        dgrdChangeNo.DataBind()
    End Sub

    Private Sub ClearAll()
        EmployeeFilterUC.ClearValues()
        txtNewEmpNo.Text = String.Empty
        txtReason.Text = String.Empty
        EmpNumberlogId = 0
        FillGrid()
    End Sub

    Private Sub SaveEmpNo()
        Dim err As Integer = -1
        objEmployee = New Employee
        objEmployeesNumberLog = New EmployeesNumberLog

        With objEmployeesNumberLog
            .FK_EmployeeId = EmployeeId
            .FK_CompanyId = EmployeeFilterUC.CompanyId
            .OldEmpNo = EmployeeFilterUC.EmpNo
            .NewEmpNo = txtNewEmpNo.Text
            .Reason = txtReason.Text
            .CREATED_BY = SessionVariables.LoginUser.ID
            err = .Add
        End With

        If err = 0 Then
            objEmployee.EmployeeId = objEmployeesNumberLog.FK_EmployeeId
            objEmployee.EmployeeNo = txtNewEmpNo.Text
            objEmployee.Update_EmpNo()
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            FillGrid()
            ClearAll()
        ElseIf err = -5 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("EmpNoExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        objEmployeesNumberLog = New EmployeesNumberLog
        With objEmployeesNumberLog
            .FromDate = dtpFromDate.DbSelectedDate
            .ToDate = dtpToDate.DbSelectedDate
            dtGrid = .GetAll_Inner
        End With
        dgrdChangeNo.DataSource = dtGrid
        dgrdChangeNo.DataBind()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

#End Region

End Class
