$(document).ready(function () {
    $('.chkbox').on('change', function () {
        $('.mypas').attr('type',
            $('.chkbox').prop('checked') == true ? "text" : "password");
    });
  });
   