using CommonSolution.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Clases
{
    public class Cliente
    {
        private string nombre;
        private string apellido;
        private string ci;
        private string pais;
        private DateTime fechaNacimiento;
        private string email;
        private bool activo;

        #region Set y Get
        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }
        public void setApellido(string apellido)
        {
            this.apellido = apellido;
        }
        public void setCi(string ci)
        {
            this.ci = ci;
        }
        public void setPais(string pais)
        {
            this.pais = pais;
        }
        public void setFechaNacimiento(DateTime fecha)
        {
            this.fechaNacimiento = fecha;
        }
        public void setEmail(string email)
        {
            this.email = email;
        }
        public void setActivo(bool activo)
        {
            this.activo = activo;
        }

        public string getNombre()
        {
            return this.nombre;
        }
        public string getApellido()
        {
            return this.apellido;
        }
        public string getCi()
        {
            return this.ci;
        }
        public string getPais()
        {
            return this.pais;
        }
        public DateTime getFechaNacimiento()
        {
            return this.fechaNacimiento;
        }
        public string getEmail()
        {
            return this.email;
        }
        public bool getActivo()
        {
            return this.activo;
        }
        #endregion
        public void mapDtoClienteToCliente(DtoCliente dto)
        {
            this.nombre = dto.nombre;
            this.apellido = dto.apellido;
            this.ci = dto.ci;
            this.pais = dto.pais;
            this.fechaNacimiento = DateTime.Parse(dto.fechaNacimiento);
            this.email = dto.email;
        }
    }
}
