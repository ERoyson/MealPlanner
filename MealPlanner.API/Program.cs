using MealPlanner.Application;
using MealPlanner.Infrastructure;

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

builder.Services
    .AddInfrastructure(configuration)
    .AddApplication();

builder.Services.Configure<AppsettingsModel>(configuration.GetSection("ConnectionStrings"));

builder.Services.AddControllers();
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
