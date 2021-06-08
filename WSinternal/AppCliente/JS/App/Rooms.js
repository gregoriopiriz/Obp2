$(document).ready(function () {

    GetRooms();
    GetCategoriesForSelect();
    GetConfortsForRooms();
});



var toAddConforts = [];
var toEditConforts = [];
var aux;

function AddRoom() {
    var newRoom = new Object;
    newRoom.numero = $('#numero').val()
    newRoom.piso = $('#piso').val()
    newRoom.tamano = $('#superficie').val()
    newRoom.numCategoria = $('#categoria').val()
    newRoom.tamanoCama = $('#tamanoCama').val()
    newRoom.descripcion = $('#descripcion').val()
    newRoom.comodidades = toAddConforts;

    $.ajax({
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(newRoom),
        url: "https://localhost:44310/api/Habitacion/AddHabitacion",
        success: function (res) {
            location.reload();
        },
        error: function (data) {

        }
    });
}

function GetRooms() {

    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "https://localhost:44310/api/Habitacion/GetAllHabitaciones",
        success: function (res) {
            generateRoomsTable(JSON.parse(res));
        },
        error: function (data) {

        }
    })
}

function UpdateRoom(conforts) {
    var newRoom = new Object;
    newRoom.numero = $('#updateNumero').val()
    newRoom.piso = $('#updatePiso').val()
    newRoom.tamano = $('#updateSuperficie').val()
    newRoom.numCategoria = $('#updateCategoria').val()
    newRoom.tamanoCama = $('#updateTamanoCama').val()
    newRoom.descripcion = $('#updateDescripcion').val()
    newRoom.comodidades = conforts

    $.ajax({
        method: "PUT",
        contentType: "application/json",
        data: JSON.stringify(newRoom),
        url: "https://localhost:44310/api/Habitacion/UpdateHabitacion",
        success: function (res) {
            location.reload();
        },
        error: function (data) {

        }
    });
}

function DeleteRoom(numero) {

    $.ajax({
        method: "DELETE",
        contentType: "application/json",
        url: "https://localhost:44310/api/Habitacion/DeleteHabitacion?numHabitacion=" + numero,
        success: function (res) {
            location.reload();
        },
        error: function (data) {

        }
    });
}

function generateRoomsTable(rooms) {


    var enumTamanoCama = ["Twin", "Queen", "King"];

    var roomsData = [];

    rooms.forEach(element => {

        var aux = [];

        aux.push(String(element.numero));
        aux.push(String(element.piso));
        aux.push(String(element.categoria));
        aux.push(String(element.tamano));
        aux.push(enumTamanoCama[element.tamanoCama]);
        aux.push(String(element.descripcion));
        aux.push(String(element.comodidades));
        aux.push(String(element.numCategoria));
        console.info(element.numCategoria);



        roomsData.push(aux);
    })

    $(document).ready(function () {
        var RoomsTable = $('#roomsTable').DataTable({
            data: roomsData,
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
            },
            columnDefs: [
                {
                    "targets": [7],
                    "visible": false,
                    "searchable": false
                },
            ]
        })
            .columns.adjust()
            .responsive.recalc();

        $('#roomsTable tr').on("click", function () {
            var data = RoomsTable.row($(this)).data();
            $("#updateRoomModal").modal({
                fadeDuration: 100,
                clickClose: true,
                showClose: false,
                closeExisting: false,
                modalClass: "w-11/12 rounded-lg bg-gray-800 px-2 py-4",
            });
            console.info(data)<
            $('#updateNumero').val(data[0]);
            $('#updatePiso').val(data[1]);
            $('#updateSuperficie').val(data[3]);
            $('#updateCategoria').val(data[7]);
            $('#updateTamanoCama').val(data[4].toUpperCase());
            $('#updateDescripcion').val(data[5]);

            aux = data[6].split(",").map(Number);

            $('#deleteRoomButton').on("click", function () {
                DeleteRoom(data[0])
            })
        });

    });


}



function GetCategoriesForSelect() {

    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "https://localhost:44310/api/Categoria/GetAllCategorias",
        success: function (res) {
            generateCategorySelect(JSON.parse(res));
        },
        error: function (data) {

        }
    })
}

function generateCategorySelect(categories) {

    categories.forEach(element => {
        $('#categoria').append(
            `<option value="${element.numero}">${element.nombre}</option>`
        )
        $('#updateCategoria').append(
            `<option value="${element.numero}">${element.nombre}</option>`
        )
    })
}

function GetConfortsForRooms() {

    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "https://localhost:44310/api/Comodidad/GetAllComodidades",
        success: function (res) {
            generateconfortsTableForAddRooms(JSON.parse(res));
            generateconfortsTableForEditRooms(JSON.parse(res));
        },
        error: function (data) {

        }
    })
}


function generateconfortsTableForAddRooms(conforts) {

    var confortsData = [];


    conforts.forEach(element => {

        var aux = [];

        aux.push(String(element.numero));
        aux.push(String(element.nombre));
        aux.push(String(element.descripcion));


        confortsData.push(aux);
    })

    $(document).ready(function () {
        var table = $('#confortsTableAddRoom').DataTable({
            data: confortsData,
            responsive: true,
            select: {
                style: 'multi'
            },
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
            columnDefs: [
                {
                    'targets': 0,
                    'checkboxes': {
                        'selectRow': true
                    }
                }
            ],
            order: [[1, 'asc']]
        })
            .columns.adjust()
            .responsive.recalc();
        $('#addRoomButton').click(function () {
            var rows_selected = table.column(0).checkboxes.selected();
            $.each(rows_selected, function (index, rowId) {
                toAddConforts.push(rowId);
            });
            AddRoom();
        });
    });
}

function generateconfortsTableForEditRooms(conforts) {

    var confortsData = [];


    conforts.forEach(element => {

        var aux = [];

        aux.push(String(element.numero));
        aux.push(String(element.nombre));
        aux.push(String(element.descripcion));


        confortsData.push(aux);
    })

    $(document).ready(function () {
        var table = $('#confortsTableEditRoom').DataTable({
            data: confortsData,
            responsive: true,
            ordering: true,
            select: {
                style: 'multi'
            },
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
            columnDefs: [
                {
                    'targets': 0,
                    'checkboxes': {
                        'selectRow': true
                    }
                }
            ],
            order: [[1, 'asc']]
        })
            .columns.adjust()
            .responsive.recalc();

        $('#updateRoomButton').click(function () {
            var rows_selected = table.column(0).checkboxes.selected();
            $.each(rows_selected, function (index, rowId) {
                toEditConforts.push(rowId);
            });

            UpdateRoom(toEditConforts)
        });


    });
}
