window.onload = load;

function load() {

    GetTodayCheckins();
    GetTodayCheckouts();
    GetCategoriasForChart();
    GetReservasForChart();
    generateSearchRoomsTable();
    generateCustomersTable();
}

var chartCat;
var chartRes;

function GetTodayCheckins() {

    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "https://localhost:44310/api/Reserva/GetTodayCheckins",
        success: function (res) {
            $('#todayCheckins').html(res);
        },
        error: function (data) {

        }
    })

}

function GetTodayCheckouts() {

    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "https://localhost:44310/api/Reserva/GetTodayCheckouts",
        success: function (res) {
            $('#todayCheckouts').html(res);
        },
        error: function (data) {

        }
    })

}



function HomeQuickSearch() {

    function GetClienteByNum(numCI) {

        $.ajax({
            method: "GET",
            contentType: "application/json",
            url: "https://localhost:44310/api/Cliente/GetClienteByNum?numCI=" + numCI,
            success: function (res) {
                $("#quickSearchCustomerModal").modal({
                    fadeDuration: 100,
                    clickClose: true,
                    showClose: false,
                    closeExisting: false,
                    modalClass: "w-11/12 rounded-lg bg-gray-800 px-2 py-4",
                });
            },
            error: function (data) {

            }
        })

    }
    function GetHabitacionByNum(numRoom) {

        $.ajax({
            method: "GET",
            contentType: "application/json",
            url: "https://localhost:44310/api/Habitacion/GetHabitacionByNum?numRoom=" + numRoom,
            success: function (res) {
                $("#quickSearchRoomModal").modal({
                    fadeDuration: 100,
                    clickClose: true,
                    showClose: false,
                    closeExisting: false,
                    modalClass: "w-11/12 rounded-lg bg-gray-800 px-2 py-4",
                });
            },
            error: function (data) {

            }
        })

    }
    function GetReservaByCheckin(checkin) {

        $.ajax({
            method: "GET",
            contentType: "application/json",
            url: "https://localhost:44310/api/Reserva/GetReservaByCheckin?checkin=" + checkin,
            success: function (res) {

            },
            error: function (data) {

            }
        })

    }
    function GetReservaByCheckin(checkout) {

        $.ajax({
            method: "GET",
            contentType: "application/json",
            url: "https://localhost:44310/api/Reserva/GetReservaByCheckout?checkout=" + checkout,
            success: function (res) {

            },
            error: function (data) {

            }
        })

    }

    function GetReservaByCheckinAndCheckout(checkin, checkout) {

        $.ajax({
            method: "GET",
            contentType: "application/json",
            url: "https://localhost:44310/api/Reserva/GetReservaByCheckinAndCheckout?checkin=" + checkin + "&checkout=" + checkout,
            success: function (res) {

            },
            error: function (data) {

            }
        })

    }

    toSearchData = {};
    toSearchData.customer = $('#quickSearchCustomer').val();
    toSearchData.room = $('#quickSearchRoom').val();
    toSearchData.checkinDate = $('#quickSearchCheckinDate').val();
    toSearchData.checkoutDate = $('#quickSearchCheckoutDate').val();

    switch (toSearchData) {
        case (toSearchData.customer != "" && toSearchData.room == "" && toSearchData.checkinDate == "" && toSearchData.checkoutDate == ""):

            GetClienteByNum(toSearchData.customer);

            break;

        case (toSearchData.customer == "" && toSearchData.room != "" && toSearchData.checkinDate == "" && toSearchData.checkoutDate == ""):

            GetHabitacionByNum(toSearchData.room);

            break;

        case (toSearchData.customer == "" && toSearchData.room == "" && toSearchData.checkinDate != "" && toSearchData.checkoutDate == ""):

            GetReservaByCheckin(toSearchData.checkinDate);

            break;

        case (toSearchData.customer == "" && toSearchData.room == "" && toSearchData.checkinDate == "" && toSearchData.checkoutDate != ""):

            GetReservaByCheckout(toSearchData.checkoutDate);

            break;

        case (toSearchData.customer == "" && toSearchData.room == "" && toSearchData.checkinDate != "" && toSearchData.checkoutDate != ""):

            GetReservaByCheckinAndCheckout(toSearchData.checkinDate, toSearchData.checkoutDate);

            break;

        default:
            break;
    }


}

function GetCategoriasForChart() {
    var fechas = {};
    fechas.fecha1 = $('#chartFirstDate').val();
    fechas.fecha2 = $('#chartSecondDate').val();

    if (chartCat != undefined) {
        $('#canvasContainerCategorias').html("");
        $('#canvasContainerCategorias').append('<canvas id="chartCategorias" width="200" height="150" class=""></canvas>');
    }

    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "https://localhost:44310/api/Categoria/GetCategoriasForChart?fecha1=" + fechas.fecha1 + "&fecha2=" + fechas.fecha2,
        success: function (res) {
            generateChartCategorias(JSON.parse(res))
        },
        error: function (data) {

        }
    })

}


