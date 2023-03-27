using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Net;

namespace SentenceBuilder.Api.SwaggerExamples.Sentences;

public class SentencesInternalServerErrorResponseExample : IExamplesProvider<ProblemDetails>
{
    public ProblemDetails GetExamples()
    {
        var problemDetails = new ProblemDetails()
        {
            Type = "https://httpstatuses.com/500",
            Title = nameof(HttpStatusCode.InternalServerError),
            Status = (int)HttpStatusCode.InternalServerError,
            Detail = "Something unexpected happened"
        };

        var exceptionDetails = new List<object>()
            {
                new {
                    Message = "Something unexpected happened",
                    Type = "System.Exception",
                    Raw = "System.Exception: Something unexpected happened\r\n   at SentenceBuilder.Api.Controllers.SentencesController.GetSentences....",
                    StackFrames = new List<object>()
                    {
                        new {
                            FilePath = "C:\\Users\\jdeane\\source\\repos\\SentenceBuilder.Api\\SentenceBuilder.Api\\Controllers\\SentencesController.cs",
                            FileName = "SentencesController.cs",
                            Function = "SentenceBuilder.Api.Controllers.SentencesController.GetSentences(CancellationToken cancellationToken)",
                            Line = 24,
                            PreContextLine = 20,
                            PreContextCode = new List<string>()
                            {
                                "        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.InternalServerError)]",
                                "        [SwaggerResponseExample((int)HttpStatusCode.InternalServerError, typeof(SentencesInternalServerErrorResponseExample))]",
                                "        public async Task<ActionResult<IEnumerable<SentencesDto>>> GetSentences(CancellationToken cancellationToken)",
                                "        {"
                            },
                            ContextCode = new List<string>()
                            {
                                "            throw new Exception(\"Something unexpected happened\");"
                            },
                            PostContextCode = new List<string>()
                            {
                                "            return await Mediator.Send(new GetSentencesQuery(), cancellationToken).ConfigureAwait(false);",
                                "        }",
                                "",
                                "        /// <summary>"
                            }
                        },
                        new
                        {
                              FilePath = "",
                              FileName = "",
                              Function = "lambda_method274(Closure , object )",
                              Line = default(int),
                              PreContextLine = default(int),
                              PreContextCode = new List<string>(),
                              ContextCode = new List<string>(),
                              PostContextCode = new List<string>()
                        }
                    }
                }
            };

        problemDetails.Extensions.Add("exceptionDetails", exceptionDetails);

        problemDetails.Extensions.Add("traceId", "00-79e6196b264e3b1dd4d7fe270de6d6f6-73a34493e34cc20b-00");

        return problemDetails;
    }
}
