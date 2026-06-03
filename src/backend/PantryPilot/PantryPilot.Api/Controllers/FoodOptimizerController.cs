using Microsoft.AspNetCore.Mvc;
using PantryPilot.Api.Interfaces;
using PantryPilot.Api.Models.Requests;
using PantryPilot.Api.Models.Responses;

[ApiController]
[Route("api/[controller]")]
public class FoodOptimizerController : ControllerBase
{
    private readonly IFoodOptimizationService _service;

    public FoodOptimizerController(IFoodOptimizationService service)
    {
        _service = service;
    }

    [HttpPost("optimize")]
    public async Task<ActionResult<OptimizationResponse>> Optimize([FromBody] OptimizationRequest request, CancellationToken cancellationToken)
    {
        var result = await _service.OptimizeAsync(request, cancellationToken);
        return Ok(result);
    }
}