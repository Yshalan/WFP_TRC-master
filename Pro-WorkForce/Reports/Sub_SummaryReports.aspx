<%@ Page Title="" Language="VB" AutoEventWireup="false"
    CodeFile="Sub_SummaryReports.aspx.vb" Inherits="Reports_Sub_SummaryReports" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" 
    Namespace="CrystalDecisions.Web" TagPrefix="CR2" %>
<script type="text/javascript" src="../js/jquery.1.11.min.js"></script>
<script type="text/javascript" src="../js/Action.js"></script>
<script type="text/javascript" src="../js/lobibox.min.js"></script>

<script language="javascript" type="text/javascript">
    //function closeWindow() {
    //    open(location, '_self').close();
    //}
</script>
<%--<body>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <form id="form1" runat="server">--%>

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

<%--        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>--%>
        <div class="container">
            <div class="row" id="dvLogin" runat="server" visible="true">
                <div class="col-md-12 gotolink">
                    <a href="../Default/Home.aspx" target="_blank" runat="server" id="lnkLogin">Go To Login Page</a>
                    <br />
                    <a href="javascript:window.location.href=window.location.href" target="_self" runat="server" id="lnkDownload">Already Login, Download File!</a>
                </div>
            </div>
        </div>
        <CR2:CrystalReportViewer ID="CRV" runat="server" AutoDataBind="True" SeparatePages="False"
            GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
            meta:resourcekey="CRVResource1" EnableDrillDown="False"
            GroupTreeImagesFolderUrl="" HasGotoPageButton="False"
            HasPageNavigationButtons="False" ToolbarImagesFolderUrl=""
            ToolPanelWidth="200px" />
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
<%--    </form>
</body>--%>




