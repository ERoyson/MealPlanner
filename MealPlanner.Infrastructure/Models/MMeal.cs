using MealPlanner.Domain.Entities;

namespace MealPlanner.Infrastructure.Models
{
    public class MMeal
    {
        public Guid Id { get; set; }
        public string MealName { get; set; }
        public string? MealDescription { get; set; }
        public MRecipe Recipe { get; set; }
    }
}
