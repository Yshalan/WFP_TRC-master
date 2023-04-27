<%@ Page Title="Define Employee Logical Group" Language="VB" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" Theme="SvTheme" CodeFile="LogicalGroup.aspx.vb" Inherits="LogicalGroup"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            function viewPolicyDetails(tmp) {
                var lang = '<%= MsgLang %>'
                var PolicyId = $find('<%= RadCmbBxPolicy.ClientID %>')._value;
                if (PolicyId != -1)
                    oWindow = radopen('../Admin/TAPolicyPopup.aspx?ID=' + PolicyId, "RadWindow1");
                else
                    if (lang == "en") {
                        ShowMessage("Please Select a Policy Name");
                    }
                    else {
                        ShowMessage("الرجاء اختيار سياسة حضور");
                    }

                return false;

            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="pnlEmpReligion" runat="server">
        <ContentTemplate>
            <div class="row">
                <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Default"
                    EnableShadow="True" InitialBehavior="None" Style="z-index: 8000;" Modal="True"
                    meta:resourcekey="RadWindowManager1Resource1">
                    <Windows>
                        <telerik:RadWindow ID="RadWindow1" runat="server" Animation="FlyIn" Behavior="Close, Move,resize"
                            Behaviors="Close, Move,resize" EnableShadow="True" Height="450px" Width="650px" IconUrl="~/images/HeaderWhiteChrome.jpg"
                            InitialBehavior="None" ShowContentDuringLoad="False" VisibleStatusbar="False"
                            Skin="Vista" meta:resourcekey="RadWindow1Resource1" Modal="True">
                        </telerik:RadWindow>
                    </Windows>
                </telerik:RadWindowManager>
            </div>

            <uc1:PageHeader ID="Emp_LogicGroup" runat="server" HeaderText="Employee Logical Group" />

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblGroupName" runat="server" CssClass="Profiletitletxt" Text="English name"
                        meta:resourcekey="lblGroupNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtGroupName" runat="server" meta:resourcekey="txtGroupNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqReligionName" runat="server" ErrorMessage="Please enter group english name"
                        Display="None" ValidationGroup="ReligionGroup" ControlToValidate="txtGroupName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        meta:resourcekey="reqReligionNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderReligioName" TargetControlID="reqReligionName"
                        runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblReligionArabicName" runat="server" CssClass="Profiletitletxt" Text="Arabic Name"
                        meta:resourcekey="lblReligionArabicNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtGroupArabicName" runat="server" meta:resourcekey="txtGroupArabicNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqReligionArabicName" runat="server" ErrorMessage="Please enter religion arabic name"
                        Display="None" ValidationGroup="ReligionGroup" ControlToValidate="txtGroupArabicName"
                        CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        meta:resourcekey="reqReligionArabicNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderReligionArName" TargetControlID="reqReligionArabicName"
                        runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblPolicy" runat="server" CssClass="Profiletitletxt" Text="TA policy name"
                        meta:resourcekey="lblPolicyResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbBxPolicy" MarkFirstMatch="True" Skin="Vista" runat="server"
                        meta:resourcekey="RadCmbBxPolicyResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqPolicyname" runat="server" ErrorMessage="Please select policy name"
                        InitialValue="--Please Select--" ValidationGroup="ReligionGroup" Display="None"
                        ControlToValidate="RadCmbBxPolicy" meta:resourcekey="reqPolicynameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderreqPolicyname" TargetControlID="reqPolicyname"
                        runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                    <a href="#" onclick="viewPolicyDetails(1)">
                        <asp:Literal ID="Literal1" runat="server" Text="View Details" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                </div>
            </div>
            <div class="row" id="dvAllowPunchOutSideLocation" runat="server" visible="false">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkAllowPunchOutSideLocation" runat="server" Text="Allow Punch OutSide Work Location"
                        meta:resourcekey="chkAllowPunchOutSideLocationResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="ReligionGroup"
                        CssClass="button" meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" OnClientClick="return ValidateDelete();" runat="server" Text="Delete" CausesValidation="False"
                        CssClass="button" meta:resourcekey="btnDeleteResource1" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CausesValidation="False" CssClass="button"
                        meta:resourcekey="btnClearResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxLoadingPanel1Resource1" />
                    <div class="filterDiv">
                        <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdVwGroup"
                            ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
                    </div>
                    <telerik:RadGrid ID="dgrdVwGroup" runat="server" AllowSorting="True" AllowPaging="True"
                        PageSize="25" GridLines="None" ShowStatusBar="True" ShowFooter="True"
                        OnItemCommand="dgrdVwGroup_ItemCommand" meta:resourcekey="dgrdVwGroupResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="GroupId,GroupName">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                    UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="GroupName" SortExpression="GroupName" HeaderText="Group English Name"
                                    meta:resourcekey="GridBoundColumnResource1" UniqueName="GroupName" />
                                <telerik:GridBoundColumn DataField="GroupArabicName" SortExpression="GroupArabicName"
                                    HeaderText="Group Arabic Name" meta:resourcekey="GridBoundColumnResource2" UniqueName="GroupArabicName" />
                                <telerik:GridCheckBoxColumn DataField="Active" SortExpression="Active" HeaderText="Is Active"
                                    AllowFiltering="False" Visible="False" meta:resourcekey="GridCheckBoxColumnResource1"
                                    UniqueName="Active" ItemStyle-CssClass="nocheckboxstyle" />
                                <telerik:GridBoundColumn DataField="GroupId" SortExpression="GroupId" Visible="False"
                                    AllowFiltering="False" meta:resourcekey="GridBoundColumnResource3" UniqueName="GroupId" />
                            </Columns>
                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                            <CommandItemTemplate>
                                <telerik:RadToolBar Skin="Hay" runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick"
                                    meta:resourcekey="RadToolBar1Resource1">
                                    <Items>
                                        <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="~/images/RadFilter.gif"
                                            ImagePosition="Right" runat="server" meta:resourcekey="RadToolBarButtonResource1" />
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
    <script type="text/javascript">
        function ValidateDelete(sender, eventArgs) {
            var grid = $find("<%=dgrdVwGroup.ClientID %>");
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
