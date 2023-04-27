Imports System.Data
Imports System.Reflection
Imports System.IO
Imports System.Threading
Imports System.Globalization
Imports Microsoft.Office.Interop
Imports System.Drawing
'Imports Excel = Microsoft.Office.Interop.Excel


Namespace SmartV.UTILITIES
    Public NotInheritable Class CtlCommon



#Region "CONSTRUCTOR"
        Private Sub New()
        End Sub
#End Region

#Region "ENUMARATIONS"
        ''' <summary>
        ''' Enum for selecting language
        ''' </summary>
        ''' <remarks>Used in FillDropDownList()</remarks>
        Public Enum Lang
            EN
            AR
        End Enum

        ''' <summary>
        ''' Enum for Operation Mode
        ''' </summary>
        ''' <remarks>Used in ClearCtlContent()</remarks>
        Public Enum OpMode
            ClearContent
            ResetIndex
            ResetTimeContent
        End Enum

        Public Enum UserType
            SystemUser = 1
            ActiveDirectoryUser = 2
        End Enum

        Public Enum NoShiftSchedule
            ConsideritOffDay = 1
            ConsiderDefaultSchedule = 2
        End Enum

        Public Enum ScheduleType
            Normal = 1
            Flexible = 2
            Advance = 3
        End Enum

        Public Enum RequestStatusEnum
            Pending = 1
            ApprovedByManager = 2
            ApprovedByHumanResource = 3
            RejectedbyManager = 4
            RejectedByHumanResource = 5
            ApprovedByGeneralManager = 6
            RejectedByGeneralManager = 7
            AutomaticApproved = 8
        End Enum
#End Region

#Region "SHARED METHODS"

#Region "GRIDVIEW UTILITIES"
        ''' <summary>
        ''' Utility to fill Grid View using DataSet
        ''' </summary>
        ''' <param name="strKeyNames">Primary-Key Fields Name</param>
        ''' <remarks></remarks>
        Public Shared Sub FillGridView(ByVal gridView As GridView, ByVal dataSet As DataSet, Optional ByVal strKeyNames() As String = Nothing)
            'Public Shared Sub FillGridView(ByVal i_gvID As GridView, ByVal i_dataSource As DataSet, ByVal i_dataMember As String, Optional ByVal i_dataKeyNames() As String = Nothing)
            CtlCommon.ClearCtlContent(New WebControl() {gridView})
            If (dataSet IsNot Nothing) Then
                If dataSet.Tables.Count > 0 AndAlso dataSet.Tables(0).Rows.Count > 0 Then
                    Try
                        With gridView
                            .DataSource = dataSet
                            '.DataMember = i_dataMember
                            If (strKeyNames IsNot Nothing) Then .DataKeyNames = strKeyNames
                            .DataBind()
                            .Visible = True
                        End With
                    Catch ex As Exception
                        'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name)
                        Dim pagePath(1) As String
                        Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                        Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                        pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                        CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                    End Try
                End If
            End If
        End Sub

        '''' <summary>
        '''' Utility to fill Grid View using DataTable
        '''' </summary>
        '''' <param name="strKeyNames">Primary-Key Fields Name</param>
        '''' <remarks></remarks>

#End Region

#Region "DataGrid UTILITIES"
        '''' <summary>
        '''' Utility to fill Grid View using DataSet
        '''' </summary>
        '''' <param name="strKeyNames">Primary-Key Fields Name</param>
        '''' <remarks></remarks>
        Public Shared Sub FillDataGrid(ByVal dgrd As DataGrid, ByVal dataSet As DataSet)
            'Public Shared Sub FillGridView(ByVal i_gvID As GridView, ByVal i_dataSource As DataSet, ByVal i_dataMember As String, Optional ByVal i_dataKeyNames() As String = Nothing)
            CtlCommon.ClearCtlContent(New WebControl() {dgrd})
            If (dataSet IsNot Nothing) Then
                If dataSet.Tables.Count > 0 AndAlso dataSet.Tables(0).Rows.Count > 0 Then
                    Try
                        With dgrd
                            .DataSource = dataSet
                            '.DataMember = i_dataMember
                            .DataBind()
                            .Visible = True
                        End With
                    Catch ex As Exception
                        'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name)
                        Dim pagePath(1) As String
                        Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                        Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                        pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                        CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                    End Try
                End If
            End If
        End Sub

        ''' <summary>
        ''' Utility to fill Grid View using DataTable
        ''' </summary>
        ''' <param name="strKeyNames">Primary-Key Fields Name</param>
        ''' <remarks></remarks>
        Public Shared Sub FillGridView(ByVal gridView As GridView, ByVal dataTable As DataTable, Optional ByVal strKeyNames() As String = Nothing)
            'Public Shared Sub FillGridView(ByVal i_gvID As GridView, ByVal i_dataSource As DataTable, ByVal i_dataMember As String, Optional ByVal i_dataKeyNames() As String = Nothing)
            CtlCommon.ClearCtlContent(New WebControl() {gridView})
            If (dataTable IsNot Nothing) Then
                If dataTable.Rows.Count > 0 Then
                    Try
                        With gridView
                            .DataSource = dataTable
                            '.DataMember = i_dataMember
                            If (strKeyNames IsNot Nothing) Then .DataKeyNames = strKeyNames
                            .DataBind()
                            .Visible = True
                        End With
                    Catch ex As Exception
                        'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name)
                        Dim pagePath(1) As String
                        Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                        Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                        pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                        CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))
                    End Try
                End If
            End If
        End Sub
