<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="RealTimeMovements.aspx.vb" Inherits="DailyTasks_RealTimeMovements"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageHeader ID="PageHeader1" runat="server" />

    <asp:UpdatePanel ID="UPAll" runat="server">
        <ContentTemplate>
            <asp:Timer ID="timerAll" runat="server" Interval="10000">
            </asp:Timer>

            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-2">
                    <asp:Label ID="lblHead" runat="server" CssClass="Profiletitletxt" Text="Last Update:"
                        meta:resourcekey="lblHeadResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:Label ID="lblLastUpdate" runat="server" CssClass="normaltxt" meta:resourcekey="lblLastUpdateResource1"></asp:Label>
                </div>
            </div>
            <div class="row">

                <div class="table-responsive ">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="gvEvents" Skin="Hay"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="gvEvents" runat="server" AllowSorting="True" AllowPaging="True"
                        GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" PageSize="20"
                        ShowFooter="True" meta:resourcekey="gvEventsResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="STATUS">
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
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>

                                        <asp:HiddenField ID="hdnDesignationArabicName" runat="server" Value='<%# Eval("DesignationArabicName") %>' />
                                        <asp:HiddenField ID="hdnEmployeeArabicName" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                        <asp:HiddenField ID="hdnRemarksArabic" runat="server" Value='<%# Eval("RemarksArabic") %>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" meta:resourcekey="GridBoundColumnResource1"
                                    UniqueName="EmployeeNo" />
                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" meta:resourcekey="GridBoundColumnResource2"
                                    UniqueName="EmployeeName" />
                                <%--<telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="اسم الموظف" 
                                                    UniqueName="EmployeeArabicName" />--%>
                                <telerik:GridBoundColumn DataField="DesignationName" HeaderText="Designation Name"
                                    UniqueName="DesignationName" meta:resourcekey="GridBoundColumnResource3" />
                                <%-- <telerik:GridBoundColumn DataField="DesignationArabicName" HeaderText="الدرجة \ المنصب" 
                                                    UniqueName="DesignationArabicName" />--%>
                                <telerik:GridBoundColumn DataField="IN_TIME" HeaderText="IN TIME" meta:resourcekey="GridBoundColumnResource14"
                                    UniqueName="IN_TIME" />
                                <telerik:GridBoundColumn DataField="OUT_TIME" HeaderText="OUT TIME" UniqueName="OUT_TIME" meta:resourcekey="GridBoundColumnResource15" />
                                <telerik:GridBoundColumn DataField="DELAY" HeaderText="DELAY" meta:resourcekey="GridBoundColumnResource16"
                                    UniqueName="DELAY" />
                                <telerik:GridBoundColumn DataField="EARLY_OUT" HeaderText="EARLY_OUT" UniqueName="EARLY_OUT" meta:resourcekey="GridBoundColumnResource17" />
                                <telerik:GridBoundColumn DataField="STATUS" HeaderText="STATUS" UniqueName="STATUS" Visible="false" />

                                <telerik:GridBoundColumn DataField="RemarksEnglish" HeaderText="RemarksEnglish" UniqueName="RemarksEnglish" meta:resourcekey="GridBoundColumnResource18" />

                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False"></GroupingSettings>
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
