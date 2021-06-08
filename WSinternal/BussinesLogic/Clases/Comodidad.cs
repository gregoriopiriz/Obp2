using BussinesLogic.Data;
using CommonSolution.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Clases
{
    public class Comodidad
    {
        private int numero;
        private string nombre;
        private string descripcion;
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
        public void setDescripcion(string descripcion)
        {
            this.descripcion = descripcion;
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
        public string getDescripcion()
        {
            return this.descripcion;
        }
        public bool getActivo()
        {
            return this.activo;
        }
        #endregion
        public void mapDtoComodidadToComodidad(DtoComodidad dto)
        {
            this.numero = ListData.currentConfortNumber;
            this.nombre = dto.nombre;
            this.descripcion = dto.descripcion;
        }
    }
}
