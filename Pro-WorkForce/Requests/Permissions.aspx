<%@ Page Language="VB" Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="Permissions.aspx.vb" Inherits="Emp_Permissions" title="Untitled Page" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../Employee/UserControls/EmpPermissions.ascx" tagname="EmpPermissions" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:EmpPermissions ID="EmpPermissions1" DisplayMode="ViewAddEdit" runat="server" PermissionType="Normal" />
</asp:Content>

