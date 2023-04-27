<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="EmployeeViolations.aspx.vb" Inherits="SelfServices_EmployeeViolations"
    meta:resourcekey="PageResource1" UICulture="auto" Theme="SvTheme" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="UserSecurityFilter"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc3" %>
<%@ Register Src="UserControls/SummaryPage.ascx" TagName="SummaryPage" TagPrefix="uc4" %>
<%@ Register Src="UserControls/ScheduleInfo.ascx" TagName="ScheduleInfo" TagPrefix="uc2" %>
<%@ Register Src="UserControls/ManagerCalendar.ascx" TagName="ManagerCalendar" TagPrefix="uc5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../Svassets/css/fullcalendar.css" rel="stylesheet" type="text/css" />

    <asp:Literal runat="server" ID="JSONdrilldown" meta:resourcekey="JSONResource1" />
    <asp:Literal runat="server" ID="JSONSeries" meta:resourcekey="JSONResource1" />
    <asp:Literal runat="server" ID="JSONChartType" meta:resourcekey="JSONResource1" />
    <asp:Literal runat="server" ID="JSONChartTitleText" meta:resourcekey="JSONResource1" />
    <asp:Literal runat="server" ID="JSONChartSubTitleText" meta:resourcekey="JSONResource1" />

    <asp:Literal runat="server" ID="JSONEmployeeWorkingHoursCountSeries" meta:resourcekey="JSONEmployeeWorkingHoursCountSeriesResource1" />
    <asp:Literal runat="server" ID="JSONEmployeeWorkingHoursCountChartType" meta:resourcekey="JSONEmployeeWorkingHoursCountChartTypeResource1" />
    <asp:Literal runat="server" ID="JSONEmployeeWorkingHoursCountChartTitleText" meta:resourcekey="JSONEmployeeWorkingHoursCountChartTitleTextResource1" />
    <asp:Literal runat="server" ID="JSONEmployeeWorkingHoursCountChartSubTitleText" meta:resourcekey="JSONEmployeeWorkingHoursCountChartSubTitleTextResource1" />
    <asp:Literal runat="server" ID="JSONEmployeeWorkingHoursCountChartDateText" meta:resourcekey="JSONEmployeeWorkingHoursCountChartDateTextResource1" />



    <script src="../DashBoard/js/jquery-2.0.2.min.js" type="text/javascript"></script>
    <script src="../DashBoard/js/highcharts.js" type="text/javascript"></script>
    <script src="../DashBoard/js/drilldown.js" type="text/javascript"></script>
    <script src="../DashBoard/js/exporting.js" type="text/javascript"></script>



    <script language="javascript" type="text/javascript">

        function RefreshPage() {
            window.location = "../SelfServices/PermissionRequest.aspx";

        }
        function validate(sender, args) {

            var RadTimePicker1 = $find("<%= RadTPfromTime.ClientID %>");
            var RadTimePicker2 = $find("<%= RadTPtoTime.ClientID %>");
            var validator = document.getElementById("<%= hdnIsvalid.ClientID %>");
            var Date1 = new Date(RadTimePicker1.get_selectedDate());
            var Date2 = new Date(RadTimePicker2.get_selectedDate());
            //args.IsValid = true;
            if ((Date2 - Date1) < 0) {
                //alert("The to time value should be greater than the from time value");
                args.IsValid = false;
                validator.value = false;
            }
            else {
                args.IsValid = true;
                validator.value = true;
            }
        }

        function SetTimeFormat() {
            var picker = $("#RadTPfromTime").data("tDateTimePicker");
            picker.timeView.format = "HH:mm";
            picker.timeView.bind();
        }

        function Devidevalidate(sender, args) {

            var RadTimePicker1 = $find("<%= RadTPDevidefromTime.ClientID %>");
            var RadTimePicker2 = $find("<%= RadTPDevidetoTime.ClientID %>");
            var validator = document.getElementById("<%= hdnDevideIsvalid.ClientID %>");
            var Date1 = new Date(RadTimePicker1.get_selectedDate());
            var Date2 = new Date(RadTimePicker2.get_selectedDate());
            //args.IsValid = true;
            if ((Date2 - Date1) < 0) {
                //alert("The to time value should be greater than the from time value");
                args.IsValid = false;
                validator.value = false;
            }
            else {
                args.IsValid = true;
                validator.value = true;
            }
        }

        function SetTimeFormat() {
            var picker = $("#RadTPDevidefromTime").data("tDateTimePicker");
            picker.timeView.format = "HH:mm";
            picker.timeView.bind();
        }
        function openRadWin(ID) {


            oWindow = radopen("../Default/AnnouncementsDetailsPopup.aspx?AnnouncementID=" + ID, "RadWindow1");

        }
        var myJSON = [];
        $(document).ready(function () {
            if (document.getElementById("ctl00_ContentPlaceHolder1_container") !== null) {
                //            debugger;
                // Internationalization    
                Highcharts.setOptions({
                    lang: {
                        drillUpText: '◁ Back to {series.name}'
                    }
                });
                var options = {
                    chart: {
                        style: {
                            fontFamily: 'arial'
                        },
                        height: 300,

                    },
                    title: {
                        text: Var_ChartTitleText,
                    },
                    credits: {
                        enabled: false
                    },
                    subtitle: {
                        text: Var_ChartSubtitleText,
                    },
                    tooltip: {
                        formatter: function () {
                            //return ' % ' + this.point.name + ' : ' + this.y;
                            return this.y + ' % ' + ' : ' + this.point.name;
                        }
                    },
                    xAxis: {
                        categories: true
                    },

                    legend: {
                        enabled: true,
                    },
                    plotOptions: {
                        series: {
                            dataLabels: {
                                enabled: true,
                                formatter: function () {
                                    //return ' % ' + this.point.name + ' : ' + this.y;
                                    return this.y + ' % ' + ' : ' + this.point.name;
                                }
                            },
                            shadow: true
                        },
                        pie: {
                            size: '70%',
                            showInLegend: true
                        }
                    },

                    series: Var_Series
                };
                // Drill Down Chart Implementation    
                options.chart.renderTo = 'ctl00_ContentPlaceHolder1_container';
                options.chart.type = Var_ChartType;  //Type Of Charts ========= column,line,pie,bar,area,scatter -- (xy),stock,surface,doughnut,bubble
                var chart1 = new Highcharts.Chart(options);
                //            chart1.series: [{name:   'Overview',colorByPoint: true,data: [{name:   'Fruits',  y: 100,drilldown:  'fruits'}, {name:   'Cars',y: 12,drilldown:  'cars'  }, {name:   'Countries',y: 8}]  }]
                //            alert('hereeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee');
                //                alert(Shifts);
            }
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

        function formatDate(date) {
            var d = new Date(date),
                month = '' + (d.getMonth() + 1),
                day = '' + d.getDate(),
                year = d.getFullYear();

            if (month.length < 2) month = '0' + month;
            if (day.length < 2) day = '0' + day;

            return [year, month, day].join('-');
        }

        function GetEmployeeWorkingHours() {
            Highcharts.chart('WorkHourscontainer', {
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
                series: Var_EmployeeWorkingHoursCountSeries//[{ name: '< 5 Hrs', data: [1483,] }, { name: '6-7 Hrs', data: [18,] }, { name: '7-8 Hrs', data: [13,] }, { name: '8-9 Hrs', data: [2,] }, { name: '> 10 Hrs', data: [5,] },]

            });

        }

        $(document).ready(function () {
            if (document.getElementById("ctl00_ContentPlaceHolder1_WorkHourscontainer") !== null) {
                //            debugger;
                // Internationalization    
                Highcharts.setOptions({
                    lang: {
                        drillUpText: '◁ Back to {series.name}'
                    }
                });


                var options = {
                    chart: {
                        style: {
                            fontFamily: 'arial'
                        },
                        height: 300,

                    },
                    title: {
                        text: Var_EmployeeWorkingHoursCountChartSubTitleText,
                    },
                    credits: {
                        enabled: false
                    },
                    subtitle: {
                        text: Var_EmployeeWorkingHoursCountChartDateText,
                    },
                    tooltip: {
                        formatter: function () {
                            //return ' % ' + this.point.name + ' : ' + this.y;
                            return this.y + ' % ' + ' : ' + this.point.name;
                        }
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

                    legend: {
                        enabled: true,
                    },
                    plotOptions: {
                        column: {
                            pointPadding: 0.2,
                            borderWidth: 0
                        }
                    },

                    series: Var_EmployeeWorkingHoursCountSeries
                };
                // Drill Down Chart Implementation    
                options.chart.renderTo = 'ctl00_ContentPlaceHolder1_WorkHourscontainer';
                options.chart.type = 'column';
                var chart1 = new Highcharts.Chart(options);
            }
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel runat="server">
        <ContentTemplate>--%>

    <uc3:PageHeader ID="PageHeader1" runat="server" />


    <asp:MultiView ID="mvEmpViolations" runat="server">
        <asp:View ID="viewEmpViolationsList" runat="server">
            <div class="row" id="dvScheduleInfo" runat="server">
                <div class="col-md-12">
                    <uc2:ScheduleInfo ID="ScheduleInfo1" runat="server" />
                </div>
            </div>
            <div class="clear space-sm"></div>
            <div class="row">
                <div class="col-md-7">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lbldate" runat="server" Text="From Date" Class="Profiletitletxt" meta:resourcekey="lbldateResource1" />
                            <telerik:RadDatePicker ID="dteFromDate" runat="server" Culture="en-US"
                                meta:resourcekey="dteFromDateResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                    Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblToDate" runat="server" Text="To Date" Class="Profiletitletxt" meta:resourcekey="lblToDateResource1" />
                            <telerik:RadDatePicker ID="dteToDate" runat="server" Culture="en-US"
                                meta:resourcekey="dteToDateResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                    Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:CompareValidator ID="CVDate" runat="server" ControlToCompare="dteFromDate" ControlToValidate="dteToDate"
                                ErrorMessage="End Date should be greater than or equal to From Date" Display="None"
                                Operator="GreaterThanEqual" Type="Date" ValidationGroup="grpSave" meta:resourcekey="CVDateResource1" />
                            <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender2"
                                runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="Label3" runat="server" Text="&nbsp;" />
                            <asp:Button runat="server" ID="btnRetrieve" Text="Retrieve" ValidationGroup="grpSave"
                                CssClass="button" meta:resourcekey="btnRetrieveResource1" />
                        </div>
                    </div>
                </div>
                <div class="col-md-5" id="divAnnouncements" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="fancy-collapse-panel">
                                <div class="panel-group" id="Div3" role="tablist" aria-multiselectable="true">
                                    <div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="Div4">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#Div5" href="#collapseOne2" aria-expanded="true" aria-controls="collapseOne">
                                                    <asp:Image ID="Image3" ImageUrl="~/assets/img/Announcements.png" runat="server" />
                                                    <span id="spanheader" runat="server">Announcements</span>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id="collapseOne2" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                            <div class="panel-body">
                                                <div class="Svpanel Announcements" style="padding: 0px 0px; margin: 0px;">
                                                    <ul style="margin: 0px;">
                                                        <asp:Repeater ID="repAnnouncement" runat="server">
                                                            <ItemTemplate>
                                                                <li>
                                                                    <span class="date">
                                                                        <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("MonthValue")%>' />
                                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("DayNo")%>' />
                                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' Visible="false" CssClass="hidden" />
                                                                    </span>
                                                                    <span class="description">

                                                                        <asp:LinkButton ID="lnkDescription" Text='<%# Eval("Title")%>' OnClientClick='<%# Eval("ID", "openRadWin({0});return false;") %>' runat="server" />
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
                                        <%--  <ul>
                               <li>
                                   <span class="date">
                                       <span>Jan</span>
                                       <span>12</span>
                                   </span>
                                   <span class="description">
                                       <asp:LinkButton ID="Label1" runat="server" Text="Label">
                                           It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.
                                       </asp:LinkButton>
                                   </span>
                               </li>
                                   <li>
                                   <span class="date">
                                       <span>Jan</span>
                                       <span>12</span>
                                   </span>
                                   <span class="description">
                                       <asp:LinkButton ID="Label14" runat="server" Text="Label">
                                           It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.
                                       </asp:LinkButton>
                                   </span>
                               </li>

                                   <li>
                                   <span class="date">
                                       <span>Jan</span>
                                       <span>12</span>
                                   </span>
                                   <span class="description">
                                       <asp:LinkButton ID="Label15" runat="server" Text="Label">
                                           It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.
                                       </asp:LinkButton>
                                   </span>
                               </li>

                                   <li>
                                   <span class="date">
                                       <span>Jan</span>
                                       <span>12</span>
                                   </span>
                                   <span class="description">
                                       <asp:LinkButton ID="Label16" runat="server" Text="Label">
                                           It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.
                                       </asp:LinkButton>
                                   </span>
                               </li>

                                
                           </ul>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12" id="dvSummaryandDashboard" runat="server">
                    <div class="fancy-collapse-panel">
                        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="headingOne">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                            <asp:Label ID="lblSummaryandDashboard" runat="server" Text="Summary and Dashboard" meta:resourcekey="lblSummaryandDashboardResource1"></asp:Label>

                                        </a>
                                    </h4>
                                </div>
                                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-4" id="dvSummaryPage" runat="server">
                                                <div class="summaryarea">
                                                    <uc4:SummaryPage ID="SummaryPage1" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-md-4" id="dvDashboard" runat="server">
                                                <div class="summaryarea margintop35">
                                                    <div id="container" class="highchartsalignment" visible="false" runat="server">
                                                    </div>
                                                    <div id="jsonContent" runat="server">
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-4" id="dvDashboardWorkHours" runat="server">
                                                <div class="summaryarea margintop35">
                                                    <div id="WorkHourscontainer" class="highchartsalignment" visible="false" runat="server">
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
                <div class="col-md-12" id="dvViolationCorrection" runat="server">
                    <div class="fancy-collapse-panel">
                        <div class="panel-group" id="Div1" role="tablist" aria-multiselectable="true">
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="Div2">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion1" href="#collapseOne1" aria-expanded="true" aria-controls="collapseOne" class="gridcolapse">

                                            <asp:Label ID="lblViolationCorrection" runat="server" Text="Violation Correction" Class="Profiletitletxt" meta:resourcekey="lblViolationCorrectionResource1" />
                                        </a>
                                    </h4>
                                </div>
                                <div id="collapseOne1" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                    <div class="panel-body">
                                        <div class="table-responsive">
                                            <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="dgrdEmpViolations"
                                                Skin="Hay" ShowApplyButton="False" meta:resourcekey="RadFilter1Resource1" />
                                            <telerik:RadGrid ID="dgrdEmpViolations" runat="server" AllowSorting="True" AllowPaging="True"
                                                GridLines="None" ShowStatusBar="True" AllowMultiRowSelection="True"
                                                PageSize="25" ShowFooter="True" meta:resourcekey="dgrdEmpViolationsResource1">
                                                <GroupingSettings CaseSensitive="False" />
                                                <ClientSettings EnablePostBackOnRowClick="false" EnableRowHoverStyle="True">
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                                <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="False" CommandItemDisplay="Top" DataKeyNames="M_DATE,Status,Description,Delay,OutTime,Duration,PermStart,PermEnd,BreakTime">
                                                    <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="M_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Violation Date"
                                                            meta:resourcekey="GridBoundColumnResource1" UniqueName="M_DATE">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Type" HeaderText="Violation Type" meta:resourcekey="GridBoundColumnResource2"
                                                            UniqueName="Type">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Duration" HeaderText="Violation Duration" meta:resourcekey="GridBoundColumnResource3"
                                                            UniqueName="Duration">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn AllowFiltering="False" DataField="FK_EmployeeId" meta:resourcekey="GridBoundColumnResource2"
                                                            UniqueName="FK_EmployeeId" Visible="False">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" meta:resourcekey="GridBoundColumnResource4"
                                                            UniqueName="Status" Visible="False">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Delay" HeaderText="Delay" meta:resourcekey="GridBoundColumnResource5"
                                                            UniqueName="Delay" Visible="False">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="IN_TIME" HeaderText="IN_TIME" UniqueName="IN_TIME"
                                                            Visible="False">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Early_Out" HeaderText="Early Out" meta:resourcekey="GridBoundColumnResource6"
                                                            UniqueName="Early_Out" Visible="False">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="OutTime" UniqueName="OutTime" Visible="False">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="PermStart" UniqueName="PermStart" DataFormatString="{0:HH:mm}" meta:resourcekey="GridBoundColumnResource7">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="PermEnd" UniqueName="PermEnd" DataFormatString="{0:HH:mm}" meta:resourcekey="GridBoundColumnResource8">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Description" HeaderText="Description" meta:resourcekey="GridBoundColumnResource6"
                                                            UniqueName="Description" Visible="False">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn meta:resourcekey="GridTemplateColumnResource1" UniqueName="TemplateColumn">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnbPermissionRequest" runat="server" CommandName="PermissionRequest"
                                                                    meta:resourcekey="lnbPermissionRequestResource1" OnClick="lnbPermissionRequest_Click"
                                                                    Text="Permission Request" />
                                                                <asp:LinkButton ID="lnbLeaveRequest" runat="server" CommandName="LeaveRequest" meta:resourcekey="lnbLeaveRequestResource1"
                                                                    OnClick="lnbLeaveRequest_Click" Text="Leave Request" />
                                                                <asp:HiddenField ID="hdnDuration" runat="server" Value='<%# Eval("Duration") %>' />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="BreakStart" HeaderText="BreakStart"
                                                            UniqueName="BreakStart" Display="False">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="BreakEnd" HeaderText="BreakEnd"
                                                            UniqueName="BreakEnd" Display="False">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="OutDuration" HeaderText="OutDuration"
                                                            UniqueName="OutDuration" Display="False">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="BreakTime" HeaderText="BreakTime"
                                                            UniqueName="BreakTime" Display="False">
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                    <CommandItemTemplate>
                                                        <telerik:RadToolBar ID="RadToolBar1" runat="server" meta:resourcekey="RadToolBar1Resource1"
                                                            OnButtonClick="RadToolBar1_ButtonClick" Skin="Hay">
                                                            <Items>
                                                                <telerik:RadToolBarButton runat="server" CommandName="FilterRadGrid" ImagePosition="Right"
                                                                    ImageUrl="~/images/RadFilter.gif" meta:resourcekey="RadToolBarButtonResource1"
                                                                    Owner="" Text="Apply filter" />
                                                            </Items>
                                                        </telerik:RadToolBar>
                                                    </CommandItemTemplate>
                                                </MasterTableView>
                                                <SelectedItemStyle ForeColor="Maroon" />
                                            </telerik:RadGrid>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="fancy-collapse-panel" id="dvCalendar" runat="server" visible="false">
                <div class="panel-group" id="Div5" role="tablist" aria-multiselectable="true">
                    <div class="panel panel-default">
                        <div class="panel-heading" role="tab" id="Div6">
                            <h4 class="panel-title">
                                <a data-toggle="collapse" data-parent="#accordion" href="#collapseclndr" aria-expanded="true" aria-controls="collapseOne">
                                    <asp:Label ID="lblCalendar" runat="server" Text="Employee Calendar" meta:resourcekey="lbllblCalendarResource1"></asp:Label>

                                </a>
                            </h4>
                        </div>
                        <div id="collapseclndr" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                            <div class="panel-body">
                                <div id="Div7" class="row" runat="server" visible="true">
                                    <div class="col-md-2 colorboxLeave">
                                        <asp:Label ID="lblCalLeaves" runat="server" Text="Leaves" meta:resourcekey="lblCalLeavesResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-2 colorboxLeavesRequest">
                                        <asp:Label ID="lblCalLeavesRequest" runat="server" Text="Leave Requests" meta:resourcekey="lblCalLeavesRequestResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-2 colorboxPermission">
                                        <asp:Label ID="lblCalPermissions" runat="server" Text="Permission" meta:resourcekey="lblCalPermissionsResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-2 colorboxPermissionsRequest">
                                        <asp:Label ID="lblCalPermissionsRequest" runat="server" Text="Permission Requests" meta:resourcekey="lblCalPermissionsRequestResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-2 colorboxNursingPermission">
                                        <asp:Label ID="lblCalNursingPermissions" runat="server" Text="Nursing Permission" meta:resourcekey="lblCalNursingPermissionsResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-2 colorboxStudyPermission">
                                        <asp:Label ID="lblCalStudyPermission" runat="server" Text="Study Permission" meta:resourcekey="lblCalStudyPermissionResource1"></asp:Label>
                                    </div>
                                    <div class="clear"></div>
                                    <div class="col-md-2 colorboxHoliday">
                                        <asp:Label ID="lblCalHoliday" runat="server" Text="Holiday" meta:resourcekey="lblCalHolidayResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-2 colorboxAbsent">
                                        <asp:Label ID="lblCalAbsent" runat="server" Text="Absent" meta:resourcekey="lblCalAbsentResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-2 colorboxViolations">
                                        <asp:Label ID="lblCalViolations" runat="server" Text="Violations" meta:resourcekey="lblCalViolationsResource1"></asp:Label>
                                    </div>
                                    <div class="col-md-2 colorboxRestDays">
                                        <asp:Label ID="lblCalRestDays" runat="server" Text="Rest Day" meta:resourcekey="lblCalRestDaysResource1"></asp:Label>
                                    </div>

                                    <%--<div class="clearfix">
                    </div>--%>
                                </div>
                                <br />
                                <div class="row">
                                    <div id="calendar"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <%--<div id="Manager_calendar" ></div>--%>
            </div>
        </asp:View>
        <asp:View ID="viewPermissionRequest" runat="server">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblPermission" runat="server" Text="Type"
                        meta:resourcekey="lblPermissionResource1" />
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="RadCmpPermissions" MarkFirstMatch="True" Skin="Vista" ToolTip="View types of employee permission"
                        runat="server" AutoPostBack="true" meta:resourcekey="RadCmpPermissionsResource1">
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="reqPermission" runat="server" ControlToValidate="RadCmpPermissions"
                        Display="None" ErrorMessage="Please select permission english name" InitialValue="--Please Select--"
                        ValidationGroup="EmpPermissionGroup" meta:resourcekey="reqPermissionResource1" />
                    <cc1:ValidatorCalloutExtender ID="ExtenderPermission" runat="server" CssClass="AISCustomCalloutStyle"
                        TargetControlID="reqPermission" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <%--<tr style="display: none">
                                                <td>
                                                    
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblGeneralGuidePermission" runat="server" CssClass="Profiletitletxt" />
                                                </td>
                                            </tr>--%>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-6" style="display: none">
                    <asp:RadioButton ID="radBtnOneDay" Text="One Time Permission" Checked="True" runat="server"
                        AutoPostBack="True" GroupName="LeaveGroup" meta:resourcekey="radBtnOneDayResource1" />
                    <asp:RadioButton ID="radBtnPeriod" Text="Permission For Period" runat="server" AutoPostBack="True"
                        GroupName="LeaveGroup" meta:resourcekey="radBtnPeriodResource1" Style="display: none" />
                </div>
            </div>
            <div class="row">
                <asp:Panel ID="PnlOneDayLeave" runat="server" meta:resourcekey="PnlOneDayLeaveResource1">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblPermissionDate" runat="server" Text="Date"
                            meta:resourcekey="lblPermissionDateResource1" />
                    </div>
                    <div class="col-md-4">
                        <asp:Label CssClass="Profiletitletxt" ID="lblAtDate" runat="server" Text="At" meta:resourcekey="lblAtDateResource1" />

                        <telerik:RadDatePicker ID="dtpPermissionDate" AllowCustomText="false" MarkFirstMatch="true"
                            Skin="Vista" runat="server" Culture="en-US" meta:resourcekey="dtpPermissionDateResource1"
                            Enabled="false">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                ToolTip="View permission date" Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="reqPermissionDate" runat="server" ControlToValidate="dtpPermissionDate"
                            Display="None" ErrorMessage="Please select permission date" ValidationGroup="EmpPermissionGroup"
                            meta:resourcekey="reqPermissionDateResource1" />
                        <cc1:ValidatorCalloutExtender ID="ExtenderPermissionDate" runat="server" TargetControlID="reqPermissionDate"
                            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                            Enabled="True">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlPeriodLeave" Visible="False" runat="server" meta:resourcekey="pnlPeriodLeaveResource1">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblDateFrom" runat="server" Text="Date"
                            meta:resourcekey="lblDateFromResource1" />
                    </div>
                    <div class="col-md-4">
                        <asp:Label CssClass="Profiletitletxt" ID="lblFromDate" runat="server" Text="From"
                            meta:resourcekey="lblFromDateResource1" />

                        <telerik:RadDatePicker ID="dtpStartDatePerm" ToolTip="Click" AllowCustomText="false"
                            MarkFirstMatch="true" Skin="Vista" runat="server" Culture="en-US" meta:resourcekey="dtpStartDatePermResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                ToolTip="View start date permission" Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="reqStartDate" runat="server" ControlToValidate="dtpStartDatePerm"
                            Display="None" ErrorMessage="Please select start date" ValidationGroup="EmpPermissionGroup"
                            meta:resourcekey="reqStartDateResource1" />
                        <cc1:ValidatorCalloutExtender ID="ExtenderStartDate" runat="server" TargetControlID="reqStartDate"
                            CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                            Enabled="True">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                    <div class="col-md-4">
                        <asp:Label CssClass="Profiletitletxt" ID="lblEndDate" runat="server" Text="To" meta:resourcekey="lblEndDateResource1" />

                        <telerik:RadDatePicker ID="dtpEndDatePerm" AllowCustomText="false" MarkFirstMatch="true"
                            Skin="Vista" runat="server" AutoPostBack="True" Culture="en-US" meta:resourcekey="dtpEndDatePermResource1">
                            <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                LabelCssClass="" ToolTip="View end date permission" Width="">
                            </DateInput>
                            <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="dtpStartDatePerm"
                            ControlToValidate="dtpEndDatePerm" Display="None" ErrorMessage="End date should be greater than or equal to Start date"
                            Operator="GreaterThanEqual" Type="Date" ValidationGroup="EmpPermissionGroup"
                            meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator><cc1:ValidatorCalloutExtender
                                ID="ValidatorCalloutExtender8" runat="server" CssClass="AISCustomCalloutStyle"
                                Enabled="True" TargetControlID="CompareValidator1" WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                    </div>
                </asp:Panel>
            </div>
            <br />
            <div class="row">
                <div id="trFullyDay" runat="server" visible="false">
                    <div class="col-md-2">
                        <%--<asp:Label ID="lblIsFullyDay" runat="server" Text="Is Fully Day" CssClass="Profiletitletxt" />--%>
                    </div>

                    <div class="col-md-4">
                        <asp:CheckBox ID="chckFullDay" runat="server" AutoPostBack="True" Text="Full Day" meta:resourcekey="chckFullDayResource1" />
                    </div>
                </div>
            </div>
            <div class="Svpanel">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label ID="lblRemainingBalance" runat="server" Text="Remaining Balance" CssClass="Profiletitletxt"
                            meta:resourcekey="lblRemainingBalanceResource1" />
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblRemainingBalanceValue" runat="server" CssClass="profiletitletxt" />
                    </div>
                    <div class="col-md-1">
                        <asp:Label ID="lblRemainingBalanceHours" runat="server" Text="Hours" CssClass="Profiletitletxt"
                            meta:resourcekey="lblRemainingBalanceHoursResource1" />
                    </div>
                </div>
            </div>
            <div id="dvTimeControls" runat="server" visible="true">
                <div class="Svpanel">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblTimeFrom" runat="server" Text="Time"
                                meta:resourcekey="lblTimeFromResource1" />
                        </div>

                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblFrom" Text="From" runat="server" meta:resourcekey="lblFromResource1"></asp:Label>

                            <telerik:RadTimePicker ID="RadTPfromTime" runat="server" AllowCustomText="false"
                                MarkFirstMatch="true" Skin="Vista" AutoPostBack="True" meta:resourcekey="RadTPfromTimeResource1"
                                AutoPostBackControl="TimeView" Enabled="false">
                                <DateInput ToolTip="View start time" DateFormat="HH:mm" AutoPostBack="True" DisplayDateFormat="HH:mm"
                                    LabelCssClass="" Width="" />
                            </telerik:RadTimePicker>
                            <asp:RequiredFieldValidator ID="reqFromtime" runat="server" ControlToValidate="RadTPfromTime"
                                Display="None" ErrorMessage="Please select start time" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="reqFromtimeResource1" />
                            <cc1:ValidatorCalloutExtender ID="ExtenderFromTime" runat="server" CssClass="AISCustomCalloutStyle"
                                TargetControlID="reqFromtime" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblTo" runat="server" Text="To" meta:resourcekey="lblToResource1"></asp:Label>

                            <telerik:RadTimePicker ID="RadTPtoTime" runat="server" AllowCustomText="false" MarkFirstMatch="true"
                                Skin="Vista" AutoPostBack="True" AutoPostBackControl="TimeView" meta:resourcekey="RadTPtoTimeResource1">
                                <DateInput ToolTip="View end time" DateFormat="HH:mm" AutoPostBack="True" DisplayDateFormat="HH:mm"
                                    LabelCssClass="" Width="" />
                            </telerik:RadTimePicker>
                            <asp:RequiredFieldValidator ID="reqToTime" runat="server" ControlToValidate="RadTPtoTime"
                                Display="None" ErrorMessage="Please select end time" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="reqToTimeResource1" />
                            <cc1:ValidatorCalloutExtender ID="ExtenderreqToTime" runat="server" CssClass="AISCustomCalloutStyle"
                                TargetControlID="reqToTime" WarningIconImageUrl="~/images/warning1.png" Enabled="True">
                            </cc1:ValidatorCalloutExtender>

                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="RadTPtoTime"
                                ClientValidationFunction="validate" meta:resourcekey="CustomValidator1Resource1"></asp:CustomValidator>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="RadTPfromTime"
                                ClientValidationFunction="validate" meta:resourcekey="CustomValidator2Resource1"></asp:CustomValidator>
                            <asp:HiddenField ID="hdnIsvalid" runat="server" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblPeriodInterval" runat="server" Text="Period"
                            meta:resourcekey="lblPeriodIntervalResource1" />
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtTimeDifference" ReadOnly="True" runat="server" meta:resourcekey="txtTimeDifferenceResource1" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblMinDurationPolicy" runat="server" Text="" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label1" is="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
                        meta:resourcekey="Label1Resource1" />
                </div>
                <div class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-btn">
                            <span class="btn btn-default" onclick="$(this).parent().find('input[type=file]').click();">Browse</span>
                            <asp:FileUpload ID="fuAttachFile" runat="server" meta:resourcekey="fuAttachFileResource1" name="uploaded_file" onchange="$(this).parent().parent().find('.form-control').html($(this).val().split(/[\\|/]/).pop());" Style="display: none;" type="file" />
                        </span>
                        <span class="form-control"></span>
                    </div>
                    <div class="veiw_remove">
                        <a id="lnbLeaveFile" target="_blank" runat="server" visible="False">
                            <asp:Label ID="lblView" runat="server" Text="View" meta:resourcekey="lblViewResource1" />
                        </a>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label CssClass="Profiletitletxt" ID="lblRemarks" runat="server" Text="Remarks"
                        meta:resourcekey="lblRemarksResource1" />
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" meta:resourcekey="txtRemarksResource1" />
                </div>
            </div>
            <div class="row">
                <asp:Panel ID="pnlTempHidRows" runat="server" Visible="False" meta:resourcekey="pnlTempHidRowsResource1">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblIsSpecifiedDays" runat="server" Text="Specified days"
                            meta:resourcekey="lblIsSpecifiedDaysResource1"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox ID="chckSpecifiedDays" runat="server" meta:resourcekey="chckSpecifiedDaysResource1" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblDays" runat="server" Text="Days" meta:resourcekey="lblDaysResource1"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtDays" runat="server" meta:resourcekey="txtDaysResource1"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblIsFlexible" runat="server" Text="Flexible"
                            meta:resourcekey="lblIsFlexibleResource1"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox ID="chckIsFlexible" runat="server" meta:resourcekey="chckIsFlexibleResource1" />
                    </div>
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblIsDividable" runat="server" Text="Dividable"
                            meta:resourcekey="lblIsDividableResource1"></asp:Label>
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox ID="chckIsDividable" runat="server" meta:resourcekey="chckIsDividableResource1" />
                    </div>
                </asp:Panel>
            </div>
            <div class="row">
                <div id="dvContainPermission" runat="server">
                    <asp:Label ID="lblGeneralGuidePermission" runat="server" CssClass="Profiletitletxt"
                        Text="General Guide" meta:resourcekey="lblGeneralGuidePermissionResource1" Visible="false" />
                </div>
                <div runat="server" id="dvPermissionGeneralGuide" style="background-color: #FDF5B8;">
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:LinkButton ID="lnbDevideTwoPermission" runat="server" Text="Devide into two permission"
                        meta:resourcekey="lnbDevideTwoPermissionResource1" />
                </div>
            </div>
            <asp:Panel ID="pnlDevideTwoPermission" runat="server" Visible="false">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblDevidePermission" runat="server" Text="Type"
                            meta:resourcekey="lblPermissionResource1" />
                    </div>
                    <div class="col-md-4">
                        <telerik:RadComboBox ID="RadCmpDevidePermissions" MarkFirstMatch="True" Skin="Vista"
                            ToolTip="View types of employee permission" runat="server" AutoPostBack="true"
                            meta:resourcekey="RadCmpPermissionsResource1">
                        </telerik:RadComboBox>
                        <asp:RequiredFieldValidator ID="reqDevidePermission" runat="server" ControlToValidate="RadCmpDevidePermissions"
                            Display="None" ErrorMessage="Please select permission english name" InitialValue="--Please Select--"
                            ValidationGroup="EmpPermissionGroup" meta:resourcekey="reqPermissionResource1" />
                        <cc1:ValidatorCalloutExtender ID="DevideExtenderPermission" runat="server" CssClass="AISCustomCalloutStyle"
                            TargetControlID="reqDevidePermission" WarningIconImageUrl="~/images/warning1.png"
                            Enabled="True">
                        </cc1:ValidatorCalloutExtender>
                    </div>
                </div>
                <%--<tr>
                                                                <td>
                                                                    
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblDevideGeneralGuidePermission" runat="server" CssClass="Profiletitletxt" />
                                                                </td>
                                                            </tr>--%>
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-4" style="display: none">
                        <asp:RadioButton ID="radDevideBtnOneDay" Text="One Time Permission" Checked="True"
                            runat="server" AutoPostBack="True" GroupName="LeaveGroup" meta:resourcekey="radBtnOneDayResource1" />
                        <asp:RadioButton ID="radDevideBtnPeriod" Text="Permission For Period" runat="server"
                            AutoPostBack="True" GroupName="LeaveGroup" meta:resourcekey="radBtnPeriodResource1"
                            Style="display: none" />
                    </div>
                </div>
                <asp:Panel ID="PnlDevideOneDayLeave" runat="server" meta:resourcekey="PnlOneDayLeaveResource1">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblDevidePermissionDate" runat="server"
                                Text="Date" meta:resourcekey="lblPermissionDateResource1" />
                        </div>
                        <div class="col-md-4">

                            <asp:Label CssClass="Profiletitletxt" ID="lblDevideAtDate" runat="server" Text="At"
                                meta:resourcekey="lblAtDateResource1" />
                            <telerik:RadDatePicker ID="dtpDevidePermissionDate" AllowCustomText="false" MarkFirstMatch="true"
                                Skin="Vista" runat="server" Culture="en-US" meta:resourcekey="dtpPermissionDateResource1"
                                Enabled="false">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                    ToolTip="View permission date" Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqDevidePermissionDate" runat="server" ControlToValidate="dtpDevidePermissionDate"
                                Display="None" ErrorMessage="Please select permission date" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="reqPermissionDateResource1" />
                            <cc1:ValidatorCalloutExtender ID="DevideExtenderPermissionDate" runat="server" TargetControlID="reqDevidePermissionDate"
                                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlDevidePeriodLeave" Visible="False" runat="server" meta:resourcekey="pnlPeriodLeaveResource1">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblDevideDateFrom" runat="server" Text="Date"
                                meta:resourcekey="lblDateFromResource1" />
                        </div>
                        <div class="col-md-1">

                            <asp:Label CssClass="Profiletitletxt" ID="lblDevideFromDate" runat="server" Text="From"
                                meta:resourcekey="lblFromDateResource1" />
                        </div>

                        <div class="col-md-3">
                            <telerik:RadDatePicker ID="dtpDevideStartDatePerm" ToolTip="Click" AllowCustomText="false"
                                MarkFirstMatch="true" Skin="Vista" runat="server" Culture="en-US" meta:resourcekey="dtpStartDatePermResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                                    ToolTip="View start date permission" Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="reqDevideStartDate" runat="server" ControlToValidate="dtpDevideStartDatePerm"
                                Display="None" ErrorMessage="Please select start date" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="reqStartDateResource1" />
                            <cc1:ValidatorCalloutExtender ID="DevideExtenderStartDate" runat="server" TargetControlID="reqDevideStartDate"
                                CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png"
                                Enabled="True">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                        <div class="col-md-1">
                            <asp:Label CssClass="Profiletitletxt" ID="lblDevideEndDate" runat="server" Text="To"
                                meta:resourcekey="lblEndDateResource1" />
                        </div>
                        <div class="col-md-3">
                            <telerik:RadDatePicker ID="dtpDevideEndDatePerm" AllowCustomText="false" MarkFirstMatch="true"
                                Skin="Vista" runat="server" AutoPostBack="True" Culture="en-US" meta:resourcekey="dtpEndDatePermResource1">
                                <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DateInput AutoPostBack="True" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy"
                                    LabelCssClass="" ToolTip="View end date permission" Width="">
                                </DateInput>
                                <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                            <asp:CompareValidator ID="DevideCompareValidator1" runat="server" ControlToCompare="dtpDevideStartDatePerm"
                                ControlToValidate="dtpDevideEndDatePerm" Display="None" ErrorMessage="End date should be greater than or equal to Start date"
                                Operator="GreaterThanEqual" Type="Date" ValidationGroup="EmpPermissionGroup"
                                meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                            <cc1:ValidatorCalloutExtender ID="DevideValidatorCalloutExtender8" runat="server"
                                CssClass="AISCustomCalloutStyle" Enabled="True" TargetControlID="DevideCompareValidator1"
                                WarningIconImageUrl="~/images/warning1.png">
                            </cc1:ValidatorCalloutExtender>
                        </div>
                    </div>
                </asp:Panel>
                <br />
                <div class="row" id="trDevideFullyDay" runat="server" visible="false">
                    <div class="col-md-2">
                        <%--<asp:Label ID="lblDevideIsFullyDay" runat="server" Text="Is Fully Day" CssClass="Profiletitletxt" />--%>
                    </div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="chckDevideFullDay" runat="server" Text="Fully Day" AutoPostBack="True" meta:resourcekey="chckFullDayResource1" />
                    </div>
                </div>
                <div class="Svpanel">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblDevideRemainingBalance" runat="server" Text="Remaining Balance"
                                CssClass="Profiletitletxt" meta:resourcekey="lblRemainingBalanceResource1" />
                        </div>
                        <div class="col-md-4">
                            <asp:Label ID="lblDevideRemainingBalanceValue" runat="server" CssClass="profiletitletxt" />
                            <asp:Label ID="lblDevideRemainingBalanceHour" runat="server" Text="Hours" CssClass="Profiletitletxt"
                                meta:resourcekey="lblRemainingBalanceHoursResource1" />
                        </div>
                    </div>
                </div>
                <div class="Svpanel">
                    <div id="dvDivideTimeControl" runat="server">
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="lblDevideTimeFrom" runat="server" Text="Time"
                                    meta:resourcekey="lblTimeFromResource1" />
                            </div>
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="lblDevideFrom" Text="From" runat="server"
                                    meta:resourcekey="lblFromResource1"></asp:Label>

                                <telerik:RadTimePicker ID="RadTPDevidefromTime" runat="server" AllowCustomText="false"
                                    MarkFirstMatch="true" Skin="Vista" AutoPostBack="True" meta:resourcekey="RadTPfromTimeResource1"
                                    AutoPostBackControl="TimeView">
                                    <DateInput ToolTip="View start time" DateFormat="HH:mm" AutoPostBack="True" DisplayDateFormat="HH:mm"
                                        LabelCssClass="" Width="" />
                                </telerik:RadTimePicker>
                                <asp:RequiredFieldValidator ID="reqDevideFromtime" runat="server" ControlToValidate="RadTPDevidefromTime"
                                    Display="None" ErrorMessage="Please select start time" ValidationGroup="EmpPermissionGroup"
                                    meta:resourcekey="reqFromtimeResource1" />
                                <cc1:ValidatorCalloutExtender ID="DevideExtenderFromTime" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="reqDevideFromtime" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>
                            </div>
                            <div class="col-md-2">
                                <asp:Label CssClass="Profiletitletxt" ID="lblDevideTo" runat="server" Text="To" meta:resourcekey="lblToResource1"></asp:Label>

                                <telerik:RadTimePicker ID="RadTPDevidetoTime" runat="server" AllowCustomText="false"
                                    MarkFirstMatch="true" Skin="Vista" AutoPostBack="True" AutoPostBackControl="TimeView"
                                    meta:resourcekey="RadTPtoTimeResource1" Enabled="false">
                                    <DateInput ToolTip="View end time" DateFormat="HH:mm" AutoPostBack="True" DisplayDateFormat="HH:mm"
                                        LabelCssClass="" Width="" />
                                </telerik:RadTimePicker>
                                <asp:RequiredFieldValidator ID="reqDevideToTime" runat="server" ControlToValidate="RadTPDevidetoTime"
                                    Display="None" ErrorMessage="Please select end time" ValidationGroup="EmpPermissionGroup"
                                    meta:resourcekey="reqToTimeResource1" />
                                <cc1:ValidatorCalloutExtender ID="DevideExtenderreqToTime" runat="server" CssClass="AISCustomCalloutStyle"
                                    TargetControlID="reqDevideToTime" WarningIconImageUrl="~/images/warning1.png"
                                    Enabled="True">
                                </cc1:ValidatorCalloutExtender>

                                <asp:CustomValidator ID="DevideCustomValidator1" runat="server" ControlToValidate="RadTPDevidetoTime"
                                    ClientValidationFunction="Devidevalidate" meta:resourcekey="CustomValidator1Resource1"></asp:CustomValidator>
                                <asp:CustomValidator ID="DevideCustomValidator2" runat="server" ControlToValidate="RadTPDevidefromTime"
                                    ClientValidationFunction="Devidevalidate" meta:resourcekey="CustomValidator2Resource1"></asp:CustomValidator>
                                <asp:HiddenField ID="hdnDevideIsvalid" runat="server" />
                            </div>

                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblDevidePeriodInterval" runat="server"
                            Text="Period" meta:resourcekey="lblPeriodIntervalResource1" />
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDevideTimeDifference" ReadOnly="True" runat="server" meta:resourcekey="txtTimeDifferenceResource1" />
                    </div>
                </div>
                <div class="row" style="display: none">
                    <div class="col-md-2">
                        <asp:Label ID="Label1Devide" is="lblAttachFile" runat="server" Text="Attched File"
                            CssClass="Profiletitletxt" meta:resourcekey="Label1Resource1" />
                    </div>
                    <div class="col-md-4">
                        <asp:FileUpload ID="fuDevideAttachFile" runat="server" meta:resourcekey="fuAttachFileResource1" />
                        <a id="lnbDevideLeaveFile" runat="server" visible="False">
                            <asp:Label ID="lblDevideView" runat="server" Text="View" meta:resourcekey="lblViewResource1" />
                        </a>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2">
                        <asp:Label CssClass="Profiletitletxt" ID="lblDevideRemarks" runat="server" Text="Remarks"
                            meta:resourcekey="lblRemarksResource1" />
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDevideRemarks" runat="server" TextMode="MultiLine" meta:resourcekey="txtRemarksResource1" />
                    </div>
                </div>
                <asp:Panel ID="pnlDevideTempHidRows" runat="server" Visible="False" meta:resourcekey="pnlTempHidRowsResource1">
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chckDevideSpecifiedDays" runat="server" Text="Specified days" meta:resourcekey="lblIsSpecifiedDaysResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label CssClass="Profiletitletxt" ID="lblDevideDays" runat="server" Text="Days"
                                meta:resourcekey="lblDaysResource1" />
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtDevideDays" runat="server" meta:resourcekey="txtDaysResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chckDevideIsFlexible" runat="server" Text="Flexible"
                                meta:resourcekey="lblIsFlexibleResource1" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-4">
                            <asp:CheckBox ID="chckDevideIsDividable" runat="server" Text="Dividable"
                                meta:resourcekey="lblIsDividableResource1" />
                        </div>
                    </div>
                </asp:Panel>
            </asp:Panel>
            <div class="row">
                <div class="col-md-12 text-center ">
                    <asp:Button ID="btnSavePermission" runat="server" Text="Save" CssClass="button" ValidationGroup="EmpPermissionGroup"
                        meta:resourcekey="btnSavePermissionResource1" />
                    <asp:Button ID="btnClearPermission" runat="server" Text="Clear" CssClass="button"
                        meta:resourcekey="btnClearPermissionResource1" Visible="false" />
                    <asp:Button ID="btnCancelPermission" runat="server" Text="Cancel" CssClass="button"
                        CausesValidation="False" meta:resourcekey="btnCancelPermissionResource1" />
                </div>
            </div>


            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblDevideGeneralGuidePermission" runat="server" CssClass="Profiletitletxt"
                        Text="General Guide" meta:resourcekey="lblGeneralGuideResource1" Visible="false" />
                </div>
                <div runat="server" id="dvDevideGeneralGuide" style="background-color: #FDF5B8;">
                </div>
            </div>
        </asp:View>
        <asp:View ID="viewLeaveRequest" runat="server">
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label4" runat="server" CssClass="Profiletitletxt" Text="Leave Type"
                        meta:resourcekey="Label4Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadComboBox ID="ddlLeaveType" runat="server" AppendDataBoundItems="True"
                        AutoPostBack="true" MarkFirstMatch="True" Skin="Vista" Width="200px" meta:resourcekey="ddlLeaveTypeResource1">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" meta:resourcekey="RadComboBoxItemResource1"
                                Owner="" Text="--Please Select--" />
                        </Items>
                    </telerik:RadComboBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlLeaveType"
                        Display="None" ErrorMessage="Please Select Leave Type" InitialValue="--Please Select--"
                        ValidationGroup="vgLeaveRequest" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="RequiredFieldValidator4" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label5" runat="server" CssClass="Profiletitletxt" Text="Request Date"
                        meta:resourcekey="Label5Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpRequestDate" runat="server" AllowCustomText="false"
                        Enabled="False" Culture="en-US" MarkFirstMatch="true" PopupDirection="TopRight"
                        ShowPopupOnFocus="True" Skin="Vista" meta:resourcekey="dtpRequestDateResource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dtpRequestDate"
                        Display="None" ErrorMessage="Please Enter Request Date" ValidationGroup="vgLeaveRequest"
                        meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="RequiredFieldValidator5" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label7" runat="server" CssClass="Profiletitletxt" Text="Leave From"
                        meta:resourcekey="Label7Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpFromDate" runat="server" AllowCustomText="false" Culture="en-US"
                        MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                        meta:resourcekey="dtpFromDateResource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtpFromDate"
                        Display="None" ErrorMessage="Please Enter From Date" ValidationGroup="vgLeaveRequest"
                        meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="RequiredFieldValidator6" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label6" runat="server" CssClass="Profiletitletxt" Text="To" meta:resourcekey="Label6Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <telerik:RadDatePicker ID="dtpToDate" runat="server" AllowCustomText="false" Culture="en-US"
                        MarkFirstMatch="true" PopupDirection="TopRight" ShowPopupOnFocus="True" Skin="Vista"
                        meta:resourcekey="dtpToDateResource1">
                        <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                        </Calendar>
                        <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" LabelCssClass=""
                            Width="">
                        </DateInput>
                        <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                    </telerik:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="dtpToDate"
                        Display="None" ErrorMessage="Please Enter To Date" ValidationGroup="vgLeaveRequest"
                        meta:resourcekey="RequiredFieldValidator7Resource1"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" CssClass="AISCustomCalloutStyle"
                        Enabled="True" TargetControlID="RequiredFieldValidator7" WarningIconImageUrl="~/images/warning1.png">
                    </cc1:ValidatorCalloutExtender>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="dtpFromDate"
                        ControlToValidate="dtpToDate" Display="None" ErrorMessage="To date should be greater than or equal to from date"
                        meta:resourcekey="CompareValidator1Resource1" Operator="GreaterThanEqual" Type="Date"
                        ValidationGroup="vgLeaveRequest"></asp:CompareValidator><cc1:ValidatorCalloutExtender
                            ID="ValidatorCalloutExtender1" runat="server" CssClass="AISCustomCalloutStyle"
                            Enabled="True" TargetControlID="CompareValidator1" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label2" is="lblAttachFile" runat="server" Text="Attched File" CssClass="Profiletitletxt"
                        meta:resourcekey="Label2Resource1" />
                </div>
                <div class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-btn">
                            <span class="btn btn-default" onclick="$(this).parent().find('input[type=file]').click();">Browse</span>
                            <asp:FileUpload ID="FileUpload1" runat="server" meta:resourcekey="FileUpload1Resource1" name="uploaded_file" onchange="$(this).parent().parent().find('.form-control').html($(this).val().split(/[\\|/]/).pop());" Style="display: none;" type="file" />

                        </span>
                        <span class="form-control"></span>
                    </div>
                    <div class="veiw_remove">
                        <a id="A1" runat="server" visible="False">
                            <asp:Label ID="Label10" runat="server" Text="View" meta:resourcekey="lblViewResource1" />
                        </a>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <asp:Label ID="Label8" runat="server" CssClass="Profiletitletxt" Text="Remarks" meta:resourcekey="Label8Resource1"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtLeaveRemarks" runat="server" Height="60px" TextMode="MultiLine" Width="320px"
                        meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" ValidationGroup="vgLeaveRequest"
                        meta:resourcekey="btnSaveResource1" />
                    <asp:Button ID="btnClear" runat="server" CausesValidation="False" CssClass="button"
                        Text="Clear" meta:resourcekey="btnClearResource1" />
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="button"
                        Text="Cancel" meta:resourcekey="btnCancelResource1" />
                </div>
            </div>
            <div class="row">
                <div style="margin-top: -100px;">
                    <asp:Label ID="lblLeaveGeneralGuide" runat="server" CssClass="Profiletitletxt" Text="General Guide"
                        meta:resourcekey="lblGeneralGuideResource1" Visible="false" />
                </div>
                <div runat="server" id="dvGeneralGuide" style="margin-top: 5px; background-color: #FDF5B8;">
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

<asp:Content ID="script" ContentPlaceHolderID="scripts" runat="server">
    <script src="../Svassets/js/fullcalendar.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="../Svassets/js/locale-all.js" charset="utf-8"></script>
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            var server = '../selfservices/EmployeeViolations.aspx/GetEmployee_Calendar';
            var initialLocaleCode = '<%=CalendarLang.ToString%>';
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();

            if (dd < 10) {
                dd = '0' + dd
            }

            if (mm < 10) {
                mm = '0' + mm
            }

            today = yyyy + '-' + mm + '-' + dd;
            console.log(today);
            $('#calendar').fullCalendar({
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay,listMonth'
                },
                defaultDate: today,
                lang: initialLocaleCode,
                //locale: "ar-ma",
                locale: initialLocaleCode,
                buttonIcons: true, // show the prev/next text
                weekNumbers: true,
                navLinks: false, // can click day/week names to navigate views
                editable: false,
                eventLimit: true, // allow "more" link when too many events
                displayEventTime: false,
                events: function (start, end, timezone, callback) {
                    //var obj = { "CalDate": $('.fc-center').children()[0].innerText}; 
                    var obj = { "CalStartDate": start, "CalEndDate": end };
                    var value = JSON.stringify(obj);

                    $.ajax({
                        type: "POST",
                        url: server,
                        cache: false,
                        data: value,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var events = [];
                            var d = JSON.parse(data.d);
                            for (var k in d) {
                                events.push({
                                    title: d[k].title,
                                    start: d[k].start,
                                    end: d[k].end,
                                    color: d[k].color
                                });
                            }
                            callback(events);
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log('There was an error on load calender');
                        }
                    });
                }
            });
        });
    </script>



    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            var server = '../selfservices/EmployeeViolations.aspx/GetManager_Calendar';
            var initialLocaleCode = '<%=CalendarLang.ToString%>';
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();

            if (dd < 10) {
                dd = '0' + dd
            }

            if (mm < 10) {
                mm = '0' + mm
            }

            today = yyyy + '-' + mm + '-' + dd;
            console.log(today);
            $('#Manager_calendar').fullCalendar({
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay,listMonth'
                },

                eventMouseover: function (calEvent, jsEvent, view) {
                    $(this).css('cursor', 'pointer');
                },
                eventClick: function (calEvent, jsEvent, view) {
                    OpenlobiWindow('Details', '../selfservices/Manager_CalendarDetails.aspx?ApptType=' + calEvent.title + '&ApptDate=' + formatDate(calEvent.start.date(Date)));
                },

                defaultDate: today,
                lang: initialLocaleCode,
                //locale: "ar-ma",
                locale: initialLocaleCode,

                buttonIcons: true, // show the prev/next text
                weekNumbers: true,
                navLinks: true, // can click day/week names to navigate views
                editable: false,
                eventLimit: true, // allow "more" link when too many events
                displayEventTime: false,
                defaultView: 'month',
                events: function (start, end, timezone, callback) {
                    //var obj = { "CalDate": $('.fc-center').children()[0].innerText}; 
                    var obj = { "CalStartDate": start, "CalEndDate": end };
                    var value = JSON.stringify(obj);

                    $.ajax({
                        type: "POST",
                        url: server,
                        cache: false,
                        data: value,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var events = [];
                            var d = JSON.parse(data.d);
                            for (var k in d) {
                                events.push({
                                    title: d[k].title,
                                    start: d[k].start,
                                    end: d[k].end,
                                    color: d[k].color
                                });
                            }
                            callback(events);
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            console.log('There was an error on load calender');
                        }
                    });
                }
            });
        });
    </script>

</asp:Content>






