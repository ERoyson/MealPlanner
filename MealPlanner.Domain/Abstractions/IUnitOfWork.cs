namespace MealPlanner.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IMealRepository Meal {  get; }

        Task<int> Complete(); // save changes in dbContext
        void Dispose();
        void Clear();

    }
}
