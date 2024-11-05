using ApiWebHospital.Model;
using ApiWebHospital.Model.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Data.RepositoryI
{
    public interface IMedicoRepository
    {
      Task<ListadoPaginadoMV<Medico>> GetMedicoList(int cantidad, int pagina, string textoBusqueda);
      Task<Medico> GetMedicoById(int id);

        Task<Medico> GetMedicoByCedula(string cedula);
      Task<int> SetMedico(Medico medico);
       void UpadateMedico(Medico medico);
      Task<bool> DeleteMedico(int id);
    }
}
