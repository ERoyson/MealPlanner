using MealPlanner.Domain.Abstractions;
using MealPlanner.Infrastructure.Data;

namespace MealPlanner.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MealPlannerDbContext _context;
        public IMealRepository Meal { get; private set; }

        public UnitOfWork(MealPlannerDbContext context)
        {
            _context = context;
            Meal = new MealRepository(_context);
        }

        public void Clear()
        {
            _context.ChangeTracker.Clear();
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
