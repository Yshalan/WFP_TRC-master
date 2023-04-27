<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="HR_ManualEntryApproval.aspx.vb" Inherits="DailyTasks_HR_ManualEntryApproval"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ShowPopUp(Mode) {
            var lang = '<%= MsgLang %>';
            if (Mode == 1) {
                if (lang == 'en') {
                    return confirm('Are you sure you want to accept Manual Entry?');
                }
                else {
                    return confirm('هل انت متأكد من قبول الادخال اليدوي؟');
                }
            }
            else if (Mode == 2) {
                if (lang == 'en') {
                    return confirm('Are you sure you want to reject Manual Entry?');
                }
                else {
                    return confirm('هل انت متأكد من رفض الادخال اليدوي؟');
                }
            }
        }

        function hideValidatorCalloutTab() {
            try {
                if (AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout != null) {
                    AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.hide();


                }
            }
            catch (err) {
            }
            return false;
        }
        function ResetPopUpManual() {
            var txtRejectedReason = document.getElementById("<%= txtRejectedReason.ClientID %>");
            txtRejectedReason.value = '';
        }
    </script>
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"
        OnClientActiveTabChanged="hideValidatorCalloutTab" meta:resourcekey="TabContainer1Resource1" Style="visibility:visible">
        <cc1:TabPanel ID="tabRequests" runat="server" HeaderText="Manual Entry Requests"
            TabIndex="0" meta:resourcekey="tabRequestsResource1">
            <ContentTemplate>
                <asp:MultiView ID="mvManualEntry" runat="server">
                    <asp:View ID="vManualEntries" runat="server">
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdEntries"
                                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                                    <ContextMenu FeatureGroupID="rfContextMenu">
                                    </ContextMenu>
                                </telerik:RadFilter>
                                <telerik:RadGrid runat="server" ID="dgrdEntries" AllowSorting="True" AllowPaging="True"
                                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                    PageSize="25" ShowFooter="True" meta:resourcekey="dgrdEntriesResource1" CellSpacing="0">
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="MoveRequestId,AttachedFile">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="MoveRequestId" SortExpression="MoveRequestId"
                                                HeaderText="MoveRequestId" Visible="False" AllowFiltering="False" UniqueName="MoveRequestId"
                                                meta:resourcekey="GridBoundColumnResource1" FilterControlAltText="Filter MoveRequestId column" />
                                            <telerik:GridBoundColumn DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No"
                                                UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource10" FilterControlAltText="Filter EmployeeNo column" />
                                            <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                                UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource2" FilterControlAltText="Filter EmployeeName column" />
                                            <telerik:GridBoundColumn DataField="ReasonName" SortExpression="ReasonName" HeaderText="Reason"
                                                UniqueName="ReasonName" meta:resourcekey="GridBoundColumnResource3" FilterControlAltText="Filter ReasonName column" />
                                            <telerik:GridBoundColumn DataField="MoveDate" SortExpression="MoveDate" HeaderText="Date"
                                                DataFormatString="{0:dd/MM/yyyy}" UniqueName="MoveDate" meta:resourcekey="GridBoundColumnResource4" FilterControlAltText="Filter MoveDate column" />
                                            <telerik:GridBoundColumn DataField="MoveTime" SortExpression="MoveTime" HeaderText="Time"
                                                DataFormatString="{0:HH:mm}" UniqueName="MoveTime" meta:resourcekey="GridBoundColumnResource5" FilterControlAltText="Filter MoveTime column" />
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="AttachedFile" Visible="False"
                                                UniqueName="AttachedFile" FilterControlAltText="Filter AttachedFile column" />
                                            <telerik:GridTemplateColumn HeaderText="Attached File" AllowFiltering="False" UniqueName="TemplateColumn"
                                                meta:resourcekey="GridTemplateColumnResource10" FilterControlAltText="Filter TemplateColumn column">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnbView" runat="server" Text="View" OnClick="lnkDownloadFile_Click" meta:resourcekey="lnbViewResource1" />
                                                    <%-- <a id="lnbView" runat="server" onclick="lnkDownloadFile_Click">
                                                        <asp:Label ID="lblView" runat="server" Text="View" meta:resourcekey="lnbViewResource1" />
                                                    </a>--%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="Remarks" SortExpression="Remarks" HeaderText="Remarks"
                                                UniqueName="Remarks" meta:resourcekey="GridBoundColumnResource6" FilterControlAltText="Filter Remarks column" />
                                            <telerik:GridBoundColumn DataField="IsFromMobile" Visible="False" AllowFiltering="False"
                                                UniqueName="IsFromMobile" meta:resourcekey="GridBoundColumnResource7" FilterControlAltText="Filter IsFromMobile column" />
                                            <telerik:GridBoundColumn DataField="EmployeeArabicName" Visible="False" AllowFiltering="False"
                                                UniqueName="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource8" FilterControlAltText="Filter EmployeeArabicName column" />
                                            <telerik:GridBoundColumn DataField="ReasonArabicName" Visible="False" UniqueName="ReasonArabicName"
                                                AllowFiltering="False" meta:resourcekey="GridBoundColumnResource9" FilterControlAltText="Filter ReasonArabicName column" />
                                            <telerik:GridBoundColumn DataField="UserID" SortExpression="UserID" HeaderText="Created By"
                                                meta:resourcekey="GridBoundColumnResource11" UniqueName="UserID" FilterControlAltText="Filter UserID column" />
                                            <telerik:GridBoundColumn DataField="CREATED_DATE" SortExpression="CREATED_DATE" HeaderText="Created Date"
                                                meta:resourcekey="GridBoundColumnResource12" UniqueName="CREATED_DATE" DataFormatString="{0:dd/MM/yyyy}" FilterControlAltText="Filter CREATED_DATE column" />
                                            <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1" FilterControlAltText="Filter TemplateColumn column">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkAccept" runat="server" Text="Accept" OnClick="lnkAccept_Click"
                                                        CommandName="accept" OnClientClick="return ShowPopUp('1')" meta:resourcekey="lnkAcceptResource1">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2" FilterControlAltText="Filter TemplateColumn column">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkReject" runat="server" Text="Reject" CommandName="reject"
                                                        OnClick="lnkReject_Click" OnClientClick="return ShowPopUp('2')" CommandArgument='<%# Eval("MoveRequestId") %>'
                                                        meta:resourcekey="lnkRejectResource1">
                                                    </asp:LinkButton>
                                                    <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                                    <asp:HiddenField ID="hdnReasonArabicName" runat="server" Value='<%# Eval("ReasonArabicName") %>' />
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
                                    </MasterTableView>
                                    <SelectedItemStyle ForeColor="Maroon" />
                                </telerik:RadGrid>
                                <asp:HiddenField runat="server" ID="hdnWindow" />
                                <cc1:ModalPopupExtender ID="mpeRejectPopupManual" runat="server" BehaviorID="modelPopupExtenderManual"
                                    TargetControlID="hdnWindow" PopupControlID="divRejectedManual" DropShadow="True" OnCancelScript="ResetPopUpManual(); return false;"
                                    CancelControlID="btnCancelPOP" Enabled="True" BackgroundCssClass="ModalBackground"
                                    DynamicServicePath="" />
                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vManualEntryDetails" runat="server">
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblDate" runat="server" CssClass="Profiletitletxt" Text="Date" meta:resourcekey="lblDateResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="RadDatePicker1" runat="server" AutoPostBack="True" Culture="en-US"
                                    meta:resourcekey="RadDatePicker1Resource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        Width="" AutoPostBack="True" LabelWidth="64px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="RadDatePicker1"
                                    Display="None" ErrorMessage="Please Select Date,The Max Date Allowed is Today"
                                    meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7"
                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                                    </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <uc2:PageFilter ID="EmployeeFilterUC" runat="server" HeaderText="Employee Filter"
                                    OneventEmployeeSelect="FillGrid" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblReason" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblReasonResource1"
                                    Text="Reason"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="ddlReason" runat="server" MarkFirstMatch="True" AppendDataBoundItems="True"
                                    DropDownStyle="DropDownList" Skin="Vista" CausesValidation="False" Width="225px"
                                    meta:resourcekey="ddlReasonResource1" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                                    ValidationGroup="grpSave" InitialValue="--Please Select--" ErrorMessage="Please Select Type"
                                    ControlToValidate="ddlReason" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4"
                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                    </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="Profiletitletxt" meta:resourcekey="lblTimeResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadMaskedTextBox ID="rmtToTime2" runat="server" Mask="##:##"
                                    CssClass="RadMaskedTextBox" Text='<%# DataBinder.Eval(Container,"DataItem.ToTime2") %>' DisplayMask="##:##"
                                    LabelCssClass="" meta:resourcekey="rmtToTime2Resource1" LabelWidth="64px">
                                    <EmptyMessageStyle Resize="None" />
                                    <ReadOnlyStyle Resize="None" />
                                    <FocusedStyle Resize="None" />
                                    <DisabledStyle Resize="None" />
                                    <InvalidStyle Resize="None" />
                                    <HoveredStyle Resize="None" />
                                    <EnabledStyle Resize="None" />
                                </telerik:RadMaskedTextBox>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="None"
                                    ErrorMessage="Please Time" InitialValue="00:00" ControlToValidate="rmtToTime2"
                                    meta:resourcekey="RequiredFieldValidator8Resource1"></asp:RequiredFieldValidator><cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8"
                                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                    </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblRemarks" runat="server" Text="Remarks" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblRemarksResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Width="220px" meta:resourcekey="txtremarksResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4 text-center">
                                <asp:Button ID="btnAccept" Text="Accept" CssClass="button" runat="server" meta:resourcekey="btnAcceptResource1"></asp:Button><asp:Button ID="btnReject" CssClass="button" Text="Reject" runat="server"
                                    meta:resourcekey="btnRejectResource1"></asp:Button><asp:Button ID="btnCancel" runat="server"
                                        CssClass="button" Text="Cancel" meta:resourcekey="btnCancelResource1" />
                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>
                <div id="divRejectedManual" class="commonPopup" style="display: none">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblRejectedReason" runat="server" Text="Rejected Reason" CssClass="Profiletitletxt"
                                meta:resourcekey="lblRejectedReasonResource1" />
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtRejectedReason" runat="server" TextMode="MultiLine" Height="60px"
                                meta:resourcekey="txtRejectedReasonResource1" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-8">
                            <asp:Button ID="btnRejectPOP" runat="server" Text="Reject" CssClass="button" meta:resourcekey="btnRejectResource1" />
                            <asp:Button ID="btnCancelPOP" runat="server" Text="Cancel" CssClass="button" meta:resourcekey="btnCancelResource1" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="tabRejected" runat="server" HeaderText="Rejected Requests" TabIndex="1"
            meta:resourcekey="tabRejectedResource1">
            <ContentTemplate>
                <div class="row">
                    <div class="table-responsive">
                        <telerik:RadFilter runat="server" ID="RadFilter2" FilterContainerID="dgrdRejectedRequests"
                            Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                        <telerik:RadGrid runat="server" ID="dgrdRejectedRequests" AllowSorting="True" AllowPaging="True"
                            GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                            PageSize="25" ShowFooter="True" meta:resourcekey="dgrdRejectedRequestsResource1">
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="MoveRequestId" SortExpression="MoveRequestId"
                                        HeaderText="MoveRequestId" Visible="False" AllowFiltering="False" UniqueName="MoveRequestId"
                                        meta:resourcekey="GridBoundColumnResource1" />
                                    <telerik:GridBoundColumn DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No"
                                        UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource10" />
                                    <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                        UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource2" />
                                    <telerik:GridBoundColumn DataField="ReasonName" SortExpression="ReasonName" HeaderText="Reason"
                                        UniqueName="ReasonName" meta:resourcekey="GridBoundColumnResource3" />
                                    <telerik:GridBoundColumn DataField="MoveDate" SortExpression="MoveDate" HeaderText="Date"
                                        DataFormatString="{0:dd/MM/yyyy}" UniqueName="MoveDate" meta:resourcekey="GridBoundColumnResource4" />
                                    <telerik:GridBoundColumn DataField="MoveTime" SortExpression="MoveTime" HeaderText="Time"
                                        DataFormatString="{0:HH:mm}" UniqueName="MoveTime" meta:resourcekey="GridBoundColumnResource5" />
                                    <telerik:GridBoundColumn DataField="Remarks" SortExpression="Remarks" HeaderText="Remarks"
                                        UniqueName="Remarks" meta:resourcekey="GridBoundColumnResource6" />
                                    <telerik:GridBoundColumn DataField="IsFromMobile" Visible="False" AllowFiltering="False"
                                        UniqueName="IsFromMobile" meta:resourcekey="GridBoundColumnResource7" />
                                    <telerik:GridBoundColumn DataField="EmployeeArabicName" Visible="False" AllowFiltering="False"
                                        UniqueName="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource8" />
                                    <telerik:GridBoundColumn DataField="ReasonArabicName" Visible="False" UniqueName="ReasonArabicName"
                                        AllowFiltering="False" meta:resourcekey="GridBoundColumnResource9" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="RejectionReason" HeaderText="Rejection Reason"
                                        meta:resourcekey="RejectionReasonColumnResource14" UniqueName="RejectionReason">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                            <asp:HiddenField ID="hdnReasonArabicName" runat="server" Value='<%# Eval("ReasonArabicName") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <CommandItemTemplate>
                                    <telerik:RadToolBar runat="server" ID="RadToolBar2" OnButtonClick="RadToolBar2_ButtonClick"
                                        Skin="Hay" meta:resourcekey="RadToolBar2Resource1">
                                        <Items>
                                            <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                ImagePosition="Right" runat="server" Owner="" meta:resourcekey="RadToolBarButton2Resource1" />
                                        </Items>
                                    </telerik:RadToolBar>
                                </CommandItemTemplate>
                            </MasterTableView><SelectedItemStyle ForeColor="Maroon" />
                        </telerik:RadGrid>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
