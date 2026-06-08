namespace PantryPilot.Api.Extensions
{
    public static class CorsExtensions
    {
        private const string PolicyName = "AllowAngular";

        public static IServiceCollection AddCorsPolicy(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(PolicyName, policy =>
                {
                    policy.WithOrigins(
                            configuration.GetSection("Cors:AllowedOrigins")
                                .Get<string[]>() ?? [])
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            return services;
        }

        public static IApplicationBuilder UseCorsPolicy(
            this IApplicationBuilder app)
        {
            app.UseCors(PolicyName);
            return app;
        }
    }
}