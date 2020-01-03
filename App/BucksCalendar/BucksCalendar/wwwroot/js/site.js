function notifyTicked() {
    var smsChecked = $('#notify-checkbox-sms').prop('checked');
    var emailChecked = $('#notify-checkbox-email').prop('checked');
    return smsChecked || emailChecked;
}

function showHideSchedule() {
    if (notifyTicked()) {
        $('#scheduled-for').prop('required', true);
        $('#scheduled-for-field').show();
    } else {
        $('#scheduled-for').prop('required', false);
        $('#scheduled-for-field').hide();
    }
}

$(document).ready(function () {
    showHideSchedule();
});

$('.notify-tickbox').click(function() {
    showHideSchedule();
});

$('#submit-btn').click(function() {
    if (!notifyTicked()) {
        $('#scheduled-for').val("");
    }
});


/* Calendar */

// URL params
var urlParams = new URLSearchParams(window.location.search);

// Current date values
var currentDate = new Date();
var currentYear = currentDate.getFullYear();
var currentMonth = currentDate.getMonth();

// Dates for calendar
var calendarMonth = urlParams.get('month') ? parseInt(urlParams.get('month')) : currentMonth;
var calendarYear = urlParams.get('year') ? parseInt(urlParams.get('year')) : currentYear;

// Selected date values
function getSelectedYearNumber() {
    return parseInt($("#year").val());
}

function getSelectedMonthNumber() {
    return parseInt($("#month").val());
}

var months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

$(document).ready(function () {
    if (!urlParams.get('month') || !urlParams.get('year')) {
        return window.location.href='/Calendar?year=' + currentYear + '&month=' + currentMonth;
    }
    
    $("#month").val(calendarMonth).change();
    $("#year").val(calendarYear).change();
    generateCalendar(calendarMonth, calendarYear);
});

/* Calendar navigation buttons */

$("#next").click(function() {
    var year = getSelectedYearNumber();
    var month = getSelectedMonthNumber();
    var calcYear = (month === 11) ? year + 1 : year;
    var calcMonth = ((month + 1) % 12);

    window.location.href='/Calendar?year=' + calcYear + '&month=' + calcMonth;
});

$("#previous").click(function() {
    var year = getSelectedYearNumber();
    var month = getSelectedMonthNumber();
    var calcYear = (month === 0) ? year - 1 : year;
    var calcMonth = (month === 0) ? 11 : month - 1;

    window.location.href='/Calendar?year=' + calcYear + '&month=' + calcMonth;
});

$("#today").click(function() {
    window.location.href='/Calendar?year=' + currentYear + '&month=' + currentMonth;
});

/* Calendar generation */

function updateTitle(month, year) {
    $("#chosen-month").text(months[month]);
    $("#chosen-year").text(year);
}

function daysInMonth(month, year) {
    return new Date(year, month + 1, 0).getDate();
}

function generateCalendar(month, year) {
    updateTitle(month, year);
    
    var calendarBody = $("#calendar-body");
    calendarBody.html("");

    var firstDayOfMonth = (new Date(year, month)).getDay();
    var dayNumber = 1;
    
    // Iterate over calendar rows
    for (var i = 0; i < 6; i++) {
        calendarBody.append("<tr id='row-" + i + "'></tr>");

        // Iterate over weekdays
        for (var j = 0; j < 7; j++) {
            if (dayNumber > daysInMonth(month, year)) {
                break;
            } else if (i === 0 && j < firstDayOfMonth) {
                $("#row-" + i).append("<td></td>");
            } else {
                $("#row-" + i).append("<td class='calendar-day' id='day-" + dayNumber + "'><div>" + dayNumber + "</div></td>");
                
                // Add style for current day
                if (dayNumber === currentDate.getDate() && year === currentYear && month === currentMonth) {
                    $("#day-" + dayNumber).html("<div class='calendar-day current-day'>" + dayNumber + "</div>");
                }
                
                dayNumber++;
            }
        }
    }
    addCalendarEvents()
}

function addCalendarEvents() {
    var modelData = $("#events").children();
    modelData.each(function () {
        // Event data
        var title = $(this).find(".event-title").text().trim();
        var category = $(this).find(".event-category").text().trim();
        var start = $(this).find(".event-start-date").text().trim();
        var end = $(this).find(".event-end-date").text().trim();

        var startDateTime = new Date(start);
        var startDate = new Date(startDateTime.getFullYear(), startDateTime.getMonth(), startDateTime.getDate());
        var endDateTime = new Date(end);
        var endDate = new Date(endDateTime.getFullYear(), endDateTime.getMonth(), endDateTime.getDate());

        var year = getSelectedYearNumber();
        var month = getSelectedMonthNumber();

        // Use 1st day of selected month if event starts in a previous month
        var firstDayOfMonth = new Date(year, month, 1);
        startDate = startDate < firstDayOfMonth ? firstDayOfMonth : startDate;

        // Use last day of selected month if event ends in a later month
        var lastDayOfMonth = new Date(year, month, daysInMonth(month, year));
        endDate = endDate > lastDayOfMonth ? lastDayOfMonth : endDate;

        var datePointer = startDate;
        do {
            if (datePointer.getMonth() !== month) {
                break;
            }

            $("#day-" + datePointer.getDate()).append(
                "<div class='category-with-dot event-block'>"
                    + "<div class='dot " + category + "-dot'></div>"
                    + "<span aria-label='Category' id='event-category'>" + title + "</span>"
                + "</div>");
            datePointer.setDate(datePointer.getDate() + 1);
        } while (datePointer <= endDate);
    });
}