<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GenerateEmailForEmployeeDeduction.aspx.vb"  
 Inherits="Reports_GenerateEmailForManagers" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
  <%--  <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">--%>

                        <CR2:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
                            GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                            meta:resourcekey="CRVResource1" EnableDrillDown="False" Visible="false" />