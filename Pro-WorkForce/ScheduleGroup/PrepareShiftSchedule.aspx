<%@ Page Title="" Language="VB"  Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="PrepareShiftSchedule.aspx.vb" Inherits="ScheduleGroup_PrepareShiftSchedule"
    UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/TAFilter.ascx" TagName="EmployeeFilter" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../CSS/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ScheduleGrid.css" rel="stylesheet" type="text/css" />
    <script src="../js/browser.js" type="text/javascript"></script>
    <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../js/jquery.dd.js" type="text/javascript"></script>
    <script src="../js/jsonpath.js" type="text/javascript"></script>
    <asp:Literal runat="server" ID="JSON" meta:resourcekey="JSONResource1" />
    <script src="../js/ScheduleGroupShift.js" type="text/javascript"></script>
    <script src="../js/brwsniff.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ShowDivColor() {
            $('#<%=divColor.ClientID%>').toggle();
        }
        $('#<%=divColor.ClientID%>').focusout(function () {
            $('#<%=divColor.ClientID%>').hide();
        });
        function ClearMonthTable() {
            if (Page_ClientValidate('WorkSchedule')) {

                var lst = jsonPath(groupJSON, "$[?(@.listed == \"true\")]");
                if (lst) {
                    for (var i = 0; i < lst.length; i++) {
                        if (lst[i]) {
                            lst[i].listed = "false";
                        }
                    }
                }
                var GrpTable = document.getElementById("tblSchedule");
                var GrpTableLength = GrpTable.rows.length;
                if (browserVersion().agent == 'msie' && browserVersion().version == '8.0') {
                    var tblScheduleRows = document.getElementById("tblSchedule");
                    var tableRows = tblScheduleRows.getElementsByTagName('tr');
                    var rowCount = tableRows.length;

                    for (var x = rowCount - 1; x > 0; x--) {
                        tblScheduleRows.removeChild(tableRows[x]);

                    }
                }

                else {
                    for (var nItem = 1; nItem < GrpTableLength; nItem++) {
                        GrpTable.deleteRow(1);
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnEmployeeIDz" runat="server" />
    <asp:HiddenField ID="hdnGroupIDs" runat="server" />
    <asp:HiddenField ID="hdnUICulture" runat="server" />
        <uc1:PageHeader ID="PageHeader1" runat="server" />
    <div id="divParameters" class="row">
        <div class="col-md-2">
            <asp:Label runat="server" ID="lblYear" CssClass="Profiletitletxt"
                Text="Year" meta:resourcekey="lblYearResource1" />
        </div>
        <div class="col-md-4">
            <asp:DropDownList runat="server" ID="ddlYear" DataTextField="txt" DataValueField="val"
                onchange="YearMonthChanged()" meta:resourcekey="ddlYearResource1">
            </asp:DropDownList>
        </div>
        <div class="col-md-2">
            <label id="lblYearMessage" style="color: Maroon;">
            </label>
        </div>
        </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label runat="server" ID="lblMonth" CssClass="Profiletitletxt"
                Text="Month" meta:resourcekey="lblMonthResource1" />
        </div>
        <div class="col-md-4">
            <asp:DropDownList runat="server" ID="ddlMonth" DataTextField="txt" DataValueField="val"
                onchange="YearMonthChanged()" meta:resourcekey="ddlMonthResource1">
            </asp:DropDownList>
        </div>
        <div class="col-md-2">
            <label id="lblMonthMessage" style="color: Maroon;">
            </label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label runat="server" ID="lblWorkSchedule" CssClass="Profiletitletxt"
                Text="Work Schedule" meta:resourcekey="lblWorkScheduleResource1" />
        </div>
        <div class="col-md-4">
            <asp:DropDownList runat="server" ID="ddlWorkSchedule" AppendDataBoundItems="True"
                DataTextField="ScheduleName" DataValueField="ScheduleId"
                onchange="ddlWorkSchedule_changed(this.options[this.selectedIndex].value);"
                meta:resourcekey="ddlWorkScheduleResource1">
                <asp:ListItem Text="-- Please select --" Value="0"
                    meta:resourcekey="ListItemResource1" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvWorkSchedule" runat="server" Display="None" ErrorMessage="Please Select Work Schedule"
                ControlToValidate="ddlWorkSchedule" InitialValue="0"
                ValidationGroup="WorkSchedule" meta:resourcekey="rfvWorkScheduleResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                TargetControlID="rfvWorkSchedule">
            </cc1:ValidatorCalloutExtender>
        </div>
        <div class="col-md-2">
            <label id="lblWorkSchedule" style="color: Maroon;">
            </label>
        </div>
    </div>
    <div class="row">
            <div id="divFilter" runat="server" style="float: right; margin-right: 30px">
            </div>
        </div>

    <div class="row" runat="server" id="divShift">
        <div class="col-md-2">
            <asp:Label runat="server" ID="lblShift" CssClass="Profiletitletxt" Text="Shift"
                Style="float: left" meta:resourcekey="lblShiftResource1" />
        </div>
        <div class="col-md-4">
            <div class="divSelect" id="divSelect" runat="server" style="border-color: #CCCCCC">
                <div id="selectedColor" style="display: inline; vertical-align: top; border-color: Aqua">
                    <div id="selectedCol" class="col" style="background-color: #FFFFFF;">
                    </div>
                    <label id="selectedTxt">
                    </label>
                </div>
                <%  If (IsArabic) Then%>
                <img id="arrow_down_Ar" src="../icons/arrow_down.png" onclick="ShowDivColor();" style="float: left; padding-left: 2px; padding-top: 2px" />
                <%Else%>
                <img id="arrow_down" src="../icons/arrow_down.png" onclick="ShowDivColor();" />
                <%End If%>
            </div>
            <div class="divColor" id="divColor" runat="server">
            </div>
        </div>
        </div>
    </br>
        <div class="row">
            <div class="table-responsive">
                <table id="tblSchedule" border="1" rules="all">
                </table>
            </div>
        </div>
        <div id="divControls" runat="server" class="row">
            <div class="col-md-12 text-center">
            <asp:Button runat="server" ID="btnSave" Text="Save" OnClientClick="PostData(); return false;"
                CssClass="button" meta:resourcekey="btnSaveResource1" />
            <asp:Button runat="server" ID="btnClear" Text="Clear" OnClientClick="ClearForm(); return false;"
                CssClass="button" meta:resourcekey="btnClearResource1" /><%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>--%>
            <img id="imgLoading" src="../images/loading.gif" style="width: 25px; height: 25px;
                display: none;" />
        </div>
    </div>
    <script type="text/javascript">


        //	the following lines to assign controls to variables to use in the js file
        //	this script must be at the end of page to avoid script errors (undefined error)
        var cntrl_ddlWorkSchedule = document.getElementById('<%=ddlWorkSchedule.ClientID %>');
        var cntrl_ddlYear = document.getElementById('<%=ddlYear.ClientID %>');
        var cntrl_ddlMonth = document.getElementById('<%=ddlMonth.ClientID %>');
        var cntrl_lblYearMessage = document.getElementById('lblYearMessage');
        var cntrl_lblMonthMessage = document.getElementById('lblMonthMessage');
        //var cntrl_lblWorkSchedule = document.getElementById('lblWorkSchedule');
        var cntrl_tblSchedule = document.getElementById('tblSchedule');
        var cntrl_divColor = document.getElementById('<%=divColor.ClientID %>');
        var cntrl_btnSave = document.getElementById('<%=btnSave.ClientID %>');
        var cntrl_btnClear = document.getElementById('<%=btnClear.ClientID %>');
        var cntrl_imgLoading = document.getElementById('imgLoading');
        var hdnEmployeez = document.getElementById('<%=hdnEmployeeIDz.ClientID %>');
        var hdnGroupIDs = document.getElementById('<%=hdnGroupIDs.ClientID %>');
    

      
    </script>
    <script type="text/javascript">
        //call after page loaded

      
    </script>
</asp:Content>
