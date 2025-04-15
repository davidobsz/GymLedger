$(document).ready(function () {
    // Check if BootstrapTable is available
    if (typeof $ !== 'undefined' && $.fn.bootstrapTable) {
        console.log("Initializing Bootstrap Tables...");
        $(".bootstrap-table").bootstrapTable();
    } else {
        console.error("BootstrapTable library is not loaded.");
    }
});


$(document).ready(function () {
    $(document).on('submit', '#addSessionForm', function (e) {
        e.preventDefault();

        const $form = $(this);
        const $submitButton = $form.find('button[type="submit"]');

        $submitButton.prop('disabled', true); // Disable button to prevent multiple clicks

        const formData = $form.serialize();
        const addSessionUrl = $('#AddSessionUrl').val(); // Get the correct URL from the hidden input

        $.ajax({
            url: addSessionUrl, // Use the hidden input value as the URL
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    // Close the modal before showing Growl
                    var modalEl = document.getElementById('sessionModal');
                    var modal = bootstrap.Modal.getInstance(modalEl);
                    if (modal) {
                        modal.hide();
                    }
                    setTimeout(() => {
                        $.growl.notice({ title: "Success", message: response.responseText });
                    }, 200); // Delay slightly to allow modal animation to complete

                    if (response.responseReload) {
                        setTimeout(() => location.reload(), 2000);
                    }
                } else {
                    $.growl.error({ title: "Error", message: response.responseText });
                    $submitButton.prop('disabled', false); // Re-enable button if error
                }
            },
            error: function () {
                $.growl.error({ title: "Error", message: "An unexpected error occurred." });
                $submitButton.prop('disabled', false);
            }
        });
    });

    $(document).on('submit', '#addExerciseForm', function (e) {
        e.preventDefault();

        const $form = $(this);
        const $submitButton = $form.find('button[type="submit"]');

        $submitButton.prop('disabled', true); // Disable button to prevent multiple clicks

        const formData = $form.serialize();
        const addExerciseUrl = $('#AddExerciseUrl').val(); // Get the correct URL from the hidden input

        $.ajax({
            url: addExerciseUrl, // Use the hidden input value as the URL
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    // Close the modal before showing Growl
                    var modalEl = document.getElementById('modalModal');
                    var modal = bootstrap.Modal.getInstance(modalEl);
                    if (modal) {
                        modal.hide();
                    }
                    setTimeout(() => {
                        $.growl.notice({ title: "Success", message: response.responseText });
                    }, 200); // Delay slightly to allow modal animation to complete

                    if (response.responseReload) {
                        setTimeout(() => location.reload(), 2000);
                    }
                } else {
                    $.growl.error({ title: "Error", message: response.responseText });
                    $submitButton.prop('disabled', false); // Re-enable button if error
                }
            },
            error: function () {
                $.growl.error({ title: "Error", message: "An unexpected error occurred." });
                $submitButton.prop('disabled', false);
            }
        });
    });
});

$(document).ready(function () {
    // Delegate the form submission event to the document to handle dynamically loaded content
    $(document).on('submit', '#deleteExerciseForm', function (e) {
        e.preventDefault(); // Prevent the default form submission

        const $form = $(this);
        const $submitButton = $form.find('button[type="submit"]');

        $submitButton.prop('disabled', true); // Disable button to prevent multiple clicks

        const formData = $form.serialize(); // Serialize the form data
        $.ajax({
            url: $form.attr('action'),
            type: 'POST',
            data: formData,
            success: function (response) {


                if (response.success) {
                    $.growl.notice({ title: "Success", message: response.responseText });
                    if (response.responseReload) {
                        setTimeout(() => location.reload(), 2000); // Reload after success
                    }
                } else {
                    $.growl.error({ title: "Error", message: response.responseText });
                    $submitButton.prop('disabled', false); // Re-enable button if error occurs
                }
            },
            error: function (xhr, status, error) {
                $.growl.error({ title: "Error", message: "An unexpected error occurred." });
                $submitButton.prop('disabled', false); // Re-enable button if error occurs
            }
        });
    });
});

