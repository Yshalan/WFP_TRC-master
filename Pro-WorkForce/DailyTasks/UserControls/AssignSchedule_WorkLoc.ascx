<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AssignSchedule_WorkLoc.ascx.vb"
    Inherits="DailyTasks_UserControls_AssignSchedule_WorkLoc" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<style type="text/css">
    .style1
    {
    }
    .td1
    {
        padding-bottom: 20px;
    }
    .style2
    {
        height: 20px;
        width: 495px;
    }
    .style3
    {
        padding-bottom: 20px;
        height: 20px;
    }
    .style6
    {
        padding-bottom: 20px;
        width: 495px;
    }
</style>
<%--<center>
    <uc1:PageHeader ID="Assign_WorkLoc" runat="server" HeaderText="Assign Schedule for Work Location" />
</center>--%>

    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" Text="Company"
                meta:resourcekey="lblCompanyResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="RadCmbBxCompany" MarkFirstMatch="True" Skin="Vista" runat="server"
                AutoPostBack="true" CausesValidation="false">
            </telerik:RadComboBox>
            <asp:RequiredFieldValidator ID="RFVCompany" ValidationGroup="grpSave_WorkLoc" InitialValue="--Please Select--"
                runat="server" ControlToValidate="RadCmbBxCompany" Display="None" ErrorMessage="Please Select Company"
                meta:resourcekey="RFVCompanyResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="VCECompany" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="RFVCompany" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblWorkLoc" runat="server" CssClass="Profiletitletxt" Text="Work Location"
                meta:resourcekey="lblWorkLocResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="RadCmbBxWorkLoc" MarkFirstMatch="True" Skin="Vista" runat="server"
                AutoPostBack="true" CausesValidation="false">
            </telerik:RadComboBox>
            <asp:RequiredFieldValidator ID="RFVWorkLoc" ValidationGroup="grpSave_WorkLoc" InitialValue="--Please Select--"
                runat="server" ControlToValidate="RadCmbBxWorkLoc" Display="None" ErrorMessage="Please Select Work Location"
                meta:resourcekey="RFVWorkLocResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="VCEWorkLoc" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="RFVWorkLoc" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="Label7" runat="server" CssClass="Profiletitletxt" Text="Schedule Type"
                meta:resourcekey="Label7Resource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="RadCmbBxScheduletype" MarkFirstMatch="True" Skin="Vista"
                runat="server" AutoPostBack="True" CausesValidation="false" meta:resourcekey="RadCmbBxScheduletypeResource1">
                <Items>
                    <%-- <telerik:RadComboBoxItem Text="--Please Select--" Value="-1" runat="server" meta:resourcekey="RadComboBoxItemResource1" />
                    <telerik:RadComboBoxItem Text="Normal" Value="1" runat="server" meta:resourcekey="RadComboBoxItemResource2" />
                    <telerik:RadComboBoxItem Text="Flexible" Value="2" runat="server" meta:resourcekey="RadComboBoxItemResource3" />
                    <telerik:RadComboBoxItem Text="Advanced" Value="3" runat="server" meta:resourcekey="RadComboBoxItemResource4" />--%>
                </Items>
            </telerik:RadComboBox>
            <asp:RequiredFieldValidator ID="rfvScheduletype" ValidationGroup="grpSave_WorkLoc"
                runat="server" ControlToValidate="RadCmbBxScheduletype" InitialValue="--Please Select--"
                Display="None" ErrorMessage="Please Select Schedule Type" meta:resourcekey="rfvScheduletypeResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="rfvScheduletype" WarningIconImageUrl="~/images/warning1.png"
                Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="Label3" runat="server" CssClass="Profiletitletxt" Text="Schedules"
                meta:resourcekey="Label3Resource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="RadCmbBxSchedules" MarkFirstMatch="True" Skin="Vista" runat="server"
                meta:resourcekey="RadCmbBxSchedulesResource1">
            </telerik:RadComboBox>
            <asp:RequiredFieldValidator ID="rfvSchedule" ValidationGroup="grpSave_WorkLoc" runat="server"
                InitialValue="--Please Select--" ControlToValidate="RadCmbBxSchedules" Display="None"
                ErrorMessage="Please Select Schedule" meta:resourcekey="rfvScheduleResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vceSchedule" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="rfvSchedule" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="From Date"
                meta:resourcekey="Label4Resource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadDatePicker ID="dtpFromdate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="English (United States)"
                meta:resourcekey="dtpFromdateResource1">
                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                </Calendar>
                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                    Width="">
                </DateInput>
                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
            </telerik:RadDatePicker>
               <asp:RequiredFieldValidator ID="rfvdtpFromDate" ValidationGroup="grpSave_WorkLoc"
                 runat="server" ControlToValidate="dtpFromdate"
                Display="None" ErrorMessage="Please Select From Date" meta:resourcekey="rfvScheduleResource11"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vcerfvdtpFromDate" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="rfvdtpFromDate" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
        </div>
        <div class="col-md-4">
            <asp:CheckBox ID="chckTemporary" runat="server" AutoPostBack="True" CausesValidation="false" Text="Is Temporary"
                meta:resourcekey="Label6Resource1" />
        </div>

    </div>
    <asp:Panel ID="pnlEndDate" runat="server" Visible="False" meta:resourcekey="pnlEndDateResource1">
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblEndDate" runat="server" CssClass="Profiletitletxt" Text="End date"
                    meta:resourcekey="lblEndDateResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadDatePicker ID="dtpEndDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                    PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US"
                    meta:resourcekey="dtpEndDateResource1">
                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                    </Calendar>
                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                        Width="">
                    </DateInput>
                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                </telerik:RadDatePicker>
                 <asp:RequiredFieldValidator ID="rfvdtpEndDate" ValidationGroup="grpSave_WorkLoc"
                            runat="server" ControlToValidate="dtpEndDate"
                            Display="None" ErrorMessage="Please Select End Date" meta:resourcekey="rfvScheduleResource12"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="vcerfvdtpEndDate" runat="server" CssClass="AISCustomCalloutStyle"
                            TargetControlID="rfvdtpEndDate" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                        </cc1:ValidatorCalloutExtender>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="dtpFromdate" ControlToValidate="dtpEndDate"
                            Visible="false" ErrorMessage="End Date should be greater than or equal to From Date"
                            Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave_company"
                            meta:resourcekey="CVDateResource1"></asp:CompareValidator>
                <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dtpFromdate" ControlToValidate="dtpEndDate"
                    Visible="false" ErrorMessage="End Date should be greater than or equal to From Date"
                    Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave_company_company"
                    meta:resourcekey="CVDateResource1"></asp:CompareValidator>
                <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender2"
                    runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                </cc1:ValidatorCalloutExtender>
            </div>
        </div>
    </asp:Panel>
    <div class="row" id="trControls" runat="server">
        <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Assign Schedule"  ValidationGroup="grpSave_WorkLoc"
                    CssClass="button" meta:resourcekey="btnSaveResource1" />
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="button" meta:resourcekey="btnNewResource1" />
        </div>
    </div>
    <div class="row">
        <div class="table-responsive">
            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" />
            <div class="filterDiv">
                <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdSchedule_WorkLocation"
                    ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
            </div>
            <telerik:RadGrid runat="server" ID="dgrdSchedule_WorkLocation" AutoGenerateColumns="false"
                PageSize="15"  AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="false"
                GroupingSettings-CaseSensitive="false">
                <MasterTableView IsFilterItemExpanded="true" CommandItemDisplay="Top" AllowFilteringByColumn="false" DataKeyNames="EmpWorkScheduleId,WorkLocationId,CompanyId,ScheduleId">
                    <CommandItemTemplate>
                        <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick">
                            <Items>
                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                    ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource1" />
                            </Items>
                        </telerik:RadToolBar>
                    </CommandItemTemplate>
                    <Columns>
                        <%-- <telerik:GridTemplateColumn AllowFiltering="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridBoundColumn DataField="EmpWorkScheduleId" HeaderText="EmpWorkScheduleId"
                            SortExpression="EmpWorkScheduleId" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CompanyId" HeaderText="CompanyId" SortExpression="CompanyId"
                            Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="WorkLocationId" HeaderText="WorkLocationId" SortExpression="WorkLocationId"
                            Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ScheduleId" HeaderText="ScheduleId" SortExpression="ScheduleId"
                            Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="WorkLocationName" HeaderText="WorkLocation Name"
                            SortExpression="WorkLocationName" meta:resourcekey="GridBoundColumn1Resource1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="WorkLocationArabicName" HeaderText="WorkLocation Arabic Name"
                            SortExpression="WorkLocationArabicName" meta:resourcekey="GridBoundColumn2Resource1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ScheduleName" HeaderText="Schedule Name" SortExpression="ScheduleName"
                            meta:resourcekey="GridBoundColumn3Resource1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ScheduleArabicName" HeaderText="Schedule Arabic Name"
                            SortExpression="ScheduleArabicName" meta:resourcekey="GridBoundColumn4Resource1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ScheduleType" HeaderText="Schedule Type" SortExpression="ScheduleType"
                            meta:resourcekey="GridBoundColumn5Resource1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date" SortExpression="FromDate"
                            DataFormatString="{0:MM/d/yyyy}" meta:resourcekey="GridBoundColumn6Resource1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" SortExpression="ToDate"
                            DataFormatString="{0:MM/d/yyyy}" meta:resourcekey="GridBoundColumn7Resource1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" SortExpression="CompanyName"
                            meta:resourcekey="GridBoundColumn8Resource1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CompanyArabicName" HeaderText="Company Arabic Name"
                            SortExpression="CompanyArabicName" meta:resourcekey="GridBoundColumn9Resource1">
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView><ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                    EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
    </div>
