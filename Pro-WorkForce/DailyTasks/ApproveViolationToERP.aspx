<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ApproveViolationToERP.aspx.vb"
    Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" Inherits="DailyTasks_ApproveViolationToERP"
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
                    <asp:Button ID="btnRetrive" Text="Retrieve Violation(s)" CssClass="button" runat="server"
                        meta:resourcekey="btnRetriveResource1"></asp:Button>
                    <asp:Button ID="btnApprove" Text="Approve Violation(s)" CssClass="button" runat="server"
                        meta:resourcekey="btnApproveResource1"></asp:Button>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" align="center">
                    <asp:ImageButton ID="btnExportToPDF" ImageUrl="~/Icons/pdf.png" runat="server" Width="40px"
                        meta:resourcekey="btnExportToPDFResource1" />
                    <asp:ImageButton ID="btnExportToExcel" ImageUrl="~/Icons/Microsoft-Excel-icon.png"
                        runat="server" Width="40px" meta:resourcekey="btnExportToExcelResource1" />
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
                        AllowMultiRowSelection="True" AutoGenerateColumns="False" PageSize="500" OnItemCommand="dgrdViewApproveViolation_ItemCommand"
                        ShowFooter="True" meta:resourcekey="dgrdViewApproveViolationResource1" >
                   
                        <MasterTableView CommandItemDisplay="Top" AllowMultiColumnSorting="True" DataKeyNames="FK_EmployeeId,EmployeeNo,EmployeeArabicName,EmployeeName"
                             EnableHierarchyExpandAll="true">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" ShowAddNewRecordButton="false"
                                ShowRefreshButton="false" />
                            <DetailTables>
                                <telerik:GridTableView  Name="EmployeeGridView" AutoGenerateColumns="false" DataKeyNames="FK_EmployeeId"
                                    Width="100%" runat="server">
                                    <ParentTableRelation>
                                        <telerik:GridRelationFields DetailKeyField="FK_EmployeeId" MasterKeyField="FK_EmployeeId"></telerik:GridRelationFields>
                                    </ParentTableRelation>
                                    
                                    <Columns>
                                        
                                        <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                            UniqueName="TemplateColumn">
                                             <HeaderTemplate>
                                            <asp:CheckBox ID="headerChkbox" Text="&nbsp;" OnCheckedChanged="ToggleSelectedState" AutoPostBack="True" runat="server"></asp:CheckBox> 
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;"/>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" HeaderText="FK_EmployeeId"
                                            SortExpression="FK_EmployeeId" Visible="False" meta:resourcekey="GridBoundColumnResource1"
                                            UniqueName="FK_EmployeeId" />
                                        <telerik:GridBoundColumn DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No"
                                            UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource12" />
                                        <telerik:GridBoundColumn DataField="EmployeeName" Visible="false" SortExpression="EmployeeName"
                                            HeaderText="Employee Name" UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource2" />
                                        <telerik:GridBoundColumn DataField="M_DATE" DataFormatString="{0:dd/MM/yyyy}" SortExpression="M_DATE"
                                            HeaderText="Date" UniqueName="M_DATE" meta:resourcekey="GridBoundColumnResource3" />
                                        <telerik:GridBoundColumn DataField="STATUS" Visible="false" SortExpression="STATUS"
                                            HeaderText="STATUS" UniqueName="STATUS" meta:resourcekey="GridBoundColumnResource4" />
                                        <telerik:GridBoundColumn DataField="REMARKS" SortExpression="REMARKS" HeaderText="REMARKS"
                                            UniqueName="REMARKS" meta:resourcekey="GridBoundColumnResource16" />
                                        <telerik:GridBoundColumn DataField="IsOldData" SortExpression="IsOldData" HeaderText="Is Old Data"
                                            UniqueName="IsOldData" />
                                        <telerik:GridBoundColumn DataField="PermissionFromToTime" SortExpression="PermissionFromToTime"
                                            HeaderText="Permission From & To Time" UniqueName="PermissionFromToTime" />
                                        <telerik:GridBoundColumn DataField="LeaveRequestRemarks" SortExpression="LeaveRequestRemarks"
                                            HeaderText="Leave Request Remarks" UniqueName="LeaveRequestRemarks" />
                                        <telerik:GridBoundColumn DataField="M_DATE_NUM" Visible="false" SortExpression="M_DATE_NUM"
                                            HeaderText="M_DATE_NUM" UniqueName="M_DATE_NUM" meta:resourcekey="GridBoundColumnResource4" />
                                    </Columns>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                            Skin="Hay" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                            <Items>
                                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                    ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1"
                                                    Owner="" />
                                            </Items>
                                        </telerik:RadToolBar>
                                    </CommandItemTemplate>
                                    <CommandItemSettings ShowExportToPdfButton="true" ExportToPdfText="Export to Pdf"
                                        ExportToExcelText="Export to Excel" ShowExportToExcelButton="true" />
                                </telerik:GridTableView>
                            </DetailTables>
                            <Columns>
                                
                                <telerik:GridBoundColumn SortExpression="EmployeeNo" HeaderText="Employee No" HeaderButtonType="TextButton"
                                    DataField="EmployeeNo" UniqueName="EmployeeNo"
                                    meta:resourcekey="GridBoundColumnResource6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="EmployeeName" DataField="EmployeeName" UniqueName="EmployeeName"
                                    HeaderText="Employee Name"
                                    meta:resourcekey="GridBoundColumnResource7">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeArabicName" SortExpression="EmployeeArabicName"
                                    HeaderText="Employee Arabic Name" UniqueName="EmployeeArabicName"
                                    meta:resourcekey="GridBoundColumnResource8" />
                            </Columns>
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
</asp:Content>
