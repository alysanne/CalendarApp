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

// Current date values
var currentDate = new Date();
var currentYear = currentDate.getFullYear();
var currentMonth = currentDate.getMonth();

// Selected date values
function getSelectedYearNumber() {
    return parseInt($("#year").val());
}

function getSelectedMonthNumber() {
    return parseInt($("#month").val());
}

var months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

$(document).ready(function () {
    $("#month").val(currentMonth.toString()).change();
    $("#year").val(currentYear.toString()).change();
    generateCalendar(currentMonth, currentYear);
});

/* Calendar navigation buttons */

$("#next").click(function() {
    var year = getSelectedYearNumber();
    var month = getSelectedMonthNumber();
    var calcYear = (month === 11) ? year + 1 : year;
    var calcMonth = (month + 1) % 12;

    $("#month").val(calcMonth.toString()).change();
    $("#year").val(calcYear.toString()).change();
    generateCalendar(calcMonth, calcYear);
});

$("#previous").click(function() {
    var year = getSelectedYearNumber();
    var month = getSelectedMonthNumber();
    var calcYear = (month === 0) ? year - 1 : year;
    var calcMonth = (month === 0) ? 11 : month - 1;

    $("#month").val(calcMonth.toString()).change();
    $("#year").val(calcYear.toString()).change();
    generateCalendar(calcMonth, calcYear);
});

$("#today").click(function() {
    $("#month").val(currentMonth).change();
    $("#year").val(currentYear).change();
    generateCalendar(currentMonth, currentYear);
});

$("#month").change(function () {
    generateCalendar(getSelectedMonthNumber(), getSelectedYearNumber());
});

$("#year").change(function () {
    generateCalendar(getSelectedMonthNumber(), getSelectedYearNumber());
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
            debugger
            if (datePointer.getMonth() !== month) {
                break;
            }

            $("#day-" + datePointer.getDate()).append(title);
            datePointer.setDate(datePointer.getDate()+1);
        } while (datePointer <= endDate);
    });
}