using Microsoft.AspNetCore.Builder;
using System;

namespace WinwinService.Base
{
    public class FromToolAuthenFilterMiddleware
    {
        public void Configure(IApplicationBuilder app)
        {
                //app.ApirAuthentication();
                app.UseMiddleware<FromToolAuthentication>();
            }
        
    }
}
