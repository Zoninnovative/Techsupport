/// <reference path="../jquery-1.10.2.min.js" />

$(document).ready(function () {


    $('#dataTables-example').DataTable({
        responsive: true,
        "lengthMenu": [[10, 25, 50, 100, 500, 1000, -1], [10, 25, 50, 100, 500, 1000, "All"]]
    });

    $('#FromDate,#ToDate').datepicker({
        format: "mm/dd/yyyy"
    });

    $('#btndownloadreport').bind('click', function (e) {
        $('#dataTables-example').tableExport({ type: 'pdf', escape: 'false', ignoreColumn: [], });
    });

    $('.btngeneratereport').change(function () {
        // validate from and to dates
        debugger;
        var fromdate = $('#FromDate').val();
        var todate = $('#ToDate').val();
        if (fromdate == "" || todate == "") {
            $('#lblreporterrormessage').text('Please provide from and to dates');
        }
        else
            window.location.href = "../UserReport/GenerateReport?userID=" + $('#UserID').val() + "&FromDate=" + $('#FromDate').val() + "&ToDate=" + $('#ToDate').val() + "&TaskType=" + $('#TaskTypeID').val() + "&TaskStatus=" + $('#TaskStatusID').val();
    });


    $('#FromDate,#ToDate').click(function () {

        $('#lblreporterrormessage').text('');
    });
    
    //$('#btnemailreport').click(function () {
    //    window.location.href = "../ProjectReport/ListAll?ProjectID=" + $(this).val();
    //});
    $('#btnemailreport').click(function () {
        //generate report and send it to user
        ////alert('this is email report');
        ////alert($('#ProjectID').val());
        //$.ajax({
        //    url: "../ProjectReport/GenerateReportEmail?ProjectID=" + $('#ProjectID').val() + "&FromDate=" + $('#FromDate').val() + "&ToDate=" + $('#ToDate').val() + "&TaskType=" + $('#TaskTypeID').val() + "&TaskStatus=" + $('#TaskStatusID').val(),
        //    success: function (data) {
        //        $('#lblreporterrormessage').text(data.Message);
        //    }
        //});
        
        $.ajax({
            url: "../UserReport/GenerateReportEmail?userID=" + $('#UserID').val() + "&FromDate=" + $('#FromDate').val() + "&ToDate=" + $('#ToDate').val() + "&TaskType=" + $('#TaskTypeID').val() + "&TaskStatus=" + $('#TaskStatusID').val(),
            success: function (data) {
                $('#lblreporterrormessage').text(data.Message);
            }
        });
    });




});