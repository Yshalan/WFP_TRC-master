<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FlexibleSchedule.ascx.vb"
    Inherits="Definitions_UserControls_FlexibleSchedule" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<telerik:RadScriptBlock ID="scriptblock" runat="server">
    <script type="text/javascript">

        function x(FromTime1, ToTime1, FromTime2, ToTime2, Duration1, FromTime3, ToTime3, FromTime4, ToTime4, chk, Duration2) {
            debugger;

            var tmpTime1 = $find(FromTime1);
            tmpTime1.set_value("0000");
            var tmpTime2 = $find(ToTime1);
            tmpTime2.set_value("0000");
            var tmpTime3 = $find(FromTime2);
            tmpTime3.set_value("0000");
            var tmpTime4 = $find(ToTime2);
            tmpTime4.set_value("0000");
            var tmpTime5 = $find(Duration1);
            tmpTime5.set_value("0000");
            var tmpTime11 = $find(FromTime3);
            tmpTime11.set_value("0000");
            var tmpTime21 = $find(ToTime3);
            tmpTime21.set_value("0000");
            var tmpTime31 = $find(FromTime4);
            tmpTime31.set_value("0000");
            var tmpTime41 = $find(ToTime4);
            tmpTime41.set_value("0000");
            var tmpTime51 = $find(Duration2);
            tmpTime51.set_value("0000");

            if (chk.checked == true) {
                tmpTime1._readOnly = true;

                tmpTime2._readOnly = true;

                tmpTime3._readOnly = true;

                tmpTime4._readOnly = true;

                tmpTime5._readOnly = true;

                tmpTime11._readOnly = true;

                tmpTime21._readOnly = true;

                tmpTime31._readOnly = true;

                tmpTime41._readOnly = true;

                tmpTime51._readOnly = true;
            }
            else {
                tmpTime1._readOnly = false;

                tmpTime2._readOnly = false;

                tmpTime3._readOnly = false;

                tmpTime4._readOnly = false;

                tmpTime5._readOnly = false;

                tmpTime11._readOnly = false;

                tmpTime21._readOnly = false;

                tmpTime31._readOnly = false;

                tmpTime41._readOnly = false;

                tmpTime51._readOnly = false;
            }
        }

        function FlexValidateGridRow(FromTime1, ToTime1, FromTime2, ToTime2, Duration1) {

            var tmpTime1 = $find(FromTime1);
            tmpTime1.set_value("0000");
            var tmpTime2 = $find(ToTime1);
            tmpTime2.set_value("0000");
            var tmpTime3 = $find(FromTime2);
            tmpTime3.set_value("0000");
            var tmpTime4 = $find(ToTime2);
            tmpTime4.set_value("0000");
            var tmpTime5 = $find(Duration1);
            tmpTime5.set_value("0000");
        }

        function FlexValidateGridRowTest(FromTime1, ToTime1, FromTime2, ToTime2, Duration1, chk) {
            var hdnToTime1 = document.getElementById("<%= hdnToTime1.ClientID %>").value;
            var toTime1 = hdnToTime1.split(',');

            var hdnToTime2 = document.getElementById("<%= hdnToTime2.ClientID %>").value;
            var toTime2 = hdnToTime2.split(',');

            var hdnToTime3 = document.getElementById("<%= hdnToTime3.ClientID %>").value;
            var toTime3 = hdnToTime3.split(',');

            var hdnToTime4 = document.getElementById("<%= hdnToTime4.ClientID %>").value;
            var toTime4 = hdnToTime4.split(',');

            var Flag = false;

            var tmpTime1 = $find(FromTime1);
            var tmpTime2 = $find(ToTime1);
            var tmpTime3 = $find(FromTime2);
            var tmpTime4 = $find(ToTime2);
            var tmpTime5 = $find(Duration1);

            var tmpChk = document.getElementById(chk);
            if (tmpChk.checked == true) {
                tmpTime1.set_value("0000");
                tmpTime2.set_value("0000");
                tmpTime3.set_value("0000");
                tmpTime4.set_value("0000");
                tmpTime5.set_value("0000");
            }
            else {
                FlexValidateTextbox(tmpTime1);
                FlexValidateTextbox(tmpTime2);
                FlexValidateTextbox(tmpTime3);
                FlexValidateTextbox(tmpTime4);
                FlexValidateTextbox(tmpTime5);

                var t1 = tmpTime1._projectedValue + ":00";
                var t2 = tmpTime5._projectedValue + ":00";

                t1 = t1.split(/\D/);
                t2 = t2.split(/\D/);
                var x1 = Number(t1[0]) * 60 * 60 + Number(t1[1]) * 60 + Number(t1[2]);
                var x2 = Number(t2[0]) * 60 * 60 + Number(t2[1]) * 60 + Number(t2[2]);
                var s = x1 + x2;
                var m = Math.floor(s / 60); s = s % 60;
                var h = Math.floor(m / 60); m = m % 60;
                var d = Math.floor(h / 24); h = h % 24;
                if (h <= 9)
                    h = "0" + h;
                if (m <= 9)
                    m = "0" + m;
                tmpTime2.set_value(h + "" + m);

                //                if (toTime1[0].trim() == tmpTime2._clientID) {
                //                    for (var i = 0; i < toTime1.length - 1; i++) {
                //                        var control = $find(toTime1[i].trim());
                //                        control.set_value(h + "" + m);
                //                    }
                //                }
                //                else if (toTime2[0].trim() == tmpTime2._clientID) {
                //                    for (var i = 0; i < toTime2.length - 1; i++) {
                //                        var control = $find(toTime2[i].trim());
                //                        control.set_value(h + "" + m);
                //                    }
                //                }


                t1 = tmpTime3._projectedValue + ":00";
                t2 = tmpTime5._projectedValue + ":00";
                t1 = t1.split(/\D/);
                t2 = t2.split(/\D/);
                var x1 = Number(t1[0]) * 60 * 60 + Number(t1[1]) * 60 + Number(t1[2]);
                var x2 = Number(t2[0]) * 60 * 60 + Number(t2[1]) * 60 + Number(t2[2]);
                var s = x1 + x2;
                var m = Math.floor(s / 60); s = s % 60;
                var h = Math.floor(m / 60); m = m % 60;
                var d = Math.floor(h / 24); h = h % 24;

                if (h <= 9)
                    h = "0" + h;
                if (m <= 9)
                    m = "0" + m;

                tmpTime4.set_value(h + "" + m);

                //                if (toTime3[0].trim() == tmpTime4._clientID) {
                //                    for (var i = 0; i < toTime3.length - 1; i++) {
                //                        var control = $find(toTime3[i].trim());
                //                        control.set_value(h + "" + m);
                //                    }
                //                }
                //                else if (toTime4[0].trim() == tmpTime4._clientID) {
                //                    for (var i = 0; i < toTime4.length - 1; i++) {
                //                        var control = $find(toTime4[i].trim());
                //                        control.set_value(h + "" + m);
                //                    }
                //                }
            }
        }

        function FlexValidateTextbox(txt) {
            var hdnFromTime1 = document.getElementById("<%= hdnFromTime1.ClientID %>").value;
            var fromTime1 = hdnFromTime1.split(',');

            var hdnFromTime2 = document.getElementById("<%= hdnFromTime2.ClientID %>").value;
            var fromTime2 = hdnFromTime2.split(',');

            var hdnFromTime3 = document.getElementById("<%= hdnFromTime3.ClientID %>").value;
            var fromTime3 = hdnFromTime3.split(',');

            var hdnFromTime4 = document.getElementById("<%= hdnFromTime4.ClientID %>").value;
            var fromTime4 = hdnFromTime4.split(',');

            var hdnToTime1 = document.getElementById("<%= hdnToTime1.ClientID %>").value;
            var toTime1 = hdnToTime1.split(',');

            var hdnToTime2 = document.getElementById("<%= hdnToTime2.ClientID %>").value;
            var toTime2 = hdnToTime2.split(',');

            var hdnToTime3 = document.getElementById("<%= hdnToTime3.ClientID %>").value;
            var toTime3 = hdnToTime3.split(',');

            var hdnToTime4 = document.getElementById("<%= hdnToTime4.ClientID %>").value;
            var toTime4 = hdnToTime4.split(',');

            var hdnDuration1 = document.getElementById("<%= hdnDuration1.ClientID %>").value;
            var duration1 = hdnDuration1.split(',');

            var hdnDuration2 = document.getElementById("<%= hdnDuration2.ClientID %>").value;
            var duration2 = hdnDuration2.split(',');

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
                    var control = $find(fromTime1[i].trim());
                    control.set_value(strTime[0] + "" + strTime[1]);
                }
            }
            else if (fromTime2[0].trim() == txt._clientID) {
                for (var i = 0; i < fromTime2.length - 1; i++) {
                    var control = $find(fromTime2[i].trim());
                    control.set_value(strTime[0] + "" + strTime[1]);
                }
            }
            if (fromTime3[0].trim() == txt._clientID) {
                for (var i = 0; i < fromTime3.length - 1; i++) {
                    var control = $find(fromTime3[i].trim());
                    control.set_value(strTime[0] + "" + strTime[1]);
                }
            }
            else if (fromTime4[0].trim() == txt._clientID) {
                for (var i = 0; i < fromTime4.length - 1; i++) {
                    var control = $find(fromTime4[i].trim());
                    control.set_value(strTime[0] + "" + strTime[1]);
                }
            }
            else if (toTime1[0].trim() == txt._clientID) {
                for (var i = 0; i < toTime1.length - 1; i++) {
                    var control = $find(toTime1[i].trim());
                    control.set_value(strTime[0] + "" + strTime[1]);
                }
            }
            else if (toTime2[0].trim() == txt._clientID) {
                for (var i = 0; i < toTime2.length - 1; i++) {
                    var control = $find(toTime2[i].trim());
                    control.set_value(strTime[0] + "" + strTime[1]);
                }
            }
            else if (toTime3[0].trim() == txt._clientID) {
                for (var i = 0; i < toTime3.length - 1; i++) {
                    var control = $find(toTime3[i].trim());
                    control.set_value(strTime[0] + "" + strTime[1]);
                }
            }
            else if (toTime4[0].trim() == txt._clientID) {
                for (var i = 0; i < toTime4.length - 1; i++) {
                    var control = $find(toTime4[i].trim());
                    control.set_value(strTime[0] + "" + strTime[1]);
                }
            }
            else if (duration1[0].trim() == txt._clientID) {
                for (var i = 0; i < duration1.length - 1; i++) {
                    var control = $find(duration1[i].trim());
                    control.set_value(strTime[0] + "" + strTime[1]);
                }
            }
            else if (duration2[0].trim() == txt._clientID) {
                for (var i = 0; i < duration2.length - 1; i++) {
                    var control = $find(duration2[i].trim());
                    control.set_value(strTime[0] + "" + strTime[1]);
                }
            }
        }
    </script>
