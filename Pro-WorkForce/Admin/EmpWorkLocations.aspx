<%@ Page Title="Work Locations" Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/AdminMaster.master"
    AutoEventWireup="false" CodeFile="EmpWorkLocations.aspx.vb" Inherits="Emp_EmpWorkLocations" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlWorkLocation" runat="server">
        <ContentTemplate>
            <table width="720" cellspacing="0" cellpadding="0">
                <tr align="left">
                    <td>
                        <div id="divWorkLocation" style="display: block">
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <uc1:PageHeader ID="userCtrlWorkLocation" HeaderText="Work Location" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCode" runat="server" CssClass="Profiletitletxt" Text="Code" 
                                                        meta:resourcekey="lblCodeResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWorkLocationCode" runat="server" 
                                                        meta:resourcekey="txtWorkLocationCodeResource1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqWorlLocationCode" runat="server" ControlToValidate="txtWorkLocationCode"
                                                        Display="None" ErrorMessage="Please enter work location code" 
                                                        ValidationGroup="WorkLocationGroup" 
                                                        meta:resourcekey="reqWorlLocationCodeResource1"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqWorlLocationCode" runat="server" CssClass="AISCustomCalloutStyle"
                                                        TargetControlID="reqWorlLocationCode" 
                                                        WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblName" runat="server" CssClass="Profiletitletxt" 
                                                        Text="English name" meta:resourcekey="lblNameResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWorkLocationName" runat="server" 
                                                        meta:resourcekey="txtWorkLocationNameResource1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqWorklocationName" runat="server" ErrorMessage="Please enter work location english name"
                                                        ValidationGroup="WorkLocationGroup" Display="None" 
                                                        ControlToValidate="txtWorkLocationName" 
                                                        meta:resourcekey="reqWorklocationNameResource1"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ExtenderWorkLocationName" TargetControlID="reqWorklocationName"
                                                        runat="server" CssClass="AISCustomCalloutStyle" 
                                                        WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblArabicName" runat="server" CssClass="Profiletitletxt" 
                                                        Text="Arabic name" meta:resourcekey="lblArabicNameResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWorkLocationArabicName" runat="server" 
                                                        meta:resourcekey="txtWorkLocationArabicNameResource1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqWorklocArName" runat="server" ErrorMessage="Please enter work location arabic name"
                                                        ValidationGroup="WorkLocationGroup" Display="None" 
                                                        ControlToValidate="txtWorkLocationArabicName" 
                                                        meta:resourcekey="reqWorklocArNameResource1"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ExtenderWorkLocationArabicName" TargetControlID="reqWorklocArName"
                                                        runat="server" CssClass="AISCustomCalloutStyle" 
                                                        WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPolicy" runat="server" CssClass="Profiletitletxt" 
                                                        Text="TA policy name" meta:resourcekey="lblPolicyResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="RadCmbBxPolicy" MarkFirstMatch="True"
                                                        Skin="Vista" runat="server" meta:resourcekey="RadCmbBxPolicyResource1">
                                                    </telerik:RadComboBox>
                                                    <asp:RequiredFieldValidator ID="reqPolicyname" runat="server" ErrorMessage="Please select policy name"
                                                        InitialValue="--Please Select--" ValidationGroup="WorkLocationGroup" Display="None"
                                                        ControlToValidate="RadCmbBxPolicy" 
                                                        meta:resourcekey="reqPolicynameResource1"></asp:RequiredFieldValidator>
                                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqPolicyname" TargetControlID="reqPolicyname"
                                                        runat="server" CssClass="AISCustomCalloutStyle" 
                                                        WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                                    </cc1:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblActive" runat="server" CssClass="Profiletitletxt" 
                                                        Text="Active" meta:resourcekey="lblActiveResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chckActive" runat="server" 
                                                        meta:resourcekey="chckActiveResource1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" 
                                                        ValidationGroup="WorkLocationGroup" meta:resourcekey="btnSaveResource1" />
                                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" 
                                                        CausesValidation="False" meta:resourcekey="btnDeleteResource1" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" 
                                                        CausesValidation="False" meta:resourcekey="btnClearResource1" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <telerik:RadGrid ID="dgrdVwWorkLocation" runat="server" AllowSorting="True" AllowPaging="True"
                                                        Skin="Hay" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                                        ShowFooter="True" meta:resourcekey="dgrdVwWorkLocationResource1">
                                                        <SelectedItemStyle ForeColor="Maroon" />
                                                        <MasterTableView AllowFilteringByColumn="True" AllowMultiColumnSorting="True"
                                                            AutoGenerateColumns="False" DataKeyNames="WorkLocationId">
                                                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                            <Columns>
                                                                <telerik:GridClientSelectColumn UniqueName="chkRow" />
                                                                <telerik:GridBoundColumn DataField="DesignationId" SortExpression="DesignationId"
                                                                    Visible="false" HeaderText="DesignationId" />
                                                                <telerik:GridBoundColumn DataField="WorkLocationCode" SortExpression="WorkLocationCode"
                                                                    HeaderText="Work Location Code" />
                                                                <telerik:GridBoundColumn DataField="WorkLocationName" SortExpression="WorkLocationName"
                                                                    HeaderText="English Name" />
                                                                <telerik:GridBoundColumn DataField="WorkLocationArabicName" SortExpression="WorkLocationArabicName"
                                                                    AllowFiltering="false" HeaderText="Arabic Name" />
                                                                <telerik:GridBoundColumn DataField="TAPolicyName" SortExpression="TAPolicyName" AllowFiltering="false"
                                                                    HeaderText="TA Policy Name" />
                                                                <telerik:GridCheckBoxColumn DataField="Active" SortExpression="Active" HeaderText="Is Active"
                                                                    AllowFiltering="false" ItemStyle-CssClass="nocheckboxstyle" />
                                                                <telerik:GridBoundColumn DataField="WorkLocationId" SortExpression="WorkLocationId"
                                                                    HeaderText="WorkLocationId" Visible="false" />
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
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
