<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="Emp_Skills.aspx.vb" Inherits="Employee_Emp_Skills"
    Theme="SvTheme" Culture="Auto" UICulture="Auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function Skillselected(source, eventArgs) {

            var hdnValueID = "<%= hdnSkillName.ClientID%>";

            document.getElementById(hdnValueID).value = eventArgs.get_value();
            __doPostBack(hdnValueID, "");
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <%--    <asp:MultiView ID="mvEmp_Skills" runat="server" ActiveViewIndex="0">
        <asp:View ID="vEmployees" runat="server">--%>

    <div class="row">
        <div class="col-md-12">
            <uc2:PageFilter ID="EmployeeFilter" runat="server" OneventEmployeeSelect="SetSortingValue"
                ShowRadioSearch="true" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 text-center">
            <asp:Button ID="btnFilter" runat="server" Text="Get By Filter" class="button" ValidationGroup="ValidateComp" meta:resourcekey="btnFilterResource1" />
        </div>
    </div>
    <%--<div class="row">
                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="grdVwEmployees"
                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                    <ContextMenu FeatureGroupID="rfContextMenu"></ContextMenu>
                </telerik:RadFilter>
                <div class="table-responsive">
                    <telerik:RadGrid ID="grdVwEmployees" runat="server" AllowSorting="True" AllowPaging="True"
                        Width="100%" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        PageSize="15" ShowFooter="True" OnItemCommand="grdVwEmployees_ItemCommand" CellSpacing="0" meta:resourcekey="grdVwEmployeesResource1">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="false" EnableRowHoverStyle="false">
                            <Selecting AllowRowSelect="false" />
                        </ClientSettings>
                        <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="Top" IsFilterItemExpanded="False" DataKeyNames="EmployeeId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No"
                                    SortExpression="EmployeeNo" UniqueName="EmployeeNo" FilterControlAltText="Filter EmployeeNo column" meta:resourcekey="GridBoundColumnResource1">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="English Name"
                                    SortExpression="EmployeeName" UniqueName="EmployeeName" FilterControlAltText="Filter EmployeeName column" meta:resourcekey="GridBoundColumnResource2">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Arabic Name"
                                    SortExpression="EmployeeArabicName"
                                    UniqueName="EmployeeArabicName" FilterControlAltText="Filter EmployeeArabicName column" meta:resourcekey="GridBoundColumnResource3">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="EmployeeId"
                                    SortExpression="EmployeeId" UniqueName="EmployeeId" Visible="False" FilterControlAltText="Filter EmployeeId column" meta:resourcekey="GridBoundColumnResource4">
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn CommandName="EditEmp"
                                    Text="Edit" UniqueName="column" FilterControlAltText="Filter column column" meta:resourcekey="GridButtonColumnResource1">
                                </telerik:GridButtonColumn>
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar ID="RadToolBar1" runat="server"
                                    OnButtonClick="RadToolBar1_ButtonClick" Skin="Hay" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                    <Items>
                                        <telerik:RadToolBarButton runat="server" CommandName="FilterRadGrid" ImagePosition="Right"
                                            ImageUrl="~/images/RadFilter.gif" CausesValidation="False"
                                            Text="Apply filter" meta:resourcekey="RadToolBarButtonResource1">
                                        </telerik:RadToolBarButton>
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                        </MasterTableView><SelectedItemStyle ForeColor="Maroon" />
                    </telerik:RadGrid>
                </div>
            </div>--%>
    <%--  </asp:View>
        <asp:View ID="vEmployeeSkills" runat="server">--%>
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-10">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:HiddenField ID="hdnSkillName" OnValueChanged="hdnValue_ValueChanged" runat="server" />
                            <asp:TextBox ID="txtSearch" runat="server" meta:resourcekey="txtSearchResource1"></asp:TextBox>
                            <cc1:AutoCompleteExtender ServiceMethod="SearchSkills"
                                MinimumPrefixLength="1"
                                CompletionInterval="100" EnableCaching="False"
                                TargetControlID="txtSearch"
                                ID="AutoCompleteExtender1" runat="server" DelimiterCharacters="" Enabled="True" ServicePath=""
                                OnClientItemSelected="Skillselected">
                            </cc1:AutoCompleteExtender>
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" meta:resourcekey="btnAddResource1" />
                            <%--<asp:Button ID="btnBack" runat="server" Text="Back" meta:resourcekey="btnBackResource1" />--%>
                        </div>
                    </div>
                </div>
            </div>
            <div id="dvDate" runat="server" visible="false" class="Svpanel">
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="lblFromDate" runat="server" Text="From" meta:resourcekey="lblFromDateResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker ID="dtpFromDate" AllowCustomText="false" MarkFirstMatch="true"
                            Skin="Vista" runat="server" Culture="English (United States)">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
                            <DateInput ID="DateInput3" DateFormat="dd/MM/yyyy" ToolTip="View permission date"
                                runat="server">
                            </DateInput>
                        </telerik:RadDatePicker>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label ID="lblToDate" runat="server" Text="To" meta:resourcekey="lblToDateResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker ID="dtpToDate" AllowCustomText="false" MarkFirstMatch="true"
                            Skin="Vista" runat="server" Culture="English (United States)">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
                            <DateInput ID="DateInput1" DateFormat="dd/MM/yyyy" ToolTip="View permission date"
                                runat="server">
                            </DateInput>
                        </telerik:RadDatePicker>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 row-eq-height">
                    <div class="width10 text-center skillfirsticon">
                        <asp:Repeater ID="repSkillCategory" runat="server">
                            <ItemTemplate>
                                <div class="" id="dvSkillCategory" runat="server">
                                    <asp:ImageButton ID="imgSkillCategory" runat="server" ImageUrl="~/images/certificates_White.png" Width="60px"
                                        OnClick="imglnkEmpSkills_Click" CommandArgument='<%# Eval("CategoryId") %>' meta:resourcekey="imgSkillCategoryResource1" />
                                    <asp:LinkButton ID="lnkSkillCategoryEn" runat="server" Text='<%# Eval("DisplayName") %>'
                                        Visible="False" OnClick="lnkEmpSkills_Click" CommandArgument='<%# Eval("CategoryId") %>' meta:resourcekey="lnkSkillCategoryEnResource1"></asp:LinkButton>
                                    <asp:LinkButton ID="lnkSkillCategoryAr" runat="server" Text='<%# Eval("DisplayArabicName") %>'
                                        Visible="False" OnClick="lnkEmpSkills_Click" CommandArgument='<%# Eval("CategoryId") %>' meta:resourcekey="lnkSkillCategoryArResource1"></asp:LinkButton>
                                    <asp:Label ID="lblCategoryId" runat="server" Text='<%# Eval("CategoryId") %>' Visible="False" CssClass="hidden" meta:resourcekey="lblCategoryIdResource1" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="width80">
                        <div class="employeeskinarae" id="Empskills" runat="server" visible="true">
                            <asp:Repeater ID="repSkills" runat="server">
                                <ItemTemplate>
                                    <span class="skillinline">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblSkillNameEn" runat="server" Text='<%# Eval("SkillName") %>' Visible="False" meta:resourcekey="lblSkillNameEnResource1" />
                                                <asp:Label ID="lblSkillNameAr" runat="server" Text='<%# Eval("SkillArabicName") %>' Visible="False" meta:resourcekey="lblSkillNameArResource1" />
                                                <asp:Label ID="lblSkillId" runat="server" Text='<%# Eval("SkillId") %>' Visible="False" CssClass="hidden" meta:resourcekey="lblSkillIdResource1" />
                                                <asp:ImageButton ID="imgRemoveSkill" runat="server" ImageUrl="~/images/RemoveSkill.png" Height="10px" Width="10px"
                                                    OnClick="imgDeleteEmpSkills_Click" CommandArgument='<%# Eval("SkillId") %>' meta:resourcekey="imgRemoveSkillResource1" />
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblSkillFromDate" runat="server" Text="From:" Visible="False" meta:resourcekey="lblSkillFromDateResource1"></asp:Label>
                                                <asp:Label ID="lblSkillFromDateVal" Text='<%# Eval("FromDate")%>' Visible="False" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblSkillToDate" runat="server" Text="To:" Visible="False" meta:resourcekey="lblSkillToDateResource1"></asp:Label>
                                                <asp:Label ID="lblSkillToDateVal" runat="server" Text='<%# Eval("ToDate")%>' Visible="False"></asp:Label>
                                            </div>
                                        </div>
                                    </span>

                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--  </asp:View>
    </asp:MultiView>--%>
</asp:Content>


