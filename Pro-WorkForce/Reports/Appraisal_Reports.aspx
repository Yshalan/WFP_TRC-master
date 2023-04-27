<%@ Page Title="" Language="VB" MasterPageFile="~/Default/ReportMaster.master" AutoEventWireup="false"
    CodeFile="Appraisal_Reports.aspx.vb" Inherits="Reports_Appraisal_Reports"
    UICulture="auto" Theme="SvTheme" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagPrefix="uc1" TagName="EmployeeFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="Filter" runat="server">

            <uc1:PageHeader ID="lblReportTitle" runat="server" HeaderText="Appraisal Reports" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label Width="125px" ID="lblAppraisalReports" runat="server" CssClass="Profiletitletxt"
                        Text="Appraisal Reports" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadComboAppraisalReports" CausesValidation="False" Filter="Contains"
                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                        ExpandDirection="Up">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvradAppraisalReports" runat="server" ControlToValidate="RadComboAppraisalReports"
                        Display="None" ErrorMessage="Please Select Report Type" InitialValue="--Please Select--"
                        ValidationGroup="btnPrint"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceradAppraisalReports" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvradAppraisalReports" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlYear" DataTextField="txt" DataValueField="val">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" ValidationGroup="btnPrint" ShowDirectStaffCheck="true" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-8">
                    <asp:RadioButtonList ID="rblFormat" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt">
                        <asp:ListItem Text="PDF" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div id="trControls" runat="server" class="row">
                <div class="col-md-12">
                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button"
                        ValidationGroup="btnPrint" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" CssClass="button" />
                </div>
            </div>
        </asp:View>
        <asp:View ID="Report" runat="server">
            <table align="center" style="background-color: #fff; width: 1024px; height: 44px;">
                <tr>
                    <td align="center" style="border-left: 1px; border-right: 1px">
                        <CR2:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
                            GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                            EnableDrillDown="False" GroupTreeImagesFolderUrl=""
                            HasGotoPageButton="False" HasPageNavigationButtons="False" ToolbarImagesFolderUrl=""
                            ToolPanelWidth="200px" />
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>

