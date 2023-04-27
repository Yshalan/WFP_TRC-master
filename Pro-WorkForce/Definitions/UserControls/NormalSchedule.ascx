<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NormalSchedule.ascx.vb"
    Inherits="Definitions_UserControls_NormalSchedule" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style>
    .commonPopup {
        left: 10% !important;
    }
</style>
<script type="text/javascript">
    //  var currentCheckBox = e.srcElement || e.target;
    // var inputs = document.getElementsByTagName("chkOffDays");

    //        for (var i = 0; i < inputs.length; i++) {
    //            var input = inputs[i];
    //            if (input.id == currentCheckBox.id)
    //                continue;
    //            if (input.id.indexOf(idFragment) < 0)
    //                continue;
    //            //clear out the rest of the checkboxes 
    //            if (input.type && input.type == "checkbox") {
    //                input.checked = false;
    //            }
    //        }
    //   }
    //    function OnClientClick()
    //    {
    //        var checking = false;
    //         
    //        var inputs = document.getElementsByTagName("input");
    //        for (var i = 0; i < inputs.length; i++) {
    //            if (inputs[i].checked == true) { 
    //                checking = true;
    //                break;
    //            }
    // 
    //        }
    //        if (!checking) {
    //            alert("Atleast one need to be selected");
    //        }
    //    }
