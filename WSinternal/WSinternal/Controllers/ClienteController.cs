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
    public class ClienteController : ApiController
    {
        [HttpPost]
        [ActionName("AddCliente")]
        public string AddCliente([FromBody]DtoCliente nuevoCliente)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HCliente.AddCliente(nuevoCliente);
            return json.Serialize(response);
        }

        [HttpPut]
        [ActionName("UpdateCliente")]
        public string UpdateCliente([FromBody]DtoCliente nuevoCliente)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HCliente.ModificarCliente(nuevoCliente);
            return json.Serialize(response);
        }

        [HttpDelete]
        [ActionName("DeleteCliente")]
        public string DeleteCliente(string ciCliente)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoRespuesta response = HCliente.BorrarCliente(ciCliente);
            return json.Serialize(response);
        }

        [HttpGet]
        [ActionName("GetAllClientes")]
        public string GetAllClientes()
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            List<DtoCliente> colDtoCliente = HCliente.GetClientes();
            return json.Serialize(colDtoCliente);
        }

        [HttpGet]
        [ActionName("GetClienteByCi")]
        public string GetClienteByCi(string ciCliente)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            DtoCliente clienteByCi = HCliente.getDtoClienteByNum(ciCliente);
            return json.Serialize(clienteByCi);
        }

        [HttpGet]
        [ActionName("GetEmailClienteByCi")]
        public string GetEmailClienteByCi(string ciCliente)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            string email = HCliente.getEmailCliente(ciCliente);
            return json.Serialize(email);
        }
    }
}
