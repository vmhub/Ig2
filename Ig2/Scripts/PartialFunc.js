$(document).ready(function () {
    $('#forward').on('click', function (e) {
        e.preventDefault();
        $('#content').load('/Search/Forward');
    });
});
$(document).ready(function () {
    $('#items tr').each(function () {
        var cash = parseFloat($(this).children('.cash').html());
        $(this).children('.cash').text(
        (cash * currentValue).toFixed(2) 
        );
    });
});