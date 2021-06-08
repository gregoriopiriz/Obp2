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
    public class HCategoria
    {
        public static DtoRespuesta AddCategoria(DtoCategoria dto)
        {
            DtoRespuesta respuesta = new DtoRespuesta();

            respuesta = validarCategoria(respuesta, dto);

            if (respuesta.estado != ESTADO_RESPUESTA.ERROR.ToString())
            {

                Categoria nuevaCategoria = new Categoria();
                nuevaCategoria.mapDtoCategoriaToCategoria(dto);

                nuevaCategoria.setActivo(true);

                ListData.colCategorias.Add(nuevaCategoria);

                ListData.currentCategoryNumber++;

                respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
                respuesta.mensaje = "La categoria se ingreso correctamente!";
            }

            return respuesta;
        }

        private static List<DtoCategoria> mapColCategoriaToColDtoCategoria(List<Categoria> colCategorias)
        {
            List<DtoCategoria> colDtoCat = new List<DtoCategoria>();
            foreach (Categoria item in colCategorias)
            {
                if (item.getActivo() == true)
                {
                    DtoCategoria dto = new DtoCategoria();

                    dto.numero = item.getNumero();
                    dto.nombre = item.getNombre();
                    dto.precio = item.getPrecio();

                    colDtoCat.Add(dto);
                }
            }

            return colDtoCat;
        }

        private static DtoCategoria mapCategoriaToDto(Categoria categoria)
        {
            DtoCategoria dto = new DtoCategoria();
            dto.numero = categoria.getNumero();
            dto.nombre = categoria.getNombre();
            dto.precio = categoria.getPrecio();
            return dto;
        }
        public static DtoCategoria getDtoCategoriaByNum(int numCategoria)
        {
            Categoria categoriaNum = GetCategoriaByNum(numCategoria);
            DtoCategoria dto = mapCategoriaToDto(categoriaNum);
            return dto;
        }

        public static List<DtoCategoria> GetCategorias()
        {
            List<DtoCategoria> colDtoCategorias = mapColCategoriaToColDtoCategoria(ListData.colCategorias);
            return colDtoCategorias;
        }

        public static DtoRespuesta ModificarCategoria(DtoCategoria dto)
        {

            DtoRespuesta respuesta = new DtoRespuesta();

            Categoria c = GetCategoriaByNum(dto.numero);
            c.setNombre(dto.nombre);
            c.setPrecio(dto.precio);

            respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
            respuesta.mensaje = "La categoria se modifico correctamente!";

            return respuesta;
        }

        public static DtoRespuesta BorrarCategoria(int numCategoria)
        {

            DtoRespuesta respuesta = new DtoRespuesta();

            Categoria categoriaGuardada = GetCategoriaByNum(numCategoria);
            categoriaGuardada.setActivo(false);

            respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
            respuesta.mensaje = "La categoria se borro correctamente!";

            return respuesta;
        }

        public static Categoria GetCategoriaByNum(int numCategoria)
        {
            foreach (Categoria cat in ListData.colCategorias)
            {
                if (numCategoria == cat.getNumero())
                {
                    return cat;
                }
            }

            return null;
        }
        private static DtoRespuesta validarCategoria(DtoRespuesta respuesta, DtoCategoria dto)
        {
            Categoria categoria = GetCategoriaByNum(dto.numero);
            if (categoria != null)
            {
                respuesta.estado = ESTADO_RESPUESTA.ERROR.ToString();
                respuesta.mensaje = "El número de categoria ya fue ingresado";
            }

            return respuesta;
        }


        public static List<DtoCategoriaForChart> GetCategoriasforChart(string fecha1, string fecha2)
        {

            List<DtoCategoriaForChart> CategoriasforChart = new List<DtoCategoriaForChart>();

            foreach (Categoria c in ListData.colCategorias)
            {
                DtoCategoriaForChart cat = new DtoCategoriaForChart();
                cat.nombre = c.getNombre();
                cat.numCategoria = c.getNumero();
                cat.cantidad = 0;
                CategoriasforChart.Add(cat);
            }

            if (fecha1 != null && fecha2 != null)
            {
                foreach (Reserva r in ListData.colReservas)
                {
                    if (r.getFechaInicio() >= DateTime.Parse(fecha1) && r.getFechaFin() <= DateTime.Parse(fecha2))
                    {
                        foreach (Habitacion h in r.getHabitacion())
                        {
                            foreach (DtoCategoriaForChart c in CategoriasforChart)
                            {
                                if (c.numCategoria == h.getCategoria().getNumero())
                                {
                                    c.cantidad += 1;
                                }
                            }
                        }
                    }
                }

                return CategoriasforChart;
            }
            else if (fecha1 != null && fecha2 == null)
            {
                foreach (Reserva r in ListData.colReservas)
                {
                    if (r.getFechaInicio() >= DateTime.Parse(fecha1))
                    {
                        foreach (Habitacion h in r.getHabitacion())
                        {
                            foreach (DtoCategoriaForChart c in CategoriasforChart)
                            {
                                if (c.numCategoria == h.getCategoria().getNumero())
                                {
                                    c.cantidad += 1;
                                }
                            }
                        }
                    }
                }

                return CategoriasforChart;
            }
            else if (fecha1 == null && fecha2 != null)
            {
                foreach (Reserva r in ListData.colReservas)
                {
                    if (r.getFechaFin() <= DateTime.Parse(fecha2))
                    {
                        foreach (Habitacion h in r.getHabitacion())
                        {
                            foreach (DtoCategoriaForChart c in CategoriasforChart)
                            {
                                if (c.numCategoria == h.getCategoria().getNumero())
                                {
                                    c.cantidad += 1;
                                }
                            }
                        }
                    }
                }

                return CategoriasforChart;
            }
            else
            {
                foreach (Reserva r in ListData.colReservas)
                {
                    foreach (Habitacion h in r.getHabitacion())
                    {
                        foreach (DtoCategoriaForChart c in CategoriasforChart)
                        {
                            if (c.numCategoria == h.getCategoria().getNumero())
                            {
                                c.cantidad += 1;
                            }
                        }
                    }
                }
            }

            return CategoriasforChart;
        }
    }
}
