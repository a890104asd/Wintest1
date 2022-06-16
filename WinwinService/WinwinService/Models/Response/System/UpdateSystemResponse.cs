using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinwinService.Models
{
    public class UpdateSystemResponse
    {
        [JsonProperty("file_name")]
        public string FileName { get; set; }

        [JsonProperty("base64_code")]
        public string Base64Code { get; set; }
    }
}
