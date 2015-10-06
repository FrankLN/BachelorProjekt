function drawGraph(newestYear) {
    console.log(newestYear)
    $('#container').highcharts({
        data: {
            table: 'datatable'
        },
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
                    this.point.y + ' ' + this.point.name.toLowerCase();
            }
        }
    });

    
}