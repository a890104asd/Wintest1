using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WinwinService.Models
{
    public class GetLiveLabelResponseData
    {
        [JsonProperty("filename")]
        public string FileName { get; set; }

        [JsonProperty("base64content")]
        public string FileBase64Code { get; set; }
    }
}
