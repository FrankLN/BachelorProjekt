$(function () {
      $( "#dialog" ).dialog({
          autoOpen: false,
          minWidth: 700,
          closeText: "",
          modal: true,
          dialogClass: 'no-close',
          closeOnEscape: false
      });
 
      var backup = [];

      $("#opener").click(function () {
          $("#dialog").dialog("open");
          backup = getPatientList();
          //console.log("open: " + backup);
      });

      $("#cancel").click(function () {
          //console.log("cancel: " + backup);
          var patientCheckboxes = document.querySelectorAll(".patientCheckbox");

          for(var i = 0; i < patientCheckboxes.length; i++)
          {

              backup = "" + backup;

              if (backup.indexOf(patientCheckboxes[i].value) > -1)
              {
                  patientCheckboxes[i].checked = true;
              }
              else
              {
                  patientCheckboxes[i].checked = false;
              }
          }
          $("#dialog").dialog("close");
      });

      $("#confirm").click(function () {
          //console.log("confirm: " + backup);
          $("#dialog").dialog("close");
      });

      $("#dialog").onclose
});

$(document).ready(function () {
    getPatientData('x');
    getEpisodeData();
});


function makePatientTable(patientModel) {
    var patientData = document.getElementById('patientTable');

    //console.log(patientModel);
    
    var result = "<thead><tr><th><input id='checkAll' type=checkbox onClick='checkAll()' checked /></th><th>Patient name</th><th>Patient securitynumber</th><th>Pacemaker serialnumber</th><th>Pacemaker name</th><th>Pacemaker type</th></tr></thead>";
    
    for (var patient in patientModel) {
        result += "<tr><td><input type=checkbox class='patientCheckbox' value='" + patientModel[patient].PatientName + "' checked /></td><td>" +
            patientModel[patient].PatientName + "</td><td>" +
            patientModel[patient].SecurityNumber + "</td><td>" +
            patientModel[patient].PacemakerSerialnumber + "</td><td>" +
            patientModel[patient].PacemakerName + "</td><td>" +
            patientModel[patient].PacemakerType + "</td></tr>";
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
    //console.log(url);
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
    updatePatientTable(searchText.value);
}

function updatePatientTable(searchText)
{
    console.log(searchText);

    var patientData = document.getElementById('patientTable');
    var rows = patientData.getElementsByTagName("tbody")[0].getElementsByTagName("tr");

    for(var i = 0; i < rows.length; i++)
    {
        console.log(rows[i]);
        console.log(rows[i].getElementsByTagName("td"));
        console.log(rows[i].getElementsByTagName("td")[1]);
        console.log(rows[i].getElementsByTagName("td")[1].innerText);

        var show = searchText.length < 1;
        for (var j = 1; j < rows[i].getElementsByTagName("td").length; j++)
        {
            show = show || rows[i].getElementsByTagName("td")[j].innerText.toLowerCase().indexOf(searchText.toLowerCase()) > -1;
        }

        if (show)
        {
            rows[i].style.display = "table-row";
        }
        else
        {
            rows[i].style.display = "none";
        }
    }
}