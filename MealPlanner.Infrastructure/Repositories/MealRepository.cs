﻿using Mapster;
using MealPlanner.Domain.Abstractions;
using MealPlanner.Domain.Entities;
using MealPlanner.Infrastructure.Data;
using MealPlanner.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MealPlanner.Infrastructure.Repositories;

internal class MealRepository : IMealRepository
{
    private readonly MealPlannerDbContext _context;
    public MealRepository(MealPlannerDbContext context)
    {
        _context = context;
    }
    public async Task<List<Meal>> GetAll(CancellationToken cancellation)
    {
        try
        {
            List<MMeal> mMeals = await _context.Meals
                .Include(m => m.Recipe).ThenInclude(r => r.Ingredients)
                .ToListAsync(cancellation);

            List<Meal> meals = mMeals.Adapt<List<Meal>>();

            return meals;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public async Task<Guid> Add(Meal meal, CancellationToken cancellation)
    {
        try
        {
            MMeal mMeal = meal.Adapt<MMeal>();

            var id = await _context.Meals.AddAsync(mMeal, cancellation);

            return mMeal.Id;
        }
        catch (Exception ex)
        {
            // throw exception
            throw;
        }
    }

    public async Task Delete(Guid id, CancellationToken cancellation)
    {
        try
        {
            MMeal? mMeal = await _context.Meals
            .Include(m => m.Recipe)
            .ThenInclude(r => r.Ingredients)
            .FirstOrDefaultAsync(m => m.Id == id, cancellation);

            if(mMeal is null)
            {
                throw new Exception("Meal not found");
            }

            _context.Ingredients.RemoveRange(mMeal.Recipe.Ingredients);
            _context.Recipes.Remove(mMeal.Recipe);
            _context.Meals.Remove(mMeal);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<Meal> GetById(Guid id, CancellationToken cancellation)
    {
        try
        {
            MMeal? mMeal = await _context.Meals
            .Include(m => m.Recipe)
            .ThenInclude(r => r.Ingredients)
            .FirstOrDefaultAsync(m => m.Id == id, cancellation);

            if (mMeal is null)
            {
                throw new ArgumentNullException(nameof(MMeal));
            }

            Meal meal = mMeal.Adapt<Meal>();

            // code below can be removed if the GetById works without any missing data.
            //List<Ingredient> ingredients = new();

            //// Mapping Model to Domain Entity.
            //foreach (MIngredient mIngredient in mMeal.Recipe.Ingredients)
            //{
            //    var ingredient = mIngredient.Adapt<Ingredient>();
            //    ingredients.Add(ingredient);
            //}
            //Recipe recipe = Recipe.Create(mMeal.Recipe.Id, ingredients);

            //Meal meal = Meal.Create(mMeal.Id, mMeal.MealName, mMeal.MealDescription, recipe);

            return meal;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task Update(Meal meal, CancellationToken cancellation)
    {
        try
        {
            var oldMeal = await _context.Meals
            .Include(m => m.Recipe)
            .ThenInclude(r => r.Ingredients).AsNoTracking()
            .SingleOrDefaultAsync(m => m.Id == meal.Id, cancellation);

            if (oldMeal is null)
            {
                throw new Exception("Meal not found");
            }

            MMeal newMeal = meal.Adapt<MMeal>();

            newMeal = newMeal.Adapt(oldMeal);

            _context.Meals.Update(newMeal);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
