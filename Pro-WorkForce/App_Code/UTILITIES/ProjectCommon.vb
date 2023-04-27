Imports Microsoft.VisualBasic

Imports Telerik.Web.UI
Imports System.Data


Imports System.Web.UI
Imports System.Web.UI.WebControls


Namespace SmartV.UTILITIES

    Public NotInheritable Class ProjectCommon
        Public Enum OpMode
            ClearContent
            ResetIndex
            ResetTimeContent
        End Enum


        ' Thid class is designed and implemented by Omar Adnan to 
        ' add scalability to TA AIS ptroject

#Region "Class Variables"
        ' DataTable to store all the records to display at the combo box
        Private dtCustomizedRecordsOrder As New DataTable
        Private cmb As RadComboBox
        Private ddl As DropDownList
        Private dtDataSource As DataTable
        Private valField As String
        Private EngNameTextField As String
        Private ArNameTextField As String
        Private ParentField As String
        Private Sequence As String
#End Region
#Region "Region : Fill terlerik combo boxes"

#Region "Region : Fill multi level combo box"


        Public Sub FillMultiLevelRadComboBox(ByVal cmb As RadComboBox, ByVal dtDataSource As DataTable, _
                                             ByVal valField As String, ByVal EngNameTextField As String, _
                                             ByVal ArNameTextField As String, ByVal ParentField As String, _
                                             Optional ByVal OrderByField As String = Nothing)
            ' Processing : filling multilevel localized RadComboBox
            If dtDataSource IsNot Nothing Then

                cmb.Items.Clear()

                If SessionVariables.CultureInfo = "en-US" Then
                    cmb.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem("--Please Select--", -1))
                Else
                    cmb.Items.Insert(0, New Telerik.Web.UI.RadComboBoxItem("--الرجاء الاختيار--", -1))
                End If


                Try
                    Me.dtDataSource = dtDataSource
                    Me.cmb = cmb
                    Me.valField = valField
                    Me.EngNameTextField = EngNameTextField
                    Me.ArNameTextField = ArNameTextField
                    Me.ParentField = ParentField
                    dtCustomizedRecordsOrder.Columns.Add(New DataColumn(valField))
                    dtCustomizedRecordsOrder.Columns.Add(New DataColumn(EngNameTextField))
                    dtCustomizedRecordsOrder.Columns.Add(New DataColumn(ArNameTextField))
                    dtCustomizedRecordsOrder.Columns.Add(New DataColumn(ParentField))
                    Dim drChilds As DataRow() = Nothing

                    If OrderByField Is Nothing Then
                        Dim drRoots() As DataRow = dtDataSource.Select(ParentField + " is null")
                        addNode(drRoots, drChilds, (drRoots.Count - 1), 0)
                    Else
                        ' Code to display items at the drop down list
                        ' grouped by the parametrized field
                        Me.Sequence = OrderByField
                        Dim drRoots() As DataRow = dtDataSource.Select(ParentField + " is null", Me.Sequence & " DESC")
                        addNodeOrderBy(drRoots, drChilds, (drRoots.Count - 1), 0)
                    End If
                Catch ex As Exception
                    ' You can warn the developer to send the correct parameters .For example , 
                    ' check the fields names and/or the correct data source , to avoid the 
                    ' pain of the exception

                End Try
            Else
                ' Do nothing , you can show warning message
            End If
        End Sub

        Public Sub FillMultiLevelDropDownList(ByVal ddl As DropDownList, ByVal dtDataSource As DataTable, _
                                             ByVal valField As String, ByVal EngNameTextField As String, _
                                             ByVal ArNameTextField As String, ByVal ParentField As String, _
                                             Optional ByVal OrderByField As String = Nothing)
            ' Processing : filling multilevel localized RadComboBox
            If dtDataSource IsNot Nothing Then

                ddl.Items.Clear()

                ddl.Items.Insert(0, New ListItem("--Please Select--", -1))

                Try
                    Me.dtDataSource = dtDataSource
                    Me.ddl = ddl
                    Me.valField = valField
                    Me.EngNameTextField = EngNameTextField
                    Me.ArNameTextField = ArNameTextField
                    Me.ParentField = ParentField
                    dtCustomizedRecordsOrder.Columns.Add(New DataColumn(valField))
                    dtCustomizedRecordsOrder.Columns.Add(New DataColumn(EngNameTextField))
                    dtCustomizedRecordsOrder.Columns.Add(New DataColumn(ArNameTextField))
                    dtCustomizedRecordsOrder.Columns.Add(New DataColumn(ParentField))
                    Dim drChilds As DataRow() = Nothing

                    If OrderByField Is Nothing Then
                        Dim drRoots() As DataRow = dtDataSource.Select(ParentField + " is null")
                        addDropDownListNode(drRoots, drChilds, (drRoots.Count - 1), 0)
                    Else
                        ' Code to display items at the drop down list
                        ' grouped by the parametrized field
                        Me.Sequence = OrderByField
                        Dim drRoots() As DataRow = dtDataSource.Select(ParentField + " is null", Me.Sequence & " DESC")
                        addNodeOrderBy(drRoots, drChilds, (drRoots.Count - 1), 0)
                    End If
                Catch ex As Exception
                    ' You can warn the developer to send the correct parameters .For example , 
                    ' check the fields names and/or the correct data source , to avoid the 
                    ' pain of the exception

                End Try
            Else
                ' Do nothing , you can show warning message
            End If
        End Sub

        Private Sub addDropDownListNode(ByVal drRoot() As DataRow, _
                            ByVal foundChilds As DataRow(), _
                            ByVal count As Integer, _
                            ByVal levelNo As Integer)
            If count >= 0 Then
                If levelNo = 0 Then
                    ' Get the current root 
                    Dim dr As DataRow = drRoot(count)
                    dtCustomizedRecordsOrder.Rows.Add(dr(valField), dr(EngNameTextField), dr(ArNameTextField), dr(ParentField))
                    ' Check if the root has childs , if exist get ordered 
                    ' by Sequence
                    foundChilds = dtDataSource.Select(ParentField + "=" & _
                                         dr(valField))
                    If foundChilds.Count <> 0 Then
                        ' Increase the level to add node on higher 
                        ' level
                        levelNo = levelNo + 1
                        addDropDownListNode(drRoot, foundChilds, count, levelNo)
                        ' Return to add at lower level
                        levelNo = levelNo - 1
                    End If
                    count = count - 1
                    addDropDownListNode(drRoot, _
                        foundChilds, _
                        count, _
                        levelNo)
                Else
                    For Each row As DataRow In foundChilds
                        ' Prepare the Id and the name of the child 
                        Dim id As Integer = row(valField)
                        Dim EngName As String = row(EngNameTextField)
                        Dim ArName As String = row(ArNameTextField)
                        Dim ParentId As String = row(ParentField)
                        ' Add the new child
                        dtCustomizedRecordsOrder.Rows.Add(id, getPrefix(levelNo) & EngName, _
                                                          getPrefix(levelNo) & ArName, ParentId)

                        ' Check if the child have sub-childs , if exist will return
                        ' Order By Sequence number

                        Dim childs As DataRow() = _
                            dtDataSource.Select(ParentField & "=" & id)
                        If childs.Length <> 0 Then
                            levelNo = levelNo + 1
                            addDropDownListNode(drRoot, childs, count, levelNo)
                            levelNo = levelNo - 1
                        End If
                    Next
                End If
            Else
                Me.ddl.DataSource = dtCustomizedRecordsOrder
                setDropDownListLocalizedTextField(Me.ddl, Me.EngNameTextField, Me.ArNameTextField)
                Me.ddl.DataValueField = valField
                Me.ddl.DataBind()
            End If
        End Sub

        Private Sub addDropDownListNodeOrderBy(ByVal drRoot() As DataRow, _
                            ByVal foundChilds As DataRow(), _
                            ByVal count As Integer, _
                            ByVal levelNo As Integer)
            If count >= 0 Then
                If levelNo = 0 Then
                    ' Get the current root 
                    Dim dr As DataRow = drRoot(count)
                    dtCustomizedRecordsOrder.Rows.Add(dr(valField), dr(EngNameTextField), dr(ArNameTextField), dr(ParentField))
                    ' Check if the root has childs , if exist get ordered 
                    ' by Sequence
                    foundChilds = dtDataSource.Select(ParentField + "=" & _
                                         dr(valField), Sequence & " DESC")

                    If foundChilds.Count <> 0 Then
                        ' Increase the level to add node on higher 
                        ' level
                        levelNo = levelNo + 1
                        addDropDownListNodeOrderBy(drRoot, foundChilds, count, levelNo)
                        ' Return to add at lower level
                        levelNo = levelNo - 1
                    End If
                    count = count - 1
                    addDropDownListNodeOrderBy(drRoot, _
                        foundChilds, _
                        count, _
                        levelNo)
                Else
                    For Each row As DataRow In foundChilds
                        ' Prepare the Id and the name of the child 
                        Dim id As Integer = row(valField)
                        Dim EngName As String = row(EngNameTextField)
                        Dim ArName As String = row(ArNameTextField)
                        Dim ParentId As String = row(ParentField)
                        ' Add the new child
                        dtCustomizedRecordsOrder.Rows.Add(id, getPrefix(levelNo) & EngName, _
                                                          getPrefix(levelNo) & ArName, ParentId)
                        ' Check if the child have sub-childs , if exist will return
                        ' Order By Sequence number
                        Dim childs As DataRow() = _
                            dtDataSource.Select(ParentField & "=" & id, Sequence & " DESC")
                        If childs.Length <> 0 Then
                            levelNo = levelNo + 1
                            addDropDownListNodeOrderBy(drRoot, childs, count, levelNo)
                            levelNo = levelNo - 1
                        End If
                    Next
                End If
            Else
                Me.ddl.DataSource = dtCustomizedRecordsOrder
                setDropDownListLocalizedTextField(Me.ddl, Me.EngNameTextField, Me.ArNameTextField)
                Me.ddl.DataValueField = valField
                Me.ddl.DataBind()
            End If

        End Sub

        Private Sub addNode(ByVal drRoot() As DataRow, _
                            ByVal foundChilds As DataRow(), _
                            ByVal count As Integer, _
                            ByVal levelNo As Integer)
            If count >= 0 Then
                If levelNo = 0 Then
                    ' Get the current root 
                    Dim dr As DataRow = drRoot(count)
                    dtCustomizedRecordsOrder.Rows.Add(dr(valField), dr(EngNameTextField), dr(ArNameTextField), dr(ParentField))
                    ' Check if the root has childs , if exist get ordered 
                    ' by Sequence
                    foundChilds = dtDataSource.Select(ParentField + "=" & _
                                         dr(valField))
                    If foundChilds.Count <> 0 Then
                        ' Increase the level to add node on higher 
                        ' level
                        levelNo = levelNo + 1
                        addNode(drRoot, foundChilds, count, levelNo)
                        ' Return to add at lower level
                        levelNo = levelNo - 1
                    End If
                    count = count - 1
                    addNode(drRoot, _
                        foundChilds, _
                        count, _
                        levelNo)
                Else
                    For Each row As DataRow In foundChilds
                        ' Prepare the Id and the name of the child 
                        Dim id As Integer = row(valField)
                        Dim EngName As String = row(EngNameTextField)
                        Dim ArName As String = row(ArNameTextField)
                        Dim ParentId As String = row(ParentField)
                        ' Add the new child
                        dtCustomizedRecordsOrder.Rows.Add(id, getPrefix(levelNo) & EngName, _
                                                          getPrefix(levelNo) & ArName, ParentId)

                        ' Check if the child have sub-childs , if exist will return
                        ' Order By Sequence number

                        Dim childs As DataRow() = _
                            dtDataSource.Select(ParentField & "=" & id)
                        If childs.Length <> 0 Then
                            levelNo = levelNo + 1
                            addNode(drRoot, childs, count, levelNo)
                            levelNo = levelNo - 1
                        End If
                    Next
                End If
            Else
                Me.cmb.DataSource = dtCustomizedRecordsOrder
                setLocalizedTextField(Me.cmb, Me.EngNameTextField, Me.ArNameTextField)
                Me.cmb.DataValueField = valField
                Me.cmb.DataBind()
            End If
        End Sub

        Private Sub addNodeOrderBy(ByVal drRoot() As DataRow, _
                            ByVal foundChilds As DataRow(), _
                            ByVal count As Integer, _
                            ByVal levelNo As Integer)
            If count >= 0 Then
                If levelNo = 0 Then
                    ' Get the current root 
                    Dim dr As DataRow = drRoot(count)
                    dtCustomizedRecordsOrder.Rows.Add(dr(valField), dr(EngNameTextField), dr(ArNameTextField), dr(ParentField))
                    ' Check if the root has childs , if exist get ordered 
                    ' by Sequence
                    foundChilds = dtDataSource.Select(ParentField + "=" & _
                                         dr(valField), Sequence & " DESC")

                    If foundChilds.Count <> 0 Then
                        ' Increase the level to add node on higher 
                        ' level
                        levelNo = levelNo + 1
                        addNodeOrderBy(drRoot, foundChilds, count, levelNo)
                        ' Return to add at lower level
                        levelNo = levelNo - 1
                    End If
                    count = count - 1
                    addNodeOrderBy(drRoot, _
                        foundChilds, _
                        count, _
                        levelNo)
                Else
                    For Each row As DataRow In foundChilds
                        ' Prepare the Id and the name of the child 
                        Dim id As Integer = row(valField)
                        Dim EngName As String = row(EngNameTextField)
                        Dim ArName As String = row(ArNameTextField)
                        Dim ParentId As String = row(ParentField)
                        ' Add the new child
                        dtCustomizedRecordsOrder.Rows.Add(id, getPrefix(levelNo) & EngName, _
                                                          getPrefix(levelNo) & ArName, ParentId)
                        ' Check if the child have sub-childs , if exist will return
                        ' Order By Sequence number
                        Dim childs As DataRow() = _
                            dtDataSource.Select(ParentField & "=" & id, Sequence & " DESC")
                        If childs.Length <> 0 Then
                            levelNo = levelNo + 1
                            addNodeOrderBy(drRoot, childs, count, levelNo)
                            levelNo = levelNo - 1
                        End If
                    Next
                End If
            Else
                Me.cmb.DataSource = dtCustomizedRecordsOrder
                setLocalizedTextField(Me.cmb, Me.EngNameTextField, Me.ArNameTextField)
                Me.cmb.DataValueField = valField
                Me.cmb.DataBind()
            End If

        End Sub

        Public Function getPrefix(ByVal level As Integer) As String
            ' Generate strings identify a class at a level
            Dim strPrefix As New StringBuilder
            For index As Integer = 0 To level
                strPrefix.Append("-")
            Next
            Return strPrefix.ToString()
        End Function
        Private Sub setLocalizedTextField(ByVal comb As RadComboBox, _
                           ByVal EnName As String, ByVal ArName As String)
            comb.DataTextField = IIf(SessionVariables.CultureInfo = "en-US", _
                                                       EnName, ArName)
        End Sub
        Private Sub setDropDownListLocalizedTextField(ByVal ddlist As DropDownList, _
                          ByVal EnName As String, ByVal ArName As String)
            ddlist.DataTextField = IIf(SessionVariables.CultureInfo = "en-US", _
                                                       EnName, ArName)
        End Sub
