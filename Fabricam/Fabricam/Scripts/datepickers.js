$(document).ready(function() {

    // Date picker setup:
    var dateofbirth_datepicker = $("#dateofbirth-datepicker");

    // Set up JQueryUI datepicker.
    dateofbirth_datepicker.datepicker({
        showOn: "focus",
        dateFormat: 'dd/mm/yy'
    });

    var datajoined_datepicker = $('#datejoined-datepicker');

    datajoined_datepicker.datepicker({
        showOn: 'focus',
        dateFormat: 'dd/mm/yy'
    });

});