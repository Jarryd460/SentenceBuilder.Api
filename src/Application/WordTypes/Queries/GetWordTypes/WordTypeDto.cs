using Application.Common.Mappings;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.WordTypes.Queries.GetWordTypes;

public class WordTypeDto : IMapFrom<WordType>
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    /// <example>1</example>
    [Required]
    public int Id { get; set; }
    /// <summary>
    /// Word type
    /// </summary>
    /// <example>Noun</example>
    [Required]
    public required string Value { get; set; }
}
