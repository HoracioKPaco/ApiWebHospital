using ApiWebHospital.Data.RepositoryI;
using ApiWebHospital.Model;
using ApiWebHospital.Model.Layout;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Data.Repository
{
    public class IngresoRepository : IIngresoRepository
    {
        private readonly EfContext efContext;

        public IngresoRepository(EfContext efContext)
        {
            this.efContext = efContext;
        }
        
        public Task<bool> DeleteIngreso(int id)
        {
            Ingreso ingreso = this.efContext.Ingreso.FirstOrDefault(i => i.Id == id);
            if (ingreso != null)
            {
                ingreso.Borrado = true;
                this.efContext.Update(ingreso);
                this.efContext.SaveChanges();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public async Task<Ingreso> GetIngreso(int id)
        {
            Ingreso ingreso = new Ingreso();
            ingreso = await efContext.Ingreso.Where(i => i.Id == id && !i.Borrado).FirstOrDefaultAsync();

            return ingreso ?? null; 
        }

        /// <summary>
        /// Este metodo retorna una lista de acuerdo a sus parametros
        /// </summary>
        /// <param name="cantidad"></param>
        /// <param name="pagina"></param>
        /// <param name="textoBusqueda"></param>
        /// <returns></returns>
        public Task<ListadoPaginadoMV<Ingreso>> GetListIngresos3(int cantidad, int pagina, string textoBusqueda)
        {
            Console.WriteLine("Ingrese al metodo del repo");
            Console.WriteLine("Cant: " + cantidad + ", pagina: " + pagina);
            Console.WriteLine("Skip: " + pagina * cantidad);
            ListadoPaginadoMV<Ingreso> listadoPaginadoMV = new ListadoPaginadoMV<Ingreso>();
            var query = this.efContext.Ingreso.Where(i => !i.Borrado);
            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                query = query.Where(i => i.NumeroSala.Equals(int.Parse(textoBusqueda)) || i.NumeroCama.Equals(textoBusqueda));
            }

            listadoPaginadoMV.Elemento = query.OrderBy(i => i.Id).OrderBy(x => x.Id)
                                          .Skip(pagina * cantidad)
                                           .Take(cantidad).ToList();
            listadoPaginadoMV.CantidadTotal = query.Count();
            return Task.FromResult(listadoPaginadoMV);
        }

        /*public Task<ListadoPaginadoMV<Ingreso>> GetListIngresos3(int cantidad, int pagina, string textoBusqueda)
        {
            Console.WriteLine("Ingrese al metodo del repo");
            Console.WriteLine("Cant: "+cantidad+", pagina: "+pagina);
            Console.WriteLine("Skip: "+pagina*cantidad);
            ListadoPaginadoMV<Ingreso> listadoPaginadoMV = new ListadoPaginadoMV<Ingreso>();
            var query = this.efContext.Ingreso.Where(i => !i.Borrado);
            if (!string.IsNullOrEmpty(textoBusqueda))
            {
                query = query.Where(i => i.NumeroSala.Equals(int.Parse(textoBusqueda)) || i.NumeroCama.Equals(textoBusqueda));
            }
            
            query = query.OrderBy(i => i.Id).OrderBy(x=>x.Id)
                                          .Skip(pagina * cantidad)
                                           .Take(cantidad);
            listadoPaginadoMV.CantidadTotal = query.Count();
            foreach (var i in query)
            {               
                listadoPaginadoMV.Elemento.Add(i);
            }
            return Task.FromResult(listadoPaginadoMV);
        }*/



        public async Task<int> SetIngreso(Ingreso ingreso)
        {
           using(var ctx= new EfContext())
            {
                await ctx.Ingreso.AddAsync(ingreso);
                await ctx.SaveChangesAsync();
            }
            return ingreso.Id;
        }

        public void UptadeIngreso(Ingreso ingreso)
        {
            using (var ctx = new EfContext())
            {
                 ctx.Update(ingreso);
                 ctx.SaveChanges();
            }
        }
    }
}
