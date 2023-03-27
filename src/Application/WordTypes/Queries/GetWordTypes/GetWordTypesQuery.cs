using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WordTypes.Queries.GetWordTypes;

public record GetWordTypesQuery : IRequest<List<WordTypeDto>>
{
}

public class GetWordTypesQueryHandler : IRequestHandler<GetWordTypesQuery, List<WordTypeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWordTypesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<WordTypeDto>> Handle(GetWordTypesQuery request, CancellationToken cancellationToken)
    {
        return await _context.WordTypes
            .AsNoTracking()
            .ProjectTo<WordTypeDto>(_mapper.ConfigurationProvider)
            .OrderBy(wordType => wordType.Value)
            .ToListAsync(cancellationToken);
    }
}
