using FluentValidation;

namespace PrTh.Application.Features.Themes.Queries.GetThemeList
{
    public class GetThemeListQueryValidator 
        : AbstractValidator<GetThemeListQuery>
    {
        public GetThemeListQueryValidator()
        {
            RuleFor(query => query.LanguageId)
                .NotEqual(Guid.Empty);
        }
    }
}