#End Region
        Public Shared Sub FillRadComboBox(ByVal RadComboBox As RadComboBox, _
                                          ByVal dataSource As DataTable, _
                                          ByVal EnTextFiled As String, _
                                          ByVal ArTextFiled As String)


            Try
                ' This method is enhancement of the method FillComboBox at the CtlCommon 
                ' class , this class intended to add localization features


                If RadComboBox Is Nothing Then
                    ' You can show a reasonable message
                    ' exit from the procedure
                    Return
                End If

                ' Add the first item is shared either data source is empty or not 
                Dim lstItm As New RadComboBoxItem
                ' Clear content of RadcomboBox
                CtlCommon.ClearCtlContent(New WebControl() {RadComboBox})

                ' Text field of the data source to be binded to the drop down list
                Dim TextFiled As String = Nothing

                With lstItm
                    .Value = -1

                    If SessionVariables.CultureInfo = "en-US" Then
                        .Text = "--Please Select--"
                        TextFiled = EnTextFiled
                    Else
                        .Text = "--الرجاء الاختيار--"
                        TextFiled = ArTextFiled
                    End If
                End With
                If (dataSource IsNot Nothing) Then
                    RadComboBox.Items.Add(lstItm)
                    lstItm = Nothing
                    If dataSource.Rows.Count > 0 Then
                        For i As Integer = 0 To dataSource.Rows.Count - 1
                            Dim newLstItm As New RadComboBoxItem
                            With newLstItm
                                .Value = dataSource.Rows(i).Item(0).ToString
                                .Text = dataSource.Rows(i).Item(TextFiled).ToString
                            End With
                            RadComboBox.Items.Add(newLstItm)
                            newLstItm = Nothing
                        Next
                    End If
                Else
                    ' if data source is nothing


                    RadComboBox.Items.Add(lstItm)
                    lstItm = Nothing
                End If
            Catch ex As Exception
                ' You can show meaningfull message
            End Try


        End Sub

        Public Shared Sub FillRadComboBox(ByVal RadComboBox As RadComboBox, _
                                          ByVal dataSource As DataTable, _
                                          ByVal EnTextFiled As String, _
                                          ByVal ArTextFiled As String, _
                                          ByVal valField As String, _
                                          Optional ByVal NoValue As Boolean = False)


            Try
                ' This method is enhancement of the method FillComboBox at the CtlCommon 
                ' class , this class intended to add localization features


                If RadComboBox Is Nothing Then
                    ' You can show a reasonable message
                    ' exit from the procedure
                    Return
                End If

                ' Add the first item is shared either data source is empty or not 
                Dim lstItm As New RadComboBoxItem
                ' Clear content of RadcomboBox
                CtlCommon.ClearCtlContent(New WebControl() {RadComboBox})

                ' Text field of the data source to be binded to the drop down list
                Dim TextFiled As String = Nothing

                With lstItm
                    .Value = -1
                    If SessionVariables.CultureInfo = "en-US" Then
                        .Text = "--Please Select--"
                        TextFiled = EnTextFiled
                    Else
                        .Text = "--الرجاء الاختيار--"
                        TextFiled = ArTextFiled
                    End If
                    RadComboBox.Items.Add(lstItm)
                End With

                If NoValue = True Then
                    lstItm = New RadComboBoxItem()
                    lstItm.Value = -2
                    If SessionVariables.CultureInfo = "en-US" Then
                        lstItm.Text = "NA"
                    Else
                        lstItm.Text = "لا إختيار"
                    End If
                    RadComboBox.Items.Add(lstItm)
                End If
                If (dataSource IsNot Nothing) Then

                    lstItm = Nothing
                    If dataSource.Rows.Count > 0 Then
                        For i As Integer = 0 To dataSource.Rows.Count - 1
                            Dim newLstItm As New RadComboBoxItem
                            With newLstItm
                                .Value = dataSource.Rows(i).Item(valField).ToString
                                .Text = dataSource.Rows(i).Item(TextFiled).ToString
                            End With
                            RadComboBox.Items.Add(newLstItm)
                            newLstItm = Nothing
                        Next
                    End If
                Else
                    ' if data source is nothing
                    RadComboBox.Items.Add(lstItm)
                    lstItm = Nothing
                End If
            Catch ex As Exception
                ' You can show meaningfull message
            End Try
        End Sub
