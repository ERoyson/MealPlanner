using MealPlanner.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MealPlanner.Infrastructure.Data
{
    public class MealPlannerDbContext : DbContext
    {
        private readonly IOptions<AppsettingsModel> _config;

        public MealPlannerDbContext(DbContextOptions<MealPlannerDbContext> options, IOptions<AppsettingsModel> config) : base(options)
        {
            _config = config;
        } // kommenterade ut denna konstruktorn vid initiering av DB (Skapad i Package Manager Console)

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                _config.Value.MySqlConnectionString,
                ServerVersion.AutoDetect(_config.Value.MySqlConnectionString)
            );
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<MMeal> Meals { get; set; }
        public DbSet<MRecipe> Recipes { get; set; }
        public DbSet<MIngredient> Ingredients { get; set;}
    }
}
