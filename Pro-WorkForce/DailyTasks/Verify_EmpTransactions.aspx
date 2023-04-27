<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Verify_EmpTransactions.aspx.vb" Inherits="DailyTasks_Verify_EmpTransactions"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="UCSecurityFilter"
    TagPrefix="uc1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="mvDeductions" runat="server" ActiveViewIndex="0">
        <asp:View ID="vDeductions" runat="server">
            <asp:UpdatePanel ID="update1" runat="server">
                <ContentTemplate>
                    <div class="updateprogressAssign">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" EnableViewState="false" DisplayAfter="0">
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
                    <uc1:PageHeader ID="PageHeader1" runat="server" />
                    <div class="row">
                        <div class="col-md-12">
                            <uc1:UCSecurityFilter ID="UCSecurityFilter1" runat="server" IsCompanyRequired="false"
                                IsEntityRequired="false" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="Year" meta:resourcekey="Label1Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlYear" DataTextField="txt" DataValueField="val"
                                meta:resourcekey="ddlYearResource1">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblMonth" runat="server" CssClass="Profiletitletxt" Text="Month" meta:resourcekey="lblMonthResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlMonth" DataTextField="txt" DataValueField="val"
                                meta:resourcekey="ddlMonthResource1">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnGet" Text="Get" CssClass="button" runat="server" meta:resourcekey="btnGetResource1"></asp:Button>
                            <asp:Button ID="btnApprove" Text="Approve Transaction(s)" CssClass="button" runat="server"
                                meta:resourcekey="btnApproveResource1"></asp:Button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdEmp_Trans"
                                Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <telerik:RadGrid runat="server" ID="dgrdEmp_Trans" AllowSorting="True" AllowPaging="True"
                                GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                PageSize="15" ShowFooter="True" meta:resourcekey="dgrdEmp_TransResource1">
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="EmployeeArabicName,FK_EmployeeId,Year,Month">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No"
                                            UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource1" />
                                        <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                            UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource2" />
                                        <telerik:GridBoundColumn DataField="Delay" SortExpression="Delay" HeaderText="Delay"
                                            UniqueName="Delay" DataFormatString="{0:HH:mm}" meta:resourcekey="GridBoundColumnResource3" />
                                        <telerik:GridBoundColumn DataField="Delay_Count" SortExpression="Delay_Count" HeaderText="Delay No."
                                            UniqueName="Delay_Count" meta:resourcekey="GridBoundColumnResource4" />
                                        <telerik:GridBoundColumn DataField="EarlyOut" SortExpression="EarlyOut" HeaderText="Early Out"
                                            UniqueName="EarlyOut" DataFormatString="{0:HH:mm}" meta:resourcekey="GridBoundColumnResource5" />
                                        <telerik:GridBoundColumn DataField="EarlyOut_Count" SortExpression="EarlyOut_Count"
                                            HeaderText="EarlyOut No." UniqueName="EarlyOut_Count" meta:resourcekey="GridBoundColumnResource6" />

                                        <telerik:GridBoundColumn DataField="TotalDelayEarlOut" SortExpression="TotalDelayEarlOut"
                                            HeaderText="Total Delay & EarlyOut" UniqueName="TotalDelayEarlOut" meta:resourcekey="GridBoundColumnResource16" />
                                        <telerik:GridBoundColumn DataField="TotalDelayEarlOut_Count" SortExpression="TotalDelayEarlOut_Count"
                                            HeaderText="Delay & EarlyOut No." UniqueName="TotalDelayEarlOut_Count" meta:resourcekey="GridBoundColumnResource17" />


                                        <telerik:GridBoundColumn DataField="MissingIN_Count" SortExpression="MissingIN_Count"
                                            HeaderText="Missing In No." UniqueName="MissingIN_Count" meta:resourcekey="GridBoundColumnResource7" />
                                        <telerik:GridBoundColumn DataField="MissingOut_Count" SortExpression="MissingOut_Count"
                                            HeaderText="Missing Out No." UniqueName="MissingOut_Count" meta:resourcekey="GridBoundColumnResource8" />
                                        <telerik:GridBoundColumn DataField="Uncomplete50WHRS" SortExpression="Uncomplete50WHRS"
                                            HeaderText="Uncomplete 50% Of Work Day" UniqueName="Uncomplete50WHRS" meta:resourcekey="GridBoundColumnResource9" />
                                        <telerik:GridBoundColumn DataField="Absent_Count" SortExpression="Absent_Count" HeaderText="Absent No."
                                            UniqueName="Absent_Count" meta:resourcekey="GridBoundColumnResource10" />
                                        <telerik:GridBoundColumn DataField="TotalDeductions" SortExpression="TotalDeductions"
                                            HeaderText="Total Deduction(s)" UniqueName="TotalDeductions" meta:resourcekey="GridBoundColumnResource15" />
                                        <telerik:GridBoundColumn DataField="EmployeeArabicName" Visible="False" AllowFiltering="False"
                                            UniqueName="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource11" />
                                        <telerik:GridBoundColumn DataField="FK_EmployeeId" Visible="False" AllowFiltering="False"
                                            UniqueName="FK_EmployeeId" meta:resourcekey="GridBoundColumnResource12" />
                                        <telerik:GridBoundColumn DataField="Year" Visible="False" AllowFiltering="False"
                                            UniqueName="Year" meta:resourcekey="GridBoundColumnResource13" />
                                        <telerik:GridBoundColumn DataField="Month" Visible="False" AllowFiltering="False"
                                            UniqueName="Month" meta:resourcekey="GridBoundColumnResource14" />
                                        <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTA2Report" runat="server" Text="TA Report"
                                                    meta:resourcekey="lnkTA2ReportResource1" OnClick="lnkTA2Report_Click" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
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
                                </MasterTableView><SelectedItemStyle ForeColor="Maroon" />
                            </telerik:RadGrid>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
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
