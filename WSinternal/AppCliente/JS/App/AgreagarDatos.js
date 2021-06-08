async function sendRequest(toAddElement, url) {
    await $.ajax({
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify(toAddElement),
        url: url,
        success: function (res) {

        },
        error: function (data) {

        }
    });
}


async function AgregarCategorias() {


    var DatosCategoria = [];

    DatosCategoria.push({ nombre: 'Standar', precio: 120 });//1
    DatosCategoria.push({ nombre: 'Deluxe', precio: 400 });//2
    DatosCategoria.push({ nombre: 'Ultra Deluxe', precio: 640 });//3
    DatosCategoria.push({ nombre: 'Presidencial', precio: 900 });//4

    await sendRequest(DatosCategoria[0], "https://localhost:44310/api/Categoria/AddCategoria");
    await sendRequest(DatosCategoria[1], "https://localhost:44310/api/Categoria/AddCategoria");
    await sendRequest(DatosCategoria[2], "https://localhost:44310/api/Categoria/AddCategoria");
    await sendRequest(DatosCategoria[3], "https://localhost:44310/api/Categoria/AddCategoria");



}
async function AgregarComodidades() {

    var DatosComodidades = [];

    DatosComodidades.push({ nombre: 'Jacuzzi', descripcion: 'La habitacion cuenta con un jacuzzi en la terraza' });//1
    DatosComodidades.push({ nombre: 'Bañera', descripcion: 'La habitacion cuenta con una bañera' });//2
    DatosComodidades.push({ nombre: 'WiFi', descripcion: 'La habitacion cuenta con una red WiFi de 60mbps' });//3
    DatosComodidades.push({ nombre: 'Aire Acondicionado', descripcion: 'La habitacion cuenta con un aire acondicionado' });//4
    DatosComodidades.push({ nombre: 'TV', descripcion: 'La habitacion cuenta con una televisión' });//5
    DatosComodidades.push({ nombre: 'Smart TV', descripcion: 'La habitacion cuenta con un Smart Tv' });//6
    DatosComodidades.push({ nombre: 'Frigobar', descripcion: 'La habitacion cuenta con un frigobar' });//7

    await sendRequest(DatosComodidades[0], "https://localhost:44310/api/Comodidad/AddComodidad");
    await sendRequest(DatosComodidades[1], "https://localhost:44310/api/Comodidad/AddComodidad");
    await sendRequest(DatosComodidades[2], "https://localhost:44310/api/Comodidad/AddComodidad");
    await sendRequest(DatosComodidades[3], "https://localhost:44310/api/Comodidad/AddComodidad");
    await sendRequest(DatosComodidades[4], "https://localhost:44310/api/Comodidad/AddComodidad");
    await sendRequest(DatosComodidades[5], "https://localhost:44310/api/Comodidad/AddComodidad");
    await sendRequest(DatosComodidades[6], "https://localhost:44310/api/Comodidad/AddComodidad");



}

async function AgregarHabitaciones() {

    var DatosHabitaciones = [];

    DatosHabitaciones.push({ numero: '102', piso: '1', tamano: '22', numCategoria: '1', tamanoCama: 'TWIN', descripcion: 'Pequeña Habitacion', comodidades: [3, 5] });
    DatosHabitaciones.push({ numero: '309', piso: '3', tamano: '34', numCategoria: '2', tamanoCama: 'QUEEN', descripcion: 'Pequeña Habitacion con vista al patio interior', comodidades: [2, 3, 4, 5] });
    DatosHabitaciones.push({ numero: '502', piso: '5', tamano: '56', numCategoria: '3', tamanoCama: 'KING', descripcion: 'Pequeña Habitacion con vista al mar', comodidades: [1, 2, 3, 4, 6, 7] });
    DatosHabitaciones.push({ numero: '203', piso: '2', tamano: '22', numCategoria: '1', tamanoCama: 'TWIN', descripcion: 'Pequeña Habitacion con vista al mar', comodidades: [3, 5] });
    DatosHabitaciones.push({ numero: '120', piso: '1', tamano: '22', numCategoria: '1', tamanoCama: 'TWIN', descripcion: 'Pequeña Habitacion con vista al mar', comodidades: [3, 5] });
    DatosHabitaciones.push({ numero: '120', piso: '1', tamano: '99', numCategoria: '4', tamanoCama: 'KING', descripcion: 'Pequeña Habitacion con vista al mar', comodidades: [1, 2, 3, 4, 6, 7] });



    await sendRequest(DatosHabitaciones[0], "https://localhost:44310/api/Habitacion/AddHabitacion");
    await sendRequest(DatosHabitaciones[1], "https://localhost:44310/api/Habitacion/AddHabitacion");
    await sendRequest(DatosHabitaciones[2], "https://localhost:44310/api/Habitacion/AddHabitacion");
    await sendRequest(DatosHabitaciones[3], "https://localhost:44310/api/Habitacion/AddHabitacion");
    await sendRequest(DatosHabitaciones[4], "https://localhost:44310/api/Habitacion/AddHabitacion");

}

