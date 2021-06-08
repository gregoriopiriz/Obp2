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
    public class HComodidad
    {

        public static DtoRespuesta AddComodidad(DtoComodidad dto)
        {
            DtoRespuesta respuesta = new DtoRespuesta();

            respuesta = validarComodidad(respuesta, dto);

            if (respuesta.estado != ESTADO_RESPUESTA.ERROR.ToString())
            {

                Comodidad nuevaComodidad = new Comodidad();
                nuevaComodidad.mapDtoComodidadToComodidad(dto);

                nuevaComodidad.setActivo(true);
                ListData.colComodidades.Add(nuevaComodidad);

                ListData.currentConfortNumber++;

                respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
                respuesta.mensaje = "La comodidad se ingreso correctamente!";
            }

            return respuesta;
        }

        public static List<DtoComodidad> mapDtoComodidadToColDtoComodidad(List<Comodidad> colComodidades)
        {
            List<DtoComodidad> colDtoComo = new List<DtoComodidad>();
            foreach (Comodidad item in colComodidades)
            {
                if (item.getActivo() == true)
                {

                    DtoComodidad dto = new DtoComodidad();

                    dto.numero = item.getNumero();
                    dto.nombre = item.getNombre();
                    dto.descripcion = item.getDescripcion();

                    colDtoComo.Add(dto);
                }
            }

            return colDtoComo;
        }

        public static List<Comodidad> mapColDtoComodidadToColComodidad(List<DtoComodidad> colDtoComodidades)
        {
            List<Comodidad> colComo = new List<Comodidad>();
            foreach (DtoComodidad item in colDtoComodidades)
            {
                Comodidad c = new Comodidad();

                c.setNumero(item.numero);
                c.setNombre(item.nombre);
                c.setDescripcion(item.descripcion);

                colComo.Add(c);
            }

            return colComo;
        }

        public static List<DtoComodidad> GetComodidades()
        {
            List<DtoComodidad> colDtoComodidades = mapDtoComodidadToColDtoComodidad(ListData.colComodidades);
            return colDtoComodidades;
        }

        public static DtoRespuesta ModificarComodidad(DtoComodidad dto)
        {

            DtoRespuesta respuesta = new DtoRespuesta();

            Comodidad c = GetComodidadByNum(dto.numero);
            if (dto.nombre != null)
            {
                c.setNombre(dto.nombre);
            }
            if (dto.descripcion != null)
            {
                c.setDescripcion(dto.descripcion);
            }

            respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
            respuesta.mensaje = "La comodidad se modifico correctamente!";

            return respuesta;
        }

        public static DtoRespuesta BorrarComodidad(int numComodidad)
        {
            DtoRespuesta respuesta = new DtoRespuesta();

            Comodidad comodidadGuardad = GetComodidadByNum(numComodidad);
            comodidadGuardad.setActivo(false);

            respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
            respuesta.mensaje = "La comodidad se borro correctamente!";

            return respuesta;
        }

        public static Comodidad GetComodidadByNum(int numComodidad)
        {
            foreach (Comodidad com in ListData.colComodidades)
            {
                if (numComodidad == com.getNumero())
                {
                    return com;
                }
            }

            return null;
        }

        public static List<Comodidad> getComodidadesByListNum(List<int> colNumComodidades)
        {
            List<Comodidad> colComodidades = new List<Comodidad>();
            foreach (int comodidades in colNumComodidades)
            {
                Comodidad comodidad = GetComodidadByNum(comodidades);
                if (comodidad != null)
                {
                    colComodidades.Add(comodidad);
                }
            }
            return colComodidades;
        }

        public static DtoComodidad getDtoComodidadByNum(int numComodidad)
        {
            Comodidad comodidadNum = GetComodidadByNum(numComodidad);
            DtoComodidad dto = mapComodidadToDto(comodidadNum);
            return dto;
        }

        private static DtoComodidad mapComodidadToDto(Comodidad comodidad)
        {
            DtoComodidad dto = new DtoComodidad();
            dto.numero = comodidad.getNumero();
            dto.descripcion = comodidad.getDescripcion();

            return dto;
        }

        private static DtoRespuesta validarComodidad(DtoRespuesta respuesta, DtoComodidad dto)
        {
            Comodidad comodidad = GetComodidadByNum(dto.numero);
            if (comodidad != null)
            {
                respuesta.estado = ESTADO_RESPUESTA.ERROR.ToString();
                respuesta.mensaje = "El número de comodidad ya fue ingresado";
            }

            return respuesta;
        }

        public static List<int> FromComodidadesToNums(List<Comodidad> comodidades)
        {
            List<int> colNumsComodidades = new List<int>();

            foreach (Comodidad item in comodidades)
            {
                colNumsComodidades.Add(item.getNumero());
            }

            return colNumsComodidades;
        }
    }
}
