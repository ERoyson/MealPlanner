using MealPlanner.Application;
using MealPlanner.Domain.Abstractions;
using MealPlanner.Infrastructure;
using MealPlanner.Infrastructure.Data;
using MealPlanner.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

string cs = configuration.GetConnectionString("MySqlConnectionString");

builder.Services.Configure<AppsettingsModel>(configuration.GetSection("ConnectionStrings"));

builder.Services.AddDbContext<MealPlannerDbContext>(options => 
    options.UseMySQL(configuration.GetConnectionString("MySqlConnectionString")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(MediatREntryPoint).Assembly));

MapsterConf.Init();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
