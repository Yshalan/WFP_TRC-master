Imports SmartV.UTILITIES
Imports System.Data
Imports SmartV.Security
Imports SmartV.DB
Imports SmartV.Security.UI
Imports System.Threading
Imports TA.Security
Imports TA.Security.UI
Imports Telerik.Web.UI
Imports SmartV.UTILITIES.ProjectCommon
Imports AjaxControlToolkit
Imports TA_Packages

Partial Class License
    Inherits System.Web.UI.Page

#Region "Page Variables"
    Private objPackages As Packages
    Private _sysGroup As SYSGroups
    Private _sysForms As SYSForms
    Private _sysPrivilege As SYSPrivilege
    Private _sysModules As SYSModules
    Private chkList As CheckBoxList
    Private ResourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"))
    Dim CultureInfo As System.Globalization.CultureInfo
    Public lang As Integer
    Dim tabcontainer As AjaxControlToolkit.TabContainer
    Private moduleTabs As List(Of SmartV.Security.UI.AISTabPanel) = New List(Of SmartV.Security.UI.AISTabPanel)
    Protected dir, textalign As String
#End Region

#Region "Properties"

    Public Property PackagesID() As Integer
        Get
            Return ViewState("PackagesID") 'hdnid.Value
        End Get
        Set(ByVal value As Integer)
            'hdnid.Value = value
            ViewState("PackagesID") = value
        End Set
    End Property

    Private Property SortDir() As String
        Get
            Return hdnsortDir.Value
        End Get
        Set(ByVal value As String)
            hdnsortDir.Value = value
        End Set
    End Property

    Private Property SortExepression() As String
        Get
            Return hdnsortExp.Value
        End Get
        Set(ByVal value As String)
            hdnsortExp.Value = value
        End Set
    End Property

    Private Property data() As DataTable
        Get
            Return ViewState("dt")
        End Get
        Set(ByVal value As DataTable)
            ViewState("dt") = value
        End Set
    End Property


    Private Property SortExep() As String
        Get
            Return ViewState("sortExp")
        End Get
        Set(ByVal value As String)
            ViewState("sortExp") = value
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

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'loadCaptions()
        CtlTab.Controls.Add(tabcontainer)
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        If Not IsPostBack Then
            LoadGrid()
        End If

        btnDelete.Attributes.Add("onclick", "javascript:return confirmDelete('" & gvGroups.ClientID & "');")

    End Sub

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        If (SessionVariables.CultureInfo Is Nothing) Then
            Response.Redirect("~/default/Login.aspx")
        Else
            If SessionVariables.CultureInfo = "ar-JO" Then
                lang = CtlCommon.Lang.AR
                Page.MasterPageFile = "~/default/ArabicMaster.master"
            Else
                lang = CtlCommon.Lang.EN
                Page.MasterPageFile = "~/default/NewMaster.master"
            End If
            Page.UICulture = SessionVariables.CultureInfo
        End If
        loadTabs()

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim dac As DAC = dac.getDAC
        Dim result As String
        objPackages = New Packages
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim errNo As Integer
        With objPackages
            .PackageName = txtPackage.Text
            For Each myTab As AISTabPanel In moduleTabs
                For Each chkBox As AISListItem In myTab.items
                    If chkBox.myItem.Selected Then
                        '.Forms = chkBox.formID
                        If result <> String.Empty Then
                            result = result & "," & chkBox.formID
                        Else
                            result = chkBox.formID
                        End If
                    Else

                    End If
                Next
            Next
            If Not (result Is Nothing) Then

                .Forms = StringRange(SortCommaSeparatedString(result))
            Else
                .Forms = String.Empty
            End If


            If PackagesID = 0 Then
                errNo = .Add()
            Else
                objPackages.PackageId = PackagesID
                errNo = .Update()
            End If
        End With

        If errNo = -11 Then

            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("Entryalreadyexists", CultureInfo), "info")
            Exit Sub
        End If

        If errNo <> 0 Then


            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("OperationFailed", CultureInfo), "error")
            Exit Sub
        End If

        CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("SaveSuccessfully", CultureInfo), "success")
        LoadGrid()
        ClearAll()
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim dac As DAC = dac.getDAC
        objPackages = New Packages
        Dim errNo As Integer
        Dim errnum As Integer
        For Each Item As GridDataItem In gvGroups.Items
            If CType(Item.FindControl("chkGroup"), CheckBox).Checked Then

                objPackages.PackageId = Item.GetDataKeyValue("PackageId")
                errnum += objPackages.Delete()

                If errnum <> 0 Then
                    ' CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("FailedPreviligies", CultureInfo) & CType(Item.FindControl("lblGroupID"), Label).Text)
                    Exit Sub
                End If

                objPackages.PackageId = Item.GetDataKeyValue("PackageId")
                errNo += objPackages.Delete
            End If
        Next

        If errNo = 0 Then
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("DeletedSuccessfully"), "success")
            LoadGrid()
        Else
            CtlCommon.ShowMessage(Me.Page, ResourceManager.GetString("ErrorDelete"), "error")
        End If
        ClearAll()
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        gvGroups.SelectedIndexes.Clear()
        ClearAll()
    End Sub

    Protected Sub gvGroups_PageIndexChanging(ByVal sender As Object, ByVal e As GridPageChangedEventArgs) Handles gvGroups.PageIndexChanged

        gvGroups.CurrentPageIndex = e.NewPageIndex
        Dim dv As New DataView(data)
        dv.Sort = SortExepression
        gvGroups.DataSource = dv
        gvGroups.DataBind()
        'CtlCommon.ClearCtlContent(New WebControl() {txtCustomerName}, CtlCommon.OpMode.ClearContent)
        'CtlCommon.ClearCtlContent(New WebControl() {txtGroupEnName}, CtlCommon.OpMode.ClearContent)

    End Sub

    Protected Sub gvGroups_NeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs) Handles gvGroups.NeedDataSource

        objPackages = New Packages
        data = objPackages.GetAll()
        gvGroups.DataSource = data
        Dim dv As New DataView(data)
        dv.Sort = SortExepression
        gvGroups.DataSource = dv
    End Sub

    Protected Sub gvGroups_SelectedIndexChanges(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvGroups.SelectedIndexChanged
        ClearAll()
        'If pnlPermissions.Visible = False Then
        '    pnlPermissions.Visible = True
        '    btnCreate.Visible = False
        'End If
        PackagesID = Convert.ToInt32(DirectCast(gvGroups.SelectedItems(0), GridDataItem).GetDataKeyValue("PackageId"))
        CultureInfo = New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Dim tabHeader As AISTabHeader = New AISTabHeader
        objPackages = New Packages
        _sysForms = New SYSForms
        Dim chkd As Boolean = True
        Dim dt As DataTable
        objPackages.PackageId = PackagesID
        objPackages.GetByPK()
        With objPackages
            txtPackage.Text = .PackageName
            dt = _sysForms.GetFormsByPackageID(PackagesID)

            '' For converting number ranges to list
            'Dim s As String = .Forms 
            'Dim values As New List(Of Integer)() 'Create an List of Integer values / numbers
            'For Each value As String In s.Split(","c) ' Go through each string between a comma
            '    If value.Contains("-"c) Then 'If this string contains a hyphen
            '        Dim begin As Integer = Convert.ToInt32(value.Split("-"c)(0)) 'split it to get the beginning value (in the first case 2)
            '        Dim [end] As Integer = Convert.ToInt32(value.Split("-"c)(1)) ' and to get the ending value (in the first case 6)
            '        For i As Integer = begin To [end] 'Then fill the integer List with values
            '            values.Add(i)
            '        Next
            '    Else
            '        values.Add(Convert.ToInt32(value)) 'If the text doesn't contain a hyphen, simply add the value to the integer List
            '    End If
            'Next



            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dRow As DataRow In dt.Rows
                    Dim formID As Integer = dRow.Item(0)
                    Dim moduleID As Integer = dRow.Item(1)
                    If Not checkForm(moduleID, formID) Then
                        chkd = False
                    End If
                Next
            End If

            For Each myTab As AISTabPanel In moduleTabs
                For Each chkBox As AISListItem In myTab.items
                    If chkBox.myItem.Selected Then
                        chkd = True
                    Else
                        chkd = False
                        Exit For
                    End If
                Next
            Next
        End With

    End Sub

#End Region

#Region "Page Methods"

    Private Sub loadCaptions()
        Dim CultureInfo As New System.Globalization.CultureInfo(SessionVariables.CultureInfo)
        Me.Page.Form.Attributes.Add("dir", "dir")
        Dim cssFile = New HtmlLink()
        'cssFile.Href = "../css/" & ResourceManager.GetString("CssFileName", CultureInfo)
        cssFile.Attributes("rel") = "stylesheet"
        cssFile.Attributes("type") = "text/css"
        cssFile.Attributes("media") = "all"
        Me.Page.Header.Controls.Add(cssFile)
        MyBase.InitializeCulture()
    End Sub

    Protected Overrides Sub InitializeCulture()
        Dim _culture As String = SessionVariables.CultureInfo
        If (String.IsNullOrEmpty(_culture)) Then
            _culture = "en-US"
            Me.UICulture = _culture
            Me.Culture = _culture
        End If
        If (_culture <> "Auto") Then
            Dim ci As New System.Globalization.CultureInfo(_culture)
            System.Threading.Thread.CurrentThread.CurrentCulture = ci
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci
        End If
        MyBase.InitializeCulture()
    End Sub

    Private Function checkForm(ByVal moduleID As Integer, ByVal formID As Integer) As Boolean
        Dim ltabHeader As AISTabHeader = New AISTabHeader
        For Each myTab As AISTabPanel In moduleTabs
            If (myTab.moduleID = moduleID) Then
                For Each chkBox As AISListItem In myTab.items
                    If (chkBox.formID = formID) Then
                        chkBox.myItem.Selected = True
                    End If
                Next
            End If
        Next
    End Function

    Private Sub LoadGrid()

        objPackages = New Packages
        data = objPackages.GetAll()
        gvGroups.DataSource = data
        Dim dv As New DataView(data)
        dv.Sort = SortExepression
        gvGroups.DataSource = dv
        gvGroups.DataBind()

    End Sub

    Private Sub loadTabs()
        tabcontainer = New AjaxControlToolkit.TabContainer
        Dim countZones As Integer = 0

        _sysModules = New SYSModules
        _sysPrivilege = New SYSPrivilege

        Dim dt As DataTable
        Dim dtPaivilege As DataTable = Nothing
        dt = _sysModules.load()
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1

                Dim tab As AISTabPanel = New AISTabPanel
                tab.Visible = True
                tab.moduleID = dt.Rows(i).Item(0)
                'If tab.moduleID <> 3 Then

                chkList = New CheckBoxList
                If SessionVariables.CultureInfo = "en-US" Then
                    dtPaivilege = _sysPrivilege.GetFormsObj(dt.Rows(i).Item(0), CtlCommon.Lang.EN)
                Else
                    dtPaivilege = _sysPrivilege.GetFormsObj(dt.Rows(i).Item(0), CtlCommon.Lang.AR)
                End If
                If dtPaivilege IsNot Nothing AndAlso dtPaivilege.Rows.Count > 0 Then
                    For j As Integer = 0 To dtPaivilege.Rows.Count - 1
                        Dim list As New AISListItem
                        If countZones > 0 Then
                            'list.myItem.Text = dtPaivilege.Rows(j).Item(1).ToString & " <a href='#' onclick='return javascript:windowOO('" & dtPaivilege.Rows(j).Item(0) & "') target='_blank'  > ..Zones.. </a>"
                            If SessionVariables.CultureInfo = "en-US" Then
                                list.myItem.Text = dtPaivilege.Rows(j).Item(1).ToString & " <a OnClick='javascript:newwindow(" & dtPaivilege.Rows(j).Item(0) & ")' style='text-decoration:underline;cursor:hand;' target='_blank'  > ..Zones.. </a>"
                            Else
                                list.myItem.Text = dtPaivilege.Rows(j).Item(1).ToString & " <a OnClick='javascript:newwindow(" & dtPaivilege.Rows(j).Item(0) & ")' style='text-decoration:underline;cursor:hand;' target='_blank'  > ..أقسام الصفحة.. </a>"
                            End If

                            'list.myItem.Text = dtPaivilege.Rows(j).Item(1).ToString & " <input id='1' type='button' onclick='javascript:window.open('Zones.aspx','popup','width=370,height=230,left=175,top=175,resizable=no');' value='..Zones..' /> "
                        Else
                            list.myItem.Text = dtPaivilege.Rows(j).Item(1).ToString
                        End If
                        list.myItem.Value = dtPaivilege.Rows(j).Item(0)

                        list.formID = dtPaivilege.Rows(j).Item(0)
                        If dt.Rows(i).Item(0).ToString() = "4" Or dt.Rows(i).Item(0).ToString() = "8" Or dt.Rows(i).Item(0).ToString() = "9" Then
                            If dtPaivilege.Rows(j).Item(2).ToString = "0" Then
                                list.myItem.Text = "<b><font size=3>" & list.myItem.Text & "</font></b>"
                            Else
                                list.myItem.Text = list.myItem.Text
                            End If
                        End If
                        tab.items.Add(list)

                        chkList.Items.Add(list.myItem)

                        list = Nothing
                    Next

                    countZones = 0
                    chkList.RepeatDirection = RepeatDirection.Vertical
                    chkList.RepeatColumns = 2
                    chkList.RepeatLayout = RepeatLayout.Table
                    chkList.AutoPostBack = False
                    chkList.Visible = True

                    ' AddHandler chkList.SelectedIndexChanged, AddressOf chkList_SelectedIndexChanged
                    chkList.ID = "cbl" & dt.Rows(i).Item(1).ToString & i
                    tab.Controls.Add(chkList)
                    tab.HeaderText = dt.Rows(i).Item(1).ToString
                    tabcontainer.Tabs.Add(tab)
                    moduleTabs.Add(tab)
                    Dim objTabHeader As New AISTabHeader
                End If
            Next
        End If
        tabcontainer.Width = Unit.Percentage(100)
    End Sub

    Protected Overrides Sub OnInit(ByVal e As EventArgs)
        loadTabs()
        MyBase.OnInit(e)
        Me.Page.RegisterRequiresControlState(Me)

    End Sub

    Private Sub ClearAll()

        txtPackage.Text = String.Empty
        ClearTabs()
        SortDir = Nothing
        PackagesID = 0
    End Sub

    Private Sub ClearTabs()
        Dim tabHeader As AISTabHeader = New AISTabHeader
        Dim myTab As AISTabPanel = Nothing
        For Each myTab In moduleTabs
            For Each chkBox As AISListItem In myTab.items
                chkBox.myItem.Selected = False
            Next
        Next
        DirectCast(DirectCast(myTab.Parent, System.Web.UI.Control), AjaxControlToolkit.TabContainer).ActiveTab = DirectCast(DirectCast(myTab.Parent, System.Web.UI.Control), AjaxControlToolkit.TabContainer).Tabs(0)
    End Sub

#End Region

    Private Function chkListPermessions() As Object
        Throw New NotImplementedException
    End Function

    Private Function StringRange(strList As String) As String
        Dim i As Long
        Dim lngStart As Long
        Dim lngCurr As Long
        Dim lngEnd As Long
        Dim strParts() As String
        ' Get out if input is blank
        If strList = "" Then
            Exit Function
        End If
        ' Split the list

        strParts = Split(strList, ",")
        ' Initialize
        lngStart = Val(Trim(strParts(0)))
        lngEnd = Val(Trim(strParts(0)))
        ' Loop
        For i = 1 To UBound(strParts)
            lngCurr = Val(Trim(strParts(i)))
            If lngCurr > lngEnd + 1 Then
                StringRange = StringRange & ", " & lngStart
                If lngEnd > lngStart Then
                    StringRange = StringRange & "-" & lngEnd
                End If
                lngStart = lngCurr
            End If
            lngEnd = lngCurr
        Next i
        ' Process final part
        StringRange = StringRange & ", " & lngStart
        If lngEnd > lngStart Then
            StringRange = StringRange & "-" & lngEnd
        End If
        ' Remove leading ", "
        If StringRange <> "" Then
            StringRange = Mid(StringRange, 3)
        End If
    End Function

    Private Function SortCommaSeparatedString(ByVal name As String) As String
        Dim stringArray As String() = name.Split(",")
        Dim value As Int32
        Dim intArray = (From str In stringArray
                       Let isInt = Int32.TryParse(str, value)
                       Where isInt
                       Select Int32.Parse(str)).ToArray
        Array.Sort(intArray)
        Dim returnValue As String = ""
        For i As Integer = 0 To intArray.GetUpperBound(0)
            returnValue = returnValue & intArray(i) & ","
        Next
        Return returnValue.Remove(returnValue.Length - 1, 1)

    End Function

 
End Class
