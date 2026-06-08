using PantryPilot.Api.Models.Requests;
using PantryPilot.Api.Models.Responses;

namespace PantryPilot.Api.Interfaces
{
    public interface IFoodOptimizationService
    {
        Task<OptimizationResponse> OptimizeAsync(OptimizationRequest request, CancellationToken cancellationToken = default);
    }

}
