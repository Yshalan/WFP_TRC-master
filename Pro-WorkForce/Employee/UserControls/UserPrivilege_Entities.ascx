<%@ Control Language="VB" AutoEventWireup="false" CodeFile="UserPrivilege_Entities.ascx.vb"
    Inherits="Employee_UserControls_UserPrevileges_Entities" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../Admin/UserControls/EmployeeFilter.ascx" TagName="Emp_Filter"
    TagPrefix="uc1" %>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
<script type="text/javascript" language="javascript">
    function CheckBoxListSelectEntity(state) {
        var chkBoxList = document.getElementById("<%= cblEntitylist.ClientID%>");
        var chkBoxCount = chkBoxList.getElementsByTagName("input");
        for (var i = 0; i < chkBoxCount.length; i++) {
            chkBoxCount[i].checked = state;
        }
        return false;
    }

    function SearchEmployees(txtSearchEntityName, cblEntitylist) {
        if ($(txtSearchEntityName).val() != "") {
            var count = 0;
            $(cblEntitylist).children('tbody').children('tr').each(function () {
                var match = false;
                $(this).children('td').children('span').children('label').each(function () {
                    if ($(this).text().toUpperCase().indexOf($(txtSearchEntityName).val().toUpperCase()) > -1)
                        match = true;
                });
                if (match) {
                    $(this).show();
                    count++;
                }
                else { $(this).hide(); }
            });

            if ('<%=searchlang%>' == 'ar') {
                $('#spnCount').html((count) + ' تطابق');
            }
            else {
                $('#spnCount').html((count) + ' match');
            }
        }
        else {
            $(cblEntitylist).children('tbody').children('tr').each(function () {
                $(this).show();
            });
            $('#spnCount').html('');
        }
    }

</script>

