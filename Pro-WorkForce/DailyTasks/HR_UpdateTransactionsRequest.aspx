<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="HR_UpdateTransactionsRequest.aspx.vb" Inherits="DailyTasks_HR_UpdateTransactionsRequest"
    Theme="SvTheme" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function ValidateTime() {
            var strval = document.getElementById('<%=rmtToTime2.ClientID%>').value;
            var statusH = true;
            var statusM = true;
            var horval = strval.substring(0, 2);

            if (horval >= 24) {
                ShowMessage("invalid time. Hour can not be greater that 23.");
                statusH = false;
                return false;
            }
            else if (horval < 0) {
                alert("Invalid time. Hour can not be hours less than 0.");
                statusH = false;
                return false;
            }


            var minval = strval.substring(3, 5);

            if (minval > 59) {
                ShowMessage("Invalid time. Minute can not be more than 59.");
                statusM = false;
                return false;
            }
            else if (minval < 0) {
                ShowMessage("Invalid time. Minute can not be less than 0.");
                return false;
                statusM = false;
            }

            if ((horval == '') && (minval == '')) {
                ShowMessage("Invalid time. time can not be less than 0.");
                return false;
                statusH = false;
                statusM = false;
            }

            if (statusM == true && statusH == true) {
                return true;
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlEmployee" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" HeaderText="Leave Types" runat="server" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblDate" runat="server" CssClass="Profiletitletxt" Text="Date" meta:resourcekey="lblDateResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" AutoPostBack="True" Culture="en-US" Enabled="False"
                        Width="120px" meta:resourcekey="RadDatePicker1Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                        </Calendar>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                            Width="" AutoPostBack="True" LabelWidth="64px">
                            <EmptyMessageStyle Resize="None" />
                            <ReadOnlyStyle Resize="None" />
                            <FocusedStyle Resize="None" />
                            <DisabledStyle Resize="None" />
                            <InvalidStyle Resize="None" />
                            <HoveredStyle Resize="None" />
                            <EnabledStyle Resize="None" />
                        </DateInput>
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="RadDatePicker1"
                        Display="None" ErrorMessage="Please Select Date,The Max Date Allowed is Today" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator7">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <uc2:PageFilter ID="EmployeeFilterUC" runat="server" headertext="Employee Filter"
                        OneventEmployeeSelect="FillGrid" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblReason" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblReasonResource1">Reason</asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlReason" runat="server" MarkFirstMatch="True" AppendDataBoundItems="True"
                        DropDownStyle="DropDownList" Skin="Vista" CausesValidation="False" Width="225px" meta:resourcekey="ddlReasonResource1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                        InitialValue="--Please Select--" ErrorMessage="Please Select Type" ValidationGroup="grpSave"
                        ControlToValidate="ddlReason" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator4">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt" meta:resourcekey="lblAttachFileResource1" />
                </div>
                <div class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-btn">
                            <span class="btn btn-default" onclick="$(this).parent().find('input[type=file]').click();">Browse</span>
                            <asp:FileUpload ID="fuAttachFile" runat="server" name="uploaded_file" onchange="$(this).parent().parent().find('.form-control').html($(this).val().split(/[\\|/]/).pop());" Style="display: none;" type="file" meta:resourcekey="fuAttachFileResource1" />
                        </span>
                        <span class="form-control"></span>
                    </div>
                    <div class="veiw_remove">
                        <a id="lnbLeaveFile" target="_blank" runat="server" visible="False">
                            <asp:Label ID="lblView" runat="server" Text="View" meta:resourcekey="lblViewResource1" />
                        </a>
                        <asp:LinkButton ID="lnbRemove" runat="server" Text="Remove" Visible="False" meta:resourcekey="lnbRemoveResource1" />
                        <asp:Label ID="lblNoAttachedFile" runat="server" Text="No Attached File" Visible="False" meta:resourcekey="lblNoAttachedFileResource1" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="Profiletitletxt" meta:resourcekey="lblTimeResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadMaskedTextBox ID="rmtToTime2" runat="server" Mask="##:##" Enabled="False"
                        Text='<%# DataBinder.Eval(Container,"DataItem.ToTime2") %>' DisplayMask="##:##"
                        LabelCssClass="" CssClass="RadMaskedTextBox" LabelWidth="64px" meta:resourcekey="rmtToTime2Resource1">
                        <EmptyMessageStyle Resize="None" />
                        <ReadOnlyStyle Resize="None" />
                        <FocusedStyle Resize="None" />
                        <DisabledStyle Resize="None" />
                        <InvalidStyle Resize="None" />
                        <HoveredStyle Resize="None" />
                        <EnabledStyle Resize="None" />
                    </telerik:RadMaskedTextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="None"
                        ErrorMessage="Please Time" InitialValue="00:00" ControlToValidate="rmtToTime2" meta:resourcekey="RequiredFieldValidator8Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator8">
                    </cc1:ValidatorCalloutExtender>
                </div>

            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks" CssClass="Profiletitletxt" meta:resourcekey="lblRemarksResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Width="220px" meta:resourcekey="txtremarksResource1"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8 text-center">
                    <asp:RadioButtonList ID="rblUpdateTransactionType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                        <asp:ListItem Text="Update Transaction" Value="1" Selected="True" meta:resourcekey="rblUpdateTransactionTypeListItem1Resource1"></asp:ListItem>
                        <asp:ListItem Text="Delete Transaction" Value="2" meta:resourcekey="rblUpdateTransactionTypeListItem2Resource1"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8 text-center">
                    <asp:Button ID="btnSave" Text="Save" CssClass="button" runat="server" OnClientClick="Page_ClientValidate(); return ValidateTime();"
                        ValidationGroup="grpSave" meta:resourcekey="btnSaveResource1"></asp:Button>
                    <asp:Button ID="ibtnDelete" CssClass="button" Text="Delete" runat="server" OnClientClick="return confirm('Are you sure you want to delete?')"
                        CausesValidation="False" meta:resourcekey="ibtnDeleteResource1"></asp:Button>
                    <asp:Button ID="ibtnClear" runat="server" CssClass="button" Text="Clear" CausesValidation="False" meta:resourcekey="ibtnClearResource1" />
                </div>
            </div>
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"
                OnClientActiveTabChanged="hideValidatorCalloutTab" meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel ID="tabRequests" runat="server" HeaderText="Update Transactions Requests" meta:resourcekey="tabRequestsResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgEmpAtt" Skin="Hay"
                                    ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                                    <ContextMenu FeatureGroupID="rfContextMenu">
                                    </ContextMenu>
                                </telerik:RadFilter>
                                <telerik:RadGrid runat="server" ID="dgEmpAtt" AutoGenerateColumns="False" PageSize="25"
                                    AllowPaging="True" AllowSorting="True" GridLines="None" Width="100%" CellSpacing="0" meta:resourcekey="dgEmpAttResource1">
                                    <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top"
                                        DataKeyNames="MoveRequestId,IsFromMobile,MobileCoordinates,EmployeeArabicName,ReasonArabicName,MoveRequestId,UpdateTransactionTypeAr">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="MoveRequestId" SortExpression="Emp_no" HeaderText="Emp_no"
                                                Visible="False" AllowFiltering="False"
                                                UniqueName="MoveRequestId" FilterControlAltText="Filter MoveRequestId column" meta:resourcekey="GridBoundColumnResource1" />
                                            <telerik:GridBoundColumn DataField="name" SortExpression="name" HeaderText="Name"
                                                UniqueName="name" FilterControlAltText="Filter name column" meta:resourcekey="GridBoundColumnResource2" />
                                            <telerik:GridBoundColumn DataField="ReasonName" SortExpression="ReasonName" HeaderText="Reason"
                                                UniqueName="ReasonName" FilterControlAltText="Filter ReasonName column" meta:resourcekey="GridBoundColumnResource3" />
                                            <telerik:GridBoundColumn DataField="MoveDate" SortExpression="M_DATE" HeaderText="Date"
                                                DataFormatString="{0:dd/MM/yyyy}"
                                                UniqueName="MoveDate" FilterControlAltText="Filter MoveDate column" meta:resourcekey="GridBoundColumnResource4" />
                                            <telerik:GridBoundColumn DataField="MoveTime" SortExpression="M_Time" HeaderText="Time"
                                                DataFormatString="{0:HH:mm}" UniqueName="MoveTime" FilterControlAltText="Filter MoveTime column" meta:resourcekey="GridBoundColumnResource5" />
                                            <telerik:GridBoundColumn DataField="Remarks" SortExpression="Remarks" HeaderText="Remarks"
                                                UniqueName="Remarks" FilterControlAltText="Filter Remarks column" meta:resourcekey="GridBoundColumnResource6" />
                                            <telerik:GridBoundColumn DataField="UpdateTransactionTypeEn" UniqueName="UpdateTransactionTypeEn" HeaderText="Type"
                                                AllowFiltering="False" FilterControlAltText="Filter UpdateTransactionTypeEn column" meta:resourcekey="GridBoundColumnResource24" />
                                            <telerik:GridBoundColumn DataField="IsRejected" SortExpression="IsRejected" HeaderText="Status"
                                                UniqueName="IsRejected" FilterControlAltText="Filter IsRejected column" meta:resourcekey="GridBoundColumnResource7" />
                                            <telerik:GridBoundColumn DataField="IsFromMobile" Visible="False" AllowFiltering="False"
                                                UniqueName="IsFromMobile" FilterControlAltText="Filter IsFromMobile column" meta:resourcekey="GridBoundColumnResource8" />
                                            <telerik:GridBoundColumn DataField="MobileCoordinates" SortExpression="MobileCoordinates"
                                                HeaderText="Mobile Punch" UniqueName="MobileCoordinates" FilterControlAltText="Filter MobileCoordinates column" meta:resourcekey="GridBoundColumnResource9" />
                                            <telerik:GridBoundColumn DataField="EmployeeArabicName" Visible="False" AllowFiltering="False"
                                                UniqueName="EmployeeArabicName" FilterControlAltText="Filter EmployeeArabicName column" meta:resourcekey="GridBoundColumnResource10" />
                                            <telerik:GridBoundColumn DataField="ReasonArabicName" Visible="False" UniqueName="ReasonArabicName"
                                                AllowFiltering="False" FilterControlAltText="Filter ReasonArabicName column" meta:resourcekey="GridBoundColumnResource11" />
                                            <telerik:GridBoundColumn DataField="UpdateTransactionTypeAr" Visible="False" UniqueName="UpdateTransactionTypeAr" HeaderText="Type"
                                                AllowFiltering="False" FilterControlAltText="Filter UpdateTransactionTypeAr column" />
                                        </Columns>
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
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                        EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </ContentTemplate>

                </cc1:TabPanel>
                <cc1:TabPanel ID="tabTransactions" runat="server" HeaderText="Employee Transactions" meta:resourcekey="tabTransactionsResource1">
                    <ContentTemplate>
                        <div class="table-responsive">
                            <telerik:RadFilter runat="server" ID="TransactionsFilter" FilterContainerID="dgrdTransactions" Skin="Hay"
                                ShowApplyButton="False" meta:resourcekey="TransactionsFilterResource1">
                            </telerik:RadFilter>
                            <telerik:RadGrid runat="server" ID="dgrdTransactions" AutoGenerateColumns="False"
                                PageSize="25" AllowPaging="True" AllowSorting="True" GridLines="None"
                                Width="100%" CellSpacing="0" meta:resourcekey="dgrdTransactionsResource1">
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                    EnableRowHoverStyle="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" DataKeyNames="MoveId,EmployeeName,EmployeeArabicName,ReasonName,ReasonArabicName">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridBoundColumn AllowFiltering="False" DataField="MoveId" HeaderText="Emp_no"
                                            SortExpression="Emp_no" UniqueName="MoveId"
                                            Visible="False" FilterControlAltText="Filter MoveId column" meta:resourcekey="GridBoundColumnResource12">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="EmployeeName"
                                            SortExpression="name" UniqueName="EmployeeName" FilterControlAltText="Filter EmployeeName column" meta:resourcekey="GridBoundColumnResource13">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ReasonName" HeaderText="Reason"
                                            SortExpression="ReasonName" UniqueName="ReasonName" FilterControlAltText="Filter ReasonName column" meta:resourcekey="GridBoundColumnResource14">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MoveDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date"
                                            SortExpression="MoveDate" UniqueName="MoveDate" FilterControlAltText="Filter MoveDate column" meta:resourcekey="GridBoundColumnResource15">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MoveTime" DataFormatString="{0:HH:mm}" HeaderText="Time"
                                            SortExpression="MoveTime" UniqueName="MoveTime" FilterControlAltText="Filter MoveTime column" meta:resourcekey="GridBoundColumnResource16">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Reader" HeaderText="Reader"
                                            SortExpression="Reader" UniqueName="Reader" FilterControlAltText="Filter Reader column" meta:resourcekey="GridBoundColumnResource17">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks"
                                            SortExpression="Remarks" UniqueName="Remarks" FilterControlAltText="Filter Remarks column" meta:resourcekey="GridBoundColumnResource18">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="False" DataField="EmployeeArabicName"
                                            UniqueName="EmployeeArabicName" Visible="False" FilterControlAltText="Filter EmployeeArabicName column" meta:resourcekey="GridBoundColumnResource19">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="False" DataField="ReasonArabicName"
                                            UniqueName="ReasonArabicName" Visible="False" FilterControlAltText="Filter ReasonArabicName column" meta:resourcekey="GridBoundColumnResource20">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Status"
                                            UniqueName="Status" Display="False" AllowFiltering="False" FilterControlAltText="Filter Status column" meta:resourcekey="GridBoundColumnResource21">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="StatusName"
                                            UniqueName="StatusName" Display="False" AllowFiltering="False" FilterControlAltText="Filter StatusName column" meta:resourcekey="GridBoundColumnResource22">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="StatusNameArabic"
                                            UniqueName="StatusNameArabic" Display="False" AllowFiltering="False" FilterControlAltText="Filter StatusNameArabic column" meta:resourcekey="GridBoundColumnResource23">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar ID="RadToolBar1" runat="server"
                                            OnButtonClick="RadToolBar2_ButtonClick" Skin="Hay" SingleClick="None" meta:resourcekey="RadToolBar1Resource2">
                                            <Items>
                                                <telerik:RadToolBarButton runat="server" CommandName="FilterRadGrid" ImagePosition="Right"
                                                    ImageUrl="~/images/RadFilter.gif"
                                                    Owner="" Text="Apply filter" meta:resourcekey="RadToolBarButtonResource2">
                                                </telerik:RadToolBarButton>
                                            </Items>
                                        </telerik:RadToolBar>
                                    </CommandItemTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

