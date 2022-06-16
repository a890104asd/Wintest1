using Newtonsoft.Json;
using System.Collections.Generic;

namespace WinwinService.Models
{
    public class GetLocalizationResponsedata
    {
        [JsonProperty("localizations")]
        public List<GetLocalization> Localizations { get; set; }    
    }

    public class GetLocalization
    {

        [JsonProperty("localization_id")]
        public int LocalizationID
        {
            get;
            set;
        }

        [JsonProperty("lgc_group")]
        public string LgcGroup
        {
            get;
            set;
        }

        [JsonProperty("context")]
        public string Context
        {
            get;
            set;
        }

        [JsonProperty("lgc_name")]
        public string LgcName
        {
            get;
            set;
        }
    }
}
