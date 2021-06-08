using BussinesLogic.Clases;
using BussinesLogic.Helpers;
using CommonSolution.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;


namespace WsInternal.Controllers
{
    public class ComodidadController : ApiController
    {
        [HttpPost]
        [ActionName("AddComodidad")]
        public string AddComodidad([FromBody]DtoComodidad nuevaComodidad)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HComodidad.AddComodidad(nuevaComodidad);
            return json.Serialize(response);
        }

        [HttpPut]
        [ActionName("UpdateComodidad")]
        public string UpdateComodidad([FromBody]DtoComodidad nuevaComodidad)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HComodidad.ModificarComodidad(nuevaComodidad);
            return json.Serialize(response);
        }

        [HttpDelete]
        [ActionName("DeleteComodidad")]
        public string DeleteComodidad(int numComodidad)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HComodidad.BorrarComodidad(numComodidad);
            return json.Serialize(response);
        }

        [HttpGet]
        [ActionName("GetAllComodidades")]
        public string GetAllComodidades()
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            List<DtoComodidad> colDtoComodidades = HComodidad.GetComodidades();
            return json.Serialize(colDtoComodidades);
        }

        [HttpGet]
        [ActionName("GetComodidadByNum")]
        public string GetHabitacionByNum(int numComodidad)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoComodidad comodidadByNum = HComodidad.getDtoComodidadByNum(numComodidad);
            return json.Serialize(comodidadByNum);
        }

    }
}
