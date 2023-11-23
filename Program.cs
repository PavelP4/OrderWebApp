using Microsoft.EntityFrameworkCore;
using OrderWebApp;
using OrderWebApp.Infrastructure;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
        webBuilder.ConfigureAppConfiguration(confBuilder => confBuilder.AddEnvironmentVariables(prefix: "ASPNETCORE_"));
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