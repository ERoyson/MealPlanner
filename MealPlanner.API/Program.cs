using MealPlanner.Application;
using MealPlanner.Infrastructure;
using MealPlanner.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string configFilePath;
string hostEnvironment;
if(builder.Environment.IsDevelopment())
{
    configFilePath = "Config/appsettings.Development.json";
    hostEnvironment = "Development";
}
else if(builder.Environment.IsProduction())
{
    configFilePath = "Config/appsettings.Production.json";
    hostEnvironment = "Production";
}
else
{
    configFilePath = "Config/appsettings.json";
    hostEnvironment = "notspecified";
}

IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(configFilePath, optional: false, reloadOnChange: true);

IConfiguration configuration = configurationBuilder.Build();

builder.Services.Configure<AppsettingsModel>(configuration.GetSection("ConnectionStrings"));

builder.Services
    .AddInfrastructure(configuration)
    .AddApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Migrations at runtime
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<MealPlannerDbContext>();
    context.Database.Migrate();
}

    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
    //    app.UseSwagger();
    //    app.UseSwaggerUI();
    //}

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
