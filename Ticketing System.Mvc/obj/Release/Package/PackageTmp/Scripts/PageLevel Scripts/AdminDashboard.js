
/// <reference path="../jquery-1.10.2.js" />

function formatJsonDate(jsonDate) {

    var date = new Date(parseInt(jsonDate.substr(6)));
   // var formatted = (date.getDate()).slice(-2) + ("/" + (date.getMonth() + 1)).slice(-2) + date.getFullYear() + "-" + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    var formatted = ("0" + date.getDate()).slice(-2) + "-" +
        ("0" + (date.getMonth() + 1)).slice(-2) + "-" +
       date.getFullYear() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    return formatted;
}



$(document).ready(function () {
   
    //users selected items storing in hidden variable next button event
    $('#nextactivities').click(function () {
        
        var pagenumber = parseInt($(this).attr('pageindex')) + 1;
        $.ajax({
            url: '../Dashboard/GetActivitiesByPageNo?pagenumber=' + pagenumber,
            success: function (data) {

                if (data != null) {
                    $('#nextactivities').attr('pageindex', pagenumber);
                    $('#previousactivities').attr('pageindex', pagenumber);
                    $('#divactivity').html('');
                    var divdata = "";

                    if (data.length == 0 || data.length < 10) {
                        //  $('#nextactivities').hide();
                        $('#divactivity').html('No More Comments Available');

                    }
                    $.each(data, function (i, item) {

                        //bind data to divactivity div

                        if (item.Comments != null) {
                            divdata = divdata + '<div class="well"><li><b> ' + item.CreatedBy + ' </b> has commented on the  task   <a href="~/Tickets/edit?ticketid=' + item.TaskID + '">' + item.DisplayName + '.</a> . +' + item.Comments + ' <span class="pull-right">-' + formatJsonDate(item.CreatedDate) + '</span> </li></div>';
                        }

                        else if (item.N_Title != null && item.O_Title == null && item.N_Description != null && item.O_Description == null) {
                            divdata = divdata + '<div class="well"> <li>  <b> ' + item.CreatedBy + '</b> has been Created a task <a href="~/Tickets/edit?ticketid=' + item.TaskID + '">' + item.DisplayName + '</a>. ( ' + item.N_Description + ')  <span class="pull-right">-' + formatJsonDate(item.CreatedDate) + '</span> </li></div>';
                        }
                        else if (item.N_Task_Statuus != null || item.O_Task_Status != null) {
                            divdata = divdata + '<div class="well"><li> <b> ' + item.CreatedBy + '</b> has beed updated the status of Task <a href="~/Tickets/edit?ticketid=' + item.TaskID + '">' + item.DisplayName + '</a> from <b class="text-warning">  ' + item.O_Task_Status + '</b> to<b class="text-success"> ' + item.N_Task_Statuus + '</b>  <span class="pull-right">-' + formatJsonDate(item.CreatedDate) + '</span></li></div>';
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
        
        var pagenumber ='';
        
        if(parseInt($(this).attr('pageindex'))>0)
            pagenumber=parseInt($(this).attr('pageindex'))-1;

        $.ajax({
            url: '../Dashboard/GetActivitiesByPageNo?pagenumber=' + pagenumber,
            success: function (data) {

                if (data != null) {
                    $('#nextactivities').attr('pageindex', pagenumber);
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
                            divdata = divdata + '<div class="well"><li><b> ' + item.CreatedBy + ' </b> has commented on the  task   <a href="~/Tickets/edit?ticketid=' + item.TaskID + '">' + formatJsonDate(item.DisplayName) + '.</a> . +' + item.Comments + ' <span class="pull-right">-' + formatJsonDate(item.CreatedDate) + '</span> </li></div>';
                        }

                        else if (item.N_Title != null && item.O_Title == null && item.N_Description != null && item.O_Description == null) {
                            divdata = divdata + '<div class="well"> <li>  <b> ' + item.CreatedBy + '</b> has been Created a task <a href="~/Tickets/edit?ticketid=' + item.TaskID + '">' + item.DisplayName + '</a>. ( ' +formatJsonDate( item.N_Description) + ')  <span class="pull-right">-' +formatJsonDate(item.CreatedDate) + '</span> </li></div>';
                        }
                        else if (item.N_Task_Statuus != null || item.O_Task_Status != null) {
                            divdata = divdata + '<div class="well"><li> <b> ' + item.CreatedBy + '</b> has beed updated the status of Task <a href="~/Tickets/edit?ticketid=' + item.TaskID + '">' + item.DisplayName + '</a> from <b class="text-warning">  ' + item.O_Task_Status + '</b> to<b class="text-success"> ' + item.N_Task_Statuus + '</b>  <span class="pull-right">-' +formatJsonDate(item.CreatedDate) + '</span></li></div>';
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
