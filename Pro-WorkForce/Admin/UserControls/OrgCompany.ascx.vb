Imports SmartV.UTILITIES
Imports System.Data
Imports System.Resources
Imports TA.Admin
Imports TA.Employees
Imports System.IO
Imports TA.DailyTasks
Imports SmartV.Version

Partial Class UserColntrols_OrgCompany
    Inherits System.Web.UI.UserControl

#Region "Class Variables"

    Private objOrgCompany As OrgCompany
    Private objTAPolicy As New TAPolicy
    Private objEmp_Designation As New Emp_Designation
    Private _DisplayMode As DisplayModeEnum
    Dim dtForParent As New DataTable
    Public Event FillTreeOrganization()
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Private objEmp_WorkLocation As Emp_WorkLocation
    Private objEmployee As Employee
    Private objVersion As version
    Private objEmp_WorkLocation_Beacon As Emp_WorkLocation_Beacon
#End Region

#Region "Properties"

    Public Enum Locations
        TreeOrgn
    End Enum

    Public Enum DisplayModeEnum
        Add
        Edit
        View
        ViewAll
        ViewAddEdit
    End Enum

    Public Property DisplayMode() As DisplayModeEnum
        Get
            Return _DisplayMode
        End Get
        Set(ByVal value As DisplayModeEnum)
            _DisplayMode = value
        End Set
    End Property

    Public Property CompanyID() As Integer
        Get
            Return ViewState("CompanyID")
        End Get
        Set(ByVal value As Integer)
            ViewState("CompanyID") = value
        End Set
    End Property

    Private Property LevelID() As Integer
        Get
            Return ViewState("LevelID")
        End Get
        Set(ByVal value As Integer)
            ViewState("LevelID") = value
        End Set
    End Property

    Private Property WorkLocationId() As Integer
        Get
            Return ViewState("WorkLocationId")
        End Get
        Set(ByVal value As Integer)
            ViewState("WorkLocationId") = value
        End Set
    End Property

    Public Property Location() As Locations
        Get
            Return ViewState("Location")
        End Get
        Set(ByVal value As Locations)
            ViewState("Location") = value
        End Set
    End Property

    Public Property Newdt() As DataTable
        Get
            Return ViewState("Newdt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("Newdt") = value
        End Set
    End Property

    Public Property WorkLocationdt() As DataTable
        Get
            Return ViewState("WorkLocationdt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("WorkLocationdt") = value
        End Set
    End Property

    Public Property AllowSave() As Boolean
        Get
            Return ViewState("AllowSave")
        End Get
        Set(ByVal value As Boolean)
            ViewState("AllowSave") = value
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

    Public Property IsOrgLevelUpdate() As Boolean
        Get
            Return ViewState("IsOrgLevelUpdate")
        End Get
        Set(value As Boolean)
            ViewState("IsOrgLevelUpdate") = value
        End Set
    End Property

    Public Property IsOrgWorkLocationUpdate() As Boolean
        Get
            Return ViewState("IsOrgWorkLocationUpdate")
        End Get
        Set(value As Boolean)
            ViewState("IsOrgWorkLocationUpdate") = value
        End Set
    End Property

    Public Property ManagerId() As Integer
        Get
            Return ViewState("ManagerId")
        End Get
        Set(ByVal value As Integer)
            ViewState("ManagerId") = value
        End Set
    End Property

    Public Property HasMobile() As Boolean
        Get
            Return ViewState("HasMobile")
        End Get
        Set(ByVal value As Boolean)
            ViewState("HasMobile") = value
        End Set
    End Property

    Public Property NoOfAllowedMobileWorkLocations() As Integer
        Get
            Return ViewState("NoOfAllowedMobileWorkLocations")
        End Get
        Set(ByVal value As Integer)
            ViewState("NoOfAllowedMobileWorkLocations") = value
        End Set
    End Property

    Public Property Beacondt() As DataTable
        Get
            Return ViewState("Beacondt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("Beacondt") = value
        End Set
    End Property

    Private Property BeaconId() As Integer
        Get
            Return ViewState("BeaconId")
        End Get
        Set(ByVal value As Integer)
            ViewState("BeaconId") = value
        End Set
    End Property

#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            Companypageload()
            CreateLevelDT()
            fillgridLevel()
            fillgridWorkLocation()
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            End If
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            RequiredFieldValidator6.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            RequiredFieldValidator1.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            ReqHighestPost.InitialValue = ResourceManager.GetString("PleaseSelect", CultureInfo)
            HasMobile = objVersion.HasMobileApplication()
            rblPunchType.SelectedValue = 0
            'HasMobile = True
            If HasMobile = True Then
                chkHasMobilePunch.Visible = True
            Else
                chkHasMobilePunch.Visible = False
            End If
            If rblPunchType.SelectedValue = 1 Then
                lblRadius.Text = "Allowed Radius"

                dvMobilePunchConsiderDuration.Visible = True
                dvMustPunchTwiceControls.Visible = False
            End If
            NoOfAllowedMobileWorkLocations = objVersion.GetNoOfAllowedMobileWorkLocations()
            'NoOfAllowedMobileWorkLocations = 4
        End If

    End Sub

    Protected Sub ibtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnSave.Click
        SaveUpdate()

    End Sub

    Protected Sub ibtnRest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnRest.Click
        clearall()
    End Sub

    'Protected Sub ibtnDeleteCompany_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDeleteCompany.Click
    '    Dim err As Integer = -1
    '    objOrgCompany = New OrgCompany
    '    objOrgCompany.CompanyId = CompanyID
    '    err = objOrgCompany.Delete()

    '    If err = 0 Then
    '        clearall()
    '        If SessionVariables.CultureInfo = "en-US" Then
    '            CtlCommon.ShowMessage(Me.Page, "Deleted Successfully")
    '        Else
    '            CtlCommon.ShowMessage(Me.Page, "تم الحذف بنجاح")
    '        End If
    '    ElseIf err = 547 Then
    '        If SessionVariables.CultureInfo = "en-US" Then
    '            CtlCommon.ShowMessage(Me.Page, "Only lowest level could be deleted")
    '        Else
    '            CtlCommon.ShowMessage(Me.Page, "خطأ أثناء الحذف")
    '        End If
    '    Else
    '        If SessionVariables.CultureInfo = "en-US" Then
    '            CtlCommon.ShowMessage(Me.Page, "Error While deleting")
    '        Else
    '            CtlCommon.ShowMessage(Me.Page, "خطأ أثناء الحذف")
    '        End If
    '    End If
    '    RaiseEvent FillTreeOrganization()
    'End Sub

    Protected Sub ibtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ibtnDelete.Click
        Dim err As Integer

        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If Not DisplayMode = DisplayModeEnum.ViewAddEdit Then
            objOrgCompany = New OrgCompany
            objOrgCompany.CompanyId = CompanyID
            err = objOrgCompany.Delete()

            If err = 0 Then
                clearall()
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            ElseIf err = 547 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("LowLevelDelete", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete", CultureInfo), "error")
            End If

        ElseIf DisplayMode = DisplayModeEnum.ViewAddEdit Then
            For Each row As GridDataItem In dgrdOrg_Company.SelectedItems
                Dim intcompanyIDId As Integer = Convert.ToInt32(row.GetDataKeyValue("CompanyId").ToString())
                objOrgCompany = New OrgCompany
                objOrgCompany.CompanyId = intcompanyIDId
                err = objOrgCompany.Delete()
            Next
            If err = 0 Then
                clearall()
                fillgridview()
                fillParent()
                fillgridLevel()
                fillgridWorkLocation()
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ComCouldnotDeleted", CultureInfo), "error")
            End If
        End If

        RaiseEvent FillTreeOrganization()




    End Sub

    Protected Sub dgrdOrg_Company_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdOrg_Company.NeedDataSource
        objOrgCompany = New OrgCompany
        dgrdOrg_Company.DataSource = objOrgCompany.GetAll
    End Sub

    Protected Sub dgrdOrg_Company_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdOrg_Company.SelectedIndexChanged
        If dgrdOrg_Company.SelectedItems.Count = 1 Then
            CompanyID = DirectCast(dgrdOrg_Company.SelectedItems(0), GridDataItem).GetDataKeyValue("CompanyId").ToString()
            fillParent()
            fillcontrolsforediting()
            ManageDeleteButton()
        End If
    End Sub

    'Protected Sub AsyncUpload1_FileUploaded(ByVal sender As Object, ByVal e As FileUploadedEventArgs)
    '    If AsyncUpload1.UploadedFiles.Count < 0 Then
    '        Exit Sub
    '    End If
    '    Thumbnail.Width = Unit.Pixel(200)
    '    Thumbnail.Height = Unit.Pixel(150)
    '    Dim fileStream As Stream = e.File.InputStream
    '    Dim imageData As Byte() = New Byte(fileStream.Length - 1) {}
    '    fileStream.Read(imageData, 0, CInt(fileStream.Length))
    '    Thumbnail.DataValue = imageData
    '    fileStream.Close()
    '    Thumbnail.Visible = True
    'End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Thumbnail.DataValue = New Byte(-1) {}
        Thumbnail.DataBind()
        Thumbnail.Visible = False
        lbtnRemoveLogo.Visible = False
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim err As Integer = -1
        Dim dr As DataRow
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Try
            If LevelID = 0 Then
                If ifexist(TextBoxLevelName.Text, TextBoxLevelNameArabic.Text) Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
                    'showResult(ProjectCommon.CodeResultMessage.CodeAlredyExist)
                    Exit Sub
                End If

                dr = Newdt.NewRow
                dr("FK_CompanyId") = CompanyID
                dr("LevelName") = TextBoxLevelName.Text
                dr("LevelArabicName") = TextBoxLevelNameArabic.Text
                dr("Enabledelete") = "True"
                Newdt.Rows.Add(dr)
                arrangeDeleteLinkForlevelGrid()

            Else
                If Newdt.Rows.Count > 0 Then
                    If ifexistwithId(TextBoxLevelName.Text, TextBoxLevelNameArabic.Text) Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ComNameExist", CultureInfo), "info")
                        fillgridLevel()
                        Exit Sub
                    End If
                    dr = Newdt.Select("LevelID= " & LevelID)(0)
                    dr("FK_CompanyId") = CompanyID
                    dr("LevelName") = TextBoxLevelName.Text
                    dr("LevelArabicName") = TextBoxLevelNameArabic.Text
                End If
            End If
            dgrdOrgLevel.DataSource = Newdt
            dgrdOrgLevel.DataBind()
            TextBoxLevelName.Text = ""
            TextBoxLevelNameArabic.Text = ""
            LevelID = 0
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("LevelAddSuccess", CultureInfo), "success")
            IsOrgLevelUpdate = True

        Catch ex As Exception
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorAdd", CultureInfo), "error")
        End Try
    End Sub

    Protected Sub lbtnRemoveLogo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnRemoveLogo.Click

        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim objOrgLevel As New OrgLevel
        Dim ERRNO As Integer = -1
        objOrgCompany = New OrgCompany
        Dim Ext As String = ""

        With objOrgCompany
            .Logo = ""
            .LAST_UPDATE_BY = SessionVariables.LoginUser.ID 'SessionVariables.LoginUser.fullName
            .CompanyId = CompanyID
            ERRNO = .UpdateLogo()

        End With
        If ERRNO = 0 Then
            If SessionVariables.CultureInfo = "en-US" Then
                ibtnSave.Text = "Update"
            Else
                ibtnSave.Text = "تحديث"
            End If

            Select Case DisplayMode.ToString
                Case "ViewAddEdit"
                    fillgridview()
            End Select
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            RaiseEvent FillTreeOrganization()

            If File.Exists(Server.MapPath(Thumbnail.ImageUrl)) Then
                File.Delete(Server.MapPath(Thumbnail.ImageUrl))

            End If

        ElseIf ERRNO = -11 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ComNameExist", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Protected Sub btnAddWork_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddWork.Click
        Dim err As Integer = -1
        Dim dr As DataRow
        objEmp_WorkLocation = New Emp_WorkLocation
        objEmp_WorkLocation_Beacon = New Emp_WorkLocation_Beacon
        Dim errorNum As Integer = -1
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        'Try
        If WorkLocationId = 0 Then
            If ifexistCodework(txtCode.Text, txtWorkLocationName.Text, txtWorkLocationArabic.Text) Then
                'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameorCodeExist", CultureInfo))
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
                Exit Sub
            ElseIf ifexistNamework(txtCode.Text, txtWorkLocationName.Text, txtWorkLocationArabic.Text) Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")
                Exit Sub
            End If
            dr = WorkLocationdt.NewRow
            dr("FK_CompanyId") = CompanyID
            dr("WorkLocationCode") = txtCode.Text.Trim
            dr("WorkLocationName") = txtWorkLocationName.Text.Trim
            dr("WorkLocationArabicName") = txtWorkLocationArabic.Text.Trim
            dr("FK_TAPolicyId") = ddlPolicy.SelectedValue
            dr("Active") = True 'chckActive.Checked
            dr("TAPolicyName") = ddlPolicy.SelectedItem.Text
            If HasMobile = True Then
                dr("HasMobilePunch") = chkHasMobilePunch.Checked
                If chkHasMobilePunch.Checked = True Then
                    dr("GPSCoordinates") = txtGPSCoordinates.Text
                    If Not txtRadius.Text = Nothing Then
                        dr("Radius") = txtRadius.Text
                    Else
                        dr("Radius") = DBNull.Value
                    End If

                    'dr("MustPunchPhysical") = chkMustPunchPhysical.Checked
                    dr("MustPunchPhysical") = If(rblPunchType.SelectedValue = 1, 1, 0)
                    If Not txtMobilePunchConsiderDuration.Text = Nothing Then
                        dr("MobilePunchConsiderDuration") = txtMobilePunchConsiderDuration.Text
                    Else
                        dr("MobilePunchConsiderDuration") = DBNull.Value
                    End If

                Else
                    dr("GPSCoordinates") = String.Empty
                    dr("Radius") = DBNull.Value
                    dr("MustPunchPhysical") = False
                    dr("MobilePunchConsiderDuration") = DBNull.Value
                End If
            Else
                dr("HasMobilePunch") = False
                dr("GPSCoordinates") = String.Empty
                dr("Radius") = DBNull.Value
                dr("MustPunchPhysical") = False
                dr("MobilePunchConsiderDuration") = DBNull.Value
            End If

            WorkLocationdt.Rows.Add(dr)
            With objEmp_WorkLocation
                .FK_CompanyId = CompanyID
                .WorkLocationCode = txtCode.Text.Trim
                .WorkLocationName = txtWorkLocationName.Text.Trim
                .WorkLocationArabicName = txtWorkLocationArabic.Text.Trim
                .FK_TAPolicyId = ddlPolicy.SelectedValue
                .Active = True
                .GPSCoordinates = txtGPSCoordinates.Text
                .CREATED_BY = SessionVariables.LoginUser.ID
                If Not txtRadius.Text = Nothing Then
                    .Radius = txtRadius.Text
                Else
                    .Radius = Nothing
                End If

                .HasMobilePunch = chkHasMobilePunch.Checked
                .MustPunchPhysical = If(rblPunchType.SelectedValue = 1, 1, 0)
                .mustPunchTwoTimes = If(rblPunchType.SelectedValue = 2, 1, 0)
                .SecondPunchRadius = If(rblPunchType.SelectedValue = 2, Convert.ToInt32(txtSecondPunch.Text), 0)
                .OutPunchRadius = If(rblPunchType.SelectedValue = 2, Convert.ToInt32(txtOutRadius.Text), 0)
                '.MustPunchPhysical = chkMustPunchPhysical.Checked
                If Not txtMobilePunchConsiderDuration.Text = Nothing Then
                    .MobilePunchConsiderDuration = txtMobilePunchConsiderDuration.Text
                Else
                    .MobilePunchConsiderDuration = Nothing
                End If

                If Not NoOfAllowedMobileWorkLocations = -1 Then
                    If chkHasMobilePunch.Checked = True Then
                        .AllowedMobileWorkLocation = NoOfAllowedMobileWorkLocations
                    Else
                        .AllowedMobileWorkLocation = Nothing
                    End If

                Else
                    .AllowedMobileWorkLocation = Nothing
                End If
                errorNum = objEmp_WorkLocation.Add()
            End With

            objEmp_WorkLocation_Beacon.FK_WorkLocationId = objEmp_WorkLocation.WorkLocationId
            If Not Beacondt Is Nothing AndAlso Beacondt.Rows.Count > 0 Then
                For Each dr2 As DataRow In Beacondt.Rows
                    With objEmp_WorkLocation_Beacon
                        .BeaconNo = dr2("BeaconNo")
                        .BeaconDesc = dr2("BeaconDesc")
                        .BeaconExpiryDate = dr2("BeaconExpiryDate")
                        .BeaconTransType = dr2("BeaconTransType")
                        .FK_WorkLocationId = objEmp_WorkLocation.WorkLocationId
                        .Add()
                    End With

                Next
            End If
            Dim errno As Integer = -1
            objEmp_WorkLocation_Beacon.FK_WorkLocationId = objEmp_WorkLocation.WorkLocationId
            If Not Beacondt Is Nothing AndAlso Beacondt.Rows.Count > 0 Then
                With objEmp_WorkLocation_Beacon
                    errno = .Add_Bulk(Beacondt)
                End With
            End If
        Else
            If WorkLocationdt.Rows.Count > 0 Then
                If ifexisCodeworkwithId(WorkLocationId, txtCode.Text, txtWorkLocationName.Text, txtWorkLocationArabic.Text) Then
                    'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameorCodeExist", CultureInfo))
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
                    Exit Sub
                ElseIf ifexisNametworkwithId(WorkLocationId, txtCode.Text, txtWorkLocationName.Text, txtWorkLocationArabic.Text) Then
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameExist", CultureInfo), "info")
                    Exit Sub
                End If

                dr = WorkLocationdt.Select("WorkLocationId= " & WorkLocationId)(0)
                dr("FK_CompanyId") = CompanyID
                dr("WorkLocationCode") = txtCode.Text.Trim
                dr("WorkLocationName") = txtWorkLocationName.Text.Trim
                dr("WorkLocationArabicName") = txtWorkLocationArabic.Text.Trim
                dr("FK_TAPolicyId") = ddlPolicy.SelectedValue
                dr("Active") = True 'chckActive.Checked
                dr("TAPolicyName") = ddlPolicy.SelectedItem.Text
                If HasMobile = True Then
                    dr("HasMobilePunch") = chkHasMobilePunch.Checked
                    If chkHasMobilePunch.Checked = True Then
                        dr("GPSCoordinates") = txtGPSCoordinates.Text
                        If Not txtRadius.Text = Nothing Then
                            dr("Radius") = txtRadius.Text
                        Else
                            dr("Radius") = DBNull.Value
                        End If
                        dr("MustPunchPhysical") = If(rblPunchType.SelectedValue = 1, 1, 0)
                        'dr("MustPunchPhysical") = chkMustPunchPhysical.Checked
                        If Not txtMobilePunchConsiderDuration.Text = Nothing Then
                            dr("MobilePunchConsiderDuration") = txtMobilePunchConsiderDuration.Text
                        Else
                            dr("MobilePunchConsiderDuration") = DBNull.Value
                        End If

                    Else
                        dr("GPSCoordinates") = String.Empty
                        dr("Radius") = DBNull.Value
                        dr("MustPunchPhysical") = False
                        dr("MobilePunchConsiderDuration") = DBNull.Value

                    End If
                Else
                    dr("HasMobilePunch") = False
                    dr("GPSCoordinates") = String.Empty
                    dr("Radius") = DBNull.Value
                    dr("MustPunchPhysical") = False
                    dr("MobilePunchConsiderDuration") = DBNull.Value
                End If


                objEmp_WorkLocation = New Emp_WorkLocation
                With objEmp_WorkLocation
                    .WorkLocationId = WorkLocationId
                    objEmp_WorkLocation = .GetByPK
                    .FK_CompanyId = CompanyID
                    .WorkLocationCode = txtCode.Text.Trim
                    .WorkLocationName = txtWorkLocationName.Text.Trim
                    .WorkLocationArabicName = txtWorkLocationArabic.Text.Trim
                    .FK_TAPolicyId = ddlPolicy.SelectedValue
                    .Active = True
                    .GPSCoordinates = txtGPSCoordinates.Text
                    If Not txtRadius.Text = Nothing Then
                        .Radius = txtRadius.Text
                    Else
                        .Radius = Nothing
                    End If
                    .MustPunchPhysical = If(rblPunchType.SelectedValue = 1, 1, 0)
                    .mustPunchTwoTimes = If(rblPunchType.SelectedValue = 2, 1, 0)
                    .SecondPunchRadius = If(rblPunchType.SelectedValue = 2, Convert.ToInt32(txtSecondPunch.Text), 0)
                    .OutPunchRadius = If(rblPunchType.SelectedValue = 2, Convert.ToInt32(txtOutRadius.Text), 0)
                    '.MustPunchPhysical = chkMustPunchPhysical.Checked
                    If Not txtMobilePunchConsiderDuration.Text = Nothing Then
                        .MobilePunchConsiderDuration = txtMobilePunchConsiderDuration.Text
                    Else
                        .MobilePunchConsiderDuration = Nothing
                    End If

                    .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                    .HasMobilePunch = chkHasMobilePunch.Checked
                    If Not NoOfAllowedMobileWorkLocations = -1 Then
                        .AllowedMobileWorkLocation = NoOfAllowedMobileWorkLocations
                    Else
                        .AllowedMobileWorkLocation = Nothing
                    End If
                    errorNum = objEmp_WorkLocation.Update()
                End With
            End If
        End If
        If errorNum = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("WorkLocAddSuccess", CultureInfo), "success")
            ClearWorkLocation()
            fillgridWorkLocation()
        ElseIf errorNum = -5 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CodeExist", CultureInfo), "info")
        ElseIf errorNum = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EnNameExist", CultureInfo), "info")
        ElseIf errorNum = -7 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ArNameExist", CultureInfo), "info")
        ElseIf errorNum = -8 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("MobileWorkLocationExceedLicense", CultureInfo), "info")
        End If

        'Catch ex As Exception
        '    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrAddWorkLoc", CultureInfo))
        ' End Try
    End Sub

    Protected Sub btnRemoveWork_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemoveWork.Click
        Dim strBuilder As New StringBuilder

        If WorkLocationId = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        Else
            Dim errorNum As Integer = -1
            objEmp_WorkLocation = New Emp_WorkLocation
            With objEmp_WorkLocation
                .WorkLocationId = WorkLocationId
                errorNum = .Delete()
                If errorNum = 0 Then
                    ClearWorkLocation()
                    fillgridWorkLocation()

                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
                Else
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
                End If
            End With


        End If
    End Sub

    Protected Sub dgrdOrgLevel_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles dgrdOrgLevel.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            If item.GetDataKeyValue("Enabledelete").ToString() = "False" Then
                Dim btn As LinkButton = DirectCast(item("column").Controls(0), LinkButton)
                btn.Visible = False
            End If
        End If
    End Sub

    Protected Sub dgrdOrgLevel_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdOrgLevel.NeedDataSource
        dgrdOrgLevel.DataSource = Newdt
    End Sub

    Protected Sub dgrdOrgLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdOrgLevel.SelectedIndexChanged
        Dim objOrgLevel As New OrgLevel
        Dim dt As DataTable = Newdt
        LevelID = CInt(CType(dgrdOrgLevel.SelectedItems(0), GridDataItem).GetDataKeyValue("LevelID").ToString())
        objOrgLevel.LevelId = LevelID
        TextBoxLevelName.Text = CType(dgrdOrgLevel.SelectedItems(0), GridDataItem).GetDataKeyValue("LevelName").ToString()  'Newdt.Rows("LevelName").ToString
        TextBoxLevelNameArabic.Text = CType(dgrdOrgLevel.SelectedItems(0), GridDataItem).GetDataKeyValue("LevelArabicName").ToString() 'Newdt.Rows("LevelArabicName").ToString
    End Sub

    Protected Sub dgrdWorkLocation_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgrdWorkLocation.NeedDataSource
        dgrdWorkLocation.DataSource = WorkLocationdt
    End Sub

    Protected Sub dgrdWorkLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgrdWorkLocation.SelectedIndexChanged
        If dgrdWorkLocation.SelectedItems.Count = 1 Then
            Dim objWorkLocation As New Emp_WorkLocation
            WorkLocationId = DirectCast(dgrdWorkLocation.SelectedItems(0), GridDataItem).GetDataKeyValue("WorkLocationId").ToString().Trim
            With objWorkLocation
                .WorkLocationId = WorkLocationId
                .GetByPK()
                txtCode.Text = .WorkLocationCode
                txtWorkLocationName.Text = .WorkLocationName
                txtWorkLocationArabic.Text = .WorkLocationArabicName
                ddlPolicy.SelectedValue = .FK_TAPolicyId
                If HasMobile = True Then
                    chkHasMobilePunch.Checked = .HasMobilePunch
                    If .HasMobilePunch = True Then
                        dvMobileControls.Visible = True
                        txtGPSCoordinates.Text = .GPSCoordinates
                        If .Radius <> 0 Then
                            txtRadius.Text = .Radius
                        Else
                            txtRadius.Text = Nothing
                        End If
                        If .MustPunchPhysical = "True" Then
                            rblPunchType.SelectedValue = 1
                            lblRadius.Text = "Allowed Radius"
                            dvMobilePunchConsiderDuration.Visible = True
                            dvMustPunchTwiceControls.Visible = False
                            txtMobilePunchConsiderDuration.Text = .MobilePunchConsiderDuration
                        ElseIf .mustPunchTwoTimes = "True" Then
                            rblPunchType.SelectedValue = 2
                            lblRadius.Text = "First Punch Radius"
                            dvMobilePunchConsiderDuration.Visible = True
                            dvMustPunchTwiceControls.Visible = True
                            txtSecondPunch.Text = .SecondPunchRadius.ToString()
                            txtOutRadius.Text = .OutPunchRadius.ToString()
                            txtMobilePunchConsiderDuration.Text = .MobilePunchConsiderDuration
                        Else
                            rblPunchType.SelectedValue = 0
                            lblRadius.Text = "Allowed Radius"
                            dvMobilePunchConsiderDuration.Visible = False
                            dvMustPunchTwiceControls.Visible = False
                        End If

                        'chkMustPunchPhysical.Checked = .MustPunchPhysical
                        'If .MustPunchPhysical = True Then
                        '    dvMobilePunchConsiderDuration.Visible = True
                        '    txtMobilePunchConsiderDuration.Text = .MobilePunchConsiderDuration
                        'Else
                        '    dvMobilePunchConsiderDuration.Visible = False
                        'End If

                    Else
                        dvMobileControls.Visible = False
                        txtGPSCoordinates.Text = String.Empty
                        txtRadius.Text = Nothing
                    End If
                        FillBeaconByWorkLocationId()
                    End If
            End With
        End If
    End Sub

    Protected Sub dgrdOrgLevel_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles dgrdOrgLevel.ItemCommand
        Try
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If e.CommandName = "Delete" Then
                Dim tmpID = CInt(DirectCast(e.Item, GridDataItem)("LevelId").Text.Trim)
                Dim objorglevel As New OrgLevel()
                With objorglevel
                    .LevelId = tmpID
                    .FK_CompanyId = CompanyID
                    If .CheckExistsInEntity() = 1 Then
                        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("RemoveReference", CultureInfo))
                        Exit Sub
                    End If

                    Newdt.Rows.Remove(Newdt.Select("LevelID = " & tmpID)(0))
                    arrangeDeleteLinkForlevelGrid()
                    dgrdOrgLevel.DataSource = Newdt
                    dgrdOrgLevel.DataBind()
                    LevelID = 0
                    TextBoxLevelName.Text = ""
                    TextBoxLevelNameArabic.Text = ""
                End With
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnClearLevels_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearLevels.Click
        TextBoxLevelName.Text = String.Empty
        TextBoxLevelNameArabic.Text = String.Empty
    End Sub

    Protected Sub btnClearWork_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearWork.Click
        txtCode.Text = String.Empty
        txtWorkLocationName.Text = String.Empty
        txtWorkLocationArabic.Text = String.Empty
        ddlPolicy.SelectedValue = -1
        dvMobileControls.Visible = False
        chkHasMobilePunch.Checked = False
        txtRadius.Text = String.Empty
        txtGPSCoordinates.Text = String.Empty
        txtBeaconNo.Text = String.Empty
        txtBeaconDesc.Text = String.Empty
        dtpBeaconExpirydate.DbSelectedDate = String.Empty
        radcmxBeaconType.SelectedValue = -1
        'chkMustPunchPhysical.Checked = False
        rblPunchType.ClearSelection()

        dvMobilePunchConsiderDuration.Visible = False
        txtMobilePunchConsiderDuration.Text = String.Empty
        BeaconId = 0
        WorkLocationId = 0
    End Sub

    Protected Sub btnRetrieve_Click(sender As Object, e As EventArgs) Handles btnRetrieve.Click
        objEmployee = New Employee
        objEmployee.FK_CompanyId = CompanyID
        objEmployee.EmployeeNo = txtEmpNo.Text
        objEmployee.UserId = SessionVariables.LoginUser.ID
        Dim dtEmployee As DataTable = objEmployee.GetEmpByEmpNo()
        If dtEmployee.Rows.Count > 0 Then
            ManagerId = dtEmployee.Rows(0)("EmployeeId")
            objEmployee.EmployeeId = ManagerId
            objEmployee.GetByPK()
            If SessionVariables.CultureInfo = "ar-JO" Then
                lblManager.Text = objEmployee.EmployeeArabicName
            Else
                lblManager.Text = objEmployee.EmployeeName
            End If
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("EmpNonotinlevel", CultureInfo))
            lblManager.Text = String.Empty
            ManagerId = 0
            txtEmpNo.Text = String.Empty
        End If
    End Sub
    Protected Sub rblPunchType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblPunchType.SelectedIndexChanged

        If rblPunchType.SelectedValue = 1 Then
            lblRadius.Text = "Allowed Radius"
            dvMobilePunchConsiderDuration.Visible = True
            dvMustPunchTwiceControls.Visible = False
            txtSecondPunch.Text = ""
            txtOutRadius.Text = ""

        ElseIf rblPunchType.SelectedValue = 2 Then
            lblRadius.Text = "First Punch Radius"
            dvMobilePunchConsiderDuration.Visible = True
            dvMustPunchTwiceControls.Visible = True
            'dvMobileControls.Visible = False
        Else
            lblRadius.Text = "Allowed Radius"
            dvMobilePunchConsiderDuration.Visible = False
            dvMustPunchTwiceControls.Visible = False
            txtSecondPunch.Text = ""
            txtOutRadius.Text = ""
            txtMobilePunchConsiderDuration.Text = ""
        End If
    End Sub
    Protected Sub chkHasMobilePunch_CheckedChanged(sender As Object, e As EventArgs) Handles chkHasMobilePunch.CheckedChanged
        If chkHasMobilePunch.Checked = True Then
            dvMobileControls.Visible = True
        Else
            dvMobileControls.Visible = False
        End If
    End Sub
    'Protected Sub chkMustPunchTwice_CheckedChanged(sender As Object, e As EventArgs) Handles chkMustPunchTwice.CheckedChanged
    '    If chkMustPunchTwice.Checked = True Then
    '        dvMustPunchTwiceControls.Visible = True
    '    Else
    '        dvMustPunchTwiceControls.Visible = False
    '    End If
    'End Sub

    Protected Sub dgrdBeacon_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgrdBeacon.ItemDataBound
        If (e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem) Then
            objEmp_WorkLocation_Beacon = New Emp_WorkLocation_Beacon
            Dim item As GridDataItem = CType(e.Item, GridDataItem)
            item = e.Item
            If item("BeaconTransType").Text = "I" Then
                item("BeaconTransType").Text = "In"
            Else
                item("BeaconTransType").Text = "Out"
            End If
        End If

    End Sub

    Protected Sub dgrdBeacon_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgrdBeacon.NeedDataSource
        objEmp_WorkLocation_Beacon = New Emp_WorkLocation_Beacon
        With objEmp_WorkLocation_Beacon
            .FK_WorkLocationId = WorkLocationId
            Beacondt = .GetByFK_WorkLocationId()
            dgrdBeacon.DataSource = Beacondt
        End With
    End Sub

    Protected Sub btnRemoveBeacon_Click(sender As Object, e As EventArgs) Handles btnRemoveBeacon.Click
        Dim strBuilder As New StringBuilder
        objEmp_WorkLocation_Beacon = New Emp_WorkLocation_Beacon
        Dim err As Integer = -1
        Dim chkCount As Integer = 0
        For Each row As GridDataItem In dgrdBeacon.Items
            If DirectCast(row.FindControl("chk"), CheckBox).Checked Then
                chkCount = chkCount + 1
                Dim intBeaconId As Integer = Convert.ToInt32(row.GetDataKeyValue("BeaconId").ToString())
                objEmp_WorkLocation_Beacon.BeaconId = intBeaconId
                err = objEmp_WorkLocation_Beacon.Delete()
                With strBuilder
                End With
            End If
        Next
        If chkCount = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("PleaseSelectFromList", CultureInfo), "info")
            Return
        End If
        If err = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully", CultureInfo), "success")
            FillBeaconByWorkLocationId()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldnotDeleteRecord", CultureInfo), "error")
        End If
    End Sub

    Protected Sub btnAddBeacon_Click(sender As Object, e As EventArgs) Handles btnAddBeacon.Click
        Dim err As Integer = -1
        Dim dr As DataRow
        objEmp_WorkLocation_Beacon = New Emp_WorkLocation_Beacon
        Dim errorNum As Integer = -1
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)

        If BeaconId = 0 Then
            If ifexistBeaconNo(txtBeaconNo.Text) Then
                'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameorCodeExist", CultureInfo))
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("BeaconNoExist", CultureInfo), "info")
                Exit Sub
            End If
            dr = Beacondt.NewRow
            dr("BeaconNo") = txtBeaconNo.Text
            dr("BeaconDesc") = txtBeaconDesc.Text
            dr("BeaconExpiryDate") = dtpBeaconExpirydate.DbSelectedDate
            dr("BeaconTransType") = radcmxBeaconType.SelectedValue
            dr("FK_WorkLocationId") = WorkLocationId

            Beacondt.Rows.Add(dr)
            With objEmp_WorkLocation_Beacon
                .BeaconNo = txtBeaconNo.Text
                .BeaconDesc = txtBeaconDesc.Text
                .BeaconExpiryDate = dtpBeaconExpirydate.DbSelectedDate
                .BeaconTransType = radcmxBeaconType.SelectedValue
                .FK_WorkLocationId = WorkLocationId

                'errorNum = objEmp_WorkLocation_Beacon.Add()
                dgrdBeacon.DataSource = Beacondt

            End With

        Else
            If Beacondt.Rows.Count > 0 Then
                If ifexistBeaconNo(txtBeaconNo.Text) Then
                    'CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("NameorCodeExist", CultureInfo))
                    CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("BeaconNoExist", CultureInfo), "info")
                    Exit Sub
                End If

                dr = WorkLocationdt.Select("BeaconId= " & BeaconId)(0)
                dr = Beacondt.NewRow
                dr("BeaconNo") = txtBeaconNo.Text
                dr("BeaconDesc") = txtBeaconDesc.Text
                dr("BeaconExpiryDate") = dtpBeaconExpirydate.DbSelectedDate
                dr("BeaconTransType") = radcmxBeaconType.SelectedValue
                dr("FK_WorkLocationId") = WorkLocationId


                objEmp_WorkLocation_Beacon = New Emp_WorkLocation_Beacon
                With objEmp_WorkLocation_Beacon
                    .BeaconId = BeaconId
                    objEmp_WorkLocation_Beacon = .GetByPK
                    .BeaconNo = txtBeaconNo.Text
                    .BeaconDesc = txtBeaconDesc.Text
                    .BeaconExpiryDate = dtpBeaconExpirydate.DbSelectedDate
                    .BeaconTransType = radcmxBeaconType.SelectedValue
                    .FK_WorkLocationId = WorkLocationId

                    errorNum = objEmp_WorkLocation_Beacon.Update()
                End With
            End If
            If errorNum = 0 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
                FillBeaconByWorkLocationId()
            ElseIf errorNum = -5 Then
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("BeaconNoExist", CultureInfo), "info")
            Else
                CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
            End If
        End If
    End Sub

