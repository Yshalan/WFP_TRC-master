<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DefineUsers.ascx.vb"
    Inherits="UserColntrols_DefineUsers" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../../Admin/UserControls/EmployeeFilter.ascx" TagName="PageFilter"
    TagPrefix="uc2" %>
<script language="javascript" type="text/javascript">

    var IsCompanyVisible = false;
    var IsEntityVisible = false;
    //    function Pass() {
    //        //alert('saed');    
    //        // document.getElementById('<%=txtPassword.ClientID%>').value ="1111111111"
    //        //document.getElementById('<%=txtConfirm.ClientID%>').value="1111111111"
    //    }

    //    function ValidateSecurityLevel() {
    //        var RowCompanies = document.getElementById('<%= RowCompanies.ClientID %>');
    //        var RowEntities = document.getElementById('<%= RowEntities.ClientID %>');

    //        if (RowCompanies.style.visibility) {
    //            if (RowCompanies.style.visibility == "visible") {
    //                IsCompanyVisible = true;
    //            }
    //        }

    //        if (RowEntities.style.visibility) {
    //            if (RowEntities.style.visibility == "visible") {
    //                IsEntityVisible = true;
    //            }
    //        }

    //        if (Page_ClientValidate('VGUsers')) {
    //            if (IsCompanyVisible) {
    //                if (Page_ClientValidate('VGCompanies')) {
    //                    if (IsEntityVisible) {
    //                        if (Page_ClientValidate('VGEntities')) {
    //                            return true;
    //                        }
    //                        else {
    //                            return false;
    //                        }
    //                    }
    //                    return true;
    //                }
    //                else {
    //                    return false;
    //                }
    //            }
    //        }
    //    }

    //    function ValidateSearch() {
    //        var passwd = document.getElementById('<%=txtPassword.ClientID%>').value
    //        var confpasswd = document.getElementById('<%=txtConfirm.ClientID%>').value

    //        if (passwd == "") {
    //            alert("الرجاء ادخال كلمة المرور")
    //            return false
    //        }
    //        else if (confpasswd == "") {
    //            alert("الرجاء ادخال كلمة مرور مساوية")
    //            return false
    //        }

    //    }
