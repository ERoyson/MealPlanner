namespace MealPlanner.Domain.Enums;

public class FoodGroup
{
    public enum FoodGroups
    {
        Unspecified = 0,
        Dairy,
        Protein,
        Fruits,
        Vegetables,
        Grains, //Grains: Bread, rice, pasta, oats, quinoa, barley, and cereal.
        Legumes, //Legumes: Beans (black beans, kidney beans, chickpeas), lentils, peas, and soy products
        NutsAndSeeds,
        OilsAndFats,
        Beverages,
        Snacks
    }
}
