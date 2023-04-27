<%@ Page Title="Employee HR" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="Emp_HR.aspx.vb" Inherits="Admin_Emp_HR" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="UserControls/EmployeeFilter.ascx" TagName="PageFilter" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
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
        function CheckBoxListSelectCompany(state) {
            var chkBoxList = document.getElementById("<%= cblCompanies.ClientID%>");
                    var chkBoxCount = chkBoxList.getElementsByTagName("input");
                    for (var i = 0; i < chkBoxCount.length; i++) {
                        chkBoxCount[i].checked = state;
                    }
                    return false;
        }

        function CheckBoxListSelectEntity(state) {
            var chkBoxList = document.getElementById("<%= cblEntities.ClientID %>");
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            for (var i = 0; i < chkBoxCount.length; i++) {
                chkBoxCount[i].checked = state;
            }
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <asp:MultiView ID="mvEmp_HR" ActiveViewIndex="0" runat="server">
        <asp:View runat="server" ID="viewEmpHRList">
            <div class="row">
                <div class="col-md-8">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="button" meta:resourcekey="btnAddResource1" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" meta:resourcekey="btnDeleteResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdEmpHR" Skin="Hay"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid runat="server" ID="dgrdEmpHR" AllowSorting="True" AllowPaging="True"
                        GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" PageSize="25"
                        ShowFooter="True" meta:resourcekey="dgrdEmpHRResource1">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False"
                            DataKeyNames="HREmployeeId,EmployeeNo">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee Number" SortExpression="EmployeeNo"
                                    UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"
                                    UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Arabic Name"
                                    SortExpression="EmployeeArabicName" UniqueName="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource6">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="DesignationName" HeaderText="Designation Name"
                                    SortExpression="DesignationName" UniqueName="DesignationName" meta:resourcekey="GridBoundColumnResource3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="DesignationArabicName" HeaderText="Designation Arabic Name"
                                    SortExpression="DesignationArabicName" UniqueName="DesignationArabicName" meta:resourcekey="GridBoundColumnResource7">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="HREmployeeId" DataType="System.Int32" Visible="False"
                                    HeaderText="HREmployeeId" UniqueName="HREmployeeId" meta:resourcekey="GridBoundColumnResource4">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="HRDesignation" DataType="System.Int32" Visible="False"
                                    HeaderText="HRDesignation" UniqueName="HRDesignation" meta:resourcekey="GridBoundColumnResource5">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnEdit" runat="server" OnClick="btn_Click" meta:resourcekey="lbtnEditBalanceResource1"
                                            Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
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
                        <SelectedItemStyle ForeColor="Maroon" />
                    </telerik:RadGrid>
                </div>
            </div>
        </asp:View>
        <asp:View runat="server" ID="viewEmpHREdit">
            <cc1:TabContainer ID="TabContainer1" runat="server" AutoPostBack="True" ActiveTabIndex="0"
                OnClientActiveTabChanged="hideValidatorCalloutTab" meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel ID="TabEmpDesigion" runat="server" HeaderText="Employee Designation"
                    meta:resourcekey="TabEmpDesigionResource1">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-12">
                                <uc1:PageFilter ID="EmployeeFilterUC" runat="server" HeaderText="Employee Filter"
                                    OneventEmployeeSelect="FilllDesigion" ValidationGroup="validateEmployeeComp" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblDesigion" runat="server" Text="Employee Designation" CssClass="Profiletitletxt"
                                    Visible="False" meta:resourcekey="lblDesigionResource1" />
                            </div>
                            <div class="col-md-4">
                                <telerik:RadComboBox ID="RadCmbDesignation" runat="server" MarkFirstMatch="True"
                                    Visible="False" Skin="Vista" meta:resourcekey="RadCmbDesignationResource1" />
                                <asp:RequiredFieldValidator ID="rfvEmployeeDesigion" runat="server" ControlToValidate="RadCmbDesignation"
                                    Display="None" ErrorMessage="Please enter employee desigion" InitialValue="-1"
                                    ValidationGroup="validateEmployeeComp" Enabled="False" meta:resourcekey="rfvEmployeeDesigionResource1" />
                                <cc1:ValidatorCalloutExtender ID="vceEmployeeDesigion" runat="server" Enabled="True"
                                    TargetControlID="rfvEmployeeDesigion">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-4">
                                <asp:RadioButtonList ID="rdlIsSpecific" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="True" meta:resourcekey="rdlIsSpecificCompanyResource1">
                                    <asp:ListItem Text="All" Value="0" meta:resourcekey="ListItemResource21" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Specific Company" Value="2" meta:resourcekey="ListItemResource20"></asp:ListItem>
                                    <asp:ListItem Text="Specific Entity" Value="1" meta:resourcekey="ListItemResource18"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row" id="divCompanies" runat="server" visible="False">
                            <div class="col-md-2">
                                <asp:Label ID="lblCompanies" runat="server" CssClass="Profiletitletxt" Text="List Of Companies"
                                    meta:resourcekey="lbllblCompaniesResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
                                    <asp:CheckBoxList ID="cblCompanies" runat="server" Style="height: 26px" DataTextField="CompanyName"
                                        DataValueField="CompanyId" meta:resourcekey="cblCompaniesListResource1">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divCompaniesSelect" runat="server" visible="False">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-2">
                                <a href="javascript:void(0)" onclick="CheckBoxListSelectCompany(true)" style="font-size: 8pt">
                                    <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                            </div>
                            <div class="col-md-2">
                                <a href="javascript:void(0)" onclick="CheckBoxListSelectCompany(false)" style="font-size: 8pt">
                                    <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:HyperLink ID="HyperLink2" runat="server" Visible="False" meta:resourcekey="hlViewEntityResource1"
                                    Text="View Companies "></asp:HyperLink>
                            </div>
                        </div>
                        <div class="row" id="divEntities" runat="server" visible="False">
                            <div class="col-md-2">
                                <asp:Label ID="lblEntities" runat="server" CssClass="Profiletitletxt" Text="List Of Entities"
                                    meta:resourcekey="lblEntitiesResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <div style="height: 200px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
                                    <asp:CheckBoxList ID="cblEntities" runat="server" Style="height: 26px" DataTextField="EntityName"
                                        DataValueField="EntityId" meta:resourcekey="cblEntityListResource1">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divEntitiesSelect" runat="server" visible="False">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-2">
                                <a href="javascript:void(0)" onclick="CheckBoxListSelectEntity(true)" style="font-size: 8pt">
                                    <asp:Literal ID="Literal3" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                            </div>
                            <div class="col-md-2">
                                <a href="javascript:void(0)" onclick="CheckBoxListSelectEntity(false)" style="font-size: 8pt">
                                    <asp:Literal ID="Literal4" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:HyperLink ID="HyperLink1" runat="server" Visible="False" meta:resourcekey="hlViewEntityResource1"
                                    Text="View Entities "></asp:HyperLink>
                            </div>
                        </div>

                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabEmpNotificationType" runat="server" HeaderText="Employee Notification Type"
                    meta:resourcekey="TabEmpNotificationTypeResource1" Visible="false">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label ID="lblNotofocationType" runat="server" Text="Notification Type" CssClass="Profiletitletxt"
                                    meta:resourcekey="lblNotofocationTypeResource1" />
                            </div>
                            <div class="col-md-4">
                                <asp:CheckBoxList ID="chlNotificationType" runat="server" meta:resourcekey="chlNotificationTypeResource1" />
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <div class="row">
                <div id="divControls" runat="server" class="col-md-12 text-center">
                    <asp:Button runat="server" ID="btnSubmit" Text="Add" ValidationGroup="validateEmployeeComp"
                        CssClass="button" meta:resourcekey="btnSubmitResource1" />
                    <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClientClick="ClearForm(); return false;"
                        CssClass="button" meta:resourcekey="btnCancelResource1" /><%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>--%>
                </div>
            </div>
            <%--   <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="button" Style="margin-top: 10px"
                                meta:resourcekey="btnSubmitResource1" ValidationGroup="validateEmployeeComp" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" Style="margin-top: 10px"
                                meta:resourcekey="btnCancelResource1" />--%>
        </asp:View>
    </asp:MultiView>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
