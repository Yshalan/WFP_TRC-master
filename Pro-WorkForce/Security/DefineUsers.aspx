<%@ Page Title="Define Users" Language="VB"  Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="DefineUsers.aspx.vb" Inherits="Security_DefineUsers" meta:resourcekey="PageResource1" uiculture="auto"  %>

<%@ Register Src="../Security/UserControls/DefineUsers.ascx" tagname="DefineUsers" tagprefix="uc1" %>

<%@ Register src="../UserControls/PageHeader.ascx" tagname="PageHeader" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel runat="server">
<ContentTemplate>
    <uc2:PageHeader ID="PageHeader1" runat="server" />
    <uc1:DefineUsers ID="DefineUsers" runat="server" />
</ContentTemplate>
</asp:UpdatePanel>
    
</asp:Content>
