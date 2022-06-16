using Newtonsoft.Json;
using System.Collections.Generic;

namespace WinwinService.Models
{
    public class ApiResponse<T> where T : class
    {
        [JsonProperty("status")]
        public int Status { get; set; }
        public T Data { get; set; }
        [JsonProperty("error")]
        public List<Errors> Error { get; set; }
    }


}
