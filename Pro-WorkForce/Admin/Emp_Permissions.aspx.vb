Imports System.Data
Imports Telerik.Web.UI
Imports TA.Employees
Imports TA.Definitions
Imports SmartV.UTILITIES
Imports TA.Admin
Imports TA.Lookup
Imports Telerik.Web.UI.Upload
Imports Telerik.Web.UI.UploadedFile
Imports System
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Class Emp_PermissionsPage
    Inherits System.Web.UI.Page

#Region "Class Variables"

    Private objOrgCompany As New OrgCompany
    Private objOrgEntity As New OrgEntity
    Private objEmp_Permissions As New Emp_Permissions
    Private objPermissionsTypes As New PermissionsTypes
    Private objEmployee As New Employee
    Dim objWeekDays As New WeekDays
    Private UploadPath As String = ConfigurationManager.AppSettings.Get("ProDocUploadPath")
    Private UploadSize As String = ConfigurationManager.AppSettings.Get("ProDocUploadSize")
    Private UploadFileTypes As String = ConfigurationManager.AppSettings.Get("ProDocUploadFileTypes")
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private Lang As CtlCommon.Lang

#End Region

#Region "Properties"

    Public Property NewUploadPath() As String
        Get
            Return ViewState("NewUploadPath")

        End Get
        Set(ByVal value As String)

            ViewState("NewUploadPath") = value

        End Set
    End Property

    Public Property FileName() As String
        Get
            Return ViewState("FileName")

        End Get
        Set(ByVal value As String)

            ViewState("FileName") = value

        End Set
    End Property

    Public Property EmployeeNo() As String
        Get
            Return ViewState("EmployeeNo")
        End Get
        Set(ByVal value As String)
            ViewState("EmployeeNo") = value
        End Set
    End Property

    Public Property PermissionId() As Integer
        Get
            Return ViewState("PermissionId")
        End Get
        Set(ByVal value As Integer)
            ViewState("PermissionId") = value
        End Set
    End Property

#End Region

