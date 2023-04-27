Imports SmartV.UTILITIES
Imports System.Data
Imports TA.Employees
Imports Telerik.Web.UI.DatePickerPopupDirection
Imports Telerik.Web.UI
Imports SmartV.UTILITIES.ProjectCommon
Imports TA.Definitions
Imports System.IO

Partial Class Emp_userControls_WebUserControl
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Enum DisplayModeEnum
        Add
        Edit
        View
        ViewAll
        ViewAddEdit
    End Enum

    Private objEmp_Leaves As Emp_Leaves
    ' Class variables , used to fill combo boxes
    Private objEmployee As Employee
    Private objLeavesTypes As LeavesTypes
    Shared SortDir As String
    Shared SortExep As String
    Shared SortExepression As String
    Shared dtCurrent As DataTable
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo

#End Region

#Region "Properties"

    Public Property LeaveId() As Integer
        Get
            Return ViewState("LeaveId")
        End Get
        Set(ByVal value As Integer)
            ViewState("LeaveId") = value
        End Set
    End Property

    Public Property DisplayMode() As DisplayModeEnum
        Get
            Return ViewState("DisplayMode")
        End Get
        Set(ByVal value As DisplayModeEnum)
            ViewState("DisplayMode") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, _
                            ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack <> True Then
            SetRadDateTimePickerPeoperties()
            ManageDisplayMode_ExceptionalCases()
            FillGridView()
            Filllists()
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            End If

            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            userCtrlEmpLeavesHeader.HeaderText = ResourceManager.GetString("EmpLeave", CultureInfo)
            btnDelete.Attributes.Add("onclick", _
                                     "javascript:return confirmDelete('" _
                                     + dgrdVwEmpLeaves.ClientID + "');")
            ManageFunctionalities()
        End If
    End Sub

    Private Sub ManageDisplayMode_ExceptionalCases()
        ' current function handle the cases where the displayMode 
        '  associated with the in appropriate value for 
        '  the LeaveId
        Dim _Exist As Integer = 0
        _Exist = IS_Exist()
        If DisplayMode = DisplayModeEnum.View Or _
            DisplayMode = DisplayModeEnum.Edit Then
            If _Exist = -1 Then

                ' If the display mode is View or edit.But, the 
                ' Id is not a valid one
                CtlCommon.ShowMessage( _
                    Me.Page, _
                    ResourceManager.GetString("ErrorEmpProcessing", CultureInfo), "error")
                DisplayMode = DisplayModeEnum.ViewAll
            End If
        ElseIf DisplayMode = DisplayModeEnum.ViewAddEdit Or _
            DisplayModeEnum.ViewAll = DisplayMode Then
            LeaveId = 0
        ElseIf DisplayMode() = DisplayModeEnum.Add Then
            If LeaveId > 0 Then
                If _Exist = 0 Then
                    DisplayMode = DisplayModeEnum.Edit
                Else
                    ' LeaveId>0 and does not matched a data base record
                    LeaveId = 0
                End If
            Else
                ' if leaveId=0 or LeaveId<0
                LeaveId = 0
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, _
                                ByVal e As System.EventArgs) Handles btnSave.Click
        Select Case DisplayMode.ToString
            Case "Add"
                saveUpdate()
            Case "Edit"
                updateOnly()
            Case "ViewAddEdit"
                saveUpdate()
            Case Else
        End Select
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim errNum As Integer = -1

        For Each row As GridViewRow In dgrdVwEmpLeaves.Rows
            Dim cb As CheckBox = DirectCast(row.FindControl("chkRow"), CheckBox)
            If cb.Checked Then
                ' Get LeaveId from hidden label
                Dim intLeaveId As Integer = _
                    Convert.ToInt32(DirectCast(row.FindControl("lblLeaveId"), Label).Text)

                ' Delete current checked item
                objEmp_Leaves = New Emp_Leaves()
                objEmp_Leaves.LeaveId = intLeaveId
                errNum = objEmp_Leaves.Delete()
            End If
        Next
        If errNum = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillGridView()
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
        End If



    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        FillGridView()
        ClearAll()
    End Sub

    Private Function IS_Exist() As Integer
        ' The View , and Edit modes require to have a valid Leave Id 
        objEmp_Leaves = New Emp_Leaves()
        objEmp_Leaves.LeaveId = LeaveId
        Dim _EXIT As Integer = 0
        If LeaveId <= 0 Then
            _EXIT = -1
        ElseIf objEmp_Leaves.FindExisting() = False Then
            _EXIT = -1
        End If
        Return _EXIT
    End Function

    Protected Sub lnkEmployeeName_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ' Get the row where link button send the event 
        Dim gv As GridViewRow = (CType(sender, LinkButton).Parent).Parent
        ' Get the PK from the hidden field
        LeaveId = CType(gv.FindControl("lblLeaveId"), Label).Text
        FillControlsForEditing()
    End Sub

