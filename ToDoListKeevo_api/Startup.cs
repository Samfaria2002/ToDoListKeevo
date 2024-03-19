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
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Npgsql;
using Newtonsoft.Json;
using AutoMapper;
using ToDoListKeevo_api.Data;


namespace ToDoListKeevo_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {   
            //Método para configurar o banco de dados PostgreSQL
            //ConnectionString definida no appsettings.json e no appsettings.Development.json
            services.AddDbContext<DataContext>(
                context => context.UseNpgsql(Configuration.GetConnectionString("PostgreConnection"))
            );

            //Método para configurar o AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Método que configura o serializador JSON, para ignorar loops de referência.
            services.AddControllers().AddNewtonsoftJson(
                options => options.SerializerSettings.ReferenceLoopHandling = 
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });


            /*Método para configurar o repositório.
            O AddScoped é um método que cria um novo escopo para cada requisição.*/
            services.AddScoped<IRepository, Repository>();

            //Método para configurar a documentação da API via Swagger
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //Método para configurar o CORS
            //CORS é um mecanismo que permite que recursos restritos em uma página da web sejam solicitados por outro domínio fora do domínio de origem.
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
            }
            
            if (env.IsDevelopment()){
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //Método para configurar o CORS
            //Esse método permite que a API seja acessada por qualquer origem, método e cabeçalho.
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}