#End Region

#Region "Methods"

    Public Sub Companypageload()
        If Not Page.IsPostBack Then
            CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
            If (SessionVariables.CultureInfo Is Nothing) Then
                Response.Redirect("~/default/Login.aspx")
            End If
            PageHeader1.HeaderText = ResourceManager.GetString("OrgCompany", CultureInfo)
            filldropdowns()
            fillgridview()
            ManageFunctionalities()
            fillParent()
            fillgridLevel()
            fillgridWorkLocation()
        End If
        If checkBxHasParent.Checked = True Then
            lblComanyParent.Visible = True
            ddlBxParentCompany.Visible = True
            RequiredFieldValidator6.Enabled = True
        Else
            lblComanyParent.Visible = False
            ddlBxParentCompany.Visible = False
            RequiredFieldValidator6.Enabled = False
        End If
        If Location = Locations.TreeOrgn Then
            checkBxHasParent.Visible = False
            PageHeader1.Visible = False
            lblHasParent.Visible = False
        End If
        ibtnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdOrg_Company.ClientID + "')")
        btnRemoveWork.Attributes.Add("onclick", "javascript:return confirmDelete('" + dgrdWorkLocation.ClientID + "')")
    End Sub

    Sub ManageDeleteButton()
        objOrgCompany = New OrgCompany
        With objOrgCompany
            .CompanyId = CompanyID
            If Not .CompanyId > 0 Then
                Exit Sub
            End If
            Select Case .CheckChildOREntitytExistsForCompany
                Case 1
                    ibtnDelete.Visible = False
                Case 0
                    ibtnDelete.Visible = True
                Case Else

            End Select

        End With
    End Sub

    Sub Adddisplaymode()
        RefreshControls(True)
        dgrdOrg_Company.Visible = False
        ibtnDelete.Visible = False
        ibtnRest.Visible = False
        'ibtnDeleteCompany.Visible = False
        ReqHighestPost.Enabled = False
    End Sub

    Sub Viewdisplaymode()
        RefreshControls(True)
        ManageControls(False)
        ibtnDelete.Visible = False
        'ibtnDeleteCompany.Visible = False
        ibtnRest.Visible = False
        ibtnSave.Visible = False
        fillcontrolsforediting()
        dgrdOrg_Company.Visible = False

    End Sub

    Sub ViewAllDisplaymode()
        RefreshControls(True)
        ManageControls(False)
        'ibtnDeleteCompany.Visible = False
        ibtnDelete.Visible = False
        ibtnRest.Visible = False
        ibtnSave.Visible = False
        ibtnDelete.Enabled = False


    End Sub

    Sub ViewEditmode()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        RefreshControls(True)
        ibtnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
        'ibtnSave.Text = "Update"
        fillcontrolsforediting()
        dgrdOrg_Company.Visible = False
        ibtnRest.Visible = False

        'ibtnDeleteCompany.Visible = True
        ReqHighestPost.Enabled = False

        ManageDeleteButton()
    End Sub

    Sub ViewAddEditDisplaymode()
        RefreshControls(True)
        ReqHighestPost.Enabled = True
        objOrgCompany = New OrgCompany
        ManageDeleteButton()
    End Sub

    Sub ManageControls(ByVal Status As Boolean)
        txtAddress.Enabled = Status
        txtCompanyArName.Enabled = Status
        txtCompanyEnName.Enabled = Status
        txtCompanyShortName.Enabled = Status
        txtFax.Enabled = Status
        'AsyncUpload1.Enabled = Status
        txtPhoneNo.Enabled = Status
        txtURL.Enabled = Status
        ddlBxParentCompany.Enabled = Status
        ddlDefaultPolicy.Enabled = Status
        ddlHighestPost.Enabled = Status
        checkBxHasParent.Enabled = Status
    End Sub

    Sub RefreshControls(ByVal Status As Boolean)
        txtAddress.Enabled = Status
        txtCompanyArName.Enabled = Status
        txtCompanyEnName.Enabled = Status
        txtCompanyShortName.Enabled = Status
        txtFax.Enabled = Status
        txtPhoneNo.Enabled = Status
        txtURL.Enabled = Status
        ddlBxParentCompany.Enabled = Status
        ddlDefaultPolicy.Enabled = Status
        ddlHighestPost.Enabled = Status
        checkBxHasParent.Enabled = Status
        ibtnSave.Text = "Save"

        If AllowDelete AndAlso Status = True Then
            ibtnDelete.Visible = Status
        ElseIf Status = False Then
            ibtnDelete.Visible = Status
        End If
        'ibtnDeleteCompany.Visible = Status
        ibtnRest.Visible = Status

        If AllowSave AndAlso Status = True Then
            ibtnSave.Visible = Status
        ElseIf Status = False Then
            ibtnSave.Visible = Status
        End If

        ibtnDelete.Enabled = Status
        'ibtnDeleteCompany.Enabled = Status
        ibtnRest.Enabled = Status
        ibtnSave.Enabled = Status
        dgrdOrg_Company.Visible = Status
        For Each row As GridDataItem In dgrdOrg_Company.Items
            row.Enabled = Status
        Next
        TabContainer1.ActiveTabIndex = 0

    End Sub

    Sub ManageFunctionalities()
        Select Case DisplayMode.ToString
            Case "Add"
                Adddisplaymode()
                CompanyID = 0
            Case "Edit"
                ViewEditmode()
            Case "View"
                Viewdisplaymode()
            Case "ViewAll"
                ViewAllDisplaymode()
                CompanyID = 0
            Case "ViewAddEdit"
                ViewAddEditDisplaymode()
                CompanyID = 0
            Case Else
        End Select
    End Sub

    Sub fillgridview()
        objOrgCompany = New OrgCompany
        dgrdOrg_Company.DataSource = objOrgCompany.GetAll
        dgrdOrg_Company.DataBind()
    End Sub

    Sub fillgridLevel()
        If CompanyID > 0 Then
            Dim objOrgLevel As New OrgLevel
            objOrgLevel.FK_CompanyId = CompanyID
            Newdt = objOrgLevel.GetAllByComapany
            Newdt.Columns("LevelId").AutoIncrement = True
            Newdt.Columns("LevelId").AutoIncrementSeed = 1
            Newdt.Columns("LevelId").AutoIncrementStep = 1
            dgrdOrgLevel.DataSource = Newdt
            dgrdOrgLevel.DataBind()
        Else
            CreateLevelDT()
            dgrdOrgLevel.DataSource = Newdt
            dgrdOrgLevel.DataBind()
        End If
    End Sub

    Sub fillgridWorkLocation()
        If CompanyID > 0 Then
            Dim objWorkLocation As New Emp_WorkLocation
            objWorkLocation.FK_CompanyId = CompanyID
            WorkLocationdt = objWorkLocation.GetAllWorkGrid
            WorkLocationdt.Columns("WorkLocationId").AutoIncrement = True
            WorkLocationdt.Columns("WorkLocationId").AutoIncrementSeed = 1
            WorkLocationdt.Columns("WorkLocationId").AutoIncrementStep = 1
            dgrdWorkLocation.DataSource = WorkLocationdt
            dgrdWorkLocation.DataBind()
        Else
            CreateWorkLocationDT()
            dgrdWorkLocation.DataSource = WorkLocationdt
            dgrdWorkLocation.DataBind()
        End If
    End Sub

    Sub filldropdowns()
        ProjectCommon.FillRadComboBox(ddlDefaultPolicy, objTAPolicy.GetAll, "TAPolicyName", "TAPolicyArabicName", "TAPolicyId", False)
        ProjectCommon.FillRadComboBox(ddlHighestPost, objEmp_Designation.GetAll, "DesignationName", "DesignationArabicName", "DesignationId", False)
        ProjectCommon.FillRadComboBox(ddlPolicy, objTAPolicy.GetAll, "TAPolicyName", "TAPolicyArabicName", "TAPolicyId", False)
    End Sub

    Sub fillParent()
        objOrgCompany = New OrgCompany
        Dim dt As DataTable = objOrgCompany.GetAllforddl
        'objOrgCompany.FK_ParentId = CompanyID
        'Dim dtChilds As DataTable = objOrgCompany.GetAllchildsByParent
        CtlCommon.FillTelerikDropDownList(ddlBxParentCompany, dt)
        If CompanyID > 0 Then
            ddlBxParentCompany.Items.Remove(ddlBxParentCompany.Items.FindItemByValue(CompanyID))
            FilterParentddl()
        End If
    End Sub

    Sub FilterParentddl()
        objOrgCompany = New OrgCompany
        Dim dt As New DataTable
        dt = objOrgCompany.GetAllforddl()
        dtForParent = dt.Clone
        getChilds(CompanyID)
        If dtForParent IsNot Nothing Then
            If dtForParent.Rows.Count > 0 Then
                For Each dr As DataRow In dtForParent.Rows
                    ddlBxParentCompany.Items.Remove(ddlBxParentCompany.Items.FindItemByValue(dr("CompanyId")))
                Next
            End If
        End If
    End Sub

    Sub getChilds(ByVal intCompanyID As Integer)
        objOrgCompany = New OrgCompany
        Dim dt As New DataTable
        dt = objOrgCompany.GetAllforddl()
        If Not dt Is Nothing And dt.Rows.Count > 0 Then
            Dim ChildClasses() As DataRow = dt.Select("FK_ParentId =" & intCompanyID)
            For Each r As DataRow In ChildClasses
                dtForParent.ImportRow(r)
                getChilds(r("CompanyId"))
            Next
        End If
    End Sub

    Sub fillcontrolsforediting()
        objOrgCompany = New OrgCompany
        objEmployee = New Employee
        If CompanyID > 0 Then
            With objOrgCompany
                .CompanyId = CompanyID
                .GetByPK()
                txtAddress.Text = .Address
                txtCompanyArName.Text = .CompanyArabicName
                txtCompanyEnName.Text = .CompanyName
                txtCompanyShortName.Text = .CompanyShortName
                objEmployee.EmployeeId = .FK_ManagerId
                objEmployee.GetByPK()
                If SessionVariables.CultureInfo = "ar-JO" Then
                    lblManager.Text = objEmployee.EmployeeArabicName
                Else
                    lblManager.Text = objEmployee.EmployeeName
                End If
                Thumbnail.Visible = False
                lbtnRemoveLogo.Visible = False
                If File.Exists(Server.MapPath("~\images\") & "\CompaniesLogo\" & .CompanyId & .Logo) Then
                    Thumbnail.ImageUrl = "~\images" & "\CompaniesLogo\" & .CompanyId & .Logo
                    Thumbnail.Visible = True
                    lbtnRemoveLogo.Visible = True
                End If
                txtPhoneNo.Text = .PhoneNumber
                txtURL.Text = .URL
                txtFax.Text = .Fax
                ddlDefaultPolicy.SelectedValue = .FK_DefaultPolicyId
                ddlPolicy.SelectedValue = .FK_DefaultPolicyId
                ddlHighestPost.SelectedValue = .FK_HighestPost
                fillgridLevel()
                fillgridWorkLocation()
                If Not IsDBNull(.FK_ParentId) And .FK_ParentId <> 0 Then
                    checkBxHasParent.Checked = True
                    lblComanyParent.Visible = True
                    ddlBxParentCompany.Visible = True
                    ddlBxParentCompany.SelectedValue = .FK_ParentId
                    RequiredFieldValidator6.Enabled = True
                Else
                    checkBxHasParent.Checked = False
                    lblComanyParent.Visible = False
                    ddlBxParentCompany.Visible = False
                    ddlBxParentCompany.SelectedValue = -1
                    RequiredFieldValidator6.Enabled = False
                End If
            End With
        Else
            dgrdOrgLevel.DataSource = Nothing
            dgrdOrgLevel.DataBind()
            dgrdWorkLocation.DataSource = Nothing
            dgrdWorkLocation.DataBind()
        End If
    End Sub

    Sub SaveUpdate()
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim objOrgLevel As New OrgLevel
        Dim objWorkLocation As New Emp_WorkLocation


        If Not Newdt.Rows.Count > 0 Then
            CtlCommon.ShowMessage(Page, ResourceManager.GetString("Addatleastonelevel", CultureInfo), "info")
            Exit Sub
        End If
        Dim ERRNO As Integer = -1
        objOrgCompany = New OrgCompany
        Dim Ext As String = ""
        With objOrgCompany
            .Address = txtAddress.Text
            .CompanyArabicName = txtCompanyArName.Text.Trim()
            .CompanyName = txtCompanyEnName.Text.Trim()
            .CompanyShortName = txtCompanyShortName.Text.Trim()
            .Country = 1
            .CREATED_BY = SessionVariables.LoginUser.ID 'SessionVariables.LoginUser.fullName
            .CREATED_DATE = Now
            .LAST_UPDATE_DATE = Now

            If rdUpldProDocuments.UploadedFiles.Count > 0 Then
                Ext = New System.IO.FileInfo(rdUpldProDocuments.UploadedFiles(0).FileName).Extension
                .Logo = Ext
            Else
                .Logo = ""
            End If
            .PhoneNumber = txtPhoneNo.Text
            .URL = txtURL.Text
            .Fax = txtFax.Text
            .FK_DefaultPolicyId = ddlDefaultPolicy.SelectedValue
            If Not ddlHighestPost.SelectedValue = -1 Then
                .FK_HighestPost = ddlHighestPost.SelectedValue
            Else
                .FK_HighestPost = Nothing
            End If

            .FK_ManagerId = ManagerId
            If checkBxHasParent.Checked = True Then
                .FK_ParentId = ddlBxParentCompany.SelectedValue
            Else
                .FK_ParentId = 0
            End If

            If CompanyID = 0 Then
                .CompanyId = 0
                .LAST_UPDATE_BY = ""
                ERRNO = .Add()
                If ERRNO = 0 Then
                    CompanyID = .CompanyId
                    objOrgLevel.FK_CompanyId = .CompanyId
                    If Not Newdt Is Nothing AndAlso Newdt.Rows.Count > 0 Then
                        For Each dr As DataRow In Newdt.Rows
                            With objOrgLevel
                                .LevelName = dr("LevelName")
                                .LevelArabicName = dr("LevelArabicName")
                                .Add()
                            End With
                        Next
                    End If
                    objWorkLocation.FK_CompanyId = .CompanyId
                    If Not WorkLocationdt Is Nothing AndAlso WorkLocationdt.Rows.Count > 0 Then
                        For Each dr As DataRow In WorkLocationdt.Rows
                            With objWorkLocation

                                .WorkLocationCode = dr("WorkLocationCode")
                                .WorkLocationName = dr("WorkLocationName")
                                .WorkLocationArabicName = dr("WorkLocationArabicName")
                                .FK_TAPolicyId = dr("FK_TAPolicyId")
                                .Active = True 'dr("Active")
                                .GPSCoordinates = dr("GPSCoordinates")
                                .CREATED_BY = SessionVariables.LoginUser.ID
                                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID
                                .Radius = dr("Radius")
                                .HasMobilePunch = dr("HasMobilePunch")
                                .Add()
                            End With
                        Next
                    End If



                    If rdUpldProDocuments.UploadedFiles.Count > 0 Then
                        Ext = New System.IO.FileInfo(rdUpldProDocuments.UploadedFiles(0).FileName).Extension
                        .Logo = Ext

                        If Not (File.Exists(Server.MapPath("~\images") & "\CompaniesLogo\")) Then
                            Directory.CreateDirectory(Server.MapPath("~\images") & "\CompaniesLogo\")
                        End If
                        rdUpldProDocuments.UploadedFiles(0).SaveAs(Server.MapPath("~\images") & "\CompaniesLogo\" & CompanyID & Ext)
                    End If
                    Select Case DisplayMode.ToString
                        Case "Add"
                            fillParent()
                            ddlBxParentCompany.SelectedValue = .FK_ParentId
                        Case Else
                            clearall()
                            fillParent()
                    End Select
                End If
            Else
                .LAST_UPDATE_BY = SessionVariables.LoginUser.ID 'SessionVariables.LoginUser.fullName
                .CompanyId = CompanyID
                ERRNO = .Update()
                If ERRNO = 0 Then

                    objOrgLevel.FK_CompanyId = .CompanyId
                    If Not Newdt Is Nothing AndAlso Newdt.Rows.Count > 0 Then
                        If IsOrgLevelUpdate Then
                            With objOrgLevel
                                ERRNO = .Add_Bulk(Newdt)
                            End With
                        End If
                    End If
                    objWorkLocation.FK_CompanyId = .CompanyId

                    If Not WorkLocationdt Is Nothing AndAlso WorkLocationdt.Rows.Count > 0 Then
                        If IsOrgWorkLocationUpdate Then
                            With objWorkLocation
                                ERRNO = .Add_Bulk(WorkLocationdt)
                            End With
                        End If
                    End If



                    If rdUpldProDocuments.UploadedFiles.Count > 0 Then
                        Ext = New System.IO.FileInfo(rdUpldProDocuments.UploadedFiles(0).FileName).Extension
                        .Logo = Ext

                        If Not (File.Exists(Server.MapPath("~\images") & "\CompaniesLogo\")) Then
                            Directory.CreateDirectory(Server.MapPath("~\images") & "\CompaniesLogo\")
                        End If
                        rdUpldProDocuments.UploadedFiles(0).SaveAs(Server.MapPath("~\images") & "\CompaniesLogo\" & CompanyID & Ext, True)
                    End If
                    fillParent()
                    ddlBxParentCompany.SelectedValue = .FK_ParentId
                End If
            End If
        End With
        If ERRNO = 0 Then
            ibtnSave.Text = ResourceManager.GetString("btnUpdate", CultureInfo)
            'ibtnSave.Text = "Update"
            Select Case DisplayMode.ToString
                Case "ViewAddEdit"
                    fillgridview()
            End Select
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
            RaiseEvent FillTreeOrganization()
        ElseIf ERRNO = -11 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ComNameExist", CultureInfo), "info")
        ElseIf ERRNO = -6 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("CouldNotDeleteWorkLocation", CultureInfo), "info")
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorSave", CultureInfo), "error")
        End If
    End Sub

    Sub clearall()
        ddlBxParentCompany.SelectedIndex = -1
        ddlDefaultPolicy.SelectedIndex = -1
        ddlHighestPost.SelectedIndex = -1
        txtAddress.Text = ""
        txtCompanyArName.Text = ""
        txtCompanyEnName.Text = ""
        txtCompanyShortName.Text = ""
        txtFax.Text = ""
        Thumbnail.Visible = False
        lbtnRemoveLogo.Visible = False
        txtPhoneNo.Text = ""
        txtURL.Text = ""
        CompanyID = 0
        fillParent()
        ClearLevel()
        TabContainer1.ActiveTabIndex = 0
        ClearWorkLocation()
        IsOrgLevelUpdate = False
        IsOrgWorkLocationUpdate = False
    End Sub

    Public Sub ClearLevel()
        TextBoxLevelName.Text = ""
        TextBoxLevelNameArabic.Text = ""
        CreateLevelDT()
        dgrdOrgLevel.DataSource = Newdt
        dgrdOrgLevel.DataBind()
        LevelID = 0
    End Sub

    Public Sub ClearWorkLocation()
        CreateWorkLocationDT()
        WorkLocationId = 0
        dgrdWorkLocation.DataSource = WorkLocationdt
        dgrdWorkLocation.DataBind()
        txtWorkLocationArabic.Text = ""
        txtWorkLocationName.Text = ""
        txtCode.Text = ""
        ddlPolicy.SelectedIndex = -1
        dvMobileControls.Visible = False
        chkHasMobilePunch.Checked = False
        txtRadius.Text = Nothing
        txtGPSCoordinates.Text = ""
        txtSecondPunch.Text = ""
        txtOutRadius.Text = ""
        'chckActive.Checked = False
    End Sub

    Public Sub ManagedByTree(ByVal Type As String, ByVal intParent As Integer)
        Select Case Type
            Case "amc"
                checkBxHasParent.Checked = False
                checkBxHasParent.Enabled = False
                lblComanyParent.Visible = False
                ddlBxParentCompany.Enabled = False
                ddlBxParentCompany.Visible = False
                lblComanyParent.Visible = False
                ClearSubTabs()
            Case "acc"
                checkBxHasParent.Checked = True
                checkBxHasParent.Enabled = False
                ddlBxParentCompany.Visible = True
                fillParent()
                ddlBxParentCompany.SelectedValue = intParent
                lblComanyParent.Visible = True
                ddlBxParentCompany.Enabled = False
                ClearSubTabs()
            Case "mcm"
                ViewEditmode()
                ClearSubTabs()
                checkBxHasParent.Enabled = False
                ddlBxParentCompany.Enabled = False
        End Select
    End Sub

    Sub ClearSubTabs()
        txtWorkLocationArabic.Text = String.Empty
        txtWorkLocationName.Text = String.Empty
        txtCode.Text = String.Empty
        ' ddlPolicy.SelectedValue = -1
        'chckActive.Checked = False
        TextBoxLevelName.Text = String.Empty
        TextBoxLevelNameArabic.Text = String.Empty
        txtGPSCoordinates.Text = String.Empty
    End Sub

    Function ifexist(ByVal LevelName As String, ByVal LevelArabicName As String) As Boolean
        If Not Newdt Is Nothing AndAlso Newdt.Rows.Count > 0 Then
            For Each i In Newdt.Rows
                If i("LevelName") = LevelName Or i("LevelArabicName") = LevelArabicName Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Function ifexistwithId(ByVal LevelName As String, ByVal LevelArabicName As String) As Boolean
        If Not Newdt Is Nothing AndAlso Newdt.Rows.Count > 0 Then
            For Each i In Newdt.Rows
                If (i("LevelName") = LevelName Or i("LevelArabicName") = LevelArabicName) And (i("LevelID") <> LevelID) Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Function ifexistCodework(ByVal WorkLocationCode As String, ByVal WorkLocationName As String, ByVal WorkLocationArabicName As String) As Boolean
        If Not WorkLocationdt Is Nothing AndAlso WorkLocationdt.Rows.Count > 0 Then
            For Each i In WorkLocationdt.Rows
                If i("WorkLocationCode") = WorkLocationCode Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Function ifexistNamework(ByVal WorkLocationCode As String, ByVal WorkLocationName As String, ByVal WorkLocationArabicName As String) As Boolean
        If Not WorkLocationdt Is Nothing AndAlso WorkLocationdt.Rows.Count > 0 Then
            For Each i In WorkLocationdt.Rows
                If i("WorkLocationName") = WorkLocationName Or i("WorkLocationArabicName") = WorkLocationArabicName Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Function ifexisCodeworkwithId(ByVal WorkLocationId As Integer, ByVal WorkLocationCode As String, ByVal WorkLocationName As String, ByVal WorkLocationArabicName As String) As Boolean
        If Not WorkLocationdt Is Nothing AndAlso WorkLocationdt.Rows.Count > 0 Then
            For Each i In WorkLocationdt.Rows
                If (i("WorkLocationCode") = WorkLocationCode) And (i("WorkLocationId") <> WorkLocationId) Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Function ifexisNametworkwithId(ByVal WorkLocationId As Integer, ByVal WorkLocationCode As String, ByVal WorkLocationName As String, ByVal WorkLocationArabicName As String) As Boolean
        If Not WorkLocationdt Is Nothing AndAlso WorkLocationdt.Rows.Count > 0 Then
            For Each i In WorkLocationdt.Rows
                If (i("WorkLocationName") = WorkLocationName Or i("WorkLocationArabicName") = WorkLocationArabicName) And (i("WorkLocationId") <> WorkLocationId) Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Sub CreateLevelDT()
        Newdt = New DataTable
        Dim dc As DataColumn
        dc = New DataColumn
        dc.ColumnName = "LevelId"
        dc.DataType = GetType(Integer)
        Newdt.Columns.Add(dc)
        Newdt.Columns("LevelId").AutoIncrement = True
        Newdt.Columns("LevelId").AutoIncrementSeed = 1
        Newdt.Columns("LevelId").AutoIncrementStep = 1
        dc = New DataColumn
        dc.ColumnName = "FK_CompanyId"
        dc.DataType = GetType(Integer)
        Newdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "LevelName"
        dc.DataType = GetType(String)
        Newdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "LevelArabicName"
        dc.DataType = GetType(String)
        Newdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "Enabledelete"
        dc.DataType = GetType(String)
        Newdt.Columns.Add(dc)
    End Sub

    Sub arrangeDeleteLinkForlevelGrid()
        'If Newdt.Rows.Count = 1 Then
        '    Newdt.Rows(0)("Enabledelete") = "True"
        'End If
        If Newdt.Rows.Count > 0 Then
            For i = 0 To Newdt.Rows.Count - 1
                Newdt.Rows(i)("Enabledelete") = "False"
            Next
            Newdt.Rows(Newdt.Rows.Count - 1)("Enabledelete") = "True"
        End If
    End Sub

    Sub CreateWorkLocationDT()
        WorkLocationdt = New DataTable
        Dim dc As DataColumn
        dc = New DataColumn
        dc.ColumnName = "WorkLocationId"
        dc.DataType = GetType(Integer)
        WorkLocationdt.Columns.Add(dc)
        WorkLocationdt.Columns("WorkLocationId").AutoIncrement = True
        WorkLocationdt.Columns("WorkLocationId").AutoIncrementSeed = 1
        WorkLocationdt.Columns("WorkLocationId").AutoIncrementStep = 1
        dc = New DataColumn
        dc.ColumnName = "FK_CompanyId"
        dc.DataType = GetType(Integer)
        WorkLocationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "WorkLocationCode"
        dc.DataType = GetType(String)
        WorkLocationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "WorkLocationName"
        dc.DataType = GetType(String)
        WorkLocationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "WorkLocationArabicName"
        dc.DataType = GetType(String)
        WorkLocationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "FK_TAPolicyId"
        dc.DataType = GetType(Integer)
        WorkLocationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "Active"
        dc.DataType = GetType(Boolean)
        WorkLocationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "TAPolicyName"
        dc.DataType = GetType(String)
        WorkLocationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "GPSCoordinates"
        dc.DataType = GetType(String)
        WorkLocationdt.Columns.Add(dc)
        dc = New DataColumn
        dc.ColumnName = "Radius"
        dc.DataType = GetType(String)
        WorkLocationdt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "MustPunchPhysical"
        dc.DataType = GetType(Boolean)
        WorkLocationdt.Columns.Add(dc)

        dc = New DataColumn
        dc.ColumnName = "MobilePunchConsiderDuration"
        dc.DataType = GetType(String)
        WorkLocationdt.Columns.Add(dc)

    End Sub

    Function ifexistBeaconNo(ByVal BeaconNo As String) As Boolean
        If Not Beacondt Is Nothing AndAlso Beacondt.Rows.Count > 0 Then
            For Each i In Beacondt.Rows
                If i("BeaconNo") = BeaconNo Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Private Sub FillBeaconByWorkLocationId()
        objEmp_WorkLocation_Beacon = New Emp_WorkLocation_Beacon
        With objEmp_WorkLocation_Beacon
            .FK_WorkLocationId = WorkLocationId
            Beacondt = .GetByFK_WorkLocationId()
            dgrdBeacon.DataSource = Beacondt
            dgrdBeacon.DataBind()
        End With
    End Sub

    'Private Sub chkMustPunchPhysical_CheckedChanged(sender As Object, e As EventArgs) Handles chkMustPunchPhysical.CheckedChanged
    '    If chkMustPunchPhysical.Checked = True Then
    '        dvMobilePunchConsiderDuration.Visible = True
    '    Else
    '        dvMobilePunchConsiderDuration.Visible = False
    '        txtMobilePunchConsiderDuration.Text = String.Empty
    '    End If
    'End Sub

#End Region

End Class
