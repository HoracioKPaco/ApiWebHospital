using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Model
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Cedula {  get; set; }
        [Required]
        [StringLength(50)]
        public string nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string Apellidopaterno { get; set; }
        [Required]
        [StringLength(50)]
        public string ApellidoMaterno { get; set;}
        [Required]
        [StringLength(10)]
        public string Celular {  get; set; }
        [Required]
        [StringLength (500)]
        public string Direccion { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string CorreoElectronico { get; set; }
        [Required]
        public bool Borrado { get; set; }

        public Paciente()
        {

        }

        public Paciente(string cedula, string nombre, string apellidopaterno, string apellidoMaterno, string celular, string direccion, string correoElectronico, bool borrado)
        {
            Cedula = cedula;
            this.nombre = nombre;
            Apellidopaterno = apellidopaterno;
            ApellidoMaterno = apellidoMaterno;
            Celular = celular;
            Direccion = direccion;
            CorreoElectronico = correoElectronico;
            this.Borrado = borrado;
        }
    }
}
