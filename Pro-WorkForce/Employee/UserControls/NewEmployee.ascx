<%@ Control Language="VB" AutoEventWireup="false" CodeFile="NewEmployee.ascx.vb"
    Inherits="NewEmployee_WebUserControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<script type="text/javascript">
    function viewPolicyDetails(tmp) {

        var PolicyId = $find('<%= RadCmbPolicies.ClientID %>')._value;
        if (PolicyId != -1)
            oWindow = radopen('../Admin/TAPolicyPopup.aspx?ID=' + PolicyId, "RadWindow1");
        return false;

    }

    function viewOverTimeRule(tmp) {

        var RuleId = $find('<%= RadCmbOvertime.ClientID %>')._value;
        if (RuleId != -1)
            oWindow = radopen('../Admin/OvertimeRulesPopUp.aspx?RuleId=' + RuleId, "RadWindow1");
        return false;

    }

    function ChangePage(URL, MSG) {
        ShowMessage(MSG);
        window.location = URL;
    }
</script>
<asp:Panel runat="server" ID="wizerdRTLStyle" Visible="false">
    <style>
        #wizHeader li {
            float: right;
        }

            #wizHeader li .currentStep:after {
                border-right-color: #04365b !important;
            }

            #wizHeader li:first-child a {
                padding-right: 10px;
            }

            #wizHeader li a {
                color: #333;
                text-decoration: none;
                padding: 10px 50px 10px 0px;
                background: brown;
                background: hsla(34,85%,35%,1);
                position: relative;
                display: block;
                float: left;
                font-size: 13px;
            }

                #wizHeader li a:before {
                    content: " ";
                    display: block;
                    width: 0;
                    height: 0;
                    border-top: 50px solid transparent;
                    border-bottom: 50px solid transparent;
                    border-right: 30px solid white;
                    position: absolute;
                    top: 50%;
                    margin-top: -50px;
                    margin-left: 0px;
                    right: 100%;
                    z-index: 1;
                    border-left: none;
                }

                #wizHeader li a:after {
                    content: " ";
                    display: block;
                    width: 0;
                    height: 0;
                    border-top: 50px solid transparent;
                    border-bottom: 50px solid transparent;
                    border-right: 30px solid hsl(0, 0%, 100%);
                    position: absolute;
                    top: 50%;
                    margin-top: -50px;
                    right: 100%;
                    z-index: 2;
                    border-left: none;
                }
    </style>
</asp:Panel>

<telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Default"
    EnableShadow="True" InitialBehavior="None" Style="z-index: 8000;" Modal="true">
    <Windows>
        <telerik:RadWindow ID="RadWindow1" runat="server" Animation="FlyIn" Behavior="Close, Move"
            Behaviors="Close, Move" EnableShadow="True" Height="600px" IconUrl="~/images/HeaderWhiteChrome.jpg"
            InitialBehavior="None" ShowContentDuringLoad="False" VisibleStatusbar="False"
            Width="100%" Skin="Vista">
        </telerik:RadWindow>
    </Windows>
</telerik:RadWindowManager>
<%--<asp:UpdatePanel ID="upNE" runat="server">
    <ContentTemplate>--%>
