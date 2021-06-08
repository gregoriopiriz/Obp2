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
    public class HGasto
    {
        public static DtoRespuesta AddGasto(DtoGasto dto, int idReserva)
        {
            DtoRespuesta respuesta = new DtoRespuesta();

            respuesta = validarGasto(respuesta, dto);

            Reserva res = HReserva.GetReservaXid(idReserva);

            List<int> numGastos = new List<int>();

            if (respuesta.estado != ESTADO_RESPUESTA.ERROR.ToString())
            {

                Gasto nuevoGasto = new Gasto();
                nuevoGasto.mapDtoGastoToGasto(dto);

                nuevoGasto.setActivo(true);
                nuevoGasto.setNumero(ListData.currentGastoNumber);
                ListData.colGastos.Add(nuevoGasto);
                numGastos.Add(nuevoGasto.getNumero());

                respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
                respuesta.mensaje = "El gasto se ingreso correctamente!";
            }

            res.setGasto(numGastos);

            return respuesta;
        }
        public static DtoRespuesta BorrarGasto(int numGasto)
        {

            DtoRespuesta respuesta = new DtoRespuesta();

            Gasto gastoGuardado = getGastosByNum(numGasto);
            gastoGuardado.setActivo(false);

            respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
            respuesta.mensaje = "El gasto se borro correctamente!";

            return respuesta;
        }

        private static List<DtoGasto> mapColGastoToColDtoGasto(List<Gasto> colGastos)
        {
            List<DtoGasto> colDtoGas = new List<DtoGasto>();
            foreach (Gasto item in colGastos)
            {
                if (item.getActivo() == true)
                {
                    DtoGasto dto = new DtoGasto();

                    dto.numero = item.getNumero();
                    dto.nombre = item.getNombre();
                    dto.observacion = item.getObservacion();
                    dto.fecha = item.getFecha();
                    dto.precioTotal = item.getPrecioTotal();

                    colDtoGas.Add(dto);
                }
            }

            return colDtoGas;
        }

        public static List<DtoGasto> GetGastos(int idReserva)
        {
            List<int> numGastos = HReserva.GetReservaXid(idReserva).getGasto();

            List<Gasto> gastos = HGasto.getGastosByListNum(numGastos);

            List<DtoGasto> colDtoGastos = mapColGastoToColDtoGasto(gastos);

            return colDtoGastos;
        }

        public static List<Gasto> getGastosByListNum(List<int> colNumGastos)
        {
            List<Gasto> colGastos = new List<Gasto>();
            foreach (int gastos in colNumGastos)
            {
                Gasto gasto = getGastosByNum(gastos);
                if (gasto != null)
                {
                    colGastos.Add(gasto);
                }
            }
            return colGastos;
        }

        public static Gasto getGastosByNum(int numGasto)
        {
            foreach (Gasto item in ListData.colGastos)
            {
                if (item.getNumero() == numGasto && item.getActivo() == true)
                {
                    return item;
                }
            }
            return null;
        }
        private static DtoRespuesta validarGasto(DtoRespuesta respuesta, DtoGasto dto)
        {
            Gasto gasto = getGastosByNum(dto.numero);
            if (gasto != null)
            {
                respuesta.estado = ESTADO_RESPUESTA.ERROR.ToString();
                respuesta.mensaje = "El número de gasto ya fue ingresado";
            }

            return respuesta;
        }

        public static DtoGasto getDtoGastoByNum(int numGasto)
        {
            Gasto gastoNum = getGastosByNum(numGasto);
            DtoGasto dto = mapGastoToDto(gastoNum);
            return dto;
        }

        private static DtoGasto mapGastoToDto(Gasto gasto)
        {
            DtoGasto dto = new DtoGasto();
            dto.numero = gasto.getNumero();
            dto.nombre = gasto.getNombre();
            dto.observacion = gasto.getObservacion();
            dto.fecha = gasto.getFecha();
            dto.precioTotal = gasto.getPrecioTotal();

            return dto;
        }
    }
}
