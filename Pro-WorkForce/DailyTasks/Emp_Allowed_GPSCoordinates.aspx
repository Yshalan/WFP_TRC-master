<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Emp_Allowed_GPSCoordinates.aspx.vb" Inherits="DailyTasks_Emp_Allowed_GPSCoordinates" Theme="SvTheme" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagPrefix="uc1" TagName="EmployeeFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Allowed GPS Coordinates" />
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab" meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel ID="tabEmployee" runat="server" HeaderText="Employee" meta:resourcekey="tabEmployeeResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <uc1:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" ValidationGroup="grpSave" ShowDirectStaffCheck="false" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblEmpLocationName" runat="server" Text="Location Name" meta:resourcekey="lblEmpLocationNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtEmpLocationName" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblEmpLocationArabicName" runat="server" Text="Location Arabic Name" meta:resourcekey="lblEmpLocationArabicNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtEmpLocationArabicName" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblGPSCoordinates" runat="server" Text="GPS Coordinates" meta:resourcekey="lblGPSCoordinatesResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtGPSCoordinates" runat="server" meta:resourcekey="txtGPSCoordinatesResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvGPSCoordinates" runat="server" ControlToValidate="txtGPSCoordinates"
                                    Display="None" ErrorMessage="Please Enter GPS Coordinates" ValidationGroup="grpSave" meta:resourcekey="rfvGPSCoordinatesResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceGPSCoordinates" runat="server" TargetControlID="rfvGPSCoordinates"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblRadius" runat="server" Text="Radius" meta:resourcekey="lblRadiusResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtRadius" MinValue="1" MaxValue="9999999"
                                    Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" DbValueFactor="1" LabelWidth="64px" meta:resourcekey="txtRadiusResource1">
                                    <NegativeStyle Resize="None" />
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                                    <EmptyMessageStyle Resize="None" />
                                    <ReadOnlyStyle Resize="None" />
                                    <FocusedStyle Resize="None" />
                                    <DisabledStyle Resize="None" />
                                    <InvalidStyle Resize="None" />
                                    <HoveredStyle Resize="None" />
                                    <EnabledStyle Resize="None" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="rfvRadius" runat="server" ControlToValidate="txtRadius"
                                    Display="None" ErrorMessage="Please Enter Radius in Meters (m)" ValidationGroup="grpSave" meta:resourcekey="rfvRadiusResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceRadius" runat="server" TargetControlID="rfvRadius"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblRadiusMeter" runat="server" Text="Meter" meta:resourcekey="lblRadiusMeterResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblFromDate" runat="server" CssClass="Profiletitletxt" Text="From Date" meta:resourcekey="lblFromDateResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpFromdate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                    PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US" meta:resourcekey="dtpFromdateResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        Width="" LabelWidth="64px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chckTemporary" runat="server" AutoPostBack="True" Text="Temporary" meta:resourcekey="chckTemporaryResource1" />
                            </div>
                        </div>
                        <asp:Panel ID="pnlToDate" runat="server" Visible="False" meta:resourcekey="pnlToDateResource1">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt" Text="To date" meta:resourcekey="lblToDateResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDatePicker ID="dtpToDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                        PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US" meta:resourcekey="dtpToDateResource1">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                            Width="" LabelWidth="64px">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                    <asp:CompareValidator ID="cvDate" runat="server" ControlToCompare="dtpFromdate" ControlToValidate="dtpToDate"
                                        ErrorMessage="To Date should be greater than or equal to From Date" Display="None"
                                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave" meta:resourcekey="cvDateResource1"></asp:CompareValidator>
                                    <cc1:ValidatorCalloutExtender TargetControlID="cvDate" ID="vceDate" runat="server"
                                        Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="grpSave" meta:resourcekey="btnSaveResource1" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" meta:resourcekey="btnClearResource1" />
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" meta:resourcekey="btnDeleteResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                                <div class="filterDiv">
                                    <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdAllowedGPS"
                                        ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1">
                                    </telerik:RadFilter>
                                </div>
                                <telerik:RadGrid runat="server" ID="dgrdAllowedGPS" AutoGenerateColumns="False" PageSize="15"
                                    AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" meta:resourcekey="dgrdAllowedGPSResource1">
                                    <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False" DataKeyNames="AllowedGPSId">
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick" SingleClick="None" meta:resourcekey="RadToolBar1Resource1">
                                                <Items>
                                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                                        ImageUrl="~/images/RadFilter.gif" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                                </Items>
                                            </telerik:RadToolBar>
                                        </CommandItemTemplate>
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False" FilterControlAltText="Filter TemplateColumn column" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                                    <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="FK_EmployeeId" HeaderText="FK_EmployeeId"
                                                AllowFiltering="False" SortExpression="FK_EmployeeId" Display="False" FilterControlAltText="Filter FK_EmployeeId column" UniqueName="FK_EmployeeId" meta:resourcekey="GridBoundColumnResource1">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName" Resizable="False" FilterControlAltText="Filter EmployeeName column" UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource2">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GPS_Coordinates" HeaderText="GPS Coordinates"
                                                SortExpression="GPS_Coordinates" FilterControlAltText="Filter GPS_Coordinates column" UniqueName="GPS_Coordinates" meta:resourcekey="GridBoundColumnResource3" />
                                            <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date" DataFormatString="{0: dd/MM/yyyy}" SortExpression="FromDate" Resizable="False" FilterControlAltText="Filter FromDate column" UniqueName="FromDate" meta:resourcekey="GridBoundColumnResource4">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" DataFormatString="{0: dd/MM/yyyy}" SortExpression="ToDate" Resizable="False" FilterControlAltText="Filter ToDate column" UniqueName="ToDate" meta:resourcekey="GridBoundColumnResource5">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Temporary" UniqueName="IsTemporary" FilterControlAltText="Filter IsTemporary column" meta:resourcekey="GridTemplateColumnResource2">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkIsTemporary" runat="server" Enabled="False" Text="&nbsp;" meta:resourcekey="chkIsTemporaryResource1" />
                                                    <asp:HiddenField ID="hdnIsTemporary" runat="server" Value='<%# Eval("IsTemporary") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                                        EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="tabLogicalGroup" runat="server" HeaderText="Logical Group" TabIndex="1" meta:resourcekey="tabLogicalGroupResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblLogicalGroup" runat="server" Text="Logical Group" meta:resourcekey="lblLogicalGroupResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="ddlLogicalGroup" runat="server" MarkFirstMatch="True" AppendDataBoundItems="True"
                                    DropDownStyle="DropDownList" Skin="Vista" CausesValidation="False" Width="225px" meta:resourcekey="ddlLogicalGroupResource1" />
                                <asp:RequiredFieldValidator ID="rfvddlLogicalGroup" runat="server" Display="None"
                                    InitialValue="--Please Select--" ErrorMessage="Please Select Logical Group" ValidationGroup="grpLGSave"
                                    ControlToValidate="ddlLogicalGroup" meta:resourcekey="rfvddlLogicalGroupResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceddlLogicalGroup" runat="server" TargetControlID="rfvddlLogicalGroup"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblLGLocationName" runat="server" Text="Location Name" meta:resourcekey="lblLGLocationNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtLGLocationName" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblLGLocationArabicName" runat="server" Text="Location Arabic Name" meta:resourcekey="lblLGLocationArabicNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtLGLocationArabicName" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblLGGPS_Coordinates" runat="server" Text="GPS Coordinates" meta:resourcekey="lblLGGPS_CoordinatesResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtLGGPSCoordinates" runat="server" meta:resourcekey="txtLGGPSCoordinatesResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtLGGPSCoordinates" runat="server" ControlToValidate="txtLGGPSCoordinates"
                                    Display="None" ErrorMessage="Please Enter GPS Coordinates" ValidationGroup="grpLGSave" meta:resourcekey="rfvtxtLGGPSCoordinatesResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vcetxtLGGPSCoordinates" runat="server" TargetControlID="rfvtxtLGGPSCoordinates"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblLGRadius" runat="server" Text="Radius" meta:resourcekey="lblLGRadiusResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtLGRadius" MinValue="1" MaxValue="9999999"
                                    Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" DbValueFactor="1" LabelWidth="64px" meta:resourcekey="txtLGRadiusResource1">
                                    <NegativeStyle Resize="None" />
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" ZeroPattern="n" />
                                    <EmptyMessageStyle Resize="None" />
                                    <ReadOnlyStyle Resize="None" />
                                    <FocusedStyle Resize="None" />
                                    <DisabledStyle Resize="None" />
                                    <InvalidStyle Resize="None" />
                                    <HoveredStyle Resize="None" />
                                    <EnabledStyle Resize="None" />
                                </telerik:RadNumericTextBox>
                                <asp:RequiredFieldValidator ID="rfvtxtLGRadius" runat="server" ControlToValidate="txtLGRadius"
                                    Display="None" ErrorMessage="Please Enter Radius in Meters (m)" ValidationGroup="grpLGSave" meta:resourcekey="rfvtxtLGRadiusResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vcetxtLGRadius" runat="server" TargetControlID="rfvtxtLGRadius"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblLGRadiusMeter" runat="server" Text="Meter" meta:resourcekey="lblLGRadiusMeterResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblLGFromDate" runat="server" CssClass="Profiletitletxt" Text="From Date" meta:resourcekey="lblLGFromDateResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpLGFromDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                    PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US" meta:resourcekey="dtpLGFromDateResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        Width="" LabelWidth="64px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkLGIsTemporary" runat="server" AutoPostBack="True" Text="Temporary" meta:resourcekey="chkLGIsTemporaryResource1" />
                            </div>
                        </div>
                        <asp:Panel ID="pnlLGToDate" runat="server" Visible="False" meta:resourcekey="pnlLGToDateResource1">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblLGToDate" runat="server" CssClass="Profiletitletxt" Text="To Date" meta:resourcekey="lblLGToDateResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDatePicker ID="dtpLGToDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                        PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US" meta:resourcekey="dtpLGToDateResource1">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                            Width="" LabelWidth="64px">
                                            <EmptyMessageStyle Resize="None" />
                                            <ReadOnlyStyle Resize="None" />
                                            <FocusedStyle Resize="None" />
                                            <DisabledStyle Resize="None" />
                                            <InvalidStyle Resize="None" />
                                            <HoveredStyle Resize="None" />
                                            <EnabledStyle Resize="None" />
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                    <asp:CompareValidator ID="cvLGDate" runat="server" ControlToCompare="dtpLGFromdate" ControlToValidate="dtpLGToDate"
                                        ErrorMessage="To Date should be greater than or equal to From Date" Display="None"
                                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpLGSave" meta:resourcekey="cvLGDateResource1"></asp:CompareValidator>
                                    <cc1:ValidatorCalloutExtender TargetControlID="cvLGDate" ID="vcecvLGDate" runat="server"
                                        Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnLGSave" runat="server" Text="Save" ValidationGroup="grpLGSave" meta:resourcekey="btnLGSaveResource1" />
                                <asp:Button ID="btnLGClear" runat="server" Text="Clear" meta:resourcekey="btnLGClearResource1" />
                                <asp:Button ID="btnLGDelete" runat="server" Text="Delete" meta:resourcekey="btnLGDeleteResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel2" meta:resourcekey="RadAjaxLoadingPanel2Resource1" />
                                <div class="filterDiv">
                                    <telerik:RadFilter runat="server" ID="RadFilter2" Skin="Hay" FilterContainerID="dgrdAllowedLGGPS"
                                        ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter2Resource1">
                                        <ContextMenu FeatureGroupID="rfContextMenu">
                                        </ContextMenu>
                                    </telerik:RadFilter>
                                </div>
                                <telerik:RadGrid runat="server" ID="dgrdAllowedLGGPS" AutoGenerateColumns="False" PageSize="15"
                                    AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" meta:resourcekey="dgrdAllowedLGGPSResource1">
                                    <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False" DataKeyNames="AllowedLGGPSId">
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar2_ButtonClick" SingleClick="None" meta:resourcekey="RadToolBar1Resource2">
                                                <Items>
                                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                                        ImageUrl="~/images/RadFilter.gif" runat="server" meta:resourcekey="RadToolBarButtonResource2" />
                                                </Items>
                                            </telerik:RadToolBar>
                                        </CommandItemTemplate>
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False" FilterControlAltText="Filter TemplateColumn column" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource3">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource2" />
                                                    <asp:HiddenField ID="hdnGroupNameAr" runat="server" Value='<%# Eval("GroupArabicName") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="FK_LogicalGroupId" HeaderText="FK_LogicalGroupId"
                                                AllowFiltering="False" SortExpression="FK_LogicalGroupId" Display="False" FilterControlAltText="Filter FK_LogicalGroupId column" UniqueName="FK_LogicalGroupId" meta:resourcekey="GridBoundColumnResource6">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GroupName" HeaderText="Group Name" SortExpression="GroupName" Resizable="False" FilterControlAltText="Filter GroupName column" UniqueName="GroupName" meta:resourcekey="GridBoundColumnResource7">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="GPS_Coordinates" HeaderText="GPS Coordinates"
                                                SortExpression="GPS_Coordinates" FilterControlAltText="Filter GPS_Coordinates column" UniqueName="GPS_Coordinates" meta:resourcekey="GridBoundColumnResource8" />
                                            <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date" DataFormatString="{0: dd/MM/yyyy}" SortExpression="FromDate" Resizable="False" FilterControlAltText="Filter FromDate column" UniqueName="FromDate" meta:resourcekey="GridBoundColumnResource9">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" DataFormatString="{0: dd/MM/yyyy}" SortExpression="ToDate" Resizable="False" FilterControlAltText="Filter ToDate column" UniqueName="ToDate" meta:resourcekey="GridBoundColumnResource10">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Temporary" UniqueName="IsTemporary" FilterControlAltText="Filter IsTemporary column" meta:resourcekey="GridTemplateColumnResource4">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkLGIsTemporary" runat="server" Enabled="False" Text="&nbsp;" meta:resourcekey="chkLGIsTemporaryResource2" />
                                                    <asp:HiddenField ID="hdnLGIsTemporary" runat="server" Value='<%# Eval("IsTemporary") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                                        EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </div>

                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

