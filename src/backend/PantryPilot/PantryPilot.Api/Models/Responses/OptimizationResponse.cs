namespace PantryPilot.Api.Models.Responses
{
    public class OptimizationResponse
    {
        public required List<RecipeResult> Recipes { get; set; }

        public int MaxPeopleFed { get; set; }

        public required List<UnusedIngredients> UnusedIngredients { get; set; }
    }

    public class RecipeResult
    {
        public required string Name { get; set; }
        public int Quantity { get; set; }
        public required List<RecipeIngredientResult> Ingredients { get; set; }

    }

    public class RecipeIngredientResult
    {
        public required string Name { get; set; }
        public int QuantityPerServing { get; set; }
        public int TotalUsed { get; set; }
    }

    public class UnusedIngredients
    {
        public required string Name { get; set; }
        public int Quantity { get; set; }


    }
}
