<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="RecalculateTransactions.aspx.vb" Inherits="DailyTasks_RecalculateTransactions"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="UserSecurityFilter"
    TagPrefix="uc1" %>--%>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="Emp_Filter"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc3:PageHeader ID="PageHeader1" runat="server" />
    &nbsp;&nbsp;&nbsp;
    <asp:UpdatePanel ID="pnlFilter" runat="server">
        <ContentTemplate>
            <%--            <div class="updateprogressAssign" style="position: fixed; top: 50%; left: 50%; transform: translate(-50%);
                z-index: 999; background: rgba(0,0,0,0.3);">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="pnlFilter">
                    <ProgressTemplate>
                        <asp:Image ID="imgLoading" runat="server" ImageAlign="Baseline" ImageUrl="~/images/STS_Loading.gif" Width="250px" Height="250px" 
                            meta:resourcekey="imgLoadingResource1" />
                        <asp:Label ID="lblCaption" runat="server" Style="color: white" meta:resourcekey="lblCaptionResource1"></asp:Label>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>--%>
            <div class="updateprogressAssign">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="pnlFilter">
                    <ProgressTemplate>
                        <div id="tblLoading" style="width: 100%; height: 100%; z-index: 100002 !important; text-align: center; position: fixed; left: 0px; top: 0px; background-size: cover; background-image: url('../images/Grey_fade.png');">
                            <asp:Image ID="imgLoading" runat="server" ImageAlign="Middle" ImageUrl="~/images/STS_Loading.gif" Width="250px" Height="250px" Style="margin-top: 20%;" />
                            <asp:Label ID="lblCaption" runat="server" Style="color: white" meta:resourcekey="lblCaptionResource1"></asp:Label>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <uc1:Emp_Filter ID="Emp_Filter" runat="server" ShowRadioSearch="true" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lbldate" runat="server" Text="From Date" Class="Profiletitletxt" meta:resourcekey="lbldateResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dteFromDate" runat="server" Culture="en-US" Width="180px"
                        meta:resourcekey="dteFromDateResource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Width="" LabelWidth="64px">
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
                    <asp:Label ID="lblToDate" runat="server" Text="To Date" Class="Profiletitletxt" meta:resourcekey="lblToDateResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dteToDate" runat="server" Culture="en-US" Width="180px"
                        meta:resourcekey="dteToDateResource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Width="" LabelWidth="64px">
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
                    <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dteFromDate" ControlToValidate="dteToDate"
                        ErrorMessage="End Date should be greater than or equal to From Date" Display="Dynamic"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave" meta:resourcekey="CVDateResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label Text="Immediately Start" ID="lblImmediatelyStart" runat="server" Class="Profiletitletxt"
                        meta:resourcekey="lblImmediatelyStartResource1" />
                </div>
                <div class="col-md-4">
                    <asp:CheckBox Text="is Immediately Start" ID="chbImmediatelyStart" runat="server"
                        AutoPostBack="True" Checked="True" meta:resourcekey="chbImmediatelyStartResource1" />
                </div>
            </div>
            <asp:Panel runat="server" ID="pRequestStartDateTime" Visible="False" CssClass="row"
                meta:resourcekey="pRequestStartDateTimeResource1">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label Text="Request Start Date Time" ID="lblRequestStartDateTime" runat="server"
                            Class="Profiletitletxt" meta:resourcekey="lblRequestStartDateTimeResource1" />
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="dteRequestStartDateTime"
                            runat="server" Culture="en-US" Width="100%" meta:resourcekey="dteFromDateResource1">
                            <TimeView CellSpacing="-1" RenderMode="Lightweight">
                                <HeaderTemplate>
                                    Time Picker
                                </HeaderTemplate>
                                <%--    <TimeTemplate>
                                    <a runat="server" href="#"></a>
                                </TimeTemplate>--%>
                            </TimeView>
                            <%--<TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />--%>
                            <Calendar EnableWeekends="True" RenderMode="Lightweight" UseColumnHeadersAsSelectors="False"
                                UseRowHeadersAsSelectors="False">
                            </Calendar>
                            <DateInput CssClass="radPreventDecorate" DateFormat="M/d/yyyy" DisplayDateFormat="M/d/yyyy"
                                LabelWidth="64px" Width="">
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDateTimePicker>
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:Button runat="server" ID="btnRecalculate" Text="Save" ValidationGroup="grpSave"
                        CssClass="button" meta:resourcekey="btnRecalculateResource1" />
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-4">
                        <asp:Label ID="lblFromDateGrid" runat="server" Text="From Date" Class="Profiletitletxt"
                            meta:resourcekey="lbldateResource1" />
                        <telerik:RadDatePicker ID="dteFromDateGrid" runat="server" Culture="en-US" Width="180px"
                            meta:resourcekey="dteFromDateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Width="" LabelWidth="64px">
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
                    <div class="col-md-4">
                        <asp:Label ID="lblToDateGrid" runat="server" Text="To Date" Class="Profiletitletxt"
                            meta:resourcekey="lblToDateResource1" />
                        <telerik:RadDatePicker ID="dteToDateGrid" runat="server" Culture="en-US" Width="180px"
                            meta:resourcekey="dteToDateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Width="" LabelWidth="64px">
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
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="dteFromDateGrid"
                            ControlToValidate="dteToDateGrid" ErrorMessage="End Date should be greater than or equal to From Date"
                            Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpFilter"
                            meta:resourcekey="CVDateResource1" />
                        <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator1" ID="ValidatorCalloutExtender2"
                            runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                    <div class="col-md-2">
                        <span>&nbsp</span><br />
                        <asp:Button runat="server" ID="btnFilter" Text="Filter" ValidationGroup="grpFilter"
                            CssClass="button" meta:resourcekey="btnFilterResource1" />
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdRecalculateRequest"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                    </telerik:RadFilter>
                    <telerik:RadGrid ID="dgrdRecalculateRequest" runat="server" AllowPaging="True" AllowSorting="True"
                        GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" AutoGenerateColumns="False"
                        PageSize="15" OnItemCommand="dgrdRecalculateRequest_ItemCommand" ShowFooter="True"
                        meta:resourcekey="dgrdRecalculateRequest" CellSpacing="0">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView CommandItemDisplay="Top">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="CompanyName" Display="False" HeaderText="Company Name"
                                    SortExpression="CompanyName" Resizable="False" meta:resourcekey="GridBoundColumnResource1"
                                    UniqueName="CompanyName" FilterControlAltText="Filter CompanyName column" />
                                <telerik:GridBoundColumn DataField="CompanyArabicName" Display="False" HeaderText="Company Name"
                                    SortExpression="CompanyArabicName" Resizable="False" meta:resourcekey="GridBoundColumnResource2"
                                    UniqueName="CompanyArabicName" FilterControlAltText="Filter CompanyArabicName column" />
                                <telerik:GridBoundColumn DataField="EntityName" Display="False" HeaderText="Entity Name"
                                    SortExpression="EntityName" Resizable="False" meta:resourcekey="GridBoundColumnResource3"
                                    UniqueName="EntityName" FilterControlAltText="Filter EntityName column" />
                                <telerik:GridBoundColumn DataField="EntityArabicName" Display="False" HeaderText="Entity Name"
                                    SortExpression="EntityArabicName" Resizable="False" meta:resourcekey="GridBoundColumnResource4"
                                    UniqueName="EntityArabicName" FilterControlAltText="Filter EntityArabicName column" />
                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee Number" SortExpression="EmployeeNo"
                                    Resizable="False" meta:resourcekey="GridBoundColumnResource5" UniqueName="EmployeeNo"
                                    FilterControlAltText="Filter EmployeeNo column" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="RequestId" HeaderText="RequestId"
                                    SortExpression="RequestId" Visible="False" meta:resourcekey="GridBoundColumnResource7"
                                    UniqueName="RequestId" FilterControlAltText="Filter RequestId column" />
                                <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date" SortExpression="FromDate"
                                    Resizable="False" meta:resourcekey="GridBoundColumnResource8" UniqueName="FromDate"
                                    DataFormatString="{0:dd/MM/yyyy}" FilterControlAltText="Filter FromDate column" />
                                <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" SortExpression="ToDate"
                                    Resizable="False" meta:resourcekey="GridBoundColumnResource9" UniqueName="ToDate"
                                    DataFormatString="{0:dd/MM/yyyy}" FilterControlAltText="Filter ToDate column" />
                                <telerik:GridBoundColumn DataField="ImmediatelyStart" HeaderText="Immediately Start"
                                    SortExpression="ImmediatelyStart" Resizable="False" meta:resourcekey="GridBoundColumnResource10"
                                    UniqueName="ImmediatelyStart" FilterControlAltText="Filter ImmediatelyStart column" />
                                <telerik:GridBoundColumn DataField="RequestStartDateTime" HeaderText="Request Start Date Time"
                                    SortExpression="RequestStartDateTime" Resizable="False" meta:resourcekey="GridBoundColumnResource11"
                                    UniqueName="RequestStartDateTime" FilterControlAltText="Filter RequestStartDateTime column"
                                    DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                                <telerik:GridBoundColumn DataField="RecalStartDateTime" HeaderText="Recalculate Start Date Time"
                                    SortExpression="RecalStartDateTime" Resizable="False" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                    meta:resourcekey="GridBoundColumnResource12" UniqueName="RecalStartDateTime"
                                    FilterControlAltText="Filter RecalStartDateTime column" />
                                <telerik:GridBoundColumn DataField="RecalStatus" Display="False" HeaderText="Recalculate Status"
                                    SortExpression="RecalStatus" Resizable="False" meta:resourcekey="GridBoundColumnResource13"
                                    UniqueName="RecalStatus" FilterControlAltText="Filter RecalStatus column" />
                                <telerik:GridTemplateColumn DataField="RecalStatus" HeaderText="Recalculate Status"
                                    UniqueName="ImgRecalStatus" FilterControlAltText="Filter ImgRecalStatus column"
                                    meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgRecalStatus" ImageUrl="~/assets/img/loading.gif"
                                            Width="34px" meta:resourcekey="imgRecalStatusResource1" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Delete" UniqueName="Delete" meta:resourcekey="Delete">
                                    <ItemTemplate>
                                        <asp:ImageButton ImageUrl="../assets/img/rubbish-bin.png" ID="imgDelete" OnCommand="imgDelete_OnCommand"
                                            Visible="false" Width="34px" CommandArgument='<%# Eval("RequestId") %>' runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ReCalEndDateTime" HeaderText="ReCal End Date Time"
                                    SortExpression="ReCalEndDateTime" Resizable="False" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                    meta:resourcekey="GridBoundColumnResource14" UniqueName="ReCalEndDateTime" FilterControlAltText="Filter ReCalEndDateTime column" />
                                <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks"
                                    Resizable="False" meta:resourcekey="GridBoundColumnResource15" UniqueName="Remarks"
                                    FilterControlAltText="Filter Remarks column" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1"
                                            Owner="" />
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                            EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
