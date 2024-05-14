using Mapster;
using MealPlanner.Application.DTOs;
using MealPlanner.Domain.Abstractions;
using MealPlanner.Domain.Entities;
using MediatR;

namespace MealPlanner.Application.Commands.Handlers
{
    public class AddMealCommandHandler : IRequestHandler<AddMealCommand, Guid>
    {
		private readonly IUnitOfWork _unitOfWork;
        public AddMealCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AddMealCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<Ingredient> ingredients = new();

                // Mapping DTO to Domain Entity.
                foreach(IngredientDTO ingredientDTO in request.meal.Recipe.Ingredients)
                {
                    var ingredient = ingredientDTO.Adapt<Ingredient>();
                    ingredients.Add(ingredient);
                }

                Recipe recipe = Recipe.Create(request.meal.Recipe.Id, ingredients);

                Meal meal = Meal.Create(
                    request.meal.Id, 
                    request.meal.MealName, 
                    request.meal.MealDescription, 
                    recipe
                    );

                Guid mealId = await _unitOfWork.Meal.Add(meal, cancellationToken);

                int rowsAdded = await _unitOfWork.Complete();

                return mealId;
            }
            catch (Exception ex)
            {
                // Log exception
                throw;
            }
        }
    }
}
