<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="Default_TALogin"
    UICulture="auto" Culture="auto" %>

<!DOCTYPE html>

<html>
<head runat="server" id="head1">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=9">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="../assets/images/favicon.png">

    <title>Work Force Pro</title>

    <!-- Bootstrap core CSS -->

    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom styles for this template -->

    <link id="lnkDofDesign" runat="server" href="../assets/css/custom-dof.css" type="text/css" title="styles1" media="screen" rel="stylesheet" />
    <link id="lnkGreenDesign" runat="server" href="../assets/css/custom.css" type="text/css" title="styles2" media="screen" rel="stylesheet" />
    <link id="lnkRedDesign" runat="server" href="../assets/css/custom-Red.css" type="text/css" title="styles3" media="screen" rel="stylesheet" />
    <link id="lnkBlueDesign" runat="server" href="../assets/css/custom-Blue.css" type="text/css" title="styles4" media="screen" rel="stylesheet" />
    <link id="lnkVioletDesign" runat="server" href="../assets/css/custom-Violet.css" type="text/css" title="styles5" media="screen" rel="stylesheet" />
    <link id="lnkGoldDesign" runat="server" href="../assets/css/custom-Gold.css" type="text/css" title="styles6" media="screen" rel="stylesheet" />
    <link id="lnkADMDesign" runat="server" href="../assets/css/custom-adm.css" type="text/css" title="styles7" media="screen" rel="stylesheet" />



    <link href="../assets/css/SvCustomLogin.css" rel="stylesheet" />
    <link href="../assets/css/Animation.css" rel="stylesheet" />
    <link href="../assets/css/animations.css" rel="stylesheet" />

    <%--    Lobi Box ALert--%>
    <%--    <link href="../CSS/lobibox.min.css" rel="stylesheet" />--%>
    <script type="text/javascript" src="../js/jquery.1.11.min.js"></script>
    <script type="text/javascript" src="../js/Action.js"></script>
    <script type="text/javascript" src="../js/lobibox.min.js"></script>

    <!-- Just for debugging purposes. -->
    <link id="css_ArCSS" runat="server" href="../svassets/css/custom_Ar.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function openRadWin() {

            oWindow = radopen("LicensePopUp.aspx", "RadWindow1");
        }
    </script>
   

