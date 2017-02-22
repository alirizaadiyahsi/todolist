$(document).on(customEvents.taskAdded, 'li', function (event) {
    var task = $(this);

    changeGroupBadgesCount(0, 1);
});

$(document).on(customEvents.taskUpdated, 'li', function (event) {

    // TODO: task updated actions

});

$(document).on(customEvents.taskOrderUpdated, 'li', function (event) {

    // TODO: task order updated actions

});

$(document).on(customEvents.taskStatusUpdated, 'li', function (event) {

    // TODO: task status updated actions

});

$(document).on(customEvents.groupActivated, 'li', function (event) {
    var group = $(this);
    var url = group.data('tasklist-url');

    group.addClass('active')
        .siblings().removeClass('active');

    $.ajax({
        url: url,
        success: function (result) {
            $('.td-t-all-container').html(result);
            $('input[name=groupId]').val(group.data('group-id'));
        },
        error: function (result) {
            console.log(result);
        }
    });
});

$(document).on(customEvents.groupAdded, 'li', function (event) {
    var group = $(this);

    // TODO: add drop event
    // add 'ui-sortable-handle' class (this class prevent touch event)
});

$(document).on(customEvents.groupUpdated, 'li', function (event) {

    // TODO: group updated actions

});

$(document).on(customEvents.groupOrderUpdated, 'li', function (event) {

    // TODO: group order updated event

});

$(document).on(customEvents.groupDeleted, 'li', function (event) {
    var group = $(this);
    var deleteUrl = group.data('delete-url');

    $.ajax({
        url: deleteUrl,
        success: function (result) {
            if (group.hasClass('active')) {
                group.remove();
                $('ul.td-g-list').find('li:first')
                    .trigger(customEvents.groupActivated);
            } else {
                group.remove();
            }
        },
        error: function (result) {
            console.log(result);
        }
    });
});