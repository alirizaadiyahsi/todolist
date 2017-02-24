var customEvents = {
    taskAdded: 'taskAddedEvent',
    taskUpdated: 'taskUpdated',
    taskOrderUpdated: 'taskOrderUpdated',
    taskStatusUpdated: 'taskStatusUpdated',
    taskDeleted : 'taskDeleted',
    groupActivated: 'groupActivated',
    groupAdded: 'groupAdded',
    groupUpdated: 'groupUpdated',
    groupOrderUpdated: 'groupOrderUpdated',
    groupDeleted: 'groupDeleted'
};


function initializePage() {
    $('.td-g-list').find('li:first')
        .trigger(customEvents.groupActivated);
}

function changeGroupBadgesCount(completedTasksCount, allTasksCount) {
    var completedTasksBadge = $('ul.td-g-list li.active .td-g-badge-completed');
    var allTasksBadge = $('ul.td-g-list li.active .td-g-badge-all');


    completedTasksBadge.text(Number(completedTasksBadge.text()) + completedTasksCount);
    allTasksBadge.text(Number(allTasksBadge.text()) + allTasksCount);
}

function sortElements(a, b) {
    return ($(b).data('sort')) < ($(a).data('sort')) ? 1 : -1;
}

function updateTasksOrder() {
    var taskIds = $('ul.td-t-list-waiting').sortable("toArray", { attribute: 'data-task-id' });

    $.ajax({
        url: app_root + 'Task/_UpdateTasksOrder',
        data: { taskIds: taskIds },
        traditional: true,
        success: function (response) {

        },
        error: function (response) {
            console.log(response);
        }
    });
}

function updateGroupsOrder() {
    var groupIds = $('ul.td-g-list').sortable("toArray", { attribute: 'data-group-id' });

    $.ajax({
        url: app_root + 'Task/_UpdateGroupsOrder',
        data: { groupIds: groupIds },
        traditional: true,
        success: function (response) {

        },
        error: function (response) {
            console.log(response);
        }
    });
}

function updateTaskGroup(group, task, activeGroup) {
    var groupCountsActive = $(activeGroup).find('.badge').text().split('/');
    var groupCounts = $(group).find('.badge').text().split('/');
    var isCompleted = $(task).find('input[type="checkbox"]').is(':checked');

    console.log(isCompleted);

    $.ajax({
        url: app_root + 'Home/_UpdateTaskGroup',
        data: { groupId: $(group).data('group-id'), taskId: $(task).data('task-id') },
        success: function (response) {

            // group count change
            if (isCompleted) {
                $(activeGroup).find('.badge').html((Number(groupCountsActive[0]) - 1) + '/' + (Number(groupCountsActive[1]) - 1));
                $(group).find('.badge').html((Number(groupCounts[0]) + 1) + '/' + (Number(groupCounts[1]) + 1));
            } else {
                $(activeGroup).find('.badge').html(Number(groupCountsActive[0]) + '/' + (Number(groupCountsActive[1]) - 1));
                $(group).find('.badge').html(Number(groupCounts[0]) + '/' + (Number(groupCounts[1]) + 1));
            }

            $(task).remove();
        },
        error: function (response) {
            console.log(response);
        }
    });
}