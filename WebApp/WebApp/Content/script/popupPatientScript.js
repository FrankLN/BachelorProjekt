$(function () {
      $( "#dialog" ).dialog({
          autoOpen: false,

      });
 
      $( "#opener" ).click(function() {
          $("#dialog").dialog("open");
          getPatientData();
      });
});

function makePatientTable(patientModel) {
    var patientData = document.getElementById('patientTable');

    console.log(patientModel);
    
    var result = "<tr><th><input id='checkAll' type=checkbox onClick='checkAll()' /></th><th>Patient name</th></tr>";
    
    for (var patient in patientModel) {
        result += "<tr><td><input type=checkbox class='patientCheckbox' value='" + patientModel[patient] + "'/></td><td>" + patientModel[patient] + "</td></tr>";
    }
    

    patientData.innerHTML = result;
}

function checkAll() {
    var myCheckboxes = document.querySelectorAll(".patientCheckbox");

    if (document.getElementById("checkAll").checked) {
        var i = 0;
        while(i < myCheckboxes.length) {
            myCheckboxes[i].checked = true;
            i++;
        }
    }
    else {
        var i = 0;
        while (i < myCheckboxes.length) {
            myCheckboxes[i].checked = false;
            i++;
        }
    }
}

function getPatientData() {
    var url = window.location.href;
    url = url.substring(url.lastIndexOf('/'));
    console.log(url);
    if (url === "/Index") url = 'getPatients';
    else url = 'Episode/getPatients';
    console.log(url);
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        cache: false,
        //data: { 'episodeType': episodeType, 'db': db, 'de': de },
        success: function (patientModel) {
            console.log(patientModel);
            makePatientTable(patientModel);
            return patientModel;
        },
        error: function () {
            alert('Error occured');
        }
    });
}
