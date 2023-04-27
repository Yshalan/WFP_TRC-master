<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DM_NursingPermissionApproval.ascx.vb"
    Inherits="Requests_UserControls_DM_DM_NursingPermissionApproval" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
    function ShowPopUpNursing(Mode) {
        var lang = '<%= MsgLang %>';
        if (Mode == 1) {
            if (lang == 'en') {
                return confirm('Are you sure you want to accept permission?');
            }
            else {
                return confirm('هل انت متأكد من قبول المغادرة؟');
            }
        }
        else if (Mode == 2) {
            if (lang == 'en') {
                return confirm('Are you sure you want to reject permission?');
            }
            else {
                return confirm('هل انت متأكد من رفض المغادرة؟');
            }
        }
    }

    function ResetPopUpNursing() {
        var txtRejectedReason = document.getElementById("<%= txtRejectedReason.ClientID %>");
        txtRejectedReason.value = '';
    }

    function CheckAllNursing(id) {
        var masterTable = $find("<%= dgrdEmpNursingRequest.ClientID%>").get_masterTableView();
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
        var masterTable = $find("<%= dgrdEmpNursingRequest.ClientID%>").get_masterTableView();
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
                <asp:Label ID="lblNursingPermissionRequests" runat="server" Text="Nursing Permission Requests" CssClass="Profiletitletxt" meta:resourcekey="lblNursingPermissionRequestsResource1" />
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

        <asp:MultiView ID="mvPermissionApproval" runat="server">
            <asp:View ID="viewDMApproval" runat="server">
                <div class="table-responsive">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdEmpNursingRequest"
                        Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="dgrdEmpNursingRequest" runat="server" AllowSorting="True" AllowPaging="True"
                        GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        PageSize="10" ShowFooter="True" meta:resourcekey="dgrdEmpLeaveRequestResource1">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="false" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="false" />
                        </ClientSettings>
                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="PermDate,PermEndDate,PermissionRequestId,AttachedFile,IsForPeriod,FK_EmployeeId,IsFlexible,FlexibilePermissionDuration,AllowedTime,Remark,IsFullDay">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False"
                                    UniqueName="TemplateColumn">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat="server" onclick="CheckAllNursing(this)" Text="&nbsp;" Visible="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Emp No" UniqueName="EmployeeNo"
                                    meta:resourcekey="GridBoundColumnResource1" />
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                                    meta:resourcekey="GridBoundColumnResource2" />
                                <telerik:GridBoundColumn DataField="PermName" HeaderText="Permission Type" UniqueName="PermName"
                                    Visible="False" meta:resourcekey="GridBoundColumnResource3" />
                                <telerik:GridBoundColumn DataField="PermDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Start Date"
                                    UniqueName="PermDate" meta:resourcekey="GridBoundColumnResource4" />
                                <telerik:GridBoundColumn DataField="PermEndDate" DataFormatString="{0:dd/MM/yyyy}"
                                    HeaderText="End Date" UniqueName="PermEndDate" meta:resourcekey="GridBoundColumnResource5" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="PermissionRequestId" Visible="False"
                                    UniqueName="PermissionRequestId" meta:resourcekey="GridBoundColumnResource6" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="AllowedTime" Visible="False"
                                    UniqueName="AllowedTime" meta:resourcekey="GridBoundColumnResource7" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" Visible="False"
                                    UniqueName="FK_EmployeeId" meta:resourcekey="GridBoundColumnResource7" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="Remarks" Visible="False"
                                    UniqueName="Remarks" meta:resourcekey="GridBoundColumnResource8" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="IsForPeriod" Visible="False"
                                    UniqueName="IsForPeriod" meta:resourcekey="GridBoundColumnResource9" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="Days" Visible="False"
                                    UniqueName="Days" meta:resourcekey="GridBoundColumnResource10" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="PermissionOption" Visible="False"
                                    UniqueName="PermissionOption" meta:resourcekey="GridBoundColumnResource11" />
                                <telerik:GridBoundColumn DataField="IsFlexible" HeaderText="Flexible" UniqueName="IsFlexible"
                                    meta:resourcekey="GridBoundColumnResource12" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFullDay" Visible="False"
                                    UniqueName="IsFullDay" meta:resourcekey="GridBoundColumnResource13" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="FlexibilePermissionDuration"
                                    Visible="False" UniqueName="FlexibilePermissionDuration" meta:resourcekey="GridBoundColumnResource14" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="AttachedFile" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource15" UniqueName="AttachedFile" />
                                <telerik:GridTemplateColumn HeaderText="Attached File" AllowFiltering="False" UniqueName="TemplateColumn"
                                    meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnbView" runat="server" Text="View" meta:resourcekey="lblViewResource1" OnClick="lnkDownloadFile_Click" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Remark" HeaderText="Remarks" UniqueName="Remark"
                                    meta:resourcekey="GridBoundColumnResource16" />
                                <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status" UniqueName="StatusName"
                                    meta:resourcekey="GridBoundColumnResource17" />
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAccept" runat="server" Text="Accept" OnClick="lnkAccept_Click"
                                            OnClientClick="return ShowPopUpNursing('1')" meta:resourcekey="lnkAcceptResource1"></asp:LinkButton>
                                        <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                        <asp:HiddenField ID="hdnStatusNameAr" runat="server" Value='<%# Eval("StatusNameArabic") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource3">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkReject" runat="server" Text="Reject" CommandName="reject"
                                            CommandArgument='<%# Eval("PermissionRequestId") %>' meta:resourcekey="lnkRejectResource1"></asp:LinkButton>
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
                <cc1:ModalPopupExtender ID="mpeRejectPopupNursing" runat="server" BehaviorID="modelPopupExtenderNursing"
                    TargetControlID="hdnWindow" PopupControlID="divRejectedNursing" DropShadow="True" OnCancelScript="ResetPopUpNursing(); return false;"
                    CancelControlID="btnCancel" Enabled="True" BackgroundCssClass="ModalBackground"
                    DynamicServicePath="" />
            </asp:View>
        </asp:MultiView>
        <div id="divRejectedNursing" class="commonPopup" style="display: none">
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
