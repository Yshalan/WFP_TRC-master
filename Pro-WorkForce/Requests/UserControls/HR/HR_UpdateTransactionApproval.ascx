<%@ Control Language="VB" AutoEventWireup="false" CodeFile="HR_UpdateTransactionApproval.ascx.vb" Inherits="Requests_UserControls_HR_HR_UpdateTransactionApproval" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
    function ShowPopUpUpdate(Mode) {
        var lang = '<%= MsgLang %>';
        if (Mode == 1) {
            if (lang == 'en') {
                return confirm('Are you sure you want to Accept Update Transaction Request?');
            }
            else {
                return confirm('هل انت متأكد من قبول طلب تحديث الحركة؟');
            }
        }
        else if (Mode == 2) {
            if (lang == 'en') {
                return confirm('Are you sure you want to Reject Update Transaction Request?');
            }
            else {
                return confirm('هل انت متأكد من قبول طلب تحديث الحركة؟');
            }
        }
    }

    function ResetPopUpUpdate() {
        var txtRejectedReason = document.getElementById("<%= txtRejectedReason.ClientID %>");
        txtRejectedReason.value = '';
    }

    function CheckAllUpdate(id) {
        var masterTable = $find("<%= dgrdUpdateTransactionRequest.ClientID%>").get_masterTableView();
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
        var masterTable = $find("<%= dgrdUpdateTransactionRequest.ClientID%>").get_masterTableView();
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
                        <asp:Image ID="imgLoading" runat="server" ImageAlign="Middle" ImageUrl="~/images/STS_Loading.gif" Width="250px" Height="250px" meta:resourcekey="imgLoadingResource1" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblManualEntryRequests" runat="server" Text="Update Transaction Requests" CssClass="Profiletitletxt" meta:resourcekey="lblManualEntryRequestsResource1" />
                <asp:Label ID="lblRequestNo" runat="server" Text="Requests Number"
                    CssClass="Profiletitletxt" meta:resourcekey="lblRequestNoResource1" />
            </div>
        </div>
        <div class="row" runat="server" id="divrdbProceed">
            <div class="col-md-4"></div>
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <asp:RadioButtonList ID="rdbProceed" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="True" meta:resourcekey="rdbProceedResource1">
                    <asp:ListItem Text="Accept All" Value="1" meta:resourcekey="ListItemResource1"></asp:ListItem>
                    <asp:ListItem Text="Reject All" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="row" runat="server" id="divRejectAllReason" visible="False">
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
                <asp:Button ID="btnProceed" Text="Proceed Selected" CssClass="button" runat="server" meta:resourcekey="btnProceedResource1"></asp:Button>
            </div>
        </div>
        <div class="table-responsive">
            <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdUpdateTransactionRequest"
                Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                <ContextMenu FeatureGroupID="rfContextMenu">
                </ContextMenu>
            </telerik:RadFilter>
            <telerik:RadGrid ID="dgrdUpdateTransactionRequest" runat="server"
                AllowSorting="True" AllowPaging="True"
                GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdManualEntryRequestResource1">
                <GroupingSettings CaseSensitive="False" />
                <ClientSettings EnableRowHoverStyle="True">
                </ClientSettings>
                <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False"
                    DataKeyNames="MoveRequestId,AttachedFile,MoveDate,MoveTime,FK_EmployeeId,Remarks,MoveId,UpdateTransactionType">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="False"
                            UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1">
                            <HeaderTemplate>
                                <asp:CheckBox ID="checkAll" runat="server" onclick="CheckAllUpdate(this)" Text="&nbsp;" meta:resourcekey="checkAllResource1" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Emp No"
                            UniqueName="EmployeeNo" FilterControlAltText="Filter EmployeeNo column" meta:resourcekey="GridBoundColumnResource1" />
                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name"
                            UniqueName="EmployeeName" FilterControlAltText="Filter EmployeeName column" meta:resourcekey="GridBoundColumnResource2" />
                        <telerik:GridBoundColumn DataField="MoveDate" DataFormatString="{0:dd/MM/yyyy}"
                            HeaderText="Date" UniqueName="MoveDate" FilterControlAltText="Filter MoveDate column" meta:resourcekey="GridBoundColumnResource3" />
                        <telerik:GridBoundColumn DataField="MoveTime" HeaderText="Time"
                            UniqueName="MoveTime" DataFormatString="{0:HH:mm}" FilterControlAltText="Filter MoveTime column" meta:resourcekey="GridBoundColumnResource4" />
                        <telerik:GridBoundColumn DataField="ReasonName" HeaderText="Reason"
                            UniqueName="ReasonName" FilterControlAltText="Filter ReasonName column" meta:resourcekey="GridBoundColumnResource5" />

                        <telerik:GridBoundColumn AllowFiltering="False" DataField="MoveRequestId" Visible="False"
                            UniqueName="MoveRequestId" FilterControlAltText="Filter MoveRequestId column" meta:resourcekey="GridBoundColumnResource6" />
                        <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" Visible="False"
                            UniqueName="FK_EmployeeId" FilterControlAltText="Filter FK_EmployeeId column" meta:resourcekey="GridBoundColumnResource7" />

                         <telerik:GridBoundColumn AllowFiltering="False" DataField="UpdateTransactionType" Visible="False"
                            UniqueName="UpdateTransactionType" FilterControlAltText="Filter UpdateTransactionType column" meta:resourcekey="GridBoundColumnResource12" />

                        <telerik:GridBoundColumn AllowFiltering="False"
                            DataField="StatusId" Visible="False"
                            UniqueName="StatusId" FilterControlAltText="Filter StatusId column" meta:resourcekey="GridBoundColumnResource8" />
                        <telerik:GridBoundColumn AllowFiltering="False" DataField="AttachedFile"
                            Visible="False"
                            UniqueName="AttachedFile" FilterControlAltText="Filter AttachedFile column" meta:resourcekey="GridBoundColumnResource9" />
                        <telerik:GridTemplateColumn HeaderText="Attached File" AllowFiltering="False"
                            UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource2">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnbView" runat="server" Text="View" OnClick="lnkDownloadFile_Click" meta:resourcekey="lnbViewResource1" />

                                <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                <asp:HiddenField ID="hdnReasonAr" runat="server" Value='<%# Eval("ReasonArabicName") %>' />
                                <asp:HiddenField ID="hdnStatusNameAr" runat="server" Value='<%# Eval("StatusNameArabic") %>' />

                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks"
                            UniqueName="Remarks" FilterControlAltText="Filter Remarks column" meta:resourcekey="GridBoundColumnResource10" />

                        <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status"
                            UniqueName="StatusName" FilterControlAltText="Filter StatusName column" meta:resourcekey="GridBoundColumnResource11" />
                        <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource3">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkAccept" runat="server" Text="Accept" OnClick="lnkAccept_Click"
                                    OnClientClick="return ShowPopUpUpdate('1')" meta:resourcekey="lnkAcceptResource1"></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource4">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkReject" runat="server" Text="Reject" CommandName="reject"
                                    CommandArgument='<%# Eval("MoveRequestId") %>' meta:resourcekey="lnkRejectResource1"></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>

                    </Columns>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    <CommandItemTemplate>
                        <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                            Skin="Hay" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                            <Items>
                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                    ImagePosition="Right" runat="server"
                                    Owner="" meta:resourcekey="RadToolBarButtonResource1" />
                            </Items>
                        </telerik:RadToolBar>
                    </CommandItemTemplate>
                </MasterTableView>
                <SelectedItemStyle ForeColor="Maroon" />
            </telerik:RadGrid>
        </div>
        <asp:HiddenField runat="server" ID="hdnWindow" />
        <cc1:ModalPopupExtender ID="mpeRejectPopupManual" runat="server" 
            TargetControlID="hdnWindow" PopupControlID="divRejectedUpdate"
            DropShadow="True" OnCancelScript="ResetPopUpUpdate(); return false;"
            CancelControlID="btnCancel" Enabled="True"
            BackgroundCssClass="ModalBackground" DynamicServicePath="" />
        <div id="divRejectedUpdate" class="commonPopup" style="display: none">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblRejectedReason" runat="server" Text="Rejected Reason" CssClass="Profiletitletxt" meta:resourcekey="lblRejectedReasonResource1" />
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
