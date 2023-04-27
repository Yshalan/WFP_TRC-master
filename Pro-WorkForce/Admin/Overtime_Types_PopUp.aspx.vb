
Imports SmartV.UTILITIES
Imports TA.Definitions
Imports TA.OverTime

Partial Class Admin_Overtime_Types_PopUp
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objLeavesTypes As LeavesTypes
    Private objOvertime_Types As Overtime_Types

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Public Properties"

    Public Property OvertimeTypeId() As String
        Get
            Return ViewState("OvertimeTypeId")
        End Get
        Set(ByVal value As String)
            ViewState("OvertimeTypeId") = value
        End Set
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
        Else
            If SessionVariables.CultureInfo = "en-US" Then
                SessionVariables.CultureInfo = "en-US"
            Else
                SessionVariables.CultureInfo = "ar-JO"
            End If
        End If
    End Sub

    Private Sub Admin_Overtime_Types_PopUp_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            FillGrid()
            FillLeavesTypes()
            rmtOvertimeTime.TextWithLiterals = "0000"
            PageHeader1.HeaderText = ResourceManager.GetString("OvertimeTypes", CultureInfo)
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdOvertimeType.ClientID + "');")

    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim err As Integer = -1
        objOvertime_Types = New Overtime_Types
        With objOvertime_Types
            .OvertimeTypeName = txtOvertimeTypeName.Text
            .OvertimeTypeArabicName = txtOvetimeTypeArabicName.Text
            .OvertimeCalculationConsideration = rblOvertimeCalculationConsideration.SelectedValue
            If rblOvertimeCalculationConsideration.SelectedValue = 1 Then
                .OvertimeRate = txtOvertimeRate.Text
            Else
                .OvertimeChangeValue = (CInt(rmtOvertimeTime.TextWithLiterals.Split(":")(0)) * 60) + CInt(rmtOvertimeTime.TextWithLiterals.Split(":")(1))
            End If


            If rblOvertimeConsideration.SelectedValue = 1 Then
                .CompensateToLeave = False
            Else
                .CompensateToLeave = True
            End If


            If rblOvertimeConsideration.SelectedValue = 2 Then
                .FK_LeaveTypeId = RadComboBoxLeaveType.SelectedValue
            Else
                .FK_LeaveTypeId = Nothing
            End If

            .MustRequested = chkMustRequested.Checked

            If OvertimeTypeId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
            Else
                .OvertimeTypeId = OvertimeTypeId
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                err = .Update
            End If

        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully"), "success")
            FillGrid()
            ClearAll()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave"), "error")
        End If

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdOvertimeType.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objOvertime_Types = New Overtime_Types
                objOvertime_Types.OvertimeTypeId = Convert.ToInt32(row.GetDataKeyValue("OvertimeTypeId").ToString())
                errNum = objOvertime_Types.Delete()

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

    Private Sub dgrdOvertimeType_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdOvertimeType.NeedDataSource
        objOvertime_Types = New Overtime_Types
        With objOvertime_Types
            dgrdOvertimeType.DataSource = .GetAll
        End With
    End Sub

    Protected Sub dgrdOvertimeType_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdOvertimeType.Skin))
    End Function

    Private Sub dgrdOvertimeType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdOvertimeType.SelectedIndexChanged
        OvertimeTypeId = Convert.ToInt32(DirectCast(dgrdOvertimeType.SelectedItems(0), GridDataItem).GetDataKeyValue("OvertimeTypeId").ToString())
        FillControls()
    End Sub

    Private Sub rblOvertimeConsideration_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblOvertimeConsideration.SelectedIndexChanged
        If rblOvertimeConsideration.SelectedValue = 2 Then
            dvLeaveType.Visible = True
        Else
            dvLeaveType.Visible = False
        End If
    End Sub

    Private Sub rblOvertimeCalculationConsideration_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblOvertimeCalculationConsideration.SelectedIndexChanged
        If rblOvertimeCalculationConsideration.SelectedValue = 1 Then
            dvOvertimeCalcConsiderationRate.Visible = True
            dvOvertimeCalcConsiderationTime.Visible = False
        Else
            dvOvertimeCalcConsiderationRate.Visible = False
            dvOvertimeCalcConsiderationTime.Visible = True
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillLeavesTypes()
        objLeavesTypes = New LeavesTypes
        With objLeavesTypes
            CtlCommon.FillTelerikDropDownList(RadComboBoxLeaveType, .GetAll, Lang)
        End With

    End Sub

    Private Sub FillGrid()
        objOvertime_Types = New Overtime_Types
        With objOvertime_Types
            dgrdOvertimeType.DataSource = .GetAll
            dgrdOvertimeType.DataBind()
        End With
    End Sub

    Private Sub ClearAll()
        txtOvertimeTypeName.Text = String.Empty
        txtOvetimeTypeArabicName.Text = String.Empty
        txtOvertimeRate.Text = String.Empty
        rblOvertimeConsideration.SelectedValue = 1
        RadComboBoxLeaveType.SelectedValue = -1
        dvLeaveType.Visible = False
        chkMustRequested.Checked = False
        rblOvertimeCalculationConsideration.SelectedValue = 1
        dvOvertimeCalcConsiderationRate.Visible = True
        dvOvertimeCalcConsiderationTime.Visible = False
        rmtOvertimeTime.TextWithLiterals = "0000"
        OvertimeTypeId = 0
    End Sub

    Private Sub FillControls()
        objOvertime_Types = New Overtime_Types
        With objOvertime_Types
            .OvertimeTypeId = OvertimeTypeId
            .GetByPK()
            txtOvertimeTypeName.Text = .OvertimeTypeName
            txtOvetimeTypeArabicName.Text = .OvertimeTypeArabicName

            rblOvertimeCalculationConsideration.SelectedValue = .OvertimeCalculationConsideration
            If .OvertimeCalculationConsideration = 1 Then
                txtOvertimeRate.Text = .OvertimeRate
                dvOvertimeCalcConsiderationRate.Visible = True
                rmtOvertimeTime.TextWithLiterals = "0000"
                dvOvertimeCalcConsiderationTime.Visible = False
            Else
                txtOvertimeRate.Text = .OvertimeRate
                dvOvertimeCalcConsiderationRate.Visible = False
                rmtOvertimeTime.TextWithLiterals = CtlCommon.GetFullTimeString(.OvertimeChangeValue)
                dvOvertimeCalcConsiderationTime.Visible = True
            End If


            If .CompensateToLeave = False Then
                rblOvertimeConsideration.SelectedValue = 1
            Else
                rblOvertimeConsideration.SelectedValue = 2
            End If


            If rblOvertimeConsideration.SelectedValue = 2 Then
                dvLeaveType.Visible = True
                RadComboBoxLeaveType.SelectedValue = .FK_LeaveTypeId
            Else
                dvLeaveType.Visible = False
                RadComboBoxLeaveType.SelectedValue = -1
            End If
            chkMustRequested.Checked = .MustRequested
        End With
    End Sub

#End Region

End Class
