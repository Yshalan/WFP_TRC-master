<%@ Page Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="ViewActiveSchedule.aspx.vb" Inherits="Admin_ViewActiveSchedule" Title="View Active Schedule"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="EmployeeFilter"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100px;
            padding: 30,10,10,30;
        }
    </style>
    <script type="text/javascript">
        function showPopup(path, name, height, width) {
            var options = 'width=' + width + ',height=' + height;
            var newwindow;
            newwindow = window.open(path, name, options);
            if (window.focus) {
                newwindow.focus();
            }
        }
        function open_window(url, target, w, h) { //opens new window 
            var parms = "width=" + w + ",height=" + h + ",menubar=no,location=no,resizable,scrollbars";
            var win = window.open(url, target, parms);
            if (win) {
                win.focus();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <div class="row">
        <div class="col-md-12">
            <uc2:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lbldate" runat="server" CssClass="Profiletitletxt" Text="Date" meta:resourcekey="lbldateResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadDatePicker ID="rdateviewactive" runat="server" Culture="English (United States)"
                meta:resourcekey="RadDatePicker1Resource1">
                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                    Width="">
                </DateInput><Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                    ViewSelectorText="x">
                </Calendar>
                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RFVviewActive" runat="server" ControlToValidate="rdateviewactive"
                Display="None" ErrorMessage="Please Enter Select Date" ValidationGroup="Get"
                meta:resourcekey="RFVviewActiveResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ExtenderViewActive" runat="server" Enabled="True"
                TargetControlID="RFVviewActive" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-4">
            <asp:CheckBox ID="chkFullList" runat="server" Text="Show Employee List" meta:resourcekey="chkFullListResource1"  Visible="false" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 text-center">
            <asp:Button ID="btnGet" runat="server" Text="Get" CssClass="button" meta:resourcekey="Button1Resource1" />
            <asp:Button ID="btnDelete" OnClientClick="return ValidateDelete();" runat="server" Text="Delete" CssClass="button" meta:resourcekey="Button2Resource1" />
        </div>
    </div>
    <div class="row">
        <div class="table-responsive">
            <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdViewSchedule"
                Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
            <telerik:RadGrid ID="dgrdViewSchedule" runat="server" AllowSorting="True" AllowPaging="True"
                GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="False"
                PageSize="15" ShowFooter="True" GroupingSettings-CaseSensitive="false">
                <SelectedItemStyle ForeColor="maroon" />
                <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False"
                    DataKeyNames="FromDate,ToDate,ScheduleName,EmpWorkScheduleId">
                    <CommandItemTemplate>
                        <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                            Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                            <Items>
                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                    ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                            </Items>
                        </telerik:RadToolBar>
                    </CommandItemTemplate>
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No"
                            meta:resourcekey="GridBound1ColumnResource1" />
                        <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                            meta:resourcekey="GridBound2ColumnResource1" />
                        <telerik:GridBoundColumn DataField="LocEngName" SortExpression="LocEngName" HeaderText="Name" />
                        <telerik:GridBoundColumn DataField="LocArbName" SortExpression="LocArbName" HeaderText="Arab Name" />
                        <telerik:GridBoundColumn DataField="ScheduleName" SortExpression="ScheduleName" HeaderText="Schedule Name"
                            meta:resourcekey="GridBound3ColumnResource1" />
                        <telerik:GridBoundColumn DataField="FromDate" SortExpression="FromDate" HeaderText="From Date"
                            DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBound4ColumnResource1" />
                        <telerik:GridBoundColumn DataField="ToDate" SortExpression="ToDate" HeaderText="To Date"
                            DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBound5ColumnResource1" />
                        <telerik:GridCheckBoxColumn DataField="IsTemporary" SortExpression="IsTemporary"
                            HeaderText="Is Temporary" ItemStyle-CssClass="nocheckboxstyle" />
                        <telerik:GridBoundColumn DataField="Remarks" SortExpression="Remarks" HeaderText="Schedule Source" />
                        <telerik:GridBoundColumn DataField="EmpWorkScheduleId" Visible="false" />
                        <telerik:GridBoundColumn DataField="UserID" SortExpression="UserID" HeaderText="Created By"
                            meta:resourcekey="GridBound7ColumnResource1" />
                        <telerik:GridBoundColumn DataField="CREATED_DATE" SortExpression="CREATED_DATE" HeaderText="Created Date"
                            DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBound8ColumnResource1" />
                    </Columns>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                </MasterTableView>
                <GroupingSettings CaseSensitive="False"></GroupingSettings>
                <ClientSettings EnableRowHoverStyle="True">
                    <%--<Selecting AllowRowSelect="True" />--%>
                </ClientSettings>
            </telerik:RadGrid>
        </div>
    </div>
    <script type="text/javascript">

        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdViewSchedule.ClientID %>");
                    var masterTable = grid.get_masterTableView();
                    var value = false;
                    for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                        var gridItemElement = masterTable.get_dataItems()[i].findElement("chk");
                        if (gridItemElement.checked) {
                            value = true;
                        }
                    }
                    if (value === false) {
                        alert("<%= Resources.Strings.ErrorDeleteRecourd %>");
                    }
                    return value;
                }
    </script>
</asp:Content>
