using ApiWebHospital.Model;
using ApiWebHospital.Model.Layout;
using ApiWebHospital.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Service
{
    public interface IPacienteService
    {
        ListadoPaginadoMV<PacienteDTO> GetListPacientes(int cantidad, int pagina, string textoBusqueda);
        Task<PacienteDTO> GetPacienteById(int id);
        Task<PacienteDTO> GetPacienteByCedula(string cedula);
        Task<int> SetPaciente(PacienteDTO pacienteDto);

        bool UpdatePaciente(int id,PacienteDTO pacienteDto);

        Task<bool> DeletePaciente(int id);
    }
}
