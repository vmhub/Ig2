$(document).ready(function () {
    $('#forward').on('click', function (e) {
        e.preventDefault();
        $('#content').load('/Search/Forward');
    });
});
/*
<select id="ddfd">
</select>
<table id="test">
        <tr id="thing-1">
            <td class="name">img</td>
            <td class="schedule">title</td>
            <td class="cash">0.23</td>
        </tr>
            <tr id="thing-1">
            <td class="name">img</td>
            <td class="schedule">title</td>
            <td class="cash">1.23</td>
        </tr>
            <tr id="thing-1">
            <td class="name">img</td>
            <td class="schedule">title</td>
            <td class="cash">0.00</td>
        </tr>
</table>
<button type="button" id="forward"></button>


var gg=1;
$(document).ready(function() {
var obj = $.parseJSON('{"base":"USD","date":"2016-08-09","rates":{"AUD":1.3056,"BGN":1.7655,"BRL":3.1635,"CAD":1.3149,"CHF":0.98384,"CNY":6.6617,"CZK":24.397,"DKK":6.713,"GBP":0.77089,"HKD":7.7586,"HRK":6.7571,"HUF":280.17,"IDR":13132.0,"ILS":3.8201,"INR":66.888,"JPY":102.25,"KRW":1105.5,"MXN":18.506,"MYR":4.0321,"NOK":8.4465,"NZD":1.4012,"PHP":46.889,"PLN":3.8517,"RON":4.0258,"RUB":64.795,"SEK":8.5643,"SGD":1.3478,"THB":34.968,"TRY":2.9802,"ZAR":13.547,"EUR":0.90269}}');
var dd='<option value=' + 2 + '>' + 'USD' + '</option>';
$.each(obj['rates'],function(key,value){
dd += '<option value=' + value + '>' + key + '</option>';
});
$("#ddfd").append(dd);
//repalce $ on server side... + main view has empty dropdown that fills onload (loads once)
});
$('select').on('change', function() {
var f = parseFloat(this.value);
gg=f;
$('#test tr').each(function(){
var d = parseFloat($(this).children('.cash').html());

$(this).children('.cash').text(
(f*d).toFixed(2)
);
});
});

$(document).ready(function () {
    $('#forward').on('click', function (e) {
        e.preventDefault();
        
        $('#test tr').each(function(){
var d = parseFloat($(this).children('.cash').html());

$(this).children('.cash').text(
(d/gg).toFixed(2)
);
});
        
        
    });
});
//alert($('#test tr').length);
//alert(gg);


*/