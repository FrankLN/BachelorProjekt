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

    //console.log(episodes);
    //console.log(dates);
    //console.log(patients);

    getNewModel(episodes, dates, patients, 'x');
}

function updateTable(newModel)
{
    var table = document.getElementById("tableContent");
    var header = document.getElementById("thTransmission");

    //console.log(header.innerText.lastIndexOf('('));
    var headerHtml = header.innerText.substring(0, header.innerText.lastIndexOf('('));
    //console.log(headerHtml);
    if (newModel.length > 0) {
        headerHtml += '(' + newModel[0].TotalTransmissions + ')';
    }
    else
    {
        headerHtml += '(0)';
    }

    //console.log("headerHtml: " + headerHtml);
    header.innerHTML = headerHtml;

    var html = '';

    for (var i = 0; i < newModel.length; i++) {
        html += '<tr class="trData">' +
                    '<td class="graphInputButton">' +
                        '<input value="' + newModel[i].EpisodeType + '" type="image" class="graphButton" img src="http://localhost:5187/Content/img/graph_icon.png" title="Show graph"></td>' +
                    '<td class="episodeType">' + newModel[i].EpisodeType + '</td>' +
                    '<td class="cellWithNumber">' + newModel[i].Transmissions + '</td>' +
                    '<td class="cellWithNumber">' + newModel[i].ProcentTransmission + ' %</td>' +
                '</tr>';
                
    }

    table.innerHTML = html;

    setOnClickListener();
}

function getNewModel(episodes, dates, patients, searchText) {
    var url = window.location.href;
    url = url.substring(url.lastIndexOf('/'));
    //console.log(url);
    if (url === "/Index") url = 'getNewModel';
    else url = 'Episode/getNewModel';
    url = url + '?episodes=' + episodes + '&datesSelected=' + dates + '&patientsChecked=' + patients + '&searchText=' + searchText;
    //console.log(url);
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        cache: false,
        success: function (model) {
            //console.log(model);
            updateTable(model);
        },
        error: function () {
            alert('Error occured');
        }
    });
}

function getEpisodeList() {
    var episodes = document.querySelectorAll(".episodeCheckbox:checked");
    //console.log(episodes);
    var i = 0;
    var episodeList = [];
    //console.log(episodes.length)
    if (episodes.length > 0) {
        while (i < episodes.length) {
            //console.log(episodes[i].value);
            episodeList.push(episodes[i].value);
            i++;
        }
    }

    //console.log(episodeList);
    return episodeList;
}


var redirect = function (url, method) {
    var form = document.createElement('form');
    form.method = method;
    form.action = url;
    form.submit();
};