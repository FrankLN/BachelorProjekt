$(function(){
    $('.date-picker1').datepicker();
})

$(function(){
    $('.date-picker2').datepicker();
})

function alertFunction(caller) {
    var dpb = document.getElementById("dpb");
    var dpe = document.getElementById("dpe");

    if (caller == 'dpb') {
        var dateBegin = dpb.value.split("/");
        dateBegin = dateBegin[2] + dateBegin[0] + dateBegin[1] + "000000";

        var dateEnd = dpe.value.split("/");
        dateEnd = dateEnd[2] + dateEnd[1] + dateEnd[0] + "000000";
    }
    else {
        var dateBegin = dpb.value.split("/");
        dateBegin = dateBegin[2] + dateBegin[1] + dateBegin[0] + "000000";

        var dateEnd = dpe.value.split("/");
        dateEnd = dateEnd[2] + dateEnd[0] + dateEnd[1] + "000000";
    }

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