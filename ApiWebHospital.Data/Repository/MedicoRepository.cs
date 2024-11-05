using ApiWebHospital.Data.RepositoryI;
using ApiWebHospital.Model;
using ApiWebHospital.Model.Layout;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Data.Repository
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly EfContext _efContext;

        public MedicoRepository(EfContext efContext)
        {
            _efContext = efContext;
        }

        public Task<bool> DeleteMedico(int id)
        {
            Medico medicoDelete = _efContext.Medico.FirstOrDefault(m => m.Id == id);
            if (medicoDelete != null)
            {
                medicoDelete.Borrado = true;
                _efContext.Medico.Update(medicoDelete);
                _efContext.SaveChanges();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public async Task<Medico> GetMedicoByCedula(string cedula)
        {
            Medico medico = new Medico();
            medico = await _efContext.Medico.FirstOrDefaultAsync(m => m.Cedula.Equals(cedula));
            return medico ?? null;
        }

        public async Task<Medico> GetMedicoById(int id)
        {
            Medico medico = new Medico();
            medico = await this._efContext.Medico.Where(x => !x.Borrado && x.Id == id).FirstOrDefaultAsync();//debuelve el primero o nulo;
           
            if (medico == null) return null;
            return medico;
        }

        public Task<ListadoPaginadoMV<Medico>> GetMedicoList(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoMV<Medico> listadoPaginadoMV = new ListadoPaginadoMV<Medico>();
            var query = _efContext.Medico.Where(x => !x.Borrado);
            if (!String.IsNullOrEmpty(textoBusqueda))
            {
                query = query.Where(x => x.Cedula.Contains(textoBusqueda) || x.Nombre.Contains(textoBusqueda));
            }
            listadoPaginadoMV.CantidadTotal = query.Count();
            listadoPaginadoMV.Elemento = query
                                         .OrderBy(x => x.Id) //orden del listado
                                         .Skip(pagina * cantidad) // Saltar paginas, cantidad de elementos de salto
                                         .Take(cantidad) //Limite superior apartir del Skip, Salto de elementos
                                         .ToList();
           return Task.FromResult(listadoPaginadoMV);
        }

        public async Task<int> SetMedico(Medico medico)
        {
            using (var ctx = new EfContext()) {
                ctx.Medico.Add(medico);
                await ctx.SaveChangesAsync();
            }
           
            return medico.Id;
        }

        public void UpadateMedico(Medico medico)
        {
           using(var ctx = new EfContext())
            {
                ctx.Update(medico);
                ctx.SaveChanges();
            }
           
        }
    }
}