$(document).ready(function () {
    // Delegate the form submission event to the document to handle dynamically loaded content
    $(document).on('submit', '#deleteSessionForm', function (e) {
        e.preventDefault(); // Prevent the default form submission

        const $form = $(this);
        const $submitButton = $form.find('button[type="submit"]');

        $submitButton.prop('disabled', true); // Disable button to prevent multiple clicks

        const formData = $form.serialize(); // Serialize the form data
        $.ajax({
            url: $form.attr('action'),
            type: 'POST',
            data: formData,
            success: function (response) {


                if (response.success) {
                    $.growl.notice({ title: "Success", message: response.responseText });
                    if (response.responseReload) {
                        setTimeout(() => location.reload(), 2000); // Reload after success
                    }
                } else {
                    $.growl.error({ title: "Error", message: response.responseText });
                    $submitButton.prop('disabled', false); // Re-enable button if error occurs
                }
            },
            error: function (xhr, status, error) {
                $.growl.error({ title: "Error", message: "An unexpected error occurred." });
                $submitButton.prop('disabled', false); // Re-enable button if error occurs
            }
        });
    });
});

$(document).ready(function () {
    // Delegate the form submission event to the document to handle dynamically loaded content
    $(document).on('submit', '#editSessionForm', function (e) {
        e.preventDefault();

        const $form = $(this);
        const $submitButton = $form.find('button[type="submit"]');

        $submitButton.prop('disabled', true); // Disable button to prevent multiple clicks

        const formData = $form.serialize();
        $.ajax({
            url: $form.attr('action'),
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $.growl.notice({ title: "Success", message: response.responseText });
                    if (response.responseReload) {
                        setTimeout(() => location.reload(), 2000);
                    }
                } else {
                    $.growl.error({ title: "Error", message: response.responseText });
                    $submitButton.prop('disabled', false); // Re-enable button if error
                }
            },
            error: function () {
                $.growl.error({ title: "Error", message: "An unexpected error occurred." });
                $submitButton.prop('disabled', false);
            }
        });
    });
});


$(document).ready(function () {
    // Delegate the form submission event to the document to handle dynamically loaded content
    $(document).on('submit', '#editExerciseForm', function (e) {
        e.preventDefault();

        const $form = $(this);
        const $submitButton = $form.find('button[type="submit"]');

        $submitButton.prop('disabled', true); // Disable button to prevent multiple clicks

        const formData = $form.serialize();
        $.ajax({
            url: $form.attr('action'),
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $.growl.notice({ title: "Success", message: response.responseText });
                    if (response.responseReload) {
                        setTimeout(() => location.reload(), 2000);
                    }
                } else {
                    $.growl.error({ title: "Error", message: response.responseText });
                    $submitButton.prop('disabled', false); // Re-enable button if error
                }
            },
            error: function () {
                $.growl.error({ title: "Error", message: "An unexpected error occurred." });
                $submitButton.prop('disabled', false);
            }
        });
    });
});

$(document).ready(function () {
    $('#exerciseTable').on('click', 'tbody tr', function () {
        var uniqueId = $(this).attr("data-uniqueid");
        var url = $('#exerciseTableContainer').data('url');

        if (uniqueId) {
            $.ajax({
                url: url,
                type: 'GET',
                data: { uniqueId: uniqueId },
                success: function (response) {

                    // Check if the modal already exists and remove it if so
                    var existingModal = $('#exerciseModal');
                    if (existingModal.length) {
                        existingModal.remove();  // Remove the existing modal content
                    }

                    // Append the new modal content to the body
                    $("body").append(response);

                    // Ensure modal is initialized and shown
                    var modal = new bootstrap.Modal(document.getElementById('exerciseModal'));
                    modal.show();
                },
                error: function (xhr, status, error) {
                    alert("Error loading exercise details.");
                }
            });
        } else {
            console.log("UniqueId not found!");
        }
    });
});

