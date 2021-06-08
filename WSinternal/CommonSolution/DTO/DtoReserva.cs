using System;
using System.Collections.Generic;

namespace CommonSolution.DTO
{
    public enum ESTADO_RESERVA { NUEVA, INGRESO, CANCELADA, FINALIZADA };
    public class DtoReserva
    {
        public int idReserva;
        public ESTADO_RESERVA estadoReserva;
        public float precioTotal;
        public string fechaInicio;
        public string fechaFin;
        public bool descuento;
        public List<int> numgasto;
        public List<int> numHabitacion;
        public bool clienteRegistrado;
        public string ciCliente;
        public string nombreCliente;
        public string apellidoCliente;
    }
}
