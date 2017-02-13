$(function () {

    // 
    // task operations...
    // --------------------------------------------------------------------


    // 
    // add task

    $('#tasksMain').on('submit', '#formAddTask', function () {
        var url = $(this).attr('action');
        var taskName = $('#taskName').val();
        var groupId = $('#groups li.active').data('groupid');
        var groupCounts = $('#groups li.active .badge').text().split('/');

        $.ajax({
            url: url,
            data: { taskName: taskName, groupId: groupId },
            success: function (result) {
                $('#tasksTodo').prepend(result);
                $('#taskName').val('');
                $('#groups li.active .badge').html(groupCounts[0] + '/' + (Number(groupCounts[1]) + 1));
            },
            error: function (result) {
                console.log(result);
            }
        });

        return false;
    });

    // 
    // update task status

    $('#tasksMain').on('change', 'input[type="checkbox"]', function () {
        var url = $(this).data('task-status-update-url');
        var task = $(this).parent('li');
        var groupCounts = $('#groups li.active .badge').text().split('/');
        var isCompleted = this.checked;

        console.log(isCompleted);

        $.ajax({
            url: url,
            data: { isCompleted: isCompleted, updateField: 'is_completed' },
            success: function (result) {

                task.remove();

                if (isCompleted) {
                    $('#tasksDone').prepend(result);
                    $('#groups li.active .badge').html((Number(groupCounts[0]) + 1) + '/' + groupCounts[1]);
                } else {
                    $('#tasksTodo').prepend(result);
                    $('#groups li.active .badge').html((Number(groupCounts[0]) - 1) + '/' + groupCounts[1]);
                }

            },
            error: function (result) {
                console.log(result);
            }
        });

    });





    // 
    // group operations...
    // --------------------------------------------------------------------


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
    });

    // select first group, when page first load
    selectFirstGroup();

    $('#groups').on('click', 'li > a', function (e) {

        // if cliecked element is close button
        // dont add active class to 'li' element
        //if (e.target !== this) {
        if ($(e.target).hasClass('icon-close')) {

        } else {
            $(this).parent('li').addClass('active').trigger('groupActivated').siblings().removeClass('active');
        }
    });

    $('#formAddGroup').submit(function () {
        var url = $(this).attr('action');
        var groupName = $('#groupName').val();

        $.ajax({
            url: url,
            data: { groupName: groupName },
            success: function (result) {
                $('#groups').prepend(result);
                $('#groupName').val('');
                // select first group, when page first load
                selectFirstGroup();
            },
            error: function (result) {
                console.log(result);
            }
        });

        return false;
    });

    $('#groups').on('click', '.close', function () {
        var groupId = $(this).data('groupid');
        var deleteUrl = $(this).data('group-delete-url');

        $.ajax({
            url: deleteUrl,
            data: { groupId: groupId },
            success: function (result) {
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
        });

    });
});




// select first group
function selectFirstGroup() {
    $('#groups li').first().addClass('active').trigger('groupActivated').siblings().removeClass('active');
}