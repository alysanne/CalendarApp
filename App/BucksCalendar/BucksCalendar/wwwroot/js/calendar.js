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
    if (window.location.pathname !== '/Calendar') {
        return;
    }
    
    if (!urlParams.get('month') || !urlParams.get('year')) {
        return window.location.href='/Calendar?year=' + currentYear + '&month=' + currentMonth;
    }

    $("#month").val(calendarMonth).change();
    $("#year").val(calendarYear).change();
    generateCalendar(calendarMonth, calendarYear);

    mapModalData();
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
        var id = $(this).find(".event-id").text().trim();
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
                "<button type='button' data-toggle='modal' data-target='#event-modal' class='category-with-dot event-block'>"
                + "<div class='dot " + category + "-dot'></div>"
                + "<span class='info-title'>" + title + "</span>"
                + "<span class='info-start-date' hidden>" + start + "</span>"
                + "<span class='info-end-date' hidden>" + end + "</span>"
                + "<span class='info-category' hidden>" + category + "</span>"
                + "<span class='info-id' hidden>" + id + "</span>"
                + "</button>");
            datePointer.setDate(datePointer.getDate() + 1);
        } while (datePointer <= endDate);
    });
}

function mapModalData() {
    $(".event-block").click(function () {
        var infoTitle = $(this).find(".info-title").text();
        var infoId = $(this).find(".info-id").text();
        var infoStartDate = $(this).find(".info-start-date").text();
        var infoEndDate = $(this).find(".info-end-date").text();
        var infoCategory = $(this).find(".info-category").text();
        var categoryLabel;
        
        switch (infoCategory) {
            case "AnnualLeave":
                categoryLabel = "Annual Leave";
                break;
            case "BankHoliday":
                categoryLabel = "Bank Holiday";
                break;
            default:
                categoryLabel = infoCategory;
        }

        var options = {day: 'numeric', weekday: 'short', year: 'numeric', month: 'short'};
        var formattedStartDate = new Date(infoStartDate).toLocaleDateString("en-GB", options);
        var formattedEndDate = new Date(infoEndDate).toLocaleDateString("en-GB", options);

        $("#modal-title").text(infoTitle);
        $(".modal-body").html("");
        $(".modal-body").append("<div class='category-with-dot'>"
            + "<div class='dot " + infoCategory + "-dot'></div>"
            + "<span>" + categoryLabel + "</span>"
            + "</div>");
        $(".modal-body").append("<span>" + formattedStartDate + "</span>");

        if (formattedStartDate !== formattedEndDate) {
            $(".modal-body").append("<span> - </span>");
            $(".modal-body").append("<span>" + formattedEndDate + "</span>");
        }

        $("#modal-details").click(function () {
            window.location.href='/Calendar/Details?id=' + infoId;
        });

        $("#modal-edit").click(function () {
            window.location.href='/Calendar/Edit?id=' + infoId;
        });

        $("#modal-delete").click(function () {
            window.location.href='/Calendar/Delete?id=' + infoId;
        });
    });
}