$(document).ready(function () {
    $('#OneRepMaxesTable').on('click', 'tbody tr', function () {
        var uniqueId = $(this).attr("data-uniqueid");
        var url = $('#oneRepMaxTableContainer').data('url');

        if (uniqueId) {
            $.ajax({
                url: url,
                type: 'GET',
                data: { uniqueId: uniqueId },
                success: function (response) {

                    // Check if the modal already exists and remove it if so
                    var existingModal = $('#oneRepMaxModal');
                    if (existingModal.length) {
                        existingModal.remove();  // Remove the existing modal content
                    }

                    // Append the new modal content to the body
                    $("body").append(response);

                    // Ensure modal is initialized and shown
                    var modal = new bootstrap.Modal(document.getElementById('oneRepMaxModal'));
                    modal.show();
                },
                error: function (xhr, status, error) {
                    alert("Error loading one rep max details.");
                }
            });
        } else {
            console.log("UniqueId not found!");
        }
    });
});

$(document).ready(function () {
    // Delegate the form submission event to the document to handle dynamically loaded content
    $(document).on('submit', '#editOneRepMaxForm', function (e) {
        e.preventDefault();

        const $form = $(this);
        const $submitButton = $form.find('button[type="submit"]');

        $submitButton.prop('disabled', true); // Disable button to prevent multiple clicks

        const formData = $form.serialize();
        $.ajax({
            url: $form.attr('action'),
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $.growl.notice({ title: "Success", message: response.responseText });
                    if (response.responseReload) {
                        setTimeout(() => location.reload(), 2000);
                    }
                } else {
                    $.growl.error({ title: "Error", message: response.responseText });
                    $submitButton.prop('disabled', false); // Re-enable button if error
                }
            },
            error: function () {
                $.growl.error({ title: "Error", message: "An unexpected error occurred." });
                $submitButton.prop('disabled', false);
            }
        });
    });
});

$(document).ready(function () {
    // Delegate the form submission event to the document to handle dynamically loaded content
    $(document).on('submit', '#deleteOneRepMaxForm', function (e) {
        e.preventDefault();

        const $form = $(this);
        const $submitButton = $form.find('button[type="submit"]');

        $submitButton.prop('disabled', true); // Disable button to prevent multiple clicks

        const formData = $form.serialize();
        $.ajax({
            url: $form.attr('action'),
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $.growl.notice({ title: "Success", message: response.responseText });
                    if (response.responseReload) {
                        setTimeout(() => location.reload(), 2000);
                    }
                } else {
                    $.growl.error({ title: "Error", message: response.responseText });
                    $submitButton.prop('disabled', false); // Re-enable button if error
                }
            },
            error: function () {
                $.growl.error({ title: "Error", message: "An unexpected error occurred." });
                $submitButton.prop('disabled', false);
            }
        });
    });
});

// gets the sessionDetails
$(document).ready(function () {
    $('#sessionTable').on('click', 'tbody tr', function () {
        var uniqueId = $(this).attr("data-uniqueid");
        var url = $('#sessionTableContainer').data('url');
        console.log(url);
        if (uniqueId) {
            $.ajax({
                url: url,
                type: 'GET',
                data: { uniqueId: uniqueId },
                success: function (response) {

                    // Check if the modal already exists and remove it if so
                    var existingModal = $('#sessionDetailModal');
                    if (existingModal.length) {
                        existingModal.remove();  // Remove the existing modal content
                    }

                    // Append the new modal content to the body
                    $("body").append(response);

                    // Ensure modal is initialized and shown
                    var modal = new bootstrap.Modal(document.getElementById('sessionDetailModal'));
                    modal.show();
                },
                error: function (xhr, status, error) {
                    alert("Error loading modal details.");
                }
            });
        } else {
            console.log("UniqueId not found!");
        }
    });
});

