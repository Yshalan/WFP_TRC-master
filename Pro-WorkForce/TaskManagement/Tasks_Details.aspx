<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Tasks_Details.aspx.vb" Inherits="Definitions_Tasks_Details" Theme="SvTheme" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Src="~/TaskManagement/UserControls/GanttChart.ascx" TagPrefix="uc1" TagName="UCGanttChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function openRadWin() {
            oWindow = radopen("../TaskManagement/TasksPopUp.aspx?ProjectId=" + <%=ProjectId%> +"", "radwindowTaskDetails");
        }

        function openEditRadWin(TaskId) {
            oWindow = radopen("../TaskManagement/TasksPopUp.aspx?ProjectId=" + <%=ProjectId%> + "&ClickType=Edit" + "&TaskId=" + TaskId + "", "radwindowTaskDetails");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <asp:MultiView ID="mvProjects" ActiveViewIndex="0" runat="server">
        <asp:View runat="server" ID="viewProjects">
            <div class="table-responsive">
                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                <div class="filterDiv">
                    <telerik:RadFilter runat="server" ID="RadFilterProjects" Skin="Hay" FilterContainerID="dgrdProjects"
                        ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilterProjectsResource1">
                    </telerik:RadFilter>
                </div>
                <telerik:RadGrid runat="server" ID="dgrdProjects" AutoGenerateColumns="False" PageSize="15"
                    AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" meta:resourcekey="dgrdProjectsResource1">
                    <GroupingSettings CaseSensitive="False"></GroupingSettings>
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                        EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView AllowFilteringByColumn="False" CommandItemDisplay="Top" DataKeyNames="ProjectId">
                        <Columns>
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="ProjectId" Display="False" FilterControlAltText="Filter ProjectId column" HeaderText="ProjectId" meta:resourcekey="GridBoundColumnResource1" SortExpression="ProjectId" UniqueName="ProjectId">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ProjectName" FilterControlAltText="Filter ProjectName column" HeaderText="Project Name" meta:resourcekey="GridBoundColumnResource2" Resizable="False" SortExpression="ProjectName" UniqueName="ProjectName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ProjectArabicName" FilterControlAltText="Filter ProjectArabicName column" HeaderText="Project Arabic Name" meta:resourcekey="GridBoundColumnResource3" SortExpression="ProjectArabicName" UniqueName="ProjectArabicName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PlannedStartDate" DataFormatString="{0:dd/MM/yyyy}" FilterControlAltText="Filter PlannedStartDate column" HeaderText="Planned Start Date" meta:resourcekey="GridBoundColumnResource4" Resizable="False" SortExpression="PlannedStartDate" UniqueName="PlannedStartDate">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PlannedEndDate" DataFormatString="{0:dd/MM/yyyy}" FilterControlAltText="Filter PlannedEndDate column" HeaderText="Planned End Date" meta:resourcekey="GridBoundColumnResource5" Resizable="False" SortExpression="PlannedEndDate" UniqueName="PlannedEndDate">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <telerik:RadToolBar ID="RadToolBar1" runat="server" meta:resourcekey="RadToolBar1Resource1" OnButtonClick="RadToolBar1_ButtonClick" SingleClick="None" Skin="Hay">
                                <Items>
                                    <telerik:RadToolBarButton runat="server" CommandName="FilterRadGrid" ImagePosition="Right" ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource1" Text="Apply filter">
                                    </telerik:RadToolBarButton>
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </asp:View>
        <asp:View runat="server" ID="viewTasks">
            <asp:UpdatePanel ID="Update1" runat="server">
                <ContentTemplate>

                    <div class="row">
                        <div class="col-md-3">
                            <asp:Button ID="btnAdd" runat="server" Text="Add Task" OnClientClick="openRadWin(); return false" meta:resourcekey="btnAddResource1" />
                        </div>
                        <div class="col-md-5">
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="lnkviewGraph" runat="server" CssClass="imgbtn_chart">
                                <img src="../images/charts-icon-16.png" />
                                <asp:Label ID="litGantt" runat="server" Text="View Gantt Chart" meta:resourcekey="litGanttResource1"></asp:Label>
                            </asp:LinkButton>
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="lnkviewProjects" runat="server" CssClass="imgbtn_chart">
                                <img src="../images/viewproject.png" />
                                <asp:Label ID="Label1" runat="server" Text="View Projects" meta:resourcekey="Label1Resource1"></asp:Label>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel2" meta:resourcekey="RadAjaxLoadingPanel2Resource1" />
                            <div class="filterDiv">
                                <telerik:RadFilter runat="server" ID="RadTasks" Skin="Hay" FilterContainerID="dgrdTasks"
                                    ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadTasksResource1">
                                </telerik:RadFilter>
                            </div>
                            <telerik:RadGrid runat="server" ID="dgrdTasks" AutoGenerateColumns="False" PageSize="15"
                                AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" meta:resourcekey="dgrdTasksResource1">
                                <GroupingSettings CaseSensitive="False"></GroupingSettings>
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnableRowHoverStyle="True">
                                </ClientSettings>
                                <MasterTableView AllowFilteringByColumn="False" CommandItemDisplay="Top" DataKeyNames="TaskId">
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="False" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource1" Text="&nbsp;" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn AllowFiltering="False" DataField="TaskId" Display="False" FilterControlAltText="Filter TaskId column" HeaderText="TaskId" meta:resourcekey="GridBoundColumnResource6" SortExpression="TaskId" UniqueName="TaskId">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TaskName" FilterControlAltText="Filter TaskName column" HeaderText="Task Name" meta:resourcekey="GridBoundColumnResource7" Resizable="False" SortExpression="TaskName" UniqueName="TaskName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PlannedStartDate" DataFormatString="{0:dd/MM/yyyy HH:mm}" FilterControlAltText="Filter PlannedStartDate column" HeaderText="Start Date" meta:resourcekey="GridBoundColumnResource8" Resizable="False" SortExpression="PlannedStartDate" UniqueName="PlannedStartDate">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PlannedEndDate" DataFormatString="{0:dd/MM/yyyy HH:mm}" FilterControlAltText="Filter PlannedEndDate column" HeaderText="End Date" meta:resourcekey="GridBoundColumnResource9" Resizable="False" SortExpression="PlannedEndDate" UniqueName="PlannedEndDate">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Project_Resources" FilterControlAltText="Filter Project_Resources column" HeaderText="Resources" meta:resourcekey="GridBoundColumnResource10" Resizable="False" SortExpression="Project_Resources" UniqueName="Project_Resources">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Totalcompletionpercentage" FilterControlAltText="Filter Totalcompletionpercentage column" HeaderText="Total Completion%" meta:resourcekey="GridBoundColumnResource11" Resizable="False" SortExpression="Totalcompletionpercentage" UniqueName="Totalcompletionpercentage">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TaskSequence" FilterControlAltText="Filter TaskSequence column" HeaderText="Task No." meta:resourcekey="GridBoundColumnResource12" Resizable="False" SortExpression="TaskSequence" UniqueName="TaskSequence">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="False" FilterControlAltText="Filter TemplateColumnlnkRequest column" meta:resourcekey="GridTemplateColumnResource2" UniqueName="TemplateColumnlnkRequest">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEditTask" runat="server" meta:resourcekey="lnkEditTaskResource1" OnClick="lnkEditTask_Click" OnClientClick='<%# Eval("TaskId", "openEditRadWin({0});return false;") %>' Text="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar ID="RadToolBar1" runat="server" meta:resourcekey="RadToolBar1Resource2" OnButtonClick="RadToolBar1_ButtonClick" SingleClick="None" Skin="Hay">
                                            <Items>
                                                <telerik:RadToolBarButton runat="server" CommandName="FilterRadGrid" ImagePosition="Right" ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource2" Text="Apply filter">
                                                </telerik:RadToolBarButton>
                                            </Items>
                                        </telerik:RadToolBar>
                                    </CommandItemTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:HiddenField ID="hdnTaskId" runat="server" />
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server"
                EnableShadow="True" meta:resourcekey="RadWindowManager1Resource1">
                <Windows>
                    <telerik:RadWindow ID="radwindowTaskDetails" runat="server" Animation="FlyIn" Behavior="Resize, Close, Move"
                        Behaviors="Resize, Close, Move" EnableShadow="True" Height="530px" ShowContentDuringLoad="False" VisibleStatusbar="False"
                        Width="700px" meta:resourcekey="radwindowTaskDetailsResource1">
                    </telerik:RadWindow>
                </Windows>
            </telerik:RadWindowManager>
        </asp:View>
        <asp:View runat="server" ID="viewGantt">
            <div class="row">
                <div class="col-md-8">
                </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="lnkviewTasks" runat="server" CssClass="imgbtn_chart">
                        <img src="../images/viewtask.png" />
                        <asp:Label ID="Label3" runat="server" Text="View Tasks" meta:resourcekey="Label3Resource1"></asp:Label>
                    </asp:LinkButton>
                </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="lnkviewProjects2" runat="server" CssClass="imgbtn_chart">
                        <img src="../images/viewproject.png" />
                        <asp:Label ID="Label2" runat="server" Text="View Projects" meta:resourcekey="Label2Resource1"></asp:Label>
                    </asp:LinkButton>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" dir="ltr">
                    
                        <uc1:UCGanttChart ID="UCGanttChart1" runat="server" />
                    
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