#End Region

#Region "Methods"

#Region "Fill Lists & GridView"

    Private Sub Filllists()
        Dim dt As DataTable = Nothing
        ' Get Employees
        objEmployee = New Employee
        dt = objEmployee.GetAll()
        If dt IsNot Nothing Then
            ' Fill Employee combo box
            RadCmbBxEmployee.DataSource = dt



            setLocalizedTextField(RadCmbBxEmployee, "EmployeeName", "EmployeeArabicName")

            RadCmbBxEmployee.DataValueField = "EmployeeId"
            RadCmbBxEmployee.DataBind()
        End If
        ' Reset the DataTable
        dt = Nothing
        ' Get leaves types
        objLeavesTypes = New LeavesTypes()
        dt = objLeavesTypes.GetAll()

        If dt IsNot Nothing Then

            CtlCommon.FillTelerikDropDownList(RadCmbBxLeavesTypes, dt)
        End If
    End Sub

    Private Sub FillGridView()
        Try
            objEmp_Leaves = New Emp_Leaves()
            objEmp_Leaves.LeaveId = LeaveId
            dtCurrent = objEmp_Leaves.GetAllInnerJoin()

            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            dgrdVwEmpLeaves.DataSource = dv
            dgrdVwEmpLeaves.DataBind()
        Catch ex As Exception
            ' MsgBox("FillGridView error" & ex.Message)
        End Try
    End Sub

