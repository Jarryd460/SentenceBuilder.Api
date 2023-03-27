using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SentenceBuilder.Api.SwaggerExamples;

public class BadRequestResponseExample : IExamplesProvider<ProblemDetails>
{
    public ProblemDetails GetExamples()
    {
        var problemDetails = new ProblemDetails()
        {
            Type = "https://httpstatuses.com/400",
            Title = nameof(HttpStatusCode.BadRequest),
            Status = (int)HttpStatusCode.BadRequest
        };

        problemDetails.Extensions.Add("traceId", "00-79e6196b264e3b1dd4d7fe270de6d6f6-73a34493e34cc20b-00");

        problemDetails.Extensions.Add("errors", new
        {
            Body = new List<string>()
                {
                    "A non-empty request body is required."
                }
        });


        return problemDetails;
    }
}
