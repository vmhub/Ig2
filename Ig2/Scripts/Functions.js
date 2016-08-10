$(document).ready(function () {
    $('#forward').on('click', function (e) {
        e.preventDefault();
        $('#content').load('/Search/Forward');
    });
});
$(document).ready(function () {
    $.ajax({
        url: '/Search/Json/',
        type: 'POST',
        data: { baze: 'usd' },
        success: function (result) {
            var obj = $.parseJSON(result);
            var dd;
            $.each(obj['rates'], function (key, value) {
                dd += '<option value=' + value + '>' + key + '</option>';
            });
            $("#valz").append(dd);
        },
        error: function () {
            alert("error");
        }
    });
});