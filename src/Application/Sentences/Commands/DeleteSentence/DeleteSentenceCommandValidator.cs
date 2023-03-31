using FluentValidation;

namespace Application.Sentences.Commands.DeleteSentence;

public class DeleteSentenceCommandValidator : AbstractValidator<DeleteSentenceCommand>
{
    public DeleteSentenceCommandValidator()
    {
        RuleFor(deleteSentenceCommand => deleteSentenceCommand.SentenceId).NotNull();
    }
}
