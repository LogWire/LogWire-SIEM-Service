using LogWire.SIEM.Service.Data.Context;
using LogWire.SIEM.Service.Data.Model;
using LogWire.SIEM.Service.Data.Repository;
using LogWire.SIEM.Service.Middleware;
using LogWire.SIEM.Service.Services.API;
using LogWire.SIEM.Service.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LogWire.SIEM.Service
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<SIEMDataContext>(opt => opt.UseMySql("server=localhost;port=3306;database=lw_siem;uid=lwuser;password=lwpassword"));

            services.AddScoped<IDataRepository<SIEMUserEntry>, SIEMUserRepository>();

            services.AddGrpc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseApiTokenAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapGrpcService<SIEMServiceServer>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
