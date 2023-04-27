<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="SharePointRpt.aspx.vb"
    Inherits="Reports_SharePointRpt" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<table>
    <tr>
        <td align="center" style="border-left: 1px; border-right: 1px">
            <CR2:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
                GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                meta:resourcekey="CRVResource1" EnableDrillDown="False" Visible="false" />
        </td>
    </tr>
</table>
