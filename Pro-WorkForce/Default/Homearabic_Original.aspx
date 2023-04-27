<%@ Page Language="VB" Title="Pro-WorkForce" AutoEventWireup="false" CodeFile="Homearabic_Original.aspx.vb" Theme="Default"
    Inherits="Default_HomeArabic" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>Work Force Pro</title>
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, minimum-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="icon" href="../assets/img/favicon.png" />
    <!-- Favicon -->
    <link href="Images/favicon.png" rel="icon" type="image/png" />

    <!-- Smart Vision core CSS -->
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css" />
    <link href="../assets/css/animations.css" rel="stylesheet" />
    <link href="../assets/css/simple-sidebar.css" rel="stylesheet" />
    <link href="../assets/css/hover.css" rel="stylesheet" />

    <%--    <link href="../assets/css/custom-Gold.css" type="text/css" title="styles6" media="screen" rel="stylesheet" disabled="" />
    <link href="../assets/css/custom-dof.css" type="text/css" title="styles1" media="screen" rel="stylesheet" />
    <link href="../assets/css/custom.css" type="text/css" title="styles2" media="screen" rel="stylesheet" disabled="" />
    <link href="../assets/css/custom-Red.css" type="text/css" title="styles3" media="screen" rel="stylesheet" disabled="" />
    <link href="../assets/css/custom-Blue.css" type="text/css" title="styles4" media="screen" rel="stylesheet" disabled="" />
    <link href="../assets/css/custom-Violet.css" type="text/css" title="styles5" media="screen" rel="stylesheet" disabled="" />--%>

    <link id="lnkDofDesign" runat="server" href="../assets/css/custom-dof.css" type="text/css" title="styles1" media="screen" rel="stylesheet" />
    <link id="lnkGreenDesign" runat="server" href="../assets/css/custom.css" type="text/css" title="styles2" media="screen" rel="stylesheet" />
    <link id="lnkRedDesign" runat="server" href="../assets/css/custom-Red.css" type="text/css" title="styles3" media="screen" rel="stylesheet" />
    <link id="lnkBlueDesign" runat="server" href="../assets/css/custom-Blue.css" type="text/css" title="styles4" media="screen" rel="stylesheet" />
    <link id="lnkVioletDesign" runat="server" href="../assets/css/custom-Violet.css" type="text/css" title="styles5" media="screen" rel="stylesheet" />
    <link id="lnkGoldDesign" runat="server" href="../assets/css/custom-Gold.css" type="text/css" title="styles6" media="screen" rel="stylesheet" />
    <link id="lnkADMDesign" runat="server" href="../assets/css/custom-adm.css" type="text/css" title="styles7" media="screen" rel="stylesheet" />

    <link href="../assets/css/custom_Ar.css" rel="stylesheet" />
    <link href="../assets/css/carousel.css" rel="stylesheet" />

    <link href="../CSS/css.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 9]><script src="../assets/js/ie8-responsive-file-warning.js"></script><![endif]-->
    <script type="text/javascript" src="../assets/js/ie-emulation-modes-warning.js"></script>

    <!-- IE10 viewport hack for Surface/desktop Windows 8 bug -->
    <script type="text/javascript" src="../assets/js/ie10-viewport-bug-workaround.js"></script>

    <script type="text/javascript" src="../js/jquery.1.11.min.js"></script>
    <script type="text/javascript" src="../js/Action.js"></script>
    <script type="text/javascript" src="../js/lobibox.min.js"></script>

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
      <link rel="stylesheet" type="text/css" href="../assets/css/all-ie-only.css" />
    <![endif]-->
    <script type="text/javascript" language="javascript">
        function openRadWin(ID) {


            oWindow = radopen("AnnouncementsDetailsPopup.aspx?AnnouncementID=" + ID, "RadWindow1");

        }

        //setInterval(function () {
        //    notify_Requests("Permission Request", "Employee Request Permission", "en")
        //}, 6 * 1000);
    </script>
    <link href="../css/TA_home.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <%--<a href="#menu-toggle" id="menu-toggle"><img src="../assets/img/togglemenu.png" alt="" /></a>--%>
            <a class="navbar-brand" href="../default/homearabic.aspx">
                <%--<asp:Image ImageUrl="../assets/img/logo.png" runat="server" ToolTip="الصفحة الرئيسية" />--%>
                <asp:Image ImageUrl="../assets/img/WF-logo.png" runat="server" ToolTip="الصفحة الرئيسية"  Height="42px" Width="216px"/>
            </a>
            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#RightHeader" aria-expanded="false" aria-controls="RightHeader">
                <i class="fa fa-ellipsis-v"></i>
            </button>
            <img id="ClientLogo" runat="server" alt="" class="Client-logo" />
            <div id="RightHeader" class="right-header collapse">
                <ul>
                    <li class="dropdown user">
                        <a href="#" class="dropdown-toggle">
                            <span class="avatar">
                                <img alt="" id="EmpImage" runat="server" src="../assets/img/user.png" />
                            </span><%--<b class="caret"></b>--%>
                            <div class="UserText">
                                <asp:Label ID="lblLoginUser" SkinID="UserName" runat="server" Text="Label"></asp:Label>
                                <div class="clear"></div>
                                <asp:Label ID="lblLoginDate" SkinID="LoginTime" runat="server" Text=""><b>Last Login:</b></asp:Label>
                                <div class="clear"></div>
                            </div>
                        </a>
                    </li>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                            <asp:Image ID="Image2" ImageUrl="../assets/img/icn_edit.png" runat="server" ToolTip="اختر لونك المفضل" />
                        </a>
                        <div class="dropdown-menu animated fadeInUp">
                            <div>
                                <h4>اختر لونك المفضل</h4>
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
                            <%--  <li><a href="#">Profile</a> </li>
                             <li><a href="#"><span class="badge bg-danger pull-right">3</span> Notifications </a></li>
                             <li><a href="#">Help</a> </li>
                             <li class="divider"></li>
                             <li><a href="#">Logout</a> </li>--%>
                        </div>
                    </li>
                    <li>
                        <asp:LinkButton ID="lnkbtnLanguage" runat="server">
                            <asp:Image ID="ImgLanguage" src="../assets/img/EnBt.png" runat="server" ToolTip="تغيير اللغة الى الانجليزية" />
                        </asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton ID="lnkbtnLogOut" runat="server">
                            <asp:Image ID="ImgLogOut" src="../assets/img/icn_logout.png" runat="server" ToolTip="تسجيل الخروج" />

                        </asp:LinkButton>
                    </li>
                </ul>
            </div>
        </header>
        <div class="clear"></div>
        <div class="Main-wrapper SliderCollapse">
            <a href="#" id="toggleSlider" runat="server"><i class="fa fa-expand"></i></a>
            <div class="pos-rel pull-left full-width">
                <div id="myCarousel" class="carousel slide">
                    <div class="carousel-inner">
                        <asp:Repeater ID="repSlider" runat="server" Visible="false">
                            <ItemTemplate>
                                <div class="<%# If(Container.ItemIndex = 0, "item active", "item")%>">
                                    <img src="<%# Eval("ImageName", "../images/SliderImages/{0}")%>" alt="Alternate Text" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="item item1 active" id="dvDefaultSlider1" runat="server" visible="false">
                            <div class="container">
                                <div class="carousel-caption">
                                    <div class="slideCaption">
                                        <h1><span class="colour">Time</span> and <span class="colour">Attendance</span> System</h1>
                                    </div>
                                    <div class="slideIcons">
                                        <img src="../assets/img/slider/icon_set.png" alt="" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="item item2" id="dvDefaultSlider2" runat="server" visible="false">
                            <div class="container">
                                <div class="carousel-caption">
                                    <div class="slideIcons">
                                        <img src="../assets/img/slider/icon_set.png" alt="" />
                                    </div>
                                    <h1><span class="colour">Time</span> and <span class="colour">Attendance</span> System</h1>
                                </div>
                            </div>
                        </div>
                        <div class="item item3" id="dvDefaultSlider3" runat="server" visible="false">
                            <div class="container">
                                <div class="carousel-caption">
                                    <div class="slideIcons">
                                        <img src="../assets/img/slider/icon_set.png" alt="" />
                                    </div>
                                    <h1><span class="colour">Time</span> and <span class="colour">Attendance</span> System</h1>
                                </div>
                            </div>
                        </div>
                    </div>
                    <a class="left carousel-control" href="#myCarousel" data-slide="prev">&lsaquo;</a>
                    <a class="right carousel-control" href="#myCarousel" data-slide="next">&rsaquo;</a>
                </div>
                <!-- /.carousel -->
            </div>
            <%--<div class="slider">
                    <img src="../assets/img/slider/slider.png" alt="" />
               </div>--%>
            <div class="clear"></div>
            <div class="col-md-8-5 menu-sec">
                <div class="Svpanel HomeMenu" id="divHomeMenu" runat="server">
                    <ul>
                        <li id="liAdmin" runat="server">
                            <asp:LinkButton ID="lnkbtnAdministration" runat="server">
                                <div class="menuIcon">
                                    <i class="iconImg">
                                        <img id="imgAdministrator" runat="server"
                                            src="../assets/img/menuicon/icon_1.png" alt="" /></i>
                                </div>
                                <div class="clear"></div>
                                <asp:Label ID="lblAdministrator" runat="server" Text="Administrator"></asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li id="liDefinitions" runat="server">
                            <asp:LinkButton ID="lnkbtnDefinitions" runat="server">
                                <div class="menuIcon">
                                    <i class="iconImg">
                                        <img id="imgDefinitions" runat="server" src="../assets/img/menuicon/icon_2.png" alt="" /></i>
                                </div>
                                <div class="clear"></div>
                                <asp:Label ID="lblDefinitions" runat="server" Text="Definitions"></asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li id="liEmployees" runat="server">
                            <asp:LinkButton ID="lnkbtnEmployees" runat="server">
                                <div class="menuIcon">
                                    <i class="iconImg">
                                        <img src="../assets/img/menuicon/icon_3.png" alt="" /></i>
                                </div>
                                <div class="clear"></div>
                                <asp:Label ID="lblEmployees" runat="server" Text="Employees"></asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li id="liDailyTask" runat="server">
                            <asp:LinkButton ID="lnkbtnDailyTask" runat="server">
                                <div class="menuIcon">
                                    <i class="iconImg">
                                        <img src="../assets/img/menuicon/icon_4.png" alt="" /></i>
                                </div>
                                <div class="clear"></div>
                                <asp:Label ID="lblDailytasks" runat="server" Text="Daily tasks"></asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li id="liHR" runat="server">
                            <asp:LinkButton ID="lnkbtnHR" runat="server">
                                <div class="menuIcon">
                                    <i class="iconImg">
                                        <img src="../assets/img/menuicon/icon_5.png" alt="" /></i>
                                </div>
                                <div class="clear"></div>
                                <asp:Label ID="lblHR" runat="server" Text="HR"></asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li id="liSelfService" runat="server">
                            <asp:LinkButton ID="lnkbtnEmpSelfService" runat="server">
                                <div class="menuIcon">
                                    <i class="iconImg">
                                        <img src="../assets/img/menuicon/icon_6.png" alt="" /></i>
                                </div>
                                <div class="clear"></div>
                                <asp:Label ID="lblSelfService" runat="server" Text="Self Service"></asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li id="liEmprequest" runat="server">
                            <asp:LinkButton ID="lnkbtnEmprequest" runat="server">
                                <div id="divrequestsCount" runat="server" class="notification">
                                    <asp:Label ID="lblRequestsCount" runat="server">  </asp:Label>
                                </div>
                                <div class="menuIcon">
                                    <i class="iconImg">
                                        <img src="../assets/img/menuicon/icon_7.png" alt="" /></i>
                                </div>
                                <div class="clear"></div>
                                <asp:Label ID="lblRequests" runat="server" Text="Requests"></asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li id="liDashboards" runat="server">
                            <asp:LinkButton ID="lnkbtnDashboards" runat="server">
                                <div class="menuIcon">
                                    <i class="iconImg">
                                        <img src="../assets/img/menuicon/icon_8.png" alt="" /></i>
                                </div>
                                <div class="clear"></div>
                                <asp:Label ID="lblDashboards" runat="server" Text="Dashboards"></asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li id="liReports" runat="server">
                            <asp:LinkButton ID="lnkbtnReports" runat="server">
                                <div class="menuIcon">
                                    <i class="iconImg">
                                        <img src="../assets/img/menuicon/icon_9.png" alt="" /></i>
                                </div>
                                <div class="clear"></div>
                                <asp:Label ID="lblReports" runat="server" Text="1rpt"></asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li id="liTaskManagement" runat="server">
                            <asp:LinkButton ID="lnkbtnTaskManagement" runat="server">
                                <div class="menuIcon">
                                    <i class="iconImg">
                                        <img src="../assets/img/menuicon/icon_12.png" alt="" height="41" width="41" /></i>
                                </div>
                                <div class="clear"></div>
                                <asp:Label ID="lblTaskManagement" runat="server" Text="Task Management"></asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li id="liAppraisal" runat="server">
                            <asp:LinkButton ID="lnkbtnAppraisal" runat="server">
                                <div class="menuIcon">
                                    <i class="iconImg">
                                        <img src="../assets/img/menuicon/icon_13.png" alt="" height="41" width="41" /></i>
                                </div>
                                <div class="clear"></div>
                                <asp:Label ID="lblAppraisal" runat="server" Text="Appraisal"></asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li id="liSchedule" runat="server">
                            <asp:LinkButton ID="lnkbtnSchedule" runat="server">
                                <div class="menuIcon">
                                    <i class="iconImg">
                                        <img src="../assets/img/menuicon/icon_11.png" alt="" /></i>
                                </div>
                                <div class="clear"></div>
                                <asp:Label ID="lblSchedule" runat="server" Text="Schedule"></asp:Label>
                            </asp:LinkButton>
                        </li>
                        <li id="liSecurity" runat="server">
                            <asp:LinkButton ID="lnkbtnSecurity" runat="server">
                                <div class="menuIcon">
                                    <i class="iconImg">
                                        <img src="../assets/img/menuicon/icon_10.png" alt="" /></i>
                                </div>
                                <div class="clear"></div>
                                <asp:Label ID="lblSecurity" runat="server" Text="Security"></asp:Label>
                            </asp:LinkButton>
                        </li>
                    </ul>
                    <%--  <asp:DataList ID="dlstEngMenu" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                    HorizontalAlign="Center" RepeatLayout="Flow" CellPadding="0" CellSpacing="0"
                    BorderWidth="0" Width="630px">
                    <ItemStyle HorizontalAlign="Left" Width="210px" />
                    <ItemTemplate>
                        <table id="tbl" border="0" cellpadding="0" cellspacing="0" width="100%" style="text-align: left;">
                            <tr>
                                <td>
                                    
                                    <asp:LinkButton CssClass="menuIcon" runat="server" ID="btnShow" Text='' Visible="True" OnClick="btnShow_Click"></asp:LinkButton>
                                    <asp:Label  CssClass="menuIcon" runat="server" ID="lblId" Text='<%#DataBinder.Eval(Container,"DataItem.ModuleID")%>'
                                        Visible="false"></asp:Label>
                                    <asp:Label CssClass="menuIcon" runat="server" ID="lblDiv" Text='<%#DataBinder.Eval(Container,"DataItem.div")%>'
                                        Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>--%>
                </div>
            </div>

            <div class="col-md-3-5 notif-sec" id="divAnnouncements" runat="server">
                <div class="Svpanel Announcements">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <h5>
                        <asp:Image ID="Image3" ImageUrl="~/assets/img/Announcements.png" runat="server" /><span>Announcements</span></h5>
                    <div class="clear"></div>
                    <ul>
                        <asp:Repeater ID="repAnnouncement" runat="server">
                            <ItemTemplate>
                                <li>
                                    <span class="date">
                                        <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("MonthArabic")%>' />
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("DayNo")%>' />
                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' Visible="false" CssClass="hidden" />
                                    </span>
                                    <span class="description">

                                        <asp:LinkButton ID="lnkDescription" Text='<%# Eval("Title_Ar")%>' OnClientClick='<%# Eval("ID", "openRadWin({0});return false;") %>' runat="server" />
                                    </span>
                                    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Behavior="Default"
                                        EnableShadow="True" InitialBehavior="None">
                                        <Windows>
                                            <telerik:RadWindow ID="RadWindow1" runat="server" Animation="FlyIn" Behavior="Close, Move"
                                                Behaviors="Close, Move" EnableShadow="True" Height="450px" IconUrl="~/images/HeaderWhiteChrome.jpg"
                                                InitialBehavior="None" ShowContentDuringLoad="False" VisibleStatusbar="False"
                                                Width="700px">
                                            </telerik:RadWindow>
                                        </Windows>
                                    </telerik:RadWindowManager>
                                </li>

                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
        <div class="clear"></div>
        <div class="footer">
            <p>
                <asp:Label ID="lblCopyRight" runat="server" Text=""></asp:Label>
            </p>
        </div>
        <!-- Javascript -->
        <script src="../assets/js/jquery.min.js"></script>
        <script src="../assets/js/bootstrap.min.js"></script>
        <script src="../assets/css/styleswitch.js"></script>
        <script src="../assets/js/carousel.js"></script>
        <script type="text/javascript" src="../assets/js/moment.js"></script>
        <script src="../assets/js/docs.min.js"></script>
        <script src="../assets/js/svCustom.js"></script>
        <script type="text/javascript" src="../assets/js/jssor.slider.min.js"></script>


        <script>
            !function ($) {
                $(function () {
                    // carousel demo
                    $('#myCarousel').carousel()

                    $("#toggleSlider").click(function () {
                        $('.Main-wrapper').toggleClass('SliderExpand SliderCollapse');
                    });

                })
            }(window.jQuery)
        </script>


        <style>
            .SliderExpand .notif-sec, .SliderExpand .menu-sec
            {
                display: none;
            }

            .SliderExpand .carousel .container
            {
            }

            .carousel .item
            {
            }

            .SliderExpand .carousel .item
            {
                height: 80vh;
            }


            .container-fluid
            {
                -webkit-box-align: center;
                -ms-flex-align: center;
                align-items: center;
                display: -webkit-box;
                display: -ms-flexbox;
                display: flex;
            }

            #toggleSlider
            {
                position: absolute;
                top: 78px;
                left: 20px;
                z-index: 9999999;
                color: rgba(156, 156, 156, 0.4);
                font-size: 20px;
                background: rgba(138, 135, 135, 0.1);
                width: 35px;
                height: 35px;
                border-radius: 11px;
                display: flex;
                align-items: center;
                justify-content: center;
                text-decoration: none !important;
            }

                #toggleSlider:hover
                {
                    color: #333;
                    background: rgba(255, 255, 255, 0.80);
                }
        </style>
    </form>
</body>
</html>
