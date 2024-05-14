using MealPlanner.Domain.Primitives;

namespace MealPlanner.Domain.Entities
{
    public sealed class Recipe : Entity
    {
        private Recipe(Guid id, List<Ingredient> ingredients) : base(id) 
        {
            Ingredients = ingredients;
        }

        public List<Ingredient> Ingredients { get; set; } = new();

        public static Recipe Create(Guid id, List<Ingredient> ingredients)
        {
            Recipe recipe = new Recipe (id, ingredients);

            return recipe;
        }
    }
}
