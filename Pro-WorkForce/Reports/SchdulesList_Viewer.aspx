<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"  StylesheetTheme="Default"
    CodeFile="SchdulesList_Viewer.aspx.vb" Inherits="Reports_SchdulesList_Viewer"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="EmployeeFilter"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <asp:multiview id="MultiView1" runat="server" activeviewindex="0">
        <asp:view id="Filter" runat="server">
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
            <table style="text-align: <%= iif(dir="ltr","left","right")%>; width: 450px">
                <tr>
                    <td>
                        <uc1:PageHeader ID="lblReportTitle" runat="server" />
                        <div dir="<%=dir %>">
                            <table style="text-align: <%= iif(dir="ltr","left","right")%>; width: 450px">
                                <tr>
                                    <td>
                                        <table style="background-color: #fff">
                                            
                                            <tr>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:label id="lblScheduleType" runat="server" cssclass="Profiletitletxt" text="Schedule Type"
                                                        meta:resourcekey="lblScheduleTypeResource1" />
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="RadComboScheduleType" CausesValidation="False" Filter="Contains"
                                                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                                                        meta:resourcekey="RadComboScheduleTypeResource1">
                                                        
                                                    </telerik:RadComboBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:label id="lblScheduleName" runat="server" cssclass="Profiletitletxt" text="Schedule Name"
                                                        meta:resourcekey="lblScheduleNameResource1" />
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="RadCmbBxScheduleName" CausesValidation="False" Filter="Contains"
                                                         Skin="Vista" runat="server" Style="width: 350px"
                                                        meta:resourcekey="RadCmbBxScheduleNameResource1">
                                                    </telerik:RadComboBox>
                                                </td>
                                                
                                            </tr>
                                         
                                            
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:radiobuttonlist id="rblFormat" runat="server" repeatdirection="Horizontal" cssclass="Profiletitletxt"
                                                        meta:resourcekey="rblFormatResource1">
                                                        <asp:listitem text="PDF" value="1" selected="True" meta:resourcekey="ListItemResource1">
                                                        </asp:listitem>
                                                        <asp:listitem text="MS Word" value="2" meta:resourcekey="ListItemResource2">
                                                        </asp:listitem>
                                                        <asp:listitem text="MS Excel" value="3" meta:resourcekey="ListItemResource3">
                                                        </asp:listitem>
                                                    </asp:radiobuttonlist>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr id="trControls" runat="server" align="center">
                                                <td id="Td1" runat="server">
                                                </td>
                                                <td id="Td2" runat="server">
                                                    <asp:button id="btnPrint" runat="server" text="Print" cssclass="button" validationgroup="Save"
                                                        meta:resourcekey="Button1Resource1" />
                                                </td>
                                                <td id="Td3" runat="server">
                                                    &nbsp;
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
        </asp:view>
        <asp:view id="Report" runat="server">
            <table align="center" style="background-color: #fff; width: 1024px; height: 44px;">
                <tr>
                    <td align="center" style="border-left: 1px; border-right: 1px">
                        <CR2:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
                            GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                            EnableDrillDown="False" GroupTreeImagesFolderUrl="" HasGotoPageButton="False"
                            HasPageNavigationButtons="False" ToolbarImagesFolderUrl="" ToolPanelWidth="200px"
                            meta:resourcekey="CRVResource1" />
                    </td>
                </tr>
            </table>
        </asp:view>
    </asp:multiview>
</asp:content>
