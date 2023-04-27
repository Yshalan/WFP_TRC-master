<%@ Page Language="VB" AutoEventWireup="false" StylesheetTheme="Default"  MasterPageFile="~/Default/AdminMaster.master" CodeFile="LeaveBalance.aspx.vb" 
Inherits="Admin_LeaveBalance" meta:resourcekey="PageResource2" uiculture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter" TagPrefix="uc1" %>

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
    <br /><br />
    <center>
    <table id="tblEmpFilter" runat="server" width="100%">
    <tr>
    <td align="center"><uc1:PageFilter ID="PageFilter1" runat="server" HeaderText="Employee Filter"
                 OneventEmployeeSelect="FillGrid" /> </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    </table></center><br />
                 
<cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
        OnClientActiveTabChanged="hideValidatorCalloutTab" 
        meta:resourcekey="TabContainer1Resource2">
                <cc1:TabPanel ID="tabBalance" runat="server" HeaderText="Leave Blanace" 
                    TabIndex="0" meta:resourcekey="tabBalanceResource2">
                    <HeaderTemplate>
                        Leave Blanace
                    </HeaderTemplate>
                <ContentTemplate>
                
<table id="tblBalance" runat="server" style="width:100%">
<tr runat="server">
            <td runat="server">
                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" />
                <div class="filterDiv">
                    <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdBalance"
                        ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " />
                </div>  
                <telerik:RadGrid runat="server" ID="dgrdBalance" AutoGenerateColumns="False" PageSize="25"
                    Skin="Hay" AllowPaging="True" AllowSorting="True" 
                    AllowFilteringByColumn="True">
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                        EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="BalanceDate" HeaderText="Balance Date" 
                                DataType="System.DateTime" SortExpression="BalanceDate"  DataFormatString="{0:dd/MM/yyyy}"
                                UniqueName="BalanceDate" >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Balance" HeaderText="Balance" 
                                DataType="System.Double" SortExpression="Balance" Resizable="False" 
                                UniqueName="Balance">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TotalBalance" DataType="System.Double" HeaderText="Total Balance"
                                SortExpression="TotalBalance" UniqueName="TotalBalance" />
                            <telerik:GridBoundColumn DataField="CREATED_DATE" HeaderText="Created Date"  DataFormatString="{0:dd/MM/yyyy}"
                                DataType="System.DateTime" SortExpression="CREATED_DATE" Resizable="False" 
                                UniqueName="CREATED_DATE">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks" 
                                SortExpression="Remarks" Resizable="False" 
                                UniqueName="Remarks">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CREATED_BY" HeaderText="Created By" 
                                SortExpression="CREATED_BY" Resizable="False" 
                                UniqueName="CREATED_BY">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick">
                                <Items>
                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        </tr>
</table>
</ContentTemplate>
</cc1:TabPanel>
<cc1:TabPanel ID="tabExpired" runat="server" HeaderText="Expired Balance" TabIndex="0" 
                    meta:resourcekey="tabExpiredResource2">
<ContentTemplate>
<table id="Table1" runat="server" style="width:100%">
<tr runat="server">
            <td runat="server">
                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel2" />
                <div class="filterDiv">
                    <telerik:RadFilter runat="server" ID="RadFilter2" Skin="Hay" FilterContainerID="dgrdBalanceExpired"
                        ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " />
                </div>
                <telerik:RadGrid runat="server" ID="dgrdBalanceExpired" 
                    AutoGenerateColumns="False" PageSize="25"
                    Skin="Hay" AllowPaging="True" AllowSorting="True" 
                    AllowFilteringByColumn="True">
                    <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="ExpireDate" HeaderText="Expire Date" DataType="System.DateTime"
                                AllowFiltering="true" SortExpression="ExpireDate" DataFormatString="{0:dd/MM/yyyy}" >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ExpireBalance" HeaderText="Expire Balance" DataType="System.Double"
                                AllowFiltering="true" ShowFilterIcon="true" SortExpression="ExpireBalance" Resizable="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CREATED_DATE" HeaderText="Created Date" DataType="System.DateTime"
                                AllowFiltering="true" ShowFilterIcon="true" SortExpression="CREATED_DATE" Resizable="false" DataFormatString="{0:dd/MM/yyyy}">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CREATED_BY" HeaderText="Created By"
                                DataType="System.String" SortExpression="CREATED_BY" Resizable="False" 
                                UniqueName="CREATED_BY">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick">
                                <Items>
                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                        EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
</table>
</ContentTemplate>
</cc1:TabPanel>
</cc1:TabContainer>


</asp:Content>
