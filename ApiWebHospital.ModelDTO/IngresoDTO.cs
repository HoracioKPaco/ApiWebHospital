using ApiWebHospital.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.ModelDTO
{
    public class IngresoDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int NumeroSala { get; set; }
        public int NumeroCama { get; set; }
        public string Diagnostico { get; set; }
        public string Observacion { get; set; }
        public int MedicoId { get; set; }
        public MedicoDto Medico { get; set; }
        public int PacienteId { get; set; }
        public PacienteDTO PacienteDTO { get; set; }

        public IngresoDTO()
        {
            Medico=new MedicoDto();

        }
    }
}
