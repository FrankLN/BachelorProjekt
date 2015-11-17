function drawGraph(newestYear) {
    //console.log(newestYear)
    $('#graphContainer').highcharts({
        data: {
            table: 'datatable'
        },
        exporting: { enabled: false },
        chart: {
            type: 'column'
        },
        title: {
            text: 'Graph over episodetype'
        },
        yAxis: {
            allowDecimals: false,
            title: {
                text: 'Transmissions ' + newestYear
            }
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.series.name + '</b><br/>' +
                    this.point.name.toLowerCase() + ': ' + this.point.y;
            }
        }
    });

    
}