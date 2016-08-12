var currentValue = 1;

//fill drop down

$(document).ready(function () {
    $.ajax({
        url: '/Search/Json/',
        type: 'POST',
        data: { baze: 'usd' },
        success: function (result) {
            var obj = $.parseJSON(result);
            if (obj.error == 'error') {
                console.log(obj.msg);
                return;
            }
            var options = '<option value="1">USD</option>';;
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

//client-side table update

$(document).ready(function () {
$('#currency').on('change', function () {
    var val = parseFloat(this.value);
    $('#items tr').each(function () {
        var cash = parseFloat($(this).children('.cash').html());
        $(this).children('.cash').text(
        (val * (cash / currentValue)).toFixed(2) // should probably cache initial values to lessen floating point arithmetic
        // stored a * b  VS  b * (a/c)
        );
    });
    currentValue = val;
    });
    
});