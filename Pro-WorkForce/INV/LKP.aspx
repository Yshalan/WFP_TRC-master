<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" StylesheetTheme="Default" UICulture="AUTO"
    CodeFile="LKP.aspx.vb" Inherits="INV_LKP" %>

<%@ Register Src="../INV/UserControls/LKP.ascx" TagName="LKP" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .Client-logo
        {
            display: none;
        }
        .table2
        {
            margin-top:30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="700px" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <uc2:PageHeader ID="PageHeader1" runat="server" />
                    </td>
            </table>
            <table width="700px" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <uc1:LKP ID="LKP1" runat="server" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
