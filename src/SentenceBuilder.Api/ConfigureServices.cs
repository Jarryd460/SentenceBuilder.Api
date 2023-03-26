using Application.Common.Interfaces;
using FluentValidation.AspNetCore;
using Infrastructure.Persistence;
using SentenceBuilder.Api.HealthChecks;
using SentenceBuilder.Api.Services;

namespace SentenceBuilder.Api;

internal static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddCheck<ApiHealthCheck>(nameof(ApiHealthCheck))
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddHealthChecksUI()
            .AddSqliteStorage(configuration.GetConnectionString("HealthChecks")!);

        services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();

        return services;
    }
}
