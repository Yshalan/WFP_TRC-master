<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="EmployeeGoals.aspx.vb" Inherits="EmployeeGoals"
    Theme="SvTheme" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" HeaderText="Employee Goals" runat="server" />
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>


            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList runat="server" ID="ddlYear" DataTextField="txt" DataValueField="val" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblGoalName" runat="server" Text="Goal Name"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtGoalName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqtxtGoalName" runat="server" ControlToValidate="txtGoalName"
                        Display="None" ErrorMessage="Please Enter Goal Name" ValidationGroup="grpSave">

                    </asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceGoalName" runat="server" TargetControlID="reqtxtGoalName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblGoalDetails" runat="server" Text="Goal Details"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtGoalDetails" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblGoalWeight" runat="server" Text="Weight"></asp:Label>
                </div>
                <div class="col-md-3">
                    <telerik:RadNumericTextBox ID="radnumGoalWeight" MinValue="0" MaxValue="100"
                        Skin="Vista" runat="server" Culture="en-US" LabelCssClass="">
                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="rfGoalWeight" runat="server" ControlToValidate="radnumGoalWeight"
                        Display="None" ErrorMessage="Please Enter Goal Weight" ValidationGroup="grpSave">
                    </asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceGoalWeight" runat="server" TargetControlID="rfGoalWeight"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
                <div class="col-md-1"><i class="fa fa-percent"></i></div>
            </div>

            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="grpSave" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" />
                </div>
            </div>

            <div class="table-responsive">
                <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdEmployeeGoals"
                    ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                <telerik:RadGrid ID="dgrdEmployeeGoals" runat="server" AllowPaging="True"
                    AllowSorting="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                    AutoGenerateColumns="False" PageSize="25" OnItemCommand="dgrdEmployeeGoals_ItemCommand"
                    ShowFooter="True">
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                        EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="GoalId">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="False"
                                UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="GoalId" AllowFiltering="false" Display="false"
                                UniqueName="GoalId" />
                            <telerik:GridBoundColumn DataField="GoalName" HeaderText="Goal Name" SortExpression="GoalName"
                                UniqueName="GoalName" />
                            <telerik:GridBoundColumn DataField="GoalDetails" HeaderText="Goal Details" SortExpression="GoalDetails"
                                UniqueName="GoalDetails" />
                            <telerik:GridBoundColumn DataField="Weight" HeaderText="Weight %"
                                SortExpression="Weight" UniqueName="Weight" />
                        </Columns>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                <Items>
                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                        ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1"
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
                    <asp:CheckBox ID="chkConfirm" runat="server" Text="Goals Definition Completed, Send To Manager For Review" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Button ID="btnSendtoMgr" runat="server" Text="Submit" ValidationGroup="grpSendtoMgr" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