$(document).ready(function () {
    // Bind click event to the addExercise button
    $('#addExercise').on('click', function (e) {
        e.preventDefault();  // Prevent default link behavior
        
        var url = $('#addExerciseContainer').data('url');

        $.ajax({
            url: url,
            type: 'GET',
            success: function (response) {
                // Remove existing modal if any
                $('#modalModal').remove();

                // Append new modal content to body
                $("body").append(response);

                // Initialize and show modal
                var modal = new bootstrap.Modal(document.getElementById('modalModal'));
                modal.show();
            },
            error: function (xhr, status, error) {
                $.growl.error({ message: "Error loading modal details." }); // Use $.growl instead of alert
            }
        });
    });
});

$(document).on('click', '#addOneRepMax', function (e) {
    e.preventDefault(); // Prevent any unwanted navigation

    const modalUrl = $('#addOneRepMaxContainer').data('url'); // URL for the partial view
    $.ajax({
        url: modalUrl,
        type: 'GET',
        success: function (response) {
            $('#addOneRepMaxContainer').html(response); // Load the modal content
            const sessionModal = new bootstrap.Modal(document.getElementById('addOneRepMaxModal'));
            sessionModal.show(); // Open the modal
        },
        error: function () {
            $.growl.error({ title: "Error", message: "Failed to load Add One Rep Max form." });
        }
    });
});

$(document).ready(function () {
    // Delegate the form submission event to the document to handle dynamically loaded content
    $(document).on('submit', '#addOneRepMaxForm', function (e) {
        e.preventDefault(); // Prevent the default form submission

        const $form = $(this);
        const $submitButton = $form.find('button[type="submit"]');

        $submitButton.prop('disabled', true); // Disable button to prevent multiple clicks

        const formData = $form.serialize(); // Serialize the form data
        $.ajax({
            url: $form.attr('action'),
            type: 'POST',
            data: formData,
            success: function (response) {


                if (response.success) {
                    $.growl.notice({ title: "Success", message: response.responseText });
                    if (response.responseReload) {
                        setTimeout(() => location.reload(), 2000); // Reload after success
                    }
                } else {
                    $.growl.error({ title: "Error", message: response.responseText });
                    $submitButton.prop('disabled', false); // Re-enable button if error occurs
                }
            },
            error: function (xhr, status, error) {
                $.growl.error({ title: "Error", message: "An unexpected error occurred." });
                $submitButton.prop('disabled', false); // Re-enable button if error occurs
            }
        });
    });
});


$(document).on('submit', '#calculateOneRepMaxForm', function (e) {
    e.preventDefault(); // Prevent the default form submission

    const $form = $(this);
    const $submitButton = $form.find('button[type="submit"]');

    $submitButton.prop('disabled', true); // Disable button to prevent multiple clicks

    const formData = $form.serialize(); // Serialize the form data
    $.ajax({
        url: $form.attr('action'),
        type: 'POST',
        data: formData,
        success: function (response) {
            if (response.success) {
                $.growl.notice({ title: "Success", message: response.responseText });
                if (response.responseReload) {
                    setTimeout(() => location.reload(), 2000); // Reload after success
                }
                $('#oneRepMaxResult').html(`<div class="alert alert-primary">Estimated 1RM: <strong>${response.Data.OneRepMax} kg</strong></div>`);

            } else {
                $.growl.error({ title: "Error", message: response.responseText });
                $submitButton.prop('disabled', false); // Re-enable button if error occurs
            }
        },
        error: function (xhr, status, error) {
            $.growl.error({ title: "Error", message: "An unexpected error occurred." });
            $submitButton.prop('disabled', false); // Re-enable button if error occurs
        }
    });

    $submitButton.prop('disabled', false);
});

$(document).on('click', '#addSession', function (e) {
    e.preventDefault(); // Prevent any unwanted navigation

    const modalUrl = $('#addSessionContainer').data('url'); // URL for the partial view
    $.ajax({
        url: modalUrl,
        type: 'GET',
        success: function (response) {
            $('#addSessionContainer').html(response); // Load the modal content
            const sessionModal = new bootstrap.Modal(document.getElementById('sessionModal'));
            sessionModal.show(); // Open the modal
        },
        error: function () {
            $.growl.error({ title: "Error", message: "Failed to load Add Session form." });
        }
    });
});



