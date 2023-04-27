<%@ Page Title="" Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Events_Definition.aspx.vb" Inherits="Definitions_Events_Definition"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="Emp_Filter"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <uc1:PageHeader ID="PageHeader1" runat="server" />
    </center>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <cc1:TabContainer runat="server" ID="TabContainer1" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab"
                meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel runat="server" ID="tab_Event" HeaderText="Event" meta:resourcekey="tab_EventResource1">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEventName" runat="server" Text="Event Name" CssClass="Profiletitletxt"
                                        meta:resourcekey="lblEventNameResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEventName" runat="server" meta:resourcekey="txtEventNameResource1"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvEventName" runat="server" ControlToValidate="txtEventName"
                                        Display="None" ErrorMessage="Please Enter Event Name" ValidationGroup="GrpSaveEvent"
                                        meta:resourcekey="rfvEventNameResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceEventName" runat="server" CssClass="AISCustomCalloutStyle"
                                        TargetControlID="rfvEventName" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date" CssClass="Profiletitletxt"
                                        meta:resourcekey="lblStartDateResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="dteStartDate" runat="server" Width="120px" Culture="en-US"
                                        meta:resourcekey="dteStartDateResource1">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DDisplayDateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                            LabelCssClass="" Width="">
                                        </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="dteStartDate"
                                        Display="None" ErrorMessage="Please Enter Start Date" ValidationGroup="GrpSaveEvent"
                                        meta:resourcekey="rfvStartDateResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceStartDate" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvStartDate" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEndDate" runat="server" Text="End Date" CssClass="Profiletitletxt"
                                        meta:resourcekey="lblEndDateResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="dteEndDate" runat="server" Width="120px" Culture="en-US"
                                        meta:resourcekey="dteEndDateResource1">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DDisplayDateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                            LabelCssClass="" Width="">
                                        </DateInput><DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                    </telerik:RadDatePicker>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="dteEndDate"
                                        Display="None" ErrorMessage="Please Enter End Date" ValidationGroup="GrpSaveEvent"
                                        meta:resourcekey="rfvEndDateResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceEndDate" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvEndDate" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                    <asp:CompareValidator ID="CompareValidator1" Display="None" runat="server" ControlToValidate="dteEndDate"
                                        ControlToCompare="dteStartDate" Operator="GreaterThanEqual" Type="Date" ErrorMessage="End date should be greater than or equal to Start date"
                                        meta:resourcekey="CompareValidator1Resource1" ValidationGroup="GrpSaveEvent"></asp:CompareValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="CompareValidator1" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDescription" runat="server" Text="Description" CssClass="Profiletitletxt"
                                        meta:resourcekey="lblDescriptionResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Panel ID="pnlResponsiblePerson" runat="server" GroupingText="Responsible Person"
                                        meta:resourcekey="pnlResponsiblePersonResource1" Width="700px">
                                        <uc2:Emp_Filter ID="objEmp_Filter" runat="server" FilterType="L" ShowOnlyManagers="false"
                                            ValidationGroup="GrpSaveEvent" />
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" ID="tab_EventGroups" HeaderText="Event Groups" Visible="false"
                    meta:resourcekey="tab_EventResourcesResource1">
                    <ContentTemplate>
                        <table>
                            <%-- <tr>
                                <td>
                                    <asp:Label ID="lblEvent" runat="server" Text="Event Name" CssClass="Profiletitletxt"
                                        meta:resourcekey="lblEventResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCmbBxEvent" AutoPostBack="true" AllowCustomText="false"
                                        MarkFirstMatch="true" Skin="Vista" runat="server" meta:resourcekey="RadCmbBxEventResource1">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvCmbEvent" runat="server" ControlToValidate="RadCmbBxEvent"
                                        Display="None" ErrorMessage="Please Select Event" InitialValue="--Please Select--"
                                        ValidationGroup="GrpSaveGroup" meta:resourcekey="rfvCmbEventResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceCmbEvent" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvCmbEvent" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>--%>
                            <tr>
                                <td>
                                    <asp:Label ID="lblGroup" runat="server" Text="Logical Group" AutoPostBack="true"
                                        CssClass="Profiletitletxt" meta:resourcekey="lblGroupResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCmbBxGroup" AllowCustomText="false" MarkFirstMatch="true"
                                        Skin="Vista" runat="server" meta:resourcekey="RadCmbBxGroupResource1">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvGroup" runat="server" ControlToValidate="RadCmbBxGroup"
                                        Display="None" ErrorMessage="Please Select Group" InitialValue="--Please Select--"
                                        ValidationGroup="GrpSaveGroup" meta:resourcekey="rfvGroupResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceGroup" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvGroup" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNoOfEmployee" runat="server" Text="Number Of Employees" CssClass="Profiletitletxt"
                                        meta:resourcekey="lblNoOfEmployeeResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtNoOfEmployees" MinValue="0" MaxValue="99999" Skin="Vista"
                                        runat="server" Culture="en-US" LabelCssClass="" Width="200px" meta:resourcekey="txtNoOfEmployeesResource1">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvNoOfEmployees" runat="server" ControlToValidate="txtNoOfEmployees"
                                        Display="None" ErrorMessage="Please Insert No Of Employees" ValidationGroup="GrpSaveGroup"
                                        meta:resourcekey="rfvNoOfEmployeesResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceNoOfEmployees" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvNoOfEmployees" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center">
                                    <asp:Button ID="btnSaveGroup" runat="server" Text="Add" CssClass="button" ValidationGroup="GrpSaveGroup"
                                        meta:resourcekey="btnSaveGroupResource1" />
                                    <asp:Button ID="btnDeleteGroup" runat="server" Text="Remove" CssClass="button" CausesValidation="False"
                                        meta:resourcekey="btnDeleteGroupResource1" />
                                    <asp:Button ID="btnClearGroup" runat="server" Text="Clear" CssClass="button" CausesValidation="False"
                                        meta:resourcekey="btnClearGroupResource1" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <telerik:RadGrid ID="dgrdEventGroups" runat="server" PageSize="25" AllowPaging="True"
                                        Skin="Hay" GridLines="None" ShowStatusBar="True" ShowFooter="True" AllowMultiRowSelection="True"
                                        meta:resourcekey="dgrdEventGroupsResource1">
                                        <SelectedItemStyle ForeColor="Maroon" />
                                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="GroupName,FK_EventId,FK_GroupId">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource1" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_GroupId" HeaderText="FK_GroupId"
                                                    SortExpression="FK_GroupId" Visible="False" UniqueName="FK_GroupId" meta:resourcekey="GridBoundColumnResource1" />
                                                <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EventId" HeaderText="FK_EventId"
                                                    SortExpression="FK_EventId" Visible="False" UniqueName="FK_EventId" meta:resourcekey="GridBoundColumnResource2" />
                                                <telerik:GridBoundColumn DataField="EventName" HeaderText="Event Name" SortExpression="EventName"
                                                    Resizable="False" UniqueName="EventName" meta:resourcekey="GridBoundColumnResource3" />
                                                <telerik:GridBoundColumn DataField="GroupName" HeaderText="Group Name" SortExpression="GroupName"
                                                    Resizable="False" UniqueName="GroupName" meta:resourcekey="GridBoundColumnResource4" />
                                                <telerik:GridBoundColumn DataField="GroupArabicName" HeaderText="Group Arabic Name"
                                                    SortExpression="GroupArabicName" Resizable="False" UniqueName="GroupArabicName"
                                                    meta:resourcekey="GridBoundColumnResource5" />
                                                <telerik:GridBoundColumn DataField="NumberOfEmployees" HeaderText="Number Of Employees"
                                                    SortExpression="NumberOfEmployees" Resizable="False" UniqueName="NumberOfEmployees"
                                                    meta:resourcekey="GridBoundColumnResource6" />
                                                <telerik:GridBoundColumn DataField="AssingedEmpCount" HeaderText="Number Of Assigned Employees"
                                                    SortExpression="AssingedEmpCount" Resizable="False" UniqueName="AssingedEmpCount"
                                                    meta:resourcekey="GridBoundColumnResource21" />
                                            </Columns>
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        </MasterTableView>
                                        <GroupingSettings CaseSensitive="False" />
                                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                            <Selecting AllowRowSelect="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" ID="tab_AssignResources" HeaderText="Assign Resources"
                    Visible="false" meta:resourcekey="tab_AssignResourcesResource1">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEventGroup" runat="server" Text="Event Groups" CssClass="Profiletitletxt"
                                        meta:resourcekey="lblEventGroupResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCmbBxEventGroup" AllowCustomText="false" MarkFirstMatch="true"
                                        AutoPostBack="true" Skin="Vista" runat="server" meta:resourcekey="RadCmbBxEventGroupResource1">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvEventGroups" runat="server" ControlToValidate="RadCmbBxEventGroup"
                                        Display="None" ErrorMessage="Please Select Event Group" ValidationGroup="GrpSaveResource"
                                        meta:resourcekey="rfvEventGroupsResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceEventGroups" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvEventGroups" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEmployees" runat="server" Text="Employees" CssClass="Profiletitletxt"
                                        meta:resourcekey="lblEmployeesResource1"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadCmbBxEmployees" AllowCustomText="false" MarkFirstMatch="true"
                                        AutoPostBack="true" Skin="Vista" runat="server">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNoOFDefinedEmp" runat="server" Text="No Of Defined Employees" CssClass="Profiletitletxt"
                                        Visible="false" meta:resourcekey="lblNoOFDefinedEmpResource1"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNoOfEmps" runat="server" CssClass="Profiletitletxt" Visible="false"></asp:Label>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center">
                                    <asp:Button ID="btnSaveResource" runat="server" Text="Add" CssClass="button" ValidationGroup="GrpSaveResource"
                                        meta:resourcekey="btnSaveResourceResource1" />
                                    <asp:Button ID="btnDeleteResource" runat="server" Text="Remove" CssClass="button"
                                        CausesValidation="False" meta:resourcekey="btnDeleteResourceResource1" />
                                    <asp:Button ID="btnClearResource" runat="server" Text="Clear" CssClass="button" CausesValidation="False"
                                        meta:resourcekey="btnClearResourceResource1" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <telerik:RadGrid ID="dgrdResources" runat="server" PageSize="25" AllowPaging="True"
                                        Skin="Hay" GridLines="None" ShowStatusBar="True" ShowFooter="True" AllowMultiRowSelection="True"
                                        meta:resourcekey="dgrdResourcesResource1">
                                        <SelectedItemStyle ForeColor="Maroon" />
                                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="GroupId,GroupName,FK_EmployeeId">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource2" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" HeaderText="FK_EmployeeId"
                                                    SortExpression="FK_EmployeeId" Visible="False" UniqueName="FK_EmployeeId" meta:resourcekey="GridBoundColumnResource7" />
                                                <telerik:GridBoundColumn AllowFiltering="False" DataField="GroupId" HeaderText="GroupId"
                                                    SortExpression="GroupId" Visible="False" UniqueName="GroupId" meta:resourcekey="GridBoundColumnResource8" />
                                                <telerik:GridBoundColumn AllowFiltering="False" DataField="GroupName" HeaderText="Group Name"
                                                    SortExpression="GroupName" UniqueName="GroupName" meta:resourcekey="GridBoundColumnResource9" />
                                                <telerik:GridBoundColumn DataField="GroupArabicName" HeaderText="Group Arabic Name"
                                                    SortExpression="GroupArabicName" Resizable="False" UniqueName="GroupArabicName"
                                                    meta:resourcekey="GridBoundColumnResource10" />
                                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" SortExpression="EmployeeNo"
                                                    Resizable="False" UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource11" />
                                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="GroupArabiEmployeeNamecName"
                                                    Resizable="False" UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource12" />
                                                <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Arabic Name"
                                                    SortExpression="EmployeeArabicName" Resizable="False" UniqueName="EmployeeArabicName"
                                                    meta:resourcekey="GridBoundColumnResource13" />
                                            </Columns>
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        </MasterTableView>
                                        <GroupingSettings CaseSensitive="False" />
                                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                            <Selecting AllowRowSelect="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <table>
                <tr id="trControls" runat="server">
                    <td colspan="3" align="center">
                        <asp:Button ID="btnSaveEvent" runat="server" Text="Save" CssClass="button" ValidationGroup="GrpSaveEvent"
                            meta:resourcekey="btnSaveEventResource1" />
                        <asp:Button ID="btnDeleteEvent" runat="server" Text="Delete" CssClass="button" CausesValidation="False"
                            meta:resourcekey="btnDeleteEventResource1" />
                        <asp:Button ID="btnClearEvent" runat="server" Text="Clear" CssClass="button" CausesValidation="False"
                            meta:resourcekey="btnClearEventResource1" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" />
                        <div class="filterDiv">
                            <telerik:RadFilter runat="server" ID="RadFilter_Event" Skin="Hay" FilterContainerID="dgrdEvents"
                                ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
                        </div>
                        <telerik:RadGrid ID="dgrdEvents" runat="server" PageSize="25" AllowPaging="True"
                            Skin="Hay" GridLines="None" ShowStatusBar="True" ShowFooter="True" AllowMultiRowSelection="True"
                            AllowSorting="true" meta:resourcekey="dgrdEventsResource1">
                            <SelectedItemStyle ForeColor="Maroon" />
                            <MasterTableView AllowMultiColumnSorting="true" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="EventName,EventId">
                                <CommandItemTemplate>
                                    <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                        Skin="Hay">
                                        <Items>
                                            <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                        </Items>
                                    </telerik:RadToolBar>
                                </CommandItemTemplate>
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource3">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource3" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="EventId" HeaderText="EventId"
                                        SortExpression="EventId" Visible="False" UniqueName="EventId" meta:resourcekey="GridBoundColumnResource14" />
                                    <telerik:GridBoundColumn DataField="EventName" HeaderText="Event Name" SortExpression="EventName"
                                        Resizable="False" UniqueName="EventName" meta:resourcekey="GridBoundColumnResource15" />
                                    <telerik:GridBoundColumn DataField="EventDescription" HeaderText="Event Description"
                                        SortExpression="EventDescription" Resizable="False" UniqueName="EventDescription"
                                        meta:resourcekey="GridBoundColumnResource16" />
                                    <telerik:GridBoundColumn DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate"
                                        UniqueName="StartDate" DataFormatString="{0:MM/dd/yyyy}" meta:resourcekey="GridBoundColumnResource17" />
                                    <telerik:GridBoundColumn DataField="EndDate" HeaderText="End Date" SortExpression="EndDate"
                                        UniqueName="EndDate" DataFormatString="{0:MM/dd/yyyy}" meta:resourcekey="GridBoundColumnResource18" />
                                    <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Resposible Person"
                                        SortExpression="EmployeeName" Resizable="False" UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource19" />
                                    <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Resposible Person Arabic"
                                        SortExpression="EmployeeArabicName" Resizable="False" UniqueName="EmployeeArabicName"
                                        meta:resourcekey="GridBoundColumnResource20" />
                                    <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBuildSchedule" runat="server" Text="Build Schedule" OnClick="lnkBuildSchedule_Click"
                                                meta:resourcekey="lnkBuildScheduleResource1"></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            </MasterTableView>
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
