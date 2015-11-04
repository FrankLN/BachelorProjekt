$(function(){
    $('.date-picker1').datepicker({ dateFormat: 'dd/mm/yy' });
})

$(function(){
    $('.date-picker2').datepicker({ dateFormat: 'dd/mm/yy' });
})

function alertFunction(caller) {
    var dpb = document.getElementById("dpb");
    var dpe = document.getElementById("dpe");

    var dateBegin = dpb.value.split("/");
    dateBegin = dateBegin[2] + dateBegin[1] + dateBegin[0] + "000000";

    var dateEnd = dpe.value.split("/");
    dateEnd = dateEnd[2] + dateEnd[1] + dateEnd[0] + "235959";


    var redirectUrl = (window.location.href).split("?")[0] + "?db=" + dateBegin + "&de=" + dateEnd;

    console.log(redirectUrl);
    redirect(redirectUrl, "post");
}

var redirect = function (url, method) {
    var form = document.createElement('form');
    form.method = method;
    form.action = url;
    form.submit();
};