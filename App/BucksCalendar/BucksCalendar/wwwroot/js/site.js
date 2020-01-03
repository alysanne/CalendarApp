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
var selectedYear = $("#year");
var selectedMonth = $("#month");

var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
var monthsLong = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

$(document).ready(function () {
    selectedMonth.val(currentMonth.toString()).change();
    selectedYear.val(currentYear.toString()).change();
    generateCalendar(currentMonth, currentYear);
});

/* Calendar navigation buttons */

$("#next").click(function() {
    var intYear = parseInt(selectedYear.val());
    var intMonth = parseInt(selectedMonth.val());

    var calcYear = (intMonth === 11) ? intYear + 1 : intYear;
    var calcMonth = (intMonth + 1) % 12;

    selectedMonth.val(calcMonth.toString()).change();
    selectedYear.val(calcYear.toString()).change();
    generateCalendar(calcMonth, calcYear);
});

$("#previous").click(function() {
    var intYear = parseInt(selectedYear.val());
    var intMonth = parseInt(selectedMonth.val());

    var calcYear = (intMonth === 0) ? intYear - 1 : intYear;
    var calcMonth = (intMonth === 0) ? 11 : intMonth - 1;
    
    selectedMonth.val(calcMonth.toString()).change();
    selectedYear.val(calcYear.toString()).change();
    generateCalendar(calcMonth, calcYear);
});

$("#today").click(function() {
    selectedMonth.val(currentMonth).change();
    selectedYear.val(currentYear).change();
    generateCalendar(currentMonth, currentYear);
});

selectedMonth.change(function () {
    var intYear = parseInt(selectedYear.val());
    var intMonth = parseInt(selectedMonth.val());
    
    generateCalendar(intMonth, intYear);
});

selectedYear.change(function () {
    var intYear = parseInt(selectedYear.val());
    var intMonth = parseInt(selectedMonth.val());

    generateCalendar(intMonth, intYear);
});

/* Calendar generation */

function updateTitle(month, year) {
    $("#chosen-month").text(monthsLong[month]);
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
                $("#row-" + i).append("<td class='calendar-day' id='day-" + dayNumber + "'>" + dayNumber + "</td>");
                
                // Add style for current day
                if (dayNumber === currentDate.getDate() && year === currentYear && month === currentMonth) {
                    $("#day-" + dayNumber).html("<div class='calendar-day current-day'>" + dayNumber + "</div>");
                }
                
                dayNumber++;
            }
        }
    }
}