<%@ Page Language="VB" AutoEventWireup="false" Theme="SvTheme" UICulture="auto"
    CodeFile="ScheduleVisit.aspx.vb" Inherits="SelfServices_ScheduleVisit" MasterPageFile="~/Default/NewMaster.master" Culture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter" TagPrefix="uc2" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=grdVisitDetails.ClientID%>");
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
    <uc1:PageHeader ID="PgScheduleVisit" runat="server" HeaderText="Schedule Visit" />

    <div class="row">
        <div class="col-md-2">
            <asp:Label CssClass="Profiletitletxt" ID="lblName" Text="Visitor Name" runat="server" meta:resourcekey="lblNameResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtName" runat="server" meta:resourcekey="txtNameResource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqtxtName" runat="server" ControlToValidate="txtName"
                Display="None" ErrorMessage="Please Enter Visitor Name" ValidationGroup="grpAdd" meta:resourcekey="reqtxtNameResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vcereqtxtName" runat="server" TargetControlID="reqtxtName"
                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>

        <div class="col-md-2">
            <asp:Label CssClass="Profiletitletxt" ID="lblNameAr" Text="Visitor Arabic Name" runat="server" meta:resourcekey="lblNameArResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtNameAr" runat="server" meta:resourcekey="txtNameArResource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqtxtNameAr" runat="server" ControlToValidate="txtNameAr"
                Display="None" ErrorMessage="Please Enter Visitor Arabic Name" ValidationGroup="grpAdd" meta:resourcekey="reqtxtNameArResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vcereqtxtNameAr" runat="server" TargetControlID="reqtxtNameAr"
                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>

    <div class="row">
        <div class="col-md-2">
            <asp:Label CssClass="Profiletitletxt" ID="lblNation" Text="Nationality" runat="server" meta:resourcekey="lblNationResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="radcmbxNationality" runat="server" MarkFirstMatch="True" Filter="Contains" ExpandDirection="Up" meta:resourcekey="radcmbxNationalityResource1"></telerik:RadComboBox>
        </div>

        <div class="col-md-2">
            <asp:Label CssClass="Profiletitletxt" ID="lblMobile" runat="server" Text=" Mobile Number " meta:resourcekey="lblMobileResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtMobile" runat="server" meta:resourcekey="txtMobileResource1"></asp:TextBox>
        </div>
    </div>

    <div class="row">
        <div class="col-md-2">
            <asp:Label CssClass="Profiletitletxt" ID="lblOrganization" runat="server" Text="Organization" meta:resourcekey="lblOrganizationResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtOrganization" runat="server" meta:resourcekey="txtOrganizationResource1"></asp:TextBox>
        </div>

        <div class="col-md-2">
            <asp:Label CssClass="Profiletitletxt" ID="lblDob" runat="server" Text="Date Of Birth" meta:resourcekey="lblDobResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadDatePicker ID="rdDOB" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US"
                meta:resourcekey="rdDOBResource1">
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
                </DateInput>
                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
            </telerik:RadDatePicker>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label CssClass="Profiletitletxt" ID="lblEid" runat="server" Text="Emirates Id Number" meta:resourcekey="lblEidResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtEID" runat="server" meta:resourcekey="txtEIDResource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqtxtEID" runat="server" ControlToValidate="txtEID"
                Display="None" ErrorMessage="Please Enter EID Number" ValidationGroup="grpAdd" meta:resourcekey="reqtxtEIDResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vcereqtxtEID" runat="server" TargetControlID="reqtxtEID"
                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>

        <div class="col-md-2">
            <asp:Label CssClass="Profiletitletxt" ID="lblExpiry" runat="server" Text="Emirates Expiry Date" meta:resourcekey="lblExpiryResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadDatePicker ID="rdEidExpiry" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US"
                meta:resourcekey="rdEidExpiryResource1">
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
                </DateInput>
                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
            </telerik:RadDatePicker>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblGender" runat="server" Text="Gender" meta:resourcekey="lblGenderResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="ddlGender" runat="server" MarkFirstMatch="True" meta:resourcekey="ddlGenderResource1">
                <Items>
                    <telerik:RadComboBoxItem Value="0" Text="--Please Select--" meta:resourcekey="RadComboBoxItemResource1" />
                    <telerik:RadComboBoxItem Value="1" Text="Male" meta:resourcekey="RadComboBoxItemResource2" />
                    <telerik:RadComboBoxItem Value="2" Text="Female" meta:resourcekey="RadComboBoxItemResource3" />
                </Items>
            </telerik:RadComboBox>

        </div>
    </div>

    <div class="row">
        <div class="col-md-12 text-center">
            <asp:Button ID="btnAdd" runat="server" Text="Add" meta:resourcekey="btnAddResource1" ValidationGroup="grpAdd" />
            <asp:Button ID="btnClearAdditionalVistor" runat="server" Text="Clear" meta:resourcekey="btnClearAdditionalVistorResource1" />
            <asp:Button ID="btnRemove" runat="server" Text="Remove" meta:resourcekey="btnRemoveResource1" />
        </div>
    </div>
    <div class="row">
        <div class="table-responsive">
            <telerik:RadFilter runat="server" ID="RadFilter2" FilterContainerID="dgrdAdditionalVisitors"
                Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter2Resource1">
                <ContextMenu FeatureGroupID="rfContextMenu">
                </ContextMenu>
            </telerik:RadFilter>
            <telerik:RadGrid ID="dgrdAdditionalVisitors" runat="server" AllowSorting="True" AllowPaging="True"
                GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" PageSize="25"
                ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdAdditionalVisitorsResource1">
                <GroupingSettings CaseSensitive="False" />
                <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top"
                    DataKeyNames="RowVisitorId,VisitorId,VisitorName,VisitorArabicName,Nationality,MobileNumber,OrganizationName,Gender,IDNumber,DOB,EIDExpiryDate">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" meta:resourcekey="GridTemplateColumnResource2">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource2" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="VisitorName" HeaderText="Visitor Name"
                            UniqueName="VisitorName" meta:resourcekey="GridBoundColumnResource5">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="VisitorArabicName" HeaderText="Visitor Arabic Name"
                            UniqueName="VisitorArabicName" meta:resourcekey="GridBoundColumnResource6">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IDNumber" HeaderText="EID No."
                            UniqueName="IDNumber" meta:resourcekey="GridBoundColumnResource7">
                        </telerik:GridBoundColumn>
                    </Columns>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    <CommandItemTemplate>
                        <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar2_ButtonClick"
                            Skin="Hay" SingleClick="None" meta:resourcekey="RadToolBar1Resource2">
                            <Items>
                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid1" ImageUrl="~/images/RadFilter.gif"
                                    ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource2" />
                            </Items>
                        </telerik:RadToolBar>
                    </CommandItemTemplate>
                </MasterTableView>
                <SelectedItemStyle ForeColor="Maroon" />
            </telerik:RadGrid>
        </div>
    </div>
    <br />
    <hr />

    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblReason" runat="server" Text="Reason Of Visit" meta:resourcekey="lblReasonResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="ddlreason" runat="server" MarkFirstMatch="True" meta:resourcekey="ddlreasonResource1">
                <Items>
                    <telerik:RadComboBoxItem Value="-1" Text="--Please Select--" meta:resourcekey="RadComboBoxItemResource4" />
                    <telerik:RadComboBoxItem Value="Offical Visit" Text="Offical Visit" meta:resourcekey="RadComboBoxItemResource5" />
                    <telerik:RadComboBoxItem Value="Meeting" Text="Meeting" meta:resourcekey="RadComboBoxItemResource6" />
                    <telerik:RadComboBoxItem Value="Interview" Text="Interview" meta:resourcekey="RadComboBoxItemResource7" />
                    <telerik:RadComboBoxItem Value="Other..." Text="Other..." meta:resourcekey="RadComboBoxItemResource8" />
                </Items>
            </telerik:RadComboBox>
            <asp:RequiredFieldValidator ID="reqddlreason" runat="server" ControlToValidate="ddlreason" InitialValue="--Please Select--"
                Display="None" ErrorMessage="Please Enter EID Number" ValidationGroup="grpSave" meta:resourcekey="reqddlreasonResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vcereqddlreason" runat="server" TargetControlID="reqddlreason"
                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
        <div class="col-md-2">
            <asp:Label ID="lblFromDateSearch" runat="server" CssClass="Profiletitletxt" Text="Visit Date" meta:resourcekey="lblFromDateSearchResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadDatePicker ID="dtpVisitDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US" meta:resourcekey="dtpVisitDateResource1">
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
                </DateInput>
                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
            </telerik:RadDatePicker>
        </div>

    </div>
    <br />
    <div class="row">
        <div class="col-md-2">
        </div>
        <div class="col-md-2">
            <asp:Label CssClass="Profiletitletxt" ID="lblFrom" Text="From Time" runat="server" meta:resourcekey="lblFromResource1"></asp:Label>
            <telerik:RadTimePicker ID="RadTPfromTime" runat="server" AllowCustomText="false"
                MarkFirstMatch="true" Skin="Vista" AutoPostBack="false" meta:resourcekey="RadTPfromTimeResource1">
                <DateInput ID="DateInput2" runat="server" ToolTip="View start time" DateFormat="HH:mm" />
            </telerik:RadTimePicker>
            <asp:RequiredFieldValidator ID="reqFromtime" runat="server" ControlToValidate="RadTPfromTime"
                Display="None" ErrorMessage="Please select start time" ValidationGroup="EmpPermissionGroup" meta:resourcekey="reqFromtimeResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ExtenderFromTime" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="reqFromtime" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
        <div class="col-md-2">
            <asp:Label CssClass="Profiletitletxt" ID="lblTo" runat="server" Text="To Time" meta:resourcekey="lblToResource1"></asp:Label>
            <telerik:RadTimePicker ID="RadTPtoTime" runat="server" AllowCustomText="false"
                MarkFirstMatch="true" Skin="Vista" AutoPostBack="false" meta:resourcekey="RadTPtoTimeResource1">
                <DateInput ID="DateInput1" runat="server" ToolTip="View start time" DateFormat="HH:mm" />
            </telerik:RadTimePicker>
            <asp:RequiredFieldValidator ID="reqToTime" runat="server" ControlToValidate="RadTPtoTime"
                Display="None" ErrorMessage="Please Select End Time" ValidationGroup="grpSave"
                meta:resourcekey="reqToTimeResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ExtenderreqToTime" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="reqToTime" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSave" runat="server" Text="Save" meta:resourcekey="btnSaveResource1" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" meta:resourcekey="btnClearResource1" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick="return ValidateDelete();" meta:resourcekey="btnDeleteResource1" />
        </div>
    </div>
    <div class="table-responsive">
        <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="grdVisitDetails"
            Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
            <ContextMenu FeatureGroupID="rfContextMenu">
            </ContextMenu>
        </telerik:RadFilter>
        <telerik:RadGrid ID="grdVisitDetails" runat="server" AllowSorting="True" AllowPaging="True"
            GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" PageSize="25"
            ShowFooter="True" CellSpacing="0" meta:resourcekey="grdVisitDetailsResource1">
            <GroupingSettings CaseSensitive="False" />
            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top"
                DataKeyNames="VisitId,VisitorId,FK_DepartmentId,FK_EmployeeId">
                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                <Columns>
                    <telerik:GridTemplateColumn AllowFiltering="false" meta:resourcekey="GridTemplateColumnResource1">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>

                    <telerik:GridBoundColumn DataField="VisitorName" HeaderText="Visitor Name"
                        UniqueName="VisitorName" meta:resourcekey="GridBoundColumnResource1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="VisitorArabicName" HeaderText="Visitor Arabic Name"
                        UniqueName="VisitorArabicName" meta:resourcekey="GridBoundColumnResource2">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ExpectedCheckInTime" DataFormatString="{0:dd/MM/yyyy hh:mm}"
                        HeaderText="Expected Check In Time"
                        UniqueName="ExpectedCheckInTime" meta:resourcekey="GridBoundColumnResource3">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ReasonOfVisit" UniqueName="ReasonOfVisit" meta:resourcekey="GridBoundColumnResource4"
                        HeaderText="Reason Of Visit">
                    </telerik:GridBoundColumn>

                </Columns>
                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                <CommandItemTemplate>
                    <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                        Skin="Hay" SingleClick="None" meta:resourcekey="RadToolBar1Resource1">
                        <Items>
                            <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                        </Items>
                    </telerik:RadToolBar>
                </CommandItemTemplate>
            </MasterTableView>
            <SelectedItemStyle ForeColor="Maroon" />
        </telerik:RadGrid>
    </div>


</asp:Content>
