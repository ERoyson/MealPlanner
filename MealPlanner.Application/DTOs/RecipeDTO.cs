namespace MealPlanner.Application.DTOs
{
    public class RecipeDTO
    {
        public Guid Id { get; set; }
        public List<IngredientDTO> Ingredients { get; set; } = new();
    }
}
