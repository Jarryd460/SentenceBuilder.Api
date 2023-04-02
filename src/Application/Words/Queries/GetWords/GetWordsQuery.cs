using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Application.Words.Queries.GetWords;

public record GetWordsQuery : IRequest<List<WordDto>>
{
    public WordTypeEnum WordTypeId { get; init; }
}

public class GetWordsQueryHandler : IRequestHandler<GetWordsQuery, List<WordDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetWordsQueryHandler(IApplicationDbContext context, IMapper mapper, ILogger logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<WordDto>> Handle(GetWordsQuery request, CancellationToken cancellationToken)
    {
        _logger.Information("Retrieving {wordTypeId} words", request.WordTypeId.ToString());

        return await _context.Words
            .Where(word => word.WordTypeId == (int)request.WordTypeId)
            .AsNoTracking()
            .ProjectTo<WordDto>(_mapper.ConfigurationProvider)
            .OrderBy(wordType => wordType.Value)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}
