<%@ Control Language="VB" AutoEventWireup="false" CodeFile="OrgEntity.ascx.vb" Inherits="UserControls_OrgEntity" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%--<asp:UpdatePanel runat="server" ID="upBUAccount">
    <ContentTemplate>--%>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function viewPolicyDetails(tmp) {

            var PolicyId = $find('<%= ddlDefaultPolicy.ClientID %>')._value;
            if (PolicyId != -1)
                oWindow = radopen('TAPolicyPopup.aspx?ID=' + PolicyId, "RadWindow1");
            return false;

        }
        function ShowPopUp(Mode) {
            var lang = '<%= MsgLang %>';
            if (Mode == 1) {
                if (lang == 'en') {
                    return confirm('Are you sure you want to Assign Manager to the selected entity?');
                }
                else {
                    return confirm('هل انت متأكد تعيين مدير لوحدة العمل التي اخترتها؟');
                }
            }

        }


    </script>
</telerik:RadCodeBlock>
<telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Default"
    EnableShadow="True" InitialBehavior="None" Style="z-index: 8000;" Modal="true">
    <Windows>
        <telerik:RadWindow ID="RadWindow1" runat="server" Animation="FlyIn" Behavior="Close, Move, resize"
            Behaviors="Close, Move, resize" EnableShadow="True" Height="500px" IconUrl="~/images/HeaderWhiteChrome.jpg"
            InitialBehavior="None" ShowContentDuringLoad="False" VisibleStatusbar="False"
            Width="700px" Skin="Vista">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>

<uc1:PageHeader ID="PageHeader1" runat="server" />


<div class="row">
    <div class="col-md-3">
        <asp:Label CssClass="Profiletitletxt" ID="lblHasParent" runat="server" Text="Has Parent Entity"
            meta:resourcekey="lblHasParentResource1"></asp:Label>
    </div>
    <div class="col-md-7">
        <asp:CheckBox ID="checkBxHasParent" runat="server" AutoPostBack="True" meta:resourcekey="checkBxHasParentResource1" />
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <asp:Label CssClass="Profiletitletxt" ID="lblComanyParent" runat="server" Text="Parent Entity"
            meta:resourcekey="lblComanyParentResource1"></asp:Label>
    </div>
    <div class="col-md-7">
        <%-- <telerik:RadComboBox ID="ddlBxParentEntity" runat="server" Width="210px">
                                            </telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtParent"
                                                Display="None" Enabled="false" ErrorMessage="Please Select Parent Entity" InitialValue="--Please Select--"
                                                ValidationGroup="org1"></asp:RequiredFieldValidator>
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                Enabled="True" TargetControlID="RequiredFieldValidator6">
                                            </ajaxToolkit:ValidatorCalloutExtender>--%>
        <asp:TextBox ID="txtParent" runat="server" Width="200px" Enabled="False" meta:resourcekey="txtParentResource1">
        </asp:TextBox>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <asp:Label CssClass="Profiletitletxt" ID="Label2" runat="server" Text="Company" meta:resourcekey="Label2Resource1">
        </asp:Label>
    </div>
    <div class="col-md-7">
        <asp:TextBox ID="txtCompany" runat="server" Width="200px" Enabled="False" meta:resourcekey="txtCompanyResource1">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCompany"
            Display="None" ErrorMessage="Please Enter Company" ValidationGroup="org1" meta:resourcekey="RequiredFieldValidator7Resource1">
        </asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
            Enabled="True" TargetControlID="RequiredFieldValidator7">
        </ajaxToolkit:ValidatorCalloutExtender>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <asp:Label CssClass="Profiletitletxt" ID="Label3" runat="server" Text="Level" meta:resourcekey="Label3Resource1">
        </asp:Label>
    </div>
    <div class="col-md-7">
        <asp:TextBox ID="txtLevel" runat="server" Width="200px" Enabled="False" meta:resourcekey="txtLevelResource1">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtLevel"
            Display="None" ErrorMessage="Please Enter Level" ValidationGroup="org1" meta:resourcekey="RequiredFieldValidator8Resource1">
        </asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
            Enabled="True" TargetControlID="RequiredFieldValidator8">
        </ajaxToolkit:ValidatorCalloutExtender>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <asp:Label CssClass="Profiletitletxt" ID="Label1" runat="server" Text="Entity Code"
            meta:resourcekey="Label1Resource1"></asp:Label>
    </div>
    <div class="col-md-7">
        <asp:TextBox ID="txtEntityCode" runat="server" Width="200px" meta:resourcekey="txtEntityCodeResource1">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEntityCode"
            Display="None" ErrorMessage="Please Enter Entity Code" ValidationGroup="org1"
            meta:resourcekey="RequiredFieldValidator5Resource1">
        </asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
            Enabled="True" TargetControlID="RequiredFieldValidator5">
        </ajaxToolkit:ValidatorCalloutExtender>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <asp:Label CssClass="Profiletitletxt" ID="lblCompanyName" runat="server" Text="English Name"
            meta:resourcekey="lblCompanyNameResource1"></asp:Label>
    </div>
    <div class="col-md-7">
        <asp:TextBox ID="txtEnName" runat="server" Width="200px" meta:resourcekey="txtEnNameResource1">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEnName"
            Display="None" ErrorMessage="Please Enter English Name" ValidationGroup="org1"
            meta:resourcekey="RequiredFieldValidator2Resource1">
        </asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
            runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
        </ajaxToolkit:ValidatorCalloutExtender>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <asp:Label CssClass="Profiletitletxt" ID="lblCompanyArabicName" runat="server" Text="Arabic Name"
            meta:resourcekey="lblCompanyArabicNameResource1"></asp:Label>
    </div>
    <div class="col-md-7">
        <asp:TextBox ID="txtArName" runat="server" Width="200px" meta:resourcekey="txtArNameResource1">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtArName"
            Display="None" ErrorMessage="Please Enter  Name in arabic" ValidationGroup="org1"
            meta:resourcekey="RequiredFieldValidator4Resource1">
        </asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
            Enabled="True" TargetControlID="RequiredFieldValidator4">
        </ajaxToolkit:ValidatorCalloutExtender>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <asp:Label CssClass="Profiletitletxt" ID="lblDefaultPolicy" runat="server" Text="Default Policy"
            meta:resourcekey="lblDefaultPolicyResource1"></asp:Label>
    </div>
    <div class="col-md-7">
        <telerik:RadComboBox ID="ddlDefaultPolicy" runat="server" MarkFirstMatch="true" Width="210px">
        </telerik:RadComboBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDefaultPolicy"
            Display="None" ErrorMessage="Please Select Default Policy" InitialValue="--Please Select--"
            ValidationGroup="org1" meta:resourcekey="RequiredFieldValidator1Resource1">
        </asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
            Enabled="True" TargetControlID="RequiredFieldValidator1">
        </ajaxToolkit:ValidatorCalloutExtender>
        <a href="#" onclick="viewPolicyDetails(1)">
            <asp:Literal ID="Literal1" runat="server" Text="View Details" meta:resourcekey="Literal1Resource1">
            </asp:Literal></a>
    </div>
