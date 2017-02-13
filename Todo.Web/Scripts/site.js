$(function () {

    // 
    // task operations...
    // --------------------------------------------------------------------


    $('#formAddTask').submit(function () {
        var url = $(this).attr('action');
        var taskName = $('#taskName').val();
        var groupId = $('#groups li.active').data('groupid');

        var groupCounts = $('#groups li.active .badge').text().split('/');

        $.ajax({
            url: url,
            data: { taskName: taskName, groupId: groupId },
            success: function (result) {
                $('#tasks').prepend(result);
                $('#taskName').val('');
                $('#groups li.active .badge').html(groupCounts[0] + '/' + (Number(groupCounts[1]) + 1));
            },
            error: function (result) {
                alert(result);
            }
        });

        return false;
    });






    // 
    // group operations...
    // --------------------------------------------------------------------


    $('#groups li').on('groupActivated', function () {
        var groupId = $(this).data('groupid');
        var url = $(this).data('gettasksurl');

        $.ajax({
            url: url,
            data: { groupId: groupId },
            success: function (result) {
                $('#tasks').html(result);
            },
            error: function (result) {
                alert(result);
            }
        });
    });

    // select first group, when page first load
    selectFirstGroup();

    $('#groups').on('click', 'li > a', function (e) {

        // if cliecked element is close button
        // dont add active class to 'li' element
        if (e.target !== this) {

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
            },
            error: function (result) {
                alert(result);
            }
        });

        return false;
    });

    $('#groups').on('click', '.close', function () {
        var groupId = $(this).data('groupid');
        var deleteUrl = $(this).data('deleteurl');

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
                alert(result);
            }
        });

    });
});




// select first group
function selectFirstGroup() {
    $('#groups li').first().addClass('active').trigger('groupActivated');
}