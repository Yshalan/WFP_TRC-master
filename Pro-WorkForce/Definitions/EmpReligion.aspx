<%@ Page Title="Employee Religion" Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/AdminMaster.master"
    AutoEventWireup="false" Theme="AIStheme" CodeFile="EmpReligion.aspx.vb" Inherits="Emp_EmpReligion" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlEmpReligion" runat="server">
        <ContentTemplate>
            <table width="700px" cellspacing="0" cellpadding="0">
                <tr >
                    <td>
                        <div id="divEmpReligion" style="display: block">
                            <table width="600px">
                                <tr>
                                    <td colspan="2">
                                        <uc1:PageHeader ID="UserCtrlReligion" runat="server" HeaderText="Employee Religion" />
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblReligionCode" runat="server" CssClass="Profiletitletxt" 
                                            Text="Code" meta:resourcekey="lblReligionCodeResource1"></asp:Label>
                                        <td>
                                            <asp:TextBox ID="txtReligionCode" runat="server" 
                                                meta:resourcekey="txtReligionCodeResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqReligionCode" runat="server" ControlToValidate="txtReligionCode"
                                                Display="None" ErrorMessage="Please enter religion code" 
                                                ValidationGroup="ReligionGroup" meta:resourcekey="reqReligionCodeResource1"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ExtenderreqReligionCode" runat="server" CssClass="AISCustomCalloutStyle"
                                                TargetControlID="reqReligionCode" 
                                                WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblReligionName" runat="server" CssClass="Profiletitletxt" 
                                            Text="English name" meta:resourcekey="lblReligionNameResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReligioName" runat="server" Width="200px" 
                                            meta:resourcekey="txtReligioNameResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqReligionName" runat="server" ErrorMessage="Please enter religion english name"
                                            Display="None" ValidationGroup="ReligionGroup" ControlToValidate="txtReligioName"
                                            CssClass="AISCustomCalloutStyle" 
                                            WarningIconImageUrl="~/images/warning1.png" 
                                            meta:resourcekey="reqReligionNameResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderReligioName" TargetControlID="reqReligionName"
                                            runat="server" CssClass="AISCustomCalloutStyle" 
                                            WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblReligionArabicName" runat="server" CssClass="Profiletitletxt" 
                                            Text="Arabic Name" meta:resourcekey="lblReligionArabicNameResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReligionArabicName" runat="server" Width="200px" 
                                            meta:resourcekey="txtReligionArabicNameResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqReligionArabicName" runat="server" ErrorMessage="Please enter religion arabic name"
                                            Display="None" ValidationGroup="ReligionGroup" ControlToValidate="txtReligionArabicName"
                                            CssClass="AISCustomCalloutStyle" 
                                            WarningIconImageUrl="~/images/warning1.png" 
                                            meta:resourcekey="reqReligionArabicNameResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderReligionArName" TargetControlID="reqReligionArabicName"
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
                                        <asp:CheckBox ID="chckBoxActive" runat="server" 
                                            meta:resourcekey="chckBoxActiveResource1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="ReligionGroup"
                                            CssClass="button" meta:resourcekey="btnSaveResource1" />
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CausesValidation="False"
                                            CssClass="button" meta:resourcekey="btnDeleteResource1" />
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" 
                                            CssClass="button" meta:resourcekey="btnClearResource1" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" >
                                        <telerik:RadGrid ID="dgrdVwReligion" runat="server" AllowSorting="True" AllowPaging="True"
                                            Skin="Hay" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                            ShowFooter="True" meta:resourcekey="dgrdVwReligionResource1">
                                            <SelectedItemStyle ForeColor="Maroon" />
                                            <MasterTableView AllowFilteringByColumn="True" AllowMultiColumnSorting="True"
                                                AutoGenerateColumns="False" DataKeyNames="ReligionId">
                                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                <Columns>
                                                    <telerik:GridClientSelectColumn UniqueName="chkRow" />
                                                    <telerik:GridBoundColumn DataField="ReligionCode" SortExpression="ReligionCode" HeaderText="Religion Code" />
                                                    <telerik:GridBoundColumn DataField="ReligionName" SortExpression="ReligionName" HeaderText="Religion English Name" />
                                                    <telerik:GridBoundColumn DataField="ReligionArabicName" SortExpression="ReligionArabicName"
                                                        HeaderText="Religion Arabic Name" />
                                                    <telerik:GridCheckBoxColumn DataField="Active" SortExpression="Active" HeaderText="Is Active"
                                                        AllowFiltering="false" />
                                                    <telerik:GridBoundColumn DataField="ReligionId" SortExpression="ReligionId" Visible="false" />
                                                </Columns>
                                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                            </MasterTableView>
                                            <GroupingSettings CaseSensitive="False" />
                                            <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
                                    </td>
                    </td>
                </tr>
            </table>
            </div> </td> </tr> </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
