using CandidateTask.API.Filters;
using CandidateTask.Application;
using CandidateTask.Infrastructure;
using CandidateTask.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionHandlerFilters>();
});

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