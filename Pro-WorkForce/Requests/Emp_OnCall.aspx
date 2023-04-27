<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Emp_OnCall.aspx.vb" Theme="SvTheme"
  MasterPageFile="~/Default/NewMaster.master"
Inherits="Requests_Emp_OnCall" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

                <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Employees On Call" />
              <asp:MultiView ID="mvEmpLastBalance" runat="server">
                <asp:View ID="viewEmpLastBalance" runat="server">
             <div class="row">
                 <div class="col-md-12">
                                <uc1:PageFilter ID="EmployeeFilterUC" runat="server" ValidationGroup="ValidateGet"
                                    ShowRadioSearch="false" />
                           </div>
                        </div>
                        <div class="row">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="Label5" runat="server" Text="Date"
                            meta:resourcekey="Label5Resource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker ID="dtpDutyDate" runat="server" AllowCustomText="false"
                            ShowPopupOnFocus="True" MarkFirstMatch="true" PopupDirection="TopRight" Skin="Vista"
                            Culture="en-US" meta:resourcekey="dtpDutyDateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dtpDutyDate"
                            Display="None" ErrorMessage="Please Enter Request Date" meta:resourcekey="RequiredFieldValidator5Resource1"
                            ValidationGroup="empleave"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="RequiredFieldValidator5" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                    <div class="row">

                        <div class="col-md-2">
                            <asp:Label ID="lblFromTime" runat="server" CssClass="Profiletitletxt" Text="From Time"
                                meta:resourcekey="lblFromTimeResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadMaskedTextBox ID="rmtFromTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                    DisplayMask="##:##" Text='0000' LabelCssClass="" meta:resourcekey="rmtFromTimeResource1">
                                </telerik:RadMaskedTextBox>
                            </div>
                        
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblToTime" runat="server" CssClass="Profiletitletxt" Text="To Time"
                                meta:resourcekey="lblToTimeResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadMaskedTextBox ID="rmtToTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                DisplayMask="##:##" Text='0000' LabelCssClass="" meta:resourcekey="rmtToTimeResource1">
                            </telerik:RadMaskedTextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chckFromHome" runat="server" Text="From Home"
                                meta:resourcekey="lblFromHomeResource1" />
                        </div>

                    </div>
                    <div class="row" id="trControls" runat="server">
                        <div class="col-md-2"></div>
                        <div class="col-md-8" id="Td1" colspan="3" runat="server">
                            <asp:Button ID="btnSave" runat="server" Text="Save"
                                ValidationGroup="VGGroup" CssClass="button" meta:resourcekey="btnSaveResource1" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" meta:resourcekey="btnDeleteResource1" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClearResource1" />
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblLevels" runat="server" Text="Entity" CssClass="Profiletitletxt"
                                meta:resourcekey="lblLevelsResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="RadCmbBxEntity" Filter="Contains"
                                MarkFirstMatch="True" Skin="Vista" runat="server"
                                meta:resourcekey="RadCmbBxEntityResource1">
                            </telerik:RadComboBox>


                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblDesignation" runat="server" Text="Designation"
                                meta:resourcekey="lblDesignationResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="RadCmbDesignation" runat="server" MarkFirstMatch="True"
                                Skin="Vista" meta:resourcekey="RadCmbDesignationResource1">
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblFromDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblFromDateResource1"
                                Text="From Date"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Culture="English (United States)"
                                meta:resourcekey="RadDatePicker1Resource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                    Width="">
                                </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="RadDatePicker1"
                                Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator9_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblToDateResource1"
                                Text="To Date"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="RadDatePicker2" runat="server" Culture="English (United States)"
                                meta:resourcekey="RadDatePicker2Resource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                    Width="">
                                </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadDatePicker2"
                                Display="None" ErrorMessage="Please Select Date" ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                TargetControlID="RequiredFieldValidator1">
                            </cc1:ValidatorCalloutExtender>
                            <br />
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="RadDatePicker1"
                                ControlToValidate="RadDatePicker2" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                                Operator="GreaterThanEqual" Type="Date" ValidationGroup="btnPrint" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender2"
                                runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-2">
                            <asp:Button ID="btnGetByFilter" runat="server" Text="Filter"
                                Style="margin-left: 35px"
                                class="button" meta:resourcekey="btnGetByFilterResource1" />

                        </div>
                    </div>
          
              <div class="row">
                <div class="table-responsive">
                    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                    <div class="filterDiv">
                        <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdEmployeesEmpOnCall"
                            ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
                    </div>
                    <telerik:RadGrid runat="server" ID="dgrdEmployeesEmpOnCall" AutoGenerateColumns="False"
                        PageSize="15"  AllowPaging="True" AllowSorting="True" GridLines="None"
                        meta:resourcekey="dgrdSchedule_EmployeeGroupsResource1">
                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="OnCallId,DutyDate,FK_EmployeeId">
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick"
                                    meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                            ImageUrl="~/images/RadFilter.gif" runat="server" meta:resourcekey="RadToolBarButtonResource1"
                                            Owner="" />
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" 
                                    meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="FK_EmployeeId" SortExpression="FK_EmployeeId"
                                    Visible="False" meta:resourcekey="GridBoundColumnResource1" UniqueName="FK_EmployeeId">
                                </telerik:GridBoundColumn>
                               <telerik:GridBoundColumn DataField="OnCallId" SortExpression="OnCallId"
                                    Visible="False" meta:resourcekey="GridBoundColumnResource1" UniqueName="OnCallId">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No." SortExpression="EmployeeNo"
                                    meta:resourcekey="GridBoundColumnResource4" UniqueName="EmployeeNo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"
                                    meta:resourcekey="GridBoundColumnResource5" UniqueName="EmployeeName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Arabic Name"
                                    SortExpression="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource6"
                                    UniqueName="EmployeeArabicName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DutyDate" HeaderText="Date"
                                    SortExpression="DutyDate" meta:resourcekey="GridBoundColumnResource6"
                                    UniqueName="DutyDate">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                            EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
                </asp:View>
                </asp:MultiView>
            </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>