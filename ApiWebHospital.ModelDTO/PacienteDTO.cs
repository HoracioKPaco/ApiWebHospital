using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.ModelDTO
{
    public class PacienteDTO
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string nombre { get; set; }
        public string Apellidopaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string CorreoElectronico { get; set; }

        public PacienteDTO() { }
    }
}
