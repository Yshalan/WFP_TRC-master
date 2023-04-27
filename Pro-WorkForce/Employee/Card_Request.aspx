<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Card_Request.aspx.vb" Inherits="Employee_Card_Request" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>

            <uc1:PageHeader ID="PageHeader1" runat="server" />
            <div class="row">
                <div class="col-md-12">
                    <uc2:PageFilter ID="EmployeeFilterUC" runat="server" HeaderText="Employee Filter"
                        ValidationGroup="grpCardPrinting" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblReason" runat="server" Text="Reason"
                        meta:resourcekey="lblReasonResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlReason" runat="server" AppendDataBoundItems="True"
                        MarkFirstMatch="True" AutoPostBack="True" Skin="Vista" meta:resourcekey="ddlReasonResource1" />
                    <asp:RequiredFieldValidator ID="rfvReason" runat="server" ControlToValidate="ddlReason"
                        Display="None" ErrorMessage="Please Select Reason" InitialValue="--Please Select--" ValidationGroup="grpCardPrinting"
                        meta:resourcekey="rfvReasonResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceReason" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvReason" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblDesignation" runat="server" CssClass="Profiletitletxt"
                        Text="Designation"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlDesignation" Enabled ="false" runat="server" AutoPostBack="True" CausesValidation="False"
                        MarkFirstMatch="True" Skin="Vista" Style="width: 350px" ValidationGroup="grpCardPrinting"
                        meta:resourcekey="RadCmbBxCompaniesResource1" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged">
                    </telerik:RadComboBox>

                    <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ControlToValidate="ddlDesignation"
                        Display="None" ErrorMessage="Please Select Designation" meta:resourcekey="rfvCompaniesResource1"
                        ValidationGroup="VGCards"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvDesignation" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblCardDesign" runat="server" CssClass="Profiletitletxt" Text="Card Type"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlCardDesign" AllowCustomText="false" MarkFirstMatch="true"
                        Skin="Vista" runat="server" ValidationGroup="VGCards">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Card Type"
                        ControlToValidate="ddlCardDesign" ValidationGroup="grpCardPrinting" InitialValue="--Please Select--"
                        Display="None" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="RequiredFieldValidator2" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>

            <div class="row" id="trOther" runat="server" visible="False">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblOther" runat="server" meta:resourcekey="lblOtherResource1" />
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtOther" runat="server" TextMode="MultiLine" meta:resourcekey="txtOtherResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvOther" runat="server" ControlToValidate="txtOther"
                        Display="None" ErrorMessage="Please Insert Other Reason" ValidationGroup="grpCardPrinting"
                        Enabled="False" meta:resourcekey="rfvOtherResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceOther" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvOther" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="grpCardPrinting"
                        meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button"
                        Text="Delete" meta:resourcekey="btnDeleteResource1" OnClientClick="return ValidateDelete();" />
                    <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="button"
                        Text="Clear" meta:resourcekey="btnClearResource1" />
                </div>
            </div>


            <div class="row">
                <div class="col-md-2">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdCardRequests"
                        Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadGrid ID="dgrdCardRequests" runat="server" AllowSorting="True" AllowPaging="True" OnNeedDataSource="dgrdCardRequests_NeedDataSource"
                        Width="100%" PageSize="15" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        ShowFooter="True" meta:resourcekey="dgrdCardRequestsResource1">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="ReasonId,Status,CardRequestId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                    UniqueName="TemplateColumn1">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" UniqueName="EmployeeNo"
                                    meta:resourcekey="GridBoundColumnResource1" />
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                                    meta:resourcekey="GridBoundColumnResource2" />
                                <telerik:GridBoundColumn DataField="CardTypeEn" HeaderText="Card Type" UniqueName="CardTypeEn" />

                                <telerik:GridBoundColumn DataField="ReasonId" HeaderText="Reason" UniqueName="ReasonId"
                                    meta:resourcekey="GridBoundColumnResource3" />
                                <telerik:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="Status"
                                    meta:resourcekey="GridBoundColumnResource4" />
                                <telerik:GridBoundColumn DataField="CardRequestId" AllowFiltering="False" Visible="False"
                                    UniqueName="CardRequestId" meta:resourcekey="GridBoundColumnResource5" />
                                <telerik:GridBoundColumn DataField="FK_EmployeeId" AllowFiltering="False" Visible="False"
                                    UniqueName="FK_EmployeeId" meta:resourcekey="GridBoundColumnResource6" />
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton runat="server" CausesValidation="False" CommandName="FilterRadGrid"
                                            ImagePosition="Right" ImageUrl="~/images/RadFilter.gif" Owner="" Text="Apply filter"
                                            meta:resourcekey="RadToolBarButtonResource1">
                                        </telerik:RadToolBarButton>
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                        </MasterTableView><SelectedItemStyle ForeColor="Maroon" />
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">

        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdCardRequests.ClientID %>");
                    var masterTable = grid.get_masterTableView();
                    var value = false;
                    for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                        var gridItemElement = masterTable.get_dataItems()[i].findElement("chk");
                        if (gridItemElement.checked) {
                            value = true;
                        }
                    }
                    if (value === false) {
                        ShowMessage("<%= Resources.Strings.ErrorDeleteRecourd %>");
                    }
                    return value;
                }
    </script>
</asp:Content>
