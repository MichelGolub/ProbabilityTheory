using FluentValidation;

namespace PrTh.Application.Features.ExerciseTranslations.Commands.UpdateExerciseTranslation
{
    public class UpdateExerciseTranslationCommandValidator
        : AbstractValidator<UpdateExerciseTranslationCommand>
    {
        public UpdateExerciseTranslationCommandValidator()
        {
            RuleFor(command => command.ExerciseId).NotEqual(Guid.Empty);
            RuleFor(command => command.LanguageId).NotEqual(Guid.Empty);
            RuleFor(command => command.Description).MaximumLength(2500);
        }
    }
}
