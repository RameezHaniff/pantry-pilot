using Microsoft.EntityFrameworkCore;
using PantryPilot.Api.Data;

namespace PantryPilot.Api.Extensions
{
    public static class DatabaseExensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<FoodDbContext>(options =>
                options.UseSqlite("Data Source=pantrypilot.db"));

            return services;
        }
    }
}
