/// <reference path="../jquery-1.10.2.js" />


$(document).ready(function () {
    //users selected items storing in hidden variable
    $('.col-md-9').find('input[type=checkbox]').change(function () {
        var checkeditems = '';
        $('.col-md-9').find('input[type=checkbox]').each(function () {  
        
            if ($(this).is(':checked'))
            {
                checkeditems = checkeditems + '#' + $(this).attr('userid');
            }
        });
        if (checkeditems.length > 0)
            checkeditems = checkeditems.substr(1, checkeditems.length);
        $('#UserIds').val(checkeditems);
    });
});