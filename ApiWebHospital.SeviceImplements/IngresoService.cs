using ApiWebHospital.Data.RepositoryI;
using ApiWebHospital.Model;
using ApiWebHospital.Model.Layout;
using ApiWebHospital.ModelDTO;
using ApiWebHospital.ModelDTO.AutoMapper;
using ApiWebHospital.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiWebHospital.SeviceImplement
{
    public class IngresoService : IIngresoService
    {
        private readonly IIngresoRepository _repository;
        private readonly IMedicoService medicoService;
        private readonly IPacienteService pacienteService;
        public IngresoService(IIngresoRepository repository,IMedicoService medicoService,IPacienteService pacienteService)
        {
            _repository = repository;
            this.medicoService = medicoService;
            this.pacienteService = pacienteService;
        }
        public Task<bool> DeleteIngreso(int id)
        {

            return this._repository.DeleteIngreso(id);
        }

        public Task<IngresoDTO> GetIngreso(int id)
        {
            Ingreso ingreso = this._repository.GetIngreso(id).Result;            
            IngresoDTO ingresoDTO = EntitiesMapper.IngresoToDto(ingreso);
            PacienteDTO pacienteDto = this.pacienteService.GetPacienteById(ingresoDTO.PacienteId).Result;
            MedicoDto medicoDto = this.medicoService.GetMedico(ingresoDTO.MedicoId).Result;
            ingresoDTO.PacienteDTO = pacienteDto;
            ingresoDTO.Medico = medicoDto;
            return Task.FromResult(ingresoDTO);            
        }
        /// <summary>
        /// esta funcion busca el listado de ingresos y los conloca en una clase
        /// que contiene una lista generica y una cantidad
        /// </summary>
        /// <param name="cantidad">cantidad de ingresos que desea ver el usuario</param>
        /// <param name="pagina">Numero de pagina relacionado con la cantidad</param>
        /// <param name="textoBusqueda">cadena de busqueda por numeroSala y numeroCama</param>
        /// <returns>retorna una tack con la clase generica</returns>
        public Task<ListadoPaginadoMV<IngresoDTO>> GetListIngresos(int cantidad, int pagina, string textoBusqueda)
        {            
            ListadoPaginadoMV<IngresoDTO> listadoPaginadoMV = new ListadoPaginadoMV<IngresoDTO>();
            //Busca la lista de ingresos
            ListadoPaginadoMV<Ingreso> listadoPaginadoMV1 = this._repository.GetListIngresos3(cantidad, pagina, textoBusqueda).Result;
            //asigna la la cantidad total
            listadoPaginadoMV.CantidadTotal = listadoPaginadoMV1.CantidadTotal;            
            //recore la lista de ingresos 
            listadoPaginadoMV1.Elemento.ForEach(ingreso =>
            {    
                //Mapea de Ingreso al Dto
                IngresoDTO ingresoDTO = EntitiesMapper.IngresoToDto(ingreso);
                //busca al medico relacionado
                MedicoDto medicoDto = this.medicoService.GetMedico(ingreso.MedicoId).Result;                
                ingresoDTO.Medico = medicoDto;///añade el objeto medico al ingresos
                //Busca al paciente relacionado
                PacienteDTO pacienteDTO = this.pacienteService.GetPacienteById(ingreso.PacienteId).Result;
                ingresoDTO.PacienteDTO= pacienteDTO;///añade al paciente relacionado
                //añade el ingreso al listado de ingresos Dto
                listadoPaginadoMV.Elemento.Add(ingresoDTO);
            });     
            
            return Task.FromResult(listadoPaginadoMV);
         }

        public async Task<int> SetIngreso(IngresoDTO ingresoDto)
        {
            Ingreso ingreso = EntitiesMapper.DtoToIngreso(new Ingreso(), ingresoDto);
            ingreso.Borrado = false;
            return await this._repository.SetIngreso(ingreso);
        }

        public bool UpdateIngreso(int id,IngresoDTO ingresoDto)
        {
           Ingreso ingreso = this._repository.GetIngreso(id).Result;
            if (ingreso != null)
            {
                ingreso = EntitiesMapper.DtoToIngreso(ingreso,ingresoDto);
                this._repository.UptadeIngreso(ingreso);
                return true;
            }
            return false;
        }
    }
}
