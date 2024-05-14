namespace MealPlanner.Application.DTOs
{
    public class MealDTO
    {
        public Guid Id { get; set; }
        public string MealName { get; set; }
        public string? MealDescription { get; set; }
        public RecipeDTO Recipe { get; set; }
    }
}
