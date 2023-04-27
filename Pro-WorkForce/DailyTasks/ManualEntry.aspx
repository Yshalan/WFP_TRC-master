<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ManualEntry.aspx.vb" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    Inherits="Admin_ManualEntry" meta:resourcekey="PageResource3" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
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
                ShowMessage("Invalid time. Hour can not be hours less than 0.");
                statusH = false;
                return false;
            }


            var minval = strval.substring(3, 5);

            if (minval > 59) {
                alert("Invalid time. Minute can not be more than 59.");
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
    <asp:UpdatePanel ID="pnlEmployee" runat="server">
        <ContentTemplate>
            <div class="updateprogressAssign">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="pnlEmployee">
                    <ProgressTemplate>
                        <%--                        <div id="tblLoading" style="width: 100%; height: 100%; z-index: 100002 !important; text-align: center; position: fixed; left: 0px; top: 0px; background-size: cover; background-image: url('../images/Grey_fade.png');">
                            <asp:Image ID="imgLoading" runat="server" ImageAlign="Middle" ImageUrl="~/images/STS_Loading.gif" Width="250px" Height="250px" />
                        </div>--%>
                        <%-- <asp:Image ID="imgLoading" runat="server" ImageAlign="AbsBottom" ImageUrl="~/images/STS_Loading.gif"
                            Width="200px" Height="200px" />--%>
                        <div id="tblLoading" style="width: 100%; height: 100%; z-index: 100002 !important; text-align: center; position: fixed; left: 0px; top: 0px; background-size: cover; background-image: url('../images/Grey_fade.png');">
                            <asp:Image ID="imgLoading" runat="server" ImageAlign="Middle" ImageUrl="~/images/STS_Loading.gif" Width="250px" Height="250px" Style="margin-top: 20%;" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <uc1:PageHeader ID="PageHeader1" HeaderText="Leave Types" runat="server" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblDateResource1"
                        Text="Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" AutoPostBack="true" Culture="English (United States)"
                        meta:resourcekey="RadDatePicker1Resource1" Width="120px">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
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
                    <asp:Label ID="lblReason" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblReasonResource3">Reason</asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlReason" runat="server" MarkFirstMatch="true" AppendDataBoundItems="True"
                        DropDownStyle="DropDownList" Skin="Vista" CausesValidation="true"
                        meta:resourcekey="ddlReasonResource1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                        ValidationGroup="grpSave" InitialValue="--Please Select--" ErrorMessage="Please Select Type"
                        ControlToValidate="ddlReason" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator4">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="Profiletitletxt" meta:resourcekey="lblTimeResource3"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadMaskedTextBox ID="rmtToTime2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                        CssClass="RadMaskedTextBox" Text='<%# DataBinder.Eval(Container,"DataItem.ToTime2") %>' DisplayMask="##:##"
                        LabelCssClass="" meta:resourcekey="rmtToTime2Resource1">
                    </telerik:RadMaskedTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="None"
                        ErrorMessage="Please Select Time" ControlToValidate="rmtToTime2" ValidationGroup="grpSave"
                        meta:resourcekey="RequiredFieldValidator8Resource1"><%--InitialValue="00:00"--%></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator8">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label1" is="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
                        meta:resourcekey="lblAttachFileResource1" />
                </div>
                <div class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-btn"><span class="btn btn-default" onclick="$(this).parent().find('input[type=file]').click();">Browse</span>
                            <asp:FileUpload ID="fuAttachFile" runat="server" meta:resourcekey="fuAttachFileResource1"
                                name="uploaded_file" onchange="$(this).parent().parent().find('.form-control').html($(this).val().split(/[\\|/]/).pop());"
                                Style="display: none;" type="file" />
                        </span><span class="form-control"></span>
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
                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks" CssClass="Profiletitletxt"
                        meta:resourcekey="lblRemarksResource3"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" meta:resourcekey="txtremarksResource1"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revRemarks" runat="server" ControlToValidate="txtRemarks"
                        ValidationExpression="^[\s\S]{0,255}$" ValidationGroup="grpSave"
                        Display="None"
                        meta:resourcekey="revRemarksResource1">
                    </asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="vceRemarks" runat="server" Enabled="True"
                        TargetControlID="revRemarks">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>




            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" Text="Save" CssClass="button" runat="server" meta:resourcekey="ibtnSaveResource1"
                        OnClientClick="Page_ClientValidate(); return ValidateTime();" ValidationGroup="grpSave"></asp:Button>
                    <asp:Button ID="ibtnDelete" CssClass="button" Text="Delete" runat="server" OnClientClick="return ValidateDelete()"
                        CausesValidation="False" meta:resourcekey="ibtnDeleteResource3"></asp:Button>
                    <asp:Button ID="ibtnClear" runat="server" CssClass="button" Text="Clear" CausesValidation="False"
                        meta:resourcekey="ibtnClearResource3" />
                </div>
            </div>

            <div class="row">
                <div class="table-responsive">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgEmpAtt" Skin="Hay"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid runat="server" ID="dgEmpAtt" AutoGenerateColumns="False" PageSize="15"
                        AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="false"
                        GridLines="None" meta:resourcekey="dgrRamadanPeriodResource1" Width="100%">
                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="moveid,IsFromMobile,MobileCoordinates,EmployeeArabicName,ReasonArabicName">
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                    UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="moveid" SortExpression="Emp_no" HeaderText="Emp_no"
                                    meta:resourcekey="TemplateFieldResource2" Visible="false" AllowFiltering="false" />
                                <telerik:GridBoundColumn DataField="name" SortExpression="name" HeaderText="Name"
                                    meta:resourcekey="TemplateFieldResource3" />
                                <telerik:GridBoundColumn DataField="ReasonName" SortExpression="ReasonName" HeaderText="Reason"
                                    meta:resourcekey="BoundFieldResource1" />
                                <telerik:GridBoundColumn DataField="MoveDate" SortExpression="M_DATE" HeaderText="Date"
                                    DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="BoundFieldResource2" />
                                <telerik:GridBoundColumn DataField="MoveTime" SortExpression="M_Time" HeaderText="Time"
                                    DataFormatString="{0:HH:mm}" meta:resourcekey="BoundFieldResource3" />
                                <telerik:GridBoundColumn DataField="Remarks" SortExpression="Remarks" HeaderText="Remarks"
                                    meta:resourcekey="BoundFieldResource4" />
                                <telerik:GridBoundColumn DataField="IsFromMobile" Visible="false" AllowFiltering="false" />
                                <telerik:GridBoundColumn DataField="MobileCoordinates" SortExpression="MobileCoordinates"
                                    HeaderText="Mobile Punch" meta:resourcekey="TemplateFieldResource5" />
                                <telerik:GridBoundColumn DataField="EmployeeArabicName" Visible="false" AllowFiltering="false" />
                                <telerik:GridBoundColumn DataField="ReasonArabicName" Visible="false" UniqueName="ReasonArabicName"
                                    AllowFiltering="false" />
                            </Columns>
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
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
            <%--<asp:GridView ID="dgEmpAtt" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                AllowSorting="True" CssClass="GridViewStyle" meta:resourcekey="dgEmpAttResource1"
                PageSize="25">
                <Columns>
                    <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" CssClass="Checkbox" meta:resourcekey="chkResource1" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Emp_no" SortExpression="Emp_no" Visible="False" meta:resourcekey="TemplateFieldResource2">
                        <ItemTemplate>
                            <asp:Label ID="lblmoveid" runat="server" CausesValidation="False" Text='<%# DataBinder.Eval(Container,"DataItem.moveid") %>'
                                meta:resourcekey="lblmoveidResource1"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" SortExpression="Name" meta:resourcekey="TemplateFieldResource3">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblEMP_NO" runat="server" CausesValidation="False" OnClick="lbltbl_id_Click"
                                Text='<%# DataBinder.Eval(Container,"DataItem.name") %>' meta:resourcekey="lblEMP_NOResource1"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ReasonName" SortExpression="ReasonName" HeaderText="Reason"
                        meta:resourcekey="BoundFieldResource1" />
                    <asp:BoundField DataField="MoveDate" SortExpression="M_DATE" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"
                        meta:resourcekey="BoundFieldResource2" />
                    <asp:BoundField DataField="MoveTime" SortExpression="M_Time" HeaderText="Time" DataFormatString="{0:HH:mm}"
                        meta:resourcekey="BoundFieldResource3" />
                    <asp:BoundField DataField="Remarks" SortExpression="Remarks" HeaderText="Remarks"
                        meta:resourcekey="BoundFieldResource4" />
                </Columns>
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <PagerStyle CssClass="PagerStyle" />
                <RowStyle CssClass="RowStyle" />
            </asp:GridView>--%>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="lnbLeaveFile" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">

        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgEmpAtt.ClientID %>");
            var masterTable = grid.get_masterTableView();
            var value = false;
            var confirmation = undefined;
            for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                var gridItemElement = masterTable.get_dataItems()[i].findElement("chk");
                if (gridItemElement.checked) {
                    if (confirm('Are you sure you want to delete?')) {
                        value = true;
                        confirmation = true;
                    } else {
                        confirmation = false;
                    }

                }
            }
            if (value === false && confirmation == undefined) {
                ShowMessage("<%= Resources.Strings.ErrorDeleteRecourd %>");
            }
            return value;
        }
    </script>
</asp:Content>
