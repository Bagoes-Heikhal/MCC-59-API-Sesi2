//< !-- Universities  -- >

$.ajax({
    url: "https://localhost:44345/api/Universities/CountEmployeeUniversity",
    success: function (result) {
        console.log(result)
        var data = result.result
        var options2 = {
            series: data.value,
            labels: data.key,
            chart: {
                width: "100%",
                type: 'donut',
                toolbar: {
                    show: true,
                    offsetX: 0,
                    offsetY: 0,
                    export: {
                        csv: {
                            filename: undefined,
                            columnDelimiter: ',',
                            headerCategory: 'category',
                            headerValue: 'value',
                            dateFormatter(timestamp) {
                                return new Date(timestamp).toDateString()
                            }
                        },
                        svg: {
                            filename: undefined,
                        },
                        png: {
                            filename: undefined,
                        }
                    },
                    autoSelected: 'zoom'
                },
            },
            responsive: [{
                breakpoint: 500,
                options: {
                    chart: {
                        width: "50%",
                    },
                    legend: {
                        position: 'bottom'
                    }
                }
            }]
        };
        var chart2 = new ApexCharts(document.querySelector("#chart2"), options2);
        chart2.render();
    },
    error: function (error) {
        console.log(error)
    }
})

$.ajax({
    url: "https://localhost:44345/api/Employees/",
    datasrc: "",
    success: function (result) {
        var male = 0;
        var female = 0;

        $.each(result, function (key, val) {
            if (val.gender == 0) {
                male++
            } female++
        });

        var options3 = {
            series: [male, female],
            labels: ["Male", "Female"],
            chart: {
                width: "90%",
                type: 'donut',
            },
            responsive: [{
                options: {

                    breakpoint: 500,
                    chart: {
                        width: "40%",
                    },
                    legend: {
                        position: 'bottom'
                    }
                }
            }]
        };
        var chart3 = new ApexCharts(document.querySelector("#chart"), options3);
        chart3.render();
    },
    error: function (error) {
        console.log(error)
    }
})

$.ajax({
    url: "https://localhost:44345/api/Universities/CountEmployeeUniversity",
    success: function (result) {
        console.log(result)
        var data = result.result
        var options3 = {
            series: [{
                name: "Employees",
                data: data.value
            }],
            chart: {
                type: 'bar',
                height: 350
            },
            plotOptions: {
                bar: {
                    borderRadius: 4,
                    horizontal: false,
                }
            },
            dataLabels: {
                enabled: false
            },
            xaxis: {
                categories: data.key,
            }
        };

        var chart3 = new ApexCharts(document.querySelector("#chart3"), options3);
        chart3.render();
    },
    error: function (error) {
        console.log(error)
    }
})