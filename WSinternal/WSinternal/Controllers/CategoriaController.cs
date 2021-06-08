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
    public class CategoriaController : ApiController
    {
        [HttpPost]
        [ActionName("AddCategoria")]
        public string AddCategoria([FromBody]DtoCategoria nuevaCategoria)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HCategoria.AddCategoria(nuevaCategoria);
            return json.Serialize(response);
        }

        [HttpPut]
        [ActionName("UpdateCategoria")]
        public string UpdateCategoria([FromBody]DtoCategoria nuevaCategoria)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HCategoria.ModificarCategoria(nuevaCategoria);
            return json.Serialize(response);
        }

        [HttpDelete]
        [ActionName("DeleteCategoria")]
        public string DeleteCategoria(int numCategoria)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HCategoria.BorrarCategoria(numCategoria);
            return json.Serialize(response);
        }

        [HttpGet]
        [ActionName("GetAllCategorias")]
        public string GetAllCategorias()
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            List<DtoCategoria> colDtoCategorias = HCategoria.GetCategorias();
            return json.Serialize(colDtoCategorias);
        }

        [HttpGet]
        [ActionName("GetCategoriaByNum")]
        public string GetCategoriaByNum(int numCategoria)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoCategoria categoriaByNum = HCategoria.getDtoCategoriaByNum(numCategoria);
            return json.Serialize(categoriaByNum);
        }

        [HttpGet]
        [ActionName("GetCategoriasForChart")]
        public string GetCategoriasForChart(string fecha1, string fecha2)
         {
            JavaScriptSerializer json = new JavaScriptSerializer();
            List<DtoCategoriaForChart> categoriasForChart = HCategoria.GetCategoriasforChart(fecha1, fecha2);
            return json.Serialize(categoriasForChart);
        }

    }
}
