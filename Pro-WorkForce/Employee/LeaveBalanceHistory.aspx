<%@ Page Language="VB" AutoEventWireup="false"  Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master"
    CodeFile="LeaveBalanceHistory.aspx.vb" Inherits="Admin_LeaveBalance" meta:resourcekey="PageResource2"
    UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function hideValidatorCalloutTab() {
            try {
                if (AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout != null) {
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.hide();


                }
            }
            catch (err) {
            }
            return false;
        } 
    </script>

        <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Employees Leave Balance" />

        <div class="row">
            <div class="col-md-12">
                <uc1:PageFilter ID="EmployeeFilterUC" runat="server" HeaderText="Employee Filter"
                    ValidationGroup="ValidateGet" />
            </div>
        </div>
    <div class="row">
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Get" ValidationGroup="ValidateGet"
                meta:resourcekey="btnSearchResource1" />
        </div>
    </div>
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab"
        meta:resourcekey="TabContainer1Resource2">
        <cc1:TabPanel ID="tabBalance" runat="server" HeaderText="Leave Blanace" TabIndex="0"
            meta:resourcekey="tabBalanceResource2">
            <ContentTemplate>
               
                    <div class="row">
                        <div class="table-responsive">
                            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" />
                            <div class="filterDiv">
                                <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdBalance"
                                    ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
                            </div>
                            <telerik:RadGrid runat="server" ID="dgrdBalance" AutoGenerateColumns="False" PageSize="15"
                                 AllowPaging="True" AllowSorting="True" meta:resourcekey="dgrdBalanceResource1"
                                AllowFilteringByColumn="True">
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                    EnableRowHoverStyle="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False" DataKeyNames="BalanceDate,CREATED_DATE,EmployeeArabicName,Balance,TotalBalance">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No." DataType="System.Int32"
                                            SortExpression="EmployeeNo" UniqueName="EmployeeNo" meta:resourcekey="GridBound1ColumnResource99">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"
                                            meta:resourcekey="GridBound0ColumnResource100" UniqueName="EmployeeName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeArabicName" Visible="false" />
                                        <telerik:GridBoundColumn DataField="BalanceDate" HeaderText="Balance Date" DataType="System.DateTime"
                                            SortExpression="BalanceDate" DataFormatString="{0:dd/MM/yyyy}" UniqueName="BalanceDate"
                                            meta:resourcekey="GridBound1ColumnResource1">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LeaveName" HeaderText="Leave Type" SortExpression="LeaveName"
                                            Resizable="False" UniqueName="LeaveName" meta:resourcekey="GridBound11ColumnResource1"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LeaveArabicName" HeaderText="Leave Type" SortExpression="LeaveArabicName"
                                            Resizable="False" UniqueName="LeaveArabicName" meta:resourcekey="GridBound11ColumnResource1"
                                            Visible="false">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Balance" HeaderText="Balance" DataType="System.Decimal"
                                            DataFormatString="{0:C2}" SortExpression="Balance" Resizable="False" UniqueName="Balance"
                                            meta:resourcekey="GridBound2ColumnResource1">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TotalBalance" DataType="System.Decimal" DataFormatString="{0:C2}"
                                            HeaderText="Total Balance" SortExpression="TotalBalance" UniqueName="TotalBalance"
                                            meta:resourcekey="GridBound3ColumnResource1" />
                                        <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks"
                                            Resizable="False" UniqueName="Remarks" meta:resourcekey="GridBound5ColumnResource1">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CREATED_DATE" HeaderText="Created Date" DataFormatString="{0:dd/MM/yyyy}"
                                            DataType="System.DateTime" SortExpression="CREATED_DATE" Resizable="False" UniqueName="CREATED_DATE"
                                            meta:resourcekey="GridBound4ColumnResource1">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CREATED_BY" HeaderText="Created By" SortExpression="CREATED_BY"
                                            Resizable="False" UniqueName="CREATED_BY" meta:resourcekey="GridBound6ColumnResource1">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick">
                                            <Items>
                                                <telerik:RadToolBarButton Text="Apply filter" ImageUrl="~/images/RadFilter.gif" CommandName="FilterRadGrid"
                                                    ImagePosition="Right" meta:resourcekey="RadToolBarButtonResource1" />
                                            </Items>
                                        </telerik:RadToolBar>
                                    </CommandItemTemplate>
                                </MasterTableView></telerik:RadGrid>
                        </div>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="tabExpired" runat="server" HeaderText="Expired Balance" TabIndex="0"
            meta:resourcekey="tabExpiredResource2">
            <ContentTemplate>
                
                    <div class="row">
                        <div class="table-responsive">
                            <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel2" />
                            <div class="filterDiv">
                                <telerik:RadFilter runat="server" ID="RadFilter2" Skin="Hay" FilterContainerID="dgrdBalanceExpired"
                                    ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
                            </div>
                            <telerik:RadGrid runat="server" ID="dgrdBalanceExpired" AutoGenerateColumns="False"
                                PageSize="15" meta:resourcekey="dgrdBalanceResource1"  AllowPaging="True"
                                AllowSorting="True" AllowFilteringByColumn="True">
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                    EnableRowHoverStyle="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False" DataKeyNames="ExpireDate,CREATED_DATE,ExpireBalance">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No." DataType="System.Int32"
                                            SortExpression="EmployeeNo" UniqueName="EmployeeNo" meta:resourcekey="GridBound10ColumnResource100">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"
                                            meta:resourcekey="GridBound11ColumnResource99" UniqueName="EmployeeName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ExpireDate" HeaderText="Expire Date" DataType="System.DateTime"
                                            SortExpression="ExpireDate" DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBound7ColumnResource1"
                                            UniqueName="ExpireDate">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ExpireBalance" HeaderText="Expire Balance" DataType="System.Double"
                                            SortExpression="ExpireBalance" Resizable="False" meta:resourcekey="GridBound8ColumnResource1"
                                            UniqueName="ExpireBalance">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CREATED_DATE" HeaderText="Created Date" DataType="System.DateTime"
                                            SortExpression="CREATED_DATE" Resizable="False" DataFormatString="{0:dd/MM/yyyy}"
                                            meta:resourcekey="GridBound9ColumnResource1" UniqueName="CREATED_DATE">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CREATED_BY" HeaderText="Created By" SortExpression="CREATED_BY"
                                            Resizable="False" UniqueName="CREATED_BY" meta:resourcekey="GridBound10ColumnResource1">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick">
                                            <Items>
                                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                    ImagePosition="Right" meta:resourcekey="RadToolBarButtonResource1" />
                                            </Items>
                                        </telerik:RadToolBar>
                                    </CommandItemTemplate>
                                </MasterTableView></telerik:RadGrid>
                        </div>
                    </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
