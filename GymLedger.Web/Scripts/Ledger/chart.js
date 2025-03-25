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

$(document).ready(function () {
    function loadOneRepMaxChart(datesReport = null) {
        var url = $('#oneRepMaxDataUrlExercise').data('url');

        $.ajax({
            url: url,
            method: 'GET',
            data: datesReport,
            dataType: 'json',
            success: function (data) {
                if (data.success && data.Data && data.Data.DataPoints) {
                    var ctx = $('#oneRepMaxChart')[0].getContext('2d');
                    console.log(data.Data.DataPoints);

                    var labels = data.Data.DataPoints.map(item => formatDate(item.Date));
                    var oneRepMaxData = data.Data.DataPoints.map(item => item.EstimatedOneRepMax);

/*                    if (window.oneRepMaxChart) {
                        window.oneRepMaxChart.destroy();
                    }*/

                    console.log("Hello");
                    window.oneRepMaxChart = new Chart(ctx, {
                        type: 'line',
                        data: {
                            labels: labels,
                            datasets: [{
                                label: 'Estimated One-Rep Max',
                                data: oneRepMaxData,
                                borderColor: 'rgba(255, 99, 132, 1)',
                                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                                borderWidth: 2,
                                tension: 0.4,
                                pointRadius: 4
                            }]
                        },
                        options: {
                            responsive: true,
                            plugins: {
                                legend: { position: 'top' },
                                tooltip: {
                                    callbacks: {
                                        label: function (tooltipItem) {
                                            return tooltipItem.label + ": " + tooltipItem.raw.toFixed(2) + " kg";
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
                console.error('Error fetching One-Rep Max chart data:', error);
            }
        });
    }

    function formatDate(dotNetDate) {
        var timestamp = parseInt(dotNetDate.replace(/\D/g, '')); // Extract numbers from /Date(XXXXXXXXXXXX)/
        var date = new Date(timestamp); // Convert to JavaScript Date object
        return date.toISOString().split('T')[0]; // Format as YYYY-MM-DD
    }

    loadOneRepMaxChart();

    $('#runReportOneRepMax').click(function () {
        var startDate = $('#startDate5').val();
        var endDate = $('#endDate5').val();

        var datesReport = {
            StartDate: startDate ? startDate : null,
            EndDate: endDate ? endDate : null
        };

        loadOneRepMaxChart(datesReport);
    });
});

$(document).ready(function () {
    function loadVolumePerSessionChart(datesReport = null) {
        var url = $('#volumePerSessionDataUrlExercise').data('url');

        $.ajax({
            url: url,
            method: 'GET',
            data: datesReport,
            dataType: 'json',
            success: function (data) {
                if (data.success && data.Data && data.Data.DataPoints) {
                    var ctx = $('#volumePerSessionChart')[0].getContext('2d');
                    console.log(data.Data.DataPoints);

                    var labels = data.Data.DataPoints.map(item => formatDate(item.Date));
                    var volumeData = data.Data.DataPoints.map(item => item.TotalVolume);

                    window.volumePerSessionChart = new Chart(ctx, {
                        type: 'line',
                        data: {
                            labels: labels,
                            datasets: [{
                                label: 'Total Volume (kg)',
                                data: volumeData,
                                borderColor: 'rgba(75, 192, 192, 1)',
                                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                                borderWidth: 2,
                                tension: 0.4,
                                pointRadius: 4
                            }]
                        },
                        options: {
                            responsive: true,
                            plugins: {
                                legend: { position: 'top' },
                                tooltip: {
                                    callbacks: {
                                        label: function (tooltipItem) {
                                            return tooltipItem.label + ": " + tooltipItem.raw.toFixed(2) + " kg";
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
                console.error('Error fetching Volume per Session chart data:', error);
            }
        });
    }

    function formatDate(dotNetDate) {
        var timestamp = parseInt(dotNetDate.replace(/\D/g, '')); // Extract numbers from /Date(XXXXXXXXXXXX)/
        var date = new Date(timestamp); // Convert to JavaScript Date object
        return date.toISOString().split('T')[0]; // Format as YYYY-MM-DD
    }

    loadVolumePerSessionChart();

    $('#runReportVolume').click(function () {
        var startDate = $('#startDateVolume').val();
        var endDate = $('#endDateVolume').val();

        var datesReport = {
            StartDate: startDate ? startDate : null,
            EndDate: endDate ? endDate : null
        };

        loadVolumePerSessionChart(datesReport);
    });
});
