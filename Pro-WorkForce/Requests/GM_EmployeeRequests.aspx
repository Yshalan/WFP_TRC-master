<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GM_EmployeeRequests.aspx.vb" MasterPageFile="~/Default/NewMaster.master"
    Inherits="Requests_GM_EmployeeRequests" Theme="SvTheme" %>


<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="UserControls/GM/GM_PermissionApproval.ascx" TagName="GM_PermissionApproval" TagPrefix="uc2" %>
<%@ Register Src="UserControls/GM/GM_StudyPermissionApproval.ascx" TagName="GM_StudyPermissionApproval" TagPrefix="uc3" %>
<%@ Register Src="UserControls/GM/GM_NursingPermissionApproval.ascx" TagName="GM_NursingPermissionApproval" TagPrefix="uc4" %>
<%@ Register Src="UserControls/GM/GM_ManualEntryApproval.ascx" TagName="GM_ManualEntryApproval" TagPrefix="uc5" %>
<%--<%@ Register src="UserControls/GM/GM_OverTimeApproval.ascx" tagname="GM_OverTimeApproval" tagprefix="uc6" %>--%>
<%@ Register Src="UserControls/GM/GM_LeaveApproval.ascx" TagName="GM_LeaveApproval" TagPrefix="uc7" %>
<%@ Register Src="UserControls/GM/GM_UpdateTransactionApproval.ascx" TagName="GM_UpdateTransactions" TagPrefix="uc8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <uc1:PageHeader ID="GM_EmployeeRequestsHeader" HeaderText="General Manager Employee Requests" runat="server" />

    <uc2:GM_PermissionApproval ID="GM_PermissionApproval1" runat="server" Visible="false" />

    <uc3:GM_StudyPermissionApproval ID="GM_StudyPermissionApproval1" runat="server" Visible="false" />

    <uc4:GM_NursingPermissionApproval ID="GM_NursingPermissionApproval1" runat="server" Visible="false" />

    <uc5:GM_ManualEntryApproval ID="GM_ManualEntryApproval1" runat="server" Visible="false" />

    <%--<uc6:GM_OverTimeApproval ID="GM_OverTimeApproval1" runat="server" Visible="false"/>
     <br />--%>
    <uc7:GM_LeaveApproval ID="GM_LeaveApproval1" runat="server" Visible="false" />

    <uc8:GM_UpdateTransactions ID="GM_UpdateTransactions1" runat="server" Visible="false" />

</asp:Content>
