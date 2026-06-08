using PantryPilot.Api.Models.Responses;

namespace PantryPilot.Api.Interfaces
{
    public interface IIngredientService
    {
        public Task<List<IngredientResponse>> GetAllAsync(CancellationToken cancellationToken = default);

    }
}
