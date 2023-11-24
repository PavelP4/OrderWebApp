using AutoMapper;
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

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddControllers();

        services.AddDbContext<AppDbContext>(options => options
                .UseSqlServer(_environment.GetDbConnectionString())
                .UseLazyLoadingProxies());
        services.AddScoped(typeof(BaseRepository<>));

        services.AddTransient<OrderService>();
        services.AddTransient<StoreService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (_environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler(!_environment.IsProduction());

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthorization();

        app.UseMiddleware<KnownExceptionsHandlerMeddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
