using FluentValidation;

namespace PrTh.Application.Features.PageTranslations.Commands.UpdatePageTranslation
{
    public class UpdatePageTranslationCommandValidator
        : AbstractValidator<UpdatePageTranslationCommand>
    {
        public UpdatePageTranslationCommandValidator()
        {
            RuleFor(command => command.LanguageId).NotEqual(Guid.Empty);
            RuleFor(command => command.PageId).NotEqual(Guid.Empty);
            RuleFor(command => command.Content).MaximumLength(3000);
        }
    }
}
