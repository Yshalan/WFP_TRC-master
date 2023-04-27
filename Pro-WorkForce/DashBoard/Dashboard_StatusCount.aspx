<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false" CodeFile="Dashboard_StatusCount.aspx.vb"
    Theme="SvTheme" Inherits="DashBoard_Dashboard_StatusCount" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="UserSecurityFilter"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../Dashboard/js1/highcharts.js"></script>
    <script src="../Dashboard/js1/series-label.js"></script>
    <script src="../Dashboard/js1/exporting.js"></script>
    <script src="../Dashboard/js1/export-data.js"></script>
    <script src="../Dashboard/js1/highcharts-more.js"></script>
    <script src="../Dashboard/js1/solid-gauge.src.js"></script>


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


    <script type="text/javascript" charset="utf-8">
        var lang = '<%=chartLang%>';

        $(document).ready(function () {
            GetEmployeeWorkingHours();
            GetStatusCount();
            GetRequestStatusCount();
            GetTransactionStats();
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

        function GetRequestStatusCount() {

            Highcharts.chart('RequestStatusCount_container', {
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
                    text: Var_RequestStatusCountChartTitleText
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
                series: Var_RequestStatusCountSeries
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


        function GetTransactionStats() {

            if (lang == 'ar') {
                Highcharts.setOptions({
                    lang: {
                        months: [
                            'يناير', 'فبراير', 'مارس', 'ابريل',
                            'مايو', 'يونيو', 'يوليو', 'اغسطس',
                            'سبتمبر', 'اكتوبر', 'نوفيمبر', 'ديسمبر'
                        ],

                        shortMonths: ['يناير', 'فبراير', 'مارس', 'ابريل',
                            'مايو', 'يونيو', 'يوليو', 'اغسطس',
                            'سبتمبر', 'اكتوبر', 'نوفيمبر', 'ديسمبر'],

                        weekdays: [
                            'الاحد', 'الاثنين', 'الثلاثاء', 'الاربعاء',
                            'الخميس', 'الجمعة', 'السبت'
                        ]
                    }
                });
            }

            Highcharts.chart('TransactionStatsContainer', {

                legend: {
                    enabled: false,
                    align: 'right',
                    verticalAlign: 'middle'
                },
                credits: {
                    enabled: false
                },
                exporting: {
                    enabled: false
                },

                title: {
                    text: Var_TransactionStatsChartTitleText
                },

                subtitle: {
                    text: ' '
                },

                yAxis: {
                    title: {
                        text: Var_TransactionStatsChartSubTitleText
                    }
                },

                xAxis: {
                    type: 'datetime',
                    tickInterval: 24 * 3600 * 1000,
                    min: Date.UTC(Var_JSONminYear, Var_JSONminMonth, Var_JSONminDay),
                    max: Date.UTC(Var_JSONmaxYear, Var_JSONmaxMonth, Var_JSONmaxDay),
                },

                legend: { "layout": "vertical", "align": "right", "verticalAlign": "middle", "enabled": true },

                plotOptions: {
                    series: {
                        label: {
                            connectorAllowed: false
                        },

                        pointStart: Date.UTC(Var_JSONminYear, Var_JSONminMonth, Var_JSONminDay),
                        pointInterval: 24 * 3600 * 1000,
                    },

                    line: {
                        dataLabels: {
                            enabled: true
                        },
                        enableMouseTracking: true
                    },

                },

                series: Var_TransactionStatsSeries,

                responsive: {
                    rules: [{
                        condition: {
                            maxWidth: 500
                        },
                        chartOptions: {
                            legend: {
                                enabled: false
                            }
                        }
                    }]
                }

            });
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc3:PageHeader ID="pageheader1" runat="server" />
    <div class="row">
        <div class="col-md-12">
            <uc1:UserSecurityFilter ID="UserSecurityFilter1" runat="server" ValidationGroup="grpSave" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lbldate" runat="server" Text="From Date" Class="Profiletitletxt" meta:resourcekey="lbldateResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadDatePicker ID="rdpFromDate" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker1Resource1"
                Width="180px">
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
    <div class="row">
        <div class="col-md-2">
            <asp:Label ID="lblToDate" runat="server" Text="To Date" Class="Profiletitletxt" meta:resourcekey="lblToDateResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadDatePicker ID="rdpToDate" runat="server" Culture="en-US" meta:resourcekey="RadDatePicker1Resource1">
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
    <div class="row">
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
        <div class="col-md-6">
            <div id="StatusCount_container" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto" class="highchartsalignment"></div>
        </div>
        <div class="col-md-6">
            <div id="RequestStatusCount_container" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto" class="highchartsalignment"></div>
        </div>
    </div>

<%--    <div class="row">
        <div class="col-md-4">
            <div class="col-md-4">
                <div class="row">
                    <div class="employee-status">
                        <div class="present">
                            <div class="top">
                                <asp:Label ID="lblChkIn" runat="server" Text="Checked In"></asp:Label>
                                <span class="dot"></span>
                            </div>
                            <div class="mid">
                                <label class="counter ng-binding" runat ="server" id="lblChkInVal"></label>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblChkOut" runat="server" Text="Checked Out"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblChkOutVal" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblAbsent" runat="server" Text="Absent"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblAbsentVal" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblLeave" runat="server" Text="Leaves"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblLeaveVal" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblPermission" runat="server" Text="Permissions"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblPermissionVal" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblNursingPermission" runat="server" Text="Nursing Permissions"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblNursingPermissionVal" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblStudyPermission" runat="server" Text="Study Permissions"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblStudyPermissionVal" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblDelay" runat="server" Text="Delay"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblDelayVal" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblEarlyOut" runat="server" Text="Early Out"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblEarlyOutVal" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblLeaveRequest" runat="server" Text="Leave Request"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblLeaveRequestval" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblNursingPermissionRequest" runat="server" Text="Nursing Permission Request"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblNursingPermissionRequestVal" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblManualEntryRequest" runat="server" Text="Manual Entry Request"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblManualEntryRequestVal" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblPermissionRequest" runat="server" Text="Permission Request"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblPermissionRequestVal" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblStudyPermissionRequest" runat="server" Text="Study Permission Request"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblStudyPermissionRequestVal" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8">
                        <asp:Label ID="lblUpdateTransactionRequest" runat="server" Text="Update Transaction Request"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblUpdateTransactionRequestVal" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

    <div class="row">
        <div class="col-md-12">
            <div id="WorkingHoursContainer" style="min-width: 310px; height: 400px; margin: 0 auto"></div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div id="TransactionStatsContainer" style="min-width: 310px; height: 400px; margin: 0 auto" class="highchartsalignment"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts" runat="Server">
</asp:Content>

