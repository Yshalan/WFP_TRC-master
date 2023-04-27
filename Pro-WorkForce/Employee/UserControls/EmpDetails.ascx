<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EmpDetails.ascx.vb" Inherits="EmpDetails_WebUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="NewEmployee.ascx" TagName="NewEmp" TagPrefix="Uc2" %>
<%@ Register Src="EditEmployee.ascx" TagName="EditEmp" TagPrefix="Uc3" %>
<%@ Register Src="../../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
    <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Employee Leaves" />
<%--<asp:UpdatePanel ID="Update1" runat="server">
    <ContentTemplate>--%>

    <div class="row">
            <asp:MultiView ID="MvEmployee" runat="server" ActiveViewIndex="2">
                <asp:View ID="VwAdd" runat="server">
                    <asp:LinkButton ID="lnkAddList" runat="server" OnClick="lnkAddList_Click" Text="Back"
                        meta:resourcekey="lnkAddListResource1"></asp:LinkButton>
                    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>--%>
                    <uc2:NewEmp ID="NewEmp1" CompanyID="0" runat="server" />
                    <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                </asp:View>
                <asp:View ID="VwEdit" runat="server">
                    <asp:LinkButton ID="lnkEditList" runat="server" OnClick="lnkEditList_Click" Text="Back"
                        meta:resourcekey="lnkEditListResource1"></asp:LinkButton>
                    <%--                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>--%>
                    <Uc3:EditEmp ID="EditEmp1" runat="server" />
                    <%--                                </ContentTemplate>
                            </asp:UpdatePanel>--%>
                </asp:View>
                <asp:View ID="VwList" runat="server">
                   
                        <div class="row">
                            <div class="col-md-12">
                                <uc2:PageFilter ID="EmployeeFilter" runat="server" OneventEmployeeSelect="SetSortingValue"
                                    ShowRadioSearch="true" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2"></div>
                            <div class="col-md-6">
                                <asp:RadioButtonList ID="rblEmpStatus" runat="server" CssClass="Profiletitletxt" 
                                    RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblEmpStatus_SelectedIndexChanged"> 
                                    <asp:ListItem Text="Active" Value="1" Selected="True" meta:resourcekey="rblEmpStatusResource1"></asp:ListItem>
                                    <asp:ListItem Text="InActive" Value="2" meta:resourcekey="rblEmpStatusResource2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnFilter" runat="server" Text="Get By Filter" class="button" ValidationGroup="ValidateComp"
                                    meta:resourcekey="btnFilterResource1" />
                                <asp:Button ID="btnadd" runat="server" Text="Add New Employee" class="button" 
                                    meta:resourcekey="btnaddResource1" CausesValidation="False" />
                            </div>
                        </div>

                        <div class="row">
                                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="grdVwEmployees"
                                    Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                           
                       
                            <div class="table-responsive">
                                <telerik:RadGrid ID="grdVwEmployees" runat="server" AllowSorting="True" AllowPaging="True"
                                    Width="100%"  GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                    PageSize="15" ShowFooter="True" OnItemCommand="grdVwEmployees_ItemCommand" meta:resourcekey="grdVwEmployeesResource1">
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="Top" IsFilterItemExpanded="False" DataKeyNames="EmployeeId">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" meta:resourcekey="GridBoundColumnResource1"
                                                SortExpression="EmployeeNo" UniqueName="EmployeeNo">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="English Name" meta:resourcekey="GridBoundColumnResource2"
                                                SortExpression="EmployeeName" UniqueName="EmployeeName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Arabic Name"
                                                meta:resourcekey="GridBoundColumnResource3" SortExpression="EmployeeArabicName"
                                                UniqueName="EmployeeArabicName">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="EmployeeId" meta:resourcekey="GridBoundColumnResource4"
                                                SortExpression="EmployeeId" UniqueName="EmployeeId" Visible="False">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn CommandName="EditEmp" meta:resourcekey="GridButtonColumnResource1"
                                                Text="Edit" UniqueName="column">
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <CommandItemTemplate>
                                            <telerik:RadToolBar ID="RadToolBar1" runat="server" meta:resourcekey="RadToolBar1Resource1"
                                                OnButtonClick="RadToolBar1_ButtonClick" Skin="Hay">
                                                <Items>
                                                    <telerik:RadToolBarButton runat="server" CommandName="FilterRadGrid" ImagePosition="Right"
                                                        ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource1" CausesValidation="false"
                                                        Text="Apply filter">
                                                    </telerik:RadToolBarButton>
                                                </Items>
                                            </telerik:RadToolBar>
                                        </CommandItemTemplate>
                                    </MasterTableView><SelectedItemStyle ForeColor="Maroon" />
                                </telerik:RadGrid>
                            </div>
                            </div>

                </asp:View>
            </asp:MultiView>
        </div>
<%--    </ContentTemplate>
</asp:UpdatePanel>
--%>