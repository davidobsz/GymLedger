

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

});