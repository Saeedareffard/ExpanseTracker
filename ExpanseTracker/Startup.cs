﻿using Application.Services;
using Domain.Interfaces;
using Infrastructure.Helpers;
using Infrastructure.Persistence.contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ExpanseTracker;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(Configuration);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Add services to the container.
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Expanse Tracker",
                Version = "v1"
            });
        });
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("ExpanseTrackerAzure"));
            options.UseSqlServer(b => b.MigrationsAssembly("ExpanseTracker"));
        });
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<GoalService>();
        services.AddTransient<TransactionService>();
        services.AddTransient<CategoryService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //app.UseAuthentication();
        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseRouting();
        //app.UseAuthorization();
        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    }
}