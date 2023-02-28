using FluentValidation;

namespace PrTh.Application.Features.TermTranslations.Commands.UpdateTermTranslation
{
    public class UpdateTermTranslationCommandValidator
        : AbstractValidator<UpdateTermTranslationCommand>
    {
        public UpdateTermTranslationCommandValidator()
        {
            RuleFor(c => c.TermId)
                .NotEqual(Guid.Empty);
            RuleFor(c => c.LanguageId)
                .NotEqual(Guid.Empty);
            RuleFor(c => c.Title)
                .MaximumLength(50);
            RuleFor(c => c.Definition)
                .MaximumLength(1000);
        }
    }
}
