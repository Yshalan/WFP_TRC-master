<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ActiveSchedule.ascx.vb"
    Inherits="Admin_UserControls_ActiveSchedule" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<style type="text/css">
    .style1
    {
    }
    .td1
    {
        padding-bottom: 20px;
    }
    .style2
    {
        height: 20px;
        width: 495px;
    }
    .style3
    {
        padding-bottom: 20px;
        height: 20px;
    }
    .style6
    {
        padding-bottom: 20px;
        width: 495px;
    }
</style>
<uc1:PageHeader ID="UserCtrlAssign" runat="server" HeaderText="Employee Active Schedule" />
<table class="style1">
    <tr>
        <td class="style6">
            <asp:Label ID="Lbl1" runat="server" CssClass="Profiletitletxt" 
                Text="Get Active Schedule" meta:resourcekey="Lbl1Resource1"></asp:Label>
        </td>
        <td class="td1">
            <asp:TextBox ID="TextBox1" runat="server" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="txtbox1" runat="server" ControlToValidate="TextBox1"
                Display="None" ErrorMessage="Please Enter Employee ID" 
                meta:resourcekey="txtbox1Resource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ExtenderreqActiveschedule" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="txtbox1" WarningIconImageUrl="~/images/warning1.png" 
                Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td class="style2">
            <asp:Button ID="btnget" runat="server" CssClass="button" Text="Get Active Schedule"
                Width="150px" meta:resourcekey="btngetResource1" />
        </td>
        <td class="style3">
        </td>
    </tr>
</table>
<div style="padding: 20px 20px 20px 20px">
    <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdAschedule"
        ShowApplyButton="false" />
    <telerik:RadGrid ID="dgrdAschedule" runat="server" AllowSorting="True" AllowPaging="True"
        Skin="Hay" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" GroupingSettings-CaseSensitive="false"
        ShowFooter="True">
        <SelectedItemStyle ForeColor="maroon" />
        <MasterTableView AllowMultiColumnSorting="True" IsFilterItemExpanded="True" CommandItemDisplay="Top"
            AutoGenerateColumns="False">
            <CommandItemTemplate>
                <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick">
                    <Items>
                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                            ImagePosition="Right" />
                    </Items>
                </telerik:RadToolBar>
            </CommandItemTemplate>
            <Columns>
                <telerik:GridClientSelectColumn UniqueName="chkRow" />
                <telerik:GridBoundColumn DataField="FK_ScheduleId" SortExpression="FK_ScheduleId"
                    HeaderText="Schedule Id" />
                <telerik:GridBoundColumn DataField="ScheduleName" SortExpression="ScheduleName" HeaderText="Schedule English Name" />
                <telerik:GridBoundColumn DataField="FK_ScheduleId" SortExpression="FK_ScheduleId"
                    Visible="False" />
            </Columns>
            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
        </MasterTableView><ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
