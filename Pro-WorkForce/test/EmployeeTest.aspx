<%@ Page Title="" Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/AdminMaster.master" AutoEventWireup="false" CodeFile="EmployeeTest.aspx.vb" Inherits="test_EmployeeTest" %>

<%@ Register src="../Employee/UserControls/Employee.ascx" tagname="Employee" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Employee ID="ObjEmployee" DisplayMode="ViewAddEdit" runat="server" />
</asp:Content>

