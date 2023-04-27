<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="ManagerEvaluation.aspx.vb" Inherits="Appraisal_ManagerEvaluation"
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

    <uc1:PageHeader ID="PageHeader1" HeaderText="Manager Evaluation" runat="server" />

    <asp:MultiView ID="mvApproveEmpGoals" runat="server" ActiveViewIndex="0">
        <asp:View ID="vEmployees" runat="server">
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                </div>
                <div class="col-md-3">
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
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
                                    <telerik:GridBoundColumn DataField="GoalName" HeaderText="Goal Name" SortExpression="GoalName"
                                        UniqueName="GoalName" />
                                    <telerik:GridBoundColumn DataField="GoalDetails" HeaderText="Goal Details" SortExpression="GoalDetails"
                                        UniqueName="GoalDetails" />
                                    <telerik:GridBoundColumn DataField="Weight" HeaderText="Weight %"
                                        SortExpression="Weight" UniqueName="Weight" />
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Employee Points"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <telerik:RadRating RenderMode="Lightweight" ID="radEvaluationPointbyEmployee" runat="server"
                                                SelectionMode="Continuous" Precision="item" Orientation="Horizontal" Value='<%# Eval("EvaluationPointbyEmployee")%>'
                                                Enabled="false">
                                            </telerik:RadRating>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Employee Remarks"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEmployeeRemarks" runat="server" TextMode="MultiLine" Text='<%# Eval("EmployeeRemarks")%>' Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Manager Points"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <telerik:RadRating RenderMode="Lightweight" ID="radFinalEvaluationPoint" runat="server"
                                                SelectionMode="Continuous" Precision="item" Orientation="Horizontal" Value='<%# Eval("EvaluationPointbyEmployee")%>'
                                                OnRate="ratingGoals_Rate" AutoPostBack="true">
                                            </telerik:RadRating>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Manager Remarks"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFinalRemarks" runat="server" TextMode="MultiLine" Text='<%# Eval("EmployeeRemarks")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>


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
                        <div class="col-md-3">
                            <asp:Label ID="lblTotalGoalsPoints" runat="server" Text="Total Goals Points: "></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblTotalGoalsPointsVal" runat="server"></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="table-responsive">
                        <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter3" FilterContainerID="dgrdSkills"
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
                                                SelectionMode="Continuous" Precision="item" Orientation="Horizontal" Value='<%# Eval("EvaluationPointbyEmployee")%>'
                                                Enabled="false">
                                            </telerik:RadRating>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Remarks"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEmployeeSkillRemarks" runat="server" TextMode="MultiLine" Text='<%# Eval("EmployeeRemarks")%>'
                                                Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Weight %"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="radnumSkillWeight" MinValue="0" MaxValue="100"
                                                Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" Text='<%# Eval("Weight")%>'
                                                OnTextChanged="ratingSkills_Rate" AutoPostBack="true">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Points"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <telerik:RadRating RenderMode="Lightweight" ID="radFinalSkillEvaluationPointby" runat="server"
                                                SelectionMode="Continuous" Precision="item" Orientation="Horizontal" Value='<%# Eval("EvaluationPointbyEmployee")%>'
                                                OnRate="ratingSkills_Rate" AutoPostBack="true">
                                            </telerik:RadRating>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Remarks"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFinalSkillRemarks" runat="server" TextMode="MultiLine" Text='<%# Eval("EmployeeRemarks")%>'></asp:TextBox>
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
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label ID="lblTotalSkillsPoints" runat="server" Text="Total Skills Points: "></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="lblTotalSkillsPointsVal" runat="server"></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="row">
                <div class="col-md-6">
                    <asp:CheckBox ID="chkConfirm" runat="server" Text="Evaluation Completed" />
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

