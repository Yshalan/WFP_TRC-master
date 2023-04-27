<%@ Page Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="WorkSchedule.aspx.vb" Inherits="Admin_WorkSchedule"
    Title="Define Normal Work Schedule" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="UserControls/NormalSchedule.ascx" TagName="Nor" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" />
            <div class="row">
                <div class="col-md-12">
                    <uc2:Nor ID="objNormalschedule" runat="server" />
                </div>
            </div>
            <div class="row" id="trControls" runat="server">
                <div class="col-md-12 text-center">
                    <asp:Button ID="ibtnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="GrSchedule"
                        meta:resourcekey="ibtnSaveResource1" />
                    <asp:Button ID="ibtnDelete" runat="server" CausesValidation="False" CssClass="button"
                        Text="Delete" meta:resourcekey="ibtnDeleteResource1" />
                    <asp:Button ID="ibtnRest" runat="server" CausesValidation="False" CssClass="button"
                        Text="Clear" meta:resourcekey="ibtnRestResource1" />
                </div>
                <div class="col-md-8">
                    <asp:HiddenField runat="server" ID="hdnWindow" />
                    <cc1:ModalPopupExtender ID="mpeSave" runat="server" BehaviorID="modelPopupExtender6"
                        PopupControlID="pnlPopup" TargetControlID="hdnWindow" CancelControlID="btnNo"
                        DropShadow="True" Enabled="True" BackgroundCssClass="ModalBackground" DynamicServicePath="">
                    </cc1:ModalPopupExtender>
                    <div id="pnlPopup" class="commonPopup" style="display: none">
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblCheck" runat="server" Text="There is Already Default Schedule."
                                    CssClass="Profiletitletxt" meta:resourcekey="lblCheckResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="lblConfirm" runat="server" Text="Are You Sure you want to set selected schedule as default?"
                                    CssClass="Profiletitletxt" meta:resourcekey="lblConfirmResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="button" meta:resourcekey="btnYesResource1" />
                                <asp:Button ID="btnNo" runat="server" Text="No" CssClass="button" meta:resourcekey="btnNoResource1" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdWorkSchedule"
                        Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadGrid ID="dgrdWorkSchedule" runat="server" AllowSorting="True" AllowPaging="True"
                        PageSize="15" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        ShowFooter="True" OnItemCommand="dgrdWorkSchedule_ItemCommand" meta:resourcekey="dgrdWorkScheduleResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False"
                            DataKeyNames="ScheduleId,ScheduleName">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource9"
                                    UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ScheduleName" SortExpression="ScheduleName" HeaderText="Schedule Name English"
                                    meta:resourcekey="GridBoundColumnResource2" UniqueName="ScheduleName" />
                                <telerik:GridBoundColumn DataField="ScheduleArabicName" SortExpression="ScheduleArabicName"
                                    HeaderText="Schedule Name Arabic" meta:resourcekey="GridBoundColumnResource3"
                                    UniqueName="ScheduleArabicName" />
                                <telerik:GridBoundColumn DataField="ScheduleId" SortExpression="ScheduleId" Visible="False"
                                    AllowFiltering="False" meta:resourcekey="GridBoundColumnResource4" UniqueName="ScheduleId" />
                                <telerik:GridBoundColumn DataField="GraceIn" SortExpression="GraceIn" HeaderText="Grace In"
                                    meta:resourcekey="GridBoundColumnResource5" UniqueName="GraceIn" />
                                <telerik:GridBoundColumn DataField="GraceOut" SortExpression="GraceOut" HeaderText="Grace Out"
                                    meta:resourcekey="GridBoundColumnResource6" UniqueName="GraceOut" />
                                <telerik:GridBoundColumn DataField="ScheduleType" SortExpression="ScheduleType" HeaderText="Schedule Type"
                                    meta:resourcekey="GridBoundColumnResource7" UniqueName="ScheduleType" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
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
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
