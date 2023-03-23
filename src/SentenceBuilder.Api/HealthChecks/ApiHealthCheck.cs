using Microsoft.Extensions.Diagnostics.HealthChecks;
using SentenceBuilder.Api.Helpers;

namespace SentenceBuilder.Api.HealthChecks
{
    internal sealed class ApiHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiHealthCheck(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using var httpClient = _httpClientFactory.CreateClient();

            var baseUrl = _httpContextAccessor.HttpContext.Request.BaseUrl();

            var response = await httpClient.GetAsync($"{baseUrl}api/information/version");

            if (response.IsSuccessStatusCode)
            {
                return await Task.FromResult(new HealthCheckResult(
                  status: HealthStatus.Healthy,
                  description: "The Api is up and running."));
            }

            return await Task.FromResult(new HealthCheckResult(
              status: HealthStatus.Unhealthy,
              description: "The Api is down."));
        }
    }
}
