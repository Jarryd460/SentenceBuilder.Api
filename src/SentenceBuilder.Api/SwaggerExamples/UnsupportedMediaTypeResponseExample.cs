using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SentenceBuilder.Api.SwaggerExamples;

public class UnsupportedMediaTypeResponseExample : IExamplesProvider<ProblemDetails>
{
    public ProblemDetails GetExamples()
    {
        var problemDetails = new ProblemDetails()
        {
            Type = "https://httpstatuses.com/415",
            Title = nameof(HttpStatusCode.UnsupportedMediaType),
            Status = (int)HttpStatusCode.UnsupportedMediaType
        };

        problemDetails.Extensions.Add("traceId", "00-79e6196b264e3b1dd4d7fe270de6d6f6-73a34493e34cc20b-00");

        return problemDetails;
    }
}
