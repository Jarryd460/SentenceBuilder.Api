using FluentValidation;

namespace Application.Sentences.Commands.CreateSentence;

public class CreateSentenceCommandValidator : AbstractValidator<CreateSentenceCommand>
{
    public CreateSentenceCommandValidator()
    {
        RuleFor(createSentenceCommand => createSentenceCommand.Sentence).NotNull();

        RuleFor(createSentenceCommand => createSentenceCommand.Sentence.SentenceWords).NotEmpty();
    }
}
