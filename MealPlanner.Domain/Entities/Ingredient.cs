using MealPlanner.Domain.Enums;
using MealPlanner.Domain.Primitives;

namespace MealPlanner.Domain.Entities
{
    public sealed class Ingredient : Entity
    {
        public Ingredient(Guid id, string ingredientName, double amount, int unit, int foodGroup) 
            : base(id)
        {
            IngredientName = ingredientName;
            Amount = amount;
            Unit = unit;
            FoodGroup = foodGroup;
        }

        public string IngredientName { get; private init; }
        public double Amount { get; private init; }
        public int Unit { get; private init; }
        public int FoodGroup { get; private init; }

        public static Ingredient Create(Guid id, string ingredientName, double amount, int unit, int foodGroup)
        {
            Ingredient ingredient = new(id, ingredientName, amount, unit, foodGroup);

            return ingredient;
        }
    }
}
