using System;
using MediatrCQRS.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MediatrCQRS.Logic.Extensions
{
    public static class ValidationExtensions
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.Scan(scan => scan
              .FromAssemblyOf<IValidationHandler>()
                .AddClasses(classes => classes.AssignableTo<IValidationHandler>())
                  .AsImplementedInterfaces()
                  .WithTransientLifetime());
        }
    }
}
