using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Application.WordTypes.Queries.GetWordTypes;

public record GetWordTypesQuery : IRequest<List<WordTypeDto>>
{
}

public class GetWordTypesQueryHandler : IRequestHandler<GetWordTypesQuery, List<WordTypeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public GetWordTypesQueryHandler(IApplicationDbContext context, IMapper mapper, ILogger logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<WordTypeDto>> Handle(GetWordTypesQuery request, CancellationToken cancellationToken)
    {
        _logger.Information("Retrieving word types");

        return await _context.WordTypes
            .AsNoTracking()
            .ProjectTo<WordTypeDto>(_mapper.ConfigurationProvider)
            .OrderBy(wordType => wordType.Value)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}
