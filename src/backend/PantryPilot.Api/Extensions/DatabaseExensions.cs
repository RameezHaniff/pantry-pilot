using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PantryPilot.Api.Data;

namespace PantryPilot.Api.Extensions
{
    public static class DatabaseExensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FoodDbContext>(options =>
            options.UseSqlite(
                    configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
