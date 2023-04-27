<%@ Page Language="VB" AutoEventWireup="false" Title="Organization Chart" CodeFile="OrgChart.aspx.vb" Inherits="Reports_OrgChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        #chart
        {
            width: 900px;
            height: 500px;
        }
        #chart div
        {
            width: 130px;
        }
        #chart span
        {
            color: red;
            font-size: 8pt;
            font-style: italic;
        }
        #chart img
        {
            height: 100px;
            width: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <%--  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/jsapi.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="https://www.google.com/jsapi"></script>--%>
    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["orgchart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            $.ajax({
                type: "POST",
                url: "OrgChart.aspx/GetChartData",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Entity');
                    data.addColumn('string', 'ParentEntity');
                    data.addColumn('string', 'ToolTip');
                    for (var i = 0; i < r.d.length; i++) {
                        var EntityId = r.d[i][0].toString();
                        var EntityName = r.d[i][1];
                        var EntityArabicName = r.d[i][2];
                        var FK_ParentId = r.d[i][3] != null ? r.d[i][3].toString() : '';
                        data.addRows([[{ v: EntityId,
                            f: EntityName + '<div>(<span>' + EntityArabicName + '</span>)</div>'/*'<img src = "Pictures/' + EntityId + '.jpg" />'*/
                        }, FK_ParentId, EntityArabicName]]);
                    }
                    var chart = new google.visualization.OrgChart($("#chart")[0]);
                    chart.draw(data, { allowHtml: true });
                },
                failure: function (r) {
                    ShowMessage(r.d);
                },
                error: function (r) {
                    ShowMessage(r.d);
                }
            });
        }
    </script>
    <div id="chart">
    </div>
    </form>
</body>
</html>
