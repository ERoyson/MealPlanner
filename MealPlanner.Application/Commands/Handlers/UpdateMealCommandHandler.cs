using Mapster;
using MealPlanner.Application.DTOs;
using MealPlanner.Domain.Abstractions;
using MealPlanner.Domain.Entities;
using MediatR;

namespace MealPlanner.Application.Commands.Handlers;

public class UpdateMealCommandHandler : IRequestHandler<UpdateMealCommand, bool>
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateMealCommandHandler(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Recipe recipe = MapRecipeDTO_To_RecipeEntity(request.MealDTO.Recipe);
            
            Meal meal = Meal.Create(
                request.MealDTO.Id, 
                request.MealDTO.MealName, 
                request.MealDTO.MealDescription,
                recipe
            );

            await _unitOfWork.Meal.Update(meal, cancellationToken);

            bool rowsChanged = await _unitOfWork.Complete() > 0;

            return rowsChanged;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private List<Ingredient> MapIngredientDTO_To_IngredientEntity(List<IngredientDTO> ingredientDTOs)
    {
        List<Ingredient> ingredients = new();
        foreach (var ingredientDTO in ingredientDTOs)
        {
            var ingredient = ingredientDTO.Adapt<Ingredient>();
            ingredients.Add(ingredient);
        }

        return ingredients;
    }

    private Recipe MapRecipeDTO_To_RecipeEntity(RecipeDTO recipeDTO)
    {
        List<Ingredient> ingredients = MapIngredientDTO_To_IngredientEntity(recipeDTO.Ingredients);

        Recipe recipe = Recipe.Create(recipeDTO.Id, ingredients);

        return recipe;
    }
}
