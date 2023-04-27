<%@ Page Title="" Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/AdminMaster.master" AutoEventWireup="false" CodeFile="PolicyIdTest.aspx.vb" Inherits="test_PolicyIdTest" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
        <tr>
            <td>
                <asp:Label ID="lblLogialGroup" runat="server" Text="Logical Group"></asp:Label>
            </td>
            <td>
                <telerik:RadComboBox ID="RadCmbLogicalGroup" Runat="server">
                </telerik:RadComboBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblWorkLocation" runat="server" Text="Work Location"></asp:Label>
            </td>
            <td>
                <telerik:RadComboBox ID="RadCmbWorkLocation" Runat="server">
                </telerik:RadComboBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblorgCompany" runat="server" Text="Org Company"></asp:Label>
            </td>
            <td>
                <telerik:RadComboBox ID="RadCmbOrgCompany" 
                OnSelectedIndexChanged="RadCmbOrgCompany_SelectedIndexChanged"
                AutoPostBack="true"
                Runat="server">
                </telerik:RadComboBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEntity" runat="server" Text="Entity"></asp:Label>
            </td>
            <td>
                <telerik:RadComboBox ID="RadCmbEntity" Runat="server">
                </telerik:RadComboBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        
        
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Button" />
            </td>
            <td>
                &nbsp;</td>
        <td>
            <asp:Button ID="Button2" runat="server" Text="Button" />
            </td>
        <td></td>
        <td></td>
        </tr>
        
    </table>
</asp:Content>

