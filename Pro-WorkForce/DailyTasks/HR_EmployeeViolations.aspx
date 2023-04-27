<%@ Page Title="" Language="VB" StylesheetTheme="Default" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="HR_EmployeeViolations.aspx.vb" Inherits="DailyTasks_HR_EmployeeViolations"
    meta:resourcekey="PageResource1" UICulture="auto" Theme="SvTheme" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="UserSecurityFilter"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc3" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="EmployeeFilter"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">


     

  
     
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel runat="server">
        <ContentTemplate>--%>
    <center>
        <uc3:PageHeader ID="PageHeader1" runat="server" />
    </center>
    <br />
    <asp:MultiView ID="mvEmpViolations" runat="server">
        <asp:View ID="viewEmpViolationsList" runat="server">

            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lbldate" runat="server" Text="From Date" Class="Profiletitletxt" meta:resourcekey="lbldateResource1" /></div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dteFromDate" runat="server" Culture="en-US" Width="180px"
                        meta:resourcekey="dteFromDateResource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblToDate" runat="server" Text="To Date" Class="Profiletitletxt" meta:resourcekey="lblToDateResource1" /></div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dteToDate" runat="server" Culture="en-US" Width="180px"
                        meta:resourcekey="dteToDateResource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dteFromDate" ControlToValidate="dteToDate"
                        ErrorMessage="End Date should be greater than or equal to From Date" Display="None"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave" meta:resourcekey="CVDateResource1" />
                    <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender2"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button runat="server" ID="btnRetrieve" Text="Retrieve" ValidationGroup="grpSave"
                        CssClass="button" meta:resourcekey="btnRetrieveResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <h4>
                        <asp:Label ID="lblViolationCorrection" runat="server" Text="Violation Correction" Class="Profiletitletxt" meta:resourcekey="lblViolationCorrectionResource1" />
                    </h4>
                </div>
            </div>
            <div class="table-responsive">
                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdEmpViolations"
                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                <telerik:RadGrid ID="dgrdEmpViolations" runat="server" AllowSorting="True" AllowPaging="True"
                    Skin="vista" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                    PageSize="25" ShowFooter="True" meta:resourcekey="dgrdEmpViolationsResource1">
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings EnablePostBackOnRowClick="false" EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top" DataKeyNames="EmployeeId,M_DATE,Status,Description,Delay,EarlyOut,OutTime,Duration,PermStart,PermEnd,Description">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnEmployeeArabicName" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="M_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Violation Date"
                                meta:resourcekey="GridBoundColumnResource1" UniqueName="M_DATE">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Type" HeaderText="Violation Type" meta:resourcekey="GridBoundColumnResource2"
                                UniqueName="Type">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" meta:resourcekey="GridBoundColumnResource10"
                                UniqueName="EmployeeNo">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" meta:resourcekey="GridBoundColumnResource11"
                                UniqueName="EmployeeName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Duration" HeaderText="Violation Duration" meta:resourcekey="GridBoundColumnResource3"
                                UniqueName="Duration">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="EmployeeId" meta:resourcekey="GridBoundColumnResource2"
                                UniqueName="EmployeeId" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Status" HeaderText="Status" meta:resourcekey="GridBoundColumnResource4"
                                UniqueName="Status" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Delay" HeaderText="Delay" meta:resourcekey="GridBoundColumnResource5"
                                UniqueName="Delay" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IN_TIME" HeaderText="IN_TIME" UniqueName="IN_TIME"
                                Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EarlyOut" HeaderText="Early Out" meta:resourcekey="GridBoundColumnResource6"
                                UniqueName="EarlyOut" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OutTime" UniqueName="OutTime" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PermStart" UniqueName="PermStart" DataFormatString="{0:HH:mm}" meta:resourcekey="GridBoundColumnResource7">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PermEnd" UniqueName="PermEnd" DataFormatString="{0:HH:mm}" meta:resourcekey="GridBoundColumnResource8">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Description" HeaderText="Description" meta:resourcekey="GridBoundColumnResource6"
                                UniqueName="Description" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnbSubmitPermission" runat="server" CommandName="SubmitPermission"
                                        meta:resourcekey="lnbSubmitPermissionResource1" OnClick="lnbSubmitPermission_Click"
                                        Text="Submit Permission" />
                                    <asp:LinkButton ID="lnbSubmitLeave" runat="server" CommandName="SubmitLeave" meta:resourcekey="lnbSubmitLeaveResource1"
                                        OnClick="lnbSubmitLeave_Click" Text="Submit Leave" />
                                    <asp:HiddenField ID="hdnDuration" runat="server" Value='<%# Eval("Duration") %>' />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        <CommandItemTemplate>
                            <telerik:RadToolBar ID="RadToolBar1" runat="server" meta:resourcekey="RadToolBar1Resource1"
                                OnButtonClick="RadToolBar1_ButtonClick" Skin="Hay">
                                <Items>
                                    <telerik:RadToolBarButton runat="server" CommandName="FilterRadGrid" ImagePosition="Right"
                                        ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource1"
                                        Owner="" Text="Apply filter" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <SelectedItemStyle ForeColor="Maroon" />
                </telerik:RadGrid>
            </div>

        </asp:View>
    </asp:MultiView>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
