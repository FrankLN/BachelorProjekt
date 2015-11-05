$(function(){
    $('.date-picker1').datepicker({ dateFormat: 'dd/mm/yy' });
})

$(function(){
    $('.date-picker2').datepicker({ dateFormat: 'dd/mm/yy' });
})

function alertFunction() {
    var dpb = document.getElementById("dpb");
    var dpe = document.getElementById("dpe");

    var dateBegin = dpb.value.split("/");
    dateBegin = dateBegin[2] + dateBegin[1] + dateBegin[0] + "000000";

    var dateEnd = dpe.value.split("/");
    dateEnd = dateEnd[2] + dateEnd[1] + dateEnd[0] + "235959";

    var patientList = getPatientList();


    var redirectUrl = (window.location.href).split("?")[0] + "?db=" + dateBegin + "&de=" + dateEnd;

    console.log(redirectUrl);
    redirect(redirectUrl, "post");
}

function getPatientList() {
    var patients = document.querySelectorAll(".patientCheckbox:checked");
    var i = 0;
    var patientList = [];
    console.log(patients.length)
    if (patients.length > 0) {
        while (i < patients.length) {
            console.log(patients[i].value);
            patientList.push(patients[i].value);
            i++;
        }
    }

    console.log(patientList);
    return patientList;
}


var redirect = function (url, method) {
    var form = document.createElement('form');
    form.method = method;
    form.action = url;
    form.submit();
};