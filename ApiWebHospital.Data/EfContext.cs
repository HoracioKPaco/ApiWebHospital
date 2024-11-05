using ApiWebHospital.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Data
{
    public class EfContext:DbContext
    {
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Ingreso> Ingreso { get; set; }
        public DbSet<Egreso> Egreso { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("server=DESKTOP-4T8TTPE\\SQLEXPRESS; database=HospitalDB; integrated security=true; TrustServerCertificate=true");
    }
}
