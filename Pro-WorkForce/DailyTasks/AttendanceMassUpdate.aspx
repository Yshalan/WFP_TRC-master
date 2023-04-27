<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="AttendanceMassUpdate.aspx.vb" Inherits="DailyTasks_AttendanceMassUpdate"
    meta:resourcekey="PageResource1" Culture="Auto" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/MultiEmployeeFilter.ascx" TagName="MultiEmployeeFilter"
    TagPrefix="uc4" %>
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

        .customValidator {
            width: 100%;
            background-color: #ffdfb1;
            display: block;
            color: #222;
            padding: 10px;
            margin: 5px 0;
            border: 1px solid #222;
        }
    </style>
    <script type="text/javascript" language="javascript">
 
        function CheckBoxListSelect(state) {
            var chkBoxList = document.getElementById("<%= cblEmpList.ClientID %>");
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }

    </script>
    <asp:UpdatePanel ID="pnlEmployee" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" HeaderText="Leave Types" runat="server" />
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblDate" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblDateResource1"
                        Text="Date"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" RenderMode="Native" AutoPostBack="True"
                        Culture="en-US" meta:resourcekey="RadDatePicker1Resource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="" AutoPostBack="True">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="RadDatePicker1"
                        Display="None" ErrorMessage="Please Select Date,The Max Date Allowed is Today"
                        meta:resourcekey="RequiredFieldValidator7Resource1" ValidationGroup="ReasonValidation"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" PopupPosition="Right"
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblCompanyResource1"
                        Text="Company"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbBxCompanies" runat="server" AutoPostBack="True" CausesValidation="False"
                        MarkFirstMatch="True" Skin="Vista" Style="width: 350px" ValidationGroup="EmpPermissionGroup"
                        meta:resourcekey="RadCmbBxCompaniesResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvCompanies" runat="server" ControlToValidate="RadCmbBxCompanies"
                        Display="None" InitialValue="--Please Select--" ErrorMessage="Please Select Company"
                        meta:resourcekey="rfvCompaniesResource1" ValidationGroup="ReasonValidation"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceCompanies" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvCompanies" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <uc4:MultiEmployeeFilter ID="MultiEmployeeFilterUC" runat="server" OneventEntitySelected="EntityChanged"
                        OneventWorkGroupSelect="WorkGroupChanged" OneventWorkLocationsSelected="WorkLocationsChanged" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="List Of Employees"
                        meta:resourcekey="Label5Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
                        <asp:CheckBoxList ID="cblEmpList" runat="server" Style="height: 26px" CssClass="checkboxlist"
                            DataTextField="EmployeeName" DataValueField="EmployeeId" meta:resourcekey="cblEmpListResource1">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="col-md-2">
                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                        <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                    <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                        <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                </div>
                <div class="col-md-2">
                    <asp:HyperLink ID="hlViewEmployee" runat="server" Visible="False" meta:resourcekey="hlViewEmployeeResource1"
                        Text="View Org Level Employees "></asp:HyperLink>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:CustomValidator ID="cvEmpListValidation" ErrorMessage="please select at least one employee"
                        ValidationGroup="ReasonValidation" ForeColor="Black" runat="server" CssClass="customValidator"
                        meta:resourcekey="cvEmpListValidationResource1" />
                    <%--ClientValidationFunction="CheckBoxListSelect"--%>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-10">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<% # Eval("PageNo")%>'></asp:LinkButton>|
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblReason" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblReasonResource3">Reason</asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlReason" runat="server" MarkFirstMatch="True" AppendDataBoundItems="True"
                        DropDownStyle="DropDownList" Skin="Vista" CausesValidation="False" meta:resourcekey="ddlReasonResource1" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                        InitialValue="--Please Select--" ErrorMessage="Please Select Reason\Type" ControlToValidate="ddlReason"
                        meta:resourcekey="RequiredFieldValidator4Resource1" ValidationGroup="ReasonValidation"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator4">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks" CssClass="Profiletitletxt"
                        meta:resourcekey="lblRemarksResource3"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" meta:resourcekey="txtremarksResource1"
                        MaxLength="100"></asp:TextBox>
                    <asp:RegularExpressionValidator Display="None" ControlToValidate="txtremarks" ID="RegularExpressionValidator2"
                        ValidationExpression="^{0,100}$" runat="server" ErrorMessage="Maximum 100 characters allowed."
                        ValidationGroup="ReasonValidation"></asp:RegularExpressionValidator><%--"^[\s\S]{0,100}$"--%>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                        TargetControlID="RegularExpressionValidator2">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" Text="Save" CssClass="button" runat="server" meta:resourcekey="ibtnSaveResource1"
                        ValidationGroup="ReasonValidation" OnClientClick="Page_ClientValidate(); return ValidateTime();"></asp:Button>
                    <asp:Button ID="ibtnDelete" CssClass="button" Text="Delete" runat="server" OnClientClick="return confirm('Are you sure you want to delete?')"
                        CausesValidation="False" meta:resourcekey="ibtnDeleteResource3" Visible="False"></asp:Button>
                    <asp:Button ID="ibtnClear" runat="server" CssClass="button" Text="Clear" CausesValidation="False"
                        meta:resourcekey="ibtnClearResource3" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgEmpAtt" Skin="Hay"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid runat="server" ID="dgEmpAtt" AutoGenerateColumns="False" PageSize="15"
                        AllowPaging="True" AllowSorting="True" GridLines="None" meta:resourcekey="dgrRamadanPeriodResource1"
                        Width="100%">
                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                    UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="moveid" SortExpression="Emp_no" HeaderText="Emp_no"
                                    meta:resourcekey="TemplateFieldResource2" Visible="False" UniqueName="moveid" />
                                <telerik:GridBoundColumn DataField="name" SortExpression="name" HeaderText="Name"
                                    meta:resourcekey="TemplateFieldResource3" UniqueName="name" />
                                <telerik:GridBoundColumn DataField="ReasonName" SortExpression="ReasonName" HeaderText="Reason"
                                    meta:resourcekey="BoundFieldResource1" UniqueName="ReasonName" />
                                <telerik:GridBoundColumn DataField="MoveDate" SortExpression="M_DATE" HeaderText="Date"
                                    DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="BoundFieldResource2" UniqueName="MoveDate" />
                                <telerik:GridBoundColumn DataField="Remarks" SortExpression="Remarks" HeaderText="Remarks"
                                    meta:resourcekey="BoundFieldResource4" UniqueName="Remarks" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="pnlEmployee">
        <ProgressTemplate>
            <div class="modal">
                <div class="center">
                    <asp:Image ID="imgLoading" runat="server" ImageUrl="~/images/loading.gif" meta:resourcekey="imgLoadingResource1" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
