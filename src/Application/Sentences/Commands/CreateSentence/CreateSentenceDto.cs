namespace Application.Sentences.Commands.CreateSentence;

public class CreateSentenceDto
{
    /// <summary>
    /// List of words in sentence
    /// </summary>
    public IList<CreateSentenceWordDto> SentenceWords { get; set; }
}