</script>
<telerik:RadScriptBlock ID="scriptblock" runat="server">
    <script type="text/javascript">
        function txtValidate(txt) {

            var GridControls = document.getElementById("<%= hdnFromTime1.ClientID %>").value;
            var fromTime1 = GridControls.split(',');
            var hdnFromTime2 = document.getElementById("<%= hdnFromTime2.ClientID %>").value;
            var fromTime2 = hdnFromTime2.split(',');
            var hdnToTime1 = document.getElementById("<%= hdnToTime1.ClientID %>").value;
            var toTime1 = hdnToTime1.split(',');
            var hdnToTime2 = document.getElementById("<%= hdnToTime2.ClientID %>").value;
            var toTime2 = hdnToTime2.split(',');
            var hdnflex1 = document.getElementById("<%= hdnflex1.ClientID %>").value;
            var flex1 = hdnflex1.split(',');
            var hdnflex2 = document.getElementById("<%= hdnflex2.ClientID %>").value;
            var flex2 = hdnflex2.split(',');
            var hdnCheckIsOffDays = document.getElementById("<%= hdnCheckIsOffDays.ClientID %>").value;
            var chk = hdnCheckIsOffDays.split(',');
            var hdnWorkHrs = document.getElementById("<%= hdnWorkHrs.ClientID%>").value;
            var WorkHrs = hdnWorkHrs.split(',');
            var checked = false;

            var strTime = String(txt._projectedValue);

            strTime = strTime.replace(/_/g, "0");
            strTime = strTime.split(/\D/);

            if (strTime[0] == "") { strTime[0] = "00"; }
            if (strTime[1] == "") { strTime[1] = "00"; }
            if (strTime[1] > 59) {
                strTime[1] = "00";
                strTime[0] = String(Number(strTime[0]) + 1);
            }
            if (strTime[0] > 23) {
                strTime[0] = "00";
            }

            txt.set_value(strTime[0] + "" + strTime[1]);

            if (fromTime1[0].trim() == txt._clientID) {
                for (var i = 0; i < fromTime1.length - 1; i++) {
                    checked = false;
                    var control = $find(fromTime1[i].trim());
                    for (var j = 0; j < chk.length - 1; j++) {
                        var chkControl = $find(chk[j].trim());
                        if (chkControl == control) {
                            checked = true;
                            break;
                        }
                    }
                    if (checked == false) {
                        if (control.get_value() == "0000") {
                            control.set_value(strTime[0] + "" + strTime[1]);
                        }
                        else if (document.getElementById("<%= hdnChanged.ClientID %>").value != "True") {
                            control.set_value(strTime[0] + "" + strTime[1]);
                        }
                    }
                }
            }
            else if (fromTime2[0].trim() == txt._clientID) {
                for (var i = 0; i < fromTime2.length - 1; i++) {
                    var control = $find(fromTime2[i].trim());
                    if (control != null) {
                        if (control.get_value() == "0000") {
                            control.set_value(strTime[0] + "" + strTime[1]);
                        }
                        else if (document.getElementById("<%= hdnChanged.ClientID %>").value != "True") {
                            control.set_value(strTime[0] + "" + strTime[1]);
                        }
                    }
                }
            }
            else if (toTime1[0].trim() == txt._clientID) {
                for (var i = 0; i < toTime1.length - 1; i++) {
                    var control = $find(toTime1[i].trim());
                    if (control != null) {
                        if (control.get_value() == "0000") {
                            control.set_value(strTime[0] + "" + strTime[1]);
                        }
                        else if (document.getElementById("<%= hdnChanged.ClientID %>").value != "True") {
                            control.set_value(strTime[0] + "" + strTime[1]);
                        }
                    }
                }
            }
            else if (toTime2[0].trim() == txt._clientID) {
                for (var i = 0; i < toTime2.length - 1; i++) {
                    var control = $find(toTime2[i].trim());
                    if (control != null) {
                        if (control.get_value() == "0000") {
                            control.set_value(strTime[0] + "" + strTime[1]);
                        }
                        else if (document.getElementById("<%= hdnChanged.ClientID %>").value != "True") {
                            control.set_value(strTime[0] + "" + strTime[1]);
                        }
                    }
                }
            }
            else if (WorkHrs[0].trim() == txt._clientID) {
                for (var i = 0; i < WorkHrs.length - 1; i++) {
                    var control = $find(WorkHrs[i].trim());
                    if (control.get_value() == "0000") {
                        control.set_value(strTime[0] + "" + strTime[1]);
                    }
                    else if (document.getElementById("<%= hdnChanged.ClientID %>").value != "True") {
                        control.set_value(strTime[0] + "" + strTime[1]);
                    }
                }
            }
            else if (flex1[0].trim() == txt._clientID) {
                for (var i = 0; i < fromTime1.length - 1; i++) {
                    checked = false;
                    var control = $find(flex1[i].trim());
                    for (var j = 0; j < chk.length - 1; j++) {
                        var chkControl = $find(chk[j].trim());
                        if (chkControl == control) {
                            checked = true;
                            break;
                        }
                    }
                    if (checked == false) {
                        if (control.get_value() == "0000") {
                            control.set_value(strTime[0] + "" + strTime[1]);
                        }
                        else if (document.getElementById("<%= hdnChanged.ClientID %>").value != "True") {
                            control.set_value(strTime[0] + "" + strTime[1]);
                        }
                    }
                }
            }
            else if (flex2[0].trim() == txt._clientID) {
                for (var i = 0; i < flex2.length - 1; i++) {
                    var control = $find(flex2[i].trim());
                    if (control != null) {
                        if (control.get_value() == "0000") {
                            control.set_value(strTime[0] + "" + strTime[1]);
                        }
                        else if (document.getElementById("<%= hdnChanged.ClientID %>").value != "True") {
                            control.set_value(strTime[0] + "" + strTime[1]);
                        }
                    }
                }
            }

            else {
                document.getElementById("<%= hdnChanged.ClientID %>").value = "True";
            }
        }

        function ValidateTextbox(FromTime1, ToTime1, FromTime2, ToTime2, Chk, rmtDuration1, rmtDuration2, flex1, flex2, WorkHrs) {
            ValidateGridRow(FromTime1, ToTime1, FromTime2, ToTime2, Chk, rmtDuration1, rmtDuration2, flex1, flex2, WorkHrs)

            var tmpTime1 = $find(FromTime1);
            var tmpTime2 = $find(ToTime1);
            var tmpTime3 = $find(FromTime2);
            var tmpTime4 = $find(ToTime2);
            var flex1 = $find(flex1);
            var flex2 = $find(flex2);
            var Duration1 = $find(rmtDuration1);
            var Duration2 = $find(rmtDuration2);
            var WorkHrs = $find(WorkHrs);
            var checked = getCheckedRadio();

            if (checked != 3) {
                txtValidate(tmpTime1);
                txtValidate(tmpTime2)
                txtValidate(tmpTime3);
                txtValidate(tmpTime4);
                if (checked == 2) {
                    txtValidate(flex1);
                    txtValidate(flex2);

                }
            }
            else {
                txtValidate(tmpTime1);
                txtValidate(flex1);
                //txtValidate(WorkHrs);
            }
            if (checked != 3) {
                CalcDuration(tmpTime1, tmpTime2, Duration1);
                CalcDuration(tmpTime3, tmpTime4, Duration2);

            }
            ValidateGridRow(FromTime1, ToTime1, FromTime2, ToTime2, Chk, rmtDuration1, rmtDuration2, flex1, flex2, WorkHrs)
        }

        function CalcDuration(tmpTime1, tmpTime2, Duration1) {

            var hdnDuration1 = document.getElementById("<%= hdnDuration1.ClientID %>").value;
            var duration1 = hdnDuration1.split(',');
            var hdnDuration2 = document.getElementById("<%= hdnDuration2.ClientID %>").value;
            var duration2 = hdnDuration2.split(',');

            var t1 = tmpTime1._projectedValue + ":00";
            var t2 = tmpTime2._projectedValue + ":00";

            t1 = t1.split(/\D/);
            t2 = t2.split(/\D/);
            var x1 = Number(t1[0]) * 60 * 60 + Number(t1[1]) * 60 + Number(t1[2]);
            var x2 = Number(t2[0]) * 60 * 60 + Number(t2[1]) * 60 + Number(t2[2]);

            var s = x2 - x1;

            if (s < 0) {
                s = 86400 + s;

            }
            var m = Math.floor(s / 60); s = s % 60;
            var h = Math.floor(m / 60); m = m % 60;
            var d = Math.floor(h / 24); h = h % 24;
            if (h <= 9)
                h = "0" + h;
            if (m <= 9)
                m = "0" + m;

            Duration1.set_value(h + "" + m);

            if (duration1[0].trim() == Duration1._clientID) {
                for (var i = 0; i < duration1.length - 1; i++) {
                    var control = $find(duration1[i].trim());
                    if (control.get_value() == "0000") {
                        control.set_value(h + "" + m);
                    }
                    else if (document.getElementById("<%= hdnChanged.ClientID %>").value != "True") {
                        control.set_value(h + "" + m);
                    }
                }
            }
            else if (duration2[0].trim() == Duration1._clientID) {
                for (var i = 0; i < duration2.length - 1; i++) {
                    var control = $find(duration2[i].trim());
                    if (control.get_value() == "0000") {
                        control.set_value(h + "" + m);
                    }
                    else if (document.getElementById("<%= hdnChanged.ClientID %>").value != "True") {
                        control.set_value(h + "" + m);
                    }
                }
            }
            else {
                document.getElementById("<%= hdnChanged.ClientID %>").value = "True";
            }

        }

        function ValidateGridRow(FromTime1, ToTime1, FromTime2, ToTime2, Chk, rmtDuration1, rmtDuration2, flex1, flex2, WorkHrs) {

            debugger;

            var tmpTime1 = $find(FromTime1);
            var tmpTime2 = $find(ToTime1);
            var tmpTime3 = $find(FromTime2);
            var tmpTime4 = $find(ToTime2);

            var tmpflex1 = $find(flex1);
            var tmpflex2 = $find(flex2);
            var WorkHrs = $find(WorkHrs);

            var Duration1 = $find(rmtDuration1);
            var Duration2 = $find(rmtDuration2);


            if (Chk.checked == true) {
                var checked = getCheckedRadio();

                if (checked != 3) {
                    tmpTime1.set_value("0000");
                    tmpTime1._readOnly = true;

                    tmpTime2.set_value("0000");
                    tmpTime2._readOnly = true;

                    tmpTime3.set_value("0000");
                    tmpTime3._readOnly = true;

                    tmpTime4.set_value("0000");
                    tmpTime4._readOnly = true;



                    if (checked == 2) {
                        tmpflex1.set_value("0000");
                        tmpflex1._readOnly = true;

                        tmpflex2.set_value("0000");
                        tmpflex2._readOnly = true;
                    }

                    Duration1.set_value("0000");
                    Duration1._readOnly = true;

                    Duration2.set_value("0000");
                    Duration2._readOnly = true;



                }
                else {
                    //tmpTime1.set_value("0000");
                    //tmpTime1._readOnly = true;
                    //tmpflex1.set_value("0000");
                    //tmpflex1._readOnly = true;
                    WorkHrs.set_value("0000");
                    WorkHrs._readOnly = true;
                }

            }
            else {


                var checked = getCheckedRadio();
                if (checked == 2) {
                    tmpTime1._readOnly = false;
                    tmpTime2._readOnly = false;
                    tmpTime3._readOnly = false;
                    tmpTime4._readOnly = false;

                    tmpflex1._readonly = false;
                    tmpflex2._readonly = false;

                    Duration1._readOnly = false;
                    Duration2._readOnly = false;
                }
                else if (checked == 3) {

                    //tmpTime1._readOnly = false;
                    //tmpflex1._readOnly = false;
                    WorkHrs._readOnly = false;
                }


            }
        }

        function xValidateTextbox(sender, args) {

            var varRow = sender._textBoxElement.parentNode.parentNode.parentNode;
            var inputs = varRow.getElementsByTagName("input");
            var i = 0;
            var Flag = false;
            for (i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (inputs[i].checked == true) {
                        sender.set_value("0000");
                        Flag = true;
                    }
                }
            }
            if (Flag == false) {
                var strTime = String(sender.get_value());
                var hh = strTime.substring(0, 2);
                var mm = strTime.substring(2, 4);
                if (hh == "") { hh = "00"; }
                if (mm == "") { mm = "00"; }
                if (mm > 59) {
                    mm = "00";
                    hh = String(Number(hh) + 1);
                }
                if (hh > 23) {
                    hh = "00";
                }
                sender.set_value(hh + mm);
            }
        }

        function getCheckedRadio() {

            var radioButtons = document.getElementById("<%= RBLScheduleType.ClientID %>");
            var radioButtonList = radioButtons.getElementsByTagName("input");

            if (radioButtonList[0].checked) {
                return 1;
            }
            else if (radioButtonList[1].checked) {
                return 2;
            }
            else if (radioButtonList[2].checked) {
                return 3;
            }
        }

        function FillAllGridText(GridControls) {

        }

    </script>
