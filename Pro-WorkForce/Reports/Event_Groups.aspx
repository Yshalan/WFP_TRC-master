<%@ Page Title="" Language="VB" MasterPageFile="~/Default/ArabicMaster.master" AutoEventWireup="false"  StylesheetTheme="Default"
    CodeFile="Event_Groups.aspx.vb" Inherits="Reports_Event_Groups" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="Filter" runat="server">
            <center>
                <table style="width: 594px; border-width: 0px; background-color: #FFFFFF; visibility: hidden;"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3">
                            <div id="logo_div">
                                <a href="../Default/logout.aspx">
                                    <div id="logout2">
                                    </div>
                                </a>
                                <div style="text-align: left">
                                    <a href="../Default/Home.aspx">
                                        <img src="../images/logo.jpg" alt="smart time" /></a>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </center>
            <table>
                <tr>
                    <td>
                        <uc1:PageHeader ID="lblReportTitle" runat="server" />
                        <div dir="<%=dir %>">
                            <table style="text-align: <%= iif(dir="ltr","left","right")%>; width: 450px">
                                <tr>
                                    <td>
                                        <table style="background-color: #fff">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblEvent" runat="server" Text="Event\Project" AutoPostBack="true"
                                                        CssClass="Profiletitletxt" meta:resourcekey="lblEventResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="RadCmbBxEvent" MarkFirstMatch="True"
                                                        AutoPostBack="True" Skin="Vista" runat="server" 
                                                        meta:resourcekey="RadCmbBxEventResource1">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblGroup" runat="server" Text="Logical Group Name" AutoPostBack="true"
                                                        CssClass="Profiletitletxt" meta:resourcekey="lblGroupResource1"></asp:Label>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="RadCmbBxGroup" MarkFirstMatch="True"
                                                        Skin="Vista" runat="server" meta:resourcekey="RadCmbBxGroupResource1">
                                                    </telerik:RadComboBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rblFormat" runat="server" RepeatDirection="Horizontal" 
                                                        CssClass="Profiletitletxt" meta:resourcekey="rblFormatResource1">
                                                        <asp:ListItem Text="PDF" Value="1" Selected="True" 
                                                            meta:resourcekey="ListItemResource1"></asp:ListItem>
                                                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr id="trControls" runat="server" align="center">
                                                <td runat="server">
                                                </td>
                                                <td runat="server">
                                                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                                                        ValidationGroup="btnPrint" />
                                                </td>
                                                <td runat="server">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width: 100%; height: 44px;">
                        </div>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="Report" runat="server">
            <table align="center" style="background-color: #fff; width: 1024px; height: 44px;">
                <tr>
                    <td align="center" style="border-left: 1px; border-right: 1px">
                        <CR2:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
                            GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                            meta:resourcekey="CRVResource1" EnableDrillDown="False" 
                            GroupTreeImagesFolderUrl="" HasGotoPageButton="False" 
                            HasPageNavigationButtons="False" ToolbarImagesFolderUrl="" 
                            ToolPanelWidth="200px" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
