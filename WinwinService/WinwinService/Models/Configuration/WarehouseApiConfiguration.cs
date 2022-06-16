namespace WinwinService.Models
{
    public class WinwinApiConfiguration
    {
        public ApiSettings ApiSettings { get; set; }
        public DatabaseConnection CommonDbConnection { get; set; }
        public EnvironmentSettings EnvironmentSettings { get; set; }
        public UpdateSystemSettings UpdateSystemSettings { get; set; }
        public ApiTokens ApiTokens { get; set; }
    }
}
