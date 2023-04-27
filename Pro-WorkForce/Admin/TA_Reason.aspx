<%@ Page Title="TA Reasons" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="TA_Reason.aspx.vb" Inherits="Emp_TA_Reason"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlTaReason" runat="server">
        <ContentTemplate>

            <uc1:PageHeader ID="PageHeaderTaReason" HeaderText="Time Attendance Reason" runat="server" />

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblReasonCode" runat="server" CssClass="Profiletitletxt" Text="Code"
                        meta:resourcekey="lblReasonCodeResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadNumericTextBox ID="txtRadReasonCode" runat="server" MaxValue="999999999"
                        MinValue="0" Culture="English (United States)" LabelCssClass="" meta:resourcekey="txtRadReasonCodeResource1">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="reqReasonCode" runat="server" ControlToValidate="txtRadReasonCode"
                        Display="None" ErrorMessage="Please enter reason code" ValidationGroup="ReasonCodeGroup"
                        meta:resourcekey="reqReasonCodeResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtendereqReasonCode" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="reqReasonCode" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblReasonName" runat="server" CssClass="Profiletitletxt" Text="English Name"
                        meta:resourcekey="lblReasonNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtReasonName" runat="server" meta:resourcekey="txtReasonNameResource1"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtReasonName" ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{0,50}$" runat="server"
                        ErrorMessage="Maximum 50 characters allowed." ValidationGroup="ReasonCodeGroup"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="revtxtReasonName" Display="Dynamic"
                        ValidationGroup="ReasonCodeGroup" runat="server"
                        ErrorMessage="Special Characters are not allowed!"
                        ValidationExpression="[^~`!@#$%\^&\*\(\)\-+=\\\|\}\]\{\['&quot;:?.>,</]+" ControlToValidate="txtReasonName">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="reqResonName" runat="server" ErrorMessage="Please enter reason english name"
                        Display="None" ControlToValidate="txtReasonName" ValidationGroup="ReasonCodeGroup"
                        meta:resourcekey="reqResonNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderResonName" TargetControlID="reqResonName"
                        runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblReasonArabicName" runat="server" CssClass="Profiletitletxt" Text="Arabic Name"
                        meta:resourcekey="lblReasonArabicNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtReasonArabicName" runat="server" meta:resourcekey="txtReasonArabicNameResource1"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtReasonArabicName" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{0,50}$" runat="server"
                        ErrorMessage="Maximum 50 characters allowed." ValidationGroup="ReasonCodeGroup"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="revtxtReasonArabicName" Display="Dynamic"
                        ValidationGroup="ReasonCodeGroup" runat="server" ErrorMessage="Special Characters are not allowed!"
                        ValidationExpression="[^~`!@#$%\^&\*\(\)\-+=\\\|\}\]\{\['&quot;:?.>,</]+" ControlToValidate="txtReasonArabicName">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="reqReasonArName" runat="server" ErrorMessage="Please enter reason arabic name"
                        Display="None" ControlToValidate="txtReasonArabicName" ValidationGroup="ReasonCodeGroup"
                        meta:resourcekey="reqReasonArNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderReasonArName" TargetControlID="reqReasonArName"
                        runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkIsScheduleTiming" runat="server" Text="Schedule Timing" AutoPostBack="true" meta:resourcekey="chkIsScheduleTimingResource1" />
                </div>
            </div>
            <div class="row" id="dvType" runat="server">
                <div class="col-md-2">
                    <asp:Label ID="lblType" runat="server" CssClass="Profiletitletxt" Text="Type" meta:resourcekey="lblTypeResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadComboBoxType" runat="server" MarkFirstMatch="true" Width="210px"
                        AutoPostBack="True" meta:resourcekey="RadComboBoxTypeResource1">
                        <Items>
                            <telerik:RadComboBoxItem Value="I" Text="In" runat="server" meta:resourcekey="RadComboBoxItemResource1" />
                            <telerik:RadComboBoxItem Value="O" Text="Out" runat="server" meta:resourcekey="RadComboBoxItemResource2" />
                        </Items>
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvType" runat="server" Display="None" ErrorMessage="Please Select a Reason Type"
                        ControlToValidate="RadComboBoxType" InitialValue="--Please Select--" ValidationGroup="ReasonCodeGroup"
                        meta:resourcekey="rfvTypeResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="rfvType_ValidatorCalloutExtender" CssClass="AISCustomCalloutStyle"
                        runat="server" Enabled="True" TargetControlID="rfvType" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div id="dvFirstIn" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chkFirstIn" runat="server" Text="Consider As First In" meta:resourcekey="chkFirstInResource1"/>
                    </div>
                </div>
            </div>
            <div id="trIsInsideWork" runat="server">
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chkIsInsideWork" runat="server" Text="Consider Inside Work" meta:resourcekey="chkIsInsideWorkResource1"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chkLastOut" runat="server" Text="Consider As Last Out" meta:resourcekey="chkLastOutResource1"/>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="ReasonCodeGroup"
                        meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" runat="server" CssClass="button" Text="Delete" CausesValidation="False"
                        meta:resourcekey="btnDeleteResource1" />
                    <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="button"
                        Text="Clear" meta:resourcekey="btnClearResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdVwTaReason"
                        Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadGrid ID="dgrdVwTaReason" runat="server" AllowSorting="True" AllowPaging="True"
                        PageSize="25" GridLines="None" ShowStatusBar="True"
                        AllowMultiRowSelection="True" ShowFooter="True" OnItemCommand="dgrdVwTaReason_ItemCommand"
                        meta:resourcekey="dgrdVwTaReasonResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="ReasonCode">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                    UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ReasonCode" SortExpression="ReasonCode" HeaderText="Reason Code"
                                    meta:resourcekey="GridBoundColumnResource1" UniqueName="ReasonCode" />
                                <telerik:GridBoundColumn DataField="ReasonName" SortExpression="ReasonName" HeaderText="Reason English Name"
                                    meta:resourcekey="GridBoundColumnResource2" UniqueName="ReasonName" />
                                <telerik:GridBoundColumn DataField="ReasonArabicName" SortExpression="ReasonArabicName"
                                    HeaderText="Reason Arabic Name" meta:resourcekey="GridBoundColumnResource3" UniqueName="ReasonArabicName" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" Skin="Hay" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    meta:resourcekey="RadToolBar1Resource1">
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
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
