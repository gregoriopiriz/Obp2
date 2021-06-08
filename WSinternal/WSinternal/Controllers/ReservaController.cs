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
    public class ReservaController : ApiController
    {
        [HttpPost]
        [ActionName("AddReserva")]
        public string AddReserva([FromBody]DtoReserva nuevaReserva)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HReserva.AddReserva(nuevaReserva);
            return json.Serialize(response);
        }

        [HttpPut]
        [ActionName("UpdateReserva")]
        public string UpdateReserva([FromBody]DtoReserva nuevaReserva)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HReserva.ModificarReserva(nuevaReserva);
            return json.Serialize(response);
        }

        [HttpDelete]
        [ActionName("CancelarReserva")]
        public string CancelarReserva(int idReserva)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HReserva.CancelarReserva(idReserva);
            return json.Serialize(response);
        }

        [HttpGet]
        [ActionName("GetAllReservas")]
        public string GetAllReservas()
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            List<DtoReserva> colDtoReservas = HReserva.GetReservas();
            return json.Serialize(colDtoReservas);
        }

        [HttpGet]
        [ActionName("GetReservaByNum")]
        public string GetReservaByNum(int idReserva)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoReserva reservaById = HReserva.getDtoReservaById(idReserva);
            return json.Serialize(reservaById);
        }

        [HttpGet]
        [ActionName("GetTodayCheckins")]
        public string GetTodayCheckins()
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            int todayCheckins = HReserva.GetNroReservasEmpiezanHoy();
            return json.Serialize(todayCheckins);
        }

        [HttpGet]
        [ActionName("GetTodayCheckouts")]
        public string GetTodayCheckouts()
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            int todayCheckouts = HReserva.GetNroReservasTerminanHoy();
            return json.Serialize(todayCheckouts);
        }

        [HttpGet]
        [ActionName("GetReservasForChart")]
        public string GetReservasForChart(string fecha1, string fecha2)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            List<DtoReservaForChart> reservasForChart = HReserva.GetReservasforChart(fecha1, fecha2);
            return json.Serialize(reservasForChart);
        }

        [HttpPut]
        [ActionName("CambiarEstado")]
        public void CambiarEstado(ESTADO_RESERVA _estado, int _idReserva)
        {
            HReserva.CambiarEstado(_estado, _idReserva);
        }
        
    }
}
