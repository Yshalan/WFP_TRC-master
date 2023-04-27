<%@ Page Language="VB" AutoEventWireup="false" Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master"
    CodeFile="ViewEmployeeManager.aspx.vb" Inherits="Admin_ViewEmployeeManager" meta:resourcekey="PageResource1"
    UICulture="auto" %>

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
                <uc2:EmployeeFilter ID="EmployeeFilter1" runat="server" ValidationGroup="Get" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lbldate" runat="server" CssClass="Profiletitletxt" Text="Date" meta:resourcekey="lbldateResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadDatePicker ID="rdateviewactive" runat="server" Culture="English (United States)"
                    meta:resourcekey="RadDatePicker1Resource1" >
                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                        Width="">
                    </DateInput><Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                        ViewSelectorText="x">
                    </Calendar>
                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                </telerik:RadDatePicker>
                 </div>
                <div class="col-md-2">
                <br />
                <br />
                <asp:RequiredFieldValidator ID="RFVviewActive" runat="server" ControlToValidate="rdateviewactive"
                    Display="Dynamic"  ErrorMessage="Please Enter Select Date" ValidationGroup="Get"
                    meta:resourcekey="RFVviewActiveResource1"></asp:RequiredFieldValidator></div>   
                <%--<cc1:ValidatorCalloutExtender ID="ExtenderViewActive" runat="server" Enabled="True"
                    TargetControlID="RFVviewActive" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                </cc1:ValidatorCalloutExtender>--%>
           
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:Button ID="btnGet" runat="server" Text="Get" ValidationGroup="Get" CssClass="button"
                    meta:resourcekey="Button1Resource1" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" meta:resourcekey="Button2Resource1" />
            </div>
        </div>
        <div class="row">
            <div class="table-responsive">
                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdViewManager"
                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                <telerik:RadGrid ID="dgrdViewManager" runat="server" AllowSorting="True" AllowPaging="True"
                    PageSize="15"  GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                    ShowFooter="True" GroupingSettings-CaseSensitive="false" meta:resourcekey="dgrdVwMgrResource1">
                    <SelectedItemStyle ForeColor="maroon" />
                    <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="FromDate,ToDate,EmployeeArabicName,ManagerArabicName,ManagerName,EmpManagerId">
                        <CommandItemTemplate>
                            <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1">
                                <Items>
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No"
                                meta:resourcekey="GridBoundColumnResource1" />
                            <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                meta:resourcekey="GridBoundColumnResource2" />
                            <telerik:GridBoundColumn DataField="EmployeeArabicName" Visible="false" />
                             <telerik:GridBoundColumn DataField="ManagerNo" SortExpression="ManagerNo" HeaderText="Manager No"
                                meta:resourcekey="GridBoundColumnResource10" />
                            <telerik:GridBoundColumn DataField="ManagerName" SortExpression="ManagerName" HeaderText="Manager Name"
                                meta:resourcekey="GridBoundColumnResource3" />
                            <telerik:GridBoundColumn DataField="ManagerArabicName" Visible="false" />
                            <telerik:GridBoundColumn DataField="FromDate" SortExpression="FromDate" HeaderText="From Date"
                                DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource4" />
                            <telerik:GridBoundColumn DataField="ToDate" SortExpression="ToDate" HeaderText="To Date"
                                DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource5" />
                            <telerik:GridCheckBoxColumn DataField="IsTemporary" SortExpression="IsTemporary"
                                HeaderText="Is Temporary" meta:resourcekey="GridBoundColumnResource6" ItemStyle-CssClass="nocheckboxstyle">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn DataField="EmpManagerId" SortExpression="EmpManagerId" Visible="false"
                                 />
                        </Columns>
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                <Items>
                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                        ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    </MasterTableView><ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </div>
</asp:Content>
