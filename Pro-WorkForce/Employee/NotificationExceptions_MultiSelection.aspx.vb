Imports SmartV.UTILITIES
Imports TA.Employees
Imports System.Data
Imports TA.Admin
Imports System.Resources

Partial Class Employee_NotificationExceptions_MultiSelection
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Public Lang As CtlCommon.Lang
    Dim objNotification_Exception As New Notification_Exception
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

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

    Public Property NotificationExceptionId() As Integer
        Get
            Return ViewState("NotificationExceptionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("NotificationExceptionId") = value
        End Set
    End Property

    Public Property FK_EmployeeId() As Integer
        Get
            Return ViewState("FK_EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_EmployeeId") = value
        End Set
    End Property

    Public Property FK_LogicalGroupId() As Integer
        Get
            Return ViewState("FK_LogicalGroupId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_LogicalGroupId") = value
        End Set
    End Property

    Public Property FK_WorkLocationId() As Integer
        Get
            Return ViewState("FK_WorkLocationId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_WorkLocationId") = value
        End Set
    End Property

    Public Property FK_EntityId() As Integer
        Get
            Return ViewState("FK_EntityId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_EntityId") = value
        End Set
    End Property

    Public Property FromDate() As DateTime
        Get
            Return ViewState("FromDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("FromDate") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            FillGrid()

            If SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
                PageHeader1.HeaderText = ResourceManager.GetString("Notification_Exception", CultureInfo)
            Else
                Lang = CtlCommon.Lang.EN
                PageHeader1.HeaderText = ResourceManager.GetString("Notification_Exception", CultureInfo)

            End If

            If Lang = CtlCommon.Lang.AR Then
                dgrdNotificationExceptions.Columns(2).Visible = False 'Employee Name
                dgrdNotificationExceptions.Columns(4).Visible = False 'LG Name
                dgrdNotificationExceptions.Columns(6).Visible = False 'WL Name
                dgrdNotificationExceptions.Columns(8).Visible = False 'Entity Name

            Else
                dgrdNotificationExceptions.Columns(3).Visible = False 'Emaployee Arabic Name
                dgrdNotificationExceptions.Columns(5).Visible = False 'LG Arabic Name
                dgrdNotificationExceptions.Columns(7).Visible = False 'WL Arabic Name
                dgrdNotificationExceptions.Columns(9).Visible = False 'Entity Arabic Name
            End If
        End If
        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdNotificationExceptions.ClientID + "')")
    End Sub

    Protected Sub Employee_NotificationExceptions_MultiSelection_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
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

    Protected Sub dgrdNotificationExceptions_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdNotificationExceptions.ItemDataBound
        If e.Item.ItemType = GridItemType.AlternatingItem Or e.Item.ItemType = GridItemType.Item Then
            Dim item As GridDataItem
            item = e.Item

        End If
    End Sub

    Protected Sub dgrdNotificationExceptions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdNotificationExceptions.SelectedIndexChanged
        NotificationExceptionId = (Convert.ToInt32(DirectCast(dgrdNotificationExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("NotificationExceptionId")))
        If Not IsDBNull((DirectCast(dgrdNotificationExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_EmployeeId"))) Then
            FK_EmployeeId = Convert.ToInt32(DirectCast(dgrdNotificationExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_EmployeeId"))
            FromDate = Convert.ToDateTime(DirectCast(dgrdNotificationExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("FromDate"))
            tabNotification_Exception.ActiveTab = tabEmpException
            NotificationException_Employee.EmployeeId = FK_EmployeeId
            NotificationException_Employee.NotificationExceptionId = NotificationExceptionId
            NotificationException_Employee.FillControls()
        ElseIf Not IsDBNull((DirectCast(dgrdNotificationExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("GroupId"))) Then
            FK_LogicalGroupId = Convert.ToInt32(DirectCast(dgrdNotificationExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("GroupId"))
            FromDate = Convert.ToDateTime(DirectCast(dgrdNotificationExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("FromDate"))
            tabNotification_Exception.ActiveTab = tabLGException
            NotificationException_LogicalGroup.NotificationExceptionId = NotificationExceptionId
            NotificationException_LogicalGroup.FK_LogicalGroupId = FK_LogicalGroupId
            NotificationException_LogicalGroup.FillControls()
        ElseIf Not IsDBNull((DirectCast(dgrdNotificationExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("WorkLocationId"))) Then
            NotificationException_WorkLocation.NotificationExceptionId = NotificationExceptionId
            FK_WorkLocationId = Convert.ToInt32(DirectCast(dgrdNotificationExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("WorkLocationId"))
            FromDate = Convert.ToDateTime(DirectCast(dgrdNotificationExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("FromDate"))
            NotificationException_WorkLocation.FK_WorkLocationId = FK_WorkLocationId
            tabNotification_Exception.ActiveTab = tabWLException
            NotificationException_WorkLocation.FillControls()

        ElseIf Not IsDBNull((DirectCast(dgrdNotificationExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("EntityId"))) Then
            NotificationException_Entity.NotificationExceptionId = NotificationExceptionId
            FK_EntityId = Convert.ToInt32(DirectCast(dgrdNotificationExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("EntityId"))
            FromDate = Convert.ToDateTime(DirectCast(dgrdNotificationExceptions.SelectedItems(0), GridDataItem).GetDataKeyValue("FromDate"))
            NotificationException_Entity.FK_EntityId = FK_EntityId
            tabNotification_Exception.ActiveTab = tabEntityException
            NotificationException_Entity.FillControls()
        End If

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim err As Integer
        Dim chk As Boolean = False
        objNotification_Exception = New Notification_Exception()
        For Each row As GridDataItem In dgrdNotificationExceptions.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                chk = True
                NotificationExceptionId = Convert.ToInt32(row.GetDataKeyValue("NotificationExceptionId").ToString())
                With objNotification_Exception
                    .NotificationExceptionId = NotificationExceptionId
                    err += .Delete()
                End With
            End If
        Next
        If chk = False Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDeleteRecourd"), "info")
        Else
            If err = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully"), "sucess")
                FillGrid()
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete"), "error")
            End If
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub FillGrid()

        objNotification_Exception = New Notification_Exception()
        dtCurrent = objNotification_Exception.GetAllInnerEmployee()
        dgrdNotificationExceptions.DataSource = dtCurrent
        dgrdNotificationExceptions.DataBind()

    End Sub

#End Region

#Region "Grid Filter"

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdNotificationExceptions.Skin))
    End Function

    Protected Sub dgrdNotificationExceptions_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdNotificationExceptions.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            radfilter1.FireApplyCommand()
        End If
    End Sub

#End Region

    
End Class
