function overlay(typeName, count) {
    if (typeName != 'close') {
        var graphHead = document.getElementById('graphHead');
        var graphData = document.getElementById('graphData');
        console.log(graphData);
        graphHead.innerHTML = "<tr><th></th><th>" + typeName + "</th></tr>";
        graphData.innerHTML = "<tr><th>" + typeName + "</th><td>" + count + "</td></tr>";


        drawGraph();
    }
    el = document.getElementById("overlay");
    el.style.visibility = (el.style.visibility == "visible") ? "hidden" : "visible";
}


