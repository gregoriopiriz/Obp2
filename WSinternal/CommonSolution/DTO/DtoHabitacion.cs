using System.Collections.Generic;

namespace CommonSolution.DTO
{
    public enum tamano_CAMA { TWIN, QUEEN, KING };
    public class DtoHabitacion
    {
        public tamano_CAMA tamanoCama;
        public int numero;
        public int piso;
        public string descripcion;
        public float tamano;
        public int numCategoria;
        public string categoria;
        public List<int> comodidades;
    }
}
