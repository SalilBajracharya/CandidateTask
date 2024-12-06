using CandidateTask.API;
using CandidateTask.Application;
using CandidateTask.Infrastructure;
using CandidateTask.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Error("init main");

try
{
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddApplicationServices();
    builder.Services.AddAPIServices();

    var app = builder.Build();

    //Applying Migrations
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }
        else
        {
            dbContext.Database.EnsureCreated();
        }
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    app.UseDeveloperExceptionPage();

    app.Run();
}
catch (Exception ex) {
    logger.Error(ex);
} finally
{
    LogManager.Shutdown();
}