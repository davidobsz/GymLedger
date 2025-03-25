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