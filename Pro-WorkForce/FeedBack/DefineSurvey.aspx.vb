
Imports System.Data
Imports SmartV.UTILITIES
Imports TA.FeedBack
Imports TA.Security

Partial Class FeedBack_DefineSurvey
    Inherits System.Web.UI.Page

#Region "Class Variable"

    Private objFeedback_Survey As Feedback_Survey
    Private objFeedback_Questions As Feedback_Questions
    Private objFeedback_Answers As Feedback_Answers
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Public Properties"

    Public Property SurveyId() As Integer
        Get
            Return ViewState("SurveyId")
        End Get
        Set(ByVal value As Integer)
            ViewState("SurveyId") = value
        End Set
    End Property

    Public Property QuestionId() As Integer
        Get
            Return ViewState("QuestionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("QuestionId") = value
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

    Private Sub FeedBack_DefineSurvey_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If

            FillGridSurvey()
            PageHeader1.HeaderText = ResourceManager.GetString("DefineSurvey", CultureInfo)
        End If

        btnDeleteQuestions.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdQuestions.ClientID + "');")

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

    Protected Sub dgrdSurvey_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdSurvey.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdSurvey.Skin))
    End Function

    Protected Sub dgrdQuestions_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdQuestions.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter2.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar2_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon1() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdQuestions.Skin))
    End Function

    Private Sub dgrdSurvey_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdSurvey.NeedDataSource
        objFeedback_Survey = New Feedback_Survey
        With objFeedback_Survey
            dgrdSurvey.DataSource = .GetAll
        End With
    End Sub

    Private Sub dgrdSurvey_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdSurvey.SelectedIndexChanged
        SurveyId = Convert.ToInt32(DirectCast(dgrdSurvey.SelectedItems(0), GridDataItem).GetDataKeyValue("SurveyId"))
        FillSurveyControls()
        FillQuestionsGrid()
    End Sub

    Private Sub dgrdSurvey_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdSurvey.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            Dim item As GridDataItem = CType(e.Item, GridDataItem)
            item = e.Item
            If Not item("SurveyLanguage").Text = "&nbsp;" Then
                If item("SurveyLanguage").Text = "1" Then
                    item("SurveyLanguage").Text = IIf(Lang = CtlCommon.Lang.AR, "الانجليزية", "English")
                ElseIf item("SurveyLanguage").Text = "2" Then
                    item("SurveyLanguage").Text = IIf(Lang = CtlCommon.Lang.AR, "العربية", "Arabic")
                Else
                    item("SurveyLanguage").Text = IIf(Lang = CtlCommon.Lang.AR, "العربية والانجليزية", "Arabic & English")
                End If
            End If

            If Not item("HasWeightage").Text = "&nbsp;" Then
                If item("HasWeightage").Text = "True" Then
                    item("HasWeightage").Text = IIf(Lang = CtlCommon.Lang.AR, "نعم", "Yes")
                Else
                    item("HasWeightage").Text = IIf(Lang = CtlCommon.Lang.AR, "لا", "No")
                End If
            End If

        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim err As Integer = -1

        objFeedback_Survey = New Feedback_Survey
        With objFeedback_Survey
            .SurveyName = txtSurveyName.Text
            .SurveyArabicName = txtSurveyArabicName.Text
            .SurveyLanguage = ddlSurveyLanguage.SelectedValue
            .HasWeightage = chkIsWeightage.Checked

            If SurveyId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
                SurveyId = .SurveyId
            Else
                .SurveyId = SurveyId
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                err = .Update
            End If
        End With

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ClearAll()
            FillGridSurvey()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearAll()
        ClearQuestions()
        tcSurvey.ActiveTabIndex = 0
    End Sub

    Private Sub dgrdQuestions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgrdQuestions.SelectedIndexChanged
        QuestionId = Convert.ToInt32(DirectCast(dgrdQuestions.SelectedItems(0), GridDataItem).GetDataKeyValue("QuestionId"))
        FillQuestionControls()
    End Sub

    Private Sub btnAddQuestions_Click(sender As Object, e As EventArgs) Handles btnAddQuestions.Click
        Dim err As Integer = -1
        objFeedback_Questions = New Feedback_Questions
        With objFeedback_Questions
            .FK_SurveyId = SurveyId
            .QuestionEnText = txtSurveyQuestion.Text
            .QuestionArText = txtSurveyQuestionAr.Text
            .QuestionType = ddlQuestionType.SelectedValue

            If QuestionId = 0 Then
                .CREATED_BY = SessionVariables.LoginUser.ID
                err = .Add
                QuestionId = .QuestionId
            Else
                .QuestionId = QuestionId
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                err = .Update
            End If
        End With

        objFeedback_Answers = New Feedback_Answers
        objFeedback_Answers.FK_QuestionId = objFeedback_Questions.QuestionId
        objFeedback_Answers.Delete_BY_QuestionId()

        If ddlQuestionType.SelectedValue = 1 Or ddlQuestionType.SelectedValue = 2 Then

            For Each item In dlAnswers.Items
                objFeedback_Answers = New Feedback_Answers
                objFeedback_Answers.FK_QuestionId = objFeedback_Questions.QuestionId

                Dim txtAnswer As RadTextBox = TryCast(item.FindControl("txtAnswer"), RadTextBox)
                Dim txtAnswerArbic As RadTextBox = TryCast(item.FindControl("txtAnswerArbic"), RadTextBox)

                If String.IsNullOrEmpty(txtAnswer.Text) Then
                    Continue For
                End If
                objFeedback_Answers.AnswerTextEn = txtAnswer.Text
                objFeedback_Answers.AnswerTextAr = txtAnswerArbic.Text
                objFeedback_Answers.SmileyIcon = ""
                If objFeedback_Answers.AnswerId = 0 Then
                    objFeedback_Answers.CREATED_BY = SessionVariables.LoginUser.ID
                    err = objFeedback_Answers.Add()
                Else
                    objFeedback_Answers.LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    err = objFeedback_Answers.Update()
                End If
            Next

        ElseIf ddlQuestionType.SelectedValue = 3 Then
            For Each item In ddlSmilies.CheckedItems
                objFeedback_Answers = New Feedback_Answers
                objFeedback_Answers.FK_QuestionId = objFeedback_Questions.QuestionId
                objFeedback_Answers.AnswerTextEn = item.Value
                objFeedback_Answers.AnswerTextAr = item.Value
                objFeedback_Answers.SmileyIcon = ""
                objFeedback_Answers.CREATED_BY = SessionVariables.LoginUser.ID
                err = objFeedback_Answers.Add()
            Next

        ElseIf ddlQuestionType.SelectedValue = 4 Then
            objFeedback_Answers = New Feedback_Answers
            objFeedback_Answers.FK_QuestionId = objFeedback_Questions.QuestionId
            objFeedback_Answers.AnswerTextEn = ""
            objFeedback_Answers.AnswerTextAr = ""
            objFeedback_Answers.SmileyIcon = ""
            objFeedback_Answers.CREATED_BY = SessionVariables.LoginUser.ID
            err = objFeedback_Answers.Add()

        ElseIf ddlQuestionType.SelectedValue = 5 Then
            objFeedback_Answers = New Feedback_Answers
            objFeedback_Answers.FK_QuestionId = objFeedback_Questions.QuestionId
            objFeedback_Answers.AnswerTextEn = txtAnswerTextBox.Text
            objFeedback_Answers.AnswerTextAr = txtAnswerTextBox.Text
            objFeedback_Answers.SmileyIcon = ""
            objFeedback_Answers.CREATED_BY = SessionVariables.LoginUser.ID
            err = objFeedback_Answers.Add()

        End If

        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            ClearQuestions()
            FillQuestionsGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Private Sub btnClearQuestion_Click(sender As Object, e As EventArgs) Handles btnClearQuestion.Click
        ClearQuestions()
    End Sub

    Private Sub dgrdQuestions_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdQuestions.NeedDataSource
        objFeedback_Questions = New Feedback_Questions
        With objFeedback_Questions
            .FK_SurveyId = SurveyId
            dgrdQuestions.DataSource = .GetAll_BY_FK_SurveyId()
        End With
    End Sub

    Public Sub SelectDropdownValuesSmilies(ByVal Answerslist As List(Of Feedback_Answers))
        For Each Item As Feedback_Answers In Answerslist
            If Lang = CtlCommon.Lang.AR Then
                ddlSmilies.FindItemByValue(Item.AnswerTextEn).Checked = True
            Else
                ddlSmilies.FindItemByValue(Item.AnswerTextAr).Checked = True
            End If

        Next
    End Sub

    Private Sub dlAnswers_ItemDataBound(sender As Object, e As DataListItemEventArgs) Handles dlAnswers.ItemDataBound
        If Not e.Item Is Nothing Then
            Dim item As DataListItem = e.Item
            Dim tb As RadTextBox = TryCast(item.FindControl("txtAnswer"), RadTextBox)
            tb.TextMode = InputMode.MultiLine
            Dim tbAr As RadTextBox = TryCast(item.FindControl("txtAnswerArbic"), RadTextBox)
            Dim hfFieldId As System.Web.UI.WebControls.HiddenField = TryCast(item.FindControl("hfanswerid"), System.Web.UI.WebControls.HiddenField)
            Dim img As System.Web.UI.WebControls.ImageButton = TryCast(item.FindControl("btnadd"), System.Web.UI.WebControls.ImageButton)
            tb.Text = DirectCast(dlAnswers.DataSource(item.ItemIndex), Feedback_Answers).AnswerTextEn
            tbAr.Text = DirectCast(dlAnswers.DataSource(item.ItemIndex), Feedback_Answers).AnswerTextAr
            Dim id As Integer = DirectCast(dlAnswers.DataSource(item.ItemIndex), Feedback_Answers).AnswerId
            hfFieldId.Value = id.ToString()
            If id <> 0 Then
                img.Visible = False
            ElseIf dlAnswers.DataSource.Count - 1 > item.ItemIndex Then
                img.Visible = False
            End If
        End If
    End Sub

    Private Sub dlAnswers_ItemCommand(source As Object, e As DataListCommandEventArgs) Handles dlAnswers.ItemCommand
        Dim dataList As New List(Of Feedback_Answers)()
        For Each item In dlAnswers.Items
            Dim tb As RadTextBox = TryCast(item.FindControl("txtAnswer"), RadTextBox)
            Dim tbAr As RadTextBox = TryCast(item.FindControl("txtAnswerArbic"), RadTextBox)
            Dim hfFieldId As System.Web.UI.WebControls.HiddenField = TryCast(item.FindControl("hfanswerid"), System.Web.UI.WebControls.HiddenField)
            Dim img As System.Web.UI.WebControls.Image = TryCast(item.FindControl("imgCrop"), System.Web.UI.WebControls.Image)
            Dim surveyanswer As Feedback_Answers = New Feedback_Answers
            surveyanswer.AnswerTextEn = tb.Text
            surveyanswer.AnswerTextAr = tbAr.Text
            surveyanswer.AnswerId = 0 ' hfFieldId.Value
            dataList.Add(surveyanswer)
        Next

        dataList.Add(New Feedback_Answers())
        dlAnswers.DataSource = dataList
        dlAnswers.DataBind()
    End Sub

    Private Sub dgrdQuestions_DetailTableDataBind(sender As Object, e As GridDetailTableDataBindEventArgs) Handles dgrdQuestions.DetailTableDataBind
        Dim parentItem As GridDataItem = CType(e.DetailTableView.ParentItem, GridDataItem)
        If parentItem.Edit Then
            Return
        End If
        If (e.DetailTableView.DataMember = "AnswerDetails") Then
            Dim QuestionType As Integer = Convert.ToInt32((parentItem.GetDataKeyValue("QuestionType").ToString()))
            If QuestionType = 1 Or QuestionType = 2 Or QuestionType = 3 Then


                Dim dt As DataTable
                QuestionId = Convert.ToInt32((parentItem.GetDataKeyValue("QuestionId").ToString()))
                objFeedback_Answers = New Feedback_Answers
                objFeedback_Answers.FK_QuestionId = QuestionId

                e.DetailTableView.DataSource = objFeedback_Answers.GetAll_By_QuestionId()

            End If
        End If
    End Sub

    Private Sub btnDeleteQuestions_Click(sender As Object, e As EventArgs) Handles btnDeleteQuestions.Click
        Dim errNum As Integer = -1
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdQuestions.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                objFeedback_Questions = New Feedback_Questions
                objFeedback_Questions.QuestionId = Convert.ToInt32(row.GetDataKeyValue("QuestionId"))
                errNum = objFeedback_Questions.Delete_Update_IsDeleted()
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillQuestionsGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If
    End Sub

    Private Sub ddlQuestionType_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles ddlQuestionType.SelectedIndexChanged
        If ddlQuestionType.SelectedValue = 1 Or ddlQuestionType.SelectedValue = 2 Then

            pnlMultipleAnswers.Visible = True
            pnlSmiley.Visible = False
            pnlRating.Visible = False
            pnlTextBox.Visible = False

        ElseIf ddlQuestionType.SelectedValue = 3 Then

            pnlSmiley.Visible = True
            pnlRating.Visible = False
            pnlTextBox.Visible = False
            pnlMultipleAnswers.Visible = False

        ElseIf ddlQuestionType.SelectedValue = 4 Then

            pnlRating.Visible = True
            pnlMultipleAnswers.Visible = False
            pnlSmiley.Visible = False

            pnlTextBox.Visible = False

        ElseIf ddlQuestionType.SelectedValue = 5 Then
            pnlTextBox.Visible = True

            pnlMultipleAnswers.Visible = False
            pnlSmiley.Visible = False
            pnlRating.Visible = False


        End If
    End Sub

    Protected Sub grdQuestions_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dgrdQuestions.SelectedIndexChanged
        Try
            QuestionId = Convert.ToInt32(DirectCast(dgrdQuestions.SelectedItems(0), GridDataItem).GetDataKeyValue("QuestionId"))
            FillControlsQuestionsGrid()
        Catch ex As Exception
            Exit Sub
        End Try

    End Sub

