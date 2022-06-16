using Microsoft.AspNetCore.Builder;

namespace WinwinService.Base
{
    public class FromErpAuthenFilterMiddleware
    {
        public void Configure(IApplicationBuilder app)
        {
                //app.ApirAuthentication();
                app.UseMiddleware<FromErpAuthentication>();
            }
        
    }
}
