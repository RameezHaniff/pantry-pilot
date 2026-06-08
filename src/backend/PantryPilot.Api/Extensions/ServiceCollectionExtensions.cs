using PantryPilot.Api.Interfaces;
using PantryPilot.Api.Services;

namespace PantryPilot.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IFoodOptimizationService, FoodOptimizationService>();
            services.AddScoped<IIngredientService, IngredientService>();

            return services;
        }
    }
}
