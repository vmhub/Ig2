$(document).ready(function () {
    $('#forward').on('click', function (e) {
        e.preventDefault();
        $('#content').load('/Search/Forward');
    });
});
$(document).ready(function () {
    var val = parseFloat($("#currency option:selected").val());
        checkList(val);
});