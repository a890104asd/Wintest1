using Newtonsoft.Json;
using System;

namespace WinwinService.Models
{
    public class ScanPrintLogRequest
    {
        /// <summary>
        /// 掃描code
        /// </summary>
        [JsonProperty("code")]
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 狀態
        /// </summary>
        [JsonProperty("status")]
        public string Status
        {
            get;
            set;
        }

        /// <summary>
        /// 操作人員
        /// </summary>
        [JsonProperty("ins_user")]
        public string User
        {
            get;
            set;
        }

        /// <summary>
        /// IP
        /// </summary>
        [JsonProperty("ip")]
        public string IP
        {
            get;
            set;
        }

        /// <summary>
        /// 倉庫Code
        /// </summary>
        [JsonProperty("whs_code")]
        public string WhsCode
        {
            get;
            set;
        }

        /// <summary>
        /// 起始時間
        /// </summary>
        [JsonProperty("start_date")]
        public DateTime? StartDate
        {
            get;
            set;
        }

        /// <summary>
        /// 結束時間
        /// </summary>
        [JsonProperty("end_date")]
        public DateTime? EndDate
        {
            get;
            set;
        }
    }
}
