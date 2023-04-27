<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="HR_ManualEntry.aspx.vb" Inherits="Definitions_HR_ManualEntry" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 80px;
        }
    </style>
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

    </script>
    <asp:updatepanel id="pnlEmployee" runat="server">
        <contenttemplate>
          
                            <uc1:PageHeader ID="PageHeader1" HeaderText="Leave Types" runat="server" />

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblDate" runat="server" CssClass="Profiletitletxt" Text="Date" meta:resourcekey="lblDateResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" AutoPostBack="True" Culture="en-US"
                        Width="120px" meta:resourcekey="RadDatePicker1Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="" AutoPostBack="True">
                        </DateInput>
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
                <div class="col-md-12">
                    <uc2:PageFilter ID="EmployeeFilterUC" runat="server" HeaderText="Employee Filter"
                        OneventEmployeeSelect="FillGrid" />
                </div>
            </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblReason" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblReasonResource1">Reason</asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadComboBox ID="ddlReason" runat="server" MarkFirstMatch="True" AppendDataBoundItems="True"
                            DropDownStyle="DropDownList" Skin="Vista" CausesValidation="False" Width="225px"
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
                        <asp:Label ID="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
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
                                <a id="lnbLeaveFile" target="_blank" runat="server" visible="False">
                                    <asp:Label ID="lblView" runat="server" Text="View" meta:resourcekey="lblViewResource1" />
                                </a>
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
                        Text='<%# DataBinder.Eval(Container,"DataItem.ToTime2") %>' DisplayMask="##:##"
                        LabelCssClass="" meta:resourcekey="rmtToTime2Resource1" CssClass="RadMaskedTextBox">
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
                    <div class="col-md-8 text-center">
                        <asp:Button ID="btnSave" Text="Save" CssClass="button" runat="server" OnClientClick="Page_ClientValidate(); return ValidateTime();"
                            ValidationGroup="grpSave" meta:resourcekey="btnSaveResource1"></asp:Button>
                        <asp:Button ID="ibtnDelete" CssClass="button" Text="Delete" runat="server" OnClientClick="return confirm('Are you sure you want to delete?')"
                            CausesValidation="False" meta:resourcekey="ibtnDeleteResource1"></asp:Button>
                        <asp:Button ID="ibtnClear" runat="server" CssClass="button" Text="Clear" CausesValidation="False"
                            meta:resourcekey="ibtnClearResource1" />
                    </div>
                </div>
                <div class="row">
                    <div class="table-responsive">
                        <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgEmpAtt" Skin="Hay"
                            ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                        <telerik:RadGrid runat="server" ID="dgEmpAtt" AutoGenerateColumns="False" PageSize="25"
                             AllowPaging="True" AllowSorting="True" GridLines="None" Width="100%"
                            meta:resourcekey="dgEmpAttResource1">
                            <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" DataKeyNames="MoveRequestId,IsFromMobile,MobileCoordinates,EmployeeArabicName,ReasonArabicName,MoveRequestId">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="MoveRequestId" SortExpression="Emp_no" HeaderText="Emp_no"
                                        Visible="False" AllowFiltering="False" meta:resourcekey="GridBoundColumnResource1"
                                        UniqueName="MoveRequestId" />
                                    <telerik:GridBoundColumn DataField="name" SortExpression="name" HeaderText="Name"
                                        meta:resourcekey="GridBoundColumnResource2" UniqueName="name" />
                                    <telerik:GridBoundColumn DataField="ReasonName" SortExpression="ReasonName" HeaderText="Reason"
                                        meta:resourcekey="GridBoundColumnResource3" UniqueName="ReasonName" />
                                    <telerik:GridBoundColumn DataField="MoveDate" SortExpression="M_DATE" HeaderText="Date"
                                        DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource4"
                                        UniqueName="MoveDate" />
                                    <telerik:GridBoundColumn DataField="MoveTime" SortExpression="M_Time" HeaderText="Time"
                                        DataFormatString="{0:HH:mm}" meta:resourcekey="GridBoundColumnResource5" UniqueName="MoveTime" />
                                    <telerik:GridBoundColumn DataField="Remarks" SortExpression="Remarks" HeaderText="Remarks"
                                        meta:resourcekey="GridBoundColumnResource6" UniqueName="Remarks" />
                                     <telerik:GridBoundColumn DataField="IsRejected" SortExpression="IsRejected" HeaderText="Status"
                                        meta:resourcekey="GridBoundColumnResource11" UniqueName="IsRejected" />
                                    <telerik:GridBoundColumn DataField="IsFromMobile" Visible="False" AllowFiltering="False"
                                        meta:resourcekey="GridBoundColumnResource7" UniqueName="IsFromMobile" />
                                    <telerik:GridBoundColumn DataField="MobileCoordinates" SortExpression="MobileCoordinates"
                                        HeaderText="Mobile Punch" meta:resourcekey="GridBoundColumnResource8" UniqueName="MobileCoordinates" />
                                    <telerik:GridBoundColumn DataField="EmployeeArabicName" Visible="False" AllowFiltering="False"
                                        meta:resourcekey="GridBoundColumnResource9" UniqueName="EmployeeArabicName" />
                                    <telerik:GridBoundColumn DataField="ReasonArabicName" Visible="False" UniqueName="ReasonArabicName"
                                        AllowFiltering="False" meta:resourcekey="GridBoundColumnResource10" />
                                        
                                </Columns>
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
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </div>
                </div>
        </contenttemplate>
        <triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </triggers>
    </asp:updatepanel>
</asp:content>
