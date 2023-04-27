<%@ Control Language="VB" AutoEventWireup="false" CodeFile="HR_PermissionRequest.ascx.vb"
    Inherits="Employee_UserControls_HR_PermissionRequest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:UpdatePanel ID="updatePnlEmpPerm" runat="server">
    <ContentTemplate>
        <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
        <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
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
                    args.IsValid = true;
                    validator.value = true;
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

        </script>

        <uc1:PageHeader ID="userCtrlEmpPermHeader" runat="server" HeaderText="Employee Permissions" />

        <div class="row">
            <div class="col-md-12">
                <uc1:PageFilter ID="EmployeeFilterUC" runat="server" HeaderText="Employee Filter"
                    OneventEmployeeSelect="FillGridView" ValidationGroup="EmpPermissionGroup" />
            </div>
        </div>
        <div class="row" id="trPermType" runat="server">
            <div class="col-md-2">
                <asp:Label CssClass="Profiletitletxt" ID="lblPermission" runat="server" Text="Type"
                    meta:resourcekey="lblPermissionResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadComboBox ID="RadCmpPermissions" MarkFirstMatch="True" AutoPostBack="True"
                    Skin="Vista" ToolTip="View types of employee permission" runat="server">
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="reqPermission" runat="server" ControlToValidate="RadCmpPermissions"
                    Display="None" ErrorMessage="Please select permission english name" InitialValue="--Please Select--"
                    ValidationGroup="EmpPermissionGroup"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ExtenderPermission" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="reqPermission" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                </cc1:ValidatorCalloutExtender>
            </div>
        </div>
        <div class="row" id="trType" runat="server">
            <div class="col-md-8" id="tdOption" runat="server">
                <asp:RadioButton ID="radBtnOneDay" Text="One Time Permission" Checked="True" runat="server"
                    AutoPostBack="True" GroupName="LeaveGroup" meta:resourcekey="radBtnOneDayResource1" />
                <asp:RadioButton ID="radBtnPeriod" Text="Permission For Period" runat="server" AutoPostBack="True"
                    GroupName="LeaveGroup" meta:resourcekey="radBtnPeriodResource1" />
                <asp:RadioButton ID="radBtnSpecificDays" Text="Specific Days for Duration" runat="server"
                    AutoPostBack="True" GroupName="LeaveGroup" meta:resourcekey="radBtnSpecificDaysResource1" />
            </div>
        </div>
        <asp:Panel ID="PnlOneDayLeave" runat="server" meta:resourcekey="PnlOneDayLeaveResource1">
            <div class="row">
                <div class="col-md-2" id="tdDate" runat="server">
                    <asp:Label CssClass="Profiletitletxt" ID="lblPermissionDate" runat="server" Text="Date"
                        meta:resourcekey="lblPermissionDateResource1"></asp:Label>
                </div>
                <div class="col-md-1">
                    <asp:Label CssClass="Profiletitletxt" ID="lblAtDate" runat="server" Text="At" meta:resourcekey="lblAtDateResource1"></asp:Label>
                </div>
                <div class="col-md-3">
                    <telerik:RadDatePicker ID="dtpPermissionDate" AllowCustomText="false" MarkFirstMatch="true"
                        Skin="Vista" runat="server" Culture="en-US" meta:resourcekey="dtpPermissionDateResource1">
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
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlPeriodLeave" Visible="False" runat="server" meta:resourcekey="pnlPeriodLeaveResource1">
            <div class="row" id="trWeekDays" runat="server">
                <div class="col-md-2" runat="server">
                    <asp:Label ID="lblDaysList" runat="server" Text="Week Days" CssClass="Profiletitletxt"
                        meta:resourcekey="lblDaysListResource1" />
                </div>
                <div id="tdWeekDays" runat="server" class="col-md-6">
                    <asp:CheckBoxList ID="chkWeekDays" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblDateFrom" runat="server" Text="Date"
                        meta:resourcekey="lblDateFromResource1"></asp:Label>
                </div>
                <div id="trDateFromTo" runat="server">
                    <div class="col-md-1">
                        <asp:Label CssClass="Profiletitletxt" ID="lblFromDate" runat="server" Text="From"
                            meta:resourcekey="lblFromDateResource1"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <telerik:RadDatePicker ID="dtpStartDatePerm" ToolTip="Click" AllowCustomText="false"
                            MarkFirstMatch="true" Skin="Vista" runat="server" Culture="en-US" meta:resourcekey="dtpStartDatePermResource1">
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            <ClientEvents OnDateSelected="DateSelected" />
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" ToolTip="View start date permission" DisplayDateFormat="dd/MM/yyyy"
                                LabelCssClass="" Width="">
                            </DateInput>
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
                    <div class="col-md-1">

                        <asp:Label CssClass="Profiletitletxt" ID="lblEndDate" runat="server" Text="To" meta:resourcekey="lblEndDateResource1"></asp:Label>
                    </div>
                    <div class="col-md-3">
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
                        <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender1"
                            runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="row" id="trNursingFlexibleDurationPermission" runat="server" visible="False">
            <div class="col-md-2">
                <asp:Label ID="lblNursingFlexibleDuration" runat="server" CssClass="Profiletitletxt"
                    Text="Flexible Permission Duration" />
            </div>
            <div class="col-md-4" id="tdNursingFlexibleDurationPermission" runat="server">
                <telerik:RadComboBox ID="RadCmbFlixebleDuration" MarkFirstMatch="True" Skin="Vista"
                    runat="server">
                    <Items>
                        <telerik:RadComboBoxItem Text="One Hour" Value="60" runat="server" Owner="" />
                        <telerik:RadComboBoxItem Text="Two Hours" Value="120" runat="server" Owner="" />
                    </Items>
                </telerik:RadComboBox>
            </div>
        </div>
        <div class="row" id="trFullyDay" runat="server">
            <div class="col-md-2">
            </div>
            <div class="col-md-4">
                <asp:CheckBox ID="chckFullDay" runat="server" Text="Is Full Day" meta:resourcekey="chckFullDayResource1" AutoPostBack="True" />
            </div>
        </div>
        <div class="row" id="trTime" runat="server">
            <div class="col-md-2">
                <asp:Label CssClass="Profiletitletxt" ID="lblTimeFrom" runat="server" Text="Time"
                    meta:resourcekey="lblTimeFromResource1"></asp:Label>
            </div>
            <div id="trTimeFromTo" runat="server">
                <div class="col-md-4">
                    <asp:RadioButtonList ID="rdlTimeOption" runat="server" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="rdlTimeOption_OnSelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Specific Time" Value="0" Selected="True" meta:resourcekey="ListItemResource1" />
                        <asp:ListItem Text="Flexible Time" Value="1" meta:resourcekey="ListItemResource2" />
                    </asp:RadioButtonList>
                </div>
            </div>
        </div>
        <div class="row" id="trFlixibleTime" runat="server">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <telerik:RadMaskedTextBox ID="rmtFlexibileTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                    CssClass="RadMaskedTextBox" DisplayMask="##:##" Text='0000' LabelCssClass="">
                    <ClientEvents OnBlur="ValidateTextboxFrom" />
                </telerik:RadMaskedTextBox>
            </div>
            <div class="col-md-2">
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
        <div class="row" id="trSpecificTime" runat="server">
            <div class="col-md-2"></div>
            <div class="col-md-1">
                <asp:Label CssClass="Profiletitletxt" ID="lblFrom" Text="From" runat="server" meta:resourcekey="lblFromResource1"></asp:Label>
            </div>
            <div class="col-md-2">
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
            <div class="col-md-12"></div>
            <div class="col-md-2"></div>
            <div class="col-md-1">
                <asp:Label CssClass="Profiletitletxt" ID="lblTo" runat="server" Text="To" meta:resourcekey="lblToResource1"></asp:Label>
            </div>
            <div class="col-md-2">
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
        <div class="row" id="trDifTime" runat="server">
            <div class="col-md-2">
                <asp:Label CssClass="Profiletitletxt" ID="lblPeriodInterval" runat="server" Text="Period"
                    meta:resourcekey="lblPeriodIntervalResource1"></asp:Label>
            </div>
            <div class="col-md-4" id="trDif" runat="server">
                <asp:TextBox ID="txtTimeDifference" ReadOnly="True" runat="server" meta:resourcekey="txtTimeDifferenceResource1" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
                    meta:resourcekey="lblAttachFileResource1" />
            </div>
            <div id="trAttachedFile" runat="server" class="col-md-4">
                <div class="input-group">
                    <span class="input-group-btn">
                        <span class="btn btn-default" onclick="$(this).parent().find('input[type=file]').click();">Browse</span>
                        <asp:FileUpload ID="fuAttachFile" runat="server" meta:resourcekey="fuAttachFileResource1" name="uploaded_file" onchange="$(this).parent().parent().find('.form-control').html($(this).val().split(/[\\|/]/).pop());" Style="display: none;" type="file" />
                    </span>
                    <span class="form-control"></span>
                </div>
                <div class="veiw_remove">
                    <a id="lnbLeaveFile" target="_blank" runat="server" visible="False">
                        <asp:Label ID="lblView" runat="server" Text="View" meta:resourcekey="lblViewResource1" />
                    </a>
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
            <div class="col-md-4" id="trRemarks" runat="server">
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
            </div>
        </div>
        <asp:Panel ID="pnlTempHidRows" runat="server" Visible="False" meta:resourcekey="pnlTempHidRowsResource1">
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chckSpecifiedDays" runat="server" Text="Specified days"
                        meta:resourcekey="lblIsSpecifiedDaysResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblDays" runat="server" Text="Days" meta:resourcekey="lblDaysResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtDays" runat="server" meta:resourcekey="txtDaysResource1"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chckIsFlexible" runat="server" Text="Flexible"
                        meta:resourcekey="lblIsFlexibleResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chckIsDividable" runat="server" Text="Dividable"
                        meta:resourcekey="lblIsDividableResource1" />
                </div>
            </div>
        </asp:Panel>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-6">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="EmpPermissionGroup"
                    meta:resourcekey="btnSaveResource1" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" CausesValidation="False"
                    meta:resourcekey="btnDeleteResource1" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" CausesValidation="False"
                    meta:resourcekey="btnClearResource1" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblFromDateSearch" runat="server" CssClass="Profiletitletxt" Text="From Date"
                    meta:resourcekey="lblFromDateSearchResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadDatePicker ID="dtpFromDateSearch" runat="server" Culture="en-US"
                    meta:resourcekey="dtpFromDateSearchResource1">
                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                    </Calendar>
                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                        Width="">
                    </DateInput>
                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                </telerik:RadDatePicker>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="dtpFromDateSearch"
                    Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
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
                <telerik:RadDatePicker ID="dtpToDateSearch" runat="server" Culture="en-US"
                    meta:resourcekey="dtpToDateSearchResource1">
                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                    </Calendar>
                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                        Width="">
                    </DateInput>
                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                </telerik:RadDatePicker>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtpToDateSearch"
                    Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                    TargetControlID="RequiredFieldValidator1">
                </cc1:ValidatorCalloutExtender>
                <br />
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="dtpFromDateSearch"
                    ControlToValidate="dtpToDateSearch" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                    Operator="GreaterThanEqual" Type="Date" ValidationGroup="btnPrint" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender3"
                    runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                </cc1:ValidatorCalloutExtender>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-2">
                <asp:Button ID="btnGet" runat="server" Text="Get" CssClass="button" ValidationGroup="btnPrint"
                    meta:resourcekey="btnGetResource1" />
            </div>
        </div>
        <div class="row">
            <div class="table-responsive">
                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdVwEmpPermissions"
                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                <telerik:RadGrid ID="dgrdVwEmpPermissions" runat="server" AllowPaging="True" AllowSorting="True"
                    Width="100%" PageSize="15" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                    ShowFooter="True" meta:resourcekey="dgrdVwEmpPermissionsResource1">
                    <GroupingSettings CaseSensitive="False" />
                    <SelectedItemStyle ForeColor="Maroon" />
                    <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="FK_EmployeeId,PermissionRequestId,FK_PermId,IsForPeriod,PermDate,PermEndDate,FK_LeaveId,FromTime,ToTime,IsFullDay,IsFlexible,FlexibilePermissionDuration">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRow" runat="server" Text="&nbsp;" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                Visible="False" meta:resourcekey="GridBoundColumnResource1" UniqueName="EmployeeName" />
                            <telerik:GridBoundColumn DataField="PermName" SortExpression="PermName" HeaderText="Permission Type"
                                UniqueName="PermName" meta:resourcekey="GridBoundColumnResource2" />
                            <telerik:GridBoundColumn DataField="PermDate" SortExpression="PermDate" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderText="From Date" UniqueName="PermDate" meta:resourcekey="GridBoundColumnResource3" />
                            <telerik:GridBoundColumn DataField="PermEndDate" SortExpression="PermEndDate" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderText="To Date" UniqueName="PermEndDate" meta:resourcekey="GridBoundColumnResource4" />
                            <telerik:GridBoundColumn DataField="FromTime" SortExpression="FromTime" DataFormatString="{0:HH:mm}"
                                HeaderText="From Time" UniqueName="FromTime" meta:resourcekey="GridBoundColumnResource5" />
                            <telerik:GridBoundColumn DataField="ToTime" SortExpression="ToTime" DataFormatString="{0:HH:mm}"
                                HeaderText="To Time" UniqueName="ToTime" meta:resourcekey="GridBoundColumnResource6" />
                            <telerik:GridBoundColumn DataField="PermissionId" SortExpression="PermissionId" Visible="False"
                                AllowFiltering="False" UniqueName="PermissionId" meta:resourcekey="GridBoundColumnResource7" />
                            <telerik:GridBoundColumn DataField="FK_LeaveId" SortExpression="FK_LeaveId" Visible="False"
                                AllowFiltering="False" UniqueName="FK_LeaveId" meta:resourcekey="GridBoundColumnResource8" />
                            <telerik:GridBoundColumn DataField="PermissionRequestId" SortExpression="PermissionRequestId" HeaderText="PermissionRequestId"
                                UniqueName="PermissionRequestId" Visible="False" meta:resourcekey="GridBoundColumnResource9" />
                            <telerik:GridBoundColumn DataField="FK_EmployeeId" SortExpression="FK_EmployeeId"
                                HeaderText="EmployeeId" UniqueName="FK_EmployeeId" Visible="False" meta:resourcekey="GridBoundColumnResource10" />
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="IsForPeriod" Visible="False"
                                UniqueName="IsForPeriod" meta:resourcekey="GridBoundColumnResource11" />
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFullDay" UniqueName="IsFullDay"
                                HeaderText="Fully Day" meta:resourcekey="GridBoundColumnResource12" />
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="IsRejected" UniqueName="IsRejected"
                                HeaderText="Status" meta:resourcekey="GridBoundColumnResource15" />
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFlexible" UniqueName="IsFlexible"
                                HeaderText="Flexible" meta:resourcekey="GridBoundColumnResource13" />
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="FlexibilePermissionDuration"
                                Visible="False" UniqueName="FlexibilePermissionDuration" meta:resourcekey="GridBoundColumnResource14" />
                        </Columns>
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
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    </MasterTableView><ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnSave" />
    </Triggers>
</asp:UpdatePanel>
