using Newtonsoft.Json;
using System.Collections.Generic;

namespace WinwinService.Models
{
    public class GetLocalizationRequest
    {
        /// <summary>
        /// 語言物件類別
        /// </summary>
        [JsonProperty("lgc_group_list")]
        public List<string> LgcGroupList
        {
            get;
            set;
        }

        /// <summary>
        /// 語言類型
        /// </summary>
        [JsonProperty("lang_type")]
        public string LangType
        {
            get;
            set;
        }


    }
}
