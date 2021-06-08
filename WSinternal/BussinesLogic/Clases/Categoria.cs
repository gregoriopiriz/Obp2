using CommonSolution.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesLogic.Data;

namespace BussinesLogic.Clases
{
    public class Categoria
    {
        public int numero;
        private string nombre;
        private float precio;
        private bool activo;


        #region Set y Get
        public void setNnumero(int numero)
        {
            this.numero = numero;
        }
        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }
        public void setPrecio(float precio)
        {
            this.precio = precio;
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
        public float getPrecio()
        {
            return this.precio;
        }
        public bool getActivo()
        {
            return this.activo;
        }
        #endregion
        public void mapDtoCategoriaToCategoria(DtoCategoria dto)
        {
            this.numero = ListData.currentCategoryNumber;
            this.nombre = dto.nombre;
            this.precio = dto.precio;
        }
    }
}
