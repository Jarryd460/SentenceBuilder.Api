using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Application.Sentences.Queries.GetSentences;

public record GetSentencesQuery : IRequest<List<SentenceDto>>
{
}

public class GetSentencesQueryHandler : IRequestHandler<GetSentencesQuery, List<SentenceDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetSentencesQueryHandler(IApplicationDbContext context, IMapper mapper, ILogger logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<SentenceDto>> Handle(GetSentencesQuery request, CancellationToken cancellationToken)
    {
        _logger.Information("Retrieving sentences");

        return await _context.Sentences
            .AsNoTracking()
            .ProjectTo<SentenceDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}