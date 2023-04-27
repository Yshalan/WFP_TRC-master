<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="CardPrinting.aspx.vb" Inherits="Employee_CardPrinting" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="MultiEmployeeFilter"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2 {
            width: 100%;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <uc1:PageHeader ID="userCtrlCardPrintHeader" runat="server" HeaderText="View Cards" />
    <%--<tr>
            <td>
                <table class="style2">
                    <tr>
                        <td colspan="4">
                            <uc2:PageFilter ID="EmployeeFilterUC" runat="server" OneventCompanySelect="CompanyChanged"
                                OneventEntitySelected="FillEmployee" />
                        </td>
                    </tr>--%>
    <%--<div class="row" id="trcompany" runat="server">
        <div class="col-md-2">
            <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblCompanyResource1"
                Text="Company"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="RadCmbBxCompanies" runat="server" AutoPostBack="True" CausesValidation="False"
                MarkFirstMatch="True" Skin="Vista" Style="width: 350px" ValidationGroup="EmpPermissionGroup"
                meta:resourcekey="RadCmbBxCompaniesResource1">
            </telerik:RadComboBox>

            <asp:RequiredFieldValidator ID="rfvCompanies" runat="server" ControlToValidate="RadCmbBxCompanies"
                Display="None" ErrorMessage="Please Select Company" meta:resourcekey="rfvCompaniesResource1"
                ValidationGroup="VGCards"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vceCompanies" runat="server" CssClass="AISCustomCalloutStyle"
                Enabled="True" TargetControlID="rfvCompanies" WarningIconImageUrl="~/images/warning1.png">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>--%>

    <div class="row">
        <div class="col-md-12">
            <uc4:MultiEmployeeFilter ID="MultiEmployeeFilterUC" runat="server" OneventEntitySelected="EntityChanged"
                OneventWorkGroupSelect="WorkGroupChanged" OneventWorkLocationsSelected="WorkLocationsChanged" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label CssClass="Profiletitletxt" ID="lblStatus" runat="server" Text="Status"
                meta:resourcekey="lblStatusResource1" />
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="ddlStatus" runat="server" AppendDataBoundItems="True"
                MarkFirstMatch="True" AutoPostBack="True" Skin="Vista" meta:resourcekey="ddlStatusResource1" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdCardRequests"
                Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
        </div>
    </div>
    <div class="row">
        <div class="table-responsive">
            <telerik:RadGrid ID="dgrdCardRequests" runat="server" AllowSorting="True" AllowPaging="True"
                Width="100%" PageSize="15" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                ShowFooter="True" meta:resourcekey="dgrdCardRequestsResource1">
                <GroupingSettings CaseSensitive="False" />
                <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="ReasonId,Status,CardRequestId,CardTypeId,FK_EmployeeId">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" UniqueName="EmployeeNo"
                            meta:resourcekey="GridBoundColumnResource1" />
                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                            meta:resourcekey="GridBoundColumnResource2" />
                        <telerik:GridBoundColumn DataField="CardTypeEn" HeaderText="Card Type" UniqueName="CardTypeEn" />
                        <telerik:GridBoundColumn DataField="ReasonId" HeaderText="Reason" UniqueName="ReasonId"
                            meta:resourcekey="GridBoundColumnResource3" />
                        <telerik:GridBoundColumn DataField="OtherReason" HeaderText="OtherReason" UniqueName="OtherReason"
                            meta:resourcekey="GridBoundColumnResource4" />
                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="Status"
                            meta:resourcekey="GridBoundColumnResource5" />
                        <telerik:GridBoundColumn DataField="CardRequestId" AllowFiltering="False" Visible="False"
                            UniqueName="CardRequestId" meta:resourcekey="GridBoundColumnResource6" />
                       
                        <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkAccept" runat="server" Text="View" OnClick="lnkAccept_Click"
                                    CommandName="accept" OnClientClick="return ShowPopUp('1')" meta:resourcekey="lnkAcceptResource1"></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    <CommandItemTemplate>
                        <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_ButtonClick"
                            Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                            <Items>
                                <telerik:RadToolBarButton runat="server" CausesValidation="False" CommandName="FilterRadGrid"
                                    ImagePosition="Right" ImageUrl="~/images/RadFilter.gif" Owner="" Text="Apply filter"
                                    meta:resourcekey="RadToolBarButtonResource1">
                                </telerik:RadToolBarButton>
                            </Items>
                        </telerik:RadToolBar>
                    </CommandItemTemplate>
                </MasterTableView><SelectedItemStyle ForeColor="Maroon" />
            </telerik:RadGrid>
        </div>
    </div>
</asp:Content>
