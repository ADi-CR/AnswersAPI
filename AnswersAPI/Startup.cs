using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnswersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AnswersAPI
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

            //agregar la cadena de conexi?n para el proyecto. 
            //TODO: debemos guardar esta cadena por medio de usersecrets.json
            //y no por medio de appsettings.json
                       
            var conn = @"SERVER=.;DATABASE=AnswersDB; User Id=AnswersUser; Password=1234";

            services.AddDbContext<AnswersDBContext>(options => options.UseSqlServer(conn));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AnswersAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnswersAPI v1"));
            }

            app.UseRouting();

            app.UseAuthorization();
            
            //TODO: revisar si hace falta alguna config para uso de JWT

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
