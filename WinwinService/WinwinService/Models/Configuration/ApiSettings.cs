namespace WinwinService.Models
{
    public class ApiSettings
    {
        public ApiUrl GetApiUrl(int Env)
        {
            ApiUrl urlinfo = null;

            switch (Env)
            {
                case (1):
                    urlinfo = DevApiUrl;
                    break;
                case (2):
                    urlinfo = UatApiUrl;
                    break;

                case (3):
                    urlinfo = ProductionApiUrl;
                    break;
                default:
                    urlinfo = ProductionApiUrl;
                    break;
            }
            return urlinfo;
        }


        public ApiUrl DevApiUrl { get; set; }
        public ApiUrl UatApiUrl { get; set; }
        public ApiUrl ProductionApiUrl { get; set; }
    }

    public class ApiUrl
    {
        public string AccountService { get; set; }
        public string ErpService { get; set; }
        public string TrackService { get; set; }
        public string StorageService { get; set; }
    }
}
