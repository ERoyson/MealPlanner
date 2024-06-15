using Mapster;
using MealPlanner.Domain.Entities;
using MealPlanner.Infrastructure.Models;

namespace MealPlanner.Infrastructure;

public class MapsterConf
{
    public static void Init()
    {
        // Map configuration here

        // Map from Model to Entity
        TypeAdapterConfig<MIngredient, Ingredient>.NewConfig()
            .MapWith(src => Ingredient.Create(src.Id, src.IngredientName, src.Amount, src.Unit, src.FoodGroup));

        TypeAdapterConfig<MRecipe, Recipe>.NewConfig()
            .MapWith(src => Recipe.Create(src.Id, src.Ingredients.Adapt<List<Ingredient>>()));

        TypeAdapterConfig<MMeal, Meal>.NewConfig()
            .MapWith(src => Meal.Create(src.Id, src.MealName, src.MealDescription, src.Recipe.Adapt<Recipe>()));
        
        // Ensure the configurations are applied globally
        TypeAdapterConfig.GlobalSettings.Compile();
    }
}

