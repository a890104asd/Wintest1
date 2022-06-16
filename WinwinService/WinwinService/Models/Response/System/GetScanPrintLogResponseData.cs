using DataBase.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WinwinService.Models
{
    public class GetScanPrintLogResponseData
    {
        /// <summary>
        /// Scan&Print Logs
        /// </summary>
        [JsonProperty("scanprintlogs")]
        public List<ScanPrintLog> ScanPrintLogs { get; set; }
    }
}
