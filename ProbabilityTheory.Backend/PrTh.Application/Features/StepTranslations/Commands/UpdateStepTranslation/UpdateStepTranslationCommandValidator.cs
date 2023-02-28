using FluentValidation;

namespace PrTh.Application.Features.StepTranslations.Commands.UpdateStepTranslation
{
    public class UpdateStepTranslationCommandValidator
        : AbstractValidator<UpdateStepTranslationCommand>
    {
        public UpdateStepTranslationCommandValidator()
        {
            RuleFor(command => command.LanguageId).NotEqual(Guid.Empty);
            RuleFor(command => command.StepId).NotEqual(Guid.Empty);
        }
    }
}
