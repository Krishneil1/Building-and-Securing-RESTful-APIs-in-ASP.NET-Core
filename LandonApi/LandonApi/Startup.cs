using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandonApi.Filters;
using LandonApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSwag.AspNetCore;
using Microsoft.EntityFrameworkCore;
using LandonApi.DBContext;

namespace LandonApi
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Basic idea you need to add controler and context to StartUp class to be utilised
            //services.AddDbContext<HotelApiDbContext>(
            //    options => options.UseSqlServer("HotelApiDbContext")
            //    );

            //services.AddDbContext<HotelApiDbContext>(options =>
            //{

            //    //var cnn = _configuration.GetConnectionString("HotelApiDbContext");
            //    //options.UseSqlServer(cnn);


            //});
            services.AddDbContext<HotelApiDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HotelApiDbContext")));



            services.Configure<HotelInfo>(
                Configuration.GetSection("Info"));//gets infor from appsetting.json
            services
                .AddMvc(options =>
                {
                    options.Filters.Add<JsonExceptionFilter>();

                    /*
                     * The following code is never going to be used in prod and used as an example if using this you
                     * will have to comment out app.UseHttpsRedirection();
                     */
                    options.Filters
                        .Add<RequireHttpsOrCloseAttribute>();
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyApp",
                    policy => policy
                                //.WithOrigins("https://example.com"));
                                .AllowAnyOrigin());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwaggerUi3WithApiExplorer(options =>
                {
                    options.GeneratorSettings
                    .DefaultPropertyNameHandling
                    = NJsonSchema.PropertyNameHandling.CamelCase;
                });
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("AllowMyApp");
            app.UseHttpsRedirection();
            app.UseMvc();
            
        }
    }
}