#End Region
#Region "DROPDOWNLIST UTILITIES"
        ''' <summary>
        ''' Utility to fill DropDownList using DataSet
        ''' </summary>
        ''' <param name="Lang">Language (English, Or Arabic)</param>
        ''' <remarks></remarks>
        Public Shared Sub FillDropDownList(ByVal dropDownList As DropDownList, ByVal dataSet As DataSet, Optional ByVal Lang As Lang = Lang.EN)
            If (dropDownList IsNot Nothing) AndAlso (dataSet IsNot Nothing) Then
                Dim lstItm As New ListItem

                CtlCommon.ClearCtlContent(New WebControl() {dropDownList})
                With lstItm
                    .Value = -1
                    If Lang = Lang.EN Then
                        .Text = "--Please Select--"
                    Else
                        .Text = "--الرجاء الاختيار--"
                    End If
                End With
                dropDownList.Items.Add(lstItm)
                lstItm = Nothing
                If dataSet.Tables.Count > 0 AndAlso dataSet.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                        Dim newLstItm As New ListItem
                        With newLstItm
                            .Value = dataSet.Tables(0).Rows(i).Item(0).ToString
                            .Text = dataSet.Tables(0).Rows(i).Item(1).ToString
                        End With
                        dropDownList.Items.Add(newLstItm)
                        newLstItm = Nothing
                    Next
                End If
            End If
        End Sub

        Public Shared Sub FillTelerikDropDownList(ByVal dropDownList As Telerik.Web.UI.RadComboBox, ByVal datatable As DataTable, Optional ByVal Lang As Lang = Lang.EN)
            If (dropDownList IsNot Nothing) AndAlso (datatable IsNot Nothing) Then
                Dim dr As DataRow
                dr = datatable.NewRow()
                dr(0) = -1
                If Lang = Lang.EN Then
                    dr(1) = "--Please Select--"
                    dropDownList.DataTextField = datatable.Columns(1).ColumnName
                Else
                    If datatable.Columns.Count > 2 Then
                        dr(2) = "--الرجاء الاختيار--"
                        dropDownList.DataTextField = datatable.Columns(2).ColumnName
                    Else
                        dr(1) = "--الرجاء الاختيار--"
                        dropDownList.DataTextField = datatable.Columns(1).ColumnName
                    End If


                End If
                datatable.Rows.InsertAt(dr, 0)

                With dropDownList
                    '.ClearSelection()
                    .DataSource = datatable
                    .DataValueField = datatable.Columns(0).ColumnName
                    .ClearSelection()
                    .DataBind()
                End With
            End If
        End Sub

        Public Shared Sub FillTelerikDropDownListWithAll(ByVal dropDownList As Telerik.Web.UI.RadComboBox, ByVal datatable As DataTable, Optional ByVal Lang As Lang = Lang.EN)
            If (dropDownList IsNot Nothing) AndAlso (datatable IsNot Nothing) Then
                Dim dr As DataRow
                dr = datatable.NewRow()
                dr(0) = -1
                If Lang = Lang.EN Then
                    dr(1) = "-- All --"
                Else
                    dr(1) = "-- الجميع --"
                End If
                datatable.Rows.InsertAt(dr, 0)
                With dropDownList
                    .DataSource = datatable
                    .DataValueField = datatable.Columns(0).ColumnName
                    .DataTextField = datatable.Columns(1).ColumnName
                    .DataBind()
                End With
            End If
        End Sub
        Public Shared Sub FillAjaxDropDownList(ByVal dropDownList As ComboBox, ByVal datatable As DataTable, Optional ByVal Lang As Lang = Lang.EN)
            If (dropDownList IsNot Nothing) AndAlso (datatable IsNot Nothing) Then
                Dim dr As DataRow
                dr = datatable.NewRow()
                dr(0) = -1
                If Lang = Lang.EN Then
                    dr(1) = "--Please Select--"
                Else
                    dr(1) = "--الرجاء الاختيار--"
                End If
                datatable.Rows.InsertAt(dr, 0)
                With dropDownList
                    .DataSource = datatable
                    .DataValueField = datatable.Columns(0).ColumnName
                    .DataTextField = datatable.Columns(1).ColumnName
                    .DataBind()
                End With
            End If
        End Sub


        Public Shared Sub ManageCtrl(ByVal page As Page, ByVal ctrlName As String, ByVal op As Integer)
            If (page IsNot Nothing) Then
                Dim strBuild As New StringBuilder
                With strBuild
                    If op = 1 Then
                        .Append(Environment.NewLine)
                        .Append("<!--")
                        .Append(Environment.NewLine)
                        .Append("document.getElementById('")
                        .Append(ctrlName.Replace("""", "\""").Replace("'", "\'"))
                        .Append("')")
                        .Append(".style.display = 'none")
                        .Append("';")
                        .Append(Environment.NewLine)
                        .Append("// -->")
                        .Append(Environment.NewLine)
                    Else
                        .Append(Environment.NewLine)
                        .Append("<!--")
                        .Append(Environment.NewLine)
                        .Append("document.getElementById('")
                        .Append(ctrlName.Replace("""", "\""").Replace("'", "\'"))
                        .Append("')")
                        .Append(".style.visibility = 'visible")
                        .Append("';")
                        .Append(Environment.NewLine)
                        .Append("// -->")
                        .Append(Environment.NewLine)
                    End If
                End With
                ScriptManager.RegisterStartupScript(page, page.GetType, Guid.NewGuid.ToString, strBuild.ToString, True)
                strBuild = Nothing
            End If
        End Sub


        '

        ''' <summary>
        ''' Utility to fill DropDownList using DataTable
        ''' </summary>
        ''' <param name="lang">Language (English, Or Arabic)</param>
        ''' <remarks></remarks>
        Public Shared Sub FillDropDownList(ByVal dropDownList As DropDownList, ByVal dataSource As DataTable, Optional ByVal Lang As Lang = Lang.EN)
            If (dropDownList IsNot Nothing) AndAlso (dataSource IsNot Nothing) Then
                Dim lstItm As New ListItem

                CtlCommon.ClearCtlContent(New WebControl() {dropDownList})
                With lstItm
                    .Value = -1
                    If Lang = Lang.EN Then
                        .Text = "--Please Select--"
                    Else
                        .Text = "--الرجاء الاختيار--"
                    End If
                End With
                dropDownList.Items.Add(lstItm)
                lstItm = Nothing
                If dataSource.Rows.Count > 0 Then
                    For i As Integer = 0 To dataSource.Rows.Count - 1
                        Dim newLstItm As New ListItem
                        With newLstItm
                            .Value = dataSource.Rows(i).Item(0).ToString
                            .Text = dataSource.Rows(i).Item(1).ToString
                        End With
                        dropDownList.Items.Add(newLstItm)
                        newLstItm = Nothing
                    Next
                End If
            Else
                CtlCommon.ClearCtlContent(New WebControl() {dropDownList})
                Dim lstItm As New ListItem
                With lstItm
                    .Value = -1
                    If Lang = Lang.EN Then
                        .Text = "--Please Select--"
                    Else
                        .Text = "--الرجاء الاختيار--"
                    End If
                End With
                dropDownList.Items.Add(lstItm)
                lstItm = Nothing
            End If
        End Sub

        ''' <summary>
        ''' Utility to fill CheckBoxList using DataTable
        ''' </summary>
        Public Shared Sub FillCheckBox(ByVal checkboxlist As CheckBoxList, ByVal dataSource As DataTable)
            If (checkboxlist IsNot Nothing) AndAlso (dataSource IsNot Nothing) Then
                CtlCommon.ClearCtlContent(New WebControl() {checkboxlist})
                If dataSource.Rows.Count > 0 Then
                    If SessionVariables.CultureInfo = "en-US" Then
                        For i As Integer = 0 To dataSource.Rows.Count - 1
                            checkboxlist.Items.Add(New ListItem(dataSource.Rows(i).Item(1).ToString, dataSource.Rows(i).Item(0).ToString))
                        Next
                    Else
                        For i As Integer = 0 To dataSource.Rows.Count - 1
                            checkboxlist.Items.Add(New ListItem(dataSource.Rows(i).Item(2).ToString, dataSource.Rows(i).Item(0).ToString))
                        Next
                    End If
                    checkboxlist.SelectedIndex = -1
                End If
            End If
        End Sub

        ''' <summary>
        ''' Utility to fill RadioButtonList using DataTable
        ''' </summary>
        Public Shared Sub FillRadioButtonList(ByVal radioButtonList As RadioButtonList, ByVal dataSource As DataTable)
            If (radioButtonList IsNot Nothing) AndAlso (dataSource IsNot Nothing) Then
                CtlCommon.ClearCtlContent(New WebControl() {radioButtonList}, OpMode.ClearContent)
                If dataSource.Rows.Count > 0 Then
                    For i As Integer = 0 To dataSource.Rows.Count - 1
                        radioButtonList.Items.Add(New ListItem(dataSource.Rows(i).Item(1).ToString, dataSource.Rows(i).Item(0).ToString))
                    Next
                    radioButtonList.SelectedIndex = 1
                End If
            End If
        End Sub

        Public Shared Sub FillRadioButtonList1(ByVal radioButtonList As RadioButtonList, ByVal dataSource As DataTable)
            If (radioButtonList IsNot Nothing) AndAlso (dataSource IsNot Nothing) Then
                CtlCommon.ClearCtlContent(New WebControl() {radioButtonList})
                If dataSource.Rows.Count > 0 Then
                    For i As Integer = 0 To dataSource.Rows.Count - 1
                        radioButtonList.Items.Add(New ListItem(dataSource.Rows(i).Item(1).ToString, dataSource.Rows(i).Item(0).ToString))
                    Next
                    radioButtonList.SelectedIndex = -1
                End If
            End If
        End Sub
