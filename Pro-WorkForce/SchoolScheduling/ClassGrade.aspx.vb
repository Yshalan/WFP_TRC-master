Imports Telerik.Web.UI
Imports SmartV.UTILITIES
Imports TA.Security
Imports System.Data
Imports TA_SchoolScheduling

Partial Class SchoolScheduling_ClassGrade
    Inherits System.Web.UI.Page

#Region "Class Variables"
    Private ObjClassGrade As New ClassGrade
    Private objCourse As New Course
    Private objClassGradeCourses As New ClassGradeCourses
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang
    Dim totalWeekly As Integer
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

    Public Property ClassGradeId() As Integer
        Get
            Return ViewState("ClassGradeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ClassGradeId") = value
        End Set
    End Property

    Public Property ClassGradeCoursesdt() As DataTable
        Get
            Return ViewState("ClassGradeCoursesdt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("ClassGradeCoursesdt") = value
        End Set
    End Property

    Public Property FK_ClassGradeId() As Integer
        Get
            Return ViewState("FK_ClassGradeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_ClassGradeId") = value
        End Set
    End Property

    Public Property FK_CourseId() As Integer
        Get
            Return ViewState("FK_CourseId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_CourseId") = value
        End Set
    End Property
#End Region

#Region "Page events"

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdClassGrade.Skin))
    End Function

    Protected Sub dgrdClassGrade_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) 'Handles dgrdClassGrade.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit
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
            Else
                Lang = CtlCommon.Lang.EN
            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            CreateClassGradeCoursesdt()
            FillGrid()
            FillCourses()
        End If


        ibtnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdClassGrade.ClientID + "')")
        btnRemove.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdClassGradeCourses.ClientID + "')")

        Dim formPath As String = Request.Url.AbsoluteUri
        Dim strArr() As String = formPath.Split("/")
        Dim objSysForms As New SYSForms
        dtCurrent = objSysForms.GetFormIDByFormPath("/" + strArr(5), SessionVariables.LoginUser.GroupId)
        For Each row As DataRow In dtCurrent.Rows
            If Not row("AllowAdd") = 1 Then
                If Not IsDBNull(row("AddBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("AddBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("AddBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowDelete") = 1 Then
                If Not IsDBNull(row("DeleteBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("DeleteBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("DeleteBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowSave") = 1 Then
                If Not IsDBNull(row("EditBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("EditBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("EditBtnName")).Visible = False
                    End If
                End If
            End If

            If Not row("AllowPrint") = 1 Then
                If Not IsDBNull(row("PrintBtnName")) Then
                    If Not UpdatePanel1.FindControl(row("PrintBtnName")) Is Nothing Then
                        UpdatePanel1.FindControl(row("PrintBtnName")).Visible = False
                    End If
                End If
            End If
        Next

    End Sub

    Protected Sub dgrdClassGrade_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dgrdClassGrade.SelectedIndexChanged
        TabGradeCourses.Visible = True
        ClassGradeId = Convert.ToInt32(DirectCast(dgrdClassGrade.SelectedItems(0), GridDataItem).GetDataKeyValue("ClassGradeId").ToString())
        ObjClassGrade = New ClassGrade
        With ObjClassGrade
            .ClassGradeId = ClassGradeId
            .GetByPK()
            txtClassGradeArabic.Text = .ClassGradeNameAr
            txtClassGradeEnglish.Text = .ClassGradeName
            txtOrder.Text = .ClassGradeOrder
            clearClassGradeCourses()
            FillClassGradeCourses()
            RadCmbCourse.Items.Clear()
            FillCourses()
        End With
    End Sub

    Protected Sub ibtnSave_Click(sender As Object, e As System.EventArgs) Handles ibtnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim err As Integer
        'Dim ClassGradeEnglish As String = txtClassGradeEnglish.Text
        With ObjClassGrade
            .ClassGradeName = txtClassGradeEnglish.Text
            .ClassGradeNameAr = txtClassGradeArabic.Text
            .ClassGradeOrder = txtOrder.Text
            .CREATED_BY = SessionVariables.LoginUser.ID
            .CREATED_DATE = Now
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_DATE = Now
        End With
        If ClassGradeId = 0 Then

            err = ObjClassGrade.Add()


            If err = 0 Then
                ClassGradeId = ObjClassGrade.ClassGradeId
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully"), "success")
                'TabGradeCourses.Visible = True
                'TabContainer1.ActiveTabIndex = 1
                'FK_ClassGradeId = dgrdClassGradeCourses.SelectedValue(ClassGradeEnglish) 'CInt(CType(dgrdClassGradeCourses.SelectedValues(0), GridDataItem)(ClassGradeEnglish).Text)
                ClearAll()
                FillGrid()
            ElseIf err = -1 Or err = -11 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave"), "error")
            End If
        Else
            ObjClassGrade.ClassGradeId = ClassGradeId
            ObjClassGrade.LAST_UPDATE_BY = SessionVariables.LoginUser.UsrID
            ObjClassGrade.LAST_UPDATE_DATE = Now
            err = ObjClassGrade.Update()

            If err = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully"), "success")
                ClearAll()
                FillGrid()
            ElseIf err = -1 Or err = -11 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate"), "error")
            End If

        End If
        If err = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo))
        ElseIf err = -7 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo))
        End If
    End Sub

    Protected Sub ibtnDelete_Click(sender As Object, e As System.EventArgs) Handles ibtnDelete.Click
        Dim err As Integer

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdClassGrade.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("ClassGradeName")
                ObjClassGrade = New ClassGrade
                ObjClassGrade.ClassGradeId = Convert.ToInt32(row.GetDataKeyValue("ClassGradeId"))
                err = ObjClassGrade.Delete()
                With strBuilder
                    If err = 0 Then
                        .Append(strCode & " Deleted")
                        .Append("\n")
                    Else
                        .Append(strCode & " Could't Delete")
                        .Append("\n")
                    End If

                End With
            End If
        Next
        CtlCommon.ShowMessage(Me.Page, strBuilder.ToString())
        FillGrid()
        ClearAll()
    End Sub

    Protected Sub ibtnRest_Click(sender As Object, e As System.EventArgs) Handles ibtnRest.Click
        TabContainer1.ActiveTabIndex = 0
        ClearAll()
        FillGrid()
    End Sub

    Protected Sub dgrdClassGrade_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdClassGrade.NeedDataSource
        Try
            ObjClassGrade = New ClassGrade()
            dgrdClassGrade.DataSource = ObjClassGrade.GetAll()

        Catch ex As Exception

        End Try
    End Sub


#End Region

#Region "Methods"

    Public Sub FillGrid()
        Try
            ObjClassGrade = New ClassGrade()
            dgrdClassGrade.DataSource = ObjClassGrade.GetAll()
            dgrdClassGrade.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ClearAll()
        txtClassGradeArabic.Text = String.Empty
        txtClassGradeEnglish.Text = String.Empty
        txtOrder.Text = String.Empty
        ClassGradeId = 0
        FillGrid()
        clearClassGradeCourses()
        FillClassGradeCourses()
    End Sub

#End Region

#Region "Class Grade Courses "

    Protected Sub dgrdClassGradeCourses_DataBound(sender As Object, e As System.EventArgs) Handles dgrdClassGradeCourses.DataBound
        Dim TotalWeeklyStr As String

        If SessionVariables.CultureInfo = "ar-JO" Then
            TotalWeeklyStr = "مجموع الحصص الاسبوعية: "
        Else
            TotalWeeklyStr = "Total Weekly: "
        End If
        totalWeekly = 0 'to set total 0, To prevent totalweekly addnig
        For Each item As GridDataItem In dgrdClassGradeCourses.MasterTableView.Items
            Dim cell As TableCell = item("WeeklyCourcesNumber")
            totalWeekly = totalWeekly + cell.Text
        Next

        Dim footerItem As GridFooterItem = dgrdClassGradeCourses.MasterTableView.GetItems(GridItemType.Footer)(0)
        footerItem("WeeklyCourcesNumber").Text = footerItem("WeeklyCourcesNumber").Text + TotalWeeklyStr + totalWeekly.ToString()
    End Sub

    Protected Sub dgrdClassGradeCourses_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdClassGradeCourses.NeedDataSource
        dgrdClassGradeCourses.DataSource = ClassGradeCoursesdt
    End Sub

    Protected Sub dgrdClassGradeCourses_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dgrdClassGradeCourses.SelectedIndexChanged
        Try
            Dim objClassGradeCourses As New ClassGradeCourses
            Dim dt As DataTable = ClassGradeCoursesdt
            FK_ClassGradeId = CInt(CType(dgrdClassGradeCourses.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_ClassGradeId"))
            FK_CourseId = CInt(CType(dgrdClassGradeCourses.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_CourseId"))

            RadCmbCourse.SelectedValue = CInt(CType(dgrdClassGradeCourses.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_CourseId"))

            objClassGradeCourses.FK_ClassGradeId = FK_ClassGradeId
            objClassGradeCourses.FK_CourseId = FK_CourseId

            txtWeeklyNo.Text = CType(dgrdClassGradeCourses.SelectedItems(0), GridDataItem).GetDataKeyValue("WeeklyCourcesNumber")

        Catch ex As Exception
        End Try
    End Sub

    Sub CreateClassGradeCoursesdt()
        ClassGradeCoursesdt = New DataTable
        Dim dc As DataColumn
        dc = New DataColumn

        dc = New DataColumn
        dc.ColumnName = "FK_ClassGradeId"
        dc.DataType = GetType(Integer)
        ClassGradeCoursesdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "FK_CourseId"
        dc.DataType = GetType(Integer)
        ClassGradeCoursesdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "WeeklyCourcesNumber"
        dc.DataType = GetType(Integer)
        ClassGradeCoursesdt.Columns.Add(dc)



    End Sub

    Sub FillClassGradeCourses()
        If ClassGradeId > 0 Then

            objClassGradeCourses = New ClassGradeCourses
            objClassGradeCourses.FK_ClassGradeId = ClassGradeId
            ClassGradeCoursesdt = objClassGradeCourses.GetAllbyFK_ClassGradeId()
            dgrdClassGradeCourses.DataSource = ClassGradeCoursesdt
            dgrdClassGradeCourses.DataBind()
        Else
            CreateClassGradeCoursesdt()
            dgrdClassGradeCourses.DataSource = ClassGradeCoursesdt
            dgrdClassGradeCourses.DataBind()
        End If
    End Sub

    Private Sub FillCourses()
        Dim dt As DataTable = Nothing
        objClassGradeCourses = New ClassGradeCourses()
        objClassGradeCourses.FK_ClassGradeId = ClassGradeId
        dt = objClassGradeCourses.GetAllCourse_ByClassGradeId()
        CtlCommon.FillTelerikDropDownList(RadCmbCourse, dt, Lang)
    End Sub

    Sub clearClassGradeCourses()
        ClassGradeCoursesdt = New DataTable
        dgrdClassGradeCourses.DataSource = ClassGradeCoursesdt
        dgrdClassGradeCourses.DataBind()
        FK_ClassGradeId = 0
        'RadCmbCourse.SelectedIndex = -1
        txtWeeklyNo.Text = ""
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        Dim err As Integer = -1
        Dim dr As DataRow
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Try

            If ClassGradeId = 0 Then
                If FK_ClassGradeId = 0 And FK_CourseId = 0 Then

                    dr = ClassGradeCoursesdt.NewRow
                    dr("FK_ClassGradeId") = ClassGradeId
                    dr("FK_CourseId") = RadCmbCourse.SelectedValue
                    dr("WeeklyCourcesNumber") = txtWeeklyNo.Text


                    ClassGradeCoursesdt.Rows.Add(dr)
                Else
                    If ClassGradeCoursesdt.Rows.Count > 0 Then

                        dr = ClassGradeCoursesdt.Select("FK_ClassGradeId= " & FK_ClassGradeId)(0)
                        dr("FK_ClassGradeId") = ClassGradeId
                        dr("FK_CourseId") = RadCmbCourse.SelectedValue
                        dr("WeeklyCourcesNumber") = txtWeeklyNo.Text

                    End If
                End If
            Else
                If FK_ClassGradeId = 0 And FK_CourseId = 0 Then
                    objClassGradeCourses = New ClassGradeCourses
                    With objClassGradeCourses
                        .FK_ClassGradeId = ClassGradeId
                        .FK_CourseId = RadCmbCourse.SelectedValue
                        .WeeklyCourcesNumber = txtWeeklyNo.Text

                        err = .Add()
                        
                    End With
                Else
                    objClassGradeCourses = New ClassGradeCourses
                    With objClassGradeCourses
                        .FK_ClassGradeId = ClassGradeId
                        .FK_CourseId = RadCmbCourse.SelectedValue
                        .WeeklyCourcesNumber = txtWeeklyNo.Text

                        err = .Update()
                        'FillClassGradeCourses()
                        'RadCmbCourse.Items.Clear()
                        'FillCourses()
                    End With
                End If
            End If
            If err = 0 Then
                FillClassGradeCourses()
                RadCmbCourse.Items.Clear()
                FillCourses()
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo))
                dgrdClassGradeCourses.DataSource = ClassGradeCoursesdt
                dgrdClassGradeCourses.DataBind()
                FK_ClassGradeId = 0
                RadCmbCourse.SelectedIndex = -1
                txtWeeklyNo.Text = ""
            Else

                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorAdd", CultureInfo))
            End If
           

        Catch ex As Exception
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorAdd", CultureInfo))

        End Try
    End Sub


    Protected Sub btnRemove_Click(sender As Object, e As System.EventArgs) Handles btnRemove.Click

        Dim err As Integer

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdClassGradeCourses.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strCode As String = row.GetDataKeyValue("CourseName").ToString()
                objClassGradeCourses = New ClassGradeCourses
                objClassGradeCourses.FK_ClassGradeId = Convert.ToInt32(row.GetDataKeyValue("FK_ClassGradeId"))
                objClassGradeCourses.FK_CourseId = Convert.ToInt32(row.GetDataKeyValue("FK_CourseId"))
                err = objClassGradeCourses.Delete()
                With strBuilder
                    If err = 0 Then
                        .Append(strCode & " Deleted")
                        .Append("\n")
                    Else
                        .Append(strCode & " Could't Delete")
                        .Append("\n")
                    End If

                End With
            End If
        Next
        CtlCommon.ShowMessage(Me.Page, strBuilder.ToString())
        FillClassGradeCourses()
        clearClassGradeCourses()
        'If ClassGradeId = 0 Then
        '    For Each row As GridDataItem In dgrdClassGrade.Items
        '        If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
        '            Dim intFK_ClassGradeId As Integer = Convert.ToInt32(row("FK_ClassGradeId").Text)
        '            ClassGradeCoursesdt.Rows.Remove(ClassGradeCoursesdt.Select("FK_ClassGradeId = " & intFK_ClassGradeId)(0))
        '        End If
        '        dgrdClassGradeCourses.DataSource = ClassGradeCoursesdt
        '        dgrdClassGradeCourses.DataBind()
        '        FK_ClassGradeId = 0
        '        RadCmbCourse.SelectedIndex = -1
        '        txtWeeklyNo.Text = ""

        '    Next
        'Else
        '    objClassGradeCourses = New ClassGradeCourses
        '    With objClassGradeCourses
        '        For Each row As GridDataItem In dgrdClassGradeCourses.Items
        '            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
        '                Dim intFK_ClassGradeId As Integer = Convert.ToInt32(row("FK_ClassGradeId").Text)
        '                Dim intFK_CourseId As Integer = Convert.ToInt32(row("FK_CourseId").Text)
        '                ClassGradeCoursesdt.Rows.Remove(ClassGradeCoursesdt.Select("FK_ClassGradeId = " & intFK_ClassGradeId)(0))
        '                .FK_ClassGradeId = intFK_ClassGradeId
        '                .FK_CourseId = intFK_CourseId
        '                .Delete()
        '            End If
        '        Next
        '    End With
        'End If
        'FillClassGradeCourses()
    End Sub
#End Region

End Class
