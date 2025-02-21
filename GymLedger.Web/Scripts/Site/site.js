

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

    $('#addExerciseForm').on('submit', function (e) {
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

    $('#editExerciseForm').on('submit', function (e) {
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
                    console.log("AJAX Success, Response:", response);

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
                    console.log("AJAX Error:", error);
                    alert("Error loading exercise details.");
                }
            });
        } else {
            console.log("UniqueId not found!");
        }
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