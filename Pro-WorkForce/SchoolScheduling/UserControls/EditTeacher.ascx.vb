Imports TA.Employees
Imports TA.Definitions
Imports SmartV.UTILITIES
Imports System.Data
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports Telerik.Web.UI
Imports TA.Lookup
Imports TA.DailyTasks
Imports System.Web.UI
Imports TA.Admin
Imports System.IO
Imports TA.Security
Imports TA_SchoolScheduling

Partial Class EditTeacher_WebUserControl
    Inherits System.Web.UI.UserControl

#Region "Class Variables"


    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Public Lang As String
    Public MsgLang As String
    Private objEmployee As Employee
    Private objCourse As Course
    Private objClassGrade As ClassGrade
    Private objCourseTeachers As CourseTeachers
    Private objTeacherGrades As TeacherGrades
    Private objclasses As New Classes
    Private objTeacherClasses As New TeacherClasses
    Private objClassGradeCourses As ClassGradeCourses
    Dim totalWeekly As Integer
#End Region

#Region "Properties"

    Public Property EmployeeId() As Integer
        Get
            Return ViewState("EmployeeId")
        End Get
        Set(ByVal value As Integer)
            ViewState("EmployeeId") = value
        End Set
    End Property

    Public Property AllowEdit() As Boolean
        Get
            Return ViewState("AllowEdit")
        End Get
        Set(ByVal value As Boolean)
            ViewState("AllowEdit") = value
        End Set
    End Property

    Public Property AllowDelete() As Boolean
        Get
            Return ViewState("AllowDelete")
        End Get
        Set(ByVal value As Boolean)
            ViewState("AllowDelete") = value
        End Set
    End Property

    Public Property ClassCoursesdt() As DataTable
        Get
            Return ViewState("ClassCoursesdt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("ClassCoursesdt") = value
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
#End Region

#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not Page.IsPostBack Then
                If (SessionVariables.CultureInfo Is Nothing) Then
                    Response.Redirect("~/default/Login.aspx")
                ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                    Lang = CtlCommon.Lang.AR
                Else
                    Lang = CtlCommon.Lang.EN
                End If

                CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
                FillControlsForEditing()
                fillCourseList()
                fillGrade()

                FillClasses()
                'FillCourses()
                FillGridTeacherClasses()
                CreateClassCoursesdt()
            End If
            
        End If
        btnRemove.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdClassCourse.ClientID + "')")
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs) Handles btnSave.Click
        Dim err As Integer
        Dim flag As Boolean = False
        'Dim ItemCount As Integer = cblCourseList.SelectedItem.Value
        Dim ItemCount As Integer = cblCourseList.Items.Count
        If ItemCount = 0 Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Me.Page, "يرجى اختيار مادة واحدة على الاقل")
            Else
                CtlCommon.ShowMessage(Me.Page, "Please select one course at least")
            End If
        Else



            For Each item As ListItem In cblCourseList.Items
                If item.Selected Then
                    flag = True
                    objCourseTeachers = New CourseTeachers
                    With objCourseTeachers
                        .FK_EmployeeId = EmployeeId
                        .FK_CourseId = item.Value
                        If EmployeeId <> 0 Then
                            .CREATED_BY = SessionVariables.LoginUser.ID
                            .CREATED_DATE = Now
                            err = objCourseTeachers.Add()
                        Else
                            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                            .LAST_UPDATE_DATE = Now
                            err = objCourseTeachers.Update()
                        End If
                    End With
                Else
                    objCourseTeachers = New CourseTeachers
                    With objCourseTeachers
                        .FK_EmployeeId = EmployeeId
                        .FK_CourseId = item.Value
                        err = objCourseTeachers.Delete()
                    End With

                End If
            Next


        End If
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo))
            FillDDLCourses()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo))
        End If

    End Sub
    Protected Sub ibtnSave_Click(sender As Object, e As System.EventArgs) Handles ibtnSave.Click
        Dim err As Integer
        Dim flag As Boolean = False
        'Dim ItemCount As Integer = cblCourseList.SelectedItem.Value
        Dim ItemCount As Integer = cblCourseList.Items.Count
        If ItemCount = 0 Then
            If SessionVariables.CultureInfo = "ar-JO" Then
                CtlCommon.ShowMessage(Me.Page, "يرجى اختيار مادة واحدة على الاقل")
            Else
                CtlCommon.ShowMessage(Me.Page, "Please select one course at least")
            End If
        Else


            For Each item As ListItem In cblGradeList.Items
                If item.Selected Then
                    flag = True
                    objTeacherGrades = New TeacherGrades
                    With objTeacherGrades
                        .FK_EmployeeId = EmployeeId
                        .FK_ClassGradeId = item.Value
                        If EmployeeId <> 0 Then
                            .CREATED_BY = SessionVariables.LoginUser.ID
                            .CREATED_DATE = Now
                            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                            .LAST_UPDATE_DATE = Now
                            err = objTeacherGrades.Add()
                        Else
                            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                            .LAST_UPDATE_DATE = Now
                            err = objTeacherGrades.Update()
                        End If
                    End With
                Else
                    objTeacherGrades = New TeacherGrades
                    With objTeacherGrades
                        .FK_EmployeeId = EmployeeId
                        .FK_ClassGradeId = item.Value
                        err = objTeacherGrades.Delete()
                    End With
                End If
            Next
        End If
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo))
            FillClasses()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo))
        End If

    End Sub
