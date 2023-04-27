<%@ Page Title="Announcements Templates" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="Announcements_Templates.aspx.vb" Inherits="Admin_AnnouncementsTemplates" meta:resourcekey="PageResource1" uiculture="auto" %>
    
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

            <uc1:PageHeader ID="UserCtrlAnnouncements" HeaderText="Announcements Templates" runat="server" />

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblTemplateType" runat="server" CssClass="Profiletitletxt" Text="Template Type" meta:resourcekey="lblTemplateTypeResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadComboTemplateType" CausesValidation="False" Filter="Contains"
                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server"
                        meta:resourcekey="RadComboTemplateTypeResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="rfvradViolationsReports" runat="server" ControlToValidate="RadComboTemplateType"
                        Display="None" ErrorMessage="Please Select Template Type" InitialValue="--Please Select--"
                        ValidationGroup="AnnouncementsGroup" meta:resourcekey="rfvradViolationsReportsResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="vceradViolationsReports" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="rfvradViolationsReports" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row" id="trLeaveType" visible="False" runat="server">
                <div class="col-md-2">
                    <asp:Label ID="lblLeaveType" runat="server" CssClass="Profiletitletxt" Text="Leave Type" meta:resourcekey="lblLeaveTypeResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadComboLeaveType" CausesValidation="False" Filter="Contains"
                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server"
                        meta:resourcekey="RadComboLeaveTypeResource1">
                    </telerik:RadComboBox>
                </div>
            </div>
            <div id="trrblIsStart" runat="server" visible="False" class="row">
                <div class="col-md-2"></div>
                    <div class="col-md-8">
                        <asp:RadioButtonList ID="rblIsStart" runat="server" RepeatDirection="Horizontal" CssClass="Profiletitletxt"
                            meta:resourcekey="rblFormatResource1">
                            <asp:ListItem Text="At leave start" Value="1" Selected="True" meta:resourcekey="rblIsStartResource1"></asp:ListItem>
                            <asp:ListItem Text="When comeback from leave" Value="0" meta:resourcekey="rblIsStartResource2"></asp:ListItem>

                        </asp:RadioButtonList>
                    </div>
                </div>
            <div class="row" id="trHolidayType" visible="False" runat="server">
                <div class="col-md-2">
                    <asp:Label ID="lblHolidayType" runat="server" CssClass="Profiletitletxt" Text="Holiday Type" meta:resourcekey="lblHolidayTypeResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadComboHolidayType" CausesValidation="False" Filter="Contains"
                        AutoPostBack="True" MarkFirstMatch="True" Skin="Vista" runat="server" Style="width: 350px"
                        meta:resourcekey="RadComboHolidayTypeResource1">
                    </telerik:RadComboBox>

                </div>
            </div>


            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblEnglishTitle" runat="server" CssClass="Profiletitletxt" Text="English Title" meta:resourcekey="lblEnglishTitleResource1"></asp:Label>
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
                    <asp:Label ID="lblArabicTitle" runat="server" CssClass="Profiletitletxt" Text="Arabic Title" meta:resourcekey="lblArabicTitleResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtArabicTitle" runat="server" Width="370px" meta:resourcekey="txtArabicTitleResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqArabicTitle" runat="server" ControlToValidate="txtArabicTitle"
                        Display="None" ErrorMessage="Please enter an Arabic Title" ValidationGroup="AnnouncementsGroup"
                        meta:resourcekey="reqArabicTitleResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderReqArabicTitle" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="reqArabicTitle" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
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
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdVwAnnouncements"
                        ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                    <telerik:RadGrid ID="dgrdVwAnnouncements" runat="server" AllowSorting="True"
                        AllowPaging="True" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                        ShowFooter="True" OnItemCommand="dgrdVwAnnouncements_ItemCommand"
                        meta:resourcekey="dgrdVwAnnouncementsResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="TemplateID,LeaveType,LeaveTypeAr,HolidayType,HolidayTypeAr,AtLeaveStart">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                    UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn AllowFiltering="False" DataField="TemplateID" HeaderText="ID"
                                    SortExpression="TemplateID" Visible="False" meta:resourcekey="GridBoundColumnResource1"
                                    UniqueName="TemplateID">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AnnouncementType" HeaderText="Announcement Type" SortExpression="AnnouncementType"
                                    Resizable="False" meta:resourcekey="GridBoundColumnResource2" UniqueName="AnnouncementType">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AnnouncementName" HeaderText="Announcement Name" SortExpression="AnnouncementName"
                                    Resizable="False" meta:resourcekey="GridBoundColumnResource3" UniqueName="AnnouncementName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LeaveType" HeaderText="Leave Type" SortExpression="LeaveType"
                                    Resizable="False" meta:resourcekey="GridBoundColumnResource4" UniqueName="LeaveType">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="HolidayType" HeaderText="Holiday Type" SortExpression="HolidayType"
                                    Resizable="False" meta:resourcekey="GridBoundColumnResource5" UniqueName="HolidayType">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LeaveTypeAR" HeaderText="Leave Type Arabic" SortExpression="LeaveTypeAR"
                                    Resizable="False" meta:resourcekey="GridBoundColumnResource6" UniqueName="LeaveTypeAR">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="HolidayTypeAr" HeaderText="Holiday Type Arabic" SortExpression="HolidayTypeAr"
                                    Resizable="False" meta:resourcekey="GridBoundColumnResource7" UniqueName="HolidayTypeAr">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TitleEn" HeaderText=" English Title"
                                    SortExpression="TitleEn"
                                    meta:resourcekey="GridBoundColumnResource8" UniqueName="TitleEn">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TitleAr" HeaderText="Arabic Title"
                                    SortExpression="TitleAr" Visible="False"
                                    Resizable="False" meta:resourcekey="GridBoundColumnResource9"
                                    UniqueName="TitleAr">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TextEn" HeaderText="English Text" SortExpression="TextEn"
                                    Resizable="False" meta:resourcekey="GridBoundColumnResource10" UniqueName="TextEn">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TextAr" HeaderText="Arabic Text"
                                    SortExpression="TextAr" Visible="False"
                                    Resizable="False" meta:resourcekey="GridBoundColumnResource11"
                                    UniqueName="TextAr">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AtLeaveStart" HeaderText="At Leave Start"
                                    SortExpression="AtLeaveStart" Visible="False"
                                    Resizable="False" meta:resourcekey="GridBoundColumnResource12"
                                    UniqueName="AtLeaveStart">
                                </telerik:GridBoundColumn>


                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1"
                                    meta:resourcekey="RadToolBar1Resource1" OnButtonClick="RadToolBar1_ButtonClick">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server"
                                            meta:resourcekey="RadToolBarButtonResource1" Owner="" />
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

