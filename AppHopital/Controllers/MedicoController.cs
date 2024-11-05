using ApiWebHospital.Model.Layout;
using ApiWebHospital.ModelDTO;
using ApiWebHospital.Service;
using AppHopital.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Net;
using System.Text.Json.Serialization;

namespace AppHopital.Controllers
{
    [EnableCors(policyName:"MyPolicy")]
    public class MedicoController: ControllerBase
    {
        private IMedicoService medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            this.medicoService = medicoService;
        }

            [HttpGet(template:"lista")]
            public IActionResult getMedicos(int cantidad = 10, int pagina=0, string textoBusqueda=null)
            {
                var respuesta =new RespuestasHttpVM<ListadoPaginadoMV<MedicoDto>>();
                try
                {
                    respuesta.Datos = medicoService.ListMedicos(cantidad, pagina, textoBusqueda);
                }catch (Exception ex)
                {
                    respuesta.StatusCode = HttpStatusCode.InternalServerError;
                    respuesta.Datos = null;
                    respuesta.Mensajes.Add(ex.Message);
                    respuesta.Mensajes.Add(ex.ToString());
                }
                return Ok(respuesta);
            }

         [HttpGet(template:"medico/{id}")]
         public IActionResult getMedico(int id)
         {
            //Crea una intancia de la clase generica 
            var respuesta = new RespuestasHttpVM<MedicoDto>();
            try
            {
                respuesta.Datos = medicoService.GetMedico(id).Result;
            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.Datos = null;
                respuesta.Mensajes.Add(ex.Message);
                respuesta.Mensajes.Add(ex.ToString());
            }
            if(respuesta.Datos==null && respuesta.Mensajes.Count() == 0)
            {
                respuesta.StatusCode = HttpStatusCode.NotFound;
                respuesta.Datos = null;
                respuesta.Mensajes.Add("Elemento no Encontrado");
            }
            return Ok(respuesta);
        }

        [HttpGet(template: "medico/cedula/{cedula}")]
        public IActionResult GetMedicoByCedula(string cedula)
        {
            //Crea una intancia de la clase generica 
            var respuesta = new RespuestasHttpVM<MedicoDto>();
            try
            {
                respuesta.Datos = medicoService.GetMedicoByCedula(cedula).Result;
            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.Datos = null;
                respuesta.Mensajes.Add(ex.Message);
                respuesta.Mensajes.Add(ex.ToString());
            }
            if (respuesta.Datos == null && respuesta.Mensajes.Count() == 0)
            {
                respuesta.StatusCode = HttpStatusCode.NotFound;
                respuesta.Datos = null;
                respuesta.Mensajes.Add("Medico no encontrado");
            }
            return Ok(respuesta);
        }

        [HttpPost(template:"guardar")]
         public IActionResult Setmedico([FromBody] MedicoDto medicoDto)
         {            
            var respuesta = new RespuestasHttpVM<int>();           
            try
            {
                respuesta.Datos = this.medicoService.SetMedico(medicoDto).Result;
            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.Datos = 0;
                respuesta.Mensajes.Add(ex.Message);
                respuesta.Mensajes.Add(ex.ToString());
            }            
            return Ok(respuesta);
        }

         [HttpPut(template:"actualizar/{id}")]
         public IActionResult UpdateMedico(int id,[FromBody] MedicoDto medico)
         {
            var respuesta = new RespuestasHttpVM<bool>();
            try
            {
                bool result = this.medicoService.UpdateMedico(medico, id);
                respuesta.Datos = result;
            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.Datos = false;
                respuesta.Mensajes.Add(ex.Message);
                respuesta.Mensajes.Add(ex.ToString());
            }
          
            return Ok(respuesta);
        }

         [HttpDelete(template:"remover/{id}")]
         public IActionResult DeleteMedico(int id)
         {
            var respuesta = new RespuestasHttpVM<bool>();
            try
            {               
                respuesta.Datos = medicoService.DeleteMedico(id).Result;
            }
            catch (Exception ex)
            {
                respuesta.StatusCode = HttpStatusCode.InternalServerError;
                respuesta.Datos = false;
                respuesta.Mensajes.Add(ex.Message);
                respuesta.Mensajes.Add(ex.ToString());
            }
            return Ok(respuesta);
        }
    }
}
