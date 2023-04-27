<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="PermissionForMultipleEmployees.aspx.vb" Inherits="Employee_PermissionForMultipleEmployees"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<%@ Register Src="../Admin/UserControls/MultiEmployeeFilter.ascx" TagName="MultiEmployeeFilter"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function CheckBoxListSelect(state) {
            var chkBoxList = document.getElementById("<%= cblEmpList.ClientID %>");
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }

        function validatecheckboxlist(sender, args) {
            var chkBoxList = document.getElementById("<%= cblEmpList.ClientID %>");
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            var check = false;
            for (var i = 0; i < chkBoxCount.length; i++) {
                if (chkBoxCount[i].checked == true) {
                    check = true;
                }
            }

            if (check == false)
                args.IsValid = false;
            else
                args.IsValid = true;
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="updateprogressAssign">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" EnableViewState="false" DisplayAfter="0">
            <ProgressTemplate>
                <div id="tblLoading" style="width: 100%; height: 100%; z-index: 100002 !important; text-align: center; position: fixed; left: 0px; top: 0px; background-size: cover; background-image: url('../images/Grey_fade.png');">

                    <div class="animategif">
                        <div align="center">
                            <%--background-image: url('../images/STS_Loading.gif');--%>
                            <asp:Image ID="imgLoading" runat="server" ImageAlign="AbsBottom" ImageUrl="~/images/STS_Loading.gif" Width="250px" Height="250px" />
                        </div>
                    </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <asp:UpdatePanel ID="pnlMultiPerm" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlEmpInfo" runat="server" GroupingText="Employees Information" meta:resourcekey="pnlEmpInfoResource1">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" Text="Company"
                            meta:resourcekey="lblCompanyResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadComboBox ID="RadCmbBxCompanies" AutoPostBack="true" AllowCustomText="false"
                            CausesValidation="false" MarkFirstMatch="true" Skin="Vista" runat="server" ValidationGroup="EmpPermissionGroup">
                        </telerik:RadComboBox>

                        <asp:RequiredFieldValidator ID="rfvCompanies" runat="server" ControlToValidate="RadCmbBxCompanies"
                            Display="None" ErrorMessage="Please Select Company" ValidationGroup="ValidateComp"
                            meta:resourcekey="rfvCompaniesResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="vceCompanies" runat="server" CssClass="AISCustomCalloutStyle"
                            TargetControlID="rfvCompanies" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <%-- <uc2:PageFilter ID="EmployeeFilterUC" runat="server" OneventCompanySelect="CompanyChanged"
                            OneventEntitySelected="FillEmployee" ValidationGroup="EmpPermissionGroup" />--%>
                        <uc4:MultiEmployeeFilter ID="MultiEmployeeFilterUC" runat="server" OneventEntitySelected="EntityChanged"
                            OneventWorkGroupSelect="WorkGroupChanged" OneventWorkLocationsSelected="WorkLocationsChanged" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblListOfEmployee" runat="server" CssClass="Profiletitletxt" Text="List Of Employees"
                            meta:resourcekey="lblListOfEmployeeResource1"></asp:Label>
                    </div>

                    <div class="col-md-4">
                        <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
                            <asp:CheckBoxList ID="cblEmpList" runat="server" Style="height: 26px" DataTextField="EmployeeName"
                                DataValueField="EmployeeId" meta:resourcekey="cblEmpListResource1">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                            <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>

                        <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                            <asp:Literal ID="Literal2" runat="server" Text="UnSelect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-10">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<% # Eval("PageNo")%>'></asp:LinkButton>|
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <br />
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlPermInfo" runat="server" GroupingText="Permission Details" meta:resourcekey="pnlPermInfoResource1">
                <div class="row" id="trPermType" runat="server">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblPermission" runat="server" Text="Type"
                            meta:resourcekey="lblPermissionResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadComboBox ID="RadCmpPermissions" MarkFirstMatch="True" Skin="Vista" ToolTip="View types of employee permission"
                            runat="server" meta:resourcekey="RadCmpPermissionsResource1">
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="reqPermission" runat="server" ControlToValidate="RadCmpPermissions"
                            Display="None" ErrorMessage="Please Select Permission English Name" InitialValue="-1"
                            ValidationGroup="EmpPermissionGroup" meta:resourcekey="reqPermissionResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ExtenderPermission" runat="server" CssClass="AISCustomCalloutStyle"
                            TargetControlID="reqPermission" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="row" id="trType" runat="server">
                    <div class="col-md-8">
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
                        <div class="col-md-2">
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
                        <div class="col-md-2">
                            <asp:Label ID="lblDaysList" runat="server" Text="Week Days" meta:resourcekey="lblDaysListResource1" />
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
                        <div class="col-md-8" id="trDateFromTo" runat="server">
                            <div class="col-md-1">
                                <asp:Label CssClass="Profiletitletxt" ID="lblFromDate" runat="server" Text="From"
                                    meta:resourcekey="lblFromDateResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
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
                            <div class="col-md-1">
                                <asp:Label CssClass="Profiletitletxt" ID="lblEndDate" runat="server" Text="To" meta:resourcekey="lblEndDateResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
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
                <div class="row" id="trFullyDay" runat="server">
                    <div class="col-md-2">
                        <asp:Label ID="lblIsFullyDay" runat="server" Text="Is Fully Day" CssClass="Profiletitletxt"
                            meta:resourcekey="chckFullDayResource1" />
                    </div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chckFullDay" Text="&nbsp;" runat="server" AutoPostBack="True" />
                    </div>
                </div>
                <div class="row" id="trTime" runat="server">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblTimeFrom" runat="server" Text="Time"
                            meta:resourcekey="lblTimeFromResource1"></asp:Label>
                    </div>
                    <div class="col-md-4" id="trTimeFromTo" runat="server">

                        <asp:RadioButtonList ID="rdlTimeOption" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="True" OnSelectedIndexChanged="rdlTimeOption_OnSelectedIndexChanged">
                            <asp:ListItem Text="Specific Time" Value="0" Selected="True" meta:resourcekey="ListItemResource1" />
                            <asp:ListItem Text="Flixible Time" Value="1" meta:resourcekey="ListItemResource2" />
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row" id="trFlixibleTime" runat="server">
                    <div class="col-md-2"></div>
                    <div class="col-md-4">
                        <telerik:RadMaskedTextBox ID="rmtFlexibileTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                            DisplayMask="##:##" Text='0000' LabelCssClass="">
                            <ClientEvents OnBlur="ValidateTextboxFrom" />
                        </telerik:RadMaskedTextBox>
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
                <br />
                <div class="row" id="trDifTime" runat="server">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblPeriodInterval" runat="server" Text="Period"
                            meta:resourcekey="lblPeriodIntervalResource1"></asp:Label>
                    </div>
                    <div class="col-md-4" id="trDif" runat="server">
                        <asp:TextBox ID="txtTimeDifference" ReadOnly="True" runat="server" meta:resourcekey="txtTimeDifferenceResource1"></asp:TextBox>
                    </div>
                </div>
                <div class="row" id="trAttachedFile" runat="server" visible="False">
                    <div class="col-md-2">
                        <asp:Label ID="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
                            meta:resourcekey="lblAttachFileResource1" />
                    </div>
                    <div class="col-md-10">
                        <div id="Div1" runat="server" class="col-md-4">
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
                            <asp:CheckBox ID="chckSpecifiedDays" runat="server" Text="&nbsp;" />
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
                            <asp:CheckBox ID="chckIsFlexible" runat="server" Text="&nbsp;" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblIsDividable" runat="server" Text="Dividable"
                                meta:resourcekey="lblIsDividableResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chckIsDividable" runat="server" Text="&nbsp;" />
                        </div>
                    </div>
                </asp:Panel>
            </asp:Panel>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="EmpPermissionGroup"
                        meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" CausesValidation="False"
                        meta:resourcekey="btnClearResource1" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
