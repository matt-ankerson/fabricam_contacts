$(document).ready(function (e) {

    var contactId;

    $(".delete-contact-btn").on("click", function (e) {
        e.preventDefault();

        contactId = $(this).attr("id");
        var invoker = $(this);

        // Launch the delete confirmation modal.
        $("#delete-modal").modal();

        // When the modal opens:
        $('#delete-modal').on('shown.bs.modal', function (e) {

            var firstname = invoker.find(".hidden-firstname").val();
            var lastname = invoker.find(".hidden-lastname").val();
            
            // Change the modal title.
            $("#modal-title").text("Delete " + firstname + " " + lastname + "?");

        });

    });

    $("#confirm-delete-btn").on("click", function(e) {
        
        // submit contact deletion to controller.
        $.ajax({
            type: "GET",
            url: "/Home/DeleteContact",
            data: { contactId: contactId },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: success_func,
            error: error_func
        });

        function success_func(data, status) {
            location.reload(true);
        }

        function error_func(error) {
            alert("There was a problem deleting the contact.");
        }

    });

});