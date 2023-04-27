<%@ Control Language="VB" AutoEventWireup="false" CodeFile="EmployeeSummary.ascx.vb" Inherits="Security_UserControls_EmployeeSummary" %>

<script src="../js/jquery-ui-1.12.1.min.js"></script>
<script src="../js/Chart.bundle.js"></script>
<script src="../js/utils.js"></script>
<asp:Literal runat="server" ID="JSONdrilldown" meta:resourcekey="JSONResource1" />
<asp:Literal runat="server" ID="JSONSeries" meta:resourcekey="JSONResource1" />
<asp:Literal runat="server" ID="JSONChartType" meta:resourcekey="JSONResource1" />
<asp:Literal runat="server" ID="JSONChartTitleText" meta:resourcekey="JSONResource1" />
<asp:Literal runat="server" ID="JSONChartSubTitleText" meta:resourcekey="JSONResource1" />

<script src="../DashBoard/js/jquery-2.0.2.min.js" type="text/javascript" charset="utf-8"></script>
<script src="../DashBoard/js/highcharts.js" type="text/javascript" charset="utf-8"></script>
<script src="../DashBoard/js/drilldown.js" type="text/javascript" charset="utf-8"></script>
<script src="../DashBoard/js/exporting.js" type="text/javascript" charset="utf-8"></script>
<meta http-equiv="Content-Type" content="text/plain" charset="UTF-8" />


<script>
    var myJSON = [];
    var Lang = '<%= chartLang%>'
    var dnloadJPEG
    var dnloadPDF
    var dnloadPNG
    var dnloadSVG
    var prntChart

    if (Lang == 'ar') {
        dnloadJPEG = "JPEG تحميل صورة",
        dnloadPDF = "PDF تحميل صورة",
        dnloadPNG = "PNG تحميل صورة",
        dnloadSVG = "SVF تحميل صورة",
        prntChart = "طباعة صورة"
    }
    else {
        dnloadJPEG = "Download JPEG",
         dnloadPDF = "Download PDF",
         dnloadPNG = "Download PNG",
         dnloadSVG = "Download SVF",
         prntChart = "Print Image"

    }
    $(document).ready(function () {
        //            debugger;
        document.charset = "utf-8",
        document.inputEncoding = "utf-8",
        document.defaultCharset = "utf-8",
        //Internationalization  

        Highcharts.setOptions({
            lang: {
                //drillUpText: '◁ Back to {series.name}',
                downloadJPEG: dnloadJPEG,
                downloadPDF: dnloadPDF,
                downloadPNG: dnloadPNG,
                downloadSVG: dnloadSVG,
                printChart: prntChart
            }
        });


        var options = {
            chart: {
                style: {
                    fontFamily: 'arial'
                },
                height: 300,
                events: {
                    drilldown: function (e) {
                        generateDrillDownJSON(e, false);
                    },
                    drillup: function (e) {
                        generateDrillDownJSON(e, true);
                    }
                }
            },
            title: {
                text: Var_ChartTitleText,
            },
            subtitle: {
                text: Var_ChartSubtitleText,
            },

            yAxis: {
                title: {
                    text: 'Number of Employees'
                }
            },

            tooltip: {
                formatter: function () {
                    return this.point.name + ' : ' + this.y + ' %';
                }
            },

            legend: {
                enabled: true,
            },

            xAxis: {
                categories: true,
                overflow: "justify"
            },
            exporting: {
                enabled: false
            },
            drilldown: Var_drilldown,

            plotOptions: {
                //allowhtml: true,
                //useHTML: true,
                series: {
                    showInLegend: true,
                    dataLabels: {
                        enabled: true,
                        overflow: "justify",
                        formatter: function () {
                            return this.y + ' %';//this.point.name + ' : ' + this.y + ' %';
                        }
                    },
                    shadow: true
                },
            },
            series: Var_Series
        };


        options.chart.renderTo = 'ctl00_ContentPlaceHolder1_EmployeeSummary1_container';
        options.chart.type = Var_ChartType;  //Type Of Charts ========= column,line,pie,bar,area,scatter -- (xy),stock,surface,doughnut,bubble



        var chart1 = new Highcharts.Chart(options);

    });

    function generateDrillDownJSON(e, isDrillUp) {
        try {
            if (isDrillUp) {
                if (myJSON != null && myJSON.length > 0) {
                    removeArrayElementByIndex(myJSON, myJSON.length - 1);
                }
                sessionStorage.setItem('DrillDownJSON', JSON.stringify(myJSON));
                $("#jsonContent").html('JSON content is: ').append(JSON.stringify(myJSON));
            } else {
                myJSON.push({
                    name: e.point.name,
                    level: myJSON.length + 1,
                });
                sessionStorage.setItem('DrillDownJSON', JSON.stringify(myJSON));
                $("#jsonContent").html('JSON content is: ').append(JSON.stringify(myJSON));
            }
        } catch (e) {
            console.log('generateHierarchyJSON :' + e.message);
        }
    }

    function removeArrayElementByIndex(myArray, index) {
        myArray.splice(index, 1);
    }

