namespace MealPlanner.Application.DTOs
{
    public class IngredientDTO
    {
        public Guid Id { get; set; }
        public string IngredientName { get; set; }
        public double Amount { get; set; }
        public int Unit { get; set; }
        public int FoodGroup { get; set; }
    }
}
