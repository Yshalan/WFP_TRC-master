<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PermissionRequest.aspx.vb"
    MasterPageFile="~/Default/NewMaster.master" Inherits="Admin_LeavesRequest" Theme="SvTheme"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<%@ Register Src="../Employee/UserControls/EmpPermissions.ascx" TagName="EmpPermissions"
    TagPrefix="uc3" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">

        function validate(sender, args) {
            var RadTimePicker1 = $find("<%= RadTPfromTime.ClientID %>");
            var RadTimePicker2 = $find("<%= RadTPtoTime.ClientID %>");
            var validator = document.getElementById("<%= hdnIsvalid.ClientID %>");
            var Date1 = new Date(RadTimePicker1.get_timeView().getTime());
            var Date2 = new Date(RadTimePicker2.get_timeView().getTime());
            //args.IsValid = true;
            if ((Date2.getHours() - Date1.getHours()) < 0) {
                //alert("The to time value should be greater than the from time value");
                args.IsValid = false;
                validator.value = false;
            }
            else {
                args.IsValid = true;
                validator.value = true;
            }
        }

        function SetTimeFormat() {
            var picker = $("#RadTPfromTime").data("tDateTimePicker");
            picker.timeView.format = "HH:mm";
            picker.timeView.bind();
        }

        function ValidateTextboxFrom() {

            var tmpTime1 = $find("<%=rmtFlexibileTime.ClientID %>");

            txtValidate(tmpTime1, true);

        }
        function txtValidate(txt, IsFrom) {
            var strTime = String(txt._projectedValue);
            strTime = strTime.split(/\D/);

            if (strTime[0] == "") { strTime[0] = "00"; }
            if (strTime[1] == "") { strTime[1] = "00"; }
            if (strTime[1] > 59) {
                strTime[1] = "00";
                strTime[0] = String(Number(strTime[0]) + 1);
            }
            if (IsFrom) {
                if (strTime[0] > 23) {
                    strTime[0] = "00";
                }
            }
            else if (strTime[0] > 24) {
                strTime[0] = "24";
            }

            txt.set_value(strTime[0] + "" + strTime[1]);
            return false;
        }


        function HideShowIsFullyDay() {

            if (document.getElementById("<%= chckFullDay.ClientID %>").checked == true) {
                document.getElementById("<%= trTime.ClientID %>").style.visibility = 'hidden';
                document.getElementById("<%= trDifTime.ClientID %>").style.visibility = 'hidden';
            }
            else {
                document.getElementById("<%= trTime.ClientID %>").style.visibility = 'visible';
                document.getElementById("<%= trDifTime.ClientID %>").style.visibility = 'visible';
            }
        }

        function getSelectedListItem() {
            var radioButtonList = document.getElementById("<%= rdlTimeOption.ClientID %>");
            var listItems = radioButtonList.getElementsByTagName("input");
            for (var i = 0; i < listItems.length; i++) {
                if (listItems[i].checked) {
                    if (listItems[i].value == 0) {
                        document.getElementById("<%= trFlixibleTime.ClientID %>").style.visibility = 'hidden';
                        document.getElementById("<%= trSpecificTime.ClientID %>").style.visibility = 'visible';
                        document.getElementById("<%= lblPeriodInterval.ClientID %>").style.visibility = 'visible';
                        document.getElementById("<%= txtTimeDifference.ClientID %>").style.visibility = 'visible';
                    }
                    else if (listItems[i].value == 1) {
                        document.getElementById("<%= trFlixibleTime.ClientID %>").style.visibility = 'visible';
                        document.getElementById("<%= trSpecificTime.ClientID %>").style.visibility = 'hidden';
                        document.getElementById("<%= lblPeriodInterval.ClientID %>").style.visibility = 'hidden';
                        document.getElementById("<%= txtTimeDifference.ClientID %>").style.visibility = 'hidden';
                    }
            }
        }
    }

    function ValidateDelete(sender, eventArgs) {
        var grid = $find("<%=dgrdEmpPermissionRequest.ClientID%>");
        var masterTable = grid.get_masterTableView();
        var value = false;
        for (var i = 0; i < masterTable.get_dataItems().length; i++) {
            var gridItemElement = masterTable.get_dataItems()[i].findElement("chk");
            if (gridItemElement.checked) {
                value = true;
            }
        }
        if (value === false) {
            ShowMessage("<%= Resources.Strings.PleaseSelectFromList%>");
        }
        return value;
    }
    </script>

    <asp:Panel ID="Update1" runat="server">
        <uc1:PageHeader ID="EmpLeaveRequestHeader" runat="server" HeaderText="Employee Permission Request" />
        <asp:MultiView ID="mvEmpPermissionRequest" runat="server">
            <asp:View ID="viewPermissionRequests" runat="server">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblstatus" runat="server" CssClass="Profiletitletxt" Text="Status"
                            meta:resourcekey="lblstatusResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadComboBox ID="ddlStatus" runat="server" MarkFirstMatch="true" meta:resourcekey="ddlStatusResource1">
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="rfvStatus" ValidationGroup="Get" runat="server" ControlToValidate="ddlStatus"
                            Display="None" ErrorMessage="Please Select Permission Status" InitialValue="--Please Select--"
                            meta:resourcekey="rfvStatusResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="rfvStatus" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblFromDateSearch" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblFromDateSearchResource1"
                            Text="From Date"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker ID="dtpFromDateSearch" runat="server" Culture="English (United States)"
                            meta:resourcekey="RadDatePicker1Resource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="dtpFromDateSearch"
                            Display="None" ErrorMessage="Please Select Date" ValidationGroup="Get" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator9_ValidatorCalloutExtender"
                            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblToDateResource1"
                            Text="To Date"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker ID="dtpToDateSearch" runat="server" Culture="English (United States)"
                            meta:resourcekey="RadDatePicker2Resource1" Width="180px">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtpToDateSearch"
                            Display="None" ErrorMessage="Please Select Date" ValidationGroup="Get" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                            TargetControlID="RequiredFieldValidator1">
                        </cc1:ValidatorCalloutExtender>
                        <br />
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="dtpFromDateSearch"
                            ControlToValidate="dtpToDateSearch" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                            Operator="GreaterThanEqual" Type="Date" ValidationGroup="Get" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                        <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender4"
                            runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="row text-center">
                    <asp:Button ID="btnGet" runat="server" Text="Get" CssClass="button" ValidationGroup="Get"
                        meta:resourcekey="btnGetResource1" />
                </div>
                <div class="row text-center">
                    <asp:Button ID="btnRequestLeave" runat="server" Text="Request Permission" CssClass="button"
                        meta:resourcekey="Button1Resource1" />
                    &nbsp;
                <asp:Button ID="btnDelete" runat="server" Text="Delete Leave" OnClientClick="return ValidateDelete();" CssClass="button" meta:resourcekey="btnDeleteResource1" />
                </div>
                <div class="table-responsive">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdEmpPermissionRequest"
                        Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="dgrdEmpPermissionRequest" runat="server" AllowSorting="True"
                        PageSize="25" AllowPaging="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        ShowFooter="True" meta:resourcekey="dgrdEmpPermissionRequestResource1">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top"
                            DataKeyNames="PermissionId,FK_StatusId,AttachedFile,IsFullDay,IsFlexible,FlexibilePermissionDuration">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <%--  <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" meta:resourceKey="GridBoundColumnResource1"
                                        SortExpression="EmployeeName" UniqueName="EmployeeName">
                                    </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="PermName" HeaderText="Permission Type" meta:resourceKey="GridBoundColumnResource11"
                                    SortExpression="PermName" UniqueName="PermName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PermArabicName" HeaderText="Permission Type"
                                    meta:resourceKey="GridBoundColumnResource11" SortExpression="PermArabicName"
                                    UniqueName="PermArabicName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PermDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date"
                                    meta:resourceKey="GridBoundColumnResource10" SortExpression="PermDate" UniqueName="PermDate">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FromTime" DataFormatString="{0:HH:mm}" HeaderText="From Time"
                                    meta:resourceKey="GridBoundColumnResource12" SortExpression="FromTime" UniqueName="FromTime">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ToTime" DataFormatString="{0:HH:mm}" HeaderText="To Time"
                                    meta:resourceKey="GridBoundColumnResource13" SortExpression="ToTime" UniqueName="ToTime">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="PermissionId" meta:resourcekey="GridBoundColumnResource2"
                                    SortExpression="PermissionId" UniqueName="PermissionId" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FK_PermId" HeaderText="Perm Id" meta:resourcekey="GridBoundColumnResource3"
                                    SortExpression="FK_PermId" UniqueName="FK_PermId" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FK_EmployeeId" HeaderText="EmployeeId" meta:resourcekey="GridBoundColumnResource4"
                                    SortExpression="FK_EmployeeId" UniqueName="FK_EmployeeId" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status" meta:resourcekey="GridBoundColumnResource5"
                                    SortExpression="StatusName" UniqueName="StatusName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="StatusNameArabic" HeaderText="Status" meta:resourcekey="GridBoundColumnResource5"
                                    SortExpression="StatusNameArabic" UniqueName="StatusNameArabic">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="RejectionReason" HeaderText="Rejection Reason"
                                    meta:resourcekey="GridBoundColumnResource14" UniqueName="RejectionReason">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FK_StatusId" UniqueName="FK_StatusId" Visible="False" />
                                <telerik:GridBoundColumn DataField="AttachedFile" UniqueName="AttachedFile" Visible="False" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFullDay" UniqueName="IsFullDay"
                                    HeaderText="Fully Day" meta:resourcekey="GridBoundColumnResource16" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFlexible" UniqueName="IsFlexible"
                                    HeaderText="Flexible" meta:resourcekey="GridBoundColumnResource17" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="FlexibilePermissionDuration"
                                    Visible="False" UniqueName="FlexibilePermissionDuration" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
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
            </asp:View>
            <asp:View ID="viewAddPermissionRequest" runat="server">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="Svpanel">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblPermission" runat="server" Text="Type"
                                        meta:resourcekey="lblPermissionResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmpPermissions" MarkFirstMatch="True" Skin="Vista" ToolTip="View types of employee permission"
                                        runat="server" meta:resourcekey="RadCmpPermissionsResource1" AutoPostBack="true">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="reqPermission" runat="server" ControlToValidate="RadCmpPermissions"
                                        Display="None" ErrorMessage="Please select permission english name" InitialValue="--Please Select--"
                                        ValidationGroup="EmpPermissionGroup" meta:resourcekey="reqPermissionResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderPermission" runat="server" CssClass="AISCustomCalloutStyle"
                                        TargetControlID="reqPermission" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <%--   <tr>
                                                        <td>
                                                            <asp:Label ID="lblGeneralGuide" runat="server" CssClass="Profiletitletxt" Text="General Guide"
                                                                meta:resourcekey="lblGeneralGuideResource1" Visible="false" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="labelGeneralGuide" runat="server" CssClass="Profiletitletxt" />
                                                        </td>
                                                    </tr>--%>
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-6">
                                    <asp:RadioButton ID="radBtnOneDay" Text="One Time Permission" Checked="True" runat="server"
                                        AutoPostBack="True" GroupName="LeaveGroup" meta:resourcekey="radBtnOneDayResource1" />
                                    <asp:RadioButton ID="radBtnPeriod" Text="Permission For Period" runat="server" AutoPostBack="True"
                                        GroupName="LeaveGroup" meta:resourcekey="radBtnPeriodResource1" />
                                </div>
                            </div>
                            <div class="row">
                                <asp:Panel ID="PnlOneDayLeave" runat="server" meta:resourcekey="PnlOneDayLeaveResource1">
                                    <div class="col-md-2">
                                        <asp:Label CssClass="Profiletitletxt" ID="lblPermissionDate" runat="server" Text="Date"
                                            meta:resourcekey="lblPermissionDateResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label CssClass="Profiletitletxt" ID="lblAtDate" runat="server" Text="At" meta:resourcekey="lblAtDateResource1"></asp:Label>
                                        <telerik:RadDatePicker ID="dtpPermissionDate" AllowCustomText="false" MarkFirstMatch="true"
                                            Skin="Vista" runat="server" Culture="English (United States)" meta:resourcekey="dtpPermissionDateResource1"
                                            AutoPostBack="true">
                                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                            </Calendar>
                                            <DateInput DateFormat="dd/MM/yyyy" ToolTip="View permission date" DisplayDateFormat="dd/MM/yyyy"
                                                LabelCssClass="" Width="">
                                            </DateInput>
                                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="reqPermissionDate" runat="server" ControlToValidate="dtpPermissionDate"
                                            Display="None" ErrorMessage="Please select permission date" ValidationGroup="EmpPermissionGroup"
                                            meta:resourcekey="reqPermissionDateResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderPermissionDate" runat="server" TargetControlID="reqPermissionDate"
                                            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </asp:Panel>
                            </div>
                            <asp:Panel ID="pnlPeriodLeave" Visible="False" runat="server" meta:resourcekey="pnlPeriodLeaveResource1">
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label CssClass="Profiletitletxt" ID="lblDateFrom" runat="server" Text="Date"
                                            meta:resourcekey="lblDateFromResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label CssClass="Profiletitletxt" ID="lblFromDate" runat="server" Text="From"
                                            meta:resourcekey="lblFromDateResource1"></asp:Label>
                                        <telerik:RadDatePicker ID="dtpStartDatePerm" ToolTip="Click" AllowCustomText="false"
                                            MarkFirstMatch="true" Skin="Vista" runat="server" Culture="English (United States)"
                                            meta:resourcekey="dtpStartDatePermResource1" AutoPostBack="true">
                                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                            </Calendar>
                                            <DateInput DateFormat="dd/MM/yyyy" ToolTip="View start date permission" DisplayDateFormat="dd/MM/yyyy"
                                                LabelCssClass="" Width="">
                                            </DateInput>
                                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="reqStartDate" runat="server" ControlToValidate="dtpStartDatePerm"
                                            Display="None" ErrorMessage="Please select start date" ValidationGroup="EmpPermissionGroup"
                                            meta:resourcekey="reqStartDateResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderStartDate" runat="server" TargetControlID="reqStartDate"
                                            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Label CssClass="Profiletitletxt" ID="lblEndDate" runat="server" Text="To" meta:resourcekey="lblEndDateResource1"></asp:Label>
                                        <telerik:RadDatePicker ID="dtpEndDatePerm" AllowCustomText="false" MarkFirstMatch="true"
                                            Skin="Vista" runat="server" AutoPostBack="True" Culture="English (United States)"
                                            meta:resourcekey="dtpEndDatePermResource1">
                                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                            </Calendar>
                                            <DateInput DateFormat="dd/MM/yyyy" ToolTip="View end date permission" AutoPostBack="True"
                                                DisplayDateFormat="dd/MM/yyyy" LabelCssClass="" Width="">
                                            </DateInput>
                                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                        </telerik:RadDatePicker>
                                        <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dtpStartDatePerm"
                                            ControlToValidate="dtpEndDatePerm" ErrorMessage="To Date should be greater than or equal to From Date"
                                            Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="EmpPermissionGroup"
                                            meta:resourcekey="CVDateResource1" />
                                        <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender2"
                                            runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                            </asp:Panel>
                            <br />
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblRemainingBalance" runat="server" Text="Remaining Balance" CssClass="Profiletitletxt"
                                        meta:resourcekey="lblRemainingBalanceResource1" />
                                </div>
                                <div class="col-md-1">
                                    <asp:Label ID="lblRemainingBalanceValue" runat="server" CssClass="profiletitletxt" />
                                </div>
                                <div class="col-md-1">
                                    <asp:Label ID="lblRemainingBalanceHours" runat="server" Text="Hours" CssClass="Profiletitletxt"
                                        meta:resourcekey="lblRemainingBalanceHoursResource1" />
                                </div>
                            </div>
                            <div id="trFullyDay" runat="server" class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblIsFullyDay" runat="server" Text="Is Full Day" CssClass="Profiletitletxt"
                                        meta:resourcekey="chckFullDayResource1" />
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="chckFullDay" Text="&nbsp;" runat="server" AutoPostBack="True" />
                                </div>
                            </div>
                            <div id="trTime" runat="server" class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblTimeFrom" runat="server" Text="Time"
                                        meta:resourcekey="lblTimeFromResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:RadioButtonList ID="rdlTimeOption" runat="server" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rdlTimeOption_OnSelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Specific Time" Value="0" Selected="True" meta:resourcekey="ListItemResource1" />
                                        <asp:ListItem Text="Flexible Time" Value="1" meta:resourcekey="ListItemResource2" />
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div id="trFlixibleTime" runat="server" class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadMaskedTextBox ID="rmtFlexibileTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                        DisplayMask="##:##" Text='0000' LabelCssClass="">
                                        <ClientEvents OnBlur="ValidateTextboxFrom" />
                                    </telerik:RadMaskedTextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblhent" runat="server" Text="Hours : Minutes" CssClass="Profiletitletxt"
                                        meta:resourcekey="lblhentResource1" />
                                    <asp:RequiredFieldValidator ID="reqFlexibiletime" runat="server" ControlToValidate="rmtFlexibileTime"
                                        Display="None" ErrorMessage="Please enter flixible time" ValidationGroup="EmpPermissionGroup"
                                        meta:resourcekey="reqFlexibiletimeResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderFlexibileTime" runat="server" CssClass="AISCustomCalloutStyle"
                                        TargetControlID="reqFlexibiletime" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div id="trSpecificTime" runat="server" class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblFrom" Text="From" runat="server" meta:resourcekey="lblFromResource1"></asp:Label>
                                    <telerik:RadTimePicker ID="RadTPfromTime" runat="server" AllowCustomText="false"
                                        MarkFirstMatch="true" Skin="Vista" AutoPostBack="True" meta:resourcekey="RadTPfromTimeResource1">
                                        <DateInput ID="DateInput4" runat="server" ToolTip="View start time" DateFormat="HH:mm" />
                                    </telerik:RadTimePicker>
                                    <asp:RequiredFieldValidator ID="reqFromtime" runat="server" ControlToValidate="RadTPfromTime"
                                        Display="None" ErrorMessage="Please select start time" ValidationGroup="EmpPermissionGroup"
                                        meta:resourcekey="reqFromtimeResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderFromTime" runat="server" CssClass="AISCustomCalloutStyle"
                                        TargetControlID="reqFromtime" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblTo" runat="server" Text="To" meta:resourcekey="lblToResource1"></asp:Label>
                                    <telerik:RadTimePicker ID="RadTPtoTime" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                        Skin="Vista" AutoPostBack="True" AutoPostBackControl="TimeView" meta:resourcekey="RadTPtoTimeResource1">
                                        <DateInput ID="DateInput1" runat="server" ToolTip="View end time" DateFormat="HH:mm" />
                                    </telerik:RadTimePicker>
                                    <asp:RequiredFieldValidator ID="reqToTime" runat="server" ControlToValidate="RadTPtoTime"
                                        Display="None" ErrorMessage="Please select end time" ValidationGroup="EmpPermissionGroup"
                                        meta:resourcekey="reqToTimeResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqToTime" runat="server" CssClass="AISCustomCalloutStyle"
                                        TargetControlID="reqToTime" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="RadTPtoTime"
                                        ClientValidationFunction="validate" Display="None" ValidationGroup="EmpPermissionGroup"
                                        meta:resourcekey="CustomValidator1Resource1" />
                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="RadTPfromTime"
                                        ClientValidationFunction="validate" Display="None" ValidationGroup="EmpPermissionGroup"
                                        meta:resourcekey="CustomValidator2Resource1" />
                                    <cc1:ValidatorCalloutExtender ID="ExtenderCusToTime" runat="server" CssClass="AISCustomCalloutStyle"
                                        TargetControlID="CustomValidator1" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderCusFromTime" runat="server" CssClass="AISCustomCalloutStyle"
                                        TargetControlID="CustomValidator2" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                    <asp:HiddenField ID="hdnIsvalid" runat="server" />
                                </div>
                            </div>
                            <br />
                            <div id="trDifTime" runat="server" class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblPeriodInterval" runat="server" Text="Duration"
                                        meta:resourcekey="lblPeriodIntervalResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtTimeDifference" ReadOnly="True" runat="server" meta:resourcekey="txtTimeDifferenceResource1"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="Label1" is="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
                                        meta:resourcekey="lblAttachFileResource1" />
                                </div>
                                <div id="trAttachedFile" runat="server" class="col-md-4">
                                    <div class="input-group">

                                        <span class="input-group-btn"><span class="btn btn-default" onclick="$(this).parent().find('input[type=file]').click();">Browse</span>
                                            <asp:FileUpload ID="fuAttachFile" runat="server" meta:resourcekey="fuAttachFileResource1"
                                                name="uploaded_file" onchange="$(this).parent().parent().find('.form-control').html($(this).val().split(/[\\|/]/).pop());"
                                                Style="display: none;" type="file" />
                                        </span><span class="form-control"></span>
                                    </div>
                                    <div class="veiw_remove">
                                        <asp:LinkButton ID="lnbLeaveFile" runat="server" Visible="False" Text="View" meta:resourcekey="lblViewResource1" OnClick="lnkDownloadFile_Click">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnbRemove" runat="server" Text="Remove" Visible="False" meta:resourcekey="lnbRemoveResource1" />
                                        <asp:Label ID="lblNoAttachedFile" runat="server" Text="No Attached File" Visible="False"
                                            meta:resourcekey="lblNoAttachedFileResource1" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblRemarks" runat="server" Text="Remarks"
                                        meta:resourcekey="lblRemarksResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Panel ID="pnlTempHidRows" runat="server" Visible="False" meta:resourcekey="pnlTempHidRowsResource1">
                                    <div class="col-md-2">
                                        <asp:Label CssClass="Profiletitletxt" ID="lblIsSpecifiedDays" runat="server" Text="Specified days"
                                            meta:resourcekey="lblIsSpecifiedDaysResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:CheckBox ID="chckSpecifiedDays" runat="server" meta:resourcekey="chckSpecifiedDaysResource1" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label CssClass="Profiletitletxt" ID="lblDays" runat="server" Text="Days" meta:resourcekey="lblDaysResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:TextBox ID="txtDays" runat="server" meta:resourcekey="txtDaysResource1"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label CssClass="Profiletitletxt" ID="lblIsFlexible" runat="server" Text="Flexible"
                                            meta:resourcekey="lblIsFlexibleResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:CheckBox ID="chckIsFlexible" runat="server" meta:resourcekey="chckIsFlexibleResource1" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label CssClass="Profiletitletxt" ID="lblIsDividable" runat="server" Text="Dividable"
                                            meta:resourcekey="lblIsDividableResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:CheckBox ID="chckIsDividable" runat="server" meta:resourcekey="chckIsDividableResource1" />
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-8">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="EmpPermissionGroup"
                                        meta:resourcekey="btnSaveResource1" />
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClearResource1" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" CausesValidation="False"
                                        meta:resourcekey="btnCancelResource1" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblGeneralGuide" runat="server" CssClass="Profiletitletxt" Text="General Guide"
                                        meta:resourcekey="lblGeneralGuideResource1" Visible="false" />
                                </div>
                            </div>
                            <div runat="server" id="dvGeneralGuide" style="margin-top: 5px; background-color: #FDF5B8;">
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:View>
        </asp:MultiView>

    </asp:Panel>

</asp:Content>
