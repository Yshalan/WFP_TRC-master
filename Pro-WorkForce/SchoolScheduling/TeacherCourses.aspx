<%@ Page Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="TeacherCourses.aspx.vb" Inherits="SchoolScheduling_TeacherCourses" Title="Teacher Courses" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="UserControls/EditTeacher.ascx" TagName="EditTeacher" TagPrefix="Uc3" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="EmployeeFilter"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/TA_innerpage.css" rel="stylesheet" type="text/css" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:MultiView ID="MvEmployee" runat="server" ActiveViewIndex="0">

        <asp:View ID="Filter" runat="server">
            <%--           <center>
                <table style="width: 594px; border-width: 0px; background-color: #FFFFFF; visibility: hidden;"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3">
                            <div id="logo_div">
                                <a href="../Default/logout.aspx">
                                    <div id="logout2">
                                    </div>
                                </a>
                                <div style="text-align: left">
                                    <a href="../Default/Home.aspx">
                                        <img src="../images/logo.jpg" alt="smart time" /></a>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </center>--%>

            <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Define Teacher Courses" meta:resourcekey="HeaderResource1" />
            <div class="row">
                <div class="col-md-12">
                    <uc1:EmployeeFilter ID="EmployeeFilter" runat="server" ShowRadioSearch="true" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="rblEmpStatus" runat="server" CssClass="Profiletitletxt"
                        RepeatDirection="Horizontal" meta:resourcekey="rblEmpStatusResource1">
                        <asp:ListItem Value="1" Text="Active" Selected="True" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                        <asp:ListItem Value="2" Text="InActive" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row" id="trControls" runat="server">
                <div class="col-md-2"></div>
                <div class="col-md-2" runat="server">
                    <asp:Button ID="btnFilter" runat="server" Text="Get By Filter" class="button" ValidationGroup="ValidateComp"
                        meta:resourcekey="btnFilterResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="grdVwEmployees"
                        Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadGrid ID="grdVwEmployees" runat="server" AllowSorting="True" AllowPaging="True"
                        Width="100%" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
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
                                <%-- <telerik:GridBoundColumn DataField="TotalWeeklyCourses" HeaderText="Total Weekly Courses"
                                                meta:resourcekey="TotalWeeklyCoursesColumnResource3" SortExpression="TotalWeeklyCourses"
                                                UniqueName="TotalWeeklyCourses">
                                            </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="EmployeeId" meta:resourcekey="GridBoundColumnResource4"
                                    SortExpression="EmployeeId" UniqueName="EmployeeId" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn CommandName="EditTeacher" meta:resourcekey="GridButtonColumnResource1"
                                    Text="Edit" UniqueName="column">
                                </telerik:GridButtonColumn>
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar ID="RadToolBar1" runat="server" meta:resourcekey="RadToolBar1Resource1"
                                    OnButtonClick="RadToolBar1_ButtonClick" Skin="Hay">
                                    <Items>
                                        <telerik:RadToolBarButton runat="server" CommandName="FilterRadGrid" ImagePosition="Right"
                                            ImageUrl="~/images/RadFilter.gif"
                                            meta:resourcekey="RadToolBarButtonResource1" CausesValidation="False"
                                            Text="Apply filter" Owner="">
                                        </telerik:RadToolBarButton>
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                        </MasterTableView><SelectedItemStyle ForeColor="Maroon" />
                    </telerik:RadGrid>
                </div>
            </div>
        </asp:View>

        <asp:View ID="VwEdit" runat="server">
            <asp:LinkButton ID="lnkEditList" runat="server" OnClick="lnkEditList_Click" Text="Back"
                meta:resourcekey="lnkEditListResource1"></asp:LinkButton>
            <Uc3:EditTeacher ID="EditTeacher1" runat="server" />

        </asp:View>
    </asp:MultiView>
</asp:Content>
