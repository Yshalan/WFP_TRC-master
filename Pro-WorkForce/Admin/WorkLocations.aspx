<%@ Page Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="WorkLocations.aspx.vb" Inherits="Emp_EmpWorkLocations" Title="Define Work Locations"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">
            function viewPolicyDetails(tmp) {

                var PolicyId = $find('<%= RadCmbBxPolicy.ClientID %>')._value;
                if (PolicyId != -1)
                    oWindow = radopen('TAPolicyPopup.aspx?ID=' + PolicyId, "RadWindow1");
                return false;

            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Default"
        EnableShadow="True" InitialBehavior="None" Style="z-index: 8000;" Modal="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Animation="FlyIn" Behavior="Close, Move"
                Behaviors="Close, Move" EnableShadow="True" Height="600px" IconUrl="~/images/HeaderWhiteChrome.jpg"
                InitialBehavior="None" ShowContentDuringLoad="False" VisibleStatusbar="False"
                Width="700px" Skin="Vista">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <asp:UpdatePanel ID="UP_WorkLocation" runat="server">
        <ContentTemplate>

            <uc1:PageHeader ID="userCtrlWorkLocation" HeaderText="Work Location" runat="server" />

            <div class="row" id="trCompany" runat="server">
                <div class="col-md-2">
                    <asp:Label ID="lblCompanyId" runat="server" CssClass="Profiletitletxt" Text="Company"
                        meta:resourcekey="lblCompanyIdResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbBxCompanyId" MarkFirstMatch="True" AutoPostBack="True"
                        Skin="Vista" runat="server" meta:resourcekey="RadCmbBxCompanyIdResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqCompanyId" runat="server" ErrorMessage="Please select Company"
                        InitialValue="--Please Select--" ValidationGroup="WorkLocationGroup" Display="None"
                        ControlToValidate="RadCmbBxCompanyId" meta:resourcekey="reqCompanyIdResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="reqCompanyId"
                        runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblCode" runat="server" CssClass="Profiletitletxt" Text="Work Location Code"
                        meta:resourcekey="lblCodeResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtWorkLocationCode" runat="server" meta:resourcekey="txtWorkLocationCodeResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqWorlLocationCode" runat="server" ControlToValidate="txtWorkLocationCode"
                        Display="None" ErrorMessage="Please enter work location code" ValidationGroup="WorkLocationGroup"
                        meta:resourcekey="reqWorlLocationCodeResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderreqWorlLocationCode" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="reqWorlLocationCode" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblName" runat="server" CssClass="Profiletitletxt" Text="English name"
                        meta:resourcekey="lblNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtWorkLocationName" runat="server" meta:resourcekey="txtWorkLocationNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqWorklocationName" runat="server" ErrorMessage="Please enter work location english name"
                        ValidationGroup="WorkLocationGroup" Display="None" ControlToValidate="txtWorkLocationName"
                        meta:resourcekey="reqWorklocationNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderWorkLocationName" TargetControlID="reqWorklocationName"
                        runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblArabicName" runat="server" CssClass="Profiletitletxt" Text="Arabic name"
                        meta:resourcekey="lblArabicNameResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtWorkLocationArabicName" runat="server" meta:resourcekey="txtWorkLocationArabicNameResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqWorklocArName" runat="server" ErrorMessage="Please enter work location arabic name"
                        ValidationGroup="WorkLocationGroup" Display="None" ControlToValidate="txtWorkLocationArabicName"
                        meta:resourcekey="reqWorklocArNameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderWorkLocationArabicName" TargetControlID="reqWorklocArName"
                        runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblPolicy" runat="server" CssClass="Profiletitletxt" Text="Work Location TA policy"
                        meta:resourcekey="lblPolicyResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmbBxPolicy" MarkFirstMatch="True" Skin="Vista" runat="server"
                        meta:resourcekey="RadCmbBxPolicyResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqPolicyname" runat="server" ErrorMessage="Please select policy name"
                        InitialValue="--Please Select--" ValidationGroup="WorkLocationGroup" Display="None"
                        ControlToValidate="RadCmbBxPolicy" meta:resourcekey="reqPolicynameResource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ExtenderreqPolicyname" TargetControlID="reqPolicyname"
                        runat="server" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                        Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                    <a href="#" onclick="viewPolicyDetails(1)">
                        <asp:Literal ID="Literal1" runat="server" Text="View Details" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center ">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="WorkLocationGroup"
                        meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" CausesValidation="False"
                        meta:resourcekey="btnDeleteResource1" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" CausesValidation="False"
                        meta:resourcekey="btnClearResource1" />
                </div>
            </div>
            <div class="row">
                <div class="table-responsive">
                    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel2" meta:resourcekey="RadAjaxLoadingPanel2Resource1" />
                    <div class="filterDiv">
                        <telerik:RadFilter Skin="Hay" runat="server" ID="RadFilter1" FilterContainerID="dgrdVwWorkLocation"
                            ShowApplyButton="False" CssClass="RadFilter RadFilter_Default " meta:resourcekey="RadFilter1Resource1" />
                    </div>
                    <telerik:RadGrid ID="dgrdVwWorkLocation" runat="server" AllowSorting="True" AllowPaging="True"
                        Width="690px" PageSize="25" Skin="Hay" GridLines="None" ShowStatusBar="True"
                        ShowFooter="True" OnItemCommand="dgrdVwWorkLocation_ItemCommand" meta:resourcekey="dgrdVwWorkLocationResource1">
                        <SelectedItemStyle ForeColor="Maroon" />
                        <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="WorkLocationCode,WorkLocationId">
                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                    UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource1" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="DesignationId" SortExpression="DesignationId"
                                    AllowFiltering="False" Visible="False" HeaderText="DesignationId" meta:resourcekey="GridBoundColumnResource1"
                                    UniqueName="DesignationId" />
                                <telerik:GridBoundColumn DataField="CompanyId" SortExpression="CompanyId" AllowFiltering="False"
                                    Visible="False" HeaderText="CompanyId" meta:resourcekey="GridBoundColumnResource2"
                                    UniqueName="CompanyId" />
                                <telerik:GridBoundColumn DataField="CompanyName" SortExpression="CompanyName" HeaderText="CompanyName"
                                    meta:resourcekey="GridBoundColumnResource3" UniqueName="CompanyName" />
                                <telerik:GridBoundColumn DataField="WorkLocationCode" SortExpression="WorkLocationCode"
                                    HeaderText="Work Location Code" meta:resourcekey="GridBoundColumnResource4" UniqueName="WorkLocationCode" />
                                <telerik:GridBoundColumn DataField="WorkLocationName" SortExpression="WorkLocationName"
                                    HeaderText="English Name" meta:resourcekey="GridBoundColumnResource5" UniqueName="WorkLocationName" />
                                <telerik:GridBoundColumn DataField="WorkLocationArabicName" SortExpression="WorkLocationArabicName"
                                    AllowFiltering="False" HeaderText="Arabic Name" meta:resourcekey="GridBoundColumnResource6"
                                    UniqueName="WorkLocationArabicName" />
                                <telerik:GridBoundColumn DataField="TAPolicyName" SortExpression="TAPolicyName" AllowFiltering="False"
                                    HeaderText="TA Policy" meta:resourcekey="GridBoundColumnResource7" UniqueName="TAPolicyName" />
                                <telerik:GridCheckBoxColumn DataField="Active" SortExpression="Active" HeaderText="Is Active"
                                    AllowFiltering="False" Visible="False" meta:resourcekey="GridCheckBoxColumnResource1"
                                    UniqueName="Active" ItemStyle-CssClass="nocheckboxstyle" />
                                <telerik:GridBoundColumn DataField="WorkLocationId" SortExpression="WorkLocationId"
                                    HeaderText="WorkLocationId" Visible="False" AllowFiltering="False" meta:resourcekey="GridBoundColumnResource8"
                                    UniqueName="WorkLocationId" />
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
</asp:Content>
