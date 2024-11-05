using ApiWebHospital.Model;
using ApiWebHospital.Model.Layout;
using AutoMapper;
using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.ModelDTO.AutoMapper
{
    public class EntitiesMapper:Profile
    {
        public EntitiesMapper() 
        {
            #region Roles
           // CreateMap<MedicoDto, Medico>();
           // CreateMap<Medico, MedicoDto>();
            #endregion
            #region informes
            CreateMap<IngresoDTO, Ingreso>();
            CreateMap<Egreso, EgresoDTO>();
            #endregion
            #region Icollection
            CreateMap<List<Medico>, List<MedicoDto>>();
            #endregion
        }

        public static MedicoDto MedicoToDto(Medico medico)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Medico, MedicoDto>());
            var mapper = config.CreateMapper();
            MedicoDto medicoDto = new MedicoDto();
            medicoDto = mapper.Map<MedicoDto>(medico);
          /* medicoDto.Nombre = medico.Nombre;
           medicoDto.Id = medico.Id;
            medicoDto.ApellidoMaterno = medico.ApellidoMaterno;
            medicoDto.ApellidoPaterno = medico.ApellidoPaterno;
            medicoDto.Abilitado = medico.Abilitado;
            medicoDto.Cedula= medico.Cedula;
            medicoDto.EsEspecialista = medico.EsEspecialista;*/
            return medicoDto;
        }

        public static Medico DtoToMedico(MedicoDto medicoDto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<MedicoDto, Medico>());
            var mapper = config.CreateMapper();
            Medico medico = mapper.Map<Medico>(medicoDto);
            return medico;
        }

        public static PacienteDTO PacienteToDto(Paciente paciente)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Paciente,PacienteDTO>());
            var mapper = config.CreateMapper();
            PacienteDTO pacienteDTO = mapper.Map<PacienteDTO> (paciente);
            return pacienteDTO;
        }
        /// <summary>
        /// Este metodo recive un dominio ingreso y retorna una clase DTO
        /// </summary>
        /// <param name="ingreso"></param>
        /// <returns></returns>
        public static IngresoDTO IngresoToDto(Ingreso ingreso)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Ingreso, IngresoDTO>()
                                                .ForMember(dest => dest.Medico, opt => opt.Ignore())
                                                .ForMember(dest=>dest.PacienteDTO,opt=>opt.Ignore()));
                                                                                            
            var mapper = config.CreateMapper();
            IngresoDTO ingresoDTO = mapper.Map<IngresoDTO>(ingreso);
            return ingresoDTO;
        }

        /// <summary>
        /// Es metodo recible una clase DTO de Ingreso y retona el dominio del mismo
        /// </summary>
        /// <param name="ingreso"></param>
        /// <param name="ingresoDto"></param>
        /// <returns>ingreso</returns>
        public static Ingreso DtoToIngreso(Ingreso ingreso,IngresoDTO ingresoDto)
        {
            ingreso.Fecha = ingresoDto.Fecha;
            ingreso.NumeroCama = ingresoDto.NumeroCama;
            ingreso.NumeroSala = ingresoDto.NumeroSala;
            ingreso.Observacion = ingresoDto.Observacion;
            ingreso.Diagnostico = ingresoDto.Diagnostico;
            ingreso.MedicoId = ingresoDto.MedicoId;
            ingreso.PacienteId = ingresoDto.PacienteId;
            return ingreso;
        }
        /// <summary>
        /// Este metodo recibe una lcase DTo de Paciente y retorna el dominio del mismo
        /// </summary>
        /// <param name="paciente"></param>
        /// <param name="pacienteDto"></param>
        /// <returns></returns>
        public static Paciente DtoToPaciente(Paciente paciente,PacienteDTO pacienteDto)
        {            
            paciente.Cedula = pacienteDto.Cedula;
            paciente.nombre = pacienteDto.nombre;
            paciente.Apellidopaterno = pacienteDto.Apellidopaterno;
            paciente.ApellidoMaterno = pacienteDto.ApellidoMaterno;
            paciente.Celular = pacienteDto.Celular;
            paciente.CorreoElectronico = pacienteDto.CorreoElectronico;
            paciente.Direccion = pacienteDto.Direccion;
            return paciente;
        }

        public static EgresoDTO EngresoToDto(Egreso engreso)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Egreso, EgresoDTO>()
                                                .ForMember(dest => dest.Medico, opt => opt.Ignore())
                                                .ForMember(dest => dest.Paciente, opt => opt.Ignore()));

            var mapper = config.CreateMapper();
            EgresoDTO egresoDTO = mapper.Map<EgresoDTO>(engreso);

            return egresoDTO;
        }

        public static Egreso DtoToEgreso(Egreso egreso,EgresoDTO egresoDTO)
        {            
            egreso.Fecha = egresoDTO.Fecha;
            egreso.Monto = egresoDTO.Monto;
            egreso.Tratamiento = egresoDTO.Tratamiento;
            egreso.MedicoId = egresoDTO.MedicoId;
            egreso.PacienteId = egresoDTO.PacienteId;
            return egreso;  
        }
    }
}