</telerik:RadScriptBlock>
<%--<updatepanel>--%>
<div class="row">
    <div class="col-md-2">
        <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text=" English Name"
            meta:resourcekey="Label1Resource1"></asp:Label>
    </div>
    <div class="col-md-4">
        <asp:TextBox ID="txtScheduleEng" runat="server" meta:resourcekey="txtScheduleEngResource1"></asp:TextBox>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtScheduleEng"
            ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{0,50}$" runat="server"
            ErrorMessage="Maximum allowed characters is 50." ValidationGroup="GrSchedule" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="revttxtScheduleEng" Display="Dynamic" ValidationGroup="GrSchedule"
            runat="server" ErrorMessage="Special Characters are not allowed!" ValidationExpression="[^~`!@#$%\^&\*\(\)\-_+=\\\|\}\]\{\['&quot;:?.>,</]+"
            ControlToValidate="txtScheduleEng" meta:resourcekey="revttxtScheduleEngResource1">
        </asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtScheduleEng"
            Display="None" ErrorMessage="Please Enter English Name " ValidationGroup="GrSchedule"
            meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
            TargetControlID="RequiredFieldValidator1">
        </cc1:ValidatorCalloutExtender>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <asp:Label ID="Label2" runat="server" CssClass="Profiletitletxt" Text="Arabic Name"
            meta:resourcekey="Label2Resource1"></asp:Label>
    </div>
    <div class="col-md-4">
        <asp:TextBox ID="txtScheduleAr" runat="server" meta:resourcekey="txtScheduleArResource1"></asp:TextBox>
        <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtScheduleAr"
            ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{0,50}$" runat="server"
            ErrorMessage="Maximum allowed characters is 50." ValidationGroup="GrSchedule" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
        <asp:RegularExpressionValidator ID="revtxtScheduleAr" Display="Dynamic" ValidationGroup="GrSchedule"
            runat="server" ErrorMessage="Special Characters are not allowed!" ValidationExpression="[^~`!@#$%\^&\*\(\)\-_+=\\\|\}\]\{\['&quot;:?.>,</]+"
            ControlToValidate="txtScheduleAr" meta:resourcekey="revttxtScheduleEngResource1">
        </asp:RegularExpressionValidator>
        <asp:HiddenField ID="hdnFromTime1" runat="server" />
        <asp:HiddenField ID="hdnFromTime2" runat="server" />
        <asp:HiddenField ID="hdnToTime1" runat="server" />
        <asp:HiddenField ID="hdnToTime2" runat="server" />
        <asp:HiddenField ID="hdnflex1" runat="server" />
        <asp:HiddenField ID="hdnflex2" runat="server" />
        <asp:HiddenField ID="hdnDuration1" runat="server" />
        <asp:HiddenField ID="hdnDuration2" runat="server" />
        <asp:HiddenField ID="hdnCheckIsOffDays" runat="server" />
        <asp:HiddenField ID="hdnChanged" runat="server" />
        <asp:HiddenField ID="hdnWorkHrs" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtScheduleAr"
            Display="None" ErrorMessage="Please Enter Arabic Name " ValidationGroup="GrSchedule"
            meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
            TargetControlID="RequiredFieldValidator2">
        </cc1:ValidatorCalloutExtender>
    </div>
