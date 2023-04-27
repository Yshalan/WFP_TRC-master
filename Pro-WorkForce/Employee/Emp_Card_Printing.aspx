<%@ Page Title="" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Emp_Card_Printing.aspx.vb" Inherits="Employee_Emp_Card_Printing" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .style2 {
            width: 100%;
        }
    </style>

    <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Requested Employee Card Printing" />
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblCardTypeEn" runat="server" CssClass="Profiletitletxt" Text="Card Type English"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtCardTypeEn" runat="server" meta:resourcekey="txtDescResource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="reqtxtCardTypeEn" runat="server" ControlToValidate="txtCardTypeEn"
                Display="None" ErrorMessage="Please Enter Card Type English" ValidationGroup="grpSave" meta:resourcekey="reqDescResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vceDesc" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="reqtxtCardTypeEn" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>

    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblCardtypeAr" runat="server" CssClass="Profiletitletxt" Text="Card Type Arabic"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtCardtypeAr" runat="server" meta:resourcekey="txtDescResource1"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCardtypeAr" runat="server" ControlToValidate="txtCardtypeAr"
                Display="None" ErrorMessage="Please Enter Card Type Arabic" ValidationGroup="grpSave" meta:resourcekey="reqDescResource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="rfvCardtypeAr" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblTemplate" runat="server" CssClass="Profiletitletxt" Text="Card Template"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="ddlCardTemplate" AllowCustomText="false" MarkFirstMatch="true"
                Skin="Vista" runat="server" ValidationGroup="VGCards">
            </telerik:RadComboBox>
            <asp:RequiredFieldValidator ID="rfvddlCardTemplate" runat="server" ErrorMessage="Please Select Card Template"
                ControlToValidate="ddlCardTemplate" ValidationGroup="btnPrint" InitialValue="--Please Select--"
                Display="None" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="rfvddlCardTemplate" WarningIconImageUrl="~/images/warning1.png"
                Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblLeaveRequestManagerLevelRequired" runat="server" Text="Card Request Manager Level Required"
                meta:resourcekey="lblLeaveRequestManagerLevelRequiredResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="radcmbLevels" Filter="Contains" MarkFirstMatch="true" Skin="Vista"
                runat="server">
            </telerik:RadComboBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblCardApproval" runat="server" CssClass="Profiletitletxt" Text="Card Approval"></asp:Label>
        </div>
        <div class="col-md-10">
            <asp:RadioButtonList ID="rlstApproval" runat="server">
                <asp:ListItem Text="Direct Manager" Value="1" meta:resourcekey="rdbtnDirectmgrResource1"
                    Selected="True"></asp:ListItem>
               <%-- <asp:ListItem Text="Human Resource" Value="2" meta:resourcekey="rdbtnHROnlyResource1"></asp:ListItem>--%>
                <asp:ListItem Text="DM,HR" Value="3" meta:resourcekey="rdbtnBothResource1"></asp:ListItem>
                <asp:ListItem Text="DM,GM,HR" Value="4" meta:resourcekey="rdbtnDMHRGMResource1"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblDesignationCard" runat="server" CssClass="Profiletitletxt" Text="Allowed Designation"></asp:Label>
        </div>
        <div class="col-md-10">
            <asp:CheckBoxList ID="chklstAlwdDesg" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
            </asp:CheckBoxList>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
        </div>
    </div>

    <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-4">
            <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="Validation" OnClick="btnSave_Click" CssClass="button" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" ValidationGroup="Validation" OnClick="btnClear_Click" CssClass="button" />
        </div>
        <div class="col-md-4">
        </div>
    </div>
    <div class="row">
        <div class="table-responsive">
            <telerik:RadGrid ID="dgrdCardTypes" runat="server" AllowSorting="True" AllowPaging="True"
                Width="100%" PageSize="15" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                ShowFooter="True" meta:resourcekey="dgrdCardRequestsResource1">
                <GroupingSettings CaseSensitive="False" />
                <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <MasterTableView AllowMultiColumnSorting="True" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="CardTypeId">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="CardTypeEn" HeaderText="Card Type En" UniqueName="CardTypeEn"
                            meta:resourcekey="GridBoundColumnResource1" />
                        <telerik:GridBoundColumn DataField="CardTypeAr" HeaderText="Card Type Ar" UniqueName="CardTypeAr"
                            meta:resourcekey="GridBoundColumnResource2" />
                        <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="lnkEdit_Click"
                                    CommandName="accept" meta:resourcekey="lnkAcceptResource1"></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                    <%-- <CommandItemTemplate>
                                    <telerik:RadToolBar ID="RadToolBar1" runat="server" OnButtonClick="RadToolBar1_ButtonClick"
                                        Skin="Hay" meta:resourcekey="RadToolBar1Resource1">
                                        <Items>
                                            <telerik:RadToolBarButton runat="server" CausesValidation="False" CommandName="FilterRadGrid"
                                                ImagePosition="Right" ImageUrl="~/images/RadFilter.gif" Owner="" Text="Apply filter"
                                                meta:resourcekey="RadToolBarButtonResource1">
                                            </telerik:RadToolBarButton>
                                        </Items>
                                    </telerik:RadToolBar>
                                </CommandItemTemplate>--%>
                </MasterTableView><SelectedItemStyle ForeColor="Maroon" />
            </telerik:RadGrid>
        </div>
    </div>
</asp:Content>
