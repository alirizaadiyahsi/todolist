//
// Run on page first load
//
$(function () {
    initializePage();
});

//
// Task operations
//
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

$(document).on('change', 'ul.td-t-list input[type="checkbox"]', function () {
    var isCompleted = this.checked;
    var task = $(this).closest('li');

    task.trigger(customEvents.taskStatusUpdated);
});

$(document).on('click', 'ul.td-t-list li a .close', function () {
    if (confirm('Are you sure want to delete?')) {
        var task = $(this).closest('li');

        task.trigger(customEvents.taskDeleted);
    }
});

$(document).on('dblclick', 'ul.td-t-list li', function () {

    var task = $(this);
    var taskEditForm = task.find('form');
    var cancelButton = task.find('.btn-cancel-save-task');
    var taskNameInput = task.find('input[type="text"]');

    task.addClass('editable-active').siblings().removeClass('editable-active');
    taskNameInput.focus();

    cancelButton.click(function () {
        task.removeClass('editable-active');
    });

    taskEditForm.submit(function () {
        $.ajax({
            url: $(this).attr('action'),
            data: { name: taskNameInput.val() },
            success: function (response) {
                task.find('span.td-t-item-name').text(taskNameInput.val());
                task.removeClass('editable-active');
            },
            error: function () {
                console.log(response);
            }
        });

        return false;
    });
});


//
// group operations
//

$(document).on('click', '#btnAddGroup', function () {
    var form = $(this).closest('form');

    $.ajax({
        type: form.attr('method'),
        url: form.attr('action'),
        data: form.serialize(),
        success: function (result) {
            $('ul.td-g-list').append($(result));
            $('#inputGroupName').val('');
        },
        error: function (result) {
            console.log(result);
        }
    });

    return false;
});

$(document).on('click', 'ul.td-g-list li', function (e) {
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

$(document).on('dblclick', 'ul.td-g-list li', function () {

    var group = $(this);
    var groupEditForm = group.find('form');
    var cancelButton = group.find('.btn-cancel-save-group');
    var groupNameInput = group.find('input[type="text"]');

    group.addClass('editable-active').siblings().removeClass('editable-active');
    groupNameInput.focus();

    cancelButton.click(function () {
        group.removeClass('editable-active');
    });

    groupEditForm.submit(function () {
        $.ajax({
            url: $(this).attr('action'),
            data: { name: groupNameInput.val() },
            success: function (response) {
                group.find('span.td-g-item-name').text(groupNameInput.val());
                group.removeClass('editable-active');
            },
            error: function () {
                console.log(response);
            }
        });

        return false;
    });
});
