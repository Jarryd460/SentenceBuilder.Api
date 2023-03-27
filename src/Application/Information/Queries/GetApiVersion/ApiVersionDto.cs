using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Information.Queries.GetVersion;

public class ApiVersionDto : IMapFrom<ApiVersion>
{
    /// <summary>
    /// Sentence Builder Api version
    /// </summary>
    /// <example>1.0.0</example>
    public string Version { get; set; }
}
