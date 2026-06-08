namespace PantryPilot.Api.Entities
{
    public class Recipe
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Feeds { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }

}
