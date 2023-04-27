<%@ Page Language="VB" AutoEventWireup="false" CodeFile="HR_EmployeeRequests.aspx.vb"
    MasterPageFile="~/Default/NewMaster.master" Inherits="Requests_HR_EmployeeRequests"
    Theme="SvTheme" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="UserControls/HR/HR_PermissionApproval.ascx" TagName="HR_PermissionApproval"
    TagPrefix="uc2" %>
<%@ Register Src="UserControls/HR/HR_StudyPermissionApproval.ascx" TagName="HR_StudyPermissionApproval"
    TagPrefix="uc3" %>
<%@ Register Src="UserControls/HR/HR_NursingPermissionApproval.ascx" TagName="HR_NursingPermissionApproval"
    TagPrefix="uc4" %>
<%@ Register Src="UserControls/HR/HR_ManualEntryApproval.ascx" TagName="HR_ManualEntryApproval"
    TagPrefix="uc5" %>
<%@ Register Src="UserControls/HR/HR_OverTimeApproval.ascx" TagName="HR_OverTimeApproval"
    TagPrefix="uc6" %>
<%@ Register Src="UserControls/HR/HR_LeaveApproval.ascx" TagName="HR_LeaveApproval"
    TagPrefix="uc7" %>
<%@ Register Src="UserControls/HR/HR_UpdateTransactionApproval.ascx" TagName="HR_UpdateTransactions" TagPrefix="uc8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="HR_EmployeeRequestsHeader" HeaderText="Human Resource Employee Requests"
        runat="server" />
    <uc2:HR_PermissionApproval ID="HR_PermissionApproval1" runat="server" Visible="false" />
    <uc3:HR_StudyPermissionApproval ID="HR_StudyPermissionApproval1" runat="server" Visible="false" />
    <uc4:HR_NursingPermissionApproval ID="HR_NursingPermissionApproval1" runat="server"
        Visible="false" />
    <uc5:HR_ManualEntryApproval ID="HR_ManualEntryApproval1" runat="server" Visible="false" />
    <uc6:HR_OverTimeApproval ID="HR_OverTimeApproval1" runat="server" Visible="false" />
    <uc7:HR_LeaveApproval ID="HR_LeaveApproval1" runat="server" Visible="false" />
    <uc8:HR_UpdateTransactions ID="HR_UpdateTransactions1" runat="server" Visible="false" />
</asp:Content>
