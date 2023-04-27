<%@ Page Title="" Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="OT_HRApproval.aspx.vb" Inherits="OverTime_OT_HRApproval" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Src="../OverTime/UserControls/UC_HROverTimeApproval.ascx" TagName="UC_HROverTimeApproval" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:UC_HROverTimeApproval ID="UC_HROverTimeApproval1" runat="server" />
</asp:Content>


