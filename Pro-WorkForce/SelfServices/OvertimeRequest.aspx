<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="OvertimeRequest.aspx.vb"
    Inherits="SelfServices_OvertimeRequest" Theme="SvTheme" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Employee Overtime Request" />

    <asp:MultiView ID="mvEmpOvertimeRequest" ActiveViewIndex="0" runat="server">
        <asp:View ID="viewOvertimeRequests" runat="server">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblstatus" runat="server" Text="Status" meta:resourcekey="lblstatusResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlStatus" AutoPostBack="True" runat="server" MarkFirstMatch="True" meta:resourcekey="ddlStatusResource1">
                    </telerik:RadComboBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblFromDateSearch" runat="server" CssClass="Profiletitletxt"
                        Text="From Date" meta:resourcekey="lblFromDateSearchResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpFromDateSearch" runat="server" Culture="en-US"
                        Width="180px" Skin="Vista" meta:resourcekey="dtpFromDateSearchResource1">
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="dtpFromDateSearch"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="Get" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator9_ValidatorCalloutExtender"
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt"
                        Text="To Date" meta:resourcekey="lblToDateResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpToDateSearch" runat="server" Culture="en-US"
                        Width="180px" Skin="Vista" meta:resourcekey="dtpToDateSearchResource1">
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtpToDateSearch"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="Get" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator1">
                    </cc1:ValidatorCalloutExtender>
                    <br />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="dtpFromDateSearch"
                        ControlToValidate="dtpToDateSearch" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="Get" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender2"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnGet" runat="server" Text="Get" CssClass="button" ValidationGroup="Get" meta:resourcekey="btnGetResource1" />
                </div>
            </div>

            <div class="table-responsive">
                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdOvertime"
                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" >
                    <ContextMenu FeatureGroupID="rfContextMenu">
                    </ContextMenu>
                </telerik:RadFilter>
                <telerik:RadGrid ID="dgrdOvertime" runat="server" AllowSorting="True" AllowPaging="True"
                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" PageSize="25"
                    ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdOvertimeResource1">
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top"
                        DataKeyNames="EmpOverTimeId,FK_EmployeeId,ProcessStatus">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="False" FilterControlAltText="Filter TemplateColumn1 column" meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn1">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="OvertimeDate" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderText="Date" FilterControlAltText="Filter OvertimeDate column" meta:resourcekey="GridBoundColumnResource1" UniqueName="OvertimeDate">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FromDateTime" HeaderText="From Time" DataFormatString="{0:HH:mm}"
                                UniqueName="FromDateTime" FilterControlAltText="Filter FromDateTime column" meta:resourcekey="GridBoundColumnResource2">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ToDateTime" HeaderText="ToTime" DataFormatString="{0:HH:mm}"
                                UniqueName="ToDateTime" FilterControlAltText="Filter ToDateTime column" meta:resourcekey="GridBoundColumnResource3">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Duration" HeaderText="Duration"
                                UniqueName="Duration" FilterControlAltText="Filter Duration column" meta:resourcekey="GridBoundColumnResource4">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status Name"
                                UniqueName="StatusName" FilterControlAltText="Filter StatusName column" meta:resourcekey="GridBoundColumnResource5">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StatusNameArabic" HeaderText="Status Name Arabic"
                                UniqueName="StatusNameArabic" FilterControlAltText="Filter StatusNameArabic column" meta:resourcekey="GridBoundColumnResource6">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="ProcessStatus"
                                UniqueName="ProcessStatus" Display="False" FilterControlAltText="Filter ProcessStatus column" meta:resourcekey="GridBoundColumnResource7">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFinallyApproved"
                                UniqueName="IsFinallyApproved" Display="False" FilterControlAltText="Filter IsFinallyApproved column" meta:resourcekey="GridBoundColumnResource8">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource2">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkRequestOvertime" runat="server" Text="Request" OnClick="lnkRequestOvertime_Click" meta:resourcekey="lnkRequestOvertimeResource1"></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>

                            <telerik:GridBoundColumn DataField="ProcessStatus" UniqueName="ProcessStatus" Visible="False" FilterControlAltText="Filter ProcessStatus column" meta:resourcekey="GridBoundColumnResource9" />
                            <telerik:GridBoundColumn DataField="FK_EmployeeId" UniqueName="FK_EmployeeId" Visible="False" FilterControlAltText="Filter FK_EmployeeId column" meta:resourcekey="GridBoundColumnResource10" />
                        </Columns>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                Skin="Hay" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
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

        </asp:View>
        <asp:View ID="viewOvertimeForm" runat="server">
            <asp:UpdatePanel ID="update1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblOvertimeDate" runat="server" Text="Date" meta:resourcekey="lblOvertimeDateResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="dtpOvertimeDate" runat="server" AllowCustomText="false"
                                Enabled="False" Culture="en-US" MarkFirstMatch="true"
                                PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpOvertimeDateResource1">
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
                                </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblFromTime" runat="server" Text="From Time" meta:resourcekey="lblFromTimeResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadTimePicker ID="RadTPFromTime" runat="server" AllowCustomText="false" Enabled="False"
                                MarkFirstMatch="true" Skin="Vista" Culture="en-GB" meta:resourcekey="RadTPFromTimeResource1">
                                <Calendar EnableWeekends="True" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                </Calendar>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                <TimeView CellSpacing="-1" Culture="en-GB">
                                    <HeaderTemplate>
                                        Time Picker
                                    </HeaderTemplate>
                                    <TimeTemplate>
                                        <a runat="server" href="#"></a>
                                    </TimeTemplate>
                                </TimeView>
                                <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                <DateInput DateFormat="HH:mm" DisplayDateFormat="HH:mm" LabelWidth="64px" ToolTip="View start time" Width="">
                                    <EmptyMessageStyle Resize="None" />
                                    <ReadOnlyStyle Resize="None" />
                                    <FocusedStyle Resize="None" />
                                    <DisabledStyle Resize="None" />
                                    <InvalidStyle Resize="None" />
                                    <HoveredStyle Resize="None" />
                                    <EnabledStyle Resize="None" />
                                </DateInput>
                            </telerik:RadTimePicker>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblToTime" runat="server" Text="To Time" meta:resourcekey="lblToTimeResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadTimePicker ID="RadTPToTime" runat="server" AllowCustomText="false" Enabled="False"
                                MarkFirstMatch="true" Skin="Vista" Culture="en-GB" meta:resourcekey="RadTPToTimeResource1">
                                <Calendar EnableWeekends="True" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False">
                                </Calendar>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                <TimeView CellSpacing="-1" Culture="en-GB">
                                    <HeaderTemplate>
                                        Time Picker
                                    </HeaderTemplate>
                                    <TimeTemplate>
                                        <a runat="server" href="#"></a>
                                    </TimeTemplate>
                                </TimeView>
                                <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                <DateInput DateFormat="HH:mm" DisplayDateFormat="HH:mm" LabelWidth="64px" ToolTip="View start time" Width="">
                                    <EmptyMessageStyle Resize="None" />
                                    <ReadOnlyStyle Resize="None" />
                                    <FocusedStyle Resize="None" />
                                    <DisabledStyle Resize="None" />
                                    <InvalidStyle Resize="None" />
                                    <HoveredStyle Resize="None" />
                                    <EnabledStyle Resize="None" />
                                </DateInput>
                            </telerik:RadTimePicker>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks" meta:resourcekey="lblRemarksResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSave" runat="server" Text="Submit" meta:resourcekey="btnSaveResource1" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" meta:resourcekey="btnCancelResource1" />
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

