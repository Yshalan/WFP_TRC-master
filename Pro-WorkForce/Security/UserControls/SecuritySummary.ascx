<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SecuritySummary.ascx.vb" Inherits="Security_UserControls_SecuritySummary" %>

<script src="../js/jquery-ui-1.12.1.min.js"></script>
<script src="../js/Chart.bundle.js"></script>
<script src="../js/utils.js"></script>
<script>
    function LoadCharts() {
        $(".rpLink, .rpExpandable, .rpExpanded").attr("style", "background-color: #2779A7 !important")

        var colors = ["E21E25", "262626"
, "FCA311", "070707", "869CB3", "837B72", "3B3833", "546A76", "B4CEB3", "B4ADA5", "A2708A",
"824670", "160035", "270260", "A2D5F8", "5C9CE0", "2E290E", "585A2D", "DBD7E1", "2A6881",
"5CC3B6", "E4572E", "194573", "136F63", "D56062", "067BC2", "FFE737", "DB9D47", "363537",
"E4572E", "B1AE91", "99D17B", "A14DA0", "FCD3A5", "F4F7A0", "83B81A", "26A146", "912018",
"EB8C23", "EDAF19", "949FA3", "1C2541", "B8801B", "1C0E1A", "B3A2DF", "E8B1E3", "406471",
"7A3B69", "406471", "EDF7D2", "18BE18", "FFF800", "1010BB", "4A0D67", "FF206E", "FF84E8",
"4A0D67", "07A0C3", "FFF1D0", "D56062", "A14DA0", "870381", "4B78C4", "A2EA9D"]


        var horizontalBarLabels = [];
        var horizontalBarData = [];
        var DoughnutLabels = [];
        var DoughnutData = [];
        var DoughnutColors = [];

        $.ajax({
            type: 'POST',
            url: 'Inner.aspx/GetGroupUsers',
            //data: '{QuestionId: "' + $find('ctl00_ContentPlaceHolder1_SurveyResult_rdQuestion').get_selectedItem().get_value() + '"}',
            contentType: 'application/json; charset= utf-8',
            dataType: 'json',
            success: function (r) {
                var json = JSON.parse(r.d)




                $.ajax({
                    type: 'POST',
                    url: 'Inner.aspx/GetGroupUsers',
                    //data: '{QuestionId: "' + $find('ctl00_ContentPlaceHolder1_SurveyResult_rdQuestion').get_selectedItem().get_value() + '"}',
                    contentType: 'application/json; charset= utf-8',
                    dataType: 'json',
                    success: function (r) {
                        var json = JSON.parse(r.d)


                        for (var i = 0; i < json.length ; i++) {
                            DoughnutLabels.push(json[i].GroupName)
                            DoughnutData.push(json[i].UserCount)
                            DoughnutColors.push(getRandomColor())
                        }

                        for (var i = 0; i < json.length ; i++) {
                            horizontalBarLabels.push(json[i].GroupName)
                            horizontalBarData.push(json[i].UserCount)
                        }

                        horizontalBar(horizontalBarLabels, horizontalBarData)
                        DoughnutChart(DoughnutLabels, DoughnutData, DoughnutColors)
                    },
                    error: function (d) {

                    }
                })


            },
            error: function (e) {
                console.log(e)
            }
        })


    }

    function horizontalBar(labels, data) {
        var color = Chart.helpers.color;
        var lang = '<%=ChartLang%>';
        if (lang == 'ar') {
            var barChartData = {
                labels: labels,
                datasets: [{
                    label: 'عدد المستخدمين',
                    backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
                    borderColor: window.chartColors.red,
                    borderWidth: 1,
                    data: data
                }
                ]

            };
        }
        else {
            var barChartData = {
                labels: labels,
                datasets: [{
                    label: 'Number Of Users',
                    backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
                    borderColor: window.chartColors.red,
                    borderWidth: 1,
                    data: data
                }
                ]

            };
        }



        var ctx = document.getElementById("canvas").getContext("2d");
        window.myBar = new Chart(ctx, {
            type: 'horizontalBar',
            data: barChartData,
            options: {
                responsive: true,
                legend: {
                    display: false,
                    text: 'Chart.js Horizontal Bar Chart',
                    labels: {
                        fontColor: 'rgb(255, 99, 132)'
                    }
                }
            }
        });

    }

    function DoughnutChart(DoughnutLabels, DoughnutData, DoughnutColors) {

        var config = {
            type: 'doughnut',
            data: {
                datasets: [{
                    data: DoughnutData,
                    backgroundColor: DoughnutColors,
                    label: 'Dataset 1'
                }],
                labels: DoughnutLabels
            },
            options: {
                responsive: true,
                legend: {
                    position: 'top'
                },
                title: {
                    display: false,
                    text: 'Chart.js Doughnut Chart'
                },
                animation: {
                    animateScale: true,
                    animateRotate: true
                }
            }
        };
        var ctx2 = document.getElementById("chart-area").getContext("2d");
        window.myDoughnut = new Chart(ctx2, config);
    }

    function randomScalingFactor() {
        return Math.round(Math.random() * 100);
    };

    function getRandomColor() {
        var letters = '0123456789ABCDEF';
        var color = '#';
        for (var i = 0; i < 6; i++) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return color;
    }

    $(document).ready(function () {
        LoadCharts();
    });
