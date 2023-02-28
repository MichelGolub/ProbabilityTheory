using FluentValidation;

namespace PrTh.Application.Features.Chapters.Queries.GetChapterDetails
{
    public class GetChapterDetailsQueryValidator 
        : AbstractValidator<GetChapterDetailsQuery>
    {
        public GetChapterDetailsQueryValidator()
        {
            RuleFor(query =>
                query.Id).NotEqual(Guid.Empty);
            RuleFor(query =>
                query.LanguageId).NotEqual(Guid.Empty);
        }
    }
}
