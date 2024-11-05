using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Model.Layout
{
    public class ListadoPaginadoMV<T>
    {
        public int CantidadTotal { get; set; }

        public List<T> Elemento { get; set; }

        public ListadoPaginadoMV() { 
          this.Elemento = new List<T>();
            this.CantidadTotal = 0;
        }
    }
}
