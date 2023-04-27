<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeavesRequest.aspx.vb" MasterPageFile="~/Default/NewMaster.master"
    Inherits="Admin_LeavesRequest" Theme="SvTheme" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        //function RefreshPage() {
        //    window.location = "../SelfServices/LeavesRequest.aspx";
        //}

    </script>
    <script type="text/javascript">
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdEmpLeaveRequest.ClientID%>");
            var masterTable = grid.get_masterTableView();
            var value = false;
            for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                var gridItemElement = masterTable.get_dataItems()[i].findElement("chk");
                if (gridItemElement.checked) {
                    value = true;
                }
            }
            if (value === false) {
                ShowMessage("<%= Resources.Strings.PleaseSelectFromList%>");
            }
            return value;
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

    </script>
    <asp:Panel ID="Update1" runat="server">
        <uc1:PageHeader ID="EmpLeaveRequestHeader" runat="server" HeaderText="Employee Leave Request" />
        <asp:MultiView ID="mvEmpLeaverequest" ActiveViewIndex="0" runat="server">
            <asp:View ID="viewLeaveRequests" runat="server">

                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblstatus" runat="server" Text="Status" meta:resourcekey="lblstatusResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadComboBox ID="ddlStatus" AutoPostBack="True" runat="server" MarkFirstMatch="true"
                            meta:resourcekey="ddlStatusResource1">
                        </telerik:RadComboBox>
                        <%-- <asp:RequiredFieldValidator ID="rfvStatus" ValidationGroup="Get" runat="server" ControlToValidate="ddlStatus"
                        Display="None" ErrorMessage="Please Select Leave Status" InitialValue="--Please Select--"
                        meta:resourcekey="rfvStatusResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvStatus" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>--%>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblFromDateSearch" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblFromDateSearchResource1"
                            Text="From Date"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker ID="dtpFromDateSearch" runat="server" Culture="English (United States)"
                            meta:resourcekey="RadDatePicker1Resource1" Width="180px" AutoPostBack="false" Skin="Vista">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="dtpFromDateSearch"
                            Display="None" ErrorMessage="Please Select Date" ValidationGroup="Get" meta:resourcekey="RequiredFieldValidator9Resource1">
                        </asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator9_ValidatorCalloutExtender"
                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblToDateResource1"
                            Text="To Date"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker ID="dtpToDateSearch" runat="server" Culture="English (United States)"
                            meta:resourcekey="RadDatePicker2Resource1" Width="180px" AutoPostBack="false" Skin="Vista">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtpToDateSearch"
                            Display="None" ErrorMessage="Please Select Date" ValidationGroup="Get" meta:resourcekey="RequiredFieldValidator1Resource1">
                        </asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                            TargetControlID="RequiredFieldValidator1">
                        </cc1:ValidatorCalloutExtender>
                        <br />
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="dtpFromDateSearch"
                            ControlToValidate="dtpToDateSearch" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                            Operator="GreaterThanEqual" Type="Date" ValidationGroup="Get" meta:resourcekey="CompareValidator2Resource1">
                        </asp:CompareValidator>
                        <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender2"
                            runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnGet" runat="server" Text="Get" CssClass="button" ValidationGroup="Get"
                            meta:resourcekey="btnGetResource1" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnRequestLeave" runat="server" Text="Request Leave" CssClass="button"
                            meta:resourcekey="Button1Resource1" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete Leave" OnClientClick="return ValidateDelete();" CssClass="button" meta:resourcekey="btnDeleteResource1" />
                    </div>
                </div>
                <cc1:TabContainer ID="TabLeavesContainer" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab"
                    meta:resourcekey="TabLeavesContainerResource1">
                    <cc1:TabPanel ID="tabLeaveHistory" runat="server" HeaderText="Leave Request(s)"
                        TabIndex="0" meta:resourcekey="tabLeaveHistoryResource1">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdEmpLeaveRequest"
                                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                                <telerik:RadGrid ID="dgrdEmpLeaveRequest" runat="server" AllowSorting="True" AllowPaging="True"
                                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" PageSize="25"
                                    ShowFooter="True" meta:resourcekey="dgrdEmpLeaveRequestResource1">
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top"
                                        DataKeyNames="StatusId,LeaveRequestId,FK_EmployeeId">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="RequestDate" DataFormatString="{0:dd/MM/yyyy}"
                                                HeaderText="Request Date" meta:resourceKey="GridBound1ColumnResource1" UniqueName="RequestDate">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="LeaveName" HeaderText="Leave Type" meta:resourceKey="GridBound2ColumnResource1"
                                                UniqueName="LeaveName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="LeaveArabicName" HeaderText="Leave Type" meta:resourceKey="GridBound2ColumnResource1"
                                                UniqueName="LeaveArabicName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FromDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="From Date"
                                                meta:resourceKey="GridBound4ColumnResource1" UniqueName="FromDate">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="To Date"
                                                meta:resourceKey="GridBound5ColumnResource1" UniqueName="ToDate">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Days" HeaderText="No. Days" meta:resourceKey="GridBound6ColumnResource1"
                                                UniqueName="Days">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="LeaveId" meta:resourcekey="GridBoundColumnResource1"
                                                UniqueName="LeaveId" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" meta:resourcekey="GridBoundColumnResource2"
                                                UniqueName="FK_EmployeeId" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status" meta:resourcekey="GridBoundColumnResource3"
                                                UniqueName="StatusName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="StatusNameArabic" HeaderText="Status" meta:resourcekey="GridBoundColumnResource3"
                                                UniqueName="StatusNameArabic">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RejectionReason" HeaderText="Rejection Reason"
                                                meta:resourcekey="GridBoundColumnResource13" UniqueName="RejectionReason">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkLeaveForm" runat="server" Text="Leave Form" OnClick="lnkLeaveForm_Click"
                                                        meta:resourcekey="lnkLeaveFormResource1" Visible="false"></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="StatusId" UniqueName="StatusId" Visible="False" />
                                            <telerik:GridBoundColumn DataField="FK_EmployeeId" UniqueName="FK_EmployeeId" Visible="False" />
                                            <telerik:GridBoundColumn DataField="LeaveId" UniqueName="LeaveId" Visible="False" />
                                            <telerik:GridBoundColumn DataField="LeaveRequestId" UniqueName="LeaveRequestId" Visible="False" />
                                            <telerik:GridBoundColumn DataField="AttachedFile" UniqueName="AttachedFile" Visible="False" />
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
                                    <SelectedItemStyle ForeColor="Maroon" />
                                </telerik:RadGrid>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tabLeavesBalance" runat="server" HeaderText="Leaves Balance" Visible="true"
                        TabIndex="0" meta:resourcekey="tabLeavesBalanceResource1">
                        <ContentTemplate>
                            <div class="table-responsive">
                                <telerik:RadFilter runat="server" ID="RadFilter2" FilterContainerID="dgrdLeaveBalance"
                                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter2Resource1" />
                                <telerik:RadGrid ID="dgrdLeaveBalance" runat="server" AllowSorting="True" AllowPaging="True"
                                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" PageSize="25"
                                    ShowFooter="True" meta:resourcekey="dgrdLeaveBalanceResource1">
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings EnablePostBackOnRowClick="false" EnableRowHoverStyle="false">
                                        <Selecting AllowRowSelect="false" />
                                    </ClientSettings>
                                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top"
                                        DataKeyNames="BalanceId,Year,FK_LeaveId,FK_EmployeeId">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="LeaveName" HeaderText="Leave Name" UniqueName="LeaveName"
                                                meta:resourcekey="GridBound7ColumnResource1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="LeaveArabicName" HeaderText="Leave Arabic Name" UniqueName="LeaveArabicName"
                                                meta:resourcekey="GridBound8ColumnResource1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Year" HeaderText="Year" UniqueName="Year" meta:resourcekey="GridBound9ColumnResource1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="LeaveBalance" HeaderText="Leave Balance" UniqueName="LeaveBalance" DataType="System.Decimal"
                                                DataFormatString="{0:F2}" meta:resourcekey="GridBound10ColumnResource1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RemainingBalance" HeaderText="Remaining Balance" UniqueName="RemainingBalance" DataType="System.Decimal"
                                                DataFormatString="{0:F2}" meta:resourcekey="GridBound11ColumnResource1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FK_EmployeeId" UniqueName="FK_EmployeeId" Display="false" />
                                            <telerik:GridBoundColumn DataField="FK_LeaveId" UniqueName="FK_LeaveId" Display="false" />
                                            <telerik:GridBoundColumn DataField="Year" UniqueName="FK_EmployeeId" Display="false" />
                                            <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDetails" runat="server" meta:resourcekey="lnkDetailsResource1" OnClick="lnkDetails_Click"
                                                        Text="Details" Visible="true"></asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar runat="server" ID="RadToolBar2" OnButtonClick="RadToolBar1_ButtonClick"
                                                Skin="Hay" meta:resourcekey="RadToolBar2Resource1">
                                                <Items>
                                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid1" ImageUrl="~/images/RadFilter.gif"
                                                        ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                                </Items>
                                            </telerik:RadToolBar>
                                        </CommandItemTemplate>
                                    </MasterTableView>
                                    <SelectedItemStyle ForeColor="Maroon" />
                                </telerik:RadGrid>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>

                <telerik:RadWindowManager ID="RadWindowManager1" runat="server"
                    EnableShadow="True">
                    <Windows>
                        <telerik:RadWindow ID="radwindowLeaveTimeline" runat="server" Animation="FlyIn" Behavior="Resize, Close, Move"
                            Behaviors="Resize, Close, Move" EnableShadow="True" Height="800px" ShowContentDuringLoad="False" VisibleStatusbar="False"
                            Width="1300px">
                        </telerik:RadWindow>
                    </Windows>
                </telerik:RadWindowManager>

            </asp:View>
            <asp:View ID="viewAddLeaveRequest" runat="server">
                <asp:UpdatePanel ID="Updatepanel1" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" meta:resourcekey="Label4Resource1"
                                    Text="Leave Type"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="ddlLeaveType" runat="server" AppendDataBoundItems="True"
                                    AutoPostBack="true" MarkFirstMatch="True" meta:resourcekey="ddlLeaveTypeResource1"
                                    Skin="Vista" Width="200px">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" meta:resourcekey="RadComboBoxItemResource1"
                                            Text="--Please Select--" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlLeaveType"
                                    Display="None" ErrorMessage="Please Select Leave Type" InitialValue="--Please Select--"
                                    meta:resourcekey="RequiredFieldValidator4Resource1" ValidationGroup="vgLeaveRequest"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="RequiredFieldValidator4" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row" id="divRemaining" runat="server" visible="false">
                            <div class="col-md-2">
                                <asp:Label ID="lblRemainingBalance" runat="server" Text="Remaining Balance" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblRemainingBalanceResource1" />
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblRemainingBalanceValue" runat="server" CssClass="profiletitletxt" />
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblRemainingBalanceDays" runat="server" Text="Days" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblRemainingBalanceDaysResource1" />
                            </div>
                        </div>
                        <%--<asp:Label ID="lblGeneralGuide" runat="server" CssClass="Profiletitletxt" Text="General Guide"
                                                meta:resourcekey="lblGeneralGuideResource1" Visible="false" />--%>
                        <%-- <asp:TextBox ID="labelGeneralGuide" runat="server" TextMode="MultiLine" Visible="false"
                                    Enabled="false" Style="resize: none; border: 0px !important; background-color: White;" />--%>
                        <%-- <asp:Label ID="labelGeneralGuide" runat="server" CssClass="Profiletitletxt" Style="word-wrap: break-word" />--%>
                        <%--<asp:Literal ID="labelGeneralGuide" runat="server"></asp:Literal>--%>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" meta:resourcekey="Label5Resource1"
                                    Text="Request Date"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpRequestDate" runat="server" AllowCustomText="false"
                                    Enabled="false" Culture="English (United States)" MarkFirstMatch="true" meta:resourcekey="dtpRequestDateResource1"
                                    PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                                        Visible="false">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                        Width="">
                                    </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dtpRequestDate"
                                    Display="None" ErrorMessage="Please Enter Request Date" meta:resourcekey="RequiredFieldValidator5Resource1"
                                    ValidationGroup="vgLeaveRequest"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="RequiredFieldValidator5" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="Label7" runat="server" CssClass="Profiletitletxt" meta:resourcekey="Label7Resource1"
                                    Text="Leave From"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpFromDate" runat="server" AllowCustomText="false" Culture="English (United States)"
                                    MarkFirstMatch="true" meta:resourcekey="dtpFromDateResource1" PopupDirection="TopRight"
                                    ShowPopupOnFocus="True" Skin="Vista" AutoPostBack="true">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                        Width="">
                                    </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtpFromDate"
                                    Display="None" ErrorMessage="Please Enter From Date" meta:resourcekey="RequiredFieldValidator6Resource1"
                                    ValidationGroup="vgLeaveRequest"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="RequiredFieldValidator6" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="Label6" runat="server" CssClass="Profiletitletxt" meta:resourcekey="Label6Resource1"
                                    Text="To"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpToDate" runat="server" AllowCustomText="false" Culture="English (United States)"
                                    MarkFirstMatch="true" meta:resourcekey="dtpToDateResource1" PopupDirection="TopRight"
                                    ShowPopupOnFocus="True" Skin="Vista" AutoPostBack="true">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                        Width="">
                                    </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="dtpToDate"
                                    Display="None" ErrorMessage="Please Enter To Date" meta:resourcekey="RequiredFieldValidator7Resource1"
                                    ValidationGroup="vgLeaveRequest"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender
                                        ID="ValidatorCalloutExtender7" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="RequiredFieldValidator7" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="dtpFromDate"
                                    ControlToValidate="dtpToDate" Display="None" ErrorMessage="To date should be greater than or equal to from date"
                                    meta:resourcekey="CompareValidator1Resource1" Operator="GreaterThanEqual" Type="Date"
                                    ValidationGroup="vgLeaveRequest"></asp:CompareValidator><cc1:ValidatorCalloutExtender
                                        ID="ValidatorCalloutExtender8" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="CompareValidator1" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div id="trAdvanceSalary" runat="server" visible="false" class="col-md-4">
                                <asp:CheckBox ID="chbWithAdvancedSalary" runat="server" Text="With Advanced Salary"
                                    CssClass="Profiletitletxt" meta:resourcekey="chbWithAdvancedSalaryResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="Label1" is="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblAttachFileResource1" />
                            </div>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <span class="input-group-btn"><span class="btn btn-default" onclick="$(this).parent().find('input[type=file]').click();">Browse</span>
                                        <asp:FileUpload ID="fuAttachFile" runat="server" meta:resourcekey="fuAttachFileResource1"
                                            name="uploaded_file" onchange="$(this).parent().parent().find('.form-control').html($(this).val().split(/[\\|/]/).pop());"
                                            Style="display: none;" type="file" />
                                    </span><span class="form-control"></span>
                                </div>
                                <div class="veiw_remove">
                                    <asp:LinkButton ID="lnbLeaveFile" runat="server" Visible="False" Text="View" meta:resourcekey="lblViewResource1" OnClick="lnkDownloadFile_Click">
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnbRemove" runat="server" Text="Remove" Visible="False" meta:resourcekey="lnbRemoveResource1" />
                                    <asp:Label ID="lblNoAttachedFile" runat="server" Text="No Attached File" Visible="False"
                                        meta:resourcekey="lblNoAttachedFileResource1" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="Label8" runat="server" CssClass="Profiletitletxt" meta:resourcekey="Label8Resource1"
                                    Text="Remarks"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtRemarks" runat="server" meta:resourcekey="txtRemarksResource1"
                                    TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-8">
                                <asp:Button ID="btnSave" runat="server" CssClass="button" meta:resourcekey="btnSaveResource1"
                                    Text="Save" ValidationGroup="vgLeaveRequest" />
                                <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="button"
                                    meta:resourcekey="btnClearResource1" Text="Clear" />
                                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="button"
                                    Text="Cancel" meta:resourcekey="btnCancelResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblGeneralGuide" runat="server" CssClass="Profiletitletxt" Text="General Guide"
                                    meta:resourcekey="lblGeneralGuideResource1" Visible="false" />
                            </div>
                            <div runat="server" id="dvGeneralGuide" style="margin-top: 5px; background-color: #FDF5B8;">
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:View>
        </asp:MultiView>
    </asp:Panel>

</asp:Content>
