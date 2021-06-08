using BussinesLogic.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Data
{
    public class ListData
    {
        public static List<Habitacion> colHabitaciones = new List<Habitacion>();
        public static List<Gasto> colGastos = new List<Gasto>();
        public static List<Comodidad> colComodidades = new List<Comodidad>();
        public static List<Categoria> colCategorias = new List<Categoria>();
        public static List<Reserva> colReservas = new List<Reserva>();
        public static List<Cliente> colClientes = new List<Cliente>();

        public static int currentCategoryNumber = 1;

        public static int currentConfortNumber = 1;

        public static int currentBookingNumber = 1;

        public static int currentGastoNumber = 1;

    }
}
