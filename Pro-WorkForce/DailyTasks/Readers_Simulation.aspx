<%@ Page Title="" Language="VB" MasterPageFile="~/Default/EmptyMaster_WithoutLogin.master" AutoEventWireup="false" CodeFile="Readers_Simulation.aspx.vb"
    Theme="SvTheme" Inherits="DailyTasks_Readers_Simulation" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="stylesheet" href="../js/compiled/flipclock.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="row">
        <div class="col-md-12">
            <uc1:PageHeader ID="pageheader1" runat="server" HeaderText="Reader Simulation" />
        </div>
    </div>
    <br />
    <br />
    <div class="row">
        <div class="col-md-4">
            <asp:Image ID="Thumbnail" runat="server" BorderStyle="Solid" BorderWidth="1px" ImageUrl="~/images/CompaniesLogo/41.png"
                Width="30%"></asp:Image>
        </div>
        <div class="col-md-6">
            <div class="clock" style="margin: 2em;"></div>
            <div class="message"></div>
        </div>
    </div>
    <br />
    <br />
    <div class="col-md-8-5 menu-sec">
        <div class="Svpanel HomeMenu" id="divHomeMenu" runat="server" style="width:90%">
            <asp:UpdatePanel ID="update1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblEmployeeNo" runat="server" Text="Employee Card No."></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtEmployeeNo" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnCheckExistance" runat="server" Text="Check Number" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblEmployeeNoStatus" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Button ID="btnIn" runat="server" Text="In" SkinID="btnSimulation" />
                                </div>

                            </div>
                            <div class="row">

                                <div class="col-md-4">
                                    <asp:Button ID="btnOut" runat="server" Text="Out" SkinID="btnSimulation" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Button ID="btnOfficialOut" runat="server" Text="Official Out" SkinID="btnSimulation" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Button ID="btnSickOut" runat="server" Text="Sick Out" SkinID="btnSimulation" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
    <script type="text/javascript">
        var clock;
        $(document).ready(function () {
            clock = $('.clock').FlipClock({
                clockFace: 'TwentyFourHourClock'
            });
        });
    </script>


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script type="text/javascript" src="../js/compiled/flipclock.js"></script>

</asp:Content>

