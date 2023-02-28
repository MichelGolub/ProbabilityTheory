using FluentValidation;

namespace PrTh.Application.Features.Questions.Queries.GetQuestionDetails
{
    public class GetQuestionDetailsQueryValidator
        : AbstractValidator<GetQuestionDetailsQuery>
    {
        public GetQuestionDetailsQueryValidator()
        {
            RuleFor(query =>
                query.Id).NotEqual(Guid.Empty);
            RuleFor(query =>
                query.LanguageId).NotEqual(Guid.Empty);
        }
    }
}