#End Region


        ''' <summary>
        ''' Utility for Clearing Control Contents
        ''' </summary>
        ''' <param name="strDate"></param>
        ''' <returns></returns>
        ''' <remarks>OpMode.clearContent applies to DropDownList, GridView, and DataList</remarks>
        Public Shared Function strChangeDateFormat(ByVal strDate As String) As String
            Dim strArr(2) As String
            strArr = strDate.Split("/")
            Return strArr(1) & "/" & strArr(0) & "/" & strArr(2) & " 12:00:00 AM"
        End Function
        'Public Shared Function CheckValidDateRange(ByVal strFromDate As String, ByVal strToDate As String) As Boolean
        '    If (Date.Compare(strChangeDateFormat(strFromDate), strChangeDateFormat(strToDate))) <= 0 Then
        '        Return True
        '    End If
        '    Return False
        'End Function
        Public Shared Sub ClearCtlContent(ByVal webControl() As WebControl, Optional ByVal OpMode As OpMode = OpMode.ClearContent)
            Try
                For Each ctl As WebControl In webControl
                    If TypeOf ctl Is TextBox Then
                        DirectCast(ctl, TextBox).Text = String.Empty
                    ElseIf TypeOf ctl Is Label Then
                        DirectCast(ctl, Label).Text = String.Empty
                    ElseIf TypeOf ctl Is RadNumericTextBox Then
                        DirectCast(ctl, RadNumericTextBox).Text = String.Empty
                    ElseIf TypeOf ctl Is RadTextBox Then
                        DirectCast(ctl, RadTextBox).Text = String.Empty
                    ElseIf TypeOf ctl Is CheckBox Then
                        DirectCast(ctl, CheckBox).Checked = False
                    ElseIf TypeOf ctl Is CheckBoxList Then
                        With DirectCast(ctl, CheckBoxList)
                            If OpMode = CtlCommon.OpMode.ClearContent Then
                                .DataSource = Nothing
                                .DataBind()
                                If .Items.Count > 0 Then
                                    .Items.Clear()
                                End If
                            Else
                                For Each chkbx As ListItem In DirectCast(ctl, CheckBoxList).Items
                                    chkbx.Selected = False
                                Next
                            End If
                        End With
                    ElseIf TypeOf ctl Is RadioButtonList Then
                        DirectCast(ctl, RadioButtonList).SelectedIndex = 0
                    ElseIf TypeOf ctl Is DropDownList Then
                        With DirectCast(ctl, DropDownList)
                            If OpMode = OpMode.ClearContent Then
                                .DataSource = Nothing
                                .DataBind()
                                If .Items.Count > 0 Then
                                    .Items.Clear()
                                End If
                            Else
                                If (.Items.Count > 1) Then
                                    .SelectedValue = -1
                                End If
                            End If
                        End With
                    ElseIf TypeOf ctl Is RadComboBox Then
                        With DirectCast(ctl, RadComboBox)
                            If OpMode = OpMode.ClearContent Then
                                .DataSource = Nothing
                                .DataBind()
                                If .Items.Count >= 0 Then
                                    .Items.Clear()
                                Else
                                    .SelectedValue = -1
                                End If

                            Else
                                If (.Items.Count > 1) Then
                                    .SelectedValue = -1

                                End If
                            End If
                        End With
                    ElseIf TypeOf ctl Is GridView Then
                        With DirectCast(ctl, GridView)
                            If OpMode = OpMode.ClearContent Then
                                .DataSource = Nothing
                                .DataBind()
                            Else
                                .SelectedIndex = 0
                            End If
                        End With
                    ElseIf TypeOf ctl Is DataList Then
                        With DirectCast(ctl, DataList)
                            If OpMode = OpMode.ClearContent Then
                                .DataSource = Nothing
                                .DataBind()
                            End If
                        End With
                    ElseIf TypeOf ctl Is ListBox Then
                        With CType(ctl, ListBox)
                            If OpMode = OpMode.ResetIndex Then
                                .SelectedIndex = 0
                            Else
                                If (.Items.Count > 0) Then
                                    .Items.Clear()
                                End If
                                .DataSource = Nothing
                                .DataBind()
                            End If
                        End With
                    End If
                Next
            Catch ex As Exception
                'CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name)
                Dim pagePath(1) As String
                Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

            Finally
                webControl = Nothing
            End Try
        End Sub

        Public Shared Sub ResetTimeCtlContent(ByVal webControl() As WebControl, Optional ByVal OpMode As OpMode = OpMode.ResetTimeContent)
            Try
                For Each ctl As WebControl In webControl
                    If TypeOf ctl Is TextBox Then
                        DirectCast(ctl, TextBox).Text = "00:00"
                    ElseIf TypeOf ctl Is RadNumericTextBox Then
                        DirectCast(ctl, RadNumericTextBox).Text = "00:00"
                    End If
                Next
            Catch ex As Exception
                'CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name)
                Dim pagePath(1) As String
                Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

            Finally
                webControl = Nothing
            End Try
        End Sub

        Public Shared Sub EnableCtlContent(ByVal webControl() As WebControl, ByVal Status As Boolean)
            Try
                For Each ctl As WebControl In webControl
                    If TypeOf ctl Is TextBox Then
                        DirectCast(ctl, TextBox).Enabled = Status
                    ElseIf TypeOf ctl Is Label Then
                        DirectCast(ctl, Label).Enabled = Status
                    ElseIf TypeOf ctl Is Button Then
                        DirectCast(ctl, Button).Enabled = Status
                    ElseIf TypeOf ctl Is CheckBox Then
                        DirectCast(ctl, CheckBox).Enabled = Status
                    ElseIf TypeOf ctl Is CheckBoxList Then
                        With DirectCast(ctl, CheckBoxList)
                            For Each chkbx As ListItem In DirectCast(ctl, CheckBoxList).Items
                                chkbx.Enabled = Status
                            Next
                        End With
                    ElseIf TypeOf ctl Is RadioButtonList Then
                        DirectCast(ctl, RadioButtonList).Enabled = Status
                    ElseIf TypeOf ctl Is DropDownList Then
                        With DirectCast(ctl, DropDownList)
                            .Enabled = Status
                        End With
                    ElseIf TypeOf ctl Is RadComboBox Then
                        With DirectCast(ctl, RadComboBox)
                            .Enabled = Status
                        End With
                    ElseIf TypeOf ctl Is RadDatePicker Then
                        With DirectCast(ctl, RadDatePicker)
                            .Enabled = Status
                        End With
                    ElseIf TypeOf ctl Is RadNumericTextBox Then
                        With DirectCast(ctl, RadNumericTextBox)
                            .Enabled = Status
                        End With
                    ElseIf TypeOf ctl Is RadTextBox Then
                        With DirectCast(ctl, RadTextBox)
                            .Enabled = Status
                        End With
                        'ElseIf TypeOf ctl Is GridView Then
                        '    With DirectCast(ctl, GridView)
                        '        If OpMode = OpMode.ClearContent Then
                        '            .DataSource = Nothing
                        '            .DataBind()
                        '        Else
                        '            .SelectedIndex = 0
                        '        End If
                        '    End With
                        'ElseIf TypeOf ctl Is DataList Then
                        '    With DirectCast(ctl, DataList)
                        '        If OpMode = OpMode.ClearContent Then
                        '            .DataSource = Nothing
                        '            .DataBind()
                        '        End If
                        '    End With
                    ElseIf TypeOf ctl Is ListBox Then
                        With CType(ctl, ListBox)

                            If (.Items.Count > 0) Then
                                .Enabled = Status
                            End If


                        End With
                    End If
                Next
            Catch ex As Exception
                'CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name)
                Dim pagePath(1) As String
                Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

            Finally
                webControl = Nothing
            End Try
        End Sub
        Public Shared Sub VisibleCtlContent(ByVal webControl() As WebControl, ByVal Status As Boolean)
            Try
                For Each ctl As WebControl In webControl
                    If TypeOf ctl Is TextBox Then
                        DirectCast(ctl, TextBox).Visible = Status
                    ElseIf TypeOf ctl Is Label Then
                        DirectCast(ctl, Label).Visible = Status
                    ElseIf TypeOf ctl Is Button Then
                        DirectCast(ctl, Button).Visible = Status
                    ElseIf TypeOf ctl Is CheckBox Then
                        DirectCast(ctl, CheckBox).Visible = Status
                    ElseIf TypeOf ctl Is CheckBoxList Then
                        With DirectCast(ctl, CheckBoxList)
                            For Each chkbx As ListItem In DirectCast(ctl, CheckBoxList).Items

                            Next
                        End With
                    ElseIf TypeOf ctl Is RadioButtonList Then
                        DirectCast(ctl, RadioButtonList).Visible = Status
                    ElseIf TypeOf ctl Is DropDownList Then
                        With DirectCast(ctl, DropDownList)
                            .Visible = Status
                        End With
                    ElseIf TypeOf ctl Is RadComboBox Then
                        With DirectCast(ctl, RadComboBox)
                            .Visible = Status
                        End With
                    ElseIf TypeOf ctl Is RadDatePicker Then
                        With DirectCast(ctl, RadDatePicker)
                            .Visible = Status
                        End With
                    ElseIf TypeOf ctl Is RadNumericTextBox Then
                        With DirectCast(ctl, RadNumericTextBox)
                            .Visible = Status
                        End With
                    ElseIf TypeOf ctl Is RadTextBox Then
                        With DirectCast(ctl, RadTextBox)
                            .Visible = Status
                        End With
                        'ElseIf TypeOf ctl Is GridView Then
                        '    With DirectCast(ctl, GridView)
                        '        If OpMode = OpMode.ClearContent Then
                        '            .DataSource = Nothing
                        '            .DataBind()
                        '        Else
                        '            .SelectedIndex = 0
                        '        End If
                        '    End With
                        'ElseIf TypeOf ctl Is DataList Then
                        '    With DirectCast(ctl, DataList)
                        '        If OpMode = OpMode.ClearContent Then
                        '            .DataSource = Nothing
                        '            .DataBind()
                        '        End If
                        '    End With 
                    ElseIf TypeOf ctl Is RequiredFieldValidator Then
                        With DirectCast(ctl, RequiredFieldValidator)
                            .Visible = Status
                        End With
                    ElseIf TypeOf ctl Is ListBox Then
                        With CType(ctl, ListBox)

                            If (.Items.Count > 0) Then
                                .Visible = Status
                            End If


                        End With
                    End If
                Next
            Catch ex As Exception
                'CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name)
                Dim pagePath(1) As String
                Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

            Finally
                webControl = Nothing
            End Try
        End Sub
        ''' <summary>
        ''' Utility for setting control focus via JavaScript
        ''' </summary>
        ''' <param name="page">Executing Page ID</param>
        ''' <param name="webControl">Control ID</param>
        ''' <remarks></remarks>
        Public Shared Sub SetCtlFocus(ByVal page As Page, ByVal webControl As WebControl)
            If (page IsNot Nothing) AndAlso (webControl IsNot Nothing) Then
                Dim strBuild As New StringBuilder
                With strBuild
                    .Append(Environment.NewLine)
                    .Append("<!--")
                    .Append(Environment.NewLine)
                    .Append("function SetFocus(){document.getElementById('")
                    .Append(webControl.ClientID)
                    .Append("').focus();}")
                    .Append("window.onload = SetFocus;")
                    .Append(Environment.NewLine)
                    .Append("// -->")
                    .Append(Environment.NewLine)
                End With
                ScriptManager.RegisterStartupScript(page, page.GetType, Guid.NewGuid.ToString, strBuild.ToString, True)
                strBuild = Nothing
            End If
        End Sub

        ''' <summary>
        ''' Utility for Displaying a MessageBox
        ''' </summary>
        ''' <param name="page">Page ID</param>
        ''' <param name="strMsg">Message to Display</param>
        ''' <remarks></remarks>
        Public Shared Sub ShowMessage(ByVal page As Page, ByVal strMsg As String, Optional msgType As String = "info")
            If (page IsNot Nothing) Then
                Dim strBuild As New StringBuilder
                With strBuild
                    .Append(Environment.NewLine)
                    .Append("<!--")
                    .Append(Environment.NewLine)
                    Select Case msgType
                        Case "success"
                            .Append("ShowMessage_Success('")
                        Case "warning"
                            .Append("ShowMessage_Warning('")
                        Case "error"
                            .Append("ShowMessage_Error('")
                        Case Else
                            .Append("ShowMessage('")
                    End Select
                    .Append(strMsg.Replace("""", "\""").Replace("'", "\'"))
                    .Append("');")
                    .Append(Environment.NewLine)
                    .Append("// -->")
                    .Append(Environment.NewLine)
                End With
                ScriptManager.RegisterStartupScript(page, page.GetType, Guid.NewGuid.ToString, strBuild.ToString, True)
                strBuild = Nothing
            End If
        End Sub

        Public Shared Function DeleteConfirmation(ByVal UniqueID As String, strMsg As String) As String
            Return String.Format("return DeleteConfirmation('{0}', '{1}', '{2}');", strMsg, UniqueID, SessionVariables.CultureInfo.ToString())
        End Function

        Public Shared Function ValidateDeletedGridWithConfirmation(ByVal gridClientID As String, senderUniqueID As String, strMsg As String, ByVal Lang As String) As String

            Return String.Format("return ValidateDeletedGridWithConfirmation('{0}', '{1}', '{2}', '{3}');", strMsg, gridClientID, senderUniqueID, Lang)
        End Function

        Public Shared Sub ShowToolTip_Round(ByVal page As Page, ByVal Title As String, ByVal strMsg As String, Optional Lang As String = "info")
            ScriptManager.RegisterStartupScript(page, page.GetType, Guid.NewGuid.ToString(), String.Format("notify(""{0}"",""{1}"",""{2}"");", Title, strMsg, Lang), True)
        End Sub

        Public Shared Function getLogPath() As String
            Return AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1)
        End Function



        ''' <summary>
        ''' Utility for creating an Error Log
        ''' </summary>
        ''' <param name="strPath"></param>
        ''' <param name="strErrMsg"></param>
        ''' <param name="strMethodName"></param>
        ''' <remarks></remarks>
        Public Shared Sub CreateErrorLog(ByVal strPath As String, ByVal strErrMsg As String, ByVal strMethodName As String)
            If (strPath IsNot Nothing) AndAlso (strPath.Trim.Length > 0) Then
                If Not (strPath.EndsWith("\)")) Then strPath = String.Concat(strPath, "\")
                Dim strBuilder As New StringBuilder
                With strBuilder
                    .Append("[")
                    .Append(String.Format("{0:dd/MM/yyyy}", DateTime.Now.ToShortDateString))
                    .Append(" - ")
                    .Append(String.Format("{0:HH:mm:ss}", DateTime.Now))
                    .Append("] ")
                    .Append(Chr(9))
                    .Append(" --- ")
                    .Append(Chr(9))
                    .Append(strErrMsg)
                    If strMethodName.Trim.Length > 0 Then
                        .Append(Chr(9))
                        .Append(" --- ")
                        .Append(Chr(9))
                        .Append("In Method: ")
                        .Append(strMethodName)
                    End If
                    .Append(Environment.NewLine)
                    Try
                        My.Computer.FileSystem.WriteAllText(strPath & DateTime.Now.ToString("yyyyMMdd") & ".log", .ToString, True)
                    Catch ex As Exception
                    End Try
                    strBuilder = Nothing
                End With
            End If
        End Sub
        'Public Shared Sub InitializeCulture(ByVal strCulture As String)
        '    If strCulture <> "" Then
        '        Thread.CurrentThread.CurrentUICulture = New CultureInfo(strCulture)
        '        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(strCulture)

        '    End If

        'End Sub



        'Public Shared Sub addPopupPanel(ByVal myPage As Page, ByVal parentCtrl As Control, ByVal myPanel As Panel, ByVal modalpopup As AjaxControlToolkit.ModalPopupExtender)
        '    'myPanel.ID += myPanel.ClientID
        '    'cancelButton.ID += cancelButton.ClientID
        '    modalpopup.PopupControlID = myPanel.ID
        '    parentCtrl.Controls.Add(myPanel)
        'End Sub

        Public Shared Function strFormatDate(ByVal sourceDate As String) As String
            Dim strDate As String
            Dim day As String = sourceDate.Substring(0, 2)
            Dim month As String = sourceDate.Substring(3, 2)
            Dim year As String = sourceDate.Substring(6, 4)
            strDate = year & "/" & month & "/" & day
            Return strDate
        End Function
        Public Shared Sub FillMonthDropDown(ByVal dropDownList As DropDownList, Optional ByVal Lang As Lang = Lang.EN)
            If (dropDownList IsNot Nothing) Then
                Dim lstItm As New ListItem
                CtlCommon.ClearCtlContent(New WebControl() {dropDownList})
                lstItm = Nothing
                For i As Integer = 1 To 12
                    Dim newLstItm As New ListItem
                    With newLstItm
                        .Value = i
                        .Text = MonthName(i, False)
                    End With
                    dropDownList.Items.Add(newLstItm)
                    newLstItm = Nothing
                Next
            End If
        End Sub
        Public Shared Sub FillYearDropDown(ByVal dropDownList As DropDownList, Optional ByVal MinYear As Integer = 1990, Optional ByVal MaxYear As Integer = 2050, Optional ByVal Lang As Lang = Lang.EN)
            If (dropDownList IsNot Nothing) Then
                Dim lstItm As New ListItem
                CtlCommon.ClearCtlContent(New WebControl() {dropDownList})
                lstItm = Nothing
                For i As Integer = MinYear To MaxYear
                    Dim newLstItm As New ListItem
                    With newLstItm
                        .Value = i
                        .Text = i
                    End With
                    dropDownList.Items.Add(newLstItm)
                    newLstItm = Nothing
                Next
            End If
        End Sub

        Public Shared Sub InitializeCulture(ByVal strCulture As String)
            If strCulture <> "" Then
                Thread.CurrentThread.CurrentUICulture = New CultureInfo(strCulture)
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(strCulture)

            End If
        End Sub

        Public Shared Sub FillListBox(ByVal lstBox As ListBox, ByVal dataSource As DataTable)
            If (lstBox IsNot Nothing) AndAlso (dataSource IsNot Nothing) Then
                If lstBox.Items.Count > 0 Then
                    lstBox.Items.Clear()
                End If
                If dataSource.Rows.Count > 0 Then
                    For i As Integer = 0 To dataSource.Rows.Count - 1
                        lstBox.Items.Add(New ListItem(dataSource.Rows(i).Item(0).ToString, dataSource.Rows(i).Item(1).ToString))
                    Next
                    lstBox.SelectedIndex = -1
                End If
            End If
        End Sub

        Public Shared Sub RemoveFromListBox(ByVal lstBox As ListBox, ByVal dataSource As DataTable)
            If (lstBox IsNot Nothing) AndAlso (dataSource IsNot Nothing) Then
                'If lstBox.Items.Count > 0 Then
                '    lstBox.Items.Clear()
                'End If
                If dataSource.Rows.Count > 0 Then
                    For i As Integer = 0 To dataSource.Rows.Count - 1
                        lstBox.Items.Remove(New ListItem(dataSource.Rows(i).Item(0).ToString, dataSource.Rows(i).Item(1).ToString))
                    Next
                    lstBox.SelectedIndex = -1
                End If
            End If
        End Sub
        Public Shared Function NumericDate(ByVal _Date As String) As Integer
            Dim Num_Date As Integer = 0
            Num_Date = Convert.ToInt32(String.Concat(_Date.Substring(6, 4), _Date.Substring(3, 2), _Date.Substring(0, 2)))
            Return Num_Date
        End Function
        Public Shared Function FormatDateString(ByVal strDate As String) As String
            Dim dateValue As Date = Nothing
            dateValue = Date.Parse(strDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))
            Return dateValue.ToString("MM/dd/yyyy")
        End Function
        Public Shared Function FormatDate(ByVal strDate As String) As Date
            Dim dateValue As Date = Nothing
            Try
                dateValue = Date.Parse(strDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-CA"))
            Catch ex As Exception
            End Try
            Return dateValue
        End Function
        Public Shared Sub SetCulture(ByRef CurrentThrd As Thread)
            CurrentThrd.CurrentCulture = New CultureInfo(SessionVariables.CultureInfo)
            CurrentThrd.CurrentUICulture = New CultureInfo(SessionVariables.CultureInfo)
        End Sub
        Public Shared Function getCulture() As String
            Return SessionVariables.CultureInfo.Substring(0, 2).ToUpper()
        End Function





        ''' <summary>
        ''' Utility to fill Grid View using DataSet
        ''' </summary>
        ''' <param name="strKeyNames">Primary-Key Fields Name</param>
        ''' <remarks></remarks>
        Public Shared Sub FillRadGridView(ByVal RadGrid As RadGrid, ByVal dataSet As DataSet, Optional ByVal strKeyNames() As String = Nothing)
            'Public Shared Sub FillGridView(ByVal i_gvID As GridView, ByVal i_dataSource As DataSet, ByVal i_dataMember As String, Optional ByVal i_dataKeyNames() As String = Nothing)
            CtlCommon.ClearCtlContent(New WebControl() {RadGrid})
            If (dataSet IsNot Nothing) Then
                If dataSet.Tables.Count > 0 AndAlso dataSet.Tables(0).Rows.Count > 0 Then
                    Try
                        With RadGrid
                            .DataSource = dataSet
                            '.DataMember = i_dataMember
                            'If (strKeyNames IsNot Nothing) Then .= strKeyNames
                            .DataBind()
                            .Visible = True
                        End With
                    Catch ex As Exception
                        'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name)
                        Dim pagePath(1) As String
                        Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                        Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                        pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                        CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                    End Try
                End If
            End If
        End Sub


        ''''''''''''''''''''''''''''''''''''''''''''''''''''


        ''' <summary>
        ''' Utility to fill Grid View using DataTable
        ''' </summary>
        ''' <param name="strKeyNames">Primary-Key Fields Name</param>
        ''' <remarks></remarks>
        Public Shared Sub FillRadGridView(ByVal RadGrid As RadGrid, ByVal dataTable As DataTable, Optional ByVal strKeyNames() As String = Nothing)
            'Public Shared Sub FillGridView(ByVal i_gvID As GridView, ByVal i_dataSource As DataTable, ByVal i_dataMember As String, Optional ByVal i_dataKeyNames() As String = Nothing)
            CtlCommon.ClearCtlContent(New WebControl() {RadGrid})
            If (dataTable IsNot Nothing) Then
                If dataTable.Rows.Count > 0 Then
                    Try
                        With RadGrid
                            .DataSource = dataTable
                            '.DataMember = i_dataMember
                            'If (strKeyNames IsNot Nothing) Then .DataKeyNames = strKeyNames
                            .DataBind()
                            .Visible = True
                        End With
                    Catch ex As Exception
                        'CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name)
                        Dim pagePath(1) As String
                        Dim i As Integer = ex.StackTrace.IndexOf(" in ")
                        Dim lasti As Integer = ex.StackTrace.IndexOf(":line ")
                        pagePath(0) = ex.StackTrace.Substring(i + 3, (lasti + 11) - i)
                        CtlCommon.CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name & " --Page:" & pagePath(0))

                    End Try
                End If
            End If
        End Sub

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Public Shared Sub FillComboBox(ByVal RadComboBox As RadComboBox, ByVal dataSource As DataTable, Optional ByVal Lang As Lang = Lang.EN)
            If (RadComboBox IsNot Nothing) AndAlso (dataSource IsNot Nothing) Then
                Dim lstItm As New RadComboBoxItem

                CtlCommon.ClearCtlContent(New WebControl() {RadComboBox})
                With lstItm
                    .Value = -1
                    If Lang = Lang.EN Then
                        .Text = "--Please Select--"
                    Else
                        .Text = "--الرجاء الاختيار--"
                    End If
                End With
                RadComboBox.Items.Add(lstItm)
                lstItm = Nothing
                If dataSource.Rows.Count > 0 Then
                    For i As Integer = 0 To dataSource.Rows.Count - 1
                        Dim newLstItm As New RadComboBoxItem
                        With newLstItm
                            .Value = dataSource.Rows(i).Item(0).ToString
                            .Text = dataSource.Rows(i).Item(1).ToString
                        End With
                        RadComboBox.Items.Add(newLstItm)
                        newLstItm = Nothing
                    Next
                End If
            Else
                CtlCommon.ClearCtlContent(New WebControl() {RadComboBox})
                Dim lstItm As New RadComboBoxItem
                With lstItm
                    .Value = -1
                    If Lang = Lang.EN Then
                        .Text = "--Please Select--"
                    Else
                        .Text = "--الرجاء الاختيار--"
                    End If
                End With
                RadComboBox.Items.Add(lstItm)
                lstItm = Nothing
            End If
        End Sub
        ''By Mohammad Shanaa 24-05-2011
        ''' <summary>
        ''' Getting Month Name according to Month Number 
        ''' it get arabic and english name (0 English, 1 Arabic, 2 Hejri)
        ''' </summary>
        ''' <param name="MonthNo">Month Number (starting from 0 - 11)</param>
        ''' <param name="Lang">0 English   ---   1 Arabic    --- 2 Hejri</param>
        ''' <returns>Month Name</returns>
        ''' <remarks></remarks>
        Public Shared Function GetMonthName(ByVal MonthNo As Integer, ByVal Lang As Integer) As String
            Dim strMonthName As String

            Dim allCultures As CultureInfo() = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            Dim LangIndex As Integer = -1
            Dim c As CultureInfo
            For i As Integer = 0 To allCultures.Length - 1
                c = DirectCast(allCultures.GetValue(i), CultureInfo)
                Select Case Lang
                    Case 0
                        If c.IetfLanguageTag = "en-US" Then
                            LangIndex = i
                        End If
                    Case 1
                        If c.IetfLanguageTag = "ar-AE" Then
                            LangIndex = i
                        End If
                    Case 2
                        If c.IetfLanguageTag = "ar-SA" Then
                            LangIndex = i
                        End If
                End Select
                If LangIndex > -1 Then
                    Exit For
                End If
            Next

            If IsNumeric(MonthNo) Then
                Dim mfi As New System.Globalization.DateTimeFormatInfo()
                c = DirectCast(allCultures.GetValue(LangIndex), CultureInfo)
                strMonthName = c.DateTimeFormat.GetMonthName(MonthNo)
            Else
                strMonthName = MonthNo.ToString
            End If

            Return strMonthName

        End Function

        ''By Mohammad shanaa   29/12/2013
        ''Get Time String For masked textbox from minutes ex: 210 min  -> 0330
        Public Shared Function GetFullTimeString(ByVal intMinutes As Integer) As String
            Dim strHour As String = ""
            Dim strMin As String = ""

            Dim intHours As Integer = Math.Floor(intMinutes / 60)
            Dim intMin As Integer = intMinutes Mod 60

            strHour = intHours.ToString.PadLeft(2, "0")
            strMin = intMin.ToString.PadLeft(2, "0")

            Return strHour + ":" + strMin
        End Function

        Public Shared Function ConvertintToTime(ByVal intMinutes As Integer) As DateTime
            Dim strHour As String = ""
            Dim strMin As String = ""
            Dim strTime As String = ""
            Dim intHours As Integer = Math.Floor(intMinutes / 60)
            Dim intMin As Integer = intMinutes Mod 60
            Dim FormattedDate As DateTime
            strHour = intHours.ToString.PadLeft(2, "0")
            strMin = intMin.ToString.PadLeft(2, "0")

            strTime = strHour + ":" + strMin
            FormattedDate = Convert.ToDateTime(strTime)

            Return FormattedDate
        End Function

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        'Public Shared Function CheckValidDateRange(ByVal strFromDate As String, ByVal strToDate As String) As Boolean
        '    If (Date.Compare(strChangeDateFormat(strFromDate), strChangeDateFormat(strToDate))) <= 0 Then
        '        Return True
        '    End If
        '    Return False
        'End Function

        'Public Shared Sub ClearCtlContent(ByVal webControl() As WebControl, Optional ByVal OpMode As OpMode = OpMode.ClearContent)
        '    Try
        '        For Each ctl As WebControl In webControl
        '            If TypeOf ctl Is TextBox Then
        '                DirectCast(ctl, TextBox).Text = String.Empty
        '            ElseIf TypeOf ctl Is Label Then
        '                DirectCast(ctl, Label).Text = String.Empty
        '            ElseIf TypeOf ctl Is CheckBox Then
        '                DirectCast(ctl, CheckBox).Checked = False
        '            ElseIf TypeOf ctl Is CheckBoxList Then
        '                With DirectCast(ctl, CheckBoxList)
        '                    If OpMode = CtlCommon.OpMode.ClearContent Then
        '                        .DataSource = Nothing
        '                        .DataBind()
        '                        If .Items.Count > 0 Then
        '                            .Items.Clear()
        '                        End If
        '                    Else
        '                        For Each chkbx As ListItem In DirectCast(ctl, CheckBoxList).Items
        '                            chkbx.Selected = False
        '                        Next
        '                    End If
        '                End With
        '            ElseIf TypeOf ctl Is RadioButtonList Then
        '                DirectCast(ctl, RadioButtonList).SelectedIndex = 0
        '            ElseIf TypeOf ctl Is DropDownList Then
        '                With DirectCast(ctl, DropDownList)
        '                    If OpMode = OpMode.ClearContent Then
        '                        .DataSource = Nothing
        '                        .DataBind()
        '                        If .Items.Count > 0 Then
        '                            .Items.Clear()
        '                        End If
        '                    Else
        '                        If (.Items.Count > 1) Then
        '                            .SelectedValue = -1
        '                        End If
        '                    End If
        '                End With
        '            ElseIf TypeOf ctl Is RadComboBox Then
        '                With DirectCast(ctl, RadComboBox)
        '                    If OpMode = OpMode.ClearContent Then
        '                        .DataSource = Nothing
        '                        .DataBind()
        '                        If .Items.Count > 0 Then
        '                            .Items.Clear()
        '                        Else
        '                            .SelectedValue = -1
        '                        End If

        '                    Else
        '                        If (.Items.Count > 1) Then
        '                            .SelectedValue = -1

        '                        End If
        '                    End If
        '                End With
        '            ElseIf TypeOf ctl Is GridView Then
        '                With DirectCast(ctl, GridView)
        '                    If OpMode = OpMode.ClearContent Then
        '                        .DataSource = Nothing
        '                        .DataBind()
        '                    Else
        '                        .SelectedIndex = 0
        '                    End If
        '                End With
        '            ElseIf TypeOf ctl Is DataList Then
        '                With DirectCast(ctl, DataList)
        '                    If OpMode = OpMode.ClearContent Then
        '                        .DataSource = Nothing
        '                        .DataBind()
        '                    End If
        '                End With
        '            ElseIf TypeOf ctl Is ListBox Then
        '                With CType(ctl, ListBox)
        '                    If OpMode = OpMode.ResetIndex Then
        '                        .SelectedIndex = 0
        '                    Else
        '                        If (.Items.Count > 0) Then
        '                            .Items.Clear()
        '                        End If
        '                        .DataSource = Nothing
        '                        .DataBind()
        '                    End If
        '                End With
        '            End If
        '        Next
        '    Catch ex As Exception
        '        CreateErrorLog(AppDomain.CurrentDomain.RelativeSearchPath.Substring(0, AppDomain.CurrentDomain.RelativeSearchPath.LastIndexOf("\") + 1), ex.Message, MethodBase.GetCurrentMethod.Name)
        '    Finally
        '        webControl = Nothing
        '    End Try
        'End Sub

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Public Shared Sub MakeFolderWritable(Folder As String)
            If IsFolderReadOnly(Folder) Then
                Dim oDir As New System.IO.DirectoryInfo(Folder)
                oDir.Attributes = oDir.Attributes And Not System.IO.FileAttributes.[ReadOnly]
            End If
        End Sub

        Public Shared Function IsFolderReadOnly(Folder As String) As Boolean
            Dim oDir As New System.IO.DirectoryInfo(Folder)
            Return ((oDir.Attributes And System.IO.FileAttributes.[ReadOnly]) > 0)
        End Function

        Public Shared Sub Open_Download_File(ByVal filename As String, ByVal filePath As String)
            If File.Exists(filePath) Then
                Dim response As HttpResponse = HttpContext.Current.Response
                response.Clear()
                response.Buffer = True
                response.Charset = ""
                response.ContentType = "application/text/plain"
                response.Charset = "UTF-8"
                response.ContentEncoding = System.Text.Encoding.UTF8
                response.AddHeader("Content-Disposition", "attachment;filename=""" & filename)
                response.WriteFile(filePath)
                response.Flush()
                response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble())
                response.[End]()
            Else
                Return
            End If

        End Sub