#End Region
#Region "Show common result message"
        Public Enum CodeResultMessage
            CodeSaveSucess
            CodeDeleteSucess
            CodeSaveFail
            CodeDeleteFail
            CodeUpdateSucess
            CodeUpdateFail
            CodeAlredyExist
        End Enum

        Public Shared Sub showResultMessage(ByVal page As Page, ByVal errCode As CodeResultMessage)
            ' This method to facilitate the disply of script 
            ' messages related to the DML commands
            If SessionVariables.CultureInfo = "en-US" Then
                Select Case errCode
                    Case CodeResultMessage.CodeSaveSucess
                        CtlCommon.ShowMessage(page, "Saved Successfully", "success")
                    Case CodeResultMessage.CodeUpdateSucess
                        CtlCommon.ShowMessage(page, "Edited Successfully", "success")
                    Case CodeResultMessage.CodeDeleteSucess
                        CtlCommon.ShowMessage(page, "Deleted Successfully", "success")
                    Case CodeResultMessage.CodeSaveFail
                        CtlCommon.ShowMessage(page, "Error While Saving", "error")
                    Case CodeResultMessage.CodeUpdateFail
                        CtlCommon.ShowMessage(page, "Error While Editing", "error")
                    Case CodeResultMessage.CodeDeleteFail
                        CtlCommon.ShowMessage(page, "Error While Deleting", "error")
                    Case CodeResultMessage.CodeAlredyExist
                        CtlCommon.ShowMessage(page, "Item Alredy Exist", "info")
                End Select
            Else
                Select Case errCode
                    Case CodeResultMessage.CodeSaveSucess
                        CtlCommon.ShowMessage(page, "تم الحفظ بنجاح", "success")
                    Case CodeResultMessage.CodeUpdateSucess
                        CtlCommon.ShowMessage(page, "تم التعديل بنجاح", "success")
                    Case CodeResultMessage.CodeDeleteSucess
                        CtlCommon.ShowMessage(page, "تم الحذف بنجاح", "success")
                    Case CodeResultMessage.CodeSaveFail
                        CtlCommon.ShowMessage(page, "خطأ أثناء الحفظ", "error")
                    Case CodeResultMessage.CodeUpdateFail
                        CtlCommon.ShowMessage(page, "خطأ أثناء التعديل", "error")
                    Case CodeResultMessage.CodeDeleteFail
                        CtlCommon.ShowMessage(page, "خطأ أثناء الحذف", "error")
                    Case CodeResultMessage.CodeAlredyExist
                        CtlCommon.ShowMessage(page, "العنصر المراد إضافته موجود", "info")
                End Select
            End If
        End Sub
