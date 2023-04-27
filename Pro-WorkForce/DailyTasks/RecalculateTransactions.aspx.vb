Imports System.Data
Imports TA.DashBoard
Imports SmartV.UTILITIES
Imports TA.Security
Imports TA.DailyTasks
Imports Telerik.Web.UI.DatePickerPopupDirection

Partial Class DailyTasks_RecalculateTransactions
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objRecalculateRequest As RecalculateRequest
#End Region

#Region "Propereties"

    Public Property CompanyId() As Integer
        Get
            Return ViewState("CompanyID")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyID") = value
        End Set
    End Property

    Public Property EntityId() As Integer
        Get
            Return ViewState("EntityID")
        End Get
        Set(ByVal value As Integer)
            ViewState("EntityID") = value
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

    Protected Sub Page_PreRenderComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRenderComplete
        If Lang = CtlCommon.Lang.AR Then
            CType(pnlFilter.FindControl("lblCaption"), Label).Text = "قد تتطلب العملية بضع دقائق."
        Else
            CType(pnlFilter.FindControl("lblCaption"), Label).Text = "Process Could take a few minutes."
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Emp_Filter.CompanyRequiredValidationGroup = btnRecalculate.ValidationGroup
            Emp_Filter.IsLevelRequired = False
            Emp_Filter.ValidationGroup = btnRecalculate.ValidationGroup

            PageHeader1.HeaderText = ResourceManager.GetString("RecalculateTransactions", CultureInfo)
            dteFromDate.SelectedDate = Date.Today
            dteToDate.SelectedDate = Date.Today

            SetRadDateTimePickerPeoperties()

        End If
        dteRequestStartDateTime.TimeView.HeaderText = String.Empty
        dteRequestStartDateTime.TimeView.TimeFormat = "HH:mm"
        dteRequestStartDateTime.TimeView.DataBind()

    End Sub

    Protected Sub btnRecalculate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRecalculate.Click
        objRecalculateRequest = New RecalculateRequest
        Dim errNo As Integer = -1
        With objRecalculateRequest
            .Fk_CompanyId = Emp_Filter.CompanyId
            If Emp_Filter.FilterType = "C" Then
                .Fk_EntityId = Emp_Filter.EntityId
            ElseIf Emp_Filter.FilterType = "L" Then
                .FK_LogicalGroupId = Emp_Filter.EntityId
            ElseIf Emp_Filter.FilterType = "W" Then
                .FK_WorkLocation = Emp_Filter.EntityId
            Else
                .Fk_EntityId = Emp_Filter.EntityId
            End If
            .Fk_EmployeeId = Emp_Filter.EmployeeId
            .FromDate = dteFromDate.SelectedDate
            .ToDate = dteToDate.SelectedDate
            .ImmediatelyStart = chbImmediatelyStart.Checked
            .RecalStartDateTime = Nothing
            .RecalStatus = 0
            .ReCalEndDateTime = Nothing
            If dteRequestStartDateTime.SelectedDate Is Nothing Then
                .RequestStartDateTime = DateTime.Now
            Else
                .RequestStartDateTime = dteRequestStartDateTime.SelectedDate
            End If

            .CREATED_BY = SessionVariables.LoginUser.fullName
            .CREATED_DATE = DateTime.Now
            errNo = .Add
            If errNo = 0 Then
                fillData()
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        End With
    End Sub

    Protected Sub dgrdRecalculateRequest_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdRecalculateRequest.Skin))
    End Function

    Protected Sub dgrdRecalculateRequest_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdRecalculateRequest.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataitem As GridDataItem = CType(e.Item, GridDataItem)
            Dim img As Image = DirectCast(dataitem("ImgRecalStatus").FindControl("imgRecalStatus"), Image)
            Dim imgDelete As ImageButton = DirectCast(dataitem("Delete").FindControl("imgDelete"), ImageButton)

            If dataitem("ImmediatelyStart").Text = "True" Then
                dataitem("ImmediatelyStart").Text = GetLocalResourceObject("Yes")
            Else
                dataitem("ImmediatelyStart").Text = GetLocalResourceObject("No")
            End If


            If dataitem("RecalStatus").Text = "0" Then
                img.ImageUrl = "~/assets/img/stopwatch.png"
                imgDelete.Visible = True
                imgDelete.ToolTip = "Delete"
                img.ToolTip = "Pending"
            ElseIf dataitem("RecalStatus").Text = "1" Then
                img.ImageUrl = "~/assets/img/loading.gif"
                img.ToolTip = "In Process"
            ElseIf dataitem("RecalStatus").Text = "2" Then
                img.ImageUrl = "~/assets/img/icon.png"
                img.ToolTip = "Completed"
            End If

            imgDelete.OnClientClick = String.Format("return confirm('{0}')", Resources.Strings.ConfirmDelete)


        End If
    End Sub

    Protected Sub dgrdRecalculateRequest_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdRecalculateRequest.NeedDataSource
        objRecalculateRequest = New RecalculateRequest
        With objRecalculateRequest
            dgrdRecalculateRequest.DataSource = .GetAll
        End With
    End Sub

    Protected Sub btnFilter_Click(sender As Object, e As System.EventArgs) Handles btnFilter.Click
        fillData()
    End Sub

    Protected Sub dgrdRecalculateRequest_PreRender(sender As Object, e As System.EventArgs) Handles dgrdRecalculateRequest.PreRender

        If SessionVariables.CultureInfo = "ar-JO" Then
            dgrdRecalculateRequest.MasterTableView.GetColumn("CompanyArabicName").Display = True
            dgrdRecalculateRequest.MasterTableView.GetColumn("EntityArabicName").Display = True
        Else
            dgrdRecalculateRequest.MasterTableView.GetColumn("CompanyName").Display = True
            dgrdRecalculateRequest.MasterTableView.GetColumn("EntityName").Display = True
        End If
    End Sub

    Protected Sub chbImmediatelyStart_CheckedChanged(sender As Object, e As System.EventArgs) Handles chbImmediatelyStart.CheckedChanged
        If chbImmediatelyStart.Checked Then
            pRequestStartDateTime.Visible = False
        Else
            pRequestStartDateTime.Visible = True
        End If
    End Sub

    Protected Sub imgDelete_OnCommand(sender As Object, e As EventArgs)
        Dim lnk As ImageButton = CType(sender, ImageButton)
        Dim errNo As Integer = -1
        objRecalculateRequest = New RecalculateRequest
        With objRecalculateRequest
            .RequestId = Convert.ToInt32(lnk.CommandArgument)
            errNo = .Delete
            If errNo = 0 Then
                fillData()
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
            End If

        End With
    End Sub

