Imports System.Data
Imports TA.Employees
Imports TA.Admin
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.DailyTasks
Imports TA.Lookup

Partial Class Emp_EmpWorkLocations
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_WorkLocation As Emp_WorkLocation
    Private objTaPolicy As TAPolicy
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Private Property WorkLocationId() As Integer
        Get
            Return ViewState("WorkLocationId")
        End Get
        Set(ByVal value As Integer)
            ViewState("WorkLocationId") = value
        End Set
    End Property

#End Region

#Region "Page events"

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
            Else
                Lang = CtlCommon.Lang.EN
            End If
            FillGridView()
            FillList()
            userCtrlWorkLocation.HeaderText = ResourceManager.GetString("EmpWorkLoc", CultureInfo)
            btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdVwWorkLocation.ClientID + "');")

        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objEmp_WorkLocation = New Emp_WorkLocation
        Dim errorNum As Integer = -1
        ' Set data into object for Add / Update
        With objEmp_WorkLocation
            .WorkLocationArabicName = txtWorkLocationArabicName.Text
            .WorkLocationName = txtWorkLocationName.Text
            .WorkLocationCode = txtWorkLocationCode.Text
            .Active = chckActive.Checked
            .FK_TAPolicyId = RadCmbBxPolicy.SelectedValue
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_DATE = Today.Date
            .CREATED_BY = SessionVariables.LoginUser.ID
            .CREATED_DATE = Today.Date
        End With
        If WorkLocationId = 0 Then
            ' Do add operation 
            errorNum = objEmp_WorkLocation.Add()

            If errorNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ElseIf errorNum = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
            ElseIf errorNum = -6 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")
            ElseIf errorNum = -7 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")

            End If
        Else
            ' Do update operations
            objEmp_WorkLocation.WorkLocationId = WorkLocationId
            errorNum = objEmp_WorkLocation.Update()


            If errorNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            ElseIf errorNum = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
            ElseIf errorNum = -6 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")

            ElseIf errorNum = -7 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")

            End If

        End If
        If errorNum = 0 Then

            FillGridView()
            ClearAll()
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1
        objEmp_WorkLocation = New Emp_WorkLocation
        For Each row As GridDataItem In dgrdVwWorkLocation.SelectedItems

            Dim intWorkLocationId As Integer = Convert.ToInt32(row.GetDataKeyValue("WorkLocationId").ToString())
            Dim objEmp_DesignationLeavesTypes As New Emp_DesignationLeavesTypes
            objEmp_WorkLocation = New Emp_WorkLocation()
            objEmp_WorkLocation.WorkLocationId = intWorkLocationId
            errNum = objEmp_WorkLocation.Delete()
        Next

        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGridView()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If

    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillGridView()
        ClearAll()
    End Sub

    Protected Sub dgrdVwWorkLocation_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdVwWorkLocation.NeedDataSource
        FillGridView()
    End Sub

    Protected Sub dgrdVwWorkLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdVwWorkLocation.SelectedIndexChanged
        WorkLocationId = Convert.ToInt32(DirectCast(dgrdVwWorkLocation.SelectedItems(0), GridDataItem).GetDataKeyValue("WorkLocationId").ToString())
        FillControls()
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGridView()
        Try
            objEmp_WorkLocation = New Emp_WorkLocation()
            dgrdVwWorkLocation.DataSource = objEmp_WorkLocation.GetAllPolicy()
            dgrdVwWorkLocation.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillList()

        ' Fill terlerik Drop down list represents the FK for the work location  
        objTaPolicy = New TAPolicy()
        Dim dt As DataTable = objTaPolicy.GetAll()
        If dt IsNot Nothing Then
            ProjectCommon.FillRadComboBox(RadCmbBxPolicy, dt, "TAPolicyName", "TAPolicyArabicName")
        End If
    End Sub

    Private Sub ClearAll()
        ' Reset controls
        txtWorkLocationArabicName.Text = String.Empty
        txtWorkLocationCode.Text = String.Empty
        txtWorkLocationName.Text = String.Empty
        RadCmbBxPolicy.SelectedIndex = 0
        chckActive.Checked = False
        WorkLocationId = 0
    End Sub

    Private Sub FillControls()
        objEmp_WorkLocation = New Emp_WorkLocation()
        objEmp_WorkLocation.WorkLocationId = WorkLocationId
        objEmp_WorkLocation.GetByPK()
        With objEmp_WorkLocation
            txtWorkLocationCode.Text = .WorkLocationCode
            txtWorkLocationName.Text = .WorkLocationName
            txtWorkLocationArabicName.Text = .WorkLocationArabicName
            RadCmbBxPolicy.SelectedValue = .FK_TAPolicyId
            chckActive.Checked = .Active
        End With
    End Sub

#End Region

End Class
