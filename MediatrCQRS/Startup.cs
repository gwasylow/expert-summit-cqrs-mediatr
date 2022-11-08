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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Enable ResponseMapping Filet for controllers
            services.AddControllers(o => o.Filters.Add(typeof(ResponseMappingFilter)));

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRS with MediatR", Version = "v1" });
            });

            //Register our DI classes
            services.AddSingleton<IToDoRepository, ToDoRepository>();

            //Register MadiatR
            services.AddMediatR(typeof(Startup).Assembly);

            //Enable MediatR Validation
            services.AddValidators();

            //Enable memory cache
            services.AddMemoryCache();

            //Setup MEdiatR Behaviours - order is important!
            //Execution determined by order
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ElapsedTimeBehaviour<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRS with MediatR v1"));
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
