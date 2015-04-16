$(document).ready(function () {
    $('#ddlrole').change(function () {
        window.location.href = "../Users/ListAll?Role=" + $(this).val();
    });
    
});