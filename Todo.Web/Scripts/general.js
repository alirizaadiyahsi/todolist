$(function () {

    // ************************************************************************************************************
    // task operations
    // ************************************************************************************************************

    /* Task status change custom event */
    $('#tasksMain').on('taskStatusChanged', 'input[type="checkbox"]', function () {
        var url = $(this).data('task-status-update-url');
        var task = $(this).parent('li');
        var groupCounts = $('#groups li.active .badge').text().split('/');
        var isCompleted = this.checked;

        $.ajax({
            url: url,
            data: { isCompleted: isCompleted, updateField: 'is_completed' },
            success: function (result) {

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
        var groupId = $('#groups li.active').data('groupid');
        var groupCounts = $('#groups li.active .badge').text().split('/');

        $.ajax({
            url: url,
            data: { taskName: taskName, groupId: groupId },
            success: function (result) {
                $('#tasksTodoList').append(result);
                $('#taskName').val('');
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
    }); // end - click

    /* Update task status */
    $('#tasksMain').on('change', 'input[type="checkbox"]', function () {
        $(this).trigger('taskStatusChanged');
    }); // end - change



    // ************************************************************************************************************
    // group operations
    // ************************************************************************************************************

    /* Group activated custom event */
    $('#groups').on('groupActivated', 'li', function () {
        var groupId = $(this).data('groupid');
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
    $('#groups').on('click', 'li > a', function (e) {
        // if cliecked element is close button
        // dont add active class to 'li' element
        if ($(e.target).hasClass('icon-close')) {

        } else {
            $(this).parent('li').addClass('active').trigger('groupActivated').siblings().removeClass('active');
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
            },
            error: function (result) {
                console.log(result);
            }
        }); // end - ajax

        return false;
    }); // end - submit

    /* Delete group */
    $('#groups').on('click', '.close', function () {
        var groupId = $(this).data('groupid');
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
    }); // end - click

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