var customEvents = {
    taskAdded: 'taskAddedEvent',
    taskUpdated: 'taskAddedEvent',
    taskOrderUpdated: 'taskAddedEvent',
    taskStatusUpdated: 'taskAddedEvent',
    groupAdded: 'taskAddedEvent',
    groupUpdated: 'taskAddedEvent',
    groupOrderUpdated: 'taskAddedEvent',
    groupActivated: 'taskAddedEvent'
};


function initializePage() {
    $('.td-g-list').find('li:first')
        .trigger(customEvents.groupActivated);
}