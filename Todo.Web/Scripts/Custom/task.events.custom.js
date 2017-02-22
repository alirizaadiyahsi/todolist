$(document).on(customEvents.taskAdded, 'li', function (event) {

    // TODO: task added actions

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

$(document).on(customEvents.groupActivated, 'li', function (event) {
    var group = $(this);

    group.addClass('active')
        .siblings().removeClass('active');

    // TODO: ajax call to this group tasks
});