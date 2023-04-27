<%@ Page Title="" Language="VB" Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Card_Request_Approval.aspx.vb" Inherits="Employee_Card_Request_Approval"
    UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ShowPopUp(Mode) {
            var lang = '<%= MsgLang %>';
            if (Mode == 1) {
                if (lang == 'en') {
                    return confirm('Are you sure you want to accept card request?');
                }
                else {
                    return confirm('هل انت متأكد من قبول طلب البطاقة؟');
                }
            }
            else if (Mode == 2) {
                if (lang == 'en') {
                    return confirm('Are you sure you want to reject card request?');
                }
                else {
                    return confirm('هل انت متأكد من رفض طلب البطاقة؟');
                }
            }
        }

    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>

        <uc1:PageHeader ID="PageHeader1" runat="server" />
                <div class="row">
                    <div class="col-md-2">
                        <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdCardRequests"
                            Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    </div>
                </div>
                <div class="row">
                    <div class="table-responsive">
                        <telerik:RadGrid ID="dgrdCardRequests" runat="server" AllowSorting="True" AllowPaging="True"
                            Width="100%" PageSize="15"  GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                            ShowFooter="True" meta:resourcekey="dgrdCardRequestsResource1">
                            <GroupingSettings CaseSensitive="False" />
                            <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="ReasonId,Status,CardRequestId,NextApprovalStatus,CardApproval">
                                <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" UniqueName="EmployeeNo"
                                        meta:resourcekey="GridBoundColumnResource1" />
                                    <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                                        meta:resourcekey="GridBoundColumnResource2" />
                                    <telerik:GridBoundColumn DataField="CardTypeEn" HeaderText="Card Type" UniqueName="CardTypeEn"
                                         />
                                    <telerik:GridBoundColumn DataField="ReasonId" HeaderText="Reason" UniqueName="ReasonId"
                                        meta:resourcekey="GridBoundColumnResource3" />
                                    <telerik:GridBoundColumn DataField="OtherReason" HeaderText="OtherReason" UniqueName="OtherReason"
                                        meta:resourcekey="GridBoundColumnResource4" />
                                    <telerik:GridBoundColumn DataField="Status" HeaderText="Status" UniqueName="Status"
                                        meta:resourcekey="GridBoundColumnResource5" />
                                    <telerik:GridBoundColumn DataField="CardRequestId" AllowFiltering="False" Visible="False"
                                        UniqueName="CardRequestId" meta:resourcekey="GridBoundColumnResource6" />
                                     <telerik:GridTemplateColumn HeaderText="Reject Reason" AllowFiltering="False" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtReason" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkAccept" runat="server" Text="Accept" OnClick="lnkAccept_Click"
                                                CommandName="accept" OnClientClick="return ShowPopUp('1')" meta:resourcekey="lnkAcceptResource1"></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkReject" runat="server" Text="Reject" CommandName="reject"
                                                OnClick="lnkReject_Click" OnClientClick="return ShowPopUp('2')" CommandArgument='<%# Eval("CardRequestId") %>'
                                                meta:resourcekey="lnkRejectResource1"></asp:LinkButton><asp:HiddenField ID="hdnEmployeeNameAr"
                                                    runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
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
