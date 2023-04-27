<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="DefineScheduleGroups.aspx.vb" Inherits="DailyTasks_DefineScheduleGroups"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/MultiEmployeeFilter.ascx" TagName="MultiEmployeeFilter"
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

        function CheckBoxListSelect(state) {
            var chkBoxList = document.getElementById("<%= cblEmpList.ClientID %>");
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }


        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdGroupDetails.ClientID %>");
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <uc2:PageHeader ID="pageheader1" HeaderText="Group Schedule" runat="server" />
    <asp:UpdatePanel ID="Upanel1" runat="server">
        <ContentTemplate>
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab"
                meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel ID="TabGroupSchedule" runat="server" HeaderText="Group Schedule" meta:resourcekey="TabGroupScheduleResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblGroupCode" runat="server" CssClass="Profiletitletxt" Text="Group Code"
                                    meta:resourcekey="lblGroupCodeResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtGroupCode" runat="server" meta:resourcekey="txtGroupCodeResource1"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="RFVGroupCode" runat="server" ControlToValidate="txtGroupCode"
                                    Display="None" ErrorMessage="Please Enter Group Code" ValidationGroup="VGGroup"
                                    meta:resourcekey="RFVGroupCodeResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="VCEGroupCode" runat="server" Enabled="True" TargetControlID="RFVGroupCode">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblNameEn" runat="server" CssClass="Profiletitletxt" Text="Group English Name"
                                    meta:resourcekey="lblNameEnResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtGroupNameEn" runat="server" meta:resourcekey="txtGroupNameEnResource1"></asp:TextBox>


                                <asp:RequiredFieldValidator ID="RFVGroupNameEn" runat="server" ControlToValidate="txtGroupNameEn"
                                    Display="None" ErrorMessage="Please Enter Group English Name " ValidationGroup="VGGroup"
                                    meta:resourcekey="RFVGroupNameEnResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="VCEGroupNameEn" runat="server" Enabled="True" TargetControlID="RFVGroupNameEn">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblNameAr" runat="server" CssClass="Profiletitletxt" Text="Group Arabic Name"
                                    meta:resourcekey="lblNameArResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtGroupNameAr" runat="server" meta:resourcekey="txtGroupNameArResource1"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="RFVGroupNameAr" runat="server" ControlToValidate="txtGroupNameAr"
                                    Display="None" ErrorMessage="Please Enter Group Arabic  Name " ValidationGroup="VGGroup"
                                    meta:resourcekey="RFVGroupNameArResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="VCEGroupNameAr" runat="server" Enabled="True" TargetControlID="RFVGroupNameAr">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblWorkDayNo" runat="server" CssClass="Profiletitletxt" Text="No. Of Work Day"
                                    meta:resourcekey="lblWorkDayNoResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="radtxtWorkDayNo" MaxValue="30" Skin="Vista" MinValue="1"
                                    runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="radtxtWorkDayNoResource1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblDays" runat="server" Text="Day(s)" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblDaysResource1" />

                                <asp:RequiredFieldValidator ID="RFVradtxtWorkDayNo" runat="server" ControlToValidate="radtxtWorkDayNo"
                                    Display="None" ErrorMessage="Please Enter No. Of Work Day" ValidationGroup="VGGroup"
                                    meta:resourcekey="RFVradtxtWorkDayNoResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="VCEradtxtWorkDayNo" runat="server" Enabled="True" TargetControlID="RFVradtxtWorkDayNo">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblRestDayNo" runat="server" CssClass="Profiletitletxt" Text="No. Of Rest Day"
                                    meta:resourcekey="lblRestDayNoResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="radtxtRestDayNo" MaxValue="30" Skin="Vista" MinValue="1"
                                    runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="RadtxtRestDayNoResource1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-1">
                                <asp:Label ID="lblDays2" runat="server" Text="Day(s)" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblDaysResource1" />

                                <asp:RequiredFieldValidator ID="RFVradtxtRestDayNo" runat="server" ControlToValidate="radtxtRestDayNo"
                                    Display="None" ErrorMessage="Please Enter No. Of Rest Day" ValidationGroup="VGGroup"
                                    meta:resourcekey="RFVradtxtRestDayNoNoResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="VCEradtxtRestDayNo" runat="server" Enabled="True" TargetControlID="RFVradtxtRestDayNo">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" Text="Company"
                                    meta:resourcekey="lblCompanyResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="RadCmbBxCompany" AutoPostBack="True" MarkFirstMatch="True"
                                    CausesValidation="False" Skin="Vista" runat="server" meta:resourcekey="RadCmbBxCompanyResource1">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ValidationGroup="VGGroup"
                                    InitialValue="--Please Select--" ControlToValidate="RadCmbBxCompany" Display="None"
                                    ErrorMessage="Please Select Company" meta:resourcekey="rfvCompanyResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceCompany" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvCompany" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblEntity" runat="server" Text="Entity" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblEntityResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="RadCmbBxEntity" MarkFirstMatch="True" Width="450px" Skin="Vista"
                                    runat="server" CausesValidation="False" meta:resourcekey="RadCmbBxEntityResource1">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvEntity" runat="server" ControlToValidate="RadCmbBxEntity"
                                    InitialValue="--Please Select--" Display="None" ErrorMessage="Please Select Entity"
                                    ValidationGroup="VGGroup" meta:resourcekey="rfvEntityResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceEntity" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="rfvEntity" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkActive" runat="server" Text="Active"
                                    meta:resourcekey="lblActiveResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="VGGroup"
                                    meta:resourcekey="btnSaveResource1" />
                                <asp:Button ID="btnDelete" OnClientClick="return ValidateDelete();" runat="server" Text="Delete" CssClass="button" meta:resourcekey="btnDeleteResource1" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClearResource1" />
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabGroupManager" runat="server" HeaderText="Group Managers" Visible="False"
                    meta:resourcekey="TabGroupManagerResource1">
                    <ContentTemplate>
                        <div class="row" id="dvActiveManager" runat="server" visible="false">
                            <div class="col-md-2">
                                <asp:Label ID="lblActiveManager" runat="server" Text="Active Group Manager: "></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:Label ID="lblActiveManagerVal" runat="server"></asp:Label>
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
                                <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="List Of Managers"
                                    meta:resourcekey="Label5Resource1"></asp:Label>
                            </div>
                            <div class="col-md-6">

                                <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc">
                                    <asp:CheckBoxList ID="cblEmpList" runat="server" Style="height: 26px" DataTextField="EmployeeName"
                                        DataValueField="EmployeeId" meta:resourcekey="cblEmpListResource1">
                                    </asp:CheckBoxList>
                                </div>
                                <div class="col-md-2">
                                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                                        <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                                </div>
                                <div class="col-md-3">
                                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                                        <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:HyperLink ID="hlViewEmployee" runat="server" Visible="False" meta:resourcekey="hlViewEmployeeResource1"
                                    Text="View Org Level Employees "></asp:HyperLink>
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
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBox ID="chckTemporary" runat="server" CausesValidation="false" AutoPostBack="True"
                                    Text="Is Temporary" meta:resourcekey="Label6Resource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
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
                                                Visible="false" ErrorMessage="End Date should be greater than or equal to From Date"
                                                Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave_Mgr"
                                                meta:resourcekey="CVDateResource1"></asp:CompareValidator>
                                            <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender2"
                                                runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                            </cc1:ValidatorCalloutExtender>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="ibtnSave" runat="server" Text="Save" CssClass="button" meta:resourcekey="btnSaveResource1" ValidationGroup="grpSave_Mgr" />
                                <%--<asp:Button ID="ibtnDelete" runat="server" Text="Delete" CssClass="button" meta:resourcekey="btnDeleteResource1" />--%>
                                <asp:Button ID="ibtnClear" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClearResource1" />
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdGroupDetails"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="dgrdGroupDetails" runat="server" AllowPaging="True"
                        AllowSorting="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        AutoGenerateColumns="False" PageSize="15" OnItemCommand="dgrdGroupDetails_ItemCommand"
                        ShowFooter="True" meta:resourcekey="dgrdGroupDetailsResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="GroupId,GroupCode">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                        <asp:HiddenField ID="hdnEntityArabicName" runat="server" Value='<%# Eval("EntityArabicName") %>' />
                                        <asp:HiddenField ID="hdnGroupNameAr" runat="server" Value='<%# Eval("GroupNameAr") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="GroupId" HeaderText="GroupId"
                                    SortExpression="GroupId" Visible="False" UniqueName="GroupId" meta:resourcekey="GridBoundColumnResource1" />
                                <telerik:GridBoundColumn DataField="GroupCode" HeaderText="Group Code" SortExpression="GroupCode"
                                    Resizable="False" UniqueName="GroupCode" meta:resourcekey="GridBoundColumnResource2" />
                                <telerik:GridBoundColumn DataField="GroupNameEn" HeaderText="Group Name" SortExpression="GroupNameEn"
                                    UniqueName="GroupNameEn" meta:resourcekey="GridBoundColumnResource3" />

                                <telerik:GridBoundColumn DataField="WorkDayNo" HeaderText="No. Of Work Day" SortExpression="WorkDayNo"
                                    UniqueName="WorkDayNo" meta:resourcekey="GridBoundColumnResource8" />
                                <telerik:GridBoundColumn DataField="RestDayNo" HeaderText="No. Of Rest Day" SortExpression="RestDayNo"
                                    UniqueName="RestDayNo" meta:resourcekey="GridBoundColumnResource9" />

                                <telerik:GridBoundColumn DataField="IsActive" HeaderText="Active" SortExpression="IsActive"
                                    Resizable="False" UniqueName="IsActive" meta:resourcekey="GridBoundColumnResource4" />
                                <telerik:GridBoundColumn DataField="EntityName" HeaderText="Entity Name" SortExpression="EntityName"
                                    Resizable="False" UniqueName="EntityName" meta:resourcekey="GridBoundColumnResource5" />

                                <telerik:GridBoundColumn DataField="CREATED_BY" HeaderText="Created By" SortExpression="CREATED_BY"
                                    Resizable="False" UniqueName="CREATED_BY" meta:resourcekey="GridBoundColumnResource6" />
                                <telerik:GridBoundColumn DataField="CREATED_DATE" HeaderText="Created Date" SortExpression="CREATED_DATE"
                                    Resizable="False" UniqueName="CREATED_DATE" DataFormatString="{0:dd/M/yyyy}"
                                    meta:resourcekey="GridBoundColumnResource7" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
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
