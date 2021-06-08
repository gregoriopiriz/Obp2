$(document).ready(function () {

    toggleTipoCliente();
    GetRoomsForBookings();
    GetRoomsForUpdateBookings()
    GetBookings();

});

var numHabs;

var numGasto;

$('input:radio[name=tipoCliente]').click(function () {
    toggleTipoCliente();
})

function toggleTipoCliente() {

    if ($('input:radio[name=tipoCliente]:checked').val() == "false") {
        $('#formForNoRegistrado').show();
        $('#formForRegistrado').hide();
    }
    else {
        $('#formForNoRegistrado').hide();
        $('#formForRegistrado').show();
    }
}

function AddBooking(numHabs) {

    var newBooking = {};
    newBooking.fechaInicio = $('#fechaInicio').val();
    newBooking.fechaFin = $('#fechaFin').val();

    if ($('input:radio[name=tipoCliente]:checked').val() == "false") {
        newBooking.CiCliente = $('#documentoNoRegistrado').val();
        newBooking.nombreCliente = $('#nombreNoRegistrado').val();
        newBooking.apellidoCliente = $('#apellidoNoRegistrado').val();
    }
    else {
        newBooking.CiCliente = $('#documentoRegistrado').val();
    }

    newBooking.numHabitacion = numHabs;
    newBooking.clienteRegistrado = $('input:radio[name=tipoCliente]:checked').val();


    $.ajax({
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(newBooking),
        url: "https://localhost:44310/api/Reserva/AddReserva",
        success: function (res) {
            location.reload()
        },
        error: function (data) {

        }
    })
}

function GetEmailClienteByCi(ciCliente) {

    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "https://localhost:44310/api/Cliente/GetEmailClienteByCi?ciCliente=" + ciCliente,
        success: function (res) {
            return res;
        },
        error: function (data) {

        }
    })
}

function UpdateBooking(numHabs, state, cR, idReserva) {

    var toEditBooking = {};
    toEditBooking.idReserva = idReserva;
    toEditBooking.CiCliente = $('#updateDocumento').val();
    toEditBooking.fechaInicio = $('#updateFechaInicio').val();
    toEditBooking.fechaFin = $('#updateFechaFin').val();
    toEditBooking.numHabitacion = numHabs;
    toEditBooking.estadoReserva = state;
    toEditBooking.clienteRegistrado = cR;

    $.ajax({
        method: "PUT",
        contentType: "application/json",
        data: JSON.stringify(toEditBooking),
        url: "https://localhost:44310/api/Reserva/UpdateReserva",
        success: function (res) {
            location.reload()
        },
        error: function (data) {

        }
    })
}

$('#editBookingModal').on('modal:close', function () {
    location.reload();
})

function GetBookings() {

    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "https://localhost:44310/api/Reserva/GetAllReservas",
        success: function (res) {
            generateCalendar(JSON.parse(res));
        },
        error: function (data) {

        }
    })
}

function CancelBooking(idReserva) {

    $.ajax({
        method: "DELETE",
        contentType: "application/json",
        url: "https://localhost:44310/api/Reserva/CancelarReserva?idReserva=" + idReserva,
        success: function (res) {
            location.reload()
        },
        error: function (data) {

        }
    })
}


function GetRoomsForBookings() {

    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "https://localhost:44310/api/Habitacion/GetAllHabitaciones",
        success: function (res) {
            generateBookingRoomsTable(JSON.parse(res));

        },
        error: function (data) {

        }
    })
}

function GetRoomsForUpdateBookings() {

    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "https://localhost:44310/api/Habitacion/GetAllHabitaciones",
        success: function (res) {
            generateUpdateBookingRoomsTable(JSON.parse(res));
        },
        error: function (data) {

        }
    })
}

function CambiarEstado(estado, idReserva) {

    $.ajax({
        method: "PUT",
        contentType: "application/json",
        url: "https://localhost:44310/api/Reserva/CambiarEstado?_estado=" + estado + "&_idReserva=" + idReserva,
        success: function (res) {
            location.reload()
        },
        error: function (data) {

        }
    })
}

