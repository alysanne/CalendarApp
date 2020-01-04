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
