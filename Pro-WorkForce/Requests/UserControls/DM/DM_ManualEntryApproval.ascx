<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DM_ManualEntryApproval.ascx.vb"
    Inherits="Requests_UserControls_DM_DM_ManualEntryApproval" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript" language="javascript">
    function ShowPopUpManual(Mode) {
        var lang = '<%= MsgLang %>';
        if (Mode == 1) {
            if (lang == 'en') {
                return confirm('Are you sure you want to accept Manual entry request?');
            }
            else {
                return confirm('هل انت متأكد من قبول طلب الادخال اليدوي؟');
            }
        }
        else if (Mode == 2) {
            if (lang == 'en') {
                return confirm('Are you sure you want to reject Manual entry request?');
            }
            else {
                return confirm('هل انت متأكد من رفض طلب الادخال اليدوي؟');
            }
        }
    }

    function ResetPopUpManual() {
        var txtRejectedReason = document.getElementById("<%= txtRejectedReason.ClientID %>");
        txtRejectedReason.value = '';
    }

    function CheckMovesAll(id) {
        var masterTable = $find("<%= dgrdManualEntryRequest.ClientID%>").get_masterTableView();
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
        var masterTable = $find("<%= dgrdManualEntryRequest.ClientID%>").get_masterTableView();
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
                <asp:Label ID="lblManualEntryRequests" runat="server" Text="Manual Entry Requests" CssClass="Profiletitletxt" meta:resourcekey="lblManualEntryRequestsResource1" />
                <asp:Label ID="lblRequestNo" runat="server" Text="Requests Number" CssClass="Profiletitletxt"
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
                <asp:Button ID="btnProceed" Text="Proceed Selected" CssClass="button" runat="server"
                    meta:resourcekey="btnProceedResource1"></asp:Button>
            </div>
        </div>
        <div class="table-responsive">
            <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdManualEntryRequest"
                Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
            <telerik:RadGrid ID="dgrdManualEntryRequest" runat="server" AllowSorting="True" AllowPaging="True"
                GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                PageSize="10" ShowFooter="True" meta:resourcekey="dgrdManualEntryRequestResource1">
                <GroupingSettings CaseSensitive="False" />
                <ClientSettings EnablePostBackOnRowClick="false" EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="MoveRequestId,AttachedFile,MoveDate,MoveTime,FK_EmployeeId,NextApprovalStatus,Remarks">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="False"
                            UniqueName="TemplateColumn">
                            <HeaderTemplate>
                                <asp:CheckBox ID="checkAll" runat="server" onclick="CheckMovesAll(this)" Text="&nbsp;" Visible="true" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Emp No" UniqueName="EmployeeNo"
                            meta:resourcekey="GridBoundColumnResource1" />
                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                            meta:resourcekey="GridBoundColumnResource2" />
                        <telerik:GridBoundColumn DataField="MoveDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date"
                            UniqueName="MoveDate" meta:resourcekey="GridBoundColumnResource3" />
                        <telerik:GridBoundColumn DataField="MoveTime" HeaderText="Time" UniqueName="MoveTime"
                            DataFormatString="{0:HH:mm}" meta:resourcekey="GridBoundColumnResource4" />
                        <telerik:GridBoundColumn DataField="ReasonName" HeaderText="Reason" UniqueName="ReasonName"
                            meta:resourcekey="GridBoundColumnResource5" />
                        <telerik:GridBoundColumn AllowFiltering="False" DataField="MoveRequestId" Visible="False"
                            UniqueName="MoveRequestId" meta:resourcekey="GridBoundColumnResource6" />
                        <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" Visible="False"
                            UniqueName="FK_EmployeeId" meta:resourcekey="GridBoundColumnResource7" />
                        <telerik:GridBoundColumn AllowFiltering="False" DataField="StatusId" Visible="False"
                            UniqueName="StatusId" meta:resourcekey="GridBoundColumnResource8" />
                        <telerik:GridBoundColumn AllowFiltering="False" DataField="AttachedFile" Visible="False"
                            meta:resourcekey="GridBoundColumnResource9" UniqueName="AttachedFile" />
                        <telerik:GridBoundColumn AllowFiltering="False" DataField="NextApprovalStatus" Visible="False"
                            UniqueName="NextApprovalStatus"
                            meta:resourcekey="GridBoundColumnResource10" />
                        <telerik:GridTemplateColumn HeaderText="Attached File" AllowFiltering="False" UniqueName="TemplateColumn"
                            meta:resourcekey="GridTemplateColumnResource1">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnbView" runat="server" Text="View" meta:resourcekey="lblViewResource1" OnClick="lnkDownloadFile_Click" />
                                <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                <asp:HiddenField ID="hdnReasonAr" runat="server" Value='<%# Eval("ReasonArabicName") %>' />
                                <asp:HiddenField ID="hdnStatusNameAr" runat="server" Value='<%# Eval("StatusNameArabic") %>' />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks" UniqueName="Remarks"
                            meta:resourcekey="GridBoundColumnResource10" />
                        <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status" UniqueName="StatusName"
                            meta:resourcekey="GridBoundColumnResource11" />
                        <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkAccept" runat="server" Text="Accept" OnClick="lnkAccept_Click"
                                    OnClientClick="return ShowPopUpManual('1')" meta:resourcekey="lnkAcceptResource1"></asp:LinkButton>

                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource3">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkReject" runat="server" Text="Reject" CommandName="reject"
                                    CommandArgument='<%# Eval("MoveRequestId") %>' meta:resourcekey="lnkRejectResource1"></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    <CommandItemTemplate>
                        <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                            Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                            <Items>
                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                    ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1"
                                    Owner="" />
                            </Items>
                        </telerik:RadToolBar>
                    </CommandItemTemplate>
                </MasterTableView>
                <SelectedItemStyle ForeColor="Maroon" />
            </telerik:RadGrid>
        </div>


        <asp:HiddenField runat="server" ID="hdnWindow" />
        <cc1:ModalPopupExtender ID="mpeRejectPopupManual" runat="server" BehaviorID="modelPopupExtenderManual"
            TargetControlID="hdnWindow" PopupControlID="divRejectedManual" DropShadow="True" OnCancelScript="ResetPopUpManual(); return false;"
            CancelControlID="btnCancel" Enabled="True" BackgroundCssClass="ModalBackground"
            DynamicServicePath="" />

        <div id="divRejectedManual" class="commonPopup" style="display: none">
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
