<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="EmployeeEvaluation.aspx.vb" Inherits="Appraisal_EmployeeEvaluation"
    Theme="SvTheme" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        var cntrl_ddlYear = document.getElementById('<%=ddlYear.ClientID %>');
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <uc1:PageHeader ID="PageHeader1" HeaderText="Employee Evaluation" runat="server" />
    <div class="row">
        <div class="col-md-3">
            <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:DropDownList runat="server" ID="ddlYear" DataTextField="txt" DataValueField="val" AutoPostBack="true">
            </asp:DropDownList>
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
                                    <telerik:RadRating RenderMode="Lightweight" ID="radEvaluationPointbyEmployee" runat="server" ItemCount="4"
                                        SelectionMode="Continuous" Precision="item" Orientation="Horizontal" Value='<%# Eval("EvaluationPointbyEmployee")%>'>
                                    </telerik:RadRating>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Remarks"
                                UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEmployeeRemarks" runat="server" TextMode="MultiLine" Text='<%# Eval("EmployeeRemarks")%>'></asp:TextBox>
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
            <div class="table-responsive">
                <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter2" FilterContainerID="dgrdSkills"
                    ShowApplyButton="False" />
                <telerik:RadGrid ID="dgrdSkills" runat="server" AllowPaging="True"
                    AllowSorting="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                    AutoGenerateColumns="False" PageSize="25" OnItemCommand="dgrdSkills_ItemCommand"
                    ShowFooter="True">
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings AllowColumnsReorder="false" ReorderColumnsOnClient="false" EnablePostBackOnRowClick="false"
                        EnableRowHoverStyle="false">
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="SkillId">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="SkillId" AllowFiltering="false" Display="false"
                                UniqueName="SkillId" />
                            <telerik:GridBoundColumn DataField="SkillName" HeaderText="Skill Name" SortExpression="SkillName"
                                UniqueName="SkillName" />
                            <telerik:GridBoundColumn DataField="SkillArabicName" HeaderText="Skill Arabic Name" SortExpression="SkillArabicName"
                                UniqueName="SkillArabicName" />
                            <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Points"
                                UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <telerik:RadRating RenderMode="Lightweight" ID="radSkillEvaluationPointbyEmployee" runat="server"
                                        SelectionMode="Continuous" Precision="item" Orientation="Horizontal" Value='<%# Eval("EvaluationPointbyEmployee")%>'>
                                    </telerik:RadRating>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Remarks"
                                UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEmployeeSkillRemarks" runat="server" TextMode="MultiLine" Text='<%# Eval("EmployeeRemarks")%>'></asp:TextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Weight %"
                                UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <telerik:RadNumericTextBox ID="radnumSkillWeight" MinValue="0" MaxValue="100"
                                        Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" Text='<%# Eval("Weight")%>'>
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBarSkills" OnButtonClick="RadToolBarSkills_ButtonClick1"
                                Skin="Hay">
                                <Items>
                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                        ImagePosition="Right" runat="server" Owner="" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <SelectedItemStyle ForeColor="Maroon" />
                </telerik:RadGrid>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-md-6">
            <asp:CheckBox ID="chkConfirm" runat="server" Text="Evaluation Completed, Send To Manager For Review" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <asp:Button ID="btnSendtoMgr" runat="server" Text="Submit" ValidationGroup="grpSendtoMgr" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

