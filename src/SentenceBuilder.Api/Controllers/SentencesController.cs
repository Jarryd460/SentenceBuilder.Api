using Application.Sentences.Commands.CreateSentence;
using Application.Sentences.Commands.DeleteSentence;
using Application.Sentences.Queries.GetSentences;
using Microsoft.AspNetCore.Mvc;
using SentenceBuilder.Api.SwaggerExamples;
using SentenceBuilder.Api.SwaggerExamples.Sentences;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Weather.Api.SwaggerExamples;

namespace SentenceBuilder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentencesController : ApiControllerBase
    {
        /// <summary>
        /// Gets all sentences
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Returns sentences</response>
        /// <response code="500">When something unexpected has happened</response>
        [HttpGet(Name = nameof(GetSentences))]
        [ProducesResponseType(typeof(IEnumerable<SentenceDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(SentencesInternalServerErrorResponseExample))]
        public async Task<ActionResult<IEnumerable<SentenceDto>>> GetSentences(CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetSentencesQuery(), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a sentence
        /// </summary>
        /// <param name="createSentenceDto"></param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/sentences
        ///     {
        ///        "SentenceWords": [
        ///             {
        ///                 "Order": 1,
        ///                 "WordId: 23
        ///             },
        ///             {
        ///                 "Order": 2,
        ///                 "WordId: 5
        ///             },
        ///             {
        ///                 "Order": 3,
        ///                 "WordId: 12
        ///             },
        ///             {
        ///                 "Order": 4,
        ///                 "WordId: 47
        ///             }
        ///        ]
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created sentence</response>
        /// <response code="400">If the item is null or the values of the fields are incorrect</response>
        /// <response code="415">When content type of request or response is not allowed</response>
        /// <response code="500">When something unexpected has happened</response>
        [HttpPost(Name = nameof(PostSentence))]
        [ProducesResponseType(typeof(SentenceDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(BadRequestResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnsupportedMediaType)]
        [SwaggerResponseExample((int)HttpStatusCode.UnsupportedMediaType, typeof(UnsupportedMediaTypeResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(SentencesInternalServerErrorResponseExample))]
        public async Task<ActionResult<SentenceDto>> PostSentence([FromBody, Required] CreateSentenceDto createSentenceDto, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new CreateSentenceCommand()
            {
                Sentence = createSentenceDto
            }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes a sentence
        /// </summary>
        /// <param name="sentenceId">The unique identifier of the sentence</param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/sentence/1
        ///
        /// </remarks>
        /// <response code="204">If the sentence was deleted</response>
        /// <response code="404">If the sentence with the specified id does not exist</response>
        /// <response code="415">When content type of request or response is not allowed</response>
        /// <response code="500">When something unexpected has happened</response>
        [HttpDelete("{sentenceId}", Name = nameof(DeleteSentence))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(NotFoundResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.UnsupportedMediaType)]
        [SwaggerResponseExample((int)HttpStatusCode.UnsupportedMediaType, typeof(UnsupportedMediaTypeResponseExample))]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]
        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(SentencesInternalServerErrorResponseExample))]
        public async Task<ActionResult> DeleteSentence([FromRoute, Required] long sentenceId, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteSentenceCommand()
            {
                SentenceId = sentenceId
            }, cancellationToken).ConfigureAwait(false);

            return NoContent();
        }
    }
}
