/// <reference path="../jquery-1.10.2.js" />


$(document).ready(function () {
    $('#CreateNewProject').hide();
    $.ajax({
        url: '../UserDashboard/GetRecentTickets',
        success: function (data) {

            $('.recentticketsdiv').html('');

            $.each(data, function (i, item) {
                var description = item.Description;
                if(description.Length>20)
                description = description.substring(1, 20);

                $('.recentticketsdiv').append('<li class="news-item">' + description + '<a href=../Tickets/Edit?ticketid=' + item.ID + '></br> Read more...</a></li>');
            });
        } 
    });




    // edit profile model 

    $('#hrefedtitprofile').click(function () {

        $.ajax({
            url: '../Account/ChangeProfile',
            success: function (data) {


                if (data.Status == 0) {
                    $('#txtfirstname').val(data.Response.FirstName);
                    $('#txtlastname').val(data.Response.LastName);
                    $('#txtmobilenumber').val(data.Response.MobileNumber);
                    $('#EditProfile').modal('show');
                }

            }
        });


    });


    // save edit profile data

    $('#hrefchangepassword').click(function () {
        $.ajax({
            url: '../Account/Logout?FirstName=' + $('#txtfirstname').val() + "&LastName=" + $('#txtlastname').val() + "&MobileNumber=" + $('#txtmobilenumber').val(),  

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

});