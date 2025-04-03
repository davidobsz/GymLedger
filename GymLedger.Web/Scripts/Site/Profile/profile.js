$(document).ready(function () {
    // Bind click event to the addExercise button
    $('#getAccountDetails').on('click', function (e) {
        e.preventDefault();  // Prevent default link behavior

        var url = $('#AccountDetailsPartial').data('url');

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

$(document).ready(function () {
    // Bind click event to the addExercise button
    $('#ChangeAccountPassword').on('click', function (e) {
        e.preventDefault();  // Prevent default link behavior

        var url = $('#ChangePasswordPartial').data('url');

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

$(document).ready(function () {
    // Delegate the form submission event to the document to handle dynamically loaded content
    $(document).on('submit', '#changePasswordForm', function (e) {
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
    // Bind click event to the addExercise button
    $('#DeleteAccount').on('click', function (e) {
        e.preventDefault();  // Prevent default link behavior

        var url = $('#DeleteAccountPartial').data('url');

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


$(document).ready(function () {
    // Delegate the form submission event to the document to handle dynamically loaded content
    $(document).on('submit', '#deleteUserForm', function (e) {
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