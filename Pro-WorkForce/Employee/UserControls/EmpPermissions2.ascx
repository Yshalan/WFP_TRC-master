<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EmpPermissions2.ascx.vb"
    Inherits="Emp_userControls_EmpPermissions2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

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
        <div id="trPermType" runat="server" class="row">
            <div class="col-md-2">
                <asp:Label CssClass="Profiletitletxt" ID="lblPermission" runat="server" Text="Type"
                    meta:resourcekey="lblPermissionResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadComboBox ID="RadCmpPermissions" MarkFirstMatch="True" AutoPostBack="true"
                    Skin="Vista" ToolTip="View types of employee permission" runat="server" meta:resourcekey="RadCmpPermissionsResource1">
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="reqPermission" runat="server" ControlToValidate="RadCmpPermissions"
                    Display="None" ErrorMessage="Please select permission english name" InitialValue="--Please Select--"
                    ValidationGroup="EmpPermissionGroup" meta:resourcekey="reqPermissionResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ExtenderPermission" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="reqPermission" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                </cc1:ValidatorCalloutExtender>
            </div>
        </div>
        <div class="row" id="trType" runat="server">
            <div class="col-md-2"></div>
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
                        Skin="Vista" runat="server" Culture="English (United States)" meta:resourcekey="dtpPermissionDateResource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput ID="DateInput3" DateFormat="dd/MM/yyyy" ToolTip="View permission date"
                            runat="server">
                        </DateInput>
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
            <div id="trWeekDays" runat="server" class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblDaysList" runat="server" Text="Week Days" meta:resourcekey="lblDaysListResource1"
                        CssClass="Profiletitletxt" />
                </div>
                <div class="col-md-6" id="tdWeekDays" runat="server">
                    <asp:CheckBoxList ID="chkWeekDays" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblDateFrom" runat="server" Text="Date"
                        meta:resourcekey="lblDateFromResource1"></asp:Label>
                </div>
                <div class="col-md-10" id="trDateFromTo" runat="server">
                    <div class="col-md-1">
                        <asp:Label CssClass="Profiletitletxt" ID="lblFromDate" runat="server" Text="From"
                            meta:resourcekey="lblFromDateResource1"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <telerik:RadDatePicker ID="dtpStartDatePerm" ToolTip="Click" AllowCustomText="false"
                            MarkFirstMatch="true" Skin="Vista" runat="server" Culture="English (United States)"
                            meta:resourcekey="dtpStartDatePermResource1">
                            <ClientEvents OnDateSelected="DateSelected" />
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput ID="DateInput1" DateFormat="dd/MM/yyyy" ToolTip="View start date permission"
                                runat="server">
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
                            Skin="Vista" runat="server" AutoPostBack="True" Culture="English (United States)"
                            meta:resourcekey="dtpEndDatePermResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput ID="DateInput2" DateFormat="dd/MM/yyyy" ToolTip="View end date permission"
                                runat="server">
                            </DateInput>
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
        <div id="trNursingFlexibleDurationPermission" runat="server" visible="false" class="row">
            <div class="col-md-2">
                <asp:Label ID="lblNursingFlexibleDuration" runat="server" CssClass="Profiletitletxt"
                    Text="Flexible Permission Duration" meta:resourcekey="lblNursingFlexibleDurationResource1" />
            </div>
            <div id="tdNursingFlexibleDurationPermission" runat="server" class="col-md-4">
                <telerik:RadComboBox ID="RadCmbFlixebleDuration" MarkFirstMatch="True" Skin="Vista"
                    runat="server">
                    <Items>
                        <telerik:RadComboBoxItem Text="One Hour" Value="60" meta:resourcekey="RadCmbFlixebleDurationItemResource2" />
                        <telerik:RadComboBoxItem Text="Two Hours" Value="120" meta:resourcekey="RadCmbFlixebleDurationItemResource3" />
                    </Items>
                </telerik:RadComboBox>
            </div>
        </div>
        <div class="row" id="trFullyDay" runat="server">
            <div class="col-md-2">
            </div>
            <div class="col-md-4">
                <asp:CheckBox ID="chckFullDay" runat="server" AutoPostBack="true" Text="Is Fully Day" meta:resourcekey="chckFullDayResource1" />
            </div>
        </div>
        <div id="trTime" runat="server" class="row">
            <div class="col-md-2">
                <asp:Label CssClass="Profiletitletxt" ID="lblTimeFrom" runat="server" Text="Time"
                    meta:resourcekey="lblTimeFromResource1"></asp:Label>
            </div>
            <div id="trTimeFromTo" runat="server" class="col-md-4">
                <asp:RadioButtonList ID="rdlTimeOption" runat="server" RepeatDirection="Horizontal"
                    OnSelectedIndexChanged="rdlTimeOption_OnSelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="Specific Time" Value="0" Selected="True" meta:resourcekey="ListItemResource1" />
                    <asp:ListItem Text="Flexible Time" Value="1" meta:resourcekey="ListItemResource2" />
                </asp:RadioButtonList>
            </div>
        </div>
            <div id="trFlixibleTime" runat="server" class="row">
                <div class="col-md-2"></div>
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
            <div class="row" id="trSpecificTime" runat="server">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblFrom" Text="From" runat="server" meta:resourcekey="lblFromResource1"></asp:Label>
                </div>
                <div class="col-md-4">
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
                <div class="col-md-10"></div>
                <div class="col-md-2"></div>
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblTo" runat="server" Text="To" meta:resourcekey="lblToResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadTimePicker ID="RadTPtoTime" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                        Skin="Vista" AutoPostBack="True" AutoPostBackControl="TimeView" meta:resourcekey="RadTPtoTimeResource1">
                        <DateInput runat="server" ToolTip="View end time" DateFormat="HH:mm" />
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
            <div id="trDifTime" runat="server" class="row">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblPeriodInterval" runat="server" Text="Period"
                        meta:resourcekey="lblPeriodIntervalResource1"></asp:Label>
                </div>
                <div id="trDif" runat="server" class="col-md-4">
                    <asp:TextBox ID="txtTimeDifference" ReadOnly="True" runat="server" meta:resourcekey="txtTimeDifferenceResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblStudyYear" Visible="false" runat="server" Text="Year"
                        meta:resourcekey="lblYearResource1"></asp:Label>
                </div>
                <div class="col-md-4">

                    <%-- <asp:TextBox ID="txtStudyYear" Visible="false" runat ="server"></asp:TextBox>--%>
                    <telerik:RadNumericTextBox ID="txtStudyYear" Visible="false" MinValue="0" MaxValue="99999" Skin="Vista"
                        runat="server" Culture="en-US" LabelCssClass="" Width="158px" meta:resourcekey="txtStudyYearResource1">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                    </telerik:RadNumericTextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblSemester" Visible="false" runat="server" Text="Semester"
                        meta:resourcekey="lblSemesterResource1"></asp:Label>
                </div>
                <div class="col-md-4">

                    <asp:TextBox ID="txtSemester" Visible="false" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
                        meta:resourcekey="lblAttachFileResource1" />
                </div>
                <div id="trAttachedFile" class="col-md-4" runat="server">
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
                        <asp:Label CssClass="Profiletitletxt" ID="lblIsSpecifiedDays" runat="server" Text="Specified days"
                            meta:resourcekey="lblIsSpecifiedDaysResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chckSpecifiedDays" runat="server" meta:resourcekey="chckSpecifiedDaysResource1" />
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
                        <asp:Label CssClass="Profiletitletxt" ID="lblIsFlexible" runat="server" Text="Flexible"
                            meta:resourcekey="lblIsFlexibleResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chckIsFlexible" runat="server" meta:resourcekey="chckIsFlexibleResource1" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblIsDividable" runat="server" Text="Dividable"
                            meta:resourcekey="lblIsDividableResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chckIsDividable" runat="server" meta:resourcekey="chckIsDividableResource1" />
                    </div>
                </div>
            </asp:Panel>
            <div class="row">
                <div class="col-md-8" colspan="2" align="center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="EmpPermissionGroup"
                        meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" CausesValidation="false"
                        meta:resourcekey="btnDeleteResource1" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" CausesValidation="False"
                        meta:resourcekey="btnClearResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblFromDateSearch" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblFromDateSearchResource1"
                        Text="From Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpFromDateSearch" runat="server" Culture="English (United States)"
                        meta:resourcekey="RadDatePicker1Resource1" Width="180px">
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
                    <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblToDateResource1"
                        Text="To Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpToDateSearch" runat="server" Culture="English (United States)"
                        meta:resourcekey="RadDatePicker2Resource1"  >
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
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="dtpFromDateSearch"
                        ControlToValidate="dtpToDateSearch" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="btnPrint" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender3"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnGet" runat="server" Text="Get" CssClass="button" ValidationGroup="btnPrint"
                        meta:resourcekey="btnGetResource1" />
                </div>
            </div>
                                        <div class="row">
                                            <div class="table-responsive">
                                                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdVwEmpPermissions"
                                                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                                                <telerik:RadGrid ID="dgrdVwEmpPermissions" runat="server" AllowPaging="True" AllowSorting="True"
                                                    Width="100%" PageSize="15"  GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                                    ShowFooter="True" meta:resourcekey="dgrdVwEmpPermissionsResource1">
                                                    <GroupingSettings CaseSensitive="False" />
                                                    <SelectedItemStyle ForeColor="Maroon" />
                                                    <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="PermissionId,FK_PermId,FK_LeaveId,FromTime,ToTime,PermDate,PermEndDate,IsFullDay,IsFlexible,FlexibilePermissionDuration,IsForPeriod,FK_EmployeeId">
                                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                        <Columns>
                                                            <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkRow" runat="server" Text="&nbsp;" />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                                                meta:resourcekey="GridBoundColumnResource1" Visible="false" />
                                                            <telerik:GridBoundColumn DataField="PermName" SortExpression="PermName" HeaderText="Permission Type"
                                                                UniqueName="PermName" meta:resourcekey="GridBoundColumnResource11" />
                                                            <telerik:GridBoundColumn DataField="PermDate" SortExpression="PermDate" DataFormatString="{0:dd/MM/yyyy}"
                                                                HeaderText="From Date" UniqueName="PermDate" meta:resourcekey="GridBoundColumnResource10" />
                                                            <telerik:GridBoundColumn DataField="PermEndDate" SortExpression="PermEndDate" DataFormatString="{0:dd/MM/yyyy}"
                                                                HeaderText="To Date" UniqueName="PermEndDate" meta:resourcekey="GridBoundColumnResource15" />
                                                            <telerik:GridBoundColumn DataField="FromTime" SortExpression="FromTime" DataFormatString="{0:HH:mm}"
                                                                HeaderText="From Time" UniqueName="FromTime" meta:resourcekey="GridBoundColumnResource13" />
                                                            <telerik:GridBoundColumn DataField="ToTime" SortExpression="ToTime" DataFormatString="{0:HH:mm}"
                                                                HeaderText="To Time" UniqueName="ToTime" meta:resourcekey="GridBoundColumnResource12" />
                                                            <telerik:GridBoundColumn DataField="PermissionId" SortExpression="PermissionId" Visible="False"
                                                                AllowFiltering="False" UniqueName="PermissionId" />
                                                            <telerik:GridBoundColumn DataField="FK_LeaveId" SortExpression="FK_LeaveId" Visible="False"
                                                                AllowFiltering="False" UniqueName="FK_LeaveId" />
                                                            <telerik:GridBoundColumn DataField="FK_PermId" SortExpression="FK_PermId" HeaderText="Perm Id"
                                                                UniqueName="FK_PermId" Visible="False" />
                                                            <telerik:GridBoundColumn DataField="FK_EmployeeId" SortExpression="FK_EmployeeId"
                                                                HeaderText="EmployeeId" UniqueName="FK_EmployeeId" Visible="False" />
                                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="IsForPeriod" Visible="False"
                                                                UniqueName="IsForPeriod" />
                                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFullDay" UniqueName="IsFullDay"
                                                                HeaderText="Fully Day" meta:resourcekey="GridBoundColumnResource16" />
                                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFlexible" UniqueName="IsFlexible"
                                                                HeaderText="Flexible" meta:resourcekey="GridBoundColumnResource17" />
                                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="FlexibilePermissionDuration"
                                                                Visible="False" UniqueName="FlexibilePermissionDuration" />
                                                                
                                                                 <telerik:GridBoundColumn DataField="Status" SortExpression="Status" HeaderText="Status"
                                                                UniqueName="Status" meta:resourcekey="GridBoundColumnResource18" />
                                                              <telerik:GridBoundColumn DataField="StatusAr" SortExpression="StatusAr" HeaderText="StatusAr"
                                                                UniqueName="StatusAr" meta:resourcekey="GridBoundColumnResource19" />
                                                                
                                                                
                                                        </Columns>
                                                        <CommandItemTemplate>
                                                            <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                                                Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                                                <Items>
                                                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                                        ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
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
             


