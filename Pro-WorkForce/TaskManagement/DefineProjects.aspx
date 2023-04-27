<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="DefineProjects.aspx.vb"
    Inherits="Definitions_DefineProjects" Theme="SvTheme" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/MultiEmployeeFilter.ascx" TagName="MultiEmployeeFilter"
    TagPrefix="uc4" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">

        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdProjects.ClientID%>");
            var masterTable = grid.get_masterTableView();
            var value = false;
            for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                var gridItemElement = masterTable.get_dataItems()[i].findElement("chkRow");
                if (gridItemElement.checked) {
                    value = true;
                }
            }
            if (value === false) {
                ShowMessage("<%= Resources.Strings.ErrorDeleteRecourd %>");
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
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab"
                meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel ID="TabProjects" runat="server" HeaderText="Projects" meta:resourcekey="TabProjectsResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblProjectName" runat="server" Text="Project Name" meta:resourcekey="lblProjectNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtProjectName" runat="server" meta:resourcekey="txtProjectNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvProjectName" runat="server" ControlToValidate="txtProjectName"
                                    Display="None" ErrorMessage="Please Enter Project Name" ValidationGroup="grpSave" meta:resourcekey="rfvProjectNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceProjectName" runat="server" TargetControlID="rfvProjectName"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblProjectArabicName" runat="server" Text="Project Arabic Name" meta:resourcekey="lblProjectArabicNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtProjectArabicName" runat="server" meta:resourcekey="txtProjectArabicNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvProjectArabicName" runat="server" ControlToValidate="txtProjectArabicName"
                                    Display="None" ErrorMessage="Please Enter Project Arabic Name" ValidationGroup="grpSave" meta:resourcekey="rfvProjectArabicNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceProjectArabicName" runat="server" TargetControlID="rfvProjectArabicName"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblProjectDesc" runat="server" Text="Project Description" meta:resourcekey="lblProjectDescResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtProjectDesc" runat="server" TextMode="MultiLine" meta:resourcekey="txtProjectDescResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblProjectArabicDesc" runat="server" Text="Project Arabic Description" meta:resourcekey="lblProjectArabicDescResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtProjectArabicDesc" runat="server" TextMode="MultiLine" meta:resourcekey="txtProjectArabicDescResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblPlannedStart" runat="server" Text="Planned Start Date" meta:resourcekey="lblPlannedStartResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpPlannedStart" runat="server" AllowCustomText="false" Culture="en-US"
                                    MarkFirstMatch="true" PopupDirection="TopRight"
                                    ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpPlannedStartResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
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
                                <asp:RequiredFieldValidator ID="rfvPlannedStart" runat="server" ControlToValidate="dtpPlannedStart"
                                    Display="None" ErrorMessage="Please Select Planned Start Date" ValidationGroup="grpSave" meta:resourcekey="rfvPlannedStartResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vcePlannedStart" runat="server" TargetControlID="rfvPlannedStart"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblPlannedEnd" runat="server" Text="Planned End Date" meta:resourcekey="lblPlannedEndResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpPlannedEnd" runat="server" AllowCustomText="false" Culture="en-US"
                                    MarkFirstMatch="true" PopupDirection="TopRight"
                                    ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpPlannedEndResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
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
                                <asp:RequiredFieldValidator ID="rfvPlannedEnd" runat="server" ControlToValidate="dtpPlannedEnd"
                                    Display="None" ErrorMessage="Please Select Planned End Date" ValidationGroup="grpSave" meta:resourcekey="rfvPlannedEndResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vcePlannedEnd" runat="server" TargetControlID="rfvPlannedEnd"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                                <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dtpPlannedStart"
                                    ControlToValidate="dtpPlannedEnd" ErrorMessage="Planned End Date Should be Greater Than or Equal to Planned Start Date"
                                    Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave" meta:resourcekey="CVDateResource1" />
                                <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="vceCVDate"
                                    runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblActualStart" runat="server" Text="Actual Start Date" meta:resourcekey="lblActualStartResource1"></asp:Label>
                            </div>

                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpActualStart" runat="server" AllowCustomText="false" Culture="en-US"
                                    MarkFirstMatch="true" PopupDirection="TopRight"
                                    ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpActualStartResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
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
                                <asp:Label ID="lblActualEnd" runat="server" Text="Actual End Date" meta:resourcekey="lblActualEndResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpActualEnd" runat="server" AllowCustomText="false" Culture="en-US"
                                    MarkFirstMatch="true" PopupDirection="TopRight"
                                    ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpActualEndResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
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

                                <asp:CompareValidator ID="CVActualDate" runat="server" ControlToCompare="dtpActualStart"
                                    ControlToValidate="dtpActualEnd" ErrorMessage="Actual End Date Should be Greater Than or Equal to Actual Start Date"
                                    Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="EmpPermissionGroup" meta:resourcekey="CVActualDateResource1" />
                                <cc1:ValidatorCalloutExtender TargetControlID="CVActualDate" ID="vceCVActualDate"
                                    runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>

                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabResources" runat="server" HeaderText="Project Resources" meta:resourcekey="TabResourcesResource1">
                    <ContentTemplate>

                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblCompanyResource1"
                                    Text="Company"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="RadCmbBxCompanies" runat="server" AutoPostBack="True" CausesValidation="False"
                                    MarkFirstMatch="True" Skin="Vista" Style="width: 350px" ValidationGroup="grpAdd"
                                    meta:resourcekey="RadCmbBxCompaniesResource1">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvCompanies" runat="server" ControlToValidate="RadCmbBxCompanies"
                                    Display="None" InitialValue="--Please Select--" ErrorMessage="Please Select Company"
                                    meta:resourcekey="rfvCompaniesResource1" ValidationGroup="grpAdd"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceCompanies" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="rfvCompanies" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <uc4:MultiEmployeeFilter ID="MultiEmployeeFilterUC" runat="server" OneventEntitySelected="EntityChanged"
                                    OneventWorkGroupSelect="WorkGroupChanged" OneventWorkLocationsSelected="WorkLocationsChanged" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="List Of Employees"
                                    meta:resourcekey="Label5Resource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
                                    <asp:CheckBoxList ID="cblEmpList" runat="server" Style="height: 26px" CssClass="checkboxlist"
                                        DataTextField="EmployeeName" DataValueField="EmployeeId" meta:resourcekey="cblEmpListResource1">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                                    <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                                <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                                    <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                            </div>
                            <div class="col-md-2">
                                <asp:HyperLink ID="hlViewEmployee" runat="server" Visible="False" meta:resourcekey="hlViewEmployeeResource1"
                                    Text="View Org Level Employees "></asp:HyperLink>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-4">
                                <asp:CustomValidator ID="cvEmpListValidation" ErrorMessage="please select at least one employee"
                                    ValidationGroup="ReasonValidation" ForeColor="Black" runat="server" CssClass="customValidator"
                                    meta:resourcekey="cvEmpListValidationResource1" />
                                <%--ClientValidationFunction="CheckBoxListSelect"--%>
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
                            <div class="col-md-2">
                                <asp:Label ID="lblResourceRole" runat="server" Text="Resource Role And Responsibility" meta:resourcekey="lblResourceRoleResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="radcmbxResourceRole" runat="server" CausesValidation="False"
                                    MarkFirstMatch="True" Skin="Vista" Style="width: 350px"
                                    meta:resourcekey="radcmbxResourceRoleResource1">
                                    <%--ValidationGroup="grpSave"--%>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvResourceRole" runat="server" ControlToValidate="radcmbxResourceRole"
                                    Display="None" InitialValue="--Please Select--" ErrorMessage="Please Select Project Role"
                                    meta:resourcekey="rfvResourceRoleResource1" ValidationGroup="grpAdd"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceResourceRole" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="rfvResourceRole" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>

                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button" ValidationGroup="grpAdd" meta:resourcekey="btnAddResource1" />
                                <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="button" CausesValidation="False" meta:resourcekey="btnRemoveResource1" />
                            </div>

                        </div>
                        <div class="table-responsive">
                            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel2" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                            <div class="filterDiv">
                                <telerik:RadFilter runat="server" ID="RadResources" Skin="Hay" FilterContainerID="dgrdResources"
                                    ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadResourcesResource1" Visible="false">
                                    <ContextMenu FeatureGroupID="rfContextMenu">
                                    </ContextMenu>
                                </telerik:RadFilter>
                            </div>
                            <telerik:RadGrid runat="server" ID="dgrdResources" AutoGenerateColumns="False" PageSize="15"
                                AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True"
                                GroupingSettings-CaseSensitive="false" meta:resourcekey="dgrdResourcesResource1">
                                <MasterTableView IsFilterItemExpanded="true" CommandItemDisplay="Top" AllowFilteringByColumn="false" DataKeyNames="EmployeeName,FK_EmployeeId">
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                            <Items>
                                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                                    ImageUrl="~/images/RadFilter.gif" runat="server" meta:resourcekey="RadToolBarButtonResource1" Visible="false" />
                                            </Items>
                                        </telerik:RadToolBar>
                                    </CommandItemTemplate>
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="false" meta:resourcekey="GridTemplateColumnResource1">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="FK_EmployeeId" HeaderText="FK_EmployeeId"
                                            AllowFiltering="false" SortExpression="FK_EmployeeId" Display="false" meta:resourcekey="GridBoundColumnResource6">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No."
                                            AllowFiltering="true" ShowFilterIcon="true" SortExpression="EmployeeNo" Resizable="false" meta:resourcekey="GridBoundColumnResource7">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name"
                                            AllowFiltering="true" ShowFilterIcon="true" SortExpression="EmployeeName" Resizable="false" meta:resourcekey="GridBoundColumnResource8">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Arabic Name"
                                            AllowFiltering="true" ShowFilterIcon="true" SortExpression="EmployeeArabicName" Resizable="false" meta:resourcekey="GridBoundColumnResource9">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DesignationName" HeaderText="Resource Role"
                                            AllowFiltering="true" ShowFilterIcon="true" SortExpression="DesignationName" Resizable="false" meta:resourcekey="GridBoundColumnResource10">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DesignationArabicName" HeaderText="Resource Role Arabic"
                                            AllowFiltering="true" ShowFilterIcon="true" SortExpression="DesignationArabicName" Resizable="false" meta:resourcekey="GridBoundColumnResource11">
                                        </telerik:GridBoundColumn>

                                    </Columns>
                                </MasterTableView>
                                <GroupingSettings CaseSensitive="False"></GroupingSettings>
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                                    EnablePostBackOnRowClick="false" EnableRowHoverStyle="true">
                                    <Selecting AllowRowSelect="false" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="grpSave" meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" OnClientClick="return ValidateDelete();" runat="server" Text="Delete" CssClass="button" CausesValidation="False" meta:resourcekey="btnDeleteResource1" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" CausesValidation="False" meta:resourcekey="btnClearResource1" />
                </div>
            </div>
            <div class="table-responsive">
                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                <div class="filterDiv">
                    <telerik:RadFilter runat="server" ID="RadProjects" Skin="Hay" FilterContainerID="dgrdProjects"
                        ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadProjectsResource1">
                        <ContextMenu FeatureGroupID="rfContextMenu">
                        </ContextMenu>
                    </telerik:RadFilter>
                </div>
                <telerik:RadGrid runat="server" ID="dgrdProjects" AutoGenerateColumns="False" PageSize="15"
                    AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True"
                    GroupingSettings-CaseSensitive="false" meta:resourcekey="dgrdProjectsResource1">
                    <MasterTableView IsFilterItemExpanded="true" CommandItemDisplay="Top" AllowFilteringByColumn="false" DataKeyNames="ProjectName,ProjectId">
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                <Items>
                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                        ImageUrl="~/images/RadFilter.gif" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="ProjectId" HeaderText="ProjectId"
                                AllowFiltering="false" SortExpression="ProjectId" Display="false" meta:resourcekey="GridBoundColumnResource1">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ProjectName" HeaderText="Project Name"
                                AllowFiltering="true" ShowFilterIcon="true" SortExpression="ProjectName" Resizable="false" meta:resourcekey="GridBoundColumnResource2">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ProjectArabicName" HeaderText="Project Arabic Name"
                                SortExpression="ProjectArabicName" meta:resourcekey="GridBoundColumnResource3" />
                            <telerik:GridBoundColumn DataField="PlannedStartDate" HeaderText="Planned Start Date"
                                AllowFiltering="true" ShowFilterIcon="true" SortExpression="PlannedStartDate" Resizable="false"
                                DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource4">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PlannedEndDate" HeaderText="Planned End Date"
                                AllowFiltering="true" ShowFilterIcon="true" SortExpression="PlannedEndDate" Resizable="false"
                                DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource5">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <GroupingSettings CaseSensitive="False"></GroupingSettings>

                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                        EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

