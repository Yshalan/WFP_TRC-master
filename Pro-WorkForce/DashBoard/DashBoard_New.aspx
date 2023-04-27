<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DashBoard_New.aspx.vb" MasterPageFile="~/Default/NewMaster.master"
    Theme="SvTheme" Inherits="DashBoard_DashBoard_New" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Admin/UserControls/UserSecurityFilter.ascx" TagName="UserSecurityFilter"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/PageHeader.ascx" TagName="PageHeader" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <asp:Literal runat="server" ID="JSONdrilldown" meta:resourcekey="JSONResource1" />
    <asp:Literal runat="server" ID="JSONSeries" meta:resourcekey="JSONResource1" />
    <asp:Literal runat="server" ID="JSONChartType" meta:resourcekey="JSONResource1" />
    <asp:Literal runat="server" ID="JSONChartTitleText" meta:resourcekey="JSONResource1" />
    <asp:Literal runat="server" ID="JSONChartSubTitleText" meta:resourcekey="JSONResource1" />
    <%--    <title>Generate JSON According To Drill Down Drill Up Events Demo-Sibeesh Passion
    </title>--%>
    <script src="js/jquery-2.0.2.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/highcharts.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/drilldown.js" type="text/javascript" charset="utf-8"></script>
    <script src="js/exporting.js" type="text/javascript" charset="utf-8"></script>
    <meta http-equiv="Content-Type" content="text/plain" charset="UTF-8" />

    <script type="text/javascript" charset="utf-8">

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
                tooltip: {
                    formatter: function () {
                        return this.point.name + ' : ' + this.y + ' %';
                    }
                },
                xAxis: {
                    categories: true,
                    overflow: "justify"
                },
                drilldown: Var_drilldown,
                legend: {
                    enabled: true

                },
                plotOptions: {
                    //allowhtml: true,
                    //useHTML: true,
                    series: {
                        dataLabels: {
                            enabled: true,
                            overflow: "justify",
                            formatter: function () {
                                return this.point.name + ' : ' + this.y + ' %';
                            }
                        },
                        shadow: true
                    },
                    pie: {
                        size: '100%',
                        showInLegend: true
                    }
                },

                series: Var_Series
                //[{
                //    name: 'Overview', colorByPoint: true, data: [
                //        {
                //            name: 'leaves', y: 3.55, drilldown: 'الاجازات'
                //        }, { name: 'التأخير', y: 2.37, drilldown: 'التأخير' },
                //    { name: 'الحضور', y: 71.60, drilldown: 'الحضور' },
                //    { name: 'الخروج المبكر', y: 1.18, drilldown: 'الخروجالمبكر' },
                //    { name: 'العطل الرسمية', y: 0.00, drilldown: 'العطلالرسمية' },
                //    { name: 'الغياب', y: 21.30, drilldown: 'الغياب' }, ]
                //}]
            };
            // Drill Down Chart Implementation 


            options.chart.renderTo = 'ctl00_ContentPlaceHolder1_container';
            options.chart.type = Var_ChartType;  //Type Of Charts ========= column,line,pie,bar,area,scatter -- (xy),stock,surface,doughnut,bubble



            var chart1 = new Highcharts.Chart(options);

            //            chart1.series: [{name:   'Overview',colorByPoint: true,data: [{name:   'Fruits',  y: 100,drilldown:  'fruits'}, {name:   'Cars',y: 12,drilldown:  'cars'  }, {name:   'Countries',y: 8}]  }]
            //            alert('hereeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee');
            //                alert(Shifts);

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



        //                          $.ajax({
        //                type: "POST",
        //                url: "ChartTest.aspx/GetData",
        //                data: '{}',
        //                contentType: "application/json; charset=utf-8",
        //                dataType: "json",
        //                success: function (result) {
        //               Flat_Series=result.d;
        ////                return Flat_Series;
        //                alert(Flat_Series);
        ////                Highcharts.series=result.d;
        //                options={series:Flat_Series};
        //                     debugger;
        //                }});

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
            <asp:Label ID="lblChartType" runat="server" Text="ChartType" meta:resourcekey="lblChartTypeResource1"></asp:Label>
        </div>
        <div class="col-md-4">
            <telerik:RadComboBox ID="ddlChartType" runat="server" AutoPostBack="True" MarkFirstMatch="True"
                Filter="Contains" Skin="Vista" CausesValidation="False" meta:resourcekey="ddlChartTypeResource1">
                <Items>
                    <telerik:RadComboBoxItem Text="Faces" Value="Faces" Selected="true" meta:resourcekey="RadComboBoxItemResource7" />
                    <telerik:RadComboBoxItem Text="column" Value="column" meta:resourcekey="RadComboBoxItemResource1" />
                    <telerik:RadComboBoxItem Text="line" Value="line" meta:resourcekey="RadComboBoxItemResource2" />
                    <telerik:RadComboBoxItem Text="pie" Value="pie" Selected="true" meta:resourcekey="RadComboBoxItemResource3" />
                    <telerik:RadComboBoxItem Text="bar" Value="bar" meta:resourcekey="RadComboBoxItemResource4" />
                    <telerik:RadComboBoxItem Text="area" Value="area" meta:resourcekey="RadComboBoxItemResource5" />
                    <telerik:RadComboBoxItem Text="scatter" Value="scatter" meta:resourcekey="RadComboBoxItemResource6" />
                </Items>
            </telerik:RadComboBox>
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
            <%--     <cc1:ValidatorCalloutExtender TargetControlID="CVDate" ID="ValidatorCalloutExtender2"
                            runat="server" Enabled="True" CssClass="AISCustomCalloutStyle" WarningIconImageUrl="~/images/warning1.png">
                        </cc1:ValidatorCalloutExtender>--%>
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
    <div runat="server" id="divBorder" visible="false" style="height: 200px; border-style: solid; border-width: 1px; border-color: #ccc; margin-top: 5px; border-radius: 5px">
        <div class="row">
            <div class="col-md-2">
            </div>
            <br />
        </div>
        <div class="row" runat="server" id="DivFaces" visible="false">
            <div class="col-md-4" align="center">
                <asp:Image ID="imgHappy" ImageUrl="~/DashBoard/Img/Happy.png" runat="server" />
                <asp:Label runat="server" ID="lblHappy" Text="Happy"></asp:Label>
                <asp:Label runat="server" ID="lblHappyValue"></asp:Label>
            </div>
            <div class="col-md-4" align="center">
                <asp:Image ID="imgNeutral" ImageUrl="~/DashBoard/Img/Neutral.png" runat="server" />
                <asp:Label runat="server" ID="lblNeutral" Text="Neutral"></asp:Label>
                <asp:Label runat="server" ID="lblNeutralValue"></asp:Label>
            </div>
            <div class="col-md-4" align="center">
                <asp:Image ID="imgSad" ImageUrl="~/DashBoard/Img/Sad.png" runat="server" />
                <asp:Label runat="server" ID="lblUnHappy" Text="UnHappy"></asp:Label>
                <asp:Label runat="server" ID="lblUnHappyValue"></asp:Label>
            </div>
        </div>
    </div>
    <div id="container" style="height: 300px" class="highchartsalignment" runat="server">
    </div>
    <div id="jsonContent" runat="server">
    </div>
</asp:Content>
