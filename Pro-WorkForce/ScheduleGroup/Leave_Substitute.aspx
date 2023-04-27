<%@ Page Title="" Language="VB" Theme="SvTheme"  MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Leave_Substitute.aspx.vb" Inherits="ScheduleGroup_Leave_Substitute"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/MultiEmployeeFilter.ascx" TagName="MultiEmployeeFilter"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Upanel1" runat="server">
        <ContentTemplate>

                <uc2:PageHeader ID="pageheader1" HeaderText="Leave Substitute" runat="server" />

            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab"
                meta:resourcekey="TabContainer1Resource1">
                <cc1:TabPanel ID="TabPending" runat="server" HeaderText="Pending Substitute" meta:resourcekey="TabPendingResource1">
                    <ContentTemplate>
                        <asp:MultiView ID="mvPending" runat="server">
                            <asp:View ID="vPending" runat="server">
                                
                                    <div class="row">
                                        <div class="col-md-4">
                                            <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="grdPending"
                                                Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="table-responsive">
                                            <telerik:RadGrid ID="grdPending" runat="server" AllowSorting="True" AllowPaging="True"
                                                Width="100%" PageSize="15"  GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                                OnItemCommand="grdPending_ItemCommand" ShowFooter="True" meta:resourcekey="grdPendingResource1">
                                                <GroupingSettings CaseSensitive="False" />
                                                <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                                <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="SubstituteId,FK_EmployeeId">
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="SubstituteId" UniqueName="SubstituteId" Visible="False"
                                                            AllowFiltering="False" meta:resourcekey="GridBoundColumnResource1" />
                                                        <telerik:GridBoundColumn DataField="FK_EmployeeId" UniqueName="FK_EmployeeId" Visible="False"
                                                            AllowFiltering="False" meta:resourcekey="GridBoundColumnResource2" />
                                                        <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" UniqueName="EmployeeNo"
                                                            meta:resourcekey="GridBoundColumnResource3" />
                                                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                                                            meta:resourcekey="GridBoundColumnResource4" />
                                                        <telerik:GridBoundColumn DataField="LeaveDate" HeaderText="Leave Date" DataFormatString="{0:dd/M/yyyy}"
                                                            UniqueName="LeaveDate" meta:resourcekey="GridBoundColumnResource5" />
                                                        <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdnEmployeeNameAr" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                                                <asp:LinkButton ID="lnkConfirm" runat="server" OnClick="lnkConfirm_Click" CommandName="confirm"
                                                                    meta:resourcekey="lnkConfirmResource1">
                                                                </asp:LinkButton>
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
                                                </MasterTableView>
                                                <SelectedItemStyle ForeColor="Maroon" />
                                            </telerik:RadGrid>
                                            </div>
                                        </div>
                            </asp:View>
                            <asp:View ID="vPendingDetails" runat="server">
                                
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label ID="lblEmployeeNo" runat="server" CssClass="Profiletitletxt" Text="Employee No."
                                                meta:resourcekey="lblEmployeeNoResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtEmployeeNo" runat="server"  Enabled="False" meta:resourcekey="txtEmployeeNoResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label ID="lblEmployeeName" runat="server" CssClass="Profiletitletxt" Text="Employee Name"
                                                meta:resourcekey="lblEmployeeNameResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtEmployeeName" runat="server"  Enabled="False" meta:resourcekey="txtEmployeeNameResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label ID="lblLeaveDate" runat="server" CssClass="Profiletitletxt" Text="Leave Date"
                                                meta:resourcekey="lblLeaveDateResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <telerik:RadDatePicker ID="dtpLeaveDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                                Enabled="False" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                                                Culture="en-US" meta:resourcekey="dtpLeaveDateResource1">
                                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                </Calendar>
                                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                                    Width="">
                                                </DateInput>
                                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                            </telerik:RadDatePicker>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Label ID="lblSubstituteDate" runat="server" CssClass="Profiletitletxt" Text="Substitute Date"
                                                meta:resourcekey="lblSubstituteDateResource1"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <telerik:RadDatePicker ID="dtpSubstituteDate" runat="server" AllowCustomText="false"
                                                MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                                                Culture="en-US" meta:resourcekey="dtpSubstituteDateResource1">
                                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                </Calendar>
                                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                                    Width="">
                                                </DateInput>
                                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                            </telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator ID="rfvSubstituteDate" runat="server" ControlToValidate="dtpSubstituteDate"
                                                Display="None" ErrorMessage="Please Select Substitute Date" ValidationGroup="grpSave"
                                                meta:resourcekey="rfvSubstituteDateResource1"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvSubstituteDate"
                                                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                                Enabled="True">
                                            </cc1:ValidatorCalloutExtender>
                                        </div>
                                    </div>
    
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="button"
                                                ValidationGroup="grpSave" meta:resourcekey="btnSaveResource1" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"  CssClass="button"
                                                meta:resourcekey="btnCancelResource1" />
                                        </div>
                                    </div>
                            </asp:View>
                        </asp:MultiView>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabConfirmed" runat="server" HeaderText="Confirmed Substitute"
                    meta:resourcekey="TabConfirmedResource1">
                    <ContentTemplate>
                       
                            <div class="row">
                                <div class="col-md-4">
                                    <telerik:RadFilter runat="server" ID="RadFilter2" FilterContainerID="dgrdConfirmed"
                                        Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter2Resource1" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="table-responsive">
                                    <telerik:RadGrid ID="dgrdConfirmed" runat="server" AllowSorting="True" AllowPaging="True"
                                        Width="100%" PageSize="15"  GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                        ShowFooter="True" OnItemCommand="dgrdConfirmed_ItemCommand" meta:resourcekey="dgrdConfirmedResource1">
                                        <GroupingSettings CaseSensitive="False" />
                                        <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                            <Selecting AllowRowSelect="True" />
                                        </ClientSettings>
                                        <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No" UniqueName="EmployeeNo"
                                                    meta:resourcekey="GridBoundColumnResource6" />
                                                <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" UniqueName="EmployeeName"
                                                    meta:resourcekey="GridBoundColumnResource7" />
                                                <telerik:GridBoundColumn DataField="LeaveDate" HeaderText="Leave Date" DataFormatString="{0:dd/M/yyyy}"
                                                    UniqueName="LeaveDate" meta:resourcekey="GridBoundColumnResource8" />
                                                <telerik:GridBoundColumn DataField="ConfirmSubstituteDate" HeaderText="Confirmed Substitute Date"
                                                    UniqueName="ConfirmSubstituteDate" DataFormatString="{0:dd/M/yyyy}" meta:resourcekey="GridBoundColumnResource9" />
                                                <telerik:GridBoundColumn DataField="UserID" HeaderText="Confirmed By" UniqueName="UserID"
                                                    meta:resourcekey="GridBoundColumnResource10" />
                                                <telerik:GridBoundColumn DataField="ModifiedDate" HeaderText="Confirmation Date"
                                                    DataFormatString="{0:dd/M/yyyy}" UniqueName="ModifiedDate" meta:resourcekey="GridBoundColumnResource11" />
                                                <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource2">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnEmployeeNameAr2" runat="server" Value='<%# Eval("EmployeeArabicName") %>' />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                            <CommandItemTemplate>
                                                <telerik:RadToolBar ID="RadToolBar2" runat="server" OnButtonClick="RadToolBar2_ButtonClick"
                                                    Skin="Hay" meta:resourcekey="RadToolBar2Resource1">
                                                    <Items>
                                                        <telerik:RadToolBarButton runat="server" CausesValidation="False" CommandName="FilterRadGrid"
                                                            ImagePosition="Right" ImageUrl="~/images/RadFilter.gif" Owner="" Text="Apply filter"
                                                            meta:resourcekey="RadToolBarButtonResource2">
                                                        </telerik:RadToolBarButton>
                                                    </Items>
                                                </telerik:RadToolBar>
                                            </CommandItemTemplate>
                                        </MasterTableView><SelectedItemStyle ForeColor="Maroon" />
                                    </telerik:RadGrid>
                                </div>
                            </div>
     
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
