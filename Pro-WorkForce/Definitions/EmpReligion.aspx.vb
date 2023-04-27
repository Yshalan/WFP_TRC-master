Imports System.Data
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports TA.Definitions

Partial Class Emp_EmpReligion
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objEmp_Religion As Emp_Religion
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Private Property ReligionId() As Integer
        Get
            Return ViewState("ReligionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ReligionId") = value
        End Set
    End Property

#End Region

#Region "PageEvents"


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
            UserCtrlReligion.HeaderText = ResourceManager.GetString("EmpReligion", CultureInfo)

            btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdVwReligion.ClientID + "');")
        End If
    End Sub

    Protected Sub dgrdVwReligion_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdVwReligion.NeedDataSource
        objEmp_Religion = New Emp_Religion()
        dgrdVwReligion.DataSource = objEmp_Religion.GetAll()
    End Sub

    Protected Sub dgrdVwReligion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdVwReligion.SelectedIndexChanged
        ReligionId = Convert.ToInt32(DirectCast(dgrdVwReligion.SelectedItems(0), GridDataItem).GetDataKeyValue("ReligionId").ToString())
        FillControls()
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, _
                                 ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
        FillGridView()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        objEmp_Religion = New Emp_Religion
        Dim errorNum As Integer = -1
        ' Set data into object for Add / Update
        With objEmp_Religion
            .ReligionCode = txtReligionCode.Text
            .ReligionName = txtReligioName.Text
            .ReligionArabicName = txtReligionArabicName.Text
            .Active = chckBoxActive.Checked
        End With
        If ReligionId = 0 Then
            ' Do add operation 
            errorNum = objEmp_Religion.Add()
            If errorNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        Else
            ' Do update operations
            objEmp_Religion.ReligionId = ReligionId
            errorNum = objEmp_Religion.Update()

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
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errNum As Integer = -1
        For Each row As GridDataItem In dgrdVwReligion.SelectedItems
            objEmp_Religion = New Emp_Religion()
            objEmp_Religion.ReligionId = Convert.ToInt32(row.GetDataKeyValue("ReligionId").ToString())
            errNum = objEmp_Religion.Delete()
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGridView()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CantDeleteReligion", CultureInfo), "error")

        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillControls()
        ' Get the data by the PK and display on the controls
        objEmp_Religion = New Emp_Religion
        objEmp_Religion.ReligionId = ReligionId
        objEmp_Religion.GetByPK()
        With objEmp_Religion
            txtReligionCode.Text = .ReligionCode
            txtReligioName.Text = .ReligionName
            txtReligionArabicName.Text = .ReligionArabicName
            chckBoxActive.Checked = .Active
        End With
    End Sub

    Private Sub ClearAll()
        ' Clear the cntrols
        txtReligioName.Text = String.Empty
        txtReligionArabicName.Text = String.Empty
        txtReligionCode.Text = String.Empty
        chckBoxActive.Checked = False
        ' Reset to next add operation
        ReligionId = 0
        ' Remove sorting and sorting arrow

    End Sub

    Private Sub FillGridView()
        Try
            objEmp_Religion = New Emp_Religion()
            dgrdVwReligion.DataSource = objEmp_Religion.GetAll()
            dgrdVwReligion.DataBind()
        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class