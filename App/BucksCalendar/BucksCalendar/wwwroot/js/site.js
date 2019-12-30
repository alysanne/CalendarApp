function notifyTicked() {
    var smsChecked = $('#notify-tickbox-sms').prop('checked');
    var emailChecked = $('#notify-tickbox-email').prop('checked');
    return smsChecked || emailChecked;
}

function showHideEndDate() {
    if ($('#all-day-event-tickbox').prop('checked')) {
        $('#end-date-field').hide();
    } else {
        $('#end-date-field').show();
    }
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
    showHideEndDate();
    showHideSchedule();
});

$('#all-day-event-tickbox').click(function() {
    showHideEndDate();
});

$('.notify-tickbox').click(function() {
    showHideSchedule();
});

$('#submit-btn').click(function() {
    if ($('#all-day-event-tickbox').prop('checked')) {
        var startDate = $('#start-date').val();
        var endDate = $('#end-date');
        endDate.val(startDate);
    }
    
    if (!notifyTicked()) {
        $('#scheduled-for').val("");
    }
});
