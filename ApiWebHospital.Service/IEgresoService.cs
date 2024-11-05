using ApiWebHospital.Model.Layout;
using ApiWebHospital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Service
{
    public interface IEgresoService
    {
        Task<ListadoPaginadoMV<EgresoDTO>> GetListEgreso(int cantidad, int pagina, string textoBusqueda);
        Task<EgresoDTO> GetEgreso(int id);
        Task<int> SetEgreso(EgresoDTO e);
        Task<bool> UpdateEgreso(EgresoDTO egresoDTO,int id);   
        Task<bool> DeleteEgreso(int id);
    }
}