</div>
<div class="row" id="trGraceIn" runat="server">
    <div class="col-md-2" id="Td1" runat="server">
        <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="Grace In"
            meta:resourcekey="Label4Resource1"></asp:Label>
    </div>
    <div class="col-md-4" id="Td2" runat="server">
        <telerik:RadNumericTextBox ID="txtGraceIn" runat="server" DataType="System.Int64"
            MinValue="0" MaxValue="360" Culture="English (United States)" LabelCssClass="">
            <NumberFormat DecimalDigits="0" GroupSeparator="" />
        </telerik:RadNumericTextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtGraceIn"
            Display="None" ErrorMessage="Please Enter  Grace In Minutes" ValidationGroup="GrSchedule"
            meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
            TargetControlID="RequiredFieldValidator4">
        </cc1:ValidatorCalloutExtender>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <asp:Label ID="lblGraceInGender" runat="server" Text="Grace In Gender" CssClass="Profiletitletxt"
            meta:resourcekey="lblGraceInGenderResource1" />
    </div>
    <div class="col-md-6">
        <asp:RadioButtonList ID="rblGraceInGender" runat="server" RepeatDirection="Horizontal"
            meta:resourcekey="rblGraceInGenderResource1">
            <asp:ListItem Text="All Employees" Selected="True" Value="a" meta:resourcekey="ListItemResource2">
            </asp:ListItem>
            <asp:ListItem Text="Male" Value="m" meta:resourcekey="ListItemResource3">
            </asp:ListItem>
            <asp:ListItem Text="Female" Value="f" meta:resourcekey="ListItemResource4">
            </asp:ListItem>
        </asp:RadioButtonList>
    </div>
