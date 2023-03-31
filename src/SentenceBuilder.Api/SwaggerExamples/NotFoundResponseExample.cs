using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace Weather.Api.SwaggerExamples
{
    public class NotFoundResponseExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples()
        {
            var problemDetails = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = nameof(HttpStatusCode.NotFound),
                Status = (int)HttpStatusCode.NotFound
            };

            problemDetails.Extensions.Add("traceId", "00-79e6196b264e3b1dd4d7fe270de6d6f6-73a34493e34cc20b-00");

            return problemDetails;
        }
    }
}