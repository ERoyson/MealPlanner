using MealPlanner.Domain.Entities;

namespace MealPlanner.Infrastructure.Models
{
    public class MRecipe
    {
        public Guid Id { get; set; }
        public List<MIngredient> Ingredients { get; set; } = new();
    }
}