#End Region

    Private Sub ClearAll()

        ' Clear controls and set to default values
        ' Reset combo boxes
        RadCmbBxEmployee.SelectedIndex = 0
        RadCmbBxLeavesTypes.SelectedIndex = 0
        ' Clear values of the date time picker
        dtpFromDate.SelectedDate = Today.Date
        dtpToDate.SelectedDate = Today.Date
        dtpRequestDate.SelectedDate = Today.Date
        txtRemarks.Text = String.Empty
        chckHalfDay.Checked = False

        ' Reset the Id to prepare for next add operation
        LeaveId = 0

        ' Remove soring and sorting arrow 
        SortExepression = Nothing
        SortDir = Nothing

    End Sub

    Private Sub FillControlsForEditing()
        objEmp_Leaves = New Emp_Leaves()
        ' Get the fields for the selected record
        objEmp_Leaves.LeaveId = LeaveId
        objEmp_Leaves.GetByPK()
        With objEmp_Leaves
            ' set values for the combo boxes
            RadCmbBxEmployee.SelectedValue = .FK_EmployeeId
            RadCmbBxLeavesTypes.SelectedValue = .FK_LeaveTypeId
            ' set values for date time picker
            dtpFromDate.SelectedDate = .FromDate
            dtpToDate.SelectedDate = .ToDate
            dtpRequestDate.SelectedDate = .RequestDate
            txtRemarks.Text = .Remarks
            chckHalfDay.Checked = .IsHalfDay
        End With
    End Sub

    Private Sub SetRadDateTimePickerPeoperties()


        ' This function set properties for terlerik controls

        ' Set Data input properties
        Me.dtpRequestDate.SelectedDate = Today.Date
        ' Set popup properties
        Me.dtpRequestDate.PopupDirection = TopRight
        Me.dtpRequestDate.ShowPopupOnFocus = True
        ' Set calender boundaries
        Me.dtpRequestDate.MinDate = New Date(2006, 1, 1)
        Me.dtpRequestDate.MaxDate = New Date(2040, 1, 1)


        ' Set Data input properties
        Me.dtpFromDate.SelectedDate = Today.Date
        ' Set popup properties
        Me.dtpFromDate.PopupDirection = TopRight
        Me.dtpFromDate.ShowPopupOnFocus = True
        ' Set calender boundaries
        Me.dtpFromDate.MinDate = New Date(2006, 1, 1)
        Me.dtpFromDate.MaxDate = New Date(2040, 1, 1)


        ' Set Data input properties
        Me.dtpToDate.SelectedDate = Today.Date
        ' Set Popup properties
        Me.dtpToDate.PopupDirection = TopRight
        Me.dtpToDate.ShowPopupOnFocus = True
        ' Set calender boundaries
        Me.dtpToDate.MinDate = New Date(2006, 1, 1)
        Me.dtpToDate.MaxDate = New Date(2040, 1, 1)

    End Sub

    Private Sub FillObjectsData()


        ' After creating a new object , and needs to fill with data 
        ' you can call this function 

        ' This function intended to add code scalability , 
        ' and it is called at SaveUpdate() and UpdateOnly() methods

        With objEmp_Leaves


            ' Get Values from the combo boxes
            .FK_EmployeeId = RadCmbBxEmployee.SelectedValue
            .FK_LeaveTypeId = RadCmbBxLeavesTypes.SelectedValue


            ' Get values from date time picker
            .RequestDate = dtpRequestDate.SelectedDate
            .FromDate = dtpFromDate.SelectedDate
            .ToDate = dtpToDate.SelectedDate

            .Remarks = txtRemarks.Text
            .IsHalfDay = chckHalfDay.Checked
            .AttachedFile = Path.GetExtension(fuAttachFile.PostedFile.FileName).Substring(1)

            ' Set systematic values
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID

            .CREATED_BY = SessionVariables.LoginUser.ID

            .CREATED_DATE = Today
            .LAST_UPDATE_DATE = Today


            .AttachedFile = "HH"

        End With

    End Sub

    Private Sub setLocalizedTextField(ByVal comb As RadComboBox, _
                         ByVal EnName As String, ByVal ArName As String)




        comb.DataTextField = IIf(SessionVariables.CultureInfo = "en-US", _
                                                   EnName, ArName)


    End Sub

#End Region

