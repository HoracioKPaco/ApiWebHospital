using ApiWebHospital.Data.RepositoryI;
using ApiWebHospital.Model;
using ApiWebHospital.Model.Layout;
using ApiWebHospital.ModelDTO;
using ApiWebHospital.ModelDTO.AutoMapper;
using ApiWebHospital.Service;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.SeviceImplement
{
    public class EgresoService : IEgresoService
    {
        private readonly IEgresoRepository egresoRepository;
        private readonly IPacienteService pacienteService;
        private readonly IMedicoService medicoService;

        public EgresoService(IEgresoRepository egresoRepository, IMedicoService medicoService,IPacienteService pacienteService)
        {
            this.egresoRepository = egresoRepository;
            this.medicoService = medicoService;
            this.pacienteService = pacienteService;

        }
        public Task<bool> DeleteEgreso(int id)
        {
            bool result = this.egresoRepository.DeleteEgreso(id).Result;
            return Task.FromResult(result);
        }

        public Task<EgresoDTO> GetEgreso(int id)
        {
            Egreso egreso = this.egresoRepository.GetById(id).Result;
            if (egreso != null)
            {
                EgresoDTO egresoDTO = EntitiesMapper.EngresoToDto(egreso);

                PacienteDTO pacienteDTO = this.pacienteService.GetPacienteById(egresoDTO.PacienteId).Result;
                MedicoDto medicoDTO = this.medicoService.GetMedico(egresoDTO.MedicoId).Result;
                egresoDTO.Medico = medicoDTO;
                egresoDTO.Paciente = pacienteDTO;
                return Task.FromResult(egresoDTO);
            }
            return null;
        }

        public Task<ListadoPaginadoMV<EgresoDTO>> GetListEgreso(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoMV<Egreso> listadoPaginadoMV = new ListadoPaginadoMV<Egreso>();            
            listadoPaginadoMV = this.egresoRepository.GetListEgresos(cantidad, pagina, textoBusqueda).Result;

            ListadoPaginadoMV<EgresoDTO> listadoPaginadoMVDto = new ListadoPaginadoMV<EgresoDTO>();
            foreach (var item in listadoPaginadoMV.Elemento)
            {
                EgresoDTO egresoDTO = EntitiesMapper.EngresoToDto(item);

                PacienteDTO pacienteDTO = this.pacienteService.GetPacienteById(egresoDTO.PacienteId).Result;
                egresoDTO.Paciente= pacienteDTO;

                MedicoDto medicoDto = this.medicoService.GetMedico(egresoDTO.MedicoId).Result;
                egresoDTO.Medico = medicoDto;

                listadoPaginadoMVDto.Elemento.Add(egresoDTO);
            }
            listadoPaginadoMVDto.CantidadTotal = listadoPaginadoMV.CantidadTotal;
            return Task.FromResult(listadoPaginadoMVDto);
        }

        public Task<int> SetEgreso(EgresoDTO egresoDto)
        {
            Egreso egreso = EntitiesMapper.DtoToEgreso(new Egreso(),egresoDto);
            egreso.Borrado = false;
            return this.egresoRepository.SetEgreso(egreso);
            
        }

        public Task<bool> UpdateEgreso(EgresoDTO egresoDTO,int id)
        {

            Egreso egreso = this.egresoRepository.GetById(id).Result;
            egreso = EntitiesMapper.DtoToEgreso(egreso,egresoDTO);
            bool result = this.egresoRepository.UpdateEgreso(egreso).Result;
            return Task.FromResult(result);
        }
    }
}
