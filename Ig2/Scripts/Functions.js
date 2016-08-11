$(document).ready(function () {
    $.ajax({
        url: '/Search/Json/',
        type: 'POST',
        data: { baze: 'usd' },
        success: function (result) {
            var obj = $.parseJSON(result);
            var options;
            $.each(obj['rates'], function (key, value) {
                options += '<option value=' + value + '>' + key + '</option>';
            });
            $("#currency").append(options);
        },
        error: function () {
            console.log('Error retrieving json.');
        }
    });
});