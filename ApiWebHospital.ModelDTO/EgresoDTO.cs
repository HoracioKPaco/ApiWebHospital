using ApiWebHospital.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.ModelDTO
{
    public class EgresoDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Tratamiento { get; set; }
        public decimal Monto { get; set; }
        public int MedicoId { get; set; }
        public MedicoDto Medico { get; set; }
        public int PacienteId { get; set; }
        public PacienteDTO Paciente { get; set; }

        public EgresoDTO()
        {

        }
    }
}
