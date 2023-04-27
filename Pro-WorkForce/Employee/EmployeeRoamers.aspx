<%@ Page Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="EmployeeRoamers.aspx.vb" Inherits="Employee_EmployeeRoamers"
    Title="Employee Roamers" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <br />
    <asp:UpdatePanel ID="pnlAssignEmployees" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <uc2:PageFilter ID="EmployeeFilterUC" runat="server" OneventCompanySelect="CompanyChanged"
                        OneventEntitySelected="FillEmployee" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="From Date"
                        meta:resourcekey="Label4Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpFromdate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                        PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US"
                        meta:resourcekey="dtpFromdateResource1">
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
                    </div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chckTemporary" runat="server" AutoPostBack="True" Text="Is Temporary"
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
                        <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dtpFromdate" ControlToValidate="dtpEndDate"
                            ErrorMessage="To Date should be greater than or equal to From Date" Display="None"
                            Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave"
                            meta:resourcekey="CVDateResource1" />
                        <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender3"
                            runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="List Of Employees"
                        meta:resourcekey="Label5Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                   <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc;
                        margin-top:5px; border-radius:5px">
                        <asp:CheckBoxList ID="cblEmpList" runat="server" Style="height: 26px" DataTextField="EmployeeName"
                            DataValueField="EmployeeId" meta:resourcekey="cblEmpListResource1">
                        </asp:CheckBoxList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-2">
                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                        <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a></td>
                </div>
                <div class="col-md-2">
                        <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                            <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                </div>
                <div class="col-md-2">
                    <asp:HyperLink ID="hlViewEmployee" runat="server" Visible="False" meta:resourcekey="hlViewEmployeeResource1">View Org Level Employees </asp:HyperLink>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-12" align="center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="110px" ValidationGroup="grpSave"
                        CssClass="button" meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnNew" runat="server" Text="New" CssClass="button" meta:resourcekey="btnNewResource1" />
                    <asp:Button ID="btnDelete" OnClientClick="return ValidateDelete();" runat="server" CausesValidation="False" CssClass="button"
                        Text="Delete" meta:resourcekey="btnDeleteResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                    <div class="filterDiv">
                        <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdSchedule_Roamer"
                            ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1">
                            <ContextMenu FeatureGroupID="rfContextMenu">
                            </ContextMenu>
                        </telerik:RadFilter>
                    </div>
                    <telerik:RadGrid runat="server" ID="dgrdSchedule_Roamer" AutoGenerateColumns="False"
                        PageSize="15"  AllowPaging="True" AllowSorting="True" CellSpacing="0"
                        GridLines="None" meta:resourcekey="dgrdSchedule_RoamerResource1">
                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="RoamerId,CompanyId,EntityId,EmployeeId,EmployeeName,EmployeeArabicName">
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" meta:resourcekey="RadToolBar1Resource1"
                                    SingleClick="None">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                            ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource1"
                                            runat="server" />
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                    UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="RoamerId" SortExpression="RoamerId" Visible="False"
                                    FilterControlAltText="Filter RoamerId column" meta:resourcekey="GridBoundColumnResource1"
                                    UniqueName="RoamerId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeId" SortExpression="EmployeeId" Visible="False"
                                    FilterControlAltText="Filter EmployeeId column" meta:resourcekey="GridBoundColumnResource2"
                                    UniqueName="EmployeeId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CompanyId" SortExpression="CompanyId" Visible="False"
                                    FilterControlAltText="Filter CompanyId column" meta:resourcekey="GridBoundColumnResource3"
                                    UniqueName="CompanyId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EntityId" SortExpression="EntityId" Visible="False"
                                    FilterControlAltText="Filter EntityId column" meta:resourcekey="GridBoundColumnResource4"
                                    UniqueName="EntityId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No." SortExpression="EmployeeNo"
                                    meta:resourcekey="GridBoundColumn1Resource1" FilterControlAltText="Filter EmployeeNo column"
                                    UniqueName="EmployeeNo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"
                                    meta:resourcekey="GridBoundColumn2Resource1" FilterControlAltText="Filter EmployeeName column"
                                    UniqueName="EmployeeName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Arabic Name"
                                    meta:resourcekey="GridBoundColumn3Resource1" SortExpression="EmployeeArabicName"
                                    FilterControlAltText="Filter EmployeeArabicName column" UniqueName="EmployeeArabicName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FromDate" HeaderText="FromDate" SortExpression="FromDate"
                                    meta:resourcekey="GridBoundColumn7Resource1" DataFormatString="{0:MM/d/yyyy}"
                                    FilterControlAltText="Filter FromDate column" UniqueName="FromDate">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" SortExpression="ToDate"
                                    meta:resourcekey="GridBoundColumn8Resource1" DataFormatString="{0:MM/d/yyyy}"
                                    FilterControlAltText="Filter ToDate column" UniqueName="ToDate">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" SortExpression="CompanyName"
                                    meta:resourcekey="GridBoundColumn9Resource1" FilterControlAltText="Filter CompanyName column"
                                    UniqueName="CompanyName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CompanyArabicName" HeaderText="Company Arabic Name"
                                    meta:resourcekey="GridBoundColumn10Resource1" SortExpression="CompanyArabicName"
                                    FilterControlAltText="Filter CompanyArabicName column" UniqueName="CompanyArabicName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EntityName" HeaderText="Entity Name" SortExpression="EntityName"
                                    meta:resourcekey="GridBoundColumn11Resource1" FilterControlAltText="Filter EntityName column"
                                    UniqueName="EntityName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EntityArabicName" HeaderText="Entity Arabic Name"
                                    meta:resourcekey="GridBoundColumn12Resource1" SortExpression="EntityArabicName"
                                    FilterControlAltText="Filter EntityArabicName column" UniqueName="EntityArabicName">
                                </telerik:GridBoundColumn>
                            </Columns>
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
            <script type="text/javascript">

                function ValidateDelete(sender, eventArgs) {
                    var grid = $find("<%=dgrdSchedule_Roamer.ClientID %>");
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
