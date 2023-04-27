
<%@ Page Language="VB" Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="AutoSchedule.aspx.vb" Inherits="SchoolScheduling_Course" Title="School Auto Scheduling" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <telerik:RadScriptBlock ID="scriptblock" runat="server">
        <script type="text/javascript">
            function colorChanged(sender) {
                sender.get_element().style.backgroundColor = "#" + sender.get_selectedColor();
                sender.get_element().style.color = "#" + sender.get_selectedColor();
                sender.get_element().value = "#" + sender.get_selectedColor();
            }
           </script>
    </telerik:RadScriptBlock>

    <script type="text/javascript" language="javascript">
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="School Auto Scheduling" />

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblsequential" runat="server" Text="Allow sequential Lessons for same Course"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="RBLsequential" runat="server"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                        <asp:ListItem Value="0">No </asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>


            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblMaxLessons" runat="server" Text="Max lessons per day for teacher"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtMaxLessons" runat="server" Text="6"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label3" runat="server" Text="Teacher Break to be distributed"></asp:Label>

                </div>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="RBLdistributed" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                        <asp:ListItem Value="0">No </asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">

                    <asp:Button ID="btnGenerate" runat="server" Text="Generate Schedule" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Label ID="lblresult" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
