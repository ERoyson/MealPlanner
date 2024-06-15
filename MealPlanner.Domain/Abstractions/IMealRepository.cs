using MealPlanner.Domain.Entities;

namespace MealPlanner.Domain.Abstractions
{
    public interface IMealRepository
    {
        // GET all, Get random week, Get by category, 
        // Get by ingredient/ingredients. (if you select a couple of ingredients and search for meals with that in it).
        // 
        public Task<List<Meal>> GetAll(CancellationToken cancellation);
        public Task<Meal> GetById(Guid id, CancellationToken cancellation);
        public Task<Guid> Add(Meal meal, CancellationToken cancellation);
        public Task Delete(Guid id, CancellationToken cancellation);
        public Task Update(Meal meal, CancellationToken cancellation);

    }
}
