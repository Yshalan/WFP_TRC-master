<%@ Control Language="VB" AutoEventWireup="false" CodeFile="AssignSchedule_Employee.ascx.vb"
    Inherits="DailyTasks_UserControls_AssignSchedule_Employee" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/UserControls/UserSecurityFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<%@ Register Src="~/Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc3" %>
<script type="text/javascript">
    function showPopup(path, name, height, width) {
        var options = 'width=' + width + ',height=' + height;
        var newwindow;
        newwindow = window.open(path, name, options);
        if (window.focus) {
            newwindow.focus();
        }
    }
    function open_window(url, target, w, h) { //opens new window 
        var parms = "width=" + w + ",height=" + h + ",menubar=no,location=no,resizable,scrollbars";
        var win = window.open(url, target, parms);
        if (win) {
            win.focus();
        }
    }

    function CheckBoxListSelect(state) {
        var chkBoxList = document.getElementById("<%= cblEmpList.ClientID %>");
        var chkBoxCount = chkBoxList.getElementsByTagName("input");
        for (var i = 0; i < chkBoxCount.length; i++) {
            chkBoxCount[i].checked = state;
        }
        return false;
    }


    function ValidateDelete(sender, eventArgs) {
        var grid = $find("<%=dgrdSchedule_Employee.ClientID %>");
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
<%--<style type="text/css">
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
</style>--%>
<%--<center>
    <uc1:PageHeader ID="Assign_Emp" runat="server" />
</center>--%>
<asp:UpdatePanel ID="upAssignScheduleEmployee" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-md-12">
                <uc2:PageFilter ID="EmployeeFilterUC" runat="server" OneventCompanySelect="CompanyChanged"
                    OneventEntitySelected="EntityChanged" CompanyRequiredFieldValidationGroup="ValidateComp"
                    ValidationGroup="ValidateComp" IsCompanyRequired="true" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label7" runat="server" CssClass="Profiletitletxt" Text="Schedule Type"
                    meta:resourcekey="Label7Resource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadComboBox ID="RadCmbBxScheduletype" MarkFirstMatch="True" Skin="Vista"
                    runat="server" AutoPostBack="True" meta:resourcekey="RadCmbBxScheduletypeResource1">
                    <Items>
                        <%-- <telerik:RadComboBoxItem Text="--Please Select--" Value="-1" runat="server" meta:resourcekey="RadComboBoxItemResource1" />
                    <telerik:RadComboBoxItem Text="Normal" Value="1" runat="server" meta:resourcekey="RadComboBoxItemResource2" />
                    <telerik:RadComboBoxItem Text="Flexible" Value="2" runat="server" meta:resourcekey="RadComboBoxItemResource3" />
                    <telerik:RadComboBoxItem Text="Advanced" Value="3" runat="server" meta:resourcekey="RadComboBoxItemResource4" />--%>
                    </Items>
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="rfvScheduletype" ValidationGroup="ValidateComp" InitialValue="--Please Select--"
                    runat="server" ControlToValidate="RadCmbBxScheduletype" Display="None" ErrorMessage="Please Select Schedule Type"
                    meta:resourcekey="rfvScheduletypeResource1"></asp:RequiredFieldValidator>
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
                <asp:RequiredFieldValidator ID="rfvSchedule" ValidationGroup="ValidateComp" runat="server"
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
                <asp:RequiredFieldValidator ID="rfvdtpFromDate" ValidationGroup="ValidateComp" runat="server"
                    ControlToValidate="dtpFromdate" Display="None" ErrorMessage="Please Select From Date"
                    meta:resourcekey="rfvScheduleResource11"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="vcerfvdtpFromDate" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="rfvdtpFromDate" WarningIconImageUrl="~/images/warning1.png"
                    Enabled="True">
                </cc1:ValidatorCalloutExtender>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label6" runat="server" CssClass="Profiletitletxt" Text="Is Temporary"
                    meta:resourcekey="Label6Resource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:CheckBox ID="chckTemporary" runat="server" Text="&nbsp;" AutoPostBack="True" />
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
                    <asp:RequiredFieldValidator ID="rfvdtpEndDate" ValidationGroup="ValidateComp" runat="server"
                        ControlToValidate="dtpEndDate" Display="None" ErrorMessage="Please Select End Date"
                        meta:resourcekey="rfvScheduleResource12"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vcerfvdtpEndDate" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvdtpEndDate" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="dtpFromdate"
                        ControlToValidate="dtpEndDate" Visible="false" ErrorMessage="End Date should be greater than or equal to From Date"
                        Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="ValidateComp"
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
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblEmployeeNo" runat="server" Text="Employee Number" CssClass="Profiletitletxt"
                    meta:resourcekey="lblEmpNoResource1" />
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtEmployeeNo" runat="server" />
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnRetrieve" runat="server" Text="Retrieve" CssClass="button" ValidationGroup="validateEmployee"
                    meta:resourcekey="btnRetrieveResource1" />
                <asp:RequiredFieldValidator ID="rfvEmployeeNo" runat="server" ControlToValidate="txtEmployeeNo"
                    Display="None" ErrorMessage="Please enter Emp No." ValidationGroup="validateEmployee"
                    meta:resourcekey="rfvEmployeeNo" />
                <cc1:ValidatorCalloutExtender ID="vceEmployeeNo" runat="server" Enabled="True" TargetControlID="rfvEmployeeNo">
                </cc1:ValidatorCalloutExtender>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="List Of Employees"
                    meta:resourcekey="Label5Resource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; border-radius: 5px;">
                    <asp:CheckBoxList ID="cblEmpList" runat="server" Style="height: 26px" DataTextField="EmployeeName"
                        DataValueField="EmployeeId" meta:resourcekey="cblEmpListResource1">
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="col-md-2">
                <div>
                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                        <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                </div>
                <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                    <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                <asp:HyperLink ID="hlViewEmployee" runat="server" Visible="False" meta:resourcekey="hlViewEmployeeResource1"
                    Text="View Org Level Employees "></asp:HyperLink>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
            </div>
            <div class="col-md-10">
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<% # Eval("PageNo")%>'></asp:LinkButton>|
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <br />
            </div>
        </div>
        <div id="trControls" runat="server" class="row">
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Assign Schedule" ValidationGroup="ValidateComp"
                    CssClass="button" meta:resourcekey="btnSaveResource1" />
                <asp:Button ID="btnNew" runat="server" Text="New" CssClass="button" meta:resourcekey="btnNewResource1" />
                <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button"
                    OnClientClick="return ValidateDelete();" Text="Delete" meta:resourcekey="btnDeleteResource1" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <uc3:PageFilter ID="EmployeeFilterUC_Grid" runat="server" OneventCompanySelect="CompanyChanged"
                    OneventEntitySelected="FillEmployee" ValidationGroup="ValidateFilter" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:Button ID="btnFilter" runat="server" Text="Get By Filter" class="button" ValidationGroup="ValidateFilter"
                    meta:resourcekey="btnFilterResource1" />
            </div>
        </div>
        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" />
        <div class="filterDiv">
            <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdSchedule_Employee"
                ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
        </div>
        <div class="table-responsive">
            <telerik:RadGrid runat="server" ID="dgrdSchedule_Employee" AutoGenerateColumns="false"
                PageSize="15" AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="false"
                GroupingSettings-CaseSensitive="false">
                <MasterTableView IsFilterItemExpanded="true" CommandItemDisplay="Top" AllowFilteringByColumn="false"
                    DataKeyNames="EmpWorkScheduleId,ScheduleId,CompanyId,EntityId,EmployeeId,EmployeeName,EmployeeArabicName">
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
                        <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                            UniqueName="TemplateColumn">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="EmpWorkScheduleId" SortExpression="EmpWorkScheduleId"
                            Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EmployeeId" SortExpression="EmployeeId" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CompanyId" SortExpression="CompanyId" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EntityId" SortExpression="EntityId" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ScheduleId" SortExpression="ScheduleId" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No." SortExpression="EmployeeNo"
                            meta:resourcekey="GridBoundColumn1Resource1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"
                            meta:resourcekey="GridBoundColumn2Resource1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Arabic Name"
                            meta:resourcekey="GridBoundColumn3Resource1" SortExpression="EmployeeArabicName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ScheduleName" HeaderText="Schedule Name" SortExpression="ScheduleName"
                            meta:resourcekey="GridBoundColumn4Resource1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ScheduleArabicName" HeaderText="Schedule Arabic Name"
                            meta:resourcekey="GridBoundColumn5Resource1" SortExpression="ScheduleArabicName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ScheduleType" HeaderText="Schedule Type" SortExpression="ScheduleType"
                            meta:resourcekey="GridBoundColumn6Resource1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="FromDate" HeaderText="FromDate" SortExpression="FromDate"
                            meta:resourcekey="GridBoundColumn7Resource1" DataFormatString="{0:MM/d/yyyy}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" SortExpression="ToDate"
                            meta:resourcekey="GridBoundColumn8Resource1" DataFormatString="{0:MM/d/yyyy}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" SortExpression="CompanyName"
                            meta:resourcekey="GridBoundColumn9Resource1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CompanyArabicName" HeaderText="Company Arabic Name"
                            meta:resourcekey="GridBoundColumn10Resource1" SortExpression="CompanyArabicName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EntityName" HeaderText="Entity Name" SortExpression="EntityName"
                            meta:resourcekey="GridBoundColumn11Resource1">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EntityArabicName" HeaderText="Entity Arabic Name"
                            meta:resourcekey="GridBoundColumn12Resource1" SortExpression="EntityArabicName">
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView><ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                    EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        <div class="modal">
            <div class="center">
                <asp:Image ID="imgLoading" runat="server" ImageUrl="~/images/loading.gif" />
                <%--<img alt="" src="../../images/loading.gif" />--%>
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
 