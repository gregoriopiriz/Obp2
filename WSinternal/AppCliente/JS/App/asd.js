var confortsData = [
    ["1", "asd", "123"],
    ["2", "asd", "123"],
    ["3", "asd", "123"],
    ["4", "asd", "123"],
    ["5", "asd", "123"],
    ["6", "asd", "123"],
    ["7", "asd", "123"],
    ["8", "asd", "123"],
    ["9", "asd", "123"],
    ["10", "asd", "123"],
];

$(document).ready(function () {
    var table = $('#confortsTable').DataTable({
        data: confortsData,
        responsive: true,
        ordering: true,
        pageLength: 6,
        language: {
            lengthMenu: "",
            info: "",
            infoEmpty: "",
            infoFiltered: "",
            infoPostFix: "",
            loadingRecords: "",
            zeroRecords: "",
            emptyTable: "",
            paginate: {
                first: "Inicio",
                previous: "Anterior",
                next: "Siguiente",
                last: "Final"
            },
            search: "",
            searchPlaceholder: "Buscar...",
            select: {
                cells: "",
                columns: "",
                rows: "",
            },

        },
    })
        .columns.adjust()
        .responsive.recalc();
});
