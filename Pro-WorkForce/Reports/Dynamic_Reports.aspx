<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Dynamic_Reports.aspx.vb" StylesheetTheme="Default" MasterPageFile="~/Default/ReportMaster.master"
    Inherits="Reports_SelfServices_Reports" meta:resourcekey="PageResource1" UICulture="auto" %>


<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagPrefix="uc1" TagName="EmployeeFilter" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/TA_innerpage.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript">
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="Filter" runat="server">
                 <%--   <center>
                        <table style="width: 594px; border-width: 0px; background-color: #FFFFFF; visibility: hidden;"
                            cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="3">
                                    <div id="logo_div">
                                        <a href="../Default/logout.aspx">
                                            <div id="logout2">
                                            </div>
                                        </a>
                                        <div style="text-align: left">
                                            <a href="../Default/Home.aspx">
                                                <img src="../images/logo.jpg" alt="smart time" /></a>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </center>--%>                
                                <uc1:PageHeader ID="lblReportTitle" runat="server" HeaderText="Dynamic Reports"
                                    meta:resourcekey="lblReportTitleResource1" />
                               <div class="row">
                                           <div class="col-md-2">
                                                <asp:Label ID="lblExtraReports" runat="server" CssClass="Profiletitletxt" Text="Report Name" meta:resourcekey="lblSelfServiceReportsResource1" />
                                            </div>
                                            <div class="col-md-4">
                                                <telerik:RadComboBox ID="RadComboExtraReports" CausesValidation="False" Filter="Contains"
                                                    AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server"
                                                    meta:resourcekey="RadComboExtraReportsResource1">
                                                </telerik:RadComboBox>
                                                <asp:RequiredFieldValidator ID="rfvradExtraReports" runat="server" ControlToValidate="RadComboExtraReports"
                                                    Display="None" ErrorMessage="Please Select Report Type" InitialValue="--Please Select--"
                                                    ValidationGroup="btnPrint" meta:resourcekey="rfvradExtraReportsResource1"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="vceradExtraReports" runat="server" CssClass="AISCustomCalloutStyle"
                                                    Enabled="True" TargetControlID="rfvradExtraReports" WarningIconImageUrl="~/images/warning1.png">
                                                </cc1:ValidatorCalloutExtender>
                                            </div>
                                        </div>
                </asp:View>
                <asp:View ID="vwDynamicReport" runat="server">
                       <asp:UpdatePanel ID="pnlDynamicRpt" runat="server">
        <ContentTemplate>
            <table width="700px" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <div id="divDynamicRpt" style="display: block">
                            <table width="600px">
                                <tr>
                                    <td colspan="2">
                                        <center>
                                            <uc1:PageHeader ID="PageHeader2" runat="server" HeaderText="Create Dynamic Report" />
                                        </center>
                                        <br />
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblReportName" runat="server" CssClass="Profiletitletxt" Text="Report Name"
                                            meta:resourcekey="lblReportNameResource1"></asp:Label>
                                        <td>
                                            <asp:TextBox ID="txtReportName" runat="server" meta:resourcekey="txtReportNameResource1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqReportName" runat="server" ControlToValidate="txtReportName"
                                                Display="None" ErrorMessage="Please enter Report Name" ValidationGroup="ReportGroup"
                                                meta:resourcekey="ReportNameResource1"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ExtenderreqReportName" runat="server" CssClass="AISCustomCalloutStyle"
                                                TargetControlID="reqReportName" WarningIconImageUrl="~/images/warning1.png"
                                                Enabled="True">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblReportView" runat="server" CssClass="Profiletitletxt" Text="Report View"
                                            meta:resourcekey="lblReportViewResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtReportView" runat="server" Width="200px" meta:resourcekey="txtReportViewResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqReportView" runat="server" ErrorMessage="Please enter Report View"
                                            Display="None" ValidationGroup="ReportGroup" ControlToValidate="txtReportView"
                                            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            meta:resourcekey="reqReportViewResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderReportView" TargetControlID="reqReportView"
                                            runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSQLQuery" runat="server" CssClass="Profiletitletxt" Text="SQL Query"
                                            meta:resourcekey="lblSQLQueryResource1"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSQLQuery" runat="server" TextMode="MultiLine" Height="200px" Width="400px" meta:resourcekey="txtSQLQueryResource1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqSQLQuery" runat="server" ErrorMessage="Please enter SQL Query"
                                            Display="None" ValidationGroup="ReportGroup" ControlToValidate="txtSQLQuery"
                                            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            meta:resourcekey="reqSQLQueryResource1"></asp:RequiredFieldValidator>
                                        <cc1:ValidatorCalloutExtender ID="ExtenderSQLQuery" TargetControlID="reqSQLQuery"
                                            runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="ReportGroup"
                                            CssClass="button" meta:resourcekey="btnSaveResource1" />
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CausesValidation="False"
                                            CssClass="button" meta:resourcekey="btnDeleteResource1" />
                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" CssClass="button"
                                            meta:resourcekey="btnClearResource1" />
                                    </td>
                                </tr>
                                <div>
                                </div>
                                <tr>
                         <td colspan="2">
                                        <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                                        <div class="filterDiv">
                                            <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdVwDynamicRpt"
                                                ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
                                        </div>
                                        <telerik:RadGrid ID="dgrdVwDynamicRpt" runat="server" AllowSorting="True" AllowPaging="True"
                                            PageSize="25" Skin="Hay" GridLines="None" ShowStatusBar="True" ShowFooter="True"
                                            OnItemCommand="dgrdVwDynamicRpt_ItemCommand" meta:resourcekey="dgrdVwDynamicRptResource1">

                                            <SelectedItemStyle ForeColor="Maroon" />
                                            <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False">
                                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                                        UniqueName="TemplateColumn">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource1" />
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                     <telerik:GridBoundColumn DataField="ReportId" SortExpression="ReportId" Visible="False"
                                                        AllowFiltering="False" meta:resourcekey="GridBoundColumnResource1" UniqueName="ReportId" />
                                                    <telerik:GridBoundColumn DataField="ReportName" SortExpression="ReportName" HeaderText="Report Name"
                                                        meta:resourcekey="GridBoundColumnResource2" UniqueName="ReportName" />
                                                    <telerik:GridBoundColumn DataField="ViewName" SortExpression="ViewName" HeaderText="View Name"
                                                        meta:resourcekey="GridBoundColumnResource3" UniqueName="ViewName" />
                                                   
                                                   
                                                </Columns>
                                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                <CommandItemTemplate>
                                                    <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                                        meta:resourcekey="RadToolBar1Resource1">
                                                        <Items>
                                                            <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="<%# GetFilterIcon() %>"
                                                                ImagePosition="Right" runat="server" 
                                                                meta:resourcekey="RadToolBarButtonResource1" Owner="" />
                                                        </Items>
                                                    </telerik:RadToolBar>
                                                </CommandItemTemplate>
                                            </MasterTableView>
                                            <GroupingSettings CaseSensitive="False" />
                                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                                <Selecting AllowRowSelect="True" />
                                            </ClientSettings>
                                        </telerik:RadGrid>
         
                                    </td>
                                    </tr>
                    </td>
                </tr>
            </table>
            </div> </td> </tr> </table>
        </ContentTemplate>
    </asp:UpdatePanel>
           
                </asp:View>
               <asp:View ID="View1" runat="server">
                   
            <table width="50%">
                <tr>
                    <td align="center">
                        <uc1:PageHeader ID="PageHeader1" runat="server" />
                        <br />
                        <div>
                            <table align="center" style="width: 450px">
                                <tr>
                                    <td>
                                        <table style="width: 600px; vertical-align: top">
                                            <tr>
                                                <td>
                                                    <table border="0" align="center" cellpadding="0" cellspacing="0" style="background-color: white;
                                                        margin-left: 40px; width: 647px;">
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblSelectReport" runat="server" Text="Select Report" CssClass="Profiletitletxt"
                                                                    meta:resourcekey="lblSelectReportResource1" />
                                                            </td>
                                                            <td align="left">
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td>
                                                                                                                                                   <telerik:RadComboBox ID="CmbReports" runat="server" AppendDataBoundItems="True" autopostback="true"
                                                                                DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" CaseSensitive="False" OnSelectedIndexChanged="CmbReports_SelectedIndexChanged"
                                                                                MarkFirstMatch="True" Skin="Vista" CausesValidation="false" ValidationGroup="group2" Width="200px" meta:resourcekey="CmbReportsResource1">
                                                                            </telerik:RadComboBox>
                                                                            <asp:RequiredFieldValidator ID="reqDosageForm" runat="server" Display="None" ErrorMessage="Please select a report Name"
                                                                                ControlToValidate="CmbReports" InitialValue="--Please Select--" ValidationGroup="group2"
                                                                                meta:resourcekey="reqDosageFormResource1"></asp:RequiredFieldValidator>
                                                                            <cc1:ValidatorCalloutExtender ID="ExtenderreqDosageForm" runat="server" TargetControlID="reqDosageForm"
                                                                                Enabled="True">
                                                                            </cc1:ValidatorCalloutExtender>
                                                                        </td>
                                                                        <td width="50">
                                                                            <table align="center">
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td width="400">
                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td width="120px" class="style2">
                                                                                        <asp:Label ID="Label1" runat="server" Text="Report Name" meta:resourcekey="Label1Resource1"></asp:Label>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="TextBox1" runat="server" Width="200px" meta:resourcekey="txtReportNameResource1"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rfvReportName" runat="server" ControlToValidate="txtReportName"
                                                                                            Display="None" ValidationGroup="group2" meta:resourcekey="rfvReportNameResource1"></asp:RequiredFieldValidator>
                                                                                        <cc1:ValidatorCalloutExtender ID="vceReportName" runat="server" Enabled="True" TargetControlID="rfvReportName">
                                                                                        </cc1:ValidatorCalloutExtender>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td width="50">
                                                                            <table align="center">
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:Label ID="lblChooseFrom" runat="server" Text="Columns To Choose From" CssClass="Profiletitletxt"
                                                                    meta:resourcekey="lblChooseFromResource1" />
                                                            </td>
                                                            <td align="left">
                                                                <table border="0" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ListBox ID="lbxItemTypes" runat="server" Width="200px" SelectionMode="Multiple"
                                                                                Height="250px" meta:resourcekey="lbxItemTypesResource1"></asp:ListBox>
                                                                        </td>
                                                                        <td width="50">
                                                                            <table align="center">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="ibtnItemAdd" runat="server" ImageUrl="~/images/ibtnAdd.jpg"
                                                                                            Width="16px" ImageAlign="Middle" meta:resourcekey="ibtnItemAddResource1" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="ibtnItemRemove" runat="server" Width="16px" ImageUrl="~/images/ibtnRemove.jpg"
                                                                                            meta:resourcekey="ibtnItemRemoveResource1" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td width="400">
                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td width="120px" class="style2">
                                                                                        <asp:Label ID="lblToIncluded" runat="server" Text="Columns To be included" CssClass="Profiletitletxt"
                                                                                            meta:resourcekey="lblToIncludedResource1" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:ListBox ID="lbxItemTypes2" runat="server" Width="200px" SelectionMode="Multiple"
                                                                                            Height="250px" meta:resourcekey="lbxItemTypes2Resource1"></asp:ListBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td width="50">
                                                                            <table align="center">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="ibtnItemUp" runat="server" ImageUrl="~/images/ibtnUp.JPG" Width="16px"
                                                                                            ImageAlign="Middle" meta:resourcekey="ibtnItemUpResource1" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="ibtnItemDown" runat="server" ImageUrl="~/images/ibtnDown.JPG"
                                                                                            Width="16px" ImageAlign="Middle" meta:resourcekey="ibtnItemDownResource1" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <div id="divFilter" runat="server" style="left">
                                                        <table border="0" width="50%" id="tblSearchCriteria1">
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Label ID="Label4" runat="server" Text="Search Criteria" meta:resourcekey="Label4Resource1" />
                                                                    <hr />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table border="0" width="50%" align="center">
                                                            <tr>
                                                                <td style="width: 130px;">
                                                                    <asp:Label ID="lblOperator" runat="server" Text="Operator" meta:resourcekey="lblOperatorResource1" />
                                                                </td>
                                                                <td style="width: 250px;">
                                                                    <telerik:RadComboBox ID="radSelectOperator" runat="server" Width="150px" DataValueField="OR"
                                                                        Skin="Vista" meta:resourcekey="radSelectOperatorResource1" />
                                                                </td>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please select Operator"
                                                                        ValidationGroup="group1" ControlToValidate="radSelectOperator" InitialValue="--Please Select--"
                                                                        Display="None" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                                                    <cc1:ValidatorCalloutExtender ID="ExtenderRequiredFieldValidator3" TargetControlID="RequiredFieldValidator3"
                                                                        runat="server" Enabled="True">
                                                                    </cc1:ValidatorCalloutExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblColumnName" runat="server" Text="Column" meta:resourcekey="lblColumnNameResource1"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadComboBox ID="radSelectColumn" runat="server" Width="200px" DataValueField="OR"
                                                                        AutoPostBack="True" Skin="Vista" meta:resourcekey="radSelectColumnResource1" />
                                                                </td>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select Column Name"
                                                                        ValidationGroup="group1" ControlToValidate="radSelectColumn" InitialValue="--Please Select--"
                                                                        Display="None" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                                                    <cc1:ValidatorCalloutExtender ID="ExtenderRequiredFieldValidator1" TargetControlID="RequiredFieldValidator1"
                                                                        runat="server" Enabled="True">
                                                                    </cc1:ValidatorCalloutExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblSelectCondition" runat="server" Text="Condition" meta:resourcekey="lblSelectConditionResource1"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadComboBox ID="radSelectCondition" runat="server" Width="150px" DataValueField="OR"
                                                                        AutoPostBack="True" Skin="Vista" meta:resourcekey="radSelectConditionResource1">
                                                                    </telerik:RadComboBox>
                                                                </td>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select Condition"
                                                                        ValidationGroup="group1" ControlToValidate="radSelectCondition" InitialValue="--Please Select--"
                                                                        Display="None" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                                                    <cc1:ValidatorCalloutExtender ID="ExtenderRequiredFieldValidator2" TargetControlID="RequiredFieldValidator2"
                                                                        runat="server" Enabled="True">
                                                                    </cc1:ValidatorCalloutExtender>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table border="0" width="50%" runat="server" id="tblSearchString" visible="False"
                                                            align="center">
                                                            <tr id="Tr1" runat="server">
                                                                <td id="Td1" style="width: 130px;" runat="server">
                                                                    <asp:Label ID="lblSearchKey" runat="server" Text="Search Key"></asp:Label>
                                                                </td>
                                                                <td id="Td2" style="width: 250px;" runat="server">
                                                                    <asp:TextBox ID="txtSearchKey" runat="server" Width="250px"></asp:TextBox>
                                                                </td>
                                                                <td id="Td3" runat="server">
                                                                    <asp:RequiredFieldValidator ID="ReqSearchKey" runat="server" ControlToValidate="txtSearchKey"
                                                                        Display="None" ErrorMessage="Please enter Search key" ValidationGroup="group1"></asp:RequiredFieldValidator>
                                                                    <cc1:ValidatorCalloutExtender ID="ExtenderReqSearchKey" runat="server" TargetControlID="ReqSearchKey"
                                                                        Enabled="True">
                                                                    </cc1:ValidatorCalloutExtender>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table border="0" width="50%" runat="server" id="tblSearchDate" visible="False" align="center">
                                                            <tr id="Tr2" runat="server">
                                                                <td id="Td4" style="width: 130px;" runat="server">
                                                                    <asp:Label ID="lblFromDate" runat="server"></asp:Label>
                                                                </td>
                                                                <td id="Td5" style="width: 250px;" runat="server">
                                                                    <telerik:RadDatePicker ID="dteFromDate" runat="server" Width="200px" Culture="en-US">
                                                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                                        </Calendar>
                                                                        <DateInput DateFormat="dd/MM/yyyy" ddisplaydateformat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                            LabelCssClass="" Width="">
                                                                        </DateInput>
                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                                                    </telerik:RadDatePicker>
                                                                </td>
                                                                <td id="Td6" runat="server">
                                                                    <asp:RequiredFieldValidator ID="ReqFromDate" runat="server" ErrorMessage="*" ValidationGroup="group1"
                                                                        ControlToValidate="dteFromDate" Enabled="False" Display="None"></asp:RequiredFieldValidator>
                                                                    <cc1:ValidatorCalloutExtender ID="ExtenderReqFromDate" TargetControlID="ReqFromDate"
                                                                        runat="server" Enabled="True">
                                                                    </cc1:ValidatorCalloutExtender>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr3" runat="server">
                                                                <td id="Td7" runat="server">
                                                                    <asp:Label ID="lblToDate" runat="server"></asp:Label>
                                                                </td>
                                                                <td id="Td8" runat="server">
                                                                    <telerik:RadDatePicker ID="dteToDate" runat="server" Width="200px" Culture="en-US">
                                                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                                        </Calendar>
                                                                        <DateInput DateFormat="dd/MM/yyyy" ddisplaydateformat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                                                            LabelCssClass="" Width="">
                                                                        </DateInput>
                                                                        <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                                                                    </telerik:RadDatePicker>
                                                                </td>
                                                                <td id="Td9" runat="server">
                                                                    <asp:RequiredFieldValidator ID="ReqToDate" runat="server" ErrorMessage="*" ValidationGroup="group1"
                                                                        ControlToValidate="dteToDate" Enabled="False" Display="None"></asp:RequiredFieldValidator>
                                                                    <cc1:ValidatorCalloutExtender ID="ExtenderReqToDate" TargetControlID="ReqToDate"
                                                                        runat="server" Enabled="True">
                                                                    </cc1:ValidatorCalloutExtender>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table border="0" width="50%" runat="server" id="tblSearchNumber" visible="False"
                                                            align="center">
                                                            <tr id="Tr4" runat="server">
                                                                <td id="Td10" style="width: 130px;" runat="server">
                                                                    <asp:Label ID="lblFrom" runat="server"></asp:Label>
                                                                </td>
                                                                <td id="Td11" style="width: 250px;" runat="server">
                                                                    <telerik:RadNumericTextBox ID="txtFrom" DataType="System.Int32" runat="server" Width="150px"
                                                                        Culture="en-GB" LabelCssClass="">
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                                </td>
                                                                <td id="Td12" runat="server">
                                                                    <asp:RequiredFieldValidator ID="ReqFrom" runat="server" ErrorMessage="*" ValidationGroup="group1"
                                                                        ControlToValidate="txtFrom" Enabled="False" Display="None"></asp:RequiredFieldValidator>
                                                                    <cc1:ValidatorCalloutExtender ID="ExtenderReqFrom" TargetControlID="ReqFrom" runat="server"
                                                                        Enabled="True">
                                                                    </cc1:ValidatorCalloutExtender>
                                                                </td>
                                                            </tr>
                                                            <tr id="Tr5" runat="server">
                                                                <td id="Td13" runat="server">
                                                                    <asp:Label ID="lblTo" runat="server"></asp:Label>
                                                                </td>
                                                                <td id="Td14" runat="server">
                                                                    <telerik:RadNumericTextBox ID="txtTo" DataType="System.Int32" runat="server" Width="150px"
                                                                        Culture="en-GB" LabelCssClass="">
                                                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                    </telerik:RadNumericTextBox>
                                                                </td>
                                                                <td id="Td15" runat="server">
                                                                    <asp:RequiredFieldValidator ID="ReqTo" runat="server" ErrorMessage="*" ValidationGroup="group1"
                                                                        ControlToValidate="txtTo" Enabled="False" Display="None"></asp:RequiredFieldValidator>
                                                                    <cc1:ValidatorCalloutExtender ID="ExtenderReqTo" TargetControlID="ReqTo" runat="server"
                                                                        Enabled="True">
                                                                    </cc1:ValidatorCalloutExtender>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="50%" align="center">
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Button ID="ibtnSave" runat="server" Text="Add Condition" CssClass="button" ValidationGroup="group1"
                                                                        Visible="False" meta:resourcekey="ibtnSaveResource1" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table width="50%" align="center">
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:GridView ID="dgrdConditions" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                                                                        EmptyDataText="No Filter Conditions" meta:resourcekey="dgrdConditionsResource1">
                                                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Condition" meta:resourcekey="TemplateFieldResource1">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCondition" runat="server" meta:resourcekey="lblConditionResource1"
                                                                                        Text='<%# DataBinder.Eval(Container,"DataItem.Condition") %>'></asp:Label></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField meta:resourcekey="TemplateFieldResource2" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblConditionID" runat="server" meta:resourcekey="lblConditionIDResource1"
                                                                                        Text='<%# DataBinder.Eval(Container,"DataItem.CondValue") %>'></asp:Label></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField meta:resourcekey="TemplateFieldResource3">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" meta:resourcekey="lnkDeleteResource1"
                                                                                        OnClick="lnkDelete_Click" Text="Delete"></asp:LinkButton></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle CssClass="HeaderStyle" />
                                                                        <PagerStyle CssClass="PagerStyle" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table align="center">
                                                        <tr>
                                                            <td>
                                                                <asp:ImageButton ID="ibtnGenerateReport" runat="server" Width="70px" ImageUrl="~/images/view_o.gif"
                                                                    ValidationGroup="group2" meta:resourcekey="ibtnGenerateReportResource1" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="Ibtnclr" runat="server" Width="70px" ImageUrl="~/images/btn_clear_02.gif"
                                                                    meta:resourcekey="IbtnclrResource1" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:View>
                <asp:View ID="Report" runat="server">
                    <table align="center" style="background-color: #fff; width: 600px; height: 44px;">
                        <tr>
                            <td colspan="3" align="center">
                                <asp:ImageButton ID="btnExportToPDF" ImageUrl="~/Icons/pdf.png" runat="server" Width="10%" meta:resourcekey="btnExportToPDFResource1" />
                                <asp:ImageButton ID="btnExportToExcel" ImageUrl="~/Icons/Microsoft-Excel-icon.png"
                                    runat="server" Width="10%" meta:resourcekey="btnExportToExcelResource1" />
                                <br />
                                <telerik:RadGrid ID="grdDynamicReport" runat="server" AllowSorting="True" AllowPaging="True"
                                    Width="100%" PageSize="25" Skin="Hay" GridLines="None"
                                    ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True" meta:resourcekey="grdDynamicReportResource1">
                                    <GroupingSettings CaseSensitive="False" />
                                    <ExportSettings HideStructureColumns="True">
                                    </ExportSettings>
                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <MasterTableView AllowMultiColumnSorting="True">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" ShowExportToExcelButton="True" ShowExportToPdfButton="True" />
                                    </MasterTableView>
                                    <SelectedItemStyle ForeColor="Maroon" />
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
            <td></td>
            <td align="center">
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="button"
                    Text="Cancel" meta:resourcekey="btnCancelResource1" />
            </td>
            </table>
                                    </td>
                                </tr>
                   <caption>
                   </caption>

            </table>
                        </div>
                        <div style="width: 100%; height: 44px;">
                        </div>
            </td>
                </tr>
            </table>
    
</asp:Content>
