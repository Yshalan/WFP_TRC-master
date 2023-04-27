<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserPrivilege_Worklocations.ascx.vb"
    Inherits="Employee_UserControls_UserPrevileges_Worklocations" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Admin/UserControls/EmployeeFilter.ascx" TagName="Emp_Filter"
    TagPrefix="uc1" %>

<script type="text/javascript" language="javascript">
    function CheckBoxListSelectWL(state) {
        var chkBoxList = document.getElementById("<%= cblWLlist.ClientID %>");
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
                <uc1:Emp_Filter ID="objEmp_FilterLocation" runat="server" ValidationGroup="grpSave_WorkLoc" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" Text="Company"
                    meta:resourcekey="lblCompanyResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadComboBox ID="RadCmbBxCompaniesLocation" MarkFirstMatch="True" Skin="Vista"
                    runat="server" AutoPostBack="True" meta:resourcekey="RadCmbBxCompaniesResource1">
                </telerik:RadComboBox>

                <asp:RequiredFieldValidator ID="RFVCompanyLocation" ValidationGroup="grpSave_WorkLoc"
                    runat="server" InitialValue="--Please Select--" ControlToValidate="RadCmbBxCompaniesLocation"
                    Display="None" ErrorMessage="Please Select Company" meta:resourcekey="RFVCompanyResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="VCECompanyLocation" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="RFVCompanyLocation" WarningIconImageUrl="~/images/warning1.png"
                    Enabled="True">
                </cc1:ValidatorCalloutExtender>
            </div>
        </div>
        <div class="row">
            <%--            <div class="col-md-2">
                <asp:Label ID="lblWorkloc" runat="server" CssClass="Profiletitletxt" Text="Work Location"
                    meta:resourcekey="lblWorklocResource1"></asp:Label>
            </div>--%>
            <%-- <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbBxWorkLocation" MarkFirstMatch="True" Skin="Vista"
                        runat="server" AutoPostBack="True" meta:resourcekey="RadCmbBxWorkLocationResource1">
                    </telerik:RadComboBox>
               
                    <asp:RequiredFieldValidator ID="RFVWorkLoc" ValidationGroup="grpSave_WorkLoc" runat="server"
                        InitialValue="--Please Select--" ControlToValidate="RadCmbBxWorkLocation" Display="None"
                        ErrorMessage="Please Select Work Location" meta:resourcekey="RFVWorkLocResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="VCEWorkLoc" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="RFVWorkLoc" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                 </div>--%>

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="List Of WorkLocation(s)"
                        meta:resourcekey="Label5Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
                        <asp:CheckBoxList ID="cblWLlist" runat="server" Style="height: 26px" CssClass="checkboxlist"
                            DataTextField="WorkLocationName" DataValueField="WorkLocationId" meta:resourcekey="cblWLlistResource1">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="col-md-2">
                    <a href="javascript:void(0)" onclick="CheckBoxListSelectWL(true)" style="font-size: 8pt">
                        <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                    <a href="javascript:void(0)" onclick="CheckBoxListSelectWL(false)" style="font-size: 8pt">
                        <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                </div>
                <div class="col-md-2">
                    <asp:HyperLink ID="hlViewEmployee" runat="server" Visible="False" meta:resourcekey="hlViewEmployeeResource1"
                        Text="View WorkLocation(s) "></asp:HyperLink>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:CustomValidator ID="cvWLValidation" ErrorMessage="please select at least one work locations"
                        ValidationGroup="grpSave_WorkLoc" ForeColor="Black" runat="server" CssClass="customValidator"
                        meta:resourcekey="cvWLValidationResource1" />

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
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="grpSave_WorkLoc"
                        CssClass="button" meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CausesValidation="False"
                        CssClass="button" meta:resourcekey="btnDeleteResource1" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" CssClass="button"
                        meta:resourcekey="btnClearResource1" />
                </div>
            </div>
            <div class="table-responsive">
                <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                <div class="filterDiv">
                    <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdManagers"
                        ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1">
                        <ContextMenu FeatureGroupID="rfContextMenu">
                        </ContextMenu>
                    </telerik:RadFilter>
                </div>
                <telerik:RadGrid ID="dgrdManagers" runat="server" AllowSorting="True" AllowPaging="True"
                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                    PageSize="25" ShowFooter="True" meta:resourcekey="dgrdManagersResource1" CellSpacing="0">
                    <SelectedItemStyle ForeColor="Maroon" />
                    <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False"
                        DataKeyNames="EmployeeName,Id,CompanyId,WorkLocationId,EmployeeId">
                        <CommandItemTemplate>
                            <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                <Items>
                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                        ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1"
                                        Owner="" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource2" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="CompanyId" UniqueName="CompanyId" Visible="False"
                                FilterControlAltText="Filter CompanyId column" meta:resourcekey="GridBoundColumnResource8" AllowFiltering="false" />
                            <telerik:GridBoundColumn DataField="WorkLocationId" UniqueName="WorkLocationId" Visible="False"
                                FilterControlAltText="Filter WorkLocationId column" meta:resourcekey="GridBoundColumnResource9" AllowFiltering="false" />
                            <telerik:GridBoundColumn DataField="EmployeeId" UniqueName="EmployeeId" Visible="False"
                                FilterControlAltText="Filter EmployeeId column" meta:resourcekey="GridBoundColumnResource10" AllowFiltering="false" />
                            <telerik:GridBoundColumn DataField="CompanyName" SortExpression="CompanyName" HeaderText="Company Name"
                                meta:resourcekey="GridBoundColumnResource1" UniqueName="CompanyName" FilterControlAltText="Filter CompanyName column" />
                            <telerik:GridBoundColumn DataField="CompanyArabicName" SortExpression="CompanyArabicName"
                                HeaderText="Company Arabic Name" meta:resourcekey="GridBoundColumnResource2"
                                UniqueName="CompanyArabicName" FilterControlAltText="Filter CompanyArabicName column" />
                            <telerik:GridBoundColumn DataField="WorklocationName" SortExpression="WorklocationName"
                                HeaderText="Worklocation Name" meta:resourcekey="GridBoundColumnResource3" UniqueName="WorklocationName" FilterControlAltText="Filter WorklocationName column" />
                            <telerik:GridBoundColumn DataField="WorklocationArabicName" SortExpression="WorklocationArabicName"
                                HeaderText="Worklocation Arabic Name" meta:resourcekey="GridBoundColumnResource4"
                                UniqueName="WorklocationArabicName" FilterControlAltText="Filter WorklocationArabicName column" />
                            <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                meta:resourcekey="GridBoundColumnResource5" UniqueName="EmployeeName" FilterControlAltText="Filter EmployeeName column" />
                            <telerik:GridBoundColumn DataField="EmployeeArabicName" SortExpression="EmployeeArabicName"
                                HeaderText="Employee Arabic Name" meta:resourcekey="GridBoundColumnResource6"
                                UniqueName="EmployeeArabicName" FilterControlAltText="Filter EmployeeArabicName column" />
                            <telerik:GridBoundColumn DataField="Id" SortExpression="Id" Visible="False" AllowFiltering="False"
                                UniqueName="Id" meta:resourcekey="GridBoundColumnResource7" FilterControlAltText="Filter Id column" />
                        </Columns>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    </MasterTableView>
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings EnablePostBackOnRowClick="false" EnableRowHoverStyle="false">
                        <Selecting AllowRowSelect="false" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
    </ContentTemplate>
</asp:UpdatePanel>