#End Region

#Region "Methods"
    Public Sub FillControlsForEditing()
        objEmployee = New Employee()
        objEmployee.EmployeeId = EmployeeId
        objEmployee.GetByPK()
        With objEmployee
            txtTeacherNumber.Text = .EmployeeNo
            'txtTeacherNumber2.Text = .EmployeeNo
            txtTeacherName.Text = .EmployeeName
            'txtTeacherName2.Text = .EmployeeName
            txtTeacherArabicName.Text = .EmployeeArabicName
            'txtTeacherArabicName2.Text = .EmployeeArabicName
        End With
        FillcblCourseList()
        FillcblGradeList()
        FillClasses()
        FillDDLCourses()
        FillGridTeacherClasses()
    End Sub

    Public Sub FillcblGradeList()
        Dim FK_ClassGradeIdVal As Integer
        Dim dt As DataTable
        dt = New DataTable
        objTeacherGrades = New TeacherGrades
        If EmployeeId <> 0 Then
            objTeacherGrades.FK_EmployeeId = EmployeeId
            dt = objTeacherGrades.GetTeacherGrades_Select_AllbyEmpId()
            For i As Integer = 0 To dt.Rows.Count - 1
                FK_ClassGradeIdVal = dt.Rows(i)("FK_ClassGradeId")
                cblGradeList.Items.FindByValue(FK_ClassGradeIdVal).Selected = True
            Next
        End If
    End Sub

    Public Sub FillcblCourseList()
        Dim FK_CourseIdVal As Integer
        Dim dt As DataTable
        dt = New DataTable
        objCourseTeachers = New CourseTeachers
        '  For Each list As ListItem In cblCourseList.Items
        If EmployeeId <> 0 Then
            objCourseTeachers.FK_EmployeeId = EmployeeId
            dt = objCourseTeachers.GetCourseTeachers_Select_AllbyEmpId()
            For i As Integer = 0 To dt.Rows.Count - 1
                FK_CourseIdVal = dt.Rows(i)("FK_CourseId")
                cblCourseList.Items.FindByValue(FK_CourseIdVal).Selected = True
            Next
        End If


        '  Next
        'objCourseTeachers = New CourseTeachers
        'If EmployeeId <> 0 Then


        '    objCourseTeachers.FK_EmployeeId = EmployeeId
        '    cblCourseList.DataSource = objCourseTeachers.GetCourseTeachers_Select_AllbyEmpId()
        '    'cblCourseList.DataTextField = "Name"
        '    'cblCourseList.DataValueField = "ID"
        '    cblCourseList.DataBind()
        'End If
    End Sub

    Public Sub fillCourseList()
        Dim objCourse As New Course
        Dim dt As DataTable = objCourse.GetAll
        If (dt IsNot Nothing) Then
            Dim dtCourse = dt
            If (dtCourse IsNot Nothing) Then
                If (dtCourse.Rows.Count > 0) Then
                    Dim dtSource As New DataTable
                    dtSource.Columns.Add("CourseId")
                    dtSource.Columns.Add("CourseName")
                    dtSource.Columns.Add("CourseNameAr")
                    Dim drRow As DataRow
                    drRow = dtSource.NewRow()
                    For Item As Integer = 0 To dtCourse.Rows.Count - 1
                        Dim drSource As DataRow
                        drSource = dtSource.NewRow
                        Dim dcCell1 As New DataColumn
                        Dim dcCell2 As New DataColumn
                        Dim dcCell3 As New DataColumn
                        dcCell1.ColumnName = "CourseId"
                        dcCell2.ColumnName = "CourseName"
                        dcCell3.ColumnName = "CourseNameAr"

                        dcCell1.DefaultValue = dtCourse.Rows(Item)(0)
                        dcCell2.DefaultValue = dtCourse.Rows(Item)(1) ' + "-" + IIf(Lang = CtlCommon.Lang.EN, dtCourse.Rows(Item)(1), dtCourse.Rows(Item)(2))
                        dcCell3.DefaultValue = dtCourse.Rows(Item)(2)
                        drSource("CourseId") = dcCell1.DefaultValue
                        drSource("CourseName") = dcCell2.DefaultValue
                        drSource("CourseNameAr") = dcCell3.DefaultValue
                        dtSource.Rows.Add(drSource)
                    Next
                    Dim dv As New DataView(dtSource)
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        'dv.Sort = "EmployeeName"
                        For Each row As DataRowView In dv

                            cblCourseList.Items.Add(New ListItem(row("CourseNameAr").ToString(), row("CourseId").ToString()))

                        Next
                    Else
                        For Each row As DataRowView In dv

                            cblCourseList.Items.Add(New ListItem(row("CourseName").ToString(), row("CourseId").ToString()))

                        Next
                        FillcblCourseList()
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub fillGrade()

        Dim objClassGrade As New ClassGrade
        Dim dt As DataTable = objClassGrade.GetAll
        If (dt IsNot Nothing) Then
            Dim dtClassGrade = dt
            If (dtClassGrade IsNot Nothing) Then
                If (dtClassGrade.Rows.Count > 0) Then
                    Dim dtSource As New DataTable
                    dtSource.Columns.Add("ClassGradeId")
                    dtSource.Columns.Add("ClassGradeName")
                    dtSource.Columns.Add("ClassGradeNameAr")
                    Dim drRow As DataRow
                    drRow = dtSource.NewRow()
                    For Item As Integer = 0 To dtClassGrade.Rows.Count - 1
                        Dim drSource As DataRow
                        drSource = dtSource.NewRow
                        Dim dcCell1 As New DataColumn
                        Dim dcCell2 As New DataColumn
                        Dim dcCell3 As New DataColumn
                        dcCell1.ColumnName = "ClassGradeId"
                        dcCell2.ColumnName = "ClassGradeName"
                        dcCell3.ColumnName = "ClassGradeNameAr"

                        dcCell1.DefaultValue = dtClassGrade.Rows(Item)(0)
                        dcCell2.DefaultValue = dtClassGrade.Rows(Item)(1) '+ "-" + IIf(Lang = CtlCommon.Lang.EN, dtClassGrade.Rows(Item)(1), dtClassGrade.Rows(Item)(2))
                        dcCell3.DefaultValue = dtClassGrade.Rows(Item)(2)
                        drSource("ClassGradeId") = dcCell1.DefaultValue
                        drSource("ClassGradeName") = dcCell2.DefaultValue
                        drSource("ClassGradeNameAr") = dcCell3.DefaultValue
                        dtSource.Rows.Add(drSource)
                    Next
                    Dim dv As New DataView(dtSource)
                    If SessionVariables.CultureInfo = "ar-JO" Then
                        'dv.Sort = "EmployeeName"
                        For Each row As DataRowView In dv

                            cblGradeList.Items.Add(New ListItem(row("ClassGradeNameAr").ToString(), row("ClassGradeId").ToString()))

                        Next
                    Else
                        For Each row As DataRowView In dv

                            cblGradeList.Items.Add(New ListItem(row("ClassGradeName").ToString(), row("ClassGradeId").ToString()))

                        Next

                    End If
                End If
            End If
        End If
    End Sub
