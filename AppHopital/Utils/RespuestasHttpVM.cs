using System.Net;

namespace AppHopital.Utils
{
    public class RespuestasHttpVM<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Datos { get; set; }

        public List<string> Mensajes { get; set;}

        public RespuestasHttpVM()
        {
            this.StatusCode = HttpStatusCode.OK;
            this.Datos = default(T);
            this.Mensajes = new List<string>();
        }
    }
}
