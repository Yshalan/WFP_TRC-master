<%@ Page Title="" Language="VB" Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="SchGroupAssignEmployee.aspx.vb" Inherits="ScheduleGroup_SchGroupAssignEmployee" %>

<%@ Register Src="../ScheduleGroup/UserControls/SchGroupAssignEmployee.ascx" TagName="SchGroupAssignEmployee"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:SchGroupAssignEmployee ID="SchGroupAssignEmployee1" runat="server" />
</asp:Content>
