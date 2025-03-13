

$(document).ready(function () {
    $('#addSessionForm').on('submit', function (e) {
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
    $('#modalTable').on('click', 'tbody tr', function () {
        var uniqueId = $(this).attr("data-uniqueid");
        var url = $('#modalTableContainer').data('url');

        if (uniqueId) {
            $.ajax({
                url: url,
                type: 'GET',
                data: { uniqueId: uniqueId },
                success: function (response) {

                    // Check if the modal already exists and remove it if so
                    var existingModal = $('#modalModal');
                    if (existingModal.length) {
                        existingModal.remove();  // Remove the existing modal content
                    }

                    // Append the new modal content to the body
                    $("body").append(response);

                    // Ensure modal is initialized and shown
                    var modal = new bootstrap.Modal(document.getElementById('modalModal'));
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



// add set
$(document).ready(function () {
    var setIndex = 0;

    // Handle add set button click
    $('#add-set').click(function () {
        var setTemplate = $('#set-template').html();
        var newSet = setTemplate.replace(/Sets\[0\]/g, 'Sets[' + setIndex + ']');
        $('#sets-container').append(newSet);
        setIndex++;
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