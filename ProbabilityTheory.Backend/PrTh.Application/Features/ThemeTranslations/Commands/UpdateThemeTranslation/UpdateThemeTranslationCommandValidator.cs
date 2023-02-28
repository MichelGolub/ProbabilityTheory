using FluentValidation;

namespace PrTh.Application.Features.ThemeTranslations.Commands.UpdateThemeTranslation
{
    public class UpdateThemeTranslationCommandValidator
        : AbstractValidator<UpdateThemeTranslationCommand>
    {
        public UpdateThemeTranslationCommandValidator()
        {
            RuleFor(command => command.ThemeId).NotEqual(Guid.Empty);
            RuleFor(command => command.LanguageId).NotEqual(Guid.Empty);
            RuleFor(command => command.Title).MaximumLength(250);
        }
    }
}