</script>


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
            url: 'Inner.aspx/GetEmployees_Entities',
            //data: '{QuestionId: "' + $find('ctl00_ContentPlaceHolder1_SurveyResult_rdQuestion').get_selectedItem().get_value() + '"}',
            contentType: 'application/json; charset= utf-8',
            dataType: 'json',
            success: function (r) {
                var json = JSON.parse(r.d)




                $.ajax({
                    type: 'POST',
                    url: 'Inner.aspx/GetEmployees_Entities',
                    //data: '{QuestionId: "' + $find('ctl00_ContentPlaceHolder1_SurveyResult_rdQuestion').get_selectedItem().get_value() + '"}',
                    contentType: 'application/json; charset= utf-8',
                    dataType: 'json',
                    success: function (r) {
                        var json = JSON.parse(r.d)


                        for (var i = 0; i < json.length ; i++) {
                            DoughnutLabels.push(json[i].EntityName)
                            DoughnutData.push(json[i].EmployeeCount)
                            DoughnutColors.push(getRandomColor())
                        }

                        for (var i = 0; i < json.length ; i++) {
                            horizontalBarLabels.push(json[i].EntityName)
                            horizontalBarData.push(json[i].EmployeeCount)
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
            var barChartData = {
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
    <div class="col-lg-3 col-md-3 col-xs-6">
        <!-- small box -->
        <div class="small-box bg-aqua">
            <div class="inner">
                <h3>
                    <asp:Label ID="lblEmployeesVal" runat="server" meta:resourcekey="lblEmployeesValResource1"></asp:Label>
                </h3>
                <p>
                    <asp:Label ID="lblEmployees" runat="server" Text="Employees" meta:resourcekey="lblEmployeesResource1"></asp:Label>
                </p>
            </div>
            <div class="icon">
                <i class="fa fa-users"></i>
            </div>
            <asp:LinkButton ID="lnkEmployeeDetails" runat="server" Text="Show Employees" class="small-box-footer" OnClientClick="LoadCharts();" meta:resourcekey="lnkEmployeeDetailsResource1"></asp:LinkButton>
        </div>
    </div>
    <!-- ./col -->
    <div class="col-lg-3 col-md-3 col-xs-6">
        <!-- small box -->
        <div class="small-box bg-yellow">
            <div class="inner">
                <h3>
                    <asp:Label ID="lblLeavesVal" runat="server" meta:resourcekey="lblLeavesValResource1"></asp:Label>
                </h3>
                <p>
                    <asp:Label ID="lblLeaves" runat="server" Text="Leaves" meta:resourcekey="lblLeavesResource1"></asp:Label>
                </p>
            </div>
            <div class="icon">
                <i class="fa-icon2"></i>
            </div>
            <asp:LinkButton ID="lnkLeavesDetails" runat="server" Text="Show Employees" class="small-box-footer" meta:resourcekey="lnkLeavesDetailsResource1"></asp:LinkButton>
        </div>
    </div>
    <!-- ./col -->
    <div class="col-lg-3 col-md-3 col-xs-6">
        <!-- small box -->
        <div class="small-box bg-green">
            <div class="inner">
                <h3>
                    <asp:Label ID="lblPermissionsVal" runat="server" meta:resourcekey="lblPermissionsValResource1"></asp:Label></h3>
                <p>
                    <asp:Label ID="lblPermissions" runat="server" Text="Permissions" meta:resourcekey="lblPermissionsResource1"></asp:Label>
                </p>
            </div>
            <div class="icon">
                <i class="fa-icon3"></i>
            </div>
            <asp:LinkButton ID="lnkPermissionsDetails" runat="server" Text="Show Employees" class="small-box-footer" meta:resourcekey="lnkPermissionsDetailsResource1"></asp:LinkButton>
        </div>
    </div>
    <!-- ./col -->
    <div class="col-lg-3 col-md-3 col-xs-6">
        <!-- small box -->
        <div class="small-box bg-red">
            <div class="inner">
                <h3>
                    <asp:Label ID="lblStudy_NursingVal" runat="server" meta:resourcekey="lblStudy_NursingValResource1"></asp:Label></h3>
                <p>
                    <asp:Label ID="lblStudy_Nursing" runat="server" Text="Study & Nursing" meta:resourcekey="lblStudy_NursingResource1"></asp:Label>
                </p>
            </div>
            <div class="icon">
                <i class="fa-icon4"></i>
            </div>
            <asp:LinkButton ID="lnkStudy_NursingDetails" runat="server" Text="Show Employees" class="small-box-footer" meta:resourcekey="lnkStudy_NursingDetailsResource1"></asp:LinkButton>
        </div>
    </div>
    <!-- ./col -->
</div>

<div class="row">
    <div class="col-md-12" runat="server" id="dvEmployees" visible="false">
        <div id="dvCharts" runat="server">
            <div class="fancy-collapse-panel">
                <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                    <div class="panel panel-default">
                        <div class="panel-heading" role="tab" id="headingOne">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                    <asp:Label ID="lblActiveEmployees" runat="server" Text="Employees & Entities" meta:resourcekey="lblActiveEmployeesResource1"></asp:Label>
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
                                                <asp:Label ID="lblEmployeeNo" Text="Employees Number:" runat="server" meta:resourcekey="lblEmployeeNoResource1" />
                                                <asp:Label ID="lblEmployeeNoVal" runat="server" meta:resourcekey="lblEmployeeNoValResource1" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="lblEntityNo" Text="Entities Number:" runat="server" meta:resourcekey="lblEntityNoResource1" />
                                                <asp:Label runat="server" ID="lblEntityNoVal" meta:resourcekey="lblEntityNoValResource1" />
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
    </div>
    <div class="col-md-12" runat="server" id="dvLeaves" visible="false">
        <div class="table-responsive">
            <telerik:RadGrid ID="dgrdLeaves" runat="server" AllowPaging="True"
                GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True"
                PageSize="25" CellSpacing="0" meta:resourcekey="dgrdLeavesResource1">
                <GroupingSettings CaseSensitive="False" />
                <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="EmployeeNo" SortExpression="EmployeeNo"
                            HeaderText="Employee No" UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource1" />
                        <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName"
                            HeaderText="Employee Name" UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource2" />
                        <telerik:GridBoundColumn DataField="EmployeeArabicName" SortExpression="EmployeeArabicName"
                            HeaderText="Employee Arabic Name" UniqueName="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource3" />
                        <telerik:GridBoundColumn DataField="LeaveName" SortExpression="LeaveName"
                            HeaderText="Leave Name" UniqueName="LeaveName" meta:resourcekey="GridBoundColumnResource4" />
                        <telerik:GridBoundColumn DataField="LeaveArabicName" SortExpression="LeaveArabicName"
                            HeaderText="Leave Arabic Name" UniqueName="LeaveArabicName" meta:resourcekey="GridBoundColumnResource5" />
                        <telerik:GridBoundColumn DataField="FromDate" SortExpression="FromDate"
                            HeaderText="From Date" UniqueName="FromDate" DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource6" />
                        <telerik:GridBoundColumn DataField="ToDate" SortExpression="ToDate"
                            HeaderText="To Date" UniqueName="ToDate" DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource7" />
                        <telerik:GridBoundColumn DataField="EntityName" SortExpression="EntityName"
                            HeaderText="Entity Name" UniqueName="EntityName" meta:resourcekey="GridBoundColumnResource8" />
                        <telerik:GridBoundColumn DataField="EntityArabicName" SortExpression="EntityArabicName"
                            HeaderText="Entity Arabic Name" UniqueName="EntityArabicName" meta:resourcekey="GridBoundColumnResource9" />
                    </Columns>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                </MasterTableView>
                <SelectedItemStyle ForeColor="Maroon" />
            </telerik:RadGrid>
        </div>
    </div>
    <div class="col-md-12" runat="server" id="dvPermissions" visible="false">
        <div class="table-responsive">
            <telerik:RadGrid ID="dgrdPermissions" runat="server" AllowPaging="True"
                GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True"
                PageSize="25" CellSpacing="0" meta:resourcekey="dgrdPermissionsResource1">
                <GroupingSettings CaseSensitive="False" />
                <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="EmployeeNo" SortExpression="EmployeeNo"
                            HeaderText="Employee No" UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource10" />
                        <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName"
                            HeaderText="Employee Name" UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource11" />
                        <telerik:GridBoundColumn DataField="EmployeeArabicName" SortExpression="EmployeeArabicName"
                            HeaderText="Employee Arabic Name" UniqueName="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource12" />
                        <telerik:GridBoundColumn DataField="PermName" SortExpression="PermName"
                            HeaderText="Permission Name" UniqueName="PermName" meta:resourcekey="GridBoundColumnResource13" />
                        <telerik:GridBoundColumn DataField="PermArabicName" SortExpression="PermArabicName"
                            HeaderText="Permission Arabic Name" UniqueName="PermArabicName" meta:resourcekey="GridBoundColumnResource14" />
                        <telerik:GridBoundColumn DataField="FromDate" SortExpression="FromDate"
                            HeaderText="From Date" UniqueName="FromDate" DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource15" />
                        <telerik:GridBoundColumn DataField="ToDate" SortExpression="ToDate"
                            HeaderText="To Date" UniqueName="ToDate" DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource16" />
                        <telerik:GridBoundColumn DataField="EntityName" SortExpression="EntityName"
                            HeaderText="Entity Name" UniqueName="EntityName" meta:resourcekey="GridBoundColumnResource17" />
                        <telerik:GridBoundColumn DataField="EntityArabicName" SortExpression="EntityArabicName"
                            HeaderText="Entity Arabic Name" UniqueName="EntityArabicName" meta:resourcekey="GridBoundColumnResource18" />
                    </Columns>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                </MasterTableView>
                <SelectedItemStyle ForeColor="Maroon" />
            </telerik:RadGrid>
        </div>
    </div>
    <div class="col-md-12" runat="server" id="dvStudyNursing" visible="false">
        <div class="table-responsive">
            <telerik:RadGrid ID="dgrdStudyNursing" runat="server" AllowPaging="True"
                GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True" ShowFooter="True"
                PageSize="25" CellSpacing="0" meta:resourcekey="dgrdStudyNursingResource1">
                <GroupingSettings CaseSensitive="False" />
                <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="True">
                    <Selecting AllowRowSelect="True" />
                </ClientSettings>
                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False">
                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="EmployeeNo" SortExpression="EmployeeNo"
                            HeaderText="Employee No" UniqueName="EmployeeNo" meta:resourcekey="GridBoundColumnResource19" />
                        <telerik:GridBoundColumn DataField="EmployeeName" SortExpression="EmployeeName"
                            HeaderText="Employee Name" UniqueName="EmployeeName" meta:resourcekey="GridBoundColumnResource20" />
                        <telerik:GridBoundColumn DataField="EmployeeArabicName" SortExpression="EmployeeArabicName"
                            HeaderText="Employee Arabic Name" UniqueName="EmployeeArabicName" meta:resourcekey="GridBoundColumnResource21" />
                        <telerik:GridBoundColumn DataField="PermName" SortExpression="PermName"
                            HeaderText="Permission Name" UniqueName="PermName" meta:resourcekey="GridBoundColumnResource22" />
                        <telerik:GridBoundColumn DataField="PermArabicName" SortExpression="PermArabicName"
                            HeaderText="Permission Arabic Name" UniqueName="PermArabicName" meta:resourcekey="GridBoundColumnResource23" />
                        <telerik:GridBoundColumn DataField="FromDate" SortExpression="FromDate"
                            HeaderText="From Date" UniqueName="FromDate" DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource24" />
                        <telerik:GridBoundColumn DataField="ToDate" SortExpression="ToDate"
                            HeaderText="To Date" UniqueName="ToDate" DataFormatString="{0:dd/MM/yyyy}" meta:resourcekey="GridBoundColumnResource25" />
                        <telerik:GridBoundColumn DataField="EntityName" SortExpression="EntityName"
                            HeaderText="Entity Name" UniqueName="EntityName" meta:resourcekey="GridBoundColumnResource26" />
                        <telerik:GridBoundColumn DataField="EntityArabicName" SortExpression="EntityArabicName"
                            HeaderText="Entity Arabic Name" UniqueName="EntityArabicName" meta:resourcekey="GridBoundColumnResource27" />
                    </Columns>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                </MasterTableView>
                <SelectedItemStyle ForeColor="Maroon" />
            </telerik:RadGrid>
        </div>
    </div>
</div>

<div class="row">
    <div id="container" class="highchartsalignment" visible="false" runat="server">
    </div>
    <div id="jsonContent" runat="server">
    </div>
</div>



