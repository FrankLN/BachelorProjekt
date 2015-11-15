$(function () {
      $( "#dialog" ).dialog({
          autoOpen: false,

      });
 
      $( "#opener" ).click(function() {
          $("#dialog").dialog("open");
      });
});

$(document).ready(function () {
    getPatientData('x');
    getEpisodeData();
});


function makePatientTable(patientModel) {
    var patientData = document.getElementById('patientTable');

    console.log(patientModel);
    
    var result = "<tr><th><input id='checkAll' type=checkbox onClick='checkAll()' checked /></th><th>Patient name</th></tr>";
    
    for (var patient in patientModel) {
        result += "<tr><td><input type=checkbox class='patientCheckbox' value='" + patientModel[patient] + "' checked /></td><td>" + patientModel[patient] + "</td></tr>";
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

function makeEpisodeTable(episodeModel)
{
    var table = document.getElementById("episodeCheck");
    var html = "";

    for(var i = 0; i < episodeModel.length; i++)
    {
        html += '<li>' +
                    '<label>' +
                        '<input type="checkbox" class="episodeCheckbox" onClick="alertFunction()" value="' + episodeModel[i] + '" checked />' + episodeModel[i] +
                    '</label>' +
                '</li>'
    }

    table.innerHTML = html;
}

function getEpisodeData()
{
    var url = window.location.href;
    url = url.substring(url.lastIndexOf('/'));
    //console.log(url);
    if (url === "/Index") url = 'getEpisodes';
    else url = 'Episode/getEpisodes';
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        cache: false,
        //data: { 'episodeType': episodeType, 'db': db, 'de': de },
        success: function (episodeModel) {
            //console.log(patientModel);
            makeEpisodeTable(episodeModel);
        },
        error: function () {
            alert('Error occured');
        }
    });
}

function getPatientData(filter) {
    var url = window.location.href;
    url = url.substring(url.lastIndexOf('/'));
    //console.log(url);
    if (url === "/Index") url = 'getPatients?filter=' + filter;
    else url = 'Episode/getPatients?filter=' + filter;
    console.log(url);
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        cache: false,
        //data: { 'episodeType': episodeType, 'db': db, 'de': de },
        success: function (patientModel) {
            //console.log(patientModel);
            makePatientTable(patientModel);
        },
        error: function () {
            alert('Error occured');
        }
    });
}

function patientSearch() {
    var searchText = document.getElementById("patientTags");
    getPatientData(searchText.value);
}