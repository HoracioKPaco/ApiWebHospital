using ApiWebHospital.Data.RepositoryI;
using ApiWebHospital.Model;
using ApiWebHospital.Model.Layout;
using ApiWebHospital.ModelDTO;
using ApiWebHospital.ModelDTO.AutoMapper;
using ApiWebHospital.Service;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.SeviceImplement
{
    public class PacienteService:IPacienteService
    {
        private IPacienteRepository pacienteRepository;
        public PacienteService(IPacienteRepository pacienteRepository) { 
           this.pacienteRepository = pacienteRepository;
        }

        public Task<bool> DeletePaciente(int id)
        {
            return this.pacienteRepository.DeletePaciente(id);
        }

        public ListadoPaginadoMV<PacienteDTO> GetListPacientes(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoMV<Paciente> listadoPaginadoMV = this.pacienteRepository.GetListPacientes(cantidad, pagina, textoBusqueda).Result;
           
            ListadoPaginadoMV<PacienteDTO> listadoPaginadoMVDto = new ListadoPaginadoMV<PacienteDTO>();
            listadoPaginadoMVDto.CantidadTotal = listadoPaginadoMV.CantidadTotal;
            foreach(Paciente pas  in listadoPaginadoMV.Elemento) {
                  listadoPaginadoMVDto.Elemento.Add(EntitiesMapper.PacienteToDto(pas));
            }

            return listadoPaginadoMVDto;
        }

        public Task<PacienteDTO> GetPacienteByCedula(string cedula)
        {
            PacienteDTO pacienteDto = new PacienteDTO();
            Paciente paciente = this.pacienteRepository.GetPacienteByCedula(cedula).Result;
            pacienteDto = EntitiesMapper.PacienteToDto(paciente);
            return Task.FromResult(pacienteDto);
        }

        public Task<PacienteDTO> GetPacienteById(int id)
        {
            Paciente paciente = this.pacienteRepository.GetPacienteById(id).Result;                     
            PacienteDTO pacienteDTO = EntitiesMapper.PacienteToDto(paciente);
            return Task.FromResult(pacienteDTO);                  
        }

        public async Task<int> SetPaciente(PacienteDTO pacienteDto)
        {
            Paciente paciente = EntitiesMapper.DtoToPaciente(new Paciente(),pacienteDto);
            paciente.Borrado = false;
            var result = await pacienteRepository.SetPaciente(paciente);
            return result;
        }

        public bool UpdatePaciente(int id, PacienteDTO pacienteDto)
        {
            Paciente paciente = this.pacienteRepository.GetPacienteById(id).Result;
            if (paciente != null)
            {
                paciente = EntitiesMapper.DtoToPaciente(paciente,pacienteDto);
                this.pacienteRepository.UpdatePaciente(paciente);
                return true;
            }
            return false;
        }
    }
}
