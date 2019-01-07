function GetGrade(value) {
    if (value <= 100 && value > 91) {
        return 'A';
    }
    else if (value <= 90 && value > 81) {
        return 'B';
    }
    else if (value <= 80 && value > 71) {
        return 'C';
    }
    else if (value <= 70 && value > 61) {
        return 'D';
    }
    else if (value <= 60 && value > 51) {
        return 'D';
    }
    else {
        return 'E';
    }
}

function millisecondsToStr(milliseconds) {
    // TIP: to find current time in milliseconds, use:
    // var  current_time_milliseconds = new Date().getTime();

    function numberEnding(number) {
        return (number > 1) ? 's' : '';
    }

    var temp = Math.floor(milliseconds / 1000);
    var years = Math.floor(temp / 31536000);
    if (years) {
        return years + ' year' + numberEnding(years);
    }
    //TODO: Months! Maybe weeks? 
    var days = Math.floor((temp %= 31536000) / 86400);
    if (days) {
        return days + ' day' + numberEnding(days);
    }
    var hours = Math.floor((temp %= 86400) / 3600);
    if (hours) {
        return hours + ' hour' + numberEnding(hours);
    }
    var minutes = Math.floor((temp %= 3600) / 60);
    if (minutes) {
        return minutes + ' minute' + numberEnding(minutes);
    }
    var seconds = temp % 60;
    if (seconds) {
        return seconds + ' second' + numberEnding(seconds);
    }
    return 'less than a second'; //'just now' //or other string you like;
}

function convertMillisecondsToDigitalClock(ms) {
    hours = Math.floor(ms / 3600000), // 1 Hour = 36000 Milliseconds
    minutes = Math.floor((ms % 3600000) / 60000), // 1 Minutes = 60000 Milliseconds
    seconds = Math.floor(((ms % 360000) % 60000) / 1000) // 1 Second = 1000 Milliseconds
    return + hours + ' : ' + minutes + ' : ' + seconds;
}

function ShowSuccessMessage(msg) {
    $("#alert-message").html(msg);
    $(".alert").show();
    setTimeout(function () {
        $('.alert').hide();
    }, 2000);
}