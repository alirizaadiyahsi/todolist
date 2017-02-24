$(document).on(customEvents.taskAdded, 'li', function (event) {
    var task = $(this);

    changeGroupBadgesCount(0, 1);
});

$(document).on(customEvents.taskStatusUpdated, 'li', function (event, isDragged) {
    var task = $(this);
    var isCompleted = task.find('input[type="checkbox"]').is(':checked');

    $.ajax({
        url: task.data('status-update-url'),
        data: { isCompleted: isCompleted },
        success: function (result) {

            if (!isDragged) {
                task.slideUp('fast', function () {
                    $(this).remove();
                });

                if (isCompleted) {
                    $(result).hide().prependTo('ul.td-t-list-done').slideDown('fast');
                    changeGroupBadgesCount(1, 0);
                } else {
                    $(result).hide().appendTo('ul.td-t-list-waiting').slideDown('fast');
                    changeGroupBadgesCount(-1, 0);
                }
            } else {
                if (isCompleted) {
                    changeGroupBadgesCount(1, 0);
                } else {
                    changeGroupBadgesCount(-1, 0);
                }
            }

            updateTasksOrder();

        },
        error: function (result) {
            console.log(result);
        }
    });

});

$(document).on(customEvents.taskDeleted, 'li', function (event) {
    var task = $(this);
    var deleteUrl = task.data('delete-url');
    var isCompleted = task.find('input[type="checkbox"]').is(':checked');

    $.ajax({
        url: deleteUrl,
        success: function (result) {
            task.remove();
            if (isCompleted) {
                changeGroupBadgesCount(-1, -1);
            } else {
                changeGroupBadgesCount(0, -1);
            }
        },
        error: function (result) {
            console.log(result);
        }
    });
});

/*
 * group operations
 */

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