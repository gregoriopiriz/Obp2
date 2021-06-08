using CommonSolution.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinesLogic.Helpers;

namespace BussinesLogic.Clases
{
    public class Habitacion
    {
        private tamano_CAMA tamanoCama;
        private int numero;
        private int piso;
        private string descripcion;
        private float tamano;
        private bool activo;
        private Categoria categoria;
        private List<Comodidad> comodidades;


        #region Set y Get
        public void settamanoCama(tamano_CAMA tamano)
        {
            this.tamanoCama = tamano;
        }
        public void setNumero(int numero)
        {
            this.numero = numero;
        }
        public void setPiso(int piso)
        {
            this.piso = piso;
        }
        public void setDescripcion(string descripcion)
        {
            this.descripcion = descripcion;
        }
        public void settamano(float tamano)
        {
            this.tamano = tamano;
        }
        public void setcategoria(Categoria categoria)
        {
            this.categoria = categoria;
        }
        public void setcomodidades(List<Comodidad> comodidad)
        {
            this.comodidades = comodidad;
        }
        public void setActivo(bool activo)
        {
            this.activo = activo;
        }

        public tamano_CAMA gettamanoCama()
        {
            return this.tamanoCama;
        }
        public int getNumero()
        {
            return this.numero;
        }
        public int getPiso()
        {
            return this.piso;
        }
        public string getDescripcion()
        {
            return this.descripcion;
        }
        public float gettamano()
        {
            return this.tamano;
        }
        public bool getActivo()
        {
            return this.activo;
        }
        public Categoria getCategoria()
        {
            return this.categoria;
        }
        public List<Comodidad> getComodidades()
        {
            return this.comodidades;
        }
        #endregion
        public void mapDtoHabitacionToHabitacion(DtoHabitacion dto)
        {
            List<Comodidad> com = new List<Comodidad>();
            foreach (int nc in dto.comodidades)
            {
                com.Add(HComodidad.GetComodidadByNum(nc));
            }

            this.tamanoCama = dto.tamanoCama;
            this.numero = dto.numero;
            this.piso = dto.piso;
            this.descripcion = dto.descripcion;
            this.tamano = dto.tamano;
            this.categoria = HCategoria.GetCategoriaByNum(dto.numCategoria);
            this.comodidades = com;
        }

    }
}
