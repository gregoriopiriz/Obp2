using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonSolution.DTO
{
    public enum ESTADO_RESPUESTA { OK, ERROR };
    public class DtoRespuesta
    {
        public string estado;

        public string mensaje;
    }
}
