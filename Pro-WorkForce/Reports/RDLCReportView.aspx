<%@ Page Title="Dynamic Report" Language="VB" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="RDLCReportView.aspx.vb" Inherits="Reports_RDLCReportView"
    meta:resourcekey="PageResource1" UICulture="auto" Theme="SvTheme" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/TA_innerpage.css" rel="stylesheet" type="text/css" />--%>
    <style type="text/css">
        .xx
        {
            background-color: #000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="Filter" runat="server">

            <uc1:PageHeader ID="lblReportTitle" runat="server" />

            <div class="row">
                <div class="col-md-2">

                    <asp:Label ID="lblSelectReport" runat="server" Text="Select Report" CssClass="Profiletitletxt"
                        meta:resourcekey="lblSelectReportResource1" />
                </div>
                <div class="col-md-3">
                    <telerik:RadComboBox ID="CmbReports" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                        DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" CaseSensitive="False"
                        MarkFirstMatch="True" Skin="Vista" CausesValidation="false" ValidationGroup="group2"
                        meta:resourcekey="CmbReportsResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqDosageForm" runat="server" Display="None" ErrorMessage="Please select a report Name"
                        ControlToValidate="CmbReports" InitialValue="--Please Select--" ValidationGroup="group2"
                        meta:resourcekey="reqDosageFormResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderreqDosageForm" runat="server" TargetControlID="reqDosageForm"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">

                    <asp:Label ID="Label1" runat="server" Text="Report Name" meta:resourcekey="Label1Resource1"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtReportName" runat="server" meta:resourcekey="txtReportNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvReportName" runat="server" ControlToValidate="txtReportName"
                        Display="None" ValidationGroup="group2" meta:resourcekey="rfvReportNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceReportName" runat="server" Enabled="True" TargetControlID="rfvReportName">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="textmove">
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="lblChooseFrom" runat="server" Text="Columns To Choose From" CssClass="Profiletitletxt"
                            meta:resourcekey="lblChooseFromResource1" />
                        <asp:ListBox ID="lbxItemTypes" runat="server" Width="100%" SelectionMode="Multiple"
                            Height="250px" meta:resourcekey="lbxItemTypesResource1"></asp:ListBox>
                    </div>
                    <div class="col-md-1 text-center">
                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                        <div style="padding-top: 100px;">
                            <div>
                                <asp:ImageButton ID="ibtnItemAdd" runat="server" ImageUrl="~/images/ibtnAdd.jpg"
                                    Width="16px" ImageAlign="Middle" meta:resourcekey="ibtnItemAddResource1" />
                            </div>
                            <div>
                                <asp:ImageButton ID="ibtnItemRemove" runat="server" Width="16px" ImageUrl="~/images/ibtnRemove.jpg"
                                    meta:resourcekey="ibtnItemRemoveResource1" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="lblToIncluded" runat="server" Text="Columns To be included" CssClass="Profiletitletxt"
                            meta:resourcekey="lblToIncludedResource1" />
                        <asp:ListBox ID="lbxItemTypes2" runat="server" Width="100%" SelectionMode="Multiple"
                            Height="250px" meta:resourcekey="lbxItemTypes2Resource1"></asp:ListBox>
                    </div>
                    <div class="col-md-1">
                        <div style="padding-top: 100px;">
                            <div>
                                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                <asp:ImageButton ID="ibtnItemUp" runat="server" ImageUrl="~/images/ibtnUp.JPG" Width="16px"
                                    ImageAlign="Middle" meta:resourcekey="ibtnItemUpResource1" />
                            </div>
                            <div>
                                <asp:ImageButton ID="ibtnItemDown" runat="server" ImageUrl="~/images/ibtnDown.JPG"
                                    Width="16px" ImageAlign="Middle" meta:resourcekey="ibtnItemDownResource1" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divFilter" runat="server" class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label4" runat="server" Text="Search Criteria" meta:resourcekey="Label4Resource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblOperator" runat="server" Text="Operator" meta:resourcekey="lblOperatorResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="radSelectOperator" runat="server" Width="150px" DataValueField="OR"
                        Skin="Vista" meta:resourcekey="radSelectOperatorResource1" />

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please select Operator"
                        ValidationGroup="group1" ControlToValidate="radSelectOperator" InitialValue="--Please Select--"
                        Display="None" meta:resourcekey="RequiredFieldValidator3Resource1" Enabled="false"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderRequiredFieldValidator3" TargetControlID="RequiredFieldValidator3"
                        runat="server" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblColumnName" runat="server" Text="Column" meta:resourcekey="lblColumnNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="radSelectColumn" runat="server" DataValueField="OR"
                        AutoPostBack="True" Skin="Vista" meta:resourcekey="radSelectColumnResource1" />

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select Column Name"
                        ValidationGroup="group1" ControlToValidate="radSelectColumn" InitialValue="--Please Select--"
                        Display="None" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderRequiredFieldValidator1" TargetControlID="RequiredFieldValidator1"
                        runat="server" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblSelectCondition" runat="server" Text="Condition" meta:resourcekey="lblSelectConditionResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="radSelectCondition" runat="server" DataValueField="OR"
                        AutoPostBack="True" Skin="Vista" meta:resourcekey="radSelectConditionResource1">
                    </telerik:RadComboBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select Condition"
                        ValidationGroup="group1" ControlToValidate="radSelectCondition" InitialValue="--Please Select--"
                        Display="None" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderRequiredFieldValidator2" TargetControlID="RequiredFieldValidator2"
                        runat="server" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div id="tblSearchString" class="row" runat="server" visible="false">
                <div class="col-md-2">
                    <asp:Label ID="lblSearchKey" runat="server" Text="Search Key"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtSearchKey" runat="server"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="ReqSearchKey" runat="server" ControlToValidate="txtSearchKey"
                        Display="None" ErrorMessage="Please enter Search key" ValidationGroup="group1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderReqSearchKey" runat="server" TargetControlID="ReqSearchKey"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row" runat="server" id="tblSearchDate" visible="false">
                <div class="col-md-2">
                    <asp:Label ID="lblFromDate" runat="server"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dteFromDate" runat="server" Culture="en-US">


                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>


                        <DateInput DateFormat="dd/MM/yyyy" ddisplaydateformat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass="" Width="">
                        </DateInput>


                        <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />


                    </telerik:RadDatePicker>

                    <asp:RequiredFieldValidator ID="ReqFromDate" runat="server" ErrorMessage="*" ValidationGroup="group1"
                        ControlToValidate="dteFromDate" Enabled="False" Display="None"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderReqFromDate" TargetControlID="ReqFromDate"
                        runat="server" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>

                <div class="col-md-2">
                    <asp:Label ID="lblToDate" runat="server"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dteToDate" runat="server" Width="200px" Culture="en-US">


                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>


                        <DateInput DateFormat="dd/MM/yyyy" ddisplaydateformat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass="" Width="">
                        </DateInput>


                        <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />


                    </telerik:RadDatePicker>

                    <asp:RequiredFieldValidator ID="ReqToDate" runat="server" ErrorMessage="*" ValidationGroup="group1"
                        ControlToValidate="dteToDate" Enabled="False" Display="None"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderReqToDate" TargetControlID="ReqToDate"
                        runat="server" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div id="tblSearchNumber" runat="server" class="row" visible="false">
                <div class="col-md-2"></div>
                <div class="col-md-2">
                    <asp:Label ID="lblFrom" runat="server"></asp:Label>
                </div>
                <div class="col-md-2">
                    <telerik:RadNumericTextBox ID="txtFrom" DataType="System.Int32" runat="server" Width="150px"
                        Culture="en-GB" LabelCssClass="">


                        <NumberFormat DecimalDigits="0" GroupSeparator="" />


                    </telerik:RadNumericTextBox>

                    <asp:RequiredFieldValidator ID="ReqFrom" runat="server" ErrorMessage="*" ValidationGroup="group1"
                        ControlToValidate="txtFrom" Enabled="False" Display="None"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderReqFrom" TargetControlID="ReqFrom" runat="server"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
                <div class="col-md-12"></div>
                <div class="col-md-2"></div>
                <div class="col-md-2">
                    <asp:Label ID="lblTo" runat="server"></asp:Label>
                </div>
                <div class="col-md-2">
                    <telerik:RadNumericTextBox ID="txtTo" DataType="System.Int32" runat="server" Width="150px"
                        Culture="en-GB" LabelCssClass="">


                        <NumberFormat DecimalDigits="0" GroupSeparator="" />


                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="ReqTo" runat="server" ErrorMessage="*" ValidationGroup="group1"
                        ControlToValidate="txtTo" Enabled="False" Display="None"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderReqTo" TargetControlID="ReqTo" runat="server"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-2">
                    <asp:Button ID="ibtnSave" runat="server" Text="Add Condition" CssClass="button" ValidationGroup="group1"
                        Visible="False" meta:resourcekey="ibtnSaveResource1" />
                </div>
            </div>

            <div class="row">
                <div class="table-responsive">
                    <asp:GridView ID="dgrdConditions" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        EmptyDataText="No Filter Conditions" meta:resourcekey="dgrdConditionsResource1">
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Condition" meta:resourcekey="TemplateFieldResource1">


                                <ItemTemplate>


                                    <asp:Label ID="lblCondition" runat="server" meta:resourcekey="lblConditionResource1" Text='<%# DataBinder.Eval(Container,"DataItem.Condition") %>'></asp:Label>


                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource2" Visible="False">


                                <ItemTemplate>


                                    <asp:Label ID="lblConditionID" runat="server" meta:resourcekey="lblConditionIDResource1" Text='<%# DataBinder.Eval(Container,"DataItem.CondValue") %>'></asp:Label>


                                </ItemTemplate>


                            </asp:TemplateField>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource3">


                                <ItemTemplate>


                                    <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" meta:resourcekey="lnkDeleteResource1" OnClick="lnkDelete_Click" Text="Delete"></asp:LinkButton>


                                </ItemTemplate>


                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="HeaderStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                    </asp:GridView>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    <asp:Button ID="ibtnGenerateReport" runat="server" Text="Button" meta:resourcekey="lnkDeleteResource2"
                        ValidationGroup="group2" CssClass="button" />
                    <asp:Button ID="Ibtnclr" runat="server" Text="Button" meta:resourcekey="lnkDeleteResource3"
                        ValidationGroup="group2" CssClass="button" />
                </div>
            </div>
        </asp:View>
        <asp:View ID="Report" runat="server">
            <div class="row">
                <div class="col-md-6 text-center">
                    <%--<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                            meta:resourcekey="ReportViewer1Resource1">
                        </rsweb:ReportViewer>--%>
                    <asp:RadioButtonList ID="rblFormat" runat="server" RepeatDirection="Horizontal"
                        CssClass="Profiletitletxt">
                        <asp:ListItem Text="PDF" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="MS Word" Value="2"></asp:ListItem>
                        <asp:ListItem Text="MS Excel" Value="3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="table-responsive">
                <telerik:RadGrid ID="grdDynamicReport" runat="server" AllowSorting="True" AllowPaging="True"
                    Width="100%" PageSize="25" GridLines="None" AutoGenerateColumns="true"
                    ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True">
                    <SelectedItemStyle ForeColor="Maroon" />
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="true">
                        <CommandItemSettings ExportToExcelText="Export to Excel" ExportToPdfText="Export to Pdf"
                             ShowExportToExcelButton="true" ShowExportToPdfButton="true" />
                    </MasterTableView>
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="button" meta:resourcekey="btnprint" />
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
