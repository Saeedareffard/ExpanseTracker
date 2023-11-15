using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Helpers;
using Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration
{
    public static class Config 
    {
        public static IServiceCollection AddInfrastructureInjections(this IServiceCollection services)
        {
            services.AddScoped<ExceptionMiddleware>().AddTransient<ISerializerService,NewtonSoftService>();
            return services;
        }

        public static IApplicationBuilder AddExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }
    }
}