// add set
$(document).ready(function () {
    var setIndex = 0;

    // Handle add set button click
    $(document).on('click', '#add-set', function () {
        var setTemplate = $('#set-template').html();
        var setIndex = $('#sets-container .set-entry').length; // Count current sets
        var newSet = setTemplate.replace(/Sets\[0\]/g, 'Sets[' + setIndex + ']');
        $('#sets-container').append(newSet);
    });


    // Handle remove set button click
    $(document).on('click', '.remove-set', function () {
        $(this).closest('.set-entry').remove();
    });

    $('form').submit(function () {
        var exerciseValue = $('#addSessionForm').val();
        // Make sure the dropdown's value is sent
        $('<input />').attr('type', 'hidden')
            .attr('name', 'ExerciseName')
            .attr('value', exerciseValue)
            .appendTo('form');
    });
});


function toggleSidebar() {
    const sidebar = document.getElementById('sidebar');
    sidebar.classList.toggle('open');
}


// download all sessions to csv
$('#sessionTable').on('load-success.bs.table', function () {
    $('#downloadallSessions').click(function () {
        var titles = [];
        var data = [];

        // get the table headers for CSV headers
        $('#sessionTable th').each(function () {
            titles.push($(this).text());
        });

        // het the actual data from table rows
        $('#sessionTable tbody tr').each(function () {
            var rowData = [];
            $(this).find('td').each(function () {
                rowData.push($(this).text());
            });
            data.push(rowData); 
        });

        // convert the data to CSV string
        var CSVString = prepCSVRow(titles, titles.length, '');
        CSVString = prepCSVRow(data.flat(), titles.length, CSVString);

        // make CSV downloadable
        var downloadLink = document.createElement("a");
        var blob = new Blob(["\ufeff", CSVString]);
        var url = URL.createObjectURL(blob);
        downloadLink.href = url;
        downloadLink.download = "allSessions.csv";

        // actually download
        document.body.appendChild(downloadLink);
        downloadLink.click();
        document.body.removeChild(downloadLink);
    });
});

$('#sessionTable').on('load-success.bs.table', function () {
    $('#downloadSessions').click(function () {
        var titles = [];
        var data = [];

        // get the table headers for CSV headers
        $('#sessionTable th').each(function () {
            titles.push($(this).text());
        });

        // het the actual data from table rows
        $('#sessionTable tbody tr').each(function () {
            var rowData = [];
            $(this).find('td').each(function () {
                rowData.push($(this).text());
            });
            data.push(rowData);
        });

        // convert the data to CSV string
        var CSVString = prepCSVRow(titles, titles.length, '');
        CSVString = prepCSVRow(data.flat(), titles.length, CSVString);

        // make CSV downloadable
        var downloadLink = document.createElement("a");
        var blob = new Blob(["\ufeff", CSVString]);
        var url = URL.createObjectURL(blob);
        downloadLink.href = url;
        downloadLink.download = "allSessions.csv";

        // actually download
        document.body.appendChild(downloadLink);
        downloadLink.click();
        document.body.removeChild(downloadLink);
    });
});


$('#exerciseTable').on('load-success.bs.table', function () {
    $('#downloadAllExercises').click(function () {
        var titles = [];
        var data = [];

        // get the table headers for CSV headers
        $('#exerciseTable th').each(function () {
            titles.push($(this).text());
        });

        // het the actual data from table rows
        $('#exerciseTable tbody tr').each(function () {
            var rowData = [];
            $(this).find('td').each(function () {
                rowData.push($(this).text());
            });
            data.push(rowData);
        });

        // convert the data to CSV string
        var CSVString = prepCSVRow(titles, titles.length, '');
        CSVString = prepCSVRow(data.flat(), titles.length, CSVString);

        // make CSV downloadable
        var downloadLink = document.createElement("a");
        var blob = new Blob(["\ufeff", CSVString]);
        var url = URL.createObjectURL(blob);
        downloadLink.href = url;
        downloadLink.download = "allExercises.csv";

        // actually download
        document.body.appendChild(downloadLink);
        downloadLink.click();
        document.body.removeChild(downloadLink);
    });
});

