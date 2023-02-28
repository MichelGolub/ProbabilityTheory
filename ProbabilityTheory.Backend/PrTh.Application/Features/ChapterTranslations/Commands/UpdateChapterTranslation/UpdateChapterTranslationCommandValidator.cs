using FluentValidation;

namespace PrTh.Application.Features.ChapterTranslations.Commands.UpdateChapterTranslation
{
    public class UpdateChapterTranslationCommandValidator
        : AbstractValidator<UpdateChapterTranslationCommand>
    {
        public UpdateChapterTranslationCommandValidator()
        {
            RuleFor(command => command.ChapterId).NotEqual(Guid.Empty);
            RuleFor(command => command.LanguageId).NotEqual(Guid.Empty);
            RuleFor(command => command.Title).MaximumLength(250);
        }
    }
}
