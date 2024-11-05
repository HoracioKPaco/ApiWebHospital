using ApiWebHospital.Model.Layout;
using ApiWebHospital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Service
{
    public interface IIngresoService
    {
        Task<ListadoPaginadoMV<IngresoDTO>> GetListIngresos(int cantidad,int pagina,string textoBusqueda);
        Task<IngresoDTO> GetIngreso(int id);
        Task<int> SetIngreso(IngresoDTO ingresoDto);
        bool UpdateIngreso(int id,IngresoDTO ingresoDto);
        Task<bool> DeleteIngreso(int id);
    }
}
