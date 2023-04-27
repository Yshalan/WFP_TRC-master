<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="ApproveEmployeeGoals.aspx.vb" Inherits="Appraisal_ApproveEmployeeGoals"
    Theme="SvTheme" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <uc1:PageHeader ID="PageHeader1" HeaderText="Approve Employee Goals" runat="server" />
    <asp:MultiView ID="mvApproveEmpGoals" runat="server" ActiveViewIndex="0">
        <asp:View ID="vEmployees" runat="server">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlYear" DataTextField="txt" DataValueField="val" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="table-responsive">
                <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdEmployees"
                    ShowApplyButton="False" />
                <telerik:RadGrid ID="dgrdEmployees" runat="server" AllowPaging="True"
                    AllowSorting="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                    AutoGenerateColumns="False" PageSize="25" OnItemCommand="dgrdEmployees_ItemCommand"
                    ShowFooter="True">
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                        EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="EmployeeId,Year">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="EmployeeId" AllowFiltering="false" Display="false"
                                UniqueName="EmployeeId" />
                            <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" SortExpression="EmployeeNo"
                                UniqueName="EmployeeNo" />
                            <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"
                                UniqueName="EmployeeName" />
                            <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Arabic Name" SortExpression="EmployeeArabicName"
                                UniqueName="EmployeeArabicName" />
                            <telerik:GridBoundColumn DataField="Year" HeaderText="Year" SortExpression="Year"
                                UniqueName="Year" />
                        </Columns>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick1"
                                Skin="Hay">
                                <Items>
                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                        ImagePosition="Right" runat="server"
                                        Owner="" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <SelectedItemStyle ForeColor="Maroon" />
                </telerik:RadGrid>
            </div>

        </asp:View>
        <asp:View ID="vEmployeeGoals" runat="server">
            <uc1:PageHeader ID="PageHeader2" runat="server" />
            <div class="row">

                <div class="col-md-2">
                    <asp:LinkButton ID="lnkBack" runat="server" Text="Back"></asp:LinkButton>
                </div>
            </div>
            <div class="table-responsive">
                <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter2" FilterContainerID="dgrdEmployeeGoals"
                    ShowApplyButton="False" />
                <telerik:RadGrid ID="dgrdEmployeeGoals" runat="server" AllowPaging="True"
                    AllowSorting="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                    AutoGenerateColumns="False" PageSize="25" OnItemCommand="dgrdEmployees_ItemCommand"
                    ShowFooter="True">
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings AllowColumnsReorder="false" ReorderColumnsOnClient="false" EnablePostBackOnRowClick="false"
                        EnableRowHoverStyle="false">
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="GoalId">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="GoalId" AllowFiltering="false" Display="false"
                                UniqueName="GoalId" />
                            <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Goal Name" Resizable="true"
                                UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGoalName" runat="server" Text='<%# Eval("GoalName")%>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqtxtGoalName" runat="server" ControlToValidate="txtGoalName"
                                        Display="None" ErrorMessage="Please Enter Goal Name" ValidationGroup="grpSendtoMgr">

                                    </asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceGoalName" runat="server" TargetControlID="reqtxtGoalName"
                                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Goal Details" Resizable="true"
                                UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGoalDetails" runat="server" TextMode="MultiLine" Text='<%# Eval("GoalDetails")%>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqtxtGoalDetails" runat="server" ControlToValidate="txtGoalDetails"
                                        Display="None" ErrorMessage="Please Enter Goal Details" ValidationGroup="grpSendtoMgr">
                                    </asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceGoalDetails" runat="server" TargetControlID="reqtxtGoalDetails"
                                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Weight %" Resizable="true"
                                UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="radnumGoalWeight" MinValue="0" MaxValue="100"
                                        Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" Text='<%# Eval("Weight")%>'>
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="reqtxtGoalWeight" runat="server" ControlToValidate="radnumGoalWeight"
                                        Display="None" ErrorMessage="Please Enter Goal Weight" ValidationGroup="grpSendtoMgr">

                                    </asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceGoalWeight" runat="server" TargetControlID="reqtxtGoalWeight"
                                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Year" HeaderText="Year" SortExpression="Year" Resizable="true"
                                UniqueName="Year" />
                        </Columns>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                Skin="Hay">
                                <Items>
                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                        ImagePosition="Right" runat="server"
                                        Owner="" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <SelectedItemStyle ForeColor="Maroon" />
                </telerik:RadGrid>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <asp:CheckBox ID="chkConfirm" runat="server" Text="Goals Definition Review Completed" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Button ID="btnSendtoMgr" runat="server" Text="Submit" ValidationGroup="grpSendtoMgr" />
                </div>
            </div>
        </asp:View>
    </asp:MultiView>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

