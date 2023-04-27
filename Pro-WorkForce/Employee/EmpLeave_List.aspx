<%@ Page Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/AdminMaster.master" AutoEventWireup="false"
    CodeFile="EmpLeave_List.aspx.vb" Inherits="EmpLeave_New" Title="Untitled Page" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Employee/UserControls/EmpLeave_List.ascx" TagName="EmpLeavelist" TagPrefix="Emp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <Emp:EmpLeavelist ID="Emp1" runat="server" EmployeeID="15" />
</asp:Content>
