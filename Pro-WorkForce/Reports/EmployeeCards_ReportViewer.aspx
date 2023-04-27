<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmployeeCards_ReportViewer.aspx.vb" Inherits="Reports_EmployeeCards_ReportViewer" %>


<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
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
    <form id="form1" runat="server">
        <div class="container">


            <div class="row">
                <div class="col-md-12">
                   <%-- <div><iframe id="fraDisabled" width="528" height="473" onMyLoad="disableContextMenu();">--%>

                          <CR2:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
                        GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                        meta:resourcekey="CRVResource1" EnableDrillDown="False"/>

                     <%--    </iframe></div>--%>

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

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function disableContextMenu() {
        if (document.layers) {
            //Capture the MouseDown event.
            document.captureEvents(Event.MOUSEDOWN);
            //Disable the OnMouseDown event handler.
            $(document).mousedown(function () {
                return false;
            });
        } else {
            //Disable the OnMouseUp event handler.
            $(document).mouseup(function (e) {
                if (e != null && e.type == "mouseup") {
                    //Check the Mouse Button which is clicked.
                    if (e.which == 2 || e.which == 3) {
                        //If the Button is middle or right then disable.
                        return false;
                    }
                }
            });
        }
 
        //Disable the Context Menu event.
        $(document).contextmenu(function () {
            return false;
        });
    }   
</script>
