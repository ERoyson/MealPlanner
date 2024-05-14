using MealPlanner.Domain.Primitives;

namespace MealPlanner.Domain.Entities
{
    public sealed class Meal : Entity // Aggregate root?
    {
        private Meal(Guid id, string mealName, string? mealDescription, Recipe recipe) : base(id)
        {
            MealName = mealName;
            MealDescription = mealDescription;
            Recipe = recipe;
        }
        public string MealName { get; private set; }
        public string? MealDescription { get; private set; }
        public Recipe Recipe { get; private set; }

        // Create meal.
        public static Meal Create(Guid id, string mealName, string? mealDescription, Recipe recipe)
        {
            Meal meal = new (id, mealName, mealDescription, recipe);

            return meal;
        }

        public static void AddIngredientToRecipe(Meal meal, Ingredient ingredient)
        {
            meal.Recipe.Ingredients.Add(ingredient);
        }
    }
}
