using MediatR;
using MediatrCQRS.Interfaces;
using MediatrCQRS.Logic.Behaviours;
using MediatrCQRS.Logic.Extensions;
using MediatrCQRS.Logic.Filters;
using MediatrCQRS.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MediatrCQRS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //TODO: #0 Demo purpose:
            //<<ToDo>> WebApi with End Points to get and insert ToDo items
            //Simple in-memory storage (Repository with CRUD)


            //Enable ResponseMapping Filter for controllers
            services.AddControllers(o => o.Filters.Add(typeof(ResponseMappingFilter)));

            //TODO: #1 Swagger configuration (API endopints)            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRS with MediatR - MS Tech Summit 2022", Version = "v1" });
            });

            //Register our DI classes
            services.AddSingleton<IToDoRepository, ToDoRepository>();

            //TODO: #2 Register MadiatR library within our assembly to mediate between objects
            services.AddMediatR(typeof(Startup).Assembly);

            //Enable MediatR Validation
            services.AddValidators();

            //Enable memory cache
            services.AddMemoryCache();

            //TODO: #9 Setup of MediatR Behaviours - order is important!
            //Execution determined by order
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ElapsedTimeBehaviour<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Enable Swagger on PROD env. for a demo purpose
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Expert Summit 2022 DEMO"));
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