function generateChartCategorias(categoriesData) {

    var data = [];
    var labels = [];
    var backgroundColors = [];

    categoriesData.forEach(categoria => {
        data.push(categoria.cantidad)
        labels.push(categoria.nombre)
        backgroundColors.push('#667eea');
    })

    var canvas = document.getElementById('chartCategorias');
    var ctx = canvas.getContext('2d');

    ctx.clearRect(0, 0, canvas.width, canvas.height);

    chartCat = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Categorias',
                data: data,
                backgroundColor: backgroundColors,
                borderWidth: 0,
            }],
        },
        options: {
            legend: {
                labels: {
                    fontColor: "#f7fafc",
                }
            },
        }
    });

}


function generateChartReservas(reservasData) {

    var data = [];
    var labels = [];
    var backgroundColors = [];

    reservasData.forEach(reserva => {
        data.push(reserva.cantidad)
        if (reserva.estado == 0) {
            labels.push("Nueva")
        }
        if (reserva.estado == 1) {
            labels.push("Ingreso")
        }
        if (reserva.estado == 2) {
            labels.push("Cancelada")
        }
        if (reserva.estado == 3) {
            labels.push("Finalizada")
        }

        backgroundColors.push('#667eea');
    })

    var canvas = document.getElementById('chartReservas');
    var ctx = canvas.getContext('2d');

    ctx.clearRect(0, 0, canvas.width, canvas.height);

    chartRes = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Reservas',
                data: data,
                backgroundColor: backgroundColors,
                borderWidth: 0,
            }],
        },
        options: {
            legend: {
                labels: {
                    fontColor: "#f7fafc",
                }
            },
        }
    });

}



function generateCustomersTable() {

    var customersData = [];


    $(document).ready(function () {
        var table = $('#customersTable').DataTable({
            data: customersData,
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

        function acomodarLaPutaFecha(_laFechaToRota) {

            var date = [];

            date.push(_laFechaToRota.substring(6, 10));
            date.push(_laFechaToRota.substring(3, 5));
            date.push(_laFechaToRota.substring(0, 2));

            var strDate = date.join("-");

            return strDate;

        }


        $('#searchCustomerButton').click(function () {
            var ci = $('#quickSearchCiCliente').val();
            GetCliente(ci);
        });

        function mostrarClientes(toShowCustomers) {

            table.clear().draw();


            var CustomerData = [];

            CustomerData.push(String(toShowCustomers.ci));
            CustomerData.push(String(toShowCustomers.nombre));
            CustomerData.push(String(toShowCustomers.apellido));
            CustomerData.push(String(toShowCustomers.fechaNacimiento));
            CustomerData.push(String(toShowCustomers.pais));
            CustomerData.push(String(toShowCustomers.email));

            table.row.add(CustomerData).draw();
        }


        function GetCliente(ci) {

            $.ajax({
                method: "GET",
                contentType: "application/json",
                url: "https://localhost:44310/api/Cliente/GetClienteByCi?ciCliente=" + ci,
                success: function (res) {
                    console.info(JSON.parse(res));
                    mostrarClientes(JSON.parse(res));
                },
                error: function (data) {

                }
            })
        }
    });
}


function generateSearchRoomsTable() {

    var enumTamanoCama = ["Twin", "Queen", "King"];

    var roomsData = [];

    $(document).ready(function () {
        var table = $('#roomsTable').DataTable({
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


        $('#searchRoomButton').click(function () {
            var fecha1 = $('#quickSearchCheckinDate').val();
            var fecha2 = $('#quickSearchCheckoutDate').val();
            GetHabitacionesLibres(fecha1, fecha2);
        });

        function mostrarHabitacionesLibres(toShowRooms) {

            table.clear().draw();


            var roomsData = [];

            toShowRooms.forEach(element => {

                var aux = [];

                aux.push(String(element.numero));
                aux.push(String(element.piso));
                aux.push(String(element.categoria));
                aux.push(String(element.tamano));
                aux.push(enumTamanoCama[element.tamanoCama]);
                aux.push(String(element.descripcion));
                aux.push(String(element.comodidades));

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

function GetReservasForChart() {
    var fechas = {};
    fechas.fecha1 = $('#chartFirstDateReserva').val();
    fechas.fecha2 = $('#chartSecondDateReserva').val();

    if (chartRes != undefined) {
        $('#canvasContainerReservas').html("");
        $('#canvasContainerReservas').append('<canvas id="chartReservas" width="200" height="150" class=""></canvas>');
    }

    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "https://localhost:44310/api/Reserva/GetReservasForChart?fecha1=" + fechas.fecha1 + "&fecha2=" + fechas.fecha2,
        success: function (res) {
            generateChartReservas(JSON.parse(res))
        },
        error: function (data) {

        }
    })

}