var CalendarBasic = {
    init: function () {
        
        var e = moment().startOf("day");
          
        $("#m_calendar").fullCalendar(
            {
                header: { left: "prev,next today", center: "title", right: "month,agendaWeek,agendaDay,listWeek" },
                dragable:!0,editable: !0, eventLimit: !0, navLinks: !0, locale: 'es',
                events: Model,
                eventRender: function (e, t) {
                    t.hasClass("fc-day-grid-event") ? (t.data("content", e.description),
                        t.data("placement", "top"),
                        mApp.initPopover(t)) : t.hasClass("fc-time-grid-event")
                            ? t.find(".fc-title").append('<div class="fc-description">' + e.description + "</div>")
                            : 0 !== t.find(".fc-list-item-title").lenght
                            && t.find(".fc-list-item-title").append('<div class="fc-description">' + e.description + "</div>")
                },
                dayClick: function (date, jsEvent, view, resourceObj) {
                    var events = $('#m_calendar').fullCalendar('clientEvents');
                    var currentEvent;
                    for (var i = 0; i < events.length; i++) {
                        var datemom = moment.utc(date).format('L');
                        var startevent = moment.utc(events[i].start).format('L');
                        if (datemom.isSame(moment(startevent))) {
                            currentEvent = events[i];
                            break;
                        }
                    }
                    if (currentEvent != null) {
                        $.ajax({
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            type: 'Get',
                            url: "/Tours/GetDayDetails/?id=" + currentEvent.id,
                            success: function (result) {
                                var date = result.date.split("T")[0];
                                var DateArr = date.split("-");
                                var month = DateArr[1];
                                $("#Date").val((month) + "-" + (DateArr[2]) + "-" + DateArr[0]);
                                $("#AdultPrice").val(result.adultPrice);
                                $("#ChildPrice").val(result.childPrice);
                                $("#BabyPrice").val(result.babyPrice);
                                $("#DayId").val(currentEvent.id);
                            },
                            error: function () {
                                swal({
                                    title: "Precio del dia !",
                                    text: "Algo mal por favor intente de nuevo",
                                    type: "error"
                                });
                            }
                        });

                    } else {
                        var month = date.month() + 1;
                        $("#Date").val(("0" + (month)).slice(-2) + "-" + ("0" + (date.date())).slice(-2) + "-" + date.year());
                    }

                    $("#DayCategoriesmodal").modal('show');
                },
                eventClick: function (calEvent) {
                    $("#DayId").val(calEvent.id);
                    $.ajax({
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        type: 'Get',
                        url: "/Tours/GetDayDetails/?id=" + calEvent.id,
                        success: function (result) {
                            var date = result.date.split("T")[0];
                            var DateArr = date.split("-");
                            var month = DateArr[1];
                            $("#Date").val(month + "-" + DateArr[2] + "-" + DateArr[0]);
                            $("#AdultPrice").val(result.adultPrice);
                            $("#ChildPrice").val(result.childPrice);
                            $("#BabyPrice").val(result.babyPrice);
                            $("#DayId").val(calEvent.id);

                        },
                        error: function () {
                            swal({
                                title: "Precio del dia !",
                                text: "Algo mal por favor intente de nuevo",
                                type: "error"
                            });
                        }
                    });
                    $("#DayCategoriesmodal").modal('show');
                }
            });
    }
}; jQuery(document).ready(function () { CalendarBasic.init(); $(".fc-time").hide(); });