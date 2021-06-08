using CommonSolution.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Clases
{
    public class Gasto
    {
        private int numero;
        private string nombre;
        private string observacion;
        private DateTime fecha;
        private float precioTotal;
        private bool activo;

        #region Set y Get
        public void setNumero(int numero)
        {
            this.numero = numero;
        }
        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }
        public void setObservacion(string observacion)
        {
            this.observacion = observacion;
        }
        public void setFecha(DateTime fecha)
        {
            this.fecha = fecha;
        }
        public void setPrecioTotal(float precio)
        {
            this.precioTotal = precio;
        }
        public void setActivo(bool activo)
        {
            this.activo = activo;
        }

        public int getNumero()
        {
            return this.numero;
        }
        public string getNombre()
        {
            return this.nombre;
        }
        public string getObservacion()
        {
            return this.observacion;
        }
        public DateTime getFecha()
        {
            return this.fecha;
        }
        public float getPrecioTotal()
        {
            return this.precioTotal;
        }
        public bool getActivo()
        {
            return this.activo;
        }
        #endregion
        public void mapDtoGastoToGasto(DtoGasto dto)
        {
            this.numero = dto.numero;
            this.nombre = dto.nombre;
            this.observacion = dto.observacion;
            this.fecha = dto.fecha;
            this.precioTotal = dto.precioTotal;
        }
    }
}
