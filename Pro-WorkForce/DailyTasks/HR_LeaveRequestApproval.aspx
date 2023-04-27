<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="HR_LeaveRequestApproval.aspx.vb" Inherits="DailyTasks_HR_LeaveRequestApproval"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ShowPopUp(Mode) {
            var lang = '<%= MsgLang %>';
            if (Mode == 1) {
                if (lang == 'en') {
                    return confirm('Are you sure you want to accept leave?');
                }
                else {
                    return confirm('هل انت متأكد من قبول الاجازة؟');
                }
            }
            else if (Mode == 2) {
                if (lang == 'en') {
                    return confirm('Are you sure you want to reject leave?');
                }
                else {
                    return confirm('هل انت متأكد من رفض الاجازة؟');
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
        function ResetPopUpLeave() {
            var txtRejectedReason = document.getElementById("<%= txtRejectedReason.ClientID %>");
            txtRejectedReason.value = '';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"
        OnClientActiveTabChanged="hideValidatorCalloutTab" meta:resourcekey="TabContainer1Resource1">
        <cc1:TabPanel ID="tabRequests" runat="server" HeaderText="Leave Requests" TabIndex="0"
            meta:resourcekey="tabRequestsResource1">
            <ContentTemplate>
                <asp:MultiView ID="mvLeaveApproval" runat="server">
                    <asp:View ID="vLeaves" runat="server">
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="grdEmpLeaves"
                                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                                    <ContextMenu FeatureGroupID="rfContextMenu"></ContextMenu>
                                </telerik:RadFilter>



                                <telerik:RadGrid ID="grdEmpLeaves" runat="server" AllowSorting="True" AllowPaging="True"
                                    Width="100%" PageSize="25" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                    OnItemCommand="grdEmpLeaves_ItemCommand" ShowFooter="True" meta:resourcekey="grdEmpLeavesResource1" CellSpacing="0">
                                    <GroupingSettings CaseSensitive="False" />

                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>

                                    <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="RequestDate,FromDate,ToDate,LeaveRequestID">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" UniqueName="EmployeeNo"
                                                meta:resourcekey="GridBoundColumnResource1" FilterControlAltText="Filter EmployeeNo column" />
                                            <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                                                meta:resourcekey="GridBoundColumnResource2" FilterControlAltText="Filter EmployeeName column" />
                                            <telerik:GridBoundColumn DataField="RequestDate" HeaderText="Request Date" DataFormatString="{0:dd/MM/yyyy}"
                                                UniqueName="RequestDate" meta:resourcekey="GridBoundColumnResource3" FilterControlAltText="Filter RequestDate column" />
                                            <telerik:GridBoundColumn DataField="LeaveName" HeaderText="Leave Type" UniqueName="LeaveName"
                                                meta:resourcekey="GridBoundColumnResource4" FilterControlAltText="Filter LeaveName column" />
                                            <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}"
                                                UniqueName="FromDate" meta:resourcekey="GridBoundColumnResource5" FilterControlAltText="Filter FromDate column" />
                                            <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}"
                                                UniqueName="ToDate" meta:resourcekey="GridBoundColumnResource6" FilterControlAltText="Filter ToDate column" />
                                            <telerik:GridBoundColumn DataField="Days" HeaderText="No. Days" UniqueName="Days"
                                                meta:resourcekey="GridBoundColumnResource7" FilterControlAltText="Filter Days column" />
                                            <telerik:GridBoundColumn DataField="LeaveRequestID" AllowFiltering="False" Visible="False"
                                                UniqueName="LeaveRequestID" meta:resourcekey="GridBoundColumnResource8" FilterControlAltText="Filter LeaveRequestID column" />
                                            <telerik:GridBoundColumn DataField="FK_EmployeeId" AllowFiltering="False" Visible="False"
                                                UniqueName="FK_EmployeeId" meta:resourcekey="GridBoundColumnResource9" FilterControlAltText="Filter FK_EmployeeId column" />
                                            <telerik:GridBoundColumn DataField="UserID" HeaderText="Created By" UniqueName="UserID"
                                                meta:resourcekey="GridBoundColumnResource10" FilterControlAltText="Filter UserID column" />
                                            <telerik:GridBoundColumn DataField="CREATED_DATE" HeaderText="Created Date" UniqueName="CREATED_DATE"
                                                meta:resourcekey="GridBoundColumnResource11" FilterControlAltText="Filter CREATED_DATE column" />
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
                                                        OnClick="lnkReject_Click" OnClientClick="return ShowPopUp('2')" CommandArgument='<%# Eval("LeaveRequestID") %>'
                                                        meta:resourcekey="lnkRejectResource1">
                                                    </asp:LinkButton>
                                                    <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                                    <asp:HiddenField ID="hdnLeaveArabicType" runat="server" Value='<%# Eval("LeaveArabicName") %>' />



                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>

                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar ID="RadToolBar1" runat="server" meta:resourcekey="RadToolBar1Resource1"
                                                OnButtonClick="RadToolBar1_ButtonClick" Skin="Hay">
                                                <Items>
                                                    <telerik:RadToolBarButton runat="server" CausesValidation="False" CommandName="FilterRadGrid"
                                                        ImagePosition="Right" ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource1"
                                                        Owner="" Text="Apply filter">
                                                    </telerik:RadToolBarButton>
                                                </Items>
                                            </telerik:RadToolBar>



                                        </CommandItemTemplate>
                                    </MasterTableView>

                                    <SelectedItemStyle ForeColor="Maroon" />
                                </telerik:RadGrid>



                                <asp:HiddenField runat="server" ID="hdnWindow" />



                                <cc1:ModalPopupExtender ID="mpeRejectPopupLeave" runat="server" BehaviorID="modelPopupExtenderLeave"
                                    TargetControlID="hdnWindow" PopupControlID="divRejectedLeave" DropShadow="True" OnCancelScript="ResetPopUpLeave(); return false;"
                                    CancelControlID="btnCancelPOP" Enabled="True"
                                    BackgroundCssClass="ModalBackground" DynamicServicePath="" />



                            </div>
                        </div>
                    </asp:View>
                    <asp:View ID="vLeaveDetails" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <uc2:PageFilter ID="EmployeeFilterUC" runat="server" HeaderText="Employee Filter"
                                    OneventEmployeeSelect="FillEmpLeaveGrid" ValidationGroup="empleave" />



                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="Label4" runat="server" Text="Leave Type"
                                    meta:resourcekey="Label4Resource1" />



                                <asp:HiddenField ID="hdnLeaveSufficient" runat="server" />



                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="ddlLeaveType" runat="server" AppendDataBoundItems="True"
                                    Width="200px" MarkFirstMatch="True" AutoPostBack="True" CausesValidation="False"
                                    Skin="Vista" meta:resourcekey="ddlLeaveTypeResource1" />



                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlLeaveType"
                                    Display="None" ErrorMessage="Please Select Leave Type" InitialValue="-1" ValidationGroup="empleave"
                                    meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>



                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="RequiredFieldValidator4" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>



                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="Label5" runat="server" Text="Request Date"
                                    meta:resourcekey="Label5Resource1"></asp:Label>



                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpRequestDate" runat="server" AllowCustomText="false"
                                    ShowPopupOnFocus="True" MarkFirstMatch="true" PopupDirection="TopRight" Skin="Vista"
                                    Culture="en-US" meta:resourcekey="dtpRequestDateResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True"></Calendar>

                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        Width="" LabelWidth="64px">
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



                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dtpRequestDate"
                                    Display="None" ErrorMessage="Please Enter Request Date" ValidationGroup="empleave"
                                    meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>



                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="RequiredFieldValidator5" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>



                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="Label7" runat="server" Text="Leave From"
                                    meta:resourcekey="Label7Resource1"></asp:Label>



                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpFromDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                    PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US"
                                    meta:resourcekey="dtpFromDateResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True"></Calendar>

                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        Width="" LabelWidth="64px">
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



                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtpFromDate"
                                    Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="empleave"
                                    meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>



                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="RequiredFieldValidator6" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>




                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="Label6" runat="server" Text="To" meta:resourcekey="Label6Resource1"></asp:Label>



                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpToDate" runat="server" AllowCustomText="false"
                                    MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                                    Culture="en-US" meta:resourcekey="dtpToDateResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True"></Calendar>

                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        Width="" LabelWidth="64px">
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



                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="dtpToDate"
                                    Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="empleave"
                                    meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>



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
                                <asp:Label ID="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblAttachFileResource1" />



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
                                    <asp:LinkButton ID="lnbLeaveFile" runat="server" Visible="False" OnClick="lnkDownloadFile_Click" Text="View" meta:resourcekey="lblViewResource1" />

                                    <asp:LinkButton ID="lnbRemove" runat="server" Text="Remove" Visible="False" meta:resourcekey="lnbRemoveResource1" />

                                    <asp:Label ID="lblNoAttachedFile" runat="server" Text="No Attached File" Visible="False"
                                        meta:resourcekey="lblNoAttachedFileResource1" />



                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="Label8" runat="server" Text="Remarks" meta:resourcekey="Label8Resource1"></asp:Label>



                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="320px" Height="60px"
                                    meta:resourcekey="txtRemarksResource1"></asp:TextBox>



                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-8 text-center">
                                <asp:Button ID="btnAccept" runat="server" Text="Accept" CssClass="button" meta:resourcekey="btnAcceptResource1" />



                                <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="button" CausesValidation="False"
                                    meta:resourcekey="btnRejectResource1" />



                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" CausesValidation="False"
                                    meta:resourcekey="btnCancelResource1" />



                            </div>
                        </div>
                    </asp:View>
                </asp:MultiView>



                <div id="divRejectedLeave" class="commonPopup" style="display: none">

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblRejectedReason" runat="server" Text="Rejected Reason" CssClass="Profiletitletxt"
                                meta:resourcekey="lblRejectedReasonResource1" />



                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtRejectedReason" runat="server" TextMode="MultiLine" Height="60px"
                                Width="320px" meta:resourcekey="txtRejectedReasonResource1" />



                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
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
                        <telerik:RadFilter runat="server" ID="RadFilter2" FilterContainerID="dgrdRejected"
                            Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                        <telerik:RadGrid ID="dgrdRejected" runat="server" AllowSorting="True" AllowPaging="True"
                            Width="100%" PageSize="25" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                            ShowFooter="True" meta:resourcekey="grdEmpLeavesResource1">
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="RequestDate,FromDate,ToDate">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" UniqueName="EmployeeNo"
                                        meta:resourcekey="GridBoundColumnResource1" />
                                    <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                                        meta:resourcekey="GridBoundColumnResource2" />
                                    <telerik:GridBoundColumn DataField="RequestDate" HeaderText="Request Date" DataFormatString="{0:dd/MM/yyyy}"
                                        UniqueName="RequestDate" meta:resourcekey="GridBoundColumnResource3" />
                                    <telerik:GridBoundColumn DataField="LeaveName" HeaderText="Leave Type" UniqueName="LeaveName"
                                        meta:resourcekey="GridBoundColumnResource4" />
                                    <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}"
                                        UniqueName="FromDate" meta:resourcekey="GridBoundColumnResource5" />
                                    <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}"
                                        UniqueName="ToDate" meta:resourcekey="GridBoundColumnResource6" />
                                    <telerik:GridBoundColumn DataField="Days" HeaderText="No. Days" UniqueName="Days"
                                        meta:resourcekey="GridBoundColumnResource7" />
                                    <telerik:GridBoundColumn DataField="LeaveRequestID" AllowFiltering="False" Visible="False"
                                        UniqueName="LeaveRequestID" meta:resourcekey="GridBoundColumnResource8" />
                                    <telerik:GridBoundColumn DataField="FK_EmployeeId" AllowFiltering="False" Visible="False"
                                        UniqueName="FK_EmployeeId" meta:resourcekey="GridBoundColumnResource9" />
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="RejectionReason" HeaderText="Rejection Reason"
                                        meta:resourcekey="RejectionReasonColumnResource14" UniqueName="RejectionReason">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                            <asp:HiddenField ID="hdnLeaveArabicType" runat="server" Value='<%# Eval("LeaveArabicName") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                <CommandItemTemplate>
                                    <telerik:RadToolBar ID="RadToolBar2" runat="server" meta:resourcekey="RadToolBar1Resource1"
                                        OnButtonClick="RadToolBar2_ButtonClick" Skin="Hay">
                                        <Items>
                                            <telerik:RadToolBarButton runat="server" CausesValidation="False" CommandName="FilterRadGrid"
                                                ImagePosition="Right" ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource1"
                                                Owner="" Text="Apply filter">
                                            </telerik:RadToolBarButton>
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
