using ApiWebHospital.Data.RepositoryI;
using ApiWebHospital.Model;
using ApiWebHospital.Model.Layout;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Data.Repository
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly EfContext _efContext;

        public PacienteRepository(EfContext efContext)
        {
            _efContext = efContext;
        }

        public Task<bool> DeletePaciente(int id)
        {
            Paciente paciente = _efContext.Paciente.FirstOrDefault(p => p.Id == id);
            if(paciente != null) {
                paciente.Borrado = true;
               this._efContext.Update(paciente);
                this._efContext.SaveChanges();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<ListadoPaginadoMV<Paciente>> GetListPacientes(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoMV<Paciente> listadoPaginadoMV = new ListadoPaginadoMV<Paciente>();
            var query = this._efContext.Paciente.Where(p => !p.Borrado);
            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                query = query.Where(p=>p.Cedula.Contains(textoBusqueda) || p.nombre.Contains(textoBusqueda) || p.ApellidoMaterno.Contains(textoBusqueda) || p.Apellidopaterno.Contains(textoBusqueda));
            }
            listadoPaginadoMV.CantidadTotal = query.Count();
            listadoPaginadoMV.Elemento = query.OrderBy(p => p.Id).Skip(pagina * cantidad)
                                         .Take(cantidad).ToList();
            return Task.FromResult(listadoPaginadoMV);
                                         
        }

        public async Task<Paciente> GetPacienteByCedula(string cedula)
        {
            Paciente paciente = new Paciente();
            paciente = await this._efContext.Paciente.FirstOrDefaultAsync(p => p.Cedula.Equals(cedula));
            return paciente ?? null;
        }

        public async Task<Paciente> GetPacienteById(int id)
        {
            Paciente paciente = new Paciente();
            paciente =await this._efContext.Paciente.Where(p => !p.Borrado && p.Id==id).FirstOrDefaultAsync();
            if (paciente == null) return null;
            return paciente;
        }

        public async Task<int> SetPaciente(Paciente paciente)
        {
           using (var ctx = new EfContext())
              {
                    ctx.Paciente.Add(paciente);
                    await ctx.SaveChangesAsync();
             }
           
            return paciente.Id;
        }

        public void UpdatePaciente(Paciente paciente)
        {
            using(var ctx = new EfContext()) {
                ctx.Update(paciente);
                ctx.SaveChanges();
            }
            
        }
    }
}
