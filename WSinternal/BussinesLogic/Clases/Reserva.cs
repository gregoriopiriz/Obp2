using BussinesLogic.Helpers;
using CommonSolution.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Clases
{
    public class Reserva
    {
        private int idReserva;
        private ESTADO_RESERVA estadoReserva;
        private float precioTotal;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private bool descuento;
        private bool activo;
        private Cliente cliente;
        private List<int> numGastos = new List<int>();
        private List<Habitacion> habitacion;
        private bool clienteRegistrado;


        #region Set y Get

        public void setIdReserva(int id)
        {
            this.idReserva = id;
        }
        public void setEstadoReserva(ESTADO_RESERVA estado)
        {
            this.estadoReserva = estado;
        }
        public void setPrecioTotal(float precio)
        {
            this.precioTotal = precio;
        }
        public void setFechaInicio(DateTime fecha)
        {
            this.fechaInicio = fecha;
        }
        public void setFechaFin(DateTime fecha)
        {
            this.fechaFin = fecha;
        }
        public void setDescuento(bool descuento)
        {
            this.descuento = descuento;
        }
        public void setActivo(bool activo)
        {
            this.activo = activo;
        }
        public void setCliente(Cliente cliente)
        {
            this.cliente = cliente;
        }
        public void setGasto(List<int> gasto)
        {
            this.numGastos = gasto;
        }
        public void setHabitacion(List<Habitacion> habitacion)
        {
            this.habitacion = habitacion;
        }
        public void setClienteRegistrado(bool isRegistrado)
        {
            this.clienteRegistrado = isRegistrado;
        }

        public int getIdReserva()
        {
            return this.idReserva;
        }
        public ESTADO_RESERVA getESTADO_RESERVA()
        {
            return this.estadoReserva;
        }
        public float getPrecioTotal()
        {
            return this.precioTotal;
        }
        public DateTime getFechaInicio()
        {
            return this.fechaInicio;
        }
        public DateTime getFechaFin()
        {
            return this.fechaFin;
        }
        public bool getDescuento()
        {
            return this.descuento;
        }
        public bool getActivo()
        {
            return this.activo;
        }
        public Cliente getCliente()
        {
            return this.cliente;
        }
        public List<int> getGasto()
        {
            return this.numGastos;
        }
        public List<Habitacion> getHabitacion()
        {
            return this.habitacion;
        }

        public bool getClienteRegistrado()
        {
            return this.clienteRegistrado;
        }

        #endregion
        public void mapDtoReservaToReserva(DtoReserva dto)
        {
            this.idReserva = dto.idReserva;
            this.estadoReserva = dto.estadoReserva;
            this.precioTotal = dto.precioTotal;
            this.fechaInicio = DateTime.Parse(dto.fechaInicio);
            this.fechaFin = DateTime.Parse(dto.fechaFin);
            this.descuento = dto.descuento;
            this.cliente = HCliente.GetClienteXci(dto.ciCliente);
            this.habitacion = HHabitacion.FromNumsToHabs(dto.numHabitacion);
            this.clienteRegistrado = dto.clienteRegistrado;
        }
    }
}
