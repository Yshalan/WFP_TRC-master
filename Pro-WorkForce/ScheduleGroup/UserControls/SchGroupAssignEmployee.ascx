<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SchGroupAssignEmployee.ascx.vb"
    Inherits="ScheduleGroup_UserControls_SchGroupAssignEmployee" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../../Admin/UserControls/UserSecurityFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
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
<script type="text/javascript">

    function ValidateDelete(sender, eventArgs) {
        var grid = $find("<%=dgrdSchedule_EmployeeGroups.ClientID %>");
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
    <uc1:PageHeader ID="Assign_Emp" runat="server" />
<asp:UpdatePanel ID="upAssignScheduleEmployee" runat="server">
    <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <uc2:PageFilter ID="EmployeeFilterUC" runat="server" OneventCompanySelect="CompanyChanged"
                        OneventEntitySelected="EntityChanged" ValidationGroup="grpSave_employee" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label7" runat="server" CssClass="Profiletitletxt" Text="Schedule Group"
                        meta:resourcekey="Label7Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="CmbSchGroup" MarkFirstMatch="True" Skin="Vista" runat="server"
                        meta:resourcekey="CmbSchGroupResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvScheduleGroup" ValidationGroup="VGGroup" InitialValue="--Please Select--"
                        runat="server" ControlToValidate="CmbSchGroup" Display="None" ErrorMessage="Please Select Schedule Group"
                        meta:resourcekey="rfvScheduleGroupResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvScheduleGroup" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
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
                        PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US"
                        meta:resourcekey="dtpFromdateResource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
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
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dtpFromdate" ControlToValidate="dtpEndDate"
                            Visible="False" ErrorMessage="End Date should be greater than or equal to From Date"
                            Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="VGGroup"
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
                        meta:resourcekey="lblEmployeeNoResource1" />
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtEmployeeNo" runat="server" meta:resourcekey="txtEmployeeNoResource1" />
                    <asp:Button ID="btnRetrieve" runat="server" Text="Retrieve" CssClass="button" ValidationGroup="validateEmployee"
                        meta:resourcekey="btnRetrieveResource1" />
                    <asp:RequiredFieldValidator ID="rfvEmployeeNo" runat="server" ControlToValidate="txtEmployeeNo"
                        Display="None" ErrorMessage="Please enter Emp No." ValidationGroup="validateEmployee"
                        meta:resourcekey="rfvEmployeeNoResource1" />
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
                                <div style=" height: 200px; overflow: auto; border-style: solid; border-width: 1px;
                                    border-color: #ccc; margin-top:5px; border-radius:5px">
                                    <asp:CheckBoxList ID="cblEmpList" runat="server" Style="height: 26px" DataTextField="EmployeeName"
                                        DataValueField="EmployeeId" meta:resourcekey="cblEmpListResource1">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                           
                                        <div class="col-md-1">
                                            <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                                                <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                                        
                                            <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                                                <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                                       </div>
                <div class="col-md-2">
                    <asp:HyperLink ID="hlViewEmployee" runat="server" Visible="False" Text="View Org Level Employees "
                        meta:resourcekey="hlViewEmployeeResource1"></asp:HyperLink>
                </div>
            </div>
           

         <div class="row">
               <div  class="col-md-2"></div>
                     <div  class="col-md-10">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<% # Eval("PageNo")%>'></asp:LinkButton>|
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="row">
                <div  class="col-md-12"><br /></div> 
                </div> 

            <div class="row" id="trControls" runat="server">
                <div class="col-md-12 text-center">
                        <asp:Button ID="btnSave" runat="server" Text="Assign Schedule Groups"
                            ValidationGroup="VGGroup" CssClass="button" meta:resourcekey="btnSaveResource1" />
                        <asp:Button ID="btnNew" runat="server" Text="New" CssClass="button" meta:resourcekey="btnNewResource1" />
                        <asp:Button ID="btnDelete" OnClientClick="return ValidateDelete();" runat="server" Text="Delete" CssClass="button" meta:resourcekey="btnDeleteResource1"/>
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                    <div class="filterDiv">
                        <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdSchedule_EmployeeGroups"
                            ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
                    </div>
                    <telerik:RadGrid runat="server" ID="dgrdSchedule_EmployeeGroups" AutoGenerateColumns="False"
                        PageSize="15" AllowPaging="True" AllowSorting="True" GridLines="None" AllowFilteringByColumn="false"
                        meta:resourcekey="dgrdSchedule_EmployeeGroupsResource1" GroupingSettings-CaseSensitive="false">
                        <MasterTableView IsFilterItemExpanded="true" CommandItemDisplay="Top" AllowFilteringByColumn="false"  DataKeyNames="GroupEmployeeId,FK_CompanyId,EntityId,FK_EmployeeId,EmployeeName,EmployeeArabicName">
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick"
                                    meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                            ImageUrl="~/images/RadFilter.gif" runat="server" meta:resourcekey="RadToolBarButtonResource1"/>
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="FK_EmployeeId" SortExpression="FK_EmployeeId"
                                    Visible="False" meta:resourcekey="GridBoundColumnResource1" UniqueName="FK_EmployeeId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FK_CompanyId" SortExpression="FK_CompanyId" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource2" UniqueName="FK_CompanyId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EntityId" SortExpression="EntityId" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource3" UniqueName="EntityId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No." SortExpression="EmployeeNo"
                                    meta:resourcekey="GridBoundColumnResource4" UniqueName="EmployeeNo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"
                                    meta:resourcekey="GridBoundColumnResource5" UniqueName="EmployeeName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Arabic Name"
                                    SortExpression="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource6"
                                    UniqueName="EmployeeArabicName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="GroupNameEn" HeaderText="Schedule Group Name"
                                    SortExpression="GroupNameEn" meta:resourcekey="GridBoundColumnResource7" UniqueName="GroupNameEn">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="GroupNameAr" HeaderText="Schedule Group Arabic Name"
                                    SortExpression="GroupNameAr" meta:resourcekey="GridBoundColumnResource8" UniqueName="GroupNameAr">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FromDate" HeaderText="FromDate" SortExpression="FromDate"
                                    DataFormatString="{0:dd/M/yyyy}" meta:resourcekey="GridBoundColumnResource9"
                                    UniqueName="FromDate">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" SortExpression="ToDate"
                                    DataFormatString="{0:dd/M/yyyy}" meta:resourcekey="GridBoundColumnResource10"
                                    UniqueName="ToDate">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" SortExpression="CompanyName"
                                    meta:resourcekey="GridBoundColumnResource11" UniqueName="CompanyName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CompanyArabicName" HeaderText="Company Arabic Name"
                                    SortExpression="CompanyArabicName" meta:resourcekey="GridBoundColumnResource12"
                                    UniqueName="CompanyArabicName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EntityName" HeaderText="Entity Name" SortExpression="EntityName"
                                    meta:resourcekey="GridBoundColumnResource13" UniqueName="EntityName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EntityArabicName" HeaderText="Entity Arabic Name"
                                    SortExpression="EntityArabicName" meta:resourcekey="GridBoundColumnResource14"
                                    UniqueName="EntityArabicName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="GroupEmployeeId" SortExpression="GroupEmployeeId"
                                    Visible="False" meta:resourcekey="GridBoundColumnResource15" UniqueName="GroupEmployeeId">
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
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upAssignScheduleEmployee">
    <ProgressTemplate>
        <div class="modal">
            <div class="center">
                <asp:Image ID="imgLoading" runat="server" ImageUrl="~/images/loading.gif" meta:resourcekey="imgLoadingResource1" />
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
