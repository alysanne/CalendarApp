function showHideEndDate() {
    if ($('#all-day-event-tickbox').prop('checked')) {
        $('#end-date-field').hide();
    } else {
        $('#end-date-field').show();
    }
}

$(document).ready(function () {
    showHideEndDate();
});

$('#all-day-event-tickbox').click(function() {
    showHideEndDate();
});

$('#submit-btn').click(function() {
    if ($('#all-day-event-tickbox').prop('checked')) {
        var startDate = $('#start-date').val();
        var endDate = $('#end-date');
        endDate.val(startDate);
    }
});
