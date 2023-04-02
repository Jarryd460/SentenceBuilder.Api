using Application.Common.Interfaces;
using Application.Sentences.Queries.GetSentences;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Application.Sentences.Commands.CreateSentence;

public record CreateSentenceCommand : IRequest<SentenceDto>
{
    public CreateSentenceDto Sentence { get; set; }
}

public class CreateSentenceCommandHandler : IRequestHandler<CreateSentenceCommand, SentenceDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public CreateSentenceCommandHandler(IApplicationDbContext context, IMapper mapper, ILogger logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<SentenceDto> Handle(CreateSentenceCommand request, CancellationToken cancellationToken)
    {
        IList<SentenceWord> sentenceWords = new List<SentenceWord>();

        foreach (var createSentenceWord in request.Sentence.SentenceWords)
        {
            sentenceWords.Add(new SentenceWord()
            {
                Order = createSentenceWord.Order,
                WordId = createSentenceWord.WordId
            });
        }

        var entity = new Sentence()
        {
            SentencesWords = sentenceWords
        };

        _context.Sentences.Add(entity);

        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        _logger.Information("Sentence with Id {Id} has been created", entity.Id);

        return await _context.Sentences
            .AsNoTracking()
            .ProjectTo<SentenceDto>(_mapper.ConfigurationProvider)
            .SingleAsync(sentence => sentence.Id == entity.Id, cancellationToken).ConfigureAwait(false);
    }
}