#Region "Export_DataTable_To_Excel"
        Public Shared Sub ExportDataSetToExcel(ByVal ds As DataTable, ByVal filename As String, Optional ByVal ds2 As DataTable = Nothing)
            Dim response As HttpResponse = HttpContext.Current.Response
            response.Clear()
            response.Buffer = True
            response.Charset = ""
            'response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            response.ContentType = "application/vnd.ms-excel"
            response.AddHeader("Content-Disposition", "attachment;filename=""" & filename & ".xls")

            Using sw As New StringWriter()
                Using htw As New HtmlTextWriter(sw)
                    Dim dg As New DataGrid()
                    Dim dsAll As New DataSet

                    If Not ds2 Is Nothing Then
                        dsAll.Tables.Add(ds2)
                    End If

                    dsAll.Tables.Add(ds)

                    dg.HeaderStyle.BackColor = Drawing.Color.AntiqueWhite
                    dg.HeaderStyle.Font.Bold = True
                    dg.HorizontalAlign = HorizontalAlign.Center

                    For i As Integer = 0 To dsAll.Tables.Count - 1
                        dg.DataSource = dsAll.Tables(i)
                        dg.DataBind()
                        dg.RenderControl(htw)
                    Next

                    'If Not ds2 Is Nothing Then
                    '    dg.Items.Item(0).Visible = False
                    '    dg.Items.Item(1).Visible = False
                    '    dg.DataBind()
                    'End If


                    response.Charset = "UTF-8"
                    response.ContentEncoding = System.Text.Encoding.UTF8
                    response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble())
                    response.Output.Write(sw.ToString())
                    response.[End]()
                End Using
            End Using
        End Sub

        Public Shared Sub ExportDataSetToExcel_WithColor(ByVal ds As DataTable, ByVal filename As String, Optional ByVal ds2 As DataTable = Nothing, Optional ByVal FontColor As Color = Nothing)
            Dim response As HttpResponse = HttpContext.Current.Response
            response.Clear()
            response.Buffer = True
            response.Charset = ""
            'response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            response.ContentType = "application/vnd.ms-excel"
            response.AddHeader("Content-Disposition", "attachment;filename=""" & filename & ".xls")

            Using sw As New StringWriter()
                Using htw As New HtmlTextWriter(sw)
                    Dim dg As New DataGrid()
                    Dim dsAll As New DataSet

                    If Not ds2 Is Nothing Then
                        dsAll.Tables.Add(ds2)
                    End If

                    dsAll.Tables.Add(ds)

                    dg.HeaderStyle.BackColor = Drawing.Color.AntiqueWhite
                    dg.HeaderStyle.Font.Bold = True
                    dg.Font.Size = 16
                    dg.HorizontalAlign = HorizontalAlign.Center

                    For i As Integer = 0 To dsAll.Tables.Count - 1
                        dg.DataSource = dsAll.Tables(i)
                        dg.DataBind()
                        If i = 1 Then
                            For item As Integer = 0 To dg.Items.Count - 1
                                If Convert.ToInt32(dg.Items(item).Cells(5).Text()) > 0 Then
                                    dg.Items(item).Cells(5).ForeColor = FontColor
                                End If
                                If Convert.ToInt32(dg.Items(item).Cells(6).Text()) > 0 Then
                                    dg.Items(item).Cells(6).ForeColor = FontColor
                                End If
                                If Convert.ToInt32(dg.Items(item).Cells(7).Text()) > 0 Then
                                    dg.Items(item).Cells(7).ForeColor = FontColor
                                End If
                                If Convert.ToInt32(dg.Items(item).Cells(8).Text()) > 0 Then
                                    dg.Items(item).Cells(8).ForeColor = FontColor
                                End If
                                If Convert.ToInt32(dg.Items(item).Cells(9).Text()) > 0 Then
                                    dg.Items(item).Cells(9).ForeColor = FontColor
                                End If
                                If Convert.ToInt32(dg.Items(item).Cells(10).Text()) > 0 Then
                                    dg.Items(item).Cells(10).ForeColor = FontColor
                                End If
                                If Convert.ToInt32(dg.Items(item).Cells(11).Text()) > 0 Then
                                    dg.Items(item).Cells(11).ForeColor = FontColor
                                End If
                                If Convert.ToInt32(dg.Items(item).Cells(12).Text()) > 0 Then
                                    dg.Items(item).Cells(12).ForeColor = FontColor
                                End If
                                If Convert.ToInt32(dg.Items(item).Cells(13).Text()) > 0 Then
                                    dg.Items(item).Cells(13).ForeColor = FontColor
                                End If
                                If Convert.ToInt32(dg.Items(item).Cells(14).Text()) > 0 Then
                                    dg.Items(item).Cells(14).ForeColor = FontColor
                                End If
                                dg.Items(item).Cells(15).ForeColor = FontColor

                            Next
                        End If

                        dg.RenderControl(htw)
                    Next

                    'If Not ds2 Is Nothing Then
                    '    dg.Items.Item(0).Visible = False
                    '    dg.Items.Item(1).Visible = False
                    '    dg.DataBind()
                    'End If


                    response.Charset = "UTF-8"
                    response.ContentEncoding = System.Text.Encoding.UTF8
                    response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble())
                    response.Output.Write(sw.ToString())
                    response.[End]()
                End Using
            End Using
        End Sub

        Public Shared Sub ExportMultipleDataSetToExcel(ByVal ds As DataSet, ByVal filename As String)
            Dim response As HttpResponse = HttpContext.Current.Response
            response.Clear()
            response.Buffer = True
            response.Charset = ""
            'response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            response.ContentType = "application/vnd.ms-excel"
            response.AddHeader("Content-Disposition", "attachment;filename=""" & filename & ".xls")

            Using sw As New StringWriter()
                Using htw As New HtmlTextWriter(sw)
                    Dim dg As New DataGrid()
                    Dim dsAll As New DataSet
                    dsAll = ds

                    dg.HeaderStyle.BackColor = Drawing.Color.AntiqueWhite
                    dg.HeaderStyle.Font.Bold = True
                    dg.HorizontalAlign = HorizontalAlign.Center

                    For i As Integer = 0 To dsAll.Tables.Count - 1
                        dg.DataSource = dsAll.Tables(i)
                        dg.DataBind()
                        dg.RenderControl(htw)
                    Next

                    response.Charset = "UTF-8"
                    response.ContentEncoding = System.Text.Encoding.UTF8
                    response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble())
                    response.Output.Write(sw.ToString())
                    response.[End]()
                End Using
            End Using
        End Sub


        'Public Shared Sub ExportMultipleDataSetToExcelSheets(ByVal ds As DataTable, ByVal filename As String, Optional ByVal ds2 As DataTable = Nothing)
        '    Dim missing As Object = Type.Missing
        '    Dim xlApp As Excel.Application
        '    Dim xlWorkBook As Excel.Workbook
        '    Dim xlWorkSheet As Excel.Worksheet

        '    xlApp = New Excel.ApplicationClass

        '    Dim dsAll As New DataSet

        '    If Not ds2 Is Nothing Then
        '        dsAll.Tables.Add(ds2)
        '    End If

        '    dsAll.Tables.Add(ds)

        '    For i As Integer = 0 To dsAll.Tables.Count - 1
        '        xlWorkSheet.Name = dsAll.Tables(1).Rows(i)("EmployeeName")
        '        Dim excelCellrange = xlWorkSheet.Range(xlWorkSheet.Cells(1, 1), xlWorkSheet.Cells(xlWorkSheet.Rows.Count, dsAll.Tables(0).Columns.Count))
        '        excelCellrange.EntireColumn.AutoFit()
        '    Next
        '    xlWorkBook.SaveAs("E:\Exported File.xlsx")
        '    xlWorkBook.Close()
        '    xlApp.Quit()
        'End Sub


#End Region

        Public Shared Function ConvertDataTableToList(Of T)(ByVal dt As DataTable) As List(Of T)
            Dim data As List(Of T) = New List(Of T)()

            For Each row As DataRow In dt.Rows
                Dim item As T = GetItem(Of T)(row)
                data.Add(item)
            Next

            Return data
        End Function

        Public Shared Function GetItem(Of T)(ByVal dr As DataRow) As T
            Dim temp As Type = GetType(T)
            Dim obj As T = Activator.CreateInstance(Of T)()

            For Each column As DataColumn In dr.Table.Columns

                For Each pro As PropertyInfo In temp.GetProperties()

                    If pro.Name = column.ColumnName Then
                        pro.SetValue(obj, dr(column.ColumnName), Nothing)
                    Else
                        Continue For
                    End If
                Next
            Next

            Return obj
        End Function

#End Region

    End Class

End Namespace
