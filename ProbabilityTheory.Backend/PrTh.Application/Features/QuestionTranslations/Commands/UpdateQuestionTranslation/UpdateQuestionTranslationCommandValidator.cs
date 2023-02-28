using FluentValidation;

namespace PrTh.Application.Features.QuestionTranslations.Commands.UpdateQuestionTranslation
{
    public class UpdateQuestionTranslationCommandValidator
        : AbstractValidator<UpdateQuestionTranslationCommand>
    {
        public UpdateQuestionTranslationCommandValidator()
        {
            RuleFor(command => command.LanguageId).NotEqual(Guid.Empty);
            RuleFor(command => command.QuestionId).NotEqual(Guid.Empty);
            RuleFor(command => command.Description).MaximumLength(3000);
            RuleFor(command => command.Answer).MaximumLength(3000);
        }
    }
}
