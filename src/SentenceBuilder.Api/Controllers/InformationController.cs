using Application.Information.Queries.GetApiVersion;
using Application.Information.Queries.GetVersion;
using Microsoft.AspNetCore.Mvc;
using SentenceBuilder.Api.SwaggerExamples.Information;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SentenceBuilder.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InformationController : ApiControllerBase
{
    /// <summary>
    /// Gets Api version
    /// </summary>
    /// <response code="200">Returns Api version</response>
    /// <response code="500">When something unexpected has happened</response>
    /// <param name="cancellationToken"></param>
    [HttpGet("version", Name = nameof(GetVersion))]
    [ProducesResponseType(typeof(ApiVersionDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(VersionInternalServerErrorResponseExample))]
    public async Task<ActionResult<ApiVersionDto>> GetVersion(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetApiVersionQuery(), cancellationToken).ConfigureAwait(false);
    }
}
