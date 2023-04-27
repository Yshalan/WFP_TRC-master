<%@ Page Title="Shift Work Schedule" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="WorkSchedule_Shifts.aspx.vb" Inherits="Admin_WorkSchedule_Shifts"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 
    <telerik:RadScriptBlock ID="scriptblock" runat="server">
        <script type="text/javascript">
            function colorChanged(sender) {
                sender.get_element().style.backgroundColor = "#" + sender.get_selectedColor();
                sender.get_element().style.color = "#" + sender.get_selectedColor();
                sender.get_element().value = "#" + sender.get_selectedColor();
            }
            function ValidateGridRow(FromTime1, ToTime1, FromTime2, ToTime2, Chk) {

                if (Chk.checked == true) {
                    var tmpTime1 = $find(FromTime1);
                    tmpTime1.set_value("0000");
                    var tmpTime2 = $find(ToTime1);
                    tmpTime2.set_value("0000");
                    var tmpTime3 = $find(FromTime2);
                    tmpTime3.set_value("0000");
                    var tmpTime4 = $find(ToTime2);
                    tmpTime4.set_value("0000");
                }
            }
            function ValidateTextbox(sender, args) {

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
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <asp:UpdatePanel runat="server" ID="upShifts">
        <ContentTemplate>
            <cc1:TabContainer runat="server" ID="TabContainer1" ActiveTabIndex="0" meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel runat="server" ID="tabMaster" HeaderText="Group Details" meta:resourcekey="tabMasterResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text=" English Name"
                                    meta:resourcekey="Label1Resource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtScheduleEng" runat="server" meta:resourcekey="txtScheduleEngResource1"></asp:TextBox>
                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtScheduleEng" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{0,50}$" runat="server"
                                    ErrorMessage="Maximum allowed characters is 50." ValidationGroup="GrSchedule"
                                    meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtScheduleEng"
                                    Display="None" ErrorMessage="Please Enter English Name " ValidationGroup="GrSchedule"
                                    meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revttxtScheduleEng" Display="Dynamic" ValidationGroup="GrSchedule"
                                    runat="server" ErrorMessage="Special Characters are not allowed!" ValidationExpression="[^~`!@#$%\^&\*\(\)\-_+=\\\|\}\]\{\['&quot;:?.>,</]+"
                                    ControlToValidate="txtScheduleEng" meta:resourcekey="revttxtScheduleEngResource1">
                                </asp:RegularExpressionValidator>
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
                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtScheduleAr" ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{0,50}$" runat="server"
                                    ErrorMessage="Maximum allowed characters is 50." ValidationGroup="GrSchedule" meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                                <asp:RegularExpressionValidator ID="revtxtScheduleAr" Display="Dynamic" ValidationGroup="GrSchedule"
                                    runat="server" ErrorMessage="Special Characters are not allowed!" ValidationExpression="[^~`!@#$%\^&\*\(\)\-_+=\\\|\}\]\{\['&quot;:?.>,</]+"
                                    ControlToValidate="txtScheduleAr" meta:resourcekey="revttxtScheduleEngResource1">
                                </asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtScheduleAr"
                                    Display="None" ErrorMessage="Please Enter English Name " ValidationGroup="GrSchedule"
                                    meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator2">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row" id="trGraceIn" runat="server">
                            <div class="col-md-2">
                                <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="Grace In"
                                    meta:resourcekey="Label4Resource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtGraceIn" runat="server" DataType="System.Int64" MinValue="0" MaxValue="360"
                                    Culture="English (United States)" LabelCssClass="">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtGraceIn"
                                    Display="None" ErrorMessage="Please Enter  Grace In Minutes" ValidationGroup="GrSchedule"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator4">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row" id="trGraceOut" runat="server">
                            <div class="col-md-2">
                                <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="Grace Out"
                                    meta:resourcekey="Label5Resource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtGraceOut" runat="server" DataType="System.Int64" MinValue="0" MaxValue="360"
                                    Culture="English (United States)" LabelCssClass="">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtGraceOut"
                                    Display="None" ErrorMessage="Please Enter  Grace Out Minutes" ValidationGroup="GrSchedule"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator5">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblMinimumAllowTime" runat="server" CssClass="Profiletitletxt" Text="Minimum Allow Time Between Shift"
                                    meta:resourcekey="lblMinimumAllowTimeResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadNumericTextBox ID="txtMinimumAllowTime" runat="server" DataType="System.Int64" MinValue="0" MaxValue="360"
                                    Culture="English (United States)" LabelCssClass="">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="lblMinutes" runat="server" Text="Minute(s)" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblMinutesResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkConsiderAtShiftEnd" runat="server" Text="Consider Shift Schedule At Shift End"
                                    meta:resourcekey="chkConsiderAtShiftEndResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:CheckBox ID="chkIsActive" runat="server" Text="Is Active"
                                    meta:resourcekey="chkIsActiveResource1" />
                            </div>
                        </div>
                        <div class="row" id="trControls" runat="server">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="ibtnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="GrSchedule"
                                    meta:resourcekey="ibtnSaveResource1" />
                                <asp:Button ID="ibtnDelete" runat="server" CausesValidation="False" CssClass="button"
                                    Text="Delete" meta:resourcekey="ibtnDeleteResource1" />
                                <asp:Button ID="ibtnRest" runat="server" CausesValidation="False" CssClass="button"
                                    Text="Clear" meta:resourcekey="ibtnRestResource1" />
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" ID="tabDetails" HeaderText="Shift Details" meta:resourcekey="tabDetailsResource1">
                    <ContentTemplate>
                        <div id="divAddShift" style="margin-bottom: 20px;">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label runat="server" CssClass="Profiletitletxt" ID="lblShiftCodeCap" Text="Shift Code"
                                        meta:resourcekey="lblShiftCodeCapResource1" />
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtShiftCode" meta:resourcekey="txtShiftCodeResource1" />
                                    <asp:RequiredFieldValidator ID="rfvShiftCode" runat="server" ControlToValidate="txtShiftCode"
                                        Display="None" ErrorMessage="Please Enter Shift Code " ValidationGroup="shift"
                                        meta:resourcekey="rfvShiftCodeResource1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtShiftCode" ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{0,50}$" runat="server"
                                        ErrorMessage="Maximum allowed characters is 50." ValidationGroup="GrSchedule"
                                        meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="Dynamic" ValidationGroup="GrSchedule"
                                        runat="server" ErrorMessage="Special Characters are not allowed!" ValidationExpression="[^~`!@#$%\^&\*\(\)\-_+=\\\|\}\]\{\['&quot;:?.>,</]+"
                                        ControlToValidate="txtShiftCode" meta:resourcekey="revttxtScheduleEngResource1">
                                    </asp:RegularExpressionValidator>
                                    <cc1:ValidatorCalloutExtender ID="rfvShiftCode_ValidatorCalloutExtender" runat="server"
                                        Enabled="True" TargetControlID="rfvShiftCode">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label runat="server" CssClass="Profiletitletxt" ID="lblShiftName" Text="ShiftName"
                                        meta:resourcekey="lblShiftNameResource1" />
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtShiftName" meta:resourcekey="txtShiftNameResource1" />
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtShiftName" ID="RegularExpressionValidator5" ValidationExpression="^[\s\S]{0,50}$" runat="server"
                                        ErrorMessage="Maximum allowed characters is 50." ValidationGroup="GrSchedule"
                                        meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Display="Dynamic" ValidationGroup="GrSchedule"
                                        runat="server" ErrorMessage="Special Characters are not allowed!" ValidationExpression="[^~`!@#$%\^&\*\(\)\-_+=\\\|\}\]\{\['&quot;:?.>,</]+"
                                        ControlToValidate="txtShiftName" meta:resourcekey="revttxtScheduleEngResource1">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-4">
                                    <asp:CheckBox ID="chkIsOffDay" runat="server" Text="Rest Day" ToolTip="Consider Shift As Rest Day (Off Day)" AutoPostBack="true" 
                                        meta:resourcekey="chkIsOffDayResource1"/>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label runat="server" CssClass="Profiletitletxt" ID="lblFromTime1" Text="From Time1"
                                        meta:resourcekey="lblFromTime1Resource1" />
                                </div>
                                <div class="col-md-2">
                                    <telerik:RadMaskedTextBox ID="txtFromTime1" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                        DisplayMask="##:##" LabelCssClass="" meta:resourcekey="txtFromTime1Resource1"
                                        Text="0000">
                                        <ClientEvents OnValueChanged="ValidateTextbox" />
                                    </telerik:RadMaskedTextBox>
                                </div>
                                <%--<asp:RequiredFieldValidator ID="rfvFromTime1" runat="server" ControlToValidate="txtFromTime1"
                                                InitialValue="00:00" Display="None" ErrorMessage="Please Enter From Time1 " ValidationGroup="shift"
                                                meta:resourcekey="rfvFromTime1Resource1"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="rfvFromTime1ValidatorCalloutExtender" runat="server"
                                                Enabled="True" TargetControlID="rfvFromTime1">
                                            </cc1:ValidatorCalloutExtender>--%>
                                <div class="col-md-2">
                                    <asp:Label runat="server" CssClass="Profiletitletxt" ID="lblToTime1" Text="ToTime1"
                                        meta:resourcekey="lblToTime1Resource1" />
                                </div>
                                <div class="col-md-2">
                                    <telerik:RadMaskedTextBox ID="txtToTime1" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                        DisplayMask="##:##" LabelCssClass="" meta:resourcekey="txtToTime1Resource1"
                                        Text="0000">
                                        <ClientEvents OnValueChanged="ValidateTextbox" />
                                    </telerik:RadMaskedTextBox>
                                </div>
                                <%-- <asp:RequiredFieldValidator ID="rfvToTime1" runat="server" ControlToValidate="txtToTime1"
                                                InitialValue="00:00" Display="None" ErrorMessage="Please Enter From Time1 " ValidationGroup="shift"
                                                meta:resourcekey="rfvToTime1Resource1"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="rfvToTime1_ValidatorCalloutExtender" runat="server"
                                                Enabled="True" TargetControlID="rfvToTime1">
                                            </cc1:ValidatorCalloutExtender>--%>
                                <div class="col-md-2">
                                    <asp:Label ID="lblFlexTime1" runat="server" Text="Flexibile Time 1" CssClass="Profiletitletxt" meta:resourcekey="lblFlexTime1Resource1"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <telerik:RadMaskedTextBox ID="radFlex1" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                        DisplayMask="##:##" LabelCssClass="" Text="0000">
                                        <ClientEvents OnValueChanged="ValidateTextbox" />
                                    </telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label runat="server" CssClass="Profiletitletxt" ID="lblFromTime2" Text="FromTime2"
                                        meta:resourcekey="lblFromTime2Resource1" />
                                </div>
                                <div class="col-md-2">
                                    <telerik:RadMaskedTextBox ID="txtFromTime2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                        DisplayMask="##:##" LabelCssClass="" meta:resourcekey="txtFromTime2Resource1"
                                        Text="0000">
                                        <ClientEvents OnValueChanged="ValidateTextbox" />
                                    </telerik:RadMaskedTextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label runat="server" CssClass="Profiletitletxt" ID="lblToTime2" Text="ToTime2"
                                        meta:resourcekey="lblToTime2Resource1" />
                                </div>
                                <div class="col-md-2">
                                    <telerik:RadMaskedTextBox ID="txtToTime2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                        DisplayMask="##:##" LabelCssClass="" meta:resourcekey="txtToTime2Resource1"
                                        Text="0000">
                                        <ClientEvents OnValueChanged="ValidateTextbox" />
                                    </telerik:RadMaskedTextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblFlexTime2" runat="server" Text="Flexibile Time 2" CssClass="Profiletitletxt" meta:resourcekey="lblFlexTime2Resource1"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <telerik:RadMaskedTextBox ID="radFlex2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                        DisplayMask="##:##" LabelCssClass="" Text="0000">
                                        <ClientEvents OnValueChanged="ValidateTextbox" />
                                    </telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label runat="server" CssClass="Profiletitletxt" ID="lblColor" Text="Color"
                                        meta:resourcekey="lblColorResource1" />
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox runat="server" ID="txtTKColor" disabled="disabled" Style="background-color: #FFFFFF; color: #FFFFFF"
                                        Width="60px" Text="#FFFFFF" meta:resourcekey="txtTKColorResource1"></asp:TextBox>
                                    <cc1:ColorPickerExtender runat="server" ID="ColorPickerExtender1" TargetControlID="txtTKColor"
                                        OnClientColorSelectionChanged="colorChanged" PopupButtonID="btnShowColorExtender"  
                                        Enabled="True" />
                                </div>
                                <div class="col-md-1">
                                    <asp:ImageButton runat="server" ID="btnShowColorExtender" ImageUrl="~/images/icon_color_picker.gif"
                                        Height="20px" Width="20px" Style="vertical-align: middle" meta:resourcekey="btnShowColorExtenderResource1" />
                                    <asp:RequiredFieldValidator ID="rfvColorCode" runat="server" ControlToValidate="txtTKColor"
                                        InitialValue="#FFFFFF" Display="None" ErrorMessage="Please Choose Color" ValidationGroup="shift"
                                        meta:resourcekey="rfvColorCodeResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="rfvColorCode_ValidatorCalloutExtender" runat="server"
                                        Enabled="True" TargetControlID="rfvColorCode">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblPayRate" runat="server" CssClass="Profiletitletxt" Text="Pay Rate"
                                        meta:resourcekey="lblPayRateResource1" Visible="false"></asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <telerik:RadNumericTextBox ID="txtPayRate" runat="server" MinValue="0" Visible="false">
                                        <NumberFormat DecimalDigits="1" />
                                    </telerik:RadNumericTextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <asp:Button runat="server" ID="btnSaveShift" Text="Save" CssClass="button" ValidationGroup="shift"
                                        meta:resourcekey="btnSaveShiftResource1" />
                                    <asp:Button runat="server" ID="btnClearShift" Text="Clear" CssClass="button" meta:resourcekey="btnClearShiftResource1" />
                                    <asp:Button runat="server" ID="btnDeleteShift" Text="Delete" CssClass="button" meta:resourcekey="btnDeleteShiftResource1" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive1">
                                <asp:GridView ID="grdShift" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    CssClass="GridViewStyle" DataKeyNames="ShiftId" meta:resourcekey="grdShiftResource1">
                                    <Columns>
                                        <asp:ButtonField CommandName="ShiftEdit" DataTextField="ShiftCode" HeaderText="Shift Code"
                                            SortExpression="ShiftCode" meta:resourcekey="ButtonFieldResource1" />
                                        <asp:BoundField DataField="ShiftName" HeaderText="Shift Name" SortExpression="ShiftName"
                                            meta:resourcekey="BoundFieldResource1" />
                                        <asp:BoundField HeaderText="From Time1" DataField="FromTime1" SortExpression="FromTime1"
                                            meta:resourcekey="BoundFieldResource2" />
                                        <asp:BoundField HeaderText="To Time1" DataField="ToTime1" SortExpression="ToTime1"
                                            meta:resourcekey="BoundFieldResource3" />
                                        <asp:BoundField HeaderText="Flex Time1" DataField="FlexTime1" SortExpression="FlexTime1"
                                            meta:resourcekey="BoundFieldResource6" />
                                        <asp:BoundField HeaderText="From Time2" DataField="FromTime2" SortExpression="FromTime2"
                                            meta:resourcekey="BoundFieldResource4" />
                                        <asp:BoundField HeaderText="To Time2" DataField="ToTime2" SortExpression="ToTime2"
                                            meta:resourcekey="BoundFieldResource5" />
                                        <asp:BoundField HeaderText="Flex Time2" DataField="FlexTime2" SortExpression="FlexTime2" ItemStyle-ForeColor="Black"
                                            meta:resourcekey="BoundFieldResource7" />
                                        <asp:TemplateField HeaderText="Color" meta:resourcekey="TemplateFieldResource1">

                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblShiftColor" Width="100px" Height="25px" ForeColor="White" Style="color: #fff !important;"
                                                    meta:resourcekey="lblShiftColorResource1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle" />
                                    <HeaderStyle CssClass="HeaderStylenew" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                </asp:GridView>
                            </div>
                        </div>

                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <div>
                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdWorkSchedule"
                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadGrid ID="dgrdWorkSchedule" runat="server" AllowSorting="True" AllowPaging="True"
                        PageSize="15" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        ShowFooter="True" OnItemCommand="dgrdWorkSchedule_ItemCommand" meta:resourcekey="dgrdWorkScheduleResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="ScheduleName,ScheduleId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ScheduleName" SortExpression="ScheduleName" HeaderText="English Name"
                                    meta:resourcekey="GridBoundColumnResource1" />
                                <telerik:GridBoundColumn DataField="ScheduleArabicName" SortExpression="ScheduleArabicName"
                                    HeaderText="Arabic Name" meta:resourcekey="GridBoundColumnResource2" />
                                <telerik:GridBoundColumn DataField="GraceIn" SortExpression="ScheduleName" HeaderText="Grace In"
                                    meta:resourcekey="GridBoundColumnResource3" />
                                <telerik:GridBoundColumn DataField="GraceOut" SortExpression="ScheduleName" HeaderText="Grace Out"
                                    meta:resourcekey="GridBoundColumnResource4" />
                                <telerik:GridBoundColumn DataField="ScheduleId" SortExpression="ScheduleId" Visible="false"
                                    AllowFiltering="false" />
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
                        <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
