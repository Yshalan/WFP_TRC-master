<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AuthorityEntry.aspx.vb"
    Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" Inherits="SelfServices_AuthorityEntry"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">

   

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Authority Entry" />
    <div class="row">

        <div class="col-md-2">
            <asp:Label ID="lblDate" runat="server" CssClass="Profiletitletxt" Text="Date" meta:resourcekey="lblDateResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadDatePicker ID="RadDatePicker1" runat="server" AutoPostBack="True" Culture="en-US"
                meta:resourcekey="RadDatePicker1Resource1">
                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                </Calendar>
                <DateInput AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                    LabelCssClass="" Width="">
                </DateInput>
                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="RadDatePicker1"
                Display="None" ErrorMessage="Please Select Date,The Max Date Allowed is Today"
                meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True"
                TargetControlID="RequiredFieldValidator7">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblAuthority" runat="server" CssClass="Profiletitletxt" Text="Authority Name"
                meta:resourcekey="lblAuthorityResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="RadCmbAuthorityName" runat="server" AppendDataBoundItems="True"
                MarkFirstMatch="True" Skin="Vista" meta:resourcekey="RadCmbAuthorityNameResource1">
            </telerik:RadComboBox>
            <asp:RequiredFieldValidator ID="reqAuthorityName" runat="server" ControlToValidate="RadCmbAuthorityName"
                Display="None" ErrorMessage="Please select Authority Name" InitialValue="--Please Select--"
                ValidationGroup="Grp1" meta:resourcekey="reqAuthorityNameResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ExtenderreqAuthorityName" runat="server" CssClass="AISCustomCalloutStyle"
                Enabled="True" TargetControlID="reqAuthorityName" WarningIconImageUrl="~/images/warning1.png">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblReason" runat="server" CssClass="Profiletitletxt" Text="Reason" meta:resourcekey="lblReasonResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="RadCmbReason" runat="server" MarkFirstMatch="True" AppendDataBoundItems="True"
                Skin="Vista" CausesValidation="False" meta:resourcekey="RadCmbReasonResource1" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                InitialValue="--Please Select--" ErrorMessage="Please Select Reason" ValidationGroup="Grp1"
                ControlToValidate="RadCmbReason" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                CssClass="AISCustomCalloutStyle" TargetControlID="RequiredFieldValidator4" WarningIconImageUrl="~/images/warning1.png">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblTime" runat="server" Text="Time" CssClass="Profiletitletxt" meta:resourcekey="lblTimeResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadMaskedTextBox ID="rmtToTime2" runat="server" Mask="##:##" TextWithLiterals="00:00"
                Text='<%# DataBinder.Eval(Container,"DataItem.ToTime2") %>' DisplayMask="##:##"
                LabelCssClass="" meta:resourcekey="rmtToTime2Resource1">
            </telerik:RadMaskedTextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="None"
                ErrorMessage="Please Time" InitialValue="00:00" ControlToValidate="rmtToTime2"
                meta:resourcekey="RequiredFieldValidator8Resource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True"
                TargetControlID="RequiredFieldValidator8">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblRemarks" runat="server" Text="Remarks" CssClass="Profiletitletxt"
                meta:resourcekey="lblRemarksResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtremarks" Text="Authority Entry" runat="server" TextMode="MultiLine" Width="225px" meta:resourcekey="txtremarksResource1"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-2">
            <asp:Button ID="btnSignIn" runat="server" SkinID="signin" Text="Button" ValidationGroup="Grp1"
                Style="background-color: Green !important;" meta:resourcekey="btnSignInResource1"></asp:Button>
            <%--<asp:ImageButton ID="btnSignIn" runat="server" Width="120px"  Text="Sign In" ImageUrl="~/images/SignINN.png"  ValidationGroup="Grp1" meta:resourcekey="btnSignInResource1"  />
            --%>
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnSignOut" runat="server" SkinID="signout" Text="Sign Out" meta:resourcekey="btnSignOutResource1"
                ValidationGroup="Grp1" Style="background-color: red !important;"></asp:Button>
            <%--<asp:ImageButton ID="btnSignOut" runat="server" Width="120px" Text="Sign Out" ImageUrl="~/images/SignOutt.png" meta:resourcekey="btnSignOutResource1" />
            --%>
        </div>
    </div>



    <div class="row">
        <div class="table-responsive ">
            <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdAuthorityEntry" Skin="Hay"
                ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
            <telerik:RadGrid runat="server" ID="dgrdAuthorityEntry" AutoGenerateColumns="False"
                PageSize="15" AllowPaging="True" AllowSorting="True" GridLines="None"
                Width="100%" meta:resourcekey="dgrdAuthorityEntryResource1">
                <GroupingSettings CaseSensitive="False" />
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                    EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" DataKeyNames="ReasonArabicName">
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                    <Columns>

                        <telerik:GridBoundColumn DataField="ReasonName" HeaderText="Reason" meta:resourcekey="GridBoundColumnResource3"
                            SortExpression="ReasonName" UniqueName="ReasonName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="False" HeaderText="Reason" DataField="ReasonArabicName" meta:resourcekey="GridBoundColumnResource3"
                            UniqueName="ReasonArabicName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="False" HeaderText="AuthorityName" DataField="AuthorityName" meta:resourcekey="GridBoundColumnResource11"
                            UniqueName="AuthorityName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="False" HeaderText="AuthorityName" DataField="AuthorityArabicName" meta:resourcekey="GridBoundColumnResource11"
                            UniqueName="AuthorityArabicName">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MoveDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date"
                            meta:resourcekey="GridBoundColumnResource4" SortExpression="MoveDate" UniqueName="MoveDate">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MoveTime" DataFormatString="{0:HH:mm}" HeaderText="Time"
                            meta:resourcekey="GridBoundColumnResource5" SortExpression="MoveTime" UniqueName="MoveTime">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks" meta:resourcekey="GridBoundColumnResource6"
                            SortExpression="Remarks" UniqueName="Remarks">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AuthorityMoveId" Visible="false"
                            SortExpression="AuthorityMoveId" UniqueName="AuthorityMoveId">
                        </telerik:GridBoundColumn>
                    </Columns>
                    <CommandItemTemplate>
                        <telerik:RadToolBar ID="RadToolBar1" runat="server" meta:resourcekey="RadToolBar1Resource1"
                            OnButtonClick="RadToolBar1_ButtonClick" Skin="Hay">
                            <Items>
                                <telerik:RadToolBarButton runat="server" CommandName="FilterRadGrid" ImagePosition="Right"
                                    ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource1"
                                    Owner="" Text="Apply filter">
                                </telerik:RadToolBarButton>
                            </Items>
                        </telerik:RadToolBar>
                    </CommandItemTemplate>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </div>
</asp:Content>
