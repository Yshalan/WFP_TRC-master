<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserPrivilege_LogicalGroup.ascx.vb"
    Inherits="Employee_UserControls_UserPrevileges_LogicalGroup" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Admin/UserControls/EmployeeFilter.ascx" TagName="Emp_Filter"
    TagPrefix="uc1" %>

<script type="text/javascript" language="javascript">
    function CheckBoxListSelectLG(state) {
        var chkBoxList = document.getElementById("<%= cblLGlist.ClientID %>");
        var chkBoxCount = chkBoxList.getElementsByTagName("input");
        for (var i = 0; i < chkBoxCount.length; i++) {
            chkBoxCount[i].checked = state;
        }
        return false;
    }
</script>

<style type="text/css">
    .style1
    {
    }

    .td1
    {
        padding-bottom: 20px;
    }

    .style2
    {
        height: 20px;
        width: 495px;
    }

    .style3
    {
        padding-bottom: 20px;
        height: 20px;
    }

    .style6
    {
        padding-bottom: 20px;
        width: 495px;
    }
</style>
<asp:UpdatePanel ID="Upanel1" runat="server">
    <ContentTemplate>

        <div class="row">
            <div class="col-md-12">
                <uc1:Emp_Filter ID="objEmp_FilterLogical" runat="server" ValidationGroup="grpSave_LogicalGrp" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" Text="Company"
                    meta:resourcekey="lblCompanyResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadComboBox ID="RadCmbBxCompaniesLogical" MarkFirstMatch="True" Skin="Vista"
                    runat="server" AutoPostBack="True" meta:resourcekey="RadCmbBxCompaniesLogicalResource1">
                </telerik:RadComboBox>

                <asp:RequiredFieldValidator ID="RFVCompanyLogical" ValidationGroup="grpSave_LogicalGrp"
                    runat="server" InitialValue="--Please Select--" ControlToValidate="RadCmbBxCompaniesLogical"
                    Display="None" ErrorMessage="Please Select Company" meta:resourcekey="RFVCompanyLogicalResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="VCECompanyLogical" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="RFVCompanyLogical" WarningIconImageUrl="~/images/warning1.png"
                    Enabled="True">
                </cc1:ValidatorCalloutExtender>
            </div>
        </div>

        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="List Of Logical Group(s)"
                    meta:resourcekey="Label5Resource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
                    <asp:CheckBoxList ID="cblLGlist" runat="server" Style="height: 26px" CssClass="checkboxlist"
                        DataTextField="GroupName" DataValueField="GroupId" meta:resourcekey="cblLGlistResource1">
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="col-md-2">
                <a href="javascript:void(0)" onclick="CheckBoxListSelectLG(true)" style="font-size: 8pt">
                    <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                <a href="javascript:void(0)" onclick="CheckBoxListSelectLG(false)" style="font-size: 8pt">
                    <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
            </div>
            <div class="col-md-2">
                <asp:HyperLink ID="hlViewEmployee" runat="server" Visible="False" meta:resourcekey="hlViewEmployeeResource1"
                    Text="View Logical Group(s) "></asp:HyperLink>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
            </div>
            <div class="col-md-4">
                <asp:CustomValidator ID="cvLGValidation" ErrorMessage="please select at least one logical group"
                    ValidationGroup="grpSave_LogicalGrp" ForeColor="Black" runat="server" CssClass="customValidator"
                    meta:resourcekey="cvLGValidationResource1" />

            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
            </div>
            <div class="col-md-10">
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("PageNo") %>' meta:resourcekey="LinkButton1Resource1"></asp:LinkButton>|
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="grpSave_LogicalGrp"
                    CssClass="button" meta:resourcekey="btnSaveResource1" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CausesValidation="False"
                    CssClass="button" meta:resourcekey="btnDeleteResource1" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" CssClass="button"
                    meta:resourcekey="btnClearResource1" />
            </div>
        </div>
        <div class="row">
            <div class="table-responsive">
                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource2" />
                <div class="filterDiv">
                    <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdManagers"
                        ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1">
                    </telerik:RadFilter>
                </div>
                <telerik:RadGrid ID="dgrdManagers" runat="server" AllowSorting="True" AllowPaging="True"
                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                    PageSize="25" ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdManagersResource2">
                    <SelectedItemStyle ForeColor="Maroon" />
                    <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top"
                        AutoGenerateColumns="False" DataKeyNames="id,CompanyId,EmployeeId,GroupId">
                        <CommandItemTemplate>
                            <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                <Items>
                                    <telerik:RadToolBarButton Text="Apply Filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                        ImagePosition="Right" meta:resourcekey="RadToolBarButtonResource1" runat="server" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="False" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource2" UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource2" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="CompanyId" UniqueName="CompanyId" Visible="False"
                                FilterControlAltText="Filter CompanyId column" meta:resourcekey="GridBoundColumnResource8" AllowFiltering="False" />
                            <telerik:GridBoundColumn DataField="GroupId" UniqueName="GroupId" Visible="False"
                                FilterControlAltText="Filter GroupId column" meta:resourcekey="GridBoundColumnResource9" AllowFiltering="False" />
                            <telerik:GridBoundColumn DataField="EmployeeId" UniqueName="EmployeeId" Visible="False"
                                FilterControlAltText="Filter EmployeeId column" meta:resourcekey="GridBoundColumnResource10" AllowFiltering="False" />
                            <telerik:GridBoundColumn DataField="CompanyName" SortExpression="CompanyName" HeaderText="Company Name"
                                meta:resourcekey="GridBoundColumnResource1" FilterControlAltText="Filter CompanyName column" UniqueName="CompanyName" />
                            <telerik:GridBoundColumn DataField="CompanyArabicName" SortExpression="CompanyArabicName"
                                HeaderText="Company Arabic Name" meta:resourcekey="GridBoundColumnResource2" FilterControlAltText="Filter CompanyArabicName column" UniqueName="CompanyArabicName" />
                            <telerik:GridBoundColumn DataField="GroupName" SortExpression="GroupName" HeaderText="Logical Group Name"
                                meta:resourcekey="GridBoundColumnResource3" FilterControlAltText="Filter GroupName column" UniqueName="GroupName" />
                            <telerik:GridBoundColumn DataField="GroupArabicName" SortExpression="GroupArabicName"
                                HeaderText="Logical Group Arabic Name" meta:resourcekey="GridBoundColumnResource4" FilterControlAltText="Filter GroupArabicName column" UniqueName="GroupArabicName" />
                            <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                meta:resourcekey="GridBoundColumnResource7" FilterControlAltText="Filter EmployeeName column" UniqueName="EmployeeName" />
                            <telerik:GridBoundColumn DataField="EmployeeArabicName" SortExpression="EmployeeArabicName"
                                HeaderText="Employee Arabic Name" meta:resourcekey="GridBoundColumnResource6" FilterControlAltText="Filter EmployeeArabicName column" UniqueName="EmployeeArabicName" />
                            <telerik:GridBoundColumn DataField="Id" SortExpression="Id" Visible="False" AllowFiltering="False"
                                meta:resourcekey="GridBoundColumnResource5" UniqueName="Id" FilterControlAltText="Filter Id column" />
                        </Columns>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    </MasterTableView>
                    <GroupingSettings CaseSensitive="False" />
                </telerik:RadGrid>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
