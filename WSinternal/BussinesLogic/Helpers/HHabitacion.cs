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
    public class HHabitacion
    {
        public static DtoRespuesta AddHabitacion(DtoHabitacion dto)
        {
            DtoRespuesta respuesta = new DtoRespuesta();

            respuesta = validarHabitacion(respuesta, dto);

            if (respuesta.estado != ESTADO_RESPUESTA.ERROR.ToString())
            {

                Habitacion nuevaHabitacion = new Habitacion();
                nuevaHabitacion.mapDtoHabitacionToHabitacion(dto);

                List<Comodidad> colComodidadesHabitacion = HComodidad.getComodidadesByListNum(dto.comodidades);
                nuevaHabitacion.setcomodidades(colComodidadesHabitacion);

                nuevaHabitacion.setActivo(true);
                ListData.colHabitaciones.Add(nuevaHabitacion);

                respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
                respuesta.mensaje = "La habitación se ingreso correctamente!";
            }

            return respuesta;
        }

        public static List<DtoHabitacion> mapColHabitacionToColDtoHabitacion(List<Habitacion> colHabitaciones)
        {
            List<DtoHabitacion> colDtoHabi = new List<DtoHabitacion>();
            foreach (Habitacion item in colHabitaciones)
            {
                if (item.getActivo() == true)
                {
                    DtoHabitacion dto = new DtoHabitacion();

                    List<int> nc = new List<int>();
                    foreach (Comodidad com in item.getComodidades())
                    {
                        nc.Add(com.getNumero());
                    }

                    dto.tamanoCama = item.gettamanoCama();
                    dto.numero = item.getNumero();
                    dto.piso = item.getPiso();
                    dto.descripcion = item.getDescripcion();
                    dto.tamano = item.gettamano();
                    dto.numCategoria = item.getCategoria().getNumero();
                    dto.categoria = item.getCategoria().getNombre();
                    dto.comodidades = nc;

                    colDtoHabi.Add(dto);
                }
            }

            return colDtoHabi;
        }
        public static List<int> mapColHabitacionToColNumHabitacion(List<Habitacion> colHabitaciones)
        {
            List<int> colNumHabi = new List<int>();
            foreach (Habitacion item in colHabitaciones)
            {
                int numHab = item.getNumero();

                colNumHabi.Add(numHab);
            }

            return colNumHabi;
        }

        private static DtoRespuesta validarHabitacion(DtoRespuesta respuesta, DtoHabitacion dto)
        {
            Habitacion habitacion = GetHabitacionByNum(dto.numero);
            if (habitacion != null)
            {
                respuesta.estado = ESTADO_RESPUESTA.ERROR.ToString();
                respuesta.mensaje = "El número de habitación ya fue ingresado";
            }

            return respuesta;
        }

        public static List<DtoHabitacion> GetHabitaciones()
        {
            List<DtoHabitacion> colDtoHabitacion = mapColHabitacionToColDtoHabitacion(ListData.colHabitaciones);
            return colDtoHabitacion;
        }

        public static DtoRespuesta ModificarHabitacion(DtoHabitacion dto)
        {
            DtoRespuesta respuesta = new DtoRespuesta();

            List<Comodidad> com = new List<Comodidad>();
            foreach (int nc in dto.comodidades)
            {
                com.Add(HComodidad.GetComodidadByNum(nc));
            }

            Habitacion h = GetHabitacionByNum(dto.numero);
            if (dto.descripcion != null)
            {
                h.setDescripcion(dto.descripcion);
            }
            h.setNumero(dto.numero);
            h.setPiso(dto.piso);
            h.settamano(dto.tamano);
            h.settamanoCama(dto.tamanoCama);
            h.setcomodidades(com);
            h.setcategoria(HCategoria.GetCategoriaByNum(dto.numCategoria));

            respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
            respuesta.mensaje = "La habitación se modifico correctamente!";

            return respuesta;
        }

        public static DtoRespuesta BorrarHabitacion(int numHabitacion)
        {

            DtoRespuesta respuesta = new DtoRespuesta();

            Habitacion habitacionGuardad = GetHabitacionByNum(numHabitacion);
            habitacionGuardad.setActivo(false);

            respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
            respuesta.mensaje = "La habitación se borro correctamente!";

            return respuesta;
        }
        public static List<Habitacion> getHabitacionByListNum(List<int> colNumHabitaciones)
        {
            List<Habitacion> colHabitaciones = new List<Habitacion>();
            foreach (int habitaciones in colNumHabitaciones)
            {
                Habitacion habitacion = GetHabitacionByNum(habitaciones);
                if (habitacion != null)
                {
                    colHabitaciones.Add(habitacion);
                }
            }
            return colHabitaciones;
        }

        public static Habitacion GetHabitacionByNum(int numero)
        {
            foreach (Habitacion item in ListData.colHabitaciones)
            {
                if (item.getNumero() == numero)
                {
                    return item;
                }
            }

            return null;
        }

        public static DtoHabitacion getDtoHabitacionByNum(int numHabitacion)
        {
            Habitacion habitacionNum = GetHabitacionByNum(numHabitacion);
            DtoHabitacion dto = mapHabitacionToDto(habitacionNum);
            return dto;
        }

        private static DtoHabitacion mapHabitacionToDto(Habitacion habitacion)
        {
            DtoHabitacion dto = new DtoHabitacion();
            dto.tamanoCama = habitacion.gettamanoCama();
            dto.numero = habitacion.getNumero();
            dto.piso = habitacion.getPiso();
            dto.descripcion = habitacion.getDescripcion();
            dto.tamano = habitacion.gettamano();
            dto.numCategoria = habitacion.getCategoria().getNumero();
            dto.categoria = habitacion.getCategoria().getNombre();
            dto.comodidades = HComodidad.FromComodidadesToNums(habitacion.getComodidades());

            return dto;
        }

        public static List<Habitacion> FromNumsToHabs(List<int> nums)
        {
            List<Habitacion> colHabs = new List<Habitacion>();

            foreach (int num in nums)
            {
                colHabs.Add(GetHabitacionByNum(num));
            }

            return colHabs;
        }

        public static List<Habitacion> GetHabitacionesLibres(DateTime fecha1, DateTime fecha2)
        {

            List<Habitacion> colHabsLibres = new List<Habitacion>();

            foreach (Habitacion h in ListData.colHabitaciones)
            {
                colHabsLibres.Add(h);
            }

            List<Habitacion> colHabsOcupadas = new List<Habitacion>();



            foreach (Reserva r in ListData.colReservas)
            {
                if (r.getFechaFin() > fecha1 && r.getFechaInicio() < fecha2 || r.getFechaInicio() > fecha1 && r.getFechaInicio() < fecha2 || r.getFechaFin() > fecha1 && r.getFechaFin() < fecha2)
                {
                    if (r.getESTADO_RESERVA() != ESTADO_RESERVA.FINALIZADA)
                    {
                    foreach (Habitacion h in r.getHabitacion())
                    {
                        colHabsOcupadas.Add(h);
                    }
                    }
                }
            }

            foreach (Habitacion h in colHabsOcupadas)
            {
                colHabsLibres.Remove(h);
            }

            return colHabsLibres;
        }

        public static List<Habitacion> GetHabitacionesLibres(DateTime fecha1, DateTime fecha2, int idReserva)
        {

            List<Habitacion> colHabsLibres = new List<Habitacion>();

            foreach (Habitacion h in ListData.colHabitaciones)
            {
                colHabsLibres.Add(h);
            }

            List<Habitacion> colHabsOcupadas = new List<Habitacion>();

            foreach (Reserva r in ListData.colReservas)
            {
                if (r.getIdReserva() != idReserva)
                {
                    if (r.getFechaFin() > fecha1 && r.getFechaInicio() < fecha2 || r.getFechaInicio() > fecha1 && r.getFechaInicio() < fecha2 || r.getFechaFin() > fecha1 && r.getFechaFin() < fecha2)
                    {
                        foreach (Habitacion h in r.getHabitacion())
                        {
                            colHabsOcupadas.Add(h);
                        }
                    }
                }
            }

            foreach (Habitacion h in colHabsOcupadas)
            {
                colHabsLibres.Remove(h);
            }

            return colHabsLibres;
        }
    }
}
