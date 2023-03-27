using FluentValidation;

namespace Application.Words.Queries.GetWords;

public class GetWordsQueryValidator : AbstractValidator<GetWordsQuery>
{
    public GetWordsQueryValidator()
    {
        RuleFor(getWordsQuery => getWordsQuery.WordTypeId).IsInEnum();
    }
}
