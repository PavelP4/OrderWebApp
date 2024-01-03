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
    var logger = services.GetRequiredService<ILogger<Program>>();

    logger.LogInformation("Start db migration check.");

    var dbContext = services.GetRequiredService<AppDbContext>();
    if (dbContext.Database.GetPendingMigrations().Any())
    {
        dbContext.Database.Migrate();
        logger.LogInformation("Db migration is applied successfully.");
    }

    logger.LogInformation("End db migration check.");
}

host.Run();