using Mapster;
using MealPlanner.Domain.Abstractions;
using MealPlanner.Domain.Entities;
using MealPlanner.Infrastructure.Data;
using MealPlanner.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MealPlanner.Infrastructure.Repositories
{
    internal class MealRepository : IMealRepository
    {
        private readonly MealPlannerDbContext _context;
        public MealRepository(MealPlannerDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Add(Meal meal, CancellationToken cancellation)
        {
            try
            {
                MMeal mMeal = meal.Adapt<MMeal>();

                var id = await _context.Meals.AddAsync(mMeal);

                return mMeal.Id;
            }
            catch (Exception ex)
            {
                // throw exception
                throw;
            }
        }

        public Task<bool> Delete(Guid id, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }

        public async Task<Meal> GetById(Guid id, CancellationToken cancellation)
        {
            MMeal? mMeal = await _context.Meals
                .Include(m => m.Recipe)
                .ThenInclude(r => r.Ingredients)
                .FirstOrDefaultAsync(m => m.Id == id);

            if(mMeal is null)
            {
                throw new ArgumentNullException(nameof(MMeal));
            }

            List<Ingredient> ingredients = new();

            // Mapping Model to Domain Entity.
            foreach (MIngredient mIngredient in mMeal.Recipe.Ingredients)
            {
                var ingredient = mIngredient.Adapt<Ingredient>();
                ingredients.Add(ingredient);
            }
            Recipe recipe = Recipe.Create(mMeal.Recipe.Id, ingredients);

            Meal meal = Meal.Create(mMeal.Id, mMeal.MealName, mMeal.MealDescription, recipe);

            return meal;
        }

        public Task<Meal> Update(Meal meal, CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