</telerik:RadScriptBlock>
<table style="width: 740px">
    <tr>
        <td width="120px">
            <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text=" English Name"
                meta:resourcekey="Label1Resource1"></asp:Label>
        </td>
        <td width="480px">
            <asp:TextBox ID="txtScheduleEng" runat="server" Width="200px" meta:resourcekey="txtScheduleEngResource1"></asp:TextBox>
            <asp:HiddenField ID="hdnFromTime1" runat="server" />
            <asp:HiddenField ID="hdnFromTime2" runat="server" />
            <asp:HiddenField ID="hdnFromTime3" runat="server" />
            <asp:HiddenField ID="hdnFromTime4" runat="server" />
            <asp:HiddenField ID="hdnToTime1" runat="server" />
            <asp:HiddenField ID="hdnToTime2" runat="server" />
            <asp:HiddenField ID="hdnToTime3" runat="server" />
            <asp:HiddenField ID="hdnToTime4" runat="server" />
            <asp:HiddenField ID="hdnDuration1" runat="server" />
            <asp:HiddenField ID="hdnDuration2" runat="server" />
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtScheduleEng"
                Display="None" ErrorMessage="Please Enter English Name " ValidationGroup="GrflxSchedule"
                meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                TargetControlID="RequiredFieldValidator1">
            </cc1:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" CssClass="Profiletitletxt" Text="Arabic Name"
                meta:resourcekey="Label2Resource1"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtScheduleAr" runat="server" Width="200px" meta:resourcekey="txtScheduleArResource1"></asp:TextBox>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtScheduleAr"
                Display="None" ErrorMessage="Please Enter English Name " ValidationGroup="GrflxSchedule"
                meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                TargetControlID="RequiredFieldValidator2">
            </cc1:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr id="trGraceIn" runat="server">
        <td id="Td1" runat="server">
            <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="Grace In"
                meta:resourcekey="Label4Resource1"></asp:Label>
        </td>
        <td id="Td2" runat="server">
            <telerik:RadNumericTextBox ID="txtGraceIn" runat="server" DataType="System.Int64"
                Culture="English (United States)" LabelCssClass="">
                <NumberFormat DecimalDigits="0" GroupSeparator="" />
            </telerik:RadNumericTextBox>
        </td>
        <td id="Td3" runat="server">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtGraceIn"
                Display="None" ErrorMessage="Please Enter  Grace In Minutes" ValidationGroup="GrflxSchedule"
                meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                TargetControlID="RequiredFieldValidator4">
            </cc1:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr id="trGraceOut" runat="server">
        <td id="Td4" runat="server">
            <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="Grace Out"
                meta:resourcekey="Label5Resource1"></asp:Label>
        </td>
        <td id="Td5" runat="server">
            <telerik:RadNumericTextBox ID="txtGraceOut" runat="server" DataType="System.Int64"
                Culture="English (United States)" LabelCssClass="">
                <NumberFormat DecimalDigits="0" GroupSeparator="" />
            </telerik:RadNumericTextBox>
        </td>
        <td id="Td6" runat="server">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtGraceOut"
                Display="None" ErrorMessage="Please Enter  Grace Out Minutes" ValidationGroup="GrflxSchedule"
                meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True"
                TargetControlID="RequiredFieldValidator5">
            </cc1:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
