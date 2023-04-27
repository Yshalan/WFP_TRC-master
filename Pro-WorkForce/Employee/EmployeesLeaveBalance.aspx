<%@ Page Language="VB" AutoEventWireup="false" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    CodeFile="EmployeesLeaveBalance.aspx.vb" Inherits="Admin_EmployeesLeaveBalance"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc1" %>
<%--<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="PageFilter"
    TagPrefix="uc1" %>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

        function ValidateTextboxFrom() {

            var tmpTime1 = $find("<%=rmtHoursBalance.ClientID %>");
            txtValidate(tmpTime1, true);
        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" runat="server" HeaderText="Employees Leave Balance" />
            <asp:MultiView ID="mvEmpLastBalance" runat="server">
                <asp:View ID="viewEmpLastBalance" runat="server">

                    <div class="row">
                        <div class="col-md-12">
                            <uc1:PageFilter ID="EmployeeFilterUC" runat="server" ValidationGroup="ValidateGet"
                                ShowRadioSearch="false" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblLeaveType" runat="server" CssClass="Profiletitletxt" Text="Leave Type"
                                meta:resourcekey="lblLeaveTypeResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="RadCmbBxLeaveType" runat="server" MarkFirstMatch="True"
                                Skin="Vista" meta:resourcekey="RadCmbBxLeaveTypeResource1" Style="width: 350px" ExpandDirection="Up">
                            </telerik:RadComboBox>
                            <%-- <asp:RequiredFieldValidator ID="rfvLeaveType" runat="server" ControlToValidate="RadCmbBxLeaveType"
                                   InitialValue="--Please Select--" Display="None" ErrorMessage="Please Select Leave Type" ValidationGroup="ValidateGet"
                                    meta:resourcekey="rfvLeaveTypeResource1" />
                                <cc1:ValidatorCalloutExtender ID="vceLeaveType" runat="server" Enabled="True" TargetControlID="rfvLeaveType"
                                    CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>--%>
                            <asp:LinkButton ID="lbtnEditBalance" runat="server" OnClick="btn_ClickLnk" ValidationGroup="ValidateGet"
                                meta:resourcekey="lbtnAddEditBalanceResource1">Add\Edit Balance</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnGet" runat="server" CssClass="button" Text="Get" ValidationGroup="ValidateGet"
                                meta:resourcekey="btnGetResource1" />
                        </div>
                    </div>

                    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                    <div class="filterDiv">
                        <telerik:RadFilter runat="server" ID="RadFilter1" Skin="Hay" FilterContainerID="dgrdLastBalance"
                            ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <telerik:RadGrid runat="server" ID="dgrdLastBalance" AutoGenerateColumns="False"
                                PageSize="15" AllowPaging="True" AllowSorting="True" AllowFilteringByColumn="True"
                                GridLines="None" meta:resourcekey="dgrdLastBalanceResource1">
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                                    EnableRowHoverStyle="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView CommandItemDisplay="Top" AllowFilteringByColumn="False" DataKeyNames="EmployeeNo,EmployeeName,LeaveName,TotalBalance,LeaveId,FK_EmployeeId,EmployeeArabicName,LeaveArabicName">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee No." DataType="System.Int32"
                                            SortExpression="EmployeeNo" UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource1">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"
                                            UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource2">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="EmployeeArabicName" Visible="false" />
                                        <telerik:GridBoundColumn DataField="LeaveName" HeaderText="Leave Type" SortExpression="LeaveName"
                                            UniqueName="LeaveName" meta:resourcekey="GridBound1ColumnResource1">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LeaveArabicName" Visible="false" />
                                        <telerik:GridBoundColumn DataField="TotalBalance" DataType="System.Double" HeaderText="Total Balance"
                                            meta:resourcekey="GridBoundColumnResource3" UniqueName="TotalBalance">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="LeaveId" DataType="System.Int32" Visible="False"
                                            HeaderText="Leave Id" meta:resourcekey="GridBoundColumnResource4" UniqueName="LeaveId">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FK_EmployeeId" DataType="System.Int32" Visible="False"
                                            HeaderText="Employee Id" meta:resourcekey="GridBoundColumnResource5" UniqueName="FK_EmployeeId">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks"
                                            UniqueName="Remarks" meta:resourcekey="GridBoundColumnResource6">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" meta:resourcekey="GridTemplateColumnResource1">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEditBalance" runat="server" OnClick="btn_Click" meta:resourcekey="lbtnEditBalanceResource1">Edit Balance</asp:LinkButton>
                                                <asp:HiddenField ID="hdnBalanceID" runat="server" Value='<%# Eval("BalanceId") %>' />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <CommandItemTemplate>
                                        <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick"
                                            meta:resourcekey="RadToolBar1Resource1">
                                            <Items>
                                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                                    ImageUrl="~/images/RadFilter.gif" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
                                            </Items>
                                        </telerik:RadToolBar>
                                    </CommandItemTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </asp:View>
                <asp:View runat="server" ID="viewEditEmpBalance">

                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblEmpNo" runat="server" Text="Employee No" CssClass="Profiletitletxt"
                                meta:resourcekey="lblEmpNoResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtEmpNo" runat="server" Enabled="False" meta:resourcekey="txtEmpNoResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name" CssClass="Profiletitletxt"
                                meta:resourcekey="lblEmployeeNameResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtEmployeeName" runat="server" Enabled="False" meta:resourcekey="txtEmployeeNameResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblEditLeaveType" runat="server" Text="Leave Type" CssClass="Profiletitletxt"
                                meta:resourcekey="lblEditLeaveTypeResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtLeaveType" runat="server" Enabled="False" meta:resourcekey="txtLeaveTypeResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblCurrentBalance" runat="server" Text="Current Balance" CssClass="Profiletitletxt"
                                meta:resourcekey="lblCurrentBalanceResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCurrentBalance" runat="server" Enabled="False" meta:resourcekey="txtCurrentBalanceResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks" CssClass="Profiletitletxt"
                                meta:resourcekey="lblRemarksResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtRemarks" runat="server" Text="Manual Entry" TextMode="MultiLine" MaxLength="100" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-4">
                            <asp:RadioButtonList ID="rbtnlstBalance" runat="server" AutoPostBack="True" meta:resourcekey="rbtnlstBalanceResource1">
                                <asp:ListItem Text="Add Balance" Value="1" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                <asp:ListItem Text="Subtract Balance" Value="2" meta:resourcekey="ListItemResource2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblBalanceType" runat="server" Text="Balance Type" meta:resourcekey="lblBalanceTypeResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:RadioButtonList ID="rblBalanceType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                <asp:ListItem Value="1" Text="Days" Selected="True" meta:resourcekey="ListItem1Resource1"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Hours" meta:resourcekey="ListItem2Resource1"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblAmount" runat="server" Text="Amount" CssClass="Profiletitletxt"
                                meta:resourcekey="lblAmountResource1"></asp:Label>
                        </div>

                        <div class="col-md-4" id="dvDaysBalance" runat="server">
                            <%--<asp:TextBox ID="txtAmount" runat="server" AutoPostBack="True" meta:resourcekey="txtAmountResource1"></asp:TextBox>--%>
                            <telerik:RadNumericTextBox ID="txtAmount" MinValue="0" MaxValue="365" AutoPostBack="true"
                                Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="txtAmountResource1">
                                <NumberFormat DecimalDigits="2" GroupSeparator="" />
                            </telerik:RadNumericTextBox>

                            <asp:RequiredFieldValidator ID="reqAmount" runat="server" Display="None" ControlToValidate="txtAmount"
                                ValidationGroup="ValidateBalance" ErrorMessage="Please entere amount value" meta:resourcekey="reqAmountResource1" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="ValidateBalance"
                                runat="server" ControlToValidate="txtAmount" ValidationExpression="^[0-9.\-]+$"
                                ErrorMessage="Please enter valid leave balance" Display="None" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True"
                                CssClass="AISCustomCalloutStyle" TargetControlID="RegularExpressionValidator1">
                            </cc1:ValidatorCalloutExtender>
                        </div>

                        <div class="col-md-4" id="dvHoursBalance" runat="server" visible="false">
                            <telerik:RadMaskedTextBox ID="rmtHoursBalance" runat="server" Mask="##:##" TextWithLiterals="00:00" AutoPostBack="true"
                                DisplayMask="##:##" Text='0000' LabelCssClass="" meta:resourcekey="rmtHoursBalanceResource1">
                                <ClientEvents OnBlur="ValidateTextboxFrom" />
                            </telerik:RadMaskedTextBox>
                            <asp:RequiredFieldValidator ID="rfvHoursBalance" runat="server" Display="None"
                                ErrorMessage="Please Select Time" ControlToValidate="rmtHoursBalance" ValidationGroup="ValidateBalance"
                                meta:resourcekey="rfvHoursBalanceResource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="vceHoursBalance" runat="server" Enabled="True"
                                TargetControlID="rfvHoursBalance">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblNewBalance" runat="server" Text="New Balance" CssClass="Profiletitletxt"
                                meta:resourcekey="lblNewBalanceResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtNewBalance" runat="server" Enabled="False" meta:resourcekey="txtNewBalanceResource1"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnSave" runat="server" Text="Save" class="button" ValidationGroup="ValidateBalance"
                                meta:resourcekey="btnSaveResource1" />&nbsp;
                                <asp:Button ID="btnBack" runat="server" Text="Cancel" class="button" CausesValidation="False"
                                    meta:resourcekey="btnBackResource1" />
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
