using Microsoft.AspNetCore.Builder;

namespace WinwinService.Base
{
    public class From3PartyAuthenFilterMiddleware
    {
        public void Configure(IApplicationBuilder app)
        {
                //app.ApirAuthentication();
                app.UseMiddleware<From3PartyAuthentication>();
            }
        
    }
}
