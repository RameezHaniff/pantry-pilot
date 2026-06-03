namespace PantryPilot.Api.Models.Responses
{
    public class OptimizationResponse
    {
        public required List<RecipeResult> Recipes { get; set; }

        public int MaxPeopleFed { get; set; }
    }

    public class RecipeResult
    {
        public required string Name { get; set; }
        public int Quantity { get; set; }
    }
}
