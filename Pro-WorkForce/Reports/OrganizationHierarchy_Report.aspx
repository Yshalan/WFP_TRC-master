<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OrganizationHierarchy_Report.aspx.vb" MasterPageFile="~/Default/ArabicMaster.master"  StylesheetTheme="Default"
Inherits="Reports_OrganizationHierarchy_Report" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>


    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
    <tr align="center">
    <td colspan="2">
    <uc1:PageHeader ID="lblReportTitle" runat="server"  />
    </td>
    </tr>
    </table>
  <br />
    <table width="450px">
   
     <tr>
                            <td>
                                <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" Text="Company"
                                    Width="200px" meta:resourcekey="lblCompanyeResource1"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="radcmbCompany" Filter="Contains" MarkFirstMatch="true" Skin="Vista"
                                    Width="210px" runat="server">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
        <tr  align="center">
            <td>
              <br />
            </td>
        </tr>
        <tr align="center">
            <td colspan="2">
                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                    ValidationGroup="btnPrint" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
