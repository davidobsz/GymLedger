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
                        type: 'pie',
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

        // Prepare the datesReport object
        var reportData = datesReport || {
            StartDate: null,
            EndDate: null
        };

        $.ajax({
            url: url,
            method: 'GET',
            data: reportData, 
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
        var startDate = $('#startDate').val();
        var endDate = $('#endDate').val();

        var datesReport = {
            StartDate: startDate ? startDate : null, 
            EndDate: endDate ? endDate : null
        };

        loadChart(datesReport);
    });
});