#Region "Sorting and paging functions"
    Protected Sub dgrdVwEmpLeaves_PageIndexChanging(ByVal sender As Object, _
                                                    ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) _
                                                    Handles dgrdVwEmpLeaves.PageIndexChanging
        Try
            dgrdVwEmpLeaves.SelectedIndex = -1
            dgrdVwEmpLeaves.PageIndex = e.NewPageIndex
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            dgrdVwEmpLeaves.DataSource = dv
            dgrdVwEmpLeaves.DataBind()
            ManageFunctionalities()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetArrowDirection()
        Try
            Dim img As System.Web.UI.WebControls.Image = New System.Web.UI.WebControls.Image()
            If Not SortDir = Nothing AndAlso Not SortDir = String.Empty Then
                If SortDir = "ASC" Then
                    img.ImageUrl = "~/images/ar-down.png"
                Else
                    img.ImageUrl = "~/images/ar-up.png"
                End If


                Select Case SortExep
                    Case "EmployeeName"
                        dgrdVwEmpLeaves.HeaderRow.Cells(1).Controls.Add(New LiteralControl(" "))
                        dgrdVwEmpLeaves.HeaderRow.Cells(1).Controls.Add(img)
                    Case "LeaveName"
                        dgrdVwEmpLeaves.HeaderRow.Cells(2).Controls.Add(New LiteralControl(" "))
                        dgrdVwEmpLeaves.HeaderRow.Cells(2).Controls.Add(img)
                    Case "FromDate"
                        dgrdVwEmpLeaves.HeaderRow.Cells(3).Controls.Add(New LiteralControl(" "))
                        dgrdVwEmpLeaves.HeaderRow.Cells(3).Controls.Add(img)
                    Case "ToDate"
                        dgrdVwEmpLeaves.HeaderRow.Cells(4).Controls.Add(New LiteralControl(" "))
                        dgrdVwEmpLeaves.HeaderRow.Cells(4).Controls.Add(img)
                    Case "IsHalfDay"
                        dgrdVwEmpLeaves.HeaderRow.Cells(5).Controls.Add(New LiteralControl(" "))
                        dgrdVwEmpLeaves.HeaderRow.Cells(5).Controls.Add(img)
                End Select
            End If
        Catch ex As Exception
            '  MsgBox("At setArrowDirection event" + ex.Message)
        End Try


    End Sub

    Protected Sub dgrdVwEmpLeaves_DataBound(ByVal sender As Object, _
                                            ByVal e As System.EventArgs) _
                                            Handles dgrdVwEmpLeaves.DataBound
        SetArrowDirection()
    End Sub

    Protected Sub dgrdVwEmpLeaves_Sorting(ByVal sender As Object, _
                                          ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) _
                                          Handles dgrdVwEmpLeaves.Sorting
        Try
            If SortDir = "ASC" Then
                SortDir = "DESC"
            Else
                SortDir = "ASC"
            End If
            SortExepression = e.SortExpression & Space(1) & SortDir
            Dim dv As New DataView(dtCurrent)
            dv.Sort = SortExepression
            SortExep = e.SortExpression
            dgrdVwEmpLeaves.DataSource = dv
            dgrdVwEmpLeaves.DataBind()


            ManageFunctionalities()
        Catch ex As Exception
            '  MsgBox("At Sorting event" + ex.Message)
        End Try
    End Sub
#End Region

