$(document).ready(function () {
    $('#forward').on('click', function (e) {
        e.preventDefault();
        $('#content').load('/Search/Forward');
    });
});