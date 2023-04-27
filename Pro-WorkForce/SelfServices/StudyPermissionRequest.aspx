<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="StudyPermissionRequest.aspx.vb" Inherits="SelfServices_StudyPermissionRequest"
    meta:resourcekey="PageResource1" UICulture="auto" Theme="SvTheme" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">


        function RefreshPage() {
            window.location = "../SelfServices/StudyPermissionRequest.aspx";

        }

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

        function DateSelected(sender, eventArgs) {
            var dt = sender.get_selectedDate();
            var myDate = new Date(sender.get_selectedDate().format("MM/dd/yyyy"));
            var hdnNurdingDay = document.getElementById("<%= hdnNurdingDay.ClientID %>");
            var nursingDay = parseInt(hdnNurdingDay.value);
            myDate.setDate(myDate.getDate() + nursingDay);
            var datepicker = $find("<%= dtpEndDatePerm.ClientID %>");
            datepicker.set_selectedDate(myDate);
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <uc1:PageHeader ID="EmpStudyPermRequestHeader" runat="server" HeaderText="Employee Study Permission Request" />
    <asp:MultiView ID="mvEmpPermissionRequest" runat="server">
        <asp:View ID="viewPermissionRequests" runat="server">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblstatus" runat="server" CssClass="Profiletitletxt" Text="Status"
                        meta:resourcekey="lblstatusResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlStatus" runat="server" MarkFirstMatch="True" meta:resourcekey="ddlStatusResource1">
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
                    <asp:Label ID="lblFromDateSearch" runat="server" CssClass="Profiletitletxt" Text="From Date"
                        meta:resourcekey="lblFromDateSearchResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpFromDateSearch" runat="server" Culture="en-US" meta:resourcekey="dtpFromDateSearchResource1">
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
                    <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt" Text="To Date"
                        meta:resourcekey="lblToDateResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpToDateSearch" runat="server" Culture="en-US" meta:resourcekey="dtpToDateSearchResource1">
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
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnGet" runat="server" Text="Get" ValidationGroup="Get" meta:resourcekey="btnGetResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnRequestPermission" runat="server" Text="Request Study Permission"
                        meta:resourcekey="btnRequestPermissionResource1" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete Permission" OnClientClick="return ValidateDelete();" meta:resourcekey="btnDeleteResource1" />
                </div>
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
                        DataKeyNames="PermissionRequestId,FK_StatusId,AttachedFile">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="PermDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="From Date"
                                SortExpression="PermDate" UniqueName="PermDate" meta:resourcekey="GridBoundColumnResource1">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PermEndDate" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderText="To Date" SortExpression="PermEndDate" UniqueName="PermEndDate" meta:resourcekey="GridBoundColumnResource2">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="PermissionRequestId" SortExpression="PermissionRequestId"
                                UniqueName="PermissionRequestId" Visible="False" meta:resourcekey="GridBoundColumnResource3">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FK_PermId" HeaderText="Perm Id" SortExpression="FK_PermId"
                                UniqueName="FK_PermId" Visible="False" meta:resourcekey="GridBoundColumnResource4">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FK_EmployeeId" HeaderText="EmployeeId" SortExpression="FK_EmployeeId"
                                UniqueName="FK_EmployeeId" Visible="False" meta:resourcekey="GridBoundColumnResource5">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FK_StatusId" UniqueName="FK_StatusId" Visible="False"
                                meta:resourcekey="GridBoundColumnResource6" />
                            <telerik:GridBoundColumn DataField="AttachedFile" UniqueName="AttachedFile" Visible="False"
                                meta:resourcekey="GridBoundColumnResource7" />
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFullDay" UniqueName="IsFullDay"
                                HeaderText="Fully Day" meta:resourcekey="GridBoundColumnResource8" />
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFlexible" UniqueName="IsFlexible"
                                HeaderText="Flexible" meta:resourcekey="GridBoundColumnResource9" />
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="FlexibilePermissionDuration"
                                UniqueName="FlexibilePermissionDuration" HeaderText="Flexibile Duration" meta:resourcekey="GridBoundColumnResource10" />
                            <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status" SortExpression="StatusName"
                                UniqueName="StatusName" meta:resourcekey="GridBoundColumnResource11">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StatusNameArabic" HeaderText="Status" SortExpression="StatusNameArabic"
                                UniqueName="StatusNameArabic" meta:resourcekey="GridBoundColumnResource12">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RejectionReason" HeaderText="Rejection Reason"
                                UniqueName="RejectionReason" meta:resourcekey="GridBoundColumnResource13">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                <Items>
                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                        ImagePosition="Right" runat="server" Owner="" meta:resourcekey="RadToolBarButtonResource1" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <SelectedItemStyle ForeColor="Maroon" />
                </telerik:RadGrid>
            </div>
        </asp:View>
        <asp:View ID="viewAddPermissionRequest" runat="server">
            <asp:UpdatePanel ID="Update1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div runat="server" id="dvGeneralGuide" style="margin-top: 5px; background-color: #FDF5B8;">
                            <asp:Label ID="lblGeneralGuide" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblGeneralGuideResource1" />
                        </div>
                    </div>
                    <div class="Svpanel">
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="lblDateFrom" runat="server" Text="Date"
                                    meta:resourcekey="lblDateFromResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:Label CssClass="Profiletitletxt" ID="lblFromDate" runat="server" Text="From"
                                    meta:resourcekey="lblFromDateResource1"></asp:Label>
                                <telerik:RadDatePicker ID="dtpStartDatePerm" ToolTip="Click" AllowCustomText="false"
                                    MarkFirstMatch="true" Skin="Vista" runat="server" Culture="en-US" meta:resourcekey="dtpStartDatePermResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" ToolTip="View start date permission" DisplayDateFormat="dd/MM/yyyy"
                                        LabelCssClass="" Width="">
                                    </DateInput>
                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    <ClientEvents OnDateSelected="DateSelected" />
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="reqStartDate" runat="server" ControlToValidate="dtpStartDatePerm"
                                    Display="None" ErrorMessage="Please select start date" ValidationGroup="EmpPermissionGroup"
                                    meta:resourcekey="reqStartDateResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ExtenderStartDate" runat="server" TargetControlID="reqStartDate"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                                <asp:HiddenField ID="hdnNurdingDay" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-4">
                            <asp:Label CssClass="Profiletitletxt" ID="lblEndDate" runat="server" Text="To" meta:resourcekey="lblEndDateResource1"></asp:Label>
                            <telerik:RadDatePicker ID="dtpEndDatePerm" AllowCustomText="false" MarkFirstMatch="true"
                                Skin="Vista" runat="server" AutoPostBack="True" Culture="en-US" meta:resourcekey="dtpEndDatePermResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" ToolTip="View end date permission" AutoPostBack="True"
                                    DisplayDateFormat="dd/MM/yyyy" LabelCssClass="" Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqEndDate" runat="server" ControlToValidate="dtpEndDatePerm"
                                Display="None" ErrorMessage="Please select end date" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="reqEndDateResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ExtenderEndDate" runat="server" TargetControlID="reqEndDate"
                                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dtpStartDatePerm"
                                ControlToValidate="dtpEndDatePerm" ErrorMessage="To Date should be greater than or equal to From Date"
                                Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="CVDateResource1" />
                            <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender111"
                                runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div id="trWeekDays" runat="server" class="row">
                        <div id="Td1" runat="server" class="col-md-2">
                            <asp:Label ID="lblDaysList" runat="server" Text="Week Days" CssClass="Profiletitletxt"
                                meta:resourcekey="lblDaysListResource1" />
                        </div>
                        <div class="col-md-10">
                            <asp:CheckBoxList ID="chkWeekDays" runat="server" RepeatColumns="3" RepeatDirection="vertical" />
                        </div>
                    </div>
                    <div id="trTime" runat="server" class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblTimeFrom" runat="server" Text="Time"
                                meta:resourcekey="lblTimeFromResource1"></asp:Label>
                        </div>
                        <div class="col-md-10">
                            <asp:RadioButtonList ID="rdlTimeOption" runat="server" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="rdlTimeOption_OnSelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Text="Specific Time" Value="0" Selected="True" meta:resourcekey="ListItemResource1" />
                                <asp:ListItem Text="Flexible Time" Value="1" meta:resourcekey="ListItemResource2" />
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div id="trFlixibleTime" runat="server" class="col-md-4">
                            <telerik:RadMaskedTextBox ID="rmtFlexibileTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                CssClass="RadMaskedTextBox" DisplayMask="##:##" Text='0000' LabelCssClass="">
                                <ClientEvents OnBlur="ValidateTextboxFrom" />
                            </telerik:RadMaskedTextBox>
                            <asp:Label ID="lblhent" runat="server" Text="Hours : Minutes" CssClass="Profiletitletxt"
                                meta:resourcekey="lblhentResource1" />
                            <asp:RequiredFieldValidator ID="reqFlexibiletime" runat="server" ControlToValidate="rmtFlexibileTime"
                                Display="None" ErrorMessage="Please enter flixible time" ValidationGroup="EmpPermissionGroup"></asp:RequiredFieldValidator>
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
                            <asp:Label CssClass="Profiletitletxt" ID="lblPeriodInterval" runat="server" Text="Period"
                                meta:resourcekey="lblPeriodIntervalResource1"></asp:Label>
                        </div>
                        <div id="trDif" runat="server" class="col-md-4">
                            <asp:TextBox ID="txtTimeDifference" ReadOnly="True" runat="server" />
                        </div>
                    </div>
                    <br />
                    <asp:Panel ID="pnlAdditionalDetails" runat="server" GroupingText="Additional Study Details" meta:resourcekey="pnlAdditionalDetailsResource1">
                        <div class="row" id="dvUniversity" runat="server" visible="false">
                            <div class="col-md-2">
                                <asp:Label ID="lblUnversity" runat="server" Text="University Or College" meta:resourcekey="lblUnversityResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="radcmbxUnversity" CausesValidation="False" Filter="Contains"
                                    AutoPostBack="false" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                                    meta:resourcekey="radcmbxUnversityResource1" ExpandDirection="Up">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div id="dvMajor_Specialization" runat="server" visible="false">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblMajor" runat="server" Text="Major" meta:resourcekey="lblMajorResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="radcmbxMajor" CausesValidation="False" Filter="Contains" 
                                        AutoPostBack="true" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                                        ExpandDirection="Up" meta:resourcekey="radcmbxMajorResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblSpecialization" runat="server" Text="Specialization" meta:resourcekey="lblSpecializationResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="radcmbxSpecialization" CausesValidation="False" Filter="Contains"
                                        AutoPostBack="false" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                                        ExpandDirection="Up" meta:resourcekey="radcmbxSpecializationResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="lblStudyYear" runat="server" Text="Study Year"
                                    meta:resourcekey="lblYearResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtStudyYear" MinValue="0" MaxValue="99999" Skin="Vista"
                                    runat="server" Culture="en-US" LabelCssClass="" Width="158px" meta:resourcekey="txtStudyYearResource1">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="lblSemester" runat="server" Text="Semester"
                                    meta:resourcekey="lblSemesterResource1"></asp:Label>
                            </div>
                            <div class="col-md-4" id="dvSemesterText" runat="server" visible="false">
                                <asp:TextBox ID="txtSemester" Visible="false" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-4" id="dvSemesterSelection" runat="server" visible="false">
                                <telerik:RadComboBox ID="radcmbxSemester" CausesValidation="False" Filter="Contains"
                                    AutoPostBack="false" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                                    meta:resourcekey="radcmbxSemesterResource1" ExpandDirection="Up">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblEmp_GPAType" runat="server" Text="GPA Type" ToolTip="Grade Point Average Type (Percentage or Points (4.0))"
                                    meta:resourcekey="lblEmp_GPATypeResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:RadioButtonList ID="rblEmp_GPAType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                    <asp:ListItem Text="Percentage" Value="1" Selected="True" meta:resourcekey="Emp_GPATypeListItem1Resource1"></asp:ListItem>
                                    <asp:ListItem Text="Points" Value="2" meta:resourcekey="Emp_GPATypeListItem2Resource1"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblEmp_GPA" runat="server" Text="GPA" ToolTip="Grade Point Average" meta:resourcekey="lblEmp_GPAResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtEmp_GPA" MinValue="0" MaxValue="100"
                                    Skin="Vista" runat="server" Culture="en-US" LabelCssClass="">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </div>
                        </div>
                    </asp:Panel>
                    <hr />
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
                                meta:resourcekey="lblAttachFileResource1" />
                        </div>
                        <div class="col-md-4">
                            <div id="trAttachedFile" runat="server">
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
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblRemarks" runat="server" Text="Remarks"
                                meta:resourcekey="lblRemarksResource1"></asp:Label>
                        </div>
                        <div id="trRemarks" runat="server" class="col-md-4">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-8">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="btnSaveResource1" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" CausesValidation="False"
                                meta:resourcekey="btnClearResource1" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" CausesValidation="False"
                                meta:resourcekey="btnCancelResource1" />
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
