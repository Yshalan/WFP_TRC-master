<%@ Page Title="Notification Type" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="NotificationType.aspx.vb" Inherits="Admin_NotificationType"
    meta:resourcekey="PageResource1" UICulture="auto" ValidateRequest="false" Trace="false" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function hideValidatorCalloutTab() {
            try {
                if (AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout != null) {
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.hide();


                }
            }
            catch (err) {
            }
            return false;
        }

        function SetFocusText(txt) {
            var hdnFocusName = document.getElementById("<%= hdnFocusName.ClientID %>");
            hdnFocusName.value = txt.id;
        }

        function CheckBoxListSelectCoordinatorType(state) {
            var chkBoxList = document.getElementById("<%= cblCoordinatorTypes.ClientID%>");
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" />
            <asp:MultiView ID="mvNotificationType" ActiveViewIndex="0" runat="server">
                <asp:View ID="viewNotificationTypeList" runat="server">
                    <div class="row">
                        <div class="table-responsive">
                            <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdNotificationType"
                                Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                            <telerik:RadGrid runat="server" ID="dgrdNotificationType" AutoGenerateColumns="False"
                                PageSize="25" AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="false"
                                GridLines="None" meta:resourcekey="dgrdLastBalanceResource1">
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                    EnableRowHoverStyle="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False"
                                    DataKeyNames="NotificationTypeId">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="TypeNameEn" HeaderText="English Type Name" SortExpression="TypeNameEn"
                                            UniqueName="TypeNameEn" meta:resourcekey="GridBoundColumnResource2">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TypeNameAr" HeaderText="Arabic Type Name" SortExpression="TypeNameAr"
                                            UniqueName="TypeNameAr" meta:resourcekey="GridBound1ColumnResource1">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="Has Email" UniqueName="HasEmail" meta:resourcekey="GridTemplateColumnResource1">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkHasEmail" runat="server" Enabled="False" Text="&nbsp;" />
                                                <asp:HiddenField ID="hdnHasEmail" runat="server" Value='<%# Eval("HasEmail") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Has SMS" UniqueName="HasSMS" meta:resourcekey="GridTemplateColumnResource2">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkHasSMS" runat="server" Enabled="False" Text="&nbsp;" />
                                                <asp:HiddenField ID="hdnHasSMS" runat="server" Value='<%# Eval("HasSMS") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="NotificationTypeId" DataType="System.Int32" Visible="False"
                                            HeaderText="NotificationTypeId" meta:resourcekey="GridBoundColumnResource5" UniqueName="NotificationTypeId">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource3">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" OnClick="btn_Click" meta:resourcekey="lbtnEditBalanceResource1"
                                                    Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
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
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </asp:View>
                <asp:View runat="server" ID="viewEditNotificationType">
                    <div style="padding-bottom: 5px;">
                        <asp:Label ID="lblNotificationType" runat="server" />
                    </div>
                    <cc1:TabContainer ID="TabContainer1" runat="server" AutoPostBack="True" ActiveTabIndex="0"
                        OnClientActiveTabChanged="hideValidatorCalloutTab" meta:resourcekey="TabContainer1Resource1">
                        <cc1:TabPanel ID="TabCheck" runat="server" HeaderText="Checks" meta:resourcekey="TabCheckResource1">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkHasEmail" runat="server" AutoPostBack="true" Text=" Has Email"
                                            meta:resourcekey="chkHasEmailResource2" />
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Panel ID="pnlEmailSendingTime" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:RadioButtonList ID="rblEmailSendingTime" runat="server" AutoPostBack="true">
                                                    <asp:ListItem Value="0" Text="Send Immediately" Selected="True" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Send At Specific Time" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:Panel ID="pnlEmailSpecificTime" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <asp:Label ID="lblEmailSendingTime" runat="server" Text="Sending Time" meta:resourcekey="lblEmailSendingTimeResource1"></asp:Label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <telerik:RadTimePicker ID="radtpEmailSpecificTime" runat="server" AllowCustomText="false"
                                                                MarkFirstMatch="true" Skin="Vista" AutoPostBack="True">
                                                                <DateInput ID="DateInput4" runat="server" ToolTip="Email Sending Specific Time" DateFormat="HH:mm" />
                                                            </telerik:RadTimePicker>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                        <br />
                                    </asp:Panel>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkHasSMS" runat="server" Text=" Has SMS" AutoPostBack="true" meta:resourcekey="chkHasSMSResource2" />
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:Panel ID="pnlSMSSendingTime" runat="server" Visible="false">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:RadioButtonList ID="rblSMSSendingTime" runat="server" AutoPostBack="true">
                                                    <asp:ListItem Value="0" Text="Send Immediately" Selected="True" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Send At Specific Time" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:Panel ID="pnlSMSSpecificTime" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <asp:Label ID="lblSMSSendingTime" runat="server" Text="Sending Time" meta:resourcekey="lblSMSSendingTimeResource1"></asp:Label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <telerik:RadTimePicker ID="radtpSMSSpecificTime" runat="server" AllowCustomText="false"
                                                                MarkFirstMatch="true" Skin="Vista" AutoPostBack="True">
                                                                <DateInput ID="DateInput1" runat="server" ToolTip="SMS Sending Specific Time" DateFormat="HH:mm" />
                                                            </telerik:RadTimePicker>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkSendToEmployee" runat="server" Text=" Send To Employee" meta:resourcekey="chkSendToEmployeeResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkSendReportToManager" runat="server" Text=" SendReportToManager"
                                            meta:resourcekey="chkSendReportToManagerResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkSendReportToDeputyManager" runat="server" Text=" Send To Deputy Manager"
                                            meta:resourcekey="chkSendReportToDeputyManagerResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkSendToReportHR" runat="server" Text=" SendToReportHR" meta:resourcekey="chkSendToReportHRResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkSendReportToCoordinator" runat="server" Text=" SendToReportCoordinator" AutoPostBack="true" meta:resourcekey="chkSendReportToCoordinatorResource1" />
                                    </div>
                                </div>
                                <div id="dvCoordinatorType" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label ID="lblCoordinatorType" runat="server" CssClass="Profiletitletxt" Text="List Of Coordinator Type(s)"
                                                meta:resourcekey="lblCoordinatorTypeResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
                                                <asp:CheckBoxList ID="cblCoordinatorTypes" runat="server" Style="height: 26px" CssClass="checkboxlist"
                                                    DataTextField="CoordinatorTypeName" DataValueField="CoordinatorTypeId">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <a href="javascript:void(0)" onclick="CheckBoxListSelectCoordinatorType(true)" style="font-size: 8pt">
                                                <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                                            <a href="javascript:void(0)" onclick="CheckBoxListSelectCoordinatorType(false)" style="font-size: 8pt">
                                                <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:HyperLink ID="hlViewCoordinatorType" runat="server" Visible="False"
                                                Text="View Coordinator Type(s) "></asp:HyperLink>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                        </div>
                                        <div class="col-md-4">
                                            <asp:CustomValidator ID="cvCoordinatorTypeValidation" ErrorMessage="please select at least one coordinator type"
                                                ValidationGroup="ValidateEmailParameters" ForeColor="Black" runat="server" CssClass="customValidator"
                                                meta:resourcekey="cvCoordinatorTypeValidationResource1" />

                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:Label ID="lblNotificationPolicy" runat="server" Text=" Notification Policy" Visible="false" meta:resourcekey="lblNotificationPolicyResource1" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:RadioButtonList ID="rblNotificationPoicy" runat="server" AutoPostBack="false" Visible="false" RepeatDirection="Vertical">
                                            <asp:ListItem Value="0" Text="Keep Notification If Punch In Again" Selected="True" meta:resourcekey="ListItem3Resource1"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Keep Notification If Punch In Again After Schedule Time Only" meta:resourcekey="ListItem4Resource1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Delete Notification If Punch In Again" meta:resourcekey="ListItem5Resource1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="row" id="dvRepeatedAbsent" runat="server" visible="false">
                                    <div class="col-md-4">
                                        <asp:Label ID="lblNoOfAbsent" runat="server" Text="No. Of Absent Days" meta:resourcekey="lblNoOfAbsentResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadNumericTextBox ID="radnumAbsentDays" MinValue="2" MaxValue="365"
                                            Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="radnumAbsentDaysResource1">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="rfvAbsentDays" runat="server" ControlToValidate="radnumAbsentDays"
                                            Display="None" ErrorMessage="Please Enter No. Of Absent Days" ValidationGroup="grpUpdate"
                                            meta:resourcekey="rfvAbsentDaysResource1" Enabled="false">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceAbsentDays" runat="server" CssClass="AISCustomCalloutStyle"
                                            TargetControlID="rfvAbsentDays" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div id="dvAdditionalLevel" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblAdditionalLevel" runat="server" Text="Additional Managerial Level"
                                                ToolTip="The Selected Policy Applies When Approval Level Contains Manage Approval, System Will Add The Selected Level Manager To Email CC"
                                                meta:resourcekey="lblAdditionalLevelResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <telerik:RadComboBox ID="radcmbLevels" Filter="Contains" MarkFirstMatch="true" Skin="Vista"
                                                runat="server">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                </div>
                                <div id="dvEmployee_OutDuration_AC" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblOutDurationPolicy_AC" runat="server" Text="Out Duration Policy"
                                                meta:resourcekey="lblOutDurationPolicy_ACResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <telerik:RadNumericTextBox ID="radnumtxtOutDurationPolicy_AC" MinValue="0" MaxValue="1440"
                                                Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="radnumtxtOutDurationPolicy_ACResource1">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="col-md-1">
                                            <asp:Label ID="lblMinutes" runat="server" Text="Minute(s)" meta:resourcekey="lblMinutesResource1"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div id="dvOccurrence" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblFirstOccurrence" runat="server" Text="First Occurrence" meta:resourcekey="lblFirstOccurrenceResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <telerik:RadNumericTextBox ID="radnumFirstOccurrence" MinValue="0" MaxValue="999"
                                                Skin="Vista" runat="server" Culture="en-US" LabelCssClass="">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblSecondOccurrence" runat="server" Text="Second Occurrence" meta:resourcekey="lblSecondOccurrenceResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <telerik:RadNumericTextBox ID="radnumSecondOccurrence" MinValue="0" MaxValue="999"
                                                Skin="Vista" runat="server" Culture="en-US" LabelCssClass="">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblThirdOccurrence" runat="server" Text="Third Occurrence" meta:resourcekey="lblThirdOccurrenceResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            <telerik:RadNumericTextBox ID="radnumThirdOccurrence" MinValue="0" MaxValue="999"
                                                Skin="Vista" runat="server" Culture="en-US" LabelCssClass="">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="dvEmp_Temperature" runat="server" visible="false">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblEmp_Temperature" runat="server" Text="Temperature Limit" meta:resourcekey="lblEmp_TemperatureResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                             <telerik:RadNumericTextBox ID="RadnumEmp_Temperature" MinValue="2" MaxValue="365"
                                            Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="RadnumEmp_TemperatureResource1">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="rfvEmp_Temperature" runat="server" ControlToValidate="radnumAbsentDays"
                                            Display="None" ErrorMessage="Please Enter Temperature Limit °C" ValidationGroup="grpUpdate"
                                            meta:resourcekey="rfvEmp_TemperatureResource1" Enabled="false">
                                        </asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceEmp_Temperature" runat="server" CssClass="AISCustomCalloutStyle"
                                            TargetControlID="rfvEmp_Temperature" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:Label ID="lblcelsiusTemperature" runat="server" Text="Celsius (°C)" meta:resourcekey="lblcelsiusTemperatureResource1"></asp:Label>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabEmailTemplate" runat="server" HeaderText="Email Template" meta:resourcekey="TabEmailTemplateResource1">
                            <HeaderTemplate>
                                <asp:Label runat="server" ID="ETHeader" Text="Email Template" meta:resourcekey="TabEmailTemplateResource2"></asp:Label>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblEmailParameters" runat="server" Text="Parameters" CssClass="Profiletitletxt"
                                            meta:resourcekey="lblEmailParametersResource1" />
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadComboBox ID="RadCmbBxParameters" MarkFirstMatch="True" Skin="Vista" runat="server"
                                            ValidationGroup="ValidateEmailParameters" meta:resourcekey="RadCmbBxParametersResource1">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="rfvCEmailParameters" runat="server" ControlToValidate="RadCmbBxParameters"
                                            Display="None" ErrorMessage="Please Select Email Parameter" ValidationGroup="ValidateEmailParameters"
                                            meta:resourcekey="rfvCompaniesResource1" InitialValue="-1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceEmailParameters" runat="server" CssClass="AISCustomCalloutStyle"
                                            TargetControlID="rfvCEmailParameters" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button" ValidationGroup="ValidateEmailParameters"
                                            meta:resourcekey="btnAddResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblEmailTemplateEn" runat="server" Text="English Email Template" CssClass="Profiletitletxt"
                                            meta:resourcekey="lblEmailTemplateEnResource1" />
                                    </div>
                                    <div class="col-md-10 table-responsive">
                                        <FTB:FreeTextBox ID="txtEmailTemplateEn" runat="server" />
                                        <asp:HiddenField ID="hdnFocusName" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfvEmailTemplateEn" runat="server" ControlToValidate="txtEmailTemplateEn"
                                            Display="None" ErrorMessage="Please enter english email template" ValidationGroup="validateEmployeeComp"
                                            meta:resourcekey="rfvEmailTemplateEnResource1" />
                                        <cc1:ValidatorCalloutExtender ID="vceEmailTemplateEn" runat="server" Enabled="True"
                                            TargetControlID="rfvEmailTemplateEn">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblEmailParametersArb" runat="server" Text="Arabic Parameters" CssClass="Profiletitletxt"
                                            meta:resourcekey="lblEmailParametersArbResource1" />
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadComboBox ID="RadCmbBxParametersArb" runat="server" MarkFirstMatch="True"
                                            meta:resourceKey="RadCmbBxParametersResource1" Skin="Vista" ValidationGroup="ValidateEmailParameters">
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button ID="btnAddArb" runat="server" CssClass="button" meta:resourceKey="btnAddResource1"
                                            Text="Add" ValidationGroup="ValidateEmailParameters" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblEmailTemplateAR" runat="server" Text="Arabic Email Template" CssClass="Profiletitletxt"
                                            meta:resourcekey="lblEmailTemplateARResource1" Width="150px" />
                                    </div>
                                    <div class="col-md-10 table-responsive">
                                        <FTB:FreeTextBox ID="txtEmailTemplateAr" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfvEmailTemplateAr" runat="server" ControlToValidate="txtEmailTemplateAr"
                                            Display="None" ErrorMessage="Please enter arabic email template" ValidationGroup="validateEmployeeComp"
                                            meta:resourcekey="rfvEmailTemplateArResource1" />
                                        <cc1:ValidatorCalloutExtender ID="vceEmailTemplateAr" runat="server" Enabled="True"
                                            TargetControlID="rfvEmailTemplateAr">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabSMSTemplate" runat="server" HeaderText="SMS Template" meta:resourcekey="TabSMSTemplateResource1">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblSMSParameters" runat="server" Text="Parameters" CssClass="Profiletitletxt"
                                            meta:resourcekey="lblSMSParametersResource1" />
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadComboBox ID="RadSMSCmbBxParameters" MarkFirstMatch="True" Skin="Vista"
                                            runat="server" ValidationGroup="ValidateSMSParameters" meta:resourcekey="RadSMSCmbBxParametersResource1">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="rfvSMSParameters" runat="server" ControlToValidate="RadSMSCmbBxParameters"
                                            Display="None" ErrorMessage="Please Select SMS Parameter" ValidationGroup="ValidateSMSParameters"
                                            meta:resourcekey="rfvCompaniesResource1" InitialValue="-1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="vceSMSParameters" runat="server" CssClass="AISCustomCalloutStyle"
                                            TargetControlID="rfvSMSParameters" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button ID="btnSMSAdd" runat="server" Text="Add" CssClass="button" ValidationGroup="ValidateSMSParameters"
                                            meta:resourcekey="btnSMSAddResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblSMSTemplateEn" runat="server" Text="English SMS Template" CssClass="Profiletitletxt"
                                            meta:resourcekey="lblSMSTemplateEnResource1" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSMSTemplateEn" runat="server" TextMode="MultiLine" Rows="5" Columns="45"
                                            onfocus="SetFocusText(this); return false;" meta:resourcekey="txtSMSTemplateEnResource1" />
                                        <asp:RequiredFieldValidator ID="rfvSMSTemplateEn" runat="server" ControlToValidate="txtSMSTemplateEn"
                                            Display="None" ErrorMessage="Please enter english SMS template" ValidationGroup="validateEmployeeComp"
                                            meta:resourcekey="rfvSMSTemplateEnResource1" />
                                        <cc1:ValidatorCalloutExtender ID="vceSMSTemplateEn" runat="server" Enabled="True"
                                            TargetControlID="rfvSMSTemplateEn">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblSMSParameters0" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblEmailParametersArbResource1"
                                            Text="Parameters" />
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadComboBox ID="RadSMSCmbBxParametersArb" runat="server" MarkFirstMatch="True"
                                            meta:resourceKey="RadSMSCmbBxParametersResource1" Skin="Vista" ValidationGroup="ValidateSMSParameters">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="rfvSMSParameters0" runat="server" ControlToValidate="RadSMSCmbBxParametersArb"
                                            Display="None" ErrorMessage="Please Select SMS Parameter" InitialValue="-1" meta:resourceKey="rfvCompaniesResource1"
                                            ValidationGroup="ValidateSMSParameters"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="rfvSMSParameters0_ValidatorCalloutExtender" runat="server"
                                            CssClass="AISCustomCalloutStyle" Enabled="True" TargetControlID="rfvSMSParameters0"
                                            WarningIconImageUrl="~/images/warning1.png">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button ID="btnSMSAddArb" runat="server" CssClass="button" meta:resourceKey="btnSMSAddResource1"
                                            Text="Add" ValidationGroup="ValidateSMSParameters" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="lblSMSTemplateAR" runat="server" Text="Arabic SMS Template" CssClass="Profiletitletxt"
                                            meta:resourcekey="lblSMSTemplateARResource1" Width="130px" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtSMSTemplateAr" runat="server" TextMode="MultiLine" Rows="5" Columns="45"
                                            onfocus="SetFocusText(this); return false;" meta:resourcekey="txtSMSTemplateArResource1" />
                                        <asp:RequiredFieldValidator ID="rfvSMSTemplateAr" runat="server" ControlToValidate="txtSMSTemplateAr"
                                            Display="None" ErrorMessage="Please enter arabic SMS template" ValidationGroup="validateEmployeeComp"
                                            meta:resourcekey="rfvSMSTemplateArResource1" />
                                        <cc1:ValidatorCalloutExtender ID="vceSMSTemplateAr" runat="server" Enabled="True"
                                            TargetControlID="rfvSMSTemplateAr">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="button" Style="margin-top: 10px"
                                meta:resourcekey="btnUpdateResource1" ValidationGroup="grpUpdate" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" Style="margin-top: 10px"
                                meta:resourcekey="btnCancelResource1" />
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
            </td> </tr> </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
