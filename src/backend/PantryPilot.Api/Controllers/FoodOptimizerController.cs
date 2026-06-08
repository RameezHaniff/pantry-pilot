using Microsoft.AspNetCore.Mvc;
using PantryPilot.Api.Interfaces;
using PantryPilot.Api.Models.Requests;
using PantryPilot.Api.Models.Responses;

[ApiController]
[Route("api/[controller]")]
public class FoodOptimizerController(IFoodOptimizationService service) : ControllerBase
{
    private readonly IFoodOptimizationService _service = service;

    [HttpPost()]
    public async Task<ActionResult<OptimizationResponse>> Optimize([FromBody] OptimizationRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.OptimizeAsync(request, cancellationToken);
        return Ok(result);
    }
}