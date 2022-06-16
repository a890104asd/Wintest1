using Hangfire;
using Hangfire.Dashboard;
using Hangfire.MySql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WinwinService.Base;
using WinwinService.Models;

namespace WinwinService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DatabaseConnection commonDbConnection = Configuration.GetSection("WinwinApiConfiguration").Get<WinwinApiConfiguration>().CommonDbConnection;
            string connectionString = $"server={commonDbConnection.ServerIp};uid={commonDbConnection.UserId};pwd={commonDbConnection.UserPwd};database={commonDbConnection.Database};Allow User Variables=True";

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options =>
            {
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.Formatting = Formatting.Indented;
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                 {
                     ApiResponse<object> apiResponse = new ApiResponse<object>()
                     {
                         Status = (int)EnumMasterErrorCode.DataFailed,
                         Error = new List<Errors>()
                     };

                     apiResponse.Error = actionContext.ModelState
                                 .Where(e => e.Value.Errors.Count > 0)
                                 .Select(e => new Errors
                                 {
                                     Key = e.Key.Replace("Data.",""),
                                     Code = "400039998",
                                     Message = e.Value.Errors.First().ErrorMessage
                                 }).ToList();
                     
                     return new BadRequestObjectResult(apiResponse);
                 };
            });
            services.AddHangfire(configuration => {
                configuration.UseStorage(
                    new MySqlStorage(
                        connectionString,
                        new MySqlStorageOptions
                        {
                            QueuePollInterval = TimeSpan.FromSeconds(10),
                            TablesPrefix = "hangfire_"
                        })
                    );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                WorkerCount = 1
            });
            app.UseHangfireDashboard("/Hangfire_Dashboard", new DashboardOptions() 
            {
                Authorization = new[] { new DashboardAuthorizeFilter() },
            });
            app.UseHangfireDashboard("/Developer_Dashboard", new DashboardOptions()
            {
                Authorization = new[] { new DashboardAuthorizeFilter() },
                IsReadOnlyFunc = (DashboardContext context) => true 
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
