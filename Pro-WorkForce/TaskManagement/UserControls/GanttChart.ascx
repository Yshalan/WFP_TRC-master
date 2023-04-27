<%@ Control Language="VB" AutoEventWireup="false" CodeFile="GanttChart.ascx.vb" Inherits="TaskManagement_UserControls_GanttChart" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>

<script type="text/javascript" src="../js/daypilot-modal-2.1.js"></script>
<script type="text/javascript" src="../js/jquery-1.9.1.min.js"></script>
<link href='../css/main.css' type="text/css" rel="stylesheet" />


<script type="text/javascript">
    function modal() {
        var m = new DayPilot.Modal();
        m.closed = function () {
            var data = this.result;
            //console.log(data);
            if (data == "OK") {
                Gantt.commandCallBack("refresh");
            }
        };
        return m;
    }

    function createChild(parent) {
        //modal().showUrl("TasksPopUp.aspx?ProjectId=" + <%=ProjectId%> + "&ParentId="  + parent.id()+ "&ClickType=Child");
        oWindow = radopen("TasksPopUp.aspx?ProjectId=" + <%=ProjectId%> + "&ParentId=" + parent.id() + "&ClickType=Child");
    }
    function add() {
        var start = new DayPilot.Date().getDatePart();
        var end = start.addDays(1);
        //modal().showUrl("TasksPopUp.aspx?ProjectId=" + <%=ProjectId%> );
        oWindow = radopen("TasksPopUp.aspx?ProjectId=" + <%=ProjectId%> );
    }
    function edit(e) {
        //modal().showUrl("TasksPopUp.aspx?ProjectId=" + <%=ProjectId%> + "&ClickType=Edit" + "&TaskId=" + e.id());
        oWindow = radopen("TasksPopUp.aspx?ProjectId=" + <%=ProjectId%> + "&ClickType=Edit" + "&TaskId=" + e.id());
    }

    $(document).ready(function () {
        if (<%=Lang%> == '1') {
            $('.gantt_default_divider_horizontal').prev().children()[3].innerText = "مخطط المشروع";
        }
        else {
            $('.gantt_default_divider_horizontal').prev().children()[3].innerText = "Project Gantt Chart";
        }
        
    });

</script>


<div class="space">
</div>


<DayPilot:DayPilotGantt
    runat="server"
    ID="Gantt"
    ClientIDMode="Static"
    RowCreateHandling="CallBack"
    RowMoveHandling="Notify"
    OnBeforeTaskRender="Gantt_OnBeforeTaskRender"
    OnRowCreate="Gantt_OnRowCreate"
    OnCommand="Gantt_OnCommand"
    OnRowMenuClick="Gantt_OnRowMenuClick"
    ContextMenuRowID="ContextMenuTask"
    ContextMenuLinkID="ContextMenuLink"
    LinkBottomMargin="5" CellGroupBy="Month" CrosshairColor="Gray" HeaderHeight="20" Height="300px" LinkCreatedJavaScript="" LinkCreateJavaScript="" 
    meta:resourcekey="GanttResource1" ScrollX="0" ScrollY="0" StartDate="2019-08-01" TaskClickedJavaScript="" TaskClickJavaScript="" 
    TaskDeletedJavaScript="" TaskDeleteHandling="Enabled" TaskDeleteJavaScript="" TaskDoubleClickedJavaScript="" TaskDoubleClickJavaScript="" 
    TaskHeight="25" TaskMovedJavaScript="" TaskMoveJavaScript="" TaskMovingJavaScript="" TaskResizedJavaScript="" TaskResizeJavaScript="" T
    askResizeMargin="5" TaskResizingJavaScript="" TaskRightClickedJavaScript="" TaskRightClickJavaScript="" Theme="gantt_default" WeekStarts="Auto">
    <Columns>

        <DayPilot:TaskColumn Title="Task" Property="text" Width="100" meta:resourcekey="TaskColumnTaskResource1" />
        <DayPilot:TaskColumn Title="Duration" Width="100" meta:resourcekey="DurationResource1" />
        <DayPilot:TaskColumn Title="Completion%" Property="complete" Width="100" meta:resourcekey="CompletionResource1" />
        <DayPilot:TaskColumn Title="Resources" Property="Project_Resources" Width="100" meta:resourcekey="ResourcesResource1" />
        <DayPilot:TaskColumn Title="From" Property="PlannedStartDate" Width="100" meta:resourcekey="FromResource1" />
        <DayPilot:TaskColumn Title="To" Property="PlannedEndDate" Width="100" meta:resourcekey="ToResource1" />
    </Columns>

