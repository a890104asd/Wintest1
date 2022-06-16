using Newtonsoft.Json;
using System.Collections.Generic;

namespace WinwinService.Models
{
    public class GetUserInfoData
    {
        [JsonProperty("users")]
        public List<UserInfo> Users { get; set; }
    }

    public class UserInfo
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("user_account")]
        public string UserName { get; set; }
        [JsonProperty("warehouse_id")]
        public string WarehouseId { get; set; }
        [JsonProperty("warehouse_alias")]
        public string WarehouseAlias { get; set; }
    }
}
