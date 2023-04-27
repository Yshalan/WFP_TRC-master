<%@ Page Language="VB" AutoEventWireup="false" StylesheetTheme="Default"  MasterPageFile="~/Default/AdminMaster.master"
    CodeFile="HomePage.aspx.vb" Inherits="Default_HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <div style="width: 100%; float: left; height: 30px; margin-left: 20px">
        <div style="width: 138px; float: left">
            <asp:LinkButton Text="Admin" ID="lbtnAdmin_1_2_3" runat="server"></asp:LinkButton></div>
        <div style="width: 138px; float: left">
            <asp:LinkButton Text="Definitions" ID="lbtnDefinitions_1_2_3" runat="server">
            </asp:LinkButton></div>
        <div style="width: 138px; float: left">
            <asp:LinkButton Text="Employee" ID="lbtnEmployee_1_2_3" runat="server">
            </asp:LinkButton>
        </div>
    </div>
    <div style="width: 100%; float: left; height: 30px; margin-left: 20px">
        <div style="width: 138px; float: left">
            <asp:LinkButton Text="DailyTasks" ID="lbtnDailyTasks_1_2_3" runat="server"></asp:LinkButton>
        </div>
        <div style="width: 138px; float: left">
            <asp:LinkButton Text="SelfServices" ID="lbtnSelfServices_2_3" runat="server">
            </asp:LinkButton>
        </div>
        <div style="width: 138px; float: left">
            <asp:LinkButton Text="Requests" ID="lbtnRequests_2_3" runat="server">
            </asp:LinkButton>
        </div>
    </div>
    <div style="width: 100%; float: left; height: 30px; margin-left: 20px">
        <div style="width: 138px; float: left">
            <asp:LinkButton Text="Dash Board" ID="lbtnDashBoard_1_2_3" runat="server"></asp:LinkButton>
        </div>
        <div style="width: 138px; float: left">
            <asp:LinkButton Text="Reports" ID="lbtnReports_1_2_3" runat="server">
            </asp:LinkButton>
        </div>
        <div style="width: 138px; float: left">
            <asp:LinkButton Text="Security" ID="lbtnSecurity_1_2_3" runat="server">
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>
