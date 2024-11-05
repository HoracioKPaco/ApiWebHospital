using ApiWebHospital.Model;
using ApiWebHospital.Model.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Data.RepositoryI
{
    public interface IEgresoRepository
    {
        Task<ListadoPaginadoMV<Egreso>> GetListEgresos(int catnidad,int pagina,string textoBusqueda);
        Task<Egreso> GetById(int id);
        Task<int> SetEgreso(Egreso egreso);
        Task<bool> UpdateEgreso(Egreso egreso);
        Task<bool> DeleteEgreso(int id);
    }
}