#End Region


#Region "Class&Courses"
    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdClassCourse.Skin))
    End Function

    Protected Sub dgrdClassCourse_DataBound(sender As Object, e As System.EventArgs) Handles dgrdClassCourse.DataBound
        Dim TotalWeeklyStr As String

        If SessionVariables.CultureInfo = "ar-JO" Then
            TotalWeeklyStr = "مجموع الحصص الاسبوعية: "
        Else
            TotalWeeklyStr = "Total Weekly: "
        End If
        totalWeekly = 0 'to set total 0, To prevent totalweekly addnig
        For Each item As GridDataItem In dgrdClassCourse.MasterTableView.Items
            Dim cell As TableCell = item("weeklyCount")
            totalWeekly = totalWeekly + cell.Text
        Next

        Dim footerItem As GridFooterItem = dgrdClassCourse.MasterTableView.GetItems(GridItemType.Footer)(0)

        footerItem("weeklyCount").Text = footerItem("weeklyCount").Text + TotalWeeklyStr + totalWeekly.ToString()

    End Sub

    Protected Sub dgrdClassCourse_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdClassCourse.NeedDataSource
        dgrdClassCourse.DataSource = ClassCoursesdt

    End Sub

    Protected Sub dgrdClassCourse_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dgrdClassCourse.SelectedIndexChanged
        Try
            Dim objTeacherClasses As New TeacherClasses
            Dim dt As DataTable = ClassCoursesdt
            FK_ClassId = CInt(CType(dgrdClassCourse.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_ClassId"))
            FK_CourseId = CInt(CType(dgrdClassCourse.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_CourseId"))
            FK_EmployeeId = CInt(CType(dgrdClassCourse.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_EmployeeId"))
            objTeacherClasses.FK_ClassId = FK_ClassId
            objTeacherClasses.FK_CourseId = FK_CourseId
            objTeacherClasses.FK_EmployeeId = FK_EmployeeId
            RadCmbClass.SelectedValue = FK_ClassId
            RadCmbCourse.SelectedValue = FK_CourseId

            txtWeeklyNo.Text = CType(dgrdClassCourse.SelectedItems(0), GridDataItem).GetDataKeyValue("weeklyCount")

        Catch ex As Exception
        End Try
    End Sub

    Private Sub FillDDLCourses()
        RadCmbCourse.Items.Clear()
        Dim dt As DataTable = Nothing
        objCourseTeachers = New CourseTeachers()
        objCourseTeachers.FK_EmployeeId = EmployeeId
        dt = objCourseTeachers.GetAll_ByEmployeeId()
        CtlCommon.FillTelerikDropDownList(RadCmbCourse, dt, Lang)
    End Sub

    Private Sub FillClasses()
        RadCmbClass.Items.Clear()
        RadCmbClass.SelectedValue = -1
        Dim dt As DataTable = Nothing
        objTeacherGrades = New TeacherGrades()
        objTeacherGrades.FK_EmployeeId = EmployeeId
        dt = objTeacherGrades.GetAll_ByEmployeeId()
        CtlCommon.FillTelerikDropDownList(RadCmbClass, dt, Lang)
    End Sub

    Sub CreateClassCoursesdt()
        ClassCoursesdt = New DataTable
        Dim dc As DataColumn
        dc = New DataColumn

        dc = New DataColumn
        dc.ColumnName = "FK_ClassId"
        dc.DataType = GetType(Integer)
        ClassCoursesdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "FK_EmployeeId"
        dc.DataType = GetType(Integer)
        ClassCoursesdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "FK_CourseId"
        dc.DataType = GetType(Integer)
        ClassCoursesdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "weeklyCount"
        dc.DataType = GetType(Integer)
        ClassCoursesdt.Columns.Add(dc)

    End Sub

    Sub FillGridTeacherClasses()
        If EmployeeId > 0 Then

            objTeacherClasses = New TeacherClasses
            objTeacherClasses.FK_EmployeeId = EmployeeId
            ClassCoursesdt = objTeacherClasses.GetAllByEmployee()
            dgrdClassCourse.DataSource = ClassCoursesdt
            dgrdClassCourse.DataBind()

        Else
            CreateClassCoursesdt()
            dgrdClassCourse.DataSource = ClassCoursesdt
            dgrdClassCourse.DataBind()
        End If
    End Sub

    Sub clearTeachersClasses()
        ClassCoursesdt = New DataTable
        dgrdClassCourse.DataSource = ClassCoursesdt
        dgrdClassCourse.DataBind()
        RadCmbCourse.SelectedIndex = -1
        RadCmbClass.SelectedIndex = -1
        txtWeeklyNo.Text = ""
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim TeacherMaxLesseons As Integer = ConfigurationManager.AppSettings("TeacherMaxLesseons")
            totalWeekly = 0 'to set total 0, To prevent totalweekly addnig
            For Each item As GridDataItem In dgrdClassCourse.MasterTableView.Items
                Dim cell As TableCell = item("weeklyCount")
                totalWeekly = totalWeekly + cell.Text
            Next
            If TeacherMaxLesseons < totalWeekly + txtWeeklyNo.Text Then
                If SessionVariables.CultureInfo = "ar-JO" Then
                    CtlCommon.ShowMessage(Me.Page, "لقد تجاوزت الحد المسموح به من الدروس وهو " & TeacherMaxLesseons)
                Else
                    CtlCommon.ShowMessage(Me.Page, "You Have Exceeded The Allowed Number Of Lessions: " & TeacherMaxLesseons)
                End If

                Exit Sub
            Else

                objTeacherClasses = New TeacherClasses
                Dim err As Integer = -1
                If EmployeeId > 0 Then
                    With objTeacherClasses
                        .FK_ClassId = RadCmbClass.SelectedValue
                        .FK_CourseId = RadCmbCourse.SelectedValue
                        .FK_EmployeeId = EmployeeId
                        .weeklyCount = txtWeeklyNo.Text
                        .CREATED_BY = SessionVariables.LoginUser.ID
                        .CREATED_DATE = Now
                        err = .Add()
                        FillGridTeacherClasses()
                    End With
                Else
                    With objTeacherClasses
                        .FK_ClassId = RadCmbClass.SelectedValue
                        .FK_CourseId = RadCmbCourse.SelectedValue
                        .FK_EmployeeId = EmployeeId
                        .weeklyCount = txtWeeklyNo.Text
                        .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                        .LAST_UPDATE_DATE = Now
                        err = .Update()
                        FillGridTeacherClasses()
                    End With

                End If
                If err = 0 Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo))
                    dgrdClassCourse.DataSource = ClassCoursesdt
                    dgrdClassCourse.DataBind()
                    FK_ClassId = 0
                    RadCmbClass.SelectedValue = -1
                    RadCmbCourse.SelectedValue = -1
                    txtWeeklyNo.Text = ""
                    txtWeeklyNo.Enabled = True
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorAdd", CultureInfo))
                End If
            End If
        Catch ex As Exception
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorAdd", CultureInfo))
        End Try
    End Sub

    Protected Sub btnClear_Click(sender As Object, e As System.EventArgs) Handles btnClear.Click
        clearTeachersClasses()
        FillGridTeacherClasses()
    End Sub

    Protected Sub btnRemove_Click(sender As Object, e As System.EventArgs) Handles btnRemove.Click
        Dim err As Integer

        Dim strBuilder As New StringBuilder
        For Each row As GridDataItem In dgrdClassCourse.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                Dim strName As String = row.GetDataKeyValue("ClassName").ToString()
                Dim strName2 As String = row.GetDataKeyValue("CourseName").ToString()
                objTeacherClasses = New TeacherClasses
                objTeacherClasses.FK_ClassId = Convert.ToInt32(row.GetDataKeyValue("FK_ClassId").ToString())
                objTeacherClasses.FK_CourseId = Convert.ToInt32(row.GetDataKeyValue("FK_CourseId").ToString())
                objTeacherClasses.FK_EmployeeId = Convert.ToInt32(row.GetDataKeyValue("FK_EmployeeId").ToString())
                err = objTeacherClasses.Delete()
                With strBuilder
                    If err = 0 Then
                        .Append(strName & " - " & strName2 & " Deleted")
                        .Append("\n")
                    Else
                        .Append(strName & " - " & strName2 & " Could't Delete")
                        .Append("\n")
                    End If

                End With
            End If
        Next
        CtlCommon.ShowMessage(Me.Page, strBuilder.ToString())
        FillGridTeacherClasses()
        RadCmbCourse.SelectedIndex = -1
        RadCmbClass.SelectedIndex = -1
        txtWeeklyNo.Text = ""

    End Sub

    Sub FillWeeklyCount()
        'If cblGradeList.SelectedIndex > 0 And ClassId > 0 Then
        Try
            Dim dt As DataTable = Nothing
            objClassGradeCourses = New ClassGradeCourses()
            objClassGradeCourses.FK_CourseId = RadCmbCourse.SelectedValue

            objClassGradeCourses.FK_ClassGradeId = cblGradeList.SelectedValue
            dt = objClassGradeCourses.GetWeeklyNo()
            If Not dt Is Nothing Then
                If Not IsDBNull(dt.Rows(0)("WeeklyCourcesNumber")) Then
                    txtWeeklyNo.Text = dt.Rows(0)("WeeklyCourcesNumber")
                    txtWeeklyNo.Enabled = False

                End If
            End If

        Catch ex As Exception

        End Try
        
        'End If

    End Sub

#End Region


   
    Protected Sub RadCmbCourse_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadCmbCourse.SelectedIndexChanged
        FillWeeklyCount()
    End Sub
End Class