</div>
<div class="row" id="trGraceOut" runat="server">
    <div class="col-md-2" id="Td4" runat="server">
        <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="Grace Out"
            meta:resourcekey="Label5Resource1"></asp:Label>
    </div>
    <div class="col-md-4" id="Td5" runat="server">
        <telerik:RadNumericTextBox ID="txtGraceOut" runat="server" DataType="System.Int64"
            MinValue="0" MaxValue="360" Culture="English (United States)" LabelCssClass="">
            <NumberFormat DecimalDigits="0" GroupSeparator="" />
        </telerik:RadNumericTextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtGraceOut"
            Display="None" ErrorMessage="Please Enter  Grace Out Minutes" ValidationGroup="GrSchedule"
            meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True"
            TargetControlID="RequiredFieldValidator5">
        </cc1:ValidatorCalloutExtender>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <asp:Label ID="lblGraceOutGender" runat="server" Text="Grace Out Gender" CssClass="Profiletitletxt"
            meta:resourcekey="lblGraceOutGenderResource1" />
    </div>
    <div class="col-md-6">
        <asp:RadioButtonList ID="rblGraceOutGender" runat="server" RepeatDirection="Horizontal"
            meta:resourcekey="rblGraceOutGenderResource1">
            <asp:ListItem Text="All Employees" Selected="True" Value="a" meta:resourcekey="ListItemResource2">
            </asp:ListItem>
            <asp:ListItem Text="Male" Value="m" meta:resourcekey="ListItemResource3">
            </asp:ListItem>
            <asp:ListItem Text="Female" Value="f" meta:resourcekey="ListItemResource4">
            </asp:ListItem>
        </asp:RadioButtonList>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
        <asp:Label ID="lblScheduletype" runat="server" Text="Schedule Type" CssClass="Profiletitletxt"
            meta:resourcekey="lblScheduletypeResource1">
        </asp:Label>
    </div>
    <div class="col-md-6">
        <asp:RadioButtonList ID="RBLScheduleType" runat="server" AutoPostBack="true">
            <asp:ListItem Value="N" Text="Normal" Selected="True" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
            <asp:ListItem Value="F" Text="Flexible" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
            <asp:ListItem Value="O" Text="Open" meta:resourcekey="ListItem3Resource1"></asp:ListItem>
        </asp:RadioButtonList>
    </div>
</div>
<div class="row">
    <div class="col-md-2">
    </div>
    <div class="col-md-3">
        <asp:CheckBox ID="ChkIsDefault" runat="server" Text="Is Default" meta:resourcekey="lblIsDefaultResource1" />
    </div>
    <div class="col-md-3">
        <asp:CheckBox ID="chkIsActive" runat="server" Text="Is Active" meta:resourcekey="chkIsActiveResource1" />
    </div>
