using Microsoft.EntityFrameworkCore;
using PantryPilot.Api.Data;
using PantryPilot.Api.Interfaces;
using PantryPilot.Api.Models.Requests;
using PantryPilot.Api.Models.Responses;

namespace PantryPilot.Api.Services
{
    public class FoodOptimizationService(FoodDbContext db) : IFoodOptimizationService
    {
        private readonly FoodDbContext _db = db;

        public async Task<OptimizationResponse> OptimizeAsync(
            OptimizationRequest request,
            CancellationToken cancellationToken = default)
        {

            if (request.Ingredients == null || !request.Ingredients.Any())
            {
                throw new ArgumentException("At least one ingredient must be provided.");
            }

            if (request.Ingredients.Any(i => i.Quantity < 0))
            {
                throw new ArgumentException("Ingredient quantities cannot be negative.");
            }

            if (request.Ingredients.GroupBy(i => i.IngredientId).Any(g => g.Count() > 1))
            {
                throw new ArgumentException("Duplicate ingredients are not allowed.");
            }

            var available = request.Ingredients.ToDictionary(i => i.IngredientId, i => i.Quantity);

            var recipes = await _db.Recipes
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri => ri.Ingredient)
                .AsNoTracking()
                .Where(x => x.RecipeIngredients.Any())
                .ToListAsync(cancellationToken);

            var recipeInfos = recipes.Select(r => new
            {
                Recipe = r,
                FeedsPerUnit = (double)r.Feeds / r.RecipeIngredients.Sum(ri => ri.Quantity)
            })
            .OrderByDescending(x => x.FeedsPerUnit)
            .ToList();

            var result = new List<RecipeResult>();
            int totalFed = 0;

            foreach (var info in recipeInfos)
            {
                int maxCount = int.MaxValue;
                foreach (var ri in info.Recipe.RecipeIngredients)
                {
                    if (!available.TryGetValue(ri.Ingredient.Id, out var avail) || ri.Quantity == 0)
                    {
                        maxCount = 0;
                        break;
                    }
                    maxCount = Math.Min(maxCount, avail / ri.Quantity);
                }
                if (maxCount > 0)
                {
                    foreach (var ri in info.Recipe.RecipeIngredients)
                        available[ri.Ingredient.Id] -= ri.Quantity * maxCount;

                    result.Add(new RecipeResult()
                    {
                        Name = info.Recipe.Name,
                        Quantity = maxCount,
                        Ingredients = info.Recipe.RecipeIngredients.Select(ri => new RecipeIngredientResult
                        {
                            Name = ri.Ingredient.Name,
                            TotalUsed = ri.Quantity * maxCount,
                            QuantityPerServing = ri.Quantity
                        }).ToList()
                    });
                    totalFed += info.Recipe.Feeds * maxCount;
                }
            }

            var allIngredients = await _db.Ingredients.AsNoTracking().ToListAsync(cancellationToken);

            var unusedIngredients = allIngredients
                .Where(i => available.TryGetValue(i.Id, out var qty) && qty > 0)
                .Select(i => new UnusedIngredients
                {
                    Name = i.Name,
                    Quantity = available[i.Id]
                })
                .ToList();

            return new OptimizationResponse()
            {
                MaxPeopleFed = totalFed,
                Recipes = result,
                UnusedIngredients = unusedIngredients
            };
        }

    }
}
