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
    [Route(template:"egreso")]
    public class EgresoController:ControllerBase
    {
        private readonly IEgresoService egresoService;

        public EgresoController(IEgresoService egresoService)
        {
            this.egresoService = egresoService;
        }

        [HttpGet(template:"listar")]
        public IActionResult GetListEgreso(int cantidad=10, int pagina=0 , string textoBusqueda="")
        {
            RespuestasHttpVM<ListadoPaginadoMV<EgresoDTO>> respuestasHttpVM = new RespuestasHttpVM<ListadoPaginadoMV<EgresoDTO>>();
            try
            {
                respuestasHttpVM.Datos = this.egresoService.GetListEgreso(cantidad,pagina,textoBusqueda).Result;
            }catch(Exception ex)
            {
                respuestasHttpVM.StatusCode = HttpStatusCode.InternalServerError;
                respuestasHttpVM.Datos = null;
                respuestasHttpVM.Mensajes.Add("Error al lista");
                respuestasHttpVM.Mensajes.Add(ex.Message);
            }
            return Ok(respuestasHttpVM);
        }

        [HttpPost(template:"agregar")]
        public IActionResult PostEgreso([FromBody]EgresoDTO dto)
        {
            RespuestasHttpVM<int> respuestasHttpVM = new RespuestasHttpVM<int>();
            try
            {
                respuestasHttpVM.Datos = this.egresoService.SetEgreso(dto).Result;
            }catch(Exception ex)
            {
                respuestasHttpVM.StatusCode=HttpStatusCode.InternalServerError;
                respuestasHttpVM.Datos = 0;
                respuestasHttpVM.Mensajes.Add("Error al agregar nuevo egreso");
                respuestasHttpVM.Mensajes.Add(ex.Message);
            }
            return Ok(respuestasHttpVM);
        }

        [HttpGet(template:"buscar/{id}")]
        public IActionResult GetEgreso(int id) {
            RespuestasHttpVM<EgresoDTO> respuestasHttpVM = new RespuestasHttpVM<EgresoDTO>();
            try
            {
                respuestasHttpVM.Datos = this.egresoService.GetEgreso(id).Result;
            }
            catch (Exception ex)
            {
                respuestasHttpVM.StatusCode = HttpStatusCode.InternalServerError;
                respuestasHttpVM.Datos = null;
                respuestasHttpVM.Mensajes.Add("Error al buscar egreso");
                respuestasHttpVM.Mensajes.Add(ex.Message);
            }
            return Ok(respuestasHttpVM);
        }

        [HttpPut(template: "actualizar/{id}")]
        public IActionResult UpdateEgreso([FromBody]EgresoDTO egresoDTO,int id)
        {
            RespuestasHttpVM<bool> respuestasHttpVM = new RespuestasHttpVM<bool>();
            try
            {
                respuestasHttpVM.Datos = this.egresoService.UpdateEgreso(egresoDTO,id).Result;
            }
            catch (Exception ex)
            {
                respuestasHttpVM.StatusCode = HttpStatusCode.InternalServerError;
                respuestasHttpVM.Datos = false;
                respuestasHttpVM.Mensajes.Add("Error al actualizar egreso");
                respuestasHttpVM.Mensajes.Add(ex.Message);
            }
            return Ok(respuestasHttpVM);
        }

        [HttpDelete(template:"delete/{id}")]
        public IActionResult DeleteEgreso(int id)
        {
            RespuestasHttpVM<bool> respuestasHttpVM = new RespuestasHttpVM<bool>();
            try
            {
                respuestasHttpVM.Datos = this.egresoService.DeleteEgreso(id).Result;
            }
            catch (Exception ex)
            {
                respuestasHttpVM.StatusCode = HttpStatusCode.InternalServerError;
                respuestasHttpVM.Datos = false;
                respuestasHttpVM.Mensajes.Add("Error al eliminar egreso");
                respuestasHttpVM.Mensajes.Add(ex.Message);
            }
            return Ok(respuestasHttpVM);
        }

    }
}
