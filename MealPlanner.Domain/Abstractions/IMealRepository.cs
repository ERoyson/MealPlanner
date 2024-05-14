using MealPlanner.Domain.Entities;

namespace MealPlanner.Domain.Abstractions
{
    public interface IMealRepository
    {
        public Task<Meal> GetById(Guid id, CancellationToken cancellation);
        public Task<Guid> Add(Meal meal, CancellationToken cancellation);
        public Task<bool> Delete(Guid id, CancellationToken cancellation);
        public Task<Meal> Update(Meal meal, CancellationToken cancellation);

    }
}