</script>


<div class="row">
    <div class="col-md-3 col-sm-6 col-xs-12">
        <div class="info-box">
            <span class="info-box-icon bg-aqua"><i class="usericon1"></i></span>

            <div class="info-box-content">
                <span class="info-box-text">
                    <asp:Label ID="lblActiveUsers" runat="server" Text="Active Users" meta:resourcekey="lblActiveUsersResource1"></asp:Label>
                </span>
                <span class="info-box-number">
                    <asp:Label ID="lblActiveUsersVal" runat="server"></asp:Label>
                </span>
            </div>
            <!-- /.info-box-content -->
        </div>
        <!-- /.info-box -->
    </div>
    <!-- /.col -->
    <div class="col-md-3 col-sm-6 col-xs-12">
        <div class="info-box">
            <span class="info-box-icon bg-red"><i class="usericon2"></i></span>

            <div class="info-box-content">
                <span class="info-box-text">
                    <asp:Label ID="lblInActiveUsers" runat="server" Text="InActive Users" meta:resourcekey="lblInActiveUsersResource1"></asp:Label>
                </span>
                <span class="info-box-number">
                    <asp:Label ID="lblInActiveUsersVal" runat="server"></asp:Label>
                </span>
            </div>
            <!-- /.info-box-content -->
        </div>
        <!-- /.info-box -->
    </div>
    <!-- /.col -->
    <div class="col-md-3 col-sm-6 col-xs-12">
        <div class="info-box">
            <span class="info-box-icon bg-green"><i class="usericon3"></i></span>

            <div class="info-box-content">
                <span class="info-box-text">
                    <asp:Label ID="lblOnlineUsers" runat="server" Text="Online Users" meta:resourcekey="lblOnlineUsersResource1"></asp:Label>
                </span>
                <span class="info-box-number">
                    <asp:Label ID="lblOnlineUsersVal" runat="server"></asp:Label>
                </span>
            </div>
            <!-- /.info-box-content -->
        </div>
        <!-- /.info-box -->
    </div>
    <!-- /.col -->
    <div class="col-md-3 col-sm-6 col-xs-12">
        <div class="info-box">
            <span class="info-box-icon bg-yellow"><i class="usericon4"></i></span>

            <div class="info-box-content">
                <span class="info-box-text">
                    <asp:Label ID="lblUserGroups" runat="server" Text="User Groups" meta:resourcekey="lblUserGroupsResource1"></asp:Label>
                </span>
                <span class="info-box-number">
                    <asp:Label ID="lblUserGroupsVal" runat="server"></asp:Label>
                </span>
            </div>
            <!-- /.info-box-content -->
        </div>
        <!-- /.info-box -->
    </div>
    <!-- /.col -->
</div>
<br />

<div id="dvCharts" runat="server">
    <div class="fancy-collapse-panel">
        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingOne">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                            <asp:Label ID="lblUsersGroups" runat="server" Text="Users & Groups" meta:resourcekey="lblUsersGroupsResource1"></asp:Label>
                        </a>
                    </h4>
                </div>
                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                    <div class="panel-body">
                        <div class="col-md-12 controls">
                            <div class="col-md-5">
                                <canvas id="canvas"></canvas>
                            </div>
                            <div class="col-md-5">
                                <canvas id="chart-area" />
                            </div>
                            <div class="col-md-2" style="padding: 0">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="Label1" Text="Active Users:" runat="server" meta:resourcekey="Label1Resource1" />
                                        <asp:Label ID="lblUserNum" runat="server" meta:resourcekey="lblUserNumResource1" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="Label2" Text="User Groups:" runat="server" meta:resourcekey="Label2Resource1" />
                                        <asp:Label runat="server" ID="lblNumberOfGroups" meta:resourcekey="lblNumberOfGroupsResource1" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

