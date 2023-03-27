using Application.Common.Mappings;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Sentences.Queries.GetSentences;

public class SentenceDto : IMapFrom<Sentence>
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    /// <example>1</example>
    [Required]
    public int Id { get; set; }
    public IList<SentenceWordDto> SentencesWords { get; set; }
}