#Region "Display Mode Functions"

    Sub ManageFunctionalities()
        Select Case DisplayMode.ToString
            Case "Add"
                Adddisplaymode()
            Case "Edit"
                ViewEditmode()
            Case "View"
                Viewdisplaymode()
            Case "ViewAll"
                ViewAllDisplaymode()
            Case "ViewAddEdit"
                ViewAddEditDisplaymode()
            Case Else
        End Select
    End Sub

    Public Sub LoadData()
        FillGridView()
        ManageFunctionalities()
    End Sub

    Sub ViewAddEditDisplaymode()
        RefreshControls(True)
    End Sub

    Sub Adddisplaymode()
        RefreshControls(True)
        dgrdVwEmpLeaves.Visible = False
        btnDelete.Visible = False
        btnClear.Visible = False
    End Sub

    Sub Viewdisplaymode()
        RefreshControls(True)
        ManageControls(False)
        btnDelete.Visible = False
        btnClear.Visible = False
        btnSave.Visible = False
        btnClear.Visible = False
        FillControlsForEditing()
        dgrdVwEmpLeaves.Visible = False
        For Each row As GridViewRow In dgrdVwEmpLeaves.Rows

            Dim lnb As LinkButton = DirectCast(row.FindControl("lnkEmployeeName"), LinkButton)
            lnb.Visible = True
            Dim cb As CheckBox = DirectCast(row.FindControl("chkRow"), CheckBox)
            cb.Visible = False
        Next
    End Sub

    Sub ViewAllDisplaymode()
        RefreshControls(True)
        ManageControls(False)
        btnDelete.Visible = False
        btnClear.Visible = False
        btnSave.Visible = False
        btnSave.Visible = False
        btnClear.Visible = False

        For Each row As GridViewRow In dgrdVwEmpLeaves.Rows
            Dim lb As LinkButton = DirectCast(row.FindControl("lnkEmployeeName"), LinkButton)
            Dim cb As CheckBox = DirectCast(row.FindControl("chkRow"), CheckBox)
            cb.Visible = False
            lb.Enabled = True
        Next


    End Sub

    Sub ViewEditmode()
        RefreshControls(True)
        If SessionVariables.CultureInfo = "en-US" Then
            btnSave.Text = "Update"
        Else
            btnSave.Text = "تحديث"
        End If
        FillControlsForEditing()
        dgrdVwEmpLeaves.Visible = False
        btnClear.Visible = False
        btnDelete.Visible = False
    End Sub

    Sub ManageControls(ByVal Status As Boolean)

        ' Toggle the combo boxes status
        RadCmbBxEmployee.Enabled = Status
        RadCmbBxLeavesTypes.Enabled = Status

        ' Toggle the date picker status
        dtpFromDate.Enabled = Status
        dtpRequestDate.Enabled = Status
        dtpToDate.Enabled = Status

        ' Toggle the status of remaining controls
        txtRemarks.Enabled = Status
        chckHalfDay.Enabled = Status




        ' Toggle the status of the check boxes at the GridView column
        For Each row As GridViewRow In dgrdVwEmpLeaves.Rows
            DirectCast(row.FindControl("chkRow"), CheckBox).Enabled = Status

        Next


    End Sub

    Sub RefreshControls(ByVal status As Boolean)
        ' Toggle the combo boxes status
        RadCmbBxEmployee.Enabled = status
        RadCmbBxLeavesTypes.Enabled = status

        ' Toggle the date picker status
        dtpFromDate.Enabled = status
        dtpRequestDate.Enabled = status
        dtpToDate.Enabled = status

        ' Toggle the status of remaining controls
        txtRemarks.Enabled = status
        chckHalfDay.Enabled = status


        For Each row As GridViewRow In dgrdVwEmpLeaves.Rows
            ' Show the select field of the GridView
            Dim lnb As LinkButton = DirectCast(row.FindControl("lnkEmployeeName"), LinkButton)
            lnb.Visible = True
            ' Hide check boxes at check box column
            Dim cb As CheckBox = DirectCast(row.FindControl("chkRow"), CheckBox)
            cb.Visible = True
        Next

        If SessionVariables.CultureInfo = "en-US" Then
            btnSave.Text = "Save"
        Else
            btnSave.Text = "تحديث"
        End If


        ' Toggle the status of the buttons
        btnDelete.Visible = status
        btnClear.Visible = status
        btnSave.Visible = status




    End Sub

    Function saveUpdate() As Integer
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errNo As Integer
        objEmp_Leaves = New Emp_Leaves()
        Dim errorNum As Integer = -1
        ' Set data into object for Add / Update

        FillObjectsData()

        If LeaveId = 0 Then
            errNo = objEmp_Leaves.Add()
            LeaveId = objEmp_Leaves.LeaveId

            If errorNum = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If

        Else
            objEmp_Leaves.LeaveId = LeaveId
            errNo = objEmp_Leaves.Update

            If errorNum = 0 Then
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
            End If


        End If

        If errNo = 0 Then

            Select Case DisplayMode.ToString
                Case "Add"
                    btnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
                Case Else
                    FillGridView()
                    ClearAll()
            End Select
        End If

        Dim extention As String = Path.GetExtension(fuAttachFile.PostedFile.FileName)
        Dim fileName = LeaveId
        Dim fPath As String = String.Empty
        Dim saveDirectory As String = String.Format("~/LeaveFiles/{0}{1}", fileName, extention)
        fuAttachFile.SaveAs(saveDirectory)

        Return errNo
    End Function

    Function updateOnly() As Integer
        Dim errNo As Integer

        objEmp_Leaves = New Emp_Leaves
        Dim errorNum As Integer = -1
        ' Set data into object for Add / Update
        FillObjectsData()

        If LeaveId > 0 Then

            ' Update a data base record , on update mode
            objEmp_Leaves.LeaveId = LeaveId
            objEmp_Leaves.Update()

        End If

        If errNo = 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("UpdatedSuccessfully", CultureInfo), "success")
        Else
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("ErrorUpdate", CultureInfo), "error")
        End If

        Return errNo
    End Function

#End Region

End Class
