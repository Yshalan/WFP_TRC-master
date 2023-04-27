<%@ Page Title="TA_Change_password" Language="VB" MasterPageFile="~/Default/NewMaster.master"
    AutoEventWireup="false" Theme="SvTheme" CodeFile="ChangePassword.aspx.vb" Inherits="Admin_Security_ChangePassword"
    Culture="auto" UICulture="auto" %>

<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../../CSS/TabStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/ControlsStyle.css" rel="stylesheet" type="text/css" />--%>

    <script>
        $(document).ready(function () {

            var pwdoption = '<%=PasswordType%>';
            var myInput = document.getElementById("ctl00_ContentPlaceHolder1_txtNewPassowrd");
            var letter = document.getElementById("ctl00_ContentPlaceHolder1_lblletter");
            var capital = document.getElementById("ctl00_ContentPlaceHolder1_lblCapital");
            var number = document.getElementById("ctl00_ContentPlaceHolder1_lblNumebr");
            var special = document.getElementById("ctl00_ContentPlaceHolder1_lblSpecial");
            var length = document.getElementById("ctl00_ContentPlaceHolder1_lblLength");

            //var letter = document.getElementById("letter");
            //var capital = document.getElementById("capital");
            //var number = document.getElementById("number");
            //var special = document.getElementById("special");
            //var length = document.getElementById("length");

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <uc1:PageHeader ID="PageHeader1" runat="server" />
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblChangePassword" runat="server" Font-Bold="True" Text="Please change your password"
                        meta:resourcekey="lblChangePassword" Font-Size="Small" ForeColor="#CC0000"></asp:Label>
                    <asp:Label ID="lblOldPassword" runat="server" meta:resourcekey="lblOldPasswordResource1"
                        Text="Old Password"></asp:Label>
                    <asp:TextBox ID="txtOldPassowrd" runat="server" meta:resourcekey="txtOldPassowrdResource1"
                        TextMode="Password" Width="200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOldPassowrd"
                        ErrorMessage="Please enter Old Password" meta:resourcekey="RequiredFieldValidator1Resource1"
                        ValidationGroup="vgChanegePassowrd">*</asp:RequiredFieldValidator>

                </div>
            </div>
            <div class="row">
                <div class="col-md-4 pos-rel">
                    <asp:Label ID="lblNewPassword" runat="server" Text="New Password"
                        meta:resourcekey="lblNewPasswordResource1"></asp:Label>
                    <asp:TextBox ID="txtNewPassowrd" runat="server" Width="200px" TextMode="Password"
                        meta:resourcekey="txtNewPassowrdResource1">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPassowrd"
                        Display="Dynamic" ErrorMessage="Please enter New Password" ValidationGroup="vgChanegePassowrd"
                        meta:resourcekey="RequiredFieldValidator2Resource1">*</asp:RequiredFieldValidator>

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
                    <asp:RegularExpressionValidator ID="PwdValidation" runat="server" ControlToValidate="txtNewPassowrd"
                        ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$" ValidationGroup="vgChanegePassowrd"
                        ErrorMessage="Password Must Include At Least One Lower Case Character, One Upper Case Charachter, One Special Character, One Number and 8 Digit Length"
                        meta:resourcekey="PwdValidationResource1">
                    </asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password"
                        meta:resourcekey="lblConfirmPasswordResource1"></asp:Label>
                    <asp:TextBox ID="txtConfirmPassowrd" runat="server" Width="200px" ValidationGroup="vgChanegePassowrd"
                        TextMode="Password" meta:resourcekey="txtConfirmPassowrdResource1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmPassowrd"
                        ErrorMessage="Please enter Confirm Password" ValidationGroup="vgChanegePassowrd"
                        meta:resourcekey="RequiredFieldValidator3Resource1">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtConfirmPassowrd"
                        ControlToValidate="txtNewPassowrd" ErrorMessage="The Passwords you typed doesn't  match please try again"
                        ValidationGroup="vgChanegePassowrd" meta:resourcekey="CompareValidator1Resource1">*</asp:CompareValidator>
                </div>
            </div>
            <div class="row">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                    ValidationGroup="vgChanegePassowrd" meta:resourcekey="ValidationSummary1Resource1"
                    ShowMessageBox="True" ShowSummary="False" />
            </div>
            <div class="row">
                <div class="col-md-4">
                    <asp:Button ID="btnSave" runat="server" SkinID="Save" Text="Save" ValidationGroup="vgChanegePassowrd"
                        Width="75px" meta:resourcekey="btnSaveResource1" />
                </div>
            </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
