function overlay(typeName, count) {
    if (typeName != 'close') {
        var db = document.getElementById('dpb').value;
        var de = document.getElementById('dpe').value;

        db = db.substring(6, 10) + db.substring(3, 5) + db.substring(0, 2) + "000000";
        de = de.substring(6, 10) + de.substring(3, 5) + de.substring(0, 2) + "235959";

        console.log(db + " " + de);
        
        var graphModel = getGraphData(typeName, db, de);

        var graphHead = document.getElementById('graphHead');
        graphHead.innerHTML = "<tr><th></th><th>" + typeName + "</th></tr>";
    }
    el = document.getElementById("overlay");
    el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
}

function makeTable(graphModel)
{
    var graphData = document.getElementById('graphData');


    console.log(graphModel);

    var result = "";
    
    //if (graphModel['Jan'] > 0)
        result = result + "<tr><th>Jan</th><td>" + graphModel.Jan + "</td></tr>";
    //if (graphModel['Feb'] > 0)
        result = result + "<tr><th>Feb</th><td>" + graphModel.Feb + "</td></tr>";
    //if (graphModel['Mar'] > 0)
        result = result + "<tr><th>Mar</th><td>" + graphModel.Mar + "</td></tr>";
    //if (graphModel['Apr'] > 0)
        result = result + "<tr><th>Apr</th><td>" + graphModel.Apr + "</td></tr>";
    //if (graphModel['May'] > 0)
        result = result + "<tr><th>May</th><td>" + graphModel.May + "</td></tr>";
    //if (graphModel['Jun'] > 0)
        result = result + "<tr><th>Jun</th><td>" + graphModel.Jun + "</td></tr>";
    //if (graphModel['Jul'] > 0)
        result = result + "<tr><th>Jul</th><td>" + graphModel.Jul + "</td></tr>";
    //if (graphModel['Aug'] > 0)
        result = result + "<tr><th>Aug</th><td>" + graphModel.Aug + "</td></tr>";
    //if (graphModel['Sep'] > 0)
        result = result + "<tr><th>Sep</th><td>" + graphModel.Sep + "</td></tr>";
    //if (graphModel['Oct'] > 0)
        result = result + "<tr><th>Oct</th><td>" + graphModel.Oct + "</td></tr>";
    //if (graphModel['Nov'] > 0)
        result = result + "<tr><th>Nov</th><td>" + graphModel.Nov + "</td></tr>";
    //if (graphModel['Dec'] > 0)
        result = result + "<tr><th>Dec</th><td>" + graphModel.Dec + "</td></tr>";


    graphData.innerHTML = result;
    console.log(graphModel.newestYear);
    drawGraph(graphModel.newestYear);
}

function getGraphData(episodeType, db, de) {
    var url = window.location.href;
    url = url.substring(url.lastIndexOf('/'));
    console.log(url);
    if(url === "/Index") url = 'getGraphData?episodeType=' + episodeType + "&db=" + db + "&de=" + de;
    else url = 'Episode/getGraphData?episodeType=' + episodeType + "&db=" + db + "&de=" + de;
    console.log(url);
    $.ajax({
        url: url,
        type: 'GET',
        dataType: 'json',
        cache: false,
        //data: { 'episodeType': episodeType, 'db': db, 'de': de },
        success: function (graphModel) {
            console.log(graphModel);
            makeTable(graphModel);
            return graphModel;
        },
        error: function () {
            alert('Error occured');
        }
    });
}


