<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DM_LeaveApproval.ascx.vb"
    Inherits="Requests_UserControls_DM_DM_LeaveApproval" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
    function ShowPopUpLeave(Mode) {
        var lang = '<%= MsgLang %>';
        if (Mode == 1) {
            if (lang == 'en') {
                return confirm('Are you sure you want to accept Leave?');
            }
            else {
                return confirm('هل انت متأكد من قبول الاجازة؟');
            }
        }
        else if (Mode == 2) {
            if (lang == 'en') {
                return confirm('Are you sure you want to reject Leave?');
            }
            else {
                return confirm('هل انت متأكد من رفض الاجازة؟');
            }
        }
    }

    function openWin() {
        window.open("../admin/AssignEmployee.aspx" ,"popup_window", 'width=600,height=400,left=400,top=100,resizable=yes');
    }
    function OnClientCloseHandler(sender, args) {
        __doPostBack();
    }
    function openRadWin(lnk) {
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex- 2;
        var gvID = $find('<%=dgrdEmpLeaveRequest.ClientID%>');
        var masterTableView = gvID.get_masterTableView();
        var firstDataItem = masterTableView.get_dataItems()[rowIndex];
        var FK_EmployeeId = firstDataItem.getDataKeyValue("FK_EmployeeId");
        var LeaveId = firstDataItem.getDataKeyValue("LeaveId");
        var LeaveFrom = firstDataItem.getDataKeyValue("FromDate");
        
        var LeaveTo = firstDataItem.getDataKeyValue("ToDate");
        var EmployeeName = firstDataItem.getDataKeyValue("EmployeeName");
        //alert(rowIndex);
        oWindow = radopen("../admin/AssignEmployee.aspx?FK_EmployeeId=" + FK_EmployeeId + "&LeaveId=" + LeaveId + "&LeaveFrom=" + LeaveFrom + "&LeaveTo=" + LeaveTo + "&EmployeeName=" + EmployeeName, "RadWindow1");
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
                        <div>
                            <asp:Image ID="imgLoading" runat="server" ImageAlign="Middle" ImageUrl="~/images/STS_Loading.gif" Width="250px" Height="250px" Style="position: fixed;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblLeaveRequests" runat="server"
                    Text="Leave Requests" CssClass="Profiletitletxt"
                    meta:resourcekey="lblLeaveRequestsResource1" />
                <asp:Label ID="lblRequestNo" runat="server" Text="Request Numbers" CssClass="Profiletitletxt" meta:resourcekey="lblRequestNoResource1" />
            </div>
        </div>

        <div class="row" runat="server" id="divrdbProceed" >
            <div class="col-md-4"></div>
            <div class="col-md-4"></div>
            <div class="col-md-4">
                <asp:RadioButtonList ID="rdbProceed" runat="server" RepeatDirection="Horizontal"
                    AutoPostBack="true" meta:resourcekey="rdbProceedResource1" Visible="false">
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
                    meta:resourcekey="btnProceedResource1" Visible="false"></asp:Button>
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
                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" ClientDataKeyNames ="EmployeeName,LeaveId,FK_EmployeeId,FromDate,ToDate" DataKeyNames="FromDate,ToDate,LeaveId,AttachedFile,LeaveTypeId,HasAdvancedSalary,FK_EmployeeId,Remarks,Requestdate,NextApprovalStatus">
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
                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Emp No" meta:resourcekey="GridBoundColumnResource1"
                                    UniqueName="EmployeeNo" />
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" meta:resourcekey="GridBoundColumnResource2"
                                    UniqueName="EmployeeName" />
                                <telerik:GridBoundColumn DataField="RequestDate" DataFormatString="{0:dd/MM/yyyy}"
                                    HeaderText="Request Date" meta:resourcekey="GridBound1ColumnResource1" UniqueName="RequestDate" />
                                <telerik:GridBoundColumn DataField="LeaveName" HeaderText="Leave Type" meta:resourcekey="GridBound2ColumnResource1"
                                    UniqueName="LeaveName" />
                                <telerik:GridBoundColumn DataField="FromDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="From Date"
                                    meta:resourcekey="GridBound4ColumnResource1" UniqueName="FromDate" />
                                <telerik:GridBoundColumn DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="To Date"
                                    meta:resourcekey="GridBound5ColumnResource1" UniqueName="ToDate" />
                                <telerik:GridBoundColumn DataField="Days" HeaderText="No. Days" meta:resourcekey="GridBound6ColumnResource1"
                                    UniqueName="Days" />
                                 <telerik:GridBoundColumn DataField="TotalBalance" HeaderText="الرصيد" 
                                    UniqueName="Balance" />
                                <telerik:GridTemplateColumn HeaderText="With Advanced Salary" AllowFiltering="False"
                                    UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource10">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chbWithAdvancedSalary" runat="server" Enabled="False"
                                            Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="HasAdvancedSalary" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource13" UniqueName="HasAdvancedSalary" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="LeaveId" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource3" UniqueName="LeaveId" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource4" UniqueName="FK_EmployeeId" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="LeaveTypeId" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource5" UniqueName="LeaveTypeId" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="StatusId" Visible="False"
                                    UniqueName="StatusId" meta:resourcekey="GridBoundColumnResource6" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="AttachedFile"
                                    Visible="False" meta:resourcekey="GridBoundColumnResource7"
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
                                <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status" meta:resourcekey="GridBoundColumnResource6"
                                    UniqueName="StatusName" />
                                 <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                         <asp:LinkButton ID="lnkAssign" runat="server" Text="Assign"   OnClientClick ="openRadWin(this);return false" meta:resourcekey="lnkAssignResource1"></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                         <asp:LinkButton ID="lnkAccept" runat="server" Text="Accept" CausesValidation="false"   OnClick="lnkAccept_Click" meta:resourcekey="lnkAcceptResource1"></asp:LinkButton>
                                       <%-- <asp:LinkButton ID="lnkAccept" runat="server" Text="Accept" OnClick="lnkAccept_Click" 
                                            OnClientClick ="openRadWin(this);return false"  meta:resourcekey="lnkAcceptResource1"></asp:LinkButton>--%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
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
                <asp:HiddenField runat="server" ID="hdnWindow2" />
                <cc1:ModalPopupExtender ID="mpeRejectPopupLeave" runat="server" BehaviorID="modelPopupExtenderLeave"
                    TargetControlID="hdnWindow" PopupControlID="divRejectedLeave" DropShadow="True" OnCancelScript="ResetPopUpLeave(); return false;"
                    CancelControlID="btnCancel" Enabled="True"
                    BackgroundCssClass="ModalBackground" DynamicServicePath="" />

               <%-- <cc1:ModalPopupExtender ID="mpeAcceptPopupLeave" runat="server" BehaviorID="abcd"
                    TargetControlID="hdnWindow2" PopupControlID="divAcceptLeave" DropShadow="True" OnCancelScript="ResetPopUpLeave(); return false;"
                    CancelControlID="btnAcCancel" Enabled="True"
                    BackgroundCssClass="ModalBackground" DynamicServicePath="" />--%>
            </asp:View>
        </asp:MultiView>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Default"
            EnableShadow="True" InitialBehavior="None">
            <Windows>
                <telerik:RadWindow ID="RadWindow1" runat="server"  Animation="FlyIn" Behavior="Close, Move, Resize"
                    Behaviors="Close, Move, Resize" EnableShadow="True" Height="530px" IconUrl="~/images/HeaderWhiteChrome.jpg"
                    InitialBehavior="None" ShowContentDuringLoad="False" VisibleStatusbar="False"
                    Width="700px">
                </telerik:RadWindow>
            </Windows>
         
        </telerik:RadWindowManager>
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
       <%-- <div id="divAcceptLeave" class="commonPopup_accept" style="display: none ">
       <div id="trcompany" runat="server" class="row">
            <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="Company" meta:resourcekey="Label1Resource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadComboBox ID="RadCmbBxCompanies" AutoPostBack="true" 
                    runat="server" ValidationGroup="ValidateComp" Style="width: 350px">
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="rfvCompanies" runat="server" ControlToValidate="RadCmbBxCompanies"
                    Display="None" ErrorMessage="Please Select Company" ValidationGroup="ValidateComp"
                    meta:resourcekey="rfvCompaniesResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="vceCompanies" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="rfvCompanies" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                </cc1:ValidatorCalloutExtender>
            </div>
        </div>
         <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblLevels" runat="server" Text="Entity" CssClass="Profiletitletxt"
                    meta:resourcekey="lblLevelsResource1"></asp:Label>
            </div>
            <div class="col-md-4">
               
                <telerik:RadComboBox ID="RadCmbBxEntity" AllowCustomText="false" Filter="Contains"
                    EnableScreenBoundaryDetection="false" MarkFirstMatch="true" Skin="Vista" runat="server"
                    AutoPostBack="true" ValidationGroup="ValidateLevels" Style="width: 350px">
                </telerik:RadComboBox>
                <asp:HiddenField ID="hdnIsEntityClick" runat="server" />
                <asp:RequiredFieldValidator ID="rfvEntity" runat="server" ControlToValidate="RadCmbBxEntity"
                    InitialValue="--Please Select--" Display="None" ErrorMessage="Please Select Entity"
                    meta:resourcekey="rfvEntityResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="vceEntity" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="rfvEntity" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                </cc1:ValidatorCalloutExtender>
            </div>
            <div class="col-md-3">
                <asp:CheckBox ID="chkDirectStaff" runat="server" Text="Direct Staff Only" Visible="false" meta:resourcekey="chkDirectStaffResource1" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblEmployees" runat="server" Text="Employee" CssClass="Profiletitletxt"
                    meta:resourcekey="lblEmployeesResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadComboBox ID="RadCmbBxEmployees" AllowCustomText="false" Filter="Contains"
                    EnableScreenBoundaryDetection="false" MarkFirstMatch="true" Skin="Vista" runat="server"
                    AutoPostBack="true" ValidationGroup="ValidateLevels" Style="width: 350px">
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" ControlToValidate="RadCmbBxEmployees"
                    InitialValue="--Please Select--" Display="None" ErrorMessage="Please Select Employee"
                    meta:resourcekey="rfvEmployeeResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="rfvEmployee" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                </cc1:ValidatorCalloutExtender>
            </div>
        </div>
        <div class="row" runat="server" visible="false" id="EmployeestxtBox">
            <div class="col-md-2">
                <asp:Label ID="lblEmployeestxt" runat="server" Text="Employee" CssClass="Profiletitletxt"
                    meta:resourcekey="lblEmployeesResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtEmployee" runat="server" meta:resourcekey="TxtEmpNoResource1"
                    Width="350px"></asp:TextBox>
            </div>
        </div>
       <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblEmpNo" runat="server" Text="Emp No." CssClass="Profiletitletxt"
                    meta:resourcekey="lblEmpNoResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="TxtEmpNo" runat="server" meta:resourcekey="TxtEmpNoResource1" AutoPostBack="false"></asp:TextBox>
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="TxtEmpNo" ID="RegularExpressionValidator1"
                    ValidationExpression="^{0,50}$"  runat="server" ErrorMessage="Maximum 50 characters allowed."
                    ValidationGroup="validateEmployeeComp"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="revTxtEmpNo" Display="Dynamic" ValidationGroup="validateEmployeeComp"
                    runat="server" ErrorMessage="Special Characters are not allowed!" ValidationExpression="[^~`!@#$%^&*()+=|}]{['&quot;:?.>,<]+" 
                    ControlToValidate="TxtEmpNo">
                </asp:RegularExpressionValidator>
                
                <asp:RequiredFieldValidator ID="rfvtxtEmpNo" runat="server" ControlToValidate="TxtEmpNo"
                    Display="None" ErrorMessage="Please Select Employee" Enabled="false" meta:resourcekey="rfvEmployeeResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="rfvtxtEmpNo" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                </cc1:ValidatorCalloutExtender>
               
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Retrieve" CausesValidation="False"
                    meta:resourcekey="btnSearchResource1" />

           

                <asp:Button ID="btnAssignEmployee" runat="server" CssClass="button" Text="Assign" 
                    CausesValidation="False" meta:resourcekey="btnSearchEmployeeResource1" ValidationGroup ="validateEmployeeComp"/>

              
                <asp:Button ID="btnAcCancel" runat="server" Text="Cancel" CssClass="button" meta:resourcekey="btnCancelResource1" />
            </div>
        </div>
        <asp:HiddenField ID="hdnEmployeeId" runat="server" />
        </div>--%>
    </ContentTemplate>
</asp:UpdatePanel>
