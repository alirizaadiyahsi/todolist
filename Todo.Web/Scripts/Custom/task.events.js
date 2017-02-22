$(function () {
    initializePage();
});

$(document).on('click', '.btn-auto-submit', function () {
    var form = $(this).closest('form');

    $.ajax({
        type: form.attr('method'),
        url: form.attr('action'),
        data: form.serialize(),
        success: function (result) {
            $('.td-g-list').append($(result));
            $('#inputGroupName').val('');
            $('.td-g-list').find("li:last")
                .trigger(customEvents.groupAdded);
        },
        error: function (result) {
            console.log(result);
        }
    });

    return false;
});

$(document).on('click', '.td-g-list li a', function () {
    $(this).closest('li')
        .trigger(customEvents.groupActivated);
});
