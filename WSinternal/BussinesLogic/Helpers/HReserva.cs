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
    public class HReserva
    {
        public static DtoRespuesta AddReserva(DtoReserva dto)
        {
            DtoRespuesta respuesta = new DtoRespuesta();

            respuesta = validarReserva(respuesta, dto);


            if (respuesta.estado != ESTADO_RESPUESTA.ERROR.ToString())
            {

                Reserva nuevaReserva = new Reserva();

                nuevaReserva.mapDtoReservaToReserva(dto);

                nuevaReserva.setIdReserva(ListData.currentBookingNumber);

                if (dto.clienteRegistrado == true)
                {
                    Cliente cliente = HCliente.GetClienteXci(dto.ciCliente);
                    nuevaReserva.setCliente(cliente);
                }
                else if (dto.clienteRegistrado != true)
                {
                    Cliente cliente = new Cliente();

                    cliente.setCi(dto.ciCliente);
                    cliente.setNombre(dto.nombreCliente);
                    cliente.setApellido(dto.apellidoCliente);

                    nuevaReserva.setCliente(cliente);
                }

                List<Habitacion> colHabitacionesReserva = new List<Habitacion>();

                foreach (int nH in dto.numHabitacion)
                {
                    colHabitacionesReserva.Add(HHabitacion.GetHabitacionByNum(nH));
                }

                nuevaReserva.setHabitacion(colHabitacionesReserva);

                nuevaReserva.setActivo(true);
                nuevaReserva.setFechaInicio(DateTime.Parse(dto.fechaInicio));
                nuevaReserva.setFechaFin(DateTime.Parse(dto.fechaFin));
                ListData.colReservas.Add(nuevaReserva);

                if (dto.clienteRegistrado != false)
                {
                    Hemail.Email("Nueva reserva confirmada desde " + nuevaReserva.getFechaInicio().ToString("dd/MM/yyyy") + " a " + nuevaReserva.getFechaFin().ToString("dd/MM/yyyy") + " Desde ya le agradecemos por su reserva y esperamos su arribo. Atte: Alchymist Grand Hotel", nuevaReserva.getCliente().getEmail());
                }

                respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
                respuesta.mensaje = "La reserva se ingreso correctamente!";

            }

            ListData.currentBookingNumber++;
            return respuesta;
        }

        private static List<DtoReserva> mapDtoReservaToColDtoReserva(List<Reserva> colReservas)
        {
            List<DtoReserva> colDtoReser = new List<DtoReserva>();
            foreach (Reserva item in colReservas)
            {
                if (item.getESTADO_RESERVA() != ESTADO_RESERVA.CANCELADA && item.getESTADO_RESERVA() != ESTADO_RESERVA.FINALIZADA)
                {
                    DtoReserva dto = new DtoReserva();
                    dto.idReserva = item.getIdReserva();
                    dto.estadoReserva = item.getESTADO_RESERVA();
                    dto.precioTotal = item.getPrecioTotal();
                    dto.fechaInicio = item.getFechaInicio().ToString("yyyy-MM-dd");
                    dto.fechaFin = item.getFechaFin().ToString("yyyy-MM-dd");
                    dto.descuento = item.getDescuento();
                    dto.ciCliente = item.getCliente().getCi();
                    dto.nombreCliente = item.getCliente().getNombre();
                    dto.apellidoCliente = item.getCliente().getApellido();
                    dto.numHabitacion = HHabitacion.mapColHabitacionToColNumHabitacion(item.getHabitacion());

                    colDtoReser.Add(dto);
                }
            }

            return colDtoReser;
        }

        public static List<DtoReserva> GetReservas()
        {
            foreach (Reserva r in ListData.colReservas)
            {
                HReserva.calcularPrecioTotal(r.getIdReserva());
            }
            List<DtoReserva> colDtoReservas = mapDtoReservaToColDtoReserva(ListData.colReservas);
            return colDtoReservas;
        }

        public static int GetNroReservasEmpiezanHoy()
        {
            int contador = 0;

            foreach (Reserva item in ListData.colReservas)
            {
                if (item.getFechaInicio() == DateTime.Today)
                {
                    contador++;
                }
            }
            return contador;
        }

        public static int GetNroReservasTerminanHoy()
        {
            int contador = 0;

            foreach (Reserva item in ListData.colReservas)
            {
                if (item.getFechaFin() == DateTime.Today)
                {
                    contador++;
                }
            }
            return contador;
        }

        public static int GetNroReservasSegunCategoria(Categoria categoria)
        {
            int contador = 0;

            foreach (Reserva item in ListData.colReservas)
            {
                foreach (Habitacion habi in item.getHabitacion())
                {
                    if (habi.getCategoria() == categoria)
                    {
                        contador++;
                    }
                }
            }
            return contador;
        }

        public static List<Reserva> GetReservaXfecha(DateTime fechaInicio, DateTime fechaFin)
        {
            List<Reserva> colReservi = null;

            foreach (Reserva item in ListData.colReservas)
            {
                if (fechaInicio != null && fechaFin != null)
                {
                    if (item.getFechaInicio() == fechaInicio && item.getFechaFin() == fechaFin)
                    {
                        colReservi.Add(item);
                    }
                }
                if (fechaInicio != null && fechaFin == null)
                {
                    if (item.getFechaInicio() == fechaInicio)
                    {
                        colReservi.Add(item);
                    }
                }
                if (fechaInicio == null && fechaFin != null)
                {
                    if (item.getFechaFin() == fechaFin)
                    {
                        colReservi.Add(item);
                    }
                }
            }
            return colReservi;
        }
        public static Reserva GetReservaXid(int id)
        {

            foreach (Reserva item in ListData.colReservas)
            {
                if (item.getIdReserva() == id)
                {
                    return item;
                }
            }

            return null;
        }

        public static DtoRespuesta ModificarReserva(DtoReserva dto)
        {

            DtoRespuesta respuesta = new DtoRespuesta();



            Reserva r = GetReservaXid(dto.idReserva);

            //r.setPrecioTotal(dto.precioTotal);
            r.setFechaInicio(DateTime.Parse(dto.fechaInicio));
            r.setFechaFin(DateTime.Parse(dto.fechaFin));
            if (dto.numHabitacion != null)
            {
                r.setHabitacion(HHabitacion.getHabitacionByListNum(dto.numHabitacion));
            }

            if (dto.clienteRegistrado != false)
            {
                Hemail.Email("Su reserva ha sido modificada con exito, Atte: Alchymist Grand Hotel", r.getCliente().getEmail());
            }

            respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
            respuesta.mensaje = "La reserva se modifico correctamente!";

            return respuesta;
        }

        public static DtoRespuesta CancelarReserva(int numReserva)
        {

            DtoRespuesta respuesta = new DtoRespuesta();

            Reserva reservaGuardad = GetReservaXid(numReserva);
            reservaGuardad.setEstadoReserva(ESTADO_RESERVA.CANCELADA);

            if (mapReservaToDto(reservaGuardad).clienteRegistrado != false)
            {
                Hemail.Email("Su reserva ha sido cancelada, lamentamos la cancelacion de su reserva y esperamos que en alguna oportunidad pueda visitarnos! Atte: Alchymist Grand Hotel", reservaGuardad.getCliente().getEmail());
            }

            respuesta.estado = ESTADO_RESPUESTA.OK.ToString();
            respuesta.mensaje = "La reserva se borro correctamente!";

            return respuesta;
        }

        private static DtoRespuesta validarReserva(DtoRespuesta respuesta, DtoReserva dto)
        {
            Reserva reserva = GetReservaXid(dto.idReserva);
            if (reserva != null)
            {
                respuesta.estado = ESTADO_RESPUESTA.ERROR.ToString();
                respuesta.mensaje = "El número de reserva ya fue ingresado";
            }

            return respuesta;
        }

        public static DtoReserva getDtoReservaById(int numReserva)
        {
            Reserva reservaNum = GetReservaXid(numReserva);
            DtoReserva dto = mapReservaToDto(reservaNum);
            return dto;
        }

        private static DtoReserva mapReservaToDto(Reserva reserva)
        {
            DtoReserva dto = new DtoReserva();

            dto.idReserva = reserva.getIdReserva();
            dto.estadoReserva = reserva.getESTADO_RESERVA();
            dto.precioTotal = reserva.getPrecioTotal();
            dto.fechaInicio = reserva.getFechaInicio().ToString();
            dto.fechaFin = reserva.getFechaFin().ToString();
            dto.descuento = reserva.getDescuento();
            dto.ciCliente = reserva.getCliente().getCi();
            dto.nombreCliente = reserva.getCliente().getNombre();
            dto.apellidoCliente = reserva.getCliente().getApellido();
            dto.numHabitacion = HHabitacion.mapColHabitacionToColNumHabitacion(reserva.getHabitacion());

            return dto;
        }

        public static List<DtoReservaForChart> GetReservasforChart(string fecha1, string fecha2)
        {

            List<DtoReservaForChart> ReservasforChart = new List<DtoReservaForChart>();


            DtoReservaForChart resNueva = new DtoReservaForChart();

            resNueva.estado = ESTADO_RESERVA.NUEVA;
            resNueva.cantidad = 0;
            ReservasforChart.Add(resNueva);

            DtoReservaForChart resIngreso = new DtoReservaForChart();
            resIngreso.estado = ESTADO_RESERVA.INGRESO;
            resIngreso.cantidad = 0;
            ReservasforChart.Add(resIngreso);

            DtoReservaForChart resCancelada = new DtoReservaForChart();
            resCancelada.estado = ESTADO_RESERVA.CANCELADA;
            resCancelada.cantidad = 0;
            ReservasforChart.Add(resCancelada);

            DtoReservaForChart resFinalizada = new DtoReservaForChart();
            resFinalizada.estado = ESTADO_RESERVA.FINALIZADA;
            resFinalizada.cantidad = 0;
            ReservasforChart.Add(resFinalizada);


            if (fecha1 != null && fecha2 != null)
            {
                foreach (Reserva r in ListData.colReservas)
                {
                    if (r.getFechaInicio() >= DateTime.Parse(fecha1) && r.getFechaFin() <= DateTime.Parse(fecha2))
                    {
                        foreach (DtoReservaForChart rr in ReservasforChart)
                        {
                            if (rr.estado == r.getESTADO_RESERVA())
                            {
                                rr.cantidad += 1;
                            }
                        }

                    }
                }

                return ReservasforChart;
            }
            else if (fecha1 != null && fecha2 == null)
            {
                foreach (Reserva r in ListData.colReservas)
                {
                    if (r.getFechaInicio() >= DateTime.Parse(fecha1))
                    {
                        foreach (DtoReservaForChart rr in ReservasforChart)
                        {
                            if (rr.estado == r.getESTADO_RESERVA())
                            {
                                rr.cantidad += 1;
                            }
                        }
                    }
                }

                return ReservasforChart;
            }
            else if (fecha1 == null && fecha2 != null)
            {
                foreach (Reserva r in ListData.colReservas)
                {
                    if (r.getFechaFin() <= DateTime.Parse(fecha2))
                    {
                        foreach (DtoReservaForChart rr in ReservasforChart)
                        {
                            if (rr.estado == r.getESTADO_RESERVA())
                            {
                                rr.cantidad += 1;
                            }
                        }
                    }
                }

                return ReservasforChart;
            }
            else
            {
                foreach (Reserva r in ListData.colReservas)
                {
                    foreach (DtoReservaForChart rr in ReservasforChart)
                    {
                        if (rr.estado == r.getESTADO_RESERVA())
                        {
                            rr.cantidad += 1;
                        }
                    }
                }
            }

            return ReservasforChart;
        }

        public static void CambiarEstado(ESTADO_RESERVA _estado, int _idReserva)
        {
            foreach (Reserva r in ListData.colReservas)
            {
                if (r.getIdReserva() == _idReserva)
                {
                    if (r.getClienteRegistrado() != false)
                    {
                        if (_estado == ESTADO_RESERVA.INGRESO)
                        {
                            Hemail.Email("Su reserva cambio de estado a " + _estado.ToString() + " Esperamos que disfrute de su estadia, no dude en visitar o llamar a nuestros asistentes de recepcion, ellos podran ayudarlo en cualquier cosa que necesite. Atte: Alchymist Grand Hotel", r.getCliente().getEmail());
                        }
                        if (_estado == ESTADO_RESERVA.FINALIZADA)
                        {
                            Hemail.Email("Su reserva cambio de estado a " + _estado.ToString() + " Esperamos que aya disfrutado de su estadia y que pueda visitarnos nuevamente, lo estaremos esperando; Recuerde que realizando otra reserva dentro de 30 dias apartir del actual, usted obtendra un descuento del 10% en el pago de su proxima reserva. Atte: Alchymist Grand Hotel", r.getCliente().getEmail());
                        }
                    }
                    r.setEstadoReserva(_estado);
                }
            }
        }

        public static void calcularPrecioTotal(int idReserva)
        {
            Reserva r = HReserva.GetReservaXid(idReserva);

            List<Gasto> gastosdeReserva = HGasto.getGastosByListNum(r.getGasto());


            TimeSpan auxCD = r.getFechaFin() - r.getFechaInicio();

            int cantDias = auxCD.Days;


            float _precioTotal = 0;


            foreach (Habitacion h in r.getHabitacion())
            {
            if(r.getESTADO_RESERVA() != ESTADO_RESERVA.INGRESO)
            {
                _precioTotal += h.getCategoria().getPrecio() * cantDias;   
            }
            }

            foreach (Gasto g in gastosdeReserva)
            {
                _precioTotal += g.getPrecioTotal();
            }


            bool hasDesc = false;

            Cliente c = r.getCliente();


            foreach (Reserva res in ListData.colReservas)
            {
                if (res.getCliente() == c)
                {
                    TimeSpan auxDesc = res.getFechaFin() - r.getFechaInicio();

                    int cantDiasDesc = auxCD.Days;

                    if (cantDiasDesc <= 30 && r.getFechaInicio() > res.getFechaFin())
                    {
                        hasDesc = true;
                    }
                }
            }

            if (hasDesc == true)
            {
                _precioTotal = (_precioTotal * 90) / 100;
                r.setPrecioTotal(_precioTotal);

            }
            else if (hasDesc == false)
            {
                r.setPrecioTotal(_precioTotal);
            }

        }
    }
}
