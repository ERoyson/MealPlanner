using Mapster;
using MealPlanner.Domain.Entities;
using MealPlanner.Domain.Enums;
using MealPlanner.Infrastructure.Models;
using System.Net.NetworkInformation;

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
            Assert.IsInstanceOfType(meal, typeof(Meal));
            Assert.AreEqual(meal.MealName, "Omelette");
            Assert.AreEqual(meal.MealDescription, "Egg in a pan");
            Assert.AreEqual(meal.Recipe, recipe);
            Assert.AreEqual(meal.Recipe.Ingredients.First().IngredientName, "Egg");

            Assert.AreEqual(meal.Recipe.Ingredients.First().FoodGroup, (int)FoodGroup.FoodGroups.Protein);
            Assert.AreEqual(meal.Recipe.Ingredients.First().Unit, (int)Unit.Units.pcs);
        }

        [TestMethod]
        public void Mapping_MMeal_To_Meal_Should_Map_All_Data()
        {
            // Arrange
            Guid mealId = Guid.NewGuid();
            string mealName = "Omelette";
            string mealDescription = "Egg in a pan";
            Guid recipeId = Guid.NewGuid();
            Guid ingredientId = Guid.NewGuid();

            MRecipe recipe = new MRecipe()
            {
                Id = recipeId,
                Ingredients = new List<MIngredient>()
                {
                    new MIngredient()
                    {
                        Id = ingredientId,
                        IngredientName = "Egg",
                        Amount = 1,
                        Unit = (int)Unit.Units.pcs,
                        FoodGroup = (int)FoodGroup.FoodGroups.Protein
                    }
                }
            };

            MMeal mMeal = new MMeal
            {
                Id = mealId,
                MealName = mealName,
                MealDescription = mealDescription,
                Recipe = recipe
            };

            // Map from Model to Entity
            TypeAdapterConfig<MIngredient, Ingredient>.NewConfig()
                .MapWith(src => Ingredient.Create(src.Id, src.IngredientName, src.Amount, src.Unit, src.FoodGroup));

            TypeAdapterConfig<MRecipe, Recipe>.NewConfig()
                .MapWith(src => Recipe.Create(src.Id, src.Ingredients.Adapt<List<Ingredient>>()));

            TypeAdapterConfig<MMeal, Meal>.NewConfig()
                .MapWith(src => Meal.Create(src.Id, src.MealName, src.MealDescription, src.Recipe.Adapt<Recipe>()));

            TypeAdapterConfig.GlobalSettings.Compile();

            // Act
            Meal meal = mMeal.Adapt<Meal>();

            // Assert
            Assert.AreEqual(mMeal.Id, meal.Id);
            Assert.AreEqual(mMeal.MealName, meal.MealName);
            Assert.AreEqual(mMeal.MealDescription, meal.MealDescription);
            Assert.AreEqual(mMeal.Recipe.Id, meal.Recipe.Id);
            Assert.AreEqual(mMeal.Recipe.Ingredients.Count, meal.Recipe.Ingredients.Count);

            var mIngredient = mMeal.Recipe.Ingredients[0];
            var ingredient = meal.Recipe.Ingredients[0];

            Assert.AreEqual(mIngredient.Id, ingredient.Id);
            Assert.AreEqual(mIngredient.IngredientName, ingredient.IngredientName);
            Assert.AreEqual(mIngredient.Amount, ingredient.Amount);
            Assert.AreEqual(mIngredient.Unit, ingredient.Unit);
            Assert.AreEqual(mIngredient.FoodGroup, ingredient.FoodGroup);
        }

    }
}