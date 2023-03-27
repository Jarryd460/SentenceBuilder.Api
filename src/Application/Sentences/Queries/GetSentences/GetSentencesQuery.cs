using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Sentences.Queries.GetSentences;

public record GetSentencesQuery : IRequest<List<SentenceDto>>
{
}

public class GetSentencesQueryHandler : IRequestHandler<GetSentencesQuery, List<SentenceDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSentencesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SentenceDto>> Handle(GetSentencesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Sentences
            .AsNoTracking()
            .ProjectTo<SentenceDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
    }
}