using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.AzureAppServices;
using OrderWebApp.Extentions;
using OrderWebApp.Infrastructure;
using OrderWebApp.Mappings;
using OrderWebApp.Repositories;
using OrderWebApp.Services;
using Microsoft.Extensions.Logging.AzureAppServices;

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

        //https://mderriey.com/2020/08/08/a-look-at-the-aspnet-core-logging-provider-for-app-service/
        services.PostConfigure<LoggerFilterOptions>(options =>
        {
            var originalRule = options.Rules.FirstOrDefault(x => x.ProviderName == typeof(FileLoggerProvider).FullName);
            if (originalRule != null)
            {
                options.Rules.Remove(originalRule);

                options.AddFilter<FileLoggerProvider>(category: null, level: LogLevel.Error);
                options.AddFilter<FileLoggerProvider>(category: "OrderWebApp", level: originalRule.LogLevel.Value);
            }
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        app.UseExceptionHandler(!_environment.IsProduction());

        if (_environment.IsDevelopment())
        {
            //app.UseDeveloperExceptionPage();

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthorization();

        app.UseMiddleware<KnownExceptionsHandlerMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
