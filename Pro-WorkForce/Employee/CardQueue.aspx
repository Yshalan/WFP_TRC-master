

<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="CardQueue.aspx.vb" Inherits="Employee_CardQueue" meta:resourcekey="PageResource1"
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
                <div class="table-responsive">
                    <telerik:RadGrid ID="dgrdCardRequests" runat="server" AllowSorting="True" AllowPaging="True"
                        Width="100%" PageSize="15" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" OnNeedDataSource="dgrdCardRequests_NeedDataSource"
                        ShowFooter="True" meta:resourcekey="dgrdCardRequestsResource1">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="ReasonId,Status,FK_EmployeeId,CardTypeId">
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
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource3">
                                        <ItemTemplate>
                                          <%-- <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/images/ibtnDown.JPG" OnClick="btnLnk_Click" meta:resourcekey="lnkprintResource1" CausesValidation="false"></asp:ImageButton>
                                           --%>  
                                            <asp:LinkButton ID="btnLink" runat="server" Text="Download" OnClick="btnLnk_Click" meta:resourcekey="lnkAcceptResource1"></asp:LinkButton>
                                     
                                            </ItemTemplate>
                                </telerik:GridTemplateColumn>
                             
                                 
                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn2" meta:resourcekey="GridTemplateColumnResource2">
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
  
</asp:Content>
