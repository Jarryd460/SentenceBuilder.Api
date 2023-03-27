using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SentenceBuilder.Api.SwaggerExamples.Information;

public class VersionInternalServerErrorResponseExample : IExamplesProvider<ProblemDetails>
{
    public ProblemDetails GetExamples()
    {
        var problemDetails = new ProblemDetails()
        {
            Type = "https://httpstatuses.com/500",
            Title = nameof(HttpStatusCode.InternalServerError),
            Status = (int)HttpStatusCode.InternalServerError,
            Detail = "Missing map from Domain.Entities.ApiVersion to Application.Information.Queries.GetApiVersion.ApiVersionDto. Create using CreateMap<ApiVersoin, ApiVersionDto>."
        };

        problemDetails.Extensions.Add("traceId", "00-79e6196b264e3b1dd4d7fe270de6d6f6-73a34493e34cc20b-00");

        return problemDetails;
    }
}

