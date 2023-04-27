<%@ Control Language="VB" AutoEventWireup="false" CodeFile="HR_LeaveApproval.ascx.vb"
    Inherits="Requests_UserControls_HR_HR_LeaveApproval" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
    function ShowPopUpLeave(Mode) {
        var lang = '<%= MsgLang %>';
        if (Mode == 1) {
            if (lang == 'en') {
                return confirm('Are you sure you want to Accept Leave?');
            }
            else {
                return confirm('هل انت متأكد من قبول الاجازة؟');
            }
        }
        else if (Mode == 2) {
            if (lang == 'en') {
                return confirm('Are you sure you want to Reject Leave?');
            }
            else {
                return confirm('هل انت متأكد من رفض الاجازة؟');
            }
        }
    }

    function ResetPopUpLeave() {
        var txtRejectedReason = document.getElementById("<%= txtRejectedReason.ClientID %>");
        txtRejectedReason.value = '';
    }

    function CheckAllLeaves(id) {
        var masterTable = $find("<%= dgrdEmpLeaveRequest.ClientID%>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == true) {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("chk").checked = true; // for checking the checkboxes
                }
            }
            else {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("chk").checked = false; // for unchecking the checkboxes
                }
            }
        }
        function unCheckHeader(id) {
            var masterTable = $find("<%= dgrdEmpLeaveRequest.ClientID%>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == false) {
                var hidden = document.getElementById("HiddenField1");
                var checkBox = document.getElementById(hidden.value);
                checkBox.checked = false;
            }
        }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="updateprogressAssign">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div id="tblLoading" style="width: 100%; height: 100%; z-index: 100002 !important; text-align: center; position: fixed; left: 0px; top: 0px; background-size: cover; background-image: url('../images/Grey_fade.png');">
                        <asp:Image ID="imgLoading" runat="server" ImageAlign="Middle" ImageUrl="~/images/STS_Loading.gif" Width="250px" Height="250px" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblLeaveRequests" runat="server" Text="Leave Requests"
                    CssClass="Profiletitletxt" meta:resourcekey="lblLeaveRequestsResource1" />
                <asp:Label ID="lblRequestNo" runat="server" Text="Request Numbers" CssClass="Profiletitletxt"
                    meta:resourcekey="lblRequestNoResource1" />
            </div>
        </div>

        <div class="row" runat="server" id="divrdbProceed">
            <div class="col-md-4"></div>
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <asp:RadioButtonList ID="rdbProceed" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="true" meta:resourcekey="rdbProceedResource1">
                    <asp:ListItem Text="Accept All" Value="1" meta:resourcekey="ListItemResource10">
                    </asp:ListItem>
                    <asp:ListItem Text="Reject All" Value="2" meta:resourcekey="ListItemResource9">
                    </asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>

        <div class="row" runat="server" id="divRejectAllReason" visible="false">
            <div class="col-md-4"></div>
            <div class="col-md-4">
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtRejectAllReason" runat="server" ToolTip="Rejection Reason" TextMode="MultiLine" placeholder="Rejection Reason" meta:resourcekey="txtRejectAllReasonResource1"></asp:TextBox>
            </div>
        </div>
        <div class="row" runat="server" id="divbtnProceed">
            <div class="col-md-4"></div>
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <asp:Button ID="btnProceed" Text="Proceed All Selected" CssClass="button" runat="server"
                    meta:resourcekey="btnProceedResource1"></asp:Button>
            </div>
        </div>

        <asp:MultiView ID="mvLeaveApproval" runat="server">
            <asp:View ID="viewDMApproval" runat="server">
                <div class="table-responsive">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdEmpLeaveRequest"
                        Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="dgrdEmpLeaveRequest" runat="server" AllowSorting="True" AllowPaging="True"
                        GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        PageSize="10" ShowFooter="True" meta:resourcekey="dgrdEmpLeaveRequestResource1">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="false" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="false" />
                        </ClientSettings>
                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top" DataKeyNames="FromDate,ToDate,LeaveId,AttachedFile,LeaveTypeId,HasAdvancedSalary,FK_EmployeeId,Requestdate,Remarks,Days">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False"
                                    UniqueName="TemplateColumn">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat="server" onclick="CheckAllLeaves(this)" Text="&nbsp;" Visible="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Emp No"
                                    meta:resourcekey="GridBoundColumnResource1" UniqueName="EmployeeNo" />
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name"
                                    meta:resourcekey="GridBoundColumnResource2" UniqueName="EmployeeName" />
                                <telerik:GridBoundColumn DataField="RequestDate" DataFormatString="{0:dd/MM/yyyy}"
                                    HeaderText="Request Date" meta:resourcekey="GridBound1ColumnResource1"
                                    UniqueName="RequestDate" />
                                <telerik:GridBoundColumn DataField="LeaveName" HeaderText="Leave Type"
                                    meta:resourcekey="GridBound2ColumnResource1" UniqueName="LeaveName" />
                                <telerik:GridBoundColumn DataField="FromDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="From Date"
                                    meta:resourcekey="GridBound4ColumnResource1" UniqueName="FromDate" />
                                <telerik:GridBoundColumn DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="To Date"
                                    meta:resourcekey="GridBound5ColumnResource1" UniqueName="ToDate" />
                                <telerik:GridBoundColumn DataField="Days" HeaderText="No. Days"
                                    meta:resourcekey="GridBound6ColumnResource1" UniqueName="Days" />
                                <telerik:GridTemplateColumn HeaderText="With Advanced Salary" AllowFiltering="False"
                                    UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource10">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chbWithAdvancedSalary" runat="server" Enabled="False"
                                            Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="HasAdvancedSalary" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource13" UniqueName="HasAdvancedSalary" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="LeaveId"
                                    Visible="False" meta:resourcekey="GridBoundColumnResource3"
                                    UniqueName="LeaveId" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId"
                                    Visible="False" meta:resourcekey="GridBoundColumnResource4"
                                    UniqueName="FK_EmployeeId" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="LeaveTypeId"
                                    Visible="False" meta:resourcekey="GridBoundColumnResource5"
                                    UniqueName="LeaveTypeId" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="AttachedFile"
                                    Visible="False" meta:resourcekey="GridBoundColumnResource6"
                                    UniqueName="AttachedFile" />
                                <telerik:GridTemplateColumn HeaderText="Attached File" AllowFiltering="False" UniqueName="TemplateColumn"
                                    meta:resourcekey="GridTemplateColumnResource3">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="lnbView" runat="server" Text="View" meta:resourcekey="lnbViewResource1" OnClick="lnkDownloadFile_Click" />
                                        <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                        <asp:HiddenField ID="hdnLeaveTpeAr" runat="server" Value='<%# Eval("LeaveArabicName") %>' />
                                        <asp:HiddenField ID="hdnStatusNameAr" runat="server" Value='<%# Eval("StatusNameArabic") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks" meta:resourcekey="GridTemplateColumnResource9"
                                    UniqueName="Remarks" />
                                <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status"
                                    meta:resourcekey="GridBoundColumnResource6" UniqueName="StatusName" />
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn"
                                    meta:resourcekey="GridTemplateColumnResource2">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAccept" runat="server" Text="Accept" OnClick="lnkAccept_Click"
                                            OnClientClick="return ShowPopUpLeave('1')" meta:resourcekey="lnkAcceptResource1"></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn"
                                    meta:resourcekey="GridTemplateColumnResource4">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkReject" runat="server" Text="Reject" CommandName="reject"
                                            CommandArgument='<%# Eval("LeaveId") %>' meta:resourcekey="lnkRejectResource1"></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkLeaveForm" runat="server" Text="Leave Form" OnClick="lnkLeaveForm_Click"
                                            meta:resourcekey="lnkLeaveFormResource1"></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
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
                        </MasterTableView>
                        <SelectedItemStyle ForeColor="Maroon" />
                    </telerik:RadGrid>
                </div>
                <asp:HiddenField runat="server" ID="hdnWindow" />
                <cc1:ModalPopupExtender ID="mpeRejectPopupLeave" runat="server" BehaviorID="modelPopupExtenderLeave"
                    TargetControlID="hdnWindow" PopupControlID="divRejectedLeave" DropShadow="True" OnCancelScript="ResetPopUpLeave(); return false;"
                    CancelControlID="btnCancel" Enabled="True"
                    BackgroundCssClass="ModalBackground" DynamicServicePath="" />
            </asp:View>
        </asp:MultiView>
        <div id="divRejectedLeave" class="commonPopup" style="display: none">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblRejectedReason" runat="server" Text="Rejected Reason" CssClass="Profiletitletxt"
                        meta:resourcekey="lblRejectedReasonResource1" />
                </div>
                <div class="col-md-10">
                    <asp:TextBox ID="txtRejectedReason" runat="server" TextMode="MultiLine" Rows="4"
                        Columns="45" meta:resourcekey="txtRejectedReasonResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="button" meta:resourcekey="btnRejectResource1" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" meta:resourcekey="btnCancelResource1" />
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
