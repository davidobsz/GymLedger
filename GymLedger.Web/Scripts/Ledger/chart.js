// ledger home pie chart
$(document).ready(function () {
    // Get the URL from the data attribute of the hidden div
    var chartDataUrl = $('#chartDataUrl').data('url');

    // Use jQuery to perform an AJAX GET request to the controller action
    $.ajax({
        url: chartDataUrl,         // The URL from the data-url attribute
        method: 'GET',             // The HTTP method (GET)
        dataType: 'json',          // Expecting JSON response
        success: function (data) {
            // Check if the response was successful
            if (data.success) {
                // Get the chart context
                var ctx = $('#exercisesPieChart')[0].getContext('2d');

                // Create the pie chart with the fetched data
                new Chart(ctx, {
                    type: 'pie',
                    data: {
                        labels: data.Data.Exercise,  // Using the Exercise data from the response
                        datasets: [{
                            label: 'Sessions',
                            data: data.Data.Sessions,   // Using the Sessions data from the response
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
});
