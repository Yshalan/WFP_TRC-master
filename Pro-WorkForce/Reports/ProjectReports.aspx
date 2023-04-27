<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjectReports.aspx.vb" MasterPageFile="~/Default/ReportMaster.master"
    Inherits="Reports_ProjectReports" meta:resourcekey="PageResource1" UICulture="auto" Theme="SvTheme" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc2" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagPrefix="uc1" TagName="EmployeeFilter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/TA_innerpage.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript">
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
    </script>
       <uc1:PageHeader ID="lblReportTitle" runat="server" HeaderText="Project Reports" />
     <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblExtraReports" runat="server" CssClass="Profiletitletxt" Text="Report Name" meta:resourcekey="lblSelfServiceReportsResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadComboExtraReports" CausesValidation="False" Filter="Contains"
                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
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
     <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
       

           <asp:View ID="vwEmployeeTask" runat="server">
                <%--  <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Employee Task Report" />--%>
            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilter ID="EmployeeFilter1" runat="server" ShowRadioSearch="true" ValidationGroup="btnPrint" />
                </div>
            </div>
               <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblempFromDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblFromDateResource1"
                        Text="From Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="radempFromDate" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker1Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="radempFromDate"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator9_ValidatorCalloutExtender"
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
                <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblempToDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblToDateResource1"
                        Text="To Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="radempToDate" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker2Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radempToDate"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator1">
                    </cc1:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="radempFromDate"
                        ControlToValidate="radempToDate" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="btnPrint" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender2"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>

                  <div class="row">
               <div class="col-md-2">
                    <asp:Label ID="Label3" runat="server" CssClass="Profiletitletxt"  Text="Project Task Completed"></asp:Label>
                </div>
                <div class="col-md-8">
                    <asp:RadioButtonList ID="rdbEmpCompleted" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt"
                        meta:resourcekey="rblFormatResource1">
                        <asp:ListItem Text="All" Value="1" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="Completed" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Pending" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
                

        </asp:View>
              <asp:View ID="vwProjectTask" runat="server">
                       <%--  <uc1:PageHeader ID="PageHeader2" runat="server" HeaderText="Project Task Report" />--%>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblproject" runat="server"  Text="Projects" ></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="radproject" Filter="Contains" MarkFirstMatch="True" Skin="Vista"
                        runat="server" >
                    </telerik:RadComboBox>
                </div>
            </div>


                  
               <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblFromDateResource1"
                        Text="From Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="radProjectFromDatea" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker1Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radProjectFromDatea"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3"
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
                <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label2" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblToDateResource1"
                        Text="To Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="radProjectToDatea" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker2Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="radProjectToDatea"
                        Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator1">
                    </cc1:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="radProjectFromDatea"
                        ControlToValidate="radProjectToDatea" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="btnPrint" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender5"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
                  
                  <div class="row">
               <div class="col-md-2">
                    <asp:Label ID="lblCompleted" runat="server" CssClass="Profiletitletxt"  Text="Project Task Completed"></asp:Label>
                </div>
                <div class="col-md-8">
                    <asp:RadioButtonList ID="rdbIsCompleted" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt"
                        meta:resourcekey="rblFormatResource1">
                        <asp:ListItem Text="All" Value="1" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="Completed" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Pending" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
     <div class="row">
         </div>
         <%--  <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt"
                        meta:resourcekey="rblFormatResource1">
                        <asp:ListItem Text="PDF" Value="1" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div id="Div1" runat="server" class="row">
                <div class="col-md-2"></div>
                <div class="col-md-2">
                    <asp:Button ID="btnprintProject" runat="server" Text="Print" CssClass="button"  ValidationGroup="btnPrint" />

                </div>
            </div>--%>

        </asp:View>
         <asp:View ID="Report" runat="server">
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
     <div class="row">
         </div>
                <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:RadioButtonList ID="rblFormat" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt"
                        meta:resourcekey="rblFormatResource1">
                        <asp:ListItem Text="PDF" Value="1" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
           <%-- <div id="trControls" runat="server" class="row">
                <div class="col-md-2"></div>
                <div class="col-md-2">
                    

                </div>
            </div>--%>
        <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-2">
            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button" meta:resourcekey="Button1Resource1"
                        ValidationGroup="btnPrint" />
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="button"
                Text="Cancel" meta:resourcekey="btnCancelResource1" />
        </div>
    </div>
</asp:Content>