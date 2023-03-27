using System.ComponentModel.DataAnnotations;

namespace Application.Sentences.Commands.CreateSentence;

public class CreateSentenceWordDto
{
    /// <summary>
    /// Order position of word in sentence with 1 indicating the start
    /// </summary>
    /// <example>1</example>
    [Required]
    public int Order { get; set; }
    /// <summary>
    /// Unique identifier of the word
    /// </summary>
    /// <example>1</example>
    [Required]
    public int WordId { get; set; }
}
