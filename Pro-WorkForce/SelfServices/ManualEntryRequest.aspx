<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="ManualEntryRequest.aspx.vb" Inherits="SelfServices_ManualEntryRequest" Theme="SvTheme"
    meta:resourcekey="PageResource1" UICulture="auto" %>

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
            window.location = "../SelfServices/ManualEntryRequest.aspx";
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
    </script>


    <uc1:PageHeader ID="EmpManualEntryRequest" runat="server" HeaderText="Employee Manual Entry Request" />
    <asp:MultiView ID="mvEmpLeaverequest" runat="server">
        <asp:View ID="viewManualEntryRequests" runat="server">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblstatus" runat="server" CssClass="Profiletitletxt" Text="Status"
                        meta:resourcekey="lblstatusResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlStatus" AutoPostBack="True" runat="server" MarkFirstMatch="True"
                        meta:resourcekey="ddlStatusResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvStatus" ValidationGroup="Get" runat="server" ControlToValidate="ddlStatus"
                        Display="None" ErrorMessage="Please Select Request Status" InitialValue="--Please Select--"
                        meta:resourcekey="rfvStatusResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvStatus" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblFromDateSearch" runat="server" CssClass="Profiletitletxt" Text="From Date"
                        meta:resourcekey="lblFromDateSearchResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpFromDateSearch" runat="server" Culture="en-US" Width="180px"
                        meta:resourcekey="dtpFromDateSearchResource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
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
                    <asp:Label ID="lblToDate" runat="server" CssClass="Profiletitletxt" Text="To Date"
                        meta:resourcekey="lblToDateResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpToDateSearch" runat="server" Culture="en-US" Width="180px"
                        meta:resourcekey="dtpToDateSearchResource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
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
                    <asp:Button ID="btnGet" runat="server" Text="Get" CssClass="button" ValidationGroup="Get"
                        meta:resourcekey="btnGetResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnRequestManualEntry" runat="server" Text="Request Manual Entry"
                        CssClass="button" meta:resourcekey="btnRequestManualEntryResource1" />

                    <asp:Button ID="btnDelete" runat="server" Text="Delete Manual Entry Request" OnClientClick="return ValidateDelete();" CssClass="button"
                        meta:resourcekey="btnDeleteResource1" />
                </div>
            </div>
            <div class="clear space-sm"></div>
            <div class="table-responsive">

                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgEmpAtt" Skin="Hay"
                    ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                <telerik:RadGrid runat="server" ID="dgrdManualEntryRequest" AutoGenerateColumns="False"
                    PageSize="25" AllowPaging="True" AllowSorting="True" GridLines="None"
                    Width="100%" meta:resourcekey="dgrdManualEntryRequestResource1">
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                        EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" DataKeyNames="MoveRequestId,Status,AttachedFile,EmployeeArabicName,StatusNameArabic,ReasonArabicName">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;"></asp:CheckBox>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="MoveRequestId" HeaderText="Emp_no"
                                meta:resourcekey="GridBoundColumnResource1" SortExpression="Emp_no" UniqueName="MoveRequestId"
                                Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="name" HeaderText="Name" meta:resourcekey="GridBoundColumnResource2"
                                SortExpression="name" UniqueName="name">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ReasonName" HeaderText="Reason" meta:resourcekey="GridBoundColumnResource3"
                                SortExpression="ReasonName" UniqueName="ReasonName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MoveDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date"
                                meta:resourcekey="GridBoundColumnResource4" SortExpression="MoveDate" UniqueName="MoveDate">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MoveTime" DataFormatString="{0:HH:mm}" HeaderText="Time"
                                meta:resourcekey="GridBoundColumnResource5" SortExpression="MoveTime" UniqueName="MoveTime">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks" meta:resourcekey="GridBoundColumnResource6"
                                SortExpression="Remarks" UniqueName="Remarks">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status" meta:resourcekey="GridBoundColumnResource7"
                                UniqueName="StatusName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="StatusNameArabic" HeaderText="Status" meta:resourcekey="GridBoundColumnResource8"
                                UniqueName="StatusNameArabic" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource9"
                                UniqueName="EmployeeArabicName" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="ReasonArabicName" meta:resourcekey="GridBoundColumnResource10"
                                UniqueName="ReasonArabicName" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Status" meta:resourcekey="GridBoundColumnResource11"
                                UniqueName="Status" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AttachedFile" UniqueName="AttachedFile" Visible="False">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <CommandItemTemplate>
                            <telerik:RadToolBar ID="RadToolBar1" runat="server" meta:resourcekey="RadToolBar1Resource1"
                                OnButtonClick="RadToolBar1_ButtonClick" Skin="Hay">
                                <Items>
                                    <telerik:RadToolBarButton runat="server" CommandName="FilterRadGrid" ImagePosition="Right"
                                        ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource1"
                                        Owner="" Text="Apply filter">
                                    </telerik:RadToolBarButton>
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </asp:View>
        <asp:View ID="viewAddManualEntryRequest" runat="server">
            <asp:UpdatePanel ID="Update1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblDate" runat="server" Text="Date" meta:resourcekey="lblDateResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="RadDatePicker1" runat="server" AutoPostBack="True" Culture="en-US"
                                Width="120px" meta:resourcekey="RadDatePicker1Resource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                    LabelCssClass="" Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="RadDatePicker1"
                                Display="None" ErrorMessage="Please Select Date,The Max Date Allowed is Today"
                                meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
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
                                DropDownStyle="DropDownList" Skin="Vista" CausesValidation="False"
                                meta:resourcekey="ddlReasonResource1" />
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
                            <asp:Label ID="lblAttachFile" runat="server" Text="Attached File" CssClass="Profiletitletxt"
                                meta:resourcekey="lblAttachFileResource1" />
                        </div>
                        <div class="col-md-4">
                            <div class="input-group">
                                <span class="input-group-btn">
                                    <span class="btn btn-default" onclick="$(this).parent().find('input[type=file]').click();">Browse</span>
                                    <asp:FileUpload ID="fuAttachFile" runat="server" meta:resourcekey="fuAttachFileResource1" name="uploaded_file" onchange="$(this).parent().parent().find('.form-control').html($(this).val().split(/[\\|/]/).pop());" Style="display: none;" type="file" />
                                </span>
                                <span class="form-control"></span>
                            </div>
                            <div class="veiw_remove">
                                <asp:LinkButton ID="lnbLeaveFile" runat="server" Visible="False" Text="View" meta:resourcekey="lblViewResource1" OnClick="lnkDownloadFile_Click">
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnbRemove" runat="server" Text="Remove" Visible="False" meta:resourcekey="lnbRemoveResource1" />
                                <asp:Label ID="lblNoAttachedFile" runat="server" Text="No Attached File" Visible="False"
                                    meta:resourcekey="lblNoAttachedFileResource1" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="Profiletitletxt" meta:resourcekey="lblTimeResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadMaskedTextBox ID="rmtToTime2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                Width="30px" Text='<%# DataBinder.Eval(Container,"DataItem.ToTime2") %>' DisplayMask="##:##"
                                LabelCssClass="" meta:resourcekey="rmtToTime2Resource1">
                            </telerik:RadMaskedTextBox>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="None"
                                ErrorMessage="Please Time" InitialValue="00:00" ControlToValidate="rmtToTime2"
                                meta:resourcekey="RequiredFieldValidator8Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True"
                                TargetControlID="RequiredFieldValidator8">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks" CssClass="Profiletitletxt"
                                meta:resourcekey="lblRemarksResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Width="220px" meta:resourcekey="txtremarksResource1"></asp:TextBox>
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
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>
    </asp:MultiView>

</asp:Content>

