<%@ Page Title="Employees" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" EnableEventValidation="false" AutoEventWireup="false" CodeFile="SendNotification.aspx.vb" Inherits="Emp_Employees" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register src="UserControls/EmpDetails.ascx" tagname="EmpDetails" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <uc1:EmpDetails ID="EmpDetails1" runat="server" />
</asp:Content>

