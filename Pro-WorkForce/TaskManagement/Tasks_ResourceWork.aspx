<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Tasks_ResourceWork.aspx.vb" Inherits="Definitions_Tasks_ResourceWork" Theme="SvTheme" meta:resourcekey="PageResource1" UICulture="auto" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagPrefix="uc1" TagName="EmployeeFilter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" />
            <div class="row">
                <%-- <div class="table-responsive">--%>
                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                <div class="row">
                    <div class="col-md-12">
                        <uc1:EmployeeFilter ID="EmployeeFilter1"
                            OneventEmployeeSelect="EmployeeFilter1_eventEmployeeSelect" runat="server" ShowRadioSearch="true" ShowDirectStaffCheck="true" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lbltasks" runat="server" Text="Tasks" CssClass="Profiletitletxt"
                            meta:resourcekey="lbltasksResource1" />
                    </div>
                    <div class="col-md-4">
                        <telerik:RadComboBox ID="RadTasks" MarkFirstMatch="True" Skin="Vista" Width="210px"
                            ToolTip="View Tasks" runat="server" meta:resourcekey="RadTasksResource1" AutoPostBack="True" OnSelectedIndexChanged="RadTasks_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblPlannedStartDate" runat="server" Text="Planned Start Date" CssClass="Profiletitletxt"
                            meta:resourcekey="lblPlannedStartDateResource" />
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker Enabled="false" ID="dtpPlannedStartDate" runat="server" AllowCustomText="false" Culture="en-US"
                            MarkFirstMatch="true" PopupDirection="TopRight"
                            ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpPlannedStartDateResource">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                Width="" LabelWidth="64px">
                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                <FocusedStyle Resize="None"></FocusedStyle>
                                <DisabledStyle Resize="None"></DisabledStyle>
                                <InvalidStyle Resize="None"></InvalidStyle>
                                <HoveredStyle Resize="None"></HoveredStyle>
                                <EnabledStyle Resize="None"></EnabledStyle>
                            </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="Label1" runat="server" Text="Planned End Date" CssClass="Profiletitletxt"
                            meta:resourcekey="lblPlannedEndDateResource" />
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker Enabled="false" ID="dtpPlannedEndDate" runat="server" AllowCustomText="false" Culture="en-US"
                            MarkFirstMatch="true" PopupDirection="TopRight"
                            ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpPlannedEndDateResource">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                Width="" LabelWidth="64px">
                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                <FocusedStyle Resize="None"></FocusedStyle>
                                <DisabledStyle Resize="None"></DisabledStyle>
                                <InvalidStyle Resize="None"></InvalidStyle>
                                <HoveredStyle Resize="None"></HoveredStyle>
                                <EnabledStyle Resize="None"></EnabledStyle>
                            </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblActualStartDate" runat="server" Text="Actual Start Date" CssClass="Profiletitletxt"
                            meta:resourcekey="lblActualStartDateResource" />
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker Enabled="false" ID="dtpActualStartDate" runat="server" AllowCustomText="false" Culture="en-US"
                            MarkFirstMatch="true" PopupDirection="TopRight"
                            ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpActualEndDateResource">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                Width="" LabelWidth="64px">
                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                <FocusedStyle Resize="None"></FocusedStyle>
                                <DisabledStyle Resize="None"></DisabledStyle>
                                <InvalidStyle Resize="None"></InvalidStyle>
                                <HoveredStyle Resize="None"></HoveredStyle>
                                <EnabledStyle Resize="None"></EnabledStyle>
                            </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                    </div>
                </div>


                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblActualEndDate" runat="server" Text="Actual End Date" CssClass="Profiletitletxt"
                            meta:resourcekey="lblActualEndDateResource" />
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker Enabled="false" ID="dtpActualEndDate" runat="server" AllowCustomText="false" Culture="en-US"
                            MarkFirstMatch="true" PopupDirection="TopRight"
                            ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpActualEndDateResource">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                Width="" LabelWidth="64px">
                                <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                <FocusedStyle Resize="None"></FocusedStyle>
                                <DisabledStyle Resize="None"></DisabledStyle>
                                <InvalidStyle Resize="None"></InvalidStyle>
                                <HoveredStyle Resize="None"></HoveredStyle>
                                <EnabledStyle Resize="None"></EnabledStyle>
                            </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblDescription" runat="server" Text="Description" CssClass="Profiletitletxt"
                            meta:resourcekey="lblDescriptionResource" />
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDescription" runat="server" Text="Description" CssClass="Profiletitletxt" Enabled="false"
                            meta:resourcekey="txtDescriptionResource" />
                    </div>
                </div>


                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblPriority" runat="server" Text="Priority" CssClass="Profiletitletxt"
                            meta:resourcekey="lblPriorityResource" />
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtPriority" runat="server" Text="" CssClass="Profiletitletxt" Enabled="false"
                            meta:resourcekey="txtPriorityResource" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="Profiletitletxt" 
                            meta:resourcekey="lblStatusResource" />
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtStatus" runat="server" Text="" CssClass="Profiletitletxt" Enabled="false"
                            meta:resourcekey="txtStatusResource" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <asp:Label ID="lblTotalcompletionpercentage" runat="server" Text="Total Completion Percentage" Enabled="false"  CssClass="Profiletitletxt"
                            meta:resourcekey="lblTotalcompletionpercentageResource" />
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtTotalcompletionpercentage" runat="server" Text="0" CssClass="Profiletitletxt" Enabled="false"
                            meta:resourcekey="txtTotalcompletionpercentageResource" />
                    </div>
                </div>

                <%--   </div>--%>

                <%--<cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"
                    meta:resourcekey="TabContainer1Resource1">
                    <cc1:TabPanel ID="TabResourceOfWorkDetails" runat="server" HeaderText="Resource Of Work Details" meta:resourcekey="TabResourceOfWorkDetailsResource1">
                        <ContentTemplate>--%>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks" meta:resourcekey="lblRemarksResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtRemarks" runat="server" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblCompletionPercentage" runat="server" Text="Completion Percentage" meta:resourcekey="lblCompletionPercentageResource"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtCompletionPercentage" runat="server" meta:resourcekey="txtCompletionPercentageResource"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblStartDateTime" runat="server" Text="Start Date Time" meta:resourcekey="lblStartDateTimeResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDateTimePicker ID="dtpStartDateTime" runat="server" AllowCustomText="false"
                                        MarkFirstMatch="true" PopupDirection="TopRight" Culture="nl"
                                        ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpStartDateTimeResource1">
                                        <Calendar   UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy HH:mm" DisplayDateFormat="dd/MM/yyyy HH:mm"
                                            Width="" LabelWidth="64px">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                        </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDateTimePicker>
                               <%--     <asp:RequiredFieldValidator ID="rfvStartDateTime" runat="server" ControlToValidate="dtpStartDateTime"
                                        Display="None" ErrorMessage="Please Select Planned Start Date" ValidationGroup="grpSave" meta:resourcekey="rfvStartDateTimeResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceStartDateTime" runat="server" TargetControlID="rfvStartDateTime"
                                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>--%>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblEndDateTime" runat="server" Text="End Date Time" meta:resourcekey="lblEndDateTimeResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDateTimePicker ID="dtpEndDateTime" runat="server" AllowCustomText="false" Culture="nl"
                                        MarkFirstMatch="true" PopupDirection="TopRight"
                                        ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpEndDateTimeResource1">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy HH:mm" DisplayDateFormat="dd/MM/yyyy HH:mm"
                                            Width="" LabelWidth="64px">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                            <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                            <FocusedStyle Resize="None"></FocusedStyle>
                                            <DisabledStyle Resize="None"></DisabledStyle>
                                            <InvalidStyle Resize="None"></InvalidStyle>
                                            <HoveredStyle Resize="None"></HoveredStyle>
                                            <EnabledStyle Resize="None"></EnabledStyle>
                                        </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDateTimePicker>
                                   <%-- <asp:RequiredFieldValidator ID="rfvEndDateTime" runat="server" ControlToValidate="dtpEndDateTime"
                                        Display="None" ErrorMessage="Please Select Planned End Date" ValidationGroup="grpSave" meta:resourcekey="rfvEndDateTimeResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceEndDateTime" runat="server" TargetControlID="rfvEndDateTime"
                                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                    <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dtpStartDateTime"
                                        ControlToValidate="dtpEndDateTime" ErrorMessage="Planned End Date Should be Greater Than or Equal to Planned Start Date"
                                        Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave" meta:resourcekey="CVDateResource1" />
                                    <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="vceCVDate"
                                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>--%>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="LeavesGroups"
                                        meta:resourcekey="btnSaveResource1" />
                                    <asp:Button ID="btnDelete" OnClientClick="return ValidateDelete();" runat="server"
                                        CausesValidation="False" CssClass="button" Text="Delete" meta:resourcekey="btnDeleteResource1" />
                                    <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="button"
                                        Text="Clear" meta:resourcekey="btnClearResource1" />
                                </div>
                            </div>
          <%--              </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>--%>

                <div class="table-responsive">
                    <telerik:RadGrid runat="server" ID="dgrdResourceWork" AutoGenerateColumns="False" PageSize="15" OnSelectedIndexChanged="dgrdResourceWork_SelectedIndexChanged"
                        AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" meta:resourcekey="dgrdResourceWorkResource1">
                        <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False" DataKeyNames="ResourceWorkId">
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick" SingleClick="None" meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                            ImageUrl="~/images/RadFilter.gif" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2" FilterControlAltText="Filter TemplateColumn column">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ResourceWorkId" HeaderText="ResourceWorkId"
                                    AllowFiltering="False" SortExpression="ResourceWorkId" Display="False" FilterControlAltText="Filter ResourceWorkId column" meta:resourcekey="GridBoundColumnResource1" UniqueName="ResourceWorkId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="StartDateTime" HeaderText="Start Date Time" SortExpression="StartDateTime" Resizable="False"
                                    DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" FilterControlAltText="Filter StartDateTime column" meta:resourcekey="GridBoundColumnResource4" UniqueName="StartDateTime">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EndDateTime" HeaderText="End Date Time" SortExpression="EndDateTime" Resizable="False"
                                    DataFormatString="{0:dd/MM/yyyy hh:mm:ss}" FilterControlAltText="Filter EndDateTime column" meta:resourcekey="GridBoundColumnResource5" UniqueName="EndDateTime">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="completionpercentage_value" HeaderText="Completion Percentage" SortExpression="completionpercentage_value" Resizable="False"
                                    FilterControlAltText="Filter Completion Percentage Column" meta:resourcekey="GridBoundColumnResource2" UniqueName="completionpercentage_value">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks"
                                    SortExpression="Remarks" FilterControlAltText="Filter Remarks column" meta:resourcekey="GridBoundColumnResource3" UniqueName="Remarks" />
                            </Columns>
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False"></GroupingSettings>

                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                            EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

