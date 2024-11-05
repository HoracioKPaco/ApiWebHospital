using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Model
{
    public class Ingreso
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int NumeroSala { get; set; }
        [Required]
        public int NumeroCama { get; set; }
        [Required]
        public string Diagnostico {  get; set; }
        [Required]
        public string Observacion {  get; set; }
        [Required]
        public bool Borrado { get; set; }
        [Required]
        public int MedicoId { get; set; }
        public Medico Medico { get; set; }
        [Required]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public Ingreso()
        {

        }
        public Ingreso(DateTime fecha, int numeroSala, int numeroCama, string diagnostico, string observacion, bool borrado, int medicoId, int pacienteId)
        {
            Fecha = fecha;
            NumeroSala = numeroSala;
            NumeroCama = numeroCama;
            Diagnostico = diagnostico;
            Observacion = observacion;
            Borrado = borrado;
            MedicoId = medicoId;
            PacienteId = pacienteId;
        }
    }
}