</head>
<body dir="<%=TextDirection%>">
    <div class="LoginBgFix animation-fadeInQuickInv"></div>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <div class="container">
            <div class="right-header">
                <ul>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                            <asp:Image ID="Image2" ImageUrl="../assets/img/icn_edit-login.png" runat="server" meta:resourcekey="Image2Resource1" />
                        </a>
                        <div class="dropdown-menu animated fadeInUp">
                            <div>
                                <h4>Choose your Theme</h4>
                                <div class="clear"></div>
                                <div class="colours" id="Colour-wrapper">
                                    <a href="../assets/css/custom-Dof.css" class="Dof styleswitch" rel="styles1"></a>
                                    <a href="../assets/css/custom.css" class="Green styleswitch" rel="styles2"></a>
                                    <a href="../assets/css/custom-Red.css" class="Red styleswitch" rel="styles3"></a>
                                    <a href="../assets/css/custom-Blue.css" class="Blue styleswitch" rel="styles4"></a>
                                    <a href="../assets/css/custom-Violet.css" class="Violet styleswitch" rel="styles5"></a>
                                    <a href="../assets/css/custom-Gold.css" class="Gold styleswitch" rel="styles6"></a>
                                    <a href="../assets/css/custom-adm.css" class="Gold styleswitch" rel="styles7"></a>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
            <div class="row">
                <div class="col-sm-7">
                    <div class="iconPane animation-fadeInQuick">
                        <div class="loginleft">
                        </div>
                        <div class="row">
                            <asp:Label ID="lblCopyRight" runat="server" Font-Size="Small"></asp:Label>
                        </div>
                        <div class="row">
                            <asp:Label ID="lblVersionNumber" runat="server" Font-Size="Small"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-sm-5">
                    <div class="loginPane animation-fadeInQuick">
                        <div class="loginSecBg"></div>
                        <span class="logoLogin">
                            <asp:Image ID="Thumbnail" runat="server" meta:resourcekey="ThumbnailResource1"></asp:Image>
                        </span>
                        <div class="clearfix"></div>
                        <span class="loginHeadTxt">
                            <asp:Label ID="lblCustomerMsg" runat="server" style="text-decoration:none !important" text="Please <b>Login</b> here" Font-Size="Medium" meta:resourcekey="Label1Resource1">
                            </asp:Label>
                            <span class="loginHeadTxtIcn">
                                <asp:Image ID="Image8" ImageUrl="~/assets/images/loginArrow.png" runat="server"
                                    meta:resourcekey="Image8Resource1" />
                                <asp:LinkButton ID="lnkHaveLicense" runat="server" Text="Already Have License" Font-Size="Medium" ForeColor="#9EDBE0"
                                    OnClientClick="openRadWin(); return false" meta:resourcekey="lnkHaveLicenseResource1"></asp:LinkButton>
                            </span>
                        </span>
                        <div class="clearfix"></div>
                        <div class="form-group fgUser">
                            <div class="input-group input-group-lg">
                                <span class="input-group-addon">
                                    <span class="sv-user">
                                        <img alt="" src="../assets/images/Licon_username.png"></span>
                                </span>
                                <asp:TextBox ID="txtUserName" CssClass="form-control form-control_login fc_login"
                                    runat="server" meta:resourcekey="txtUserNameResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group fgPswd">
                            <div class="input-group input-group-lg">
                                <span class="input-group-addon">
                                    <span class="passwordIcon">
                                        <img alt="" src="../assets/images/Licon_pswrd.png">
                                    </span>
                                </span>
                                <asp:TextBox ID="txtPassword" CssClass="form-control form-control_login fc_pswd"
                                    TextMode="Password" runat="server" meta:resourcekey="txtPasswordResource1"></asp:TextBox>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="ui-checkbox">
                            <asp:CheckBox ID="chkUsePC" Text="Use PC Credential" runat="server" AutoPostBack="False" meta:resourcekey="chkUsePCResource1" />
                        </div>
                        <div class="clearfix"></div>
                        <asp:Button ID="login_button" runat="server" class="Lbutton" Text="Sign In" meta:resourcekey="login_buttonResource1" />
                    </div>
                </div>
                <!--for small Devices-->
                <div class="col-sm-7 iconPane-smallDev animation-fadeInQuick">
                    <div class="loginleft">
                    </div>
                </div>
                <!--for small Devices-->
            </div>
        </div>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server"
            EnableShadow="True" meta:resourcekey="RadWindowManager1Resource1">
            <Windows>
                <telerik:RadWindow ID="RadWindow1" runat="server" Animation="FlyIn" Behavior="Close, Move"
                    Behaviors="Close, Move" EnableShadow="True" IconUrl="~/images/HeaderWhiteChrome.jpg"
                    InitialBehavior="None" ShowContentDuringLoad="False" VisibleStatusbar="False"
                    Width="700px" Height="230px" meta:resourcekey="RadWindow1Resource1">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </form>


    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="../assets/js/jquery.min.js"></script>
    <script src="../assets/js/bootstrap.min.js"></script>
    <script src="../assets/css/styleswitch.js"></script>
    <script src="../assets/js/vendor/holder.min.js"></script>
    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script src="../assets/js/ie10-viewport-bug-workaround.js"></script>

    <script>
        $(".fc_login").focus(function () {
            $(".fgUser").css({ transform: 'scale(1.1)' });
        });

        $(".fc_login").focusout(function () {
            $(".fgUser").css({ transform: 'scale(1)' });
        });

        $(".fc_pswd").focus(function () {
            $(".fgPswd").css({ transform: 'scale(1.1)' });
        });

        $(".fc_pswd").focusout(function () {
            $(".fgPswd").css({ transform: 'scale(1)' });
        });
    </script>

    <script>
        function newDoc() {
            window.location.assign("default.aspx")
        }

    </script>


</body>

</html>

