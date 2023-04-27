<%@ Control Language="VB" AutoEventWireup="false" CodeFile="LeftMenu.ascx.vb" Inherits="UserControls_LeftMenu" %>
<%@ Import Namespace="smartv.UTILITIES" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<nav class="collapse navbar-collapse bs-navbar-collapse sidebar" role="navigation">
    <ul class="sidebar nav-mega">
        <script type="text/javascript">
            $(document).ready(function () {
                $(".test").addClass(function () {
                    var columnCount = $(".columns li").length;
                    if (columnCount <= 10) {
                        $(".columns").addClass("one-col");
                        $(".columns").css("column-count", "1");
                    } else if (columnCount > 10 && columnCount <= 20) {
                        $(".columns").css("column-count", "2");
                    } else if (columnCount > 20) {
                        $(".columns").css("column-count", "4");
                    }
                })
            })
        </script>

        <asp:Repeater ID="Repeater2" runat="server">
            <ItemTemplate>
                <li class="dropdown" style="text-align: center;">
                    <a href="#" data-pjax=".content-body" class="dropdown-toggle test" data-toggle="dropdown">
                        <%--  <i class="sidebar-icon fa fa-home" style="text-align:center;"></i>--%>
                        <i class="iconImg" style="display: block;">
                            <img id="img" runat="server" src='<%# Eval("icon") %>' alt="" />
                        </i>
                        <span class="sidebar-text"  style="text-align: center;"><%# Eval(IIf(SessionVariables.CultureInfo = "en-US", "Desc_En", "Desc_Ar"))%></span>
                    </a>

                    <ul class="columns dropdown-menu mega-menu mega-menu-ar">
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <li>
                                  <div class="row-fluid">
                                      <i class="glyphicon glyphicon-arrow-right"></i>
                                      <i class="glyphicon glyphicon-arrow-left"></i>
                                    <a href='<%#Eval("FormPath").ToString().Replace("//", "/")%>' data-pjax=".content-body">
                                        <span class="sidebar-text"><%# Eval(IIf(SessionVariables.CultureInfo = "en-US", "Desc_En", "Desc_Ar"))%></span>
                                    </a>
                                   </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</nav>
                


                

