using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MMT.CustomerOrder.Api;
using MMT.CustomerOrder.Core;
using MMT.CustomerOrder.DataSource;
using MMT.CustomerOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMT.CustomerOrder
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
            services.AddControllers();
           services.AddDbContext<MMTDigitalContext>(options =>
           options.UseSqlServer(Configuration["MMTDigitalTestContext"])
           .EnableSensitiveDataLogging()
           );
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUrlBuilder,UrlBuilder>();
            services.AddScoped<IUserApi, UserApi>();
            services.AddScoped<IOrderApi, OrderApi>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
