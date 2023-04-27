<%@ Page Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="WorkScheduleFlexible.aspx.vb" Inherits="Admin_WorkScheduleFlexible"
    Title="Define Flexible work Schedule" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <telerik:RadScriptBlock ID="scriptblock" runat="server">
        <script type="text/javascript">

            function x(FromTime1, ToTime1, FromTime2, ToTime2, Duration1, FromTime3, ToTime3, FromTime4, ToTime4, Duration2, chk) {
                if (chk.checked = true) {
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
                }
            }
            function ValidateGridRow(FromTime1, ToTime1, FromTime2, ToTime2, Duration1) {

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

            function ValidateGridRowTest(FromTime1, ToTime1, FromTime2, ToTime2, Duration1, chk) {
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
                    ValidateTextbox(tmpTime1);
                    ValidateTextbox(tmpTime2);
                    ValidateTextbox(tmpTime3);
                    ValidateTextbox(tmpTime4);
                    ValidateTextbox(tmpTime5);

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
                }

            }
            function ValidateTextbox(txt) {
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
            }
    
     
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 740px">
                <tr>
                    <td colspan="3">
                        <center>
                            <uc1:PageHeader ID="PageHeader1" runat="server" />
                        </center>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td width="120px">
                        <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text=" English Name"
                            meta:resourcekey="Label1Resource1"></asp:Label>
                    </td>
                    <td width="480px">
                        <asp:TextBox ID="txtScheduleEng" runat="server" Width="200px" meta:resourcekey="txtScheduleEngResource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtScheduleEng"
                            Display="None" ErrorMessage="Please Enter English Name " ValidationGroup="GrSchedule"
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
                            Display="None" ErrorMessage="Please Enter English Name " ValidationGroup="GrSchedule"
                            meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                            TargetControlID="RequiredFieldValidator2">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr id="trGraceIn" runat="server">
                    <td runat="server">
                        <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="Grace In"
                            meta:resourcekey="Label4Resource1"></asp:Label>
                    </td>
                    <td runat="server">
                        <telerik:RadNumericTextBox ID="txtGraceIn" runat="server" DataType="System.Int64"
                            Culture="English (United States)" LabelCssClass="">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td runat="server">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtGraceIn"
                            Display="None" ErrorMessage="Please Enter  Grace In Minutes" ValidationGroup="GrSchedule"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                            TargetControlID="RequiredFieldValidator4">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                </tr>
                <tr id="trGraceOut" runat="server">
                    <td runat="server">
                        <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="Grace Out"
                            meta:resourcekey="Label5Resource1"></asp:Label>
                    </td>
                    <td runat="server">
                        <telerik:RadNumericTextBox ID="txtGraceOut" runat="server" DataType="System.Int64"
                            Culture="English (United States)" LabelCssClass="">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                        </telerik:RadNumericTextBox>
                    </td>
                    <td runat="server">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtGraceOut"
                            Display="None" ErrorMessage="Please Enter  Grace Out Minutes" ValidationGroup="GrSchedule"></asp:RequiredFieldValidator>
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
            <table style="width: 750px">
                <tr>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div style="border: solid 1px #A5B3C5; height: 30px; border-bottom: none;">
                            <div style="width: 76px; border-right: solid 1px #fff; background-color: #27710a;
                                height: 30px; float: left;">
                            </div>
                            <div style="border-left: solid 1px #e1eaf3; color: #FFFFFF; padding-top: 5px; font-weight: bold;
                                padding-left: 5px; border-right: solid 1px #fff; background-color: #27710a; height: 25px;
                                float: left; text-align: center; width: 303px;">
                                Shift 1
                            </div>
                            <div style="border-left: solid 1px #e1eaf3; background-color: #27710a; color: #FFFFFF;
                                padding-top: 5px; font-weight: bold; text-align: center; height: 25px; width: 296px;
                                float: left; padding-left: 5px">
                                Shift 2
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
                                            <asp:Label ID="lblDays" runat="server" Text=''
                                                meta:resourcekey="lblDaysResource1" />
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
                                            <asp:CheckBox ID="Shift2offDay" runat="server" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Button ID="ibtnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="GrSchedule"
                            meta:resourcekey="ibtnSaveResource1" />
                        <asp:Button ID="ibtnDelete" runat="server" CausesValidation="False" CssClass="button"
                            Text="Delete" meta:resourcekey="ibtnDeleteResource1" />
                        <asp:Button ID="ibtnRest" runat="server" CausesValidation="False" CssClass="button"
                            Text="Clear" meta:resourcekey="ibtnRestResource1" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdWorkSchedule"
                            Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <telerik:RadGrid ID="dgrdWorkSchedule" runat="server" AllowSorting="True" AllowPaging="True"
                            PageSize="25" Skin="Hay" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                            ShowFooter="True" OnItemCommand="dgrdWorkSchedule_ItemCommand" meta:resourcekey="dgrdWorkScheduleResource1">
                            <SelectedItemStyle ForeColor="Maroon" />
                            <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="ScheduleId">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource13"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource1" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="ScheduleName" SortExpression="ScheduleName" HeaderText="Schedule Name English"
                                        meta:resourcekey="GridBoundColumnResource2" UniqueName="ScheduleName" />
                                    <telerik:GridBoundColumn DataField="ScheduleArabicName" SortExpression="ScheduleArabicName"
                                        HeaderText="Schedule Name Arabic" meta:resourcekey="GridBoundColumnResource3"
                                        UniqueName="ScheduleArabicName" />
                                    <telerik:GridBoundColumn DataField="ScheduleId" SortExpression="ScheduleId" Visible="False"
                                        AllowFiltering="False" meta:resourcekey="GridBoundColumnResource4" UniqueName="ScheduleId" />
                                    <telerik:GridBoundColumn DataField="GraceIn" SortExpression="GraceIn" HeaderText="Grace In"
                                        meta:resourcekey="GridBoundColumnResource5" UniqueName="GraceIn" />
                                    <telerik:GridBoundColumn DataField="GraceOut" SortExpression="GraceOut" HeaderText="Grace Out"
                                        meta:resourcekey="GridBoundColumnResource6" UniqueName="GraceOut" />
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
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