<asp:UpdatePanel ID="Upanel1" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-md-12">
                <uc1:Emp_Filter ID="objEmp_FilterEntity" runat="server" ValidationGroup="grpSave_entity" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblCompany" runat="server" CssClass="Profiletitletxt" Text="Company"
                    meta:resourcekey="lblCompanyResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadComboBox ID="RadCmbBxCompaniesEntity" MarkFirstMatch="True" Skin="Vista"
                    runat="server" AutoPostBack="True" meta:resourcekey="RadCmbBxCompaniesResource1">
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="RFVCompanyentity" ValidationGroup="grpSave_entity"
                    runat="server" InitialValue="--Please Select--" ControlToValidate="RadCmbBxCompaniesEntity"
                    Display="None" ErrorMessage="Please Select Company" meta:resourcekey="RFVCompanyResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="VCECompanyEntity" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="RFVCompanyentity" WarningIconImageUrl="~/images/warning1.png"
                    Enabled="True">
                </cc1:ValidatorCalloutExtender>
            </div>
        </div>
        <%--        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblEntity" runat="server" CssClass="Profiletitletxt" Text="Entity"
                    meta:resourcekey="lblEntityResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadComboBox ID="RadcmbBxEntity" MarkFirstMatch="True" Skin="Vista" runat="server"
                    AutoPostBack="True" meta:resourcekey="RadcmbBxEntityResource1" Style="width: 350px">
                </telerik:RadComboBox>
                <asp:RequiredFieldValidator ID="RFVEntity" ValidationGroup="grpSave_entity" runat="server"
                    InitialValue="--Please Select--" ControlToValidate="RadcmbBxEntity" Display="None"
                    ErrorMessage="Please Select Entity" meta:resourcekey="RFVEntityResource1"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="VCEEntity" runat="server" CssClass="AISCustomCalloutStyle"
                    TargetControlID="RFVEntity" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                </cc1:ValidatorCalloutExtender>
            </div>
        </div>--%>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblLevels" runat="server" Text="Entity Levels" meta:resourcekey="lblLevelsResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:RadioButtonList ID="rblLevels" runat="server" AutoPostBack="true" meta:resourcekey="rblLevelsResource1">
                </asp:RadioButtonList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="lblSearchEntityName" runat="server" Text="Search Entity Name" meta:resourcekey="lblSearchEntityNameResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <asp:TextBox ID="txtSearchEntityName" runat="server" onkeyup="SearchEmployees(this,'#cblEntitylist');"
                    placeholder="Search Entity" meta:resourcekey="txtSearchEntityNameResource1"></asp:TextBox>
                <span id="spnCount"></span>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="List Of Entity(s)"
                    meta:resourcekey="Label5Resource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
                    <asp:CheckBoxList ID="cblEntitylist" runat="server" Style="height: 26px" CssClass="checkboxlist" ClientIDMode="Static"
                        DataTextField="EntityName" DataValueField="EntityId" meta:resourcekey="cblEntitylistResource1">
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="col-md-2">
                <a href="javascript:void(0)" onclick="CheckBoxListSelectEntity(true)" style="font-size: 8pt">
                    <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                <a href="javascript:void(0)" onclick="CheckBoxListSelectEntity(false)" style="font-size: 8pt">
                    <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
            </div>
            <div class="col-md-2">
                <asp:HyperLink ID="hlViewEmployee" runat="server" Visible="False" meta:resourcekey="hlViewEmployeeResource1"
                    Text="View Entity(s) "></asp:HyperLink>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
            </div>
            <div class="col-md-4">
                <asp:CustomValidator ID="cvEntityValidation" ErrorMessage="please select at least one entity"
                    ValidationGroup="grpSave_entity" ForeColor="Black" runat="server" CssClass="customValidator"
                    meta:resourcekey="cvEntityValidationResource1" />

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
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <asp:CheckBox ID="chkIsCoordinator" runat="server" Text="Is Coordinator" AutoPostBack="true" meta:resourcekey="chkIsCoordinatorResource1" />
            </div>
        </div>
        <div class="row" id="dvCoordinatorType" runat="server" visible="false">
            <div class="col-md-2">
                <asp:Label ID="lblCoordinatorType" runat="server" Text="Coordinator Type" meta:resourcekey="lblCoordinatorTypeResource1"></asp:Label>
            </div>
            <div class="col-md-4">
                <telerik:RadComboBox ID="radcmbxCoordinatorType" CausesValidation="False" Filter="Contains"
                    MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px">
                </telerik:RadComboBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="grpSave_entity"
                    CssClass="button" meta:resourcekey="btnSaveResource1" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CausesValidation="False"
                    CssClass="button" meta:resourcekey="btnDeleteResource1" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" CssClass="button"
                    meta:resourcekey="btnClearResource1" />
            </div>
        </div>
        <div class="row">
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
                    GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" PageSize="25"
                    ShowFooter="True" meta:resourcekey="dgrdManagersResource1" CellSpacing="0">
                    <SelectedItemStyle ForeColor="Maroon" />
                    <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False"
                        DataKeyNames="Id,CompanyId,EntityId,EmployeeId,IsCoordinator">
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
                                FilterControlAltText="Filter CompanyId column" meta:resourcekey="GridBoundColumnResource9" AllowFiltering="false" />
                            <telerik:GridBoundColumn DataField="EntityId" UniqueName="EntityId" Visible="False"
                                FilterControlAltText="Filter EntityId column" meta:resourcekey="GridBoundColumnResource10" AllowFiltering="false" />
                            <telerik:GridBoundColumn DataField="EmployeeId" UniqueName="EmployeeId" Visible="False"
                                FilterControlAltText="Filter EmployeeId column" meta:resourcekey="GridBoundColumnResource11" AllowFiltering="false" />
                            <telerik:GridBoundColumn DataField="EmployeeNo" SortExpression="EmployeeNo" HeaderText="Employee No"
                                meta:resourcekey="GridBoundColumnResource8" UniqueName="EmployeeNo" FilterControlAltText="Filter EmployeeNo column" />
                            <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName" HeaderText="Employee Name"
                                meta:resourcekey="GridBoundColumnResource5" UniqueName="EmployeeName" FilterControlAltText="Filter EmployeeName column" />
                            <telerik:GridBoundColumn DataField="EmployeeArabicName" SortExpression="EmployeeArabicName"
                                HeaderText="Employee Arabic Name" meta:resourcekey="GridBoundColumnResource6"
                                UniqueName="EmployeeArabicName" FilterControlAltText="Filter EmployeeArabicName column" />
                            <telerik:GridBoundColumn DataField="EntityName" SortExpression="EntityName" HeaderText="Entity Name"
                                meta:resourcekey="GridBoundColumnResource3" UniqueName="EntityName" FilterControlAltText="Filter EntityName column" />
                            <telerik:GridBoundColumn DataField="EntityArabicName" SortExpression="EntityArabicName"
                                HeaderText="Entity Arabic Name" meta:resourcekey="GridBoundColumnResource4" UniqueName="EntityArabicName" FilterControlAltText="Filter EntityArabicName column" />
                            <telerik:GridBoundColumn DataField="CompanyName" SortExpression="CompanyName" HeaderText="Company Name"
                                meta:resourcekey="GridBoundColumnResource1" UniqueName="CompanyName" FilterControlAltText="Filter CompanyName column" />
                            <telerik:GridBoundColumn DataField="CompanyArabicName" SortExpression="CompanyArabicName"
                                HeaderText="Company Arabic Name" meta:resourcekey="GridBoundColumnResource2"
                                UniqueName="CompanyArabicName" FilterControlAltText="Filter CompanyArabicName column" />
                            <telerik:GridBoundColumn DataField="Id" SortExpression="Id" Visible="False" AllowFiltering="False"
                                UniqueName="Id" meta:resourcekey="GridBoundColumnResource7" FilterControlAltText="Filter Id column" />
                            <telerik:GridBoundColumn DataField="IsCoordinator" SortExpression="IsCoordinator" Display="False" AllowFiltering="False"
                                UniqueName="IsCoordinator" />
                        </Columns>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    </MasterTableView>
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="false">
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
