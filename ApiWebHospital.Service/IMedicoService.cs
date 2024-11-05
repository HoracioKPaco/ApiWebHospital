using ApiWebHospital.Model.Layout;
using ApiWebHospital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Service
{
    public interface IMedicoService
    {
        Task<int> SetMedico(MedicoDto medicoDto);
        Task<MedicoDto> GetMedico(int id);
        
        Task<MedicoDto> GetMedicoByCedula(string cedula);
        ListadoPaginadoMV<MedicoDto> ListMedicos(int cantidad, int pagina, string textoBusqueda);
        bool UpdateMedico(MedicoDto medicoDto,int id);
        Task<bool> DeleteMedico(int id);
    }
}
