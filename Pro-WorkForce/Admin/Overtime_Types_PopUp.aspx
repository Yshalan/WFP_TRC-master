<%@ Page Title="" Language="VB" MasterPageFile="~/Default/EmptyMaster.master" AutoEventWireup="false" CodeFile="Overtime_Types_PopUp.aspx.vb" Inherits="Admin_Overtime_Types_PopUp"
    Theme="SvTheme" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ValidateTextboxFrom() {

            var tmpTime1 = $find("<%=rmtOvertimeTime.ClientID %>");
            txtValidate(tmpTime1, true);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="update1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12">
                    <uc1:PageHeader ID="PageHeader1" runat="server" />
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblOvetimeTypeName" runat="server" Text="Overtime Type Name" meta:resourcekey="lblOvetimeTypeNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtOvertimeTypeName" runat="server" meta:resourcekey="txtOvertimeTypeNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvOvetimeTypeName" runat="server" ControlToValidate="txtOvertimeTypeName"
                        Display="None" ErrorMessage="Please Enter Ovetime Type Name" ValidationGroup="grpSave" meta:resourcekey="rfvOvetimeTypeNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceOvertimeTypeName" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvOvetimeTypeName" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblOvetimeTypeArabicName" runat="server" Text="Overtime Type Arabic Name" meta:resourcekey="lblOvetimeTypeArabicNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtOvetimeTypeArabicName" runat="server" meta:resourcekey="txtOvetimeTypeArabicNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvOvetimeTypeArabicName" runat="server" ControlToValidate="txtOvetimeTypeArabicName"
                        Display="None" ErrorMessage="Please Enter Ovetime Type Arabic Name" ValidationGroup="grpSave" meta:resourcekey="rfvOvetimeTypeArabicNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceOvetimeTypeArabicName" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvOvetimeTypeArabicName" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblOvertimeCalculationConsideration" runat="server" Text="Overtime Calculation Consideration"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="rblOvertimeCalculationConsideration" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                        <asp:ListItem Value="1" Text="Rate" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Time"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row" id="dvOvertimeCalcConsiderationRate" runat="server" visible="true">
                <div class="col-md-2">
                    <asp:Label ID="lblOvertimeRate" runat="server" Text="Overtime Rate" meta:resourcekey="lblOvertimeRateResource1"></asp:Label>
                </div>
                <div class="col-md-4">

                    <telerik:RadNumericTextBox ID="txtOvertimeRate" MinValue="0" MaxValue="9999999"
                        Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" DbValueFactor="1" LabelWidth="64px" meta:resourcekey="txtOvertimeRateResource1">
                        <NegativeStyle Resize="None" />
                        <NumberFormat DecimalDigits="2" GroupSeparator="" ZeroPattern="n" />
                        <EmptyMessageStyle Resize="None" />
                        <ReadOnlyStyle Resize="None" />
                        <FocusedStyle Resize="None" />
                        <DisabledStyle Resize="None" />
                        <InvalidStyle Resize="None" />
                        <HoveredStyle Resize="None" />
                        <EnabledStyle Resize="None" />
                    </telerik:RadNumericTextBox>
                    <asp:RequiredFieldValidator ID="rfvOvertimeRate" runat="server" ControlToValidate="txtOvertimeRate"
                        Display="None" ErrorMessage="Please Enter Overtime Rate" ValidationGroup="grpSave" meta:resourcekey="rfvOvertimeRateResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceOvertimeRate" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="rfvOvertimeRate" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row" id="dvOvertimeCalcConsiderationTime" runat="server" visible="false">
                <div class="col-md-2">
                    <asp:Label ID="lblOvertimeTime" runat="server" Visible="true"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadMaskedTextBox ID="rmtOvertimeTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                        DisplayMask="##:##" Text='0000' LabelCssClass=""  >
                        <ClientEvents OnBlur="ValidateTextboxFrom" />
                    </telerik:RadMaskedTextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblOvertimeConsideration" runat="server" Text="Overtime To Be Considered" meta:resourcekey="lblOvertimeConsiderationResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="rblOvertimeConsideration" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                        <asp:ListItem Value="1" Text="Financial" Selected="true" meta:resourcekey="rdbtnListItem1Resource1"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Compensate Overtime To Leave Balance" meta:resourcekey="rdbtnListItem2Resource1"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row" id="dvLeaveType" runat="server" visible="False">
                <div class="col-md-2">
                    <asp:Label ID="lblLeaveType" runat="server" Text="Leave Type" meta:resourcekey="lblLeaveTypeResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadComboBoxLeaveType" runat="server" MarkFirstMatch="True" ExpandDirection="Up"
                        Width="210px" meta:resourcekey="RadComboBoxLeaveTypeResource1">
                    </telerik:RadComboBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkMustRequested" runat="server" Text="Must Requested" meta:resourcekey="chkMustRequestedResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="grpSave" meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" meta:resourcekey="btnClearResource1" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" meta:resourcekey="btnDeleteResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdOvertimeType"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                    </telerik:RadFilter>
                    <telerik:RadGrid ID="dgrdOvertimeType" runat="server" AllowPaging="True"
                        AllowSorting="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        AutoGenerateColumns="False" PageSize="25" OnItemCommand="dgrdOvertimeType_ItemCommand"
                        ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdOvertimeTypeResource1">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                            EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="OvertimeTypeId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False"
                                    UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="OvertimeTypeId" Visible="False" UniqueName="OvertimeTypeId" FilterControlAltText="Filter OvertimeTypeId column" meta:resourcekey="GridBoundColumnResource1" />
                                <telerik:GridBoundColumn DataField="OvertimeTypeName" HeaderText="Overtime Type Name" SortExpression="OvertimeTypeName"
                                    UniqueName="OvertimeTypeName" FilterControlAltText="Filter OvertimeTypeName column" meta:resourcekey="GridBoundColumnResource2" />
                                <telerik:GridBoundColumn DataField="OvertimeTypeArabicName" HeaderText="Overtime Type Arabic Name" SortExpression="OvertimeTypeArabicName"
                                    UniqueName="OvertimeTypeArabicName" FilterControlAltText="Filter OvertimeTypeArabicName column" meta:resourcekey="GridBoundColumnResource3" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" SingleClick="None" meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server"
                                            Owner="" meta:resourcekey="RadToolBarButtonResource1" />
                                    </Items>
                                </telerik:RadToolBar>
                            </CommandItemTemplate>
                        </MasterTableView>
                        <SelectedItemStyle ForeColor="Maroon" />
                    </telerik:RadGrid>
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

