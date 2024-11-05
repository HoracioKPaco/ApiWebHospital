using ApiWebHospital.Model.Layout;
using ApiWebHospital.ModelDTO;
using ApiWebHospital.Service;
using AppHopital.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AppHopital.Controllers
{
    
    [EnableCors(policyName: "MyPolicy")]
    [Route(template: "paciente")]
    public class PacienteController:ControllerBase
    {
        private IPacienteService pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            this.pacienteService = pacienteService;
        }

        [HttpGet(template: "lista")]
        public IActionResult GetListPacientes(int cantidad = 10, int pagina = 0,string textoBusqueda="")
        {
            RespuestasHttpVM<ListadoPaginadoMV<PacienteDTO>> respuestasHttpVM = new RespuestasHttpVM<ListadoPaginadoMV<PacienteDTO>>();
            try
            {
                respuestasHttpVM.Datos = this.pacienteService.GetListPacientes(cantidad, pagina, textoBusqueda);
            }catch (Exception ex)
            {
                respuestasHttpVM.StatusCode = HttpStatusCode.InternalServerError;
                respuestasHttpVM.Datos = null;
                respuestasHttpVM.Mensajes.Add(ex.Message);
                respuestasHttpVM.Mensajes.Add(ex.ToString());
            }
            return Ok(respuestasHttpVM);
        }

        [HttpPost(template:"agregar")]
        public IActionResult SetPaciente([FromBody]PacienteDTO pacienteDTO)
        {
            RespuestasHttpVM<int> respuestasHttpVM = new RespuestasHttpVM<int>();
            try
            {
                respuestasHttpVM.Datos = this.pacienteService.SetPaciente(pacienteDTO).Result;
            }catch(Exception ex)
            {
                respuestasHttpVM.StatusCode = HttpStatusCode.InternalServerError;
                respuestasHttpVM.Datos = 0;
                respuestasHttpVM.Mensajes.Add(ex.Message);
                respuestasHttpVM.Mensajes.Add(ex.ToString());
            }
            return Ok(respuestasHttpVM);
        }

        [HttpGet(template:"buscar/{id}")]
        public IActionResult getPaciente(int id)
        {
            RespuestasHttpVM<PacienteDTO> respuestasHttpVM = new RespuestasHttpVM<PacienteDTO>();
            try
            {
                respuestasHttpVM.Datos = this.pacienteService.GetPacienteById(id).Result;
            }catch(Exception ex)
            {
                respuestasHttpVM.StatusCode = HttpStatusCode.InternalServerError;
                respuestasHttpVM.Datos = null;
                respuestasHttpVM.Mensajes.Add("No se encuentra al paciente");
                respuestasHttpVM.Mensajes.Add(ex.Message);
                respuestasHttpVM.Mensajes.Add(ex.ToString());
            }
            return Ok(respuestasHttpVM);
        }


        [HttpGet(template: "cedula/{cedula}")]
        public IActionResult GetPacienteByCedula(string cedula)
        {
            RespuestasHttpVM<PacienteDTO> respuestasHttpVM = new RespuestasHttpVM<PacienteDTO>();
            try
            {
                PacienteDTO pacienteDTO = this.pacienteService.GetPacienteByCedula(cedula).Result;
                respuestasHttpVM.Datos = pacienteDTO;
                if (pacienteDTO == null)
                {                  
                    respuestasHttpVM.Mensajes.Add("Paciente no existe");
                }
            }
            catch (Exception ex)
            {
                respuestasHttpVM.StatusCode = HttpStatusCode.InternalServerError;
                respuestasHttpVM.Datos = null;
                respuestasHttpVM.Mensajes.Add("No se encuentra al paciente");
                respuestasHttpVM.Mensajes.Add(ex.Message);
                respuestasHttpVM.Mensajes.Add(ex.ToString());
            }
            return Ok(respuestasHttpVM);
        }

        [HttpPut(template: "actualizar/{id}")]
        public IActionResult PutPaciente(int id, [FromBody]PacienteDTO dto)
        {
            RespuestasHttpVM<bool> respuestasHttpVM = new RespuestasHttpVM<bool>();
            try
            {
                respuestasHttpVM.Datos = this.pacienteService.UpdatePaciente(id, dto);
            }catch(Exception ex)
            {
                respuestasHttpVM.StatusCode = HttpStatusCode.InternalServerError;
                respuestasHttpVM.Datos = false;
                respuestasHttpVM.Mensajes.Add(ex.Message);
                respuestasHttpVM.Mensajes.Add(ex.ToString());
            }
            return Ok(respuestasHttpVM);
        }

        [HttpDelete(template:"eliminar/{id}")]
        public IActionResult DeletePaciente(int id)
        {
            RespuestasHttpVM<bool> respuestasHttpVM = new RespuestasHttpVM<bool>();
            try
            {
                respuestasHttpVM.Datos = this.pacienteService.DeletePaciente(id).Result;
            }catch(Exception ex)
            {
                respuestasHttpVM.StatusCode = HttpStatusCode.InternalServerError;
                respuestasHttpVM.Datos = false;
                respuestasHttpVM.Mensajes.Add(ex.Message);
                respuestasHttpVM.Mensajes.Add(ex.ToString());
            }
            return Ok(respuestasHttpVM);
        }
    }
}