$('#OneRepMaxesTable').on('load-success.bs.table', function () {
    $('#downloadallOneRepMaxes').click(function () {
        var titles = [];
        var data = [];

        // get the table headers for CSV headers
        $('#OneRepMaxesTable th').each(function () {
            titles.push($(this).text());
        });

        // het the actual data from table rows
        $('#OneRepMaxesTable tbody tr').each(function () {
            var rowData = [];
            $(this).find('td').each(function () {
                rowData.push($(this).text());
            });
            data.push(rowData);
        });

        // convert the data to CSV string
        var CSVString = prepCSVRow(titles, titles.length, '');
        CSVString = prepCSVRow(data.flat(), titles.length, CSVString);

        // make CSV downloadable
        var downloadLink = document.createElement("a");
        var blob = new Blob(["\ufeff", CSVString]);
        var url = URL.createObjectURL(blob);
        downloadLink.href = url;
        downloadLink.download = "allOneRepMaxes.csv";

        // actually download
        document.body.appendChild(downloadLink);
        downloadLink.click();
        document.body.removeChild(downloadLink);
    });
});

// function to prepare CSV rows
function prepCSVRow(arr, columnCount, initial) {
    var row = '';
    var delimeter = ','; 
    var newLine = '\r\n';

    // Split the array into chunks of the column count (for rows)
    function splitArray(_arr, _count) {
        var splitted = [];
        var result = [];
        _arr.forEach(function (item, idx) {
            if ((idx + 1) % _count === 0) {
                splitted.push(item);
                result.push(splitted);
                splitted = [];
            } else {
                splitted.push(item);
            }
        });
        return result;
    }

    var plainArr = splitArray(arr, columnCount);

    // Convert array to CSV string
    plainArr.forEach(function (arrItem) {
        arrItem.forEach(function (item, idx) {
            row += item + ((idx + 1) === arrItem.length ? '' : delimeter);
        });
        row += newLine;
    });
    return initial + row;
}

// chart download functionality 
$(document).ready(function () {
    $("#downloadExercisesPieChart").click(function () {
        var canvas = document.getElementById("exercisesPieChart");
        var image = canvas.toDataURL("image/png"); // Convert canvas to image URL

        var link = document.createElement("a");
        link.href = image;
        link.download = "exercises_pie_chart.png";
        link.click();
    });
});

$(document).ready(function () {
    $("#downloadSessionsLineChart").click(function () {
        var canvas = document.getElementById("sessionsLineChart");
        var image = canvas.toDataURL("image/png"); // Convert canvas to image URL

        var link = document.createElement("a");
        link.href = image;
        link.download = "sessions_line_chart.png"; 
        link.click();
    });
});

$(document).ready(function () {
    $("#downloadSetsBarChart").click(function () {
        var canvas = document.getElementById("setsLineChart");
        var image = canvas.toDataURL("image/png"); // Convert canvas to image URL

        var link = document.createElement("a");
        link.href = image;
        link.download = "sets_bar_chart.png";
        link.click();
    });
});

$(document).ready(function () {
    $("#downloadSetsProgressChart").click(function () {
        var canvas = document.getElementById("setsBarchartExercise");
        var image = canvas.toDataURL("image/png"); // Convert canvas to image URL

        var link = document.createElement("a");
        link.href = image;
        link.download = "sets_progress_chart.png";
        link.click();
    });
});

$(document).ready(function () {
    $("#downloadORMChart").click(function () {
        var canvas = document.getElementById("oneRepMaxChart");
        var image = canvas.toDataURL("image/png"); // Convert canvas to image URL

        var link = document.createElement("a");
        link.href = image;
        link.download = "one_rep_max_chart.png";
        link.click();
    });
});

$(document).ready(function () {
    $("#downloadVolumeChart").click(function () {
        var canvas = document.getElementById("volumePerSessionChart");
        var image = canvas.toDataURL("image/png"); // Convert canvas to image URL

        var link = document.createElement("a");
        link.href = image;
        link.download = "volume_chart.png";
        link.click();
    });
});