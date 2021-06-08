$(document).ready(function () {
    console.log("ready!");
    GetConforts()
});

function AddConfort() {
    var newConfort = {};
    newConfort.nombre = $('#nombreComodidad').val()
    newConfort.descripcion = $('#descripcionComodidad').val()

    $.ajax({
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(newConfort),
        url: "https://localhost:44310/api/Comodidad/AddComodidad",
        success: function (res) {
            location.reload();
        },
        error: function (data) {

        }
    });
}

function UpdateConfort() {
    var newConfort = new Object;
    newConfort.numero = $('#updateNumeroComodidad').val();
    newConfort.nombre = $('#updateNombreComodidad').val();
    newConfort.descripcion = $('#updateDescripcionComodidad').val();


    $.ajax({
        method: "PUT",
        contentType: "application/json",
        data: JSON.stringify(newConfort),
        url: "https://localhost:44310/api/Comodidad/UpdateComodidad",
        success: function (res) {
            location.reload();
        },
        error: function (data) {

        }
    });
}

function DeleteConfort() {


    $.ajax({
        method: "DELETE",
        contentType: "application/json",
        url: "https://localhost:44310/api/Comodidad/DeleteComodidad?numComodidad=" + $('#updateNumeroComodidad').val(),
        success: function (res) {
            location.reload();
        },
        error: function (data) {

        }
    });
}

function GetConforts() {
    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "https://localhost:44310/api/Comodidad/GetAllComodidades",
        success: function (res) {
            generateConfortsTable(JSON.parse(res));
        },
        error: function (data) {

        }
    })
}


function generateConfortsTable(conforts) {

    var confortsData = [];


    conforts.forEach(element => {

        var aux = [];

        aux.push(String(element.numero));
        aux.push(String(element.nombre));
        aux.push(String(element.descripcion));


        confortsData.push(aux);
    })

    $(document).ready(function () {
        var table = $('#confortsTable').DataTable({
            data: confortsData,
            responsive: true,
            ordering: true,
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
        $('#confortsTable tr').click(function () {
            var data = table.row($(this)).data();
            openUpdateConfortModal(data[0], data[1], data[2]);
        })
    });
}
function openUpdateConfortModal(numC, NombC, DesC) {
    $("#updateConfortModal").modal({
        fadeDuration: 100,
        clickClose: true,
        showClose: false,
        closeExisting: false,
        modalClass: "w-1/2 rounded-lg bg-gray-800 px-2 py-4"
    });

    $('#updateNumeroComodidad').val(numC);
    $('#updateNombreComodidad').val(NombC);
    $('#updateDescripcionComodidad').val(DesC);
}

