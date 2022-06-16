namespace WinwinService.Models
{
    public class ApiRequest<T> where T : class
    {
        public T Data { get; set; }
    }
}
