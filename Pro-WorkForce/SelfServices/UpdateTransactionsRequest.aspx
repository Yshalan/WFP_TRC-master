<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="UpdateTransactionsRequest.aspx.vb" Inherits="SelfServices_UpdateTransactionsRequest"
    Theme="SvTheme" meta:resourcekey="PageResource1" UICulture="auto" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function RefreshPage() {
            window.location = "../SelfServices/UpdateTransactionsRequest.aspx";
        }

        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdManualEntryRequest.ClientID%>");
            var masterTable = grid.get_masterTableView();
            var value = false;
            for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                var gridItemElement = masterTable.get_dataItems()[i].findElement("chk");
                if (gridItemElement.checked) {
                    value = true;
                }
            }
            if (value === false) {
                ShowMessage("<%= Resources.Strings.PleaseSelectFromList%>");
            }
            return value;
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


    <uc1:PageHeader ID="Emp_UpdateTransactionsRequest" runat="server" HeaderText="Employee Update Transaction Request" />
    <asp:MultiView ID="mvEmpLeaverequest" runat="server">
        <asp:View ID="viewManualEntryRequests" runat="server">
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0"
                OnClientActiveTabChanged="hideValidatorCalloutTab" meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel ID="tabRequests" runat="server" HeaderText="Update Transactions Requests" meta:resourcekey="tabRequestsResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblstatus" runat="server" CssClass="Profiletitletxt" Text="Status" meta:resourcekey="lblstatusResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="ddlStatus" AutoPostBack="True" runat="server" MarkFirstMatch="True" meta:resourcekey="ddlStatusResource1">
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvStatus" ValidationGroup="Get" runat="server" ControlToValidate="ddlStatus"
                                    Display="None" ErrorMessage="Please Select Request Status" InitialValue="--Please Select--" meta:resourcekey="rfvStatusResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="rfvStatus" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblFromDateSearch" runat="server" CssClass="Profiletitletxt" Text="From Date" meta:resourcekey="lblFromDateSearchResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpFromDateSearch" runat="server" Culture="en-US" Width="180px" meta:resourcekey="dtpFromDateSearchResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        Width="" LabelWidth="64px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="dtpFromDateSearch"
                                    Display="None" ErrorMessage="Please Select Date" ValidationGroup="Get" meta:resourcekey="RequiredFieldValidator9Resource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator9_ValidatorCalloutExtender"
                                    runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt" Text="To Date" meta:resourcekey="lblToDateResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpToDateSearch" runat="server" Culture="en-US" Width="180px" meta:resourcekey="dtpToDateSearchResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                        Width="" LabelWidth="64px">
                                        <EmptyMessageStyle Resize="None" />
                                        <ReadOnlyStyle Resize="None" />
                                        <FocusedStyle Resize="None" />
                                        <DisabledStyle Resize="None" />
                                        <InvalidStyle Resize="None" />
                                        <HoveredStyle Resize="None" />
                                        <EnabledStyle Resize="None" />
                                    </DateInput>
                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtpToDateSearch"
                                    Display="None" ErrorMessage="Please Select Date" ValidationGroup="Get" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                                    TargetControlID="RequiredFieldValidator1">
                                </cc1:ValidatorCalloutExtender>
                                <br />
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="dtpFromDateSearch"
                                    ControlToValidate="dtpToDateSearch" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                                    Operator="GreaterThanEqual" Type="Date" ValidationGroup="Get" meta:resourcekey="CompareValidator2Resource1"></asp:CompareValidator>
                                <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender2"
                                    runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnGet" runat="server" Text="Get" CssClass="button" ValidationGroup="Get" meta:resourcekey="btnGetResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnDelete" runat="server" Text="Delete Manual Entry Request" OnClientClick="return ValidateDelete();" CssClass="button" meta:resourcekey="btnDeleteResource1" />
                            </div>
                        </div>
                        <div class="clear space-sm"></div>
                        <div class="table-responsive">
                            <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdManualEntryRequest" Skin="Hay"
                                ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                                <ContextMenu FeatureGroupID="rfContextMenu">
                                </ContextMenu>
                            </telerik:RadFilter>
                            <telerik:RadGrid runat="server" ID="dgrdManualEntryRequest" AutoGenerateColumns="False"
                                PageSize="25" AllowPaging="True" AllowSorting="True" GridLines="None"
                                Width="100%" CellSpacing="0" meta:resourcekey="dgrdManualEntryRequestResource1">
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                    EnableRowHoverStyle="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" DataKeyNames="MoveRequestId,Status,AttachedFile,EmployeeArabicName,StatusNameArabic,ReasonArabicName">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="False"
                                            UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1"></asp:CheckBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn AllowFiltering="False" DataField="MoveRequestId" HeaderText="Emp_no"
                                            SortExpression="Emp_no" UniqueName="MoveRequestId"
                                            Visible="False" FilterControlAltText="Filter MoveRequestId column" meta:resourcekey="GridBoundColumnResource1">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="name" HeaderText="Name"
                                            SortExpression="name" UniqueName="name" FilterControlAltText="Filter name column" meta:resourcekey="GridBoundColumnResource2">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ReasonName" HeaderText="Reason"
                                            SortExpression="ReasonName" UniqueName="ReasonName" FilterControlAltText="Filter ReasonName column" meta:resourcekey="GridBoundColumnResource3">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MoveDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date"
                                            SortExpression="MoveDate" UniqueName="MoveDate" FilterControlAltText="Filter MoveDate column" meta:resourcekey="GridBoundColumnResource4">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MoveTime" DataFormatString="{0:HH:mm}" HeaderText="Time"
                                            SortExpression="MoveTime" UniqueName="MoveTime" FilterControlAltText="Filter MoveTime column" meta:resourcekey="GridBoundColumnResource5">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks"
                                            SortExpression="Remarks" UniqueName="Remarks" FilterControlAltText="Filter Remarks column" meta:resourcekey="GridBoundColumnResource6">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status"
                                            UniqueName="StatusName" FilterControlAltText="Filter StatusName column" meta:resourcekey="GridBoundColumnResource7">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="StatusNameArabic" HeaderText="Status"
                                            UniqueName="StatusNameArabic" Visible="False" FilterControlAltText="Filter StatusNameArabic column" meta:resourcekey="GridBoundColumnResource8">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="False" DataField="EmployeeArabicName"
                                            UniqueName="EmployeeArabicName" Visible="False" FilterControlAltText="Filter EmployeeArabicName column" meta:resourcekey="GridBoundColumnResource9">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="False" DataField="ReasonArabicName"
                                            UniqueName="ReasonArabicName" Visible="False" FilterControlAltText="Filter ReasonArabicName column" meta:resourcekey="GridBoundColumnResource10">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Status"
                                            UniqueName="Status" Visible="False" FilterControlAltText="Filter Status column" meta:resourcekey="GridBoundColumnResource11">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AttachedFile" UniqueName="AttachedFile" Visible="False" FilterControlAltText="Filter AttachedFile column" meta:resourcekey="GridBoundColumnResource12">
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar ID="RadToolBar1" runat="server"
                                            OnButtonClick="RadToolBar1_ButtonClick" Skin="Hay" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                            <Items>
                                                <telerik:RadToolBarButton runat="server" CommandName="FilterRadGrid" ImagePosition="Right"
                                                    ImageUrl="~/images/RadFilter.gif"
                                                    Owner="" Text="Apply filter" meta:resourcekey="RadToolBarButtonResource1">
                                                </telerik:RadToolBarButton>
                                            </Items>
                                        </telerik:RadToolBar>
                                    </CommandItemTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </ContentTemplate>

                </cc1:TabPanel>
                <cc1:TabPanel ID="TabTransactions" runat="server" HeaderText="Daily Transactions" TabIndex="1" meta:resourcekey="TabTransactionsResource1">
                    <ContentTemplate>
                        <div class="table-responsive">
                            <telerik:RadFilter runat="server" ID="TransactionsFilter" FilterContainerID="dgrdTransactions" Skin="Hay"
                                ShowApplyButton="False" meta:resourcekey="TransactionsFilterResource1">
                                <ContextMenu FeatureGroupID="rfContextMenu">
                                </ContextMenu>
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
                                            Visible="False" FilterControlAltText="Filter MoveId column" meta:resourcekey="GridBoundColumnResource13">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="EmployeeName"
                                            SortExpression="name" UniqueName="EmployeeName" FilterControlAltText="Filter EmployeeName column" meta:resourcekey="GridBoundColumnResource14">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ReasonName" HeaderText="Reason"
                                            SortExpression="ReasonName" UniqueName="ReasonName" FilterControlAltText="Filter ReasonName column" meta:resourcekey="GridBoundColumnResource15">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MoveDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date"
                                            SortExpression="MoveDate" UniqueName="MoveDate" FilterControlAltText="Filter MoveDate column" meta:resourcekey="GridBoundColumnResource16">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="MoveTime" DataFormatString="{0:HH:mm}" HeaderText="Time"
                                            SortExpression="MoveTime" UniqueName="MoveTime" FilterControlAltText="Filter MoveTime column" meta:resourcekey="GridBoundColumnResource17">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Reader" HeaderText="Reader"
                                            SortExpression="Reader" UniqueName="Reader" FilterControlAltText="Filter Reader column" meta:resourcekey="GridBoundColumnResource18">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks"
                                            SortExpression="Remarks" UniqueName="Remarks" FilterControlAltText="Filter Remarks column" meta:resourcekey="GridBoundColumnResource19">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="False" DataField="EmployeeArabicName"
                                            UniqueName="EmployeeArabicName" Visible="False" FilterControlAltText="Filter EmployeeArabicName column" meta:resourcekey="GridBoundColumnResource20">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="False" DataField="ReasonArabicName"
                                            UniqueName="ReasonArabicName" Visible="False" FilterControlAltText="Filter ReasonArabicName column" meta:resourcekey="GridBoundColumnResource21">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Status"
                                            UniqueName="Status" Display="False" AllowFiltering="False" FilterControlAltText="Filter Status column" meta:resourcekey="GridBoundColumnResource22">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="StatusName"
                                            UniqueName="StatusName" Display="False" AllowFiltering="False" FilterControlAltText="Filter StatusName column" meta:resourcekey="GridBoundColumnResource23">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="StatusNameArabic"
                                            UniqueName="StatusNameArabic" Display="False" AllowFiltering="False" FilterControlAltText="Filter StatusNameArabic column" meta:resourcekey="GridBoundColumnResource24">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource2">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUpdate" runat="server" Text="Update" OnClick="lnkUpdate_Click" meta:resourcekey="lnkUpdateResource1"></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                         <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource2">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="lnkDelete_Click" meta:resourcekey="lnkDeleteResource1"></asp:LinkButton>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar ID="RadToolBar1" runat="server"
                                            OnButtonClick="RadToolBar2_ButtonClick" Skin="Hay" meta:resourcekey="RadToolBar1Resource2" SingleClick="None">
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
        </asp:View>
        <asp:View ID="viewAddManualEntryRequest" runat="server">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblDate" runat="server" Text="Date" meta:resourcekey="lblDateResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" AutoPostBack="True" Culture="en-US"
                        Width="120px" Enabled="False" meta:resourcekey="RadDatePicker1Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                        </Calendar>
                        <DateInput AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" Width="" LabelWidth="64px">
                            <EmptyMessageStyle Resize="None" />
                            <ReadOnlyStyle Resize="None" />
                            <FocusedStyle Resize="None" />
                            <DisabledStyle Resize="None" />
                            <InvalidStyle Resize="None" />
                            <HoveredStyle Resize="None" />
                            <EnabledStyle Resize="None" />
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="RadDatePicker1"
                        Display="None" ErrorMessage="Please Select Date,The Max Date Allowed is Today" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator7">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblReason" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblReasonResource1">Reason</asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlReason" runat="server" MarkFirstMatch="True" AppendDataBoundItems="True"
                        DropDownStyle="DropDownList" Skin="Vista" CausesValidation="False" meta:resourcekey="ddlReasonResource1" />
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
                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="Profiletitletxt" meta:resourcekey="lblTimeResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadMaskedTextBox ID="rmtToTime2" runat="server" Mask="##:##"
                        Width="30px" Text='<%# DataBinder.Eval(Container,"DataItem.ToTime2") %>' DisplayMask="##:##"
                        LabelCssClass="" Enabled="False" LabelWidth="64px" meta:resourcekey="rmtToTime2Resource1">
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
            <asp:UpdatePanel ID="Update1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblAttachFile" runat="server" Text="Attached File" CssClass="Profiletitletxt" meta:resourcekey="lblAttachFileResource1" />
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
                                <asp:LinkButton ID="lnbLeaveFile" runat="server" Visible="False" Text="View" OnClick="lnkDownloadFile_Click" meta:resourcekey="lnbLeaveFileResource1"></asp:LinkButton>
                                <asp:LinkButton ID="lnbRemove" runat="server" Text="Remove" Visible="False" meta:resourcekey="lnbRemoveResource1" />
                                <asp:Label ID="lblNoAttachedFile" runat="server" Text="No Attached File" Visible="False" meta:resourcekey="lblNoAttachedFileResource1" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks" CssClass="Profiletitletxt" meta:resourcekey="lblRemarksResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Width="220px"
                        Enabled="False" meta:resourcekey="txtremarksResource1"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:Button ID="btnSave" Text="Save" CssClass="button" runat="server"
                        ValidationGroup="grpSave" meta:resourcekey="btnSaveResource1"></asp:Button>
                    <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="button"
                        Text="Clear" meta:resourcekey="btnClearResource1" />
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="button"
                        Text="Cancel" meta:resourcekey="btnCancelResource1" />
                </div>
            </div>
        </asp:View>
    </asp:MultiView>

</asp:Content>


