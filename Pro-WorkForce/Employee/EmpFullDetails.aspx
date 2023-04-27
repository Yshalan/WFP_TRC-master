<%@ Page Title="" Language="VB"  Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="EmpFullDetails.aspx.vb" Inherits="Employee_EmpFullDetails" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="Emp_Filter"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <uc1:PageHeader ID="PageHeader1" runat="server" />

    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
           
                <div class="row">
                    <div class="col-md-12">
                        <uc1:Emp_Filter ID="objEmp_Filter" runat="server" ShowOnlyManagers="false" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="button" meta:resourcekey="btnShowResource1" />
                    </div>
                </div>
                <div class="row">
                    <div class="table-responsive">
                        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                        <div class="filterDiv">
                            <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdEmpDetails"
                                ShowApplyButton="false" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
                        </div>
                        <telerik:RadGrid ID="dgrdEmpDetails" runat="server" AllowSorting="True" AllowPaging="True"
                             GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                            ShowFooter="True" PageSize="15" meta:resourcekey="dgrdEmpDetailsResource1">
                            <ExportSettings>
                                <Pdf PageTitle="Emp_FullDetails" PaperSize="A4" />
                            </ExportSettings>
                            <SelectedItemStyle ForeColor="Maroon" />
                            <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="false">
                                <CommandItemTemplate>
                                    <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                        meta:resourcekey="RadToolBar1Resource1">
                                        <Items>
                                            <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                                ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource1"
                                                runat="server" Owner="" />
                                        </Items>
                                    </telerik:RadToolBar>
                                </CommandItemTemplate>
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No"
                                        UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource1" />
                                    <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                        UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource2" />
                                    <telerik:GridBoundColumn DataField="EmployeeArabicName" SortExpression="EmployeeArabicName"
                                        HeaderText="Employee Arabic Name" UniqueName="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource3" />
                                    <telerik:GridBoundColumn DataField="DesignationName" SortExpression="DesignationName"
                                        HeaderText="Designation Name" UniqueName="DesignationName" meta:resourcekey="GridBoundColumnResource4" />
                                    <telerik:GridBoundColumn DataField="DesignationArabicName" SortExpression="DesignationArabicName"
                                        HeaderText="Designation Arabic Name" UniqueName="DesignationArabicName" meta:resourcekey="GridBoundColumnResource5" />
                                    <telerik:GridBoundColumn DataField="GradeName" SortExpression="GradeName" HeaderText="Grade Name"
                                        UniqueName="GradeName" meta:resourcekey="GridBoundColumnResource6" />
                                    <telerik:GridBoundColumn DataField="GradeArabicName" SortExpression="GradeArabicName"
                                        HeaderText="Grade Arabic Name" UniqueName="GradeArabicName" meta:resourcekey="GridBoundColumnResource7" />
                                    <telerik:GridBoundColumn DataField="WorkLocationName" SortExpression="WorkLocationName"
                                        HeaderText="WorkLocation Name" UniqueName="WorkLocationName" meta:resourcekey="GridBoundColumnResource8" />
                                    <telerik:GridBoundColumn DataField="WorkLocationArabicName" SortExpression="WorkLocationArabicName"
                                        HeaderText="WorkLocation Arabic Name" UniqueName="WorkLocationArabicName" meta:resourcekey="GridBoundColumnResource9" />
                                    <telerik:GridBoundColumn DataField="GroupName" SortExpression="GroupName" HeaderText="Group Name"
                                        UniqueName="GroupName" meta:resourcekey="GridBoundColumnResource10" />
                                    <telerik:GridBoundColumn DataField="GroupArabicName" SortExpression="GroupArabicName"
                                        HeaderText="Group Arabic Name" UniqueName="GroupArabicName" meta:resourcekey="GridBoundColumnResource11" />
                                    <telerik:GridBoundColumn DataField="ScheduleName" SortExpression="ScheduleName" HeaderText="Schedule Name"
                                        UniqueName="ScheduleName" meta:resourcekey="GridBoundColumnResource12" />
                                    <telerik:GridBoundColumn DataField="ScheduleArabicName" SortExpression="ScheduleArabicName"
                                        HeaderText="Schedule Arabic Name" UniqueName="ScheduleArabicName" meta:resourcekey="GridBoundColumnResource13" />


                                        <telerik:GridBoundColumn DataField="FromDate" SortExpression="FromDate" Visible="false"
                                        HeaderText="From Date" UniqueName="FromDate" meta:resourcekey="GridBoundColumnResource21"/>
                                        <telerik:GridBoundColumn DataField="ToDate" SortExpression="ToDate"  Visible="false"
                                        HeaderText="To Date" UniqueName="ToDate"   meta:resourcekey="GridBoundColumnResource22"/>

                                    <telerik:GridBoundColumn DataField="ManagerName" SortExpression="ManagerName" HeaderText="Manager Name"
                                        UniqueName="ManagerName" meta:resourcekey="GridBoundColumnResource14" />
                                    <telerik:GridBoundColumn DataField="ManagerArabicName" SortExpression="ManagerArabicName"
                                        HeaderText="Manager Arabic Name" UniqueName="ManagerArabicName" meta:resourcekey="GridBoundColumnResource15" />
                                    <telerik:GridBoundColumn DataField="TAPolicyName" SortExpression="TAPolicyName" HeaderText="TAPolicy Name"
                                        UniqueName="TAPolicyName" meta:resourcekey="GridBoundColumnResource16" />
                                    <telerik:GridBoundColumn DataField="TAPolicyArabicName" SortExpression="TAPolicyArabicName"
                                        HeaderText="TAPolicy Arabic Name" UniqueName="TAPolicyArabicName" meta:resourcekey="GridBoundColumnResource17" />
                                    <telerik:GridBoundColumn DataField="RuleName" SortExpression="RuleName" HeaderText="Rule Name"
                                        UniqueName="RuleName" meta:resourcekey="GridBoundColumnResource18" />
                                    <telerik:GridBoundColumn DataField="RuleArabicName" SortExpression="RuleArabicName"
                                        HeaderText="Rule Arabic Name" UniqueName="RuleArabicName" meta:resourcekey="GridBoundColumnResource19" />
                                </Columns>
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            </MasterTableView>
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
