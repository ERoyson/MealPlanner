using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MealPlanner.Infrastructure.Data;

public class MealPlannerDbContextFactory : IDesignTimeDbContextFactory<MealPlannerDbContext>
{
    MealPlannerDbContext IDesignTimeDbContextFactory<MealPlannerDbContext>.CreateDbContext(string[] args)
    {
        // During development only to run migrations to database.

        string connectionString = "server=localhost;port=3306;database=MealPlanner;Uid=root;Pwd=rootadmin;";

        var optionsBuilder = new DbContextOptionsBuilder<MealPlannerDbContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return new MealPlannerDbContext(optionsBuilder.Options, null);
    }
}