</div>
<div class="row">
    <div class="col-md-3">
        <asp:Label CssClass="Profiletitletxt" ID="lblHighestPost" runat="server" Text="Highest Post"
            meta:resourcekey="lblHighestPostResource1"></asp:Label>
    </div>
    <div class="col-md-7">
        <telerik:RadComboBox ID="ddlHighestPost" runat="server" MarkFirstMatch="true" Width="210px">
        </telerik:RadComboBox>
        <asp:RequiredFieldValidator ID="ReqHighestPost" runat="server" ControlToValidate="ddlHighestPost"
            Display="None" ErrorMessage="Please Select Highest post" InitialValue="--Please Select--"
            ValidationGroup="org1" meta:resourcekey="ReqHighestPostResource1">
        </asp:RequiredFieldValidator>
        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
            Enabled="True" TargetControlID="ReqHighestPost">
        </ajaxToolkit:ValidatorCalloutExtender>
    </div>
</div>
<div class="row">

    <div class="col-md-12">
        <asp:Panel ID="pnlManagerInfo" runat="server" GroupingText="Manager Information"
            meta:resourcekey="pnlManagerInfoResource1">
            <asp:TextBox ID="txtEmpNo" runat="server">
            </asp:TextBox>
            <asp:Button ID="btnRetrieve" runat="server" Text="Retrieve" CssClass="button" meta:resourcekey="btnRetrieveResource1" />
            <br />
            <asp:Label CssClass="Profiletitletxt" ID="lblManager" runat="server"></asp:Label>
        </asp:Panel>
    </div>


</div>
<div class="row">
    <div class="col-md-12 text-center">
        <asp:Button ID="ibtnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="org1"
            meta:resourcekey="ibtnSaveResource1" OnClick="ibtnSave_Click" OnClientClick="return ShowPopUp('1')" />
        <asp:Button ID="ibtnDelete" runat="server" Text="Delete" CssClass="button" CausesValidation="False"
            OnClientClick="return confirm('Are you sure you want delete');" meta:resourcekey="ibtnDeleteResource1" />
        <asp:Button ID="ibtnRest" runat="server" Text="Clear" CssClass="button" CausesValidation="False"
            meta:resourcekey="ibtnRestResource1" />
    </div>

</div>
<div class="row">
    <div class="table-responsive">
        <telerik:RadGrid ID="dgrdOrg_Entity" runat="server" AllowSorting="True" AllowPaging="True"
            Skin="Hay" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="true"
            ShowFooter="True" GroupingSettings-CaseSensitive="false">
            <SelectedItemStyle ForeColor="maroon" />
            <MasterTableView AllowFilteringByColumn="True" AllowMultiColumnSorting="True" IsFilterItemExpanded="true"
                AutoGenerateColumns="False" DataKeyNames="EntityId">
                <Columns>
                    <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" />
                    <telerik:GridBoundColumn DataField="EntityId" HeaderText="EntityId" SortExpression="EntityId"
                        Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EntityName" HeaderText="Entity Name" AllowFiltering="true"
                        ShowFilterIcon="true" SortExpression="EntityName" Resizable="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="EntityArabicName" HeaderText="Arabic Name" SortExpression="EntityArabicName" />
                    <telerik:GridBoundColumn DataField="parentname" HeaderText="Parent Name" AllowFiltering="false"
                        ShowFilterIcon="true" SortExpression="parentname" Resizable="false">
                    </telerik:GridBoundColumn>
                </Columns>
                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
            </MasterTableView><ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </div>

</div>



<%--</ContentTemplate>
</asp:UpdatePanel>--%>