</script>
<script language="javascript" type="text/javascript">
    function confirmDelete(gvUsers) {
        var lang = '<%= MsgLang %>'
        var TargetBaseControl = null;
        try {
            //get target base control.
            TargetBaseControl = document.getElementById(gvUsers);

        }
        catch (err) {
            TargetBaseControl = null;
        }

        if (TargetBaseControl == null) {
            if (lang == 'en') {
                ShowMessage('No data')
            }
            else {
                ShowMessage('لا يوجد بيانات')
            }
            return false;
        }

        //get all the control of the type INPUT in the base control.
        var Inputs = TargetBaseControl.getElementsByTagName("input");
        var TargetChildControl = "chk";
        for (var n = 0; n < Inputs.length; ++n) {
            if (Inputs[n].type == 'checkbox' && Inputs[n].checked && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0) {
                if (lang == 'en') {
                    return confirm('Are you sure you want to delete?');
                }
                else {
                    return confirm('هل أنت متأكد من الحذف؟');
                }
            }

        }
        if (lang == 'en') {
            alert('Please select from the list');
        }
        else {
            alert('الرجاء الاختيار من القائمة');
        }
        return false;
    }

    function ResetPopUp() {
        var txtOldPassowrd = document.getElementById("<%= txtOldPassowrd.ClientID %>");
        txtOldPassowrd.value = '';
        var txtNewPassowrd = document.getElementById("<%= txtNewPassowrd.ClientID %>");
        txtNewPassowrd.value = '';
        var txtConfirmPassowrd = document.getElementById("<%= txtConfirmPassowrd.ClientID %>");
        txtConfirmPassowrd.value = '';
    }

    $(document).ready(function () {


        var pwdoption = '<%=PasswordType%>';
        var myInput = document.getElementById("ctl00_ContentPlaceHolder1_DefineUsers_txtPassword");
        var letter = document.getElementById("ctl00_ContentPlaceHolder1_DefineUsers_lblletter");
        var capital = document.getElementById("ctl00_ContentPlaceHolder1_DefineUsers_lblCapital");
        var number = document.getElementById("ctl00_ContentPlaceHolder1_DefineUsers_lblNumebr");
        var special = document.getElementById("ctl00_ContentPlaceHolder1_DefineUsers_lblSpecial");
        var length = document.getElementById("ctl00_ContentPlaceHolder1_DefineUsers_lblLength");
        if (parseInt(pwdoption) == 2) {


            // When the user clicks on the password field, show the message box
            myInput.onfocus = function () {
                document.getElementById("pwdmessage").style.display = "block";
            }

            // When the user clicks outside of the password field, hide the message box
            myInput.onblur = function () {
                document.getElementById("pwdmessage").style.display = "none";
            }

            // When the user starts to type something inside the password field
            myInput.onkeyup = function () {
                // Validate lowercase letters
                var lowerCaseLetters = /[a-z]/g;
                if (myInput.value.match(lowerCaseLetters)) {
                    letter.classList.remove("invalid");
                    letter.classList.add("valid");
                } else {
                    letter.classList.remove("valid");
                    letter.classList.add("invalid");
                }

                // Validate capital letters
                var upperCaseLetters = /[A-Z]/g;
                if (myInput.value.match(upperCaseLetters)) {
                    capital.classList.remove("invalid");
                    capital.classList.add("valid");
                } else {
                    capital.classList.remove("valid");
                    capital.classList.add("invalid");
                }

                // Validate numbers
                var numbers = /[0-9]/g;
                if (myInput.value.match(numbers)) {
                    number.classList.remove("invalid");
                    number.classList.add("valid");
                } else {
                    number.classList.remove("valid");
                    number.classList.add("invalid");
                }

                // Validate Special Characters
                var specials = /(?=.*[!@#$%^&*])/g;
                if (myInput.value.match(specials)) {
                    special.classList.remove("invalid");
                    special.classList.add("valid");
                } else {
                    special.classList.remove("valid");
                    special.classList.add("invalid");
                }

                // Validate length
                if (myInput.value.length >= 8) {
                    length.classList.remove("invalid");
                    length.classList.add("valid");
                } else {
                    length.classList.remove("valid");
                    length.classList.add("invalid");
                }
            }
        }
    });

</script>
<body>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblEmpName" runat="server" Text="User Full Name" CssClass="Profiletitletxt"
                meta:resourcekey="lblEmpNameResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtUserName" runat="server" Width="200px" meta:resourcekey="txtUserNameResource1">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
                Display="None" ErrorMessage="Please enter user full name" ValidationGroup="VGUsers"
                meta:resourcekey="valSumUsersResource1">
            </asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="RequiredFieldValidator1" WarningIconImageUrl="~/images/warning1.png"
                Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblGroup" runat="server" Text="User Group" CssClass="Profiletitletxt"
                meta:resourcekey="lblGroupResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="ddlGroup" runat="server" MarkFirstMatch="true" meta:resourcekey="ddlGroupResource1">
            </telerik:RadComboBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Group Name"
                ControlToValidate="ddlGroup" ValidationGroup="VGUsers" InitialValue="--Please Select--"
                Display="None" meta:resourcekey="RequiredFieldValidator2Resource1">
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlGroup"
                ValidationGroup="VGUsers" ErrorMessage="Please Select Group Name" InitialValue="--الرجاء الاختيار--"
                meta:resourcekey="RequiredFieldValidator3Resource1" Display="None">
            </asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="RequiredFieldValidator2" WarningIconImageUrl="~/images/warning1.png"
                Enabled="True">
            </cc1:ValidatorCalloutExtender>
            <cc1:ValidatorCalloutExtender ID="vcUserGroup" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="RequiredFieldValidator3" WarningIconImageUrl="~/images/warning1.png"
                Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblUserId" runat="server" Text="User ID" CssClass="Profiletitletxt"
                meta:resourcekey="lblUserIdResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtUserID" runat="server" CssClass="labelControlsStyle" Width="150px"
                meta:resourcekey="txtUserIDResource1">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="valReqUserId" runat="server" ControlToValidate="txtUserID"
                Display="None" ErrorMessage="Please Enter User ID" ValidationGroup="VGUsers"
                meta:resourcekey="valReqUserIdResource1">
            </asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="valReqUserId" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
            </cc1:ValidatorCalloutExtender>
            <%--<asp:LinkButton ID="LnkChPasswd" runat="server" Font-Italic="True" Text="<%$ Resources:DefineUsers, lnkchangePasswd %>"
                    Font-Size="XX-Small" ForeColor="#FF3300"></asp:LinkButton>--%>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblUserType" runat="server" Text="User Type" CssClass="Profiletitletxt"
                meta:resourcekey="lblUserTypeResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:RadioButtonList ID="rblUserType" runat="server" AutoPostBack="true" CssClass="Profiletitletxt"
                RepeatDirection="Horizontal" meta:resourcekey="rblUserTypeeResource1">
                <asp:ListItem Value="1" Text="System User" Selected="True" meta:resourcekey="rblUserTypeItem1Resource1">
                </asp:ListItem>
                <asp:ListItem Value="2" Text="Active Directory User" meta:resourcekey="rblUserTypeItem2Resource1">
                </asp:ListItem>
                <asp:ListItem Value="3" Text="Both System and AD" meta:resourcekey="rblUserTypeItem3Resource1">
                </asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>
    <div id="trPassword" runat="server" class="row">
        <div class="col-md-2">
            <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="Profiletitletxt"
                meta:resourcekey="lblPasswordResource1"></asp:Label>
        </div>
        <div class="col-md-4 pos-rel">
            <asp:TextBox ID="txtPassword" runat="server" CssClass="labelControlsStyle" meta:resourcekey="txtPasswordResource1"
                TextMode="Password">
            </asp:TextBox>
            <asp:RequiredFieldValidator ID="valReqPassword" runat="server" ControlToValidate="txtPassword"
                Display="None" ErrorMessage="Please Enter Password" ValidationGroup="VGUsers"
                meta:resourcekey="valReqPasswordResource1">
            </asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="valReqPassword" WarningIconImageUrl="~/images/warning1.png"
                Enabled="True">
            </cc1:ValidatorCalloutExtender>
            <div id="pwdmessage">
                <asp:Label ID="lblInfo" runat="server" SkinID="Remark" Text="Password Must Have:"
                    meta:resourcekey="lblInfoResource1"></asp:Label>
                <asp:Label ID="lblletter" runat="server" SkinID="PWD-Validation" Text="Lowercase Letter"
                    meta:resourcekey="lblletterResource1"></asp:Label>
                <asp:Label ID="lblCapital" runat="server" SkinID="PWD-Validation" Text="Capital (Uppercase) Letter"
                    meta:resourcekey="lblCapitalResource1"></asp:Label>
                <asp:Label ID="lblNumebr" runat="server" SkinID="PWD-Validation" Text="Number"
                    meta:resourcekey="lblNumebrResource1"></asp:Label>
                <asp:Label ID="lblSpecial" runat="server" SkinID="PWD-Validation" Text="Special Character"
                    meta:resourcekey="lblSpecialResource1"></asp:Label>
                <asp:Label ID="lblLength" runat="server" SkinID="PWD-Validation" Text="Minimum 8 Characters"
                    meta:resourcekey="lblLengthResource1"></asp:Label>
            </div>
        </div>

        <div class="col-md-4">
            <asp:RegularExpressionValidator ID="PwdValidation" runat="server" ControlToValidate="txtPassword"
                ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$" ValidationGroup="VGUsers"
                ErrorMessage="Password Must Include At Least One Lower Case Character, One Upper Case Charachter, One Special Character, One Number and 8 Digit Length"
                meta:resourcekey="PwdValidationResource1">
            </asp:RegularExpressionValidator>
        </div>
    </div>
    <div id="trConfirmPassword" runat="server" class="row">
        <div class="col-md-2">
            <asp:Label ID="lblConfirm" runat="server" Text="Confirm Password" CssClass="Profiletitletxt"
                meta:resourcekey="lblConfirmResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtConfirm" runat="server" CssClass="labelControlsStyle" meta:resourcekey="txtConfirmResource1"
                TextMode="Password">
            </asp:TextBox>
            <asp:CompareValidator ID="valCompConfirm" runat="server" ControlToCompare="txtPassword"
                ControlToValidate="txtConfirm" ErrorMessage="Please Confirm the Password" ValidationGroup="VGUsers"
                meta:resourcekey="valCompConfirmResource1">
            </asp:CompareValidator>
        </div>
    </div>
    <div id="trChangePassword" runat="server" visible="false" class="row">
        <div class="col-md-2">
            <asp:Label ID="lblChangePassword" runat="server" Text="Password" CssClass="Profiletitletxt"
                meta:resourcekey="lblPasswordResource1" />
        </div>
        <div class="col-md-4">
            <asp:LinkButton ID="lnbChangePassword" runat="server" Text="Change Password" meta:resourcekey="lnbChangePasswordResource1"
                CausesValidation="false" />
            <asp:HiddenField ID="hdnChangePassword" runat="server" />
            <cc1:ModalPopupExtender ID="mpeChangePwdPopup" runat="server" BehaviorID="modelPopupExtender1"
                TargetControlID="lnbChangePassword" PopupControlID="divChangePwd" DropShadow="True"
                OnCancelScript="ResetPopUp(); return false;" CancelControlID="btnCancel" Enabled="true"
                BackgroundCssClass="ModalBackground" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblUserEmail" runat="server" CssClass="Profiletitletxt" Text="User Email"
                meta:resourcekey="lblUserEmailResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtUserEmail" runat="server" CssClass="labelControlsStyle" meta:resourcekey="txtUserEmailResource1">
            </asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblJobDesc" runat="server" CssClass="Profiletitletxt" Text="Job Description"
                meta:resourcekey="lblJobDescResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtJobDesc" runat="server" CssClass="labelControlsStyle" meta:resourcekey="txtJobDescResource1">
            </asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblUserPhone" runat="server" CssClass="Profiletitletxt" Text="User Phone"
                meta:resourcekey="lblUserPhoneResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtPhone" runat="server" CssClass="labelControlsStyle" meta:resourcekey="txtPhoneResource1">
            </asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblIsActive" runat="server" CssClass="Profiletitletxt" Text="Active"
                meta:resourcekey="lblIsActiveResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:CheckBox ID="chkActive" runat="server" Text="&nbsp;" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblRemarks" runat="server" CssClass="Profiletitletxt" Text="Remarks"
                meta:resourcekey="lblRemarksResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="labelControlsStyle"
                Width="200px" meta:resourcekey="txtRemarksResource1">
            </asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblDefaultAppLang" runat="server" CssClass="Profiletitletxt" Text="Default Application Language"
                meta:resourcekey="lblDefaultAppLangResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:RadioButtonList ID="rblDefaultAppLang" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Arabic" Value="AR" meta:resourcekey="ListItemArbResource1" Selected="True">
                </asp:ListItem>
                <asp:ListItem Text="English" Value="EN" meta:resourcekey="ListItemEngResource2">
                </asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>

    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblDefaultEmailLang" runat="server" CssClass="Profiletitletxt" Text="Default Email Language"
                meta:resourcekey="lblDefaultEmailLangResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:RadioButtonList ID="rblDefaultEmailLang" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Arabic" Value="AR" meta:resourcekey="ListItemArbResource1" Selected="True">
                </asp:ListItem>
                <asp:ListItem Text="English" Value="EN" meta:resourcekey="ListItemEngResource2">
                </asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblDefaultSMSLang" runat="server" CssClass="Profiletitletxt" Text="Default SMS Language"
                meta:resourcekey="lblDefaultSMSLangResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:RadioButtonList ID="rblDefaultSMSLang" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Arabic" Value="AR" meta:resourcekey="ListItemArbResource1" Selected="True">
                </asp:ListItem>
                <asp:ListItem Text="English" Value="EN" meta:resourcekey="ListItemEngResource2">
                </asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblSecurityLevel" runat="server" CssClass="Profiletitletxt" Text="Security Level"
                meta:resourcekey="lblSecurityLevelResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:RadioButtonList ID="rBtnLstSecurity" runat="server" RepeatDirection="Horizontal"
                meta:resourcekey="rBtnLstSecurityResource1">
                <asp:ListItem Text="System Level" Value="1" meta:resourcekey="ListItemResource1">
                </asp:ListItem>
                <asp:ListItem Text="Company Level" Value="3" meta:resourcekey="ListItemResource2">
                </asp:ListItem>
                <asp:ListItem Text="Unit Level" Value="2" meta:resourcekey="ListItemResource3">
                </asp:ListItem>
                <%--   <asp:ListItem Text="Entity Level" Value="3" meta:resourcekey="ListItemResource3"></asp:ListItem>--%>
            </asp:RadioButtonList>
        </div>
    </div>
    <div id="RowCompanies" runat="server" class="row">
        <div class="col-md-4">
            <telerik:RadComboBox ID="RadCmbBxCompanies" AutoPostBack="true" AllowCustomText="false"
                MarkFirstMatch="true" Skin="Vista" runat="server">
            </telerik:RadComboBox>
            <asp:RequiredFieldValidator ID="rfvCompanies" InitialValue="--Please Select--" runat="server"
                ValidationGroup="VGUsers" ControlToValidate="RadCmbBxCompanies" Display="None"
                ErrorMessage="Please Select Company" meta:resourcekey="rfvCompaniesResource1">
            </asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vceCompanies" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="rfvCompanies" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div id="RowEntities" runat="server" class="row">
        <div class="col-md-4">
            <telerik:RadComboBox ID="RadCmbBxEntity" AutoPostBack="true" AllowCustomText="false"
                ValidationGroup="VGUsers" MarkFirstMatch="true" Skin="Vista" runat="server">
            </telerik:RadComboBox>
            <asp:RequiredFieldValidator ID="rfvEntiy" InitialValue="--Please Select--" ValidationGroup="VGEntities"
                runat="server" ControlToValidate="RadCmbBxEntity" Display="None" ErrorMessage="Please Select Entity"
                meta:resourcekey="rfvEntiyResource1">
            </asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="vceEntity" runat="server" CssClass="AISCustomCalloutStyle"
                TargetControlID="rfvEntiy" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
            </cc1:ValidatorCalloutExtender>
        </div>
    </div>
    <div id="dvHasMobile" runat="server">
        <div class="row">
            <div class="col-md-2">
            </div>
            <div class="col-md-4">
                <asp:CheckBox ID="chkIsMobile" runat="server" Text="Has Mobile" AutoPostBack="true" meta:resourcekey="chkIsMobileResource1" />
            </div>
        </div>
        <div id="dvMobileControls" runat="server" visible="false">
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkIsSelfService" runat="server" Text="Has Self-Service" meta:resourcekey="chkIsSelfServiceResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkSelfServiceReports" runat="server" Text="Has Self-Service Reports" meta:resourcekey="chkSelfServiceReportsResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkMobilePunch" runat="server" Text="Has Mobile Punch" meta:resourcekey="chkMobilePunchResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkAllowAutoPunch" runat="server" Text="Allow Auto Punch" meta:resourcekey="chkAllowAutoPunchResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-4">
                    <asp:CheckBox ID="chkRegUser" runat="server" Text="Registered User" Enabled="false" meta:resourcekey="chkRegUserResource1" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblRegisterdDevice" runat="server" Text="Registered Device" meta:resourcekey="lblRegisterdDeviceResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtRegisteredDevice" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="lblDeviceType" runat="server" Text="Device Type" meta:resourcekey="lblDeviceTypeResource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtDeviceType" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:LinkButton ID="lnkClearMobileVal" runat="server" Text="Clear Device" ForeColor="Green" Visible="false" meta:resourcekey="lnkClearMobileValResource1"></asp:LinkButton>
                </div>
            </div>
        </div>



    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Panel ID="pnlEmployeeFilter" runat="server" GroupingText="Employee" Width="100%"
                meta:resourcekey="pnlEmployeeFilterResource1">
                <uc2:PageFilter ID="EmployeeFilterUC" runat="server" OneventEmployeeSelect="LoadGridByEmplotyeeID" />
            </asp:Panel>
        </div>
    </div>
    <div id="trControls" runat="server" class="row">
        <div class="col-md-12 text-center">
            <asp:Button ID="btnSave" runat="server" ValidationGroup="VGUsers" Text="Save" CssClass="button"
                CausesValidation="true" EnableViewState="False" meta:resourcekey="btnSaveResource1" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="button" meta:resourcekey="btnDeleteResource1" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" meta:resourcekey="btnClearResource1" />
        </div>
    </div>
    <div class="row">
        <div class="table-responsive">
            <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="gvUsers" Skin="Hay"
                ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
            <telerik:RadGrid ID="gvUsers" runat="server" AutoGenerateColumns="false" PageSize="25"
                AllowPaging="true" AllowSorting="true" AllowFilteringByColumn="true" GroupingSettings-CaseSensitive="false">
                <SelectedItemStyle ForeColor="Maroon" />
                <MasterTableView IsFilterItemExpanded="true" CommandItemDisplay="Top" DataKeyNames="ID"
                    AllowFilteringByColumn="false">
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" Text="&nbsp;" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" DataType="System.Int32" AllowFiltering="false"
                            SortExpression="ID" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LoginName" HeaderText="User Id" DataType="System.String"
                            AllowFiltering="true" ShowFilterIcon="true" SortExpression="LoginName" Resizable="false"
                            meta:resourcekey="GridBoundColumnResource8">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="User_FullName" HeaderText="User Full Name" DataType="System.String"
                            AllowFiltering="true" ShowFilterIcon="true" SortExpression="User_FullName" Resizable="false"
                            meta:resourcekey="GridBoundColumnResource7">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GRPID" HeaderText="GRPID" DataType="System.Int32"
                            AllowFiltering="false" SortExpression="GRPID" Visible="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="GROUPNAME" HeaderText="User Group" DataType="System.String"
                            AllowFiltering="true" ShowFilterIcon="true" SortExpression="GROUPNAME" Resizable="false"
                            meta:resourcekey="GridBoundColumnResource9">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="EmployeeNo" HeaderText="Employee Number" DataType="System.String"
                            AllowFiltering="true" ShowFilterIcon="true" SortExpression="EmployeeNo" Resizable="false"
                            meta:resourcekey="GridBoundColumnResource10">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RegisteredUser" HeaderText="Registered User" DataType="System.String"
                            AllowFiltering="true" ShowFilterIcon="true" SortExpression="RegisteredUser" Resizable="false"
                            meta:resourcekey="GridBoundColumnResource11">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="RegisteredDevice" HeaderText="Registered Device" DataType="System.String"
                            AllowFiltering="true" ShowFilterIcon="true" SortExpression="RegisteredDevice" Resizable="false"
                            meta:resourcekey="GridBoundColumnResource12">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="DeviceType" HeaderText="Device Type" DataType="System.String"
                            AllowFiltering="true" ShowFilterIcon="true" SortExpression="DeviceType" Resizable="false"
                            meta:resourcekey="GridBoundColumnResource13">
                        </telerik:GridBoundColumn>
                    </Columns>
                    <CommandItemTemplate>
                        <telerik:RadToolBar runat="server" ID="RadToolBar1" Skin="Hay" OnButtonClick="RadToolBar1_ButtonClick">
                            <Items>
                                <telerik:RadToolBarButton Text="Apply filter" CommandName="FilterRadGrid" ImagePosition="Right"
                                    ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource1" />
                            </Items>
                        </telerik:RadToolBar>
                    </CommandItemTemplate>
                </MasterTableView><ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"
                    EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
            </telerik:RadGrid>
            <%--<asp:GridView ID="gvUsers" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="mGrid" HeaderStyle-CssClass="hdr" PagerStyle-CssClass="pgr"
                        RowStyle-CssClass="raw" Visible="False" Width="100%" meta:resourcekey="gvUsersResource1">
                        <RowStyle CssClass="raw" />
                        <Columns>
                            <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkUser" runat="server" meta:resourcekey="chkUserResource1" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ControlStyle-CssClass="tableAlign" HeaderText="User Full Name"
                                meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEmployeeid" runat="server" CssClass="hyperLink" ForeColor="Black"
                                        CausesValidation="False" OnClick="lnkEmployeeid_Click" Text='<%# DataBinder.Eval(Container,"DataItem.User_FullName") %>'
                                        meta:resourcekey="lnkEmployeeidResource1"></asp:LinkButton>
                                </ItemTemplate>
                                <ControlStyle CssClass="tableAlign" />
                                <ItemStyle />
                            </asp:TemplateField>
                            <asp:BoundField DataField="GROUPNAME" HeaderText="User Group" meta:resourcekey="BoundFieldResource1" />
                            <asp:TemplateField HeaderText="ID" Visible="False" meta:resourcekey="TemplateFieldResource3">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserID" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                                        meta:resourcekey="lblUserIDResource2"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GRPID" Visible="False" meta:resourcekey="TemplateFieldResource4">
                                <ItemTemplate>
                                    <asp:Label ID="lblGrpid" runat="server" Text='<%# Bind("GrpID") %>' meta:resourcekey="lblGrpidResource1"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("GrpID") %>' meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False" meta:resourcekey="TemplateFieldResource5">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserType" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.UserType") %>'
                                        meta:resourcekey="lblUserTypeResource1"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="LOCID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocID" runat="server" Text='<%# Bind("LocationID") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("LocationID") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <HeaderStyle CssClass="hdr" />
                    </asp:GridView>--%>
        </div>
    </div>
    <div id="divChangePwd" class="commonPopup" style="display: none">
        <div class="row" runat="server" id="divOldPassword" visible="false">
            <div class="col-md-4">
                <asp:Label ID="lblOldPassword" runat="server" CssClass="Profiletitletxt" meta:resourcekey="lblOldPasswordResource1"
                    Text="Old Password"></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="txtOldPassowrd" runat="server" meta:resourcekey="txtOldPassowrdResource1"
                    TextMode="Password">
                </asp:TextBox>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtOldPassowrd"
                        ErrorMessage="Please enter Old Password" Display="Dynamic" EnableClientScript="true"
                        meta:resourcekey="RequiredFieldValidator4Resource1" ValidationGroup="vgChanegePassowrd">*</asp:RequiredFieldValidator>--%>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblNewPassword" runat="server" Text="New Password" CssClass="Profiletitletxt"
                    meta:resourcekey="lblNewPasswordResource1"></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="txtNewPassowrd" runat="server" TextMode="Password" meta:resourcekey="txtNewPassowrdResource1">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNewPassowrd"
                    Display="Dynamic" EnableClientScript="true" ErrorMessage="Please enter New Password"
                    ValidationGroup="vgChanegePassowrd" meta:resourcekey="RequiredFieldValidator5Resource1">*</asp:RequiredFieldValidator>
                <cc1:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="txtNewPassowrd"
                    DisplayPosition="RightSide" PreferredPasswordLength="8" MinimumNumericCharacters="1"
                    PrefixText="Strength:">
                </cc1:PasswordStrength>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password" CssClass="Profiletitletxt"
                    meta:resourcekey="lblConfirmPasswordResource1"></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="txtConfirmPassowrd" runat="server" ValidationGroup="vgChanegePassowrd"
                    TextMode="Password" meta:resourcekey="txtConfirmPassowrdResource1">
                </asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtConfirmPassowrd"
                    ErrorMessage="Please enter Confirm Password" Display="Dynamic" EnableClientScript="true"
                    ValidationGroup="vgChanegePassowrd" meta:resourcekey="RequiredFieldValidator6Resource1">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtConfirmPassowrd"
                    ControlToValidate="txtNewPassowrd" ErrorMessage="The Passwords you typed doesn't  match please try again"
                    ValidationGroup="vgChanegePassowrd" meta:resourcekey="CompareValidator1Resource1">*</asp:CompareValidator>
            </div>
        </div>
        <div class="row">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="Profiletxt"
                ValidationGroup="vgChanegePassowrd" meta:resourcekey="ValidationSummary1Resource1"
                ShowMessageBox="true" ShowSummary="false" />
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Button ID="btnChangePassword" runat="server" CssClass="button" Text="Save" ValidationGroup="vgChanegePassowrd"
                    meta:resourcekey="btnChangePasswordResource1" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" meta:resourcekey="btnCancelResource1" />
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnid" runat="server" Value="0" />
    <asp:HiddenField ID="hdnsortDir" runat="server" Value="ASC" />
    <asp:HiddenField ID="hdnsortExp" runat="server" />
    <asp:HiddenField ID="hdLocID" runat="server" />
    <asp:HiddenField ID="hdngrpID" runat="server" />
</body>
