/// <reference path="../jquery-1.10.2.js" />


$(document).ready(function () {
     
    $.ajax({
        url: '../Dashboard/GetRecentTickets',
        success: function (data) {
            $('.recentticketsdiv').html('');
            $.each(data, function (i, item) {
                var description = item.Description;
                if (description.Length > 20)
                    description = description.substring(1, 20);
                $('.recentticketsdiv').append('<li class="news-item">' + description + '<a href=../Tickets/Edit?ticketid=' + item.ID + '></br>  Read more...</a></li>');
            });
        } 
    });


    // edit profile model 

    $('#hrefedtitprofile').click(function () {

        $.ajax({
            url: '../Account/ChangeProfile',
            success: function (data) {
  

                if (data.Status == 0)
                {
 
                    $('#txtfirstname').val(data.Response.FirstName);
                    $('#txtlastname').val(data.Response.LastName);
                    $('#txtmobilenumber').val(data.Response.MobileNumber);
                    $('#EditProfile').modal('show');
                }
                

            }
        });


    });


    // save edit profile data

    $('#btnupdateprofile').click(function () {
       
        $.ajax({
            url: '../Account/PostChangeProfile?FirstName='+$('#txtfirstname').val()+"&LastName="+$('#txtlastname').val()+"&MobileNumber="+$('#txtmobilenumber').val(), 
            success: function (data) {
                if (data.Status == 0) {
                    $('#EditProfile').modal('hide');
                }
                else {

                    $('#lblerrormessage').text(data.Message);
                }

            }
        });


    });

    // save change password data

    $('#btnupdatepassword').click(function () {
        $.ajax({
            url: '../Account/ChangePassword?oldpassword=' + $('#txtoldpassword').val() + "&newpassword=" + $('#txtnewpasssword').val(),
            success: function (data) {
                if (data.Status == 0) {
                    $('#hrefchangepassword').modal('hide');
                }
                else {

                    $('#lblerrormessagereset').text(data.Message);
                }

            }
        });


    });

    //users selected items storing in hidden variable next button event
    $('#nextactivities').click(function () {

        var pagenumber = parseInt($(this).attr('pageindex')) + 1;
        $.ajax({
            url: '../UserDashboard/GetActivitiesByPageNo?pagenumber=' + pagenumber,
            success: function (data) {

                if (data != null) {
                    $('#nextactivities').attr('pageindex', pagenumber);
                    $('#divactivity').html('');
                    var divdata = "";

                    if (data.length == 0 || data.length < 10) {
                        //  $('#nextactivities').hide();
                        $('#divactivity').html('No More Comments Available');

                    }
                    $.each(data, function (i, item) {

                        //bind data to divactivity div

                        if (item.Comments != null) {
                            divdata = divdata + '<div class="well"><li><b> ' + item.CreatedBy + ' </b> has commented on the  task   <a href="~/Tickets/edit?ticketid=' + item.TaskID + '">' + item.DisplayName + '.</a> . +' + item.Comments + ' <span class="pull-right">-' + item.CreatedDate + '</span> </li></div>';
                        }

                        else if (item.N_Title != null && item.O_Title == null && item.N_Description != null && item.O_Description == null) {
                            divdata = divdata + '<div class="well"> <li>  <b> ' + item.CreatedBy + '</b> has been Created a task <a href="~/Tickets/edit?ticketid=' + item.TaskID + '">' + item.DisplayName + '</a>. ( ' + item.N_Description + ')  <span class="pull-right">-' + item.CreatedDate + '</span> </li></div>';
                        }
                        else if (item.N_Task_Statuus != null || item.O_Task_Status != null) {
                            divdata = divdata + '<div class="well"><li> <b> ' + item.CreatedBy + '</b> has beed updated the status of Task <a href="~/Tickets/edit?ticketid=' + item.TaskID + '">' + item.DisplayName + '</a> from <b class="text-warning">  ' + item.O_Task_Status + '</b> to<b class="text-success"> ' + item.N_Task_Statuus + '</b>  <span class="pull-right">-' + item.CreatedDate + '</span></li></div>';
                        }
                    });
                    $('#divactivity').html(divdata);
                }
                else {
                    //  $('#nextactivities').hide();
                }
            }
        });



    });

    // Previous button Event handler
    $('#previousactivities').click(function () {

        var pagenumber = parseInt($(this).attr('pageindex')) - 1;
        $.ajax({
            url: '../UserDashboard/GetActivitiesByPageNo?pagenumber=' + pagenumber,
            success: function (data) {

                if (data != null) {
                    $('#previousactivities').attr('pageindex', pagenumber);
                    $('#divactivity').html('');
                    var divdata = "";

                    if (data.length == 0 || data.length < 10) {
                        //  $('#previousactivities').hide();
                        $('#divactivity').html('No More Comments Available');

                    }
                    $.each(data, function (i, item) {

                        //bind data to divactivity div

                        if (item.Comments != null) {
                            divdata = divdata + '<div class="well"><li><b> ' + item.CreatedBy + ' </b> has commented on the  task   <a href="~/Tickets/edit?ticketid=' + item.TaskID + '">' + item.DisplayName + '.</a> . +' + item.Comments + ' <span class="pull-right">-' + item.CreatedDate + '</span> </li></div>';
                        }

                        else if (item.N_Title != null && item.O_Title == null && item.N_Description != null && item.O_Description == null) {
                            divdata = divdata + '<div class="well"> <li>  <b> ' + item.CreatedBy + '</b> has been Created a task <a href="~/Tickets/edit?ticketid=' + item.TaskID + '">' + item.DisplayName + '</a>. ( ' + item.N_Description + ')  <span class="pull-right">-' + item.CreatedDate + '</span> </li></div>';
                        }
                        else if (item.N_Task_Statuus != null || item.O_Task_Status != null) {
                            divdata = divdata + '<div class="well"><li> <b> ' + item.CreatedBy + '</b> has beed updated the status of Task <a href="~/Tickets/edit?ticketid=' + item.TaskID + '">' + item.DisplayName + '</a> from <b class="text-warning">  ' + item.O_Task_Status + '</b> to<b class="text-success"> ' + item.N_Task_Statuus + '</b>  <span class="pull-right">-' + item.CreatedDate + '</span></li></div>';
                        }
                    });
                    $('#divactivity').html(divdata);
                }
                else {
                    //  $('#previousactivities').hide();
                }
            }
        });



    });

});