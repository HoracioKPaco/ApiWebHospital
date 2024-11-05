using ApiWebHospital.Data.Repository;
using ApiWebHospital.Data.RepositoryI;
using ApiWebHospital.Data.UnitOfWork;
using ApiWebHospital.Model;
using ApiWebHospital.Model.Layout;
using ApiWebHospital.ModelDTO;
using ApiWebHospital.ModelDTO.AutoMapper;
using ApiWebHospital.Service;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.SeviceImplement
{
    public class MedicoService : IMedicoService
    {
        private IMedicoRepository medicoRepository;

        public MedicoService(IMedicoRepository medicoRepository)
        {            
            this.medicoRepository = medicoRepository;
        }
        public Task<bool> DeleteMedico(int id)
        {
           return this.medicoRepository.DeleteMedico(id);           
        }

        public Task<MedicoDto> GetMedico(int id)
        {
            Medico findMedico = new Medico();
            findMedico = this.medicoRepository.GetMedicoById(id).Result;
            MedicoDto medicoDto =EntitiesMapper.MedicoToDto(findMedico);
            return Task.FromResult(medicoDto);
        }

        public Task<MedicoDto> GetMedicoByCedula(string cedula)
        {
            MedicoDto medicoDto = new MedicoDto();
            Medico medico = this.medicoRepository.GetMedicoByCedula(cedula).Result;
            medicoDto = EntitiesMapper.MedicoToDto(medico);
            return Task.FromResult(medicoDto);
        }

        public ListadoPaginadoMV<MedicoDto> ListMedicos(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoMV<Medico> listadoPaginadoMV = this.medicoRepository.GetMedicoList(cantidad, pagina, textoBusqueda).Result;
            //  ListadoPaginadoMV<MedicoDto> listadoPaginadoDTO = mapper.Map<ListadoPaginadoMV<MedicoDto>>(listadoPaginadoMV);
            ListadoPaginadoMV<MedicoDto> listadoPaginadoDTO=new ListadoPaginadoMV<MedicoDto>();
            listadoPaginadoDTO.CantidadTotal= listadoPaginadoMV.CantidadTotal;
            foreach (Medico item in listadoPaginadoMV.Elemento)
            {
                listadoPaginadoDTO.Elemento.Add(EntitiesMapper.MedicoToDto(item));
            }
            return listadoPaginadoDTO;
        }

        public Task<int> SetMedico(MedicoDto medicoDto)
        {
            Medico medico = new Medico();
            medico.Borrado = false;
            medico =  EntitiesMapper.DtoToMedico(medicoDto);         
            var result = this.medicoRepository.SetMedico(medico);
            return result;
        }

        public bool UpdateMedico(MedicoDto medicoDto, int id)
        {
            Medico medico = this.medicoRepository.GetMedicoById(id).Result;
            if(medico != null) {
                  medico.Nombre = medicoDto.Nombre;
                  medico.Abilitado = medicoDto.Abilitado;
                  medico.ApellidoPaterno = medicoDto.ApellidoPaterno;
                  medico.ApellidoMaterno = medicoDto.ApellidoMaterno;
                  medico.Cedula= medicoDto.Cedula;
                  medico.EsEspecialista = medicoDto.EsEspecialista;
                this.medicoRepository.UpadateMedico(medico);
                return true;
            }
            return false;
        }
    }
}
