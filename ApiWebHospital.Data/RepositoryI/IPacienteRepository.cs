using ApiWebHospital.Model;
using ApiWebHospital.Model.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.Data.RepositoryI
{
    public interface IPacienteRepository
    {
        Task<ListadoPaginadoMV<Paciente>> GetListPacientes(int cantidad,int pagina,string textoBusqueda);
        Task<Paciente> GetPacienteById(int id);
        Task<Paciente> GetPacienteByCedula(string cedula);
        Task<int> SetPaciente(Paciente paciente);

        void UpdatePaciente(Paciente paciente);
        Task<bool> DeletePaciente(int id);
    }
}