</table>
<table style="width: 710px">
    <tr>
        <td colspan="3">
            <div style="border: solid 1px #A5B3C5; height: 30px; border-bottom: none;">
                <div style="width: 76px; border-right: solid 1px #fff; background-color: #27710a;
                    height: 30px; float: left;">
                </div>
                <div style="border-left: solid 1px #e1eaf3; color: #FFFFFF; padding-top: 5px; font-weight: bold;
                    padding-left: 5px; border-right: solid 1px #fff; background-color: #27710a; height: 25px;
                    float: left; text-align: center; width: 283px;">
                    <asp:Label ID="lblShift1" runat="server" meta:resourcekey="lblShift1Resource1"></asp:Label>
                    <%--Shift 1--%>
                </div>
                <div style="border-left: solid 1px #e1eaf3; background-color: #27710a; color: #FFFFFF;
                    padding-top: 5px; font-weight: bold; text-align: center; height: 25px; width: 278px;
                    float: left; padding-left: 5px">
                    <asp:Label ID="lblShift2" runat="server" meta:resourcekey="lblShift2Resource1"></asp:Label>
                    <%-- Shift 2--%>
                </div>
                <div style="border-left: solid 1px #e1eaf3; background-color: #27710a; color: #4C607A;
                    padding-top: 5px; font-weight: bold; text-align: center; height: 25px; width: 47px;
                    float: left; padding-left: 5px">
                </div>
            </div>
            <telerik:RadGrid ID="dgrdWeekTimeShift1" runat="server" AllowSorting="True" AllowPaging="True"
                Skin="Hay" GridLines="None" ShowStatusBar="True" ShowFooter="True" meta:resourcekey="dgrdWeekTimeShift1Resource1">
                <SelectedItemStyle ForeColor="Maroon" />
                <GroupingSettings CaseSensitive="False" />
                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="Dayid">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="Dayid" SortExpression="Dayid" Visible="false" />
                        <telerik:GridTemplateColumn HeaderText="Work Days">
                            <ItemTemplate>
                                <asp:Label ID="lblDays" runat="server" Text='' meta:resourcekey="lblDaysResource1" />
                                <asp:HiddenField ID="hdnEnDay" runat="server" Value='<%# DataBinder.Eval(Container,"DataItem.Days") %>' />
                                <asp:HiddenField ID="hdnArDay" runat="server" Value='<%# DataBinder.Eval(Container,"DataItem.ArabicDays") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="From Time1">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtFromTime1" runat="server" TextWithLiterals="00:00"
                                    Mask="##:##" Width="30px" AutoPostBack="false" DisplayMask="##:##" Text='<%#DataBinder.Eval(Container,"DataItem.FromTime1")%>'>
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="From Time2">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtFromTime2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    Width="30px" Text='<%#DataBinder.Eval(Container,"DataItem.FromTime2")%>' AutoPostBack="false"
                                    DisplayMask="##:##">
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Duration">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtDuration1" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    Width="30px" Text='<%#DataBinder.Eval(Container,"DataItem.Duration1")%>' DisplayMask="##:##">
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="To Time1">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtToTime1" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    Width="30px" AutoPostBack="false" DisplayMask="##:##">
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="To Time2">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtToTime2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    Width="30px" AutoPostBack="false" DisplayMask="##:##">
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="From Time1">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtFromTime3" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    Width="30px" AutoPostBack="false" DisplayMask="##:##" Text='<%#DataBinder.Eval(Container,"DataItem.FromTime3")%>'>
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="From Time2">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtFromTime4" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    Width="30px" Text='<%#DataBinder.Eval(Container,"DataItem.FromTime4")%>' AutoPostBack="false"
                                    DisplayMask="##:##">
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Duration">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtDuration2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    Width="30px" Text='<%#DataBinder.Eval(Container,"DataItem.Duration2")%>' DisplayMask="##:##">
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="To Time1">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtToTime3" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    Width="30px" AutoPostBack="false" DisplayMask="##:##">
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="To Time2">
                            <ItemTemplate>
                                <telerik:RadMaskedTextBox ID="rmtToTime4" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    Width="30px" AutoPostBack="false" DisplayMask="##:##">
                                </telerik:RadMaskedTextBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Is Offday">
                            <ItemTemplate>
                                <asp:CheckBox ID="Shift2offDay" runat="server" CssClass="Checkbox" Checked='<%# DataBinder.Eval(Container,"DataItem.IsOffDay") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                </MasterTableView>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
