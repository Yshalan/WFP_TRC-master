Imports TA.Definitions
Imports System.Data
Imports TA.Lookup
Imports TA.DailyTasks
Imports SmartV.UTILITIES
Imports Telerik.Web.UI
Imports System.Globalization
Imports SmartV.Version
Imports TA.Security

Partial Class Definitions_RamadanPeriod
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Dim objRamadanPeriod As New RamadanPeriod

#End Region

#Region "Propereties"

    Public Property RamadanID() As Integer
        Get
            Return ViewState("RamadanID")
        End Get
        Set(ByVal value As Integer)
            ViewState("RamadanID") = value
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("RamadanPeriod", CultureInfo)

            dteFromDate.SelectedDate = Today
            dteToDate.SelectedDate = Today

            FillGrid()
        End If

        ibtnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrRamadanPeriod.ClientID + "')")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not pnlRamadanPeriod.FindControl(row("AddBtnName")) Is Nothing Then
                        pnlRamadanPeriod.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not pnlRamadanPeriod.FindControl(row("DeleteBtnName")) Is Nothing Then
                        pnlRamadanPeriod.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not pnlRamadanPeriod.FindControl(row("EditBtnName")) Is Nothing Then
                        pnlRamadanPeriod.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not pnlRamadanPeriod.FindControl(row("PrintBtnName")) Is Nothing Then
                        pnlRamadanPeriod.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Sub dgrRamadanPeriod_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrRamadanPeriod.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        ElseIf (e.Item.ItemType = GridItemType.Item) Then
            Dim Itm As GridDataItem = e.Item
        End If
    End Sub

    Protected Sub dgrRamadanPeriod_ItemDataBound(ByVal sender As Object, ByVal e As GridItemEventArgs) Handles dgrRamadanPeriod.ItemDataBound

        If ((e.Item.ItemType = GridItemType.Item) Or (e.Item.ItemType = GridItemType.AlternatingItem)) Then
            Dim item As GridDataItem
            item = e.Item

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("FromDate").ToString())) And (Not item.GetDataKeyValue("FromDate").ToString() = "")) Then
                Dim fromTime As DateTime = item.GetDataKeyValue("FromDate").ToString()
                item("FromDate").Text = fromTime.ToShortDateString()
            End If

            If ((Not String.IsNullOrEmpty(item.GetDataKeyValue("ToDate").ToString())) And (Not item.GetDataKeyValue("ToDate").ToString() = "")) Then
                Dim fromTime As DateTime = item.GetDataKeyValue("ToDate").ToString()
                item("ToDate").Text = fromTime.ToShortDateString()
            End If
        End If
    End Sub

    Protected Sub dgrRamadanPeriod_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrRamadanPeriod.NeedDataSource
        Dim dt As New DataTable
        dt = objRamadanPeriod.GetAll()
        dgrRamadanPeriod.DataSource = dt
    End Sub

    Protected Sub dgrRamadanPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dgrRamadanPeriod.SelectedIndexChanged

        If dgrRamadanPeriod.SelectedItems.Count = 1 Then

            RamadanID = CInt(CType(dgrRamadanPeriod.SelectedItems(0), GridDataItem).GetDataKeyValue("Id").ToString())
            objRamadanPeriod = New RamadanPeriod()
            With objRamadanPeriod
                .RamadanID = RamadanID
                .GetByPK()

                dteFromDate.SelectedDate = .FromDate
                dteToDate.SelectedDate = .ToDate
            End With
        End If

    End Sub

    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ibtnSave.Click

        objRamadanPeriod = New RamadanPeriod()
        Dim err As Integer
        With objRamadanPeriod
            Dim d1, d2 As DateTime
            d1 = dteFromDate.SelectedDate
            d2 = dteToDate.SelectedDate
            Dim diff As TimeSpan = d2.Subtract(d1)
            If diff.Days + 1 = 30 Or diff.Days + 1 = 29 Then

                .FromDate = dteFromDate.SelectedDate
                .ToDate = dteToDate.SelectedDate

                If RamadanID = 0 Then
                    .CREATED_BY = SessionVariables.LoginUser.UsrID

                    err = .Add()
                    If err = 0 Then
                        FillGrid()
                        CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                        ClearAll()
                    ElseIf err = -5 Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
                    Else
                        CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
                    End If
                Else
                    .RamadanID = RamadanID
                    .LAST_UPDATE_BY = SessionVariables.LoginUser.UsrID

                    err = .Update()
                    If err = 0 Then
                        FillGrid()
                        CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
                        ClearAll()
                    Else
                        CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
                    End If

                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DateExist", CultureInfo), "info")
                End If


                dteFromDate.SelectedDate = Today
                dteToDate.SelectedDate = Today
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("RamadnPeriodValidation", CultureInfo), "info")
            End If
        End With
    End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ibtnDelete.Click
        Dim err As Integer
        Dim counter As Integer = 0
        For Each row As GridDataItem In dgrRamadanPeriod.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                counter = counter + 1
                Dim RamadanId As Integer = Convert.ToInt32(row.GetDataKeyValue("Id").ToString())
                objRamadanPeriod = New RamadanPeriod()
                With objRamadanPeriod
                    .RamadanID = RamadanId
                    err += .Delete()
                End With
            End If
        Next
        If counter = 0 Then
            CtlCommon.ShowMessage(Me.Page, IIf(Lang = CtlCommon.Lang.AR, "الرجاء اختيار الفترة المراد حذفها", " Please Select Period to delete"), "info")
            Return
        End If
        If err = 0 Then
            FillGrid()
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub FillGrid()

        Dim dt As New DataTable
        dt = objRamadanPeriod.GetAll()
        dgrRamadanPeriod.DataSource = dt
        dgrRamadanPeriod.DataBind()

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrRamadanPeriod.Skin))
    End Function

    Private Sub ClearAll()

        dteFromDate.SelectedDate = Today
        dteToDate.SelectedDate = Today
        RamadanID = 0

    End Sub

#End Region

End Class
