using ApiWebHospital.Model;
using ApiWebHospital.Model.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Data.RepositoryI
{
    public interface IIngresoRepository
    {
        Task<ListadoPaginadoMV<Ingreso>> GetListIngresos3(int cantidad, int pagina, string textoBusqueda);
        Task<Ingreso> GetIngreso(int id);
        Task<int> SetIngreso(Ingreso ingreso);
        void UptadeIngreso(Ingreso ingreso);
        Task<bool> DeleteIngreso(int id);
    }
}
