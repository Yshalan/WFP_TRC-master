<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DM_PermissionApproval.ascx.vb"
    Inherits="Requests_UserControls_DM_PermissionApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript" language="javascript">

    function CheckAllPermissions(id) {
        var masterTable = $find("<%= dgrdEmpPermissionRequest.ClientID%>").get_masterTableView();
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



    function ShowPopUp(Mode) {
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

    function ResetPopUpPermission() {
        var txtRejectedReason = document.getElementById("<%= txtRejectedReason.ClientID %>");
        txtRejectedReason.value = '';
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
        <%--                <div class="updateprogressAssign">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" EnableViewState="false" DisplayAfter="0">
                <ProgressTemplate>
                    <div id="tblLoading" style="width: 100%; height: 100%; z-index: 100002 !important; text-align: center; position: fixed; left: 0px; top: 0px; background-size: cover; background-image: url('../images/Grey_fade.png');">

                        <div class="animategif">
                            <div align="center">
                                background-image: url('../images/STS_Loading.gif');
        <asp:Image ID="imgLoading" runat="server" ImageAlign="AbsBottom" ImageUrl="~/images/STS_Loading.gif" Width="250px" Height="250px" />
        </div>
                        </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>--%>
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblPermissionRequests" runat="server" Text="Permission Requests" CssClass="Profiletitletxt" meta:resourcekey="lblPermissionRequestsResource1" />
                <asp:Label ID="lblRequestNo" runat="server" Text="Requests Number" CssClass="Profiletitletxt" meta:resourcekey="lblRequestNoResource1" />
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
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdEmpPermissionRequest"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="dgrdEmpPermissionRequest" runat="server" AllowSorting="True" AllowPaging="True"
                        GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="true"
                        PageSize="10" ShowFooter="True" meta:resourcekey="dgrdEmpPermissionRequestResource1">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="false" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="false" />
                        </ClientSettings>
                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="PermDate,PermEndDate,PermissionId,AttachedFile,IsForPeriod,PermTypeId,FK_EmployeeId,FromTime,ToTime,TotalTime,IsFullDay,IsFlexible,FlexibilePermissionDuration,Days,Remark,NextApprovalStatus">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>

                                <telerik:GridTemplateColumn AllowFiltering="False"
                                    UniqueName="TemplateColumn">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat="server" onclick="CheckAllPermissions(this);" Text="&nbsp;" Visible="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Emp No" meta:resourcekey="GridBoundColumnResource1"
                                    UniqueName="EmployeeNo" />
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" meta:resourcekey="GridBoundColumnResource2"
                                    UniqueName="EmployeeName" />
                                <telerik:GridBoundColumn DataField="PermName" HeaderText="Permission Type" meta:resourcekey="GridBound2ColumnResource1"
                                    UniqueName="PermName" />
                                <telerik:GridBoundColumn DataField="PermDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Start Date"
                                    meta:resourcekey="GridBoundColumnResource3" UniqueName="PermDate" />
                                <telerik:GridBoundColumn DataField="PermEndDate" DataFormatString="{0:dd/MM/yyyy}"
                                    HeaderText="End Date" meta:resourcekey="GridBoundColumnResource4" UniqueName="PermEndDate" />
                                <telerik:GridBoundColumn DataField="FromTime" DataFormatString="{0:HH:mm}" HeaderText="From Time"
                                    meta:resourcekey="GridBoundColumnResource5" UniqueName="FromTime" />
                                <telerik:GridBoundColumn DataField="ToTime" DataFormatString="{0:HH:mm}" HeaderText="To Time"
                                    meta:resourcekey="GridBoundColumnResource6" UniqueName="ToTime" />
                                <telerik:GridBoundColumn DataField="TotalTime" HeaderText="Total Time" DataFormatString="{0:N3}"
                                    meta:resourcekey="GridBoundColumnResource16" UniqueName="TotalTime" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="PermissionId" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource7" UniqueName="PermissionId" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource8" UniqueName="FK_EmployeeId" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="PermTypeId" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource9" UniqueName="PermTypeId" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="Remarks" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource10" UniqueName="Remarks" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="IsForPeriod" Visible="False"
                                    UniqueName="IsForPeriod" meta:resourcekey="GridBoundColumnResource11" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="Days" Visible="False"
                                    UniqueName="Days" meta:resourcekey="GridBoundColumnResource12" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="PermissionOption" Visible="False"
                                    UniqueName="PermissionOption"
                                    meta:resourcekey="GridBoundColumnResource13" />
                                <telerik:GridBoundColumn DataField="IsFlexible" HeaderText="Flexible" meta:resourcekey="GridBoundColumnResource14"
                                    UniqueName="IsFlexible" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFullDay" Visible="False"
                                    UniqueName="IsFullDay" meta:resourcekey="GridBoundColumnResource15" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="FlexibilePermissionDuration"
                                    Visible="False" UniqueName="FlexibilePermissionDuration"
                                    meta:resourcekey="GridBoundColumnResource17" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="AttachedFile"
                                    Visible="False" meta:resourcekey="GridBoundColumnResource18"
                                    UniqueName="AttachedFile" />
                                <telerik:GridTemplateColumn HeaderText="Attached File" AllowFiltering="False" UniqueName="TemplateColumn"
                                    meta:resourcekey="GridTemplateColumnResource3">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnbView" runat="server" Text="View" meta:resourcekey="lnbViewResource1" OnClick="lnkDownloadFile_Click" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Remaining Balance" AllowFiltering="False"
                                    UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource13">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemainingBalance" runat="server"
                                            meta:resourcekey="lblRemainingBalanceResource1" />
                                        <asp:HiddenField ID="hdnFromTime" runat="server" Value='<%# Eval("FromTime") %>' />
                                        <asp:HiddenField ID="hdnToTime" runat="server" Value='<%# Eval("ToTime") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Remark" HeaderText="Remarks" meta:resourcekey="GridTemplateColumnResource12"
                                    UniqueName="Remark" />
                                <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status" meta:resourcekey="GridBoundColumnResource11"
                                    UniqueName="StatusName" />
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAccept" runat="server" Text="Accept" OnClick="lnkAccept_Click"
                                            OnClientClick="return ShowPopUp('1')" meta:resourcekey="lnkAcceptResource1"></asp:LinkButton>
                                        <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                        <asp:HiddenField ID="hdnPermissionTypeAr" runat="server" Value='<%# Eval("PermArabicName") %>' />
                                        <asp:HiddenField ID="hdnStatusNameAr" runat="server" Value='<%# Eval("StatusNameArabic") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkReject" runat="server" Text="Reject" CommandName="reject"
                                            CommandArgument='<%# Eval("PermissionId") %>' meta:resourcekey="lnkRejectResource1"></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="NextApprovalStatus" Display="false"
                                    UniqueName="NextApprovalStatus" />
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
                <cc1:ModalPopupExtender ID="mpeRejectPopupPermission" runat="server" BehaviorID="modelPopupExtenderPermission"
                    TargetControlID="hdnWindow" DropShadow="True" PopupControlID="divRejectedPermission" OnCancelScript="ResetPopUpPermission(); return false;" CancelControlID="btnCancel"
                    Enabled="True"
                    BackgroundCssClass="ModalBackground" DynamicServicePath="" />

            </asp:View>
        </asp:MultiView>
        <div id="divRejectedPermission" class="commonPopup" style="display: none">
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
                <div class="col-md-12">
                    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="button" meta:resourcekey="btnRejectResource1" />

                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" meta:resourcekey="btnCancelResource1" />
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
