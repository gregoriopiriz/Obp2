using BussinesLogic.Clases;
using BussinesLogic.Data;
using CommonSolution.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Helpers
{
    public class HCliente
    {
        public static DtoRespuesta AddCliente(DtoCliente dto)
        {
            DtoRespuesta respuesta = new DtoRespuesta();

            respuesta = validarCliente(respuesta, dto);

            if (respuesta.estado != ESTADO_RESPUESTA.ERROR.ToString())
            {

                Cliente nuevoCliente = new Cliente();
                nuevoCliente.mapDtoClienteToCliente(dto);

                nuevoCliente.setActivo(true);
                ListData.colClientes.Add(nuevoCliente);
                
                Hemail.Email("Le agradecemos por haberse registrado como Cliente, para futuras reservas puede utilizar su numero de Ci o Pasaporte para rellenar sus datos personales", nuevoCliente.getEmail());

                respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
                respuesta.mensaje = "El cliente se ingreso correctamente!";

            }

            return respuesta;
        }

        public static DtoCliente mapClienteToDtoCliente(Cliente cliente)
        {
            DtoCliente dto = new DtoCliente();

            dto.nombre = cliente.getNombre();
            dto.apellido = cliente.getApellido();
            dto.ci = cliente.getCi();
            dto.pais = cliente.getPais();
            dto.fechaNacimiento = cliente.getFechaNacimiento().ToString("dd/MM/yyyy");
            dto.email = cliente.getEmail();

            return dto;
        }


        private static List<DtoCliente> mapDtoClienteToColDtoCliente(List<Cliente> colClientes)
        {
            List<DtoCliente> colDtoCli = new List<DtoCliente>();
            foreach (Cliente item in colClientes)
            {
                if (item.getActivo() == true)
                {

                    DtoCliente dto = new DtoCliente();

                    dto.nombre = item.getNombre();
                    dto.apellido = item.getApellido();
                    dto.ci = item.getCi();
                    dto.pais = item.getPais();
                    dto.fechaNacimiento = item.getFechaNacimiento().ToString("dd/MM/yyyy");
                    dto.email = item.getEmail();

                    colDtoCli.Add(dto);
                }
            }

            return colDtoCli;
        }

        public static List<DtoCliente> GetClientes()
        {
            List<DtoCliente> colDtoClientes = mapDtoClienteToColDtoCliente(ListData.colClientes);
            return colDtoClientes;
        }

        public static Cliente mapDtoClienteToCliente(DtoCliente dto)
        {
            Cliente cli = new Cliente();

            cli.setNombre(dto.nombre);
            cli.setApellido(dto.apellido);
            cli.setCi(dto.ci);
            cli.setPais(dto.pais);
            cli.setFechaNacimiento(DateTime.Parse(dto.fechaNacimiento));
            cli.setEmail(dto.email);

            return cli;
        }

        private static DtoRespuesta validarCliente(DtoRespuesta respuesta, DtoCliente dto)
        {
            Cliente cliente = GetClienteXci(dto.ci);
            if (cliente != null)
            {
                respuesta.estado = ESTADO_RESPUESTA.ERROR.ToString();
                respuesta.mensaje = "El cliente ya fue ingresado";
            }

            return respuesta;
        }

        public static Cliente GetClienteXci(string ci)
        {
            foreach (Cliente item in ListData.colClientes)
            {
                if (item.getCi() == ci)
                {
                    return item;
                }
            }
            return null;
        }

        public static DtoRespuesta ModificarCliente(DtoCliente dto)
        {
            DtoRespuesta respuesta = new DtoRespuesta();

            Cliente c = GetClienteXci(dto.ci);
            if (dto.nombre != null)
            {
                c.setNombre(dto.nombre);
            }
            if (dto.apellido != null)
            {
                c.setApellido(dto.apellido);
            }
            if (dto.ci != null)
            {
                c.setCi(dto.ci);
            }
            if (dto.pais != null)
            {
                c.setPais(dto.pais);
            }
            if (dto.fechaNacimiento != null)
            {
                c.setFechaNacimiento(DateTime.Parse(dto.fechaNacimiento));
            }
            if (dto.email != null)
            {
                c.setEmail(dto.email);
            }

            respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
            respuesta.mensaje = "El cliente se modifico correctamente!";

            return respuesta;
        }

        public static DtoRespuesta BorrarCliente(string numCli)
        {

            DtoRespuesta respuesta = new DtoRespuesta();

            Cliente clienteGuardado = GetClienteXci(numCli);
            clienteGuardado.setActivo(false);

            respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
            respuesta.mensaje = "El cliente se borro correctamente!";

            return respuesta;
        }

        public static DtoCliente getDtoClienteByNum(string numCli)
        {
            Cliente clienteNum = GetClienteXci(numCli);
            DtoCliente dto = mapClienteToDtoCliente(clienteNum);
            return dto;
        }

        public static string getEmailCliente(string numCli)
        {
            Cliente clienteNum = GetClienteXci(numCli);
            string email = clienteNum.getEmail();
            return email;
        }
    }
}
