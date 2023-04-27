<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ApproveViolationToManaftPayroll.aspx.vb"
    Theme="SvTheme" MasterPageFile="~/Default/ReportMaster.master" Inherits="DailyTasks_ApproveViolationToManaftPayroll"
    meta:resourcekey="PageResource1" UICulture="auto" %>


<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc3" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagPrefix="uc1" TagName="EmployeeFilter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--    <style type="text/css">
        .SelectedStyle {
            background-color: red !important;
        }

        .ui-checkbox input[type="checkbox"] {
            display: block;
            height: 20px;
            width: 16px;
        }
    </style>--%>
    <script type="text/javascript">
        function Select() {
            var masterTable = $find("<%= dgrdViewApproveViolation.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            for (var i = 0; i < row.length; i++) {
                masterTable.get_dataItems()[i].set_selected(true);
            }
        }
        function deselect() {
            NegotiationsGrid.MasterTableView.Control.getElementsByTagName("INPUT");
            var masterTable = $find("<%= dgrdViewApproveViolation.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            for (var i = 0; i < row.length; i++) {
                masterTable.get_dataItems()[i].set_selected(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc3:PageHeader ID="PageHeader1" runat="server" />
    <br />

    <asp:MultiView ID="mvApproval" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwList" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblYear" runat="server" CssClass="Profiletitletxt" Text="Year" meta:resourcekey="lblYearResource1"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList runat="server" ID="ddlYear" DataTextField="txt" AutoPostBack="true" DataValueField="val" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                        meta:resourcekey="ddlYearResource1">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblMonth" runat="server" CssClass="Profiletitletxt" Text="Month" meta:resourcekey="lblMonthResource1"></asp:Label>
                </div>
                <div class="col-md-2">
                    <asp:DropDownList runat="server" ID="ddlMonth" DataTextField="txt" DataValueField="val" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"
                        meta:resourcekey="ddlMonthResource1">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" align="center">
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-2">
                        <%--<span>--%>
                        <asp:CheckBox runat="server" ID="chkDelay" AutoPostBack="false" OnCheckedChanged="chkDelay_CheckedChanged" meta:resourcekey="chkDelayResource1" Text="&nbsp;" />
                        <asp:Label runat="server" ID="lblDelay" Text="Delay" meta:resourcekey="lblDelayResource"></asp:Label>
                        <%--</span>--%>
                    </div>
                    <div class="col-md-2">
                        <%--<span>--%>
                        <asp:CheckBox runat="server" ID="chkEarlyOut" AutoPostBack="false" OnCheckedChanged="chkEarlyOut_CheckedChanged" meta:resourcekey="chkEarlyOutResource1" Text="&nbsp;" />
                        <asp:Label runat="server" ID="Label1" Text="Early Out" meta:resourcekey="lblEarlyOutResource"></asp:Label>
                        <%--</span>--%>
                    </div>
                    <div class="col-md-2">
                        <%--<span>--%>
                        <asp:CheckBox runat="server" ID="chkOutDuration" Visible="true" meta:resourcekey="chkOutDurationResource1" Text="&nbsp;" />
                        <asp:Label runat="server" ID="Label5" Text="Out Durtion" Visible="true" meta:resourcekey="lblOutDurationResource"></asp:Label>
                        <%--</span>--%>
                    </div>
                    <div class="col-md-2">
                    </div>
                    <div class="col-md-2">
                    </div>
                    <div>
                        <%--<span>--%>
                        <asp:CheckBox runat="server" ID="chkAbsent" Visible="false" meta:resourcekey="chkAbsentResource1" Text="&nbsp;" />
                        <asp:Label runat="server" ID="Label2" Text="Absent" Visible="false" meta:resourcekey="lblAbsentResource"></asp:Label>
                        <%--</span>--%>
                    </div>
                    <div>
                        <%--<span>--%>
                        <asp:CheckBox runat="server" ID="chkMissingIn" Visible="false" meta:resourcekey="chkMissingInResource1" Text="&nbsp;" />
                        <asp:Label runat="server" ID="Label3" Text="Missing In" Visible="false" meta:resourcekey="lblMissingInResource"></asp:Label>
                        <%--</span>--%>
                    </div>
                    <div>
                        <%--<span>--%>
                        <asp:CheckBox runat="server" ID="chkMissingOut" Visible="false" meta:resourcekey="chkMissingOutResource1" Text="&nbsp;" />
                        <asp:Label runat="server" ID="Label4" Text="Missing Out" Visible="false" meta:resourcekey="lblMissingOutResource"></asp:Label>
                        <%--</span>--%>
                    </div>

                </div>
            </div>
            <div class="row">
                <div class="col-md-12" align="center">
                    <asp:Button ID="btnRetrive" Text="Retrieve Violation(s)" CssClass="button" runat="server"
                        meta:resourcekey="btnRetriveResource1"></asp:Button>
                    <asp:Button ID="btnApprove" Text="Approve Violation(s)" CssClass="button" runat="server"
                        meta:resourcekey="btnApproveResource1"></asp:Button>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" align="center">
                    <asp:ImageButton ID="btnExportToPDF" Visible="False" ImageUrl="~/Icons/pdf.png" runat="server" Width="40px"
                        meta:resourcekey="btnExportToPDFResource1" />
                    <asp:ImageButton ID="btnExportToExcel" Visible="False" ImageUrl="~/Icons/Microsoft-Excel-icon.png"
                        runat="server" Width="40px" meta:resourcekey="btnExportToExcelResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadGrid ID="dgrdViewApproveViolation" runat="server" AllowPaging="True"
                        AllowSorting="False" GridLines="None" ShowStatusBar="True"
                        AutoGenerateColumns="False" PageSize="15" OnItemCommand="dgrdViewApproveViolation_ItemCommand"
                        ShowFooter="False" meta:resourcekey="dgrdViewApproveViolationResource1" CellSpacing="0">
                        <GroupingSettings CaseSensitive="False" />
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView AllowMultiColumnSorting="False" CommandItemDisplay="Top"
                            DataKeyNames="RecordId,FK_EmployeeId,EmployeeNo,EmployeeArabicName,EmployeeName">
                            <DetailTables>
                                <telerik:GridTableView AllowSorting="false" runat="server" DataKeyNames="FK_EmployeeId,RecordId" meta:resourcekey="GridTableViewResource1" Name="EmployeeGridView" Width="100%">
                                    <ParentTableRelation>
                                        <telerik:GridRelationFields DetailKeyField="FK_EmployeeId" MasterKeyField="FK_EmployeeId" />
                                    </ParentTableRelation>
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="RecordId" FilterControlAltText="Filter RecordId column" HeaderText="Id" Visible="false" meta:resourceKey="GridBoundColumnResourceRecordId" SortExpression="RecordId" UniqueName="RecordId">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" FilterControlAltText="Filter FK_EmployeeId column" HeaderText="FK_EmployeeId" meta:resourceKey="GridBoundColumnEmployeeId" SortExpression="FK_EmployeeId" UniqueName="FK_EmployeeId" Visible="False">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeNo" FilterControlAltText="Filter EmployeeNo column" HeaderText="Employee No" meta:resourceKey="GridBoundColumnResourceEmployeeNo" SortExpression="EmployeeNo" UniqueName="EmployeeNo">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="StrFromDate" FilterControlAltText="Filter StrFromDate column" HeaderText="From Date" meta:resourceKey="GridBoundColumnResourceFromDate" SortExpression="StrFromDate" UniqueName="StrFromDate">
                                            <HeaderStyle Width="100px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="StrTodate" FilterControlAltText="Filter StrTodate column" HeaderText="To Date" meta:resourceKey="GridBoundColumnResourceTodate" SortExpression="StrTodate" UniqueName="StrTodate">
                                            <HeaderStyle Width="100px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DelayCount" FilterControlAltText="Filter DelayCount column" HeaderText="Delay Count / Days" meta:resourceKey="GridBoundColumnResourceDelayCount" SortExpression="DelayCount" UniqueName="DelayCount">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DelayDuration" FilterControlAltText="Filter DelayDuration column" HeaderText="Delay Duration / Mins" meta:resourceKey="GridBoundColumnResourceDelayDuration" SortExpression="DelayDuration" UniqueName="DelayDuration">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EarlyOutCount" FilterControlAltText="Filter EarlyOutCount column" HeaderText="Early Out Count / Days" meta:resourceKey="GridBoundColumnResourceEarlyOutCount" SortExpression="EarlyOutCount" UniqueName="EarlyOutCount">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EarlyOutDuration" FilterControlAltText="Filter EarlyOutDuration column" HeaderText="Early Out Duration / Mins" meta:resourceKey="GridBoundColumnResourceEarlyOutDuration" SortExpression="EarlyOutDuration" UniqueName="EarlyOutDuration">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="OutDurationCount" FilterControlAltText="Filter OutDurationCount column" HeaderText="Out Duration Count / Days" meta:resourceKey="GridBoundColumnResourceOutDurationCount" SortExpression="OutDurationCount" UniqueName="OutDurationCount">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="OutDuration" FilterControlAltText="Filter OutDuration column" HeaderText="Out Duration Duration / Mins" meta:resourceKey="GridBoundColumnResourceOutDuration" SortExpression="OutDuration" UniqueName="OutDuration">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AbsentCount" FilterControlAltText="Filter AbsentCount column" Visible="false" HeaderText="Absent Count / Days" meta:resourceKey="GridBoundColumnResourceAbsentCount" SortExpression="AbsentCount" UniqueName="AbsentCount">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MissingInCount" FilterControlAltText="Filter MissingInCount column" Visible="false" HeaderText="Missing In Count / Days" meta:resourceKey="GridBoundColumnResourceMissingInCount" SortExpression="MissingInCount" UniqueName="MissingInCount">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MissingOutCount" FilterControlAltText="Filter MissingOutCount column" Visible="false" HeaderText="Missing Out Count / Days" meta:resourceKey="GridBoundColumnResourceMissingOutCount" SortExpression="MissingOutCount" UniqueName="MissingOutCount">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar ID="RadToolBar1" runat="server" meta:resourceKey="RadToolBar1Resource1" OnButtonClick="RadToolBar1_ButtonClick" SingleClick="None" Skin="Hay">
                                            <Items>
                                                <telerik:RadToolBarButton runat="server" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif" meta:resourceKey="RadToolApplyFilterResource1" Owner="" Text="Apply filter">
                                                </telerik:RadToolBarButton>
                                            </Items>
                                        </telerik:RadToolBar>
                                    </CommandItemTemplate>
                                </telerik:GridTableView>
                            </DetailTables>
                            <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="False" ShowRefreshButton="False" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourceKey="GridTemplateColumnResource1" UniqueName="chk">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" Text="&nbsp;" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" AutoPostBack="True" Checked='<%# Eval("IsSelected")%>' OnCheckedChanged="chk_CheckedChanged" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="EmployeeNo" AllowFiltering="False" HeaderText="Employee No" meta:resourceKey="GridBoundColumnResourceEmployeeNo" SortExpression="EmployeeNo" UniqueName="EmployeeNo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeName" AllowFiltering="False" HeaderText="Employee Name" meta:resourceKey="GridBoundColumnResourceEmployeeName" SortExpression="EmployeeName" UniqueName="EmployeeName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeArabicName" AllowFiltering="False" HeaderText="Employee Arabic Name" meta:resourceKey="GridBoundColumnResourceEmployeeArabicName" SortExpression="EmployeeArabicName" UniqueName="EmployeeArabicName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DesignationName" AllowFiltering="False" HeaderText="Designation" meta:resourceKey="GridBoundColumnResourceDesignationName" SortExpression="DesignationName" UniqueName="DesignationName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DesignationArabicName" AllowFiltering="False" HeaderText="Designation Arabic Name" meta:resourceKey="GridBoundColumnResourceDesignationArabicName" SortExpression="DesignationArabicName" UniqueName="DesignationArabicName">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <SelectedItemStyle ForeColor="Maroon" />
                    </telerik:RadGrid>
                </div>
            </div>
        </asp:View>
        <asp:View ID="vwFinalApproval" runat="server">
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadGrid runat="server" ID="dgDeduction" AutoGenerateColumns="False" AllowSorting="False" GridLines="None" Width="100%"
                        meta:resourcekey="dgEmpAttResource1" CellSpacing="0" OnItemCommand="dgDeduction_ItemCommand">
                        <GroupingSettings CaseSensitive="False" />
                        <MasterTableView AllowMultiColumnSorting="False" CommandItemDisplay="Top" DataKeyNames="DeductionId,ReferenceId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="ReferenceId" FilterControlAltText="Filter ReferenceId column" HeaderText="Reference Id" meta:resourceKey="GridBoundReferenceIdResource" SortExpression="ReferenceId" UniqueName="ReferenceId" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Employee_Number" FilterControlAltText="Filter Employee_Number column" HeaderText="Employee Number" meta:resourceKey="GridBoundEmployee_NumberResource" SortExpression="Employee_Number" UniqueName="Employee_Number">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Date_From" DataFormatString="{0:dd/MM/yyyy}" FilterControlAltText="Filter Date_From column" HeaderText="Date From" meta:resourceKey="GridBoundDate_FromResource" SortExpression="Date_From" UniqueName="Date_From">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Date_To" DataFormatString="{0:dd/MM/yyyy}" FilterControlAltText="Filter Date_To column" HeaderText="Date To" meta:resourceKey="GridBoundDate_ToResource" SortExpression="Date_To" UniqueName="Date_To">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Deduction_Days" FilterControlAltText="Filter Deduction_Days column" HeaderText="Deduction Days" meta:resourceKey="GridBoundDeduction_DaysResource" SortExpression="Deduction_Days" UniqueName="Deduction_Days">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Deduction_Hours" FilterControlAltText="Filter Deduction_Hours column" HeaderText="Deduction Hours" meta:resourceKey="GridBoundDeduction_HoursResource" SortExpression="Deduction_Hours" UniqueName="Deduction_Hours">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Deduction_Mins" FilterControlAltText="Filter Deduction_Mins column" HeaderText="Deduction Mins" meta:resourceKey="GridBoundDeduction_MinsResource" SortExpression="Deduction_Mins" UniqueName="Deduction_Mins">
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn CommandName="Remove" Text="Remove" UniqueName="btnRemove" meta:resourceKey="GridBoundDeduction_btnRemoveResource">
                                </telerik:GridButtonColumn>
                            </Columns>
                            <CommandItemTemplate>
                                <telerik:RadToolBar ID="RadToolBar1" runat="server" meta:resourceKey="RadToolBar1Resource1" OnButtonClick="RadToolBar1_ButtonClick" SingleClick="None" Skin="Hay">
                                    <Items>
                                        <telerik:RadToolBarButton runat="server" CommandName="FilterRadGrid" ImagePosition="Right" ImageUrl="~/images/RadFilter.gif" meta:resourceKey="RadToolBarButtonResource1" Owner="" Text="Apply filter">
                                        </telerik:RadToolBarButton>
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>

                    <div class="text-right">
                        <asp:Button ID="btnGoBackList" runat="server" OnClick="btnGoBackList_Click"
                            CssClass="button" Text="Go to Violation Lists" meta:resourcekey="btnGoBackListResource" />
                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                            CssClass="button" Text="Submit" meta:resourcekey="btnSubmitResource" />
                    </div>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
