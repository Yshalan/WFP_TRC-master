<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ApproveViolationToDOF.aspx.vb" EnableEventValidation="true"
    Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" Inherits="DailyTasks_ApproveViolationToDOF"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc3" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagPrefix="uc1" TagName="EmployeeFilter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <style type="text/css">
        .SelectedStyle
        {
            background-color: red !important;
        }

        .ui-checkbox input[type="checkbox"]
        {
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="updateprogressAssign">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" EnableViewState="false" DisplayAfter="0">
                    <ProgressTemplate>
                        <div id="tblLoading" style="width: 100%; height: 100%; z-index: 100002 !important; text-align: center; position: fixed; left: 0px; top: 0px; background-size: cover; background-image: url('../images/Grey_fade.png');">

                            <div class="animategif">
                                <div align="center">
                                    <%--background-image: url('../images/STS_Loading.gif');--%>
                                    <asp:Image ID="imgLoading" runat="server" ImageAlign="AbsBottom" ImageUrl="~/images/STS_Loading.gif" Width="250px" Height="250px" />
                                </div>
                            </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="vGrid" runat="server">

                    <uc3:PageHeader ID="PageHeader1" runat="server" />
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <uc1:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" ShowDirectStaffCheck="true" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblYear" runat="server" CssClass="Profiletitletxt" Text="Year" meta:resourcekey="lblYearResource1"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList runat="server" ID="ddlYear" DataTextField="txt" DataValueField="val"
                                meta:resourcekey="ddlYearResource1">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblMonth" runat="server" CssClass="Profiletitletxt" Text="Month" meta:resourcekey="lblMonthResource1"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList runat="server" ID="ddlMonth" DataTextField="txt" DataValueField="val"
                                meta:resourcekey="ddlMonthResource1">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" align="center">
                            <asp:Button ID="btnProcess" Text="Process Violation(s)" OnClientClick="javascript:SaveWithParameter('ProcessRecal')" ClientIDMode="Static" CssClass="button" runat="server"
                                meta:resourcekey="btnProcessResource1"></asp:Button>
                            <asp:Button ID="btnRefresh" Text="Refresh" CssClass="button" runat="server" OnClick="btnRefresh_Click"
                                meta:resourcekey="btnRefreshResource1"></asp:Button>
                            <asp:Button ID="btnRetrive" Text="Retrieve Violation(s)" CssClass="button" runat="server"
                                meta:resourcekey="btnRetriveResource1"></asp:Button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter2" FilterContainerID="dgrdRecalculateRequest"
                                ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                            </telerik:RadFilter>
                            <telerik:RadGrid ID="dgrdRecalculateRequest" runat="server" AllowPaging="True" AllowSorting="True"
                                GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" AutoGenerateColumns="False"
                                PageSize="15" OnItemCommand="dgrdRecalculateRequest_ItemCommand" ShowFooter="True"
                                meta:resourcekey="dgrdRecalculateRequest" CellSpacing="0">
                                <SelectedItemStyle ForeColor="Maroon" />
                                <MasterTableView CommandItemDisplay="Top">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="CompanyName" Display="False" HeaderText="Company Name"
                                            SortExpression="CompanyName" Resizable="False" meta:resourcekey="Request_CompanyName"
                                            UniqueName="CompanyName" FilterControlAltText="Filter CompanyName column" />
                                        <telerik:GridBoundColumn DataField="CompanyArabicName" Display="False" HeaderText="Company Name"
                                            SortExpression="CompanyArabicName" Resizable="False" meta:resourcekey="Request_CompanyArabicName"
                                            UniqueName="CompanyArabicName" FilterControlAltText="Filter CompanyArabicName column" />
                                        <telerik:GridBoundColumn DataField="EntityName" Display="False" HeaderText="Entity Name"
                                            SortExpression="EntityName" Resizable="False" meta:resourcekey="Request_EntityName"
                                            UniqueName="EntityName" FilterControlAltText="Filter EntityName column" />
                                        <telerik:GridBoundColumn DataField="EntityArabicName" Display="False" HeaderText="Entity Name"
                                            SortExpression="EntityArabicName" Resizable="False" meta:resourcekey="Request_EntityArabicName"
                                            UniqueName="EntityArabicName" FilterControlAltText="Filter EntityArabicName column" />
                                        <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee Number" SortExpression="EmployeeNo"
                                            Resizable="False" meta:resourcekey="Request_EmployeeNo" UniqueName="EmployeeNo"
                                            FilterControlAltText="Filter EmployeeNo column" />
                                        <telerik:GridBoundColumn AllowFiltering="False" DataField="RequestId" HeaderText="RequestId"
                                            SortExpression="RequestId" Visible="False" meta:resourcekey="Request_RequestId"
                                            UniqueName="RequestId" FilterControlAltText="Filter RequestId column" />
                                        <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date" SortExpression="FromDate"
                                            Resizable="False" meta:resourcekey="Request_FromDate" UniqueName="FromDate"
                                            DataFormatString="{0:dd/MM/yyyy}" FilterControlAltText="Filter FromDate column" />
                                        <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" SortExpression="ToDate"
                                            Resizable="False" meta:resourcekey="Request_ToDate" UniqueName="ToDate"
                                            DataFormatString="{0:dd/MM/yyyy}" FilterControlAltText="Filter ToDate column" />
                                        <telerik:GridBoundColumn DataField="ImmediatelyStart" HeaderText="Immediately Start"
                                            SortExpression="ImmediatelyStart" Resizable="False" meta:resourcekey="Request_ImmediatelyStart"
                                            UniqueName="ImmediatelyStart" FilterControlAltText="Filter ImmediatelyStart column" />
                                        <telerik:GridBoundColumn DataField="RequestStartDateTime" HeaderText="Request Start Date Time"
                                            SortExpression="RequestStartDateTime" Resizable="False" meta:resourcekey="Request_RequestStartDateTime"
                                            UniqueName="RequestStartDateTime" FilterControlAltText="Filter RequestStartDateTime column"
                                            DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                                        <telerik:GridBoundColumn DataField="RecalStartDateTime" HeaderText="Recalculate Start Date Time"
                                            SortExpression="RecalStartDateTime" Resizable="False" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                            meta:resourcekey="Request_RecalStartDateTime" UniqueName="RecalStartDateTime"
                                            FilterControlAltText="Filter RecalStartDateTime column" />
                                        <telerik:GridBoundColumn DataField="RecalStatus" Display="False" HeaderText="Recalculate Status"
                                            SortExpression="RecalStatus" Resizable="False" meta:resourcekey="Request_RecalStatus"
                                            UniqueName="RecalStatus" FilterControlAltText="Filter RecalStatus column" />
                                        <telerik:GridTemplateColumn DataField="RecalStatus" HeaderText="Recalculate Status"
                                            UniqueName="ImgRecalStatus" FilterControlAltText="Filter ImgRecalStatus column"
                                            meta:resourcekey="Request_RecalStatus">
                                            <ItemTemplate>
                                                <asp:Image runat="server" ID="imgRecalStatus" ImageUrl="~/assets/img/loading.gif"
                                                    Width="34px" meta:resourcekey="imgRecalStatusResource1" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn HeaderText="Delete" UniqueName="Delete" meta:resourcekey="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ImageUrl="../assets/img/rubbish-bin.png" ID="imgDelete" OnCommand="imgDelete_OnCommand"
                                                    Visible="false" Width="34px" CommandArgument='<%# Eval("RequestId") %>' runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="ReCalEndDateTime" HeaderText="ReCal End Date Time"
                                            SortExpression="ReCalEndDateTime" Resizable="False" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                            meta:resourcekey="Request_ReCalEndDateTime" UniqueName="ReCalEndDateTime" FilterControlAltText="Filter ReCalEndDateTime column" />
                                        <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks"
                                            Resizable="False" meta:resourcekey="Request_Remarks" UniqueName="Remarks"
                                            FilterControlAltText="Filter Remarks column" />
                                    </Columns>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar2_ButtonClick"
                                            Skin="Hay" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                            <Items>
                                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                    ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1"
                                                    Owner="" />
                                            </Items>
                                        </telerik:RadToolBar>
                                    </CommandItemTemplate>
                                </MasterTableView>
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                    EnableRowHoverStyle="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </div>

                </asp:View>
                <asp:View ID="vQueve" runat="server">
                    <div class="row">
                        <div class="col-md-12" align="left">
                            <asp:Button ID="btnBack" Text="Back" CssClass="button" runat="server"
                                meta:resourcekey="btnBackResource1" OnClick="btnBack_Click"></asp:Button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" align="center">
                            <asp:Button ID="btnApprove" Text="Approve Violation(s)" CssClass="button" runat="server"
                                meta:resourcekey="btnApproveResource1"></asp:Button>
                             <asp:Button ID="btnExportToExcel" Text="Export To Excel" OnClick="btnExportToExcel_Click" CssClass="button" runat="server"
                                meta:resourcekey="btnExportToExcelResource1"></asp:Button>
               
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdViewApproveViolation"
                                ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                                <ContextMenu FeatureGroupID="rfContextMenu">
                                </ContextMenu>
                            </telerik:RadFilter>
                            <telerik:RadGrid ID="dgrdViewApproveViolation" runat="server" AllowPaging="True"
                                AllowSorting="True" GridLines="None" ShowStatusBar="True"
                                AutoGenerateColumns="False" PageSize="1000" OnItemCommand="dgrdViewApproveViolation_ItemCommand"
                                ShowFooter="True" meta:resourcekey="dgrdViewApproveViolationResource1">

                                <MasterTableView CommandItemDisplay="Top" AllowMultiColumnSorting="True" DataKeyNames="ID,FK_EmployeeId,EmployeeNo,EmployeeArabicName,EmployeeName,M_DATE_NUM"
                                    EnableHierarchyExpandAll="true">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="false"
                                        ShowRefreshButton="false" />
                                    <Columns>

                                        <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                            UniqueName="TemplateColumn">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="headerChkbox" Text="&nbsp;" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True" runat="server"></asp:CheckBox>
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" HeaderText="FK_EmployeeId"
                                            SortExpression="FK_EmployeeId" Visible="False" meta:resourcekey="GridBoundColumnResource1"
                                            UniqueName="FK_EmployeeId" />

                                        <telerik:GridBoundColumn DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No"
                                            UniqueName="EmployeeNo" meta:resourcekey="Grid_EmployeeNo" />

                                        <telerik:GridBoundColumn DataField="EmployeeName" Visible="true" SortExpression="EmployeeName"
                                            HeaderText="Employee Name" UniqueName="EmployeeName" meta:resourcekey="Grid_EmployeeName" />

                                        <telerik:GridBoundColumn DataField="EmployeeArabicName" SortExpression="EmployeeArabicName" HeaderText="Employee Arabic Name"
                                            UniqueName="EmployeeArabicName" meta:resourcekey="Grid_EmployeeArabicName" />

                                        <telerik:GridBoundColumn DataField="M_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="M_DATE"
                                            HeaderText="Date" UniqueName="M_DATE" meta:resourcekey="Grid_DATE" />

                                        <telerik:GridBoundColumn DataField="ViolationType" SortExpression="ViolationType"
                                            HeaderText="Violation Type" UniqueName="ViolationType" meta:resourcekey="Grid_ViolationType" />

                                        <telerik:GridBoundColumn DataField="transaction_status" SortExpression="transaction_status"
                                            HeaderText="Transaction Status" UniqueName="transaction_status" meta:resourcekey="Grid_transaction_status" />

                                        <telerik:GridBoundColumn DataField="transaction_type" SortExpression="transaction_type"
                                            HeaderText="Transaction Type" UniqueName="transaction_type" meta:resourcekey="Grid_transaction_type" />

                                        <telerik:GridBoundColumn DataField="LastBalance" SortExpression="LastBalance"
                                            HeaderText="Last Leave Balance" UniqueName="LastBalance" meta:resourcekey="Grid_LastLeaveBalance" />

                                        <telerik:GridBoundColumn DataField="RemainingBalance" SortExpression="RemainingBalance" HeaderText="Remaining Leave Balance"
                                            UniqueName="RemainingLeaveBalance" meta:resourcekey="Grid_RemainingBalance" />

                                        <telerik:GridBoundColumn DataField="ProcessDateTime" SortExpression="ProcessDateTime" HeaderText="Process DateTime"
                                            UniqueName="ProcessDateTime" meta:resourcekey="Grid_ProcessDateTime" />
                                    </Columns>
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                            Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                            <Items>
                                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                    ImagePosition="Right" runat="server" Owner="" meta:resourcekey="RadToolBarButtonResource1" />
                                            </Items>
                                        </telerik:RadToolBar>
                                    </CommandItemTemplate>
                                </MasterTableView>
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="false"
                                    EnableRowHoverStyle="True" AllowExpandCollapse="false">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="vReport" runat="server">
                    <table align="center" style="background-color: #fff; width: 1024px; height: 44px;">
                        <tr>
                            <td align="center" style="border-left: 1px; border-right: 1px">
                                <CR2:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
                                    GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                                    meta:resourcekey="CRVResource1" EnableDrillDown="False" GroupTreeImagesFolderUrl=""
                                    HasGotoPageButton="False" HasPageNavigationButtons="False" ToolbarImagesFolderUrl=""
                                    ToolPanelWidth="200px" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
