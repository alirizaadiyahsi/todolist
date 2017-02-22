var customEvents = {
    taskAdded: 'taskAddedEvent',
    taskUpdated: 'taskUpdated',
    taskOrderUpdated: 'taskOrderUpdated',
    taskStatusUpdated: 'taskStatusUpdated',
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