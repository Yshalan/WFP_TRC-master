<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="Emp_Shifts_ModificationDate.aspx.vb"
    Inherits="DailyTasks_Emp_Shifts_ModificationDate" Theme="SvTheme" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <uc1:PageHeader ID="PageHeader1" HeaderText="Shifts Modification Period" runat="server" />
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="rblModificationType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" meta:resourcekey="rblModificationTypeResource1">
                        <asp:ListItem Value="1" Text="Repeated Monthly" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Specific Period" meta:resourcekey="ListItemResource2"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div id="dvDays" runat="server">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblFromDay" runat="server" Text="From Day" meta:resourcekey="lblFromDayResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadComboBox ID="cmbxFromDay" runat="server" Skin="Vista" meta:resourcekey="cmbxFromDayResource1">
                            <Items>
                                <telerik:RadComboBoxItem Value="-1" Text="--Please Select--" runat="server" meta:resourcekey="RadComboBoxItemResource1"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="1" Text="1" runat="server" meta:resourcekey="RadComboBoxItemResource2"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="2" Text="2" runat="server" meta:resourcekey="RadComboBoxItemResource3"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="3" Text="3" runat="server" meta:resourcekey="RadComboBoxItemResource4"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="4" Text="4" runat="server" meta:resourcekey="RadComboBoxItemResource5"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="5" Text="5" runat="server" meta:resourcekey="RadComboBoxItemResource6"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="6" Text="6" runat="server" meta:resourcekey="RadComboBoxItemResource7"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="7" Text="7" runat="server" meta:resourcekey="RadComboBoxItemResource8"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="8" Text="8" runat="server" meta:resourcekey="RadComboBoxItemResource9"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="9" Text="9" runat="server" meta:resourcekey="RadComboBoxItemResource10"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="10" Text="10" runat="server" meta:resourcekey="RadComboBoxItemResource11"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="11" Text="11" runat="server" meta:resourcekey="RadComboBoxItemResource12"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="12" Text="12" runat="server" meta:resourcekey="RadComboBoxItemResource13"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="13" Text="13" runat="server" meta:resourcekey="RadComboBoxItemResource14"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="14" Text="14" runat="server" meta:resourcekey="RadComboBoxItemResource15"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="15" Text="15" runat="server" meta:resourcekey="RadComboBoxItemResource16"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="16" Text="16" runat="server" meta:resourcekey="RadComboBoxItemResource17"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="17" Text="17" runat="server" meta:resourcekey="RadComboBoxItemResource18"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="18" Text="18" runat="server" meta:resourcekey="RadComboBoxItemResource19"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="19" Text="19" runat="server" meta:resourcekey="RadComboBoxItemResource20"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="20" Text="20" runat="server" meta:resourcekey="RadComboBoxItemResource21"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="21" Text="21" runat="server" meta:resourcekey="RadComboBoxItemResource22"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="22" Text="22" runat="server" meta:resourcekey="RadComboBoxItemResource23"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="23" Text="23" runat="server" meta:resourcekey="RadComboBoxItemResource24"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="24" Text="24" runat="server" meta:resourcekey="RadComboBoxItemResource25"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="25" Text="25" runat="server" meta:resourcekey="RadComboBoxItemResource26"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="26" Text="26" runat="server" meta:resourcekey="RadComboBoxItemResource27"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="27" Text="27" runat="server" meta:resourcekey="RadComboBoxItemResource28"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="28" Text="28" runat="server" meta:resourcekey="RadComboBoxItemResource29"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="29" Text="29" runat="server" meta:resourcekey="RadComboBoxItemResource30"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="30" Text="30" runat="server" meta:resourcekey="RadComboBoxItemResource31"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="31" Text="31" runat="server" meta:resourcekey="RadComboBoxItemResource32"></telerik:RadComboBoxItem>
                            </Items>
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="rfvFromDay" runat="server" ControlToValidate="cmbxFromDay" InitialValue="--Please Select--"
                            Display="None" ErrorMessage="Please Select From Day" ValidationGroup="grpSave" meta:resourcekey="rfvFromDayResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="vceFromDay" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="rfvFromDay" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblToDay" runat="server" Text="To Day" meta:resourcekey="lblToDayResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadComboBox ID="cmbxToDay" runat="server" Skin="Vista" meta:resourcekey="cmbxToDayResource1">
                            <Items>
                                <telerik:RadComboBoxItem Value="-1" Text="--Please Select--" runat="server" meta:resourcekey="RadComboBoxItemResource33"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="1" Text="1" runat="server" meta:resourcekey="RadComboBoxItemResource34"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="2" Text="2" runat="server" meta:resourcekey="RadComboBoxItemResource35"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="3" Text="3" runat="server" meta:resourcekey="RadComboBoxItemResource36"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="4" Text="4" runat="server" meta:resourcekey="RadComboBoxItemResource37"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="5" Text="5" runat="server" meta:resourcekey="RadComboBoxItemResource38"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="6" Text="6" runat="server" meta:resourcekey="RadComboBoxItemResource39"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="7" Text="7" runat="server" meta:resourcekey="RadComboBoxItemResource40"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="8" Text="8" runat="server" meta:resourcekey="RadComboBoxItemResource41"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="9" Text="9" runat="server" meta:resourcekey="RadComboBoxItemResource42"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="10" Text="10" runat="server" meta:resourcekey="RadComboBoxItemResource43"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="11" Text="11" runat="server" meta:resourcekey="RadComboBoxItemResource44"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="12" Text="12" runat="server" meta:resourcekey="RadComboBoxItemResource45"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="13" Text="13" runat="server" meta:resourcekey="RadComboBoxItemResource46"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="14" Text="14" runat="server" meta:resourcekey="RadComboBoxItemResource47"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="15" Text="15" runat="server" meta:resourcekey="RadComboBoxItemResource48"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="16" Text="16" runat="server" meta:resourcekey="RadComboBoxItemResource49"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="17" Text="17" runat="server" meta:resourcekey="RadComboBoxItemResource50"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="18" Text="18" runat="server" meta:resourcekey="RadComboBoxItemResource51"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="19" Text="19" runat="server" meta:resourcekey="RadComboBoxItemResource52"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="20" Text="20" runat="server" meta:resourcekey="RadComboBoxItemResource53"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="21" Text="21" runat="server" meta:resourcekey="RadComboBoxItemResource54"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="22" Text="22" runat="server" meta:resourcekey="RadComboBoxItemResource55"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="23" Text="23" runat="server" meta:resourcekey="RadComboBoxItemResource56"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="24" Text="24" runat="server" meta:resourcekey="RadComboBoxItemResource57"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="25" Text="25" runat="server" meta:resourcekey="RadComboBoxItemResource58"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="26" Text="26" runat="server" meta:resourcekey="RadComboBoxItemResource59"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="27" Text="27" runat="server" meta:resourcekey="RadComboBoxItemResource60"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="28" Text="28" runat="server" meta:resourcekey="RadComboBoxItemResource61"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="29" Text="29" runat="server" meta:resourcekey="RadComboBoxItemResource62"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="30" Text="30" runat="server" meta:resourcekey="RadComboBoxItemResource63"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Value="31" Text="31" runat="server" meta:resourcekey="RadComboBoxItemResource64"></telerik:RadComboBoxItem>
                            </Items>
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="rfvToDay" runat="server" ControlToValidate="cmbxToDay" InitialValue="--Please Select--"
                            Display="None" ErrorMessage="Please Select To Day" ValidationGroup="grpSave" meta:resourcekey="rfvToDayResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="vceToDay" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="rfvToDay" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
            </div>
            <div id="dvSpecificDate" runat="server" visible="False">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblFromDate" runat="server" Text="From Date" meta:resourcekey="lblFromDateResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker ID="dtpFromDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                            PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US" meta:resourcekey="dtpFromDateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                Width="" LabelWidth="64px">
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="dtpFromDate"
                            Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="grpSave" meta:resourcekey="rfvFromDateResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="vceromDate" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="rfvFromDate" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblToDate" runat="server" Text="To Date" meta:resourcekey="lblToDateResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker ID="dtpToDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                            PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="en-US" meta:resourcekey="dtpToDateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" EnableWeekends="True">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                Width="" LabelWidth="64px">
                                <EmptyMessageStyle Resize="None" />
                                <ReadOnlyStyle Resize="None" />
                                <FocusedStyle Resize="None" />
                                <DisabledStyle Resize="None" />
                                <InvalidStyle Resize="None" />
                                <HoveredStyle Resize="None" />
                                <EnabledStyle Resize="None" />
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="dtpToDate"
                            Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="grpSave" meta:resourcekey="rfvToDateResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="vceToDate" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="rfvToDate" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                        <asp:CompareValidator ID="cvDate" runat="server" ControlToCompare="dtpFromDate"
                            ControlToValidate="dtpToDate" Display="None" ErrorMessage="To Date must be greater than or Equal From Date!"
                            Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave" meta:resourcekey="cvDateResource1"></asp:CompareValidator>
                        <cc1:ValidatorCalloutExtender TargetControlID="cvDate" ID="vceDate"
                            runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </div>
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
                    <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdModificationDate"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1">
                        <ContextMenu FeatureGroupID="rfContextMenu">
                        </ContextMenu>
                    </telerik:RadFilter>
                    <telerik:RadGrid ID="dgrdModificationDate" runat="server" AllowPaging="True"
                        AllowSorting="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        AutoGenerateColumns="False" PageSize="25" OnItemCommand="dgrdModificationDate_ItemCommand"
                        ShowFooter="True" CellSpacing="0" meta:resourcekey="dgrdModificationDateResource1">
                        <GroupingSettings CaseSensitive="False" />
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                            EnableRowHoverStyle="True">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="ModificationId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False"
                                    UniqueName="TemplateColumn" FilterControlAltText="Filter TemplateColumn column" meta:resourcekey="GridTemplateColumnResource1">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" meta:resourcekey="chkResource1" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="ModificationId" HeaderText="ModificationId"
                                    SortExpression="ModificationId" Visible="False" UniqueName="ModificationId" FilterControlAltText="Filter ModificationId column" meta:resourcekey="GridBoundColumnResource1" />
                                <telerik:GridBoundColumn DataField="DateOption" HeaderText="Date Option" SortExpression="DateOption"
                                    UniqueName="DateOption" FilterControlAltText="Filter DateOption column" meta:resourcekey="GridBoundColumnResource2" />
                                <telerik:GridBoundColumn DataField="FromDay" HeaderText="From Day" SortExpression="FromDay"
                                    UniqueName="FromDay" FilterControlAltText="Filter FromDay column" meta:resourcekey="GridBoundColumnResource3" />
                                <telerik:GridBoundColumn DataField="ToDay" HeaderText="ToDay" SortExpression="ToDay"
                                    UniqueName="ToDay" FilterControlAltText="Filter ToDay column" meta:resourcekey="GridBoundColumnResource4" />
                                <telerik:GridBoundColumn DataField="FromDate" HeaderText="From Date" DataFormatString="{0: dd/MM/yyyy}"
                                    SortExpression="FromDate" UniqueName="FromDate" FilterControlAltText="Filter FromDate column" meta:resourcekey="GridBoundColumnResource5" />
                                <telerik:GridBoundColumn DataField="ToDate" HeaderText="To Date" DataFormatString="{0: dd/MM/yyyy}"
                                    SortExpression="ToDate" UniqueName="ToDate" FilterControlAltText="Filter ToDate column" meta:resourcekey="GridBoundColumnResource6" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    Skin="Hay" meta:resourcekey="RadToolBar1Resource1" SingleClick="None">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1"
                                            Owner="" />
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
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

