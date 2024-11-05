using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Model
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Cedula { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(50)]
        public string ApellidoPaterno { get; set; }
        [Required]
        [MaxLength(50)]
        public string ApellidoMaterno { get; set; }
        [Required]
        public bool EsEspecialista { get; set; }
        [Required]
        public bool Abilitado { get; set; }
        [Required]
        public bool Borrado { get; set; }

        public Medico()
        {

        }
        public Medico(string cedula, string nombre, string apellidoPaterno, string apellidoMaterno, bool esEspecialista, bool abilitado, bool borrado)
        {
            Cedula = cedula;
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            EsEspecialista = esEspecialista;
            Abilitado = abilitado;
            Borrado = borrado;
        }
    }
}
