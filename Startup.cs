﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderWebApp.Extentions;
using OrderWebApp.Infrastructure;
using OrderWebApp.Mappings;
using OrderWebApp.Repositories;
using OrderWebApp.Services;

namespace OrderWebApp;

public class Startup
{
    public readonly IConfiguration _configuration;
    public readonly IWebHostEnvironment _environment;

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var mapper = new MapperConfiguration(x =>
        {
            x.AddProfile(new OrderMapping());
            x.AddProfile(new StoreMapping());
        }).CreateMapper();

        services.AddSingleton(mapper);

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<AppDbContext>(options => 
            options
                .UseSqlServer(_configuration.GetDatabaseConnection())
                .UseLazyLoadingProxies());

        services.AddScoped(typeof(BaseRepository<>));

        services.AddTransient<OrderService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (_environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
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