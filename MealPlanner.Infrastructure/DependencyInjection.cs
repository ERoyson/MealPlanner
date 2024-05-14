
using MealPlanner.Domain.Abstractions;
using MealPlanner.Infrastructure.Data;
using MealPlanner.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MealPlanner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MealPlannerDbContext>(options =>
            options.UseMySQL(configuration.GetConnectionString("MySqlConnectionString")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        MapsterConf.Init();

        return services;
    }

}
