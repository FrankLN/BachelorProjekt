$(function(){
    $('.date-picker1').datepicker({ dateFormat: 'dd/mm/yy' });
})

$(function(){
    $('.date-picker2').datepicker({ dateFormat: 'dd/mm/yy' });
})

$(function() {
    var availableTags = [];

    $("#tags").autocomplete({
        source: availableTags
    });

    $("#patientTags").autocomplete({
        source: availableTags
    });


    getTags();
    getPatientTags();
});

function getPatientTags() {
    var url = window.location.href;
    url = url.substring(url.lastIndexOf('/'));
    console.log(url);
    if (url === "/Index") url = 'getPatientTags';
    else url = 'Episode/getPatientTags';
    console.log(url);
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        cache: false,
        success: function (model) {
            console.log(model);
            $("#patientTags").autocomplete('option', 'source', model)
        },
        error: function () {
            alert('Error occured');
        }
    });
}

function getTags() {
    var url = window.location.href;
    url = url.substring(url.lastIndexOf('/'));
    console.log(url);
    if (url === "/Index") url = 'getTags';
    else url = 'Episode/getTags';
    console.log(url);
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        cache: false,
        success: function (model) {
            console.log(model);
            $("#tags").autocomplete('option', 'source', model)
        },
        error: function () {
            alert('Error occured');
        }
    });
}

function search() {
    var searchText = document.getElementById("tags");
    alertFunction(searchText.value)
}

function alertFunction(seachText) {
    if (document.getElementById('tags').value.length >= 2) {
        seachText = document.getElementById('tags').value;
    }
    else {
        seachText = typeof seachText !== 'undefined' ? seachText : "x";
    }

    console.log(seachText);
    console.log(seachText.length);

    var dpb = document.getElementById("dpb");
    var dpe = document.getElementById("dpe");

    var dateBegin = dpb.value.split("/");
    dateBegin = dateBegin[2] + dateBegin[1] + dateBegin[0] + "000000";

    var dateEnd = dpe.value.split("/");
    dateEnd = dateEnd[2] + dateEnd[1] + dateEnd[0] + "235959";

    var dates = dateBegin + "|" + dateEnd;

    var episodeList = getEpisodeList();
    var episodes = ""
    for(var i = 0; i < episodeList.length; i++)
    {
        episodes += episodeList[i] + "|";
        //console.log(episodes);
    }

    var patientList = getPatientList();
    var patients = "";
    for (var i = 0; i < patientList.length; i++) {
        patients += patientList[i] + "|";
        //console.log(patients);
    }

    console.log(episodes);
    console.log(dates);
    console.log(patients);

    getNewModel(episodes, dates, patients, seachText);
}

function updateTable(newModel)
{
    var table = document.getElementById("tableContent");

    var html = '';

    for (var i = 0; i < newModel.length; i++) {
        html += '<tr class="trData">' +
                    '<td class="graphInputButton">' +
                        '<input type="image" class="graphButton" img src="http://localhost:5187/Content/img/graph_icon.png" title="Show graph" onclick="' +
                        'overlay(\'' + newModel[i].EpisodeType + '\', \'' + newModel[i].Transmissions + '\')"></td>' +
                    '<td class="episodeType">' + newModel[i].EpisodeType + '</td>' +
                    '<td class="cellWithNumber">' + newModel[i].Transmissions + '</td>' +
                    '<td class="cellWithNumber">' + newModel[i].ProcentTransmission + ' %</td>' +
                '</tr>';
                
    }

    table.innerHTML = html;
}

function getNewModel(episodes, dates, patients, searchText) {
    var url = window.location.href;
    url = url.substring(url.lastIndexOf('/'));
    console.log(url);
    if (url === "/Index") url = 'getNewModel';
    else url = 'Episode/getNewModel';
    url = url + '?episodes=' + episodes + '&datesSelected=' + dates + '&patientsChecked=' + patients + '&searchText=' + searchText;
    console.log(url);
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        cache: false,
        success: function (model) {
            console.log(model);
            updateTable(model);
        },
        error: function () {
            alert('Error occured');
        }
    });
}

function getEpisodeList() {
    var episodes = document.querySelectorAll(".episodeCheckbox:checked");
    console.log(episodes);
    var i = 0;
    var episodeList = [];
    console.log(episodes.length)
    if (episodes.length > 0) {
        while (i < episodes.length) {
            console.log(episodes[i].value);
            episodeList.push(episodes[i].value);
            i++;
        }
    }

    console.log(episodeList);
    return episodeList;
}


function getPatientList() {
    var patients = document.querySelectorAll(".patientCheckbox:checked");
    var i = 0;
    var patientList = [];
    //console.log(patients.length)
    if (patients.length > 0) {
        while (i < patients.length) {
            //console.log(patients[i].value);
            patientList.push(patients[i].value);
            i++;
        }
    }

    //console.log(patientList);
    return patientList;
}


var redirect = function (url, method) {
    var form = document.createElement('form');
    form.method = method;
    form.action = url;
    form.submit();
};