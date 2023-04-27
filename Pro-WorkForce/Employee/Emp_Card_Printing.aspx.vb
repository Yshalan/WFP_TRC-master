Imports TA.Card_Request
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.Employees
Imports TA.Security
Imports TA.Admin

Partial Class Employee_Emp_Card_Printing
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objCard_Request As Card_Request
    Private objEmp_Cards As Emp_Cards
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Public Lang As CtlCommon.Lang
    Public MsgLang As String

    ''' <summary>
    ''' '''''''''''
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private objCardTypes As CardTypes
    Private objCard_Template As Card_Template
    Private objEmp_Designation As Emp_Designation
    Private objCardDesignations As Card_Designations
    Private objOrgLevel As OrgLevel

#End Region

#Region "Properties"

    Private Enum CardRequestStatus

        Pending = 1
        UnderPrinting = 2
        Printed = 3
        Rejected = 4

    End Enum

    Public Property dtGrid() As DataTable
        Get
            Return ViewState("dtGrid")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtGrid") = value
        End Set
    End Property
    Public Property dtChklst() As DataTable
        Get
            Return ViewState("dtChklst")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtChklst") = value
        End Set
    End Property
    Public Property dtDesigCard() As DataTable
        Get
            Return ViewState("dtDesigCard")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dtDesigCard") = value
        End Set
    End Property
    Public Property CardTypeId() As Integer
        Get
            Return ViewState("CardTypeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("CardTypeId") = value
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
                MsgLang = "ar"
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                Lang = CtlCommon.Lang.EN
                MsgLang = "en"
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
            Else
                Lang = CtlCommon.Lang.EN
            End If

            FillLevels()
            FillTemplates()
            FillGrid()
            FillCheckBox()
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            PageHeader1.HeaderText = ResourceManager.GetString("DefineCardType", CultureInfo)
        End If

    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        CardTypeId = CInt(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem).GetDataKeyValue("CardTypeId").ToString())
        AcceptCardRequest()
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        objCardTypes = New CardTypes
        Dim err As Integer
        With objCardTypes
            .CardTypeId = CardTypeId
            .CardTypeEn = txtCardTypeEn.Text
            .CardTypeAr = txtCardtypeAr.Text
            .CardRequestManagerLevelRequired = Convert.ToInt32(radcmbLevels.SelectedValue)
            .CardApproval = Convert.ToInt32(rlstApproval.SelectedValue)
            .Fk_TemplateId = Convert.ToInt32(ddlCardTemplate.SelectedValue)
            .CreatedBY = SessionVariables.LoginUser.ID
            .LastUpdatedBy = SessionVariables.LoginUser.ID
            If CardTypeId = 0 Then
                err = .Add
                CardTypeId = .CardTypeId
            Else
                err = .Update

            End If
        End With
        If err = 0 Then
            objCardDesignations = New Card_Designations
            With objCardDesignations
                .Fk_CardTypeId = CardTypeId
                .Delete()
                For Each li As ListItem In chklstAlwdDesg.Items
                    If li.Selected Then

                        .Fk_DesignationId = Convert.ToInt32(li.Value)
                        err = .Add
                    End If
                Next
            End With
        End If
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo))
            FillGrid()
            Clear()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo))
        End If
    End Sub
    Protected Sub btnClear_Click(sender As Object, e As EventArgs)
        Clear()
    End Sub
#End Region

#Region "Methods"

    Private Sub FillTemplates()
        objCard_Template = New Card_Template
        With objCard_Template
            CtlCommon.FillTelerikDropDownList(ddlCardTemplate, .GetAll, Lang)
        End With
    End Sub
    Private Sub AcceptCardRequest()
        objCardTypes = New CardTypes
        Dim err As Integer = -1
        rlstApproval.ClearSelection()
        With objCardTypes
            .CardTypeId = CardTypeId
            .GetByPK()
            txtCardtypeAr.Text = .CardTypeAr
            txtCardTypeEn.Text = .CardTypeEn
            radcmbLevels.SelectedValue = .CardRequestManagerLevelRequired.ToString()
            ddlCardTemplate.SelectedValue = .Fk_TemplateId.ToString()
            rlstApproval.SelectedValue = .CardApproval.ToString()
        End With
        objCardDesignations = New Card_Designations
        With objCardDesignations
            .Fk_CardTypeId = CardTypeId
            dtDesigCard = .GetAllByCardType()
        End With
        chklstAlwdDesg.ClearSelection()
        For index As Integer = 0 To dtDesigCard.Rows.Count - 1
            For Each li As ListItem In chklstAlwdDesg.Items
                If li.Value = dtDesigCard.Rows(index)("Fk_DesignationId").ToString() Then
                    li.Selected = True
                End If
            Next
        Next
    End Sub
    Private Sub FillGrid()
        objCardTypes = New CardTypes
        With objCardTypes
            dtGrid = .GetAll()
        End With
        dgrdCardTypes.DataSource = dtGrid
        dgrdCardTypes.DataBind()
    End Sub
    Private Sub FillCheckBox()
        objEmp_Designation = New Emp_Designation
        With objEmp_Designation
            dtChklst = .GetAll()
        End With
        chklstAlwdDesg.DataSource = dtChklst
        chklstAlwdDesg.DataTextField = "DesignationName"
        chklstAlwdDesg.DataValueField = "DesignationId"
        chklstAlwdDesg.DataBind()
    End Sub
    Private Sub Clear()
        txtCardtypeAr.Text = String.Empty
        txtCardTypeEn.Text = String.Empty
        CardTypeId = 0
        radcmbLevels.ClearSelection()

        ddlCardTemplate.ClearSelection()
        chklstAlwdDesg.ClearSelection()
        rlstApproval.ClearSelection()
    End Sub
    Private Sub FillLevels()
        objOrgLevel = New OrgLevel
        With objOrgLevel
            CtlCommon.FillTelerikDropDownList(radcmbLevels, .GetAll_Company, Lang)
        End With
    End Sub
#End Region

End Class
