<%@ Page Title="" Language="VB" Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
     CodeFile="HR_PermissionRequest.aspx.vb" Inherits="Definitions_HR_PermissionRequest" Culture="auto" UICulture="auto" %>

<%@ Register Src="../DailyTasks/UserControls/HR_PermissionRequest.ascx" tagname="HR_PermissionRequest" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:HR_PermissionRequest ID="HR_PermissionRequest1" DisplayMode="ViewAddEdit" runat="server" PermissionType="Normal" />
</asp:Content>
