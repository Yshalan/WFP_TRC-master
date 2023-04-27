<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AssignAdvancedSchedule.aspx.vb" MasterPageFile="~/Default/NewMaster.master"
Inherits="DailyTasks_AssignAdvancedSchedule" Theme="SvTheme" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/DailyTasks/UserControls/AssignSchedule_Employee.ascx" TagName="Assign_Emp"
    TagPrefix="uc1" %>
    <%@ Register Src="~/UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc6" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <uc6:PageHeader ID="AssignAdvancedSchedule" runat="server" />
            <div class="row">
                    <uc1:Assign_Emp ID="objAssign_Emp" IsManager="1" IsAdvanced="1" runat="server" />

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
