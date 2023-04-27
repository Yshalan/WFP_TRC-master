Imports Telerik.Web.UI
Imports SmartV.UTILITIES
Imports System.Data
Imports TA_SchoolScheduling
Imports TA.Security

Partial Class SchoolScheduling_Classes
    Inherits System.Web.UI.Page


#Region "Class Variables"
    Private objclasses As Classes
    Private objClassGrade As ClassGrade
    Private objCourse As Course
    Private objTeacherGrades As TeacherGrades
    Private objTeacherClasses As TeacherClasses
    Private objCourseTeachers As New CourseTeachers
    Private objClassGradeCourses As New ClassGradeCourses
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Dim totalWeekly As Integer
#End Region

#Region "Properties"
    Public Property Lang() As CtlCommon.Lang
        Get
            Return ViewState("Lang")
        End Get
        Set(ByVal value As CtlCommon.Lang)
            ViewState("Lang") = value
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

    Public Property ClassId() As Integer
        Get
            Return ViewState("ClassId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ClassId") = value
        End Set
    End Property

    Public Property FK_ClassId() As Integer
        Get
            Return ViewState("FK_ClassId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_ClassId") = value
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

    Public Property FK_CourseId() As Integer
        Get
            Return ViewState("FK_CourseId")
        End Get
        Set(ByVal value As Integer)
            ViewState("FK_CourseId") = value
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

    Public Property TeachersClassesdt() As DataTable
        Get
            Return ViewState("TeachersClassesdt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("TeachersClassesdt") = value
        End Set
    End Property
#End Region

#Region "Page events"
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
            totalWeekly = 0
            FillClassGrade()
            FillGrid()
            FillCourses()
            'FillTeacher()
            CreateTeachersClassesdt()
        End If
        ibtnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdClass.ClientID + "')")

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

    Protected Sub ibtnSave_Click(sender As Object, e As System.EventArgs) Handles ibtnSave.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        objclasses = New Classes
        Dim err As Integer
        With objclasses
            .ClassName = TxtClassName.Text
            .ClassNameAr = txtClassNameAr.Text
            .FK_ClassGradeId = RadCmbGrade.SelectedValue
            .CREATED_BY = SessionVariables.LoginUser.ID
            .CREATED_DATE = Now
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_DATE = Now
        End With
        If ClassId = 0 Then

            err = objclasses.Add()
            If err = 0 Then
                ClassId = objclasses.ClassId
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully"), "success")
                ClearAll()
                FillGrid()
            ElseIf err = -1 Or err = -11 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave"), "error")
            End If
        Else
            objclasses.ClassId = ClassId
            objclasses.LAST_UPDATE_BY = SessionVariables.LoginUser.UsrID
            objclasses.LAST_UPDATE_DATE = Now
            err = objclasses.Update()

            If err = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully"), "success")
                ClearAll()
                FillGrid()
            ElseIf err = -1 Or err = -11 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorUpdate"), "error")
            End If
        End If

        If err = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
        ElseIf err = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")
        ElseIf err = -7 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")

        End If
    End Sub

    Protected Sub ibtnDelete_Click(sender As Object, e As System.EventArgs) Handles ibtnDelete.Click
        Dim err As Integer
        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdClass.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim intClassId As Integer = Convert.ToInt32(row.GetDataKeyValue("ClassId"))
                Dim strCode As String = row.GetDataKeyValue("ClassName").ToString()
                objclasses = New Classes
                objclasses.ClassId = intClassId
                objclasses.Delete()
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
        ClearAll()
        FillGrid()
    End Sub

    Protected Sub ibtnRest_Click(sender As Object, e As System.EventArgs) Handles ibtnRest.Click
        ClearAll()
        FillGrid()
    End Sub

    Protected Sub dgrdClass_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdClass.ItemCommand
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub dgrdClass_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdClass.NeedDataSource
        Try
            objclasses = New Classes()
            objclasses.lang = Lang
            dgrdClass.DataSource = objclasses.GetAll_ByClassGrade()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub dgrdClass_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dgrdClass.SelectedIndexChanged
        ClassId = CInt(DirectCast(dgrdClass.SelectedItems(0), GridDataItem).GetDataKeyValue("ClassId").ToString().Trim)

        FK_ClassGradeId = CInt(CType(dgrdClass.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_ClassGradeId"))
        objclasses = New Classes
        With objclasses
            .ClassId = ClassId
            .GetByPK()
            TxtClassName.Text = .ClassName
            txtClassNameAr.Text = .ClassNameAr
            RadCmbGrade.SelectedValue = .FK_ClassGradeId
            'clearTeachersClasses()
            RadCmbCourse.SelectedIndex = -1
            RadCmbTeacher.SelectedIndex = -1
            txtWeeklyNo.Text = ""
            FillTeacherClasses()
            RadCmbCourse.Items.Clear()
            FillCourses()
        End With
       
    End Sub
#End Region

#Region "Methods"
    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdClass.Skin))
    End Function

    Public Sub FillGrid()
        Try
            objclasses = New Classes()
            objclasses.lang = Lang
            dgrdClass.DataSource = objclasses.GetAll_ByClassGrade()

            dgrdClass.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ClearAll()
        TxtClassName.Text = String.Empty
        txtClassNameAr.Text = String.Empty
        RadCmbGrade.SelectedIndex = -1
        ClassId = 0
        FillGrid()
        clearTeachersClasses()
        FillTeacherClasses()
        TabContainer1.ActiveTabIndex = 0
    End Sub

    Private Sub FillClassGrade()
        Dim dt As DataTable = Nothing
        objClassGrade = New ClassGrade()
        dt = objClassGrade.GetAll()
        CtlCommon.FillTelerikDropDownList(RadCmbGrade, dt, Lang)
    End Sub
