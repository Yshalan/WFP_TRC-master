<%@ Page Title="Announcements" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="Announcements.aspx.vb" Inherits="Admin_Announcements" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Src="~/Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlAnnouncements" runat="server">
        <ContentTemplate>

            <uc1:PageHeader ID="UserCtrlAnnouncements" HeaderText="Announcements" runat="server" />

            <div class="row">
                <div class="col-md-12">
                    <uc2:PageFilter ID="EmployeeFilterUC" runat="server" HeaderText="Employee Filter"
                        ValidationGroup="AnnouncementsGroup" ShowRadioSearch="true" />

                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblEnglishTitle" runat="server" CssClass="Profiletitletxt" Text="English Title" meta:resourcekey="CodeResource2"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtEnglishTitle" runat="server" meta:resourcekey="txtEnglishTitleResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqEnglishTitle" runat="server" ControlToValidate="txtEnglishTitle"
                        Display="None" ErrorMessage="Please enter an English Title" ValidationGroup="AnnouncementsGroup"
                        meta:resourcekey="reqEnglishTitleResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderReqEnglishTitle" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="reqEnglishTitle" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblArabicTitle" runat="server" CssClass="Profiletitletxt" Text="Arabic Title" meta:resourcekey="CodeResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtArabicTitle" runat="server" meta:resourcekey="txtArabicTitleResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqArabicTitle" runat="server" ControlToValidate="txtArabicTitle"
                        Display="None" ErrorMessage="Please enter an Arabic Title" ValidationGroup="AnnouncementsGroup"
                        meta:resourcekey="reqArabicTitleResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderReqArabicTitle" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="reqArabicTitle" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="rblIsSpecificDate" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                        <asp:ListItem Value="1" Text="Specific Date" Selected="True" meta:resourcekey="rblIsSpecificDateListItem1Resource1"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Specific Period" meta:resourcekey="rblIsSpecificDateListItem2Resource1"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkIsYearlyFixed" runat="server" Text="Yearly Fixed" AutoPostBack="true" meta:resourcekey="chkIsYearlyFixedResource1" />
                </div>
            </div>

            <div id="dvSpecificDate" runat="server">

                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblDate" runat="server" CssClass="Profiletitletxt" Text="Announcement Date"
                            meta:resourcekey="lblDateResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <%--                        <telerik:RadDatePicker ID="dteDate" runat="server"
                            EnableTyping="False" Culture="en-US"
                            meta:resourcekey="dteDateResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False"
                                ViewSelectorText="x" CssClass="">
                            </Calendar>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" CssClass="" />
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                ReadOnly="True" Width="">
                            </DateInput>
                        </telerik:RadDatePicker>--%>

                        <telerik:RadDatePicker ID="dteDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                            PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="English (United States)">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="reqDteDate" runat="server" ErrorMessage="Please select a date"
                            Display="None" ValidationGroup="AnnouncementsGroup" ControlToValidate="dteDate" meta:resourcekey="reqDteDateResource1"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="ExtenderReqDteDate" TargetControlID="reqDteDate"
                            runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                            Enabled="True">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
            </div>
            <div id="dvSpecificPeriod" runat="server" visible="false">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblFromDate" runat="server" Text="From Date"  meta:resourcekey="lblFromDateResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker ID="dtpFromDate" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                            PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" Culture="English (United States)">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="dtpFromDate" meta:resourcekey="rfvFromDateResource1"
                            Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="AnnouncementsGroup"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="vceFromDate" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="rfvFromDate" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblToDate" runat="server" Text="To Date" meta:resourcekey="lblToDateResource1"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <telerik:RadDatePicker ID="dtpToDate" runat="server" AllowCustomText="false" 
                            MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                            Culture="English (United States)">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="dtpToDate" meta:resourcekey="rfvToDateResource1"
                            Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="AnnouncementsGroup"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="vceToDate" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="rfvToDate" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                        <asp:CompareValidator ID="cvDate" Display="None" runat="server" ControlToValidate="dtpToDate"
                            ControlToCompare="dtpFromDate" Operator="GreaterThanEqual" Type="Date"
                            ErrorMessage="To Date Should be Greater Than or Equal to From Date" meta:resourcekey="cvDateResource1"
                            ValidationGroup="AnnouncementsGroup"></asp:CompareValidator>
                        <cc1:ValidatorCalloutExtender ID="vceDate" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="cvDate" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:RadioButtonList ID="rblLanguage" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt" meta:resourcekey="rblFormatResource1">
                        <asp:ListItem Text="User Language" Value="0" Selected="True" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="English" Value="1" meta:resourcekey="ListItemResource2"></asp:ListItem>
                        <asp:ListItem Text="Arabic" Value="2" meta:resourcekey="ListItemResource3"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblEnglishContent" runat="server" CssClass="Profiletitletxt" Text="English Content"
                        meta:resourcekey="lblEnglishContentResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtEnglishContent" runat="server" TextMode="MultiLine" Columns="50" Rows="5" meta:resourcekey="txtEnlgishContentResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqEnglishContent" runat="server" ErrorMessage="Please enter content in English"
                        Display="None" ValidationGroup="AnnouncementsGroup" ControlToValidate="txtEnglishContent" meta:resourcekey="reqEnglishContentResource1"></asp:RequiredFieldValidator>

                    <cc1:ValidatorCalloutExtender ID="ExtenderReqEnglishContent" TargetControlID="reqEnglishContent"
                        runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblArabicContent" runat="server" CssClass="Profiletitletxt" Text="Arabic Content"
                        meta:resourcekey="lblArabicContentResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtArabicContent" runat="server" TextMode="MultiLine" Columns="50" Rows="5" meta:resourcekey="txtArabicContentResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqArabicContent" runat="server" ErrorMessage="Please enter content in Arabic"
                        Display="None" ValidationGroup="AnnouncementsGroup" ControlToValidate="txtArabicContent" meta:resourcekey="reqArabicContentResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderReqArabicContent" TargetControlID="reqArabicContent"
                        runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>


            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="AnnouncementsGroup"
                        meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CssClass="button"
                        Text="Delete" meta:resourcekey="btnDeleteResource1" OnClientClick="return ValidateDelete();" />
                    <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="button"
                        Text="Clear" meta:resourcekey="btnClearResource1" />
                </div>
            </div>

            <div class="table-responsive">
                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdVwAnnouncements"
                    ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                <telerik:RadGrid ID="dgrdVwAnnouncements" runat="server" AllowSorting="True"
                    AllowPaging="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                    ShowFooter="True" OnItemCommand="dgrdVwAnnouncements_ItemCommand"
                    meta:resourcekey="dgrdVwAnnouncementsResource1">
                    <SelectedItemStyle ForeColor="Maroon" />
                    <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="ID,Fk_EmployeeId,Fk_EntityId,Fk_CompanyId">
                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" Text="&nbsp;" runat="server" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AllowFiltering="False" DataField="ID" HeaderText="ID"
                                SortExpression="ID" Visible="False" meta:resourcekey="GridBoundColumnResource1"
                                UniqueName="ID">
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="AnnouncementDate" HeaderText="Announcement Date" SortExpression="AnnouncementDate"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource2" UniqueName="AnnouncementDate" DataFormatString="{0:dd MM yyyy}">
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="Title_En" HeaderText=" English Title"
                                SortExpression="Title_En"
                                meta:resourcekey="GridBoundColumnResource3" UniqueName="Title_En">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Title_Ar" HeaderText="Arabic Title"
                                SortExpression="Title_Ar" Visible="False"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource4"
                                UniqueName="Title_Ar">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Content_En" HeaderText="English Content" SortExpression="Content_En"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource5" UniqueName="Content_En">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Content_Ar" HeaderText="Arabic Content"
                                SortExpression="Content_Ar" Visible="False"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource6"
                                UniqueName="Content_Ar">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EmployeeName" HeaderText="Employee Name"
                                SortExpression="EmployeeName"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource7"
                                UniqueName="EmployeeName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EmployeeArabicName" HeaderText="Employee Arabic Name"
                                SortExpression="EmployeeArabicName" Visible="False"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource8"
                                UniqueName="EmployeeArabicName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CompanyName" HeaderText="Company"
                                SortExpression="CompanyName"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource9"
                                UniqueName="CompanyName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CompanyArabicName" HeaderText="Company Arabic Name"
                                SortExpression="CompanyArabicName" Visible="False"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource10"
                                UniqueName="CompanyArabicName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WorkLocationName" HeaderText="Work Location"
                                SortExpression="WorkLocationName"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource11"
                                UniqueName="WorkLocationName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WorkLocationArabicName" HeaderText="Work Location Arabic Name"
                                SortExpression="WorkLocationArabicName" Visible="False"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource12"
                                UniqueName="WorkLocationArabicName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EntityName" HeaderText="Entity"
                                SortExpression="EntityName"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource13"
                                UniqueName="EntityName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="EntityArabicName" HeaderText="Entity Arabic Name"
                                SortExpression="EntityArabicName" Visible="False"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource14"
                                UniqueName="EntityArabicName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GroupName" HeaderText="Logical Group"
                                SortExpression="GroupName"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource15"
                                UniqueName="GroupName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="GroupArabicName" HeaderText="Group Arabic Name"
                                SortExpression="GroupArabicName" Visible="False"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource16"
                                UniqueName="GroupArabicName">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Fk_EmployeeId" HeaderText="Employee ID"
                                SortExpression="Fk_EmployeeId" Visible="False"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource17"
                                UniqueName="Fk_EmployeeId">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="LanguageSelection" HeaderText="Language Selection"
                                SortExpression="LanguageSelection" Visible="False"
                                Resizable="False" meta:resourcekey="GridBoundColumnResource18"
                                UniqueName="LanguageSelection">
                            </telerik:GridBoundColumn>
                              <telerik:GridBoundColumn DataField="IsSpecificDate"  AllowFiltering="false"
                                SortExpression="IsSpecificDate" Display="False"
                                Resizable="False"  
                                UniqueName="IsSpecificDate">
                            </telerik:GridBoundColumn>
                              <telerik:GridBoundColumn DataField="IsYearlyFixed" AllowFiltering="false"
                                SortExpression="IsYearlyFixed" Display="False"
                                Resizable="False"  
                                UniqueName="IsYearlyFixed">
                            </telerik:GridBoundColumn>

                                        <telerik:GridBoundColumn DataField="FromDate" AllowFiltering="false"
                                SortExpression="FromDate" Display="False"
                                Resizable="False"  
                                UniqueName="FromDate">
                            </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="ToDate" AllowFiltering="false"
                                SortExpression="ToDate" Display="False"
                                Resizable="False"  
                                UniqueName="ToDate">
                            </telerik:GridBoundColumn>

                        </Columns>
                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        <CommandItemTemplate>
                            <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1"
                                meta:resourcekey="RadToolBar1Resource1">
                                <Items>
                                    <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                        ImagePosition="Left" runat="server"
                                        meta:resourcekey="RadToolBarButtonResource1" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <GroupingSettings CaseSensitive="False" />
                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True" EnablePostBackOnRowClick="True"
                        EnableRowHoverStyle="True">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdVwAnnouncements.ClientID %>");
            var masterTable = grid.get_masterTableView();
            var value = false;
            for (var i = 0; i < masterTable.get_dataItems().length; i++) {
                var gridItemElement = masterTable.get_dataItems()[i].findElement("chk");
                if (gridItemElement.checked) {
                    value = true;
                }
            }
            if (value === false) {
                ShowMessage("<%= Resources.Strings.ErrorDeleteRecourd %>");
            }
            return value;
        }
    </script>
</asp:Content>

