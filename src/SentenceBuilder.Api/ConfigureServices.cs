namespace SentenceBuilder.Api
{
    internal static class ConfigureServices
    {
        public static IServiceCollection AddWebUIServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
