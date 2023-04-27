<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DM_EmployeeRequests.aspx.vb" MasterPageFile="~/Default/NewMaster.master"
    Inherits="Requests_DM_EmployeeRequests" Theme="SvTheme" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="UserControls/DM/DM_PermissionApproval.ascx" TagName="DM_PermissionApproval" TagPrefix="uc2" %>
<%@ Register Src="UserControls/DM/DM_StudyPermissionApproval.ascx" TagName="DM_StudyPermissionApproval" TagPrefix="uc3" %>
<%@ Register Src="UserControls/DM/DM_NursingPermissionApproval.ascx" TagName="DM_NursingPermissionApproval" TagPrefix="uc4" %>
<%@ Register Src="UserControls/DM/DM_ManualEntryApproval.ascx" TagName="DM_ManualEntryApproval" TagPrefix="uc5" %>
<%@ Register Src="UserControls/DM/DM_OverTimeApproval.ascx" TagName="DM_OverTimeApproval" TagPrefix="uc6" %>
<%@ Register Src="UserControls/DM/DM_LeaveApproval.ascx" TagName="DM_LeaveApproval" TagPrefix="uc7" %>
<%@ Register Src="UserControls/DM/DM_UpdateTransactionApproval.ascx" TagName="DM_UpdateTransactions" TagPrefix="uc8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="refresh" content="60">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <asp:Timer ID="tmrRefresh" runat="server" Enabled="true" Interval="1000" OnTick="tmrRefresh_Tick">
                            </asp:Timer>--%>
    <uc1:PageHeader ID="DM_EmployeeRequestsHeader" HeaderText="Direct Manager Employee Requests" runat="server" />
    <uc2:DM_PermissionApproval ID="DM_PermissionApproval1" runat="server" Visible="false" />

    <uc3:DM_StudyPermissionApproval ID="DM_StudyPermissionApproval1" runat="server" Visible="false" />

    <uc4:DM_NursingPermissionApproval ID="DM_NursingPermissionApproval1" runat="server" Visible="false" />

    <uc5:DM_ManualEntryApproval ID="DM_ManualEntryApproval1" runat="server" Visible="false" />

    <uc6:DM_OverTimeApproval ID="DM_OverTimeApproval1" runat="server" Visible="false" />

    <uc7:DM_LeaveApproval ID="DM_LeaveApproval1" runat="server" Visible="false" />

    <uc8:DM_UpdateTransactions ID="DM_UpdateTransactions1" runat="server" Visible="false" />

</asp:Content>

