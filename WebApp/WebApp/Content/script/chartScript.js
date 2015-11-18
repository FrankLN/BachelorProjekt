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
        colors: ['#286152', '#434348', '#90ed7d', '#f7a35c', '#8085e9',
                '#f15c80', '#e4d354', '#2b908f', '#f45b5b', '#91e8e1'],
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