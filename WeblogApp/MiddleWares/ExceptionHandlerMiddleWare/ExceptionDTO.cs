using Newtonsoft.Json;
using System.Collections;

namespace WeblogApp.MiddleWares.ExceptionHandlerMiddleWare
{
    public class ExceptionDTO
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public IDictionary Datas { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