function generateBookingRoomsTable(rooms) {

    var enumTamanoCama = ["Twin", "Queen", "King"];

    var roomsData = [];

    rooms.forEach(element => {

        var aux = [];

        aux.push(String(element.numero));
        aux.push(String(element.numero));
        aux.push(String(element.piso));
        aux.push(String(element.categoria));
        aux.push(enumTamanoCama[element.tamanoCama]);

        roomsData.push(aux);
    })

    $(document).ready(function () {
        var table = $('#bookingRoomsTable').DataTable({
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
            select: {
                style: 'multi'
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

        $('#addBookingButton').click(function () {
            var toBookRooms = [];
            var rows_selected = table.column(0).checkboxes.selected();
            $.each(rows_selected, function (index, rowId) {
                toBookRooms.push(rowId);
            });

            AddBooking(toBookRooms)
        });

        $('#checkHabsButton').click(function () {
            var fecha1 = $('#fechaInicio').val();
            var fecha2 = $('#fechaFin').val();
            GetHabitacionesLibres(fecha1, fecha2);
        });

        function mostrarHabitacionesLibres(toShowRooms) {

            table.clear().draw();


            var roomsData = [];

            toShowRooms.forEach(element => {

                var aux = [];

                aux.push(String(element.numero));
                aux.push(String(element.numero));
                aux.push(String(element.piso));
                aux.push(String(element.categoria));
                aux.push(enumTamanoCama[element.tamanoCama]);

                roomsData.push(aux);
            })

            roomsData.forEach(room => {
                table.row.add(room).draw();
            })

        }


        function GetHabitacionesLibres(fecha1, fecha2) {

            $.ajax({
                method: "GET",
                contentType: "application/json",
                url: "https://localhost:44310/api/Habitacion/GetHabitacionesLibres?fecha1=" + fecha1 + "&fecha2=" + fecha2,
                success: function (res) {
                    mostrarHabitacionesLibres(JSON.parse(res));
                },
                error: function (data) {

                }
            })
        }

    });


}

function generateUpdateBookingRoomsTable(rooms) {

    $('#HabitacionesReservadas').val(String(numHabs));

    var enumTamanoCama = ["Twin", "Queen", "King"];

    var roomsData = [];

    rooms.forEach(element => {

        var aux = [];

        aux.push(String(element.numero));
        aux.push(String(element.numero));
        aux.push(String(element.piso));
        aux.push(String(element.categoria));
        aux.push(enumTamanoCama[element.tamanoCama]);



        roomsData.push(aux);
    })


    $(document).ready(function () {
        var table = $('#updateBookingRoomsTable').DataTable({
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
            select: {
                style: 'multi'
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

        $('#updateBookingButton').click(function () {
            var toBookRooms = [];
            var rows_selected = table.column(0).checkboxes.selected();
            $.each(rows_selected, function (index, rowId) {
                toBookRooms.push(rowId);
            });

            UpdateBooking(toBookRooms, $('#estadoReserva').val(), $('#clienteRegistrado').val(), $('#idReserva').val())
        });
        $('#checkinBookingButton').click(function () {
            var toBookRooms = [];
            var rows_selected = table.column(0).checkboxes.selected();
            $.each(rows_selected, function (index, rowId) {
                toBookRooms.push(rowId);
            });

            CambiarEstado('INGRESO', $('#idReserva').val())
        });
        $('#checkoutBookingButton').click(function () {
            var toBookRooms = [];
            var rows_selected = table.column(0).checkboxes.selected();
            $.each(rows_selected, function (index, rowId) {
                toBookRooms.push(rowId);
            });

            CambiarEstado('FINALIZADA', $('#idReserva').val())
        });

        $('#checkModHabsButton').click(function () {
            var fecha1 = $('#updateFechaInicio').val();
            var fecha2 = $('#updateFechaFin').val();
            var idReserva = $('#idReserva').val();
            GetHabitacionesLibres(fecha1, fecha2, idReserva);
        });

        function mostrarHabitacionesLibres(toShowRooms) {

            table.clear().draw();


            var roomsData = [];

            toShowRooms.forEach(element => {

                var aux = [];

                aux.push(String(element.numero));
                aux.push(String(element.numero));
                aux.push(String(element.piso));
                aux.push(String(element.categoria));
                aux.push(enumTamanoCama[element.tamanoCama]);

                roomsData.push(aux);
            })

            roomsData.forEach(room => {
                table.row.add(room).draw();
            })

        }


        function GetHabitacionesLibres(fecha1, fecha2, idReserva) {

            $.ajax({
                method: "GET",
                contentType: "application/json",
                url: "https://localhost:44310/api/Habitacion/GetHabitacionesLibres?fecha1=" + fecha1 + "&fecha2=" + fecha2 + "&idReserva=" + idReserva,
                success: function (res) {
                    mostrarHabitacionesLibres(JSON.parse(res));
                },
                error: function (data) {

                }
            })
        }

    });
}




// TENES QUE ARREGLAR LA FECHA ACA PARA QUE TAMBIEN DEVUELVA LA HORA LOS MIN Y LOS SEG, 
// SI QUERES PODES SACARLE LOS MIN Y SEG SI TE MOLESTA PERO ES EN VARIOS LUGARES
function acomodarLaPutaFecha(_laFechaToRota) {

    var date = [];

    date.push(_laFechaToRota.substring(6, 10));
    date.push(_laFechaToRota.substring(3, 5));
    date.push(_laFechaToRota.substring(0, 2));

    var strDate = date.join("-");

    return strDate;

}


function generateCalendar(eventList) {

    var eventListParsed = [];

    var estadoReserva = ['Nueva', 'Ingreso', 'Finalizada', 'Cancelada'];

    eventList.forEach(reserva => {

        var aux = {};

        aux.title = reserva.nombreCliente + " " + reserva.apellidoCliente + " | Estado: " + estadoReserva[reserva.estadoReserva] + " | $ " + reserva.precioTotal;
        aux.start = reserva.fechaInicio + " 00:00:00";
        aux.end = reserva.fechaFin + " 23:59:00";
        aux.ciCliente = reserva.ciCliente;
        aux.nombreCliente = reserva.nombreCliente;
        aux.apellidoCliente = reserva.apellidoCliente;
        aux.idReserva = reserva.idReserva;
        aux.clienteRegistrado = reserva.clienteRegistrado;
        aux.estado = reserva.estadoReserva;
        aux.numHabitaciones = reserva.numHabitacion;
        aux.precioTotal = reserva.precioTotal;


        eventListParsed.push(aux);

    })

    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        plugins: ['dayGrid'],
        locale: 'es',
        height: 500,
        selectable: true,
        defaultView: 'dayGridWeek',
        header: {
            left: 'title',
            center: '',
            right: 'today prev,next'
        },
        footer: {
            center: 'AddBookingButton',
        },
        events: eventListParsed,
        eventClick: function (info) {
            openEditBookingModal();

            var start = calendar.formatDate(info.event.start, {
                day: '2-digit',
                month: '2-digit',
                year: 'numeric',
                hour: '2-digit',
                minute: '2-digit',
                second: '2-digit',
            });
            var end = calendar.formatDate(info.event.end, {
                day: '2-digit',
                month: '2-digit',
                year: 'numeric',
                hour: '2-digit',
                minute: '2-digit',
                second: '2-digit',
            });

            $('#updateFechaInicio').val(acomodarLaPutaFecha(start));
            $('#updateFechaFin').val(acomodarLaPutaFecha(end));
            $('#updateNombre').val(info.event.extendedProps.nombreCliente);
            $('#updateApellido').val(info.event.extendedProps.apellidoCliente);
            $('#updateDocumento').val(info.event.extendedProps.ciCliente);
            $('#idReserva').val(info.event.extendedProps.idReserva);
            $('#clienteRegistrado').val(info.event.extendedProps.clienteRegistrado);
            $('#estadoReserva').val(info.event.extendedProps.estadoReserva);

            $('#precioTotal').val("$ " + info.event.extendedProps.precioTotal);

            numHabs = info.event.extendedProps.numHabitaciones;

            $('#HabitacionesReservadas').val(String(numHabs));

            $('#cancelBookingButton').click(function () {
                CancelBooking(info.event.extendedProps.idReserva);
            });

            GetGastos($('#idReserva').val());

        }
    });

    calendar.render();
};


$('#addGastoButton').click(function () {
    AddGasto($('#idReserva').val());
});


function AddGasto(idReserva) {

    var newGasto = {};
    newGasto.nombre = $('#NombreGasto').val();
    newGasto.observacion = $('#DescripcionGasto').val();
    newGasto.precioTotal = $('#PrecioGasto').val();

    $.ajax({
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(newGasto),
        url: "https://localhost:44310/api/Gasto/AddGasto?idReserva=" + idReserva,
        success: function (res) {
            location.reload()
        },
        error: function (data) {

        }
    })
}

function GetGastos(idReserva) {

    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "https://localhost:44310/api/Gasto/GetAllGastos?idReserva=" + idReserva,
        success: function (res) {
            generateGastosTable(JSON.parse(res));
        },
        error: function (data) {

        }
    })
}

function DeleteGasto(numGasto) {

    $.ajax({
        method: "DELETE",
        contentType: "application/json",
        url: "https://localhost:44310/api/Gasto/DeleteGasto?numGasto=" + numGasto,
        success: function (res) {
            location.reload();
        },
        error: function (data) {

        }
    })
}


function generateGastosTable(gastos) {

    var gastosData = [];

    gastos.forEach(element => {

        var aux = [];

        aux.push(String(element.numero));
        aux.push(String(element.nombre));
        aux.push(String(element.observacion));
        aux.push(String(element.precioTotal));


        gastosData.push(aux);
    })


    $(document).ready(function () {
        var table = $('#updateGastosTable').DataTable({
            data: gastosData,
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
            order: [[1, 'asc']]
        })
            .columns.adjust()
            .responsive.recalc();




        $('#updateGastosTable tr').on("click", function () {
            var data = table.row().data();
            console.info('la puta madre');
            openDeleteGastoModal()

            $('#deleteGastoButton').on("click", function () {
                DeleteGasto(data[0])
            })
        });
    });
};
