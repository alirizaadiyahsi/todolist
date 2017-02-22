$(function () {
    initializePage();
});

$(document).on('click', '#btnAddTask', function () {
    var form = $(this).closest('form');

    $.ajax({
        type: form.attr('method'),
        url: form.attr('action'),
        data: form.serialize(),
        success: function (result) {
            $('ul.td-t-list-waiting').append($(result));
            $('#inputTaskName').val('');
            $('ul.td-t-list').find("li:last")
                .trigger(customEvents.taskAdded);
        },
        error: function (result) {
            console.log(result);
        }
    });

    return false;
});

$(document).on('click', '#btnAddGroup', function () {
    var form = $(this).closest('form');

    $.ajax({
        type: form.attr('method'),
        url: form.attr('action'),
        data: form.serialize(),
        success: function (result) {
            $('ul.td-g-list').append($(result));
            $('#inputGroupName').val('');
            $('ul.td-g-list').find("li:last")
                .trigger(customEvents.groupAdded);
        },
        error: function (result) {
            console.log(result);
        }
    });

    return false;
});

$(document).on('click', 'ul.td-g-list li', function (e) {
    // if cliked button isn't the close button
    if (!$(e.target).hasClass('icon-close')) {
        var group = $(this);

        group.trigger(customEvents.groupActivated);
    }
});

$(document).on('click', 'ul.td-g-list li a .close', function () {
    if (confirm('Are you sure want to delete?')) {
        var group = $(this).closest('li');

        group.trigger(customEvents.groupDeleted);
    }
});
