using Application.WordTypes.Queries.GetWordTypes;
using Microsoft.AspNetCore.Mvc;
using SentenceBuilder.Api.SwaggerExamples.WordTypes;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SentenceBuilder.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WordTypesController : ApiControllerBase
{
    /// <summary>
    /// Gets all word types
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Returns all word types</response>
    /// <response code="500">When something unexpected has happened</response>
    [HttpGet(Name = nameof(GetWordTypes))]
    [ProducesResponseType(typeof(IEnumerable<WordTypeDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
    [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(WordTypesInternalServerErrorResponseExample))]
    public async Task<ActionResult<IEnumerable<WordTypeDto>>> GetWordTypes(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetWordTypesQuery(), cancellationToken).ConfigureAwait(false);
    }
}
