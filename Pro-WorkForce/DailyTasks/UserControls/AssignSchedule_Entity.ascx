<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AssignSchedule_Entity.ascx.vb"
    Inherits="DailyTasks_UserControls_AssignSchedule_Entity" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<style type="text/css">
    .style1 {
    }

    .td1 {
        padding-bottom: 20px;
    }

    .style2 {
        height: 20px;
        width: 495px;
    }

    .style3 {
        padding-bottom: 20px;
        height: 20px;
    }

    .style6 {
        padding-bottom: 20px;
        width: 495px;
    }
</style>
<%--<center>
    <uc1:PageHeader ID="Assign_Entity" runat="server"  />
</center>--%>

<div class="row">
    <div class="col-md-2">
        <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" Text="Company"
            meta:resourcekey="lblCompanyResource1"></asp:Label>
    </div>
    <div class="col-md-4">
        <telerik:RadComboBox ID="RadCmbBxCompany" AutoPostBack="true" AllowCustomText="false"
            MarkFirstMatch="true" CausesValidation="false" Skin="Vista" runat="server">
        </telerik:RadComboBox>
        <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ValidationGroup="grpSave_entity"
            InitialValue="--Please Select--" ControlToValidate="RadCmbBxCompany" Display="None"
            ErrorMessage="Please Select Company" meta:resourcekey="rfvCompanyResource1"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="vceCompany" runat="server" CssClass="AISCustomCalloutStyle"
            TargetControlID="rfvCompany" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
        </cc1:ValidatorCalloutExtender>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <asp:Label ID="lblentity" runat="server" Text="Entity" CssClass="Profiletitletxt"
            meta:resourcekey="lblentityResource1"></asp:Label>
    </div>
    <div class="col-md-4">
        <telerik:RadComboBox ID="RadCmbBxEntity" AllowCustomText="false" MarkFirstMatch="true"
            Skin="Vista" runat="server" CausesValidation="false" AutoPostBack="true">
        </telerik:RadComboBox>
        <asp:RequiredFieldValidator ID="rfvEntity" runat="server" ControlToValidate="RadCmbBxEntity"
            InitialValue="--Please Select--" Display="None" ErrorMessage="Please Select Entity"
            meta:resourcekey="rfvEntityResource1" ValidationGroup="grpSave_entity"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="vceEntity" runat="server" CssClass="AISCustomCalloutStyle"
            TargetControlID="rfvEntity" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
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
                <%--  <telerik:RadComboBoxItem Text="--Please Select--" Value="-1" runat="server" meta:resourcekey="RadComboBoxItemResource1" />
                    <telerik:RadComboBoxItem Text="Normal" Value="1" runat="server" meta:resourcekey="RadComboBoxItemResource2" />
                    <telerik:RadComboBoxItem Text="Flexible" Value="2" runat="server" meta:resourcekey="RadComboBoxItemResource3" />
                    <telerik:RadComboBoxItem Text="Advanced" Value="3" runat="server" meta:resourcekey="RadComboBoxItemResource4" />--%>
            </Items>
        </telerik:RadComboBox>
        <asp:RequiredFieldValidator ID="rfvScheduletype" ValidationGroup="grpSave_entity"
            InitialValue="--Please Select--" runat="server" ControlToValidate="RadCmbBxScheduletype"
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
        <asp:RequiredFieldValidator ID="rfvSchedule" ValidationGroup="grpSave_entity" runat="server"
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
        <asp:RequiredFieldValidator ID="rfvdtpFromDate" ValidationGroup="grpSave_entity"
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
        <asp:CheckBox ID="chckTemporary" runat="server" CausesValidation="false" AutoPostBack="True"
            Text="Is Temporary"
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
            <asp:RequiredFieldValidator ID="rfvdtpEndDate" ValidationGroup="grpSave_entity"
                runat="server" ControlToValidate="dtpEndDate"
                Display="None" ErrorMessage="Please Select End Date" meta:resourcekey="rfvScheduleResource12"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vcerfvdtpEndDate" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="rfvdtpEndDate" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
            </cc1:ValidatorCalloutExtender>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="dtpFromdate" ControlToValidate="dtpEndDate"
                Visible="false" ErrorMessage="End Date should be greater than or equal to From Date"
                Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave_entity"
                meta:resourcekey="CVDateResource1"></asp:CompareValidator>
            <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dtpFromdate" ControlToValidate="dtpEndDate"
                Visible="false" ErrorMessage="End Date should be greater than or equal to From Date"
                Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave_entity"
                meta:resourcekey="CVDateResource1"></asp:CompareValidator>
            <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender2"
                runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
