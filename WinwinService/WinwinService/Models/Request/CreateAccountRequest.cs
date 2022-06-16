using Newtonsoft.Json;

namespace WinwinService.Models
{
    public class CreateAccountRequest
    {
        /// <summary>
        /// 電子郵件
        /// </summary>
        [JsonProperty("email")]
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// 密碼
        /// </summary>
        [JsonProperty("password")]
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// 姓名
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 年齡
        /// </summary>
        [JsonProperty("age")]
        public int Age
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 性別
        /// </summary>
        [JsonProperty("gender")]
        public string Gender
        {
            get;
            set;
        }

        /// <summary>
        /// 所在地區
        /// </summary>
        [JsonProperty("area")]
        public string Area
        {
            get;
            set;
        }
    }
}
