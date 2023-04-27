<%@ Page Title="" Language="VB" MasterPageFile="~/Default/EmptyMaster_WithScriptManager.master" AutoEventWireup="false"
    CodeFile="Manager_CalendarDetails.aspx.vb" Inherits="SelfServices_Manager_CalendarDetails"
    Theme="SvTheme" UICulture="Auto" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-3">
            <asp:Label ID="lblType" runat="server" Text="Type" meta:resourcekey="lblTypeResource1"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:Label ID="lblTypeVal" runat="server" SkinID="Remark" meta:resourcekey="lblTypeValResource1"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-md-3">
            <asp:Label ID="lblDate" runat="server" Text="Date" meta:resourcekey="lblDateResource1"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:Label ID="lblDateVal" runat="server" SkinID="Remark" meta:resourcekey="lblDateValResource1"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:Label ID="lblDay" runat="server" Text="Day" meta:resourcekey="lblDayResource1"></asp:Label>
        </div>
        <div class="col-md-3">
            <asp:Label ID="lblDayVal" runat="server" SkinID="Remark" meta:resourcekey="lblDayValResource1"></asp:Label>
        </div>
    </div>
    <div class="table-responsive">
        <telerik:RadGrid runat="server" ID="dgrdCal_Details" AutoGenerateColumns="False" PageSize="15"
            AllowPaging="True" AllowSorting="false" AllowFilteringByColumn="false" CellSpacing="0" GridLines="None" meta:resourcekey="dgrdCal_DetailsResource1">
            <GroupingSettings CaseSensitive="False"></GroupingSettings>
            <ClientSettings AllowColumnsReorder="false" ReorderColumnsOnClient="false"
                EnablePostBackOnRowClick="false" EnableRowHoverStyle="false">
                <Selecting AllowRowSelect="false" />
            </ClientSettings>
            <MasterTableView AllowFilteringByColumn="False" CommandItemDisplay="Top" DataKeyNames="FK_EmployeeId">
                <Columns>

                    <telerik:GridBoundColumn DataField="FK_EmployeeId"
                        HeaderText="FK_EmployeeId" Resizable="False" Display="false" AllowFiltering="false"
                        UniqueName="FK_EmployeeId" AllowSorting="false" meta:resourcekey="GridBoundColumnResource1">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EmployeeNo" FilterControlAltText="Filter EmployeeNo column"
                        HeaderText="Employee No" Resizable="False" AllowSorting="false"
                        UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource2">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EmployeeName" FilterControlAltText="Filter EmployeeName column"
                        HeaderText="Employee Name" Resizable="False" AllowSorting="false"
                        UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource3">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EmployeeArabicName" FilterControlAltText="Filter EmployeeArabicName column"
                        HeaderText="Employee Arabic Name" Resizable="False" AllowSorting="false"
                        UniqueName="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource4">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Details" FilterControlAltText="Filter Details column"
                        HeaderText="Details" Resizable="False" AllowSorting="false"
                        UniqueName="Details" meta:resourcekey="GridBoundColumnResource5">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Cal_Subject" 
                        HeaderText="Type"
                        UniqueName="Cal_Subject" Visible="true" meta:resourcekey="GridBoundColumnResource7">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Cal_SubjectAr"  
                        HeaderText="Type"  
                        UniqueName="Cal_SubjectAr" Visible="true" meta:resourcekey="GridBoundColumnResource7">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="IsFlexible" Resizable="False" AllowSorting="false" AllowFiltering="false"
                        UniqueName="IsFlexible" meta:resourcekey="GridBoundColumnResource6" Display="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="ApptType" Resizable="False" AllowSorting="false" AllowFiltering="false"
                        UniqueName="ApptType" meta:resourcekey="GridBoundColumnResource6" Display="false">
                    </telerik:GridBoundColumn>
                </Columns>
                <CommandItemTemplate>
                </CommandItemTemplate>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

