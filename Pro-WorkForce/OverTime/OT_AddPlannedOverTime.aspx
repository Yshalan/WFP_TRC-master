<%@ Page Title="" Language="VB" StylesheetTheme="Default"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="OT_AddPlannedOverTime.aspx.vb" Inherits="OverTime_OT_AddPlannedOverTime" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../OverTime/UserControls/UC_PlannedOverTime.ascx" TagName="UC_PlannedOverTime" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:UC_PlannedOverTime ID="UC_PlannedOverTime" runat="server" />
</asp:Content>


