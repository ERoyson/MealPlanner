using MealPlanner.Domain.Entities;
using MealPlanner.Domain.Enums;

namespace Domain.Test
{
    [TestClass]
    public class MealTest
    {
        [TestMethod]
        public void MealDotCreateShouldCreateMeal()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string mealName = "Omelette";
            string? mealDescription = "Egg in a pan";
            Recipe recipe = Recipe.Create(
                Guid.NewGuid(),
                new List<Ingredient>
                {
                    Ingredient.Create(
                        Guid.NewGuid(),
                        "Egg", 
                        (double)1,
                        (int)Unit.Units.pcs,
                        (int)FoodGroup.FoodGroups.Protein)
                }
            );

            // Act
            Meal meal = Meal.Create(
                Guid.NewGuid(), 
                mealName, 
                mealDescription, 
                recipe);

            // Assert
            Assert.IsInstanceOfType(meal, typeof( Meal ));
            Assert.AreEqual(meal.MealName, "Omelette");
            Assert.AreEqual(meal.MealDescription, "Egg in a pan");
            Assert.AreEqual(meal.Recipe, recipe);
            Assert.AreEqual(meal.Recipe.Ingredients.First().IngredientName, "Egg");

            Assert.AreEqual(meal.Recipe.Ingredients.First().FoodGroup, (int)FoodGroup.FoodGroups.Protein);
            Assert.AreEqual(meal.Recipe.Ingredients.First().Unit, (int)Unit.Units.pcs);
        }
    }
}