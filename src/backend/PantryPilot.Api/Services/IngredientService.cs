using Microsoft.EntityFrameworkCore;
using PantryPilot.Api.Data;
using PantryPilot.Api.Interfaces;
using PantryPilot.Api.Models.Responses;

namespace PantryPilot.Api.Services
{
    public class IngredientService(FoodDbContext db) : IIngredientService
    {

        private readonly FoodDbContext _db = db;

        public async Task<List<IngredientResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var ingredients = await _db.Ingredients.AsNoTracking()
                .Select(x => new IngredientResponse
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync(cancellationToken);

            return ingredients;
        }


    }
}
