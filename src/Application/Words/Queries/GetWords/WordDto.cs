using Application.Common.Mappings;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Words.Queries.GetWords;

public class WordDto : IMapFrom<Word>
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    /// <example>1</example>
    [Required]
    public int Id { get; set; }
    /// <summary>
    /// Word
    /// </summary>
    /// <example>You</example>
    [Required]
    public string Value { get; set; }
    /// <summary>
    /// Word type Id
    /// </summary>
    /// <example>3</example>
    [Required]
    public required string WordTypeId { get; set; }
}
