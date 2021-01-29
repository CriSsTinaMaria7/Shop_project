using System.Linq;
using API.Errors;
using API.Extensions;
using API.Middleware;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString
            ("DefaultConnection")));
            //obtinere acces la extensia pentru IServiceCollection
            services.AddApplicationServices();
            services.AddSwaggerDocumentation();
            services.AddCors(opt =>
            {
                //se specifică aplicației clientului că, 
                //dacă rulează pe un port nesigur, 
                //nu va fi returnat un antet care sa permite browserului 
                // să afișeze aceste informații
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("https://loacalhost:4200");
                });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");       
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            // Swagger va permite navigarea la o pagină web, 
            // care va afișa toate punctele noastre finale API
            app.UseSwaggerDocumentation();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           

        }
    }
}
