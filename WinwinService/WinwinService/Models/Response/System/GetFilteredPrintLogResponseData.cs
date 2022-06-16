using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WinwinService.Models
{
    public class GetFilteredPrintLogResponseData
    {
        /// <summary>
        /// Scan&Print Logs
        /// </summary>
        [JsonProperty("filtered_print_logs")]
        public List<FilteredPrintLog> FilterPrintLogs { get; set; }
    }

    public class FilteredPrintLog
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("times")]
        public string Times { get; set; }
        [JsonProperty("user")]
        public string User { get; set; }
    }
}
