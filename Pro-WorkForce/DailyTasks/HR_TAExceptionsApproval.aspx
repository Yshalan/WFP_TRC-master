<%@ Page Title="" Language="VB" Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="HR_TAExceptionsApproval.aspx.vb" Inherits="DailyTasks_HR_TAExceptionsApproval"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="EmployeeFilter"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ShowPopUp(Mode) {
            var lang = '<%= MsgLang %>';
            if (Mode == 1) {
                if (lang == 'en') {
                    return confirm('Are you sure you want to accept TA Exception?');
                }
                else {
                    return confirm('هل انت متأكد من قبول استثناء الحضور؟');
                }
            }
            else if (Mode == 2) {
                if (lang == 'en') {
                    return confirm('Are you sure you want to reject TA Exception?');
                }
                else {
                    return confirm('هل انت متأكد من رفض استثناء الحضور؟');
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
        function ResetPopUpTAException() {
            var txtRejectedReason = document.getElementById("<%= txtRejectedReason.ClientID %>");
            txtRejectedReason.value = '';
        }
    </script>
        <uc1:PageHeader ID="PageHeader1" runat="server" />
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"
        OnClientActiveTabChanged="hideValidatorCalloutTab" meta:resourcekey="TabContainer1Resource1">
        <cc1:TabPanel ID="tabRequests" runat="server" HeaderText="TA Exception Requests"
            TabIndex="0" meta:resourcekey="tabRequestsResource1">
            <ContentTemplate>
                <asp:multiview id="mvTAExceptionApproval" runat="server">
                    <asp:view id="vTAExceptions" runat="server">
                        <div class="row">
                            <div class="table-responsive">
                                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdTAExceptions"
                                        Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                                    <telerik:RadGrid runat="server" ID="dgrdTAExceptions" AllowSorting="True" AllowPaging="True"
                                         GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                        PageSize="25" ShowFooter="True" meta:resourcekey="dgrdTAExceptionsResource1">
                                        <GroupingSettings CaseSensitive="False" />
                                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                            <Selecting AllowRowSelect="True" />
                                        </ClientSettings>
                                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="FK_EmployeeId,FromDate">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee Number" SortExpression="EmployeeNo"
                                                    UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource1">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"
                                                    UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource2">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Arabic Name"
                                                    SortExpression="EmployeeArabicName" UniqueName="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource3">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FK_EmployeeId" DataType="System.Int32" Visible="False"
                                                    HeaderText="FK_EmployeeId" UniqueName="FK_EmployeeId" meta:resourcekey="GridBoundColumnResource4">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FromDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="From Date"
                                                    UniqueName="FromDate" meta:resourcekey="GridBoundColumnResource5">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="To Date"
                                                    UniqueName="ToDate" meta:resourcekey="GridBoundColumnResource6" />
                                                <telerik:GridBoundColumn DataField="UserID" HeaderText="Created By" SortExpression="UserID"
                                                    UniqueName="UserID" meta:resourcekey="GridBoundColumnResource7">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CREATED_DATE" HeaderText="Created Date" SortExpression="CREATED_DATE"
                                                    UniqueName="CREATED_DATE" meta:resourcekey="GridBoundColumnResource8" DataFormatString="{0:dd/MM/yyyy}">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                                    <ItemTemplate>
                                                        <asp:linkbutton id="lnkAccept" runat="server" text="Accept" onclick="lnkAccept_Click"
                                                            commandname="accept" onclientclick="return ShowPopUp('1')" meta:resourcekey="lnkAcceptResource1">
                                                        </asp:linkbutton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                                                    <ItemTemplate>
                                                        <asp:linkbutton id="lnkReject" runat="server" text="Reject" commandname="reject"
                                                            onclick="lnkReject_Click" onclientclick="return ShowPopUp('2')" commandargument='<%# Eval("FK_EmployeeId") & ";" & Eval("FromDate") %>'
                                                            meta:resourcekey="lnkRejectResource1">
                                                        </asp:linkbutton>
                                                        <asp:hiddenfield id="hdnEmployeeNameAr" runat="server" value='<%# Eval("EmployeeArabicName") %>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <CommandItemTemplate>
                                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                                    <Items>
                                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                            ImagePosition="Right" runat="server" Owner="" meta:resourcekey="RadToolBarButtonResource1" />
                                                    </Items>
                                                </telerik:RadToolBar>
                                            </CommandItemTemplate>
                                        </MasterTableView>
                                        <SelectedItemStyle ForeColor="Maroon" />
                                    </telerik:RadGrid>
                                    <asp:HiddenField runat="server" ID="hdnWindow" />
                    <cc1:ModalPopupExtender ID="mpeRejectPopupTAException" runat="server" BehaviorID="modelPopupExtenderTAException"
                        TargetControlID="hdnWindow" DropShadow="True"  PopupControlID = "divRejectedTAException" OnCancelScript = "ResetPopUpTAException(); return false;" CancelControlID = "btnCancelPOP"
                        Enabled="True" 
                        BackgroundCssClass="ModalBackground" DynamicServicePath="" />
                                </div>
                            </div>
                    </asp:view>
                    <asp:view id="vTAExceptionDetails" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                    <uc3:EmployeeFilter ID="EmployeeFilter1" runat="server" ValidationGroup="TaException" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:label id="Label4" runat="server" cssclass="Profiletitletxt" text="From Date"
                                        meta:resourcekey="Label4Resource1"></asp:label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDatePicker ID="dtpFromdate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                        PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US"
                                        meta:resourcekey="dtpFromdateResource1">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                            Width="">
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                    <asp:requiredfieldvalidator id="reqFromdate" runat="server" controltovalidate="dtpFromdate"
                                        display="None" errormessage="Please select from date" validationgroup="TaException"
                                        meta:resourcekey="reqFromdateResource1">
                                    </asp:requiredfieldvalidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderPermissionDate" runat="server" TargetControlID="reqFromdate"
                                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:label id="Label6" runat="server" cssclass="Profiletitletxt" text="Is Temporary"
                                        meta:resourcekey="Label6Resource1"></asp:label>
                                </div>
                                <div class="col-md-4">
                                    <asp:checkbox id="chckTemporary" runat="server" autopostback="True" meta:resourcekey="chckTemporaryResource1" />
                                </div>                           
                            </div>
                            <asp:panel id="pnlEndDate" runat="server" visible="False" meta:resourcekey="pnlEndDateResource1">
                                <div class="row">
                                    <div class="col-md-2">
                                        <asp:label id="lblEndDate" runat="server" cssclass="Profiletitletxt" text="End date"
                                            meta:resourcekey="lblEndDateResource1"></asp:label>
                                    </div>
                                    <div class="col-md-4">
                                        <telerik:RadDatePicker ID="dtpEndDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                            PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US"
                                            meta:resourcekey="dtpEndDateResource1">
                                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                            </Calendar>
                                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                                Width="">
                                            </DateInput>
                                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                        </telerik:RadDatePicker>
                                        <asp:requiredfieldvalidator id="rfvToDate" runat="server" controltovalidate="dtpEndDate"
                                            display="None" errormessage="Please select to date" validationgroup="TaException"
                                            meta:resourcekey="rfvToDateResource1">
                                        </asp:requiredfieldvalidator>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvToDate"
                                            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                            Enabled="True">
                                        </cc1:ValidatorCalloutExtender>
                                    </div>
                                </div>
                            </asp:panel>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:label id="lblReason" runat="server" text="Reason" cssclass="Profiletitletxt"
                                        meta:resourcekey="lblReasonResource1" />
                                </div>
                                <div class="col-md-4">
                                    <asp:textbox id="txtReason" runat="server" rows="4" columns="45" textmode="MultiLine"
                                        meta:resourcekey="txtReasonResource1" />
                                    <asp:requiredfieldvalidator id="rfvReason" runat="server" controltovalidate="txtReason"
                                        display="None" errormessage="Please enter reason" validationgroup="TaException"
                                        meta:resourcekey="rfvReasonResource1">
                                    </asp:requiredfieldvalidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvReason"
                                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>                          
                            <div class="row">
                                <div class="col-md-8 text-center">
                                    <asp:button id="btnAccept" runat="server" text="Accept" cssclass="button" meta:resourcekey="btnAcceptResource1" />
                                    <asp:button id="btnReject" runat="server" text="Reject" cssclass="button" meta:resourcekey="btnRejectResource1" />
                                    <asp:button id="btnCancel" runat="server" text="Cancel" cssclass="button" meta:resourcekey="btnCancelResource1" />
                                </div>
                            </div>
                    </asp:view>
                </asp:multiview>
                
                      <div id="divRejectedTAException" class="commonPopup" style="display: none">
                
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
                        <div class="col-md-8 text-center">
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
                            <telerik:RadFilter runat="server" ID="RadFilter2" FilterContainerID="dgrdRequests"
                                Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />               
                            <telerik:RadGrid runat="server" ID="dgrdRequests" AllowSorting="True" AllowPaging="True"
                                 GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                PageSize="25" ShowFooter="True" meta:resourcekey="dgrdTAExceptionsResource1">
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee Number" SortExpression="EmployeeNo"
                                            UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource1">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"
                                            UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource2">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Arabic Name"
                                            SortExpression="EmployeeArabicName" UniqueName="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource3">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FK_EmployeeId" DataType="System.Int32" Visible="False"
                                            HeaderText="FK_EmployeeId" UniqueName="FK_EmployeeId" meta:resourcekey="GridBoundColumnResource4">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FromDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="From Date"
                                            UniqueName="FromDate" meta:resourcekey="GridBoundColumnResource5">
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridBoundColumn DataField="ToDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="To Date"
                                            UniqueName="ToDate" meta:resourcekey="GridBoundColumnResource6" />
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="RejectionReason" HeaderText="Rejection Reason"
                                            meta:resourcekey="RejectionReasonColumnResource14" UniqueName="RejectionReason">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                                            <ItemTemplate>
                                                <asp:hiddenfield id="hdnEmployeeNameAr" runat="server" value='<%# Eval("EmployeeArabicName") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar runat="server" ID="RadToolBar2" OnButtonClick="RadToolBar2_ButtonClick"
                                            Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                            <Items>
                                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                                    ImagePosition="Right" runat="server" Owner="" meta:resourcekey="RadToolBarButtonResource1" />
                                            </Items>
                                        </telerik:RadToolBar>
                                    </CommandItemTemplate>
                                </MasterTableView>
                                <SelectedItemStyle ForeColor="Maroon" />
                            </telerik:RadGrid>
                       </div>
</div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    </asp:UpdatePanel>
</asp:content>
