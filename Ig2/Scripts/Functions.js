$(document).ready(function () {
    $('#forward').on('click', function (e) {
        e.preventDefault();
        $('#content').load('/Search/Forward');
    });
});
//http://stackoverflow.com/questions/15437007/partial-view-click-event-not-fire