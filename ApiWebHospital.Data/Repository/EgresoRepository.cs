using ApiWebHospital.Data.RepositoryI;
using ApiWebHospital.Model;
using ApiWebHospital.Model.Layout;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Data.Repository
{
    public class EgresoRepository : IEgresoRepository
    {
        private readonly EfContext efContext;
        public EgresoRepository(EfContext efContext)
        {
            this.efContext = efContext;
        }

        public async Task<bool> DeleteEgreso(int id)
        {
           using(var ctx=new EfContext())
            {
                try
                {
                    Egreso egreso = ctx.Egreso.FirstOrDefault(e => e.Id == id);
                    if (egreso != null)
                    {
                        egreso.Borrado = true;
                        ctx.Update(egreso);
                        await ctx.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }catch (Exception ex)
                {
                    Console.WriteLine($"Error al borrar Egreso: {ex.Message}");
                    return false; 
                }
            }
        }

        public async Task<Egreso> GetById(int id)
        {
           using( var ctx=new EfContext())
            {
                Egreso egreso =await ctx.Egreso.Where(e=>e.Id==id && !e.Borrado).FirstOrDefaultAsync();                
                return egreso ?? null;              
            }  
        }

        public Task<ListadoPaginadoMV<Egreso>> GetListEgresos(int cantidad, int pagina, string textoBusqueda)
        {
            Console.WriteLine("Ingrese al metodo del repo");
            Console.WriteLine("Cant: " + cantidad + ", pagina: " + pagina);
            Console.WriteLine("Skip: " + pagina * cantidad);
            ListadoPaginadoMV<Egreso> listadoPaginadoMV = new ListadoPaginadoMV<Egreso>();

            var query = this.efContext.Egreso.Include(e=>e.Medico).Include(e=>e.Paciente).Where(e => !e.Borrado);
          
            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                query = query.Include(e => e.Medico)
                             .Where(e => e.Medico.Cedula.Contains(textoBusqueda) || e.Paciente.Cedula.Contains(textoBusqueda));
            }
            Console.WriteLine("query: " + query.Count());
            listadoPaginadoMV.Elemento = query.OrderBy(e => e.Id)
                                              .Skip(pagina * cantidad)
                                              .Take(cantidad).ToList();
           /*foreach(Egreso e in query)
            {
                listadoPaginadoMV.Elemento.Add(e);
            }*/

            listadoPaginadoMV.CantidadTotal = query.Count();
            return Task.FromResult(listadoPaginadoMV);
        }

        public async Task<int> SetEgreso(Egreso egreso)
        {
           using(var ctx=new EfContext())
            {
                await ctx.Egreso.AddAsync(egreso);
                await ctx.SaveChangesAsync();
                return egreso.Id;
            }
        }

        public async Task<bool> UpdateEgreso(Egreso egreso)
        {
            using(var ctx=new EfContext())
            {
               var d = ctx.Update(egreso);
                await ctx.SaveChangesAsync();
                    
                return d.State==EntityState.Modified;
            }           
        }
    }
}
