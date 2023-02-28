using FluentValidation;

namespace PrTh.Application.Features.Terms.Queries.GetTermList
{
    public class GetTermListQueryValidator 
        : AbstractValidator<GetTermListQuery>
    {
        public GetTermListQueryValidator()
        {
            RuleFor(q => q.LanguageId)
                .NotEqual(Guid.Empty);
        }
    }
}