#End Region




        'Public Shared Sub ClearAjaxTollkitContainer(ByVal tabContainer As AjaxControlToolkit.TabContainer, Optional ByVal OpMode As OpMode = OpMode.ClearContent)

        '    ' Not Working 
        '    ' still under construction !!!

        '    ' This method is enhancement of Clear methods at ctlCommon class 
        '    ' to clear all the controls at the AJAX Toolkit TabContainer

        '    For Each control As Object In tabContainer.Controls

        '        If TypeOf control Is AjaxControlToolkit.TabPanel Then
        '            Dim tabPanel As AjaxControlToolkit.TabPanel = _
        '                CType(control, AjaxControlToolkit.TabPanel)
        '            For Each ctl As Object In tabPanel.Controls
        '                ' Clear Controls at a tab
        '                If TypeOf ctl Is TextBox Then
        '                    DirectCast(ctl, TextBox).Text = String.Empty
        '                ElseIf TypeOf ctl Is Label Then
        '                    DirectCast(ctl, Label).Text = String.Empty
        '                ElseIf TypeOf ctl Is RadNumericTextBox Then
        '                    DirectCast(ctl, RadNumericTextBox).Text = String.Empty
        '                ElseIf TypeOf ctl Is RadTextBox Then
        '                    DirectCast(ctl, RadTextBox).Text = String.Empty
        '                ElseIf TypeOf ctl Is CheckBox Then
        '                    DirectCast(ctl, CheckBox).Checked = False
        '                ElseIf TypeOf ctl Is CheckBoxList Then
        '                    With DirectCast(ctl, CheckBoxList)
        '                        If OpMode = CtlCommon.OpMode.ClearContent Then
        '                            .DataSource = Nothing
        '                            .DataBind()
        '                            If .Items.Count > 0 Then
        '                                .Items.Clear()
        '                            End If
        '                        Else
        '                            For Each chkbx As ListItem In DirectCast(ctl, CheckBoxList).Items
        '                                chkbx.Selected = False
        '                            Next
        '                        End If
        '                    End With
        '                ElseIf TypeOf ctl Is RadioButtonList Then
        '                    DirectCast(ctl, RadioButtonList).SelectedIndex = 0
        '                ElseIf TypeOf ctl Is DropDownList Then
        '                    With DirectCast(ctl, DropDownList)
        '                        If OpMode = OpMode.ClearContent Then
        '                            .DataSource = Nothing
        '                            .DataBind()
        '                            If .Items.Count > 0 Then
        '                                .Items.Clear()
        '                            End If
        '                        Else
        '                            If (.Items.Count > 1) Then
        '                                .SelectedValue = -1
        '                            End If
        '                        End If
        '                    End With
        '                ElseIf TypeOf ctl Is RadComboBox Then
        '                    With DirectCast(ctl, RadComboBox)
        '                        If OpMode = OpMode.ClearContent Then
        '                            .DataSource = Nothing
        '                            .DataBind()
        '                            If .Items.Count >= 0 Then
        '                                .Items.Clear()
        '                            Else
        '                                .SelectedValue = -1
        '                            End If

        '                        Else
        '                            If (.Items.Count > 1) Then
        '                                .SelectedValue = -1

        '                            End If
        '                        End If
        '                    End With
        '                ElseIf TypeOf ctl Is GridView Then
        '                    With DirectCast(ctl, GridView)
        '                        If OpMode = OpMode.ClearContent Then
        '                            .DataSource = Nothing
        '                            .DataBind()
        '                        Else
        '                            .SelectedIndex = 0
        '                        End If
        '                    End With
        '                ElseIf TypeOf ctl Is DataList Then
        '                    With DirectCast(ctl, DataList)
        '                        If OpMode = OpMode.ClearContent Then
        '                            .DataSource = Nothing
        '                            .DataBind()
        '                        End If
        '                    End With
        '                ElseIf TypeOf ctl Is ListBox Then
        '                    With CType(ctl, ListBox)
        '                        If OpMode = OpMode.ResetIndex Then
        '                            .SelectedIndex = 0
        '                        Else
        '                            If (.Items.Count > 0) Then
        '                                .Items.Clear()
        '                            End If
        '                            .DataSource = Nothing
        '                            .DataBind()
        '                        End If
        '                    End With
        '                End If
        '            Next ctl
        '        End If

        '    Next control

        'End Sub



    End Class
End Namespace
