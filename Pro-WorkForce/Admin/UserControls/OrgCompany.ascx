<%@ Control Language="VB" AutoEventWireup="false" CodeFile="OrgCompany.ascx.vb" Inherits="UserColntrols_OrgCompany" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%-- <asp:UpdatePanel runat="server" ID="upBUAccount">
    <ContentTemplate>--%>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="Thumbnail" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function viewPolicyDetails(tmp) {

            var PolicyId = $find('<%= ddlDefaultPolicy.ClientID %>')._value;
            if (PolicyId != -1)
                oWindow = radopen('TAPolicyPopup.aspx?ID=' + PolicyId, "RadWindow1");
            return false;

        }

        function ChangeDefaultPolicy() {
            var CompanyDDl = $find('<%= ddlDefaultPolicy.ClientID %>');
            var WorkDDL = $find('<%= ddlPolicy.ClientID %>');
            var itm = WorkDDL.findItemByValue(CompanyDDl._value);
            itm.select();

        }
        function ValidatePage() {
            var tabContainer = $get('<%=TabContainer1.ClientID%>');



            var valCntl = $get('<%=RequiredFieldValidator2.ClientID%>');

            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =RequiredFieldValidator2_ValidatorCalloutExtender.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }
            var valCntl = $get('<%=RequiredFieldValidator4.ClientID%>');

            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ValidatorCalloutExtender3.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }
            var valCntl = $get('<%=RequiredFieldValidator1.ClientID%>');

            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ValidatorCalloutExtender1.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }

            }
            var valCntl = $get('<%=ReqHighestPost.ClientID%>');

            if (valCntl != undefined && valCntl != null) {
                ValidatorValidate(valCntl);

                if (!valCntl.isvalid) {

                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(0);
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout = $find('<% =ValidatorCalloutExtender2.ClientID %>');
                        AjaxControlToolkit.ValidatorCalloutBehavior._currentCallout.show(true);
                    }
                    return false;
                }
                var grid = $find("<%=dgrdOrgLevel.ClientID %>");
                var MasterTable = grid.get_masterTableView();
                var Rows = MasterTable.get_dataItems();

                if (Rows.length <= 0) {

                    ShowMessage("Please add atleast one level");
                    if (tabContainer != undefined && tabContainer != null) {
                        tabContainer = tabContainer.control;
                        tabContainer.set_activeTabIndex(1);

                    }
                    return false;
                }
            }
        }
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
        function fileUploaded(sender, args) {
            $find("<%= RadAjaxManager1.GetCurrent(Page).ClientID %>").ajaxRequest();
        }
        function fileUploadRemoved(sender, args) {
            $find("<%= Thumbnail.ClientID %>").style.visible = false;
            sender.deleteFileInputAt(0);
        }

        function ConfirmDelete() {
            return confirm('Are you sure you want to remove logo?');
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
<cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab"
    meta:resourcekey="TabContainer1Resource1">
    <cc1:TabPanel ID="Tab1" runat="server" HeaderText="Company" meta:resourcekey="Tab1Resource1">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label CssClass="Profiletitletxt" ID="lblHasParent" runat="server" Text="Has Parent Class"
                        meta:resourcekey="lblHasParentResource1"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:CheckBox ID="checkBxHasParent" runat="server" AutoPostBack="True" meta:resourcekey="checkBxHasParentResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label CssClass="Profiletitletxt" ID="lblComanyParent" runat="server" Text="Parent Company"
                        Visible="False" meta:resourcekey="lblComanyParentResource1"></asp:Label>
                </div>
                <div class="col-md-6">
                    <telerik:RadComboBox ID="ddlBxParentCompany" runat="server" MarkFirstMatch="True"
                        Width="210px" meta:resourcekey="ddlBxParentCompanyResource1">
                    </telerik:RadComboBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlBxParentCompany"
                        Display="None" Enabled="False" ErrorMessage="Please Select Parent Company" InitialValue="--Please Select--"
                        ValidationGroup="org1" meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>

                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator6">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label CssClass="Profiletitletxt" ID="lblCompanyName" runat="server" Text="English Name"
                        Width="150px" meta:resourcekey="lblCompanyNameResource1"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtCompanyEnName" runat="server" Width="200px" meta:resourcekey="txtCompanyEnNameResource1"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCompanyEnName"
                        Display="None" ErrorMessage="Please Enter English Name" ValidationGroup="org1"
                        meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>

                    <cc1:ValidatorCalloutExtender ID="RequiredFieldValidator2_ValidatorCalloutExtender"
                        runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label CssClass="Profiletitletxt" ID="lblCompanyArabicName" runat="server" Text="Arabic Name"
                        meta:resourcekey="lblCompanyArabicNameResource1"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtCompanyArName" runat="server" Width="200px" meta:resourcekey="txtCompanyArNameResource1"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCompanyArName"
                        Display="None" ErrorMessage="Please Enter Company Name in arabic" ValidationGroup="org1"
                        meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>

                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator4">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label CssClass="Profiletitletxt" ID="lblCompanyShortName" runat="server" Text="Short Name"
                        meta:resourcekey="lblCompanyShortNameResource1"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtCompanyShortName" runat="server" Width="200px" meta:resourcekey="txtCompanyShortNameResource1"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label CssClass="Profiletitletxt" ID="lblAddress" runat="server" Text="Address"
                        meta:resourcekey="lblAddressResource1"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="203px" meta:resourcekey="txtAddressResource1"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label CssClass="Profiletitletxt" ID="lblPhno" runat="server" Text="Phone No."
                        meta:resourcekey="lblPhnoResource1"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtPhoneNo" runat="server" Width="200px" meta:resourcekey="txtPhoneNoResource1"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label CssClass="Profiletitletxt" ID="lblFax" runat="server" Text="Fax" meta:resourcekey="lblFaxResource1"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtFax" runat="server" Width="200px" meta:resourcekey="txtFaxResource1"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label CssClass="Profiletitletxt" ID="lblUrl" runat="server" Text="U.R.L" meta:resourcekey="lblUrlResource1"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtURL" runat="server" Width="200px" meta:resourcekey="txtURLResource1"></asp:TextBox>
                </div>
            </div>
            <div class="svPanel">
                <div class="row">
                    <div class="col-md-3">
                        <asp:Label CssClass="Profiletitletxt" ID="lblLogo" runat="server" Text="Logo" meta:resourcekey="lblLogoResource1"></asp:Label>
                    </div>
                    <div class="col-md-6">
                        <div class="upload-panel">
                            <asp:LinkButton ID="lbtnRemoveLogo" runat="server" Text="Remove Logo" OnClientClick="return ConfirmDelete()"
                                meta:resourcekey="lbtnRemoveLogoResource1"></asp:LinkButton>

                            <telerik:RadBinaryImage ID="Thumbnail" runat="server" AlternateText="Thumbnail" Height="120px"
                                ResizeMode="Fit" Visible="False" Width="180px" meta:resourcekey="ThumbnailResource1" />

                            <asp:ImageButton ID="ImageButton1" runat="server" Height="15px" ImageUrl="~/images/closefile.PNG"
                                Style="float: right;" Visible="False" Width="60px" meta:resourcekey="ImageButton1Resource1" />
                            <br />
                            <telerik:RadUpload ID="rdUpldProDocuments" runat="server" Width="246px" AllowedFileExtensions="jpg,jpeg,png,gif,bmp"
                                ControlObjectsVisibility="None" Height="37px" MaxFileInputsCount="1" meta:resourcekey="rdUpldProDocumentsResource1">
                            </telerik:RadUpload>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-3">
                    <asp:Label CssClass="Profiletitletxt" ID="lblDefaultPolicy" runat="server" Text="Default Policy"
                        meta:resourcekey="lblDefaultPolicyResource1"></asp:Label>

                </div>
                <div class="col-md-6">
                    <telerik:RadComboBox ID="ddlDefaultPolicy" runat="server" MarkFirstMatch="True" Width="210px"
                        OnClientSelectedIndexChanged="ChangeDefaultPolicy" meta:resourcekey="ddlDefaultPolicyResource1">
                    </telerik:RadComboBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDefaultPolicy"
                        Display="None" ErrorMessage="Please Select Default Policy" InitialValue="--Please Select--"
                        ValidationGroup="org1" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>

                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True"
                        TargetControlID="RequiredFieldValidator1">
                    </cc1:ValidatorCalloutExtender>

                    <a href="#" onclick="viewPolicyDetails(1)">
                        <asp:Literal ID="Literal1" runat="server" Text="View Details" meta:resourcekey="Literal1Resource1"></asp:Literal>

                    </a>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Panel ID="pnlManagerInfo" runat="server" GroupingText="General Manager Information"
                        meta:resourcekey="pnlManagerInfoResource1">
                        <asp:TextBox ID="txtEmpNo" runat="server"></asp:TextBox>

                        <asp:Button ID="btnRetrieve" runat="server" Text="Retrieve" CssClass="button" meta:resourcekey="btnRetrieveResource1" />

                        <br />
                        <asp:Label CssClass="Profiletitletxt" ID="lblManager" ForeColor="#0066FF" runat="server"></asp:Label>

                    </asp:Panel>
                </div>

            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label CssClass="Profiletitletxt" ID="lblHighestPost" runat="server" Text="Highest Post"
                        meta:resourcekey="lblHighestPostResource1" Visible="False"></asp:Label>

                </div>
                <div class="col-md-6">
                    <telerik:RadComboBox ID="ddlHighestPost" runat="server" MarkFirstMatch="True" Width="210px"
                        meta:resourcekey="ddlHighestPostResource1" Visible="False">
                    </telerik:RadComboBox>

                    <asp:RequiredFieldValidator ID="ReqHighestPost" runat="server" ControlToValidate="ddlHighestPost"
                        Display="None" ErrorMessage="Please Select Highest post" InitialValue="--Please Select--"
                        ValidationGroup="org1" meta:resourcekey="ReqHighestPostResource1" Enabled="False"></asp:RequiredFieldValidator>

                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True"
                        TargetControlID="ReqHighestPost">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
        </ContentTemplate>
    </cc1:TabPanel>
    <cc1:TabPanel ID="Tab2" runat="server" HeaderText="Company Levels" meta:resourcekey="Tab2Resource1">
        <ContentTemplate>
            <asp:UpdatePanel ID="upTab2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label CssClass="Profiletitletxt" ID="Label2" runat="server" Width="140px" Text="Level Name"
                                meta:resourcekey="Label2Resource1"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="TextBoxLevelName" runat="server" Width="200px" meta:resourcekey="TextBoxLevelNameResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
                                ErrorMessage="Please Enter Level Name" ControlToValidate="TextBoxLevelName" ValidationGroup="groupAdd"
                                meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator5_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label CssClass="Profiletitletxt" ID="Label3" runat="server" Text="Level Arabic Name"
                                meta:resourcekey="Label3Resource1"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="TextBoxLevelNameArabic" runat="server" Width="200px" meta:resourcekey="TextBoxLevelNameArabicResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="None"
                                ErrorMessage="Please Enter Level Name" ControlToValidate="TextBoxLevelNameArabic"
                                ValidationGroup="groupAdd" meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator7_ValidatorCalloutExtender"
                                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="button" ValidationGroup="groupAdd"
                                meta:resourcekey="btnAddResource1" />
                            <asp:Button ID="btnClearLevels" runat="server" CssClass="button" Text="Clear" meta:resourcekey="btnClearLevelsResource1" />
                        </div>

                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <telerik:RadGrid ID="dgrdOrgLevel" runat="server" AllowPaging="True" GridLines="None"
                                ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True" meta:resourcekey="dgrdOrgLevelResource1">
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="Enabledelete,LevelID,LevelName,LevelArabicName">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="LevelName" SortExpression="LevelName" HeaderText="Level Name"
                                            UniqueName="LevelName" meta:resourcekey="GridBoundColumnResource1" />
                                        <telerik:GridBoundColumn DataField="LevelArabicName" SortExpression="LevelArabicName"
                                            HeaderText="Arabic Name" UniqueName="LevelArabicName" meta:resourcekey="GridBoundColumnResource2" />
                                        <telerik:GridBoundColumn DataField="Enabledelete" SortExpression="Enabledelete" Visible="False"
                                            UniqueName="Enabledelete" meta:resourcekey="GridBoundColumnResource3" />
                                        <telerik:GridBoundColumn DataField="LevelId" SortExpression="LevelId" HeaderText="LevelId"
                                            Visible="False" UniqueName="LevelId" meta:resourcekey="GridBoundColumnResource4" />
                                        <telerik:GridButtonColumn CommandName="Delete" ConfirmText="Are you sure ?" Text="Delete"
                                            UniqueName="column" meta:resourcekey="GridButtonColumnResource1">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                </MasterTableView>
                                <SelectedItemStyle ForeColor="Maroon" />
                            </telerik:RadGrid>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
    </cc1:TabPanel>
    <cc1:TabPanel ID="Tab3" runat="server" HeaderText="Work Locations" meta:resourcekey="Tab3Resource1">
        <ContentTemplate>
            <asp:UpdatePanel ID="upTab3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label CssClass="Profiletitletxt" ID="lblCode" runat="server" Width="140px" Text="Code"
                                meta:resourcekey="lblCodeResource1"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtCode" runat="server" Width="200px" meta:resourcekey="txtCodeResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCode" runat="server" Display="None" ErrorMessage="Please Enter Work Location Code"
                                ControlToValidate="txtCode" ValidationGroup="groupAddWork" meta:resourcekey="rfvCodeResource1" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceCode" runat="server" Enabled="True"
                                TargetControlID="rfvCode">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label CssClass="Profiletitletxt" ID="lblWorkLocationName" runat="server" Width="140px"
                                Text="English Name" meta:resourcekey="lblWorkLocationNameResource1"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtWorkLocationName" runat="server" Width="200px" meta:resourcekey="txtWorkLocationNameResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvWorkLocationName" runat="server" Display="None"
                                ErrorMessage="Please Enter Work Location Name" ControlToValidate="txtWorkLocationName"
                                ValidationGroup="groupAddWork" meta:resourcekey="rfvWorkLocationNameResource1" />
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceWorkLocationName" runat="server" Enabled="True"
                                TargetControlID="rfvWorkLocationName">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label CssClass="Profiletitletxt" ID="lblWorkLocationArabic" runat="server" Text="Arabic Name"
                                meta:resourcekey="lblWorkLocationArabicResource1"></asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtWorkLocationArabic" runat="server" Width="200px" meta:resourcekey="txtWorkLocationArabicResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvWorkLocationArabic" runat="server" Display="None"
                                ErrorMessage="Please Enter Level Name" ControlToValidate="txtWorkLocationArabic"
                                ValidationGroup="groupAddWork" meta:resourcekey="rfvWorkLocationArabicResource1"></asp:RequiredFieldValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="vceWorkLocationArabic" runat="server" Enabled="True"
                                TargetControlID="rfvWorkLocationArabic">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label CssClass="Profiletitletxt" ID="lblPolicy" runat="server" Text="TA policy name"
                                meta:resourcekey="lblPolicyResource1" />
                        </div>
                        <div class="col-md-6">
                            <telerik:RadComboBox ID="ddlPolicy" runat="server" MarkFirstMatch="true" Width="210px"
                                meta:resourcekey="ddlPolicyResource1" />
                            <asp:RequiredFieldValidator ID="rfvPolicy" runat="server" ControlToValidate="ddlPolicy"
                                Display="None" ErrorMessage="Please Select a Policy" InitialValue="--Please Select--"
                                ValidationGroup="groupAddWork" meta:resourcekey="rfvPolicyResource1" />
                            <cc1:ValidatorCalloutExtender ID="vcePolicy" runat="server" TargetControlID="rfvPolicy"
                                Enabled="True" />
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-6">
                            <asp:CheckBox ID="chkHasMobilePunch" runat="server" Text="Has Mobile Punch" AutoPostBack="true" meta:resourcekey="chkHasMobilePunchResource1" /></td>
                        </div>

                    </div>
                    <div id="dvMobileControls" runat="server" visible="false">
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label CssClass="Profiletitletxt" ID="lblGPSCoordinates" runat="server" Text="GPS Coordinates"
                                    meta:resourcekey="lblGPSCoordinatesResource1" />
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtGPSCoordinates" runat="server" Width="200px" meta:resourcekey="txtGPSCoordinatessResource1"></asp:TextBox>
                            </div>

                        </div>
                        
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label CssClass="Profiletitletxt" ID="lblPunchType" runat="server" Text="Punch Type"
                                    meta:resourcekey="lblPunchType1" />
                            </div>
                            <div class="col-md-9">
                                <%--<asp:CheckBox ID="chkMustPunchPhysical" runat="server" Text="Must Punch Physical" AutoPostBack="true"
                                    ToolTip="Must Punch on The Reader If Employee Punched on Mobile"
                                    meta:resourcekey="chkMustPunchPhysicalResource1" />--%>
                                <asp:RadioButtonList ID="rblPunchType" runat="server"  AutoPostBack="true" CssClass="Profiletitletxt"
                                    RepeatDirection="Horizontal" meta:resourcekey="rblUserTypeeResource1">
                                    <asp:ListItem Value="1" Text="Must Punch Physical" Selected="True" meta:resourcekey="rblUserTypeItem1Resource1">
                                    </asp:ListItem>
                                    <asp:ListItem Value="2" Text="Must Punch Twice" meta:resourcekey="rblUserTypeItem2Resource1">
                                    </asp:ListItem>
                                    <asp:ListItem Value="0" Text="None" meta:resourcekey="rblUserTypeItem3Resource1">
                                    </asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label CssClass="Profiletitletxt" ID="lblRadius" runat="server" Text="Radius"
                                    meta:resourcekey="lblRadiusResource1" />
                            </div>
                            <div class="col-md-6">
                                <telerik:RadNumericTextBox ID="txtRadius" MaxValue="99999" Skin="Vista"
                                    runat="server" Culture="en-US" LabelCssClass="">
                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                </telerik:RadNumericTextBox>
                                <%-- <asp:RequiredFieldValidator ID="rfvRadius" runat="server" Display="None" ErrorMessage="Please Insert Radius, At Least (0)"
                                                                ControlToValidate="txtRadius" ValidationGroup="groupAddWork" meta:resourcekey="rfvRadiusResource1" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="vceRadius" runat="server" Enabled="True"
                                                                TargetControlID="rfvRadius">
                                                            </ajaxToolkit:ValidatorCalloutExtender>--%>
                            </div>
                        </div>
                        <div id="dvMustPunchTwiceControls" runat="server" visible="false">
                            <%--<div class="row">
                                <div class="col-md-3">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblFistPunch" runat="server" Text="First Punch Radius"
                                        meta:resourcekey="lblFistPunchResource1" />
                                </div>

                                <div class="col-md-6">
                                    <telerik:RadNumericTextBox ID="txtFistPunch" MaxValue="99999" Skin="Vista"
                                        runat="server" Culture="en-US" LabelCssClass="">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                   
                                </div>

                            </div>--%>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblSecondPunch" runat="server" Text="Second Punch Radius"
                                        meta:resourcekey="lblSecondPunchResource1" />
                                </div>
                                <div class="col-md-6">
                                    <telerik:RadNumericTextBox ID="txtSecondPunch" MaxValue="99999" Skin="Vista"
                                        runat="server" Culture="en-US" LabelCssClass="">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                    <%--<asp:TextBox ID="txtSecondPunch" runat="server" Width="200px" meta:resourcekey="txtSecondPunchResource1"></asp:TextBox>--%>
                                </div>

                            </div>
                            <%--<div class="row">
                                <div class="col-md-3">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblTime" runat="server" Text="Time Between 1st and 2nd Punch"
                                        meta:resourcekey="lblTimeResource1" />
                                </div>
                                <div class="col-md-6">
                                    <telerik:RadNumericTextBox ID="txtTime" MaxValue="99999" Skin="Vista"
                                        runat="server" Culture="en-US" LabelCssClass="">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                    
                                </div>

                            </div>--%>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblOut" runat="server" Text="Out Radius"
                                        meta:resourcekey="lblOutResource1" />
                                </div>
                                <div class="col-md-6">
                                    <telerik:RadNumericTextBox ID="txtOutRadius" MaxValue="99999" Skin="Vista"
                                        runat="server" Culture="en-US" LabelCssClass="">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                    <%--<asp:TextBox ID="txtOutRadius" runat="server" Width="200px" meta:resourcekey="txtOutRadiusResource1"></asp:TextBox>--%>
                                </div>

                            </div>
                        </div>
                        <div id="dvMobilePunchConsiderDuration" runat="server" visible="false">
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="lblMobilePunchConsiderDuration" runat="server" Text="Punch Consider Duration"
                                        ToolTip="Employee Must Punch on the Reader -If Punched from Mobile- in Order to Consider Mobile Punch Within Number of Minute(s)"
                                        meta:resourcekey="lblMobilePunchConsiderDurationResource1"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <telerik:RadNumericTextBox ID="txtMobilePunchConsiderDuration" MinValue="0" MaxValue="9999999"
                                        Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="txtMobilePunchConsiderDurationResource1">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:Label ID="lblMins" runat="server" Text="Minute(s)" meta:resourcekey="lblMinsResource1"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <%-- <div class="row">
                            <div class="col-md-6">
                            <asp:CheckBox ID="chkMustPunchTwice" runat="server" Text="Must Punch Twice" AutoPostBack="true" meta:resourcekey="chkMustPunchTwiceResource1" /></td>
                        </div>
                        </div>--%>

                       
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblBeaconNo" runat="server" Text="Beacon Serial No." meta:resourcekey="lblBeaconNoResource1"></asp:Label></td>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtBeaconNo" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvBeaconNo" runat="server" ControlToValidate="txtBeaconNo"
                                    Display="None" ErrorMessage="Please Insert Beacon Serial No."
                                    ValidationGroup="groupAddBeacon" />
                                <cc1:ValidatorCalloutExtender ID="vceBeaconNo" runat="server" TargetControlID="rfvBeaconNo"
                                    Enabled="True" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblBeaconDesc" runat="server" Text="Beacon Description" meta:resourcekey="lblBeaconDescResource1"></asp:Label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtBeaconDesc" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblBeaconExpiryDate" runat="server" Text="Beacon Expiry Date" meta:resourcekey="lblBeaconExpiryDateResource1"></asp:Label></td>
                            </div>
                            <div class="col-md-6">
                                <telerik:RadDatePicker ID="dtpBeaconExpirydate" runat="server" Culture="en-US">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                        Width="">
                                    </DateInput><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="rfvBeaconExpirydate" runat="server" ControlToValidate="dtpBeaconExpirydate"
                                    Display="None" ErrorMessage="Please Select Date" ValidationGroup="groupAddBeacon" meta:resourcekey="rfvBeaconExpirydateResource1"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="vceBeaconExpirydate"
                                    runat="server" Enabled="True" TargetControlID="rfvBeaconExpirydate">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="lblBeaconType" runat="server" Text="Beacon Transaction Type" meta:resourcekey="lblBeaconTypeResource1"></asp:Label></td>
                            </div>
                            <div class="col-md-6">
                                <telerik:RadComboBox ID="radcmxBeaconType" runat="server" MarkFirstMatch="true">
                                    <Items>
                                        <telerik:RadComboBoxItem Value="-1" Text="--Please Select--" Selected="true" meta:resourcekey="RadComboBoxItem1Resource1" />
                                        <telerik:RadComboBoxItem Value="0" Text="In" meta:resourcekey="RadComboBoxItem2Resource1" />
                                        <telerik:RadComboBoxItem Value="1" Text="Out" meta:resourcekey="RadComboBoxItem3Resource1" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="rfvBeaconType" runat="server" ControlToValidate="radcmxBeaconType"
                                    Display="None" ErrorMessage="Please Select Beacon Transaction Type" InitialValue="--Please Select--"
                                    ValidationGroup="groupAddBeacon" meta:resourcekey="rfvBeaconTypeResource1" />
                                <cc1:ValidatorCalloutExtender ID="vceBeaconType" runat="server" TargetControlID="rfvBeaconType"
                                    Enabled="True" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnAddBeacon" runat="server" Text="Add" CssClass="button" ValidationGroup="groupAddBeacon" meta:resourcekey="btnAddBeaconResource1" />
                                <asp:Button ID="btnRemoveBeacon" runat="server" Text="Remove" CssClass="button" OnClientClick="confirmdelete()" meta:resourcekey="btnRemoveBeaconResource1" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="table-responsive">
                                <telerik:RadGrid ID="dgrdBeacon" runat="server" AllowSorting="True" AllowPaging="True"
                                    PageSize="5" GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                    ShowFooter="True">
                                    <SelectedItemStyle ForeColor="Maroon" />
                                    <MasterTableView IsFilterItemExpanded="False" CommandItemDisplay="Top" AutoGenerateColumns="False" DataKeyNames="BeaconId">
                                        <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="BeaconNo" SortExpression="BeaconNo" HeaderText="Beacon No."
                                                UniqueName="BeaconNo" meta:resourcekey="GridBoundColumnBeaconNoResource1" />
                                            <telerik:GridBoundColumn DataField="BeaconTransType" SortExpression="BeaconTransType"
                                                HeaderText="Beacon Transaction Type" UniqueName="BeaconTransType" meta:resourcekey="GridBoundColumnBeaconTransTypeResource1" />

                                        </Columns>
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <CommandItemTemplate>
                                        </CommandItemTemplate>
                                    </MasterTableView>
                                    <GroupingSettings CaseSensitive="False" />
                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnAddWork" runat="server" Text="Save" CssClass="button" ValidationGroup="groupAddWork"
                                meta:resourcekey="btnAddWorkResource1" />
                            &nbsp;
                                                    <asp:Button ID="btnRemoveWork" runat="server" CssClass="button" Text="Remove" meta:resourcekey="btnRemoveWorkResource1" />
                            &nbsp;
                                                    <asp:Button ID="btnClearWork" runat="server" CssClass="button" Text="Clear" meta:resourcekey="btnClearWorkResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="table-responsive">
                            <telerik:RadGrid ID="dgrdWorkLocation" runat="server" AllowPaging="True"
                                GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True"
                                meta:resourcekey="dgrdWorkLocationResource1">
                                <GroupingSettings CaseSensitive="False" />
                                <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" DataKeyNames="WorkLocationId,WorkLocationCode,WorkLocationName,WorkLocationArabicName,FK_TAPolicyId,GPSCoordinates">
                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="False" meta:resourcekey="GridTemplateColumnResource1"
                                            UniqueName="TemplateColumn">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" meta:resourcekey="chkResource1" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="WorkLocationCode" SortExpression="WorkLocationCode"
                                            HeaderText="Work Location Code" UniqueName="WorkLocationCode" meta:resourcekey="GridBoundColumnResource5" />
                                        <telerik:GridBoundColumn DataField="WorkLocationName" SortExpression="WorkLocationName"
                                            HeaderText="English Name" UniqueName="WorkLocationName" meta:resourcekey="GridBoundColumnResource6" />
                                        <telerik:GridBoundColumn DataField="WorkLocationArabicName" SortExpression="WorkLocationArabicName"
                                            HeaderText="Arabic Name" UniqueName="WorkLocationArabicName" meta:resourcekey="GridBoundColumnResource7" />
                                        <%--<telerik:GridCheckBoxColumn DataField="Active" SortExpression="Active" HeaderText="Is Active"
                                                                        AllowFiltering="False" meta:resourcekey="GridCheckBoxColumnResource1" UniqueName="Active" />--%>
                                        <telerik:GridBoundColumn DataField="WorkLocationId" SortExpression="WorkLocationId"
                                            Visible="False" UniqueName="WorkLocationId" meta:resourcekey="GridBoundColumnResource8" />
                                        <telerik:GridBoundColumn DataField="FK_TAPolicyId" SortExpression="FK_TAPolicyId"
                                            Visible="False" UniqueName="FK_TAPolicyId" meta:resourcekey="GridBoundColumnResource9" />
                                        <telerik:GridBoundColumn DataField="TAPolicyName" SortExpression="TAPolicyName" AllowFiltering="False"
                                            HeaderText="TA Policy Name" meta:resourcekey="GridBoundColumnResource10" UniqueName="TAPolicyName" />
                                        <telerik:GridBoundColumn DataField="GPSCoordinates" SortExpression="GPSCoordinates" AllowFiltering="False"
                                            HeaderText="GPS Coordinates" meta:resourcekey="GPSCoordinatesColumnResource" UniqueName="GPSCoordinates" />
                                    </Columns>
                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                </MasterTableView>
                                <SelectedItemStyle ForeColor="Maroon" />
                            </telerik:RadGrid>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAddWork" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

        </ContentTemplate>
    </cc1:TabPanel>
</cc1:TabContainer>
<div class="row">
    <div class="col-md-12 text-center">
        <asp:Button ID="ibtnSave" runat="server" Text="Save" CssClass="button" ValidationGroup="org1"
            OnClientClick="javascript:return ValidatePage();" meta:resourcekey="ibtnSaveResource1" />
        <asp:Button ID="ibtnDelete" runat="server" Text="Delete" CssClass="button" CausesValidation="False"
            OnClientClick="return confirm('Are you sure you want delete');" meta:resourcekey="ibtnDeleteResource1" />
        <%--    <asp:Button ID="ibtnDeleteCompany" runat="server" Text="Delete" CssClass="button"
                            CausesValidation="false" OnClientClick="return confirm('Are you sure you want delete');" />--%>
        <asp:Button ID="ibtnRest" runat="server" Text="Clear" CssClass="button" CausesValidation="False"
            meta:resourcekey="ibtnRestResource1" />
    </div>
</div>
<div class="row">
    <div class="table-responsive">
        <telerik:RadGrid ID="dgrdOrg_Company" runat="server" AllowSorting="True" AllowPaging="True"
            GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="true"
            ShowFooter="True" GroupingSettings-CaseSensitive="false">
            <SelectedItemStyle ForeColor="maroon" />
            <MasterTableView AllowFilteringByColumn="True" AllowMultiColumnSorting="True" IsFilterItemExpanded="true"
                AutoGenerateColumns="False" DataKeyNames="CompanyId">
                <Columns>
                    <telerik:GridClientSelectColumn UniqueName="chk" />
                    <telerik:GridBoundColumn DataField="CompanyId" HeaderText="CompanyId" SortExpression="CompanyId"
                        Visible="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CompanyName" HeaderText="Company Name" AllowFiltering="true"
                        ShowFilterIcon="true" SortExpression="CompanyName" Resizable="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="CompanyShortName" HeaderText="Company Code" SortExpression="CompanyShortName" />
                    <telerik:GridBoundColumn DataField="CompanyArabicName" HeaderText="Arabic Name" AllowFiltering="true"
                        ShowFilterIcon="true" SortExpression="CompanyArabicName" Resizable="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="parentname" HeaderText="Parent Company" AllowFiltering="true"
                        ShowFilterIcon="true" SortExpression="parentname" Resizable="false">
                    </telerik:GridBoundColumn>
                </Columns>
                <PagerStyle Mode="NextPrevNumericAndAdvanced" />
            </MasterTableView><ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </telerik:RadGrid>
    </div>
</div>

<%--</ContentTemplate>
</asp:UpdatePanel>--%>