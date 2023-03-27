using Application.Information.Queries.GetVersion;
using MediatR;
using System.Reflection;

namespace Application.Information.Queries.GetApiVersion;

public record GetApiVersionQuery : IRequest<ApiVersionDto>
{
}

public class GetVersionQueryHandler : IRequestHandler<GetApiVersionQuery, ApiVersionDto>
{
    public GetVersionQueryHandler()
    {
    }

    public async Task<ApiVersionDto> Handle(GetApiVersionQuery request, CancellationToken cancellationToken)
    {
        return new ApiVersionDto()
        {
            Version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion
        };
    }
}
