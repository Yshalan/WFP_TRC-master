<%@ Page Title="" Language="VB"  Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="NursingPermission.aspx.vb" Inherits="Employee_NursingPermission" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../Employee/UserControls/EmpPermissions.ascx" TagName="EmpPermissions"
    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:EmpPermissions ID="EmpPermissions1" DisplayMode="ViewAddEdit" runat="server" PermissionType="Nursing" />
</asp:Content>
