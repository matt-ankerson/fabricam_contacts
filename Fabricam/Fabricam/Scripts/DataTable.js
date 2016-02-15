$(document).ready(function () {

    var colCount = 0;

    var table = $('#contact-table').DataTable({
        responsive: true,
        initComplete: function() {
            this.api().columns().every(function() {
                // Only for the third, fourth and fifth column.
                // Table is simple enough to afford this approach.
                if ((colCount == 2) || (colCount == 3) || (colCount == 4)) {
                    var column = this;
                    var select = $('<select><option value="">No filter</option></select>')
                        .appendTo($(column.footer()).empty())
                        .on('change', function () {
                            var val = $.fn.dataTable.util.escapeRegex(
                                $(this).val()
                            );
                            column
                                .search(val ? '^' + val + '$' : '', true, false)
                                .draw();
                        });

                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option value="' + d + '">' + d + '</option>');
                    });
                }
                colCount++;
            });
        }
    });
});