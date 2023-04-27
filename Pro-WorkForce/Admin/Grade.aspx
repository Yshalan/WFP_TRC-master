<%@ Page Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Grade.aspx.vb" Inherits="Admin_Emp_Grade" Title="Define Grades" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function openRadWin() {

            var cmbvalue = $find('<%=CmbOvertimeRule.ClientID%>')._value;
            if (cmbvalue > 0) {
                oWindow = radopen("OvertimeRulesPopUp.aspx?RuleId=" + cmbvalue, "RadWindow1");
            }
        }

        function hideValidatorCalloutTab() {
            try {
                if (AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout != null) {
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.hide();


                }
            }
            catch (err) {
            }
            return false;
        }


        function CheckIsAnual(chk) {
            var gridView = $find("<%= dgrdLeavesTypes.ClientID %>").get_masterTableView().get_element().tBodies[0];
            if (chk.checked == true) {
                var Row_Index = chk.parentNode.parentNode.parentNode.rowIndex;
                if (chk.parentNode.parentNode.parentNode.childNodes[7].getElementsByTagName("input")(0).checked == true) {
                    for (i = 0; i < gridView.rows.length; i++) {
                        if (i + 1 != Row_Index) {
                            if (gridView.rows[i].cells[7].getElementsByTagName("input")(0).checked == true) {
                                gridView.rows[i].cells[0].getElementsByTagName("input")(0).checked = false;
                            }
                        }
                    }
                }
            }


        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <uc1:PageHeader ID="PageHeader1" runat="server" />

            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab"
                meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel ID="Tab1" runat="server" HeaderText="Grade" meta:resourcekey="Tab1Resource1">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="lblGradeCode" runat="server" Text="Grade Code"
                                    meta:resourcekey="lblGradeCodeResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="TxtGradeCode" runat="server" meta:resourcekey="TxtGradeCodeResource1"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="reqGradeCode" runat="server" ControlToValidate="TxtGradeCode"
                                    Display="None" ErrorMessage="Please Enter a Grade Code" ValidationGroup="Grp1"
                                    meta:resourcekey="reqGradeCodeResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ExtenderreqGradeCode" runat="server" Enabled="True"
                                    TargetControlID="reqGradeCode" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="lblGradeEnName" runat="server" Text="English Name"
                                    meta:resourcekey="lblGradeEnNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="TxtGradeName" runat="server" meta:resourcekey="TxtGradeNameResource1"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="reqGradeName" runat="server" ControlToValidate="TxtGradeName"
                                    Display="None" ErrorMessage="Please Enter a Grade Name" ValidationGroup="Grp1"
                                    meta:resourcekey="reqGradeNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ExtenderreqGradeName" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="reqGradeName" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="lblGradeArName" runat="server" Text="Arabic Name"
                                    meta:resourcekey="lblGradeArNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtGradeArName" runat="server" meta:resourcekey="txtGradeArNameResource1"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="reqGradeArName" runat="server" ControlToValidate="txtGradeArName"
                                    Display="None" ErrorMessage="Please Enter grade arabic name" ValidationGroup="Grp1"
                                    meta:resourcekey="reqGradeArNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ExtenderGradeArName" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="reqGradeArName" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <%--<tr id="trAnnualLeaveBalance" runat="server">
                                            <td runat="server">
                                                <asp:Label CssClass="Profiletitletxt" ID="lblAnnualLeaveBalance" runat="server" Text="Annual Leave Balance"
                                                    meta:resourcekey="lblAnnualLeaveBalanceResource1"></asp:Label>
                                            </td>
                                            <td runat="server">
                                                <telerik:RadNumericTextBox ID="TxtAnnualLeaveBalance" MinValue="0" MaxValue="365"
                                                    Skin="Vista" runat="server" Culture="English (United States)" LabelCssClass="">
                                                    <NumberFormat DecimalDigits="2" GroupSeparator="" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td runat="server">
                                                &nbsp;
                                            </td>
                                        </tr>--%>
                        <div class="row" id="trOverTimeRule" runat="server">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="lblOverTime" runat="server" Text="Overtime Rule Name"
                                    meta:resourcekey="lblOverTimeResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="CmbOvertimeRule" runat="server" MarkFirstMatch="True" Skin="Vista">
                                </telerik:RadComboBox>
                                <a href="#" onclick="openRadWin();">
                                    <asp:Literal ID="Literal1" runat="server" Text="View Details" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                                <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Default"
                                    EnableShadow="True" InitialBehavior="None">
                                    <Windows>
                                        <telerik:RadWindow ID="RadWindow1" runat="server" Animation="FlyIn" Behavior="Close, Move"
                                            Behaviors="Close, Move" EnableShadow="True" Height="450px" IconUrl="~/images/HeaderWhiteChrome.jpg"
                                            InitialBehavior="None" ShowContentDuringLoad="False" VisibleStatusbar="False"
                                            Skin="Windows7" Width="700px">
                                        </telerik:RadWindow>
                                    </Windows>
                                </telerik:RadWindowManager>

                              <%--  <asp:RequiredFieldValidator ID="reqOvertimeRule" runat="server" ControlToValidate="CmbOvertimeRule"
                                    Display="None" ErrorMessage="Please Select a Overtime Rule Name" InitialValue="--Please Select--"
                                    ValidationGroup="Grp1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ExtenderreqOvertimeRule" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="reqOvertimeRule" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="ChkIsTAException" runat="server" Text="TA Exception" meta:resourcekey="lblTAExceptionResource1" />
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="Tab2" runat="server" HeaderText="Leave Types" meta:resourcekey="Tab2Resource1" Visible="false">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="Label44" runat="server" Text="Leave Type"
                                    meta:resourcekey="Label44Resource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="ddlLeaveTypes" runat="server" meta:resourcekey="ddlLeaveTypesResource1">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlLeaveTypes"
                                    Display="None" ErrorMessage="Please Select Leave Type" InitialValue="--Please Select--"
                                    ValidationGroup="groupAdd" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator1" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                                <div class="col-md-1">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button" ValidationGroup="groupAdd"
                                        meta:resourcekey="btnAddResource1" />
                                </div>
                                <div class="col-md-1">
                                    <asp:Button ID="btnRemove" runat="server" CssClass="button" Text="Remove" meta:resourcekey="btnRemoveResource1" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadGrid ID="dgrdLeavesTypes" PageSize="15" runat="server" AllowPaging="True"
                                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True"
                                    meta:resourcekey="dgrdLeavesTypesResource1">
                                    <SelectedItemStyle ForeColor="Maroon" />
                                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="LeaveID">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                                UniqueName="TemplateColumn">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="LeaveName" SortExpression="LeaveName" HeaderText="Leave Name"
                                                meta:resourcekey="GridBoundColumnResource1" UniqueName="LeaveName" />
                                            <telerik:GridBoundColumn DataField="LeaveArabicName" SortExpression="LeaveArabicName"
                                                HeaderText="Arabic Name" meta:resourcekey="GridBoundColumnResource2" UniqueName="LeaveArabicName" />
                                            <telerik:GridBoundColumn DataField="Balance" SortExpression="Balance" HeaderText="Balance"
                                                meta:resourcekey="GridBoundColumnResource3" UniqueName="Balance" />
                                            <telerik:GridBoundColumn DataField="MonthlyBalancing" SortExpression="MonthlyBalancing"
                                                HeaderText="Monthly Balancing" meta:resourcekey="GridBoundColumnResource4" UniqueName="MonthlyBalancing" />
                                            <telerik:GridBoundColumn DataField="MinDuration" SortExpression="MinDuration" HeaderText="Min Duration"
                                                meta:resourcekey="GridBoundColumnResource5" UniqueName="MinDuration" />
                                            <telerik:GridBoundColumn DataField="MaxDuration" SortExpression="MaxDuration" HeaderText="Max Duration"
                                                meta:resourcekey="GridBoundColumnResource6" UniqueName="MaxDuration" />
                                            <telerik:GridCheckBoxColumn DataField="IsAnnual" SortExpression="IsAnnual" HeaderText="Is Annual"
                                                meta:resourcekey="GridCheckBoxColumnResource1" UniqueName="IsAnnual" ItemStyle-CssClass="nocheckboxstyle" />
                                            <telerik:GridBoundColumn DataField="LeaveID" SortExpression="LeaveID" HeaderText="LeaveID"
                                                Visible="False" meta:resourcekey="GridBoundColumnResource7" UniqueName="LeaveID" />
                                        </Columns>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                    </MasterTableView>
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="ibtnSave" runat="server" Text="Save" CssClass="button"
                        ValidationGroup="Grp1" meta:resourcekey="ibtnSaveResource1" />
                    <asp:Button ID="ibtnDelete" OnClientClick="return ValidateDelete();" runat="server" Text="Delete" CssClass="button" meta:resourcekey="ibtnDeleteResource1" />
                    <asp:Button ID="ibtnRest" runat="server" Text="Clear" CssClass="button" meta:resourcekey="ibtnRestResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdEmp_Grade"
                        Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="dgrdEmp_Grade" runat="server" AllowSorting="True" AllowPaging="True"
                        PageSize="15" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        ShowFooter="True" meta:resourcekey="dgrdEmp_GradeResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="GradeId,GradeCode">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource2"
                                    UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="GradeCode" SortExpression="GradeCode" HeaderText="Grade Code"
                                    meta:resourcekey="GridBoundColumnResource8" UniqueName="GradeCode" />
                                <telerik:GridBoundColumn DataField="GradeName" SortExpression="GradeName" HeaderText="Grade English Name"
                                    meta:resourcekey="GridBoundColumnResource9" UniqueName="GradeName" />
                                <telerik:GridBoundColumn DataField="GradeArabicName" SortExpression="GradeArabicName"
                                    HeaderText="Grade Arabic Name" meta:resourcekey="GridBoundColumnResource10" UniqueName="GradeArabicName" />
                                <telerik:GridBoundColumn DataField="GradeId" SortExpression="GradeId" HeaderText="GradeId"
                                    Visible="False" meta:resourcekey="GridBoundColumnResource11" UniqueName="GradeId" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<script type="text/javascript" language="javascript">
        var Lang = '<%= Lang %>'
        window.onload = function () {
            var ViewDetails = document.getElementById('lnkViewDetails');
            if (Lang == '1') {
                ViewDetails.innerHTML = '<%= GetLocalResourceObject("ViewDetails") %>';
            }
            else {
                ViewDetails.innerHTML = '<%= GetLocalResourceObject("ViewDetails") %>';
            }
        }
    </script>--%>
    <script type="text/javascript">
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdEmp_Grade.ClientID %>");
            var masterTable = grid.get_masterTableView();
            var value = false;
            for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                var gridItemElement = masterTable.get_dataItems()[i].findElement("chk");
                if (gridItemElement.checked) {
                    value = true;
                }
            }
            if (value === false) {
                ShowMessage("<%= Resources.Strings.ErrorDeleteRecourd %>");
            }
            return value;
        }

    </script>
</asp:Content>
