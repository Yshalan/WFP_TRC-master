<%@ Page Language="VB" Title="Send Email Notification" StylesheetTheme="Default" MasterPageFile="~/Default/EmptyMaster.master"
    AutoEventWireup="false" CodeFile="SendEmail.aspx.vb" Inherits="Admin_EmployeeSearch"
    UICulture="auto" Theme="SvTheme" Culture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function CloseAndRefresh() {
            var oWnd = GetRadWindow();
            oWnd.close();
            GetRadWindow().BrowserWindow.location.reload();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updatepanel1">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" />

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblSend" runat="server" Text="Please Enter Email Address to Send Notification" CssClass="Profiletitletxt" meta:resourcekey="lblValueResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtSend" runat="server" meta:resourcekey="txtSendResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvValue" runat="server" ControlToValidate="txtSend"
                        Display="None" ErrorMessage="Please Insert Search Value" ValidationGroup="GrpSearch"
                        meta:resourcekey="rfvValueResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceValue" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvValue" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:Button ID="btnSendEmp" runat="server" CssClass="button" Text="Send Notification" ValidationGroup="GrpSearch" meta:resourcekey="btnSendEmpResource1" />
                </div>
            </div>
        
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
