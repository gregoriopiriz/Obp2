$(document).ready(function () {
    GetCustomers()
});


function AddCustomer() {
    var newCustomer = new Object;
    newCustomer.ci = $('#documento').val();
    newCustomer.nombre = $('#nombre').val();
    newCustomer.apellido = $('#apellido').val();
    newCustomer.pais = $('#pais').val();
    newCustomer.fechaNacimiento = $('#fechaNacimiento').val();
    newCustomer.email = $('#email').val();


    $.ajax({
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(newCustomer),
        url: "https://localhost:44310/api/Cliente/AddCliente",
        success: function (res) {
            location.reload()
        },
        error: function (data) {

        }
    });
}

function UpdateCustomer() {
    var toUpdateCustomer = new Object;
    toUpdateCustomer.ci = $('#updateDocumento').val();
    toUpdateCustomer.nombre = $('#updateNombre').val();
    toUpdateCustomer.apellido = $('#updateApellido').val();
    toUpdateCustomer.pais = $('#updatePais').val();
    toUpdateCustomer.fechaNacimiento = $('#updateFechaNacimiento').val();
    toUpdateCustomer.email = $('#updateEmail').val();


    $.ajax({
        method: "PUT",
        contentType: "application/json",
        data: JSON.stringify(toUpdateCustomer),
        url: "https://localhost:44310/api/Cliente/UpdateCliente",
        success: function (res) {
            location.reload()
        },
        error: function (data) {

        }
    });
}

function GetCustomers() {

    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "https://localhost:44310/api/Cliente/GetAllClientes",
        success: function (res) {
            generateCustomersTable(JSON.parse(res));
        },
        error: function (data) {

        }
    })
}


function DeleteCustomers(num) {

    $.ajax({
        method: "DELETE",
        contentType: "application/json",
        url: "https://localhost:44310/api/Cliente/DeleteCliente/?ciCliente=" + num,
        success: function (res) {
            location.reload()
        },
        error: function (data) {

        }
    })
}







function generateCustomersTable(customers) {

    var customersData = [];


    customers.forEach(element => {

        var aux = [];

        aux.push(String(element.ci));
        aux.push(String(element.nombre));
        aux.push(String(element.apellido));
        aux.push(String(element.fechaNacimiento));
        aux.push(String(element.pais));
        aux.push(String(element.email));


        customersData.push(aux);
    })

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


        $('#customersTable tr').click(function () {
            var data = table.row($(this)).data();
            openUpdateCustomerModal(data[0], data[1], data[2], acomodarLaPutaFecha(data[3]), data[4], data[5]);

            $('#deleteCustomerButton').click(function () {
                DeleteCustomers(data[0])
            })
        })
    });
}

function openUpdateCustomerModal(ci, nombre, apellido, fechnac, pais, email) {
    $("#editCustomerModal").modal({
        fadeDuration: 100,
        clickClose: true,
        showClose: false,
        closeExisting: false,
        modalClass: "w-1/2 rounded-lg bg-gray-800 px-2 py-4"
    });

    $('#updateDocumento').val(ci);
    $('#updateNombre').val(nombre);
    $('#updateApellido').val(apellido);
    $('#updateFechaNacimiento').val(fechnac);
    $('#updatePais').val(pais);
    $('#updateEmail').val(email);
}