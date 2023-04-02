using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Application.Sentences.Commands.DeleteSentence;

public record DeleteSentenceCommand : IRequest
{
    public long SentenceId { get; set; }
}

public class DeleteSentenceCommandHandler : IRequestHandler<DeleteSentenceCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger _logger;

    public DeleteSentenceCommandHandler(IApplicationDbContext context, IMapper mapper, ILogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(DeleteSentenceCommand request, CancellationToken cancellationToken)
    {
        var sentence = await _context.Sentences.SingleOrDefaultAsync(sentence => sentence.Id == request.SentenceId, cancellationToken).ConfigureAwait(false);

        if (sentence is not null)
        {
            _context.Sentences.Remove(sentence);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            _logger.Information("Sentence with Id {Id} has been deleted", sentence.Id);
        }
    }
}
