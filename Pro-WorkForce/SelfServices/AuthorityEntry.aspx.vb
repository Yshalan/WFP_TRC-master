Imports TA.DailyTasks
Imports SmartV.UTILITIES
Imports TA.Security
Imports System.Data
Imports TA.Definitions
Imports TA.Admin
Imports TA_AuthorityMovements
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Sockets
Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.DirectoryServices

Partial Class SelfServices_AuthorityEntry
    Inherits System.Web.UI.Page
#Region "Class Variables"


    Private Lang As CtlCommon.Lang
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objRECALC_REQUEST As RECALC_REQUEST
    Private objSysForms As SYSForms
    Private objAuthorities As Authorities
    Private objEmpMove As Emp_Move
    Private objTA_Reason As TA_Reason
    Private objAuthority_Movements As Authority_Movements
#End Region

#Region "Public Properties"

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

            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                dgrdAuthorityEntry.Columns(0).Visible = False
                dgrdAuthorityEntry.Columns(1).Visible = True
                dgrdAuthorityEntry.Columns(2).Visible = False
                dgrdAuthorityEntry.Columns(3).Visible = True

                rmtToTime2.Style("margin-left") = "0px"
                rmtToTime2.Style("margin-right") = "48px"
                RadDatePicker1.Style("margin-left") = "0px"
                RadDatePicker1.Style("margin-right") = "48px"

            Else
                Lang = CtlCommon.Lang.EN
                dgrdAuthorityEntry.Columns(0).Visible = True
                dgrdAuthorityEntry.Columns(1).Visible = False
                dgrdAuthorityEntry.Columns(2).Visible = True
                dgrdAuthorityEntry.Columns(3).Visible = False

            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            reqAuthorityName.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            getPageTitle()
            FillAuthority()
            FillReasons()
            FillGrid()

            rmtToTime2.Text = Now.TimeOfDay.ToString()
            RadDatePicker1.SelectedDate = Now
            RadDatePicker1.MaxDate = Now

            rmtToTime2.Enabled = False
            RadDatePicker1.Enabled = False



        End If
    End Sub


    Protected Sub btnSignIn_Click(sender As Object, e As System.EventArgs) Handles btnSignIn.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim err As Integer
        objAuthority_Movements = New Authority_Movements
        ''''
        ''''
        objTA_Reason = New TA_Reason
        objTA_Reason.ReasonCode = RadCmbReason.SelectedValue

        objTA_Reason.GetByPK()
        If objTA_Reason.Type = "O" Then
            If Lang = CtlCommon.Lang.AR Then
                CtlCommon.ShowMessage(Me.Page, "يرجى اختيار سبب دخول", "info")
            Else
                CtlCommon.ShowMessage(Me.Page, "Please Select IN Reason", "info")
            End If

            Exit Sub
        End If

        ''''
        With objAuthority_Movements
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FK_AuthoritId = RadCmbAuthorityName.SelectedValue
            .FK_ReasonId = RadCmbReason.SelectedValue
            .MoveDate = Now.Date
            .MoveTime = Now
            .Type = objTA_Reason.Type
            .Remarks = txtremarks.Text
            '.IP_Address = Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
            '.IP_Address = GetLocalIPv4(NetworkInterfaceType.Ethernet)
            If Request.UserHostAddress = "::1" Then
                .IP_Address = Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
            Else
                .IP_Address = Request.UserHostAddress
            End If

            .Domain = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString()
            .PCName = Dns.GetHostName()


            .CREATED_BY = SessionVariables.LoginUser.ID
            .CREATED_DATE = Now
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_DATE = Now

            err = .Add()

            If err = 0 Then

                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                FillGrid()
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")

            End If

        End With

    End Sub

    Protected Sub btnSignOut_Click(sender As Object, e As System.EventArgs) Handles btnSignOut.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim err As Integer
        objAuthority_Movements = New Authority_Movements
        ''''
        ''''
        objTA_Reason = New TA_Reason
        objTA_Reason.ReasonCode = RadCmbReason.SelectedValue

        objTA_Reason.GetByPK()
        If objTA_Reason.Type = "I" Then
            If Lang = CtlCommon.Lang.AR Then
                CtlCommon.ShowMessage(Me.Page, "يرجى اختيار سبب خروج", "info")
            Else
                CtlCommon.ShowMessage(Me.Page, "Please Select Out Reason", "info")
            End If

            Exit Sub
        End If

        ''''
        With objAuthority_Movements
            .FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
            .FK_AuthoritId = RadCmbAuthorityName.SelectedValue
            .FK_ReasonId = RadCmbReason.SelectedValue
            .MoveDate = Now.Date
            .MoveTime = Now
            .Type = objTA_Reason.Type
            .Remarks = txtremarks.Text
            If Request.UserHostAddress = "::1" Then
                .IP_Address = Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
            Else
                .IP_Address = Request.UserHostAddress
            End If
            .Domain = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString()
            .PCName = Dns.GetHostName()
            .CREATED_BY = SessionVariables.LoginUser.ID
            .CREATED_DATE = Now
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_DATE = Now

            err = .Add()

            If err = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                FillGrid()
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "errors")

            End If

        End With




    End Sub


#End Region


#Region "Methods"
    Private Sub getPageTitle()
        Dim objSysForms As New SYSForms
        Dim dt As New DataTable
        dt = objSysForms.GetByPK(966)
        If SessionVariables.CultureInfo = "ar-JO" Then
            PageHeader1.HeaderText = dt.Rows(0)("Desc_Ar")
        Else
            PageHeader1.HeaderText = dt.Rows(0)("Desc_En")

        End If
    End Sub

    Private Sub FillAuthority()
        Dim dt As DataTable = Nothing
        objAuthorities = New Authorities()
        dt = objAuthorities.GetAll()
        CtlCommon.FillTelerikDropDownList(RadCmbAuthorityName, dt, Lang)
    End Sub

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

    Private Sub FillReasons()
        objTA_Reason = New TA_Reason()
        CtlCommon.FillTelerikDropDownList(RadCmbReason, objTA_Reason.GetAll, Lang)
    End Sub

    Private Sub FillGrid()
        dgrdAuthorityEntry.DataSource = Nothing
        objAuthority_Movements = New Authority_Movements


        objAuthority_Movements.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        dtCurrent = objAuthority_Movements.GetAllByEmployeeId
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdAuthorityEntry.DataSource = dv
        dgrdAuthorityEntry.DataBind()
    End Sub
#End Region

#Region "Grid Filter"
    Protected Sub dgrdAuthorityEntry_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdAuthorityEntry.NeedDataSource
        dgrdAuthorityEntry.DataSource = Nothing
        objAuthority_Movements = New Authority_Movements


        objAuthority_Movements.FK_EmployeeId = SessionVariables.LoginUser.FK_EmployeeId
        dtCurrent = objAuthority_Movements.GetAllByEmployeeId
        Dim dv As New DataView(dtCurrent)
        dv.Sort = SortExepression
        dgrdAuthorityEntry.DataSource = dv


    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdAuthorityEntry.Skin))
    End Function

    Protected Sub dgrdAuthorityEntry_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdAuthorityEntry.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

#End Region
End Class
