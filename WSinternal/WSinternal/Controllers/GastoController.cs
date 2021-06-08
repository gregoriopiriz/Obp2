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
    public class GastoController : ApiController
    {
        [HttpPost]
        [ActionName("AddGasto")]
        public string AddGasto([FromBody]DtoGasto nuevoGasto, int idReserva)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HGasto.AddGasto(nuevoGasto, idReserva);
            return json.Serialize(response);
        }

        [HttpDelete]
        [ActionName("DeleteGasto")]
        public string DeleteGasto(int numGasto)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HGasto.BorrarGasto(numGasto);
            return json.Serialize(response);
        }

        [HttpGet]
        [ActionName("GetAllGastos")]
        public string GetAllGastos(int idReserva)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            List<DtoGasto> colDtoGastos = HGasto.GetGastos(idReserva);
            return json.Serialize(colDtoGastos);
        }


        [HttpGet]
        [ActionName("GetGastoByNum")]
        public string GetGastoByNum(int numGasto)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoGasto gastoByNum = HGasto.getDtoGastoByNum(numGasto);
            return json.Serialize(gastoByNum);
        }
    }
}
