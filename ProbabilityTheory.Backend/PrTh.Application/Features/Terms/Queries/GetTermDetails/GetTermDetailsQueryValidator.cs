using FluentValidation;

namespace PrTh.Application.Features.Terms.Queries.GetTermDetails
{
    public class GetTermDetailsQueryValidator 
        : AbstractValidator<GetTermDetailsQuery>
    {
        public GetTermDetailsQueryValidator()
        {
            RuleFor(q => q.LanguageId)
                .NotEqual(Guid.Empty);
            RuleFor(q => q.TermId)
                .NotEqual(Guid.Empty);
        }
    }
}