#Region "Page events"

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
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            ElseIf SessionVariables.CultureInfo = "ar-JO" Then
                Lang = CtlCommon.Lang.AR
            Else
                Lang = CtlCommon.Lang.EN
            End If
            initializePermissions()
        End If
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim Err As Integer = -1
        If IsUpload() Then
            With objEmp_Permissions
                .PermissionId = PermissionId
                .AttachedFile = FileName
                Err = .UpdateAttachment
            End With

        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        With objEmp_Permissions
            .PermissionId = PermissionId
            .Delete()
            Clearall()
        End With
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clearall()
    End Sub

    Protected Sub lnkList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkList.Click
        Clearall()
        mvPermissions.ActiveViewIndex = 0
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        savePermission()
    End Sub

    Protected Sub lnkInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkInfo.Click
        mvPermissions.ActiveViewIndex = 1

    End Sub

    Protected Sub RadComboBoxCompanyAdd_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxCompanyAdd.SelectedIndexChanged
        fillEntityAdd()
        fillemployeesAdd()
    End Sub

    Protected Sub RadComboBoxEntityAdd_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxEntityAdd.SelectedIndexChanged
        fillemployeesAdd()
    End Sub

    Protected Sub rdbpermissionduration_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbpermissionduration.SelectedIndexChanged
        ArrangePermissionDate()
    End Sub

    Protected Sub RadComboBoxCompany_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxCompany.SelectedIndexChanged
        fillEntity()
        fillemployees()
    End Sub

    Protected Sub RadComboBoxEntity_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxEntity.SelectedIndexChanged
        fillemployees()
    End Sub

    Protected Sub dgrdEmployeePermission_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub

    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return RadAjaxLoadingPanel.GetWebResourceUrl(Page, String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", dgrdEmployeePermission.Skin))
    End Function

    Protected Sub dgrdEmployeePermission_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdEmployeePermission.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            If item.GetDataKeyValue("AttachedFile").ToString() = "Files Not Found" Then
                Dim btn As LinkButton = DirectCast(item.FindControl("lnkDocumentName"), LinkButton)
                btn.Visible = False
            End If
        End If
    End Sub

    Protected Sub dgrdEmployeePermission_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdEmployeePermission.NeedDataSource
        dgrdEmployeePermission.DataSource = objEmp_Permissions.GetAllBySearchCriteria(dteStartDate.SelectedDate, dteEndDate.SelectedDate, _
If(RadComboBoxCompany.SelectedValue > 0, RadComboBoxCompany.SelectedValue, "-1"), If(RadComboBoxEntity.SelectedValue <> "", RadComboBoxEntity.SelectedValue, "-1") _
, If(RadComboBoxEmployee.SelectedValue <> "", RadComboBoxEmployee.SelectedValue, "-1"))
    End Sub

    Protected Sub dgrdEmployeePermission_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdEmployeePermission.SelectedIndexChanged
        PermissionId = Convert.ToInt32(DirectCast(dgrdEmployeePermission.SelectedItems(0), GridDataItem).GetDataKeyValue("PermissionId").ToString())
        EmployeeNo = DirectCast(dgrdEmployeePermission.SelectedItems(0), GridDataItem).GetDataKeyValue("EmployeeNo").ToString()
        Tab2.Visible = True
        FillPermission()
        mvPermissions.ActiveViewIndex = 1
        RadComboBoxCompanyAdd.SelectedValue = Convert.ToInt32(DirectCast(dgrdEmployeePermission.SelectedItems(0), GridDataItem).GetDataKeyValue("CompanyId").ToString())
        fillEntityAdd()
        RadComboBoxEntityAdd.SelectedValue = Convert.ToInt32(DirectCast(dgrdEmployeePermission.SelectedItems(0), GridDataItem).GetDataKeyValue("FK_EntityId").ToString())
        btnSave.Text = "Update"
    End Sub

    Protected Sub ibtnApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnApply.Click
        dgrdEmployeePermission.Rebind()
        fillgrid()
    End Sub

    Protected Sub lnkRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRemove.Click
        If IO.File.Exists(NewUploadPath + FileName) Then
            IO.File.Delete(NewUploadPath + FileName)
            lnkRemove.Visible = False
            lblMessage.Visible = False
            FileName = String.Empty
        End If

    End Sub

#End Region

#Region "Methods"

    Public Sub fillEntity()
        objOrgEntity = New OrgEntity
        With objOrgEntity
            .FK_CompanyId = RadComboBoxCompany.SelectedValue
            Dim dt As DataTable = objOrgEntity.SelectAllForDDLByCompany()
            Dim row As DataRow = dt.Select("EntityName = 'All Entities'")(0)
            dt.Rows.Remove(row)
            CtlCommon.FillTelerikDropDownList(RadComboBoxEntity, dt)

        End With
    End Sub

    Public Sub fillEntityAdd()
        objOrgEntity = New OrgEntity
        With objOrgEntity
            .FK_CompanyId = RadComboBoxCompanyAdd.SelectedValue
            Dim dt As DataTable = objOrgEntity.SelectAllForDDLByCompany()
            Dim row As DataRow = dt.Select("EntityName = 'All Entities'")(0)
            dt.Rows.Remove(row)
            CtlCommon.FillTelerikDropDownList(RadComboBoxEntityAdd, dt)

        End With
    End Sub

    Public Sub fillemployees()

        If RadComboBoxCompany.SelectedValue > 0 Then

            If RadComboBoxEntity.SelectedValue > 0 Then

                With objOrgEntity
                    .EntityId = RadComboBoxEntity.SelectedValue
                    CtlCommon.FillTelerikDropDownList(RadComboBoxEmployee, .GetEmployeesByOrgEntity)
                End With

            Else

                With objOrgCompany
                    .CompanyId = RadComboBoxCompany.SelectedValue
                    CtlCommon.FillTelerikDropDownList(RadComboBoxEmployee, .GetEmployeesByOrgCompany)
                End With
            End If
            'Else
            '    With objOrgCompany
            '        .CompanyId = RadComboBoxCompany.SelectedValue
            '        CtlCommon.FillTelerikDropDownList(RadComboBoxEmployee, .GetEmployeesByOrgCompany)
            '    End With
        End If




    End Sub

    Public Sub fillemployeesAdd()
        If RadComboBoxCompanyAdd.SelectedValue = -1 Then
            With objEmployee
                CtlCommon.FillTelerikDropDownList(RadComboBoxEmployeeAdd, .GetAllforDDL)
            End With
        End If
        If RadComboBoxCompanyAdd.SelectedValue > 0 Then
            If RadComboBoxEntityAdd.SelectedValue > 0 Then
                With objOrgEntity
                    .EntityId = RadComboBoxEntityAdd.SelectedValue
                    CtlCommon.FillTelerikDropDownList(RadComboBoxEmployeeAdd, .GetEmployeesByOrgEntity)
                End With
            Else

                With objOrgCompany
                    .CompanyId = RadComboBoxCompanyAdd.SelectedValue
                    CtlCommon.FillTelerikDropDownList(RadComboBoxEmployeeAdd, .GetEmployeesByOrgCompany)
                End With
            End If
        End If
    End Sub

    Private Function GetFirstDayOfMonth(ByVal dtDate As DateTime) As DateTime
        Dim dtFrom As DateTime = dtDate
        dtFrom = dtFrom.AddDays(-1 * (dtFrom.Day - 1))
        Return dtFrom
    End Function

    Private Function GetLastDayOfMonth(ByVal dtDate As DateTime) As DateTime
        Dim dtTo As New DateTime(dtDate.Year, dtDate.Month, 1)
        dtTo = dtTo.AddMonths(1)
        dtTo = dtTo.AddDays(-1 * (dtTo.Day))
        Return dtTo
    End Function

    Sub ArrangePermissionDate()
        Select Case rdbpermissionduration.SelectedValue

            Case "1"
                DivPENDDATE.Visible = False
                ReqValdtePermissionEnddate.Enabled = False
                divDays.Visible = False

            Case "2"
                DivPENDDATE.Visible = True
                ReqValdtePermissionEnddate.Enabled = True
                divDays.Visible = False

            Case "3"
                DivPENDDATE.Visible = True
                ReqValdtePermissionEnddate.Enabled = True

                CtlCommon.FillCheckBox(chkDays, objWeekDays.GetAll())
                divDays.Visible = True
        End Select
    End Sub

    Sub savePermission()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim Err As Integer = -1
        With objEmp_Permissions

            .FK_EmployeeId = RadComboBoxEmployeeAdd.SelectedValue

            .FK_PermId = RadComboBoxPermissionsType.SelectedValue
            .FromTime = tpFromTime.SelectedDate.Value
            .ToTime = tpToTime.SelectedDate.Value
            .Remark = txtRemarks.Text
            .PermDate = dtePermDate.DbSelectedDate
            .IsDividable = False
            .IsFlexible = False
            .IsFullDay = False
            .IsSpecificDays = False

            Select Case rdbpermissionduration.SelectedValue
                Case "1"
                    .IsForPeriod = False
                Case "2"
                    .IsForPeriod = True
                    .PermEndDate = dtePermissionEnddate.SelectedDate
                Case "3"
                    .IsForPeriod = True
                    .PermEndDate = dtePermissionEnddate.SelectedDate
                    Dim count As Integer = 0
                    For Each lst As ListItem In chkDays.Items
                        If lst.Selected Then
                            count += 1
                            Exit For
                        End If
                    Next
                    If count = 0 Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DaySelect", CultureInfo), "info")
                        Exit Sub
                    End If
                    .Days = _
             (From oItem As ListItem In chkDays.Items _
              Where oItem.Selected Select oItem.Value). _
              Aggregate(Of StringBuilder)(New StringBuilder, _
              Function(Current As StringBuilder, sValue As String) _
              Current.AppendFormat(",{0}", sValue)).ToString.Substring(1)
            End Select

            .CREATED_BY = SessionVariables.LoginUser.ID
            .CREATED_DATE = Date.Today
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
            .LAST_UPDATE_DATE = Date.Today
            If PermissionId = 0 Then
                Err = .Add()
                PermissionId = .PermissionId
                With objEmployee
                    .EmployeeId = RadComboBoxEmployeeAdd.SelectedValue
                    EmployeeNo = .GetByPK.EmployeeNo
                    Tab2.Visible = True
                End With

            Else
                .PermissionId = PermissionId
                Err = .Update
            End If

            If Err = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        End With
    End Sub

    Function IsUpload() As Boolean
        lblMessage.Visible = False
        lnkRemove.Visible = False
        Dim blnStatus As Boolean = True
        If RadUpload1.UploadedFiles.Count > 0 Then
            If UploadFileTypes.Contains(LCase(RadUpload1.UploadedFiles.Item(0).GetExtension())) Then
                Dim x As Integer = RadUpload1.UploadedFiles.Item(0).ContentLength
                If UploadSize >= RadUpload1.UploadedFiles.Item(0).ContentLength Then
                    AddDeleteDependencyForFile(RadUpload1.UploadedFiles)
                    UpdateProgressContext()
                    Return True
                Else
                    Dim size As Single = CSng(UploadSize) / 1024
                    size = Math.Round(size, 2)
                    lblMessage.Text = ResourceManager.GetString("UploadSize", CultureInfo) + " " + CStr(size) + ResourceManager.GetString("KB", CultureInfo)
                    lblMessage.Visible = True
                    Return False
                End If
            Else
                lblMessage.Text = ResourceManager.GetString("UploadSupport", CultureInfo) + " (" + UploadFileTypes + " ) " + ResourceManager.GetString("Files", CultureInfo)
                lblMessage.Visible = True
                Return False
            End If
        End If
        Return blnStatus
    End Function



    Private Sub AddDeleteDependencyForFile(ByVal uploadedFileCollection As UploadedFileCollection)
        NewUploadPath = UploadPath & "\\PermissionDocument" & "\\" & EmployeeNo & "\\"
        Dim uploadedFile As UploadedFile
        For Each uploadedFile In uploadedFileCollection
            If Not IO.Directory.Exists(NewUploadPath) Then
                IO.Directory.CreateDirectory(NewUploadPath)
            End If
            Dim strFile As New StringBuilder
            strFile.Append("Permission_")
            strFile.Append(EmployeeNo.ToString)
            strFile.Append("_")
            strFile.Append(Now.ToString("ddMMHHmm"))
            strFile.Append(Path.GetExtension(uploadedFile.FileName))
            FileName = strFile.ToString
            uploadedFile.SaveAs(NewUploadPath + FileName, True)
            Dim timeOut As TimeSpan = TimeSpan.FromMinutes(1)
        Next
    End Sub

    Private Sub DownloadFile(ByVal fname As String, ByVal forceDownload As Boolean)
        Dim fullpath = fname
        Dim name = Path.GetFileName(fullpath)
        Dim ext = Path.GetExtension(fullpath)
        Dim type As String = ""
        If Not IsDBNull(ext) Then
            ext = LCase(ext)
        End If
        Select Case ext
            Case ".htm", ".html"
                type = "text/HTML"
            Case ".txt"
                type = "text/plain"
            Case ".doc"
                type = "Application/msword"
            Case ".rtf"
                type = "Application/msword"
            Case ".docx"
                type = "Application/msword"
            Case ".csv"
                type = "Application/x-msexcel"
            Case ".xls"
                type = "Application/x-msexcel"
            Case ".xlsx"
                type = "Application/x-msexcel"
            Case ".pdf"
                type = "Application/pdf"
            Case ".jpg"
                type = "Application/jpg"
            Case ".bmp"
                type = "Application/bmp"
            Case ".png"
                type = "Application/png"
            Case ".gif"
                type = "Application/gif"
            Case Else
                type = "text/plain"
        End Select

        If (forceDownload) Then
            Response.AppendHeader("content-disposition", _
            "attachment; filename=" + name)
        End If
        If type <> "" Then
            Response.ContentType = type
        End If
        Response.WriteFile(fullpath)
        Response.End()
    End Sub

    Public Sub lnkDocumentName_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FileName = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("AttachedFile").Text)
        EmployeeNo = CStr(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridDataItem)("EmployeeNo").Text)
        NewUploadPath = UploadPath & "PermissionDocument" & "\\" & EmployeeNo & "\\"
        Dim filetodownload As String = NewUploadPath & FileName
        Dim MyFile As New FileInfo(filetodownload)
        If MyFile.Exists() Then
            DownloadFile(filetodownload, True)
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("FileNotFound", CultureInfo), "info")
        End If


    End Sub

    Private Sub UpdateProgressContext()
        Const total As Integer = 100
        Dim progress As RadProgressContext = RadProgressContext.Current
        progress.Speed = "N/A"
        For i As Integer = 0 To total - 1
            progress.PrimaryTotal = 1
            progress.PrimaryValue = 1
            progress.PrimaryPercent = 100
            progress.SecondaryTotal = total
            progress.SecondaryValue = i
            progress.SecondaryPercent = i
            progress.CurrentOperationText = "Step " & i.ToString()
            If Not Response.IsClientConnected Then
                Exit For
            End If
            progress.TimeEstimated = (total - i) * 10
            System.Threading.Thread.Sleep(10)
        Next
        lblMessage.Text = FileName + " " + ResourceManager.GetString("UpdatedSuccessfully", CultureInfo)
        lblMessage.Visible = True
        lnkRemove.Visible = True
    End Sub

    Sub FillPermission()
        With objEmp_Permissions
            .PermissionId = PermissionId
            .GetByPK()
            RadComboBoxEmployeeAdd.SelectedValue = .FK_EmployeeId
            RadComboBoxPermissionsType.SelectedValue = .FK_PermId
            tpFromTime.SelectedDate = .FromTime
            tpToTime.SelectedDate = .ToTime
            txtRemarks.Text = .Remark
            dtePermDate.DbSelectedDate = .PermDate
            If .PermEndDate.HasValue Then
                dtePermissionEnddate.SelectedDate = .PermEndDate
            End If
            If Not .IsForPeriod Then
                rdbpermissionduration.SelectedValue = 1
                DivPENDDATE.Visible = False
                ReqValdtePermissionEnddate.Enabled = False
                divDays.Visible = False
            Else
                DivPENDDATE.Visible = True
                ReqValdtePermissionEnddate.Enabled = True
                If (.Days) Is Nothing Then
                    rdbpermissionduration.SelectedValue = 2
                Else
                    rdbpermissionduration.SelectedValue = 3
                    divDays.Visible = True
                    CtlCommon.FillCheckBox(chkDays, objWeekDays.GetAll())
                    Dim items As String() = .Days.ToString().Split(",")
                    For Each item In items
                        For Each lst As ListItem In chkDays.Items
                            If lst.Value = item Then
                                lst.Selected = True
                            End If
                        Next
                    Next
                End If
            End If
        End With
    End Sub

    Sub Clearall()
        PermissionId = 0
        txtRemarks.Text = ""
        dteEndDate.Clear()
        dtePermDate.Clear()
        dtePermissionEnddate.Clear()
        dteStartDate.Clear()
        tpFromTime.Clear()
        tpToTime.Clear()
        rdbpermissionduration.SelectedValue = 1
        'initializePermissions()
        dteStartDate.SelectedDate = GetFirstDayOfMonth(Today)
        dteEndDate.SelectedDate = GetLastDayOfMonth(Today)
        RadComboBoxEntity.SelectedValue = -1
        RadComboBoxEmployee.SelectedValue = -1
        RadComboBoxCompany.SelectedValue = -1
        RadComboBoxEntityAdd.SelectedValue = -1
        RadComboBoxEmployeeAdd.SelectedValue = -1
        RadComboBoxCompanyAdd.SelectedValue = -1
        RadComboBoxPermissionsType.SelectedValue = -1
        fillgrid()
        EmployeeNo = ""
        Tab2.Visible = False
        lblMessage.Visible = False
        lnkRemove.Visible = False
        FileName = ""
        btnSave.Text = "Save"
        ArrangePermissionDate()
    End Sub

    Sub initializePermissions()
        CtlCommon.FillTelerikDropDownList(RadComboBoxCompany, objOrgCompany.GetAllforddl)
        CtlCommon.FillTelerikDropDownList(RadComboBoxCompanyAdd, objOrgCompany.GetAllforddl)
        CtlCommon.FillTelerikDropDownList(RadComboBoxPermissionsType, objPermissionsTypes.GetAll)
        'fillEntity()
        'fillemployees()
        'fillEntityAdd()
        fillemployeesAdd()
        dteStartDate.SelectedDate = GetFirstDayOfMonth(Today)
        dteEndDate.SelectedDate = GetLastDayOfMonth(Today)
        ArrangePermissionDate()
        fillgrid()
        lblMessage.Visible = False
        lnkRemove.Visible = False
    End Sub

    Sub fillgrid()

        dgrdEmployeePermission.DataSource = objEmp_Permissions.GetAllBySearchCriteria(dteStartDate.SelectedDate, dteEndDate.SelectedDate, _
If(RadComboBoxCompany.SelectedValue > 0, RadComboBoxCompany.SelectedValue, "-1"), If(RadComboBoxEntity.SelectedValue <> "", RadComboBoxEntity.SelectedValue, "-1") _
, If(RadComboBoxEmployee.SelectedValue <> "", RadComboBoxEmployee.SelectedValue, "-1"))
        dgrdEmployeePermission.DataBind()
    End Sub

#End Region

End Class
