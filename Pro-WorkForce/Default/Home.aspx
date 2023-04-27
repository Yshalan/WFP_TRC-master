<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" Theme="SvTheme"
    CodeFile="Home.aspx.vb" Inherits="Default_Theme2" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="UserSecurityFilter"
    TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../Dashboard/js1/highcharts.js"></script>
    <script src="../Dashboard/js1/series-label.js"></script>
    <script src="../Dashboard/js1/exporting.js"></script>
    <script src="../Dashboard/js1/export-data.js"></script>
    <script src="../Dashboard/js1/highcharts-more.js"></script>
    <script src="../Dashboard/js1/solid-gauge.src.js"></script>

    <script src="../js/jquery-ui-1.12.1.min.js"></script>
    <script src="../js/Chart.bundle.js"></script>
    <script src="../js/utils.js"></script>


    <asp:Literal runat="server" ID="JSONStatusCountSeries" meta:resourcekey="JSONStatusCountSeriesResource1" />
    <asp:Literal runat="server" ID="JSONStatusCountChartType" meta:resourcekey="JSONStatusCountChartTypeResource1" />
    <asp:Literal runat="server" ID="JSONStatusCountChartTitleText" meta:resourcekey="JSONStatusCountChartTitleTextResource1" />
    <asp:Literal runat="server" ID="JSONStatusCountChartSubTitleText" meta:resourcekey="JSONStatusCountChartSubTitleTextResource1" />

    <asp:Literal runat="server" ID="JSONRequestStatusCountSeries" meta:resourcekey="JSONRequestStatusCountSeriesResource1" />
    <asp:Literal runat="server" ID="JSONRequestStatusCountChartType" meta:resourcekey="JSONRequestStatusCountChartTypeResource1" />
    <asp:Literal runat="server" ID="JSONRequestStatusCountChartTitleText" meta:resourcekey="JSONRequestStatusCountChartTitleTextResource1" />
    <asp:Literal runat="server" ID="JSONRequestStatusCountChartSubTitleText" meta:resourcekey="JSONRequestStatusCountChartSubTitleTextResource1" />

    <asp:Literal runat="server" ID="JSONEmployeeWorkingHoursCountSeries" meta:resourcekey="JSONEmployeeWorkingHoursCountSeriesResource1" />
    <asp:Literal runat="server" ID="JSONEmployeeWorkingHoursCountChartType" meta:resourcekey="JSONEmployeeWorkingHoursCountChartTypeResource1" />
    <asp:Literal runat="server" ID="JSONEmployeeWorkingHoursCountChartTitleText" meta:resourcekey="JSONEmployeeWorkingHoursCountChartTitleTextResource1" />
    <asp:Literal runat="server" ID="JSONEmployeeWorkingHoursCountChartSubTitleText" meta:resourcekey="JSONEmployeeWorkingHoursCountChartSubTitleTextResource1" />
    <asp:Literal runat="server" ID="JSONEmployeeWorkingHoursCountChartDateText" meta:resourcekey="JSONEmployeeWorkingHoursCountChartDateTextResource1" />


    <asp:Literal runat="server" ID="JSONTransactionStatsSeries" meta:resourcekey="JSONTransactionStatsSeriesResource1" />
    <asp:Literal runat="server" ID="JSONTransactionStatsChartType" meta:resourcekey="JSONTransactionStatsChartTypeResource1" />
    <asp:Literal runat="server" ID="JSONTransactionStatsChartTitleText" meta:resourcekey="JSONTransactionStatsChartTitleTextResource1" />
    <asp:Literal runat="server" ID="JSONTransactionStatsChartSubTitleText" meta:resourcekey="JSONTransactionStatsChartSubTitleTextResource1" />

    <asp:Literal runat="server" ID="JSONminYear" meta:resourcekey="JSONminYearResource1" />
    <asp:Literal runat="server" ID="JSONminMonth" meta:resourcekey="JSONminMonthResource1" />
    <asp:Literal runat="server" ID="JSONminDay" meta:resourcekey="JSONminDayResource1" />

    <asp:Literal runat="server" ID="JSONmaxYear" meta:resourcekey="JSONmaxYearResource1" />
    <asp:Literal runat="server" ID="JSONmaxMonth" meta:resourcekey="JSONmaxMonthResource1" />
    <asp:Literal runat="server" ID="JSONmaxDay" meta:resourcekey="JSONmaxDayResource1" />

    <asp:Literal runat="server" ID="JSONSummaryViolationCountSeries" meta:resourcekey="JSONSummaryViolationCountSeriesResource1" />
    <asp:Literal runat="server" ID="JSONSummaryViolationCountChartType" meta:resourcekey="JSONSummaryViolationCountChartTypeResource1" />
    <asp:Literal runat="server" ID="JSONSummaryViolationCountChartTitleText" meta:resourcekey="JSONSummaryViolationCountChartTitleTextResource1" />
    <asp:Literal runat="server" ID="JSONSummaryViolationCountChartSubTitleText" meta:resourcekey="JSONSummaryViolationCountChartSubTitleTextResource1" />

    <asp:Literal runat="server" ID="JSONPermissionRequestCountSeries" meta:resourcekey="JSONPermissionRequestCountSeriesResource1" />
    <asp:Literal runat="server" ID="JSONPermissionRequestCountChartType" meta:resourcekey="JSONPermissionRequestCountChartTypeResource1" />
    <asp:Literal runat="server" ID="JSONPermissionRequestCountChartTitleText" meta:resourcekey="JSONPermissionRequestCountChartTitleTextResource1" />
    <asp:Literal runat="server" ID="JSONPermissionRequestCountChartSubTitleText" meta:resourcekey="JSONPermissionRequestCountChartSubTitleTextResource1" />

    <asp:Literal runat="server" ID="JSONLeaveRequestCountSeries" meta:resourcekey="JSONLeaveRequestCountSeriesResource1" />
    <asp:Literal runat="server" ID="JSONLeaveRequestCountChartType" meta:resourcekey="JSONLeaveRequestCountChartTypeResource1" />
    <asp:Literal runat="server" ID="JSONLeaveRequestCountChartTitleText" meta:resourcekey="JSONLeaveRequestCountChartTitleTextResource1" />
    <asp:Literal runat="server" ID="JSONLeaveRequestCountChartSubTitleText" meta:resourcekey="JSONLeaveRequestCountChartSubTitleTextResource1" />

    <script type="text/javascript" charset="utf-8">
        var lang = '<%=chartLang%>';

        $(document).ready(function () {
            GetEmployeeWorkingHours();
            GetStatusCount();
            GetViolationSummary();
            GetPermissionRequestDash();
            GetLeaveRequestDash();
            LoadCharts();
            LoadEmployeeCharts();
        })

        function GetStatusCount() {

            Highcharts.chart('StatusCount_container', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                credits: {
                    enabled: false
                },
                title: {
                    text: Var_StatusCountChartTitleText
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true
                        },
                        showInLegend: true
                    }
                },
                series: Var_StatusCountSeries
            });


        }

        function GetEmployeeWorkingHours() {
            Highcharts.chart('WorkingHoursContainer', {
                chart: {
                    type: 'column'
                },
                credits: {
                    enabled: false
                },
                title: {
                    text: Var_EmployeeWorkingHoursCountChartSubTitleText
                },
                subtitle: {
                    text: Var_EmployeeWorkingHoursCountChartDateText
                },
                xAxis: {

                    categories: [
                      Var_EmployeeWorkingHoursCountChartSubTitleText
                    ],
                    crosshair: false
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: Var_EmployeeWorkingHoursCountChartTitleText
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                      '<td style="padding:0"><b>{point.y:.0f} </b></td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                plotOptions: {
                    column: {
                        pointPadding: 0.2,
                        borderWidth: 0
                    }
                },
                series: Var_EmployeeWorkingHoursCountSeries
            });

        }

        function GetViolationSummary() {
            Highcharts.chart('ViolationSummary_container', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },
                credits: {
                    enabled: false
                },
                title: {
                    text: Var_SummaryViolationCountChartTitleText,
                    align: 'center',
                    verticalAlign: 'middle',
                    y: 50
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        dataLabels: {
                            enabled: true,
                            //distance: -50,
                            style: {
                                fontWeight: 'bold',
                                color: 'black'
                            }
                        },
                        showInLegend: true,
                        startAngle: -90,
                        endAngle: 90,
                        center: ['50%', '75%'],
                        size: '110%'
                    }
                },
                series: [{
                    type: 'pie',
                    name: 'Pecentage',
                    innerSize: '50%',
                    data: Var_SummaryViolationCountSeries
                }]
            });


        }

        function GetPermissionRequestDash() {
            Highcharts.chart('PermissionRequest_container', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },
                title: {
                    text: Var_PermissionRequestCountChartTitleText,
                    align: 'center',
                    verticalAlign: 'middle',
                    y: -135
                },
                credits: {
                    enabled: false
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        dataLabels: {
                            enabled: true,
                            distance: -50,
                            style: {
                                fontWeight: 'bold',
                                color: 'white'
                            }
                        },
                        showInLegend: true,
                        startAngle: -270,
                        endAngle: 90,
                        center: ['50%', '50%'],
                        size: '70%'
                    }

                },
                series: [{
                    type: 'pie',
                    name: 'Percent',
                    innerSize: '50%',
                    data: Var_PermissionRequestCountSeries
                }]
            });
        }

        function GetLeaveRequestDash() {
            Highcharts.chart('LeaveRequest_container', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },
                title: {
                    text: Var_LeaveRequestCountChartTitleText,
                    align: 'center',
                    verticalAlign: 'middle',
                    y: -135
                },
                credits: {
                    enabled: false
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        dataLabels: {
                            enabled: true,
                            distance: -50,
                            style: {
                                fontWeight: 'bold',
                                color: 'white'
                            }
                        },
                        showInLegend: true,
                        startAngle: -270,
                        endAngle: 90,
                        center: ['50%', '50%'],
                        size: '70%'
                    }
                },
                series: [{
                    type: 'pie',
                    name: 'Percent',
                    innerSize: '50%',
                    data: Var_LeaveRequestCountSeries
                }]
            });
        }

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
                url: 'Home.aspx/GetGroupUsers',
                contentType: 'application/json; charset= utf-8',
                dataType: 'json',
                success: function (r) {
                    var json = JSON.parse(r.d)




                    $.ajax({
                        type: 'POST',
                        url: 'Home.aspx/GetGroupUsers',
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
            var lang = '<%=chartLang%>';
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



            var ctx = document.getElementById("canvas_Users").getContext("2d");
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
            var ctx2 = document.getElementById("chart-area_Users").getContext("2d");
            window.myDoughnut = new Chart(ctx2, config);
        }



        function getRandomColor() {
            var letters = '0123456789ABCDEF';
            var color = '#';
            for (var i = 0; i < 6; i++) {
                color += letters[Math.floor(Math.random() * 16)];
            }
            return color;
        }







        function LoadEmployeeCharts() {
            $(".rpLink, .rpExpandable, .rpExpanded").attr("style", "background-color: #2779A7 !important")

            var colors = ["E21E25", "262626"
    , "FCA311", "070707", "869CB3", "837B72", "3B3833", "546A76", "B4CEB3", "B4ADA5", "A2708A",
    "824670", "160035", "270260", "A2D5F8", "5C9CE0", "2E290E", "585A2D", "DBD7E1", "2A6881",
    "5CC3B6", "E4572E", "194573", "136F63", "D56062", "067BC2", "FFE737", "DB9D47", "363537",
    "E4572E", "B1AE91", "99D17B", "A14DA0", "FCD3A5", "F4F7A0", "83B81A", "26A146", "912018",
    "EB8C23", "EDAF19", "949FA3", "1C2541", "B8801B", "1C0E1A", "B3A2DF", "E8B1E3", "406471",
    "7A3B69", "406471", "EDF7D2", "18BE18", "FFF800", "1010BB", "4A0D67", "FF206E", "FF84E8",
    "4A0D67", "07A0C3", "FFF1D0", "D56062", "A14DA0", "870381", "4B78C4", "A2EA9D"]


            var horizontalBarEmpLabels = [];
            var horizontalBarEmpData = [];
            var DoughnutEmpLabels = [];
            var DoughnutEmpData = [];
            var DoughnutEmpColors = [];

            $.ajax({
                type: 'POST',
                url: 'Home.aspx/GetEmployees_Entities',
                contentType: 'application/json; charset= utf-8',
                dataType: 'json',
                success: function (r) {
                    var json = JSON.parse(r.d)




                    $.ajax({
                        type: 'POST',
                        url: 'Home.aspx/GetEmployees_Entities',
                        contentType: 'application/json; charset= utf-8',
                        dataType: 'json',
                        success: function (r) {
                            var json = JSON.parse(r.d)


                            for (var i = 0; i < json.length ; i++) {
                                DoughnutEmpLabels.push(json[i].EntityName)
                                DoughnutEmpData.push(json[i].EmployeeCount)
                                DoughnutEmpColors.push(getEmployeeRandomColor())
                            }

                            for (var i = 0; i < json.length ; i++) {
                                horizontalBarEmpLabels.push(json[i].EntityName)
                                horizontalBarEmpData.push(json[i].EmployeeCount)
                            }

                            horizontalBarEmp(horizontalBarEmpLabels, horizontalBarEmpData)
                            DoughnutEmployeeChart(DoughnutEmpLabels, DoughnutEmpData, DoughnutEmpColors)
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

        function horizontalBarEmp(labels, data) {
            var color = Chart.helpers.color;
            var lang = '<%=chartLang%>';
            if (lang == 'ar') {
                var barChartEmpData = {
                    labels: labels,
                    datasets: [{
                        label: 'عدد الموظفين',
                        backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
                        borderColor: window.chartColors.red,
                        borderWidth: 1,
                        data: data
                    }
                    ]

                };
            }
            else {
                var barChartEmpData = {
                    labels: labels,
                    datasets: [{
                        label: 'Number Of Employees',
                        backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
                        borderColor: window.chartColors.red,
                        borderWidth: 1,
                        data: data
                    }
                    ]

                };
            }



            var ctx = document.getElementById("canvas_Employee").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'horizontalBar',
                data: barChartEmpData,
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

        function DoughnutEmployeeChart(DoughnutEmpLabels, DoughnutEmpData, DoughnutEmpColors) {

            var config = {
                type: 'doughnut',
                data: {
                    datasets: [{
                        data: DoughnutEmpData,
                        backgroundColor: DoughnutEmpColors,
                        label: 'Dataset 1'
                    }],
                    labels: DoughnutEmpLabels
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
            var ctx2 = document.getElementById("chart-area_Employee").getContext("2d");
            window.myDoughnut = new Chart(ctx2, config);
        }

        function randomScalingFactor() {
            return Math.round(Math.random() * 100);
        };

        function getEmployeeRandomColor() {
            var letters = '0123456789ABCDEF';
            var color = '#';
            for (var i = 0; i < 6; i++) {
                color += letters[Math.floor(Math.random() * 16)];
            }
            return color;
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <%--  <div class="col-md-4  PredictedData" runat="server" id="dvPrediction" visible="false">
            
        </div>--%>
        <div class="col-md-2 right-Calendar">
            <telerik:RadDatePicker ID="rdpFromDate" runat="server" Culture="en-US" AutoPostBack="true"
                Width="180px" meta:resourcekey="rdpFromDateResource1">
                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                </Calendar>
                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                    Width="" LabelWidth="64px">
                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                    <FocusedStyle Resize="None"></FocusedStyle>
                    <DisabledStyle Resize="None"></DisabledStyle>
                    <InvalidStyle Resize="None"></InvalidStyle>
                    <HoveredStyle Resize="None"></HoveredStyle>
                    <EnabledStyle Resize="None"></EnabledStyle>
                </DateInput>
                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
            </telerik:RadDatePicker>
        </div>
    </div>
    <div class="row" runat="server" id="dvPrediction" visible="false">
        <div class="col-md-12 text-center">
            <asp:Label ID="lblPrediction" runat="server" Text="Predicted Data from A.I. Engine" meta:resourcekey="lblPredictionResource1"></asp:Label>
        </div>
    </div>
    <div class="pull-right" style="margin-top:2%">

        <%--        <div style="overflow: hidden;">
                        <div class="form-group">
                <div class="row">
                    <div class="col-md-8">
                        <div id="datetimepicker12">
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                $(function () {
                    $('#datetimepicker12').datetimepicker({
                        inline: true,

                    });
                });


            </script>
        </div>--%>

        <div class="row">
            <div class="col-md-2 StatusCount_container">
                <div id="PermissionRequest_container" style="min-width: 310px; height: 400px; margin: 0 auto" class="highchartsalignment"></div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 StatusCount_container">
                <div id="LeaveRequest_container" style="min-width: 310px; height: 400px; margin: 0 auto" class="highchartsalignment"></div>
            </div>
        </div>
    </div>
    <div class="row" runat="server" visible="false">
        <div class="col-md-12">
            <uc1:UserSecurityFilter ID="UserSecurityFilter1" runat="server" ValidationGroup="grpSave" />
        </div>
    </div>

    <div class="row"  runat="server" visible="false">
        <div class="col-md-2">
            <asp:Label ID="lbldate" runat="server" Text="From Date" Class="Profiletitletxt" meta:resourcekey="lbldateResource1"></asp:Label>
        </div>

    </div>
    <div class="row" runat="server" visible="false">
        <div class="col-md-2">
            <asp:Label ID="lblToDate" runat="server" Text="To Date" Class="Profiletitletxt" meta:resourcekey="lblToDateResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadDatePicker ID="rdpToDate" runat="server" Culture="en-US" meta:resourcekey="rdpToDateResource1">
                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                </Calendar>
                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                    Width="" LabelWidth="64px">
                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                    <FocusedStyle Resize="None"></FocusedStyle>
                    <DisabledStyle Resize="None"></DisabledStyle>
                    <InvalidStyle Resize="None"></InvalidStyle>
                    <HoveredStyle Resize="None"></HoveredStyle>
                    <EnabledStyle Resize="None"></EnabledStyle>
                </DateInput>
                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
            </telerik:RadDatePicker>
            <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="rdpFromDate" ControlToValidate="rdpToDate"
                ErrorMessage="End Date should be greater than or equal to From Date" Display="Dynamic"
                Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave" meta:resourcekey="CVDateResource1"></asp:CompareValidator>

        </div>
    </div>
    <div class="row" runat="server" visible="false">
        <div class="col-md-2">
        </div>
        <div class="col-md-4">
            <asp:Button runat="server" ID="btnRetrive" Text="Retrieve" ValidationGroup="grpSave"
                CssClass="button" meta:resourcekey="btnRetriveResource1" />
        </div>
    </div>
    <br />
    <br />
    <div class="row">
        <div class="col-md-5 StatusCount_container">
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="employee-status">
                        <div class="col-md-2 present">
                            <span class="dotBlue"></span>
                            <div class="top">
                                <asp:Label ID="lblChkIn" runat="server" Text="Checked In" meta:resourcekey="lblChkInResource1"></asp:Label>
                            </div>
                            <div class="col-md-1 mid">
                                <asp:Label ID="lblChkInVal" runat="server" meta:resourcekey="lblChkInValResource1"></asp:Label>
                            </div>
                            <div class="bottom">
                                <span class="left-sec">
                                    <asp:Label ID="lblChkInMale" runat="server" Text="Male" meta:resourcekey="lblMaleResource1"></asp:Label>
                                    <asp:Label ID="lblChkInMaleVal" runat="server" Text="0"></asp:Label>
                                </span>
                                <span class="right-sec">
                                    <asp:Label ID="lblChkInFemale" runat="server" Text="Female" meta:resourcekey="lblFemaleResource1"></asp:Label>
                                    <asp:Label ID="lblChkInFemaleVal" runat="server" Text="0"></asp:Label>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2 CheckedOut">
                            <span class="dotRed"></span>
                            <div class="top">
                                <asp:Label ID="lblChkOut" runat="server" Text="Checked Out" meta:resourcekey="lblChkOutResource1"></asp:Label>
                            </div>
                            <div class="col-md-1 mid">
                                <asp:Label ID="lblChkOutVal" runat="server" meta:resourcekey="lblChkOutValResource1"></asp:Label>
                            </div>
                            <div class="bottom">
                                <span class="left-sec">
                                    <asp:Label ID="lblChkOutMale" runat="server" Text="Male" meta:resourcekey="lblMaleResource1"></asp:Label>
                                    <asp:Label ID="lblChkOutMaleVal" runat="server" Text="0"></asp:Label>
                                </span>
                                <span class="right-sec">
                                    <asp:Label ID="lblChkOutFemale" runat="server" Text="Female" meta:resourcekey="lblFemaleResource1"></asp:Label>
                                    <asp:Label ID="lblChkOutFemaleVal" runat="server" Text="0"></asp:Label>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2 Absent">
                            <span class="dotgreen"></span>
                            <div class="top">
                                <asp:Label ID="lblAbsent" runat="server" Text="Absent" meta:resourcekey="lblAbsentResource1"></asp:Label>
                            </div>
                            <div class="col-md-1 mid">
                                <asp:Label ID="lblAbsentVal" runat="server" meta:resourcekey="lblAbsentValResource1"></asp:Label>
                            </div>
                            <div class="bottom">
                                <span class="left-sec">
                                    <asp:Label ID="lblAbsentMale" runat="server" Text="Male" meta:resourcekey="lblMaleResource1"></asp:Label>
                                    <asp:Label ID="lblAbsentMaleVal" runat="server" Text="0"></asp:Label>
                                </span>
                                <span class="right-sec">
                                    <asp:Label ID="lblAbsentFemale" runat="server" Text="Female" meta:resourcekey="lblFemaleResource1"></asp:Label>
                                    <asp:Label ID="lblAbsentFemaleVal" runat="server" Text="0"></asp:Label>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2 Leaves">
                            <span class="dotblack"></span>
                            <div class="top">
                                <asp:Label ID="lblLeave" runat="server" Text="Leaves" meta:resourcekey="lblLeaveResource1"></asp:Label>
                            </div>
                            <div class="col-md-1 mid">
                                <asp:Label ID="lblLeaveVal" runat="server" meta:resourcekey="lblLeaveValResource1"></asp:Label>
                            </div>
                            <div class="bottom">
                                <span class="left-sec">
                                    <asp:Label ID="lblLeaveMale" runat="server" Text="Male" meta:resourcekey="lblMaleResource1"></asp:Label>
                                    <asp:Label ID="lblLeaveMaleVal" runat="server" Text="0"></asp:Label>
                                </span>
                                <span class="right-sec">
                                    <asp:Label ID="lblLeaveFemale" runat="server" Text="Female" meta:resourcekey="lblFemaleResource1"></asp:Label>
                                    <asp:Label ID="lblLeaveFemaleVal" runat="server" Text="0"></asp:Label>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4 StatusCount_container">
            <div id="StatusCount_container" class="highchartsalignment"></div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-5 StatusCount_container">
            <div id="WorkingHoursContainer" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
        </div>
        <div class="col-md-5 StatusCount_container">
            <%--<div id="RequestStatusCount_container" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto" class="highchartsalignment"></div>--%>
            <div id="ViolationSummary_container" style="min-width: 310px; height: 400px; margin: 0 auto" class="highchartsalignment"></div>
        </div>
    </div>


    <div class="row">
        <div class="col-md-5 StatusCount_container">
            <canvas id="canvas_Users" class="highchartsalignment"></canvas>
        </div>
        <div class="col-md-5 StatusCount_container">
            <canvas id="chart-area_Users" class="highchartsalignment" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-6">
            <div class="col-md-3">
                <asp:Label ID="Label1" Text="Active Users:" runat="server" meta:resourcekey="Label1Resource1" />
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblUserNum" runat="server" meta:resourcekey="lblUserNumResource1" />
            </div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3">
                <asp:Label ID="Label2" Text="User Groups:" runat="server" meta:resourcekey="Label2Resource1" />
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" ID="lblNumberOfGroups" meta:resourcekey="lblNumberOfGroupsResource1" />
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-5 StatusCount_container">
            <canvas id="canvas_Employee" class="highchartsalignment"></canvas>
        </div>
        <div class="col-md-5 StatusCount_container">
            <canvas id="chart-area_Employee" class="highchartsalignment" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-6 StatusCount_container">
            <div class="col-md-3">
                <asp:Label ID="lblEmployeeNo" Text="Employees Number:" runat="server" meta:resourcekey="lblEmployeeNoResource1" />
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblEmployeeNoVal" runat="server" meta:resourcekey="lblEmployeeNoValResource1" />
            </div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3">
                <asp:Label ID="lblEntityNo" Text="Entities Number:" runat="server" meta:resourcekey="lblEntityNoResource1" />
            </div>

            <div class="col-md-3">
                <asp:Label runat="server" ID="lblEntityNoVal" meta:resourcekey="lblEntityNoValResource1" />
            </div>

        </div>
    </div>

    <br />
    <br />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server"></asp:Content>

