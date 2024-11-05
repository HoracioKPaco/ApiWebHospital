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
    [Route(template:"Ingreso")]
    public class IngresoController:ControllerBase
    {
        private readonly IIngresoService ingresoService;

        public IngresoController(IIngresoService ingresoService)
        {
            this.ingresoService = ingresoService;            
        }

        [HttpGet(template:"lista")]
        public IActionResult GetListIngresos(int cantidad=10,int pagina=0, string textoBusqueda="") 
        {
            RespuestasHttpVM<ListadoPaginadoMV<IngresoDTO>> respuestasHttpVM = new RespuestasHttpVM<ListadoPaginadoMV<IngresoDTO>>();
            try
            {
                respuestasHttpVM.Datos = this.ingresoService.GetListIngresos(cantidad, pagina, textoBusqueda).Result;
            }catch (Exception ex)
            {
                respuestasHttpVM.Datos = null;
                respuestasHttpVM.Mensajes.Add("Error Al listar Ingresos");
                respuestasHttpVM.Mensajes.Add(ex.Message);
            }
            return Ok(respuestasHttpVM);
        }

        [HttpPost(template:"guardar")]
        public IActionResult SaveIngreso([FromBody]IngresoDTO dto)
        {
            RespuestasHttpVM<int> respuestasHttpVM = new RespuestasHttpVM<int>();
            try
            {
                respuestasHttpVM.Datos = this.ingresoService.SetIngreso(dto).Result;
            }catch(Exception ex)
            {
                respuestasHttpVM.StatusCode = HttpStatusCode.InternalServerError;
                respuestasHttpVM.Datos = 0;
                respuestasHttpVM.Mensajes.Add("Error al guardar Ingreso");
                respuestasHttpVM.Mensajes.Add(ex.Message);
            }
            return Ok(respuestasHttpVM);
        }

        [HttpGet(template:"buscar/{id}")]
        public IActionResult FindIngreso(int id)
        {
            RespuestasHttpVM<IngresoDTO> respuestasHttpVM = new RespuestasHttpVM<IngresoDTO>();
            try
            {
                respuestasHttpVM.Datos = this.ingresoService.GetIngreso(id).Result;

            }catch(Exception ex)
            {
                respuestasHttpVM.StatusCode = HttpStatusCode.InternalServerError;
                respuestasHttpVM.Datos = null;
                respuestasHttpVM.Mensajes.Add("No se pudo encontrar al ingreso");
                respuestasHttpVM.Mensajes.Add(ex.Message);
            }
            return Ok(respuestasHttpVM);
        }

        [HttpPut(template: "actualizar/{id}")]
        public IActionResult UpdateIngreso([FromBody]IngresoDTO ingresoDTO,int id)
        {
            RespuestasHttpVM<bool> respuestasHttpVM = new RespuestasHttpVM<bool>();
            try
            {
               respuestasHttpVM.Datos = this.ingresoService.UpdateIngreso(id,ingresoDTO);

            }
            catch (Exception ex)
            {
                respuestasHttpVM.StatusCode = HttpStatusCode.InternalServerError;
                respuestasHttpVM.Datos = false;
                respuestasHttpVM.Mensajes.Add("No se pudo actualizar el ingreso");
                respuestasHttpVM.Mensajes.Add(ex.Message);
            }
            return Ok(respuestasHttpVM);
        }

        [HttpDelete(template: "delete/{id}")]
        public IActionResult ingreso(int id)
        {
            RespuestasHttpVM<bool> respuestasHttpVM = new RespuestasHttpVM<bool>();
            try
            {
                respuestasHttpVM.Datos = this.ingresoService.DeleteIngreso(id).Result;

            }
            catch (Exception ex)
            {
                respuestasHttpVM.StatusCode = HttpStatusCode.InternalServerError;
                respuestasHttpVM.Datos = false;
                respuestasHttpVM.Mensajes.Add("No se pudo eliminar el ingreso");
                respuestasHttpVM.Mensajes.Add(ex.Message);
            }
            return Ok(respuestasHttpVM);
        }

    }
}
