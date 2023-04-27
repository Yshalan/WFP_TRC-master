<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="SurveyAssignment.aspx.vb"
    Theme="SvTheme" Inherits="FeedBack_SurveyAssignments" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/MultiEmployeeFilter.ascx" TagName="MultiEmployeeFilter"
    TagPrefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <script type="text/javascript" language="javascript">

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


    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" HeaderText="Survey Assignment" runat="server" />

            <cc1:TabContainer ID="tcAssignSurvey" runat="server" ActiveTabIndex="0" Width="100%"
                CssClass="Tab" meta:resourcekey="tcAssignSurveyResource1">
                <cc1:TabPanel ID="tabEmployee" runat="server" HeaderText="Employee(s)" ToolTip="Employees" meta:resourcekey="tabEmployeeResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt"
                                    Text="Company" meta:resourcekey="lblCompanyResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="RadCmbBxCompanies" runat="server" AutoPostBack="True" CausesValidation="False"
                                    MarkFirstMatch="True" Skin="Vista" Style="width: 350px" ValidationGroup="grpEmpSave" meta:resourcekey="RadCmbBxCompaniesResource1">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvCompanies" runat="server" ControlToValidate="RadCmbBxCompanies"
                                    Display="None" InitialValue="--Please Select--" ErrorMessage="Please Select Company"
                                    ValidationGroup="grpEmpSave" meta:resourcekey="rfvCompaniesResource1"></asp:RequiredFieldValidator>
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
                                <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="List Of Employees" meta:resourcekey="Label5Resource1"></asp:Label>
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
                                <asp:HyperLink ID="hlViewEmployee" runat="server" Visible="False"
                                    Text="View Org Level Employees " meta:resourcekey="hlViewEmployeeResource1"></asp:HyperLink>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-4">
                                <asp:CustomValidator ID="cvEmpListValidation" ErrorMessage="please select at least one employee"
                                    ValidationGroup="grpEmpSave" ForeColor="Black" runat="server" CssClass="customValidator" meta:resourcekey="cvEmpListValidationResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-10">
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("PageNo") %>' meta:resourcekey="LinkButton1Resource1"></asp:LinkButton>|
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblSurvey" runat="server" Text="Survey" meta:resourcekey="lblSurveyResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="radcmbxSurvey" runat="server" CausesValidation="False"
                                    MarkFirstMatch="True" Skin="Vista" Style="width: 350px" ValidationGroup="grpEmpSave" meta:resourcekey="radcmbxSurveyResource1">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvSurvey" runat="server" ControlToValidate="radcmbxSurvey"
                                    Display="None" InitialValue="--Please Select--" ErrorMessage="Please Select Survey"
                                    ValidationGroup="grpEmpSave" meta:resourcekey="rfvSurveyResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vcerfvSurvey" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="rfvSurvey" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="Svpanel">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblFromDate" runat="server" CssClass="Profiletitletxt" Text="From Date" meta:resourcekey="lblFromDateResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDateTimePicker ID="dtpFromdate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                        PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="nl" meta:resourcekey="dtpFromdateResource1">
                                        <TimeView CellSpacing="-1" Culture="nl">
                                            <HeaderTemplate>
                                                Time Picker
                                            </HeaderTemplate>
                                            <TimeTemplate>
                                                <a runat="server" href="#"></a>
                                            </TimeTemplate>
                                        </TimeView>
                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy HH:mm" DisplayDateFormat="dd/MM/yyyy HH:mm"
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
                                    </telerik:RadDateTimePicker>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt" Text="To Date" meta:resourcekey="lblToDateResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDateTimePicker ID="dtpToDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                        PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="nl" meta:resourcekey="dtpToDateResource1">
                                        <TimeView CellSpacing="-1" Culture="nl">
                                            <HeaderTemplate>
                                                Time Picker
                                            </HeaderTemplate>
                                            <TimeTemplate>
                                                <a runat="server" href="#"></a>
                                            </TimeTemplate>
                                        </TimeView>
                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy HH:mm" DisplayDateFormat="dd/MM/yyyy HH:mm"
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
                                    </telerik:RadDateTimePicker>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnEmpSave" runat="server" Text="Save" ValidationGroup="grpEmpSave" meta:resourcekey="btnEmpSaveResource1" />
                                <asp:Button ID="btnEmpClear" runat="server" Text="Clear" meta:resourcekey="btnEmpClearResource1" />
                                <asp:Button ID="btnEmpDelete" runat="server" Text="Delete" meta:resourcekey="btnEmpDeleteResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdEmpSurvey"
                                    ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                                </telerik:RadFilter>
                                <telerik:RadGrid ID="dgrdEmpSurvey" runat="server" AllowPaging="True"
                                    AllowSorting="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                    AutoGenerateColumns="False" PageSize="25" OnItemCommand="dgrdEmpSurvey_ItemCommand"
                                    ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdEmpSurveyResource1">
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                        EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="AssignmentId">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False"
                                                UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                                    <asp:HiddenField ID="hdnEmployeeArabicName" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                                    <asp:HiddenField ID="hdnSurveyArabicName" runat="server" Value='<%# Eval("SurveyArabicName") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" Visible="False" UniqueName="FK_EmployeeId" FilterControlAltText="Filter FK_EmployeeId column" meta:resourcekey="GridBoundColumnResource1" />
                                            <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" SortExpression="EmployeeNo"
                                                UniqueName="EmployeeNo" FilterControlAltText="Filter EmployeeNo column" meta:resourcekey="GridBoundColumnResource2" />
                                            <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"
                                                UniqueName="EmployeeName" FilterControlAltText="Filter EmployeeName column" meta:resourcekey="GridBoundColumnResource3" />
                                            <telerik:GridBoundColumn DataField="SurveyName" HeaderText="Survey Name"
                                                SortExpression="SurveyName" UniqueName="SurveyName" FilterControlAltText="Filter SurveyName column" meta:resourcekey="GridBoundColumnResource4" />
                                            <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date"
                                                SortExpression="FromDate" UniqueName="FromDate" FilterControlAltText="Filter FromDate column"
                                                DataFormatString="{0:dd/MM/yyyy HH:mm}" meta:resourcekey="GridBoundColumnResource5" />
                                            <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date"
                                                SortExpression="ToDate" UniqueName="ToDate" FilterControlAltText="Filter ToDate column"
                                                DataFormatString="{0:dd/MM/yyyy HH:mm}" meta:resourcekey="GridBoundColumnResource6" />
                                        </Columns>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                                Skin="Hay" SingleClick="None" meta:resourcekey="RadToolBar1Resource1">
                                                <Items>
                                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                        ImagePosition="Right" runat="server"
                                                        Owner="" meta:resourcekey="RadToolBarButtonResource1" />
                                                </Items>
                                            </telerik:RadToolBar>
                                        </CommandItemTemplate>
                                    </MasterTableView>
                                    <SelectedItemStyle ForeColor="Maroon" />
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="tabLogicalGroup" runat="server" HeaderText="Logical Group" ToolTip="Logical Group" meta:resourcekey="tabLogicalGroupResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblLogicalGroup" runat="server" Text="Logical Group" meta:resourcekey="lblLogicalGroupResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="ddlLogicalGroup" runat="server" MarkFirstMatch="True" AppendDataBoundItems="True"
                                    DropDownStyle="DropDownList" Skin="Vista" CausesValidation="False" Width="225px" meta:resourcekey="ddlLogicalGroupResource1" />
                                <asp:RequiredFieldValidator ID="rfvddlLogicalGroup" runat="server" Display="None"
                                    InitialValue="--Please Select--" ErrorMessage="Please Select Logical Group" ValidationGroup="grpLGSave"
                                    ControlToValidate="ddlLogicalGroup" meta:resourcekey="rfvddlLogicalGroupResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceddlLogicalGroup" runat="server" TargetControlID="rfvddlLogicalGroup"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblLgSurvey" runat="server" Text="Survey" meta:resourcekey="lblLgSurveyResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="radcmbxLGSurvey" runat="server" CausesValidation="False"
                                    MarkFirstMatch="True" Skin="Vista" Style="width: 350px" ValidationGroup="grpLGSave" meta:resourcekey="radcmbxLGSurveyResource1">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvradcmbxLGSurvey" runat="server" ControlToValidate="radcmbxLGSurvey"
                                    Display="None" InitialValue="--Please Select--" ErrorMessage="Please Select Survey"
                                    ValidationGroup="grpLGSave" meta:resourcekey="rfvradcmbxLGSurveyResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceradcmbxLGSurvey" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="rfvradcmbxLGSurvey" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="Svpanel">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblLGFromDate" runat="server" CssClass="Profiletitletxt" Text="From Date" meta:resourcekey="lblLGFromDateResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDateTimePicker ID="dtpLGFromDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                        PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="nl" meta:resourcekey="dtpLGFromDateResource1">
                                        <TimeView CellSpacing="-1" Culture="nl">
                                            <HeaderTemplate>
                                                Time Picker
                                            </HeaderTemplate>
                                            <TimeTemplate>
                                                <a runat="server" href="#"></a>
                                            </TimeTemplate>
                                        </TimeView>
                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy HH:mm" DisplayDateFormat="dd/MM/yyyy HH:mm"
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
                                    </telerik:RadDateTimePicker>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblLGToDate" runat="server" CssClass="Profiletitletxt" Text="To Date" meta:resourcekey="lblLGToDateResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDateTimePicker ID="dtpLGToDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                        PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="nl" meta:resourcekey="dtpLGToDateResource1">
                                        <TimeView CellSpacing="-1" Culture="nl">
                                            <HeaderTemplate>
                                                Time Picker
                                            </HeaderTemplate>
                                            <TimeTemplate>
                                                <a runat="server" href="#"></a>
                                            </TimeTemplate>
                                        </TimeView>
                                        <TimePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy HH:mm" DisplayDateFormat="dd/MM/yyyy HH:mm"
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
                                    </telerik:RadDateTimePicker>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnLGSave" runat="server" Text="Save" ValidationGroup="grpLGSave" meta:resourcekey="btnLGSaveResource1" />
                                <asp:Button ID="btnLGClear" runat="server" Text="Clear" meta:resourcekey="btnLGClearResource1" />
                                <asp:Button ID="btnLGDelete" runat="server" Text="Delete" meta:resourcekey="btnLGDeleteResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter2" FilterContainerID="dgrdLGSurvey"
                                    ShowApplyButton="False" meta:resourcekey="RadFilter2Resource1">
                                </telerik:RadFilter>
                                <telerik:RadGrid ID="dgrdLGSurvey" runat="server" AllowPaging="True"
                                    AllowSorting="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                    AutoGenerateColumns="False" PageSize="25" OnItemCommand="dgrdLGSurvey_ItemCommand"
                                    ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdLGSurveyResource1">
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                        EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="AssignmentId">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False"
                                                UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource2">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource2" />
                                                    <asp:HiddenField ID="hdnGroupArabicName" runat="server" Value='<%# Eval("GroupArabicName") %>' />
                                                    <asp:HiddenField ID="hdnLGSurveyArabicName" runat="server" Value='<%# Eval("SurveyArabicName") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_LogicalGroupId" Visible="False" UniqueName="FK_LogicalGroupId" FilterControlAltText="Filter FK_LogicalGroupId column" meta:resourcekey="GridBoundColumnResource7" />
                                            <telerik:GridBoundColumn DataField="GroupName" HeaderText="Group Name" SortExpression="GroupName"
                                                UniqueName="GroupName" FilterControlAltText="Filter GroupName column" meta:resourcekey="GridBoundColumnResource8" />
                                            <telerik:GridBoundColumn DataField="SurveyName" HeaderText="Survey Name"
                                                SortExpression="SurveyName" UniqueName="SurveyName" FilterControlAltText="Filter SurveyName column" meta:resourcekey="GridBoundColumnResource10" />
                                            <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date"
                                                SortExpression="FromDate" UniqueName="FromDate" FilterControlAltText="Filter FromDate column"
                                                DataFormatString="{0:dd/MM/yyyy hh:mm}" meta:resourcekey="GridBoundColumnResource11" />
                                            <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date"
                                                SortExpression="ToDate" UniqueName="ToDate" FilterControlAltText="Filter ToDate column"
                                                DataFormatString="{0:dd/MM/yyyy hh:mm}" meta:resourcekey="GridBoundColumnResource12" />
                                        </Columns>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar2_ButtonClick"
                                                Skin="Hay" SingleClick="None" meta:resourcekey="RadToolBar1Resource2">
                                                <Items>
                                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid1" ImageUrl="~/images/RadFilter.gif"
                                                        ImagePosition="Right" runat="server"
                                                        Owner="" meta:resourcekey="RadToolBarButtonResource2" />
                                                </Items>
                                            </telerik:RadToolBar>
                                        </CommandItemTemplate>
                                    </MasterTableView>
                                    <SelectedItemStyle ForeColor="Maroon" />
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