</asp:Panel>
<div class="row" id="trControls" runat="server">
    <div class="col-md-12 text-center">
        <asp:Button ID="btnSave" runat="server" Text="Assign Schedule" ValidationGroup="grpSave_entity"
            OnClientClick="validate()" CssClass="button" meta:resourcekey="btnSaveResource1" />
        <asp:Button ID="btnNew" runat="server" Text="New" CssClass="button" meta:resourcekey="btnNewResource1" />
    </div>
</div>
<asp:HiddenField runat="server" ID="hdnWindow" />
<cc1:ModalPopupExtender ID="mpeSave" runat="server" BehaviorID="modelPopupExtender6"
    PopupControlID="pnlPopup" TargetControlID="hdnWindow" CancelControlID="btnCancel"
    DropShadow="True" Enabled="True" BackgroundCssClass="ModalBackground" DynamicServicePath="">
</cc1:ModalPopupExtender>
<div id="pnlPopup" class="commonPopup" style="display: none">
    <div class="row">
        <div class="col-md-6">
            <asp:Label ID="lblCheck" runat="server" Text="Do You Want To Include Sub Entitie(s)?"
                CssClass="Profiletitletxt" meta:resourcekey="lblCheckResource1" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 text-center">
            <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="button" meta:resourcekey="btnYesResource1" />
            <asp:Button ID="btnNo" runat="server" Text="No" CssClass="button" meta:resourcekey="btnNoResource1" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" meta:resourcekey="btnCancelResource1" />
        </div>
    </div>
</div>
<div class="row">
    <div class="table-responsive">
        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" />
        <div class="filterDiv">
            <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdSchedule_Entity"
                ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
        </div>
        <telerik:RadGrid runat="server" ID="dgrdSchedule_Entity" AutoGenerateColumns="false"
            PageSize="15" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="false"
            GroupingSettings-CaseSensitive="false">
            <MasterTableView IsFilterItemExpanded="true" CommandItemDisplay="Top" AllowFilteringByColumn="false" DataKeyNames="EmpWorkScheduleId,CompanyId,EntityId,ScheduleId">
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
                    <telerik:GridBoundColumn DataField="EntityId" HeaderText="EntityId" SortExpression="EntityId"
                        Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ScheduleId" HeaderText="ScheduleId" SortExpression="ScheduleId"
                        Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ScheduleName" HeaderText="Schedule Name" SortExpression="ScheduleName"
                        meta:resourcekey="GridBoundColumn1Resource1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ScheduleArabicName" HeaderText="Schedule Arabic Name"
                        SortExpression="ScheduleArabicName" meta:resourcekey="GridBoundColumn2Resource1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ScheduleType" HeaderText="Schedule Type" SortExpression="ScheduleType"
                        meta:resourcekey="GridBoundColumn3Resource1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date" SortExpression="FromDate"
                        DataFormatString="{0:MM/d/yyyy}" meta:resourcekey="GridBoundColumn4Resource1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" SortExpression="ToDate"
                        DataFormatString="{0:MM/d/yyyy}" meta:resourcekey="GridBoundColumn5Resource1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" SortExpression="CompanyName"
                        meta:resourcekey="GridBoundColumn6Resource1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CompanyArabicName" HeaderText="Company Arabic Name"
                        SortExpression="CompanyArabicName" meta:resourcekey="GridBoundColumn7Resource1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EntityName" HeaderText="Entity Name" SortExpression="EntityName"
                        meta:resourcekey="GridBoundColumn8Resource1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EntityArabicName" HeaderText="Entity Arabic Name"
                        SortExpression="EntityArabicName" meta:resourcekey="GridBoundColumn9Resource1">
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView><ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </div>
</div>
