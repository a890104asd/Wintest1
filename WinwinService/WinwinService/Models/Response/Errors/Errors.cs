using Newtonsoft.Json;
using System;

namespace WinwinService.Models
{
    public class Errors
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