</div>
<div class="row">
    <div class="table-responsive">
        <div style="border: solid 1px #A5B3C5; height: 30px; border-bottom: none;" id="dvScheduleHeader" runat="server">
            <div style="width: 10%; border-right: solid 1px #fff; background-color: #27710a; height: 35px; float: left;">
            </div>
            <div style="border-left: solid 1px #e1eaf3; color: #FFFFFF; padding-top: 5px; font-weight: bold; padding-left: 5px; border-right: solid 1px #fff; background-color: #27710a; height: 35px; float: left; text-align: center; width: 43%;">
                <asp:Label ID="lblShift1" runat="server" meta:resourcekey="lblShift1Resource1"></asp:Label>
                <%-- Shift 1--%>
            </div>
            <div style="border-left: solid 1px #e1eaf3; background-color: #27710a; color: #FFFFFF; padding-top: 5px; font-weight: bold; text-align: center; height: 35px; width: 43%; float: left; padding-left: 5px">
                <asp:Label ID="lblShift2" runat="server" meta:resourcekey="lblShift2Resource1"></asp:Label>
                <%--Shift 2--%>
            </div>
            <div style="border-left: solid 1px #e1eaf3; background-color: #27710a; color: #4C607A; padding-top: 5px; font-weight: bold; text-align: center; height: 35px; width: 4%; float: left; padding-left: 5px">
            </div>
        </div>
        <telerik:RadGrid ID="dgrdWeekTime" runat="server" AllowSorting="True" AllowPaging="True"
            GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True"
            meta:resourcekey="dgrdWeekTimeResource1">
            <SelectedItemStyle ForeColor="Maroon" />
            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="Dayid">
                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                <Columns>
                    <telerik:GridBoundColumn DataField="Dayid" SortExpression="Dayid" Visible="False"
                        meta:resourcekey="GridBoundColumnResource1" UniqueName="Dayid" />
                    <telerik:GridTemplateColumn HeaderText="Work Days" meta:resourcekey="GridTemplateColumnResource1"
                        UniqueName="TemplateColumn">
                        <ItemTemplate>
                            <asp:Label ID="lblDays" runat="server" Text='' meta:resourcekey="lblDaysResource1" />
                            <asp:HiddenField ID="hdnEnDay" runat="server" Value='<%# DataBinder.Eval(Container,"DataItem.Days") %>' />
                            <asp:HiddenField ID="hdnArDay" runat="server" Value='<%# DataBinder.Eval(Container,"DataItem.ArabicDays") %>' />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="From Time1" meta:resourcekey="GridTemplateColumnResource2"
                        UniqueName="TemplateColumn1">
                        <ItemTemplate>
                            <telerik:RadMaskedTextBox ID="rmtFromTime1" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                DisplayMask="##:##" Text='<%# DataBinder.Eval(Container,"DataItem.FromTime1") %>'
                                LabelCssClass="" meta:resourcekey="rmtFromTime1Resource1">
                            </telerik:RadMaskedTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="To Time1" meta:resourcekey="GridTemplateColumnResource3"
                        UniqueName="TemplateColumn2">
                        <ItemTemplate>
                            <telerik:RadMaskedTextBox ID="rmtToTime1" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                Text='<%# DataBinder.Eval(Container,"DataItem.ToTime1") %>' DisplayMask="##:##"
                                LabelCssClass="" meta:resourcekey="rmtToTime1Resource1">
                            </telerik:RadMaskedTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Duration" meta:resourcekey="GridTemplateColumnResource4"
                        UniqueName="TemplateColumn3">
                        <ItemTemplate>
                            <telerik:RadMaskedTextBox ID="rmtDuration1" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                ReadOnly="True" DisplayMask="##:##" LabelCssClass="" meta:resourcekey="rmtDuration1Resource1"
                                Text="0000">
                            </telerik:RadMaskedTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Flex Time 1" UniqueName="Flex1" meta:resourcekey="GridTemplateColumnResource9">
                        <ItemTemplate>
                            <telerik:RadMaskedTextBox ID="Flex1" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                DisplayMask="##:##" LabelCssClass="" Text='<%# DataBinder.Eval(Container,"DataItem.Duration1") %>'>
                            </telerik:RadMaskedTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="From Time2" meta:resourcekey="GridTemplateColumnResource5"
                        UniqueName="TemplateColumn4">
                        <ItemTemplate>
                            <telerik:RadMaskedTextBox ID="rmtFromTime2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                Text='<%# DataBinder.Eval(Container,"DataItem.FromTime2") %>' DisplayMask="##:##"
                                LabelCssClass="" meta:resourcekey="rmtFromTime2Resource1">
                            </telerik:RadMaskedTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="To Time2" meta:resourcekey="GridTemplateColumnResource6"
                        UniqueName="TemplateColumn5">
                        <ItemTemplate>
                            <telerik:RadMaskedTextBox ID="rmtToTime2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                Text='<%# DataBinder.Eval(Container,"DataItem.ToTime2") %>' DisplayMask="##:##"
                                LabelCssClass="" meta:resourcekey="rmtToTime2Resource1">
                            </telerik:RadMaskedTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Duration" meta:resourcekey="GridTemplateColumnResource7"
                        UniqueName="TemplateColumn6">
                        <ItemTemplate>
                            <telerik:RadMaskedTextBox ID="rmtDuration2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                ReadOnly="True" DisplayMask="##:##" LabelCssClass="" meta:resourcekey="rmtDuration2Resource1"
                                Text="0000">
                            </telerik:RadMaskedTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Flex Time 2" UniqueName="Flex2" meta:resourcekey="GridTemplateColumnResource10">
                        <ItemTemplate>
                            <telerik:RadMaskedTextBox ID="Flex2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                ReadOnly="false" DisplayMask="##:##" LabelCssClass="" Text='<%# DataBinder.Eval(Container,"DataItem.Duration2") %>'>
                            </telerik:RadMaskedTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Work Hours" UniqueName="WorkHrs" meta:resourcekey="GridTemplateColumnResource11">
                        <ItemTemplate>
                            <telerik:RadMaskedTextBox ID="radWorkHrs" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                DisplayMask="##:##" Text='<%# DataBinder.Eval(Container, "DataItem.WorkHrs")%>'
                                LabelCssClass="" meta:resourcekey="radWorkHrsResource1">
                            </telerik:RadMaskedTextBox>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn HeaderText="Is Off Day" meta:resourcekey="GridTemplateColumnResource8"
                        UniqueName="TemplateColumn7">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkOffDays" runat="server" CssClass="Checkbox" Checked='<%# DataBinder.Eval(Container,"DataItem.IsOffDay") %>'
                                Text="&nbsp;" />
                        </ItemTemplate>
                    </telerik:GridTemplateColumn>
                </Columns>
                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
            </MasterTableView>
            <GroupingSettings CaseSensitive="False" />
            <ClientSettings EnablePostBackOnRowClick="False" EnableRowHoverStyle="True">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </div>
