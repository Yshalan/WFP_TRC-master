<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="NursingPermissionRequest.aspx.vb" Inherits="SelfServices_NursingPermissionRequest"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">

        function DateSelected(sender, eventArgs) {
            var dt = sender.get_selectedDate();
            var myDate = new Date(sender.get_selectedDate().format("MM/dd/yyyy"));
            var hdnNurdingDay = document.getElementById("<%= hdnNurdingDay.ClientID %>");
            var nursingDay = parseInt(hdnNurdingDay.value);
            myDate.setDate(myDate.getDate() + nursingDay);
            var datepicker = $find("<%= dtpEndDatePerm.ClientID %>");
            datepicker.set_selectedDate(myDate);
        }

        function RefreshPage() {
            window.location = "../SelfServices/NursingPermissionRequest.aspx";

        }

        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdEmpPermissionRequest.ClientID%>");
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <uc1:PageHeader ID="EmpLeaveRequestHeader" runat="server" HeaderText="Employee Nursing Permission Request" />
    <asp:MultiView ID="mvEmpPermissionRequest" runat="server">
        <asp:View ID="viewPermissionRequests" runat="server">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblstatus" runat="server" CssClass="Profiletitletxt" Text="Status"
                        meta:resourcekey="lblstatusResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlStatus" runat="server" MarkFirstMatch="True" Width="200px"
                        meta:resourcekey="ddlStatusResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvStatus" ValidationGroup="Get" runat="server" ControlToValidate="ddlStatus"
                        Display="None" ErrorMessage="Please Select Permission Status" InitialValue="--Please Select--"
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
                    <telerik:RadDatePicker ID="dtpFromDateSearch" runat="server" Culture="en-US"
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
                    <telerik:RadDatePicker ID="dtpToDateSearch" runat="server" Culture="en-US"
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
                    <cc1:ValidatorCalloutExtender TargetControlID="CompareValidator2" ID="ValidatorCalloutExtender4"
                        runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12 text-center ">
                    <asp:Button ID="btnGet" runat="server" Text="Get" CssClass="button" ValidationGroup="Get"
                        meta:resourcekey="btnGetResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center ">
                    <asp:Button ID="btnRequestPermission" runat="server" Text="Request Permission" CssClass="button"
                        meta:resourcekey="btnRequestPermissionResource1" />

                    <asp:Button ID="btnDelete" runat="server" Text="Delete Permission" OnClientClick="return ValidateDelete();" CssClass="button"
                        meta:resourcekey="btnDeleteResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive ">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdEmpPermissionRequest"
                        Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="dgrdEmpPermissionRequest" runat="server" AllowSorting="True"
                        PageSize="15" AllowPaging="True" GridLines="None" ShowStatusBar="True"
                        AllowMultiRowSelection="True" ShowFooter="True" meta:resourcekey="dgrdEmpPermissionRequestResource1">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top" DataKeyNames="PermissionRequestId,FK_StatusId,AttachedFile">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                    UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="PermDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="From Date"
                                    SortExpression="PermDate" UniqueName="PermDate" meta:resourcekey="GridBoundColumnResource1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PermEndDate" DataFormatString="{0:dd/MM/yyyy}"
                                    HeaderText="To Date" SortExpression="PermEndDate" UniqueName="PermEndDate" meta:resourcekey="GridBoundColumnResource2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="PermissionRequestId" SortExpression="PermissionRequestId"
                                    UniqueName="PermissionRequestId" Visible="False" meta:resourcekey="GridBoundColumnResource3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FK_PermId" HeaderText="Perm Id" SortExpression="FK_PermId"
                                    UniqueName="FK_PermId" Visible="False" meta:resourcekey="GridBoundColumnResource4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FK_EmployeeId" HeaderText="EmployeeId" SortExpression="FK_EmployeeId"
                                    UniqueName="FK_EmployeeId" Visible="False" meta:resourcekey="GridBoundColumnResource5">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FK_StatusId" UniqueName="FK_StatusId" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource6" />
                                <telerik:GridBoundColumn DataField="AttachedFile" UniqueName="AttachedFile" Visible="False"
                                    meta:resourcekey="GridBoundColumnResource7" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFullDay" UniqueName="IsFullDay"
                                    HeaderText="Fully Day" meta:resourcekey="GridBoundColumnResource8" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="IsFlexible" UniqueName="IsFlexible"
                                    HeaderText="Flexible" meta:resourcekey="GridBoundColumnResource9" />
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="FlexibilePermissionDuration"
                                    UniqueName="FlexibilePermissionDuration" HeaderText="Duration" meta:resourcekey="GridBoundColumnResource10" />
                                <telerik:GridBoundColumn DataField="StatusName" HeaderText="Status" SortExpression="StatusName"
                                    UniqueName="StatusName" meta:resourcekey="GridBoundColumnResource11">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="StatusNameArabic" HeaderText="Status" SortExpression="StatusNameArabic"
                                    UniqueName="StatusNameArabic" meta:resourcekey="GridBoundColumnResource12">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="RejectionReason" HeaderText="Rejection Reason"
                                    UniqueName="RejectionReason" meta:resourcekey="GridBoundColumnResource13">
                                </telerik:GridBoundColumn>
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
            </div>
        </asp:View>
        <asp:View ID="viewAddPermissionRequest" runat="server">
            <asp:UpdatePanel ID="Update1" runat="server">
                <ContentTemplate>

                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblFromDate" runat="server" Text="From"
                                meta:resourcekey="lblFromDateResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="dtpStartDatePerm" runat="server" AllowCustomText="false" Culture="English (United States)"
                                MarkFirstMatch="true" PopupDirection="TopRight"
                                ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpStartDatePermResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" ToolTip="View start date permission" DisplayDateFormat="dd/MM/yyyy"
                                    LabelCssClass="" Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                <ClientEvents OnDateSelected="DateSelected" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqStartDate" runat="server" ControlToValidate="dtpStartDatePerm"
                                Display="None" ErrorMessage="Please select start date" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="reqStartDateResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ExtenderStartDate" runat="server" TargetControlID="reqStartDate"
                                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                            <asp:HiddenField ID="hdnNurdingDay" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblEndDate" runat="server" Text="To" meta:resourcekey="lblEndDateResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="dtpEndDatePerm" AllowCustomText="false" MarkFirstMatch="true"
                                Skin="Vista" runat="server" AutoPostBack="True" Culture="en-US" meta:resourcekey="dtpEndDatePermResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" ToolTip="View end date permission" AutoPostBack="True"
                                    DisplayDateFormat="dd/MM/yyyy" LabelCssClass="" Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqEndDate" runat="server" ControlToValidate="dtpEndDatePerm"
                                Display="None" ErrorMessage="Please select end date" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="reqEndDateResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ExtenderEndDate" runat="server" TargetControlID="reqEndDate"
                                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                            <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dtpStartDatePerm"
                                ControlToValidate="dtpEndDatePerm" ErrorMessage="To Date should be greater than or equal to From Date"
                                Display="None" Operator="GreaterThanEqual" Type="Date" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="CVDateResource1" />
                            <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender2"
                                runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblNursingFlexibleDuration" runat="server" CssClass="Profiletitletxt"
                                Text="Flexible Permission Duration" meta:resourcekey="lblNursingFlexibleDurationResource1" />
                        </div>
                        <div class="col-md-4" id="tdNursingFlexibleDurationPermission" runat="server">
                            <telerik:RadComboBox ID="RadCmbFlixebleDuration" MarkFirstMatch="True" Skin="Vista"
                                runat="server" meta:resourcekey="RadCmbFlixebleDurationResource1">
                                <Items>
                                    <telerik:RadComboBoxItem Text="One Hour" Value="60" meta:resourcekey="RadCmbFlixebleDurationItemResource2"
                                        runat="server" Owner="" />
                                    <telerik:RadComboBoxItem Text="Two Hours" Value="120" meta:resourcekey="RadCmbFlixebleDurationItemResource3"
                                        runat="server" Owner="" />
                                </Items>
                            </telerik:RadComboBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblAllowedTime" runat="server" Text="Allowed Time" CssClass="Profiletitletxt"
                                meta:resourcekey="lblAllowedTimeResource1" />
                        </div>
                        <div class="col-md-8">
                            <asp:RadioButtonList ID="rlstAllowed" runat="server" meta:resourcekey="rlstAllowedResource1"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="Morning" Value="1" meta:resourcekey="MorningResource1" Selected="True">
                                </asp:ListItem>
                                <asp:ListItem Text="Evening" Value="2" meta:resourcekey="EveningResource1">
                                </asp:ListItem>
                                <asp:ListItem Text="Morning & Evening" Value="3" meta:resourcekey="MorningEveningResource1">
                                </asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
                                meta:resourcekey="lblAttachFileResource1" />
                        </div>
                        <div class="col-md-4">
                            <div id="trAttachedFile" runat="server">
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
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblRemarks" runat="server" Text="Remarks"
                                meta:resourcekey="lblRemarksResource1"></asp:Label>
                        </div>
                        <div class="col-md-4" id="trRemarks" runat="server">
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="btnSaveResource1" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button"
                                meta:resourcekey="btnClearResource1" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" CausesValidation="False"
                                meta:resourcekey="btnCancelResource1" />
                        </div>
                    </div>
                    <div class="row" runat="server" id="dvGeneralGuide" style="margin-top: 5px; background-color: #FDF5B8;">
                        <asp:Label ID="lblGeneralGuide" runat="server" CssClass="Profiletitletxt"
                            meta:resourcekey="lblGeneralGuideResource1" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:View>
    </asp:MultiView>

</asp:Content>
