﻿$(document).ready(function () {

    // Make the admin window disappear when the close button is pressed.
    $("#admin_error_window_close").on("click", function (e) {
        $("#admin_error_window").remove();
    });
});