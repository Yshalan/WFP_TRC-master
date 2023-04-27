<%@ Page Language="VB" StylesheetTheme="Default" MasterPageFile="~/Default/EmptyMaster.master" AutoEventWireup="false"
    CodeFile="TAPolicyPopup.aspx.vb" Inherits="Admin_TAPolicyPopup" meta:resourcekey="PageResource1"
    UICulture="auto" Theme="SvTheme" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <uc1:PageHeader ID="PageHeader1" runat="server" />
        </div>
    </div>
    <div class="row">
        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" meta:resourcekey="TabContainer1Resource1">
            <cc1:TabPanel ID="Tab1" runat="server" HeaderText="TA Policy" TabIndex="0" meta:resourcekey="Tab1Resource1">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="TA Policy Name English"
                                meta:resourcekey="Label1Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtPolicyEnglish" runat="server" meta:resourcekey="txtPolicyEnglishResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label2" runat="server" CssClass="Profiletitletxt" Text="TA Policy Name Arabic"
                                meta:resourcekey="Label2Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtPolicyArabic" runat="server" meta:resourcekey="txtPolicyArabicResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblHasLaunchBreak" runat="server" Text="Has Launch Break" CssClass="Profiletitletxt"
                                meta:resourcekey="lblHasLaunchBreakResource1" />
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chkHasLaunchBreak" runat="server" AutoPostBack="true" />
                        </div>
                    </div>
                    <div class="row" id="trLaunchBreak" visible="false" runat="server">
                        <div class="col-md-2">
                            <asp:Label ID="lblLaunchBreakDuration" runat="server" Text="Launch Break Duration"
                                CssClass="Profiletitletxt" meta:resourcekey="lblLaunchBreakDurationResource1" />
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="txtLaunchBreakDuration" runat="server" DataType="System.Int64"
                                Culture="English (United States)" LabelCssClass="">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="row" id="trCompensateLaunchbreak" visible="false" runat="server">
                        <div class="col-md-2">
                            <asp:Label ID="lblCompensateLaunchbreak" runat="server" Text="Compensate Launch Break"
                                CssClass="Profiletitletxt" meta:resourcekey="lblCompensateLaunchbreakResource1" />
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chkCompensateLaunchbreak" runat="server" />
                        </div>
                    </div>
                    <div class="row" id="trLaunchbreakReason" visible="false" runat="server">
                        <div class="col-md-2">
                            <asp:Label ID="lblLaunchbreakReason" runat="server" Text="Launch Break Reason" CssClass="Profiletitletxt"
                                meta:resourcekey="lblLaunchbreakReasonResource1" />
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="ddlLaunchbreakReason" runat="server" MarkFirstMatch="true"
                                AppendDataBoundItems="True" Skin="Vista" CausesValidation="False" meta:resourcekey="ddlLaunchbreakReason" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblHasPrayTime" runat="server" Text="Has Pray Time" CssClass="Profiletitletxt"
                                meta:resourcekey="lblHasPrayTimeResource1" />
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chkHasPrayTime" runat="server" AutoPostBack="true" />
                        </div>
                    </div>
                    <div class="row" runat="server" id="trPrayTime" visible="false">
                        <div class="col-md-2">
                            <asp:Label ID="lblPrayTimeDuration" runat="server" Text="Pray Time Duration" CssClass="Profiletitletxt"
                                meta:resourcekey="lblPrayTimeDurationResource1" />
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="txtPrayTimeDuration" runat="server" DataType="System.Int64"
                                Culture="English (United States)" LabelCssClass="">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>

                    <div class="row" id="trCompensatePrayTime" visible="false" runat="server">
                        <div class="col-md-2">
                            <asp:Label ID="lblCompensatePrayTime" runat="server" Text="Compensate Pray Time"
                                CssClass="Profiletitletxt" meta:resourcekey="lblCompensatePrayTimeResource1" />
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chkCompensatePrayTime" runat="server" />
                        </div>
                    </div>
                    <div class="row" runat="server" id="trPrayTimeReason" visible="false">
                        <div class="col-md-2">
                            <asp:Label ID="lblPrayTimeReason" runat="server" Text="Pray Time Reason" CssClass="Profiletitletxt"
                                meta:resourcekey="lblPrayTimeReasonResource1" />
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="ddlPrayTimeReason" runat="server" MarkFirstMatch="true"
                                AppendDataBoundItems="True" Skin="Vista" CausesValidation="False" meta:resourcekey="ddlPrayTimeReasonReason" />
                        </div>
                    </div>
                    <div class="row" id="trGraceIn" runat="server">
                        <div class="col-md-2">
                            <asp:Label ID="Label3" runat="server" CssClass="Profiletitletxt" Text="Grace In Mins"
                                meta:resourcekey="Label3Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="txtGraceIn" runat="server" DataType="System.Int64"
                                LabelCssClass="" Culture="English (United States)" meta:resourcekey="txtGraceInResource1">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="row" id="trGraceOut" runat="server">
                        <div class="col-md-2">
                            <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="Grace Out Mins"
                                meta:resourcekey="Label4Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="txtGraceOut" runat="server" DataType="System.Int64"
                                LabelCssClass="" Culture="English (United States)" meta:resourcekey="txtGraceOutResource1">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblIsFromGrace" runat="server" CssClass="Profiletitletxt" Text="Delay Is From Grace"
                                meta:resourcekey="lblIsFromGraceResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chkDelalIsFromGrace" runat="server" meta:resourcekey="chkDelalIsFromGraceResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblEarlyOutIsFromGrace" runat="server" CssClass="Profiletitletxt"
                                Text="Early Out Is From Grace" meta:resourcekey="lblEarlyOutIsFromGraceResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chkEarlyOutisFromGrace" runat="server" meta:resourcekey="chkEarlyOutisFromGraceResource1" />
                        </div>
                    </div>

                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="Tab2" runat="server" HeaderText="Absent Rules" TabIndex="1" meta:resourcekey="Tab2Resource1">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="Absent Rule Type"
                                meta:resourcekey="Label5Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="ddlAbsentRuleType" runat="server" AutoPostBack="True" meta:resourcekey="ddlAbsentRuleTypeResource1">
                                <Items>
                                    <telerik:RadComboBoxItem Value="0" Text="--Please Select--" runat="server" meta:resourcekey="RadComboBoxItemResource1" />
                                    <telerik:RadComboBoxItem Value="1" Text="One Day Delay Limit" runat="server" meta:resourcekey="RadComboBoxItemResource2" />
                                    <telerik:RadComboBoxItem Value="2" Text="Consecutive Delays" runat="server" meta:resourcekey="RadComboBoxItemResource3" />
                                    <telerik:RadComboBoxItem Value="3" Text="No. of delays per period" runat="server"
                                        meta:resourcekey="RadComboBoxItemResource4" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label6" runat="server" CssClass="Profiletitletxt" Text="English Name"
                                meta:resourcekey="Label6Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtAbsentRuleEnglishName" runat="server" meta:resourcekey="txtAbsentRuleEnglishNameResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label7" runat="server" CssClass="Profiletitletxt" Text="Arabic Name"
                                meta:resourcekey="Label7Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtAbsentRuleArabichName" runat="server" meta:resourcekey="txtAbsentRuleArabichNameResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblVariable1" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblVariable1Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="txtVar1" runat="server" DataType="System.Int64" Culture="English (United States)"
                                LabelCssClass="" meta:resourcekey="txtVar1Resource1">
                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblVariable2" runat="server" CssClass="Profiletitletxt" Text="Variable 2"
                                meta:resourcekey="lblVariable2Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="txtVar2" runat="server" DataType="System.Int64" Culture="English (United States)"
                                LabelCssClass="" meta:resourcekey="txtVar2Resource1">
                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <telerik:RadGrid ID="dgrdAbsentRules" runat="server" AllowPaging="True" Skin="Hay"
                            GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True"
                            meta:resourcekey="dgrdAbsentRulesResource1">
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="AbsentRuleId,RuleName,RuleArabicName,AbsentRuleType,Variable1,Variable2">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource1" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="RuleName" SortExpression="RuleName" HeaderText="English Name"
                                        UniqueName="RuleName" meta:resourcekey="GridBoundColumnResource1" />
                                    <telerik:GridBoundColumn DataField="RuleArabicName" SortExpression="RuleArabicName"
                                        HeaderText="Arabic Name" UniqueName="RuleArabicName" meta:resourcekey="GridBoundColumnResource2" />
                                    <telerik:GridBoundColumn DataField="AbsentRuleId" SortExpression="AbsentRuleId" Visible="False"
                                        UniqueName="AbsentRuleId" meta:resourcekey="GridBoundColumnResource3" />
                                    <telerik:GridBoundColumn DataField="FK_TAPolicyId" SortExpression="FK_TAPolicyId"
                                        Visible="False" UniqueName="FK_TAPolicyId" meta:resourcekey="GridBoundColumnResource4" />
                                    <telerik:GridBoundColumn DataField="AbsentRuleType" SortExpression="AbsentRuleType"
                                        Visible="False" UniqueName="AbsentRuleType" meta:resourcekey="GridBoundColumnResource5" />
                                    <telerik:GridBoundColumn DataField="Variable1" SortExpression="Variable1" Visible="False"
                                        UniqueName="Variable1" meta:resourcekey="GridBoundColumnResource6" />
                                    <telerik:GridBoundColumn DataField="Variable2" SortExpression="Variable2" Visible="False"
                                        UniqueName="Variable2" meta:resourcekey="GridBoundColumnResource7" />
                                </Columns>
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            </MasterTableView>
                            <SelectedItemStyle ForeColor="Maroon" />
                        </telerik:RadGrid>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Violations" TabIndex="2"
                meta:resourcekey="TabPanel1Resource1">
                <ContentTemplate>

                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label8" runat="server" CssClass="Profiletitletxt" Text="Violation Rule Type"
                                meta:resourcekey="Label8Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="ddlViolationRuleType" runat="server" AutoPostBack="True"
                                MarkFirstMatch="true" meta:resourcekey="ddlViolationRuleTypeResource1">
                                <Items>
                                    <telerik:RadComboBoxItem Value="0" Text="--Please Select--" runat="server" meta:resourcekey="RadComboBoxItemResource5" />
                                    <telerik:RadComboBoxItem Value="1" Text="One Day Delay Limit" runat="server" Visible="false"
                                        meta:resourcekey="RadComboBoxItemResource6" />
                                    <telerik:RadComboBoxItem Value="2" Text="Consecutive Delays" runat="server" Visible="false"
                                        meta:resourcekey="RadComboBoxItemResource7" />
                                    <telerik:RadComboBoxItem Value="3" Text="No. of delays per period" runat="server"
                                        meta:resourcekey="RadComboBoxItemResource8" />
                                    <telerik:RadComboBoxItem Value="4" Text="One Absent Day" Visible="false" runat="server"
                                        meta:resourcekey="RadComboBoxItemResource9" />
                                    <telerik:RadComboBoxItem Value="5" Text="Consecutive Absent Days" runat="server"
                                        Visible="false" meta:resourcekey="RadComboBoxItemResource10" />
                                    <telerik:RadComboBoxItem Value="6" Text="Absent days per period" runat="server" meta:resourcekey="RadComboBoxItemResource11" />
                                    <telerik:RadComboBoxItem Value="7" Text="One Day Early Out Limit" Visible="false"
                                        runat="server" meta:resourcekey="RadComboBoxItemResource12" />
                                    <telerik:RadComboBoxItem Value="8" Text="Consecutive Early Outs" Visible="false"
                                        runat="server" meta:resourcekey="RadComboBoxItemResource13" />
                                    <telerik:RadComboBoxItem Value="9" Text="No. of Early Outs Per Period" runat="server"
                                        meta:resourcekey="RadComboBoxItemResource14" />
                                    <telerik:RadComboBoxItem Value="10" Text="Missing In - Missing Out Per Period" runat="server"
                                        meta:resourcekey="RadComboBoxItemResource15" />
                                    <telerik:RadComboBoxItem Value="11" Text="Delay or Early Out Per Period" runat="server"
                                        meta:resourcekey="RadComboBoxItemResource16" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label9" runat="server" CssClass="Profiletitletxt" Text="English Name"
                                meta:resourcekey="Label9Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtViolationEn" runat="server" meta:resourcekey="txtViolationEnResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label10" runat="server" CssClass="Profiletitletxt" Text="Arabic Name"
                                meta:resourcekey="Label10Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtViolationAr" runat="server" meta:resourcekey="txtViolationArResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblViolationVAr1" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblViolationVAr1Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="txtViolationVar1" runat="server" DataType="System.Int64"
                                Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtViolationVar1Resource1">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblViolationVAr3" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblViolationVAr3Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadNumericTextBox ID="txtViolationVar3" runat="server" DataType="System.Int64"
                                Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtViolationVar1Resource1">
                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                            </telerik:RadNumericTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblViolationVAr2" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblViolationVAr2Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="ddlViolationVAr2" runat="server" AutoPostBack="True" MarkFirstMatch="true"
                                meta:resourcekey="ddlViolationVAr2Resource1">
                                <Items>
                                    <telerik:RadComboBoxItem Value="0" Text="--Please Select--" runat="server" meta:resourcekey="RadComboBoxItemResource5" />
                                    <telerik:RadComboBoxItem Value="1" Text="One Month" runat="server" meta:resourcekey="RadComboBoxItemResource17" />
                                    <telerik:RadComboBoxItem Value="3" Text="Three Months" runat="server" meta:resourcekey="RadComboBoxItemResource18" />
                                    <telerik:RadComboBoxItem Value="6" Text="Six Months" runat="server" meta:resourcekey="RadComboBoxItemResource19" />
                                    <telerik:RadComboBoxItem Value="12" Text="One Year" runat="server" meta:resourcekey="RadComboBoxItemResource20" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label13" runat="server" CssClass="Profiletitletxt" Text="Violation Action"
                                meta:resourcekey="Label13Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="ddlViolationAction" runat="server" MarkFirstMatch="true"
                                MaxHeight="200px" meta:resourcekey="ddlViolationActionResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblViolationAction2" runat="server" CssClass="Profiletitletxt" Text="Violation Action"
                                meta:resourcekey="lblViolationAction2Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="ddlViolationAction2" runat="server" MarkFirstMatch="true"
                                MaxHeight="200px" meta:resourcekey="ddlViolationAction2Resource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblViolationAction3" runat="server" CssClass="Profiletitletxt" Text="Violation Action"
                                meta:resourcekey="lblViolationAction3Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="ddlViolationAction3" runat="server" MarkFirstMatch="true"
                                MaxHeight="200px" meta:resourcekey="ddlViolationAction3Resource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblViolationAction4" runat="server" CssClass="Profiletitletxt" Text="Violation Action"
                                meta:resourcekey="lblViolationAction4Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="ddlViolationAction4" runat="server" MarkFirstMatch="true"
                                MaxHeight="200px" meta:resourcekey="ddlViolationAction4Resource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblViolationAction5" runat="server" CssClass="Profiletitletxt" Text="Violation Action"
                                meta:resourcekey="lblViolationAction5Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="ddlViolationAction5" runat="server" MarkFirstMatch="true"
                                MaxHeight="200px" meta:resourcekey="ddlViolationAction5Resource1" />
                        </div>
                    </div>
                    <div class="table-responsive">
                        <telerik:RadGrid ID="dgrdViolation" runat="server" AllowPaging="True" Skin="Hay"
                            GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True"
                            PageSize="25" meta:resourcekey="dgrdViolationResource1">
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="ViolationId,ViolationName,ViolationArabicName,ViolationRuleType,Variable1,Variable2,Variable3,FK_ViolationActionId,FK_ViolationActionId2,FK_ViolationActionId3,FK_ViolationActionId4,FK_ViolationActionId5">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource3"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource2" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="ViolationName" SortExpression="ViolationName"
                                        HeaderText="English Name" UniqueName="ViolationName" meta:resourcekey="GridBoundColumnResource8" />
                                    <telerik:GridBoundColumn DataField="ViolationArabicName" SortExpression="ViolationArabicName"
                                        HeaderText="Arabic Name" UniqueName="ViolationArabicName" meta:resourcekey="GridBoundColumnResource9" />
                                    <telerik:GridBoundColumn DataField="ViolationId" SortExpression="ViolationId" Visible="False"
                                        UniqueName="ViolationId" meta:resourcekey="GridBoundColumnResource10" />
                                    <telerik:GridBoundColumn DataField="FK_TAPolicyId" SortExpression="FK_TAPolicyId"
                                        Visible="False" UniqueName="FK_TAPolicyId" meta:resourcekey="GridBoundColumnResource11" />
                                    <telerik:GridBoundColumn DataField="ViolationRuleType" SortExpression="ViolationRuleType"
                                        Visible="False" UniqueName="ViolationRuleType" meta:resourcekey="GridBoundColumnResource12" />
                                    <telerik:GridBoundColumn DataField="Variable1" SortExpression="Variable1" Visible="False"
                                        UniqueName="Variable1" meta:resourcekey="GridBoundColumnResource13" />
                                    <telerik:GridBoundColumn DataField="Variable2" SortExpression="Variable2" Visible="False"
                                        UniqueName="Variable2" meta:resourcekey="GridBoundColumnResource14" />
                                    <telerik:GridBoundColumn DataField="Variable3" SortExpression="Variable2" Visible="False"
                                        UniqueName="Variable3" meta:resourcekey="GridBoundColumnResource14" />
                                    <telerik:GridBoundColumn DataField="FK_ViolationActionId" SortExpression="FK_ViolationActionId"
                                        Visible="False" UniqueName="FK_ViolationActionId" meta:resourcekey="GridBoundColumnResource15" />
                                    <telerik:GridBoundColumn DataField="FK_ViolationActionId2" SortExpression="FK_ViolationActionId"
                                        Visible="False" UniqueName="FK_ViolationActionId2" meta:resourcekey="GridBoundColumnResource15" />
                                    <telerik:GridBoundColumn DataField="FK_ViolationActionId3" SortExpression="FK_ViolationActionId"
                                        Visible="False" UniqueName="FK_ViolationActionId3" meta:resourcekey="GridBoundColumnResource15" />
                                    <telerik:GridBoundColumn DataField="FK_ViolationActionId4" SortExpression="FK_ViolationActionId"
                                        Visible="False" UniqueName="FK_ViolationActionId4" meta:resourcekey="GridBoundColumnResource15" />
                                    <telerik:GridBoundColumn DataField="FK_ViolationActionId5" SortExpression="FK_ViolationActionId"
                                        Visible="False" UniqueName="FK_ViolationActionId5" meta:resourcekey="GridBoundColumnResource15" />
                                    <telerik:GridTemplateColumn HeaderText="Rule Type" meta:resourcekey="GridTemplateColumnResource4"
                                        UniqueName="TemplateColumn1" DataField="TemplateColumn1">
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            </MasterTableView>
                            <SelectedItemStyle ForeColor="Maroon" />
                        </telerik:RadGrid>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
</asp:Content>
