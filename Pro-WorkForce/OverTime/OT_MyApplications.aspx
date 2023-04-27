<%@ Page Title="" Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="OT_MyApplications.aspx.vb" Inherits="OverTime_OT_MyApplications" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../OverTime/UserControls/UC_MyOTApplications.ascx" TagName="UC_MyOTApplications" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:UC_MyOTApplications ID="UC_MyOTApplications1" runat="server" />
</asp:Content>