#End Region

#Region "Courses&Teachers"
    Private Sub FillCourses()
        RadCmbCourse.Items.Clear()
        RadCmbCourse.SelectedValue = -1
        Dim dt As DataTable = Nothing
        objCourse = New Course()
        objCourse.FK_ClassGradeId = FK_ClassGradeId
        dt = objCourse.GetAllByCourseId()
        CtlCommon.FillTelerikDropDownList(RadCmbCourse, dt, Lang)
    End Sub

    Private Sub FillTeacher()
        'RadCmbTeacher.Items.Clear()
        'RadCmbTeacher.SelectedValue = -1
        Dim dt As DataTable = Nothing
        objCourseTeachers = New CourseTeachers()
        objCourseTeachers.FK_CourseId = RadCmbCourse.SelectedValue
        dt = objCourseTeachers.GetAll_ByCourseId()
        CtlCommon.FillTelerikDropDownList(RadCmbTeacher, dt, Lang)
    End Sub

    Sub CreateTeachersClassesdt()
        TeachersClassesdt = New DataTable
        Dim dc As DataColumn
        dc = New DataColumn

        dc = New DataColumn
        dc.ColumnName = "FK_ClassId"
        dc.DataType = GetType(Integer)
        TeachersClassesdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "FK_EmployeeId"
        dc.DataType = GetType(Integer)
        TeachersClassesdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "FK_CourseId"
        dc.DataType = GetType(Integer)
        TeachersClassesdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "weeklyCount"
        dc.DataType = GetType(Integer)
        TeachersClassesdt.Columns.Add(dc)

    End Sub

    Protected Sub dgrdTeacherClasses_DataBound(sender As Object, e As System.EventArgs) Handles dgrdTeacherClasses.DataBound

        Dim TotalWeeklyStr As String
        If SessionVariables.CultureInfo = "ar-JO" Then
            TotalWeeklyStr = "مجموع الحصص الاسبوعية: "
        Else
            TotalWeeklyStr = "Total Weekly: "
        End If
        totalWeekly = 0 'to set total 0, To prevent totalweekly addnig
        For Each item As GridDataItem In dgrdTeacherClasses.MasterTableView.Items
            Dim cell As TableCell = item("weeklyCount")
            totalWeekly = totalWeekly + cell.Text
        Next

        Dim footerItem As GridFooterItem = dgrdTeacherClasses.MasterTableView.GetItems(GridItemType.Footer)(0)
       
        footerItem("weeklyCount").Text = footerItem("weeklyCount").Text + TotalWeeklyStr + totalWeekly.ToString()

    End Sub

    Protected Sub dgrdTeacherClasses_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdTeacherClasses.NeedDataSource
        dgrdTeacherClasses.DataSource = TeachersClassesdt

    End Sub

    Protected Sub dgrdTeacherClasses_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dgrdTeacherClasses.SelectedIndexChanged
        Try
            Dim objTeacherClasses As New TeacherClasses
            Dim dt As DataTable = TeachersClassesdt
            FK_ClassId = CInt(CType(dgrdTeacherClasses.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_ClassId"))
            FK_CourseId = CInt(CType(dgrdTeacherClasses.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_CourseId"))
            FK_EmployeeId = CInt(CType(dgrdTeacherClasses.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_EmployeeId"))
            objTeacherClasses.FK_ClassId = FK_ClassId
            objTeacherClasses.FK_CourseId = FK_CourseId
            objTeacherClasses.FK_EmployeeId = FK_EmployeeId
            RadCmbCourse.SelectedValue = FK_CourseId
            RadCmbTeacher.SelectedValue = FK_EmployeeId
            FillTeacher()
            txtWeeklyNo.Text = CType(dgrdTeacherClasses.SelectedItems(0), GridDataItem).GetDataKeyValue("weeklyCount")


        Catch ex As Exception
        End Try
    End Sub

    Sub FillTeacherClasses()
        If ClassId > 0 Then

            objTeacherClasses = New TeacherClasses
            objTeacherClasses.FK_ClassId = ClassId
            TeachersClassesdt = objTeacherClasses.GetAllByClassId()
            dgrdTeacherClasses.DataSource = TeachersClassesdt
            dgrdTeacherClasses.DataBind()

        Else
            CreateTeachersClassesdt()
            dgrdTeacherClasses.DataSource = TeachersClassesdt
            dgrdTeacherClasses.DataBind()
        End If
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        Dim err As Integer = -1
        Dim ClassMaxLeasson As Integer = ConfigurationManager.AppSettings("ClassMaxLeasson")
        Dim TeacherMaxLesseons As Integer = ConfigurationManager.AppSettings("TeacherMaxLesseons")
        Dim TotoalTeacherLesseons As Integer = 0
        totalWeekly = 0 'to set total 0, To prevent totalweekly addnig 
        For Each item As GridDataItem In dgrdTeacherClasses.MasterTableView.Items
            Dim cell As TableCell = item("weeklyCount")
            totalWeekly = totalWeekly + cell.Text
        Next
        For Each item As GridDataItem In dgrdTeacherClasses.MasterTableView.Items
            Dim cellFK_EmployeeId As TableCell = item("FK_EmployeeId")
            Dim cell As TableCell = item("weeklyCount")
            If cellFK_EmployeeId.Text = RadCmbTeacher.SelectedValue Then
                TotoalTeacherLesseons = TotoalTeacherLesseons + cell.Text
            End If
        Next
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Try

            If ClassId = 0 Then
                CtlCommon.ShowMessage(Me.Page, "Please select class")
            Else
                objTeacherClasses = New TeacherClasses
                If TeacherMaxLesseons < TotoalTeacherLesseons + txtWeeklyNo.Text Then
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        CtlCommon.ShowMessage(Me.Page, "لقد تجاوزت عدد الحصص المسموح به لكل مدرس وهو " & TeacherMaxLesseons)
                    Else
                        CtlCommon.ShowMessage(Me.Page, "You Have Exceeded The Number Of Lessions For Each Teacher: " & TeacherMaxLesseons)
                    End If

                    Exit Sub
                Else
                    If ClassMaxLeasson < totalWeekly + txtWeeklyNo.Text Then
                        If SessionVariables.CultureInfo = "ar-JO" Then
                            CtlCommon.ShowMessage(Me.Page, "لقد تجاوزت الحد المسموح به من الدروس وهو " & ClassMaxLeasson)
                        Else
                            CtlCommon.ShowMessage(Me.Page, "You Have Exceeded The Allowed Number Of Lessions: " & ClassMaxLeasson)
                        End If

                        Exit Sub
                    Else
                        With objTeacherClasses
                            .FK_ClassId = ClassId
                            .FK_CourseId = RadCmbCourse.SelectedValue
                            .FK_EmployeeId = RadCmbTeacher.SelectedValue
                            .weeklyCount = txtWeeklyNo.Text
                            .CREATED_BY = SessionVariables.LoginUser.ID
                            .CREATED_DATE = Now
                            err = .Add()
                            FillTeacherClasses()
                        End With
                    End If
                End If

                End If
                If err = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                    dgrdTeacherClasses.DataSource = TeachersClassesdt
                    dgrdTeacherClasses.DataBind()
                    FK_ClassId = 0
                    RadCmbCourse.SelectedValue = -1
                    RadCmbTeacher.SelectedValue = -1
                    txtWeeklyNo.Text = ""
                ElseIf err = 1 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
                    dgrdTeacherClasses.DataSource = TeachersClassesdt
                    dgrdTeacherClasses.DataBind()
                    FK_ClassId = 0
                    RadCmbCourse.SelectedIndex = -1
                    RadCmbTeacher.SelectedValue = -1
                    txtWeeklyNo.Text = ""
                Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorAdd", CultureInfo), "error")
                End If


        Catch ex As Exception
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorAdd", CultureInfo), "error")

        End Try
    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As System.EventArgs) Handles btnRemove.Click
        Dim err As Integer

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdTeacherClasses.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strName As String = row.GetDataKeyValue("CourseName").ToString()
                objTeacherClasses = New TeacherClasses
                objTeacherClasses.FK_ClassId = Convert.ToInt32(row.GetDataKeyValue("FK_ClassId"))
                objTeacherClasses.FK_CourseId = Convert.ToInt32(row.GetDataKeyValue("FK_CourseId"))
                objTeacherClasses.FK_EmployeeId = Convert.ToInt32(row.GetDataKeyValue("FK_EmployeeId"))
                err = objTeacherClasses.Delete()
                With strBuilder
                    If err = 0 Then
                        .Append(strName & " Deleted")
                        .Append("\n")
                    Else
                        .Append(strName & " Could't Delete")
                        .Append("\n")
                    End If

                End With
            End If
        Next
        CtlCommon.ShowMessage(Me.Page, strBuilder.ToString())
        FillTeacherClasses()
        RadCmbCourse.SelectedIndex = -1
        RadCmbTeacher.SelectedIndex = -1
        txtWeeklyNo.Text = ""

    End Sub

    Sub clearTeachersClasses()
        TeachersClassesdt = New DataTable
        dgrdTeacherClasses.DataSource = TeachersClassesdt
        dgrdTeacherClasses.DataBind()
        ClassId = 0
        RadCmbCourse.SelectedIndex = -1
        RadCmbTeacher.SelectedIndex = -1
        txtWeeklyNo.Text = ""
    End Sub

    Sub FillWeeklyCount()
        If RadCmbGrade.SelectedIndex > 0 And ClassId > 0 Then
            Dim dt As DataTable = Nothing
            objClassGradeCourses = New ClassGradeCourses()
            objClassGradeCourses.FK_CourseId = RadCmbCourse.SelectedValue

            objClassGradeCourses.FK_ClassGradeId = RadCmbGrade.SelectedValue
            dt = objClassGradeCourses.GetWeeklyNo()
            If Not dt Is Nothing Then
                If Not IsDBNull(dt.Rows(0)("WeeklyCourcesNumber")) Then
                    txtWeeklyNo.Text = dt.Rows(0)("WeeklyCourcesNumber")
                    txtWeeklyNo.Enabled = False

                End If
            End If

        End If
     
    End Sub

    Protected Sub RadCmbCourse_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbCourse.SelectedIndexChanged
        RadCmbTeacher.Items.Clear()
        FillTeacher()
        FillWeeklyCount()
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        RadCmbCourse.SelectedIndex = -1
        RadCmbTeacher.SelectedIndex = -1
        txtWeeklyNo.Text = ""
        FillTeacherClasses()
    End Sub
#End Region

 
  
End Class
