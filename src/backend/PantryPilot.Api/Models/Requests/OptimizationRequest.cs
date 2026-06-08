namespace PantryPilot.Api.Models.Requests
{

    public class IngredientQuantity
    {
        public required int IngredientId { get; set; }
        public required int Quantity { get; set; }
    }

    public class OptimizationRequest
    {
        public required List<IngredientQuantity> Ingredients { get; set; }
    }
}