#End Region

#Region "Page Method"

    Private Function DateToString(ByVal TempDate As Date) As String
        Dim tempDay As String
        Dim tempMonth As String

        If TempDate.Month.ToString.Length = 1 Then
            tempMonth = "0" + TempDate.Month.ToString
        Else
            tempMonth = TempDate.Month.ToString
        End If
        If TempDate.Day.ToString.Length = 1 Then
            tempDay = "0" + TempDate.Day.ToString
        Else
            tempDay = TempDate.Day.ToString
        End If
        Return TempDate.Year.ToString() + tempMonth + tempDay
    End Function

    Protected Sub fillData()
        objRecalculateRequest = New RecalculateRequest
        With objRecalculateRequest
            If dteFromDateGrid.SelectedDate Is Nothing Then
            Else
                .FromDate = dteFromDateGrid.SelectedDate
            End If

            If dteToDateGrid.SelectedDate Is Nothing Then
            Else
                .ToDate = dteToDateGrid.SelectedDate
            End If


            dgrdRecalculateRequest.DataSource = .GetAll
            dgrdRecalculateRequest.DataBind()
        End With
    End Sub

    Private Sub SetRadDateTimePickerPeoperties()


        ' This function set properties for terlerik controls

        'Imports Telerik.Web.UI.DatePickerPopupDirection

        ' Set TimeView properties 
        Me.dteRequestStartDateTime.TimeView.StartTime = New TimeSpan(0, 0, 0)
        Me.dteRequestStartDateTime.TimeView.EndTime = New TimeSpan(23.5, 0, 0)
        Me.dteRequestStartDateTime.TimeView.HeaderText = String.Empty
        Me.dteRequestStartDateTime.TimeView.Interval = New TimeSpan(0, 30, 0)
        Me.dteRequestStartDateTime.TimeView.Columns = 5

        ' Set Popup window properties
        Me.dteRequestStartDateTime.PopupDirection = TopRight
        Me.dteRequestStartDateTime.ShowPopupOnFocus = True



        ' Set default value
        Me.dteRequestStartDateTime.SelectedDate = Now

    End Sub

#End Region

End Class
