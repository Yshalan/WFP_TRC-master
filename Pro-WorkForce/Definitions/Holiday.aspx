<%@ Page Title="Define Holidays" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="Holiday.aspx.vb" Inherits="Admin_Holiday" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function showYear() {
            if (document.getElementById('<%=chkYearlyFixed.ClientID%>').checked == true) {
                document.getElementById('<%=dvDay.ClientID%>').style.display = 'block';
                document.getElementById('<%=dvDate.ClientID%>').style.display = 'none';

                ValidatorEnable(document.getElementById('<%=ReqFVFromDate.ClientID%>'), false);
                ValidatorEnable(document.getElementById('<%=ReqFVToDate.ClientID%>'), false);
                ValidatorEnable(document.getElementById('<%=ReqFVFromDay.ClientID%>'), true);
                ValidatorEnable(document.getElementById('<%=ReqFVToDay.ClientID%>'), true);

            }
            else {
                document.getElementById('<%=dvDay.ClientID%>').style.display = 'none';
                document.getElementById('<%=dvDate.ClientID%>').style.display = 'block';

                ValidatorEnable(document.getElementById('<%=ReqFVFromDate.ClientID%>'), true);
                ValidatorEnable(document.getElementById('<%=ReqFVToDate.ClientID%>'), true);
                ValidatorEnable(document.getElementById('<%=ReqFVFromDay.ClientID%>'), false);
                ValidatorEnable(document.getElementById('<%=ReqFVToDay.ClientID%>'), false);

            }
            hideValidatorCallout();
        }

        function showCompany(control) {
            var SpecificCompany = document.getElementById('<%=rbtnSpecificCompany.ClientID%>');
            var AllCompanies = document.getElementById('<%=rbtnAllCompanies.ClientID%>');
            var SpecificWorkLocation = document.getElementById('<%=rbtnSpecificWorkLocation.ClientID%>');
            if (SpecificCompany != null) {
                if (SpecificCompany.id == control.id) {
                    document.getElementById('<%=dvCompany.ClientID%>').style.display = 'block';
                    document.getElementById('<%=dvWorkLocation.ClientID%>').style.display = 'none';
                    AllCompanies.checked = false;
                    SpecificWorkLocation.checked = false;
                }
                else if (AllCompanies.id == control.id) {
                    document.getElementById('<%=dvCompany.ClientID%>').style.display = 'none';
                    document.getElementById('<%=dvWorkLocation.ClientID%>').style.display = 'none';
                    if (SpecificCompany != null) {
                        SpecificCompany.checked = false;
                    }
                    SpecificWorkLocation.checked = false;
                }
                else {
                    document.getElementById('<%=dvCompany.ClientID%>').style.display = 'none';
                    document.getElementById('<%=dvWorkLocation.ClientID%>').style.display = 'block';
                    if (SpecificCompany != null) {
                        SpecificCompany.checked = false;
                    }
                    AllCompanies.checked = false;
                }
            }
        }

        function showReligion(control) {

            var AllReligions = document.getElementById('<%=rbtnAllReligions.ClientID%>');
            var SpecificReligions = document.getElementById('<%=rbtnSpecificReligions.ClientID%>');

            if (SpecificReligions.id == control.id) {
                document.getElementById('<%=dvReligion.ClientID%>').style.display = 'block';
                AllReligions.checked = false;
            }
            else {
                document.getElementById('<%=dvReligion.ClientID%>').style.display = 'none';
                SpecificReligions.checked = false;
            }
        }

        function showLogicalGroup(control) {
            var AllLogicalGroups = document.getElementById('<%=rbtnAllLogicalGroup.ClientID%>');
            var SpecificLogicalGroups = document.getElementById('<%=rbtnSpecificLogicalGroup.ClientID%>');

            if (SpecificLogicalGroups.id == control.id) {
                document.getElementById('<%=dvLogicalGroup.ClientID%>').style.display = 'block';
                AllLogicalGroups.checked = false;
            }
            else {
                document.getElementById('<%=dvLogicalGroup.ClientID%>').style.display = 'none';
                SpecificLogicalGroups.checked = false;
            }
        }

        function showEmployeeType(control) {

            var AllEmployeeTypes = document.getElementById('<%=rbtnAllEmployeeTypes.ClientID%>');
            var SpecificEmployeeTypes = document.getElementById('<%=rbtnSpecificEmployeeTypes.ClientID%>');

            if (SpecificEmployeeTypes.id == control.id) {
                document.getElementById('<%=dvEmployeeType.ClientID%>').style.display = 'block';
                AllEmployeeTypes.checked = false;
            }
            else {
                document.getElementById('<%=dvEmployeeType.ClientID%>').style.display = 'none';
                SpecificEmployeeTypes.checked = false;
            }
        }

        function hideValidatorCallout() {
            try {
                if (AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout != null) {
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.hide();
                }
            }
            catch (err) {
            }
            return false;
        }

        function ValidateCheckBoxList() {
            var CompanyCheckBoxList = document.getElementById("<%= chkLstCompany.ClientID %>");

            if (document.getElementById('<%=dvCompany.ClientID%>').style.display = 'block') {

                for (var i = 0; i <= CompanyCheckBoxList.firstChild.children.length - 1; i++) {
                    if (CompanyCheckBoxList.firstChild.children(i).cells(0).children(0).checked == 'true')

                        args.IsValid = true
                    return true;

                }
                ShowMessage('None is selected')
                args.IsValid = false
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <uc1:PageHeader ID="PageHeader1" runat="server" />

    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="Holiday Name English"
                meta:resourcekey="Label1Resource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtHolidayEnglish" runat="server" meta:resourcekey="txtHolidayEnglishResource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqHolidayEngName" runat="server" ControlToValidate="txtHolidayEnglish"
                Display="None" ErrorMessage="Please Enter English Name " ValidationGroup="GrPolicy"
                meta:resourcekey="reqHolidayEngNameResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ExtenderEngName" runat="server" Enabled="True"
                TargetControlID="reqHolidayEngName" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="Label2" runat="server" CssClass="Profiletitletxt" Text="Holiday Name Arabic"
                meta:resourcekey="Label2Resource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtHolidayArabic" runat="server" meta:resourcekey="txtHolidayArabicResource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqHolidayArName" runat="server" ControlToValidate="txtHolidayArabic"
                Display="None" ErrorMessage="Please Enter Arablic Name" ValidationGroup="GrPolicy"
                meta:resourcekey="reqHolidayArNameResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ExtenderArName" runat="server" Enabled="True" TargetControlID="reqHolidayArName"
                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <asp:CheckBox ID="chkYearlyFixed" runat="server" CssClass="Profiletitletxt" OnClick="showYear()"
                Text="Repeated Yearly" meta:resourcekey="chkYearlyFixedResource1" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div id="dvDate" runat="server" style="display: block" class="col-md-12">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblDateFrom" runat="server" CssClass="Profiletitletxt" Text="From Date"
                        meta:resourcekey="lblDateFromResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dteFromDate" runat="server" DatePopupButton-Visible="true"
                        EnableTyping="False" Culture="English (United States)">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                            TitleFormat="MMMM yyyy">
                        </Calendar>
                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        <DateInput DateFormat="dd/MM/yyyy" DDisplayDateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="ReqFVFromDate" runat="server" ControlToValidate="dteFromDate"
                        Display="None" ErrorMessage="Select From Date " ValidationGroup="GrPolicy" meta:resourcekey="ReqFVFromDateResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="VCEFromDate" runat="server" Enabled="True" TargetControlID="ReqFVFromDate"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">

                <div class="col-md-2">
                    <asp:Label ID="lblDateTo" runat="server" CssClass="Profiletitletxt" Text="To Date"
                        meta:resourcekey="lblDateToResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dteToDate" runat="server" DatePopupButton-Visible="true"
                        EnableTyping="False" Culture="English (United States)">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"
                            TitleFormat="MMMM yyyy">
                        </Calendar>
                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        <DateInput DateFormat="dd/MM/yyyy" DDisplayDateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="ReqFVToDate" runat="server" ControlToValidate="dteToDate"
                        Display="None" ErrorMessage="Select To Date " ValidationGroup="GrPolicy" meta:resourcekey="ReqFVToDateResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="VCEToDate" runat="server" Enabled="True" TargetControlID="ReqFVToDate"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
                <div class="col-md-2">
                    <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dteFromDate" ControlToValidate="dteToDate"
                        ErrorMessage="To Date should be greater than or equal to From Date" Display="None"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="GrPolicy" meta:resourcekey="CVDateResource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender1"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="CVDay" runat="server" ControlToCompare="dteFromDay" ControlToValidate="dteToDay"
                        ErrorMessage="To Day/Month should be greater than or equal to From Day/Month"
                        Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="GrPolicy"
                        meta:resourcekey="CVDayResource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="CVDay" ID="ValidatorCalloutExtender2"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
        </div>
    </div>
    <div class="Svpanel">
        <div class="row" id="dvDay" runat="server" style="display: none">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblDateFrom0" runat="server" Text="From Day/Month" class="Profiletitletxt"
                        meta:resourcekey="lblDateFrom0Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dteFromDay" runat="server" Culture="English (United States)"
                        DatePopupButton-Visible="true" EnableTyping="False">
                        <Calendar TitleFormat="MMMM" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                            ViewSelectorText="x">
                        </Calendar>
                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        <DateInput DateFormat="dd-MMMM" DisplayDateFormat="dd-MMMM">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="ReqFVFromDay" runat="server" ControlToValidate="dteFromDay"
                        Enabled="False" Display="None" ErrorMessage="Select From Month and Day " ValidationGroup="GrPolicy"
                        meta:resourcekey="ReqFVFromDayResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="VCEFromDay" runat="server" Enabled="True" TargetControlID="ReqFVFromDay"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblDateTo0" runat="server" Text="To  Day/Month" class="Profiletitletxt"
                        meta:resourcekey="lblDateTo0Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dteToDay" runat="server" Culture="English (United States)"
                        DatePopupButton-Visible="true" EnableTyping="False">
                        <Calendar TitleFormat="MMMM" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                            ViewSelectorText="x">
                        </Calendar>
                        <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        <DateInput DateFormat="dd-MMMM" DisplayDateFormat="dd-MMMM">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="ReqFVToDay" runat="server" ControlToValidate="dteToDay"
                        Enabled="False" Display="None" ErrorMessage="Select To Month and Day " ValidationGroup="GrPolicy"
                        meta:resourcekey="ReqFVToDayResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="VCEToDay" runat="server" Enabled="True" TargetControlID="ReqFVToDay"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-10">
            <asp:RadioButton ID="rbtnAllCompanies" runat="server" CssClass="Profiletitletxt"
                AutoPostBack="true" Text="All Companies" OnClick="showCompany(this)" meta:resourcekey="rbtnAllCompaniesResource1"
                GroupName="Type" />
            <asp:RadioButton ID="rbtnSpecificCompany" runat="server" CssClass="Profiletitletxt"
                AutoPostBack="true" Text="Specific Company" OnClick="showCompany(this)" meta:resourcekey="rbtnSpecificCompanyResource1"
                GroupName="Type" />
            <asp:RadioButton ID="rbtnSpecificWorkLocation" runat="server" CssClass="Profiletitletxt"
                AutoPostBack="true" OnClick="showCompany(this)" Text="Specific Work Location"
                meta:resourcekey="rbtnSpecificWorkLocationResource1" GroupName="Type" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <div id="dvCompany" runat="server" style="display: none; background-color: #fafafa; border: solid 1px 000000;">
                <asp:CheckBoxList ID="chkLstCompany" runat="server" BorderStyle="None" RepeatColumns="3"
                    RepeatDirection="Horizontal" meta:resourcekey="chkLstCompanyResource1">
                </asp:CheckBoxList>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <div id="dvWorkLocation" runat="server" style="display: none; background-color: #fafafa; border: solid 1px 000000;">
                <asp:CheckBoxList ID="chkLstWorkLocation" runat="server" RepeatDirection="Vertical"
                    meta:resourcekey="chkLstWorkLocationResource1">
                </asp:CheckBoxList>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <asp:RadioButton ID="rbtnAllReligions" runat="server" CssClass="Profiletitletxt"
                OnClick="showReligion(this)" Text="All Religions" meta:resourcekey="rbtnAllReligionsResource1" />
            <asp:RadioButton ID="rbtnSpecificReligions" runat="server" CssClass="Profiletitletxt"
                OnClick="showReligion(this)" Text="Specific religion" meta:resourcekey="rbtnSpecificReligionsResource1" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <div id="dvReligion" runat="server" style="display: none; background-color: #fafafa; border: solid 1px FF0000;">
                <asp:CheckBoxList ID="chkLstReligion" runat="server" RepeatDirection="Vertical"
                    meta:resourcekey="chkLstReligionResource1">
                </asp:CheckBoxList>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-10">
            <asp:RadioButton ID="rbtnAllEmployeeTypes" runat="server" CssClass="Profiletitletxt"
                OnClick="showEmployeeType(this)" Text="All Employee Types" meta:resourcekey="rbtnAllEmployeeTypesResource1" />
            <asp:RadioButton ID="rbtnSpecificEmployeeTypes" runat="server" CssClass="Profiletitletxt"
                OnClick="showEmployeeType(this)" Text="Specific Employee Type" meta:resourcekey="rbtnSpecificEmployeeTypesResource1" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <div id="dvEmployeeType" runat="server" style="display: none; background-color: #fafafa; border: solid 1px FF0000;">
                <asp:CheckBoxList ID="chkLstEmployeeType" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                    Width="400px" meta:resourcekey="chkLstEmployeeTypeResource1">
                </asp:CheckBoxList>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-10">
            <asp:RadioButton ID="rbtnAllLogicalGroup" runat="server" CssClass="Profiletitletxt"
                OnClick="showLogicalGroup(this)" Text="All Logical Group" meta:resourcekey="rbtnAllLogicalGroup" />
            <asp:RadioButton ID="rbtnSpecificLogicalGroup" runat="server" CssClass="Profiletitletxt"
                OnClick="showLogicalGroup(this)" Text="Specific Logical Group" meta:resourcekey="rbtnSpecificLogicalGroup" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <div id="dvLogicalGroup" runat="server" style="display: none; background-color: #fafafa; border: solid 1px FF0000;">
                <asp:CheckBoxList ID="chkLstLogicalGroup" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                    Width="400px" meta:resourcekey="chkLstLogicalGroup">
                </asp:CheckBoxList>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-2">
        </div>
        <div class="col-md-4">
            <asp:CheckBox ID="chkIsReligionRelated" runat="server" Text="Religion Related Holiday"
                ToolTip="By Selecting This Option, The Defined Holiday Can Be Considered In The Overtime Calculation"
                meta:resourcekey="chkIsReligionRelatedResource1" />
        </div>
    </div>


    <div class="row" id="trControls" runat="server">
        <div class="col-md-12 text-center">
            <asp:Button ID="ibtnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="GrPolicy"
                meta:resourcekey="ibtnSaveResource1" />
            <asp:Button ID="ibtnDelete" OnClientClick="return ValidateDelete();" runat="server" Text="Delete" CssClass="button" CausesValidation="False"
                meta:resourcekey="ibtnDeleteResource1" />
            <asp:Button ID="ibtnRest" runat="server" Text="Clear" CssClass="button" CausesValidation="False"
                meta:resourcekey="ibtnRestResource1" />
        </div>
    </div>
    <div class="row">
        <div class="table-responsive">
            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" />
            <div class="filterDiv">
                <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdHoliday"
                    ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
            </div>
            <telerik:RadGrid runat="server" ID="dgrdHoliday" AutoGenerateColumns="false" PageSize="15"
                AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true"
                GroupingSettings-CaseSensitive="false">
                <MasterTableView IsFilterItemExpanded="true" CommandItemDisplay="Top" AllowFilteringByColumn="false" DataKeyNames="HolidayName,HolidayId">
                    <CommandItemTemplate>
                        <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick">
                            <Items>
                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                    ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource1" />
                            </Items>
                        </telerik:RadToolBar>
                    </CommandItemTemplate>
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="HolidayId" HeaderText="HolidayId" DataType="System.Int32"
                            AllowFiltering="false" SortExpression="HolidayId" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="HolidayName" HeaderText="Holiday Name" DataType="System.String"
                            AllowFiltering="true" ShowFilterIcon="true" SortExpression="HolidayName" Resizable="false"
                            meta:resourcekey="GridBoundColumnResource7">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="HolidayArabicName" DataType="System.String" HeaderText="Arabic Name"
                            SortExpression="HolidayArabicName" meta:resourcekey="GridBoundColumnResource8" />
                        <telerik:GridBoundColumn DataField="StartDate" HeaderText="Start Date" DataType="System.String"
                            AllowFiltering="true" ShowFilterIcon="true" SortExpression="StartDate" Resizable="false"
                            meta:resourcekey="GridBoundColumnResource9">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EndDate" HeaderText="End Date" DataType="System.String"
                            AllowFiltering="true" ShowFilterIcon="true" SortExpression="EndDate" Resizable="false"
                            meta:resourcekey="GridBoundColumnResource10">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IsYearly" DataType="System.String" AllowFiltering="false"
                            ShowFilterIcon="false" Resizable="false">
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView><ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                    EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
    </div>
    <script type="text/javascript">
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdHoliday.ClientID %>");
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
