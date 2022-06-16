using Newtonsoft.Json;
using System.Collections.Generic;

namespace WinwinService.Models
{
    public class NewWeightResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("error")]
        public List<Errors> Error { get; set; }
    }
}
