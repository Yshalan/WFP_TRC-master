<%@ Control Language="VB" AutoEventWireup="false" CodeFile="HR_StudyPermissionApproval.ascx.vb"
    Inherits="Requests_UserControls_HR_HR_StudyPermissionApproval" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript" language="javascript">
    function ShowPopUpStudy(Mode) {
        var lang = '<%= MsgLang %>';
        if (Mode == 1) {
            if (lang == 'en') {
                return confirm('Are you sure you want to accept Permission?');
            }
            else {
                return confirm('هل انت متأكد من قبول المغادرة؟');
            }
        }
        else if (Mode == 2) {
            if (lang == 'en') {
                return confirm('Are you sure you want to reject Permission?');
            }
            else {
                return confirm('هل انت متأكد من رفض المغادرة؟');
            }
        }
    }

    function ResetPopUpStudy() {
        var txtRejectedReason = document.getElementById("<%= txtRejectedReason.ClientID %>");
        txtRejectedReason.value = '';
    }

    function CheckAllStudy(id) {
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
                <asp:Label ID="lblStudyPermissionRequests" runat="server" Text="Study Permission Requests" CssClass="Profiletitletxt" meta:resourcekey="lblPermissionRequestsResource1" />
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
                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top" DataKeyNames="PermDate,PermEndDate,PermissionRequestId,AttachedFile,IsForPeriod,FK_EmployeeId,IsFullDay,IsFlexible,
                            FlexibilePermissionDuration,PermissionOption,FromTime,ToTime,Days,Remark,DaysName,DaysArabicName,StudyYear,Semester,FK_UniversityId,Emp_GPAType,Emp_GPA,FK_MajorId,FK_SpecializationId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False"
                                    UniqueName="TemplateColumn">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat="server" onclick="CheckAllStudy(this)" Text="&nbsp;" Visible="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" meta:resourcekey="GridBoundColumnResource1"
                                    UniqueName="EmployeeNo" />
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" meta:resourcekey="GridBoundColumnResource2"
                                    UniqueName="EmployeeName" />
                                <telerik:GridBoundColumn DataField="PermDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Start Date"
                                    meta:resourcekey="GridBoundColumnResource3" UniqueName="PermDate" />
                                <telerik:GridBoundColumn DataField="PermEndDate" DataFormatString="{0:dd/MM/yyyy}"
                                    HeaderText="End Date" meta:resourcekey="GridBoundColumnResource4" UniqueName="PermEndDate" />
                                <telerik:GridBoundColumn DataField="FromTime" DataFormatString="{0:HH:mm}" HeaderText="From Time"
                                    meta:resourcekey="GridBoundColumnResource5" UniqueName="FromTime" />
                                <telerik:GridBoundColumn DataField="ToTime" DataFormatString="{0:HH:mm}" HeaderText="To Time"
                                    meta:resourcekey="GridBoundColumnResource6" UniqueName="ToTime" />
                                <telerik:GridBoundColumn DataField="IsFlexible" HeaderText="Flexible" UniqueName="IsFlexible"
                                    meta:resourcekey="GridBoundColumnResource7" />
                                <telerik:GridBoundColumn DataField="DaysName" HeaderText="Days Name" UniqueName="DaysName"
                                    meta:resourcekey="GridBoundColumnResource200" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="PermissionRequestId" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource8" UniqueName="PermissionRequestId" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource9" UniqueName="FK_EmployeeId" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="PermTypeId" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource10" UniqueName="PermTypeId" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="Remarks" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource11" UniqueName="Remarks" />

                                <telerik:GridBoundColumn AllowFiltering="False" DataField="IsForPeriod" Visible="False"
                                    UniqueName="IsForPeriod" meta:resourcekey="GridBoundColumnResource12" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="Days" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource13" UniqueName="Days" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="AttachedFile" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource14" UniqueName="AttachedFile" />
                                <telerik:GridTemplateColumn HeaderText="Attached File" AllowFiltering="False" UniqueName="TemplateColumn"
                                    meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="lnbView" runat="server" Text="View" meta:resourcekey="lnbViewResource1" OnClick="lnkDownloadFile_Click" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Remark" HeaderText="Remarks" UniqueName="Remark"
                                    meta:resourcekey="GridBoundColumnResource15" />
                                <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status" meta:resourcekey="GridBoundColumnResource16"
                                    UniqueName="StatusName" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="PermissionOption" Visible="False"
                                    UniqueName="PermissionOption" meta:resourcekey="GridBoundColumnResource17" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFlexible" Visible="False"
                                    UniqueName="IsFlexible" meta:resourcekey="GridBoundColumnResource18" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFullDay" Visible="False"
                                    UniqueName="IsFullDay" meta:resourcekey="GridBoundColumnResource19" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="FlexibilePermissionDuration"
                                    Visible="False" UniqueName="FlexibilePermissionDuration" meta:resourcekey="GridBoundColumnResource20" />
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAccept" runat="server" Text="Accept" OnClick="lnkAccept_Click"
                                            OnClientClick="return ShowPopUpStudy('1')" meta:resourcekey="lnkAcceptResource1"></asp:LinkButton>
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
                <cc1:ModalPopupExtender ID="mpeRejectPopupStudy" runat="server" BehaviorID="modelPopupExtenderStudy"
                    TargetControlID="hdnWindow" PopupControlID="divRejectedStudy" DropShadow="True" OnCancelScript="ResetPopUpStudy(); return false;"
                    CancelControlID="btnCancel" Enabled="True" BackgroundCssClass="ModalBackground"
                    DynamicServicePath="" />
            </asp:View>
        </asp:MultiView>
        <div id="divRejectedStudy" class="commonPopup" style="display: none">
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
