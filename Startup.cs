using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using ToDoListKeevo.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Npgsql;
using Newtonsoft.Json;
using AutoMapper;


namespace ToDoListKeevo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //Método para configurar os serviços da aplicação
        public void ConfigureServices(IServiceCollection services)
        {   
            //Método para configurar o banco de dados PostgreSQL
            //ConnectionString definida no appsettings.json e no appsettings.Development.json
            services.AddDbContext<DataContext>(
                context => context.UseNpgsql(Configuration.GetConnectionString("PostgreConnection"))
            );

            //Método para configurar o AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Método para configurar o CORS
            services.AddControllers().AddNewtonsoftJson(
                options => options.SerializerSettings.ReferenceLoopHandling = 
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            /*Método para configurar o repositório.
            O AddScoped é um método que cria um novo escopo para cada requisição.*/
            services.AddScoped<IRepository, Repository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}