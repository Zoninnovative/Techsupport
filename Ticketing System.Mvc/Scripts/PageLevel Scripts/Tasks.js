/// <reference path="../jquery-1.10.2.min.js" />
 
$(document).ready(function () {
    $('#btnFilterTasks').click(function () {
        window.location.href = "../Tickets/ListAll?ProjectID=" + $('#ddlproject').val() + "&task_Type=" + $('#ddltasktype').val() + "&task_status=" + $('#ddltaskstatus').val() + "&priority=" + $('#ddlpriority').val();
    });


    $('#addFileUpload').click(function () {
        debugger;
        if ($('.fileUploader').length < 3) {
            var fileUploader = $('#fileUploader').clone();
            $(fileUploader).find('.col-md-1 i').removeClass('fa-plus').addClass('fa-times');
            $(fileUploader).find('.col-md-1 button').addClass('deleteFile');
            $('#appendFileUpload').append(fileUploader);
        }
    });
    $('#appendFileUpload').on('click', '.deleteFile', function () {
        $(this).parents('.fileUploader').remove();
    });


    $('#btnpostcomment').click(function () {
        var commenttext = $('#CommentText').val();
    });
    $('#editTaskForm').validate({
        rules: {
            Title: {
                required: true
            },
            Description: {
                required: true
            },
            ProjectID: {
                required: true
            },
            AssignedTo: {
                required: true
            },
            RefereedTo: {
                required: true
            },
            DueDate: {
                required: true
            },
            AssigndedDate: {
                required: true
            },
            PriorityID: {
                required: true
            },
            Task_Status: {
                required: true
            },
            TypeID: {
                required: true
            },
        },
        messages: {
            Title: {
                required: "Please enter title"
            },
            Description: {
                required: "Please enter description"
            },
            ProjectID: {
                required: "Please select project"
            },
            AssignedTo: {
                required: "Please select email"
            },
            RefereedTo: {
                required: "Please select email"
            },
            DueDate: {
                required: "Please select due date"
            },
            AssigndedDate: {
                required: "Please select assigned date"
            },
            PriorityID: {
                required: "Please select priority"
            },
            Task_Status: {
                required: "Please select status"
            },
            TypeID: {
                required: "Please select type"
            },
        },

    });

    $('.img-pop').click(function () {
        $('#screenshotPop').html($(this).html());
        $('#screenshotPop').find('img').removeAttr('height').addClass('col-md-12');
        $('#screenName').html($(this).find('img').attr('title'));
    });
});