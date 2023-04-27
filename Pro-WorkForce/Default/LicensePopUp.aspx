<%@ Page Language="VB" AutoEventWireup="false"
    CodeFile="LicensePopUp.aspx.vb" Inherits="Default_LicensePopUp" Theme="SvTheme" %>



<!DOCTYPE html>
<script type="text/javascript">

    function GetRadWindow() {
        var oWindow = null;
        if (window.radWindow) oWindow = window.radWindow;
        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
        return oWindow;
    }

    function CloseAndRefresh() {
        var oWnd = GetRadWindow();
        oWnd.close();
        GetRadWindow().BrowserWindow.location.reload();
    }
</script>


<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=9">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="icon" href="../assets/images/favicon.png">

    <title>STSupreme</title>

    <!-- Bootstrap core CSS -->

    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <link href="../assets/css/custom-dof.css" type="text/css" title="styles1" media="screen" rel="stylesheet" />
    <link href="../assets/css/custom.css" type="text/css" title="styles2" media="screen" rel="stylesheet" disabled="" />
    <link href="../assets/css/custom-Red.css" type="text/css" title="styles3" media="screen" rel="stylesheet" disabled="" />
    <link href="../assets/css/custom-Blue.css" type="text/css" title="styles4" media="screen" rel="stylesheet" disabled="" />
    <link href="../assets/css/custom-Violet.css" type="text/css" title="styles5" media="screen" rel="stylesheet" disabled="" />
    <link href="../assets/css/custom-Gold.css" type="text/css" title="styles6" media="screen" rel="stylesheet" disabled="" />
    <link href="../assets/css/SvCustomLogin.css" rel="stylesheet" />
    <link href="../assets/css/Animation.css" rel="stylesheet" />
    <link href="../assets/css/animations.css" rel="stylesheet" />
</head>
<body>

    <div class="LoginBgFix animation-fadeInQuickInv"></div>

    <form id="form1" runat="server">

        <div class="container">


            <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblLicensekey" runat="server" Text="License Key"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:TextBox ID="txtNewLicense" runat="server" TextMode="MultiLine" Style="resize: none"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" />
                </div>

            </div>
        </div>
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
</body>

</html>


