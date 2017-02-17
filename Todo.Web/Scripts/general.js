$(function () {

    // ************************************************************************************************************
    // task operations
    // ************************************************************************************************************

    /* Task status change custom event */
    $('#tasksMain').on('taskStatusChanged', 'input[type="checkbox"]', function (event, isDragged) {
        var url = $(this).data('task-status-update-url');
        var task = $(this).closest('li');
        var groupCounts = $('#groups li.active .badge').text().split('/');
        var isCompleted = this.checked;

        $.ajax({
            url: url,
            data: { isCompleted: isCompleted, updateField: 'is_completed' },
            success: function (result) {

                if (!isDragged) {
                    task.remove();

                    // group count change
                    if (isCompleted) {
                        $('#tasksDoneList').prepend(result);
                        $('#groups li.active .badge').html((Number(groupCounts[0]) + 1) + '/' + groupCounts[1]);
                    } else {
                        $('#tasksTodoList').prepend(result);
                        $('#groups li.active .badge').html((Number(groupCounts[0]) - 1) + '/' + groupCounts[1]);
                        $("#tasksTodoList li").sort(sortElements).appendTo('#tasksTodoList');
                    }
                } else {
                    if (isCompleted) {
                        $('#groups li.active .badge').html((Number(groupCounts[0]) + 1) + '/' + groupCounts[1]);
                    } else {
                        $('#groups li.active .badge').html((Number(groupCounts[0]) - 1) + '/' + groupCounts[1]);
                    }
                }
            },
            error: function (result) {
                console.log(result);
            }
        }); // end - ajax
    }); // end - custom event

    /* Add task */
    $('#tasksMain').on('submit', '#formAddTask', function () {
        var url = $(this).attr('action');
        var taskName = $('#taskName').val();
        var groupId = $('#groups li.active').data('group-id');
        var groupCounts = $('#groups li.active .badge').text().split('/');

        $.ajax({
            url: url,
            data: { taskName: taskName, groupId: groupId },
            success: function (result) {
                $('#tasksTodoList').append(result);
                $('#taskName').val('');
                $('#tasksTodoList').sortable("refresh");

                // group count change
                $('#groups li.active .badge').html(groupCounts[0] + '/' + (Number(groupCounts[1]) + 1));
            },
            error: function (result) {
                console.log(result);
            }
        }); // end - ajax

        return false;
    }); // end - submit

    /* Delete task */
    $('#tasksMain').on('click', '.close', function () {

        if (confirm('Are you sure want to delete?')) {
            var taskId = $(this).data('task-id');
            var deleteUrl = $(this).data('task-delete-url');
            var groupCounts = $('#groups li.active .badge').text().split('/');

            $.ajax({
                url: deleteUrl,
                data: { taskId: taskId },
                success: function (result) {
                    // group count change
                    if ($('#taskChk_' + taskId).is(':checked')) {
                        $('#groups li.active .badge').html((Number(groupCounts[0]) - 1) + '/' + (Number(groupCounts[1]) - 1));
                    } else {
                        $('#groups li.active .badge').html((groupCounts[0]) + '/' + (Number(groupCounts[1]) - 1));
                    }

                    $('#task_' + taskId).remove();

                },
                error: function (result) {
                    console.log(result);
                }
            });// end - ajax
        }

    }); // end - click

    /* Update task status */
    $('#tasksMain').on('change', 'input[type="checkbox"]', function () {
        $(this).trigger('taskStatusChanged');
    }); // end - change

    /* Update task name */
    $('#tasksMain').on('dblclick', 'li', function () {

        var clickedTask = $(this);
        var taskId = clickedTask.data('task-id');
        var clickedTaskInputGroup = $('#taskInput_' + taskId);
        var saveButton = clickedTaskInputGroup.find('.btn-save-task');
        var cancelButton = clickedTaskInputGroup.find('.btn-cancel-save');
        var taskEditForm = $(this).find('form');
        var taskInput = clickedTask.find('input[type="text"]');
        var allInputGroups = $('.input-task-edit');

        // reset editable
        $('#tasksMain').find('li').removeClass('.editable-active').children().show();
        allInputGroups.hide();

        // add editable
        clickedTask.addClass('.editable-active').children().hide();
        taskEditForm.show();
        taskInput.focus();

        cancelButton.click(function () {
            clickedTask.removeClass('.editable-active').children().show();
            clickedTaskInputGroup.hide();
        });

        taskEditForm.submit(function () {
            $.ajax({
                url: $(this).attr('action'),
                data: { name: taskInput.val(), updateField: 'name' },
                success: function (response) {
                    clickedTask.find('span.task-name').text(taskInput.val());

                    // reset editable
                    clickedTask.removeClass('.editable-active').children().show();
                    clickedTaskInputGroup.hide();
                },
                error: function () {
                    console.log(response);
                }
            });

            return false;
        });
    });



    // ************************************************************************************************************
    // group operations
    // ************************************************************************************************************

    /* Group activated custom event */
    $('#groups').on('groupActivated', 'li', function () {
        var groupId = $(this).data('group-id');
        var url = $(this).data('get-tasks-url');

        $.ajax({
            url: url,
            data: { groupId: groupId },
            success: function (result) {
                $('#tasksMain').html(result);
            },
            error: function (result) {
                console.log(result);
            }
        });
    }); // end - custom event

    /* Select first group */
    selectFirstGroup();

    /* Active group */
    $('#groups').on('click', 'li', function (e) {
        // if cliecked element is close button
        // dont add active class to 'li' element
        if ($(e.target).hasClass('icon-close')) {

        } else {
            $(this).addClass('active').trigger('groupActivated').siblings().removeClass('active');
        }
    }); // end - click

    /* Add group */
    $('#formAddGroup').submit(function () {
        var url = $(this).attr('action');
        var groupName = $('#groupName').val();

        $.ajax({
            url: url,
            data: { groupName: groupName },
            success: function (result) {
                $('#groups').append(result);
                $('#groupName').val('');
                $('#groups').sortable("refresh");

                /* Task dropped to group */
                $('#' + $(result).attr('id')).droppable({
                    accept: '#tasksTodoList li,#tasksDoneList li',
                    tolerance: 'pointer',
                    drop: function (event, ui) {
                        var activeGroupId = $('#groups li.active').data('group-id');
                        var droppedGroupId = $(this).data('group-id');
                        var taskId = $(ui.draggable).data('task-id');

                        if (activeGroupId != droppedGroupId) {
                            updateTaskGroup(this, ui.draggable, $('#groups li.active'));
                        }
                    }
                });
            },
            error: function (result) {
                console.log(result);
            }
        }); // end - ajax

        return false;
    }); // end - submit

    /* Delete group */
    $('#groups').on('click', '.close', function () {
        if (confirm('Are you sure want to delete?')) {
            var groupId = $(this).data('group-id');
            var deleteUrl = $(this).data('group-delete-url');

            $.ajax({
                url: deleteUrl,
                data: { groupId: groupId },
                success: function (result) {
                    // if remove group, first, jquery couldn't found group
                    if ($('#group_' + groupId).hasClass('active')) {
                        $('#group_' + groupId).remove();
                        selectFirstGroup();
                    } else {
                        $('#group_' + groupId).remove();
                    }

                },
                error: function (result) {
                    console.log(result);
                }
            }); // end - ajax
        }
    }); // end - click

    /* Update group name */
    $('#groups').on('dblclick', 'li', function () {

        var clickedGroup = $(this);
        var groupId = clickedGroup.data('group-id');
        var clickedGroupInputGroup = $('#groupInput_' + groupId);
        var saveButton = clickedGroupInputGroup.find('.btn-save-group');
        var cancelButton = clickedGroupInputGroup.find('.btn-cancel-save');
        var groupEditForm = $(this).find('form');
        var groupInput = clickedGroup.find('input[type="text"]');
        var allInputGroups = $('.input-group-edit');

        // reset editable
        $('#groups').find('li').removeClass('.editable-active').children().show();
        allInputGroups.hide();

        // add editable
        clickedGroup.addClass('.editable-active').children().hide();
        groupEditForm.show();
        groupInput.focus();

        cancelButton.click(function () {
            clickedGroup.removeClass('.editable-active').children().show();
            clickedGroupInputGroup.hide();
        });

        groupEditForm.submit(function () {
            $.ajax({
                url: $(this).attr('action'),
                data: { name: groupInput.val() },
                success: function (response) {
                    clickedGroup.find('span.group-name').text(groupInput.val());

                    // reset editable
                    clickedGroup.removeClass('.editable-active').children().show();
                    clickedGroupInputGroup.hide();
                },
                error: function () {
                    console.log(response);
                }
            });

            return false;
        });
    });

}); // end - document ready



// ****************************************************************************************************************
// methods
// ****************************************************************************************************************

/* Select first group */
function selectFirstGroup() {
    $('#groups li').first().addClass('active').trigger('groupActivated').siblings().removeClass('active');
}

/* Sort elements */
function sortElements(a, b) {
    return ($(b).data('sort')) < ($(a).data('sort')) ? 1 : -1;
}

/* Update tasks order */
function updateTasksOrder() {
    var taskIds = $('#tasksTodoList').sortable("toArray", { attribute: 'data-task-id' });

    $.ajax({
        url: app_root + 'Home/_UpdateTasksOrder',
        data: { taskIds: taskIds },
        traditional: true,
        success: function (response) {

        },
        error: function (response) {
            console.log(response);
        }
    });
}

/* Update groups order */
function updateGroupsOrder() {
    var groupIds = $('#groups').sortable("toArray", { attribute: 'data-group-id' });

    $.ajax({
        url: app_root + 'Home/_UpdateGroupsOrder',
        data: { groupIds: groupIds },
        traditional: true,
        success: function (response) {

        },
        error: function (response) {
            console.log(response);
        }
    });
}

/* Update groups order */
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