<div class="row">
    <div class="col-md-12">
        <asp:Wizard ID="Employee" runat="server" StartNextButtonType="Button" StartNextButtonText="Next"
            OnNextButtonClick="Employee_NextButtonClick" StartNextButtonStyle-CssClass="button"
            StepNextButtonText="Next" StepNextButtonStyle-CssClass="button" StepPreviousButtonType="Button"
            StepPreviousButtonStyle-CssClass="button" StepPreviousButtonText="Previous" FinishPreviousButtonType="Button"
            FinishPreviousButtonStyle-CssClass="button" FinishPreviousButtonText="Previous"
            FinishCompleteButtonType="Button" FinishCompleteButtonStyle-CssClass="button"
            FinishCompleteButtonText="Finish" DisplaySideBar="false" ActiveStepIndex="0"
            meta:resourcekey="EmployeeResource1">
            <WizardSteps>
                <asp:WizardStep ID="WizardStep1" runat="server" Title="Employee Info" StepType="Start"
                    meta:resourcekey="WizardStep1Resource1">
                    <asp:UpdatePanel ID="upUpload" runat="server">
                        <%--                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnUpload1" />
                                    </Triggers>--%>
                        <ContentTemplate>
                            <%--<tr>
                                                <td>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                    <asp:Button ID="btnUpload1" runat="server" Text="Upload" OnClick="UploadImg" />
                                                </td>
                                                <td>
                                                    <asp:Image ID="EmpImg" runat="server" BorderStyle="Solid" />
                                                </td>
                                            </tr>--%>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblEmployeeType" runat="server" Text="Employee Type"
                                        meta:resourcekey="lblEmployeeTypeResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="radEmployeeType" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="true" MarkFirstMatch="True" Skin="Vista" meta:resourcekey="radEmployeeTypeResource1">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="--Please Select--" Value="0" meta:resourcekey="RadComboBoxItemResource3" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="rfvradEmployeeType" runat="server" ControlToValidate="radEmployeeType"
                                        Display="None" ErrorMessage="Please Select Employee Type" InitialValue="--Please Select--"
                                        ValidationGroup="gpNext" meta:resourcekey="rfvradEmployeeTypeResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceradEmployeeType" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvradEmployeeType" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row" id="trContractEndDate" runat="server" visible="false">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblContractEndDate" runat="server" Text="Expiry date"
                                        meta:resourcekey="lblContractEndDateResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDatePicker ID="rdpContractEndDate" runat="server" AllowCustomText="false"
                                        Culture="English (United States)" MarkFirstMatch="true" PopupDirection="TopRight"
                                        ShowPopupOnFocus="True" Skin="Vista" AutoPostBack="true" meta:resourcekey="rdpContractEndDateResource1">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                            Width="">
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="rfvContractEndDate" runat="server" ControlToValidate="rdpContractEndDate"
                                        ValidationGroup="gpNext" Display="None" ErrorMessage="Please Enter Contract End Date"
                                        Enabled="false" meta:resourcekey="rfvContractEndDateResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vceContractEndDate" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvContractEndDate" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row" id="trExternalParty" runat="server" visible="false">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblExternalPartyName" runat="server" Text="External Party Name"
                                        meta:resourcekey="lblExternalPartyNameResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtExternalPartyName" runat="server" meta:resourcekey="txtExternalPartyNameResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtExternalPartyName" runat="server" ControlToValidate="txtExternalPartyName"
                                        ValidationGroup="gpNext" Display="None" ErrorMessage="Please Enter External Party Name"
                                        Enabled="false" meta:resourcekey="rfvtxtExternalPartyNameResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="vcetxtExternalPartyName" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvtxtExternalPartyName" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblNumber" runat="server" Text="Employee Number"
                                        meta:resourcekey="lblNumberResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtEmployeeNumber" runat="server" meta:resourcekey="txtEmployeeNumberResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqEmployeeNumber" runat="server" ControlToValidate="txtEmployeeNumber"
                                        Display="None" ErrorMessage="Please enter employee number" ValidationGroup="gpNext"
                                        meta:resourcekey="reqEmployeeNumberResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderEmployeeNo" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqEmployeeNumber" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblPayRollNumber" runat="server" Text="PayRoll Number"
                                        meta:resourcekey="lblPayRollNumberResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtPayRollNumber" runat="server" meta:resourcekey="txtPayRollNumberResource1"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblCardNumber" runat="server" Text="Employee Card Number"
                                        meta:resourcekey="lblCardNumberResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <%--<telerik:RadNumericTextBox ID="txtCardNumber" runat="server" MinValue="1" Culture="English (United States)"
                                                LabelCssClass="">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" />
                                            </telerik:RadNumericTextBox>--%>
                                    <asp:TextBox ID="txtCardNumber" runat="server" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCardNumber" runat="server" ControlToValidate="txtCardNumber"
                                        Display="None" ErrorMessage="Please enter employee number" ValidationGroup="gpNext"
                                        meta:resourcekey="reqEmployeeNumberResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqEmployeeNumber" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                                <div class="col-md-2">
                                    <div id="dvCardNoExists" runat="server" visible="false">
                                        <asp:Label ID="lblCardNoExists" runat="server"
                                            Text="Card No. Exists!, Please Insert another Number"
                                            meta:resourcekey="lblCardNoExistsResource1" Font-Size="6pt" ForeColor="red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblEnglishName" runat="server" Text="English Name"
                                        meta:resourcekey="lblEnglishNameResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtEnglishName" runat="server" meta:resourcekey="txtEnglishNameResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqEnglishName" runat="server" ControlToValidate="txtEnglishName"
                                        Display="None" ErrorMessage="Please enter english name" ValidationGroup="gpNext"
                                        meta:resourcekey="reqEnglishNameResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqEnglishName" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqEnglishName" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblArabicName" runat="server" Text="Arabic Name"
                                        meta:resourcekey="lblArabicNameResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtArabicName" runat="server" meta:resourcekey="txtArabicNameResource1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqArabicName" runat="server" ControlToValidate="txtArabicName"
                                        Display="None" ErrorMessage="Please enter arabic name" ValidationGroup="gpNext"
                                        meta:resourcekey="reqArabicNameResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqArabicName" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row" id="trGTReaders" runat="server">
                                <div class="col-md-2">
                                    <asp:Label ID="lblGTReader" runat="server" Text="GT Reader Values" meta:resourcekey="lblGTReaderResource1"
                                        CssClass="Profiletitletxt" />
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="radCmbGTReaders" runat="server" Skin="Vista"
                                        AutoPostBack="true" MarkFirstMatch="true">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblPinNo" runat="server" Text="PIN No." meta:resourcekey="lblPinNoResource1"
                                        CssClass="Profiletitletxt" Visible="false" />
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtPinNo" runat="server" meta:resourcekey="txtPinNoResource1"
                                        Visible="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblOrgCompany" runat="server" Text="Company"
                                        meta:resourcekey="lblOrgCompanyResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbOrgCompany" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" MarkFirstMatch="True" OnSelectedIndexChanged="RadCmbOrgCompany_SelectedIndexChanged"
                                        Skin="Vista" meta:resourcekey="RadCmbOrgCompanyResource1">
                                        <%-- <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="--Please Select--" meta:resourcekey="RadComboBoxItemResource1" />
                                                </Items>--%>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="reqOrgCompany" runat="server" ControlToValidate="RadCmbOrgCompany"
                                        Display="None" ErrorMessage="Please select company" InitialValue="--Please Select--"
                                        ValidationGroup="gpNext" meta:resourcekey="reqOrgCompanyResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqOrgCompany" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqOrgCompany" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblEntity" runat="server" Text="Entity"
                                        meta:resourcekey="lblEntityResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbEntity" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="True" EnableScreenBoundaryDetection="false" MarkFirstMatch="True" Skin="Vista" meta:resourcekey="RadCmbEntityResource1">
                                        <%--  <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="--Please Select--" meta:resourcekey="RadComboBoxItemResource2" />
                                                </Items>--%>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="reqEmpEntity" runat="server" ControlToValidate="RadCmbEntity"
                                        Display="None" ErrorMessage="Please select entity name" InitialValue="--Please Select--"
                                        ValidationGroup="gpNext" meta:resourcekey="reqEmpEntityResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqEmpEntity" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqEmpEntity" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblWorkLocation" runat="server" Text="Work Location"
                                        meta:resourcekey="lblWorkLocationResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbWorkLocation" runat="server" MarkFirstMatch="True"
                                        AutoPostBack="True" EnableScreenBoundaryDetection="false" Skin="Vista" meta:resourcekey="RadCmbWorkLocationResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblGrade" runat="server" Text="Grade" meta:resourcekey="lblGradeResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbGrade" runat="server" EnableScreenBoundaryDetection="false" MarkFirstMatch="True" Skin="Vista"
                                        AutoPostBack="True" meta:resourcekey="RadCmbGradeResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblDesignation" runat="server" Text="Designation"
                                        meta:resourcekey="lblDesignationResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbDesignation" runat="server" EnableScreenBoundaryDetection="false" MarkFirstMatch="True"
                                        AutoPostBack="True" Skin="Vista" meta:resourcekey="RadCmbDesignationResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblLogicalGroup" runat="server" Text="Logical Group"
                                        meta:resourcekey="lblLogicalGroupResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbLogicalGroup" runat="server" EnableScreenBoundaryDetection="false" MarkFirstMatch="True"
                                        AutoPostBack="True" Skin="Vista" meta:resourcekey="RadCmbLogicalGroupResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblGender" runat="server" Text="Gender"
                                        meta:resourcekey="lblGenderResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:RadioButton ID="RadioBtnMale" runat="server" Checked="True" GroupName="EmpGender"
                                        Text="Male" meta:resourcekey="RadioBtnMaleResource1" />
                                    <asp:RadioButton ID="RadioBtnFemale" runat="server" GroupName="EmpGender" Text="Female"
                                        meta:resourcekey="RadioBtnFemaleResource1" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblDob" runat="server" Text="Birth Date"
                                        meta:resourcekey="lblDobResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDatePicker ID="dtpBirthDate" runat="server" DateInput-MinDate="01/01/1920 12:00:00 AM"
                                        Culture="English (United States)" AllowCustomText="false" MarkFirstMatch="true"
                                        PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpBirthDateResource1"
                                        AutoPostBack="true">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                            Width="">
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblAge" runat="server" Text="Age" meta:resourcekey="lblAgeResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtAge" runat="server" meta:resourcekey="txtAgeResource1" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvAge" runat="server" ControlToValidate="txtAge"
                                        Display="None" ErrorMessage="Please enter age" ValidationGroup="EmployeeGroup"
                                        meta:resourcekey="rfvAgeResource1" />
                                    <cc1:ValidatorCalloutExtender ID="vceAge" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvAge" WarningIconImageUrl="~/images/warning1.png" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblEmailAddress" runat="server" Text="E-mail Address"
                                        meta:resourcekey="lblEmailAddressResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtEmailAddress" runat="server" meta:resourcekey="txtEmailAddressResource1"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailAddress"
                                        Display="None" ErrorMessage="invalid e-mail address format" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="gpNext" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="RegularExpressionValidator1" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblMobile" runat="server" Text="Mobile No."
                                        meta:resourcekey="lblMobileResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtMobileNo" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblNationality" runat="server" Text="Nationality"
                                        meta:resourcekey="lblNationalityResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbNationality" EnableScreenBoundaryDetection="false" runat="server" MarkFirstMatch="True"
                                        Skin="Vista" meta:resourcekey="RadCmbNationalityResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblNationalId" runat="server" Text="National Id No."
                                        meta:resourcekey="lblNationalIdResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtNationalId" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblReligion" runat="server" Text="Religion"
                                        meta:resourcekey="lblReligionResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbReligion" EnableScreenBoundaryDetection="false" runat="server" MarkFirstMatch="True" Skin="Vista"
                                        meta:resourcekey="RadCmbReligionResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblMaritalStatus" runat="server" Text="Marital Status"
                                        meta:resourcekey="lblMaritalStatusResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbMaritalStatus" EnableScreenBoundaryDetection="false" runat="server" MarkFirstMatch="True"
                                        Skin="Vista" meta:resourcekey="RadCmbMaritalStatusResource1">
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblJoinDate" runat="server" Text="Join Date"
                                        meta:resourcekey="lblJoinDateResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDatePicker ID="dtpJoinDate" runat="server" AllowCustomText="false" Culture="English (United States)"
                                        MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                                        AutoPostBack="true" meta:resourcekey="dtpJoinDateResource1" MinDate="1/1/1950 12:00:00 AM">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                            Width="">
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="rfvJoinDate" runat="server" ControlToValidate="dtpJoinDate"
                                        ValidationGroup="gpNext" Display="None" ErrorMessage="Please Enter Join Date"
                                        meta:resourcekey="rfvJoinDateResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="rfvJoinDate" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblStatus" runat="server" Text="Status"
                                        meta:resourcekey="lblStatusResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadComboBox ID="RadCmbStatus" runat="server" AppendDataBoundItems="True"
                                        AutoPostBack="true" MarkFirstMatch="True" Skin="Vista" meta:resourcekey="RadCmbStatusResource1">
                                        <%-- <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="--Please Select--" Value="0" meta:resourcekey="RadComboBoxItemResource3" />
                                                </Items>--%>
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="reqEmpStatus" runat="server" ControlToValidate="RadCmbStatus"
                                        Display="None" ErrorMessage="Please select status name" InitialValue="--Please Select--"
                                        ValidationGroup="gpNext" meta:resourcekey="reqEmpStatusResource1"></asp:RequiredFieldValidator>
                                    <cc1:ValidatorCalloutExtender ID="ExtenderreqEmpStatus" runat="server" CssClass="AISCustomCalloutStyle"
                                        Enabled="True" TargetControlID="reqEmpStatus" WarningIconImageUrl="~/images/warning1.png">
                                    </cc1:ValidatorCalloutExtender>
                                </div>
                            </div>
                            <div class="row" id="Termdate" runat="server" visible="false">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblTerminationDate" runat="server" Text="Terminiation Date"
                                        meta:resourcekey="lblTerminationDateResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <telerik:RadDatePicker ID="dtpTerminationDate" runat="server" AllowCustomText="false"
                                        Culture="English (United States)" MarkFirstMatch="true" PopupDirection="TopRight"
                                        ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpJoinDateResource1">
                                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                            Width="">
                                        </DateInput>
                                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                    </telerik:RadDatePicker>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label CssClass="Profiletitletxt" ID="lblRemarks" runat="server" Text="Remarks"
                                        meta:resourcekey="lblRemarksResource1"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep2" runat="server" Title="TA Policy" StepType="Step"
                    meta:resourcekey="WizardStep2Resource1">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblTaPolicy" runat="server" Text="TA Policy"
                                meta:resourcekey="lblTaPolicyResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="RadCmbPolicies" MarkFirstMatch="True" Skin="Vista" runat="server"
                                meta:resourcekey="RadCmbPoliciesResource1">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadCmbPolicies"
                                Display="None" ErrorMessage="Please select TA policy" InitialValue="--Please Select--"
                                ValidationGroup="gpNext" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" CssClass="AISCustomCalloutStyle"
                                Enabled="True" TargetControlID="RequiredFieldValidator3" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                            <a href="#" onclick="viewPolicyDetails(1)">
                                <asp:Literal ID="Literal1" runat="server" Text="View Details" meta:resourcekey="Literal1Resource1"></asp:Literal></a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lStartDate" runat="server" Text="Start date"
                                meta:resourcekey="lStartDateResource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="dtpStartDate" runat="server" AllowCustomText="false" ShowPopupOnFocus="True"
                                MarkFirstMatch="true" PopupDirection="TopRight" Skin="Vista" Culture="English (United States)"
                                meta:resourcekey="dtpStartDateResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                    Width="" /><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtpStartDate"
                                Display="None" ErrorMessage="Please enter start date" ValidationGroup="gpNext"
                                meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="AISCustomCalloutStyle"
                                Enabled="True" TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chckTemporary" runat="server" AutoPostBack="True" Text="Temporary"
                                meta:resourcekey="lblTemporaryResource1" />
                        </div>
                    </div>
                    <asp:Panel ID="pnlEndDate" runat="server" Visible="False" meta:resourcekey="pnlEndDateResource1">
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="lblEndDate" runat="server" Text="End date"
                                    meta:resourcekey="lblEndDateResource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpEndDate" runat="server" AllowCustomText="false" Culture="English (United States)"
                                    MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                                    meta:resourcekey="dtpEndDateResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                        Width="">
                                    </DateInput>
                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                                <asp:CompareValidator ID="Comparevalidator2" runat="server" ControlToValidate="dtpEndDate"
                                    ControlToCompare="dtpStartDate" Display="None" Operator="GreaterThanEqual" ErrorMessage="End date should be greater than start date"
                                    meta:resourcekey="Comparevalidator2Resource1"></asp:CompareValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="Comparevalidator2" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                    </asp:Panel>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep3" runat="server" Title="Overtime Rules" StepType="Step"
                    meta:resourcekey="WizardStep3Resource1">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="Label1" runat="server" Text="Over Time Rule"
                                meta:resourcekey="Label1Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadComboBox ID="RadCmbOvertime" MarkFirstMatch="True" Skin="Vista" runat="server"
                                meta:resourcekey="RadCmbOvertimeResource1">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="RadCmbOvertime"
                                Display="None" ErrorMessage="Please select Overtime rule" InitialValue="--Please Select--"
                                meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" CssClass="AISCustomCalloutStyle"
                                Enabled="True" TargetControlID="RequiredFieldValidator4" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                            <a href="#" onclick="viewOverTimeRule(1)">
                                <asp:Literal ID="Literal2" runat="server" Text="Select All" meta:resourcekey="Literal2Resource1"></asp:Literal></a>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="Label2" runat="server" Text="Start date"
                                meta:resourcekey="Label2Resource1"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <telerik:RadDatePicker ID="dtpOTStartDate" runat="server" AllowCustomText="false"
                                ShowPopupOnFocus="True" MarkFirstMatch="true" PopupDirection="TopRight" Skin="Vista"
                                Culture="English (United States)" meta:resourcekey="dtpOTStartDateResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                    Width="" /><DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="dtpOTStartDate"
                                Display="None" ErrorMessage="Please enter start date" ValidationGroup="gpNext"
                                meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="AISCustomCalloutStyle"
                                Enabled="True" TargetControlID="RequiredFieldValidator2" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chckOvrTemporary" runat="server" AutoPostBack="True" Text="Temporary"
                                meta:resourcekey="Label3Resource1" />
                        </div>
                    </div>
                    <asp:Panel ID="PnlOTEnddate" runat="server" Visible="false" meta:resourcekey="PnlOTEnddateResource1">
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="Label4" runat="server" Text="End date"
                                    meta:resourcekey="Label4Resource1"></asp:Label>
                            </div>
                            <div class="col-md-4">
                                <telerik:RadDatePicker ID="dtpOTEndDate" runat="server" AllowCustomText="false" Culture="English (United States)"
                                    MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                                    meta:resourcekey="dtpOTEndDateResource1">
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/M/yyyy" DisplayDateFormat="dd/M/yyyy" LabelCssClass=""
                                        Width="">
                                    </DateInput>
                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                                <asp:CompareValidator ID="Comparevalidator1" runat="server" ControlToValidate="dtpOTEndDate"
                                    ControlToCompare="dtpOTStartDate" Display="None" Operator="GreaterThanEqual"
                                    ErrorMessage="To date should be greater than From date" meta:resourcekey="Comparevalidator1Resource1"></asp:CompareValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CssClass="AISCustomCalloutStyle"
                                    Enabled="True" TargetControlID="Comparevalidator1" WarningIconImageUrl="~/images/warning1.png">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                        </div>
                    </asp:Panel>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep4" runat="server" Title="Finish" StepType="Finish"
                    meta:resourcekey="WizardStep4Resource1">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblEmpimg" runat="server" Text="Employee Image"
                                meta:resourcekey="lblEmpimgResource1"></asp:Label>
                        </div>
                        <div class="col-md-10">
                            <div id="trAttachedFile" runat="server" class="col-md-4">
                                <div class="input-group">
                                    <span class="input-group-btn"><span class="btn btn-default" onclick="$(this).parent().find('input[type=file]').click();">Browse</span>
                                        <asp:FileUpload ID="FileUpload1" runat="server" meta:resourcekey="fuAttachFileResource1"
                                            name="uploaded_file" onchange="$(this).parent().parent().find('.form-control').html($(this).val().split(/[\\|/]/).pop());"
                                            Style="display: none;" type="file" />
                                    </span><span class="form-control"></span>
                                </div>
                            </div>
                        </div>
                        <%-- <asp:Image ID="Image1" runat="server" BorderStyle="Solid" />--%>
                    </div>
                    <div class="row">
                        <div class="col-md-12 text-center">
                            <asp:Label CssClass="Profiletitletxt" ID="lblSubmitApplication" runat="server" Text=""
                                Font-Bold="true" meta:resourcekey="lblSubmitApplicationResource1"></asp:Label>
                        </div>
                    </div>
                </asp:WizardStep>
            </WizardSteps>
            <HeaderTemplate>
                <ul id="wizHeader" style="border: 1px solid #ccc; margin: 10px 0;">
                    <asp:Repeater runat="server" ID="SideBarList" OnItemCommand="SideBarList_ItemCommand">
                        <ItemTemplate>
                            <li>
                                <%--<a class="<%# GetClassForWizardStep(Container.DataItem) %>" title="<%#Eval("Name")%>">
                                <%# Eval("Name")%></a>--%>
                                <asp:LinkButton ValidationGroup="gpNext" ID="LinkButton1" runat="server" CssClass="<%# GetClassForWizardStep(Container.DataItem) %>">
                                    <%# Eval("Name")%>
                                </asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </HeaderTemplate>
            <%--  <NavigationStyle VerticalAlign="Top" CssClass="Wizardpanel" />--%>
            <%--            <StartNextButtonStyle CssClass="button"></StartNextButtonStyle>
            <StepNextButtonStyle CssClass="button"></StepNextButtonStyle>
            <StepPreviousButtonStyle CssClass="button"></StepPreviousButtonStyle>
            <SideBarButtonStyle CssClass="WizardSideBarButton" Width="170px" Font-Size="Small" />
            <SideBarStyle CssClass="WizardSidebar" />
            <FinishCompleteButtonStyle CssClass="button"></FinishCompleteButtonStyle>
            <FinishPreviousButtonStyle CssClass="button"></FinishPreviousButtonStyle>
            <HeaderStyle VerticalAlign="Top" CssClass="WizardHeader" />
            <StepStyle VerticalAlign="Top" CssClass="WizardStep" Height="370px" />--%>
            <%--<StartNavigationTemplate>
                <asp:Button ID="btnNextDummy" runat="server" Visible="false" />
            </StartNavigationTemplate>
            <StepNavigationTemplate>
                <asp:Button ID="btnNextDummy1" runat="server" Visible="false" />
            </StepNavigationTemplate>
            <FinishNavigationTemplate>
                <asp:Button ID="btnNextDummy2" runat="server" Visible="false" />
            </FinishNavigationTemplate>--%>
        </asp:Wizard>
    </div>
</div>
<%--    </ContentTemplate>
</asp:UpdatePanel>
--%>