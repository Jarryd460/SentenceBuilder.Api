using Application.Words.Queries.GetWords;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using SentenceBuilder.Api.SwaggerExamples.Words;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SentenceBuilder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : ApiControllerBase
    {
        /// <summary>
        /// Gets all word matching the specified word type
        /// </summary>
        /// <param name="wordTypeId"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Returns all words matching specified word type <paramref name="wordTypeId"/></response>
        /// <response code="500">When something unexpected has happened</response>
        [HttpGet(Name = nameof(GetWords))]
        [ProducesResponseType(typeof(IEnumerable<WordDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(WordsInternalServerErrorResponseExample))]
        public async Task<ActionResult<IEnumerable<WordDto>>> GetWords([FromQuery] WordTypeEnum wordTypeId, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetWordsQuery()
            {
                WordTypeId = wordTypeId
            }, cancellationToken).ConfigureAwait(false);
        }
    }
}
