// Write your Javascript code.

$(function() {
    $('[data-toggle="tooltip"]').tooltip({
        delay: {
            show: 500,
            hide: 100
        }
    });
});

$(function() {
    $('[data-provide="datepicker"]').datepicker({
        format: 'dd-mm-yyyy',
        endDate: '0d'
    });
});




$('[data-toggle="modal"]').click(function () {
    $("#DeleteModal").find("input[type='hidden']").val($(this).data("ord-id"));
});
