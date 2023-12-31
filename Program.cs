using Microsoft.EntityFrameworkCore;
using OrderWebApp;
using OrderWebApp.Infrastructure;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(builder => builder.AddAzureWebAppDiagnostics())
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
        webBuilder.ConfigureAppConfiguration(confBuilder => confBuilder.AddEnvironmentVariables());
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var environment = services.GetRequiredService<IWebHostEnvironment>();
    
    var dbContext = services.GetRequiredService<AppDbContext>();
    if (dbContext.Database.GetPendingMigrations().Any())
    {
        dbContext.Database.Migrate();
    }
}

host.Run();