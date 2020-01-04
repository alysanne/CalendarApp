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

var mainProfile = $('main[role="main"]').has('div.profile-content');
mainProfile.css('margin', '35px auto');
mainProfile.css('width', '70%');

// buttons
mainProfile.find(".nav-pills .nav-link.active, .nav-pills .show > .nav-link").css('background-color', '#f85c6a');
mainProfile.find(".nav-pills .nav-link").css('color', '#ffffff');