</div>

<div class="row">
    <div class="col-md-12 text-center">
        <asp:Button ID="btnAddRamadan" runat="server" Text="Add Ramadan Schedule" CssClass="button"
            Visible="false" meta:resourcekey="btnAddRamadanResource" />
        <asp:HiddenField runat="server" ID="hdnWindow" />
        <cc1:ModalPopupExtender ID="mpeRamadanPopup" runat="server" BehaviorID="modelPopupExtender1"
            TargetControlID="hdnWindow" PopupControlID="divAddRamadan" RepositionMode="RepositionOnWindowResize"
            CancelControlID="btnCancel" Enabled="true" BackgroundCssClass="modalBackground" />
    </div>
</div>
<div id="divAddRamadan" class="commonPopup" style="display: none; left: 10% !important;">
    <div class="row" id="trRamadanGraceIn" runat="server">
        <div class="col-md-4" id="RamadanTd1" runat="server">
            <asp:Label ID="Label4Ramadan" runat="server" CssClass="Profiletitletxt" Text="Grace In"
                meta:resourcekey="Label4Resource1"></asp:Label>
        </div>
        <div class="col-md-6" id="RamadanTd2" runat="server">
            <telerik:RadNumericTextBox ID="txtRamadanGraceIn" runat="server" DataType="System.Int64"
                Culture="English (United States)" LabelCssClass="" MaxValue="360" MinValue="0">
                <NumberFormat DecimalDigits="0" GroupSeparator="" />
            </telerik:RadNumericTextBox>
            <div class="col-md-2" id="RamadanTd3" runat="server">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4Ramadan" runat="server" ControlToValidate="txtRamadanGraceIn"
                    Display="None" ErrorMessage="Please Enter  Grace In Minutes" ValidationGroup="GrRamadanSchedule"
                    meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4Ramadan" runat="server"
                    Enabled="True" TargetControlID="RequiredFieldValidator4">
                </cc1:ValidatorCalloutExtender>
            </div>
        </div>
    </div>
    <div class="row" id="trRamadanGraceOut" runat="server">
        <div class="col-md-4" id="RamadanTd4" runat="server">
            <asp:Label ID="Label5Ramadan" runat="server" CssClass="Profiletitletxt" Text="Grace Out"
                meta:resourcekey="Label5Resource1"></asp:Label>
        </div>
        <div class="col-md-6" id="RamadanTd5" runat="server">
            <telerik:RadNumericTextBox ID="txtRamadanGraceOut" runat="server" DataType="System.Int64"
                Culture="English (United States)" LabelCssClass="" MaxValue="360" MinValue="0">
                <NumberFormat DecimalDigits="0" GroupSeparator="" />
            </telerik:RadNumericTextBox>
        </div>
        <div class="col-md-2" id="RamadanTd6" runat="server">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5Ramadan" runat="server" ControlToValidate="txtRamadanGraceOut"
                Display="None" ErrorMessage="Please Enter  Grace Out Minutes" ValidationGroup="GrRamadanSchedule"
                meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5Ramadan" runat="server"
                Enabled="True" TargetControlID="RequiredFieldValidator5">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 ">
            <div style="border: solid 1px #A5B3C5; height: 30px; border-bottom: none;" id="dvRamadanHeader" runat="server">
                <div style="width: 10%; border-right: solid 1px #fff; background-color: #27710a; height: 30px; float: left;">
                </div>
                <div style="border-left: solid 1px #e1eaf3; color: #FFFFFF; padding-top: 5px; font-weight: bold; padding-left: 5px; border-right: solid 1px #fff; background-color: #27710a; height: 25px; float: left; text-align: center; width: 43%">
                    Shift 1
                </div>
                <div style="border-left: solid 1px #e1eaf3; background-color: #27710a; color: #FFFFFF; padding-top: 5px; font-weight: bold; text-align: center; height: 25px; width: 43%; float: left; padding-left: 5px">
                    Shift 2
                </div>
                <div style="border-left: solid 1px #e1eaf3; background-color: #27710a; color: #4C607A; padding-top: 5px; font-weight: bold; text-align: center; height: 25px; width: 4%; float: left; padding-left: 5px">
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 table-responsive ">
            <telerik:RadGrid ID="dgrdRamadanTime" runat="server" AllowSorting="True" AllowPaging="True"
                GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="false" ShowFooter="True"
                hi meta:resourcekey="dgrdWeekTimeResource1">
                <SelectedItemStyle ForeColor="Maroon" />
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="false"
                    Selecting-AllowRowSelect="false">
                </ClientSettings>
                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="Dayid" ClientIDMode="Static">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="Dayid" SortExpression="Dayid" Visible="False"
                            meta:resourcekey="GridBoundColumnResource1" UniqueName="Dayid" />
                        <telerik:GridTemplateColumn HeaderText="Work Days" meta:resourcekey="GridTemplateColumnResource1"
                            UniqueName="TemplateColumn">
                            <ItemTemplate>
                                <asp:Label ID="lblDays" runat="server" Text='' meta:resourcekey="lblDaysResource1" />
                                <asp:HiddenField ID="hdnEnDay" runat="server" Value='<%# DataBinder.Eval(Container,"DataItem.Days") %>' />
                                <asp:HiddenField ID="hdnArDay" runat="server" Value='<%# DataBinder.Eval(Container,"DataItem.ArabicDays") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="From Time1" meta:resourcekey="GridTemplateColumnResource2"
                            UniqueName="TemplateColumn1">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtFromTime1" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    DisplayMask="##:##" Text='<%# DataBinder.Eval(Container,"DataItem.FromTime1") %>'
                                    LabelCssClass="" meta:resourcekey="rmtFromTime1Resource1">
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="To Time1" meta:resourcekey="GridTemplateColumnResource3"
                            UniqueName="TemplateColumn2">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtToTime1" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    Text='<%# DataBinder.Eval(Container,"DataItem.ToTime1") %>' DisplayMask="##:##"
                                    LabelCssClass="" meta:resourcekey="rmtToTime1Resource1">
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Duration" meta:resourcekey="GridTemplateColumnResource4"
                            UniqueName="TemplateColumn3">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtDuration1" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    ReadOnly="True" DisplayMask="##:##" LabelCssClass="" meta:resourcekey="rmtDuration1Resource1"
                                    Text="0000">
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Flex Time 1" UniqueName="Flex1" meta:resourcekey="GridTemplateColumnResource9">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="Flex1" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    DisplayMask="##:##" LabelCssClass="" Text='<%# DataBinder.Eval(Container,"DataItem.Duration1") %>'>
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="From Time2" meta:resourcekey="GridTemplateColumnResource5"
                            UniqueName="TemplateColumn4">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtFromTime2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    Text='<%# DataBinder.Eval(Container,"DataItem.FromTime2") %>' DisplayMask="##:##"
                                    LabelCssClass="" meta:resourcekey="rmtFromTime2Resource1">
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="To Time2" meta:resourcekey="GridTemplateColumnResource6"
                            UniqueName="TemplateColumn5">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtToTime2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    Text='<%# DataBinder.Eval(Container,"DataItem.ToTime2") %>' DisplayMask="##:##"
                                    LabelCssClass="" meta:resourcekey="rmtToTime2Resource1">
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Duration" meta:resourcekey="GridTemplateColumnResource7"
                            UniqueName="TemplateColumn6">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtDuration2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    ReadOnly="True" DisplayMask="##:##" LabelCssClass="" meta:resourcekey="rmtDuration2Resource1"
                                    Text="0000">
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Flex Time 2" UniqueName="Flex2" meta:resourcekey="GridTemplateColumnResource10">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="Flex2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    ReadOnly="false" DisplayMask="##:##" LabelCssClass="" Text='<%# DataBinder.Eval(Container,"DataItem.Duration2") %>'>
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Work Hours" UniqueName="WorkHrs" meta:resourcekey="GridTemplateColumnResource11">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="radWorkHrs" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    DisplayMask="##:##" Text='<%# DataBinder.Eval(Container, "DataItem.WorkHrs")%>'
                                    LabelCssClass="" meta:resourcekey="radWorkHrsResource1">
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Is Off Day" meta:resourcekey="GridTemplateColumnResource8"
                            UniqueName="TemplateColumn7">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkOffDays" ClientIDMode="AutoID" runat="server" CssClass="Checkbox" Checked='<%# DataBinder.Eval(Container,"DataItem.IsOffDay") %>'
                                    Text="&nbsp;" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                </MasterTableView>
                <GroupingSettings CaseSensitive="False" />
                <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 text-responsive">
            <asp:Button ID="btnSaveRamadan" runat="server" Text="Save" CssClass="button" ValidationGroup="GrRamadanSchedule" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" />
        </div>
    </div>
</div>
<%--    </updatepanel>--%>
