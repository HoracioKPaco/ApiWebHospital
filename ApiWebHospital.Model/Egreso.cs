using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Model
{
    public class Egreso
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [MaxLength]
        public string Tratamiento { get; set; }
        [Required]
        [Range(0,999999999999.99)]
        public decimal Monto { get; set; }
        [Required]
        public bool Borrado { get; set; }
        [Required]
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        [Required]        
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public Egreso()
        {

        }
        public Egreso(DateTime fecha, string tratamiento, decimal monto, bool borrado, int medicoId, int pacienteId)
        {
            Fecha = fecha;
            Tratamiento = tratamiento;
            Monto = monto;
            Borrado = borrado;
            MedicoId = medicoId;
            PacienteId = pacienteId;
        }
    }
}
