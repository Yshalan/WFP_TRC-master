<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ManagerCalendar.ascx.vb" Inherits="SelfServices_UserControls_ManagerCalendar" %>


<link href="../Svassets/css/fullcalendar.css" rel="stylesheet" type="text/css" />


<div class="row">
    <div id="Manager_calendar"></div>
</div>

<script src="../Svassets/js/fullcalendar.min.js" type="text/javascript" charset="utf-8"></script>
<script src="../Svassets/js/locale-all.js" charset="utf-8"></script>
<script type="text/javascript" charset="utf-8">
    $(document).ready(function () {
        var server = '../selfservices/UserControls/ManagerCalendar.ascx/GetManager_Calendar';
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
                            debugger;
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

