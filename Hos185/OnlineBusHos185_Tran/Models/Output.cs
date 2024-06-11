using Newtonsoft.Json.Linq;

namespace Hos185_His.Models
{
    public class Output<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        public int statusCode { get; set; }
        public T data { get; set; }
    }
}