</DayPilot:DayPilotGantt>

<DayPilot:DayPilotMenu runat="server" ID="ContextMenuTask" ClientIDMode="Static" MenuBackColor="#FFFFFF" MenuBorderColor="#ACA899" MenuItemColor="#2859AB" MenuTitleBackColor="#ECE9D8">
    <MenuItems>
        <DayPilot:MenuItem Text="Add Child Task..." Action="JavaScript" JavaScript="createChild(this.source);"></DayPilot:MenuItem>
        <DayPilot:MenuItem Text="-"></DayPilot:MenuItem>
        <DayPilot:MenuItem Text="Edit..." Action="JavaScript" JavaScript="edit(this.source)"></DayPilot:MenuItem>
        <%--<DayPilot:MenuItem Text="Convert to Milestone" Action="CallBack" Command="ToMilestone"></DayPilot:MenuItem>--%>
        <DayPilot:MenuItem Text="Delete" Action="CallBack" Command="Delete"></DayPilot:MenuItem>
    </MenuItems>
</DayPilot:DayPilotMenu>

<DayPilot:DayPilotMenu runat="server" ID="ContextMenuGroup" ClientIDMode="Static" MenuBackColor="#FFFFFF" MenuBorderColor="#ACA899" MenuItemColor="#2859AB" MenuTitleBackColor="#ECE9D8">
    <MenuItems>
        <DayPilot:MenuItem Text="Add Child Task..." Action="JavaScript" JavaScript="createChild(this.source);"></DayPilot:MenuItem>
        <DayPilot:MenuItem Text="-"></DayPilot:MenuItem>
        <DayPilot:MenuItem Text="Edit..." Action="JavaScript" JavaScript="edit(this.source)"></DayPilot:MenuItem>
        <DayPilot:MenuItem Text="Delete" Action="CallBack" Command="Delete"></DayPilot:MenuItem>
    </MenuItems>
</DayPilot:DayPilotMenu>

<DayPilot:DayPilotMenu runat="server" ID="ContextMenuMilestone" ClientIDMode="Static" MenuBackColor="#FFFFFF" MenuBorderColor="#ACA899" MenuItemColor="#2859AB" MenuTitleBackColor="#ECE9D8">
    <MenuItems>
        <DayPilot:MenuItem Text="Edit..." Action="JavaScript" JavaScript="edit(this.source)"></DayPilot:MenuItem>
        <%--<DayPilot:MenuItem Text="Convert to Task" Action="CallBack" Command="ToTask"></DayPilot:MenuItem>--%>
        <DayPilot:MenuItem Text="Delete" Action="CallBack" Command="Delete"></DayPilot:MenuItem>
    </MenuItems>
</DayPilot:DayPilotMenu>

<DayPilot:DayPilotMenu ID="ContextMenuLink" runat="server" MenuBackColor="#FFFFFF" MenuBorderColor="#ACA899" MenuItemColor="#2859AB" MenuTitleBackColor="#ECE9D8">
    <DayPilot:MenuItem Text="Delete" Action="Callback" Command="Delete"></DayPilot:MenuItem>
</DayPilot:DayPilotMenu>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Resize, Close, Move"
    EnableShadow="True" Height="530px" Width="700px" Animation="FlyIn" Behaviors="Resize, Close, Move" meta:resourcekey="RadWindowManager1Resource1">
    <Windows>
        <telerik:RadWindow ID="radwindowTaskDetails" runat="server" Animation="FlyIn"
            EnableShadow="True"
            InitialBehavior="None" ShowContentDuringLoad="False" VisibleStatusbar="False"
            CssClass="radwindowforchart" meta:resourcekey="radwindowTaskDetailsResource1">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
