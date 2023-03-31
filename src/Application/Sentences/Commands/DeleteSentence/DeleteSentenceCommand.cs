using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Sentences.Commands.DeleteSentence;

public record DeleteSentenceCommand : IRequest
{
    public long SentenceId { get; set; }
}

public class DeleteSentenceCommandHandler : IRequestHandler<DeleteSentenceCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeleteSentenceCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(DeleteSentenceCommand request, CancellationToken cancellationToken)
    {
        var sentence = await _context.Sentences.SingleOrDefaultAsync(sentence => sentence.Id == request.SentenceId, cancellationToken).ConfigureAwait(false);

        if (sentence is not null)
        {
            _context.Sentences.Remove(sentence);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
