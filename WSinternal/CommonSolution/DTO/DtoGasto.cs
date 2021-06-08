using System;

namespace CommonSolution.DTO
{
    public class DtoGasto
    {
        public int numero;
        public string nombre;
        public string observacion;
        public DateTime fecha = DateTime.Today;
        public float precioTotal;
    }
}
