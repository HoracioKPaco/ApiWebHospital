using ApiWebHospital.Data.RepositoryI;
using ApiWebHospital.Service;
using ApiWebHospital.SeviceImplement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppPruevas
{
    public class prueva
    {
        private readonly IIngresoService ingresoService;
        private readonly IEgresoRepository egresoRepository;

        public prueva(IEgresoRepository egresoRepository)
        {
            this.egresoRepository = egresoRepository;
        }

        public void IniciarPruevas()
        {
            Console.WriteLine("Iniciar prueva");

            var l =egresoRepository.GetListEgresos(10, 1, "b322").Result;
            Console.WriteLine("resultado: "+l.Elemento.Count());
            foreach(var item in l.Elemento)
            {
                Console.WriteLine("resultado: " + item.Medico.Cedula);
            }
            
        }
    }
}
