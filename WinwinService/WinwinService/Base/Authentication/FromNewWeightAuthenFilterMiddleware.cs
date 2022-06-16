using Microsoft.AspNetCore.Builder;

namespace WinwinService.Base
{
    public class FromNewWeightAuthenFilterMiddleware
    {
        public void Configure(IApplicationBuilder app)
        {
                //app.ApirAuthentication();
                app.UseMiddleware<FromNewWeightAuthentication>();
            }
        
    }
}
