<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="HR_LeaveRequest.aspx.vb" Inherits="Employee_HR_LeaveRequest" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlAssignEmployees" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" />
            <asp:UpdatePanel ID="Update1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-12">
                            <uc2:PageFilter ID="EmployeeFilterUC" runat="server" HeaderText="Employee Filter"
                                OneventEmployeeSelect="FillEmpLeaveGrid" ValidationGroup="empleave" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="Label4" runat="server"
                                Text="Leave Type" meta:resourcekey="Label4Resource1" />
                            <asp:HiddenField ID="hdnLeaveSufficient" runat="server" />
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="ddlLeaveType" runat="server" AppendDataBoundItems="True"
                                MarkFirstMatch="True" AutoPostBack="True" CausesValidation="False"
                                Skin="Vista" meta:resourcekey="ddlLeaveTypeResource1" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlLeaveType"
                                Display="None" ErrorMessage="Please Select Leave Type" InitialValue="-1"
                                ValidationGroup="empleave" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CssClass="AISCustomCalloutStyle"
                                Enabled="True" TargetControlID="RequiredFieldValidator4" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="Label5" runat="server"
                                Text="Request Date" meta:resourcekey="Label5Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="dtpRequestDate" runat="server" AllowCustomText="false"
                                ShowPopupOnFocus="True" MarkFirstMatch="true" PopupDirection="TopRight" Skin="Vista"
                                Culture="en-US" meta:resourcekey="dtpRequestDateResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                    Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dtpRequestDate"
                                Display="None" ErrorMessage="Please Enter Request Date"
                                ValidationGroup="empleave" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" CssClass="AISCustomCalloutStyle"
                                Enabled="True" TargetControlID="RequiredFieldValidator5" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="Label7" runat="server"
                                Text="Leave From" meta:resourcekey="Label7Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="dtpFromDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                                Culture="en-US" meta:resourcekey="dtpFromDateResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                    Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtpFromDate"
                                Display="None" ErrorMessage="Please Enter From Date"
                                ValidationGroup="empleave" meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" CssClass="AISCustomCalloutStyle"
                                Enabled="True" TargetControlID="RequiredFieldValidator6" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="Label6" runat="server" Text="To"
                                meta:resourcekey="Label6Resource1"></asp:Label>
                        </div>

                        <div class="col-md-4">

                            <telerik:RadDatePicker ID="dtpToDate" runat="server" AllowCustomText="false"
                                MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                                Culture="en-US" meta:resourcekey="dtpToDateResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                    Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="dtpToDate"
                                Display="None" ErrorMessage="Please Enter To Date"
                                ValidationGroup="empleave" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" CssClass="AISCustomCalloutStyle"
                                Enabled="True" TargetControlID="RequiredFieldValidator7" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="CompareValidator1" Display="None" runat="server" ControlToValidate="dtpToDate"
                                ControlToCompare="dtpFromDate" Operator="GreaterThanEqual" Type="Date" ErrorMessage="To date should be greater than or equal to from date"
                                ValidationGroup="empleave" meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" CssClass="AISCustomCalloutStyle"
                                Enabled="True" TargetControlID="CompareValidator1" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblAttachFile" runat="server" Text="Attched File"
                                CssClass="Profiletitletxt" meta:resourcekey="lblAttachFileResource1" />
                        </div>
                        <div class="col-md-4">
                            <div class="input-group">
                                <span class="input-group-btn">
                                    <span class="btn btn-default" onclick="$(this).parent().find('input[type=file]').click();">Browse</span>
                                    <asp:FileUpload ID="fuAttachFile" runat="server" meta:resourcekey="fuAttachFileResource1" name="uploaded_file" onchange="$(this).parent().parent().find('.form-control').html($(this).val().split(/[\\|/]/).pop());" Style="display: none;" type="file" />
                                </span>
                                <span class="form-control"></span>
                            </div>
                            <div class="veiw_remove">
                                <a id="lnbLeaveFile" target="_blank" runat="server" visible="False">
                                    <asp:Label ID="lblView" runat="server" Text="View" meta:resourcekey="lblViewResource1" />
                                </a>
                                <asp:LinkButton ID="lnbRemove" runat="server" Text="Remove" Visible="False" meta:resourcekey="lnbRemoveResource1" />
                                <asp:Label ID="lblNoAttachedFile" runat="server" Text="No Attached File" Visible="False"
                                    meta:resourcekey="lblNoAttachedFileResource1" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="Label8" runat="server" Text="Remarks"
                                meta:resourcekey="Label8Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine"
                                meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8 text-center">
                            <asp:HiddenField runat="server" ID="hdnWindow" />
                            <cc1:ModalPopupExtender ID="mpeRejectPopup" runat="server" BehaviorID="modelPopupExtender1"
                                TargetControlID="hdnWindow" PopupControlID="divConfirmed"
                                DropShadow="True" CancelControlID="btnCancel"
                                Enabled="True" BackgroundCssClass="ModalBackground"
                                DynamicServicePath="" />
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button"
                                ValidationGroup="empleave" meta:resourcekey="btnSaveResource1" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button"
                                CausesValidation="False" meta:resourcekey="btnDeleteResource1" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button"
                                CausesValidation="False" meta:resourcekey="btnClearResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblFromDate" runat="server" CssClass="Profiletitletxt"
                                Text="From Date" meta:resourcekey="lblFromDateResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="dtpFromDateSearch" runat="server" Culture="en-US"
                                meta:resourcekey="dtpFromDateSearchResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                    Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="dtpFromDateSearch"
                                Display="None" ErrorMessage="Please Select Date"
                                ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator9_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt"
                                Text="To Date" meta:resourcekey="lblToDateResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="dtpToDateSearch" runat="server" Culture="en-US"
                                meta:resourcekey="dtpToDateSearchResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                    Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtpToDateSearch"
                                Display="None" ErrorMessage="Please Select Date"
                                ValidationGroup="btnPrint" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                                TargetControlID="RequiredFieldValidator1">
                            </cc1:ValidatorCalloutExtender>
                            <br />
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="dtpFromDateSearch"
                                ControlToValidate="dtpToDateSearch" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                                Operator="GreaterThanEqual" Type="Date" ValidationGroup="btnPrint"
                                meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                            <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender2"
                                runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8 text-center">
                            <asp:Button ID="btnGet" runat="server" Text="Get" CssClass="button"
                                ValidationGroup="btnPrint" meta:resourcekey="btnGetResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="grdEmpLeaves"
                                Skin="Hay" ShowApplyButton="False"
                                meta:resourcekey="RadFilter1Resource1" />
                            <telerik:RadGrid ID="grdEmpLeaves" runat="server" AllowSorting="True" AllowPaging="True"
                                Width="100%" PageSize="15" GridLines="None"
                                ShowStatusBar="True" AllowMultiRowSelection="True"
                                ShowFooter="True" meta:resourcekey="grdEmpLeavesResource1">
                                <SelectedItemStyle ForeColor="Maroon" />
                                <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="LeaveRequestID,FK_EmployeeId,RequestDate,FromDate,ToDate">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="False"
                                            meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                                <asp:HiddenField ID="hdnLeaveArabicType" runat="server" Value='<%# Eval("LeaveArabicName") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="RequestDate" HeaderText="Request Date"
                                            DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource1"
                                            UniqueName="RequestDate" />
                                        <telerik:GridBoundColumn DataField="LeaveName" HeaderText="Leave Type"
                                            meta:resourcekey="GridBoundColumnResource2" UniqueName="LeaveName" />
                                        <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date"
                                            DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource3"
                                            UniqueName="FromDate" />
                                        <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date"
                                            DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource4"
                                            UniqueName="ToDate" />
                                        <telerik:GridBoundColumn DataField="IsRejected" HeaderText="Status"
                                            meta:resourcekey="GridBoundColumnResource8"
                                            UniqueName="IsRejected" />
                                        <telerik:GridBoundColumn DataField="Days" HeaderText="No. Days"
                                            meta:resourcekey="GridBoundColumnResource5" UniqueName="Days" />
                                        <telerik:GridBoundColumn DataField="LeaveRequestID" AllowFiltering="False"
                                            Visible="False" meta:resourcekey="GridBoundColumnResource6"
                                            UniqueName="LeaveRequestID" />
                                        <telerik:GridBoundColumn DataField="FK_EmployeeId" AllowFiltering="False"
                                            Visible="False" meta:resourcekey="GridBoundColumnResource7"
                                            UniqueName="FK_EmployeeId" />
                                    </Columns>
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                            Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                            <Items>
                                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                    ImagePosition="Right" runat="server"
                                                    meta:resourcekey="RadToolBarButtonResource1" Owner="" />
                                            </Items>
                                        </telerik:RadToolBar>
                                    </CommandItemTemplate>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                </MasterTableView>
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </div>
                    <div id="divConfirmed" class="commonPopup" style="display: none">

                        <div class="row">
                            <div class="col-md-4">
                                <asp:Label ID="lblConfirmed" runat="server" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblConfirmedResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="button"
                                    meta:resourcekey="btnConfirmResource1" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button"
                                    meta:resourcekey="btnCancelResource1" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
