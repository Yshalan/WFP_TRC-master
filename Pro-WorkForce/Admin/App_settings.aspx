﻿<%@ Page Title="TA App Settings" Language="VB" Theme="SvTheme" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" CodeFile="App_settings.aspx.vb" Inherits="TA_App_settings" MaintainScrollPositionOnPostback="True"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .ajax__validatorcallout_popup_table {
            position: absolute;
        }

        .ajax__validatorcallout_popup_table_row {
            background: #fff;
        }
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
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

                var tmpTime1 = $find("<%=rmtFlexibileTime.ClientID %>");
                txtValidate(tmpTime1, true);
            }

            function ValidateDynamicReportView(sender, args) {
                var radioButtonList = document.getElementById("<%= rlsDynamicReportView.ClientID %>");
                var radioButtonCount = radioButtonList.getElementsByTagName("input");
                var check = false;
                for (var i = 0; i < radioButtonCount.length; i++) {
                    if (radioButtonCount[i].checked == true) {
                        check = true;
                    }
                }

                if (check == false)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            }

            function CheckBoxListSelect(state) {
                var chkBoxList = document.getElementById("<%= cblDurationTotalsToAppear.ClientID %>");
                var chkBoxCount = chkBoxList.getElementsByTagName("input");
                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = state;
                }
                return false;
            }

            function ValidateTextboxNoOfHours_MustComplete() {

                var tmpTime1 = $find("<%=rmtNoOfHours_MustComplete.ClientID%>");
                txtValidate(tmpTime1, true);
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Thumbnail" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
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
    <uc1:PageHeader ID="PageHeader1" runat="server" />
    <div>
        <asp:UpdatePanel ID="update1" runat="server">
            <ContentTemplate>
                <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" OnClientActiveTabChanged="hideValidatorCalloutTab"
                    meta:resourcekey="TabContainer1Resource1">
                    <cc1:TabPanel ID="tabCustomerInfo" runat="server" HeaderText="Customer Information"
                        TabIndex="0" meta:resourcekey="tabCustomerInfoResource1">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Label ID="lblCompanyName1" runat="server" CssClass="Profiletitletxt" Text="Customer English Name"
                                        meta:resourcekey="lblCompanyName1Resource1"></asp:Label>
                                    <asp:TextBox ID="txtFirstCompanyName" runat="server" meta:resourcekey="txtFirstCompanyNameResource1">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator SkinID="RequiredFieldValidator1" ID="reqFirstCompanyName"
                                        runat="server" ControlToValidate="txtFirstCompanyName" Display="None" ErrorMessage="Please enter Company Name"
                                        ValidationGroup="ReligionGroup" meta:resourcekey="reqFirstCompanyNameResource1">
                                    </asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtCompanyNameFirst" runat="server" CssClass="AISCustomCalloutStyle"
                                        TargetControlID="reqFirstCompanyName" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtFirstCompanyName"
                                        ID="RegularExpressionValidator3" ValidationExpression="^[\s\S]{0,100}$" runat="server"
                                        ErrorMessage="Maximum 100 characters allowed." ValidationGroup="ReligionGroup"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator SkinID="RequiredFieldValidator1" ID="RequiredFieldValidator4"
                                        runat="server" ControlToValidate="txtSecondCompanyName" Display="None" ErrorMessage="Please enter Company Name"
                                        ValidationGroup="ReligionGroup" meta:resourcekey="reqSecondCompanyNameResource1">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label2" runat="server" CssClass="Profiletitletxt" Text="Customer English Short Name"
                                        meta:resourcekey="Label2Resource1"></asp:Label>
                                    <asp:TextBox ID="txtSecondCompanyName" runat="server" meta:resourcekey="txtSecondCompanyNameResource1">
                                    </asp:TextBox>
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtSecondCompanyName"
                                        ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{0,50}$" runat="server"
                                        ErrorMessage="Maximum 50 characters allowed." ValidationGroup="ReligionGroup"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator SkinID="RequiredFieldValidator1" ID="RequiredFieldValidator1"
                                        runat="server" ControlToValidate="txtSecondCompanyName" Display="None" ErrorMessage="Please enter Company Name"
                                        ValidationGroup="ReligionGroup" meta:resourcekey="reqSecondCompanyNameResource1">
                                    </asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                                        TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Label ID="Label1" runat="server" CssClass="Profiletitletxt" Text="Customer Arabic Name"
                                        meta:resourcekey="Label1Resource1"></asp:Label>
                                    <asp:TextBox ID="txtFirstCompanyNameArabic" runat="server" meta:resourcekey="txtFirstCompanyNameArabicResource1">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator SkinID="RequiredFieldValidator1" ID="RequiredFieldValidator2"
                                        runat="server" ControlToValidate="txtFirstCompanyNameArabic" Display="None" ErrorMessage="Please enter Company Name"
                                        ValidationGroup="ReligionGroup" meta:resourcekey="reqFirstCompanyNameArabicResource1">
                                    </asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="AISCustomCalloutStyle"
                                        TargetControlID="RequiredFieldValidator2" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtFirstCompanyNameArabic"
                                        ID="RegularExpressionValidator1" ValidationExpression="^[\s\S]{0,50}$" runat="server"
                                        ErrorMessage="Maximum 50 characters allowed." ValidationGroup="ReligionGroup"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label3" runat="server" CssClass="Profiletitletxt" Text="Customer Arabic Short Name"
                                        meta:resourcekey="Label3Resource1"></asp:Label>
                                    <asp:TextBox ID="txtSecondCompanyNameArabic" runat="server" meta:resourcekey="txtSecondCompanyNameArabicResource1">
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator SkinID="RequiredFieldValidator1" ID="RequiredFieldValidator3"
                                        runat="server" ControlToValidate="txtSecondCompanyNameArabic" Display="None"
                                        ErrorMessage="Please enter Company Name" ValidationGroup="ReligionGroup" meta:resourcekey="reqSecondCompanyNameArabicResource1">
                                    </asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="AISCustomCalloutStyle"
                                        TargetControlID="RequiredFieldValidator3" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtSecondCompanyNameArabic"
                                        ID="RegularExpressionValidator4" ValidationExpression="^[\s\S]{0,50}$" runat="server"
                                        ErrorMessage="Maximum 50 characters allowed." ValidationGroup="ReligionGroup"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="Employee No. Length"
                                        meta:resourcekey="Label4Resource1"></asp:Label>
                                    <telerik:RadNumericTextBox ID="txtEmployeeNoLength" MinValue="0" MaxValue="9999999"
                                        Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="txtEmployeeNoLengthResource1">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="reqEmployeeNoLength" runat="server" ControlToValidate="txtEmployeeNoLength"
                                        Display="None" ErrorMessage="Please enter Employee No. Length" ValidationGroup="ReligionGroup"
                                        meta:resourcekey="reqEmployeeNoLengthResource1">
                                    </asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtEmployeeNoLength" runat="server" CssClass="AISCustomCalloutStyle"
                                        TargetControlID="reqEmployeeNoLength" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="Employee Card Length"
                                        meta:resourcekey="Label5Resource1"></asp:Label>
                                    <telerik:RadNumericTextBox ID="txtEmployeeCardLength" MinValue="0" MaxValue="99999"
                                        Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="txtEmployeeCardLengthResource1">
                                        <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="reqEmployeeCardLength" runat="server" ControlToValidate="txtEmployeeCardLength"
                                        Display="None" ErrorMessage="Please enter Employee Card Length" ValidationGroup="ReligionGroup"
                                        meta:resourcekey="reqEmployeeCardLengthResource1">
                                    </asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtEmployeeCardLength" runat="server" CssClass="AISCustomCalloutStyle"
                                        TargetControlID="reqEmployeeCardLength" WarningIconImageUrl="~/images/warning1.png"
                                        Enabled="True">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                </div>
                                <div class="col-md-4">
                                    <asp:CheckBox ID="chkShowSTSupremeLogo" Text="Show Smart Time Logo" runat="server"
                                        meta:resourcekey="lblShowSTSupremeLogoResource1" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblDefaultTheme" runat="server" Text="Default Theme" meta:resourcekey="lblDefaultThemeResource1"></asp:Label>
                                    <telerik:RadComboBox ID="radDefaultTheme" runat="server" Skin="Vista">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="1" Text="Gray" meta:resourcekey="RadComboBoxItemDofResource1" />
                                            <telerik:RadComboBoxItem Value="2" Text="Green" Selected="true" meta:resourcekey="RadComboBoxItemGreenResource1" />
                                            <telerik:RadComboBoxItem Value="3" Text="Red" meta:resourcekey="RadComboBoxItemRedResource1" />
                                            <telerik:RadComboBoxItem Value="4" Text="Blue" meta:resourcekey="RadComboBoxItemBlueResource1" />
                                            <telerik:RadComboBoxItem Value="5" Text="Violet" meta:resourcekey="RadComboBoxItemVioletResource1" />
                                            <telerik:RadComboBoxItem Value="6" Text="Gold" meta:resourcekey="RadComboBoxItemGoldResource1" />
                                            <telerik:RadComboBoxItem Value="7" Text="White" meta:resourcekey="RadComboBoxItemWhiteResource1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-1">
                                    <asp:Label ID="lblLogo" CssClass="Profiletitletxt" runat="server" Text="Logo" meta:resourcekey="lblLogoResource1">
                                    </asp:Label>
                                </div>
                                <div class="col-md-3">
                                    <div class="upload-panel">
                                        <asp:Image ID="Thumbnail" runat="server" BorderStyle="Solid" BorderWidth="1px" meta:resourcekey="ThumbnailResource1"
                                            Width="100%"></asp:Image>
                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/closefile.PNG"
                                            Visible="False" meta:resourcekey="ImageButton1Resource1" />
                                    </div>
                                    <div class="input-group">
                                        <span class="input-group-btn"><span class="btn btn-default" onclick="$(this).parent().find('input[type=file]').click();">Browse</span>
                                            <asp:FileUpload ID="BrFromFile" title="Select Logo File" runat="server" meta:resourcekey="BrFromFileResource1"
                                                name="uploaded_file" onchange="$(this).parent().parent().find('.form-control').html($(this).val().split(/[\\|/]/).pop());"
                                                Style="display: none;" type="file" />
                                        </span><span class="form-control"></span>
                                    </div>
                                    <asp:RegularExpressionValidator ID="revFileUploadValidation" Display="Dynamic" ValidationGroup="ReligionGroup"
                                        runat="server" ErrorMessage="Only picture files are allowed!" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.gif|.png|.PNG|.GIF|.jpg|.JPG|.jpeg|.JPEG)$"
                                        ControlToValidate="BrFromFile">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Time Attendance Settings"
                        TabIndex="1" meta:resourcekey="TASettingtab">
                        <ContentTemplate>
                            <div class="fancy-collapse-panel">
                                <div class="panel-group" id="accordion3" role="tablist" aria-multiselectable="true">
                                    <div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="headingOne3">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#accordion3" href="#collapseOne3" aria-expanded="true" aria-controls="collapseOne3">
                                                    <asp:Label ID="lblGeneralSettings" runat="server" Text="General Settings" meta:resourcekey="lblGeneralSettingsResource1"></asp:Label>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapseOne3" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne3">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblArchivingMonths" runat="server" CssClass="Profiletitletxt" Text="Archiving Month(s)"
                                                            meta:resourcekey="lblArchivingMonthsResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadNumericTextBox ID="txtArchivingMonths" MinValue="0" MaxValue="48" Skin="Vista"
                                                            runat="server" Culture="en-US" LabelCssClass="" Width="190px" meta:resourcekey="txtArchivingMonthsResource1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblShowLGWithEntityPerv" runat="server" Text="Show Logical Group With Entity Privilege"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblShowLGWithEntityPervResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblShowLGWithEntityPerv" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlShowViolationCorrectionResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>


                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblShowViolationCorrection" runat="server" Text="Show Violation Correction"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblShowViolationCorrectionResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdlShowViolationCorrection" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlShowViolationCorrectionResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblShowThemeToUsers" runat="server" Text="Show Themes To Users" CssClass="Profiletitletxt"
                                                            meta:resourcekey="lblShowThemeToUsersResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdlShowThemeToUsers" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlShowAbsentInViolationCorrectionResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblShowAnnouncement" runat="server" Text="Show Announcement" CssClass="Profiletitletxt"
                                                            meta:resourcekey="lblShowAnnouncementResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdlShowAnnouncement" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlShowAnnouncementResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblShowAnnouncementSelfSevice" runat="server" Text="Show Announcement on Self Service"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblShowAnnouncementSelfSeviceResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdlShowAnnouncementSelfSevice" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlShowAnnouncementSelfSeviceResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblAllowMoreOneManualEntry" runat="server" Text="Allow More One Request Manual Entry"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblAllowMoreOneManualEntryResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdlAllowMoreOneManualEntry" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlShowAnnouncementResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblShowEmployeeList" runat="server" Text="Show Employee Filter List"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblShowEmployeeListResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdlShowEmployeeList" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlShowEmployeeListResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblAllowDeleteSchedule" runat="server" Text="Allow Delete Employee Schedule"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblAllowDeleteScheduleResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblAllowDeleteSchedule" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rblAllowDeleteScheduleResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblShowDirectchk" runat="server" Text="Allow Filtering By Direct Staff"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblShowDirectchkResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblShowDirectchk" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblEmployeeManagerFilter" runat="server" Text="Manager Reports Filter"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblEmployeeManagerFilterResource" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdlEmployeeManagerFilter" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlEmployeeManagerFilterResource1">
                                                            <asp:ListItem Text="Show Only Direct Reported Employees" Value="1" meta:resourcekey="DirectItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Show Direct and Indirect reported Employees" Value="2" meta:resourcekey="IndirectItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblManagerDefaultPage" runat="server" Text="Manager & Deputy Manager Default Page"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblManagerDefaultPageResource" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblManagerDefaultPage" runat="server" RepeatDirection="Vertical">
                                                            <asp:ListItem Value="0" Text="Employee Requests Page" Selected="True" meta:resourcekey="ListItem30Resource1"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Manager Summary" meta:resourcekey="ListItem32Resource1"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Manager Reports" meta:resourcekey="ListItem33Resource1"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Groups Definition" meta:resourcekey="ListItem31Resource1"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblFillCheckBoxList" runat="server" Text="Fill Employee in CheckBoxList"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblFillCheckBoxListResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdlFillCheckBoxList" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlFillCheckBoxListResource1">
                                                            <asp:ListItem Text="Fill On Load" Value="1" meta:resourcekey="LoadItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Fill On Filter" Value="2" meta:resourcekey="FilterItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblWeekStartDay" runat="server" Text="Week Start Day" CssClass="Profiletitletxt"
                                                            meta:resourcekey="lblWeekStartDayResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadComboBox ID="RadWeeksDay" MarkFirstMatch="True" Skin="Vista" Width="210px"
                                                            ToolTip="View Week Days" runat="server" meta:resourcekey="RadWeeksDayResource1">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblSchedules" runat="server" CssClass="Profiletitletxt" Text="Work Schedules"
                                                            meta:resourcekey="lblSchedulesResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadComboBox ID="RadComboBoxWorkSchedules" runat="server" MarkFirstMatch="true" ExpandDirection="Up"
                                                            meta:resourcekey="RadComboBoxWorkSchedulesResource1">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblStudyPermSchedule" runat="server" CssClass="Profiletitletxt" Text="Study Permission Work Schedule"
                                                            meta:resourcekey="lblStudyPermScheduleResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadComboBox ID="RadComboBoxStudyWorkSchedules" runat="server" MarkFirstMatch="true" ExpandDirection="Up"
                                                            Width="210px" meta:resourcekey="RadComboBoxStudyWorkSchedules">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblOnCallSchedule" runat="server" CssClass="Profiletitletxt" Text="Employee On Call Work Schedule"
                                                            meta:resourcekey="lblOnCallScheduleResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadComboBox ID="RadComboBoxOnCallWorkSchedules" runat="server" MarkFirstMatch="true" ExpandDirection="Up"
                                                            Width="210px" meta:resourcekey="RadComboBoxOnCallWorkSchedules">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblSystemUsers" runat="server" Text="System Users" CssClass="Profiletitletxt"
                                                            meta:resourcekey="lblSystemUsersResource1" />
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:RadioButtonList ID="rblSystemUsers" runat="server" RepeatDirection="Horizontal"
                                                            AutoPostBack="True" meta:resourcekey="rblSystemUsersResource1">
                                                            <asp:ListItem Text="System Users" Value="1" meta:resourcekey="ListItemResource2">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Domain" Value="2" meta:resourcekey="ListItemResource3">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Mixed Mode" Value="3" meta:resourcekey="ListItemResource4">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row" id="dvPasswordType" runat="server">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblPasswordType" runat="server" Text="Password Complexity"
                                                            meta:resourcekey="lblPasswordTypeResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblPasswordType" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Simple" Value="1" Selected="True" meta:resourcekey="SimplePasswordResource1"></asp:ListItem>
                                                            <asp:ListItem Text="Complex" Value="2" meta:resourcekey="ComplexPasswordResource1"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblShowLoginForm" runat="server" Text="Show Login Form" CssClass="Profiletitletxt"
                                                            meta:resourcekey="lblShowLoginFormResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdlShowLoginForm" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlShowLoginFormResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblOvertime" runat="server" CssClass="Profiletitletxt" Text="Allow Edit Over Time"
                                                            meta:resourcekey="lblOvertimeResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblOvertime" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rblOvertimeResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblChangeNo" runat="server" CssClass="Profiletitletxt" Text="Allow Change Employee No. for"
                                                            meta:resourcekey="lblChangeNoResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:RadioButtonList ID="rblChangeNo" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rblChangeNoResource1">
                                                            <asp:ListItem Text="All Employees" Value="1" meta:resourcekey="ListItemResource26"
                                                                Selected="true">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Internal Employees" Value="2" meta:resourcekey="ListItemResource27">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="External Employees" Value="3" meta:resourcekey="ListItemResource28">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="fancy-collapse-panel">
                                <div class="panel-group" id="accordion2" role="tablist" aria-multiselectable="true">
                                    <div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="headingOne2">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#accordion2" href="#collapseOne2" aria-expanded="true" aria-controls="collapseOne2">
                                                    <asp:Label ID="lblCalculationSettings" runat="server" Text="Calculation Settings" meta:resourcekey="lblCalculationSettingsResource1"></asp:Label>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapseOne2" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne2">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label6" runat="server" CssClass="Profiletitletxt" Text="Manage default overtime rule by"
                                                            meta:resourcekey="Label6Resource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButton ID="rdbtnGradeot" runat="server" Text="Grade" GroupName="Software1"
                                                            meta:resourcekey="rdbtnGradeotResource1" />
                                                        <asp:RadioButton ID="rdbtnDesignationot" runat="server" Text="Designation" GroupName="Software1"
                                                            meta:resourcekey="rdbtnDesignationotResource1" />
                                                    </div>
                                                </div>
                                                <%--<tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" CssClass="Profiletitletxt" Text="Manage default Leave by"
                                    Width="200px" meta:resourcekey="Label8Resource1"></asp:Label>
                                <td>
                                    <asp:RadioButton ID="rdbtnNone" runat="server" Text="None" GroupName="Software3"
                                        meta:resourcekey="rdbtnNoneResource1" />&nbsp;
                                    <asp:RadioButton ID="rdbtnGradeAl" runat="server" Text="Grade" GroupName="Software3"
                                        meta:resourcekey="rdbtnGradeAlResource1" />
                                    <asp:RadioButton ID="rdbtnDesignationAl" runat="server" Text="Designation" GroupName="Software3"
                                        meta:resourcekey="rdbtnDesignationAlResource1" />
                                </td>
                            </td>
                        </tr>--%>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label7" runat="server" CssClass="Profiletitletxt" Text="Manage default TA Exception by"
                                                            meta:resourcekey="Label7Resource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButton ID="rdbtnGradeTaex" runat="server" Text="Grade" GroupName="Software2"
                                                            meta:resourcekey="rdbtnGradeTaexResource1" />
                                                        <asp:RadioButton ID="rdbtnDesignationTaex" runat="server" Text="Designation" GroupName="Software2"
                                                            meta:resourcekey="rdbtnDesignationTaexResource1" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblGrace" runat="server" CssClass="Profiletitletxt" Text="Grace Period By"
                                                            meta:resourcekey="lblGraceResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdbGrace" runat="server" RepeatDirection="Horizontal" meta:resourcekey="rdbGraceResource1">
                                                            <asp:ListItem Text="Work Schedule" Value="2" meta:resourcekey="ListItemResource10">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="TA Policy" Value="1" meta:resourcekey="ListItemResource9">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4"></div>
                                                    <div class="col-md-4">
                                                        <asp:CheckBox ID="chkExcludeGraceFromLostTime" runat="server" Text="Exclude Grace Period From LostTime"
                                                            meta:resourcekey="chkExcludeGraceFromLostTimeResource1" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblConsiderAbsent" runat="server" CssClass="Profiletitletxt" Text="Consider Absent After"
                                                            meta:resourcekey="lblConsiderAbsentResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadMaskedTextBox ID="rmtFlexibileTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                                            DisplayMask="##:##" Text='0000' LabelCssClass="" meta:resourcekey="rmtFlexibileTimeResource1">
                                                            <ClientEvents OnBlur="ValidateTextboxFrom" />
                                                        </telerik:RadMaskedTextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblConsiderAbsentEvenIfAttend" runat="server" CssClass="Profiletitletxt"
                                                            Text="Consider Absent Even If Attend" meta:resourcekey="lblConsiderAbsentEvenIfAttendResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdlConsiderAbsentEvenAttend" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlConsiderAbsentEvenAttendResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div id="Div1" class="row" runat="server" visible="false">
                                                    <div class="col-md-4">
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblConsiderAbsentOrLogicalAbsent" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdbGraceResource1">
                                                            <asp:ListItem Text="Absent" Value="1" Selected="True" meta:resourcekey="ListItemAbsent">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Logical Absent" Value="2" meta:resourcekey="ListItemLogicalAbsent">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblConsiderDays" runat="server" Text="Consider Days" CssClass="Profiletitletxt"
                                                            meta:resourcekey="lblConsiderDaysResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadNumericTextBox ID="txtConsiderDays" MinValue="0" MaxValue="99999" Skin="Vista"
                                                            runat="server" Culture="en-US" LabelCssClass="" Width="200px" meta:resourcekey="txtConsiderDaysResource1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <asp:RequiredFieldValidator ID="reqConsiderDays" runat="server" ControlToValidate="txtConsiderDays"
                                                            Display="None" ErrorMessage="Please enter Consider Days" ValidationGroup="ReligionGroup"
                                                            meta:resourcekey="reqConsiderDaysResource1">
                                                        </asp:RequiredFieldValidator>
                                                        <cc1:ValidatorCalloutExtender ID="ExtConsiderDays" runat="server" CssClass="AISCustomCalloutStyle"
                                                            TargetControlID="reqConsiderDays" WarningIconImageUrl="~/images/warning1.png"
                                                            Enabled="True">
                                                        </cc1:ValidatorCalloutExtender>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:Label ID="Label8" runat="server" CssClass="Profiletitletxt" Text="Minutes" meta:resourcekey="lblMinutesResource1" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblIsAbsentRestPolicy" runat="server" Text="Consider Rest Day(s) To Absent If Between Absent Days"
                                                            meta:resourcekey="lblIsAbsentRestPolicyResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblIsAbsentRestPolicy" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rblIsAbsentRestPolicyResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" Selected="True" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label9" runat="server" CssClass="Profiletitletxt" Text="In Consequence Transactions Consider"
                                                            meta:resourcekey="Label9Resource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:RadioButtonList ID="rdbConsequenceTransactions" runat="server" meta:resourcekey="rdbConsequenceTransactionsResource1">
                                                            <asp:ListItem Text="First In/ Last Out" Value="1" meta:resourcekey="ListItemResource15">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Last In/ First Out" Value="2" meta:resourcekey="ListItemResource16">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblMintransactiontime" runat="server" CssClass="Profiletitletxt" Text="Minimum Time Between Two Transactions"
                                                            meta:resourcekey="lblMintransactiontimeResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadNumericTextBox ID="txtMintransactiontime" MinValue="0" MaxValue="99999"
                                                            Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" Width="200px" meta:resourcekey="txtMintransactiontimeResource1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                        <asp:RequiredFieldValidator ID="reqMintransactiontime" runat="server" ControlToValidate="txtMintransactiontime"
                                                            Display="None" ErrorMessage="Please enter minimum time between Transactions"
                                                            ValidationGroup="ReligionGroup" meta:resourcekey="reqMintransactiontimeResource1">
                                                        </asp:RequiredFieldValidator>
                                                        <cc1:ValidatorCalloutExtender ID="ExtMintransactiontime" runat="server" CssClass="AISCustomCalloutStyle"
                                                            TargetControlID="reqMintransactiontime" WarningIconImageUrl="~/images/warning1.png"
                                                            Enabled="True">
                                                        </cc1:ValidatorCalloutExtender>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:Label ID="lblMinutes" runat="server" CssClass="Profiletitletxt" Text="Minutes"
                                                            meta:resourcekey="lblMinutesResource1"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblNoShiftShcedule" runat="server" CssClass="Profiletitletxt" Text="Advance Schedule With no Shift"
                                                            meta:resourcekey="lblNoShiftShceduleResource1" />
                                                    </div>
                                                    <div class="col-md-8">
                                                        <asp:RadioButtonList ID="rlsNoShiftShcedule" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="NoShiftShceduleResource1">
                                                            <asp:ListItem Text="Consider it Off Day" Value="1" meta:resourcekey="ListItemResource24">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Consider Default Schedule" Value="2" meta:resourcekey="ListItemResource25">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Absent" Value="3" meta:resourcekey="ListItemResource31">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblConsiderRestInshiftSch" runat="server" Text="In Shift Schedule Consider" meta:resourcekey="lblConsiderRestInshiftSchResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblConsiderRestInshiftSch" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Text="Rest Day First" Selected="True" meta:resourcekey="ListItem1ConsiderRestInshiftSchResource1"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Leave & Permission First" meta:resourcekey="ListItem2ConsiderRestInshiftSchResource1"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblMinOutViolation" runat="server" CssClass="Profiletitletxt" Text="Minimum Out Duration To Be Considered As Violation"
                                                            meta:resourcekey="lblMinOutViolation"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadNumericTextBox ID="radcmbMinOutViolation" MaxValue="99999" MinValue="0"
                                                            Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" Width="200px" meta:resourcekey="radcmbMinOutViolationResource1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:Label ID="Label10" runat="server" Text="Minute(s)" CssClass="Profiletitletxt"
                                                            meta:resourcekey="lblMinutesResource1" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="fancy-collapse-panel">
                                <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                                    <div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="headingOne">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                                    <asp:Label ID="lblSelfServices" runat="server" Text="Self Services Settings" meta:resourcekey="lblSelfServicesResource1"></asp:Label>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblLeaveApproval" runat="server" CssClass="Profiletitletxt" Text="Self Service Workflow Level(s)"
                                                            meta:resourcekey="lblLeaveApprovalResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:RadioButtonList ID="rlstApproval" runat="server" meta:resourcekey="rlstApprovalResource1"
                                                            RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Direct Manager Only" Value="1" meta:resourcekey="rdbtnDirectmgrResource1">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Human Resource Only" Value="2" meta:resourcekey="rdbtnHROnlyResource1">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Both" Value="3" meta:resourcekey="rdbtnBothResource1">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="DM,HR,GM" Value="4" meta:resourcekey="rdbtnDMHRGMResource1">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblHasMultiApproval" runat="server" Text="Has Multi Approval" CssClass="Profiletitletxt"
                                                            meta:resourcekey="lblHasMultiApprovalResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdlHasMultiApproval" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlHasMultiApprovalResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblApprovalRecalMethod" runat="server" Text="Approval Recalculate Method"
                                                            meta:resourcekey="lblApprovalRecalMethodResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:RadioButtonList ID="rblApprovalRecalMethod" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Text="Immediatly" Selected="True" meta:resourcekey="ApprovalRecalMethodListItem1Resource1"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Recalculate Request" meta:resourcekey="ApprovalRecalMethodListItem2Resource1"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblEmployeeRequests" runat="server" CssClass="Profiletitletxt" Text="Employee Requests"
                                                            meta:resourcekey="lblEmployeeRequestsResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="table-responsive" style="height: 130px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc">
                                                            <asp:CheckBoxList ID="cblEployeeRequests" runat="server" Style="height: 26px" RepeatDirection="Vertical">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                    <%-- <td style="vertical-align: top">
                                
                                       
                                            <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                                                <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                                           <br />
                                            <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                                                <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                                        
                            </td>--%>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblAllowedManual" runat="server" CssClass="Profiletitletxt" Text="Manual Entry Allowed Before"
                                                            meta:resourcekey="lblAllowedManualResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadNumericTextBox ID="radtxtAllowedManual" MaxValue="99999" Skin="Vista"
                                                            runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="radtxtAllowedManualResource1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:Label ID="lbldays2" runat="server" Text="Days" CssClass="Profiletitletxt" meta:resourcekey="lblDaysResource1" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblNoManualEntry" runat="server" CssClass="Profiletitletxt" Text="No. Of Allowed Entry Requests Per Day"
                                                            meta:resourcekey="lblNoManualEntryResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadNumericTextBox ID="radtxtNoManualEntry" MaxValue="99999" Skin="Vista"
                                                            runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="radtxtNoManualEntryResource1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:Label ID="lblTimes" runat="server" Text="Time(s)" CssClass="Profiletitletxt"
                                                            meta:resourcekey="lblTimesResource1" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblNoManualEntryPerMonth" runat="server" CssClass="Profiletitletxt" Text="No. Of Allowed Entry Requests Per Month"
                                                            meta:resourcekey="lblNoManualEntryPerMonthResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadNumericTextBox ID="radnumNoManualEntryPerMonth" MaxValue="99999" Skin="Vista"
                                                            runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="radnumNoManualEntryPerMonthResource1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:Label ID="Label14" runat="server" Text="Time(s)" CssClass="Profiletitletxt"
                                                            meta:resourcekey="lblTimesResource1" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblAllowEditManualEntryRequestDate" runat="server" Text="Allow Edit Manual Entry Request Date"
                                                            meta:resourcekey="lblAllowEditManualEntryRequestDateResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblAllowEditManualEntryRequestDate" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Text="Yes" Selected="True" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="No" meta:resourcekey="ListItemResource18"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblAllowEditManualEntryRequestTime" runat="server" Text="Allow Edit Manual Entry Request Time"
                                                            meta:resourcekey="lblAllowEditManualEntryRequestTimeResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblAllowEditManualEntryRequestTime" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Text="Yes" Selected="True" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="No" meta:resourcekey="ListItemResource18"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblNumberInTransactionRequests" runat="server" Text="Number of In Transaction(s) Per Day"
                                                            meta:resourcekey="lblNumberInTransactionRequestsResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadNumericTextBox ID="txtNumberInTransactionRequests" MaxValue="99999" MinValue="0"
                                                            Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" Width="200px" meta:resourcekey="txtNumberInTransactionRequestsResource1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblNumberOutTransactionRequests" runat="server" Text="Number of Out Transaction(s) Per Day"
                                                            meta:resourcekey="lblNumberOutTransactionRequestsResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadNumericTextBox ID="txtNumberOutTransactionRequests" MaxValue="99999" MinValue="0"
                                                            Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" Width="200px" meta:resourcekey="txtNumberOutTransactionRequestsResource1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4"></div>
                                                    <div class="col-md-4">
                                                        <asp:CheckBox ID="chkIsAutoApproveManualEntryRequest" runat="server" Text="Auto Approve Manual Entry Request"
                                                            meta:resourcekey="chkIsAutoApproveManualEntryRequestResource1" AutoPostBack="true" />
                                                    </div>
                                                </div>
                                                <div class="row" id="dvManagerLevel" runat="server">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblManagerLevel" runat="server" CssClass="Profiletitletxt" Text="Manager Level Required For Self Service Manual Entry Request(s) Approval"
                                                            meta:resourcekey="lblManagerLevelResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadComboBox ID="radcmbLevels" Filter="Contains" MarkFirstMatch="true" Skin="Vista"
                                                            runat="server">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblManualRequestAttachement" runat="server" Text="Attachment Is Mandatory For Manual Entry Request"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblManualRequestAttachementResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblManualRequestAttachement" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rblManualRequestAttachementResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>

                                                  <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label17" runat="server" Text="Remark Is Mandatory For Manual Entry Request"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblManualRequestRemarkResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblManualRequestRemark" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rblManualRequestRemarkResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>





                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblHRManualRequestAttachement" runat="server" Text="Attachment Is Mandatory For Human Resource Manual Entry"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblHRManualRequestAttachementResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblHRManualRequestAttachement" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rblHRManualRequestAttachementResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblAttachmentIsMandatory" runat="server" Text="Attachment Is Mandatory For Nursing & Study Permission"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblAttachmentIsMandatoryPermissionResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdlAttachmentIsMandatory" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlAttachmentIsMandatoryResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblRemoteWorkTAReason" runat="server" CssClass="Profiletitletxt" Text="Remote Attendance Reasons"
                                                            meta:resourcekey="lblRemoteWorkTAReasonResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="table-responsive" style="height: 130px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc">
                                                            <asp:CheckBoxList ID="cblRemoteWorkTAReason" runat="server" Style="height: 26px" RepeatDirection="Vertical">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblViolationJustificationDays" runat="server" Text="No Of Days To Justify Violations"
                                                            meta:resourcekey="lblViolationJustificationDaysResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <telerik:RadNumericTextBox ID="radnumViolationJustificationDays" MinValue="0" MaxValue="365"
                                                            Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" Width="200px" meta:resourcekey="radnumViolationJustificationDaysResource1">
                                                            <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                    <div class="col-md-1">
                                                        <asp:Label ID="Label12" runat="server" Text="Days" CssClass="Profiletitletxt" meta:resourcekey="lblDaysResource1" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblViolationJustificationDaysPolicy" runat="server" Text="Exclude From Violation Justification Days"
                                                            meta:resourcekey="lblViolationJustificationDaysPolicyResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:CheckBoxList ID="cblViolationJustificationDaysPolicy" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Holiday" Value="1" meta:resourcekey="cblViolationJustificationDaysPolicyItem1Resource1"></asp:ListItem>
                                                            <asp:ListItem Text="Leave" Value="2" meta:resourcekey="cblViolationJustificationDaysPolicyItem2Resource1"></asp:ListItem>
                                                            <asp:ListItem Text="Rest Day" Value="3" meta:resourcekey="cblViolationJustificationDaysPolicyItem3Resource1"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblDivideTwoPermission" runat="server" Text="Show Divide Two Permission"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblDivideTwoPermissionResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdlDivideTwoPermission" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlDivideTwoPermissionResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblShowAbsentInViolationCorrection" runat="server" Text="Show Absent In Violation Correction"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblShowAbsentInViolationCorrectionResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdlShowAbsentInViolationCorrection" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlShowAbsentInViolationCorrectionResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblShowLeavelnk" runat="server" Text="Show Leave Request Link in Violation Correction"
                                                            meta:resourcekey="lblShowLeavelnkResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblShowLeavelnk" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rblShowLeavelnkResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblShowPermission" runat="server" Text="Show Permission Request Link in Violation Correction"
                                                            meta:resourcekey="lblShowPermissionlnkResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblShowPermissionlnk" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rblShowPermissionlnkResource1">
                                                            <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblIsFirstGrid" runat="server" Text="In Self Service Request First Page to Show"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblIsFirstGridResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rdlIsFirstGrid" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rdlIsFirstGridResource1">
                                                            <asp:ListItem Text="Filter" Value="1" meta:resourcekey="FilterItemResource17">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Request" Value="2" meta:resourcekey="RequestItemResource18">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblDurationTotalsToAppear" runat="server" CssClass="Profiletitletxt"
                                                            Text="Employee Summary Page" meta:resourcekey="lblDurationTotalsToAppearResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="table-responsive" style="height: 130px; overflow: auto; border-style: solid; border-width: 1px; border-color: #ccc">
                                                            <asp:CheckBoxList ID="cblDurationTotalsToAppear" runat="server" Style="height: 26px"
                                                                RepeatDirection="Vertical">
                                                            </asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                    <%--
                                            <a href="javascript:void(0)" onclick="CheckBoxListSelect(true)" style="font-size: 8pt">
                                                <asp:Literal ID="Literal1" runat="server" Text="Select All" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                                           <br />
                                            <a href="javascript:void(0)" onclick="CheckBoxListSelect(false)" style="font-size: 8pt">
                                                <asp:Literal ID="Literal2" runat="server" Text="Unselect All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                                                    --%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="fancy-collapse-panel" id="dvMobileSettings" runat="server">
                                <div class="panel-group" id="accordion9" role="tablist" aria-multiselectable="true">
                                    <div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="headingOne9">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#accordion9" href="#collapseOne9" aria-expanded="true" aria-controls="collapseOne9">
                                                    <asp:Label ID="lblMobileSettings" runat="server" Text="Mobile Settings" meta:resourcekey="lblMobileSettingsResource1"></asp:Label>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapseOne9" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne9">
                                            <div class="panel-body">

                                                <div class="row" id="dvHasTawajudFeatures" runat="server" visible="false">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblHasTawajudFeatures" runat="server" Text="Enable Tawajud Features" ToolTip="By Enabling This Option, Your Mobile Application Will Enable Tawajud Application Features" meta:resourcekey="lblHasTawajudFeaturesResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblHasTawajudFeatures" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Text="Yes" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="No" Selected="True" meta:resourcekey="ListItemResource18"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row" id="dvHasMultiLocations" runat="server" visible="false">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblHasMultiLocations" runat="server" Text="Enable Multiple Work Locations" ToolTip="By Enabling This Feature, Application Will Allow Mobile User To Punch In & Out From Multiple Work Location(s)" meta:resourcekey="lblHasMultiLocationsResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblHasMultiLocations" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Text="Yes" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="No" Selected="True" meta:resourcekey="ListItemResource18"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row" id="dvHasHeartBeat" runat="server" visible="false">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblHasHeartBeat" runat="server" Text="Enable Heart Beat" ToolTip="By Enabling This Feature, Mobile Application Will Send Device Location Periodically" meta:resourcekey="lblHasHeartBeatResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblHasHeartBeat" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Text="Yes" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="No" Selected="True" meta:resourcekey="ListItemResource18"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row" id="dvHasFeedback" runat="server" visible="false">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblHasFeedback" runat="server" Text="Enable Feedback" ToolTip="If Your Application Contains Feedback Module, Administrator Can Send FeedBack Questions For Mobile Users Periodically" meta:resourcekey="lblHasFeedbackResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblHasFeedback" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Text="Yes" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="No" Selected="True" meta:resourcekey="ListItemResource18"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row" id="dvAllowFingerPunch" runat="server" visible="false">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblAllowFingerPunch" runat="server" Text="Enable Punch With Finger" ToolTip="By Enabling This Feature, Mobile Users Will Be Able to Punch In & Out Using Their Finger" meta:resourcekey="lblAllowFingerPunchResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblAllowFingerPunch" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Text="Yes" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="No" Selected="True" meta:resourcekey="ListItemResource18"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row" id="dvAllowFingerLogin" runat="server" visible="false">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblAllowFingerLogin" runat="server" Text="Enable Login With Finger" ToolTip="By Enabling This Feature, Mobile Users Will Be Able to Login To The Mobile Application Using Their Finger" meta:resourcekey="lblAllowFingerLoginResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblAllowFingerLogin" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Text="Yes" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="No" Selected="True" meta:resourcekey="ListItemResource18"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row" id="dvAllowFacePunch" runat="server" visible="false">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblAllowFacePunch" runat="server" Text="Enable Punch With Face" ToolTip="By Enabling This Feature, Mobile Users Will Be Able to Punch In & Out Using Face Identification" meta:resourcekey="lblAllowFacePunchResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblAllowFacePunch" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Text="Yes" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="No" Selected="True" meta:resourcekey="ListItemResource18"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row" id="dvAllowFaceLogin" runat="server" visible="false">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblAllowFaceLogin" runat="server" Text="Enable Login With Face" ToolTip="By Enabling This Feature, Mobile Users Will Be Able to Login To The Mobile Application Using Face Identification" meta:resourcekey="lblAllowFaceLoginResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblAllowFaceLogin" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Text="Yes" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="No" Selected="True" meta:resourcekey="ListItemResource18"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="fancy-collapse-panel">
                                <div class="panel-group" id="accordion1" role="tablist" aria-multiselectable="true">
                                    <div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="headingOne1">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#accordion1" href="#collapseOne1" aria-expanded="true" aria-controls="collapseOne1">
                                                    <asp:Label ID="lblReportSettings" runat="server" Text="Report Settings" meta:resourcekey="lblReportSettingsResource1"></asp:Label>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapseOne1" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne1">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblDefaultReportFormat" runat="server" Text="Default Report Format"
                                                            CssClass="Profiletitletxt" meta:resourcekey="lblDefaultReportFormatResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblDefaultReportFormat" runat="server" RepeatDirection="Horizontal"
                                                            CssClass="Profiletitletxt" meta:resourcekey="rblFormatResource1">
                                                            <asp:ListItem Text="PDF" Value="1" meta:resourcekey="PDFResource1"></asp:ListItem>
                                                            <asp:ListItem Text="MS Word" Value="2" meta:resourcekey="WordResource2"></asp:ListItem>
                                                            <asp:ListItem Text="MS Excel" Value="3" meta:resourcekey="ExcelResource3"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblDynamicReport" runat="server" Text="Dynamic Report View" CssClass="Profiletitletxt"
                                                            meta:resourcekey="lblDynamicReportResource1" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rlsDynamicReportView" runat="server" RepeatDirection="Horizontal"
                                                            meta:resourcekey="rlsDynamicReportViewResource1">
                                                            <asp:ListItem Text="SQL" Value="1" meta:resourcekey="ListItemResource19">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Web" Value="2" meta:resourcekey="ListItemResource20">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <asp:CustomValidator ID="rfvReportView" runat="server" ClientValidationFunction="ValidateDynamicReportView"
                                                            ErrorMessage="Please Select Report View Method" ValidationGroup="ReligionGroup"
                                                            ControlToValidate="rlsDynamicReportView" Display="None" meta:resourcekey="rfvReportView" />
                                                        <cc1:ValidatorCalloutExtender ID="vceReportView" runat="server" CssClass="AISCustomCalloutStyle"
                                                            TargetControlID="rfvReportView" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                                                        </cc1:ValidatorCalloutExtender>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label13" runat="server" Text="" CssClass="Profiletitletxt" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rbNormalColoredReport" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Normal Report" Value="0" meta:resourcekey="NormalReport">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Colored Report" Value="1" meta:resourcekey="ColoredReport">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblDailyReportDate" runat="server" Text="Daily Report Date Calendar" meta:resourcekey="lblDailyReportDateResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblDailyReportDate" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Month Range" Value="0" meta:resourcekey="DailyReportMonthResource1">
                                                            </asp:ListItem>
                                                            <asp:ListItem Text="Day Range" Value="1" meta:resourcekey="DailyReportDayResource1">
                                                            </asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="lblDeductionReport" runat="server" Text="Monthly Deduction Report" meta:resourcekey="lblDeductionReportResource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:RadioButtonList ID="rblDeductionReport" runat="server" RepeatDirection="Horizontal" meta:resourcekey="rblDeductionReportResource1">
                                                            <asp:ListItem Value="1" Text="Time Attendance2 Report" Selected="True" meta:resourcekey="ListItemTimeAttendanceResource"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Daily Report" meta:resourcekey="ListItemDailyReportResource"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tabPermissionsAndLeaves" runat="server" HeaderText="Permissions And Leaves"
                        TabIndex="4" meta:resourcekey="tabPermissionsAndLeavesResource1">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="upPermissionsAndLeaves" runat="server">
                                <ContentTemplate>
                                    <div class="fancy-collapse-panel">
                                        <div class="panel-group" id="accordion4" role="tablist" aria-multiselectable="true">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="headingOne4">
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion4" href="#collapseOne4" aria-expanded="true" aria-controls="collapseOne4">
                                                            <asp:Label ID="lblLeavesSettings" runat="server" Text="Leaves Settings" meta:resourcekey="lblLeavesSettingsResource1"></asp:Label>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="collapseOne4" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne4">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblLeaveApprovalFrom" runat="server" Text="Leave Approval From" CssClass="Profiletitletxt"
                                                                    meta:resourcekey="lblLeaveApprovalFromResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <telerik:RadComboBox ID="cmbLeaveApprovalFrom" MarkFirstMatch="true" Skin="Vista"
                                                                    runat="server">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="System" Value="1" runat="server" meta:resourcekey="RadComboBoxItemResource2" />
                                                                        <telerik:RadComboBoxItem Text="Leave Defintion" Value="2" runat="server" meta:resourcekey="RadComboBoxItemResource3" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblMaternityLeaveType" runat="server" CssClass="Profiletitletxt" Text="Maternity Leave Type"
                                                                    meta:resourcekey="lblMaternityLeaveTypeResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <telerik:RadComboBox ID="radcmbMaternityLeaveType" Filter="Contains" MarkFirstMatch="true"
                                                                    Skin="Vista" runat="server">
                                                                </telerik:RadComboBox>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblNursingRequireMaternity" runat="server" Text="Must have Maternity Leave to apply for Nursing Permission"
                                                                    CssClass="Profiletitletxt" meta:resourcekey="lblNursingRequireMaternityResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:CheckBox ID="chkNursingRequireMaternity" Text="&nbsp;" runat="server" AutoPostBack="true" />
                                                            </div>
                                                        </div>
                                                        <div id="MaternityLeaveDuration" runat="server" visible="false" class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblMaternityLeaveDuration" runat="server" Text="Maternity Leave Duration"
                                                                    CssClass="Profiletitletxt" meta:resourcekey="lblMaternityLeaveDurationResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <telerik:RadNumericTextBox ID="txtMaternityLeaveDuration" MinValue="0" MaxValue="99999"
                                                                    Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" Width="200px" meta:resourcekey="txtMaternityLeaveDurationResource1">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:Label ID="lblMonths" runat="server" CssClass="Profiletitletxt" Text="Month(s)"
                                                                    meta:resourcekey="lblMonthsResource1" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblConsiderLeaveOnOffDay" runat="server" Text="Leave Day Status In Off Day"
                                                                    meta:resourcekey="lblConsiderLeaveOnOffDayResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButtonList ID="rblConsiderLeaveOnOffDay" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Leave" Value="0" Selected="True" meta:resourcekey="ListItem1ConsiderLeaveOnOffDayResource1"></asp:ListItem>
                                                                    <asp:ListItem Text="Rest Day" Value="1" meta:resourcekey="ListItem2ConsiderLeaveOnOffDayResource1"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="fancy-collapse-panel">
                                        <div class="panel-group" id="accordion5" role="tablist" aria-multiselectable="true">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="headingOne5">
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion5" href="#collapseOne5" aria-expanded="true" aria-controls="collapseOne5">
                                                            <asp:Label ID="lblPermissionsSettings" runat="server" Text="Permissions Settings" meta:resourcekey="lblPermissionsSettingsResource1"></asp:Label>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="collapseOne5" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne5">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblPermissionApprovalFrom" runat="server" Text="Permission Approval From"
                                                                    CssClass="Profiletitletxt" meta:resourcekey="lblPermissionApprovalFromResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <telerik:RadComboBox ID="cmbPermissionApprovalFrom" MarkFirstMatch="true" Skin="Vista"
                                                                    runat="server">
                                                                    <Items>
                                                                        <telerik:RadComboBoxItem Text="System" Value="1" runat="server" meta:resourcekey="RadComboBoxItemResource2" />
                                                                        <telerik:RadComboBoxItem Text="Permission Defintion" Value="2" runat="server" meta:resourcekey="RadComboBoxItemResource4" />
                                                                    </Items>
                                                                </telerik:RadComboBox>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblPersonalPermissionType" runat="server" CssClass="Profiletitletxt"
                                                                    Text="Personal Permission Type" meta:resourcekey="lblPersonalPermissionTypeResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">

                                                                <telerik:RadComboBox ID="radcmbPersonalPermissionType" Filter="Contains" MarkFirstMatch="true"
                                                                    Skin="Vista" runat="server">
                                                                </telerik:RadComboBox>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblAutoPersonalPermissionDelay" runat="server" Text="Auto Personal Permission For Delay"
                                                                    CssClass="Profiletitletxt" meta:resourcekey="lblAutoPersonalPermissionDelayResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButtonList ID="rdlAutoPersonalPermissionDelay" runat="server" RepeatDirection="Horizontal"
                                                                    meta:resourcekey="rdlAutoPersonalPermissionDelayResource1" AutoPostBack="true">
                                                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                                    </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="trDelayDuration">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblAutoPermissionDelayDuration" runat="server" CssClass="Profiletitletxt"
                                                                    Text="Maximum Delay Duration" meta:resourcekey="lblAutoPermissionDelayDurationResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <telerik:RadNumericTextBox ID="txtAutoPermissionDelayDuration" MinValue="1" MaxValue="480"
                                                                    Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" Width="200px" meta:resourcekey="txtAutoPermissionDelayDurationResource1">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </div>
                                                            <asp:Label ID="lblMinut" runat="server" CssClass="Profiletitletxt" Text="Minutes"
                                                                meta:resourcekey="lblMinutesResource1" />
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblAutoPersonalPermissionEarlyOut" runat="server" Text="Auto Personal Permission For Early Out"
                                                                    CssClass="Profiletitletxt" meta:resourcekey="lblAutoPersonalPermissionEarlyOutResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButtonList ID="rdlAutoPersonalPermissionEarlyOut" runat="server" RepeatDirection="Horizontal"
                                                                    meta:resourcekey="rdlAutoPersonalPermissionEarlyOutResource1" AutoPostBack="true">
                                                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                                    </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="row" runat="server" id="trEarlyOutDuration">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblAutoPermissionEarlyOutDuration" runat="server" CssClass="Profiletitletxt"
                                                                    Text="Maximum Early Out Duration" meta:resourcekey="lblAutoPermissionEarlyOutDurationResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <telerik:RadNumericTextBox ID="txtAutoPermissionEarlyOutDuration" MinValue="1" MaxValue="480"
                                                                    Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" Width="200px" meta:resourcekey="txtAutoPermissionEarlyOutDurationResource1">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </div>
                                                            <asp:Label ID="lblMinute" runat="server" CssClass="Profiletitletxt" Text="Minutes"
                                                                meta:resourcekey="lblMinutesResource1" />
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblHasFullDayPermission" runat="server" Text="Has Full Day Permission"
                                                                    Visible="false" CssClass="Profiletitletxt" meta:resourcekey="lblHasFullDayPermissionResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButtonList ID="rdlFullDayPermission" runat="server" RepeatDirection="Horizontal"
                                                                    Visible="false" meta:resourcekey="rdlFullDayPermissionResource1">
                                                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                                    </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblHasPermissionForPeriod" runat="server" Text="Has Permission For Period"
                                                                    Visible="false" CssClass="Profiletitletxt" meta:resourcekey="lblHasPermissionForPeriodResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButtonList ID="rdlHasPermissionForPeriod" runat="server" RepeatDirection="Horizontal"
                                                                    Visible="false" meta:resourcekey="rdlHasPermissionForPeriodResource1">
                                                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                                    </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblConsiderPermissionBalance" runat="server" Text="Consider Permission Balance In"
                                                                    CssClass="Profiletitletxt" meta:resourcekey="lblConsiderPermissionBalanceResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButtonList ID="rdlConsiderPermissionBalance" runat="server" RepeatDirection="Horizontal"
                                                                    meta:resourcekey="rdlConsiderPermissionBalanceResource1">
                                                                    <asp:ListItem Text="Minutes " Value="0" Selected="True" meta:resourcekey="MinutesResource1">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="Hours" Value="1" meta:resourcekey="HoursResource1">
                                                                    </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblMustCompleteNoHours_RequestPermission" runat="server" Text="Must Complete No. Of Hours In Order To Request Extra Permissions"
                                                                    meta:resourcekey="lblMustCompleteNoHours_RequestPermissionResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButtonList ID="rblMustCompleteNoHours_RequestPermission" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                                                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18" Selected="True">
                                                                    </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div id="dvMustCompleteNoHours_RequestPermission" runat="server" visible="false">
                                                            <div class="row">
                                                                <div class="col-md-4">
                                                                    <asp:Label ID="lblNoOfHours_MustComplete" runat="server" Text="No. Of Hours"
                                                                        meta:resourcekey="lblNoOfHours_MustCompleteResource1"></asp:Label>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <telerik:RadMaskedTextBox ID="rmtNoOfHours_MustComplete" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                                                        DisplayMask="##:##" Text='0000' LabelCssClass="">
                                                                        <ClientEvents OnBlur="ValidateTextboxNoOfHours_MustComplete" />
                                                                    </telerik:RadMaskedTextBox>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-md-4"></div>
                                                                <div class="col-md-4">
                                                                    <asp:CheckBox ID="chkIncludeConsiderInWorkPermissions" runat="server" Text="Include (Consider Inside Work Hours) Permissions"
                                                                        meta:resourcekey="chkIncludeConsiderInWorkPermissionsResource1" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="fancy-collapse-panel">
                                        <div class="panel-group" id="accordion6" role="tablist" aria-multiselectable="true">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="headingOne6">
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion6" href="#collapseOne6" aria-expanded="true" aria-controls="collapseOne6">
                                                            <asp:Label ID="lblStudyPermissionsSettings" runat="server" Text="Study Permissions Settings" meta:resourcekey="lblStudyPermissionsSettingsResource1"></asp:Label>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="collapseOne6" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne6">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-md-2">
                                                                <asp:Label ID="lblStudyPermissionApproval" runat="server" CssClass="Profiletitletxt"
                                                                    Text="Study Permission Workflow Level(s)" meta:resourcekey="lblStudyPermissionApprovalResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-10">
                                                                <asp:RadioButtonList ID="rdlStudyPermissionApproval" runat="server" meta:resourcekey="rdlStudyPermissionApprovalResource1">
                                                                    <asp:ListItem Text="Direct Manager Only" Value="1" meta:resourcekey="rdbtnStudyDirectmgrResource1">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="Human Resource Only" Value="2" meta:resourcekey="rdbtnStudyHROnlyResource1">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="Both" Value="3" meta:resourcekey="rdbtnStudyBothResource1">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="DM,HR,GM" Value="4" meta:resourcekey="rdbtnStudyDMHRGMResource1">
                                                                    </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4"></div>
                                                            <div class="col-md-4">
                                                                <asp:CheckBox ID="chkStudyPerm_Exception" runat="server" Text="Notification Exception"
                                                                    ToolTip="By Checking This Option, Email and SMS Notifications Will Be Disabled For Employee Study Permission Request"
                                                                    meta:resourcekey="chkStudyPerm_ExceptionResource1" />
                                                            </div>
                                                        </div>
                                                        <div class="row" id="dvEnableUniversitySelection_StudyPermission" runat="server" visible="false">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblEnableUniversitySelection_StudyPermission" runat="server" Text="Enable Universtiy Selection"
                                                                    meta:resourcekey="lblEnableUniversitySelection_StudyPermissionResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButtonList ID="rblEnableUniversitySelection_StudyPermission" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Value="1" Text="Yes" meta:resourcekey="ListItemResource17"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="No" Selected="True" meta:resourcekey="ListItemResource18"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="dvEnableMajorSelection_StudyPermission" runat="server" visible="false">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblEnableMajorSelection_StudyPermission" runat="server" Text="Enable Major & Specialization Selection"
                                                                    meta:resourcekey="lblEnableMajorSelection_StudyPermissionResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButtonList ID="rblEnableMajorSelection_StudyPermission" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18" Selected="True">
                                                                    </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="row" id="dvEnableSemesterSelection_StudyPermission" runat="server" visible="false">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblEnableSemesterSelection_StudyPermission" runat="server" Text="Enable Semester Selection"
                                                                    meta:resourcekey="lblEnableSemesterSelection_StudyPermission"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButtonList ID="rblEnableSemesterSelection_StudyPermission" runat="server" RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18" Selected="True">
                                                                    </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblMaxStudyDuration" runat="server" CssClass="Profiletitletxt" Text="Maximum Duration of Study Permission"
                                                                    meta:resourcekey="lblMaxStudyDurationResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <telerik:RadNumericTextBox ID="txtMaxStudyDuration" MinValue="1" MaxValue="99999"
                                                                    Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="txtMaxStudyDurationResource1">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:Label ID="Label11" runat="server" CssClass="Profiletitletxt" Text="Minutes"
                                                                    meta:resourcekey="lblMinutesResource1" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblStudyAllowedAfterDays" runat="server" Text="Allowed After" meta:resourcekey="lblStudyAllowedAfterDaysResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <telerik:RadNumericTextBox ID="txtStudyAllowedAfterDays" MinValue="1" MaxValue="365"
                                                                    Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="txtStudyAllowedAfterDaysResource1">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:Label ID="Label15" runat="server" Text="Days" CssClass="Profiletitletxt" meta:resourcekey="lblDaysResource1" />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblStudyAllowedBeforeDays" runat="server" Text="Allowed Before" meta:resourcekey="lblStudyAllowedBeforeDaysResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <telerik:RadNumericTextBox ID="txtStudyAllowedBeforeDays" MinValue="1" MaxValue="365"
                                                                    Skin="Vista" runat="server" Culture="en-US" LabelCssClass="" meta:resourcekey="txtStudyAllowedBeforeDaysResource1">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:Label ID="Label16" runat="server" Text="Days" CssClass="Profiletitletxt" meta:resourcekey="lblDaysResource1" />
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblDefaultStudyPermissionTime" runat="server" CssClass="Profiletitletxt"
                                                                    Text="Default Study Permission Time" meta:resourcekey="lblDefaultStudyPermissionTimeResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:Label ID="lblFromTime" runat="server" CssClass="Profiletitletxt" Text="From"
                                                                    meta:resourcekey="lblFromTimeResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <telerik:RadMaskedTextBox ID="rmtFromTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                                                    DisplayMask="##:##" Text='0000' LabelCssClass="" meta:resourcekey="rmtFromTimeResource1">
                                                                    <ClientEvents OnBlur="ValidateTextboxFrom" />
                                                                </telerik:RadMaskedTextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:Label ID="lblToTime" runat="server" CssClass="Profiletitletxt" Text="To" meta:resourcekey="lblToTimeResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <telerik:RadMaskedTextBox ID="rmtToTime" runat="server" Mask="##:##" TextWithLiterals="00:00"
                                                                    DisplayMask="##:##" Text='0000' LabelCssClass="" meta:resourcekey="rmtToTimeResource1">
                                                                    <ClientEvents OnBlur="ValidateTextboxFrom" />
                                                                </telerik:RadMaskedTextBox>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblStudyPermissionFlexibleTime" runat="server" CssClass="Profiletitletxt"
                                                                    Text="Default Study Permission Flexible Time" meta:resourcekey="lblStudyPermissionFlexibleTimeResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <telerik:RadMaskedTextBox ID="rmtStudyPermissionFlexibleTime" runat="server" Mask="##:##"
                                                                    TextWithLiterals="00:00" DisplayMask="##:##" Text='0000' LabelCssClass="" meta:resourcekey="rmtStudyPermissionFlexibleTimeResource1">
                                                                    <ClientEvents OnBlur="ValidateTextboxFrom" />
                                                                </telerik:RadMaskedTextBox>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblHasFlexiblePermission" runat="server" Text="Has Flexible Study Permission"
                                                                    CssClass="Profiletitletxt" meta:resourcekey="lblHasFlexiblePermissionResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButtonList ID="rdlHasFlexiblePermission" runat="server" RepeatDirection="Horizontal"
                                                                    meta:resourcekey="rdlHasFlexiblePermissionResource1">
                                                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                                    </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblStudyGeneralGuide" runat="server" Text="General Guide for Study Permission"
                                                                    CssClass="Profiletitletxt" meta:resourcekey="lblStudyGeneralGuideResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtStudyGeneralGuide" runat="server" TextMode="MultiLine" />
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblStudyGeneralGuideAr" runat="server" Text="Arabic General Guide for Study Permission"
                                                                    CssClass="Profiletitletxt" meta:resourcekey="lblStudyGeneralGuideArResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtStudyGeneralGuideAr" runat="server" TextMode="MultiLine" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="fancy-collapse-panel">
                                        <div class="panel-group" id="accordion7" role="tablist" aria-multiselectable="true">
                                            <div class="panel panel-default">
                                                <div class="panel-heading" role="tab" id="headingOne7">
                                                    <h4 class="panel-title">
                                                        <a data-toggle="collapse" data-parent="#accordion7" href="#collapseOne7" aria-expanded="true" aria-controls="collapseOne7">
                                                            <asp:Label ID="lblNursingPermissionsSettings" runat="server" Text="Nursing Permissions Settings" meta:resourcekey="lblNursingPermissionsSettingsResource1"></asp:Label>
                                                        </a>
                                                    </h4>
                                                </div>
                                                <div id="collapseOne7" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne7">
                                                    <div class="panel-body">
                                                        <div class="row">
                                                            <div class="col-md-2">
                                                                <asp:Label ID="lblNursingPermissionApproval" runat="server" CssClass="Profiletitletxt"
                                                                    Text="Nursing Permission Workflow Level(s)" meta:resourcekey="lblNursingPermissionApprovalResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-10">
                                                                <asp:RadioButtonList ID="rdlNursingPermissionApproval" runat="server" meta:resourcekey="rdlNursingPermissionApprovalResource1">
                                                                    <asp:ListItem Text="Direct Manager Only" Value="1" meta:resourcekey="rdbtnNursingDirectmgrResource1">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="Human Resource Only" Value="2" meta:resourcekey="rdbtnNursingHROnlyResource1">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="Both" Value="3" meta:resourcekey="rdbtnNursingBothResource1">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="DM,HR,GM" Value="4" meta:resourcekey="rdbtnNursingDMHRGMResource1">
                                                                    </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4"></div>
                                                            <div class="col-md-4">
                                                                <asp:CheckBox ID="chkNursingPerm_Exception" runat="server" Text="Notification Exception"
                                                                    ToolTip="By Checking This Option, Email and SMS Notifications Will Be Disabled For Employee Nursing Permission Request"
                                                                    meta:resourcekey="chkNursingPerm_ExceptionResource1" />
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblNursingDays" runat="server" Text="Nursing Days" CssClass="Profiletitletxt"
                                                                    meta:resourcekey="lblNursingDaysResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <telerik:RadNumericTextBox ID="txtNursingDays" MinValue="0" MaxValue="99999" Skin="Vista"
                                                                    runat="server" Culture="en-US" LabelCssClass="" Width="190px" meta:resourcekey="txtNursingDaysResource1">
                                                                    <NumberFormat DecimalDigits="0" GroupSeparator="" />
                                                                </telerik:RadNumericTextBox>
                                                            </div>
                                                            <div class="col-md-1">
                                                                <asp:Label ID="lblDays" runat="server" Text="Days" CssClass="Profiletitletxt" meta:resourcekey="lblDaysResource1" />
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblNursingPermAttend" runat="server" Text="Self Service Nursing Permission Allowed When Employee Attend To Work"
                                                                    CssClass="Profiletitletxt" meta:resourcekey="lblNursingPermAttendResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:CheckBox Text="&nbsp;" ID="chkNursingPermAttend" runat="server" />
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblHasFlexibleNursingPermission" runat="server" Text="Has Flexible Nursing Permission"
                                                                    CssClass="Profiletitletxt" meta:resourcekey="lblHasFlexibleNursingPermissionResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButtonList ID="rdlHasFlexibleNursingPermission" runat="server" RepeatDirection="Horizontal"
                                                                    meta:resourcekey="rdlHasFlexibleNursingPermissionResource1">
                                                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                                    </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblAllowNursingPermissionInRamadan" runat="server" Text="Allow Nursing Permission In Rmadan"
                                                                    ToolTip="Allow Apply for Nursing Permission in The Month of Ramadan"
                                                                    CssClass="Profiletitletxt" meta:resourcekey="lblAllowNursingPermissionInRamadanResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButtonList ID="rdlAllowNursingPermissionInRamadan" runat="server" RepeatDirection="Horizontal"
                                                                    meta:resourcekey="rdlAllowNursingPermissionInRamadanResource1">
                                                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                                    </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblConsiderNursingInRamadan" runat="server" Text="Consider Nursing Permission In Ramadan"
                                                                    ToolTip="To Consider Nursing Permission During The Month of Ramadan"
                                                                    meta:resourcekey="lblConsiderNursingInRamadanResource1"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:RadioButtonList ID="rblConsiderNursingInRamadan" runat="server" RepeatDirection="Horizontal"
                                                                    meta:resourcekey="rblConsiderNursingInRamadanResource1">
                                                                    <asp:ListItem Text="Yes" Value="1" meta:resourcekey="ListItemResource17" Selected="True">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="2" meta:resourcekey="ListItemResource18">
                                                                    </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblNursingGeneralGuide" runat="server" Text="General Guide for Nursing Permission"
                                                                    CssClass="Profiletitletxt" meta:resourcekey="lblNursingGeneralGuideResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtNursingGeneralGuide" runat="server" TextMode="MultiLine" />
                                                            </div>
                                                        </div>
                                                        <hr />
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <asp:Label ID="lblNursingGeneralGuideAr" runat="server" Text="Arabic General Guide for Nursing Permission"
                                                                    CssClass="Profiletitletxt" meta:resourcekey="lblNursingGeneralGuideArResource1" />
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtNursingGeneralGuideAr" runat="server" TextMode="MultiLine" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="tabDomainUsers" runat="server" HeaderText="Domain Users Settings"
                        TabIndex="2" meta:resourcekey="tabDomainUsers" Visible="false">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="upImportUsers" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblDomainName" runat="server" CssClass="Profiletitletxt" meta:resourceKey="lblDomainNameResource1"
                                                Text="Domain Name"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtDomainName" runat="server" meta:resourcekey="txtDomainNameResource1"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label runat="server" ID="LablblSampleDomain" Text="(Example :  smartai.ae)" meta:resourcekey="LablblSampleDomainResource1"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblDomainUserName" runat="server" CssClass="Profiletitletxt" meta:resourceKey="lblDomainUserNameResource1"
                                                Text="Domain User Name"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtDomainUserName" runat="server" meta:resourcekey="txtDomainUserNameResource1"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label runat="server" ID="lblSampleDomainUser" Text="(Example :  smartv\USERNAME)"
                                                meta:resourcekey="lblSampleDomainUserResource1"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblDomainPass" runat="server" CssClass="Profiletitletxt" meta:resourceKey="lblDomainPassResource1"
                                                Text="Domain Password"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:TextBox ID="txtDomainPassword" runat="server" TextMode="Password" meta:resourcekey="txtDomainPasswordResource1"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label ID="lblDefaultPrivGroup" runat="server" CssClass="Profiletitletxt" meta:resourceKey="lblDefaultPrivGroupResource1"
                                                Text="Default Group"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <telerik:RadComboBox ID="ddlUsersGroup" runat="server" MarkFirstMatch="True" meta:resourceKey="RadWeeksDayResource1"
                                                Skin="Vista" ToolTip="View Week Days">
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <asp:Button ID="btnImport" runat="server" CssClass="button" meta:resourcekey="btnImportResource1"
                                                Text="Import" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upImportUsers">
                                <ProgressTemplate>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <img src="../images/loading.gif" style="height: 50px" />
                                        </div>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <center id="UpdatePanel1" runat="server">
        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="ReligionGroup"
            CssClass="button" meta:resourcekey="btnSaveResource1" />
    </center>
</asp:Content>
