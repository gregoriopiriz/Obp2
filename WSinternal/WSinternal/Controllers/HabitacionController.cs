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
    public class HabitacionController : ApiController
    {

        [HttpPost]
        [ActionName("AddHabitacion")]
        public string AddHabitacion([FromBody]DtoHabitacion nuevaHabitacion)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HHabitacion.AddHabitacion(nuevaHabitacion);
            return json.Serialize(response);
        }

        [HttpPut]
        [ActionName("UpdateHabitacion")]
        public string UpdateHabitacion([FromBody]DtoHabitacion nuevaHabitacion)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HHabitacion.ModificarHabitacion(nuevaHabitacion);
            return json.Serialize(response);
        }

        [HttpDelete]
        [ActionName("DeleteHabitacion")]
        public string DeleteHabitacion(int numHabitacion)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HHabitacion.BorrarHabitacion(numHabitacion);
            return json.Serialize(response);
        }

        [HttpGet]
        [ActionName("GetAllHabitaciones")]
        public string GetAllHabitaciones()
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            List<DtoHabitacion> colDtoHabitaciones = HHabitacion.GetHabitaciones();
            return json.Serialize(colDtoHabitaciones);
        }

        [HttpGet]
        [ActionName("GetHabitacionByNum")]
        public string GetHabitacionByNum(int numHabitacion)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoHabitacion habitacionByNum = HHabitacion.getDtoHabitacionByNum(numHabitacion);
            return json.Serialize(habitacionByNum);
        }

        [HttpGet]
        [ActionName("GetHabitacionesLibres")]
        public string GetHabitacionesLibres(string fecha1, string fecha2)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            List<DtoHabitacion> HabitacionesLibres= HHabitacion.mapColHabitacionToColDtoHabitacion(HHabitacion.GetHabitacionesLibres(DateTime.Parse(fecha1), DateTime.Parse(fecha2)));
            return json.Serialize(HabitacionesLibres);
        }

        [HttpGet]
        [ActionName("GetHabitacionesLibres")]
        public string GetHabitacionesLibres(string fecha1, string fecha2, int idReserva)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            List<DtoHabitacion> HabitacionesLibres = HHabitacion.mapColHabitacionToColDtoHabitacion(HHabitacion.GetHabitacionesLibres(DateTime.Parse(fecha1), DateTime.Parse(fecha2), idReserva));
            return json.Serialize(HabitacionesLibres);
        }

    }


}
