using FluentValidation;

namespace PrTh.Application.Features.Pages.Queries.GetPageDetails
{
    public class GetPageDetailsQueryValidator 
        : AbstractValidator<GetPageDetailsQuery>
    {
        public GetPageDetailsQueryValidator()
        {
            RuleFor(query =>
                query.Id).NotEqual(Guid.Empty);
            RuleFor(query =>
                query.LanguageId).NotEqual(Guid.Empty);
        }
    }
}
