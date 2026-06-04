using Microsoft.AspNetCore.Mvc;
using PantryPilot.Api.Interfaces;
using PantryPilot.Api.Models.Responses;


[ApiController]
[Route("api/[controller]")]
public class IngredientController(IIngredientService service) : ControllerBase
{
    private readonly IIngredientService _service = service;

    [HttpGet]
    public async Task<ActionResult<List<IngredientResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(cancellationToken);
        return Ok(result);
    }

}

