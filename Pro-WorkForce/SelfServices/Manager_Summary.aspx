<%@ Page Title="" Language="VB" MasterPageFile="~/Default/NewMaster.master" AutoEventWireup="false"
    CodeFile="Manager_Summary.aspx.vb" Inherits="SelfServices_Manager_Summary"
    UICulture="auto" Theme="SvTheme" meta:resourcekey="PageResource1" %>

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

    <script language="javascript" type="text/javascript">

        function ViewAll_EmployeeEvents(eventtitle, eventdate) {
            OpenlobiWindow('!', '../selfservices/Manager_CalendarDetails.aspx?ApptType=' + eventtitle + '&ApptDate=' + formatDate(eventdate(Date)));
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
    </script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script language="javascript" type="text/javascript" charset="utf-8" lang="ar">
        function parseArabic(str) {
            return str.replace(/[\u0660-\u0669]/g, function (c) {
                return c.charCodeAt(0) - 0x0660;
            }).replace(/[\u06f0-\u06f9]/g, function (c) {
                return c.charCodeAt(0) - 0x06f0;
            });
        }


    </script>
    <style>
        .lobibox.lobibox-window .lobibox-body {
            background-color: #fff !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <uc3:pageheader id="PageHeader1" runat="server" />

    <div id="Div7" class="row" runat="server" visible="true">
        <div class="col-md-2 colorboxAttendees">
            <asp:Label ID="lblCalAttendees" runat="server" Text="Attendees" meta:resourcekey="lblCalAttendeesResource1"></asp:Label>
        </div>
        <div class="col-md-2 colorboxDelay">
            <asp:Label ID="lblCalDelay" runat="server" Text="Delay" meta:resourcekey="lblCalDelayResource1"></asp:Label>
        </div>
        <div class="col-md-2 colorboxEarlyOut">
            <asp:Label ID="lblCalEarlyOut" runat="server" Text="Early Out" meta:resourcekey="lblCalEarlyOutResource1"></asp:Label>
        </div>
        <div class="col-md-2 colorboxMissingTransaction">
            <asp:Label ID="lblCalMissingTransaction" runat="server" Text="Missing Transaction" meta:resourcekey="lblCalMissingTransactionResource1"></asp:Label>
        </div>
        <div class="col-md-2 colorboxLeave">
            <asp:Label ID="lblCalLeaves" runat="server" Text="Leaves" meta:resourcekey="lblCalLeavesResource1"></asp:Label>
        </div>
        <div class="col-md-2 colorboxPermission">
            <asp:Label ID="lblCalPermissions" runat="server" Text="Permission" meta:resourcekey="lblCalPermissionsResource1"></asp:Label>
        </div>
        <div class="clear"></div>
        <div class="col-md-2 colorboxAbsent">
            <asp:Label ID="lblCalAbsent" runat="server" Text="Absent" meta:resourcekey="lblCalAbsentResource1"></asp:Label>
        </div>
        <div class="col-md-2 colorboxLeavesRequest">
            <asp:Label ID="lblCalLeaveRequest" runat="server" Text="Leave Request" meta:resourcekey="lblCalLeaveRequestResource1"></asp:Label>
        </div>
        <div class="col-md-2 colorboxPermissionsRequest">
            <asp:Label ID="lblCalPermissionRequest" runat="server" Text="Permission Request" meta:resourcekey="lblCalPermissionRequestResource1"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div id="Manager_calendar" class="managercalendar"></div>
    </div>
</asp:Content>

<asp:Content ID="script" ContentPlaceHolderID="scripts" runat="server">
    <script src="../Svassets/js/fullcalendar.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="../Svassets/js/locale-all.js" charset="utf-8"></script>

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
                    debugger;
                    OpenlobiWindow('!', '../selfservices/Manager_CalendarDetails.aspx?ApptType=' + calEvent.title + '&ApptDate=' + formatDate(calEvent.start._i));
                },

                eventRender: function (event, element) {
                    element.addClass('event-on-' + event.start.format('YYYY-MM-DD'));
                },
                eventAfterAllRender: function (view) {

                    $('#Manager_calendar .fc-day.fc-widget-content').each(function (i) {
                        var eventdate = $(this).data('date');

                        //date = new date(date)
                        //alert(date);
                        var count = $('#Manager_calendar a.fc-event.event-on-' + eventdate).length;
                        if (count > 0) {
                            if (view.type == 'month') {
                                if (initialLocaleCode == 'en') {
                                    $(this).append("<a class=" + "'foo'" + "onclick=" + "OpenlobiWindow(" + "'!'" + "," + "'../selfservices/Manager_CalendarDetails.aspx?ApptType=All&ApptDate=" + "" + eventdate + "'" + ")>View All</a>" + "");
                                }
                                else {
                                    $(this).append("<a class=" + "'foo'" + "onclick=" + "OpenlobiWindow(" + "'!'" + "," + "'../selfservices/Manager_CalendarDetails.aspx?ApptType=All&ApptDate=" + "" + parseArabic(eventdate) + "'" + ")>عرض الكل</a>" + "");
                                }
                            }
                        }
                    });
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






