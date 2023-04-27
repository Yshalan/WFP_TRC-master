<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OvertimeRulesPopUp.aspx.vb" MasterPageFile="~/Default/EmptyMaster.master"
    Inherits="Admin_OvertimeRulesPopUp" meta:resourcekey="PageResource1" UICulture="auto" Theme="SvTheme" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">

                <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="640px"
                    Enabled="False" meta:resourcekey="TabContainer1Resource1">
                    <cc1:TabPanel ID="Tab1" runat="server" HeaderText="Overtime Rule" meta:resourcekey="Tab1Resource1">
                        <ContentTemplate>

                            <div class="row">
                                <div class="col-md-2"> <asp:Label ID="lblEngname" runat="server" CssClass="Profiletitletxt" Text="English Name"
                                            meta:resourcekey="lblEngnameResource1"></asp:Label></div>
                                <div class="col-md-4"><asp:TextBox ID="TxtRuleName" runat="server" meta:resourcekey="TxtRuleNameResource1"></asp:TextBox></div>
                            </div>
                                                        <div class="row">
                                <div class="col-md-2"><asp:Label ID="lblRuleArName" runat="server" CssClass="Profiletitletxt" Text="Arabic Name"
                                            meta:resourcekey="lblRuleArNameResource1"></asp:Label></div>
                                <div class="col-md-4"> <asp:TextBox ID="txtRuleArName" runat="server" meta:resourcekey="txtRuleArNameResource1"></asp:TextBox></div>
                            </div>
                                                        <div class="row">
                                <div class="col-md-2"> <asp:Label ID="lblOvertimeEligibility" runat="server" CssClass="Profiletitletxt"
                                            Text="Overtime Eligibility" meta:resourcekey="lblOvertimeEligibilityResource1"></asp:Label></div>
                                <div class="col-md-4"> <asp:RadioButtonList ID="rdbOTEligibility" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="True" meta:resourcekey="rdbOTEligibilityResource1">
                                            <asp:ListItem Text="Overtime Allowed" Value="1" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                            <asp:ListItem Text="Overtime Not Allowed" Value="0" Selected="True" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                        </asp:RadioButtonList></div>
                            </div>

                            <table width="100%">

                                <div id="dvOtEligibility" runat="server" visible="False">
                                    <div class="row">
        <div class="col-md-2"> <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="Minimum Overtime Duration/Mins"
                                                meta:resourcekey="Label4Resource1"></asp:Label></div>
                                <div class="col-md-4"> <telerik:RadNumericTextBox ID="TxtMinOvertime" runat="server" Culture="English (United States)"
                                                MinValue="0" DataType="System.Int32" Skin="Vista" LabelCssClass="" meta:resourcekey="TxtMinOvertimeResource1">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" GroupSizes="1" />
                                            </telerik:RadNumericTextBox></div>

                                    </div>
                                                                                            <div class="row">
                                <div class="col-md-2">  <asp:Label ID="lblApprovalRequired" runat="server" CssClass="Profiletitletxt" Text="Approval Required To Be Considered"
                                                meta:resourcekey="lblApprovalRequiredResource1"></asp:Label></div>
                                <div class="col-md-4"><asp:RadioButtonList ID="rdbApprovalReqd" runat="server" RepeatDirection="Horizontal"
                                                meta:resourcekey="rdbApprovalReqdResource1">
                                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0" Selected="True" meta:resourcekey="ListItemResource4"></asp:ListItem>
                                            </asp:RadioButtonList></div>
                            </div>
                                                                                           <div class="row">
                                <div class="col-md-2"> <asp:Label ID="lblOffDay" runat="server" CssClass="Profiletitletxt" Text="Consider Off Day"
                                                meta:resourcekey="lblOffDayResource1"></asp:Label></div>
                                <div class="col-md-4"><asp:RadioButtonList ID="rdbconsiderOffDay" runat="server" RepeatDirection="Horizontal"
                                                meta:resourcekey="rdbconsiderOffDayResource1">
                                                <asp:ListItem Text="High" Value="1" meta:resourcekey="ListItemResource5"></asp:ListItem>
                                                <asp:ListItem Text="Low" Value="0" Selected="True" meta:resourcekey="ListItemResource6"></asp:ListItem>
                                            </asp:RadioButtonList></div>
                            </div>
                                                          <div class="row">
                                <div class="col-md-2"> <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="Consider Holiday"
                                                meta:resourcekey="Label1Resource1"></asp:Label></div>
                                <div class="col-md-4"> <asp:RadioButtonList ID="rdbConsiderHoliday" runat="server" RepeatDirection="Horizontal"
                                                meta:resourcekey="rdbConsiderHolidayResource1">
                                                <asp:ListItem Text="High" Value="1" meta:resourcekey="ListItemResource7"></asp:ListItem>
                                                <asp:ListItem Text="Low" Value="0" Selected="True" meta:resourcekey="ListItemResource8"></asp:ListItem>
                                            </asp:RadioButtonList></div>
                            </div>
                                                                                            <div class="row">
                                <div class="col-md-2"> <asp:Label ID="Label2" runat="server" CssClass="Profiletitletxt" Text="Overtime Compensate Late Time"
                                                meta:resourcekey="Label2Resource1"></asp:Label></div>
                                <div class="col-md-4"><asp:RadioButtonList ID="rdbOtCompLateTime" runat="server" RepeatDirection="Horizontal"
                                                meta:resourcekey="rdbOtCompLateTimeResource1">
                                                <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource9"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0" Selected="True" meta:resourcekey="ListItemResource10"></asp:ListItem>
                                            </asp:RadioButtonList></div>
                            </div>
                                                                                           <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-4"><asp:RadioButtonList ID="rdbOTLeaveOrFinance" runat="server" meta:resourcekey="rdbOTLeaveOrFinanceResource1">
                                                <asp:ListItem Text="Overtime To Be Added To Leave Balance " Value="1" Selected="True"
                                                    meta:resourcekey="ListItemResource11"></asp:ListItem>
                                                <asp:ListItem Text="Overtime To Be Consider As Financial" Value="0" meta:resourcekey="ListItemResource12"></asp:ListItem>
                                            </asp:RadioButtonList></div>
                            </div>
                                  
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblHighRate" runat="server" CssClass="Profiletitletxt" Text="High Rate"
                                                meta:resourcekey="lblHighRateResource1"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="TxtHighRate" runat="server" Culture="English (United States)"
                                                Skin="Vista" LabelCssClass="" meta:resourcekey="TxtHighRateResource1">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" GroupSizes="1" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblLowRate" runat="server" CssClass="Profiletitletxt" Text="Low Rate"
                                                meta:resourcekey="lblLowRateResource1"></asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="TxtLowRate" runat="server" Culture="English (United States)"
                                                Skin="Vista" LabelCssClass="" meta:resourcekey="TxtLowRateResource1">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="" GroupSizes="1" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                </div>
                            </table>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="Tab2" runat="server" HeaderText="High Time" meta:resourcekey="Tab2Resource1">
                        <ContentTemplate>
                            <table width="400px" align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label6" runat="server" CssClass="Profiletitletxt" Text="Consider Specific Time As High Overtime "
                                            meta:resourcekey="Label6Resource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rdbHighHasTime" runat="server" RepeatDirection="Horizontal"
                                            AutoPostBack="True" Enabled="False" meta:resourcekey="rdbHighHasTimeResource1">
                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource13"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="0" Selected="True" meta:resourcekey="ListItemResource14"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <div id="dvHasTime" runat="server" visible="False">
                                <table width="400px" align="center">
                                    <tr>
                                        <td>
                                            <telerik:RadGrid ID="dgrdHighTime" runat="server" AllowPaging="True" Width="350px"
                                                PageSize="5" Skin="Hay" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                                ShowFooter="True" meta:resourcekey="dgrdHighTimeResource1">
                                                <SelectedItemStyle ForeColor="Maroon" />
                                                <MasterTableView IsFilterItemExpanded="False" AutoGenerateColumns="False">
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="HighTimeId" Visible="false" />
                                                        <telerik:GridBoundColumn DataField="FromTime" HeaderText="From Time" />
                                                        <telerik:GridBoundColumn DataField="ToTime" HeaderText="To Time" />
                                                        <telerik:GridBoundColumn DataField="FK_RuleId" Visible="false" />
                                                    </Columns>
                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                </MasterTableView>
                                                <GroupingSettings CaseSensitive="False" />
                                                <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