#End Region


#Region "Methods"

    Private Sub FillControlsQuestionsGrid()
        objFeedback_Questions = New Feedback_Questions
        With objFeedback_Questions
            .QuestionId = QuestionId
            .GetByPK()
            txtSurveyQuestion.Text = .QuestionEnText
            txtSurveyQuestionAr.Text = .QuestionArText
            ddlQuestionType.SelectedValue = .QuestionType
            FillAnswers(.QuestionType)
        End With
    End Sub

    Private Sub FillGridSurvey()
        objFeedback_Survey = New Feedback_Survey
        With objFeedback_Survey
            dgrdSurvey.DataSource = .GetAll
            dgrdSurvey.DataBind()
        End With
    End Sub

    Private Sub ClearAll()
        txtSurveyName.Text = String.Empty
        txtSurveyArabicName.Text = String.Empty
        ddlSurveyLanguage.SelectedValue = -1
        chkIsWeightage.Checked = False

        SurveyId = 0
        dgrdSurvey.Rebind()
    End Sub

    Private Sub FillSurveyControls()
        objFeedback_Survey = New Feedback_Survey
        With objFeedback_Survey
            .SurveyId = SurveyId
            .GetByPK()
            txtSurveyName.Text = .SurveyName
            txtSurveyArabicName.Text = .SurveyArabicName
            chkIsWeightage.Checked = .HasWeightage
            ddlSurveyLanguage.SelectedValue = .SurveyLanguage
        End With
    End Sub

    Private Sub FillQuestionsGrid()
        objFeedback_Questions = New Feedback_Questions
        With objFeedback_Questions
            .FK_SurveyId = SurveyId
            dgrdQuestions.DataSource = .GetAll_BY_FK_SurveyId()
            dgrdQuestions.DataBind()
        End With
    End Sub

    Private Sub ClearQuestions()
        txtSurveyQuestion.Text = String.Empty
        txtSurveyQuestionAr.Text = String.Empty
        ddlQuestionType.SelectedValue = -1

        Dim list As List(Of Feedback_Answers) = New List(Of Feedback_Answers)
        list.Add(New Feedback_Answers())
        dlAnswers.DataSource = list
        dlAnswers.DataBind()

        pnlRating.Visible = False
        pnlMultipleAnswers.Visible = False
        pnlSmiley.Visible = False

        pnlTextBox.Visible = False

        QuestionId = 0
        dgrdQuestions.Rebind()
    End Sub

    Private Sub FillQuestionControls()
        objFeedback_Questions = New Feedback_Questions
        With objFeedback_Questions
            .QuestionId = QuestionId
            .GetByPK()
            txtSurveyQuestion.Text = .QuestionEnText
            txtSurveyQuestionAr.Text = .QuestionArText
            ddlQuestionType.SelectedValue = .QuestionType
            FillAnswers(.QuestionType)
        End With
    End Sub

    Private Sub FillAnswers(ByVal QuestionType As Integer)
        objFeedback_Answers = New Feedback_Answers
        objFeedback_Answers.FK_QuestionId = QuestionId
        Dim dt As DataTable = objFeedback_Answers.GetAll_By_QuestionId()
        Dim list As List(Of Feedback_Answers) = New List(Of Feedback_Answers)
        list = CtlCommon.ConvertDataTableToList(Of Feedback_Answers)(dt)

        If QuestionType = 1 Or QuestionType = 2 Then

            pnlMultipleAnswers.Visible = True
            pnlSmiley.Visible = False
            pnlRating.Visible = False
            pnlTextBox.Visible = False

            list.Add(New Feedback_Answers())

            dlAnswers.DataSource = list
            dlAnswers.DataBind()

        ElseIf QuestionType = 3 Then
            SelectDropdownValuesSmilies(list)
            pnlSmiley.Visible = True
            pnlRating.Visible = False
            pnlTextBox.Visible = False
            pnlMultipleAnswers.Visible = False

        ElseIf QuestionType = 4 Then

            pnlRating.Visible = True
            pnlMultipleAnswers.Visible = False
            pnlSmiley.Visible = False

            pnlTextBox.Visible = False

        ElseIf QuestionType = 5 Then
            pnlTextBox.Visible = True

            pnlMultipleAnswers.Visible = False
            pnlSmiley.Visible = False
            pnlRating.Visible = False


        End If

    End Sub

#End Region



End Class
