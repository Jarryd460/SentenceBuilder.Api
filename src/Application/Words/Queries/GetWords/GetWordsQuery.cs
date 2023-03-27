using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Words.Queries.GetWords;

public record GetWordsQuery : IRequest<List<WordDto>>
{
    public WordTypeEnum WordTypeId { get; init; }
}

public class GetWordsQueryHandler : IRequestHandler<GetWordsQuery, List<WordDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWordsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<WordDto>> Handle(GetWordsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Words
            .Where(word => word.WordTypeId == (int)request.WordTypeId)
            .AsNoTracking()
            .ProjectTo<WordDto>(_mapper.ConfigurationProvider)
            .OrderBy(wordType => wordType.Value)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}
