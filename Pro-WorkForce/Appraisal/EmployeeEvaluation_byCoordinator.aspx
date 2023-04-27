<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="EmployeeEvaluation_byCoordinator.aspx.vb" Inherits="Appraisal_EmployeeEvaluation"
    Theme="SvTheme" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <uc1:PageHeader ID="PageHeader1" HeaderText="Employee Evaluation by Coordinator" runat="server" />

    <div class="row">
        <div class="col-md-12">
            <uc2:PageFilter ID="EmployeeFilterUC" runat="server" HeaderText="Employee Filter"
                OneventEmployeeSelect="FillGrid" ValidationGroup="grpSave" />
        </div>
    </div>
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div class="table-responsive">
                <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdEmployeeGoals"
                    ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                <telerik:RadGrid ID="dgrdEmployeeGoals" runat="server" AllowPaging="True"
                    AllowSorting="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                    AutoGenerateColumns="False" PageSize="25" OnItemCommand="dgrdEmployeeGoals_ItemCommand"
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
                            <telerik:GridBoundColumn DataField="GoalName" HeaderText="Goal Name" SortExpression="GoalName"
                                UniqueName="GoalName" />
                            <telerik:GridBoundColumn DataField="GoalDetails" HeaderText="Goal Details" SortExpression="GoalDetails"
                                UniqueName="GoalDetails" />
                            <telerik:GridBoundColumn DataField="Weight" HeaderText="Weight"
                                SortExpression="Weight" UniqueName="Weight" />
                            <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Points"
                                UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <telerik:RadRating RenderMode="Lightweight" ID="radEvaluationPointbyEmployee" runat="server"
                                        SelectionMode="Continuous" Precision="item" Orientation="Horizontal">
                                    </telerik:RadRating>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Remarks"
                                UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEmployeeRemarks" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
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
                <div class="col-md-3">
                    <asp:CheckBox ID="chkConfirm" runat="server" Text="I Admit That All Goals Mentioned are Final" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnSendtoMgr" runat="server" Text="Send To Manager" ValidationGroup="grpSendtoMgr" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

