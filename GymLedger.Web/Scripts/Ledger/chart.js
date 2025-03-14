// ledger home pie chart
$(document).ready(function () {
    // Get the URL from the data attribute of the hidden div
    var chartDataUrl = $('#chartDataUrl').data('url');

    // Function to load the pie chart with optional date range
    function loadPieChart(datesReport = null) {
        $.ajax({
            url: chartDataUrl,        
            method: 'GET',            
            data: datesReport,        
            dataType: 'json',          
            success: function (data) {
                // Check if the response was successful
                if (data.success) {
                    // Get the chart context
                    var ctx = $('#exercisesPieChart')[0].getContext('2d');

                    if (window.exercisesChart) {
                        window.exercisesChart.destroy();
                    }

                    window.exercisesChart = new Chart(ctx, {
                        type: 'doughnut',
                        data: {
                            labels: data.Data.Exercise,  
                            datasets: [{
                                label: 'Sessions',
                                data: data.Data.Sessions,  
                                borderWidth: 1
                            }]
                        },
                        options: {
                            responsive: true,
                            plugins: {
                                legend: {
                                    position: 'top',
                                },
                                tooltip: {
                                    callbacks: {
                                        label: function (tooltipItem) {
                                            return tooltipItem.label + ": " + tooltipItem.raw;
                                        }
                                    }
                                }
                            }
                        }
                    });
                } else {
                    console.error('Error fetching chart data:', data.responseText);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error fetching chart data:', error);
            }
        });
    }

    loadPieChart();

    // Run Report button event
    $('#runReportExercises').click(function () {
        // Get the values of start and end date inputs
        var startDate = $('#startDate').val();
        var endDate = $('#endDate').val();

        var datesReport = {
            StartDate: startDate ? startDate : null, 
            EndDate: endDate ? endDate : null         
        };

        loadPieChart(datesReport);
    });
});


$(document).ready(function () {
    function loadChart(datesReport = null) {
        var url = $('#sessionDataUrl').data('url');
        $.ajax({
            url: url,
            method: 'GET',
            data: datesReport, 
            dataType: 'json',
            success: function (data) {
                if (data.success) {
                    var ctx = $('#sessionsLineChart')[0].getContext('2d');
 
                    if (window.sessionsChart) {
                        window.sessionsChart.destroy();
                    }

                    // Create new chart
                    window.sessionsChart = new Chart(ctx, {
                        type: 'line',
                        data: {
                            labels: data.Data.Exercises,
                            datasets: [{
                                label: 'Exercises Progress',
                                data: data.Data.Progress,
                                borderWidth: 1
                            }]
                        },
                        options: {
                            responsive: true,
                            plugins: {
                                legend: { position: 'top' },
                                tooltip: {
                                    callbacks: {
                                        label: function (tooltipItem) {
                                            return tooltipItem.label + ": " + tooltipItem.raw;
                                        }
                                    }
                                }
                            }
                        }
                    });
                } else {
                    console.error('Error fetching chart data:', data.responseText);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error fetching chart data:', error);
            }
        });
    }

    loadChart();

    $('#runReportSessions').click(function () { 
        var startDate = $('#startDate2').val();
        var endDate = $('#endDate2').val();

        var datesReport = {
            StartDate: startDate ? startDate : null, 
            EndDate: endDate ? endDate : null
        };

        loadChart(datesReport);
    });
});

$(document).ready(function () {
    function loadChart(datesReport = null) {
        var url = $('#setsDataUrl').data('url');

        $.ajax({
            url: url,
            method: 'GET',
            data: datesReport,
            dataType: 'json',
            success: function (data) {
                if (data.success) {
                    var ctx = $('#setsLineChart')[0].getContext('2d');

                    if (window.setsChart) {
                        window.setsChart.destroy();
                    }

                    // Create new chart
                    window.setsChart = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: data.Data.Exercise,
                            datasets: [{
                                label: 'Sets Count',
                                data: data.Data.Sets,
                                borderWidth: 1
                            }]
                        },
                        options: {
                            responsive: true,
                            plugins: {
                                legend: { position: 'top' },
                                tooltip: {
                                    callbacks: {
                                        label: function (tooltipItem) {
                                            return tooltipItem.label + ": " + tooltipItem.raw;
                                        }
                                    }
                                }
                            }
                        }
                    });
                } else {
                    console.error('Error fetching chart data:', data.responseText);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error fetching chart data:', error);
            }
        });
    }

    loadChart();

    $('#runReportSets').click(function () {
        var startDate = $('#startDate3').val();
        var endDate = $('#endDate3').val();

        var datesReport = {
            StartDate: startDate ? startDate : null,
            EndDate: endDate ? endDate : null
        };
        console.log(datesReport);

        loadChart(datesReport);
    });
});

$(document).ready(function () {
    function loadChart(datesReport = null) {
        var url = $('#setsBarchartDataUrlExercise').data('url');

        $.ajax({
            url: url,
            method: 'GET',
            data: datesReport,
            dataType: 'json',
            success: function (data) {
                if (data.success && data.Data && data.Data.GetExercisesSetsBarchartDetailViews) {
                    var ctx = $('#setsBarchartExercise')[0].getContext('2d');

                    if (window.setsChart) {
                        window.setsChart.destroy();
                    }

                    // Extract SetNumber for X-axis labels and SetProgress for Y-axis data
                    var labels = data.Data.GetExercisesSetsBarchartDetailViews.map(item => "Set " + item.SetNumber);
                    var setProgressData = data.Data.GetExercisesSetsBarchartDetailViews.map(item => item.SetProgress);

                    // Create new chart
                    window.setsChart = new Chart(ctx, {
                        type: 'bar',
                        data: {
                            labels: labels,
                            datasets: [{
                                label: 'Total Progress Per Set',
                                data: setProgressData,
                                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                                borderColor: 'rgba(75, 192, 192, 1)',
                                borderWidth: 1
                            }]
                        },
                        options: {
                            responsive: true,
                            plugins: {
                                legend: { position: 'top' },
                                tooltip: {
                                    callbacks: {
                                        label: function (tooltipItem) {
                                            return tooltipItem.label + ": " + tooltipItem.raw;
                                        }
                                    }
                                }
                            },
                            scales: {
                                y: { beginAtZero: true }
                            }
                        }
                    });
                } else {
                    console.error('Error: Invalid response structure', data);
                }
            },
            error: function (xhr, status, error) {
                console.error('Error fetching chart data:', error);
            }
        });
    }

    loadChart();

    $('#runReportSetsExercise').click(function () {
        var startDate = $('#startDate4').val();
        var endDate = $('#endDate4').val();

        var datesReport = {
            StartDate: startDate ? startDate : null,
            EndDate: endDate ? endDate : null
        };
        console.log(datesReport);

        loadChart(datesReport);
    });
});

