<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="HR_PermissionApproval.aspx.vb" Inherits="DailyTasks_HR_PermissionApproval"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="UserControls/HR_PermissionApproval.ascx" TagName="HR_PermissionApproval"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ShowPopUp(Mode) {
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
        function ResetPopUpPermission() {
            var txtRejectedReason = document.getElementById("<%= txtRejectedReason.ClientID %>");
            txtRejectedReason.value = '';
        }
    </script>
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"
        OnClientActiveTabChanged="hideValidatorCalloutTab" meta:resourcekey="TabContainer1Resource1" Style="visibility: visible">
        <cc1:TabPanel ID="tabRequests" runat="server" HeaderText="Permission Requests" TabIndex="0"
            meta:resourcekey="tabRequestsResource1">
            <ContentTemplate>
                <asp:MultiView ID="mvPermissionApproval" runat="server">
                    <asp:View ID="vPermissions" runat="server">
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdVwEmpPermissions"
                                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                                    <ContextMenu FeatureGroupID="rfContextMenu">
                                    </ContextMenu>
                                </telerik:RadFilter>
                                <telerik:RadGrid ID="dgrdVwEmpPermissions" runat="server" AllowPaging="True" AllowSorting="True"
                                    Width="100%" PageSize="25" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                    ShowFooter="True" meta:resourcekey="dgrdVwEmpPermissionsResource1" CellSpacing="0">
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top" DataKeyNames="PermissionRequestId,FromTime,ToTime,PermDate,PermEndDate,IsFullDay,IsFlexible,FlexibilePermissionDuration,AttachedFile">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" meta:resourcekey="GridBoundColumnResource1"
                                                SortExpression="EmployeeNo" UniqueName="EmployeeNo" FilterControlAltText="Filter EmployeeNo column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" meta:resourcekey="GridBoundColumnResource2"
                                                SortExpression="EmployeeName" UniqueName="EmployeeName" FilterControlAltText="Filter EmployeeName column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PermName" HeaderText="Permission Type" meta:resourcekey="GridBoundColumnResource3"
                                                SortExpression="PermName" UniqueName="PermName" FilterControlAltText="Filter PermName column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PermDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="From Date"
                                                meta:resourcekey="GridBoundColumnResource4" SortExpression="PermDate" UniqueName="PermDate" FilterControlAltText="Filter PermDate column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PermEndDate" DataFormatString="{0:dd/MM/yyyy}"
                                                HeaderText="To Date" meta:resourcekey="GridBoundColumnResource5" SortExpression="PermEndDate"
                                                UniqueName="PermEndDate" FilterControlAltText="Filter PermEndDate column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FromTime" DataFormatString="{0:HH:mm}" HeaderText="From Time"
                                                meta:resourcekey="GridBoundColumnResource6" SortExpression="FromTime" UniqueName="FromTime" FilterControlAltText="Filter FromTime column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ToTime" DataFormatString="{0:HH:mm}" HeaderText="To Time"
                                                meta:resourcekey="GridBoundColumnResource7" SortExpression="ToTime" UniqueName="ToTime" FilterControlAltText="Filter ToTime column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="PermissionId" meta:resourcekey="GridBoundColumnResource8"
                                                SortExpression="PermissionId" UniqueName="PermissionId" Visible="False" FilterControlAltText="Filter PermissionId column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_LeaveId" meta:resourcekey="GridBoundColumnResource9"
                                                SortExpression="FK_LeaveId" UniqueName="FK_LeaveId" Visible="False" FilterControlAltText="Filter FK_LeaveId column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PermissionRequestId" HeaderText="PermissionRequestId"
                                                meta:resourcekey="GridBoundColumnResource10" SortExpression="PermissionRequestId"
                                                UniqueName="PermissionRequestId" Visible="False" FilterControlAltText="Filter PermissionRequestId column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FK_EmployeeId" HeaderText="EmployeeId" meta:resourcekey="GridBoundColumnResource11"
                                                SortExpression="FK_EmployeeId" UniqueName="FK_EmployeeId" Visible="False" FilterControlAltText="Filter FK_EmployeeId column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="IsForPeriod" meta:resourcekey="GridBoundColumnResource12"
                                                UniqueName="IsForPeriod" Visible="False" FilterControlAltText="Filter IsForPeriod column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFullDay" HeaderText="Full Day"
                                                meta:resourcekey="GridBoundColumnResource13" UniqueName="IsFullDay" FilterControlAltText="Filter IsFullDay column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFlexible" HeaderText="Flexible"
                                                meta:resourcekey="GridBoundColumnResource14" UniqueName="IsFlexible" FilterControlAltText="Filter IsFlexible column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="FlexibilePermissionDuration"
                                                meta:resourcekey="GridBoundColumnResource15" UniqueName="FlexibilePermissionDuration"
                                                Visible="False" FilterControlAltText="Filter FlexibilePermissionDuration column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="AttachedFile"
                                                Visible="False" meta:resourcekey="GridBoundColumnResource13"
                                                UniqueName="AttachedFile" FilterControlAltText="Filter AttachedFile column" />
                                            <telerik:GridTemplateColumn HeaderText="Attached File" AllowFiltering="False" UniqueName="TemplateColumn"
                                                meta:resourcekey="GridTemplateColumnResource3" FilterControlAltText="Filter TemplateColumn column">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnbView" runat="server" Text="View" OnClick="lnkDownloadFile_Click" meta:resourcekey="lnbViewResource1" />
                                                    <%--<a target="_blank" id="lnbView" runat="server">
                                                        <asp:Label ID="lblView" runat="server" Text="View Attachment" meta:resourcekey="lnbViewResource1" />
                                                    </a>--%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="UserID" HeaderText="Created By" meta:resourcekey="GridBoundColumnResource16"
                                                SortExpression="UserID" UniqueName="UserID" FilterControlAltText="Filter UserID column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CREATED_DATE" HeaderText="Created Date" meta:resourcekey="GridBoundColumnResource17"
                                                SortExpression="CREATED_DATE" UniqueName="CREATED_DATE" DataFormatString="{0:dd/MM/yyyy}" FilterControlAltText="Filter CREATED_DATE column">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                                UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkAccept" runat="server" CommandName="accept" meta:resourcekey="lnkAcceptResource1"
                                                        OnClick="lnkAccept_Click" OnClientClick="return ShowPopUp('1')" Text="Accept">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource2"
                                                UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkReject" runat="server" CommandArgument='<%# Eval("PermissionRequestId") %>'
                                                        CommandName="reject" meta:resourcekey="lnkRejectResource1" OnClientClick="return ShowPopUp('2')"
                                                        Text="Reject">
                                                    </asp:LinkButton>
                                                    <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                                    <asp:HiddenField ID="hdnPermArabicType" runat="server" Value='<%# Eval("PermArabicName") %>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar ID="RadToolBar1" runat="server" meta:resourcekey="RadToolBar1Resource1"
                                                OnButtonClick="RadToolBar1_ButtonClick" Skin="Hay">
                                                <Items>
                                                    <telerik:RadToolBarButton runat="server" CausesValidation="false" CommandName="FilterRadGrid"
                                                        ImagePosition="Right" ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource1"
                                                        Owner="" Text="Apply filter">
                                                    </telerik:RadToolBarButton>
                                                </Items>
                                            </telerik:RadToolBar>
                                        </CommandItemTemplate>
                                    </MasterTableView>
                                    <SelectedItemStyle ForeColor="Maroon" />
                                </telerik:RadGrid>
                            </div>
                        </div>
                        <asp:HiddenField runat="server" ID="hdnWindow" />
                        <cc1:ModalPopupExtender ID="mpeRejectPopupPermission" runat="server" BehaviorID="modelPopupExtenderPermission"
                            TargetControlID="hdnWindow" DropShadow="True" PopupControlID="divRejectedPermission" OnCancelScript="ResetPopUpPermission(); return false;" CancelControlID="btnCancel"
                            Enabled="True"
                            BackgroundCssClass="ModalBackground" DynamicServicePath="" />
                    </asp:View>
                    <asp:View ID="vPermissionDetails" runat="server">
                        <uc3:HR_PermissionApproval ID="HR_PermissionRequest1" DisplayMode="ViewAddEdit" runat="server"
                            PermissionType="Normal" />
                    </asp:View>
                </asp:MultiView>
                <div id="divRejectedPermission" class="commonPopup" style="display: none">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblRejectedReason" runat="server" Text="Rejected Reason" CssClass="Profiletitletxt"
                                meta:resourcekey="lblRejectedReasonResource1" />
                        </div>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtRejectedReason" runat="server" TextMode="MultiLine" Rows="4"
                                Columns="45" meta:resourcekey="txtRejectedReasonResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-8">
                            <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="button" meta:resourcekey="btnRejectResource1" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" meta:resourcekey="btnCancelResource1" />
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
                        <telerik:RadGrid ID="dgrdRejected" runat="server" AllowPaging="True" AllowSorting="True"
                            Width="100%" PageSize="25" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                            ShowFooter="True" meta:resourcekey="dgrdVwEmpPermissionsResource1">
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top" DataKeyNames="FromTime,ToTime,PermDate,PermEndDate,IsFullDay,IsFlexible,FlexibilePermissionDuration">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" meta:resourcekey="GridBoundColumnResource1"
                                        SortExpression="EmployeeNo" UniqueName="EmployeeNo">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" meta:resourcekey="GridBoundColumnResource2"
                                        SortExpression="EmployeeName" UniqueName="EmployeeName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PermName" HeaderText="Permission Type" meta:resourcekey="GridBoundColumnResource3"
                                        SortExpression="PermName" UniqueName="PermName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PermDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="From Date"
                                        meta:resourcekey="GridBoundColumnResource4" SortExpression="PermDate" UniqueName="PermDate">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PermEndDate" DataFormatString="{0:dd/MM/yyyy}"
                                        HeaderText="To Date" meta:resourcekey="GridBoundColumnResource5" SortExpression="PermEndDate"
                                        UniqueName="PermEndDate">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FromTime" DataFormatString="{0:HH:mm}" HeaderText="From Time"
                                        meta:resourcekey="GridBoundColumnResource6" SortExpression="FromTime" UniqueName="FromTime">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ToTime" DataFormatString="{0:HH:mm}" HeaderText="To Time"
                                        meta:resourcekey="GridBoundColumnResource7" SortExpression="ToTime" UniqueName="ToTime">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="PermissionId" meta:resourcekey="GridBoundColumnResource8"
                                        SortExpression="PermissionId" UniqueName="PermissionId" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_LeaveId" meta:resourcekey="GridBoundColumnResource9"
                                        SortExpression="FK_LeaveId" UniqueName="FK_LeaveId" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PermissionRequestId" HeaderText="PermissionRequestId"
                                        meta:resourcekey="GridBoundColumnResource10" UniqueName="PermissionRequestId"
                                        Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FK_EmployeeId" HeaderText="EmployeeId" meta:resourcekey="GridBoundColumnResource11"
                                        UniqueName="FK_EmployeeId" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="IsForPeriod" meta:resourcekey="GridBoundColumnResource12"
                                        UniqueName="IsForPeriod" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFullDay" HeaderText="Full Day"
                                        meta:resourcekey="GridBoundColumnResource13" UniqueName="IsFullDay">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFlexible" HeaderText="Flexible"
                                        meta:resourcekey="GridBoundColumnResource14" UniqueName="IsFlexible">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="RejectionReason" HeaderText="Rejection Reason"
                                        meta:resourcekey="RejectionReasonColumnResource14" UniqueName="RejectionReason">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="False" DataField="FlexibilePermissionDuration"
                                        meta:resourcekey="GridBoundColumnResource15" UniqueName="FlexibilePermissionDuration"
                                        Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource2"
                                        UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                            <asp:HiddenField ID="hdnPermArabicType" runat="server" Value='<%# Eval("PermArabicName") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                <CommandItemTemplate>
                                    <telerik:RadToolBar ID="RadToolBar2" runat="server" meta:resourcekey="RadToolBar1Resource1"
                                        OnButtonClick="RadToolBar2_ButtonClick" Skin="Hay">
                                        <Items>
                                            <telerik:RadToolBarButton runat="server" CausesValidation="false" CommandName="FilterRadGrid"
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
