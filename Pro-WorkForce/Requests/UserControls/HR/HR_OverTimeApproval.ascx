<%@ Control Language="VB" AutoEventWireup="false" CodeFile="HR_OverTimeApproval.ascx.vb"
    Inherits="Requests_UserControls_HR_HR_OverTimeApproval" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script type="text/javascript" language="javascript">
    function ShowPopUpOverTime(Mode) {
        var lang = '<%= MsgLang %>';
        if (Mode == 1) {
            if (lang == 'en') {
                return confirm('Are you sure you want to Accept OverTime?');
            }
            else {
                return confirm('هل انت متأكد من قبول العمل الاضافي؟');
            }
        }
        else if (Mode == 2) {
            if (lang == 'en') {
                return confirm('Are you sure you want to Reject OverTime?');
            }
            else {
                return confirm('هل انت متأكد من رفض العمل الاضافي؟');
            }
        }
    }

    function ResetPopUpOverTime() {
        var txtRejectedReason = document.getElementById("<%= txtRejectedReason.ClientID %>");
        txtRejectedReason.value = '';
    }

    function CheckAllOverTime(id) {
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
                <asp:Label ID="lblOverTimeRequests" runat="server" Text="Over Time Requests" CssClass="Profiletitletxt" meta:resourcekey="lblOverTimeRequestsResource1" />
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
                <asp:Button ID="btnProceed" Text="Proceed Selected" CssClass="button" runat="server"
                    meta:resourcekey="btnProceedResource1"></asp:Button>
            </div>
        </div>
        <asp:MultiView ID="mvLeaveApproval" runat="server">
            <asp:View ID="viewDMApproval" runat="server">
                <div class="table-responsive">
                    <telerik:RadGrid ID="dgrdEmpLeaveRequest" runat="server" AllowSorting="True" AllowPaging="True"
                        GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        PageSize="10" ShowFooter="True" meta:resourcekey="dgrdEmpLeaveRequestResource1">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="false" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="false" />
                        </ClientSettings>
                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top" DataKeyNames="FromDateTime,ToDateTime,Duration,FK_OvertimeRuleId,EmpOvertimeId,ApprovedDuration">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False"
                                    UniqueName="TemplateColumn">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat="server" onclick="CheckAllOverTime(this)" Text="&nbsp;" Visible="true" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Emp No" meta:resourcekey="GridBoundColumnResource1"
                                    UniqueName="EmployeeNo" />
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" meta:resourcekey="GridBoundColumnResource7"
                                    UniqueName="EmployeeName" />
                                          <telerik:GridBoundColumn DataField="FromDateTime" HeaderText="Date" meta:resourcekey="GridBoundDateResource10"
                                    UniqueName="Date" DataFormatString="{0:dd/MM/yyyy}"/>
                                <telerik:GridBoundColumn DataField="FromDateTime" HeaderText="From" meta:resourcekey="GridBoundColumnResource8"
                                    UniqueName="FromDateTime" />
                                <telerik:GridBoundColumn DataField="ToDateTime" HeaderText="To" meta:resourcekey="GridBoundColumnResource9"
                                    UniqueName="ToDateTime" />
                                <telerik:GridBoundColumn DataField="Duration" HeaderText="Duration" meta:resourcekey="GridBoundColumnResource2"
                                    UniqueName="Duration" />
                                <telerik:GridTemplateColumn HeaderText="Approved Duration" UniqueName="txtApprovedDuration" 
                                    meta:resourcekey="GridTemplateColumnResource9" >
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtApprovedDuration" MinValue="0" MaxValue="99999"
                                            Skin="Vista" runat="server" Culture="en-US" LabelCssClass=""  
                                            Width="60px" meta:resourcekey="txtApprovedDurationResource1">
                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridCheckBoxColumn DataField="IsHigh" HeaderText="High" SortExpression="IsHigh"
                                    Resizable="False" UniqueName="IsHigh" meta:resourcekey="GridCheckBoxColumnResource1" ItemStyle-CssClass="nocheckboxstyle">
                                </telerik:GridCheckBoxColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_OvertimeRuleId" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource4" UniqueName="FK_OvertimeRuleId" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource4" UniqueName="FK_EmployeeId" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="EmpOverTimeId" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource5" UniqueName="EmpOverTimeId" />
                                <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status" meta:resourcekey="GridBoundColumnResource6"
                                    UniqueName="StatusName" />
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAccept" runat="server" Text="Accept" OnClick="lnkAccept_Click"
                                            OnClientClick="return ShowPopUpOverTime('1')" meta:resourcekey="lnkAcceptResource1"></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkReject" runat="server" Text="Reject" CommandName="reject"
                                            CommandArgument='<%# Eval("EmpOvertimeId") %>' meta:resourcekey="lnkRejectResource1"></asp:LinkButton>
                                        <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                        <asp:HiddenField ID="hdnStatusNameAr" runat="server" Value='<%# Eval("StatusNameArabic") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar ID="RadToolBar1" runat="server" Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                        </MasterTableView>
                        <SelectedItemStyle ForeColor="Maroon" />
                    </telerik:RadGrid>
                </div>
                <asp:HiddenField runat="server" ID="hdnWindow" />
                <cc1:ModalPopupExtender ID="mpeRejectPopupOverTime" runat="server" BehaviorID="modelPopupExtenderOverTime"
                    TargetControlID="hdnWindow" PopupControlID="divRejectedOverTime" DropShadow="True" OnCancelScript="ResetPopUpOverTime(); return false;"
                    CancelControlID="btnCancel" Enabled="True"
                    BackgroundCssClass="ModalBackground" DynamicServicePath="" />
            </asp:View>
        </asp:MultiView>
        <div id="divRejectedOverTime" class="commonPopup" style="display: none">
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
                    <asp:HiddenField runat="server" ID="hdnDuration" />
                </div>
            </div>

        </div>
    </ContentTemplate>
</asp:UpdatePanel>
