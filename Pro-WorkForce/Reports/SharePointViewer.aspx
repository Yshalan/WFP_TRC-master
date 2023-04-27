<%@ Page Title="" Language="VB" AutoEventWireup="false" CodeFile="SharePointViewer.aspx.vb"
    Inherits="Reports_SharePointViewer" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<table>
    <tr>
        <td align="center" style="border-left: 1px; border-right: 1px">
            <CR2:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
                GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                meta:resourcekey="CRVResource1" EnableDrillDown="False" Visible="false" />


<%--
                    <ajaxToolkit:HiddenField ID="hdEmployeeId" runat="Server"></ajaxToolkit:HiddenField>
                    <ajaxToolkit:HiddenField ID="hdEmployeeId" runat="Server"></ajaxToolkit:HiddenField>
                    <ajaxToolkit:HiddenField ID="hdDateFrom" runat="Server"></ajaxToolkit:HiddenField>
        <ajaxToolkit:HiddenField ID="hdDateTo" runat="Server"></ajaxToolkit:HiddenField>
    
        <ajaxToolkit:HiddenField ID="hdRptType" runat="Server"></ajaxToolkit:HiddenField>
        <ajaxToolkit:HiddenField ID="hdLang" runat="Server"></ajaxToolkit:HiddenField>
        <ajaxToolkit:HiddenField ID="hdRptFormat" runat="Server"></ajaxToolkit:HiddenField>
    
        <ajaxToolkit:HiddenField ID="hdManagerId" runat="Server"></ajaxToolkit:HiddenField>
        <ajaxToolkit:HiddenField ID="hdUserId" runat="Server"></ajaxToolkit:HiddenField>
        <ajaxToolkit:HiddenField ID="hdReportSelectType" runat="Server" />--%>
        </td>
    </tr>
</table>
<%--<input type="hidden" id="hdEmployeeId" />--%>