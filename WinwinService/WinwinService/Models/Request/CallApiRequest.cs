using Newtonsoft.Json;
using System.Collections.Generic;

namespace WinwinService.Models
{
    public class CallApiRequest
    {
        public string Url { get; set; }
        public EnumHttpMethod ApiMethod { get; set; }
        public object Request { get; set; }
        public Dictionary<string,string> Headers { get; set; }
    }
}
