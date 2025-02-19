$(document).ready(function () {
    $('#registerForm').on('submit', function (e) {
        e.preventDefault(); // Prevent default form submission

        const formData = $(this).serialize(); // Serialize form data

        $.ajax({
            url: $(this).attr('action'),
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $.growl.notice({ title: "Success", message: response.responseText });
                    if (response.responseReload) {
                        setTimeout(() => location.reload(), 2000); // Reload the page after 2 seconds
                    }
                } else {
                    $.growl.error({ title: "Error", message: response.responseText });
                }
            },
            error: function () {
                $.growl.error({ title: "Error", message: "An unexpected error occurred." });
            }
        });
    });
});

$(document).ready(function () {
    $('#loginForm').on('submit', function (e) {
        e.preventDefault(); // Prevent default form submission

        const formData = $(this).serialize(); // Serialize form data

        $.ajax({
            url: $(this).attr('action'),
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    $.growl.notice({ title: "Success", message: response.responseText });

                    setTimeout(function () {
                        window.location.href = response.redirectUrl;
                    }, 2000); 
                } else {
                    $.growl.error({ title: "Error", message: response.responseText });
                }
            },
            error: function () {
                $.growl.error({ title: "Error", message: "An unexpected error occurred." });
            }
        });
    });
});

