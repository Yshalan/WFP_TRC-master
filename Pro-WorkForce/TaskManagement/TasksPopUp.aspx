<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TasksPopUp.aspx.vb" Inherits="Definitions_TasksPopUp"
    Theme="SvTheme" MasterPageFile="~/Default/EmptyMaster.master" meta:resourcekey="PageResource1" UICulture="auto" Culture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="EmployeeFilter"
    TagPrefix="uc4" %>

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

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function CloseAndRefresh() {
            var oWnd = GetRadWindow();
            oWnd.close();
            GetRadWindow().BrowserWindow.location.reload();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab" meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel ID="TabGeneral" runat="server" HeaderText="General" meta:resourcekey="TabGeneralResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblTaskName" runat="server" Text="Task Name" meta:resourcekey="lblTaskNameResource1"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtTaskName" runat="server" meta:resourcekey="txtTaskNameResource1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTaskName" runat="server" ControlToValidate="txtTaskName"
                                    Display="None" ErrorMessage="Please Enter Task Name" ValidationGroup="grpSave" meta:resourcekey="rfvTaskNameResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceTaskName" runat="server" TargetControlID="rfvTaskName"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblTaskDesc" runat="server" Text="Task Description" meta:resourcekey="lblTaskDescResource1"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtTaskDesc" runat="server" TextMode="MultiLine" meta:resourcekey="txtTaskDescResource1"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblPlannedStart" runat="server" Text="Planned Start Date" meta:resourcekey="lblPlannedStartResource1"></asp:Label>
                            </div>
                            <div class="col-md-3 radwindowclndr">
                                <telerik:RadDatePicker ID="dtpPlannedStart" runat="server" AllowCustomText="false" Culture="en-US"
                                    MarkFirstMatch="true" PopupDirection="TopRight"
                                    ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpPlannedStartResource1">
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
                                <asp:RequiredFieldValidator ID="rfvPlannedStart" runat="server" ControlToValidate="dtpPlannedStart"
                                    Display="None" ErrorMessage="Please Select Planned Start Date" ValidationGroup="grpSave" meta:resourcekey="rfvPlannedStartResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vcePlannedStart" runat="server" TargetControlID="rfvPlannedStart"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblPlannedEnd" runat="server" Text="Planned End Date" meta:resourcekey="lblPlannedEndResource1"></asp:Label>
                            </div>
                            <div class="col-md-3 radwindowclndr">
                                <telerik:RadDatePicker ID="dtpPlannedEnd" runat="server" AllowCustomText="false" Culture="en-US"
                                    MarkFirstMatch="true" PopupDirection="TopRight"
                                    ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpPlannedEndResource1">
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
                        <br />
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblActualStart" runat="server" Text="Actual Start Date" meta:resourcekey="lblActualStartResource1"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblActualStartVal" runat="server" meta:resourcekey="lblActualStartValResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblActualEnd" runat="server" Text="Actual End Date" meta:resourcekey="lblActualEndResource1"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblActualEndVal" runat="server" meta:resourcekey="lblActualEndValResource1"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblParentTask" runat="server" Text="Parent Task" meta:resourcekey="lblParentTaskResource1"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <telerik:RadComboBox ID="radcmbxParent" runat="server" AutoPostBack="True" CausesValidation="False"
                                    MarkFirstMatch="True" Skin="Vista" Style="width: 350px" ExpandDirection="Up" meta:resourcekey="radcmbxParentResource1">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblSeq_No" runat="server" Text="Task No." meta:resourcekey="lblSeq_NoResource1"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <telerik:RadNumericTextBox ID="txtRetriveSequence" runat="server" MinValue="1" MaxValue="999" Culture="en-US" DbValueFactor="1" LabelCssClass="" LabelWidth="64px" meta:resourcekey="txtRetriveSequenceResource1">
                                    <NegativeStyle Resize="None" />
                                    <NumberFormat DecimalDigits="0" ZeroPattern="n" />
                                    <EmptyMessageStyle Resize="None" />
                                    <ReadOnlyStyle Resize="None" />
                                    <FocusedStyle Resize="None" />
                                    <DisabledStyle Resize="None" />
                                    <InvalidStyle Resize="None" />
                                    <HoveredStyle Resize="None" />
                                    <EnabledStyle Resize="None" />
                                </telerik:RadNumericTextBox>
                                <asp:Button ID="btnRetrive" runat="server" Text="Retrive" meta:resourcekey="btnRetriveResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblProiority" runat="server" Text="Proiority" meta:resourcekey="lblProiorityResource1"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <telerik:RadComboBox ID="RadCmbBxProiority" runat="server" AutoPostBack="True" CausesValidation="False"
                                    MarkFirstMatch="True" Skin="Vista" Style="width: 350px" ValidationGroup="grpAdd" ExpandDirection="Up" meta:resourcekey="RadCmbBxProiorityResource1">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblCompletePercentage" runat="server" Text="Completion Percentage" meta:resourcekey="lblCompletePercentageResource1"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <telerik:RadNumericTextBox ID="radnumCompletePercentage" runat="server" MaxValue="100" MinValue="0" Culture="en-US" DbValueFactor="1" LabelCssClass="" LabelWidth="64px" meta:resourcekey="radnumCompletePercentageResource1">
                                    <NegativeStyle Resize="None" />
                                    <NumberFormat DecimalDigits="0" ZeroPattern="n" />
                                    <EmptyMessageStyle Resize="None" />
                                    <ReadOnlyStyle Resize="None" />
                                    <FocusedStyle Resize="None" />
                                    <DisabledStyle Resize="None" />
                                    <InvalidStyle Resize="None" />
                                    <HoveredStyle Resize="None" />
                                    <EnabledStyle Resize="None" />
                                </telerik:RadNumericTextBox>
                                <i class="fa fa-percent"></i>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabResources" runat="server" HeaderText="Resources" meta:resourcekey="TabResourcesResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <uc4:EmployeeFilter ID="EmployeeFilter" runat="server"
                                    ShowRadioSearch="true" ValidationGroup="grpAddResource" IsEmployeeRequired="true" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblInvolvement" runat="server" Text="Project Involvement" meta:resourcekey="lblInvolvementResource1"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <telerik:RadNumericTextBox ID="txtInvolvement" MinValue="0" MaxValue="100"
                                    Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" DbValueFactor="1" LabelWidth="64px" meta:resourcekey="txtInvolvementResource1">
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
                                <i class="fa fa-percent"></i>

                                <asp:RequiredFieldValidator ID="rfvInvolvement" runat="server" ControlToValidate="txtInvolvement"
                                    Display="None" ErrorMessage="Please Insert Involvement Percentage" ValidationGroup="grpAddResource" meta:resourcekey="rfvInvolvementResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceInvolvement" runat="server" TargetControlID="rfvInvolvement"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnAddResource" runat="server" Text="Add" ValidationGroup="grpAddResource" meta:resourcekey="btnAddResourceResource1" />
                                <asp:Button ID="btnRemoveResource" runat="server" Text="Remove" meta:resourcekey="btnRemoveResourceResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel2" meta:resourcekey="RadAjaxLoadingPanel2Resource1" />
                                <div class="filterDiv">
                                    <telerik:RadFilter runat="server" ID="RadResources" Skin="Hay" FilterContainerID="dgrdResources"
                                        ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " Visible="False" meta:resourcekey="RadResourcesResource1">
                                    </telerik:RadFilter>
                                </div>
                                <telerik:RadGrid runat="server" ID="dgrdResources" AutoGenerateColumns="False" PageSize="15"
                                    AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" meta:resourcekey="dgrdResourcesResource1">
                                    <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False" DataKeyNames="EmployeeName,FK_EmployeeId">
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick" SingleClick="None" meta:resourcekey="RadToolBar1Resource1">
                                                <Items>
                                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                                        ImageUrl="~/images/RadFilter.gif" runat="server" Visible="False" meta:resourcekey="RadToolBarButtonResource1" />
                                                </Items>
                                            </telerik:RadToolBar>
                                        </CommandItemTemplate>
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="FK_EmployeeId" HeaderText="FK_EmployeeId"
                                                AllowFiltering="False" SortExpression="FK_EmployeeId" Display="False" FilterControlAltText="Filter FK_EmployeeId column" meta:resourcekey="GridBoundColumnResource1" UniqueName="FK_EmployeeId">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No." SortExpression="EmployeeNo" Resizable="False" FilterControlAltText="Filter EmployeeNo column" meta:resourcekey="GridBoundColumnResource2" UniqueName="EmployeeNo">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName" Resizable="False" FilterControlAltText="Filter EmployeeName column" meta:resourcekey="GridBoundColumnResource3" UniqueName="EmployeeName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Arabic Name" SortExpression="EmployeeArabicName" Resizable="False" FilterControlAltText="Filter EmployeeArabicName column" meta:resourcekey="GridBoundColumnResource4" UniqueName="EmployeeArabicName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Involvmentpercentage" HeaderText="Project Involvement" SortExpression="Involvmentpercentage" Resizable="False" FilterControlAltText="Filter Involvmentpercentage column" meta:resourcekey="GridBoundColumnResource5" UniqueName="Involvmentpercentage">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <GroupingSettings CaseSensitive="False"></GroupingSettings>
                                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPredecessors" runat="server" HeaderText="Predecessors\Dependencies" meta:resourcekey="TabPredecessorsResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblRelatedTask" runat="server" Text="Related Task" meta:resourcekey="lblRelatedTaskResource1"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <telerik:RadComboBox ID="radcmbxRelatedTask" runat="server" CausesValidation="False"
                                    MarkFirstMatch="True" Skin="Vista" Style="width: 350px" ValidationGroup="grpAddPredecessors" ExpandDirection="Up" meta:resourcekey="radcmbxRelatedTaskResource1">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvelatedTask" runat="server" ControlToValidate="radcmbxRelatedTask"
                                    Display="None" ErrorMessage="Please Select Related Task" ValidationGroup="grpAddPredecessors"
                                    InitialValue="--Please Select--" meta:resourcekey="rfvelatedTaskResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceRelatedTask" runat="server" TargetControlID="rfvelatedTask"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblPredecessors" runat="server" Text="Relation Type" meta:resourcekey="lblPredecessorsResource1"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <telerik:RadComboBox ID="radcmbxPredecessors" runat="server" CausesValidation="False"
                                    MarkFirstMatch="True" Skin="Vista" Style="width: 350px" ValidationGroup="grpAddPredecessors" ExpandDirection="Up" meta:resourcekey="radcmbxPredecessorsResource1">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvPredecessors" runat="server" ControlToValidate="radcmbxPredecessors"
                                    Display="None" ErrorMessage="Please Select Predecessors" ValidationGroup="grpAddPredecessors"
                                    InitialValue="--Please Select--" meta:resourcekey="rfvPredecessorsResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vcePredecessors" runat="server" TargetControlID="rfvPredecessors"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Button ID="btnAddPredecessors" runat="server" Text="Add" ValidationGroup="grpAddPredecessors" meta:resourcekey="btnAddPredecessorsResource1" />
                                <asp:Button ID="btnRemovePredecessors" runat="server" Text="Remove" meta:resourcekey="btnRemovePredecessorsResource1" />
                            </div>
                        </div>
                        <div class="table-responsive">
                            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                            <div class="filterDiv">
                                <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdPredeccessors"
                                    ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " Visible="False" meta:resourcekey="RadFilter1Resource1">
                                </telerik:RadFilter>
                            </div>
                            <telerik:RadGrid runat="server" ID="dgrdPredeccessors" AutoGenerateColumns="False" PageSize="15"
                                AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" meta:resourcekey="dgrdPredeccessorsResource1">
                                <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False" DataKeyNames="TaskId">
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick" SingleClick="None" meta:resourcekey="RadToolBar1Resource2">
                                            <Items>
                                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid1" ImagePosition="Right"
                                                    ImageUrl="~/images/RadFilter.gif" runat="server" Visible="False" meta:resourcekey="RadToolBarButtonResource2" />
                                            </Items>
                                        </telerik:RadToolBar>
                                    </CommandItemTemplate>
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="False" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource2" UniqueName="TemplateColumn">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource2" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="TaskId" HeaderText="TaskId"
                                            AllowFiltering="False" SortExpression="TaskId" Display="False" FilterControlAltText="Filter TaskId column" meta:resourcekey="GridBoundColumnResource6" UniqueName="TaskId">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TaskName" HeaderText="Task Name" SortExpression="TaskName" Resizable="False" FilterControlAltText="Filter TaskName column" meta:resourcekey="GridBoundColumnResource7" UniqueName="TaskName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="RelationType" HeaderText="Relation Type" SortExpression="RelationType" Resizable="False" FilterControlAltText="Filter RelationType column" meta:resourcekey="GridBoundColumnResource8" UniqueName="RelationType">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TaskSequence" HeaderText="Task No." SortExpression="TaskSequence" Resizable="False" FilterControlAltText="Filter TaskSequence column" meta:resourcekey="GridBoundColumnResource9" UniqueName="TaskSequence">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <GroupingSettings CaseSensitive="False"></GroupingSettings>
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>

            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="grpSave" meta:resourcekey="btnSaveResource1" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>