async function AgregarClientes() {

    var DatosClientes = [];

    DatosClientes.push({ ci: '52320270', nombre: 'Alejandro', apellido: 'Migues', pais: 'Uruguay', fechaNacimiento: '12-11-2000', email: 'alejandromigues2@gmail.com' });
    DatosClientes.push({ ci: '47180588', nombre: 'Gregorio', apellido: 'Piriz', pais: 'Uruguay', fechaNacimiento: '19-12-1994', email: 'gregorio.piriz@hotmail.com' });
    DatosClientes.push({ ci: '45626748', nombre: 'Pepe', apellido: 'Gonzales', pais: 'Venezuela', fechaNacimiento: '22-11-1998', email: 'pepeAtr@yahoo.com' });


    await sendRequest(DatosClientes[0], "https://localhost:44310/api/Cliente/AddCliente");
    await sendRequest(DatosClientes[1], "https://localhost:44310/api/Cliente/AddCliente");
    await sendRequest(DatosClientes[2], "https://localhost:44310/api/Cliente/AddCliente");


}

async function AgregarClientesIngles() {

    var DatosClientes = [];

    DatosClientes.push({ ci: '52320270', nombre: 'Alejandro', apellido: 'Migues', pais: 'Uruguay', fechaNacimiento: '11-12-2000', email: 'alejandromigues2@gmail.com' });
    DatosClientes.push({ ci: '47180588', nombre: 'Gregorio', apellido: 'Piriz', pais: 'Uruguay', fechaNacimiento: '12-19-1994', email: 'gregorio.piriz@hotmail.com' });
    DatosClientes.push({ ci: '45626748', nombre: 'Pepe', apellido: 'Gonzales', pais: 'Venezuela', fechaNacimiento: '11-22-1998', email: 'pepeAtr@yahoo.com' });


    await sendRequest(DatosClientes[0], "https://localhost:44310/api/Cliente/AddCliente");
    await sendRequest(DatosClientes[1], "https://localhost:44310/api/Cliente/AddCliente");
    await sendRequest(DatosClientes[2], "https://localhost:44310/api/Cliente/AddCliente");


}

async function AgregarReservas() {

    var DatosReservas = [];


    DatosReservas.push({ fechaInicio: '12/12/2019', fechaFin: '15/12/2019', numHabitacion: [102, 309], ciCliente: '52320270', clienteRegistrado: 'true' });
    DatosReservas.push({ fechaInicio: '13/12/2019', fechaFin: '17/12/2019', numHabitacion: [502], ciCliente: '47180588', clienteRegistrado: 'true' });
    DatosReservas.push({ fechaInicio: '14/12/2019', fechaFin: '31/12/2019', numHabitacion: [203], ciCliente: '45626748', clienteRegistrado: 'true' });
    DatosReservas.push({ fechaInicio: '06/12/2019', fechaFin: '12/12/2019', numHabitacion: [309], ciCliente: '45626748', clienteRegistrado: 'true' });
    DatosReservas.push({ fechaInicio: '12/12/2019', fechaFin: '08/01/2020', numHabitacion: [120], ciCliente: '45626748', clienteRegistrado: 'true' });


    await sendRequest(DatosReservas[0], "https://localhost:44310/api/Reserva/AddReserva");
    await sendRequest(DatosReservas[1], "https://localhost:44310/api/Reserva/AddReserva");
    await sendRequest(DatosReservas[2], "https://localhost:44310/api/Reserva/AddReserva");

}

async function AgregarReservasIngles() {

    var DatosReservas = [];


    DatosReservas.push({ fechaInicio: '12/11/2019', fechaFin: '12/16/2019', numHabitacion: [102, 309], ciCliente: '52320270', clienteRegistrado: 'true' });
    DatosReservas.push({ fechaInicio: '12/11/2019', fechaFin: '12/14/2019', numHabitacion: [502], ciCliente: '47180588', clienteRegistrado: 'true' });
    DatosReservas.push({ fechaInicio: '12/09/2019', fechaFin: '12/13/2019', numHabitacion: [203], ciCliente: '45626748', clienteRegistrado: 'true' });




    await sendRequest(DatosReservas[0], "https://localhost:44310/api/Reserva/AddReserva");
    await sendRequest(DatosReservas[1], "https://localhost:44310/api/Reserva/AddReserva");
    await sendRequest(DatosReservas[2], "https://localhost:44310/api/Reserva/AddReserva");

}